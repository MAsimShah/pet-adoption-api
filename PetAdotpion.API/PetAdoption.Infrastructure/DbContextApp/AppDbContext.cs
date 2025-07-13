using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetAdoption.Domain;

namespace PetAdoption.Infrastructure.DbContextApp
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Pets Table
        public DbSet<Pet> Pets { get; set; }
        public DbSet<PetPhoto> PetPhotos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Hardcoded password hash for password "admin"
            const string passwordHash = "AQAAAAEAACcQAAAAEGyPWqF74/ivwz6iKdN9UXeb1rSknH1XR+cvV3YfTnRDt8CvwP6UOwASgXcF3w==";

            // Seeding admin user
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = "a1111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa", // Static GUID as string
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@admin.com",
                    NormalizedEmail = "ADMIN@ADMIN.COM",
                    EmailConfirmed = true,
                    PasswordHash = passwordHash,
                    SecurityStamp = "STATIC_SECURITY_STAMP", // Must be static too
                    ConcurrencyStamp = "STATIC_CONCURRENCY_STAMP", // Must be static,
                    IsAdmin = true,
                }
            );


        }
    }
}
