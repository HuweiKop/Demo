using BLLCommon;
using Common;
using Contract;
using DAL;
using Model;
using Model.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PropertyService : IPropertyService
    {
        //IDbContextBase context = DbContextFactory.GetDbContext();

        public PagedList<Property> GetAllProperties(Property property, string orderBy, string orderAs, int page)
        {
            try
            {
                using (IDbContextBase context = DbContextFactory.GetDbContext())
                {
                    string name = property == null ? "" : property.Name + "";
                    Expression<Func<Property, bool>> conditions = obj => obj.Name.Contains(name);
                    PagedList<Property> listProperty = context.FindAllByPage<Property>(conditions, orderBy, orderAs, page);

                    return listProperty;
                }
            }
            catch
            {

                throw;
            }
        }

        public Property GetPropertyById(Guid propertyId)
        {
            try
            {
                using (IDbContextBase context = DbContextFactory.GetDbContext())
                {
                    return context.Find<Property>(propertyId);
                }
            }
            catch 
            {
                
                throw;
            }
        }

        public bool InsertProperty(Property property)
        {
            try
            {
                using (IDbContextBase context = DbContextFactory.GetDbContext())
                {
                    property.Id = Guid.NewGuid();
                    context.Insert<Property>(property, false);
                    return context.SaveChanges();
                }
            }
            catch
            {

                throw;
            }
        }

        public bool UpdateProperty(Property property)
        {
            try
            {
                using (IDbContextBase context = DbContextFactory.GetDbContext())
                {
                    context.Update<Property>(property,false);
                    return context.SaveChanges();
                }
            }
            catch
            {

                throw;
            }
        }

        public bool DeleteProperty(Guid propertyId)
        {
            try
            {
                using (IDbContextBase context = DbContextFactory.GetDbContext())
                {
                    context.Delete<Property>(propertyId, false);

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
