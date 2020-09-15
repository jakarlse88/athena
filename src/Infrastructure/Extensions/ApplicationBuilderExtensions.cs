using System;
using Athena.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
        /// <returns></returns>
        internal static IApplicationBuilder UseSwaggerUI(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Athena API v1");
                c.RoutePrefix = string.Empty;
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