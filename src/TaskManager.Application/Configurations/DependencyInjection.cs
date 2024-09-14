using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TaskManager.Application.Services;

namespace TaskManager.Application.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddValidatorsFromAssembly(Assembly.Load("TaskManager.Application"), includeInternalTypes: true);

            services.AddScoped<TaskService>();
            services.AddScoped<AuthService>();

            return services;
        }
    }
}