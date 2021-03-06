﻿using System;
using System.Collections.Generic;
using System.Data;

namespace DbConnector.Adapter
{
    public interface IDbAdapter
    {
        IDbCommand DbCommand { get; }
        IDbConnection DbConnection { get; }

        int ExecuteQuery(string storedProcedure, IDbDataParameter[] parameters, Action<IDbDataParameter[]> returnParameters = null);
        //void ExecuteReader(string storedProcedure, IDbDataAdapter[] parameters = null);
        object ExecuteScalar(string storedProcedure, IDbDataParameter[] parameters = null);
        void LoadFromDataSets(string storedProcedure, Action<IDataReader, int> dataMapper, IDbDataParameter[] parameters = null);
        IEnumerable<T> LoadObject<T>(string storedProcedure, IDbDataParameter[] parameters = null) where T : class;
        IEnumerable<T> LoadObject<T>(string storedProcedure, Func<IDataReader, T> mapper, IDbDataParameter[] parameters = null);
    }
}