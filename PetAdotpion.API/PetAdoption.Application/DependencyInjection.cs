using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetAdoption.Application.DTO;
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

            AppSettingConfiguration? config = configuration.GetSection("AppSetting").Get<AppSettingConfiguration>() ?? new AppSettingConfiguration();
            services.AddSingleton<AppSettingConfiguration>(config);

            services.AddScoped<IPetService, PetService>();
            services.AddScoped<IPetPhotoService, PetPhotoService>();
            services.AddScoped<IPetRequestService, PetRequestService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IDashboardService, DashboardService>();

            #endregion services

            #region Mappings

            services.AddAutoMapper(typeof(PetMap));
            services.AddAutoMapper(typeof(PetPhotoMap));
            services.AddAutoMapper(typeof(PetRequestMap));
            services.AddAutoMapper(typeof(UserMap));

            #endregion Mappings

            return services;
        }
    }
}