using Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SqlService
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        protected static readonly string strConn = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

        /// <summary>
        /// 查询方法，返回Dataset
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <returns></returns>
        public DataSet QueryDataSet(string strSql)
        {
            try
            {
                DataSet ds = QueryDataSet(strSql, null);

                return ds;
            }
            catch { throw; }
        }

        /// <summary>
        /// 查询方法，返回Dataset
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <param name="sqlParameterList">参数</param>
        /// <returns></returns>
        public DataSet QueryDataSet(string strSql, List<SqlParameter> sqlParameterList)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(strConn))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = strSql;
                        if (sqlParameterList != null)
                        {
                            for (int i = 0; i < sqlParameterList.Count; i++)
                            {
                                sqlParameterList[i].Value = sqlParameterList[i].Value.ToString().Replace("[", "[[]");
                                sqlCommand.Parameters.Add(sqlParameterList[i]);
                            }
                        }
                        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                        DataSet dataSet = new DataSet();
                        sqlDataAdapter.Fill(dataSet);
                        return dataSet;
                    }
                }
            }
            catch { throw; }
        }

        /// <summary>
        /// 查询方法，返回DataTable
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <returns></returns>
        public DataTable QueryDataTable(string strSql)
        {
            try
            {
                DataTable dt = QueryDataTable(strSql, null);

                return dt;
            }
            catch { throw; }
        }

        /// <summary>
        /// 查询方法
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <param name="sqlParameterList">参数</param>
        /// <returns></returns>
        public DataTable QueryDataTable(string strSql, List<SqlParameter> sqlParameterList)
        {
            try
            {
                DataTable dt = QueryDataSet(strSql, sqlParameterList).Tables[0];

                return dt;
            }
            catch { throw; }
        }

        public string QueryOneField(string strSql, List<SqlParameter> sqlParameterList)
        {
            try
            {
                DataTable dt = QueryDataTable(strSql, sqlParameterList);
                return dt.Rows.Count > 0 ? dt.Rows[0][0].ToString() : "";
            }
            catch { throw; }
        }

        public string QueryOneField(string strSql)
        {
            try
            {
                return QueryDataTable(strSql).Rows[0][0].ToString();
            }
            catch { throw; }
        }

        public List<string> QueryOneColumn(string strSql, List<SqlParameter> sqlParameterList, SqlConnection conn)
        {
            try
            {
                DataTable dt = QueryDataTable(strSql, sqlParameterList, conn);
                List<string> list = new List<string>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    list.Add(dt.Rows[i][0].ToString());
                }
                return list;
            }
            catch { throw; }
        }

        public List<string> QueryOneColumn(string strSql, List<SqlParameter> sqlParameterList)
        {
            try
            {
                DataTable dt = QueryDataTable(strSql, sqlParameterList);
                List<string> list = new List<string>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    list.Add(dt.Rows[i][0].ToString());
                }
                return list;
            }
            catch { throw; }
        }

        public List<string> QueryOneColumn(string strSql)
        {
            try
            {
                DataTable dt = QueryDataTable(strSql);
                List<string> list = new List<string>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    list.Add(dt.Rows[i][0].ToString());
                }
                return list;
            }
            catch { throw; }
        }

        public DataSet QueryDataSet(string strSql, List<SqlParameter> sqlParameterList, SqlConnection conn)
        {
            try
            {
                if (conn == null)
                {
                    conn = new SqlConnection();
                }
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                using (SqlCommand sqlCommand = conn.CreateCommand())
                {
                    sqlCommand.CommandText = strSql;
                    if (sqlParameterList != null)
                    {
                        for (int i = 0; i < sqlParameterList.Count; i++)
                        {
                            sqlParameterList[i].Value = sqlParameterList[i].Value.ToString().Replace("[", "[[]");
                            sqlCommand.Parameters.Add(sqlParameterList[i]);
                        }
                    }
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    DataSet dataSet = new DataSet();
                    sqlDataAdapter.Fill(dataSet);
                    return dataSet;
                }
            }
            catch
            {

                throw;
            }
        }

        public DataTable QueryDataTable(string strSql, List<SqlParameter> sqlParameterList, SqlConnection conn)
        {
            try
            {
                DataTable dt = QueryDataSet(strSql, sqlParameterList, conn).Tables[0];

                return dt;
            }
            catch { throw; }
        }

        public string QueryOneField(string strSql, List<SqlParameter> sqlParameterList, SqlConnection conn)
        {
            try
            {
                string field = QueryDataTable(strSql, sqlParameterList, conn).Rows[0][0].ToString();

                return field;
            }
            catch { throw; }
        }

        /// <summary>
        /// 执行操作，返回受影响行数
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <returns></returns>
        public int ExecuteNonQueryReturnCount(string strSql)
        {
            try
            {
                int intCount = ExecuteNonQueryReturnCount(strSql, null);

                return intCount;
            }
            catch { throw; }
        }

        /// <summary>
        /// 执行操作，返回受影响行数
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <param name="sqlParameterList">参数</param>
        /// <returns></returns>
        public int ExecuteNonQueryReturnCount(string strSql, List<SqlParameter> sqlParameterList)
        {
            try
            {
                int intCount = 0;

                using (SqlConnection sqlConnection = new SqlConnection(strConn))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = strSql;
                        if (sqlParameterList != null)
                        {
                            for (int i = 0; i < sqlParameterList.Count; i++)
                            {
                                sqlCommand.Parameters.Add(sqlParameterList[i]);
                            }
                        }
                        intCount = sqlCommand.ExecuteNonQuery();

                        return intCount;
                    }
                }
            }
            catch { throw; }
        }

        /// <summary>
        /// 执行操作，返回是否执行成功
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <returns></returns>
        public bool ExecuteNonQueryReturnBool(string strSql)
        {
            try
            {
                bool bol = ExecuteNonQueryReturnBool(strSql, null);

                return bol;
            }
            catch { throw; }
        }

        /// <summary>
        /// 执行操作，返回是否执行成功
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <param name="sqlParameterList">参数</param>
        /// <returns></returns>
        public bool ExecuteNonQueryReturnBool(string strSql, List<SqlParameter> sqlParameterList)
        {
            try
            {
                bool bol = false;
                int intCount = 0;

                intCount = ExecuteNonQueryReturnCount(strSql, sqlParameterList);
                if (intCount > 0)
                {
                    bol = true;
                }

                return bol;
            }
            catch { throw; }
        }

        /// <summary>
        /// 事物提交处理，如果有一条未执行成功，则整体回退
        /// </summary>
        /// <param name="listSql">SQL语句</param>
        /// <param name="listListParameter">参数</param>
        /// <returns></returns>
        public int ExecuteTransactionReturnCount(List<string> listSql, List<List<SqlParameter>> listListParameter)
        {

            int intCount = 0;
            using (SqlConnection sqlConnection = new SqlConnection(strConn))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
                    sqlCommand.Transaction = sqlTransaction;
                    int f = 0;
                    try
                    {
                        for (int i = 0; i < listSql.Count; i++)
                        {
                            sqlCommand.CommandText = listSql[i];
                            for (int j = 0; i < listListParameter.Count && listListParameter[i] != null && j < listListParameter[i].Count; j++)
                            {
                                sqlCommand.Parameters.Add(listListParameter[i][j]);
                            }
                            f = i;
                            intCount = sqlCommand.ExecuteNonQuery();
                            sqlCommand.Parameters.Clear();
                        }

                        sqlTransaction.Commit();

                        return intCount;
                    }
                    catch(Exception ex)
                    {
                        int a = f;
                        sqlTransaction.Rollback();
                        throw;
                    }

                }
            }
        }

        public int ExecuteTransactionReturnCount(List<string> listSql)
        {
            try
            {
                return ExecuteTransactionReturnCount(listSql, new List<List<SqlParameter>>());
            }
            catch 
            {
                
                throw;
            }
            
        }

        public List<T> GetEntities<T>(string strSql, List<SqlParameter> listPar) where T : new()
        {
            try
            {
                DataTable dt;
                List<T> listClass = new List<T>();

                if (listPar != null)
                {
                    dt = QueryDataTable(strSql, listPar);
                }
                else
                {
                    dt = QueryDataTable(strSql);
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    T typeClass = new T();
                    typeClass = SetValueTools.SetPropertyValue<T>(dt, i);
                    listClass.Add(typeClass);
                }

                return listClass;
            }
            catch { throw; }
        }

        public List<T> GetEntities<T>(string strSql) where T : new()
        {
            try
            {
                return GetEntities<T>(strSql, null);
            }
            catch { throw; }
        }

        public List<T> GetEntities<T>(string strSql, List<SqlParameter> listPar,SqlConnection conn) where T : new()
        {
            try
            {
                DataTable dt;
                List<T> listClass = new List<T>();

                if (listPar == null)
                {
                    listPar = new List<SqlParameter>();
                }
                dt = QueryDataTable(strSql, listPar, conn);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    T typeClass = new T();
                    typeClass = SetValueTools.SetPropertyValue<T>(dt, i);
                    listClass.Add(typeClass);
                }

                return listClass;
            }
            catch { throw; }
        }

        public T GetEntity<T>(string strSql, List<SqlParameter> listPar) where T : new()
        {
            try
            {
                DataTable dt;
                List<T> listClass = new List<T>();

                if (listPar != null)
                {
                    dt = QueryDataTable(strSql, listPar);
                }
                else
                {
                    dt = QueryDataTable(strSql);
                }

                T typeClass = new T();
                typeClass = SetValueTools.SetPropertyValue<T>(dt, 0);
                listClass.Add(typeClass);

                return typeClass;
            }
            catch { throw; }
        }

        public T GetEntity<T>(string strSql) where T : new()
        {
            try
            {
                return GetEntity<T>(strSql, null);
            }
            catch { throw; }
        }

    }
    
}
