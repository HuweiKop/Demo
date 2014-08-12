using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace Common
{
    public class Caching
    {
        public static object Get(string name)
        {
            return HttpRuntime.Cache.Get(name);
        }

        public static void Remove(string name)
        {
            if (HttpRuntime.Cache[name] != null)
                HttpRuntime.Cache.Remove(name);
        }

        public static void Set(string name, object value)
        {
            HttpRuntime.Cache.Insert(name, value, null, DateTime.Now.AddMinutes(20), Cache.NoSlidingExpiration);
        }

        public static void Clear()
        {
            var enumerator = HttpRuntime.Cache.GetEnumerator();

            while (enumerator.MoveNext())
            {
                HttpRuntime.Cache.Remove(enumerator.Key + "");
            }
        }
    }
}
