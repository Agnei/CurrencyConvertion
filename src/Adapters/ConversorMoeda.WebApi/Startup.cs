using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ConversorMoeda.WebApi.Configuration;
using System;

namespace ConversorMoeda.WebApi
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                    .AddControllersAsServices() //Injecting Controllers themselves thru DI //For further info see: http://docs.autofac.org/en/latest/integration/aspnetcore.html#controllers-as-services            
                    .SetCompatibilityVersion2_1();

            services.AddCorsExtensions()
                    .AddSwagger()
                    .ConfigureHttpClient();

            var serviceProvider = services.AddDryIoCExtension(Configuration);

            return serviceProvider;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var enviroment = env.IsDevelopment() ? app.UseDeveloperExceptionPage() : app.UseHsts();

            app.UseCors("CorsPolicy")
               .UseHttpsRedirection()
               .UseMvcWithDefaultRoute()
               .UseSwagger()
               .UseSwaggerExtensions();
        }
    }
}