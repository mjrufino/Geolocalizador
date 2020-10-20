using MassTransit;
using MQ.Models.Messages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Geocodificador.MQ.Consumers
{
    public class GeolocalizacionCons : IConsumer<GeoLocalizacionMsg>
    {
        private readonly HttpClient _client;
        public GeolocalizacionCons(HttpClient client)
        {
            _client = client;
        }
        public async Task Consume(ConsumeContext<GeoLocalizacionMsg> context)
        {
            try
            {
                var geoLocalizacionMsg = context.Message;
                Console.WriteLine($"Consumido\nid: {geoLocalizacionMsg.id}");

                _client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml");
                _client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");
                _client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                _client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Charset", "ISO-8859-1");

                HttpResponseMessage response = await _client.GetAsync($"https://nominatim.openstreetmap.org/search?street={geoLocalizacionMsg.calle}%20{geoLocalizacionMsg.numero}&city={geoLocalizacionMsg.ciudad}&county={geoLocalizacionMsg.provincia}&country={geoLocalizacionMsg.pais}&postalcode={geoLocalizacionMsg.codigo_postal}&format=jsonv2");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                //Se envia en lista porque puede traer mas de una locacion
                var geoOSM = JsonConvert.DeserializeObject<List<GeoOSMMsg.GeoOSM>>(responseBody);
                var geoOSMMsg = new GeoOSMMsg
                {
                    id = context.Message.id,
                    geoOSM = geoOSM
                };

                await context.Publish(geoOSMMsg);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\n" + e.InnerException?.Message);
            }
        }
    }
}
