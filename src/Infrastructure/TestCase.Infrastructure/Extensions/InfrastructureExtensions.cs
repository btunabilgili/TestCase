using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestCase.Application.Interfaces;
using TestCase.Infrastructure.Contexts;
using TestCase.Infrastructure.Repositories;
using TestCase.Infrastructure.Services;

namespace TestCase.Infrastructure.Extensions
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TestCaseContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

            services.AddTransient<IJobService, JobService>();

            return services;
        }
    }
}
