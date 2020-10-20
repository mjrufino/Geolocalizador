using API_GEO.MQ.Geocodificador.Publishers.Interfaces;
using MassTransit;
using MQ.Models.Messages;
using System;
using System.Threading.Tasks;

namespace API_GEO.MQ.Geocodificador.Publishers
{
    public class GeocodificadorPub : IGeocodificadorPub
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public GeocodificadorPub(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public Task EnviarGeoLocalizacion(GeoLocalizacionMsg geoLocalizacionMsg)
        {
            try
            {
                return _publishEndpoint.Publish(geoLocalizacionMsg);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\n" + e.InnerException?.Message);
                return Task.FromException(e);
            }
        }
    }
}
