using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.APIModel
{
    public class PropertyStatisticsResult
    {
        public decimal? property_vacancy { get; set; }

        public List<CommercialActivitiesProportionApi> listCAP { get; set; }
    }
}
