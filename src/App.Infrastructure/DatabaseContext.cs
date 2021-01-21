using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace App.Infrastructure
{
    public class DatabaseContext : DbContext, IDataProtectionKeyContext
    {
        public DatabaseContext() { }

        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
