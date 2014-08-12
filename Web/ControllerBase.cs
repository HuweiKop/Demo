using BLLCommon;
using Contract;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Web
{
    public abstract class ControllerBase : Controller
    {
        public virtual ICommonModelService CommonModelService
        {
            get
            {
                return ServiceContext.Current.CommonModelService;
            }
        }

        public virtual IAccountService AccountService
        {
            get 
            {
                return ServiceContext.Current.AccountService;
            }
        }

        public virtual IPropertyService PropertyService
        {
            get
            {
                return ServiceContext.Current.PropertyService;
            }
        }

        public virtual ICityService CityService
        {
            get
            {
                return ServiceContext.Current.CityService;
            }
        }

        public virtual IDistrictService DistrictService
        {
            get
            {
                return ServiceContext.Current.DistrictService;
            }
        }
    }
}
