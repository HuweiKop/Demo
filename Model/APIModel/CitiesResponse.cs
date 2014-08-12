using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.APIModel
{
    public class CitiesResponse : ReturnBaseModel
    {
        public CitiesResult result { get; set; }
    }
}
