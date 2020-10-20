using System;
using System.Collections.Generic;
using System.Text;

namespace MQ.Models.Messages
{
    public class GeoOSMMsg
    {
        public int id { get; set; }
        
        public List<GeoOSM> geoOSM { get; set; }

        //Proyecto model si se utilizan multiples modelos de la api
        public class GeoOSM
        {
            public int osm_id { get; set; }
            public string lat { get; set; }
            public string lon { get; set; }
        }
    }
}
