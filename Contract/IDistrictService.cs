using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Common;

namespace Contract
{
    public interface IDistrictService
    {
        PagedList<District> GetAllDistricts(District district, string orderBy, string orderAs, int page);
        District GetDistrictById(Guid districtId);
        bool InsertDistrict(District district);
        bool UpdateDistrict(District district);
        bool DeleteDistrict(Guid districtId);
    }
}
