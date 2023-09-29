using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Data.SqlClient;
using System.Net.Http;

namespace Library.App_Code.CSharp
{
    public class BaseClass : Page
    {
        public static string LibApiUrl = "";
        public static HttpClient apiCall = new HttpClient();
        private static string constr;
        public void ApiUrl(string url)
        {
            if (apiCall.BaseAddress!=null)
              apiCall.BaseAddress = new Uri(url);
        }
        public BaseClass()
        {
            var logged = LoggedUser.Logged();
            if (logged==null)
            {
                HttpContext.Current. Response.Redirect("~/default.aspx");
                return;
            }
            constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            LibApiUrl= ConfigurationManager.AppSettings["libapilocal"];
        }

        public static string retConstr(string SqlConName="")
        {
            if (string.IsNullOrEmpty(SqlConName))
            {
                return constr;
            }
            else
            {
                return ConfigurationManager.ConnectionStrings[SqlConName].ConnectionString;
            }
        }

        public class FillDsTables
        {
            private string StandConnStr;//= retConstr(System.Web.HttpContext.Current.Session["LibWiseDBConn"].ToString());
            private SqlConnection StandConn;//= new OleDbConnection(StandConnStr);
            private SqlCommand StandComm; // = New OleDbCommand
            private string Err;

            public FillDsTables()
            {
                StandConnStr = retConstr("SqlConn");
                StandConn = new SqlConnection(StandConnStr);
            }

            public string FillDs(string Qry, ref DataSet dsLoc, string dtTab)
            {
                try
                {
                    StandConn.Open();
                    StandComm = new SqlCommand(Qry);
                    StandComm.Connection = StandConn;
                    StandComm.CommandTimeout = 600;
                    SqlDataAdapter StandDa = new SqlDataAdapter(Qry, StandConn);
                    StandDa.Fill(dsLoc, dtTab);
                    StandConn.Close();
                    return "";
                }
                catch (Exception ex)
                {
                    // message.PageMesg(ex.Message, Me, DBUTIL.dbUtilities.MsgLevel.Failure)
                    StandConn.Close();
                    return ex.Message;
                }
            }
            public string FillDs(string Qry, ref DataTable dtTab)
            {
                try
                {
                    StandConn.Open();
                    StandComm = new SqlCommand(Qry);
                    StandComm.Connection = StandConn;
                    StandComm.CommandTimeout = 600;
                    SqlDataAdapter StandDa = new SqlDataAdapter(Qry, StandConn);
                    StandDa.Fill(dtTab);
                    StandConn.Close();
                    return "";
                }
                catch (Exception ex)
                {
                    // message.PageMesg(ex.Message, Me, DBUTIL.dbUtilities.MsgLevel.Failure)
                    StandConn.Close();
                    return ex.Message;
                }
            }
            // UNUSED BELOW!!!!! NOT OK
            public string FillDs(SqlConnection Conn, SqlCommand Comm, string Qry, ref DataSet dsLoc, string dtTab)
            {
                try
                {
                    // Comm.CommandType = CommandType.Text
                    // Comm.CommandText = Qry
                    // Comm.Connection = Conn
                    // Comm = New OleDbCommand(Qry)
                    SqlDataAdapter StandDa = new SqlDataAdapter(Qry, Conn);
                    StandDa.Fill(dsLoc, dtTab);
                    return "";
                }
                catch (Exception ex)
                {
                    // message.PageMesg(ex.Message, Me, DBUTIL.dbUtilities.MsgLevel.Failure)

                    return ex.Message;
                }
            }
                public string FillDs(string Qry, DataTable dtTab, SqlConnection PCon)
                {
                    try
                    {
                        // StandConn.Open()
                        StandComm = new SqlCommand(Qry);
                        StandComm.Connection = PCon;
                        SqlDataAdapter StandDa = new SqlDataAdapter(StandComm);
                        // StandDa.SelectCommand = Qry
                        StandDa.Fill(dtTab);
                        return "";
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                }
                public string FillDs(string Qry, ref DataTable dtTab, SqlCommand PCom)
                {
                    try
                    {
                        // StandConn.Open()
                        // StandComm = New OleDbCommand(Qry)
                        // StandComm.Connection = PCon
                        PCom.CommandText = Qry;
                        SqlDataAdapter StandDa = new SqlDataAdapter(PCom);
                        // StandDa.SelectCommand = Qry
                        StandDa.Fill(dtTab);
                        return "";
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                }
            ///
                public  string InsUpd(string Qry)
                {
                    try
                    {
                        StandConn.Open();
                        StandComm = new SqlCommand(Qry);
                        StandComm.Connection = StandConn;
                        // Dim StandDa As New OleDbDataAdapter(Qry, StandConn)
                        StandComm.ExecuteNonQuery();
                        StandConn.Close();
                        return "";
                    }
                    catch (Exception ex)
                    {
                        StandConn.Close();
                        return ex.Message + " " + Err;
                    }
                }

                public  string InsUpd(string Qry, SqlCommand Comm)
                {
                    try
                    {
                        Comm.CommandType = CommandType.Text;
                        Comm.CommandText = Qry;
                        Comm.ExecuteNonQuery();
                        return "";
                    }
                    catch (Exception ex)
                    {
                        return ex.Message + " " + Err;
                    }
                }

                public string InsUpd(string Qry, SqlConnection Conn)
                {
                    try
                    {
                        if (Conn.State == ConnectionState.Closed)
                            Conn.Open();
                        StandComm = new SqlCommand(Qry);
                        StandComm.Connection = Conn;
                        // Dim StandDa As New OleDbDataAdapter(Qry, StandConn)
                        StandComm.ExecuteNonQuery();
                        return "";
                    }
                    catch (Exception ex)
                    {
                        return ex.Message + " " + Err;
                    }
                }
            

            ///
        }

    }

}
