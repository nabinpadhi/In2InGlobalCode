using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Configuration;
using System.Reflection;
using System.Management;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Security;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

/*
* Description : SMARTDataAccessHelper is user defined custom class - access point for database operations.
* Author : KSS Pvt. Ltd.
* Copyright : Copyright ©2014 by KSS ITTPM. All rights reserved
*/
namespace InGlobal.DataAccess
{
    #region User defined custom data library
    /// <summary>
    /// User defined custom class - access point for database operations.
    /// </summary>
    /// 
    public class KSSITTPMDataAccessHelper
    {

        private bool isADConnection;
	
        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public KSSITTPMDataAccessHelper()
        {
            Singleton = Singleton.GetConnectionString();
        }
        public KSSITTPMDataAccessHelper(bool rb_isADConnection)
        {
            this.isADConnection = rb_isADConnection;
            Singleton = Singleton.GetConnectionString();
        }       
        #endregion

        #region Declaration
        DataSet dataSet = new DataSet();
        IDataReader dataReader;
        DbCommand dbCommand = null;
        SqlDatabase sqlDatabase = null;
        DbConnection dbConnection;
        DbTransaction dbTransaction;
        int loopCount;
        int rowsAffected;

        Singleton Singleton;
        string connectionString;
        #endregion

        #region GetDbConnection
        /// <summary>
        /// Get database connection object (connection key).
        /// </summary>
        /// <param name="dbConnectionKey">Determines the database connection string for usage</param>
        /// <returns>Database connection object</returns>
        private object GetDbConnection()
        {
            object dbConnection = null;
            try
            {
                if (this.isADConnection)
                {
                    connectionString = Singleton.ht["ADConnection"].ToString();
                }
                else
                {
                    connectionString = Singleton.ht["KSSDBConnection"].ToString();
                }

                SqlDatabase sqlDatabase = new SqlDatabase(connectionString);
                dbConnection = sqlDatabase;
                
                return dbConnection;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region GetDbCommand for SQL Query
        /// <summary>
        /// Get database command object for sql query text.
        /// </summary>
        /// <param name="dbConnection">Database connection object</param>
        /// <param name="sql">Sql query text</param>
        /// <returns>Database command object</returns>
        private DbCommand GetDbCommand(object dbConnection, string sql)
        {
            DbCommand dbCmd = null;
            try
            {

                SqlDatabase sqlDb = (SqlDatabase)dbConnection;
                dbCmd = sqlDb.GetSqlStringCommand(sql);
                dbCmd.CommandTimeout = 3600;
                return dbCmd;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region GetDbCommand for Stored Procedure
        /// <summary>
        /// Get database command object for stored procedure.
        /// </summary>
        /// <param name="dbConnection">Database connection object</param>
        /// <param name="storedProcedureName">Stored procedure name</param>
        /// <param name="parameterValues">Parameter values (Object type)</param>
        /// <returns>Database command object</returns>
        private DbCommand GetDbCommand(object dbConnection, string storedProcedureName, params object[] parameterValues)
        {
            DbCommand dbCmd = null;
            try
            {

                SqlDatabase sqlDb = (SqlDatabase)dbConnection;
                if (parameterValues != null)
                {
                    dbCmd = sqlDb.GetStoredProcCommand(storedProcedureName, parameterValues);
                }
                else
                {
                    dbCmd = sqlDb.GetStoredProcCommand(storedProcedureName);
                }

                dbCmd.CommandTimeout = 3600;
                return dbCmd;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region GetOutputParameter
        /// <summary>
        /// Get the output parameter hashtable for stored procedure.
        /// </summary>
        /// <param name="databaseCommand">Database command object</param>
        /// <returns>Out parameter (hashtable)</returns>
        private Hashtable GetOutputParameter(DbCommand databaseCommand)
        {
            int dbcmdParameterCount;
            string outputParamName;
            string outputParamValue;
            Hashtable outputParamHashTbl = new Hashtable();

            try
            {
                dbcmdParameterCount = databaseCommand.Parameters.Count;

                for (loopCount = 0; loopCount < dbcmdParameterCount; loopCount++)
                {
                    if (databaseCommand.Parameters[loopCount].Direction == ParameterDirection.Output && databaseCommand.Parameters[loopCount].DbType != DbType.Object)
                    {
                        outputParamName = databaseCommand.Parameters[loopCount].ParameterName.ToString();
                        outputParamValue = databaseCommand.Parameters[loopCount].Value.ToString();

                        outputParamHashTbl.Add(outputParamName, outputParamValue);
                    }
                }
                return outputParamHashTbl;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region GetDataSet for SQL Query
        /// <summary>
        /// Get dataset from sql query text.
        /// </summary>
        /// <param name="dbConnectionKey">Determines the database connection string</param>
        /// <param name="sql">Sql query text</param>
        /// <returns>Dataset</returns>

        #endregion

        #region GetDataSet for Stored Procedure
        /// <summary>
        /// Get dataset from stored procedure.
        /// </summary>
        /// <param name="dbConnectionKey">Determines the database connection string</param>
        /// <param name="storedProcedureName">Stored procedure name</param>
        /// <param name="outputParamHashTable">Out parameter (hashtable)</param>
        /// <param name="parameterValues">Parameter values (Object type)</param>
        /// <returns>Dataset</returns>
        public DataSet GetDataSet(string storedProcedureName, ref Hashtable outputParamHashTable, params object[] parameterValues)
        {
            try
            {

                sqlDatabase = (SqlDatabase)GetDbConnection();
                dbCommand = GetDbCommand(sqlDatabase, storedProcedureName, parameterValues);
                dataSet = sqlDatabase.ExecuteDataSet(dbCommand);

                outputParamHashTable = GetOutputParameter(dbCommand);
                return dataSet;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetDataSet(string storedProcedureNameOrSQL)
        {
            try
            {

                sqlDatabase = (SqlDatabase)GetDbConnection();
                dbCommand = GetDbCommand(sqlDatabase, storedProcedureNameOrSQL);
                dataSet = sqlDatabase.ExecuteDataSet(dbCommand);
                return dataSet;
            }
            catch (Exception)
            {
                throw;
            }
        }/*
        public DataSet GetDataSet(string directSQL)
        {
            try
            {

                sqlDatabase = (SqlDatabase)GetDbConnection();
                dbCommand = GetDbCommand(sqlDatabase,directSQL);
                dataSet = sqlDatabase.ExecuteDataSet(dbCommand);
                return dataSet;
            }
            catch (Exception)
            {
                throw;
            }
        }*/
        public DataSet GetDataSet(string storedProcedureName, SqlParameter[] sqlParams)
        {
            try
            {
                int index = 0;
                object[] parameterValues = new object[sqlParams.Length];
                foreach (SqlParameter spm in sqlParams)
                {
                    parameterValues[index] = new object();
                    parameterValues[index] = spm.Value;
                    index = index + 1;
                }

                sqlDatabase = (SqlDatabase)GetDbConnection();
                dbCommand = GetDbCommand(sqlDatabase, storedProcedureName, parameterValues);
                dataSet = sqlDatabase.ExecuteDataSet(dbCommand);
                return dataSet;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet GetDataSet(string storedProcedureName, bool useTransaction, SqlParameter[] sqlParams)
        {
            DbTransaction currentTrasection = null;
            try
            {
                int index = 0;
                object[] parameterValues = new object[sqlParams.Length];
                foreach (SqlParameter spm in sqlParams)
                {
                    parameterValues[index] = new object();
                    parameterValues[index] = spm.Value;
                    index = index + 1;
                }

                sqlDatabase = (SqlDatabase)GetDbConnection();
                dbCommand = GetDbCommand(sqlDatabase, storedProcedureName, parameterValues);
                if (dbConnection == null || dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection = sqlDatabase.CreateConnection();
                    dbConnection.Open();
                }
                if (useTransaction)
                {
                    currentTrasection = dbConnection.BeginTransaction();                    
                    dataSet = sqlDatabase.ExecuteDataSet(dbCommand,currentTrasection );
                    currentTrasection.Commit();
                }
                else
                {
                    dataSet = sqlDatabase.ExecuteDataSet(dbCommand);
                }
            }
            catch (Exception)
            {
                if (dbConnection != null && dbConnection.State == ConnectionState.Open)
                {
                    if (currentTrasection != null) { currentTrasection.Rollback(); }

                    dbConnection.Close();
                }
                throw;
            }
            return dataSet;
        }

        #endregion

        #region GetDataReader for SQL Query
        /// <summary>
        /// Get datareader from sql query text.
        /// </summary>
        /// <param name="dbConnectionKey">Determines the database connection string</param>
        /// <param name="sql">Sql query text</param>
        /// <returns>DataReader</returns>
        public IDataReader GetDataReader(string sql)
        {
            try
            {

                sqlDatabase = (SqlDatabase)GetDbConnection();
                dbCommand = GetDbCommand(sqlDatabase, sql);
                dataReader = sqlDatabase.ExecuteReader(dbCommand);

                return dataReader;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region GetDataReader for Stored Procedure
        /// <summary>
        /// Get datareader from stored procedure.
        /// </summary>
        /// <param name="dbConnectionKey">Determines the database connection string</param>
        /// <param name="storedProcedureName">Stored procedure name</param>
        /// <param name="outputParamHashTable">Out parameter (hashtable)</param>
        /// <param name="parameterValues">Parameter values (Object type)</param>
        /// <returns>DataReader</returns>
        public IDataReader GetDataReader(string storedProcedureName, ref Hashtable outputParamHashTable, params object[] parameterValues)
        {
            try
            {

                sqlDatabase = (SqlDatabase)GetDbConnection();
                dbCommand = GetDbCommand(sqlDatabase, storedProcedureName, parameterValues);
                dataReader = sqlDatabase.ExecuteReader(dbCommand);

                outputParamHashTable = GetOutputParameter(dbCommand);

                return dataReader;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region GetScalar for Stored Procedure Query
        /// <summary>
        /// Get scalar values from sql query text.
        /// </summary>
        /// <param name="dbConnectionKey">Determines the database connection string</param>
        /// <param name="sql">Sql query text</param>
        /// <returns>Scalar Object</returns>
        public object GetScalar(string spName, params object[] parameterValues)
        {
            object scalarValue = null;
            try
            {

                sqlDatabase = (SqlDatabase)GetDbConnection();
                dbCommand = GetDbCommand(sqlDatabase, spName, parameterValues);
                scalarValue = sqlDatabase.ExecuteScalar(dbCommand);

                return scalarValue;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public object GetScalar(string spName, SqlParameter[] sqlParams)
        {
            object returnValue = null;
            try
            {
                int index = 0;
                object[] parameterValues = new object[sqlParams.Length];
                foreach (SqlParameter spm in sqlParams)
                {
                    parameterValues[index] = new object();
                    parameterValues[index] = spm.Value;
                    index = index + 1;
                }

                sqlDatabase = (SqlDatabase)GetDbConnection();
                dbCommand = GetDbCommand(sqlDatabase, spName, parameterValues);
                returnValue = sqlDatabase.ExecuteScalar(dbCommand);
                return returnValue;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public object GetScalar(string spName, ref DbTransaction currentTrasaction, SqlParameter[] sqlParams)
        {
            object scalarValue = null;
            try
            {
                int index = 0;
                object[] parameterValues = new object[sqlParams.Length];
                foreach (SqlParameter spm in sqlParams)
                {
                    parameterValues[index] = new object();
                    parameterValues[index] = spm.Value;
                    index = index + 1;
                }
                sqlDatabase = (SqlDatabase)GetDbConnection();
                if (dbConnection == null || dbConnection.State != ConnectionState.Closed)
                {
                    dbConnection = sqlDatabase.CreateConnection();
                    dbConnection.Open();
                }
                currentTrasaction = dbConnection.BeginTransaction();
                scalarValue = sqlDatabase.ExecuteScalar(currentTrasaction, spName, parameterValues);
                return scalarValue;
            }
            catch (Exception)
            {
                if (dbConnection != null && dbConnection.State == ConnectionState.Open)
                {
                    if (currentTrasaction != null) { currentTrasaction.Rollback(); }

                    dbConnection.Close();
                }
                throw;
            }
        }
        #endregion

        #region ExecuteNonQuery for Stored Procedure       
        /// <summary>
        /// Get scalar values from sql query text.
        /// </summary>
        /// <param name="dbConnectionKey">Determines the database connection string</param>
        /// <param name="sql">Sql query text</param>
        /// <returns>Scalar Object</returns>
        public int ExecuteNonQuery(string spName)
        {
            int returnValue = -1;
            try
            {                               
                sqlDatabase = (SqlDatabase)GetDbConnection();
                dbCommand = GetDbCommand(sqlDatabase, spName);
                returnValue = sqlDatabase.ExecuteNonQuery(dbCommand);
                return returnValue;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Get scalar values from sql query text.
        /// </summary>
        /// <param name="dbConnectionKey">Determines the database connection string</param>
        /// <param name="sql">Sql query text</param>
        /// <returns>Scalar Object</returns>
        public int ExecuteNonQuery(string spName, SqlParameter[] sqlParams)
        {
            int returnValue = -1;
            try
            {
                int index = 0;
                object[] parameterValues = new object[sqlParams.Length];
                foreach (SqlParameter spm in sqlParams)
                {
                    parameterValues[index] = new object();
                    parameterValues[index] = spm.Value;
                    index = index + 1;
                }

                sqlDatabase = (SqlDatabase)GetDbConnection();
                
                dbCommand = GetDbCommand(sqlDatabase, spName, parameterValues);
                returnValue = sqlDatabase.ExecuteNonQuery(dbCommand);
                return returnValue;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int ExecuteNonQuery(string spName, ref DbTransaction currentTrasaction, SqlParameter[] sqlParams)
        {
            int returnValue = -1;
            try
            {
                int index = 0;
                object[] parameterValues = new object[sqlParams.Length];
                foreach (SqlParameter spm in sqlParams)
                {
                    parameterValues[index] = new object();
                    parameterValues[index] = spm.Value;
                    index = index + 1;
                }
                sqlDatabase = (SqlDatabase)GetDbConnection();
                if (dbConnection == null || dbConnection.State != ConnectionState.Closed)
                {
                    dbConnection = sqlDatabase.CreateConnection();
                    dbConnection.Open();
                }
                currentTrasaction = dbConnection.BeginTransaction();
                returnValue = sqlDatabase.ExecuteNonQuery(currentTrasaction, spName, parameterValues);
                return returnValue;
            }
            catch (Exception)
            {
                if (dbConnection != null && dbConnection.State == ConnectionState.Open)
                {
                    if (currentTrasaction != null) { currentTrasaction.Rollback(); }

                    dbConnection.Close();
                }
                throw;
            }
        }
        #endregion

        #region HandleTransaction for SQL QUERY
        /// <summary>
        /// Handle transaction (execute non query), using sql query text.
        /// </summary>
        /// <param name="dbConnectionKey">Determines the database connection string</param>
        /// <param name="sql">Sql query text</param>
        /// <returns>Boolean</returns>
        public bool HandleTransaction(string sql)
        {
            bool status = false;
            try
            {
                sqlDatabase = (SqlDatabase)GetDbConnection();
                dbCommand = GetDbCommand(sqlDatabase, sql);
                dbConnection = sqlDatabase.CreateConnection();
                dbConnection.Open();
                dbTransaction = dbConnection.BeginTransaction();
                rowsAffected = sqlDatabase.ExecuteNonQuery(dbCommand, dbTransaction);
                dbTransaction.Commit();
                dbConnection.Close();
                status = true;
            }
            catch (Exception)
            {
                if (dbConnection != null && dbConnection.State == ConnectionState.Open)
                {
                    if (dbTransaction != null) { dbTransaction.Rollback(); }

                    dbConnection.Close();
                }
                throw;
            }

            return status;
        }
        #endregion

        #region HandleTransaction for Stored Procedure
        /// <summary>
        /// Handles transaction (execute non query), using stored procedure.
        /// </summary>
        /// <param name="dbConnectionKey">Determines the database connection string</param>
        /// <param name="storedProcedureName">Stored procedure name</param>
        /// <param name="outputParamHashTable">Out parameter hashtable</param>
        /// <param name="parameterValues">Parameter values (Object type)</param>
        /// <returns>Boolean</returns>
        public bool HandleTransaction(string storedProcedureName, ref Hashtable outputParamHashTable, params object[] parameterValues)
        {
            bool status = false;
            try
            {

                sqlDatabase = (SqlDatabase)GetDbConnection();
                dbCommand = GetDbCommand(sqlDatabase, storedProcedureName, parameterValues);
                dbConnection = sqlDatabase.CreateConnection();
                dbConnection.Open();
                dbTransaction = dbConnection.BeginTransaction();
                rowsAffected = sqlDatabase.ExecuteNonQuery(dbCommand, dbTransaction);
                dbTransaction.Commit();
                dbConnection.Close();
                outputParamHashTable = GetOutputParameter(dbCommand);
                status = true;
            }
            catch (Exception)
            {
                if (dbConnection != null && dbConnection.State == ConnectionState.Open)
                {
                    if (dbTransaction != null) { dbTransaction.Rollback(); }

                    dbConnection.Close();
                }
                throw;
            }

            return status;
        }
        #endregion

        #region GetTransaction
        /// <summary>
        /// Get transaction object.
        /// </summary>
        /// <param name="dbConnectionKey">Determines the database connection string</param>
        /// <param name="transaction">Transaction object</param>
        /// <returns>Boolean</returns>
        public bool GetTransaction(ref DbTransaction transaction)
        {
            bool status = false;
            try
            {
                sqlDatabase = (SqlDatabase)GetDbConnection();
                dbConnection = sqlDatabase.CreateConnection();
                dbConnection.Open();
                transaction = dbConnection.BeginTransaction();
                status = true;
            }
            catch (Exception)
            {
                if (dbConnection != null && dbConnection.State == ConnectionState.Open)
                {
                    if (transaction != null) { transaction.Rollback(); }

                    dbConnection.Close();
                }
                throw;
            }
            return status;
        }
        #endregion

        #region HandleTransaction for SQL QUERY with transaction management
        /// <summary>
        /// Handles transaction (execute non query with transaction object), using sql query text.
        /// </summary>
        /// <param name="dbConnectionKey">Determines the database connection string</param>
        /// <param name="sql">Sql query text</param>
        /// <param name="transaction">Transaction object</param>
        /// <returns>Boolean</returns>
        public bool HandleTransaction(string sql, ref DbTransaction transaction)
        {
            bool status = false;
            try
            {
                sqlDatabase = (SqlDatabase)GetDbConnection();
                dbCommand = GetDbCommand(sqlDatabase, sql);
                dbConnection = sqlDatabase.CreateConnection();
                rowsAffected = sqlDatabase.ExecuteNonQuery(dbCommand, transaction);
                status = true;
            }
            catch (Exception)
            {
                if (dbConnection != null && dbConnection.State == ConnectionState.Open)
                {
                    if (transaction != null) { transaction.Rollback(); }

                    dbConnection.Close();
                }
                throw;
            }

            return status;
        }
        #endregion

        #region HandleTransaction for Stored Procedure with transaction management
        /// <summary>
        /// Handles transaction (execute non query with transaction object), using stored procedure.
        /// </summary>
        /// <param name="dbConnectionKey">Determines the database connection string</param>
        /// <param name="storedProcedureName">Stored procedure name</param>
        /// <param name="transaction">Transaction object</param>
        /// <param name="outputParamHashTable">Out parameter hashtable</param>
        /// <param name="parameterValues">Parameter values (Object type)</param>
        /// <returns>Boolean</returns>
        public bool HandleTransaction(string storedProcedureName, ref DbTransaction transaction, ref Hashtable outputParamHashTable, params object[] parameterValues)
        {
            bool status = false;
            try
            {

                sqlDatabase = (SqlDatabase)GetDbConnection();
                dbCommand = GetDbCommand(sqlDatabase, storedProcedureName, parameterValues);
                rowsAffected = sqlDatabase.ExecuteNonQuery(dbCommand, transaction);
                outputParamHashTable = GetOutputParameter(dbCommand);
                status = true;
            }
            catch (Exception)
            {
                if (dbConnection != null && dbConnection.State == ConnectionState.Open)
                {
                    if (transaction != null) { transaction.Rollback(); }

                    dbConnection.Close();
                }
                throw;
            }

            return status;
        }
        #endregion

        #region SetTransactionState
        /// <summary>
        /// Set the state of the transaction (Commit / Rollback).
        /// </summary>
        /// <param name="transactionState">Transaction to be set (either Commit / Rollback)</param>
        /// <param name="transaction">Transaction object</param>
        /// <returns>Boolean</returns>
        public bool SetTransactionState(string transactionState, DbTransaction transaction)
        {
            bool status = false;
            try
            {
                if (transactionState.ToUpper() == "COMMIT") { transaction.Commit(); }
                else if (transactionState.ToUpper() == "ROLLBACK") { transaction.Rollback(); }

                dbConnection.Close();
                status = true;
            }
            catch (Exception)
            {
                if (dbConnection != null && dbConnection.State == ConnectionState.Open)
                {
                    if (transaction != null) { transaction.Rollback(); }

                    dbConnection.Close();
                }
            }

            return status;
        }
        #endregion

        #region Handle Data concurrency
        /// <summary>
        /// Handling data concurency, check whether the row is edited.
        /// </summary>
        /// <param name="tableName">Table name</param>
        /// <param name="rowID">Row identifier (Oracle)</param>
        /// <param name="lastModified">Transaction last modified date / time</param>
        /// <param name="dbConnectionKey">Determines the database connection string</param>
        /// <returns>Boolean</returns>
        public bool IsRowChanged(string tableName, string rowID, DateTime lastModified)
        {
            bool status = true;
            DateTime currentModified = DateTime.Now;
            string sql = "select modified from " + tableName + " where rowid = '" + rowID + "'";
            try
            {
                //if (dataSource.ToUpper() == OracleDataSource)
                //{
                //    oracleDatabase = (OracleDatabase)GetDbConnection();
                //    dbCommand = GetDbCommand(oracleDatabase, sql);
                //    dataSet = oracleDatabase.ExecuteDataSet(dbCommand);
                //    if ((dataSet.Tables[0] != null) && (dataSet.Tables[0].Rows.Count > 0))
                //    {
                //        currentModified = Convert.ToDateTime(dataSet.Tables[0].Rows[0]["modified"].ToString());
                //        if (currentModified.CompareTo(lastModified) == 0)
                //            status = false;
                //    }
                //}
            }
            catch (Exception)
            {
                throw;
            }

            return status;
        }
        #endregion

        #region Update Object Data

        public object UpdateObject(object mo_Object, string storedProcedureName, bool useTransaction)
        {
            object resultObject = false;
            try
            {
                dataReader = null;

                resultObject = WriteObject(mo_Object, storedProcedureName,useTransaction);

            }
            catch (Exception exception)
            {
                throw new Exception("Data Updation Failed", exception);
            }
            return resultObject;
        }
        public object UpdateObject(object mo_Object, string storedProcedureName)
        {
            object resultObject = null;
            try
            {
                dataReader = null;

                resultObject = WriteObject(mo_Object, storedProcedureName);

            }
            catch (Exception exception)
            {
                throw new Exception("Data Updation Failed", exception);
            }
            return resultObject;
        }

        public DataSet UpdateObject(object mo_Object, string updateStoredProcedureName, string getStoredProcedureName)
        {
            DataSet resultDataSet = null;
            try
            {
                dataReader = null;

                resultDataSet = WriteObject(mo_Object, updateStoredProcedureName, getStoredProcedureName);

            }
            catch (Exception exception)
            {
                throw new Exception("Data Updation Failed", exception);
            }
            return resultDataSet;
        }

        private object WriteObject(object targetObject, string storedProcedureName, bool useTransaction)
        {
            object returnObject = null;
            DbTransaction currentTrasection = null;
            try
            {
                Type targetType = targetObject.GetType();
                BindingFlags bindingFlags = BindingFlags.GetProperty | BindingFlags.Public |
                    BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.IgnoreCase;

                sqlDatabase = (SqlDatabase)GetDbConnection();
                dbCommand = GetDbCommand(sqlDatabase, storedProcedureName, null);
                if (dbConnection == null || dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection = sqlDatabase.CreateConnection();
                    dbConnection.Open();

                }

                SqlParameter[] procParams = GetParameterSets();

                //AttachParameters(procParams);

                foreach (IDataParameter dataParameter in dbCommand.Parameters)
                {
                    PropertyInfo propertyInfo;
                    string propToSearch = dataParameter.ParameterName.TrimStart('@');

                    propertyInfo = targetObject.GetType().GetProperty(BuildProperty(propToSearch,dataParameter.DbType), bindingFlags);

                    if (propertyInfo != null)
                    {
                        object innerObject = propertyInfo.GetValue(targetObject, null);

                        if (propertyInfo.PropertyType.Name == typeof(DateTime).Name)
                        {
                            DateTime sqlMinDate = DateTime.Parse("1/1/1753"),
                                sqlMaxdate = DateTime.Parse("12/31/9999");

                            if ((DateTime)innerObject < sqlMinDate)
                                innerObject = sqlMinDate;

                            if ((DateTime)innerObject > DateTime.MaxValue)
                                innerObject = sqlMaxdate;
                        }

                        dataParameter.Value = (innerObject != null) ? innerObject : System.DBNull.Value;
                        switch (dataParameter.DbType.ToString())
                        {
                            case "AnsiString":
                                dataParameter.Value = dataParameter.Value.ToString().Replace("'", "");
                                break;
                            case "AnsiStringFixedLength":
                                dataParameter.Value = dataParameter.Value.ToString().Replace("'", "");
                                break;
                        }
                    }
                }
                if (useTransaction)
                {
                    currentTrasection = dbConnection.BeginTransaction();
                    dbCommand.Transaction = currentTrasection;
                    returnObject = dbCommand.ExecuteScalar();
                    currentTrasection.Commit();
                }
                else
                {
                    returnObject = dbCommand.ExecuteScalar();
                }
            }
            catch (Exception mo_Exception)
            {
                currentTrasection.Rollback();
                throw new Exception("Data Access Layer", mo_Exception);
            }
            return returnObject;
        }

        private string BuildProperty(string propToSearch, DbType dbType)
        {
           string ms_PropertyName = string.Empty;
            switch (dbType)
           {
               case DbType.String:
                case DbType.AnsiString:
                case DbType.AnsiStringFixedLength:
                   ms_PropertyName =  "ps_" + propToSearch;     
                    break;
               case DbType.Int16:
               case DbType.Int32:
               case DbType.Int64:
                   ms_PropertyName =  "pi_" + propToSearch;
                    break;
               case DbType.Decimal:
                   ms_PropertyName =  "pf_" + propToSearch;     
                    break;
               case DbType.Boolean:
                   ms_PropertyName =  "pb_" + propToSearch;       
                    break;
               case DbType.Date:
               case DbType.DateTime:
                   ms_PropertyName=  "pd_" + propToSearch;     
                    break;
                case DbType.Binary:
                    ms_PropertyName = "px_" + propToSearch;
                    break;
           }
            return ms_PropertyName;
        }

        

        private object WriteObject(object targetObject, string storedProcedureName)
        {
            object returnObject = null;
            try
            {
                Type targetType = targetObject.GetType();
                BindingFlags bindingFlags = BindingFlags.GetProperty | BindingFlags.Public |
                    BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.IgnoreCase;

                sqlDatabase = (SqlDatabase)GetDbConnection();
                dbCommand = GetDbCommand(sqlDatabase, storedProcedureName, null);
                if (dbConnection == null || dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection = sqlDatabase.CreateConnection();
                    dbConnection.Open();

                }

                SqlParameter[] procParams = GetParameterSets();

                //AttachParameters(procParams);

                foreach (IDataParameter dataParameter in dbCommand.Parameters)
                {
                    PropertyInfo propertyInfo;
                    string propToSearch = dataParameter.ParameterName.TrimStart('@');
                    propertyInfo = targetObject.GetType().GetProperty(BuildProperty(propToSearch, dataParameter.DbType), bindingFlags);

                    if (propertyInfo != null)
                    {
                        object innerObject = propertyInfo.GetValue(targetObject, null);

                        if (propertyInfo.PropertyType.Name == typeof(DateTime).Name)
                        {
                           //provide the min and max date format as dd/mm/yyyy
                            DateTime sqlMinDate = DateTime.Parse("1/1/1753"),
                                sqlMaxdate = DateTime.Parse("12/31/9999");//modified

                            if ((DateTime)innerObject < sqlMinDate)
                                innerObject = sqlMinDate;

                            if ((DateTime)innerObject > DateTime.MaxValue)
                                innerObject = sqlMaxdate;
                        }

                        dataParameter.Value = (innerObject != null) ? innerObject : System.DBNull.Value;
                        switch (dataParameter.DbType.ToString())
                        {
                            case "AnsiString":
                                dataParameter.Value = dataParameter.Value.ToString().Replace("'", "");
                                break;
                            case "AnsiStringFixedLength":
                                dataParameter.Value = dataParameter.Value.ToString().Replace("'", "");
                                break;
                        }
                    }
                }

                returnObject = dbCommand.ExecuteScalar();
            }
            catch (Exception mo_Exception)
            {
                throw new Exception("Data Access Layer", mo_Exception);
            }
            return returnObject;
        }

        private DataSet WriteObject(object targetObject, string updateStoredProcedureName, string getStoredProcedureName)
        {
            DataSet returnDataSet = null;
            try
            {
                Type targetType = targetObject.GetType();
                BindingFlags bindingFlags = BindingFlags.GetProperty | BindingFlags.Public |
                    BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.IgnoreCase;

                sqlDatabase = (SqlDatabase)GetDbConnection();
                dbCommand = GetDbCommand(sqlDatabase, updateStoredProcedureName, null);
                if (dbConnection == null || dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection = sqlDatabase.CreateConnection();
                    dbConnection.Open();

                }

                SqlParameter[] procParams = GetParameterSets();

                //AttachParameters(procParams);

                foreach (IDataParameter dataParameter in dbCommand.Parameters)
                {
                    PropertyInfo propertyInfo;
                    string propToSearch = dataParameter.ParameterName.TrimStart('@');
                    propertyInfo = targetObject.GetType().GetProperty(BuildProperty(propToSearch, dataParameter.DbType), bindingFlags);

                    if (propertyInfo != null)
                    {
                        object innerObject = propertyInfo.GetValue(targetObject, null);

                        if (propertyInfo.PropertyType.Name == typeof(DateTime).Name)
                        {
                            DateTime sqlMinDate = DateTime.Parse("1/1/1753"),
                                sqlMaxdate = DateTime.Parse("12/31/9999");

                            if ((DateTime)innerObject < sqlMinDate)
                                innerObject = sqlMinDate;

                            if ((DateTime)innerObject > DateTime.MaxValue)
                                innerObject = sqlMaxdate;
                        }

                        dataParameter.Value = (innerObject != null) ? innerObject : System.DBNull.Value;
                        switch (dataParameter.DbType.ToString())
                        {
                            case "AnsiString":
                                dataParameter.Value = dataParameter.Value.ToString().Replace("'", "");
                                break;
                            case "AnsiStringFixedLength":
                                dataParameter.Value = dataParameter.Value.ToString().Replace("'", "");
                                break;
                        }
                    }
                }
                //update the database
                dbCommand.ExecuteScalar();
                //get the Updated Dataset
                returnDataSet = GetDataSet(getStoredProcedureName);
            }
            catch (Exception mo_Exception)
            {
                throw new Exception("Data Access Layer", mo_Exception);
            }
            return returnDataSet;
        }
        private SqlParameter[] GetParameterSets(ref DbTransaction currentTrasaction)
        {
            SqlParameter[] discoveredParameters;
            dbCommand.Connection = dbConnection;
            dbCommand.Transaction = dbConnection.BeginTransaction();
            currentTrasaction = dbCommand.Transaction;
            SqlCommandBuilder.DeriveParameters(dbCommand as SqlCommand);
            discoveredParameters = new SqlParameter[dbCommand.Parameters.Count];
            dbCommand.Parameters.CopyTo(discoveredParameters, 0);
            return discoveredParameters;

        }
        private SqlParameter[] GetParameterSets()
        {
            SqlParameter[] discoveredParameters;
            dbCommand.Connection = dbConnection;
            SqlCommandBuilder.DeriveParameters(dbCommand as SqlCommand);
            discoveredParameters = new SqlParameter[dbCommand.Parameters.Count];
            dbCommand.Parameters.CopyTo(discoveredParameters, 0);
            return discoveredParameters;

        }
        private void AttachParameters(SqlParameter[] sqlParameters)
        {
            try
            {
                foreach (SqlParameter sqlParameter in sqlParameters)
                {
                    //check for derived output value with no value assigned
                    if ((sqlParameter.Direction == ParameterDirection.InputOutput) && (sqlParameter.Value == null))
                    {
                        sqlParameter.Value = DBNull.Value;
                    }

                    dbCommand.Parameters.Add(sqlParameter);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Attach Parameter Failed", ex);
            }
        }
        #endregion

        #region Get Object and Object Array
        public object GetObject(string storedProcedureName, object targetObject)
        {
            return GetObjectArray(storedProcedureName, targetObject);
        }
        public object GetObjectArray(string storedProcedureName, object targetObject)
        {
            ArrayList objectArrayList = new ArrayList();
            Type targetType = targetObject.GetType();

            try
            {
                sqlDatabase = (SqlDatabase)GetDbConnection();
                dbCommand = GetDbCommand(sqlDatabase, storedProcedureName,null);
                sqlDatabase.DiscoverParameters(dbCommand);
                BindingFlags bindingFlags = BindingFlags.GetProperty | BindingFlags.Public |
                   BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.IgnoreCase;
                
                foreach (SqlParameter dataParameter in dbCommand.Parameters)
                {
                    PropertyInfo propertyInfo;
                    string propToSearch = dataParameter.ParameterName.TrimStart('@');
                    if (propToSearch != "RETURN_VALUE")
                    {
                        propertyInfo = targetObject.GetType().GetProperty(BuildProperty(propToSearch, dataParameter.DbType), bindingFlags);

                        if (propertyInfo != null)
                        {
                            object innerObject = propertyInfo.GetValue(targetObject, null);

                            if (propertyInfo.PropertyType.Name == typeof(DateTime).Name)
                            {
                                DateTime sqlMinDate = DateTime.Parse("1/1/1753"),
                                    sqlMaxdate = DateTime.Parse("12/31/9999");

                                if ((DateTime)innerObject < sqlMinDate)
                                    innerObject = sqlMinDate;

                                if ((DateTime)innerObject > DateTime.MaxValue)
                                    innerObject = sqlMaxdate;
                            }

                            dataParameter.Value = (innerObject != null) ? innerObject : System.DBNull.Value;
                            switch (dataParameter.DbType.ToString())
                            {
                                case "AnsiString":
                                    dataParameter.Value = dataParameter.Value.ToString().Replace("'", "");
                                    break;
                                case "AnsiStringFixedLength":
                                    dataParameter.Value = dataParameter.Value.ToString().Replace("'", "");
                                    break;
                            }
                        }
                    }
                }
                dataReader = sqlDatabase.ExecuteReader(dbCommand);

                while (dataReader.Read())
                {                    
                    ReadObject(targetObject, dataReader);
                    objectArrayList.Add(targetObject);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Getting Object Array Failed", ex);
            }
            finally
            {

                if (dataReader != null)
                {
                    dataReader.Close();
                }
            }


            if (objectArrayList.Count > 1)
            {
                return (object[])objectArrayList.ToArray(targetType);
            }
            else
            {
                return targetObject;
            }
        }

        protected void ReadObject(object mo_Object, IDataReader ro_IDataReader)
        {
            try
            {
                Type mo_Type = mo_Object.GetType();

                BindingFlags mo_BindingFlags = BindingFlags.SetProperty | BindingFlags.Public |
                                        BindingFlags.Instance | BindingFlags.IgnoreCase;

                int mi_FieldCount = ro_IDataReader.FieldCount;

                for (int mi_LoopIndex = 0; mi_LoopIndex < mi_FieldCount; mi_LoopIndex++)
                {
                    string ms_ColumnName = ro_IDataReader.GetName(mi_LoopIndex);
                    string ms_PropertyName = ms_ColumnName;
                    object mo_object = ro_IDataReader.GetValue(mi_LoopIndex);

                    //added code for getting property names
                    PropertyInfo[] mo_PropertyInfo = mo_Object.GetType().GetProperties();

                    foreach (PropertyInfo mo_PropertyElement in mo_PropertyInfo)
                    {
                        if (ms_PropertyName == mo_PropertyElement.Name.Split('_').GetValue(1).ToString())
                        {
                            ms_PropertyName = mo_PropertyElement.Name.ToString();
                            break;
                        }
                    }
                    PropertyInfo mo_PropertyInfoType = mo_Type.GetProperty(ms_PropertyName, mo_BindingFlags);

                    if (mo_object != DBNull.Value && mo_PropertyInfoType != null)
                    {
                        mo_Type.InvokeMember(ms_PropertyName, mo_BindingFlags, null, mo_Object,
                            new object[] { mo_object });
                    }
                }
            }
            catch (Exception mo_Exception)
            {
                throw new Exception("Data Access Layer", mo_Exception);
            }
        }

        #endregion

        public object UpdateColumnDatas(object updatedFieldWithObject)
        {
            object resultObject = null;
            try
            {
                dataReader = null;
                resultObject = WriteObject(updatedFieldWithObject, "s_UpdateColumnData");

            }
            catch (Exception exception)
            {
                throw new Exception("Data Updation Failed", exception);
            }
            return resultObject;
        }
        public object UpdateTableField(object updatedFieldWithObject)
        {
            object resultObject = null;
            try
            {
                dataReader = null;
                resultObject = WriteObject(updatedFieldWithObject, "s_UpdateTableField");

            }
            catch (Exception exception)
            {
                throw new Exception("Data Updation Failed", exception);
            }
            return resultObject;
        }
        public DataSet UpdateTableField(object updatedFieldWithObject,string getStoredProcedure)
        {
            DataSet resultObject = null;
            try
            {
                dataReader = null;
                resultObject = WriteObject(updatedFieldWithObject, "s_UpdateTableField", getStoredProcedure);

            }
            catch (Exception exception)
            {
                throw new Exception("Data Updation Failed", exception);
            }
            return resultObject;
        }
    }

    #endregion

    #region Singleton for retrieving Database Type and populate Connection Strings in Hash table
    /// <summary>
    /// Singleton implementation, maintains multiple database connection string (key/value pair)
    /// </summary>
    public sealed class Singleton
    {
        private static volatile Singleton instance = null;
        /// <summary>
        /// 
        /// </summary>

        public Hashtable ht = new Hashtable();

        private ConnectionStringsSection conStr;

        /// <summary>
        /// Gets the connection string
        /// </summary>
        /// <returns></returns>
        public static Singleton GetConnectionString()
        {
            if (instance == null)
            {
                instance = new Singleton();
                instance.conStr = (ConnectionStringsSection)ConfigurationManager.GetSection("connectionStrings");
                for (int i = 0; i < instance.conStr.ConnectionStrings.Count; i++)
                {
                    ConnectionStringSettings cs = instance.conStr.ConnectionStrings[i];

                    instance.ht.Add(cs.Name.ToString(), cs.ConnectionString.ToString());
                }
            }
            //if (new Microsoft.Singleton.DBAccess.SingletonAccess().IsValidAccess)
            //{
                //instance.ht.Clear();
            //}
            return instance;
        }
    }
    #endregion
}
