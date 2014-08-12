using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Common;

namespace Contract
{
    public interface ICityService
    {
        PagedList<City> GetAllCities(City city, string orderBy, string orderAs, int page);
        City GetCityById(Guid cityId);
        bool InsertCity(City city);
        bool UpdateCity(City city);
        bool DeleteCity(Guid cityId);
    }
}
