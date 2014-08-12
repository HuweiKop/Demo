using DAL;
using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BLLCommon
{
    public class Import2
    {
        public void ImporttoExcel(string fileName)
        {
            SqlService sqlService = SqlServiceFactory.GetSQLService();
            FileInfo existingFile = new FileInfo(fileName);
            Dictionary<string, int> dictHeader = new Dictionary<string, int>();
            Hashtable hashTables = new Hashtable();
            Hashtable hash = null;
            XmlDocument dom = new XmlDocument();
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string[] arr = path.Split('\\');
            path = "";
            for (int i = 0; i < arr.Length - 2; i++)
            {
                path += arr[i] + "\\";
            }
            dom.Load(path + "ImportExcel2.xml");//装载XML文档 

            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                ExcelWorksheet commonWorksheet = package.Workbook.Worksheets[1];

                int colStart = commonWorksheet.Dimension.Start.Column;  //工作区开始列
                int colEnd = commonWorksheet.Dimension.End.Column;       //工作区结束列
                int rowStart = commonWorksheet.Dimension.Start.Row;       //工作区开始行号

                string mergeHeader = "";

                for (int i = colStart; i <= colEnd; i++)
                {
                    string headerName = commonWorksheet.Cells[rowStart, i].Value + "" + commonWorksheet.Cells[rowStart + 1, i].Value;
                    if (commonWorksheet.Cells[rowStart, i].Merge)
                    {
                        if (commonWorksheet.Cells[rowStart, i].Value != null)
                        {
                            mergeHeader = commonWorksheet.Cells[rowStart, i].Value + "";
                        }
                        else
                        {
                            headerName = mergeHeader + commonWorksheet.Cells[rowStart + 1, i].Value;
                        }
                    }
                    headerName = headerName.Replace("\n", "");
                    dictHeader[headerName] = i;
                }

                bool loadData = false;
                foreach (ExcelWorksheet worksheet in package.Workbook.Worksheets)
                {
                    int rowEnd = worksheet.Dimension.End.Row;       //工作区结束行号

                    foreach (XmlElement model in dom.DocumentElement.ChildNodes)
                    {
                        string tableName = model.SelectSingleNode("name").InnerText;
                        List<string> listSql = new List<string>();

                        XmlNode keys = model.SelectSingleNode("tableUniques");
                        if (!loadData)
                        {
                            hash = new Hashtable();
                            if (keys != null)
                            {
                                hash = GetDataBaseUnique(tableName, keys);
                            }
                            if (!hashTables.Contains(tableName))
                            {
                                hashTables.Add(tableName, hash);
                            }
                        }
                        else
                        {
                            hash = (Hashtable)hashTables[tableName];
                        }

                        for (int i = rowStart + 2; i <= rowEnd; i++)
                        {
                            bool whitespace = false;
                            string sql = "";
                            string fields = "";
                            string values = "";
                            XmlNode xmlFields = model.SelectSingleNode("fields");
                            string unique = "";
                            foreach (XmlElement field in xmlFields.ChildNodes)
                            {

                                string fieldName = field.SelectSingleNode("name").InnerText;
                                string columnName = "";
                                object fieldValue = null;

                                int row = i;
                                //if (field.SelectSingleNode("row") != null)
                                //{
                                //    row = Convert.ToInt32(field.SelectSingleNode("row").InnerText);
                                //}
                                if (field.SelectSingleNode("columns") != null)
                                {
                                    XmlNode columns = field.SelectSingleNode("columns");
                                    foreach (XmlElement column in columns)
                                    {
                                        row = i;
                                        if (!string.IsNullOrWhiteSpace(column.GetAttribute("row")))
                                        { row = Convert.ToInt32(column.GetAttribute("row")); }
                                        columnName = column.InnerText;
                                        fieldValue += worksheet.Cells[row, dictHeader[columnName]].Value + "";
                                    }
                                }
                                else
                                {
                                    if (field.SelectSingleNode("column").Attributes["row"] != null)
                                    { row = Convert.ToInt32(field.SelectSingleNode("column").Attributes["row"].Value); }
                                    columnName = field.SelectSingleNode("column").InnerText;
                                    fieldValue = worksheet.Cells[row, dictHeader[columnName]].Value;
                                }

                                if ("yes".Equals(field.GetAttribute("unique")))
                                {
                                    if (string.IsNullOrWhiteSpace(fieldValue + ""))
                                    {
                                        whitespace = true;
                                        break;
                                    } 
                                    unique += fieldValue;
                                }
                                if ("yes".Equals(field.GetAttribute("notnull")))
                                {
                                    if (string.IsNullOrWhiteSpace(fieldValue + ""))
                                    {
                                        whitespace = true;
                                        break;
                                    }
                                }
                                if ("yes".Equals(field.GetAttribute("fk")))
                                {
                                    string referenceTable = field.GetAttribute("referenceTable");
                                    fieldValue = (fieldValue == null) ? null : ((Hashtable)hashTables[referenceTable])[fieldValue];
                                }
                                fields += "[" + fieldName + "],";
                                if (fieldValue == null)
                                {
                                    values += "NULL,";
                                }
                                else
                                {
                                    values += "N'" + (fieldValue + "").Replace("'", "''") + "',";
                                }
                            }
                            if (whitespace) 
                                break;

                            if (hash.Contains(unique))
                            {
                                continue;
                            }

                            string id = "";
                            id = Guid.NewGuid() + "";
                            if (!string.IsNullOrWhiteSpace(unique))
                            {
                                hash.Add(unique, id);
                            }
                            if (fields.Length > 0)
                            {
                                fields = fields.Substring(0, fields.Length - 1);
                                values = values.Substring(0, values.Length - 1);
                            }
                            sql += "insert into " + tableName + " (Id,";
                            sql += fields;
                            sql += ") values ('" + id + "',";
                            sql += values;
                            sql += ")";
                            listSql.Add(sql);
                        }
                        sqlService.ExecuteTransactionReturnCount(listSql);
                    }
                    loadData = true;
                }
            }
        }

        private Hashtable GetDataBaseUnique(string tableName, XmlNode tableUniques)
        {
            try
            {
                SqlService sqlService = SqlServiceFactory.GetSQLService();

                Hashtable ht = new Hashtable();
                StringBuilder sql = new StringBuilder();
                StringBuilder sbFields = new StringBuilder();
                StringBuilder sbJoinTable = new StringBuilder();
                foreach (XmlElement unique in tableUniques.ChildNodes)
                {
                    if (unique.SelectSingleNode("joinTable") == null)
                    {
                        sbFields.Append(tableName);
                        sbFields.Append(".");
                        sbFields.Append(unique.SelectSingleNode("field").InnerText);
                        sbFields.Append("+");
                    }
                    else
                    {
                        XmlNode joinTable = unique.SelectSingleNode("joinTable");
                        sbFields.Append(joinTable.InnerText);
                        sbFields.Append(".");
                        sbFields.Append(unique.SelectSingleNode("field").InnerText);
                        sbFields.Append("+");

                        sbJoinTable.Append(" join ");
                        sbJoinTable.Append(joinTable.InnerText);
                        sbJoinTable.Append(" on ");
                        sbJoinTable.Append(joinTable.Attributes["onTable"].Value);
                        sbJoinTable.Append(".");
                        sbJoinTable.Append(joinTable.Attributes["onField"].Value);
                        sbJoinTable.Append("=");
                        sbJoinTable.Append(joinTable.InnerText);
                        sbJoinTable.Append(".");
                        sbJoinTable.Append(joinTable.Attributes["joinField"].Value);
                    }
                }
                sbFields.Remove(sbFields.Length - 1, 1);
                sbFields.Append(" as [Key],");
                sbFields.Append(tableName);
                sbFields.Append(".Id as [Value]");

                sql.Append("select ").Append(sbFields).Append(" from ").Append(tableName).Append(sbJoinTable);

                DataTable dt = sqlService.QueryDataTable(sql.ToString());

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ht.Add(dt.Rows[i]["Key"], dt.Rows[i]["Value"]);
                }

                return ht;
            }
            catch
            {

                throw;
            }
        }
    }
}
