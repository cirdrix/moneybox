using System;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using MoneyBox.DataAccess.Mappings;
using MoneyBox.Domain;

namespace MoneyBox.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public Guid MyGuid { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            MyGuid = Guid.NewGuid();
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Box> Boxes { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
     
            modelBuilder.Entity<ApplicationUser>().ToTable("users");
            modelBuilder.Entity<IdentityUserRole>().ToTable("usersroles");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("userlogins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("userclaims");
            modelBuilder.Entity<IdentityRole>().ToTable("roles");

            modelBuilder.Configurations.Add(new BoxMap());
        }
    }
}