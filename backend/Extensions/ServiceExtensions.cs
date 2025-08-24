using backend.Repositories;
using backend.Services;

namespace backend.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<IContribuyenteRepository, ContribuyenteRepository>();
            services.AddScoped<IComprobanteFiscalRepository, ComprobanteFiscalRepository>();

            // Services
            services.AddScoped<IContribuyenteService, ContribuyenteService>();
            services.AddScoped<IComprobanteFiscalService, ComprobanteFiscalService>();

            return services;
        }

        public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("http://localhost:3000")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            return services;
        }
    }
}