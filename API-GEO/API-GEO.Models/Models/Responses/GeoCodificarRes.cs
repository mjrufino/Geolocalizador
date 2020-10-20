using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_GEO.Models.Responses
{
    public class GeoCodificarRes
    {
        public GeoCodificarRes()
        {
            id = string.Empty;
            latitud = 0;
            longitud = 0;
            estado = string.Empty;
        }

        public GeoCodificarRes(string id)
        {
            this.id = id;
            latitud = 0;
            longitud = 0;
            estado = string.Empty;
        }
        public string id { get; set; }

        public float? latitud { get; set; }

        public float? longitud { get; set; }

        public string estado { get; set; }
    }
}
