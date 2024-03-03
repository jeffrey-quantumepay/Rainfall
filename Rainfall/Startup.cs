using Microsoft.Extensions.Configuration;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using System.Reflection;
using Rainfall.Application;

namespace Rainfall.Web.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddRainfallApplication(Configuration);

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();


            services.AddMvc(options => {
                //options.Filters.Add<UnhandledExceptionFilter>();  // unhandled exceptions will be handled insde the a Mediatr pipleline behavior
            })
            .AddFluentValidation(options => {
                options.AutomaticValidationEnabled = false;  // the validators will be executed inside a Mediatr pipeline behaviour
                                                             //  options.ValidatorOptions.PropertyNameResolver = SnakeCasePropertyResolver.ResolvePropertyName;
            });

            services.AddControllers(options => {
                // options
            })
              .AddJsonOptions(options => {
                  // options
                  //options.JsonSerializerOptions.

              });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
            }
            app.UseSwaggerUI();


            app.UseHttpsRedirection();

            app.UseRouting();

          //  app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
     }
}
