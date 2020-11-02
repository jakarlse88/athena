using System;
using System.IO;
using System.Reflection;
using Athena.Data;
using Athena.Infrastructure.Auth;
using Athena.Infrastructure.Config;
using Athena.Repositories;
using Athena.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Serilog;
using System.IO;
using System.Reflection;
using System;

namespace Athena.Infrastructure
{
    internal static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds service layer services to the application dependency injection container. 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        internal static IServiceCollection ConfigureServiceLayer(this IServiceCollection services)
        {
            services.AddTransient<ITechniqueService, TechniqueService>();
            services.AddTransient<ITechniqueTypeService, TechniqueTypeService>();
            services.AddTransient<ITechniqueCategoryService, TechniqueCategoryService>();

            return services;
        }

        /// <summary>
        /// Adds repository layer services to the application dependency injection container. 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        internal static IServiceCollection ConfigureRepositoryLayer(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

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
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    var authConfig = configuration.GetSection(Auth0Options.Auth0).Get<Auth0Options>();

                    options.Authority = authConfig.Authority;
                    options.Audience = authConfig.ApiIdentifier;
                });

            return services;
        }

        /// <summary>
        /// Configures policy-based authorization.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        internal static IServiceCollection ConfigureAuthorization(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                var section = configuration.GetSection(AuthorizationPolicyOptions.Permissions);

                if (!section.Exists())
                {
                    throw new Exception(
                        $"The section '{AuthorizationPolicyConstants.PermissionsClaimName}' does not exist in the configuration object");
                }

                Log.Information("Building authorization policies...");

                foreach (var child in section.GetChildren())
                {
                    foreach (var childOfChild in child.GetChildren())
                    {
                        var policyName = AuthorizationPolicyConstants.MapConfigKeyToPolicyName(childOfChild.Key);

                        options.AddPolicy(policyName, policy =>
                            policy.RequireClaim(AuthorizationPolicyOptions.PermissionClaimName,
                                childOfChild.Value));

                        Log.Information(
                            $"\t-> Built policy '{policyName}', which requires claim '{childOfChild.Value}'");
                    }
                }

                Log.Information("Authorization policies built.");
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
                opt.UseSqlServer(configuration.GetConnectionString(AthenaDbContext.ConnectionStringName)));

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
                builder // Testing with Hecate ,run locally
                    .WithOrigins("http://localhost:5001")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();

                builder // Testing with Peitho (Blazor WASM), run locally
                    .WithOrigins("http://localhost:5000")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();

                builder // Testing with Peitho (Angular), run locally
                    .WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
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