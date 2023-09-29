using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using Microsoft.VisualBasic;

/// <summary>
/// Summary description for InstitutionInformation
/// </summary>
/// 
namespace Library.App_Code.CSharp
{

    public class InstitutionInfo
    {
        OleDbConnection conn = new OleDbConnection(ConfigurationManager.ConnectionStrings[(String)System.Web.HttpContext.Current.Session["LibWiseDBConn"]].ConnectionString);

        public InstitutionInfo()
        { }

        public string selectname(string name)
        {
            OleDbCommand cmdselectname = new OleDbCommand();
            try
            {
                cmdselectname.Connection = conn;
                cmdselectname.CommandText = "select Name from InstitutionInformation where Name='" + name + "'";
                cmdselectname.Connection.Open();
                string name1 = cmdselectname.ExecuteScalar().ToString();
                cmdselectname.Connection.Close();
                return name1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int deletedata(string id, string Name, string SortName, string LblBranchCode, string Description, string Address, string City, string State, string Country, string PinCode, string ContactNo, string FaxNo, string EmailID, string WebSite, string AffiliatedBy, string ApprovedBy, string StartYear, string PrivGovr, string year, string UID, string entrydate, string flag, string isAuditRequired, OleDbCommand cmd)
        {
            //SqlCommand cmdselectdata = new SqlCommand();
            //OleDbCommand cmdselectdata = new OleDbCommand();
            try
            {
                //cmdselectdata.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "insert_InstitutionInformation_1";
                //cmdselectdata.CommandText = "delete from DefaultWorkingHour where Defaultworkinghourid=" + id + "";
                cmd.Parameters.Add("@id", OleDbType.Integer).Value = id;
                //IIf(docdt = String.Empty, DBNull.Value, docdt)
                cmd.Parameters.Add("@Name", OleDbType.VarWChar, 50).Value = Name;
                cmd.Parameters.Add("@SortName", OleDbType.VarWChar, 10).Value = SortName;
                cmd.Parameters.Add("@LblBranchCode", OleDbType.VarWChar, 10).Value = LblBranchCode;
                cmd.Parameters.Add("@Description", OleDbType.VarWChar, 50).Value = Description;

                cmd.Parameters.Add("@Address", OleDbType.VarWChar, 20).Value = Address;
                cmd.Parameters.Add("@City", OleDbType.VarWChar, 20).Value = City;
                cmd.Parameters.Add("@State", OleDbType.VarWChar, 20).Value = State;
                cmd.Parameters.Add("@Country", OleDbType.VarWChar, 20).Value = Country;
                cmd.Parameters.Add("@PinCode", OleDbType.Integer).Value = PinCode;
                cmd.Parameters.Add("@ContactNo", OleDbType.Integer).Value = ContactNo;
                cmd.Parameters.Add("@FaxNo", OleDbType.Integer).Value = FaxNo;
                cmd.Parameters.Add("@EmailID", OleDbType.VarWChar, 30).Value = EmailID;
                cmd.Parameters.Add("@WebSite", OleDbType.VarWChar, 20).Value = WebSite;
                cmd.Parameters.Add("@AffiliatedBy", OleDbType.VarWChar, 20).Value = AffiliatedBy;
                cmd.Parameters.Add("@ApprovedBy", OleDbType.VarWChar, 20).Value = ApprovedBy;
                cmd.Parameters.Add("@StartYear", OleDbType.Integer).Value = StartYear;
                cmd.Parameters.Add("@PrivGovr", OleDbType.VarWChar, 20).Value = PrivGovr;

                cmd.Parameters.Add("@year", OleDbType.VarWChar, 9).Value = year;
                cmd.Parameters.Add("@UID", OleDbType.VarWChar, 20).Value = UID;
                cmd.Parameters.Add("@entrydate", OleDbType.Date).Value = entrydate;
                cmd.Parameters.Add("@flag", OleDbType.VarWChar, 1).Value = flag;
                cmd.Parameters.Add("@isAuditRequired", OleDbType.VarWChar, 1).Value = isAuditRequired;
                //SqlDataAdapter sda = new SqlDataAdapter(cmdselectdata);
                int i = cmd.ExecuteNonQuery();
                return i;
                //OleDbDataAdapter sda = new OleDbDataAdapter(cmd);
                //DataSet ds = new DataSet();
                //sda.Fill(ds);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int updatedata(string id, string Name, string SortName, string LblBranchCode, string Description, string Address, string City, string State, string Country, string PinCode, string ContactNo, string FaxNo, string EmailID, string WebSite, string AffiliatedBy, string ApprovedBy, string StartYear, string PrivGovr, string year, string UID, string entrydate, string flag, string isAuditRequired, OleDbCommand cmd)
        {
            //SqlCommand cmd = new SqlCommand();
            //OleDbCommand cmd = new OleDbCommand();
            try
            {
                //cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "insert_InstitutionInformation_1";
                //cmdselectdata.CommandText = "update DefaultWorkingHour set Effective_Date='" + LblEffectivedate + "',Shift_name='" + SiftName + "',Shift_intime='" + Intime + "',Shift_outtime='" + ShiftOuttime + "',Break_intime='" + LblBreakIntime + "',Break_outtime='" + BreakOuttime + "' where Defaultworkinghourid=" + id + "";
                cmd.Parameters.Add("@id", OleDbType.Integer).Value = id;
                //IIf(docdt = String.Empty, DBNull.Value, docdt)
                cmd.Parameters.Add("@Name", OleDbType.VarWChar, 50).Value = Name;
                cmd.Parameters.Add("@SortName", OleDbType.VarWChar, 10).Value = SortName;
                cmd.Parameters.Add("@LblBranchCode", OleDbType.VarWChar, 10).Value = LblBranchCode;
                cmd.Parameters.Add("@Description", OleDbType.VarWChar, 50).Value = Description;

                cmd.Parameters.Add("@Address", OleDbType.VarWChar, 20).Value = Address;
                cmd.Parameters.Add("@City", OleDbType.VarWChar, 20).Value = City;
                cmd.Parameters.Add("@State", OleDbType.VarWChar, 20).Value = State;
                cmd.Parameters.Add("@Country", OleDbType.VarWChar, 20).Value = Country;
                cmd.Parameters.Add("@PinCode", OleDbType.Integer).Value = PinCode;
                cmd.Parameters.Add("@ContactNo", OleDbType.Integer).Value = ContactNo;
                cmd.Parameters.Add("@FaxNo", OleDbType.Integer).Value = FaxNo;
                cmd.Parameters.Add("@EmailID", OleDbType.VarWChar, 30).Value = EmailID;
                cmd.Parameters.Add("@WebSite", OleDbType.VarWChar, 20).Value = WebSite;
                cmd.Parameters.Add("@AffiliatedBy", OleDbType.VarWChar, 20).Value = AffiliatedBy;
                cmd.Parameters.Add("@ApprovedBy", OleDbType.VarWChar, 20).Value = ApprovedBy;
                cmd.Parameters.Add("@StartYear", OleDbType.Integer).Value = StartYear;
                cmd.Parameters.Add("@PrivGovr", OleDbType.VarWChar, 20).Value = PrivGovr;

                cmd.Parameters.Add("@year", OleDbType.VarWChar, 9).Value = year;
                cmd.Parameters.Add("@UID", OleDbType.VarWChar, 20).Value = UID;
                cmd.Parameters.Add("@entrydate", OleDbType.Date).Value = entrydate;
                cmd.Parameters.Add("@flag", OleDbType.VarWChar, 1).Value = flag;
                cmd.Parameters.Add("@isAuditRequired", OleDbType.VarWChar, 1).Value = isAuditRequired;
                //cmd.Connection.Open();
                int i = cmd.ExecuteNonQuery();
                //cmd.Connection.Close();
                return i;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet selectdata(string id)
        {
            //SqlCommand cmdselectdata = new SqlCommand();
            OleDbCommand cmdselectdata = new OleDbCommand();
            try
            {
                cmdselectdata.Connection = conn;
                cmdselectdata.CommandText = "select Name, SortName, BranchCode, Description, Address, City, State, Country, PinCode, ContactNo, FaxNo, EmailID, WebSiteURL, AffiliatedBy, ApprovedBy, StartYear, PrivateGovr from InstitutionInformation where Institution_ID=" + id + "";
                cmdselectdata.Parameters.Add("@id", OleDbType.Integer).Value = id;
                //SqlDataAdapter sda = new SqlDataAdapter(cmdselectdata);
                OleDbDataAdapter sda = new OleDbDataAdapter(cmdselectdata);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet fillGrid()
        {
            //SqlCommand cmdgridfill = new SqlCommand();
            OleDbCommand cmdgridfill = new OleDbCommand();
            try
            {
                cmdgridfill.Connection = conn;
                cmdgridfill.CommandText = "select Institution_ID,Name,SortName,BranchCode from InstitutionInformation";
                //SqlDataAdapter sda = new SqlDataAdapter(cmdgridfill);
                OleDbDataAdapter sda = new OleDbDataAdapter(cmdgridfill);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void gridfill1(GridView gv, string sql, OleDbCommand cmd)
        {

            try
            {
                OleDbCommand cmdgridfill = new OleDbCommand();
                //cmd.Connection = conn;
                //cmdgridfill.Connection.Open();
                cmdgridfill.Connection = conn;
                cmdgridfill.CommandText = sql;
                OleDbDataAdapter sda = new OleDbDataAdapter(cmdgridfill);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                gv.DataSource = ds;
                gv.DataBind();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int insertdata(int id, string Name, string SortName, string LblBranchCode, string Description, string Address, string City, string State, string Country, string PinCode, string ContactNo, string FaxNo, string EmailID, string WebSite, string AffiliatedBy, string ApprovedBy, string StartYear, string PrivGovr, string year, string UID, string entrydate, string flag, string isAuditRequired, OleDbCommand cmd)
        {
            //OleDbCommand cmd = new OleDbCommand();
            //cmd.Connection = conn;
            try
            {
                //cmd.CommandText = "insert into DefaultWorkingHour(Defaultworkinghourid,Effective_Date,Shift_name,Shift_intime,Shift_outtime,Break_intime,Break_outtime,session_year,UID) values(?,?,?,?,?,?,?,?,?)";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "insert_InstitutionInformation_1";
                cmd.Parameters.Add("@id", OleDbType.Integer).Value = id;
                //IIf(docdt = String.Empty, DBNull.Value, docdt)
                cmd.Parameters.Add("@Name", OleDbType.VarWChar, 50).Value = Name;
                cmd.Parameters.Add("@SortName", OleDbType.VarWChar, 10).Value = SortName;
                cmd.Parameters.Add("@LblBranchCode", OleDbType.VarWChar, 10).Value = LblBranchCode;
                cmd.Parameters.Add("@Description", OleDbType.VarWChar, 50).Value = Description;

                cmd.Parameters.Add("@Address", OleDbType.VarWChar, 20).Value = Address;
                cmd.Parameters.Add("@City", OleDbType.VarWChar, 20).Value = City;
                cmd.Parameters.Add("@State", OleDbType.VarWChar, 20).Value = State;
                cmd.Parameters.Add("@Country", OleDbType.VarWChar, 20).Value = Country;
                cmd.Parameters.Add("@PinCode", OleDbType.Integer).Value = PinCode;
                cmd.Parameters.Add("@ContactNo", OleDbType.Integer).Value = ContactNo;
                cmd.Parameters.Add("@FaxNo", OleDbType.Integer).Value = Convert.ToInt32(FaxNo);
                cmd.Parameters.Add("@EmailID", OleDbType.VarWChar, 30).Value = EmailID;
                cmd.Parameters.Add("@WebSite", OleDbType.VarWChar, 20).Value = WebSite;
                cmd.Parameters.Add("@AffiliatedBy", OleDbType.VarWChar, 20).Value = AffiliatedBy;
                cmd.Parameters.Add("@ApprovedBy", OleDbType.VarWChar, 20).Value = ApprovedBy;
                cmd.Parameters.Add("@StartYear", OleDbType.Integer).Value = StartYear;
                cmd.Parameters.Add("@PrivGovr", OleDbType.VarWChar, 20).Value = PrivGovr;

                cmd.Parameters.Add("@year", OleDbType.VarWChar, 9).Value = year;
                cmd.Parameters.Add("@UID", OleDbType.VarWChar, 20).Value = UID;
                cmd.Parameters.Add("@entrydate", OleDbType.Date).Value = entrydate;
                cmd.Parameters.Add("@flag", OleDbType.VarWChar, 1).Value = flag;
                cmd.Parameters.Add("@isAuditRequired", OleDbType.VarWChar, 1).Value = isAuditRequired;

                //OleDbDataAdapter sda = new OleDbDataAdapter(cmd);
                //DataSet ds = new DataSet();
                //sda.Fill(ds);


                //cmd.Parameters.Add("@UID", OleDbType.VarWChar, 20).Value = UID;
                //cmd.Parameters.Add("@year", OleDbType.VarWChar, 9).Value = year;
                //cmd.Connection.Open();
                int i = cmd.ExecuteNonQuery();
                //cmd.Connection.Close();
                return i;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int GenerateID()
        {
            //SqlCommand cmdid = new SqlCommand();
            OleDbCommand cmdid = new OleDbCommand();
            int strid;
            cmdid.Connection = conn;
            cmdid.CommandText = "select coalesce(max(Institution_ID),'0',max(Institution_ID)) from InstitutionInformation";
            cmdid.Connection.Open();
            strid = (int)cmdid.ExecuteScalar();
            cmdid.Connection.Close();
            if (strid.Equals(0))
            {
                strid = 1;
            }
            else
            {
                strid = strid + 1;
            }
            return strid;
        }
    }

}
