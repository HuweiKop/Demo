using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.APIModel
{
    public class PropertyStatisticsResponse : ReturnBaseModel
    {
        public PropertyStatisticsResult result { get; set; }
    }
}
