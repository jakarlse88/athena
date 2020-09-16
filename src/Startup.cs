using Athena.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;

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
            services.AddControllers();

            services
                .ConfigureAuthentication(Configuration)
                .ConfigureDbContext(Configuration)
                .ConfigureRepositoryLayer()
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
                .UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}