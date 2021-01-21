using Microsoft.Extensions.Configuration;
using System;

namespace App.Web.Infrastructure
{
    public interface IConnectionStringProvider
    {
        string GetConnectionString();
    }

    public class ConnectionStringProvider : IConnectionStringProvider
    {
        private readonly IConfiguration configuration;

        public ConnectionStringProvider(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GetConnectionString()
        {
            var connection =
                    AreEnvVarConfigured

                    ? $"Server={Environment.GetEnvironmentVariable("POSTGRES_HOSTNAME")};Database={Environment.GetEnvironmentVariable("POSTGRES_DATABASE")};Port={Environment.GetEnvironmentVariable("POSTGRES_PORT")};User Id={Environment.GetEnvironmentVariable("POSTGRES_USER")};Password={Environment.GetEnvironmentVariable("POSTGRES_PASSWORD")};"

                    : configuration.GetConnectionString("DefaultConnection");

            return connection;
        }

        private bool AreEnvVarConfigured => !string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("POSTGRES_HOSTNAME"))
            && !string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("POSTGRES_PORT"))
            && !string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("POSTGRES_DATABASE"))
            && !string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("POSTGRES_USER"))
            && !string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("POSTGRES_PASSWORD"));
    }
}
