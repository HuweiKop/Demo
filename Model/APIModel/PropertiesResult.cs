using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.APIModel
{
    public class PropertiesResult
    {
        //public string cbd_code { get; set; }
        public List<PropertyApi> properties { get; set; }

        public Location center { get; set; }
    }
}
