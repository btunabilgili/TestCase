using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using TestCase.Application.Tools;
using TestCase.Application.Interfaces;

namespace TestCase.Application.Extensions
{
    public static class ApplicationExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        }
    }
}
