using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.APIModel
{
    public class ShopApi
    {
        public string shop_code { get; set; }
        public string shop_name { get; set; }
        public string address { get; set; }
        public string shop_num { get; set; }
        public string floor { get; set; }
        public decimal? area { get; set; }
        public string shop_commercial_activities { get; set; }
        public string shop_position { get; set; }
        public string telephone { get; set; }
        public Location location { get; set; }
    }
}
