using System;
using System.Data;
using System.Data.OleDb;

namespace CommanUtilities
{
    public class MSAccessDataUtility
    {
        private OleDbCommand command;
        public MSAccessDataUtility()
        {
           
            command = new OleDbCommand();
            command.Connection = GetConnection();

        }
        private static OleDbConnection accessConnection = null;
        public OleDbConnection GetConnection()
        {

            //string conString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].‌​ToString();
            

            if (accessConnection == null)
            {

                string strAccessConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ToString();

                // string strAccessConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source = E:\\Web-Apps\\Hospital_Web_App\\Data\\HospitalDB.mdb;Persist Security Info=False;";

                accessConnection = new OleDbConnection(strAccessConnectionString);
                accessConnection.Open();
            }
            return accessConnection;


        }

        public bool AddRow(String InsertQuery)
        {
            ExecuteNonQuery(InsertQuery);
            return true;
        }
        public bool ExecuteNonQuery(String Query)
        {
            command.CommandText = Query;
            command.ExecuteNonQuery();
             return true;
        }
        public bool DeleteRow(String DeleteQuery)
        {
            ExecuteNonQuery(DeleteQuery);
            return true;
        }
        public bool UpdateRow(String UpdateQuery)
        {
            ExecuteNonQuery(UpdateQuery);
            return true;
        }
        public Int32 GetScalarValue(String Query)
        {
           // int _MaxVal = 0;
            command.CommandText = Query;
            var temp=  command.ExecuteScalar();
           return Convert.IsDBNull(temp)? 0:Convert.ToInt32(temp);
        }

        public decimal GetScalarValueDecimal(String Query)
        {
            // int _MaxVal = 0;
            command.CommandText = Query;
            var temp = command.ExecuteScalar();
            return Convert.IsDBNull(temp) ? 0 : Convert.ToDecimal(temp);
        }

        public string GetScalarValueString(String Query)
        {
            // int _MaxVal = 0;
            command.CommandText = Query;
            var temp = command.ExecuteScalar();
            return Convert.IsDBNull(temp) ? "" : temp.ToString();
        }

        public DataSet GetTable(string TableName)
        {
            return GetTable(TableName,"");
        }

        public DataTable GetTableValue(string TableName, string Condition)
        {
            String condition = Condition == "" ? "" : "  where " + Condition;
            command.CommandText = @"select * from " + TableName + condition;
            DataTable dt = new DataTable();
            OleDbDataAdapter adapter = new OleDbDataAdapter(command);
            adapter.Fill(dt);
             return dt;
        }

        public DataSet GetTable(string TableName, string Condition)
        {
            String condition = Condition == "" ? "" : "  where " + Condition;
            String Query = @"select * from " + TableName + condition;
            return GetQueryExecute(Query);
        }

        public DataSet GetTableByQuery(string Query)
        {
            return GetQueryExecute(Query);
        }

        private DataSet GetQueryExecute(String Query)
        {
            command.CommandText = Query;
            DataTable dt = new DataTable();
            OleDbDataAdapter adapter = new OleDbDataAdapter(command);
            adapter.Fill(dt);

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);

            dt.Dispose();
            return ds;
        }

    }

  
}


