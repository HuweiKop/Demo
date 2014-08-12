using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class AssemblyHelper
    {
        public static T FindTypeByInterface<T>(string searchPattern = "BLL*.dll") where T : class
        {
            var interfaceType = typeof(T);

            string domain = GetBaseDirectory();
            string[] dllFiles = Directory.GetFiles(domain, searchPattern, SearchOption.TopDirectoryOnly);

            foreach (string dllFileName in dllFiles)
            {
                foreach (Type type in Assembly.LoadFrom(dllFileName).GetLoadableTypes())
                {
                    if (interfaceType != type && interfaceType.IsAssignableFrom(type))
                    {
                        var instance = Activator.CreateInstance(type) as T;
                        return instance;
                    }
                }
            }

            return null;
        }

        public static IEnumerable<Type> GetLoadableTypes(this Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.Where(t => t != null);
            }
        }

        public static string GetBaseDirectory()
        {
            string baseDirectory = AppDomain.CurrentDomain.SetupInformation.PrivateBinPath;

            if (baseDirectory == null)
            {
                baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            }

            return baseDirectory;
        }
    }
}
