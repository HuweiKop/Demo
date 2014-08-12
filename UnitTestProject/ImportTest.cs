using BLLCommon;
using DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace UnitTestProject
{
    [TestClass]
    public class ImportTest
    {
        [TestMethod]
        public void ImporttoExcelTest()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string[] arr = path.Split('\\');
            path = "";
            for (int i = 0; i < arr.Length - 2; i++)
            {
                path += arr[i] + "\\";
            }
            string fileName = path + "ImportData.xlsx";

            Import3 import = new Import3();
            import.ImporttoExcel(fileName);
        }

        [TestMethod]
        public void test()
        {
            string s = "";
            int l = s.Length;
            SqlService service = SqlServiceFactory.GetSQLService();
            string sql = @"update Property set [des]='" + s + @"'
where id='A8284ECC-91FC-47B5-A0B3-11EA6C78627E'";
            service.ExecuteNonQueryReturnBool(sql);
            //for (int i = 0; i < 400; i++)
            //{
            //    s += "aaaaaaaaaa";
            //}
            //var s1 = s;
        }
    }
}
