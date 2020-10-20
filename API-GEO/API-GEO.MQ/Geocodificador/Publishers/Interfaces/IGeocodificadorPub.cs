using MQ.Models.Messages;
using System.Threading.Tasks;

namespace API_GEO.MQ.Geocodificador.Publishers.Interfaces
{
    public interface IGeocodificadorPub
    {
        Task EnviarGeoLocalizacion(GeoLocalizacionMsg geoLocalizacionMsg);
    }
}
