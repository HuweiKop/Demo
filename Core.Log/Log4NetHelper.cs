using Common;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Log
{
    public static class Log4NetHelper
    {
        private static readonly string strConn = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

        static Log4NetHelper()
        {
            string config = FileConfigHelper.GetConfig("log4net");

            config = config.Replace("{connectionString}", strConn);
            MemoryStream ms = new MemoryStream(Encoding.Default.GetBytes(config));
            log4net.Config.XmlConfigurator.Configure(ms);
        }

        public static void Error(object message, Exception ex)
        {
            var logger = LogManager.GetLogger("ServiceExceptionLog");
            logger.Error(SerializeObject(message), ex);
        }

        private static string SerializeObject(object message)
        {
            if (message is string || message == null)
                return message + "";
            else
                return JsonConvert.SerializeObject(message, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }
    }
}
