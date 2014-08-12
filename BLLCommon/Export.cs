using DAL;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BLLCommon
{
    public class Export
    {
        public void ExporttoExcel(string fileName, string destinationFolder)
        {
            try
            {
                List<DataTable> listTable = GetDataSet();
                MemoryStream stream = GetStream(listTable);
                SaveMemoryStream(stream, fileName, destinationFolder);
            }
            catch 
            {
                
                throw;
            }
        }

        public MemoryStream GetStream(List<DataTable> listTable)
        {
            MemoryStream stream = new MemoryStream();

            using (ExcelPackage package = new ExcelPackage(stream))
            {
                for (int table = 0; table < listTable.Count; table++)
                {
                    DataTable dt = listTable[table];
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(dt.TableName);

                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        worksheet.Cells[1, i + 1].Value = dt.Columns[i].ColumnName;
                    }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            worksheet.Cells[i + 2, j + 1].Value = dt.Rows[i][j];
                        }
                    }
                }

                package.Save();
                return stream;
            }
        }

        public List<DataTable> GetDataSet()
        {
            try
            {
                SqlService sqlService = SqlServiceFactory.GetSQLService();
                XmlDocument dom = new XmlDocument();
                List<DataTable> listTable = new List<DataTable>();
                string path = AppDomain.CurrentDomain.BaseDirectory;
                string[] arr = path.Split('\\');
                path = "";
                for (int i = 0; i < arr.Length - 2; i++)
                {
                    path += arr[i] + "\\";
                }
                dom.Load(path + "ExportExcel2.xml");//装载XML文档 

                foreach (XmlElement sheet in dom.DocumentElement.ChildNodes)
                {
                    StringBuilder fields = new StringBuilder();
                    StringBuilder joinTables = new StringBuilder();
                    string tableName = sheet.SelectSingleNode("table").InnerText;
                    string sheetName = sheet.SelectSingleNode("name").InnerText;
                    XmlNode columns = sheet.SelectSingleNode("columns");
                    foreach (XmlElement column in columns)
                    {
                        string field = column.SelectSingleNode("field").InnerText;
                        string showColumn = column.SelectSingleNode("name").InnerText;
                        if ("yes".Equals(column.GetAttribute("fk")))
                        {
                            joinTables.Append(" left join ");
                            joinTables.Append(column.GetAttribute("referenceTable"));
                            joinTables.Append(" on ");
                            if (column.SelectSingleNode("joinTable") == null)
                            {
                                joinTables.Append(tableName);
                            }
                            else
                            {
                                joinTables.Append(column.SelectSingleNode("joinTable").InnerText);
                            }
                            joinTables.Append(".");
                            joinTables.Append(field);
                            joinTables.Append("=");
                            joinTables.Append(column.GetAttribute("referenceTable"));
                            joinTables.Append(".");
                            joinTables.Append(column.GetAttribute("referenceColumn"));

                            fields.Append(column.GetAttribute("referenceTable"));
                            fields.Append(".");
                            fields.Append(column.GetAttribute("showColumn"));
                            fields.Append(" as '");
                            fields.Append(showColumn);
                            fields.Append("',");
                        }
                        else
                        {
                            fields.Append(field);
                            fields.Append(" as '");
                            fields.Append(showColumn);
                            fields.Append("',");
                        }
                    }
                    if (fields.Length > 0)
                    {
                        fields.Remove(fields.Length - 1, 1);
                        string sql = "select " + fields + " from " + tableName + " " + joinTables;

                        DataTable dt = sqlService.QueryDataTable(sql);
                        dt.TableName = sheetName;

                        listTable.Add(dt);
                    }
                }
                return listTable;
            }
            catch 
            {
                
                throw;
            }
        }

        private static void SaveMemoryStream(MemoryStream stream, string fileName, string destinationFolder)
        {
            string fullPath = Path.Combine(destinationFolder, fileName);
            if (File.Exists(fullPath))
            {
                try
                {
                    File.Delete(fullPath);
                }
                catch (Exception ex)
                {
                    //如果该文件正在打开时会报异常，所以须重新取名
                    fileName = string.Format("{0}_{1}{2}", Path.GetFileNameWithoutExtension(fullPath), Guid.NewGuid().ToString(), Path.GetExtension(fullPath));
                    fullPath = Path.Combine(destinationFolder, fileName);
                }
            }
            FileStream outStream = File.Open(fullPath, FileMode.OpenOrCreate);
            stream.WriteTo(outStream);
            outStream.Flush();
            outStream.Close();
        }
    }
}
