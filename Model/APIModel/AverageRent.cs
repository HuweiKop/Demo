using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.APIModel
{
    public class AverageOfMonthRentResponse : ReturnBaseModel
    {
        public AverageOfMonthRentResult result { get; set; }
    }

    public class AverageOfQuarterRentResponse : ReturnBaseModel
    {
        public List<AverageOfQuarterRentResult> result { get; set; }
    }

    public class AverageOfQuarterRentResult
    {
        public string cbd_code { get; set; }

        public string cbd_name { get; set; }

        public List<AverageOfQuarterRent> listRent { get; set; }
    }

    public class AverageOfMonthRentResult 
    {
        public string cbd_code { get; set; }

        public string cbd_name { get; set; }

        public List<AverageOfMonthRent> listRent { get; set; }
    }

    public class AverageRent
    {
        public decimal? averageRnet { get; set; }
    }

    public class AverageOfMonthRent : AverageRent
    {
        public string month { get; set; }
    }

    public class AverageOfQuarterRent : AverageRent
    {
        public string quarter { get; set; }
    }
}
