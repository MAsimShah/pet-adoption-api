using Microsoft.EntityFrameworkCore;
using PetAdoption.Domain;

namespace PetAdoption.Infrastructure.DbContextApp
{
    public class AppDbContext : DbContext //IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Pets Table
        public DbSet<Pet> Pets { get; set; }
        public DbSet<PetPhoto> PetPhotos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
