using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.APIModel
{
    public class BaseResult
    {
        public ResultType status { get; set; }

        public string message { get; set; }
    }
}
