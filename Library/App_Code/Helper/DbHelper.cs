using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace EduERPAPI.DB
{
    public class DbHelper
    {

    }   
    public interface IDataBase
    {      
         IDbConnection CreateConnection(string dbConnectionString);
         IDbCommand CreateCommand();
         IDbConnection CreateOpenConnection(string dbConnectionString);
         IDbCommand CreateCommand(string commandText, IDbConnection connection);
         IDbCommand CreateStoredProcCommand(string procName, IDbConnection connection);
         IDataParameter CreateParameter(string parameterName, object parameterValue);
         IDataAdapter CreateAdatapter(SqlCommand cmd);

    }
    public class SqlDataBase : IDataBase
    {
        public static string UserdbConnectionString { get; private set; }
        public static string ERPdbConnectionString { get; private set; }

        public IDbCommand CreateCommand()
        {
            return new SqlCommand();
        }
        public IDataAdapter CreateAdatapter(SqlCommand cmd)
        {
            return new SqlDataAdapter(cmd);
        }
        public IDbCommand CreateCommand(string commandText, IDbConnection connection)
        {
            SqlCommand command = (SqlCommand)CreateCommand();
            command.CommandText = commandText;
            command.Connection = (SqlConnection)connection;
            command.CommandType = CommandType.Text;
            return command;
        }
        public IDbConnection CreateConnection(string dbConnectionString)
        {
            return new SqlConnection(dbConnectionString);
        }
        public IDbConnection CreateOpenConnection(string dbConnectionString)
        {
            SqlConnection connection = (SqlConnection)CreateConnection(dbConnectionString);
            connection.Open();
            return connection;
        }
        public IDataParameter CreateParameter(string parameterName, object parameterValue)
        {
            return new SqlParameter(parameterName, parameterValue);
        }
        public IDbCommand CreateStoredProcCommand(string procName, IDbConnection connection)
        {
            SqlCommand command = (SqlCommand)CreateCommand();
            command.CommandText = procName;
            command.Connection = (SqlConnection)connection;
            command.CommandType = CommandType.StoredProcedure;
            return command;
        }
        public IDbCommand CreateStoredProcCommand(string procName, IDbConnection connection, SqlParameter[] commandParameters)
        {
            SqlCommand command = (SqlCommand)CreateCommand();
            command.CommandText = procName;
            command.Connection = (SqlConnection)connection;
            command.CommandType = CommandType.StoredProcedure;
            if (commandParameters != null)
            {
                foreach (SqlParameter p in commandParameters)
                {
                    //check for derived output value with no value assigned
                    if ((p.Direction == ParameterDirection.InputOutput) && (p.Value == null))
                    {
                        p.Value = DBNull.Value;
                    }
                    command.Parameters.Add(p);
                }
            }

            return command;
        }
        public IDbCommand CreateStoredFuncCommand(string procName, IDbConnection connection, SqlParameter[] commandParameters)
        {
            SqlCommand command = (SqlCommand)CreateCommand();
            command.CommandText = procName;
            command.Connection = (SqlConnection)connection;
            command.CommandType = CommandType.Text;
            if (commandParameters != null)
            {
                foreach (SqlParameter p in commandParameters)
                {
                    //check for derived output value with no value assigned
                    if ((p.Direction == ParameterDirection.InputOutput) && (p.Value == null))
                    {
                        p.Value = DBNull.Value;
                    }
                    command.Parameters.Add(p);
                }
            }

            return command;
        }
        public void ExceProc(string ProcName, ref DataSet objDataset, SqlParameter[] CommandParameters, string dbConnectionString)
        {
            /*
            using SqlCommand cmd = (SqlCommand)CreateStoredProcCommand(ProcName, (SqlConnection)CreateOpenConnection(dbConnectionString), CommandParameters);
            {
                using var adp = (SqlDataAdapter)CreateAdatapter(cmd);
                {
                    DataSet ds = new();
                    adp.Fill(ds);
                    objDataset = ds;
                }
            }
            */
            SqlCommand cmd = (SqlCommand)CreateStoredProcCommand(ProcName, (SqlConnection)CreateOpenConnection(dbConnectionString), CommandParameters);

            var adp = (SqlDataAdapter)CreateAdatapter(cmd);

            DataSet ds = new DataSet();
            adp.Fill(ds);
            objDataset = ds;


        }
          public void ExceProc(string ProcName, out DataTable objDataset, SqlParameter[] commandParameters, string dbConnectionString)
          {
               SqlCommand cmd = (SqlCommand)CreateStoredProcCommand(ProcName, (SqlConnection)CreateOpenConnection(dbConnectionString), commandParameters);
              
                   var adp = (SqlDataAdapter)CreateAdatapter(cmd);
                  
                      DataSet ds = new DataSet();
                      adp.Fill(ds);
                      objDataset = ds.Tables[0];
                  
              
      }
      
        public void ExceFunc(string FuncString, ref DataTable objDataset, SqlParameter[] CommandParameters, string dbConnectionString)
        {
            SqlCommand cmd = (SqlCommand)CreateStoredFuncCommand(FuncString, (SqlConnection)CreateOpenConnection(dbConnectionString), CommandParameters);

            var adp = (SqlDataAdapter)CreateAdatapter(cmd);

            adp.Fill(objDataset);



        }
        public static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            foreach (SqlParameter p in commandParameters)
            {
                if ((p.Direction == ParameterDirection.InputOutput) && (p.Value == null))
                {
                    p.Value = DBNull.Value;
                }
                command.Parameters.Add(p);
            }

        }
        public static string GetConnectionString()// (char dbCon)
        {
            var r = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;
            return r;
            /*
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).
                      AddJsonFile("appsettings.json", optional: true,
                      reloadOnChange: true);

                //  string UserdbConnection = "";
                string UserdbPassword = builder.Build().GetSection("ConnectionStrings").GetSection("Password").Value;


                //  UserdbConnection = builder.Build().GetSection("ConnectionStrings").GetSection("UserdbConnectionString").Value;
                UserdbConnectionString = builder.Build().GetSection("ConnectionStrings").GetSection("UserdbConnectionString").Value + "";//;Password=" + UserdbPassword + ";"; ;

                // UserdbConnection = builder.Build().GetSection("ConnectionStrings").GetSection("ERPdbConnectionString").Value;
                ERPdbConnectionString = builder.Build().GetSection("ConnectionStrings").GetSection("ERPdbConnectionString").Value + "";//;Password=" + UserdbPassword + ";"; ;

                return "";
            */
        }
    }
}
