using App.Domain.FeatureToggles;
using App.SharedKernel.Domain;
using App.SharedKernel.Guards;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Domain
{
    public class Application : Entity, ICreatedAt
    {
        private const string DEFAULT_ENV_NAME = "[default]";

        public DateTime CreatedAt { get; set; }

        public string Name { get; set; }

        public ICollection<UserApplication> Users { get; set; }

        public ICollection<ApplicationEnvironment> Environments { get; set; }

        public ICollection<FeatureToggle> FeatureToggles { get; set; }

        public virtual ApplicationAccessKey AccessKey { get; set; }

        public static Application New(User owner, string name)
        {
            Guard.IsNotNullOrEmpty(name, "The application should have a name");
            Guard.IsNotNull(owner, "Owner is null");

            var application = new Application()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Environments = new List<ApplicationEnvironment>(),
                CreatedAt = DateTime.UtcNow,
            };

            //Define owner for the application
            application.GrantPermissionForUser(owner, ApplicationPermissions.Owner);

            //Create default environment
            application.AddEnvironment(DEFAULT_ENV_NAME, true);

            //Create access key
            application.GenerateAccessKey();

            //Features
            application.FeatureToggles = new List<FeatureToggle>();

            return application;
        }

        public UserApplication GrantPermissionForUser(User user, ApplicationPermissions permission)
        {
            if (Users == null)
                Users = new List<UserApplication>();

            Guard.IsFalse(Users.Any(x => x.UserId == user.Id), "User already has permissions");

            var userApplication = UserApplication.GrantPermission(user, this, permission);
            Users.Add(userApplication);

            return userApplication;
        }

        public void RevokePermissionForUser(Guid userId)
        {
            Guard.IsNotNull(Users);

            var userApplication = Users.FirstOrDefault(x => x.UserId == userId && x.Permission != ApplicationPermissions.Owner);
            Guard.IsNotNull("The user does not exits");

            Users.Remove(userApplication);
        }

        public void RemoveEnvironment(ApplicationEnvironment environment)
        {
            Guard.IsNotNull(Environments, "Unload environments");
            Guard.IsTrue(Environments.Any(x => x.Id == environment.Id), "Invalid environment");
            Guard.IsFalse(environment.IsDefault, "You can't delete the default environment");

            Environments.Remove(environment);
        }

        public ApplicationEnvironment AddEnvironment(string name)
        {
            return AddEnvironment(name, false);
        }

        private ApplicationEnvironment AddEnvironment(string name, bool isDefault)
        {
            Guard.IsNotNull(Environments);
            Guard.IsFalse(Environments.Any(x => x.Name == name), "This environment already exits");

            var environment = ApplicationEnvironment.NewEnv(Id, name, isDefault);


            Environments.Add(environment);

            return environment;

        }

        public FeatureToggle AddNewFeatureToggle(string key, string description)
        {
            Guard.IsFalse(FeatureToggles.Any(x => x.Key == key), "Duplicated key for this application");

            var featureToggle = FeatureToggle.New(key, description, this);
            FeatureToggles.Add(featureToggle);

            return featureToggle;
        }

        public FeatureToggle AddNewFeatureToggleForDefaultEnv(string key, string description, bool enabled)
        {
            var defaultEnv = Environments.FirstOrDefault(x => x.IsDefault);

            Guard.IsNotNull(defaultEnv, "Invalid default environment for this application");

            var featureToggle = AddNewFeatureToggle(key, description);
            var appVal = defaultEnv.AddOrEditFeatureToggleValue(featureToggle.Id, enabled);

            return featureToggle;
        }

        public void GenerateAccessKey()
        {
            if (AccessKey == null)
                AccessKey = ApplicationAccessKey.NewKey(Id);
            else
                AccessKey.Generate();
        }

        public ApplicationEnvironment GetDefaultEnvironment()
        {
            return Environments.First(e => e.Name == DEFAULT_ENV_NAME);
        }
    }
}
