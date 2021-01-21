using App.Service.Common;
using System;

namespace App.Web.Infrastructure
{
    public class UrlService : IUrlService
    {
        private readonly string baseUrl;

        public UrlService(string baseUrl)
        {
            this.baseUrl = baseUrl;
        }
        public string GetBaseUrl()
        {
            var url = Environment.GetEnvironmentVariable("BASE_URL");
            if (string.IsNullOrEmpty(url))
                url = baseUrl;

            return url;
        }
    }
}
