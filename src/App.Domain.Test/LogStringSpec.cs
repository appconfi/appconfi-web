using App.Domain.ActivityLogs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace App.Domain.Test
{
    [TestClass]
    public class LogStringSpec

    {
        [TestMethod]
        public void Given_args_Should_return_key_value_string()
        {
            var description = LogString.WithDescription(new Dictionary<string, string> { { "A", "a" }, { "B", "b" } });
            Assert.AreEqual("A:a, B:b", description);
        }

        [TestMethod]
        public void Given_empty_args_Should_return_empty_string()
        {
            var description = LogString.WithDescription(new Dictionary<string, string> { });
            Assert.AreEqual(string.Empty, description);
        }
    }
}
