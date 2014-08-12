using Common;
using Core.Cache;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// DAL基类，实现Repository通用泛型数据访问模式
    /// </summary>
    public class DbContextBase : IDbContextBase
    {
        static string connStr = ConfigurationManager.ConnectionStrings["Entities"].ConnectionString;
        DbContext context = new DbContext(connStr);
        bool isLog = true;

        public DbContextBase()
        {
            //var objectContext = (this as IObjectContextAdapter).ObjectContext;
            //objectContext.CommandTimeout = 500;

            //this.Database.Connection.ConnectionString = connStr;
            //context.Configuration.LazyLoadingEnabled = false;
            //this.Configuration.ProxyCreationEnabled = false;
        }

        public DbContextBase DisabledLazyLoading()
        {
            try
            {
                context.Configuration.LazyLoadingEnabled = false;
                return this;
            }
            catch 
            {
                
                throw;
            }
        }

        public DbContextBase EnabledLazyLoading()
        {
            try
            {
                context.Configuration.LazyLoadingEnabled = true;
                return this;
            }
            catch
            {

                throw;
            }
        }

        public IDbContextBase CloseLog()
        {
            isLog = false;
            return this;
        }

        public IDbContextBase OpenLog()
        {
            isLog = true;
            return this;
        }

        public bool Update<T>(T entity, bool isSave = true) where T : class, new()
        {
            try
            {
                var set = context.Set<T>();
                set.Attach(entity);
                context.Entry<T>(entity).State = EntityState.Modified;
                if (!isSave) { return false; }
                return SaveChanges();
            }
            catch 
            {
                
                throw;
            }
            
        }

        public bool Insert<T>(T entity, bool isSave = true) where T : class, new()
        {
            try
            {
                context.Set<T>().Add(entity);
                if (!isSave) { return false; }
                return SaveChanges();
            }
            catch 
            {
                
                throw;
            }
            
        }

        public bool Delete<T>(T entity, bool isSave = true) where T : class, new()
        {
            try
            {
                context.Entry<T>(entity).State = EntityState.Deleted;
                if (!isSave) { return false; }
                return SaveChanges();
            }
            catch 
            {
                
                throw;
            }
            
        }

        public bool Delete<T>(Guid id, bool isSave = true) where T : class, new()
        {
            try
            {
                T entity = context.Set<T>().Find(id);
                context.Entry<T>(entity).State = EntityState.Deleted;
                if (!isSave) { return false; }
                return SaveChanges();
            }
            catch
            {

                throw;
            }

        }

        public bool Delete<T>(Expression<Func<T, bool>> conditions, bool isSave = true) where T : class, new()
        {
            try
            {
                var entities = context.Set<T>().Where(conditions);
                foreach (T entity in entities)
                {
                    context.Entry<T>(entity).State = EntityState.Deleted;
                }
                if (!isSave) { return false; }
                return SaveChanges();
            }
            catch
            {

                throw;
            }

        }

        public T Find<T>(params object[] keyValues) where T : class, new()
        {
            try
            {
                return context.Set<T>().Find(keyValues);
            }
            catch 
            {
                
                throw;
            }
            
        }

        public T FindFirstOrDefault<T>(Expression<Func<T, bool>> conditions = null) where T : class, new()
        {
            try
            {
                if (conditions == null)
                {
                    return context.Set<T>().FirstOrDefault();
                }
                return context.Set<T>().FirstOrDefault(conditions);
            }
            catch
            {

                throw;
            }

        }

        public List<T> FindAll<T>(Expression<Func<T, bool>> conditions = null) where T : class, new()
        {
            try
            {
                return FindAllIQueryable<T>(conditions).ToList<T>();
            }
            catch 
            {
                
                throw;
            }
            
        }

        public IQueryable<T> FindAllIQueryable<T>(Expression<Func<T, bool>> conditions = null) where T : class, new()
        {
            try
            {
                if (conditions == null)
                    return context.Set<T>();
                else
                    return context.Set<T>().Where(conditions);
            }
            catch 
            {
                
                throw;
            }
        }

        public PagedList<T> FindAllByPage<T>(Expression<Func<T, bool>> conditions, string orderBy, string orderAs, int pageIndex, int pageSize = 10) where T : class, new()
        {
            try
            {
                var queryList = conditions == null ? context.Set<T>() : context.Set<T>().Where(conditions) as IQueryable<T>;

                return queryList.OrderBy(orderBy, orderAs).ToPagedList(pageIndex, pageSize);
            }
            catch
            {

                throw;
            }
        }

        

        public bool SaveChanges()
        {
            try
            {
                if (isLog)
                    WriteAuditLog();
                var result = context.SaveChanges();
                return result > 0;
            }
            catch 
            {
                
                throw;
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }

        internal void WriteAuditLog()
        {
            LogDbContext logContext = new LogDbContext();

            foreach (var dbEntry in context.ChangeTracker.Entries().Where(p => p.State == EntityState.Added || p.State == EntityState.Deleted || p.State == EntityState.Modified))
            {

                string operaterName = CacheHelper.Get("UserName").ToString();

                Task.Factory.StartNew(() =>
                {
                    var tableAttr = dbEntry.Entity.GetType().GetCustomAttributes(typeof(TableAttribute), false).SingleOrDefault() as TableAttribute;
                    string tableName = tableAttr != null ? tableAttr.Name : dbEntry.Entity.GetType().Name;
                    var moduleName = dbEntry.Entity.GetType().FullName.Split('.').Skip(1).FirstOrDefault();

                    logContext.WriteLog(operaterName, moduleName, tableName, dbEntry.State.ToString(), dbEntry.Entity);
                });
            }

        }
    }
}
