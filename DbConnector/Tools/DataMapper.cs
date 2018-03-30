using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConnector.Tools//SINGLETON : datamapper
{
    public sealed class DataMapper<T> where T : class// it HAS to be a class cuz of the condition. dont have to instantiate and only 1 copy 
    {
        private System.Reflection.PropertyInfo[] props;//array of properties from our class
        private static readonly DataMapper<T> _instance = new DataMapper<T>();//this is calling the private contructor

        private DataMapper()//these 2 prevent you from doing a NEW instatiation
        {
            props = typeof(T).GetProperties();//grabs all the properties here so it doesn't doit every single time in the MapToObject
        }

        static DataMapper() { }//part of the requriemnts for the singlton pattern

        public static DataMapper<T> Instance { get { return _instance; } }

        public T MapToObject(IDataReader reader)
        {
            IEnumerable<string> colnames =
                reader.GetSchemaTable().Rows.Cast<DataRow>().Select(
                    c => c["ColumnName"].ToString().ToLower()).ToList();

            T obj = Activator.CreateInstance<T>();//reflection

            foreach (System.Reflection.PropertyInfo prop in props)
            {
                if (colnames.Contains(prop.Name.ToLower()))
                {
                    if (reader[prop.Name] != DBNull.Value)
                    {
                        if (reader[prop.Name].GetType() == typeof(decimal))
                        {
                            prop.SetValue(obj, (reader.GetDouble(prop.Name)));
                        }
                        else
                        {
                            prop.SetValue(obj,
                            (reader.GetValue(reader.GetOrdinal(prop.Name)) ?? null),
                            null);
                        }
                    }
                }
            }
            return obj;
        }
    }

    public static class DataHelper
    {


        //overloading and methoed extensions 
        public static double GetDouble(this DataRow dr, string columnName)//gets sql string and converts it to double for c#
        {
            double dbl = 0;
            double.TryParse(dr[columnName].ToString(), out dbl);
            return dbl;
        }

        public static double GetDouble(this DataRow dr, int columnIndex)
        {
            double dbl = 0;
            double.TryParse(dr[columnIndex].ToString(), out dbl);
            return dbl;
        }

        public static double GetDouble(this IDataReader reader, string columnName)
        {
            double dbl = 0;
            double.TryParse(reader[columnName].ToString(), out dbl);
            return dbl;
        }

        public static double GetDouble(this IDataReader reader, int columnIndex)
        {
            double dbl = 0;
            double.TryParse(reader[columnIndex].ToString(), out dbl);
            return dbl;
        }

        public static T GetParamValue<T>(this IDataParameter[] dbParams,//an extension
            string paramName)//u can try a try catch and finally here (finally is always called reguardless of try or catch)
        {
            foreach (IDataParameter param in dbParams)
            {
                if (param.ParameterName.ToLower() == paramName.ToLower())
                {
                    return (T)Convert.ChangeType(param.Value, typeof(T));
                }
            }
            return default(T);
        }

    }
}
