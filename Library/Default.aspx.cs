using Library.App_Code.CSharp;
using Library.Models;
using Microsoft.SqlServer.Management.Sdk.Differencing;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Library.App_Code.CSharp.BaseClass;

namespace Library
{
    public partial class _Default : Page
    {
        
         dbUtilities messg=new dbUtilities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //  Session["LibWiseDBConn"] = "OledbConnectionString";// ConfigurationManager.ConnectionStrings["OledbConnectionString"].ConnectionString;
                //  Session["UserId"] = 1125;
                // Session["user_id"] = "Admin";
                // Session["UserName"] = "Admin";
            }
        }

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            string qer = "select id,usertype,userid,password,memberid,SaltVc,status,status1,ValidUpTo from userdetails ";
            qer += " where userid='" + txUserId.Text.Trim() + "'";

            FillDsTables fd = new FillDsTables();
            DataTable dtu = new DataTable();
            fd.FillDs(qer, ref dtu);
            if (dtu.Rows.Count == 0)
            {
                labers.Text = "Login not found";
                return;
            }
            List<Library.Models.UserDetail> d = new List<Models.UserDetail>();
            var xd = ExtConvert.ConvertTo<Library.Models.UserDetail>(dtu);

            var pw = LoggedEncrDecr.decrypt(xd[0].Password, xd[0].SaltVc);
            if (pw == txPassw.Text)
            {
               var ipAdd = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                if (string.IsNullOrEmpty(ipAdd))
                {
                    ipAdd = Request.ServerVariables["REMOTE_ADDR"];
                }

                qer = "select academicsession,startdate,enddate from AcedemicSessionInformation,librarysetupinformation where librarysetupinformation.CurrentAcademicSession=AcedemicSessionInformation.academicsession";
                DataTable dtSes = new DataTable();
                fd.FillDs(qer, ref dtSes);
                if (dtSes.Rows.Count == 0)
                {
                    messg.PageMesg("Acad Sess Not set", this, dbUtilities.MsgLevel.Warning);
                    return;
                }
                qer = "select isAuditRequired from librarysetupinformation where institutename<> ''";
                DataTable dtAud = new DataTable();
                fd.FillDs(qer,ref dtAud);
                string auds =  "|IsAudit:N";
                if (dtAud.Rows.Count > 0)
                {
                    auds = "|IsAudit:" +( dtAud.Rows[0][0].ToString().ToUpper()=="Y"?"Y":"N");
                }

                string ud = "msspl|UserId:"+ xd[0].Id+"|UserType:"+ xd[0].userType + "|User_Id:" + xd[0].UserId.ToString() + auds+ "|UserName:" + xd[0].UserId + "|Session:" + dtSes.Rows[0]["academicsession"].ToString() + "|sessionStartDate:" + Convert.ToDateTime( dtSes.Rows[0]["startdate"]).ToString("dd-MMM-yyyy") + "|sessionEndDate:" + Convert.ToDateTime( dtSes.Rows[0]["enddate"]).ToString("dd-MMM-yyyy") + "|ipaddress:"+ipAdd+"|LoginTime:" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                SaveCookie(ud);
                Response.Redirect("HomePage.aspx");
            }
            else
            {
                labers.Text = "Incorrect login";
                return;
            }

        }
        private bool SaveCookie(string userData)
        {
            try
            {
                FormsAuthenticationTicket tkt;
                string cookiestr;
                HttpCookie ck;
                tkt = new FormsAuthenticationTicket(1, userData, DateTime.Now,
                DateTime.Now.AddDays(7), true, userData);
                cookiestr = FormsAuthentication.Encrypt(tkt);
                ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);
                //ck = new HttpCookie("MssplUser", cookiestr);
                ck.Path = FormsAuthentication.FormsCookiePath;
                Response.Cookies.Add(ck);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;

        }
    }
}