using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Web;

namespace Cority.Helper
{
    public static class EnumExtension
    {
        public static string ToDescriptionString(this Enum val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val
               .GetType()
               .GetField(val.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description.ToSafeLogString() : val.ToString();
        }
        public static string ToSafeLogString(this string str)
        {
            if (String.IsNullOrEmpty(str))
                return String.Empty;
            try
            {
                return HttpUtility.HtmlEncode(str.Replace('\r', '_').Replace('\n', '_').Replace('\0', '_'));
            }
            catch (Exception err)
            {
                Trace.TraceError($"ToSafeLogString Error {err}");
                return "(unsafe string omitted)";
            }
        }
    }
}
