using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Domain.Test
{
    [TestClass]
    public class TargetSpecificUsersSpecs
    {
        [TestMethod]
        public void Should_be_target()
        {
            var userTargetting = new UserTargeting
            {
                EnvironmentId = System.Guid.Empty
            };
            var target = TargetSpecificUsers.New(userTargetting, "country", TargetOption.IsIn, "es,pl");
            var user = new TargetUser("1234556");
            user.Add("country", "es");
            var isTarget = target.IsTarget(user);
            Assert.IsTrue(isTarget);
        }

        [TestMethod]
        public void Should_not_be_target()
        {
            var userTargetting = new UserTargeting
            {
                EnvironmentId = System.Guid.Empty
            };
            var target = TargetSpecificUsers.New(userTargetting, "country", TargetOption.IsIn, "es,pl");
            var user = new TargetUser("1234556");
            user.Add("country", "cu");
            var isTarget = target.IsTarget(user);
            Assert.IsFalse(isTarget);
        }
    }
}
