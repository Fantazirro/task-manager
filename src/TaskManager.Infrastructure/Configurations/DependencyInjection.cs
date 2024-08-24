using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Application.Interfaces.Data;
using TaskManager.Infrastructure.Authentication;
using TaskManager.Infrastructure.Persistence;
using TaskManager.Infrastructure.Persistence.Repositories;
using TaskManager.Application.Interfaces.Auth;
using TaskManager.Infrastructure.Persistence.Interceptors;
using Microsoft.AspNetCore.Http;

namespace TaskManager.Infrastructure.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services)
        {
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<AuditEntitiesInterceptor>();

            services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
            {
                var auditInterceptor = serviceProvider.GetService<AuditEntitiesInterceptor>()!;

                options
                    //.UseNpgsql(Environment.GetEnvironmentVariable("PostgreSqlConnection"))
                    .UseNpgsql("Host=localhost;Port=5432;Database=task-manager;Username=postgres;Password=postgres")
                    .UseSnakeCaseNamingConvention()
                    .AddInterceptors(auditInterceptor);
            });

            services.AddTransient<IJwtProvider, JwtProvider>();
            services.AddTransient<IPasswordHasher, PasswordHasher>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserTaskRepository, UserTaskRepository>();           

            return services;
        }
    }
}