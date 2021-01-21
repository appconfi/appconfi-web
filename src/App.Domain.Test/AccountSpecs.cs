using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Domain.Test
{
    [TestClass]
    public class AccountSpecs
    {
        [TestMethod]
        public void Given_a_recently_register_user_account_should_not_be_verified()
        {
            var user = User.Register("email@test.com", "qwerty", "Test", "User");
            Assert.IsFalse(user.Account.IsVerified);
        }

        [TestMethod]
        public void Given_a_register_user_account_should_have_salt()
        {
            var user = User.Register("email@test.com", "qwerty", "Test", "User");
            Assert.IsNotNull(user.Account.Salt);
        }

        [TestMethod]
        public void Given_a_register_user_should_have_a_valid_password()
        {
            var user = User.Register("email@test.com", "qwerty", "Test", "User");
            var account = user.Account;
            Assert.IsTrue(account.IsValidPassword("qwerty"));
        }
    }
}
