using App.SharedKernel.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Domain.Test
{
    [TestClass]
    public class UserSpecs
    {
        [TestMethod]
        public void Given_a_recently_register_should_have_and_account_associeted()
        {
            DomainEvents.Register((UserRegistered @event) => { });
            var user = User.Register("email@test.com", "qwerty", "Test", "User");

            Assert.IsNotNull(user.Account);
        }

        [TestMethod]
        public void Given_a_register_user_should_raise_a_domain_event()
        {
            UserRegistered e = null;
            DomainEvents.Register((UserRegistered @event) => { e = @event; });
            var user = User.Register("email@test.com", "qwerty", "Test", "User");

            Assert.IsNotNull(e);
            Assert.IsNotNull(e.User.Id);
        }
    }
}
