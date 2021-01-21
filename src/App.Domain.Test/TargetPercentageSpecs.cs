using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Domain.Test
{
    [TestClass]
    public class TargetPercentageSpecs
    {
        [TestMethod]
        public void Given_a_0_Percent_Should_Not_Be_Traget()
        {
            var userTargetting = new UserTargeting
            {
                EnvironmentId = System.Guid.Empty
            };
            var target = TargetPercentage.New(0, userTargetting);
            Assert.IsFalse(target.IsTarget(new TargetUser("12345678")));
        }

        [TestMethod]
        public void Given_a_100_Percent_Should_Be_Traget()
        {
            var userTargetting = new UserTargeting
            {
                EnvironmentId = System.Guid.Empty
            };
            var target = TargetPercentage.New(100, userTargetting);
            Assert.IsTrue(target.IsTarget(new TargetUser("12345678")));
        }

        [TestMethod]
        public void Given_a_50_Percent_Should_Not_Be_Traget()
        {
            var userTargetting = new UserTargeting
            {
                EnvironmentId = System.Guid.Empty
            };
            var target = TargetPercentage.New(50, userTargetting);
            Assert.IsFalse(target.IsTarget(new TargetUser("12345678")));
        }

        [TestMethod]
        public void Given_a_80_Percent_Should_Not_Be_Traget()
        {
            var userTargetting = new UserTargeting
            {
                EnvironmentId = System.Guid.Empty
            };
            var target = TargetPercentage.New(80, userTargetting);
            Assert.IsTrue(target.IsTarget(new TargetUser("12345678")));
        }
    }
}
