using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rainfall.SharedLibrary;
using Rainfall.SharedLibrary.Behaviours;
using System.Reflection;

namespace Rainfall.Application
{
    public static class Bootstrap
    {
        public static IServiceCollection AddRainfallApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            services.AddHttpClient();

            return services;

        }
    }
}
