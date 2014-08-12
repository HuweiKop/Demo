using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.APIModel
{
    public class CBDInfoResponse : ReturnBaseModel
    {
        public List<CBDInfo> resultInfo { get; set; }

        public List<AverageOfQuarterRentResult> resultAverage { get; set; }
    }

    public class CBDInfo
    {
        public string cbd_code { get; set; }

        public string cbd_name { get; set; }

        public int sumProperty { get; set; }

        public decimal sumArea { get; set; }

        public decimal sumUsedArea { get; set; }

        public decimal sumUnusedArea { get; set; }
    }
}
