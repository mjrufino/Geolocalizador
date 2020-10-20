using System;
using API_GEO.Manager;
using API_GEO.Manager.Interfaces;
using API_GEO.MQ.Geocodificador.Consumers;
using API_GEO.MQ.Geocodificador.Publishers;
using API_GEO.MQ.Geocodificador.Publishers.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace API_GEO
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    //TODO usar las configs de appsettings.json
                    cfg.Host(new Uri("rabbitmq://guest:guest@rabbitmq:5672"));
                    cfg.ReceiveEndpoint("event-listener", e =>
                    {
                        e.Consumer<GeocodificadorCons>();
                    });
                });
            });

            services.AddMassTransitHostedService();

            services.AddControllers();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IApiGeoMan, ApiGeoMan>();

            services.AddScoped<IGeocodificadorPub, GeocodificadorPub>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
