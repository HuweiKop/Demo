using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDbContextBase : IDisposable
    {
        DbContextBase DisabledLazyLoading();
        DbContextBase EnabledLazyLoading();
        IDbContextBase CloseLog();
        IDbContextBase OpenLog();
        bool Update<T>(T entity, bool isSave = true) where T : class, new();
        bool Insert<T>(T entity, bool isSave = true) where T : class, new();
        bool Delete<T>(T entity, bool isSave = true) where T : class, new();
        bool Delete<T>(Guid id, bool isSave = true) where T : class, new();
        bool Delete<T>(Expression<Func<T, bool>> conditions, bool isSave = true) where T : class, new();
        T Find<T>(params object[] keyValues) where T : class, new();
        T FindFirstOrDefault<T>(Expression<Func<T, bool>> conditions = null) where T : class, new();
        List<T> FindAll<T>(Expression<Func<T, bool>> conditions = null) where T : class, new();
        IQueryable<T> FindAllIQueryable<T>(Expression<Func<T, bool>> conditions = null) where T : class, new();
        PagedList<T> FindAllByPage<T>(Expression<Func<T, bool>> conditions, string orderBy, string orderAs, int pageIndex, int pageSize = 10) where T : class, new();
        bool SaveChanges();
    }
}
