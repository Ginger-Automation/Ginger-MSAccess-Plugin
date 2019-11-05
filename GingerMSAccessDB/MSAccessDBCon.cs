#region License
/*
Copyright © 2014-2019 European Support Limited

Licensed under the Apache License, Version 2.0 (the "License")
you may not use this file except in compliance with the License.
You may obtain a copy of the License at 

http://www.apache.org/licenses/LICENSE-2.0 

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS, 
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
See the License for the specific language governing permissions and 
limitations under the License. 
*/
#endregion


using Amdocs.Ginger.Plugin.Core;
using Amdocs.Ginger.Plugin.Core.DatabaseLib;
using Amdocs.Ginger.Plugin.Core.Reporter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;



namespace MSAccessDB
{
    [GingerService("MSAccessService", "MSAccess Database service")]
    public class MSAccessDBCon : IDatabase, ISQLDatabase
    {        
        private IReporter mReporter;
        // private DbTransaction tran = null;        
        // OleDbConnection mOleDbConnection;

        [Default("Microsoft.ACE.OLEDB.12.0")]  // For accdb
        [DatabaseParam("Provider")]
        public string Provider { get; set; }

        [DatabaseParam("DataSource")]
        public string DataSource { get; set; }  // mdb file location

        private string mCnnectionString;
        public string ConnectionString
        { 
            get                  
            {
                if (!string.IsNullOrEmpty(mCnnectionString))
                {
                    return mCnnectionString;
                }

                string conn = $"Provider={Provider};Data Source={DataSource};";
                return conn;
            }
            set
            {
                mCnnectionString = value;
            }
        }

        //public bool OpenConnection()
        //{
        //    using (OleDbConnection conn = new OleDbConnection(ConnectionString))
        //    {
        //        conn.Open();
        //        if (conn.State == ConnectionState.Open)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}

        //public bool TestConnection()
        //{
        //    try
        //    {                
        //        using (OleDbConnection conn = new OleDbConnection(ConnectionString))
        //        {                    
        //            conn.Open();
        //            if (conn.State == ConnectionState.Open)
        //            {
        //                return true;
        //            }
        //            // conn.Close();  // is needed ??
        //            return false;                    
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        //public void CloseConnection()
        //{
        //    //try
        //    //{
        //    //    if (conn != null)
        //    //    {
        //    //        conn.Close();
        //    //    }
        //    //}
        //    //catch (Exception e)
        //    //{
        //    //    Reporter.ToLog(eLogLevel.ERROR, "Failed to close DB Connection", e);
        //    //    throw (e);
        //    //}
        //    //finally
        //    //{
        //    //    conn?.Dispose();
        //    //}
        //}
        
        //public object ExecuteQuery(string Query)
        //{                        
        //    using (OleDbConnection conn = new OleDbConnection(ConnectionString))
        //    {                
        //        OleDbCommand cmd = new OleDbCommand(Query, conn);   
                
        //        // int rows = cmd.ExecuteNonQuery()

        //        // conn.Open();
        //        OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
        //        DataTable results = new DataTable();
        //        adapter.Fill(results);
        //        // conn.Close();
        //        return results;
        //    }

            

        //    //using (SqlDataAdapter dataAdapter= new SqlDataAdapter(Query, sqlConnection))
        //    //{
        //    //    // create the DataSet 
        //    //    DataSet dataSet = new DataSet();
        //    //    // fill the DataSet using our DataAdapter 
        //    //    dataAdapter.Fill(dataSet);
        //    //}

        //    //// MakeSureConnectionIsOpen();
        //    //List<string> Headers = new List<string>();
        //    //List<List<string>> Records = new List<List<string>>();
        //    //bool IsConnected = false;
        //    //List<object> ReturnList = new List<object>();
        //    //DataTable dataTable = new DataTable();
        //    //DbDataReader reader = null;
        //    //try
        //    //{
        //    //    if (conn == null)
        //    //    {
        //    //        IsConnected = OpenConnection(KeyvalParamatersList);
        //    //    }
        //    //    if (IsConnected || conn != null)
        //    //    {
        //    //        DbCommand command = conn.CreateCommand();
        //    //        command.CommandText = Query;
        //    //        command.CommandType = CommandType.Text;

        //    //        // Retrieve the data.
        //    //        reader = command.ExecuteReader();

        //    //        // Create columns headers
        //    //        for (int i = 0; i < reader.FieldCount; i++)
        //    //        {
        //    //            Headers.Add(reader.GetName(i));
        //    //            dataTable.Columns.Add(reader.GetName(i));
        //    //        }

        //    //        while (reader.Read())
        //    //        {
        //    //            List<string> record = new List<string>();
        //    //            for (int i = 0; i < reader.FieldCount; i++)
        //    //            {
        //    //                record.Add(reader[i].ToString());
        //    //            }
        //    //            Records.Add(record);
        //    //            dataTable.Rows.Add(record);
        //    //        }
        //    //        ReturnList.Add(Headers);
        //    //        ReturnList.Add(Records);
        //    //    }
        //    //}
        //    //catch (Exception e)
        //    //{
        //    //    Reporter.ToLog(eLogLevel.ERROR, "Failed to execute query:" + Query, e);
        //    //    throw e;
        //    //}
        //    //finally
        //    //{
        //    //    if (reader != null)
        //    //        reader.Close();
        //    //}
            
        //    //return dataTable;
        //}

        //public string GetConnectionString(Dictionary<string,string> parameters)
        //{
        //    string connStr = null;
        //    bool res;
        //    res = false;

        //    string ConnectionString = parameters.FirstOrDefault(pair => pair.Key == "ConnectionString").Value;
        //    string User = parameters.FirstOrDefault(pair => pair.Key == "UserName").Value;
        //    string Password = parameters.FirstOrDefault(pair => pair.Key == "Password").Value;
        //    string TNS = parameters.FirstOrDefault(pair => pair.Key == "TNS").Value;
        //    if (String.IsNullOrEmpty(ConnectionString) == false)
        //    {

        //        connStr = ConnectionString.Replace("{USER}", User);

        //        String deCryptValue = EncryptionHandler.DecryptString(Password, ref res, false);
        //        if (res == true)
        //        { connStr = connStr.Replace("{PASS}", deCryptValue); }
        //        else
        //        { connStr = connStr.Replace("{PASS}", Password); }
        //    }
        //    else
        //    {
        //        String strConnString = TNS;
        //        String strProvider;
        //        connStr = "Data Source=" + TNS + ";User Id=" + User + ";";

        //        String deCryptValue = EncryptionHandler.DecryptString(Password, ref res, false);

        //        if (res == true) { connStr = connStr + "Password=" + deCryptValue + ";"; }
        //        else { connStr = connStr + "Password=" + Password + ";"; }


        //        if (strConnString.Contains(".accdb"))
        //        {
        //            strProvider = "Provider=Microsoft.ACE.OLEDB.12.0;";
        //        }
        //        else { strProvider = "Provider=Microsoft.ACE.OLEDB.12.0;"; }

        //        connStr = strProvider + connStr;
        //    }
        //    return connStr;
        //}

        //public string GetSingleValue(string Table, string Column, string Where)
        //{
        //    string sql = "SELECT {0} FROM {1} WHERE {2}";
        //    sql = String.Format(sql, Column, Table, Where);
        //    CheckConnectionString(ConnectionString);
        //    using (OleDbConnection conn = new OleDbConnection(ConnectionString))
        //    {
        //        conn.Open();
        //        String rc = null;
        //        DbDataReader reader = null;

        //        try
        //        {
        //            DbCommand command = conn.CreateCommand();
        //            command.CommandText = sql;
        //            command.CommandType = CommandType.Text;

        //            // Retrieve the data.
        //            reader = command.ExecuteReader();
        //            while (reader.Read())
        //            {
        //                rc = reader[0].ToString();
        //                break; // We read only first row
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            throw e;
        //        }
        //        finally
        //        {
        //            reader.Close();
        //        }

        //        return rc;
        //    }


        //}

        //public List<string> GetTablesList(string Name = null)
        //{            
        //    using (OleDbConnection conn = new OleDbConnection(ConnectionString))
        //    {
        //        List<string> tables = new List<string>();
        //        try
        //        {
        //            // conn.Open();
        //            DataTable table = conn.GetSchema("Tables");
        //            string tableName = "";
        //            foreach (DataRow row in table.Rows)
        //            {
        //                tableName = (string)row[2];
        //                if (!tableName.StartsWith("MSys"))  // ignore access system table
        //                {
        //                    tables.Add(tableName);
        //                }
        //            }

        //        }
        //        catch (Exception e)
        //        {
        //            mReporter.ToLog(eLogLevel.ERROR, "Failed to get table list " + e);
        //            throw (e);
        //        }
        //        return tables;
        //    }
            
        //}



        //public List<string> GetTablesColumns(string table)
        //{            
        //    using (OleDbConnection conn = new OleDbConnection(ConnectionString))
        //    {
        //        conn.Open();
        //        DbDataReader reader = null;
        //        List<string> rc = new List<string>() { "" };
        //        try
        //        {
        //            DbCommand command = conn.CreateCommand();
        //            // Do select with zero records
        //            command.CommandText = "select * from " + table + " where 1 = 0";
        //            command.CommandType = CommandType.Text;

        //            reader = command.ExecuteReader();
        //            // Get the schema and read the cols
        //            DataTable schemaTable = reader.GetSchemaTable();
        //            foreach (DataRow row in schemaTable.Rows)
        //            {
        //                string ColName = (string)row[0];
        //                rc.Add(ColName);
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            mReporter.ToLog(eLogLevel.ERROR, "", e);
        //            throw (e);
        //        }
        //        finally
        //        {
        //            reader.Close();
        //        }
        //        return rc;
        //    }
        //}

        

        //public string RunUpdateCommand(string updateCmd, bool commit = true)
        //{
        //    string result = "";
        //    CheckConnectionString(ConnectionString);
        //    using (OleDbConnection conn = new OleDbConnection(ConnectionString))
        //    {
        //        using (DbCommand command = conn.CreateCommand())
        //        {
        //            conn.Open();
        //            try
        //            {
        //                if (commit)
        //                {
        //                    tran = conn.BeginTransaction();
        //                    // to Command object for a pending local transaction
        //                    command.Connection = conn;
        //                    command.Transaction = tran;
        //                }
        //                command.CommandText = updateCmd;
        //                command.CommandType = CommandType.Text;

        //                result = command.ExecuteNonQuery().ToString();
        //                if (commit)
        //                {
        //                    tran.Commit();
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                tran.Rollback();
        //                mReporter.ToLog2(eLogLevel.ERROR, "Commit failed for:" + updateCmd, e);
        //                throw e;
        //            }
        //        }
        //    }
        //    return result;
        //}

        //public int GetRecordCount(string Query)
        //{
        //    string sql = "SELECT COUNT(1) FROM " + Query;
        //    String rc = null;
        //    DbDataReader reader = null;
        //    CheckConnectionString(ConnectionString);
        //    using (OleDbConnection conn = new OleDbConnection(ConnectionString))
        //    {
        //        try
        //        {
        //            conn.Open();
        //            DbCommand command = conn.CreateCommand();
        //            command.CommandText = sql;
        //            command.CommandType = CommandType.Text;

        //            // Retrieve the data.
        //            reader = command.ExecuteReader();
        //            while (reader.Read())
        //            {
        //                rc = reader[0].ToString();
        //                break; // We read only first row = count of records
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            // mReporter.ToLog2(eLogLevel.ERROR, "Failed to execute query:" + sql, e);
        //            throw e;
        //        }
        //        finally
        //        {
        //            if (reader != null)
        //            {
        //                reader.Close();
        //            }
        //        }
        //    }

        //    return Convert.ToInt32(rc);
        //}

        
        

        //public void InitReporter(IReporter reporter)
        //{
        //    mReporter = reporter;
        //}

        public DbConnection GetDbConnection()
        {
            OleDbConnection OleDbConnection = new OleDbConnection();
            OleDbConnection.ConnectionString = ConnectionString;
            return OleDbConnection;
        }

        public List<string> GetTablesList(DataTable TablesSchema)
        {
            List<string> tables = new List<string>();
            
            foreach (DataRow row in TablesSchema.Rows)
            {
                string tableName = (string)row[2];
                if (!tableName.StartsWith("MSys"))  // ignore access system table
                {
                    tables.Add(tableName);
                }
            }
                        
            return tables;
        }
    

        public List<string> GetTableColumns(string table)
        {
            throw new NotImplementedException();
        }
    }
}
