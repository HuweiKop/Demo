using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.APIModel
{
    public class ShopResponse : ReturnBaseModel
    {
        public ShopApi shopResult { get; set; }

        public PropertyInfoResult propertyResult { get; set; }
    }
}
