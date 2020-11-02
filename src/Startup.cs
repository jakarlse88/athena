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
                .ConfigureAuthentication(Configuration)
                .ConfigureAuthorization(Configuration)
                .AddControllers()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddFluentValidation(fv =>
                    fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            services
                .ConfigureDbContext(Configuration)
                .ConfigureRepositoryLayer()
                .ConfigureServiceLayer()
                .ConfigureCors()
                .ConfigureSwagger()
                .AddAutoMapper(typeof(Startup)); ;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.ApplyMigrations(); // I'm assuming I'll be doing something more sophisticated in production.
            }

            app
                .UseSwaggerUI()
                .UseRouting()
                .UseCors()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }
    }
}