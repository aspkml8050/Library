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

/// <summary>
/// Summary description for mailOperation
/// </summary>
/// nam
/// 
namespace Library.App_Code.CSharp
{


public class mailOperation
{
    //OleDbConnection con = null;
    OleDbCommand com = null;
    OleDbDataReader dr = null;

    //const string SP_CHECKUSER = "CheckUser";
	public mailOperation()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public mailing CheckUser(string UserID,string chkmailAllow,OleDbConnection con,string selectcol1,string selectcol2,string tabName,string filtColmn)
    {
        string sqlStr="select " + selectcol1 + " as email1," + selectcol2 + " as email2 from " + tabName + " where " + filtColmn + " = '" + UserID + "';select email,smptp_IPadd,iuser ,ipwd,isEmailAllowed," + chkmailAllow + " as emAllow from librarysetupinformation";
        //string sqlStr="select email1, email2 from circusermanagement where userid = '" + UserID + "';select email,smptp_IPadd,iuser ,ipwd,isEmailAllowed," + chkmailAllow + " as emAllow from librarysetupinformation";
        com = new OleDbCommand(sqlStr, con);
        com.CommandType = CommandType.Text;
        //com.Parameters.Add("@UserId", DbType.String).Value = UserID; //email1, email2, circusermanagement, userid 
        dr = com.ExecuteReader();
        mailing _user = null;
        dr.Read();
        _user = new mailing();
        if (dr.HasRows)
        {              
            _user.MailT1 = dr["email1"].ToString();
            _user.MailT2 = dr["email2"].ToString();             
        }
        dr.NextResult();
        if(dr.Read()){
         _user.MailF = dr["email"].ToString();
         _user.SmtpAdd = dr["smptp_IPadd"].ToString();
         _user.Uid = dr["iuser"].ToString();
         _user.Pwd = dr["ipwd"].ToString();
         _user.IsmailAllowed = dr["isEmailAllowed"].ToString();
         _user.IsemAllow = dr["emAllow"].ToString();
        }
        dr.Close();
        com.Dispose();
        return _user;
    }

    //public mailing CheckUser1(string UserID,string chkmailAllow,OleDbCommand com)
    //{
    //    string sqlStr="select email1, email2 from circusermanagement where userid = '" + UserID + "';select email,smptp_IPadd,iuser ,ipwd,isEmailAllowed," + chkmailAllow + " as emAllow from librarysetupinformation";
    //    //com = new OleDbCommand(sqlStr, con);
    //    com.CommandText=sqlStr;
    //    com.CommandType = CommandType.Text;
    //    //com.Parameters.Add("@UserId", DbType.String).Value = UserID;
    //    dr = com.ExecuteReader();
    //    mailing _user = null;
    //    dr.Read();
    //    _user = new mailing();
    //    if (dr.HasRows)
    //    {              
    //        _user.MailT1 = dr["email1"].ToString();
    //        _user.MailT2 = dr["email2"].ToString();             
    //    }
    //    dr.NextResult();
    //    if(dr.Read()){
    //     _user.MailF = dr["email"].ToString();
    //     _user.SmtpAdd = dr["smptp_IPadd"].ToString();
    //     _user.Uid = dr["iuser"].ToString();
    //     _user.Pwd = dr["ipwd"].ToString();
    //     _user.IsmailAllowed = dr["isEmailAllowed"].ToString();
    //     _user.IsemAllow = dr["emAllow"].ToString();
    //    }
    //    dr.Close();
    //    return _user;
    //}


}
}
