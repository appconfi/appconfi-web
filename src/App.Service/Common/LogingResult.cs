

namespace App.Service.Common
{
    using System;
    public class LogingResult
    {
        public bool ValidCredentials { get; set; }

        public Guid UserId { get; set; }

        public string Error { get; set; }
    }
}
