using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.APIModel
{
    public class InformationResponse : ReturnBaseModel
    {
        public InformationApi result { get; set; }
    }
}
