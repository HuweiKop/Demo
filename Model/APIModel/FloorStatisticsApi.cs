using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.APIModel
{
    public class FloorStatisticsApi
    {
        public string floor { get; set; }

        public string floor_code { get; set; }

        public decimal? floor_vacancy { get; set; }

        public string floor_pic { get; set; }

        public List<CommercialActivitiesProportionApi> listCAP { get; set; }
    }
}
