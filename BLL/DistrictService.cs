using BLLCommon;
using Common;
using Contract;
using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BLL
{
    public class DistrictService : IDistrictService
    {
        public PagedList<District> GetAllDistricts(District district, string orderBy, string orderAs, int page)
        {
            try
            {
                using (IDbContextBase context = DbContextFactory.GetDbContext())
                {
                    string name = district == null ? "" : district.Name + "";
                    PagedList<District> listDistrict = context.FindAllByPage<District>(obj => obj.Name.Contains(name), orderBy, orderAs, page);

                    return listDistrict;
                }
            }
            catch
            {

                throw;
            }
        }

        public District GetDistrictById(Guid districtId)
        {
            try
            {
                using (IDbContextBase context = DbContextFactory.GetDbContext())
                {
                    return context.Find<District>(districtId);
                }
            }
            catch
            {

                throw;
            }
        }

        public bool InsertDistrict(District district)
        {
            try
            {
                using (IDbContextBase context = DbContextFactory.GetDbContext())
                {
                    district.Id = Guid.NewGuid();
                    return context.Insert<District>(district);
                }
            }
            catch
            {

                throw;
            }
        }

        public bool UpdateDistrict(District district)
        {
            try
            {
                using (IDbContextBase context = DbContextFactory.GetDbContext())
                {
                    return context.Update<District>(district);
                }
            }
            catch
            {

                throw;
            }
        }

        public bool DeleteDistrict(Guid districtId)
        {
            try
            {
                using (IDbContextBase context = DbContextFactory.GetDbContext())
                {
                    context.Delete<District>(districtId, false);

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
