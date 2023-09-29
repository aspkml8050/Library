using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace Library.App_Code.MultipleFramworks
{
    public class OleDbFramework
    {
        // Global Variables
        private static OleDbTransaction trans;
        private static OleDbConnection Tcon;
        private static OleDbCommand Tcmd;

        // A function that will return the ConnectionString of the specified name
        /// <summary>
        ///         ''' A function that will return the ConnectionString of the specified name
        ///         ''' </summary>
        ///         ''' <param name="Name">Type The Name of the ConnectionString and it will return the value ConnectionString from the Web.config File</param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        public string GetConnectionString(string Name)
        {
            string constr = ConfigurationManager.ConnectionStrings[Name].ConnectionString.ToString();
            return constr;
        }

        // A function that will return the Default ConnectionString i.e. OledbConnectionString
        public string DefaultConnectionString()
        {
            string constr = ConfigurationManager.ConnectionStrings["OledbConnectionString"].ConnectionString.ToString();
            return constr;
        }

        // A function that will execute the query send by the caller and return the DataSet containg the result
        public DataSet GetDataSet(string query, string ConnectionString, string Name = "Table")
        {
            DataSet ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter(query, ConnectionString);
            da.Fill(ds, Name);
            return ds;
        }

        // A function that will execute the query send by the caller and return the DataTable containg the result
        public DataTable GetDataTable(string query, string ConnectionString, string Name = "Table")
        {
            DataSet ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter(query, ConnectionString);
            da.Fill(ds, Name);
            return ds.Tables[0];
        }

        public void LogStatus(string userid, string Lstatus)
        {
            OleDbConnection Lcon = new OleDbConnection(GetConnectionString(System.Web.HttpContext.Current.Session["LibWiseDBConn"].ToString()).ToString());
            OleDbCommand Lcmd = new OleDbCommand();
            Lcon.Open();
            Lcmd.CommandType = CommandType.StoredProcedure;
            Lcmd.CommandText = "LoginStatus";
            Lcmd.Connection = Lcon;
            Lcmd.Parameters.Add("@userid", OleDbType.VarChar).Value = userid.Trim().ToString();
            Lcmd.Parameters.Add("@Lstatus", OleDbType.VarChar).Value = Lstatus;
            Lcmd.ExecuteNonQuery();
            Lcon.Close();
            Lcon.Dispose();
        }

        // A function that will execute any query on the database that will not generate any result
        public bool ExecuteQueryOnDB(string query, string ConnectionString)
        {
            bool flag = false;
            try
            {
                OleDbConnection con = new OleDbConnection();
                con.ConnectionString = ConnectionString;

                con.Open();

                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 100;
                cmd.Connection = con;

                cmd.ExecuteNonQuery();

                con.Close();
                con.Dispose();
                flag = true;
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return flag;
        }

        // A single Function which is used to Execute any procedure on the Database
        public bool ExecuteProcedure(string ProcedureName, ParameterCollection Parameters, string ConnectionString)
        {
            bool flag = false;
            try
            {
                for (int i = 0; i <= Parameters.Count - 1; i++)
                    Parameters[i].Name = Parameters[i].Name.Trim('@');

                OleDbConnection con = new OleDbConnection();
                con.ConnectionString = ConnectionString;

                con.Open();

                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = ProcedureName.Trim();
                cmd.CommandTimeout = 320;
                cmd.Connection = con;

                OleDbCommandBuilder.DeriveParameters(cmd);

                OleDbParameterCollection PCollection;
                PCollection = cmd.Parameters;

                for (int i = 0; i <= Parameters.Count - 1; i++)
                {
                    if (Parameters[i].DbType == DbType.Byte)
                        PCollection[Parameters[i].Name].Value = Convert.FromBase64String(Parameters[i].DefaultValue);
                    else if (Parameters[i].DbType == DbType.Binary)
                        PCollection[Parameters[i].Name].Value = Convert.FromBase64String(Parameters[i].DefaultValue);
                    else if (Parameters[i].DbType == DbType.DateTime)
                        PCollection[Parameters[i].Name].Value = Convert.ToDateTime(Parameters[i].DefaultValue);
                    else if (Parameters[i].DbType == DbType.Int32)
                        PCollection[Parameters[i].Name].Value = Convert.ToInt32(Parameters[i].DefaultValue);
                    else if (Parameters[i].DbType == DbType.String)
                        PCollection[Parameters[i].Name].Value = Convert.ToString(Parameters[i].DefaultValue);
                    else if (Parameters[i].DbType == DbType.Date)
                        PCollection[Parameters[i].Name].Value = Convert.ToDateTime(Parameters[i].DefaultValue);
                    else if (Parameters[i].DbType == DbType.Decimal)
                        PCollection[Parameters[i].Name].Value = Convert.ToDecimal(Parameters[i].DefaultValue);
                    else if (Parameters[i].DbType == DbType.Double)
                        PCollection[Parameters[i].Name].Value = Convert.ToDouble(Parameters[i].DefaultValue);
                    else
                        PCollection[Parameters[i].Name].Value = Convert.ToString(Parameters[i].DefaultValue);
                }

                cmd.ExecuteNonQuery();

                con.Close();
                con.Dispose();
                flag = true;
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return flag;
        }

        // A function that will start the Execution of the Transaction
        public void BeginTransaction(string ConnectionString)
        {
            Tcon = new OleDbConnection(ConnectionString);
            Tcon.Open();
            trans = Tcon.BeginTransaction();
            Tcmd = new OleDbCommand();
        }

        public string ExecuteScalarTrans(string ProcedureName, ParameterCollection Parameters)
        {
            Tcmd.Parameters.Clear();
            Tcmd.CommandText = ProcedureName;
            Tcmd.CommandType = CommandType.StoredProcedure;
            Tcmd.CommandTimeout = 320;
            Tcmd.Connection = Tcon;
            Tcmd.Transaction = trans;

            string Value = Tcmd.ExecuteScalar().ToString();
            return Value;
        }

        public string ExecuteScalarTrans(string query)
        {
            // Tcmd.Parameters.Clear()
            Tcmd.CommandText = query;
            Tcmd.CommandType = CommandType.Text;
            Tcmd.CommandTimeout = 320;
            Tcmd.Connection = Tcon;
            Tcmd.Transaction = trans;

            string Value = Tcmd.ExecuteScalar().ToString();
            return Value;
        }

        public bool ExecuteQueryTrans(string query)
        {
            bool flag = false;
            try
            {
                Tcmd.Parameters.Clear();
                Tcmd.CommandText = query;
                Tcmd.CommandType = CommandType.Text;
                Tcmd.CommandTimeout = 320;
                Tcmd.Connection = Tcon;
                Tcmd.Transaction = trans;

                Tcmd.ExecuteNonQuery();
                flag = true;
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return flag;
        }

        // A single Function which is used to Execute any procedure on the Database in the Transactional Manner
        public bool ExecuteProcedureTrans(string ProcedureName, ParameterCollection Parameters, string ConnectionString)
        {
            bool flag = false;
            try
            {
                Tcmd.CommandType = CommandType.StoredProcedure;
                Tcmd.CommandText = ProcedureName.Trim();
                Tcmd.CommandTimeout = 320;
                Tcmd.Connection = Tcon;
                Tcmd.Transaction = trans;

                OleDbCommandBuilder.DeriveParameters(Tcmd);

                for (int i = 0; i <= Parameters.Count - 1; i++)
                    Parameters[i].Name = Parameters[i].Name.Trim('@');

                OleDbParameterCollection PCollection;
                PCollection = Tcmd.Parameters;

                for (int i = 0; i <= Parameters.Count - 1; i++)
                {
                    if (Parameters[i].DbType == DbType.Byte)
                        PCollection[Parameters[i].Name].Value = Convert.FromBase64String(Parameters[i].DefaultValue);
                    else if (Parameters[i].DbType == DbType.Binary)
                        PCollection[Parameters[i].Name].Value = Convert.FromBase64String(Parameters[i].DefaultValue);
                    else if (Parameters[i].DbType == DbType.DateTime)
                        PCollection[Parameters[i].Name].Value = Convert.ToDateTime(Parameters[i].DefaultValue);
                    else if (Parameters[i].DbType == DbType.Int32)
                        PCollection[Parameters[i].Name].Value = Convert.ToInt32(Parameters[i].DefaultValue);
                    else if (Parameters[i].DbType == DbType.String)
                        PCollection[Parameters[i].Name].Value = Convert.ToString(Parameters[i].DefaultValue);
                    else if (Parameters[i].DbType == DbType.Date)
                        PCollection[Parameters[i].Name].Value = Convert.ToDateTime(Parameters[i].DefaultValue);
                    else if (Parameters[i].DbType == DbType.Decimal)
                        PCollection[Parameters[i].Name].Value = Convert.ToDecimal(Parameters[i].DefaultValue);
                    else if (Parameters[i].DbType == DbType.Double)
                        PCollection[Parameters[i].Name].Value = Convert.ToDouble(Parameters[i].DefaultValue);
                    else
                        PCollection[Parameters[i].Name].Value = Convert.ToString(Parameters[i].DefaultValue);
                }

                Tcmd.ExecuteNonQuery();

                flag = true;
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return flag;
        }

        // A function that will Commit the execution of the transaction
        public bool CommitTransaction()
        {
            bool flag = false;
            try
            {
                trans.Commit();
                Tcon.Close();
                Tcon.Dispose();
                Tcmd.Dispose();
                trans.Dispose();
                flag = true;
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return flag;
        }

        // A function that will Rollback the execution of the transaction
        public bool RollbackTransaction()
        {
            bool flag = false;
            try
            {
                trans.Rollback();
                Tcmd.Dispose();
                Tcon.Close();
                Tcon.Dispose();
                trans.Dispose();
                flag = true;
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return flag;
        }

        // A function that will shows the Alert Box on the page
        public void AlertBox(string Message, Page refP)
        {
            string str = "window.alert('" + Message + "')";
            ScriptManager.RegisterClientScriptBlock(refP, refP.GetType(), "UniqueKey", str, true);
        }

        // A function that will shows the Confirm Box on the page
        public void ConfirmBox(string Message, Page refP)
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
            string script = "window.confirm('" + Message + "')";
            ScriptManager.RegisterClientScriptBlock(refP, refP.GetType(), "UniqueKey", script, true);
        }

        public string ExecuteScalar(string query, string ConnectionString)
        {
            string str = null;
            OleDbConnection con = new OleDbConnection(ConnectionString);
            con.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 320;
            cmd.Connection = con;

            str = cmd.ExecuteScalar().ToString();

            con.Close();
            con.Dispose();
            cmd.Dispose();

            return str;
        }

        // A function that will bind the data directly to the DropDownList and return it
        public DropDownList BindDropDownList(string query, string DataTextField, string DataValueField, string ConnectionString, string SelectValue)
        {
            DataSet ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter(query, ConnectionString);
            da.Fill(ds);
            DropDownList DDL = new DropDownList();
            DDL.DataTextField = DataTextField;
            DDL.DataValueField = DataValueField;
            DDL.DataSource = ds.Tables[0];
            DDL.DataBind();
            DDL.Items.Add(SelectValue);
            DDL.SelectedIndex = DDL.Items.Count - 1;
            return DDL;
        }

        // A function that will bind the data directly to the DropDownList
        public void BindDropDownList(string query, string DataTextField, string DataValueField, string ConnectionString, DropDownList DropDownList, string SelectValue)
        {
            DataSet ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter(query, ConnectionString);
            da.Fill(ds);
            DropDownList.DataTextField = DataTextField;
            DropDownList.DataValueField = DataValueField;
            DropDownList.DataSource = ds.Tables[0];
            DropDownList.DataBind();
            DropDownList.Items.Add(SelectValue);
            DropDownList.SelectedIndex = DropDownList.Items.Count - 1;
        }

        // A function that will bind the data directly to the Listbox and return it
        public ListBox BindListBox(string query, string DataTextField, string DataValueField, string ConnectionString)
        {
            DataSet ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter(query, ConnectionString);
            da.Fill(ds);
            ListBox LB = new ListBox();
            LB.DataTextField = DataTextField;
            LB.DataValueField = DataValueField;
            LB.DataSource = ds.Tables[0];
            LB.DataBind();
            return LB;
        }

        // A function that will bind the data directly to the ListBox
        public void BindListBox(string query, string DataTextField, string DataValueField, string ConnectionString, ListBox ListBox)
        {
            DataSet ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter(query, ConnectionString);
            da.Fill(ds);
            ListBox.DataTextField = DataTextField;
            ListBox.DataValueField = DataValueField;
            ListBox.DataSource = ds.Tables[0];
            ListBox.DataBind();
        }

        // A function that will bind the data directly to the RadioButtonList and return it
        public RadioButtonList BindRadioButtonList(string query, string DataTextField, string DataValueField, string ConnectionString)
        {
            DataSet ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter(query, ConnectionString);
            da.Fill(ds);
            RadioButtonList RBL = new RadioButtonList();
            RBL.DataTextField = DataTextField;
            RBL.DataValueField = DataValueField;
            RBL.DataSource = ds.Tables[0];
            RBL.DataBind();
            return RBL;
        }

        // A function that will bind the data directly to the RadioButtonList
        public void BindRadioButtonList(string query, string DataTextField, string DataValueField, string ConnectionString, RadioButtonList RadioButtonList)
        {
            DataSet ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter(query, ConnectionString);
            da.Fill(ds);
            RadioButtonList.DataTextField = DataTextField;
            RadioButtonList.DataValueField = DataValueField;
            RadioButtonList.DataSource = ds.Tables[0];
            RadioButtonList.DataBind();
        }

        // A function that will bind the data directly to the CheckBoxList and return it
        public CheckBoxList BindCheckBoxList(string query, string DataTextField, string DataValueField, string ConnectionString)
        {
            DataSet ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter(query, ConnectionString);
            da.Fill(ds);
            CheckBoxList CBL = new CheckBoxList();
            CBL.DataTextField = DataTextField;
            CBL.DataValueField = DataValueField;
            CBL.DataSource = ds.Tables[0];
            CBL.DataBind();
            return CBL;
        }

        // A function that will bind the data directly to the CheckBoxList
        public void BindCheckBoxList(string query, string DataTextField, string DataValueField, string ConnectionString, CheckBoxList CheckBoxList)
        {
            DataSet ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter(query, ConnectionString);
            da.Fill(ds);
            CheckBoxList.DataTextField = DataTextField;
            CheckBoxList.DataValueField = DataValueField;
            CheckBoxList.DataSource = ds.Tables[0];
            CheckBoxList.DataBind();
        }

        // A function that will bind the data directly to the GridView
        public GridView BindGridView(string query, string ConnectionString)
        {
            DataSet ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter(query, ConnectionString);
            da.Fill(ds);
            GridView GV = new GridView();
            GV.DataSource = ds.Tables[0];
            GV.DataBind();
            return GV;
        }

        // A function that will bind the data directly to the DataGrid
        public DataGrid BindDataGrid(string query, string ConnectionString)
        {
            DataSet ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter(query, ConnectionString);
            da.Fill(ds);
            DataGrid DG = new DataGrid();
            DG.DataSource = ds.Tables[0];
            DG.DataBind();
            return DG;
        }
    }
}