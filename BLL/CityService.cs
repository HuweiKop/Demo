using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Contract;
using DAL;
using Model;
using BLLCommon;
using Model.CommonModel;

namespace BLL
{
    public class CityService : ICityService
    {
        public PagedList<City> GetAllCities(City city, string orderBy, string orderAs, int page)
        {
            try
            {
                using (IDbContextBase context = DbContextFactory.GetDbContext())
                {
                    string name = city == null ? "" : city.Name + "";
                    PagedList<City> listCity = context.FindAllByPage<City>(obj => obj.Name.Contains(name), orderBy, orderAs, page);

                    return listCity;
                }
            }
            catch
            {

                throw;
            }
        }

        public City GetCityById(Guid cityId)
        {
            try
            {
                using (IDbContextBase context = DbContextFactory.GetDbContext())
                {
                    return context.Find<City>(cityId);
                }
            }
            catch
            {

                throw;
            }
        }

        public bool InsertCity(City city)
        {
            try
            {
                using (IDbContextBase context = DbContextFactory.GetDbContext())
                {
                    city.Id = Guid.NewGuid();
                    return context.Insert<City>(city);
                }
            }
            catch
            {

                throw;
            }
        }

        public bool UpdateCity(City city)
        {
            try
            {
                using (IDbContextBase context = DbContextFactory.GetDbContext())
                {
                    return context.Update<City>(city);
                }
            }
            catch
            {

                throw;
            }
        }

        public bool DeleteCity(Guid cityId)
        {
            try
            {
                using (IDbContextBase context = DbContextFactory.GetDbContext())
                {
                    context.Delete<City>(cityId, false);
                    context.Delete<District>(obj => obj.CityId == cityId, false);

                    return context.SaveChanges();
                }
            }
            catch
            {

                throw;
            }
        }
    }
}
