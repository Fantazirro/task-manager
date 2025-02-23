using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Application.Interfaces.Data;
using TaskManager.Infrastructure.Authentication;
using TaskManager.Infrastructure.Persistence;
using TaskManager.Infrastructure.Persistence.Repositories;
using TaskManager.Application.Interfaces.Auth;
using TaskManager.Infrastructure.Persistence.Interceptors;
using Microsoft.Extensions.Configuration;
using TaskManager.Application.Interfaces.Services;
using TaskManager.Infrastructure.Services;

namespace TaskManager.Infrastructure.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<AuditEntitiesInterceptor>();

            services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
            {
                var auditInterceptor = serviceProvider.GetService<AuditEntitiesInterceptor>()!;

                options
                    .UseNpgsql(configuration.GetConnectionString("PostgreSqlConnection"))
                    .UseSnakeCaseNamingConvention()
                    .AddInterceptors(auditInterceptor);
            });

            services.AddTransient<IJwtProvider, JwtProvider>();
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddTransient<UserIdProvider>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();

            services
                .AddFluentEmail(configuration["Email:SenderEmail"], configuration["Email:Sender"])
                .AddSmtpSender(
                    configuration["Email:Host"], 
                    configuration.GetValue<int>("Email:Port"),
                    configuration["Email:Username"],
                    configuration["Email:Password"]);
            services.AddScoped<IEmailSender, EmailSender>();

            var memcachedServer = configuration["Memcached:Server"];
            var memcachedPort = configuration.GetValue<int>("Memcached:Port");

            services.AddEnyimMemcached(options => options.AddServer(memcachedServer, memcachedPort), true);
            services.AddScoped<ICacheService, CacheService>();

            return services;
        }
    }
}