using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Data;


namespace Toch
{
    public class Utils
    {
        // Fields
        private static string FALSEWORDS = "NF0";
        private static string TRUEWORDS = "YSVT1";

        // Methods
        public static string BooleanToString(bool flag)
        {
            if (flag)
            {
                return "S";
            }
            return "N";
        }

        public static bool FieldAsBoolean(IDataReader dr, string fieldName)
        {
            try
            {
                return (bool)FieldAsObject(dr, fieldName);
            }
            catch
            {
                return StringToBoolean(FieldAsString(dr, fieldName));
            }
        }

        public static DateTime FieldAsDateTime(IDataReader dr, string fieldName)
        {
            return (DateTime)FieldAsObject(dr, fieldName);
        }

        public static decimal FieldAsDecimal(IDataReader dr, string fieldName)
        {
            return !dr.IsDBNull(dr.GetOrdinal(fieldName)) ? (decimal)FieldAsObject(dr, fieldName) : 0;
        }

        public static float FieldAsFloat(IDataReader dr, string fieldName)
        {
            return !dr.IsDBNull(dr.GetOrdinal(fieldName)) ? (float)FieldAsObject(dr, fieldName) : 0;
        }

        public static double FieldAsDouble(IDataReader dr, string fieldName)
        {
            return !dr.IsDBNull(dr.GetOrdinal(fieldName)) ? (double)FieldAsObject(dr, fieldName) : 0;
        }

        public static int FieldAsInt32(IDataReader dr, string fieldName)
        {
            return !dr.IsDBNull(dr.GetOrdinal(fieldName)) ? (int)FieldAsObject(dr, fieldName) : 0;
        }

        public static int FieldAsInt16(IDataReader dr, string fieldName)
        {
            return !dr.IsDBNull(dr.GetOrdinal(fieldName)) ? (Int16)FieldAsObject(dr, fieldName) : 0;
        }

        private static object FieldAsObject(IDataReader dr, string fieldName)
        {
            return dr.GetValue(dr.GetOrdinal(fieldName));
        }

        public static string FieldAsString(IDataReader dr, string fieldName)
        {
            return !dr.IsDBNull(dr.GetOrdinal(fieldName)) ? FieldAsObject(dr, fieldName).ToString() : String.Empty;
        }

        public static bool FieldIsNull(IDataReader dr, string fieldName)
        {
            return dr.IsDBNull(dr.GetOrdinal(fieldName));
        }

        public static string ObterDescricao(Enum valor)
        {
            DescriptionAttribute[] customAttributes = (DescriptionAttribute[])valor.GetType().GetField(valor.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (customAttributes.Length <= 0)
            {
                return valor.ToString();
            }
            return (customAttributes[0].Description ?? "Nulo");
        }

        public static bool StringToBoolean(string str)
        {
            str = str.Trim();
            if (str == string.Empty)
            {
                return false;
            }
            if (str.Length == 1)
            {
                str = str.ToUpper();
                if (TRUEWORDS.Contains(str))
                {
                    return true;
                }
                if (FALSEWORDS.Contains(str))
                {
                    return false;
                }
            }
            throw new Exception(string.Format("ERRO(001): O valor {0} n\x00e3o \x00e9 v\x00e1lido.", str));
        }
    }
}
