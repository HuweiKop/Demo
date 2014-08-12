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
    public class Import3
    {
        public void ImporttoExcel(string fileName)
        {

            FileInfo existingFile = new FileInfo(fileName);
            Dictionary<string, int> dictHeader = null;

            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                SqlService sqlService = SqlServiceFactory.GetSQLService();
                XmlDocument dom = new XmlDocument();
                Hashtable hashTables = new Hashtable();
                Hashtable hash = null;
                string path = AppDomain.CurrentDomain.BaseDirectory;
                string[] arr = path.Split('\\');
                path = "";
                for (int i = 0; i < arr.Length - 2; i++)
                {
                    path += arr[i] + "\\";
                }
                dom.Load(path + "ImportExcel.xml");//装载XML文档 
                foreach (XmlElement model in dom.DocumentElement.ChildNodes)
                {
                    string tableName = model.SelectSingleNode("name").InnerText;
                    List<string> listSql = new List<string>();

                    hash = new Hashtable();

                    if (!hashTables.Contains(tableName))
                    {
                        XmlNode keys = model.SelectSingleNode("tableUniques");
                        if (keys != null)
                        {
                            hash = GetDataBaseUnique(tableName, keys);
                        }
                        hashTables.Add(tableName, hash);
                    }
                    else
                    {
                        hash = (Hashtable)hashTables[tableName];
                    }
                    if (string.IsNullOrWhiteSpace(model.GetAttribute("data")))
                    {
                        //读取数据 
                        string sheetName = model.SelectSingleNode("sheetName").InnerText;
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[sheetName];


                        int colStart = worksheet.Dimension.Start.Column;  //工作区开始列
                        int colEnd = worksheet.Dimension.End.Column;       //工作区结束列
                        int rowStart = worksheet.Dimension.Start.Row;       //工作区开始行号
                        int rowEnd = worksheet.Dimension.End.Row;       //工作区结束行号

                        dictHeader = new Dictionary<string, int>();
                        //将每列标题添加到字典中
                        for (int i = colStart; i <= colEnd; i++)
                        {
                            string headerName = worksheet.Cells[rowStart, i].Value + "";
                            headerName = headerName.Replace("\n", "");
                            dictHeader[headerName] = i;
                        }

                        for (int i = rowStart + 1; i <= rowEnd; i++)
                        {
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

                                if (field.SelectSingleNode("columns") != null)
                                {
                                    XmlNode columns = field.SelectSingleNode("columns");
                                    foreach (XmlElement column in columns)
                                    {
                                        columnName = column.InnerText;
                                        fieldValue += worksheet.Cells[i, dictHeader[columnName]].Value + "";
                                    }
                                }
                                else
                                {
                                    if (field.SelectSingleNode("value") == null)
                                    {
                                        columnName = field.SelectSingleNode("column").InnerText;
                                        fieldValue = worksheet.Cells[i, dictHeader[columnName]].Value;
                                    }
                                    else
                                    {
                                        fieldValue = field.SelectSingleNode("value").InnerText;
                                    }
                                }

                                if (string.IsNullOrWhiteSpace(fieldValue + "") && !string.IsNullOrWhiteSpace(field.GetAttribute("defaultValue")))
                                    fieldValue = field.GetAttribute("defaultValue");

                                if ("yes".Equals(field.GetAttribute("unique")))
                                { unique += fieldValue; }
                                if ("yes".Equals(field.GetAttribute("fk")))
                                {
                                    string referenceTable = field.GetAttribute("referenceTable");
                                    fieldValue = ((Hashtable)hashTables[referenceTable])[fieldValue];
                                }
                                fields += "[" + fieldName + "],";
                                values += "N'" + (fieldValue + "").Replace("'", "''") + "',";
                            }

                            if (hash.Contains(unique))
                            {
                                continue;
                            }

                            string id = "";
                            id = Guid.NewGuid() + "";
                            hash.Add(unique, id);
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
                    }
                    else
                    {
                        XmlNode xmlFields = model.SelectSingleNode("record");
                        foreach (XmlElement record in xmlFields.ChildNodes)
                        {
                            string fields = "";
                            string values = "";
                            string unique = "";
                            string sql = "";
                            foreach (XmlElement field in record.ChildNodes)
                            {
                                string fieldValue = field.SelectSingleNode("data").InnerText;

                                if (string.IsNullOrWhiteSpace(fieldValue + "") && !string.IsNullOrWhiteSpace(field.GetAttribute("defaultValue")))
                                    fieldValue = field.GetAttribute("defaultValue");

                                if ("yes".Equals(field.GetAttribute("unique")))
                                { unique += fieldValue; }

                                fields += "[" + field.SelectSingleNode("name").InnerText + "],";
                                values += "N'" + (fieldValue + "").Replace("'", "''") + "',";
                            }

                            if (hash.Contains(unique))
                            {
                                continue;
                            }

                            string id = "";
                            id = Guid.NewGuid() + "";
                            hash.Add(unique, id);
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
                    }
                    sqlService.ExecuteTransactionReturnCount(listSql);
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
