using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
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
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString, string hangfireConnectionString)
        {
            services.AddDbContext<TestCaseContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddHangfire(options =>
                options.UsePostgreSqlStorage(hangfireConnectionString));
            services.AddHangfireServer();

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

            services.AddTransient<IHangfireService, HangfireService>();
            services.AddTransient<IFakeEmailService, FakeEmailService>();
            services.AddTransient<IJobService, JobService>();

            return services;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseHangfireDashboard();

            return app;
        }
    }
}
