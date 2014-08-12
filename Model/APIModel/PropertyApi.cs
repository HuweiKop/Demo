using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.APIModel
{
    public class PropertyApi
    {
        public string property_code { get; set; }
        public string property_name { get; set; }
        public string property_englishName { get; set; }
        public Location location { get; set; }
    }
}
