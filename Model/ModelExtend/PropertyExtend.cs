using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class Property
    {
        private Guid? cityId;
        public Guid? CityId
        {
            get
            {
                if (cityId != null)
                    return cityId;
                using (Entities entities = new Entities())
                {
                    var o = entities.Districts.FirstOrDefault(obj => obj.Id == DistrictId);
                    district = o == null ? "" : o.Name;
                    cityId = o == null ? null : o.CityId;
                    return cityId;
                }
            }
        }

        private string district;
        public string DistrictName
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(district))
                    return district;
                using (Entities entities = new Entities())
                {
                    var o = entities.Districts.FirstOrDefault(obj => obj.Id == DistrictId);
                    district = o == null ? "" : o.Name;
                    cityId = o == null ? null : o.CityId;
                    return district;
                }
            }
        }

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
