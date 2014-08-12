using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class SetValueTools
    {
        /// <summary>
        /// 通过DataTable设置对象中的属性
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="dt"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static T SetPropertyValue<T>(DataTable dt, int i) where T : new()
        {
            T typeClass = new T();
            PropertyInfo[] propertyInfo = typeof(T).GetProperties();
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                foreach (PropertyInfo p in propertyInfo)
                {
                    if (dt.Columns[j].ColumnName.Equals(p.Name) && dt.Rows[i][j] != null)
                    {
                        if (p.PropertyType.ToString().Contains("System.Double"))
                        {
                            double d = 0;
                            if (double.TryParse(Convert.ToString(dt.Rows[i][j]), out d))
                            {
                                p.SetValue(typeClass, d);
                            }
                        }
                        else if (p.PropertyType.ToString().Contains("System.Int32"))
                        {
                            int num = 0;
                            if (int.TryParse(Convert.ToString(dt.Rows[i][j]), out num))
                            {
                                p.SetValue(typeClass, num);
                            }
                        }
                        else if (p.PropertyType.ToString().Contains("System.DateTime"))
                        {
                            DateTime time = new DateTime();
                            if (DateTime.TryParse(Convert.ToString(dt.Rows[i][j]), out time))
                            {
                                p.SetValue(typeClass, time);
                            }
                        }
                        else if (p.PropertyType.ToString().Contains("System.Guid"))
                        {
                            Guid id = new Guid();
                            if (Guid.TryParse(Convert.ToString(dt.Rows[i][j]), out id))
                            {
                                p.SetValue(typeClass, id);
                            }
                        }
                        else if (p.PropertyType.ToString().Contains("System.Decimal"))
                        {
                            decimal d = 0;
                            if (decimal.TryParse(Convert.ToString(dt.Rows[i][j]), out d))
                            {
                                p.SetValue(typeClass, d);
                            }
                        }
                        else
                        {
                            p.SetValue(typeClass, dt.Rows[i][j].ToString());
                        }
                    }
                }
            }

            return typeClass;
        }

        public static List<T> SetPropertyValue<T>(DataTable dt) where T : new()
        {
            try
            {
                List<T> listClass = new List<T>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    T typeClass = new T();
                    typeClass = SetPropertyValue<T>(dt, i);
                    listClass.Add(typeClass);
                }

                return listClass;
            }
            catch { throw; }
        }

        /// <summary>
        /// 通过对象设置对象中的属性
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T SetPropertyValue<T>(T t, ref T t1) where T : new()
        {
            T typeClass = new T();
            PropertyInfo[] propertyInfo = typeof(T).GetProperties();
            foreach (PropertyInfo p in propertyInfo)
            {
                if (typeof(T).GetProperty(p.Name).GetValue(t) != null)
                {
                    if (p.PropertyType.ToString().Equals("System.Nullable`1[System.Double]"))
                    {
                        double d = 0;
                        if (double.TryParse(Convert.ToString(typeof(T).GetProperty(p.Name).GetValue(t)), out d))
                        {
                            p.SetValue(typeClass, d);
                        }
                    }
                    else if (p.PropertyType.ToString().Equals("System.Nullable`1[System.Int32]"))
                    {
                        int num = 0;
                        if (int.TryParse(Convert.ToString(typeof(T).GetProperty(p.Name).GetValue(t)), out num))
                        {
                            p.SetValue(typeClass, num);
                        }
                    }
                    else if (p.PropertyType.ToString().Equals("System.Nullable`1[System.DateTime]"))
                    {
                        DateTime time = new DateTime();
                        if (DateTime.TryParse(Convert.ToString(typeof(T).GetProperty(p.Name).GetValue(t)), out time))
                        {
                            p.SetValue(typeClass, time);
                        }
                    }
                    else
                    {
                        p.SetValue(t1, typeof(T).GetProperty(p.Name).GetValue(t));
                    }
                }
            }

            return typeClass;
        }

        public static void SetPropertyNullValue<T>(ref T t)
        {
            PropertyInfo[] propertyInfo = typeof(T).GetProperties();
            foreach (PropertyInfo p in propertyInfo)
            {
                if (typeof(T).GetProperty(p.Name).GetValue(t) == null)
                {
                    p.SetValue(t, "");
                }
            }
        }
    }
}
