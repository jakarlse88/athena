using System;
using System.IO;
using System.Reflection;
using Athena.Data;
using Athena.Infrastructure.Config;
using Athena.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Athena.Infrastructure
{
    internal static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds repository layer services to the application dependency injection container. 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        internal static IServiceCollection ConfigureRepositoryLayer(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
        
        /// <summary>
        /// Configures OIDC authentication with Auth0.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        internal static IServiceCollection ConfigureAuthentication(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var authConfig = configuration.GetSection(Auth0Options.Auth0).Get<Auth0Options>();

                options.Authority = authConfig.Authority;
                options.Audience = authConfig.ApiIdentifier;
            });

            return services;
        }


        /// <summary>
        /// Configures the application DB context.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        internal static IServiceCollection ConfigureDbContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AthenaDbContext>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString("AthenaDb")));

            return services;
        }

        /// <summary>
        /// Configures CORS policy.
        /// </summary>
        /// <param name="services"></param>
        internal static IServiceCollection ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options => options.AddDefaultPolicy(builder =>
            {
                // Testing with locally run client
                builder
                    .WithOrigins("https://localhost:5001")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                
                // The real question is, what to do re: CORS when the application/services are deployed with Kubernetes?
                
            }));

            return services;
        }
        
        /// <summary>
        /// Configures Swagger.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        internal static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Athena",
                    Version = "v1",
                    Description = "Technique and form catalogue service.",
                    Contact = new OpenApiContact
                    {
                        Name = "Jon Karlsen",
                        Email = "karlsen.jon@outlook.com",
                        Url = new Uri("https://github.com/jakarlse88")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                config.IncludeXmlComments(xmlPath);
            });

            return services;
        }
    }
}