using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.CommonModel
{
    public class Message
    {
        public int status { get; set; }
        public string message { get; set; }

        public Result result { get; set; }
    }

    public class Result
    {
        public Location location { get; set; }

        public int pricise { get; set; }

        public int confidence { get; set; }

        public string level { get; set; }
    }

    public class Location
    {
        public decimal lng { get; set; }

        public decimal lat { get; set; }
    }
}
