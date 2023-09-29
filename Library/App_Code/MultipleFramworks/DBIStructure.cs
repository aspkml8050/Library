using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Script;
using System.Web.UI;

// A Framework that contains all the Functions to interact with the database!
// Powered By
// Mohammad Aamir Khan
// Date : 9-12-2011
namespace Library.App_Code.MultipleFramworks
{
    public class DBIStructure
    {

        // Global Variables
        private SqlTransaction trans;
        private SqlConnection Tcon;
        private SqlCommand Tcmd;

     //   public string getModified_HTML(string msg)
       /// {
          //  return msg.Replace("&lt;", "<").Replace("&gt;", ">").Replace("&quot;", Strings.Chr(34));
       // }

        /*public int SwitchDB(string constr)
        {
            string count = null;
            try
            {
                count = ExecuteScalar("select count(*) from sys.objects where type='U'", constr);
            }
            catch
            {
                count = "-1";
            }
            return Convert.ToInt32(count);
        }
        */
        public void createCookies(string cookiesName, string cookiesValue, int ExpiryHours = 0)
        {
            HttpCookie cookies = new HttpCookie(cookiesName);
            cookies.Value = cookiesValue;
            cookies.Expires = System.DateTime.UtcNow.AddHours(5.3).AddHours(ExpiryHours);
            System.Web.HttpContext.Current.Response.Cookies.Add(cookies);
        }

        public string getCookies(string cookiesName)
        {
            string value = "";
            try
            {
                value = System.Web.HttpContext.Current.Request.Cookies[cookiesName].Value;
            }
            catch (Exception ex)
            {
            }
            return value;
        }

        public void removeCookies(string cookiesName)
        {
            try
            {
                createCookies(cookiesName, null, -1);
            }
            catch (Exception ex)
            {
            }
        }

        public void removeAllCookies()
        {
            try
            {
                System.Web.HttpCookieCollection cCollection = System.Web.HttpContext.Current.Request.Cookies;
                for (int i = 0; i <= cCollection.Keys.Count - 1; i++)
                    createCookies(cCollection[i].Name, null, -1);
            }
            catch (Exception ex)
            {
            }
        }

        // A function that will return the ConnectionString of the specified name
        /// <summary>
        ///         ''' A function that will return the ConnectionString of the specified name
        ///         ''' </summary>
        ///         ''' <param name="Name">Type The Name of the ConnectionString and it will return the value ConnectionString from the Web.config File</param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        public virtual string GetConnectionString(string Name)
        {
            string constr = null;
                MultipleFramworks .SQLFramework obj = new MultipleFramworks.SQLFramework();
                constr = obj.GetConnectionString(Name);

              //  if (SwitchDB(constr) == -1)
                //    constr = obj.GetConnectionString("SQLServer2");
          //  else if (System.Web.HttpContext.Current.Session["DBName"] == "Oracle")
           // {
            //    MultipleFrameworks.OracleFramework obj = new MultipleFrameworks.OracleFramework();
             //   constr = obj.GetConnectionString(Name);
           // }
           // else if (System.Web.HttpContext.Current.Session["DBName"] == "MySQL")
            //{
             //   MultipleFrameworks.MySQLFramework obj = new MultipleFrameworks.MySQLFramework();
              //  constr = obj.GetConnectionString(Name);
           // }
            //else if (System.Web.HttpContext.Current.Session["DBName"] == "DB2")
           // {
             //   MultipleFrameworks.DB2Framework obj = new MultipleFrameworks.DB2Framework();
              //  constr = obj.GetConnectionString(Name);
           // }
//            else
  //          {
    //            OleDbFramework obj = new OleDbFramework();
      //          constr = obj.GetConnectionString(Name);
        //    }
            return constr;
        }

        public virtual string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings[getCookies("LibWiseDBConn")].ConnectionString.ToString();
        }

        // A function that will return the Default ConnectionString i.e. SQLConnectionString
        /// <summary>
        ///         ''' It will return the Default ConnectionString for the different Database Servers!
        ///         ''' </summary>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        public virtual string DefaultConnectionString()
        {
            string constr = null;
//            if (System.Web.HttpContext.Current.Session["DBName"] == "SQLServer")
  //          {
                SQLFramework obj = new SQLFramework();
                constr = obj.DefaultConnectionString();
    //        }
//            else if (System.Web.HttpContext.Current.Session["DBName"] == "Oracle")
  //          {
    //            MultipleFrameworks.OracleFramework obj = new MultipleFrameworks.OracleFramework();
      //          constr = obj.DefaultConnectionString();
        //    }
          //  else if (System.Web.HttpContext.Current.Session["DBName"] == "MySQL")
          //  {
            //    MultipleFrameworks.MySQLFramework obj = new MultipleFrameworks.MySQLFramework();
              //  constr = obj.DefaultConnectionString();
           // }
           // else if (System.Web.HttpContext.Current.Session["DBName"] == "DB2")
           // {
            //    MultipleFrameworks.DB2Framework obj = new MultipleFrameworks.DB2Framework();
             //   constr = obj.DefaultConnectionString();
           // }
      //      else
        //    {
          //       OleDbFramework obj = new  OleDbFramework();
            //    constr = obj.DefaultConnectionString();
            //}
            return constr;
        }

        /*
        public DataTable FillCatalogList()
        {
            AdminIndexServerClass Admin = new AdminIndexServerClass();
            // remove any existing item in the catalog list

            // finds the first catalog in the list
            bool FoundCatalog = Admin.FindFirstCatalog();

            // loop through all catalogs present
            DataTable dt = new DataTable();
            dt.Columns.Add("CatalogName");
            dt.Columns.Add("CatalogLocation");
            // dt.Columns.Add("IsUpToDate")
            // dt.Columns.Add("DocumentsToFilters")
            // dt.Columns.Add("FilteredDocumentCount")
            // dt.Columns.Add("DelayedFilterCount")
            // dt.Columns.Add("FreshTestCount")
            // dt.Columns.Add("IndexSize")
            // dt.Columns.Add("PctMergeComplete")
            // dt.Columns.Add("PendingScanCount")
            // dt.Columns.Add("PersistentIndexCount")
            // dt.Columns.Add("QueryCount")
            // dt.Columns.Add("StateInfo")
            // dt.Columns.Add("TotalDocumentCount")
            // dt.Columns.Add("UniqueKeyCount")
            // dt.Columns.Add("WordListCount")
            while ((FoundCatalog))
            {

                // gets the object representing the current catalog
                ICatAdm Catalog = (ICatAdm)Admin.GetCatalog();

                var dr = dt.NewRow();
                dr["CatalogName"] = Catalog.CatalogName;
                dr["CatalogLocation"] = Catalog.CatalogLocation;
                // dr("IsUpToDate") = Catalog.IsUpToDate.ToString()
                // dr("DocumentsToFilters") = Catalog.DocumentsToFilter.ToString()
                // dr("FilteredDocumentCount") = Catalog.FilteredDocumentCount.ToString()
                // dr("DelayedFilterCount") = Catalog.DelayedFilterCount.ToString()
                // dr("FreshTestCount") = Catalog.FreshTestCount.ToString()
                // dr("IndexSize") = Catalog.IndexSize.ToString()
                // dr("PctMergeComplete") = Catalog.PctMergeComplete.ToString()
                // dr("PendingScanCount") = Catalog.PendingScanCount.ToString()
                // dr("PersistentIndexCount") = Catalog.PersistentIndexCount.ToString()
                // dr("QueryCount") = Catalog.QueryCount.ToString()
                // dr("StateInfo") = Catalog.StateInfo.ToString()
                // dr("TotalDocumentCount") = Catalog.TotalDocumentCount.ToString()
                // dr("UniqueKeyCount") = Catalog.UniqueKeyCount.ToString()
                // dr("WordListCount") = Catalog.WordListCount.ToString()

                dt.Rows.Add(dr);

                // finds the next catalog in the list
                FoundCatalog = Admin.FindNextCatalog();
            }

            return dt;
        }


        public ICatAdm GetCatalog(string CatalogName)
        {
            AdminIndexServerClass Admin = new AdminIndexServerClass();
            // find the catalog by name
            return (ICatAdm)Admin.GetCatalogByName(CatalogName);
        }
        */
        /*
        public DataTable FillScopeList(string CatalogName)
        {

            // get a handle to the newly selected catalog
            ICatAdm Catalog = GetCatalog(CatalogName);

            // search for the first scope item
            bool FoundScope = Catalog.FindFirstScope();

            // loop through all the scope items
            DataTable dt = new DataTable();
            dt.Columns.Add("ExcludeScope");
            dt.Columns.Add("Logon");
            dt.Columns.Add("Path");
            dt.Columns.Add("VirtualScope");

            while ((FoundScope))
            {
                // get a handle to the current scope item
                IScopeAdm Scope = (IScopeAdm)Catalog.GetScope();

                DataRow dr = dt.NewRow();
                // create a new list view item and set its values
                // ListViewItem Item = ListOfScopes.Items.Add(Scope.Alias);
                dr["ExcludeScope"] = Scope.ExcludeScope.ToString();
                dr["Logon"] = Scope.Logon;
                dr["Path"] = Scope.Path;
                dr["VirtualScope"] = Scope.VirtualScope.ToString();

                dt.Rows.Add(dr);
                // search for the next scope item
                FoundScope = Catalog.FindNextScope();
            }

            return dt;
        }
        */
        /// <summary>
        ///         ''' A function executes the query and return the DataSet containg the result
        ///         ''' </summary>
        ///         ''' <param name="query">SQL Query you want to execute on database</param>
        ///         ''' <param name="ConnectionString">Connection String to connect to the database</param>
        ///         ''' <param name="Name">Name of the Table by which you will identify it, this is optional</param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        public virtual DataSet GetDataSet(string query, string ConnectionString, string Name = "Table")
        {
            DataSet ds = new DataSet();
         //   if (System.Web.HttpContext.Current.Session["DBName"] == "SQLServer")
          //  {
                SQLFramework obj = new SQLFramework();
                ds = obj.GetDataSet(query, ConnectionString, Name);
            //}
//            else if (System.Web.HttpContext.Current.Session["DBName"] == "Oracle")
 //           {
  //              MultipleFrameworks.OracleFramework obj = new MultipleFrameworks.OracleFramework();
    //            ds = obj.GetDataSet(query, ConnectionString, Name);
      //      }
        //    else if (System.Web.HttpContext.Current.Session["DBName"] == "MySQL")
          //  {
            //    MultipleFrameworks.MySQLFramework obj = new MultipleFrameworks.MySQLFramework();
              //  ds = obj.GetDataSet(query, ConnectionString, Name);
            //}
          //  else if (System.Web.HttpContext.Current.Session["DBName"] == "DB2")
           // {
            //    MultipleFrameworks.DB2Framework obj = new MultipleFrameworks.DB2Framework();
             //   ds = obj.GetDataSet(query, ConnectionString, Name);
            //}
          //  else
           // {
            //    OleDbFramework obj = new OleDbFramework();
             //   ds = obj.GetDataSet(query, ConnectionString, Name);
            //}
            return ds;
        }
        


        /// <summary>
        ///         ''' A function executes the query and return the DataTable containg the result
        ///         ''' </summary>
        ///         ''' <param name="query">Query that you want to execute on the database</param>
        ///         ''' <param name="ConnectionString">ConnectionString to connect to the database</param>
        ///         ''' <param name="Name">Name of the DataTable by which you want to identify it</param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        public DataTable GetDataTable(string query, string ConnectionString, string Name = "Table")
        {
            DataTable dt = new DataTable();
                SQLFramework obj = new SQLFramework();
                dt = obj.GetDataTable(query, ConnectionString, Name);
            return dt;
        }



        /// <summary>
        ///         ''' It is used to set the Login Status of the Perticular User i.e. I for Login and O for LogOut!
        ///         ''' </summary>
        ///         ''' <param name="userid">userid of the user to make him/her login/logout</param>
        ///         ''' <param name="Lstatus">Lstatus i.e. I for Login and O for Logout</param>
        ///         ''' <remarks></remarks>
        public void LogStatus(string userid, string Lstatus)
        {
//            if (System.Web.HttpContext.Current.Session["DBName"] == "SQLServer")
  //          {
                SQLFramework obj = new SQLFramework();
                obj.LogStatus(userid, Lstatus);
    //        }
           // else if (System.Web.HttpContext.Current.Session["DBName"] == "Oracle")
           // {
             //   MultipleFrameworks.OracleFramework obj = new MultipleFrameworks.OracleFramework();
              //  obj.LogStatus(userid, Lstatus);
           // }
           // else if (System.Web.HttpContext.Current.Session["DBName"] == "MySQL")
            //{
              //  MultipleFrameworks.MySQLFramework obj = new MultipleFrameworks.MySQLFramework();
              //  obj.LogStatus(userid, Lstatus);
           // }
           // else if (System.Web.HttpContext.Current.Session["DBName"] == "DB2")
            //{
             //   MultipleFrameworks.DB2Framework obj = new MultipleFrameworks.DB2Framework();
              //  obj.LogStatus(userid, Lstatus);
           // }
          //  else
            //{
              //  OleDbFramework obj = new OleDbFramework();
               // obj.LogStatus(userid, Lstatus);
           // }
        }



        /// <summary>
        ///         ''' A function which is used to execute any DML query on the database that will not generate any result
        ///         ''' </summary>
        ///         ''' <param name="query">query to execute on database</param>
        ///         ''' <param name="ConnectionString">ConnectionString to connect to the database</param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        public bool ExecuteQueryOnDB(string query, string ConnectionString)
        {
            bool flag = false;
                SQLFramework obj = new SQLFramework();
                flag = obj.ExecuteQueryOnDB(query, ConnectionString);
//            else if (System.Web.HttpContext.Current.Session["DBName"] == "Oracle")
 //           {
   //             MultipleFrameworks.OracleFramework obj = new MultipleFrameworks.OracleFramework();
     //           flag = obj.ExecuteQueryOnDB(query, ConnectionString);
      //      }
        //    else if (System.Web.HttpContext.Current.Session["DBName"] == "MySQL")
         //   {
          //      MultipleFrameworks.MySQLFramework obj = new MultipleFrameworks.MySQLFramework();
           //     flag = obj.ExecuteQueryOnDB(query, ConnectionString);
            //}
            //else if (System.Web.HttpContext.Current.Session["DBName"] == "DB2")
           // {
            //    MultipleFrameworks.DB2Framework obj = new MultipleFrameworks.DB2Framework();
             //   flag = obj.ExecuteQueryOnDB(query, ConnectionString);
           // }
            return flag;
        }



        /// <summary>
        ///         ''' A Function that is used to Execute any procedure on the Database
        ///         ''' </summary>
        ///         ''' <param name="ProcedureName">Name of the Procedure to execute it</param>
        ///         ''' <param name="Parameters">ParameterCollection that contain all the parameter values for this Stored Procedure</param>
        ///         ''' <param name="ConnectionString">ConnectionString to connect to the database</param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        public bool ExecuteProcedure(string ProcedureName, ParameterCollection Parameters, string ConnectionString)
        {
            bool flag = false;
                SQLFramework obj = new SQLFramework();
                flag = obj.ExecuteProcedure(ProcedureName, Parameters, ConnectionString);
           // else if (System.Web.HttpContext.Current.Session["DBName"] == "Oracle")
           // {
            //    MultipleFrameworks.OracleFramework obj = new MultipleFrameworks.OracleFramework();
             //   flag = obj.ExecuteProcedure(ProcedureName, Parameters, ConnectionString);
           // }
           // else if (System.Web.HttpContext.Current.Session["DBName"] == "MySQL")
            //{
             //   MultipleFrameworks.MySQLFramework obj = new MultipleFrameworks.MySQLFramework();
              //  flag = obj.ExecuteProcedure(ProcedureName, Parameters, ConnectionString);
           // }
            //else if (System.Web.HttpContext.Current.Session["DBName"] == "DB2")
           // {
           //     MultipleFrameworks.DB2Framework obj = new MultipleFrameworks.DB2Framework();
            //    flag = obj.ExecuteProcedure(ProcedureName, Parameters, ConnectionString);
            //}
            return flag;
        }



        /// <summary>
        ///         ''' A function that will start the Execution of the Transaction i.e. used to begin the transactional execution of the queries"
        ///         ''' </summary>
        ///         ''' <param name="ConnectionString">ConnectionString to connect to the database</param>
        ///         ''' <remarks></remarks>
        public void BeginTransaction(string ConnectionString)
        {
                SQLFramework obj = new SQLFramework();
                obj.BeginTransaction(ConnectionString);
           // else if (System.Web.HttpContext.Current.Session["DBName"] == "Oracle")
           // {
           //     MultipleFrameworks.OracleFramework obj = new MultipleFrameworks.OracleFramework();
            //    obj.BeginTransaction(ConnectionString);
            //}
          //  else if (System.Web.HttpContext.Current.Session["DBName"] == "MySQL")
           // {
            //    MultipleFrameworks.MySQLFramework obj = new MultipleFrameworks.MySQLFramework();
             //   obj.BeginTransaction(ConnectionString);
            //}
           // else if (System.Web.HttpContext.Current.Session["DBName"] == "DB2")
            //{
             //   MultipleFrameworks.DB2Framework obj = new MultipleFrameworks.DB2Framework();
              //  obj.BeginTransaction(ConnectionString);
            //}
      //      else
        //    {
          //      OleDbFramework obj = new OleDbFramework();
            //    obj.BeginTransaction(ConnectionString);
           // }
        }



        /// <summary>
        ///         ''' A Function which is used to Execute any procedure on the Database in the Transactional Manner
        ///         ''' </summary>
        ///         ''' <param name="ProcedureName">Name of the Stored Procedure</param>
        ///         ''' <param name="Parameters">ParameterCollection that conatain the values for all the parameters</param>
        ///         ''' <param name="ConnectionString">ConnectionString to connect to the database</param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        public bool ExecuteProcedureTrans(string ProcedureName, ParameterCollection Parameters, string ConnectionString)
        {
            bool flag = false;
        //    if (System.Web.HttpContext.Current.Session["DBName"] == "SQLServer")
         //   {
                SQLFramework obj = new SQLFramework();
                flag = obj.ExecuteProcedureTrans(ProcedureName, Parameters, ConnectionString);
           // }
//            else if (System.Web.HttpContext.Current.Session["DBName"] == "Oracle")
  //          {
    //            MultipleFrameworks.OracleFramework obj = new MultipleFrameworks.OracleFramework();
      //          flag = obj.ExecuteProcedureTrans(ProcedureName, Parameters, ConnectionString);
        //    }
          //  else if (System.Web.HttpContext.Current.Session["DBName"] == "MySQL")
          //  {
            //    MultipleFrameworks.MySQLFramework obj = new MultipleFrameworks.MySQLFramework();
             //   flag = obj.ExecuteProcedureTrans(ProcedureName, Parameters, ConnectionString);
           // }
          //  else if (System.Web.HttpContext.Current.Session["DBName"] == "DB2")
          //  {
            //    MultipleFrameworks.DB2Framework obj = new MultipleFrameworks.DB2Framework();
             //   flag = obj.ExecuteProcedureTrans(ProcedureName, Parameters, ConnectionString);
            //}
           // else
           // {
             //   MultipleFrameworks.OledbFramework obj = new MultipleFrameworks.OledbFramework();
              //  flag = obj.ExecuteProcedureTrans(ProcedureName, Parameters, ConnectionString);
           // }
            return flag;
        }

        

        /// <summary>
        ///         ''' It will Commit all the execution of the transactions and the changes made to the database
        ///         ''' </summary>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        public bool CommitTransaction()
        {
            bool flag = false;
        //    if (System.Web.HttpContext.Current.Session["DBName"] == "SQLServer")
          //  {
                SQLFramework obj = new SQLFramework();
                flag = obj.CommitTransaction();
            //}
            //else if (System.Web.HttpContext.Current.Session["DBName"] == "Oracle")
            //{
            //    MultipleFrameworks.OracleFramework obj = new MultipleFrameworks.OracleFramework();
            //    flag = obj.CommitTransaction();
            //}
            //else if (System.Web.HttpContext.Current.Session["DBName"] == "MySQL")
            //{
            //    MultipleFrameworks.MySQLFramework obj = new MultipleFrameworks.MySQLFramework();
            //    flag = obj.CommitTransaction();
            //}
            //else if (System.Web.HttpContext.Current.Session["DBName"] == "DB2")
            //{
            //    MultipleFrameworks.DB2Framework obj = new MultipleFrameworks.DB2Framework();
            //    flag = obj.CommitTransaction();
            //}
          //  else
           // {
            //    OleDbFramework obj = new OleDbFramework();
             //   flag = obj.CommitTransaction();
           // }
            return flag;
        }



        /// <summary>
        ///         ''' It will Rollback all the execution of the transactions and undo the changes made to the database
        ///         ''' </summary>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        public bool RollbackTransaction()
        {
            bool flag = false;
          //  if (System.Web.HttpContext.Current.Session["DBName"] == "SQLServer")
           // {
                SQLFramework obj = new SQLFramework();
                flag = obj.RollbackTransaction();
            //}
//            else if (System.Web.HttpContext.Current.Session["DBName"] == "Oracle")
  //          {
    //            MultipleFrameworks.OracleFramework obj = new MultipleFrameworks.OracleFramework();
      //          flag = obj.RollbackTransaction();
        //    }
          //  else if (System.Web.HttpContext.Current.Session["DBName"] == "MySQL")
           // {
            //    MultipleFrameworks.MySQLFramework obj = new MultipleFrameworks.MySQLFramework();
             //   flag = obj.RollbackTransaction();
            //}
           // else if (System.Web.HttpContext.Current.Session["DBName"] == "DB2")
            //{
             //   MultipleFrameworks.DB2Framework obj = new MultipleFrameworks.DB2Framework();
              //  flag = obj.RollbackTransaction();
            //}
           // else
            //{
             //   OleDbFramework obj = new OleDbFramework();
              //  flag = obj.RollbackTransaction();
           // }
            return flag;
        }



        /// <summary>
        ///         ''' A function that will shows the Alert Box on the page
        ///         ''' </summary>
        ///         ''' <param name="Message">Message that you want to display</param>
        ///         ''' <param name="refP">Reference of the page</param>
        ///         ''' <remarks></remarks>
        public void AlertBox(string Message, Page refP)
        {
            string str = "window.alert('" + Message + "')";
            ScriptManager.RegisterClientScriptBlock(refP, refP.GetType(), "UniqueKey", str, true);
        }



        /// <summary>
        ///         ''' A function that will shows the Confirm Box on the page
        ///         ''' </summary>
        ///         ''' <param name="Message">Message that you want to display</param>
        ///         ''' <param name="refP">Reference of the page</param>
        ///         ''' <remarks></remarks>
        public void ConfirmBox(string Message, string Hid, Page refP)
        {
            // Dim script As String = "var r = window.confirm('Press a button');"
            // script &= " /n if (r==true)"
            // script &= " /n {"
            // script &= " \n window.alert('You pressed OK!');"
            // script &= " \n }"
            // script &= " \n Else"
            // script &= " \n {"
            // script &= " \n window.alert('You pressed Cancel!');"
            // script &= " \n }"
            string script = "var a = window.confirm('" + Message + "'); document.getElementById('" + Hid + "').value = a;";
            ScriptManager.RegisterClientScriptBlock(refP, refP.GetType(), "UniqueKey", script, true);
        }



        /// <summary>
        ///         ''' Returns the ConnectionName for the Database Server , by which you login
        ///         ''' </summary>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        public string GetConnectionName()
        {
            var cons = "SqlConn";// ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;

            return cons; 
            /*
            string conName = null;
            if (System.Web.HttpContext.Current.Session["DBName"] == "SQLServer")
                conName = "SQLConnectionString";
 //           else if (System.Web.HttpContext.Current.Session["DBName"] == "Oracle")
   //             conName = "OracleConnectionString";
     //       else if (System.Web.HttpContext.Current.Session["DBName"] == "MySQL")
       //         conName = "MySQLConnectionString";
         //   else if (System.Web.HttpContext.Current.Session["DBName"] == "DB2")
           //     conName = "DB2ConnectionString";
          //  else if (System.Web.HttpContext.Current.Session["DBName"] == "Access")
            //    conName = "OledbConnectionString";
            else
                conName = System.Web.HttpContext.Current.Session["LibWiseDBConn"].ToString();
            return conName;
            */
        }


        /// <summary>
        ///         ''' A Function is used to Auto Insert the Item Types in Item_Type Table in Library!
        ///         ''' </summary>
        ///         ''' <param name="ConnectionString">Connection String to coonect to the Database Server</param>
        ///         ''' <param name="uid">User By whom these details are created</param>
        ///         ''' <remarks></remarks>
        public void Save_ItemType(string ConnectionString, string uid)
        {
            string item = string.Empty;
            item = ExecuteScalar("Select item_type from Item_type where item_type ='Articles'", ConnectionString);
            int id = 0;
            if (item == "")
            {
                id =Convert.ToInt32( ExecuteScalar("Select coalesce(max(id),0) from Item_type", ConnectionString));
                ExecuteQueryOnDB(" insert into Item_type values('" + id + 1 + "','Articles','Arr','0x01010000','" + uid + "')", ConnectionString);
            }

            item = ExecuteScalar("Select item_type from Item_type where item_type ='Project Reports' ", ConnectionString);

            if (item == "")
            {
                id = Convert.ToInt32(ExecuteScalar("Select coalesce(max(id),0) from Item_type", ConnectionString));
                ExecuteQueryOnDB(" insert into Item_type values('" + id + 1 + "','Project Reports','PR','0x01010000','" + uid + "')", ConnectionString);
            }

            item = ExecuteScalar("Select item_type from Item_type where item_type ='Thesis' ", ConnectionString);
            if (item == "")
            {
                id = Convert.ToInt32(ExecuteScalar("Select coalesce(max(id),0) from Item_type", ConnectionString));
                ExecuteQueryOnDB(" insert into Item_type values('" + id + 1 + "','Thesis','ths','0x01010000','" + uid + "')", ConnectionString);
            }

            // item = ExecuteScalar("Select item_type from Item_type where item_type ='Journals' ", ConnectionString)
            // If item = "" Then
            // id = ExecuteScalar("Select coalesce(max(id),0) from Item_type", ConnectionString)
            // ExecuteQueryOnDB(" insert into Item_type values('" & id + 1 & "','Journals','jrnl','0x01010000','" & uid & "')", ConnectionString)
            // End If

            item = ExecuteScalar("Select item_type from Item_type where item_type ='Books' ", ConnectionString);
            if (item == "")
            {
                id = Convert.ToInt32(ExecuteScalar("Select coalesce(max(id),0) from Item_type", ConnectionString));
                ExecuteQueryOnDB(" insert into Item_type values('" + id + 1 + "','Books','bks','0x01010000','" + uid + "')", ConnectionString);
            }

            item = ExecuteScalar("Select item_type from Item_type where item_type ='E-Books' ", ConnectionString);
            if (item == "")
            {
                id = Convert.ToInt32(ExecuteScalar("Select coalesce(max(id),0) from Item_type", ConnectionString));
                ExecuteQueryOnDB(" insert into Item_type values('" + id + 1 + "','E-Books','E-Bks','0x01010000','" + uid + "')", ConnectionString);
            }
        }



        /// <summary>
        ///         ''' This will return the value of first row - first column i.e. scalar value
        ///         ''' </summary>
        ///         ''' <param name="query">Query to get the result</param>
        ///         ''' <param name="ConnectionString">ConnectionString to connect to the database</param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        public string ExecuteScalar(string query, string ConnectionString)
        {
            string str = null;
          //  if (System.Web.HttpContext.Current.Session["DBName"] == "SQLServer")
           // {
                SQLFramework obj = new SQLFramework();
                str = obj.ExecuteScalar(query, ConnectionString);
            //}
//            else if (System.Web.HttpContext.Current.Session["DBName"] == "Oracle")
 //           {
   //             MultipleFrameworks.OracleFramework obj = new MultipleFrameworks.OracleFramework();
     //           str = obj.ExecuteScalar(query, ConnectionString);
       //     }
         //   else if (System.Web.HttpContext.Current.Session["DBName"] == "MySQL")
          //  {
           //     MultipleFrameworks.MySQLFramework obj = new MultipleFrameworks.MySQLFramework();
            //    str = obj.ExecuteScalar(query, ConnectionString);
            //}
           // else if (System.Web.HttpContext.Current.Session["DBName"] == "DB2")
            //{
             //   MultipleFrameworks.DB2Framework obj = new MultipleFrameworks.DB2Framework();
              //  str = obj.ExecuteScalar(query, ConnectionString);
            //}
           // else
            //{
              //  OleDbFramework obj = new OleDbFramework();
               // str = obj.ExecuteScalar(query, ConnectionString);
            //}
            return str;
        }



        /// <summary>
        ///         ''' It will execute the Procedure on the database in the transactional manner that will return the scalar value
        ///         ''' </summary>
        ///         ''' <param name="ProcedureName">Name of the Procedure</param>
        ///         ''' <param name="Parameters">ParametersCollection to the procedure with their values and Datatypes</param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        public string ExecuteScalarTrans(string ProcedureName, ParameterCollection Parameters)
        {
            string Value = null;
           // if (System.Web.HttpContext.Current.Session["DBName"] == "SQLServer")
            //{
                SQLFramework obj = new SQLFramework();
                Value = obj.ExecuteScalarTrans(ProcedureName, Parameters);
            //}
//            else if (System.Web.HttpContext.Current.Session["DBName"] == "Oracle")
  //          {
    //            MultipleFrameworks.OracleFramework obj = new MultipleFrameworks.OracleFramework();
      //          Value = obj.ExecuteScalarTrans(ProcedureName, Parameters);
        //    }
          //  else if (System.Web.HttpContext.Current.Session["DBName"] == "MySQL")
           // {
            //    MultipleFrameworks.MySQLFramework obj = new MultipleFrameworks.MySQLFramework();
             //   Value = obj.ExecuteScalarTrans(ProcedureName, Parameters);
            //}
          //  else if (System.Web.HttpContext.Current.Session["DBName"] == "DB2")
            //{
             //   MultipleFrameworks.DB2Framework obj = new MultipleFrameworks.DB2Framework();
              //  Value = obj.ExecuteScalarTrans(ProcedureName, Parameters);
           // }
//            else
  //          {
    //            OleDbFramework obj = new OleDbFramework();
      //          Value = obj.ExecuteScalarTrans(ProcedureName, Parameters);
        //    }
            return Value;
        }



        /// <summary>
        ///         ''' It will execute the query in the transactional manner over the database and return the scalar value
        ///         ''' </summary>
        ///         ''' <param name="query">Query to execute over the database that will compute single result</param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        public string ExecuteScalarTrans(string query)
        {
            string Value = null;
          //  if (System.Web.HttpContext.Current.Session["DBName"] == "SQLServer")
            //{
                SQLFramework obj = new SQLFramework();
                Value = obj.ExecuteScalarTrans(query);
            //}
//            else if (System.Web.HttpContext.Current.Session["DBName"] == "Oracle")
  //          {
    //            MultipleFrameworks.OracleFramework obj = new MultipleFrameworks.OracleFramework();
      //          Value = obj.ExecuteScalarTrans(query);
        //    }
          //  else if (System.Web.HttpContext.Current.Session["DBName"] == "MySQL")
           // {
            //    MultipleFrameworks.MySQLFramework obj = new MultipleFrameworks.MySQLFramework();
             //   Value = obj.ExecuteScalarTrans(query);
            //}
           // else if (System.Web.HttpContext.Current.Session["DBName"] == "DB2")
            //{
             //   MultipleFrameworks.DB2Framework obj = new MultipleFrameworks.DB2Framework();
              //  Value = obj.ExecuteScalarTrans(query);
            //}
//            else
  //          {
    //            OleDbFramework obj = new OleDbFramework();
      //          Value = obj.ExecuteScalarTrans(query);
        //    }
            return Value;
        }



        /// <summary>
        ///         ''' It will execute the query in the transactional manner over the database
        ///         ''' </summary>
        ///         ''' <param name="query">Query to execute on database</param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        public bool ExecuteQueryTrans(string query)
        {
            bool flag = false;
          //  if (System.Web.HttpContext.Current.Session["DBName"] == "SQLServer")
           // {
                SQLFramework obj = new SQLFramework();
                flag = obj.ExecuteQueryTrans(query);
            //}
//            else if (System.Web.HttpContext.Current.Session["DBName"] == "Oracle")
  //          {
    //            MultipleFrameworks.OracleFramework obj = new MultipleFrameworks.OracleFramework();
      //          flag = obj.ExecuteQueryTrans(query);
        //    }
         //   else if (System.Web.HttpContext.Current.Session["DBName"] == "MySQL")
          //  {
           //     MultipleFrameworks.MySQLFramework obj = new MultipleFrameworks.MySQLFramework();
            //    flag = obj.ExecuteQueryTrans(query);
            //}
          //  else if (System.Web.HttpContext.Current.Session["DBName"] == "DB2")
           // {
            //    MultipleFrameworks.DB2Framework obj = new MultipleFrameworks.DB2Framework();
             //   flag = obj.ExecuteQueryTrans(query);
            //}
          //  else
            //{
              //  OleDbFramework obj = new OleDbFramework();
                //flag = obj.ExecuteQueryTrans(query);
            //}
            return flag;
        }



        /// <summary>
        ///         ''' This will send the E-Mail to the database table according to the user Id
        ///         ''' </summary>
        ///         ''' <param name="tablename">The Name of the Table in which user details are available</param>
        ///         ''' <param name="columnname">The Name of the column in which the userid available</param>
        ///         ''' <param name="userid">The userid of the user, by which they are uniquely identified</param>
        ///         ''' <param name="subject">The Subject Of the E-Mail Message</param>
        ///         ''' <param name="body">The Body Message of the E-Mail</param>
        ///         ''' <param name="emailPage">Reference of the Page</param>
        ///         ''' <param name="mailfirst">"To" E-mail Id Column Name</param>
        ///         ''' <param name="mailsecond">"CC" E-Mail Id Column Name</param>
        ///         ''' <param name="ConnectionString">ConnectionString To Connect to the database</param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        public bool sendMailToTable(string tablename, string columnname, string userid, string subject, string body, Page emailPage, string mailfirst, string mailsecond, string ConnectionString)
        {
            bool flag = false;
            string mailto="", cc="";
            string record = "select * from " + tablename + " where " + columnname + "='" + userid + "'";
            DataSet ds = new DataSet();
            ds = GetDataSet(record, ConnectionString);
            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                mailto = ds.Tables[0].Rows[i][mailfirst].ToString();
                cc = ds.Tables[0].Rows[i][mailsecond].ToString();
            }
            subject = subject.Replace("'", "");
            body = body.Replace("'", "");
            string cmdstr = "Insert Into MailStatus(MailTo,CC,Subject,Body,EmailPage,Status,MailGenerateDT) Values('" + mailto + "','" + cc + "','" + subject + "','" + body + "','" + emailPage.ToString() + "','P','" + System.DateTime.Now.ToString() + "')";
            flag = ExecuteQueryOnDB(cmdstr, ConnectionString);
            return flag;
        }



        /// <summary>
        ///         ''' This will send the E-Mail to the database table according to the user Id
        ///         ''' </summary>
        ///         ''' <param name="subject">Subject of the E-Mail</param>
        ///         ''' <param name="body">Body of the E-Mail</param>
        ///         ''' <param name="emailPage">Reference of the Page</param>
        ///         ''' <param name="mailfirst">First E-Mail id of the Receiver</param>
        ///         ''' <param name="mailsecond">Second E-Mail id of the Receiver</param>
        ///         ''' <param name="ConnectionString">ConnectionString to connect to the database</param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        public bool sendMailToTable(string subject, string body, Page emailPage, string mailfirst, string mailsecond, string ConnectionString)
        {
            bool flag = false;
            try
            {
                string mailto, cc;
                mailto = mailfirst;
                cc = mailsecond;
                subject = subject.Replace("'", "");
                body = body.Replace("'", "");
                string cmdstr = "Insert Into MailStatus(MailTo,CC,Subject,Body,EmailPage,Status,MailGenerateDT) Values('" + mailto + "','" + cc + "','" + subject + "','" + body + "','" + emailPage.ToString() + "','P','" + System.DateTime.Now.ToString() + "')";
                flag = ExecuteQueryOnDB(cmdstr, ConnectionString);
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return flag;
        }



        /// <summary>
        ///         ''' The function is created to check the existance of child record of a master table at the time of deletion of record
        ///         ''' </summary>
        ///         ''' <param name="idFld">Column to select from the database table</param>
        ///         ''' <param name="tablename">Name of the database table</param>
        ///         ''' <param name="condition">Condition on which the result should be filtered</param>
        ///         ''' <param name="connectString">ConnectionString to connect to the database</param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        public bool checkChildExistance(string idFld, string tablename, string condition, string connectString)
        {
            bool result;
            DataSet chkds = new DataSet();
            chkds = GetDataSet("select " + idFld + " from " + tablename + " where " + condition, connectString, "result");
            if (chkds.Tables[0].Rows.Count > 0)
                result = true; // child exists
            else
                result = false;
            chkds.Dispose();
            return result;
        }



        public void populateAfterDeletion(DataGrid Grid, string qryString, string ConnectionString)
        {
            // Dim connStr As New OleDb.OleDbConnection(ConnectionStrings("ConnectionString").ConnectionString)
            // connStr.Open()
            int count_value, f, pg;
            pg = Grid.PageSize;
            f = Grid.CurrentPageIndex;
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            ds = GetDataSet(qryString, ConnectionString);
            if (ds.Tables[0].Rows.Count > 0)
            {
                count_value = ds.Tables[0].Rows.Count;
                if (f < (count_value / pg))
                    Grid.CurrentPageIndex = f;
                else
                    Grid.CurrentPageIndex = (count_value / pg) - 1;
                Grid.DataSource = ds.Tables[0].DefaultView;
                Grid.DataBind();
            }
            else
            {
                Grid.DataSource = dt;
                Grid.DataBind();
            }
            ds.Dispose();
        }



        /// <summary>
        ///         ''' It will bind the data directly to the DropDownList and return the DropDownList to the requester page
        ///         ''' </summary>
        ///         ''' <param name="query">Query to execute on the database</param>
        ///         ''' <param name="DataTextField">DataTextField Name for the DropDownList</param>
        ///         ''' <param name="DataValueField">DataValueField Name for the DropDownList</param>
        ///         ''' <param name="ConnectionString">ConnectionString To Connect to the Database</param>
        ///         ''' <param name="SelectValue">The default select value in DropDownList like "---Select---"</param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        public DropDownList BindDropDownList(string query, string DataTextField, string DataValueField, string ConnectionString, string SelectValue)
        {
            DropDownList DDL = new DropDownList();
          //  if (System.Web.HttpContext.Current.Session["DBName"] == "SQLServer")
            //{
                SQLFramework obj = new SQLFramework();
                DDL = obj.BindDropDownList(query, DataTextField, DataValueField, ConnectionString, SelectValue);
            //}
//            else if (System.Web.HttpContext.Current.Session["DBName"] == "Oracle")
 //           {
  //              MultipleFrameworks.OracleFramework obj = new MultipleFrameworks.OracleFramework();
   //             DDL = obj.BindDropDownList(query, DataTextField, DataValueField, ConnectionString, SelectValue);
    //        }
     //       else if (System.Web.HttpContext.Current.Session["DBName"] == "MySQL")
      //      {
       //         MultipleFrameworks.MySQLFramework obj = new MultipleFrameworks.MySQLFramework();
        //        DDL = obj.BindDropDownList(query, DataTextField, DataValueField, ConnectionString, SelectValue);
         //   }
          //  else if (System.Web.HttpContext.Current.Session["DBName"] == "DB2")
           // {
             //   MultipleFrameworks.DB2Framework obj = new MultipleFrameworks.DB2Framework();
             //   DDL = obj.BindDropDownList(query, DataTextField, DataValueField, ConnectionString, SelectValue);
           // }
           // else
           // {
             //   OleDbFramework obj = new OleDbFramework();
              //  DDL = obj.BindDropDownList(query, DataTextField, DataValueField, ConnectionString, SelectValue);
           // }
            return DDL;
        }



        /// <summary>
        ///         ''' It will directly bind the data to the DropDownList
        ///         ''' </summary>
        ///         ''' <param name="query">Query that will execute over the database</param>
        ///         ''' <param name="DataTextField">DataTextField column to bind the database to DropDownList </param>
        ///         ''' <param name="DataValueField">DataValueField column to bind the database to DropDownList</param>
        ///         ''' <param name="ConnectionString">ConnectionString to Connect to the database</param>
        ///         ''' <param name="DropDownList">DropDownList on which the result set will be bind</param>
        ///         ''' <param name="SelectValue">The default selected value of the DropDownList</param>
        ///         ''' <remarks></remarks>
        public void BindDropDownList(string query, string DataTextField, string DataValueField, string ConnectionString, DropDownList DropDownList, string SelectValue)
        {
//            if (System.Web.HttpContext.Current.Session["DBName"] == "SQLServer")
  //          {
                SQLFramework obj = new SQLFramework();
                obj.BindDropDownList(query, DataTextField, DataValueField, ConnectionString, DropDownList, SelectValue);
    //        }
//            else if (System.Web.HttpContext.Current.Session["DBName"] == "Oracle")
  //          {
    //            MultipleFrameworks.OracleFramework obj = new MultipleFrameworks.OracleFramework();
      //          obj.BindDropDownList(query, DataTextField, DataValueField, ConnectionString, DropDownList, SelectValue);
        //    }
        //    else if (System.Web.HttpContext.Current.Session["DBName"] == "MySQL")
         //   {
          //      MultipleFrameworks.MySQLFramework obj = new MultipleFrameworks.MySQLFramework();
           //     obj.BindDropDownList(query, DataTextField, DataValueField, ConnectionString, DropDownList, SelectValue);
            //}
          //  else if (System.Web.HttpContext.Current.Session["DBName"] == "DB2")
           // {
            //    MultipleFrameworks.DB2Framework obj = new MultipleFrameworks.DB2Framework();
             //   obj.BindDropDownList(query, DataTextField, DataValueField, ConnectionString, DropDownList, SelectValue);
           // }
      //      else
       //     {
         //       OleDbFramework obj = new OleDbFramework();
           //     obj.BindDropDownList(query, DataTextField, DataValueField, ConnectionString, DropDownList, SelectValue);
            //}
        }



        /// <summary>
        ///         ''' It will bind the data directly to the Listbox and return it
        ///         ''' </summary>
        ///         ''' <param name="query">Query that will execute on the Database</param>
        ///         ''' <param name="DataTextField">DataTextField column that will bind to the ListBox</param>
        ///         ''' <param name="DataValueField">DataValueField column that will bind to the ListBox</param>
        ///         ''' <param name="ConnectionString">ConnectionString to connect to the database</param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        public ListBox BindListBox(string query, string DataTextField, string DataValueField, string ConnectionString)
        {
            ListBox LB = new ListBox();
          //  if (System.Web.HttpContext.Current.Session["DBName"] == "SQLServer")
           // {
                SQLFramework obj = new SQLFramework();
                LB = obj.BindListBox(query, DataTextField, DataValueField, ConnectionString);
            //}
//            else if (System.Web.HttpContext.Current.Session["DBName"] == "Oracle")
  //          {
    //            MultipleFrameworks.OracleFramework obj = new MultipleFrameworks.OracleFramework();
      //          LB = obj.BindListBox(query, DataTextField, DataValueField, ConnectionString);
        //    }
         //   else if (System.Web.HttpContext.Current.Session["DBName"] == "MySQL")
          //  {
           //     MultipleFrameworks.MySQLFramework obj = new MultipleFrameworks.MySQLFramework();
            //    LB = obj.BindListBox(query, DataTextField, DataValueField, ConnectionString);
            //}
          //  else if (System.Web.HttpContext.Current.Session["DBName"] == "DB2")
           // {
            //    MultipleFrameworks.DB2Framework obj = new MultipleFrameworks.DB2Framework();
             //   LB = obj.BindListBox(query, DataTextField, DataValueField, ConnectionString);
            ///}
//            else
  //          {
    //            OleDbFramework obj = new OleDbFramework();
      //          LB = obj.BindListBox(query, DataTextField, DataValueField, ConnectionString);
        //    }
            return LB;
        }



        /// <summary>
        ///         ''' It will bind the data directly to the ListBox
        ///         ''' </summary>
        ///         ''' <param name="query">Query that will execute on the database</param>
        ///         ''' <param name="DataTextField">DataTextField Column that will bind to the DropDownList</param>
        ///         ''' <param name="DataValueField">DataValueField Column that will bind to the DropDownList</param>
        ///         ''' <param name="ConnectionString">ConnectionString to connect to the database</param>
        ///         ''' <param name="ListBox">ListBox to which the database will be bind</param>
        ///         ''' <remarks></remarks>
        public void BindListBox(string query, string DataTextField, string DataValueField, string ConnectionString, ListBox ListBox)
        {
//            if (System.Web.HttpContext.Current.Session["DBName"] == "SQLServer")
  //          {
                SQLFramework obj = new SQLFramework();
                obj.BindListBox(query, DataTextField, DataValueField, ConnectionString, ListBox);
    //        }
//            else if (System.Web.HttpContext.Current.Session["DBName"] == "Oracle")
  //          {
    //            MultipleFrameworks.OracleFramework obj = new MultipleFrameworks.OracleFramework();
      //          obj.BindListBox(query, DataTextField, DataValueField, ConnectionString, ListBox);
        //    }
          //  else if (System.Web.HttpContext.Current.Session["DBName"] == "MySQL")
           // {
            //    MultipleFrameworks.MySQLFramework obj = new MultipleFrameworks.MySQLFramework();
             //   obj.BindListBox(query, DataTextField, DataValueField, ConnectionString, ListBox);
            //}
         //   else if (System.Web.HttpContext.Current.Session["DBName"] == "DB2")
          //  {
           //     MultipleFrameworks.DB2Framework obj = new MultipleFrameworks.DB2Framework();
            //    obj.BindListBox(query, DataTextField, DataValueField, ConnectionString, ListBox);
            //}
    //        else
      //      {
        //        OleDbFramework obj = new OleDbFramework();
          //      obj.BindListBox(query, DataTextField, DataValueField, ConnectionString, ListBox);
            //}
        }



        /// <summary>
        ///         ''' It will bind the data directly to the RadioButtonList and return it
        ///         ''' </summary>
        ///         ''' <param name="query">Query that will execute over the database</param>
        ///         ''' <param name="DataTextField">DataTextField Column that will bind to the RadioButtonList</param>
        ///         ''' <param name="DataValueField">DataValueField Column that will bind to the RadioButtonList</param>
        ///         ''' <param name="ConnectionString">ConnectionString to Connect to the database</param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        public RadioButtonList BindRadioButtonList(string query, string DataTextField, string DataValueField, string ConnectionString)
        {
            RadioButtonList RBL = new RadioButtonList();
//            if (System.Web.HttpContext.Current.Session["DBName"] == "SQLServer")
  //          {
                SQLFramework obj = new SQLFramework();
                RBL = obj.BindRadioButtonList(query, DataTextField, DataValueField, ConnectionString);
    //        }
//            else if (System.Web.HttpContext.Current.Session["DBName"] == "Oracle")
  //          {
    //            MultipleFrameworks.OracleFramework obj = new MultipleFrameworks.OracleFramework();
      //          RBL = obj.BindRadioButtonList(query, DataTextField, DataValueField, ConnectionString);
        //    }
         //   else if (System.Web.HttpContext.Current.Session["DBName"] == "MySQL")
          //  {
           //     MultipleFrameworks.MySQLFramework obj = new MultipleFrameworks.MySQLFramework();
            //    RBL = obj.BindRadioButtonList(query, DataTextField, DataValueField, ConnectionString);
            //}
          //  else if (System.Web.HttpContext.Current.Session["DBName"] == "DB2")
           // {
            //    MultipleFrameworks.DB2Framework obj = new MultipleFrameworks.DB2Framework();
             //   RBL = obj.BindRadioButtonList(query, DataTextField, DataValueField, ConnectionString);
            //}
    //        else
      //      {
        //        OleDbFramework obj = new OleDbFramework();
          //      RBL = obj.BindRadioButtonList(query, DataTextField, DataValueField, ConnectionString);
            //}
            return RBL;
        }



        /// <summary>
        ///         ''' It will bind the data directly to the RadioButtonList
        ///         ''' </summary>
        ///         ''' <param name="query">Query to execute on the database that will generate some results</param>
        ///         ''' <param name="DataTextField">DataTextField Column that will bind to the RadioButtonList</param>
        ///         ''' <param name="DataValueField">DataValueField Column that will bind to the RadioButtonList</param>
        ///         ''' <param name="ConnectionString">ConnectionString to connect to the database</param>
        ///         ''' <param name="RadioButtonList">RadioButtonList to which the Result will be bind</param>
        ///         ''' <remarks></remarks>
        public void BindRadioButtonList(string query, string DataTextField, string DataValueField, string ConnectionString, RadioButtonList RadioButtonList)
        {
//            if (System.Web.HttpContext.Current.Session["DBName"] == "SQLServer")
  //          {
                SQLFramework obj = new SQLFramework();
                obj.BindRadioButtonList(query, DataTextField, DataValueField, ConnectionString, RadioButtonList);
    //        }
//            else if (System.Web.HttpContext.Current.Session["DBName"] == "Oracle")
 //           {
  //              MultipleFrameworks.OracleFramework obj = new MultipleFrameworks.OracleFramework();
   //             obj.BindRadioButtonList(query, DataTextField, DataValueField, ConnectionString, RadioButtonList);
    //        }
      //      else if (System.Web.HttpContext.Current.Session["DBName"] == "MySQL")
       //     {
        //        MultipleFrameworks.MySQLFramework obj = new MultipleFrameworks.MySQLFramework();
         //       obj.BindRadioButtonList(query, DataTextField, DataValueField, ConnectionString, RadioButtonList);
          //  }
           // else if (System.Web.HttpContext.Current.Session["DBName"] == "DB2")
            //{
             //   MultipleFrameworks.DB2Framework obj = new MultipleFrameworks.DB2Framework();
              //  obj.BindRadioButtonList(query, DataTextField, DataValueField, ConnectionString, RadioButtonList);
            //}
    //        else
      //      {
        //        OleDbFramework obj = new OleDbFramework();
          //      obj.BindRadioButtonList(query, DataTextField, DataValueField, ConnectionString, RadioButtonList);
            //}
        }



        /// <summary>
        ///         ''' It will bind the data directly to the CheckBoxList and return it
        ///         ''' </summary>
        ///         ''' <param name="query">Query that will execute on the database</param>
        ///         ''' <param name="DataTextField">DataTextField Column that will bind to the CheckBoxList</param>
        ///         ''' <param name="DataValueField">DataValueField Column that will bind to the CheckBoxList</param>
        ///         ''' <param name="ConnectionString">ConnectionString to connect to the database</param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        public CheckBoxList BindCheckBoxList(string query, string DataTextField, string DataValueField, string ConnectionString)
        {
            CheckBoxList CBL = new CheckBoxList();
//            if (System.Web.HttpContext.Current.Session["DBName"] == "SQLServer")
  //          {
                SQLFramework obj = new SQLFramework();
                CBL = obj.BindCheckBoxList(query, DataTextField, DataValueField, ConnectionString);
    //        }
//            else if (System.Web.HttpContext.Current.Session["DBName"] == "Oracle")
 //           {
   //             MultipleFrameworks.OracleFramework obj = new MultipleFrameworks.OracleFramework();
     //           CBL = obj.BindCheckBoxList(query, DataTextField, DataValueField, ConnectionString);
       //     }
         //   else if (System.Web.HttpContext.Current.Session["DBName"] == "MySQL")
           // {
            //    MultipleFrameworks.MySQLFramework obj = new MultipleFrameworks.MySQLFramework();
             //   CBL = obj.BindCheckBoxList(query, DataTextField, DataValueField, ConnectionString);
            //}
          //  else if (System.Web.HttpContext.Current.Session["DBName"] == "DB2")
           // {
            //    MultipleFrameworks.DB2Framework obj = new MultipleFrameworks.DB2Framework();
             //   CBL = obj.BindCheckBoxList(query, DataTextField, DataValueField, ConnectionString);
            //}
    //        else
      //      {
        //        OleDbFramework obj = new OleDbFramework();
          //      CBL = obj.BindCheckBoxList(query, DataTextField, DataValueField, ConnectionString);
            //}
            return CBL;
        }



        /// <summary>
        ///         ''' It will bind the data directly to the CheckBoxList
        ///         ''' </summary>
        ///         ''' <param name="query">Query that will execute on the database and generate the result</param>
        ///         ''' <param name="DataTextField">DataTextField Column that will bind to the CheckBoxList</param>
        ///         ''' <param name="DataValueField">DataValueField Column that will bind to the CheckBoxList</param>
        ///         ''' <param name="ConnectionString">ConnectionString to connect to the database</param>
        ///         ''' <param name="CheckBoxList">CheckBoxList on which the result will be bind</param>
        ///         ''' <remarks></remarks>
        public void BindCheckBoxList(string query, string DataTextField, string DataValueField, string ConnectionString, CheckBoxList CheckBoxList)
        {
//            if (System.Web.HttpContext.Current.Session["DBName"] == "SQLServer")
  //          {
                SQLFramework obj = new SQLFramework();
                obj.BindCheckBoxList(query, DataTextField, DataValueField, ConnectionString, CheckBoxList);
    //        }
//            else if (System.Web.HttpContext.Current.Session["DBName"] == "Oracle")
 //           {
  //              MultipleFrameworks.OracleFramework obj = new MultipleFrameworks.OracleFramework();
   //             obj.BindCheckBoxList(query, DataTextField, DataValueField, ConnectionString, CheckBoxList);
    //        }
     //       else if (System.Web.HttpContext.Current.Session["DBName"] == "MySQL")
      //      {
       ///         MultipleFrameworks.MySQLFramework obj = new MultipleFrameworks.MySQLFramework();
         //       obj.BindCheckBoxList(query, DataTextField, DataValueField, ConnectionString, CheckBoxList);
          //  }
           // else if (System.Web.HttpContext.Current.Session["DBName"] == "DB2")
      //      {
       //         MultipleFrameworks.DB2Framework obj = new MultipleFrameworks.DB2Framework();
        //        obj.BindCheckBoxList(query, DataTextField, DataValueField, ConnectionString, CheckBoxList);
         //   }
    //        else
      //      {
        //        OleDbFramework obj = new OleDbFramework();
          //      obj.BindCheckBoxList(query, DataTextField, DataValueField, ConnectionString, CheckBoxList);
            //}
        }



        /// <summary>
        ///         ''' It will bind the data directly to the GridView
        ///         ''' </summary>
        ///         ''' <param name="query">Query that will execute on the database and generate the result set</param>
        ///         ''' <param name="ConnectionString">ConnectionString to Connect to the database</param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
  /*      public GridView BindGridView(string query, string ConnectionString)
        {
            GridView GV = new GridView();
            if (System.Web.HttpContext.Current.Session["DBName"] == "SQLServer")
            {
                SQLFramework obj = new SQLFramework();
                GV = obj.BindGridView(query, ConnectionString);
            }
          //  else if (System.Web.HttpContext.Current.Session["DBName"] == "Oracle")
            //{
             //   MultipleFrameworks.OracleFramework obj = new MultipleFrameworks.OracleFramework();
              //  GV = obj.BindGridView(query, ConnectionString);
           // }
            //else if (System.Web.HttpContext.Current.Session["DBName"] == "MySQL")
           // {
            //    MultipleFrameworks.MySQLFramework obj = new MultipleFrameworks.MySQLFramework();
             //   GV = obj.BindGridView(query, ConnectionString);
           // }
           // else if (System.Web.HttpContext.Current.Session["DBName"] == "DB2")
            //{
             //   MultipleFrameworks.DB2Framework obj = new MultipleFrameworks.DB2Framework();
              //  GV = obj.BindGridView(query, ConnectionString);
            //}
            else
            {
                OleDbFramework obj = new OleDbFramework();
                GV = obj.BindGridView(query, ConnectionString);
            }
            return GV;
        }

*/

        /// <summary>
        ///         ''' It will bind the data directly to the DataGrid
        ///         ''' </summary>
        ///         ''' <param name="query">Query that will execute on the database and generate the result set</param>
        ///         ''' <param name="ConnectionString">ConnectionString to  connect to the database</param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        public DataGrid BindDataGrid(string query, string ConnectionString)
        {
            DataGrid DG = new DataGrid();
  //          if (System.Web.HttpContext.Current.Session["DBName"] == "SQLServer")
    //        {
                SQLFramework obj = new SQLFramework();
                DG = obj.BindDataGrid(query, ConnectionString);
      //      }
        //    else if (System.Web.HttpContext.Current.Session["DBName"] == "Oracle")
      //      {
              //  MultipleFrameworks.OracleFramework obj = new MultipleFrameworks.OracleFramework();
             //   DG = obj.BindDataGrid(query, ConnectionString);
           // }
          //  else if (System.Web.HttpContext.Current.Session["DBName"] == "MySQL")
         //   {
            //    MultipleFrameworks.MySQLFramework obj = new MultipleFrameworks.MySQLFramework();
          //      DG = obj.BindDataGrid(query, ConnectionString);
        //    }
        //    else if (System.Web.HttpContext.Current.Session["DBName"] == "DB2")
      //      {
    //            MultipleFrameworks.DB2Framework obj = new MultipleFrameworks.DB2Framework();
  //              DG = obj.BindDataGrid(query, ConnectionString);
//            }
        //    else
          //  {
            //    OleDbFramework obj = new OleDbFramework();
              //  DG = obj.BindDataGrid(query, ConnectionString);
            //}
            return DG;
        }
        
    }
}

