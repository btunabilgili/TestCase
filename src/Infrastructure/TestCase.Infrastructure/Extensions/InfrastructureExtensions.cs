using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestCase.Application.Interfaces;
using TestCase.Infrastructure.Contexts;
using TestCase.Infrastructure.Repositories;
using TestCase.Infrastructure.Services;
using TestCase.Infrastructure.UnitOfWork;

namespace TestCase.Infrastructure.Extensions
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TestCaseContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IUnitOfWork<,>), typeof(UnitOfWork<,>));

            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IJobService, JobService>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
