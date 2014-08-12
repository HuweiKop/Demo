using Contract;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLLCommon
{
    public class CommonModelService : ICommonModelService
    {
        public List<T> GetAllData<T>(Expression<Func<T, bool>> conditions = null) where T : class,new()
        {
            using (IDbContextBase context = DbContextFactory.GetDbContext())
            {
                return context.FindAll<T>(conditions);
            }
        }

        public bool InsertData<T>(T t) where T : class,new()
        {
            using (IDbContextBase context = DbContextFactory.GetDbContext())
            {
                return context.Insert<T>(t);
            }
        }
    }
}
