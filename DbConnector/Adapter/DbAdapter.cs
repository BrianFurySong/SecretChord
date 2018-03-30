using DbConnector.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConnector.Adapter
{
    public class DbAdapter : IDbAdapter
    {
        public IDbCommand DbCommand { get; private set; }//for the crud
        public IDbConnection DbConnection { get; private set; }//for connections
        private const int _timeout = 5000;

        public DbAdapter(IDbCommand dbCommand, IDbConnection dbConnection)//a way to populate those (a const is a way for the class to be used)
        {
            DbCommand = dbCommand;
            DbConnection = dbConnection;
        }

        public void LoadFromDataSets(string storedProcedure, Action<IDataReader, int> dataMapper, IDbDataParameter[] parameters = null)
        {
            try
            {
                if (string.IsNullOrEmpty(storedProcedure))
                    throw new ArgumentException("Missing stored procedure");
                int result = 0;
                using (IDbConnection conn = DbConnection)
                using (IDbCommand cmd = DbCommand)
                {
                    if (conn.State != ConnectionState.Open) { conn.Open(); }
                    cmd.CommandTimeout = _timeout;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = storedProcedure;
                    cmd.Connection = conn;
                    if (parameters != null)
                        foreach (IDbDataParameter param in parameters)
                            cmd.Parameters.Add(param);

                    IDataReader reader = cmd.ExecuteReader();

                    while (true)
                    {

                        while (reader.Read())
                        {
                            dataMapper?.Invoke(reader, result);
                        }
                        result += 1;
                        if (reader.IsClosed || !reader.NextResult())
                            break;

                        if (result > 10)
                            throw new Exception("Too many result sets returned!");

                    }

                }
            }
            catch { throw; }
        }

        //public void ExecuteReader(string storedProcedure, IDbDataAdapter[] parameters = null)
        //{
        //    using (IDbConnection conn = DbConnection)
        //    using (IDbCommand cmd = DbCommand)
        //    {
        //        if (conn.State != ConnectionState.Open) { conn.Open(); }
        //        cmd.CommandTimeout = _timeout;//try that long b4 it says it doesn't work
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = storedProcedure;
        //        cmd.Connection = conn;//knows how to connect to ur db

        //        if (parameters != null)
        //        {
        //            foreach (IDbDataParameter param in parameters)
        //            {
        //                cmd.Parameters.Add(param);
        //            }
        //        }

        //        IDataReader reader = cmd.ExecuteReader();//takes data one at a time from db and sends it to 'reader'
        //        while (reader.Read())//while there is something to read in the reader
        //        {
        //            string fname = reader["firstName"].ToString();//basic reader
        //            //do something: process recordset
        //        }
        //    }
        //}

        public IEnumerable<T> LoadObject<T>(string storedProcedure, IDbDataParameter[] parameters = null) where T : class
        {
            List<T> items = new List<T>();

            using (IDbConnection conn = DbConnection)
            using (IDbCommand cmd = DbCommand)
            {
                if (conn.State != ConnectionState.Open) { conn.Open(); }
                cmd.CommandTimeout = _timeout;//try that long b4 it says it doesn't work
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = storedProcedure;
                cmd.Connection = conn;//knows how to connect to ur db

                if (parameters != null)
                {
                    foreach (IDbDataParameter param in parameters)
                    {
                        cmd.Parameters.Add(param);
                    }
                }

                IDataReader reader = cmd.ExecuteReader();//takes data one at a time from db and sends it to 'reader'
                while (reader.Read())//while there is something to read in the reader
                {
                    items.Add(DataMapper<T>.Instance.MapToObject(reader));

                }
            }
            return items;
        }

        public IEnumerable<T> LoadObject<T>(string storedProcedure, Func<IDataReader, T> mapper, IDbDataParameter[] parameters = null)
        {//an anon delegate 'the func'
            List<T> items = new List<T>();
            using (IDbConnection conn = DbConnection)
            using (IDbCommand cmd = DbCommand)
            {
                if (conn.State != ConnectionState.Open) { conn.Open(); }
                cmd.CommandTimeout = _timeout;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = storedProcedure;
                cmd.Connection = conn;

                if (parameters != null)
                {
                    foreach (IDbDataParameter param in parameters)
                    {
                        cmd.Parameters.Add(param);
                    }
                }

                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    items.Add(mapper(reader));
                }
                return items;
            }
        }

        //insert
        public int ExecuteQuery(string storedProcedure, IDbDataParameter[] parameters, Action<IDbDataParameter[]> returnParameters = null)
        {//action says i'll just take input and i'll just execute it
            using (IDbConnection conn = DbConnection)
            using (IDbCommand cmd = DbCommand)
            {
                if (conn.State != ConnectionState.Open) { conn.Open(); }
                cmd.CommandTimeout = _timeout;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = storedProcedure;
                cmd.Connection = conn;

                if (parameters != null)
                {
                    foreach (IDbDataParameter param in parameters)
                    {
                        cmd.Parameters.Add(param);
                    }
                }

                int returnVal = cmd.ExecuteNonQuery();

                if (returnParameters != null)
                {
                    returnParameters(parameters);
                }
                return returnVal;
            }
        }

        //scalar: if u wanna return 1 thing
        public object ExecuteScalar(string storedProcedure, IDbDataParameter[] parameters = null)
        {
            using (IDbConnection conn = DbConnection)
            using (IDbCommand cmd = DbCommand)
            {
                if (conn.State != ConnectionState.Open) { conn.Open(); }
                cmd.CommandTimeout = _timeout;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = storedProcedure;
                cmd.Connection = conn;

                if (parameters != null)
                {
                    foreach (IDbDataParameter param in parameters)
                    {
                        cmd.Parameters.Add(param);
                    }
                }

                return cmd.ExecuteScalar();
            }
        }


    }
}

