using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Core.Cache
{
    public class CacheHelper
    {
        public static object Get(string name)
        {
            return Caching.Get(name);
        }

        public static void Set(string name, object value)
        {
            Caching.Set(name, value);
        }

        public static void Remove(string name)
        {
            Caching.Remove(name);
        }

        public static F Get<F>(string key, Func<F> getRealData)
        {
            F data = default(F);
            var cacheData = Get(key);
            if (cacheData == null)
            {
                data = getRealData();
                if (data != null)
                {
                    Set(key, data);
                }
            }
            else
            {
                data = (F)cacheData;
            }

            return data;
        }

        public static F GetItem<F>(string key, Func<F> getRealData)
        {
            if (HttpContext.Current == null)
                return getRealData();

            var httpContextItem = HttpContext.Current.Items;
            if (httpContextItem.Contains(key))
            {
                return (F)httpContextItem[key];
            }
            else
            {
                var data = getRealData();
                if (data != null)
                    httpContextItem[key] = data;
                return data;
            }
        }
    }
}
