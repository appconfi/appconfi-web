using App.Infrastructure;
using App.Service.Accounts;
using App.Service.Applications;
using App.Service.Applications.Policies;
using App.Service.Common;
using App.Service.Email.SendGrid;
using App.Service.Importer;
using App.Service.Targeting;
using App.SharedKernel.Repository;
using App.Web.Auth;
using App.Web.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Web
{
    public static class DependencyInjection
    {
        public static void AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            //Configure
            services.AddOptions();
            services.Configure<SendGridConfig>(configuration.GetSection("SendGrid"));
            services.Configure<JWTConfig>(configuration.GetSection("JWT"));

            //IOC Singleton
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IUrlService>(new UrlService(configuration["BaseUrl"]));

            //IOC Services
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IEmailService, SendGridEmailService>();
            services.AddTransient<IAccountManager, AccountManager>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IApplicationService, ApplicationService>();
            services.AddTransient<IEnvironmentService, EnvironmentService>();
            services.AddTransient<IAccessKeysService, AccessKeysService>();
            services.AddTransient<IFeatureToggleService, FeatureToggleService>();
            services.AddTransient<IFeatureExporter, FeatureExporter>();
            services.AddTransient<IInvitationService, InvitationService>();
            services.AddTransient<IApplicationImporter, ApplicationImporter>();
            services.AddTransient<IImporterService, ImporterService>();
            services.AddTransient<IUserTargetingService, UserTargetingService>();
            services.AddTransient<IActivityService, ActivityService>();

            //IOC Policies
            services.AddTransient<IHasApplicationPermissionPolicy, HasApplicationPermissionPolicy>();
            services.AddTransient<IHasEnvironmentPermissionPolicy, HasEnvironmentPermissionPolicy>();
            services.AddTransient<IHasValidAccessKeyPolicy, HasValidAccessKeyPolicy>();
        }
    }
}
