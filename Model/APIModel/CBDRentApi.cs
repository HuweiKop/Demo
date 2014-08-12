using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.APIModel
{
    public class CBDRentApi : ReturnBaseModel
    {
        public string cbd_code { get; set; }
        public string cbd_name { get; set; }
        public decimal? averageRent { get; set; }
    }
}
