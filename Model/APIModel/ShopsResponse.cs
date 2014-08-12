using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.APIModel
{
    public class ShopsResponse : ReturnBaseModel
    {
        public List<ShopResult> result { get; set; }

        public Location center { get; set; }
    }

    public class ShopResult
    {
        public string brand_name { get; set; }
        public string brand_code { get; set; }
        public string brand_des { get; set; }
        public ShopApi shop { get; set; }
    }
}
