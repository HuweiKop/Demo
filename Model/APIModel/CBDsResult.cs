using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.APIModel
{
    public class CBDsResult
    {
        public string city_code { get; set; }
        public int property_count { get; set; }
        public int brand_count { get; set; }
        public List<CBDApi> cbds { get; set; }
    }
}
