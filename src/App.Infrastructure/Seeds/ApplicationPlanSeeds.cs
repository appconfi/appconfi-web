using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Seeds
{
    public static class ApplicationPlanSeeds
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            //var Free = new ApplicationPlan
            //{
            //    Id = 100,
            //    Active = true,
            //    Name = "Free",
            //    NumberOfApplicationsAllowed = 2,
            //    NumberOfEnvironmentsAllowed = 3,
            //    NumberOfMembersAllowed = 1,
            //    DefaultPlan = true
            //};

            //var Starter = new ApplicationPlan
            //{
            //    Id = 101,
            //    Active = true,
            //    Name = "Starter",
            //    NumberOfApplicationsAllowed = 5,
            //    NumberOfEnvironmentsAllowed = 10,
            //    NumberOfMembersAllowed = 5,
            //};

            //var Growth = new ApplicationPlan
            //{
            //    Id = 102,
            //    Active = true,
            //    Name = "Growth",
            //    NumberOfApplicationsAllowed = 20,
            //    NumberOfEnvironmentsAllowed = 20,
            //    NumberOfMembersAllowed = 30,
            //};

            //var King = new ApplicationPlan
            //{
            //    Id = 103,
            //    Active = true,
            //    Name = "King",
            //    NumberOfApplicationsAllowed = null,
            //    NumberOfEnvironmentsAllowed = null,
            //    NumberOfMembersAllowed = null,
            //};

            //modelBuilder.Entity<ApplicationPlan>(b => {
            //    b.HasData(Free);
            //    b.OwnsOne(e => e.Price).HasData(Money.Euro(0));
            //});

            //modelBuilder.Entity<ApplicationPlan>(b => {
            //    b.HasData(Starter);
            //    b.OwnsOne(e => e.Price).HasData(Money.Euro(10));
            //});

            //modelBuilder.Entity<ApplicationPlan>(b => {
            //    b.HasData(Growth);
            //    b.OwnsOne(e => e.Price).HasData(Money.Euro(35));
            //});

            //modelBuilder.Entity<ApplicationPlan>(b => {
            //    b.HasData(King);
            //    b.OwnsOne(e => e.Price).HasData(Money.Euro(99));
            //});
        }
    }
}
