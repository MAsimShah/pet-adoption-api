using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetAdoption.Application.Interfaces;
using PetAdoption.Application.Mappings;
using PetAdoption.Application.Services;

namespace PetAdoption.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region services

            services.AddScoped<IPetService, PetService>();

            #endregion services

            #region Mappings

            services.AddAutoMapper(typeof(PetMap));

            #endregion Mappings

            return services;
        }
    }
}
