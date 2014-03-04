using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace MvcDI.Util
{
    /// <summary>
    /// string型共通クラス
    /// </summary>
    public class StringUtil
    {
        /// <summary>
        /// ToString表示内容生成
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string ToStringBuilder(object o)
        {
            var sb = new StringBuilder();
            var t = o.GetType();
            var pi = t.GetProperties();

            sb.Append(t.Name).Append(" ");
            sb.Append("Property ( ");
            foreach (var p in pi)
            {
                if (p.PropertyType.IsGenericType == false)
                {
                    sb.Append(p.Name).Append("[").Append(p.GetValue(o, null)).Append("] ");
                }
            }
            sb.Append(")");

            var fi = t.GetFields();

            sb.Append(t.Name);
            sb.Append("Field (");
            foreach (var f in fi)
            {
                if (f.FieldType.IsGenericType == false)
                {
                    sb.Append(f.Name).Append("[").Append(f.GetValue(o)).Append("] ");
                }
            }
            sb.Append(")");
            return sb.ToString();
        }
    }
}
