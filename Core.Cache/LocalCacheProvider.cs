using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Cache
{
    public class LocalCacheProvider : ICacheProvider
    {
        public object Get(string name)
        {
            return Caching.Get(name);
        }

        public void Set(string name, object value)
        {
            Caching.Set(name, value);
        }

        public void Remove(string name)
        {
            Caching.Remove(name);
        }
    }
}
