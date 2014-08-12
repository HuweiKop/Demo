using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class FieldHelper
    {
        public static string ConvertBool(string obj)
        {
            if ("0".Equals(obj)) return "否";
            else if ("1".Equals(obj)) return "是";
            else return "";
        }
    }
}
