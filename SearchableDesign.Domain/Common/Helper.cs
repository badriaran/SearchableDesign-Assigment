using System.Globalization;

namespace SearchableDesign.Domain.Helper
{
    public  class Helper
    {
        public static int ToInt32(string value)
        {
            if (string.IsNullOrEmpty(value))
                return 0;
            return int.Parse(value, (IFormatProvider)CultureInfo.CurrentCulture);
        }
        public static double ToDouble(string value)
        {
            if (string.IsNullOrEmpty(value))
                return 0;
            return double.Parse(value, System.Globalization.CultureInfo.InvariantCulture);
        }
        public static string GetDBNUllString(object val)
        {
            if (val == DBNull.Value)
                return string.Empty;
            else
            {
                return val.ToString();

            }
        }
        public static string GetDBNUllDateString(object val, string format)
        {
            if (val == DBNull.Value)
                return string.Empty;
            else
            {
                if (DateTime.TryParse(val.ToString(), out DateTime dateString))
                    return dateString.ToString(format);
                else
                    return string.Empty;
            }
        }
    }
}
