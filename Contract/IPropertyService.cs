using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public interface IPropertyService
    {
        PagedList<Property> GetAllProperties(Property property, string orderBy, string orderAs, int page);

        Property GetPropertyById(Guid propertyId);



        bool InsertProperty(Property property);

        bool UpdateProperty(Property property);

        bool DeleteProperty(Guid propertyId);
    }
}
