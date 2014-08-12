using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class ListHelper
    {
        public static List<T> ListDesc<T>(this List<T> list)
        {
            List<T> newList = new List<T>();
            for (int i = list.Count - 1; i >= 0; i--)
            {
                newList.Add(list[i]);
            }
            return newList;
        }
    }
}
