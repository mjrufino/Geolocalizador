using API_GEO.Models.Requests;
using API_GEO.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace API_GEO.Manager.Interfaces
{
    public interface IApiGeoMan
    {
        int AgregarLocalizacion(GeoLocalizacionReq geoLocalizacion);

        GeoCodificarRes ObtenerLocalizacion(string id);
    }
}
