using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.APIModel
{
    public class DistrictApi
    {
        public string district_code { get; set; }
        public string district_name { get; set; }
        public Location location { get; set; }
    }
}
