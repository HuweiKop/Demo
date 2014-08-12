using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Cache
{
    public interface ICacheProvider
    {
        object Get(string key);
        void Set(string key, object value);
        void Remove(string key);
    }
}
