using Common;
using Core.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public abstract class ServiceFactory
    {
        public abstract T CreateService<T>() where T : class;
    }

    public class RefServiceFactory : ServiceFactory
    {
        public override T CreateService<T>()
        {
            var interfaceName = typeof(T);
            return CacheHelper.Get(string.Format("Service_{0}", interfaceName), () =>
            {
                return AssemblyHelper.FindTypeByInterface<T>();
            });
        }
    }
}
