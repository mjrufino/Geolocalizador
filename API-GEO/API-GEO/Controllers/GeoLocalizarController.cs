using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_GEO.Manager.Interfaces;
using API_GEO.Models.Requests;
using API_GEO.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_GEO.Controllers
{
    [ApiController]
    [Route("API-GEO/[action]")]
    public class GeoLocalizarController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IApiGeoMan _apiGeoMan;

        public GeoLocalizarController(IHttpContextAccessor httpContextAccessor , IApiGeoMan apiGeoMan)
        {
            _httpContextAccessor = httpContextAccessor;
            _apiGeoMan = apiGeoMan;
        }

        [HttpPost]
        public IActionResult GeoLocalizar([FromBody] GeoLocalizacionReq geoLocalizacion)
        {
            int id = _apiGeoMan.AgregarLocalizacion(geoLocalizacion);

            return new AcceptedResult(_httpContextAccessor.HttpContext.Request.Host.Value + "/api-geo/GeoCodificar?id=" + id, new { id });
        }

        [HttpGet]
        public GeoCodificarRes GeoCodificar([FromQuery(Name = "id")] string id)
        {
            var geoCodificarRes = _apiGeoMan.ObtenerLocalizacion(id);

            return geoCodificarRes;
        }
    }
}
