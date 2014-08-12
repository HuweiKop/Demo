using BLLCommon;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject
{
    [TestClass]
    public class ExportTest
    {
        [TestMethod]
        public void GetDataTableTest()
        {
            Export2 export = new Export2();
            export.ExporttoExcel("a.xlsx","d:\\");
        }
    }
}
