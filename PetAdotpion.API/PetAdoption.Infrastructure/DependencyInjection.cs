using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetAdoption.Application.Interfaces.InfrastructureInterfaces;
using PetAdoption.Domain;
using PetAdoption.Infrastructure.DbContextApp;
using PetAdoption.Infrastructure.Interfaces;
using PetAdoption.Infrastructure.Repositories;

namespace PetAdoption.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // identity configure
            services.AddIdentityCore<User>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false; // disables special characters
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4; // your custom minimum length
                options.Password.RequiredUniqueChars = 0; // optional: no unique character requirement
            })
                .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
            
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IAuthRepository, AuthRepository>();
            services.AddTransient<IPetRepository, PetRepository>();
            services.AddTransient<IPetPhotoRepository, PetPhotoRepository>();

            return services;
        }
    }
}
