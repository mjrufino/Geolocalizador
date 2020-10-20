using Geocodificador.MQ.Consumers;
using MassTransit;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Geocodificador
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        public static async Task Main()
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri("rabbitmq://guest:guest@rabbitmq:5672"));
                cfg.ReceiveEndpoint("event-listener", e =>
                {
                    e.Consumer(() => new GeolocalizacionCons(client));
                });
            });

            await busControl.StartAsync();
            try
            {
                Console.WriteLine("Press enter to exit");

                await Task.Delay(Timeout.Infinite);
            }
            finally
            {
                await busControl.StopAsync();
            }

        }
    }
}
