using System;
using Athena.Data;
using Athena.Infrastructure.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Serilog;

namespace Athena.Infrastructure
{
    internal static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Enable Swagger UI.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        internal static IApplicationBuilder UseSwaggerUI(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseSwagger();
            app.UseSwaggerUI(uiOptions =>
            {
                uiOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "Athena API v1");
                uiOptions.RoutePrefix = string.Empty;

                var authOptions =
                    configuration
                        .GetSection(SwaggerAuthorizationOptions.Swagger)
                        .GetSection(SwaggerAuthorizationOptions.Authorization)
                        .Get<SwaggerAuthorizationOptions>();

                if (authOptions is null 
                    || string.IsNullOrWhiteSpace(authOptions.ClientId)
                    || string.IsNullOrWhiteSpace(authOptions.ClientSecret)
                    || string.IsNullOrWhiteSpace(authOptions.RedirectUrl))
                {
                    Log.Error("Unable to configure SwaggerUI authorization; one or more configuration settings are missing.");
                    return;
                }
                
                uiOptions.OAuthClientId(authOptions.ClientId);
                uiOptions.OAuthClientSecret(authOptions.ClientSecret);
                uiOptions.OAuth2RedirectUrl(authOptions.RedirectUrl);
            });

            return app;
        }

        /// <summary>
        /// Applies any pending migrations.
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        internal static IApplicationBuilder ApplyMigrations(this IApplicationBuilder app)
        {
            Log.Information("Applying migrations, please wait.");

            using (var serviceScope = app.ApplicationServices.CreateScope())
            using (var context = serviceScope.ServiceProvider.GetRequiredService<AthenaDbContext>())
            {
                try
                {
                    var retry =
                        Policy
                            .Handle<SqlException>()
                            .WaitAndRetry(new[]
                            {
                                TimeSpan.FromSeconds(3),
                                TimeSpan.FromSeconds(6),
                                TimeSpan.FromSeconds(9)
                            });

                    retry.Execute(() => context.Database.Migrate());
                }
                catch (Exception ex)
                {
                    Log.Fatal("A fatal error occurred while trying to apply migrations: {@ex}", ex);
                    throw;
                }
            }

            Log.Information("Migrations operation successful, continuing application startup.");
            return app;
        }
    }
}