using API_GEO.Data.DB;
using API_GEO.Data.DB.Entities;
using API_GEO.Manager.Interfaces;
using API_GEO.Models.Requests;
using API_GEO.Models.Responses;
using API_GEO.MQ;
using API_GEO.MQ.Geocodificador.Publishers.Interfaces;
using GreenPipes.Contracts;
using MQ.Models.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API_GEO.Manager
{
    public class ApiGeoMan : IApiGeoMan
    {
        private readonly IGeocodificadorPub _geocodificadorPub;

        public ApiGeoMan(IGeocodificadorPub geocodificadorPub)
        {
            _geocodificadorPub = geocodificadorPub;
        }

        public int AgregarLocalizacion(GeoLocalizacionReq geoLocalizacionReq)
        {
            int resultado = 0;

            try
            {
                //TODO patron de repositorio
                using (var context = new geodbContext())
                {
                    //TODO mapper
                    var geoLocalizacion = new GeoLocalizacion
                    {
                        Calle = geoLocalizacionReq.calle,
                        Ciudad = geoLocalizacionReq.ciudad,
                        CodigoPostal = geoLocalizacionReq.codigo_postal,
                        Numero = geoLocalizacionReq.numero,
                        Pais = geoLocalizacionReq.pais,
                        Provincia = geoLocalizacionReq.provincia,
                        Procesando = 1
                    };
                    context.Add(geoLocalizacion);

                    context.SaveChanges();
                    resultado = geoLocalizacion.Id;

                    //TODO mapper
                    GeoLocalizacionMsg geoLocalizacionMsg = new GeoLocalizacionMsg
                    {
                        calle = geoLocalizacion.Calle,
                        ciudad = geoLocalizacion.Ciudad,
                        codigo_postal = geoLocalizacion.CodigoPostal,
                        id = geoLocalizacion.Id,
                        numero = geoLocalizacion.Numero,
                        pais = geoLocalizacion.Pais,
                        provincia = geoLocalizacion.Provincia
                    };

                    _geocodificadorPub.EnviarGeoLocalizacion(geoLocalizacionMsg);
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message + "\n" + e.InnerException?.Message);
            }

            return resultado;
        }

        public GeoCodificarRes ObtenerLocalizacion(string id)
        {
            int parsedId;
            var geoCodificarRes = new GeoCodificarRes(id);

            if (!int.TryParse(id, out parsedId))
            {
                //Lanzar error
                return geoCodificarRes;
            }

            GeoLocalizacion geoLocalizacion;
            using (var context = new geodbContext())
            {
                geoLocalizacion = context.GeoLocalizacion.Find(parsedId);
            }

            if (geoLocalizacion == null)
            {
                geoCodificarRes.estado = "No existe registro";
            }
            else if (geoLocalizacion.Procesando == 1)
            {
                geoCodificarRes.estado = "Procesando";
            }
            else
            {
                geoCodificarRes.latitud = geoLocalizacion.Latitud;
                geoCodificarRes.longitud = geoLocalizacion.Longitud;
                geoCodificarRes.estado = "Terminado";
            }

            return geoCodificarRes;
        }
    }
}
