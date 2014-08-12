using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class District
    {
        private string city;
        public string CityName
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(city))
                    return city;
                using (Entities entities = new Entities())
                {
                    var o = entities.Cities.FirstOrDefault(obj => obj.Id == CityId);
                    city = o == null ? "" : o.Name;
                    return city;
                }
            }
        }
    }
}
