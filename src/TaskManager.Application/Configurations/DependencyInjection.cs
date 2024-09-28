using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TaskManager.Application.Interfaces.Common;

namespace TaskManager.Application.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddValidatorsFromAssembly(Assembly.Load("TaskManager.Application"), includeInternalTypes: true);

            var useCases = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.GetInterfaces().Contains(typeof(IUseCase)));

            foreach (var useCase in useCases) 
                services.AddScoped(useCase);

            return services;
        }
    }
}