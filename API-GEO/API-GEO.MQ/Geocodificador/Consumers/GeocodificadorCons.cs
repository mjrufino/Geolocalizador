using API_GEO.Data.DB;
using MassTransit;
using Microsoft.EntityFrameworkCore.Internal;
using MQ.Models.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_GEO.MQ.Geocodificador.Consumers
{
    public class GeocodificadorCons : IConsumer<GeoOSMMsg>
    {
        public async Task Consume(ConsumeContext<GeoOSMMsg> context)
        {
            var geoOSMMsg = context.Message;

            Console.WriteLine($"Consumido\nid: {geoOSMMsg.id}");

            using (var contextdb = new geodbContext())
            {
                var geoLocalizacion = await contextdb.GeoLocalizacion.FindAsync(geoOSMMsg.id);

                //Si existe el registro
                if (geoLocalizacion != null)
                {
                    //Si no existe el resultado, marco como procesado exitoso
                    geoLocalizacion.Procesando = 0;

                    if (geoOSMMsg.geoOSM.Count > 0)
                    {
                        //Tomo la primer locacion, ya que se especifica un unico resultado
                        var geoOSM = geoOSMMsg.geoOSM.First();

                        geoLocalizacion.OSMId = geoOSM.osm_id;
                        geoLocalizacion.Latitud = float.Parse(geoOSM.lat);
                        geoLocalizacion.Longitud = float.Parse(geoOSM.lon);
                    }

                    contextdb.Update(geoLocalizacion);
                    contextdb.SaveChanges();
                }
            }
        }
    }
}
