using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    //public class AuditLog
    //{
    //    public Guid Id { get; set; }
    //    public string UserName { get; set; }
    //    public string ModuleName { get; set; }
    //    public string TableName { get; set; }
    //    public string EventType { get; set; }
    //    public string NewValues { get; set; }
    //}

    public class LogDbContext 
    {
        public void WriteLog(string userName, string moduleName, string tableName, string eventType, object newValues)
        {
            using (IDbContextBase context = DbContextFactory.GetDbContext())
            {
                context.CloseLog();
                AuditLog audiLog = new AuditLog();
                audiLog.Id = Guid.NewGuid();
                audiLog.UserName = userName;
                audiLog.ModuleName = moduleName;
                audiLog.TableName = tableName;
                audiLog.EventType = eventType;
                audiLog.NewValues = JsonConvert.SerializeObject(newValues, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                audiLog.Time = DateTime.Now;
                context.Insert(audiLog);
            }
        }
    }
}
