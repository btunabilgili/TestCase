using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using TestCase.Application.Common;
using TestCase.Application.Extensions;
using TestCase.Application.Interfaces;
using TestCase.Infrastructure.Extensions;
using TestCase.WebAPI.Middlewares;

namespace TestCase.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Serilog
            builder.Logging.ClearProviders();
            builder.Host.UseSerilog((hostContext, services, configuration) => {
                configuration
                    .WriteTo.Console()
                    .WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("logs"), "logs", needAutoCreateTable: true);
            });
            #endregion

            #region JWTAuth
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JWTSettings:Issuer"],
                    ValidAudience = builder.Configuration["JWTSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTSettings:Key"]!)),
                    ClockSkew = TimeSpan.Zero
                };
            });

            builder.Services.AddAuthorization();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("V1", new OpenApiInfo
                {
                    Version = "V1",
                    Title = "TestCase API",
                    Description = "Main API Documantation of TestCase API"
                });

                var securityScheme = new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                };

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "bearerAuth"
                            }
                        },
                        Array.Empty<string>()
                    }
                };

                options.AddSecurityDefinition("bearerAuth", securityScheme);
                options.AddSecurityRequirement(securityRequirement);
            });
            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JWTSettings"));
            #endregion

            builder.Services.AddTransient<ExceptionMiddleware>();
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("testCase")!, builder.Configuration.GetConnectionString("hangfire")!);

            var app = builder.Build();

            app.UseMiddleware<ExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options => {
                    options.SwaggerEndpoint("/swagger/V1/swagger.json", "Main API Documantation of TestCase API");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseInfrastructure();

            app.MapControllers();

            #region HangfireJobs
            RecurringJob.RemoveIfExists("email-remainder-job");
            RecurringJob.AddOrUpdate<IHangfireService>("email-remainder-job", x => x.SendReminderEmailAsync(), Cron.Daily);
            #endregion

            app.Run();
        }
    }
}