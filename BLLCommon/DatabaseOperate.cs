using DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLCommon
{
    public class DatabaseOperate
    {
        public void ClearBusinessData()
        {
            SqlService sqlService = SqlServiceFactory.GetSQLService();

            string sql = @"select name from sysobjects where xtype='U'
and name != 'User'
and name != 'Role'
and name != 'User_Role'
and name != 'Resource'
and name != 'Permission'
and name != 'LoginInfo'
and name != 'Log4net'
and name != 'BrowseInfo'
and name != 'sysdiagrams'";

            List<string> listTableName = sqlService.QueryOneColumn(sql);
            List<string> listSql = new List<string>();

            foreach (string tableName in listTableName)
            {
                sql = "delete from " + tableName;
                listSql.Add(sql);
            }

            sqlService.ExecuteTransactionReturnCount(listSql);
        }
    }
}
