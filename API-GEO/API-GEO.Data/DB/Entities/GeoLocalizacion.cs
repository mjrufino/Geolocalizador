using System;
using System.Collections.Generic;

namespace API_GEO.Data.DB.Entities
{
    public partial class GeoLocalizacion
    {
        public int Id { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Ciudad { get; set; }
        public string CodigoPostal { get; set; }
        public string Provincia { get; set; }
        public string Pais { get; set; }
        public float? Latitud { get; set; }
        public float? Longitud { get; set; }
        public short Procesando { get; set; }
        public int? OSMId { get; set; }
    }
}
