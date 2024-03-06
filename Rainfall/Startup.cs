using Microsoft.Extensions.Configuration;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using System.Reflection;
using Rainfall.Application;
using Microsoft.OpenApi.Models;

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
              });



            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Rainfall API",
                    Version = "1.0",
                    Description = "An API which provides rainfall reading data",
                    Contact = new OpenApiContact
                    {
                        Name = "Sorted",
                        Url = new Uri("https://wwww.sorted.com")
                    },
                    
                });

                options.AddServer(new OpenApiServer()
                {
                    Url = "http://localhost:3000",
                    Description = "Rainfall Api"
                });

                // https://stackoverflow.com/questions/62424769/using-swashbuckle-5-x-specify-nullable-true-on-a-generic-t-parameter-reference
                options.UseAllOfToExtendReferenceSchemas();

                foreach (var file in Directory.GetFiles(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)), "*.xml"))
                {
                    options.IncludeXmlComments(file, true);
                }
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseHttpsRedirection();

            app.UseRouting();

          //  app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });

            app.UseSwagger();


            if (env.IsDevelopment())
            {
                app.UseSwaggerUI(options => {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Rainfall API");
                });

                app.UseReDoc(options => {
                    options.DocumentTitle = "Rainfall API";
                    options.SpecUrl = "/swagger/v1/swagger.json";
                });
            }
            else
            {
                app.UseSwaggerUI(options => {
                    options.SwaggerEndpoint("/rainfall/swagger/v1/swagger.json", "Rainfall API");
                });

                app.UseReDoc(options => {
                    options.DocumentTitle = "Rainfall API";
                });
            }
        }
     }
}
