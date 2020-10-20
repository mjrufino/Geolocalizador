using System;
using System.Collections.Generic;
using System.Text;

namespace MQ.Models.Messages
{
    public class GeoLocalizacionMsg
    {
        public int id { get; set; }

        public string calle { get; set; }

        public string numero { get; set; }

        public string ciudad { get; set; }

        public string codigo_postal { get; set; }

        public string provincia { get; set; }

        public string pais { get; set; }
    }
}
