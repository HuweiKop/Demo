using BLLCommon;
using Contract;
using Core.Cache;
using Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web
{
    public class ServiceContext
    {
        public static ServiceContext Current
        {
            get
            {
                return CacheHelper.GetItem<ServiceContext>("ServiceContext", () => { return new ServiceContext(); });
            }
        }

        public ICommonModelService CommonModelService
        {
            get
            {
                return ServiceHelper.CreateService<ICommonModelService>();
            }
        }

        public IAccountService AccountService
        {
            get
            {
                return ServiceHelper.CreateService<IAccountService>();
            }
        }

        public IPropertyService PropertyService
        {
            get
            {
                return ServiceHelper.CreateService<IPropertyService>();
            }
        }

        public ICityService CityService
        {
            get
            {
                return ServiceHelper.CreateService<ICityService>();
            }
        }

        public IDistrictService DistrictService
        {
            get
            {
                return ServiceHelper.CreateService<IDistrictService>();
            }
        }
    }
}
