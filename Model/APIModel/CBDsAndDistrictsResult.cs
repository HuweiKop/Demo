using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.APIModel
{
    public class CBDsAndDistrictsResult
    {
        public string city_code { get; set; }
        public List<CBDApi> cbds { get; set; }
        public List<DistrictApi> districts { get; set; }
    }
}
