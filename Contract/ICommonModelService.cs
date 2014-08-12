using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public interface ICommonModelService
    {
        List<T> GetAllData<T>(Expression<Func<T, bool>> conditions = null) where T : class,new();

        bool InsertData<T>(T t) where T : class,new();
    }
}
