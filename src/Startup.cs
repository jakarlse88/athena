using Athena.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using FluentValidation.AspNetCore;

namespace Athena
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddFluentValidation(fv =>
                    fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            services
                .ConfigureAuthentication(Configuration)
                .ConfigureDbContext(Configuration)
                .ConfigureRepositoryLayer()
                .ConfigureServiceLayer()
                .ConfigureCors()
                .ConfigureSwagger()
                .AddAutoMapper(typeof(Startup));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // I'm assuming I'll be doing something more sophisticated in production.
                app.ApplyMigrations();
            }

            app
                .UseHttpsRedirection()
                .UseSwaggerUI()
                .UseRouting()
                .UseCors()
                .UseAuthorization()
                .UseAuthentication()
                .UseEndpoints(endpoints =>
                {
                    if (env.IsDevelopment())
                    {
                        endpoints.MapControllers();
                    }
                    else
                    {
                        endpoints.MapControllers().RequireAuthorization();
                    }
                });
        }
    }
}