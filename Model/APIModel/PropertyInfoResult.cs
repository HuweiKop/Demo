using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.APIModel
{
    public class PropertyInfoResult
    {
        public string property_code { get; set; }

        public string property_name { get; set; }

        public string property_city { get; set; }

        public string property_district { get; set; }

        public string property_businessDistrict { get; set; }

        public string property_type { get; set; }

        public string property_status { get; set; }

        public string property_address { get; set; }

        public string property_finishTime { get; set; }

        public decimal? property_area { get; set; }

        public string property_developer { get; set; }

        public decimal? property_fee { get; set; }

        public decimal? property_rent { get; set; }

        public int? property_carPort { get; set; }

        public int property_floorCount { get; set; }

        public string property_des { get; set; }

        public string cbd_code { get; set; }

        public PropertyStatisticsResult property_statistics { get; set; }

        public Location location { get; set; }

        //public List<PropertyInfoResult> property_suggestion { get; set; }
    }
}
