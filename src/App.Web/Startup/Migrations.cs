using App.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace App.Web
{
    public static class Migrations
    {
        private const string IN_MEMORY_PROVIDER_NAME = "Microsoft.EntityFrameworkCore.InMemory";

        public static void RunMigrations(this IApplicationBuilder applicationBuilder)
        {

            using (var serviceScope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                    .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<DatabaseContext>())
                {
                    if (context.Database.ProviderName != IN_MEMORY_PROVIDER_NAME)
                        context.Database.Migrate();
                }
            }
        }
    }
}
