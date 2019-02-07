using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Infrastructure.Core.IoC.DryIoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Core.Http;



namespace ConversorMoeda.WebApi.Configuration
{
    public static partial class StartupExtensions
    {
        public static IServiceProvider AddDryIoCExtension(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDryIoC<ConversorMoedaCompositeRoot>();
        }

        public static IServiceCollection ConfigureHttpClient(this IServiceCollection services)
        {
            services.AddHttpClient<IHttpClient, StandardHttpClient>()
                    .AddPolicyHandler(HttpPolicy.GetRetryPolicy())
                    .AddPolicyHandler(HttpPolicy.GetCircuitBreakerPolicy());

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "CurrencyConvertion HTTP API",
                    Version = "v1",
                    Description = "API para o servico de Coorte",
                    TermsOfService = "Termos de servico"
                });
            });

            return services;
        }

        public static IServiceCollection AddCorsExtensions(this IServiceCollection services)
        {
            return services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }

        // Give me all of the 2.1 behaviors
        public static IMvcBuilder SetCompatibilityVersion2_1(this IMvcBuilder builder) => builder.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

        public static IApplicationBuilder UseSwaggerExtensions(this IApplicationBuilder app)
             => app.UseSwaggerUI(c =>
             {
                 c.SwaggerEndpoint($"/swagger/v1/swagger.json", "CurrencyConvertion.API V1");
                 c.RoutePrefix = string.Empty;
             });
    }
}
