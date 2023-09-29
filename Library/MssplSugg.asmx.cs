using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Configuration;
using System.Data.OleDb;
using System.Data;
using System.Web.Script.Services;
using System.Data.SqlClient;
using Mdatas;
using System.Web.Security;
using Library.App_Code.CSharp;
using static Library.App_Code.CSharp.BaseClass;
using System.Web.Script.Serialization;
using Microsoft.SqlServer.Management.Smo;

namespace Library
{
    /// <summary>
    /// Summary description for MssplSugg
    /// </summary>
    [ScriptService]
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class MssplSugg : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        public bool IsAllDigits(string s)
        {
            foreach (char c in s)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }
        public string reversStr(string s)
        {
            string r = "";
            for (int indD = s.Length - 1; indD >= 0; indD--)
                r += r[indD - 1];

            return r;
        }
        //Kamal Sir
        [WebMethod(EnableSession = true)]
        public string LogOpenPage(string hrefLink, string pgid, string Uid)
        {
            string Ret = "";
            string Qer;
            var logged = LoggedUser.Logged();
            if (string.IsNullOrEmpty(Uid))
                Uid = logged.User_Id;// HttpContext.Current.Session["user_id"].ToString();
            GlobClassTr gCl = new GlobClassTr();
            try
            {
                string pgidIds = "";
                gCl.TrOpen();
                if (!string.IsNullOrEmpty(hrefLink))
                {
                    string hrf = hrefLink.Substring(0, hrefLink.IndexOf(("?")));


                    Qer = "select * from Popup_new where href like '" + hrf + "%'";
                    DataTable dtId = gCl.DataT(Qer);
                    if (dtId.Rows.Count > 0)
                    {
                        pgidIds = dtId.Rows[0]["menu_id"].ToString();
                    }
                }
                else
                {
                    pgidIds = pgid;
                }

                try
                {
                    int u = Convert.ToInt32(Uid);
                }
                catch
                {
                    Qer = "select id from userdetails where userid='" + Uid + "'";
                    DataTable dtU = gCl.DataT(Qer);
                    if (dtU.Rows.Count == 0)
                        throw new ApplicationException("Log Pages - Invalid userid given " + Uid);
                    Uid = dtU.Rows[0][0].ToString();
                }
                DateTime t = DateTime.Now;
                List<SqlParameter> lsP = new List<SqlParameter>();
                SqlParameter p = new SqlParameter("Cur", t);
                p.SqlDbType = SqlDbType.DateTime;
                lsP.Add(p);
                string Id;
                Qer = "select isnull(max(vid),0)+1 from logvisitedpages ";
                Id = gCl.ExScaler(Qer).ToString();
                Qer = "insert into logvisitedpages (vid,href,pgid,userid,optime) values (";
                Qer += Id + ",'" + hrefLink + "','" + pgidIds + "','" + Uid + "',@Cur)";
                gCl.IUD(Qer, lsP.ToArray());
                gCl.TrClose();
            }
            catch (Exception Exp)
            {
                gCl.TrRollBack();
                Ret = Exp.Message + "; Log failed.";
            }
            if (Ret == "")
                Ret = "Ok";
            return Ret;
        }
        [WebMethod()]
        public  ReturnVals[] txtGrdLocation(string prefixText)
        {
            var RetArray = new List<ReturnVals>();
            var GetDDL = new FillDsTables();
            var dsLoc = new DataSet();
            string Errs;
            try
            {
                Errs = GetDDL.FillDs("select top 300 .dbo.locdecode2(id) loc, id from mapped_location where .dbo.locdecode2(id) like '%" + prefixText.Trim() + "%'", ref dsLoc, "Loc");
                if (!string.IsNullOrEmpty(Errs))
                {
                    throw new ApplicationException(Errs);
                }
                var loopTo = dsLoc.Tables[0].Rows.Count - 1;
                for (int ixd = 0; ixd <= loopTo; ixd++)
                    RetArray.Add(new ReturnVals { label = dsLoc.Tables[0].Rows[ixd][0].ToString(), value = dsLoc.Tables[0].Rows[ixd][0].ToString() + "," + dsLoc.Tables[0].Rows[ixd][1].ToString() });
                return RetArray.ToArray();
            }
            catch (Exception ex)
            {
                return RetArray.ToArray();
            }
        }
        [WebMethod()]
        public ReturnVals[] GetLocationJq(string prefixText)
        {
            List<ReturnVals> RetArray = new List<ReturnVals>();
            var GetDDL = new FillDsTables();
            var dsLoc = new DataSet();
            JavaScriptSerializer jser = new JavaScriptSerializer();

            string Errs;
            try
            {
                Errs = GetDDL.FillDs("select top 300 .dbo.locdecode2(id) loc, id from mapped_location where .dbo.locdecode2(id) like '%" + prefixText.Trim() + "%'", ref dsLoc, "Loc");
                if (!string.IsNullOrEmpty(Errs))
                {
                    throw new ApplicationException(Errs);
                }
                var loopTo = dsLoc.Tables[0].Rows.Count - 1;
                for (int ixd = 0; ixd <= loopTo; ixd++)
                    RetArray.Add(new ReturnVals { value= dsLoc.Tables[0].Rows[ixd][1].ToString(), label= dsLoc.Tables[0].Rows[ixd][0].ToString().Replace("-","_")+","+ dsLoc.Tables[0].Rows[ixd][1].ToString() } );
                var str=jser.Serialize( RetArray );
                return RetArray.ToArray();
            }
            catch (Exception ex)
            {
                RetArray.Add( new ReturnVals { value="-1", label=ex.Message});
                var str = jser.Serialize(RetArray);
                return RetArray.ToArray(); 
            }
        }
        [WebMethod()]
        public string[] GetSuggestionstop_Ajax(string prefixText, int count)
        {
            List<string> RetArray = new List<string>();
            GlobClassTr gClas = new GlobClassTr();
            var logged = LoggedUser.Logged();
            try
            {
                if (prefixText.Trim() != "")
                {

                string qer = " select distinct top 90 Menu_name,Href   from Popup_new a join menu_perm b on a.Menu_id=b.menuid and a.Href is not null ";
                qer += " join userdetails c on b.usertypeid=c.usertype where c.userid='" + logged.User_Id + "' ";
                qer += " and menu_name like '%" + prefixText.Trim().Replace("'", "''") + "%' ";
                qer += "  ";
                qer += "  ";
                gClas.TrOpen();
                DataTable dtFd = gClas.DataT(qer);

                    for (int indX = 0; indX < dtFd.Rows.Count; indX++)
                    RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dtFd.Rows[indX][0].ToString(), dtFd.Rows[indX][1].ToString()));
                gClas.TrClose();
                }
            }

            catch (Exception Exp)
            {
                gClas.TrRollBack();
                RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Error in Suggestion: " + Exp.Message, "0"));

            }
            return RetArray.ToArray();
        }
            [WebMethod()]
        public string GetAccBooksJq(string prefixText)
        {
            List<ReturnVals> RetArray = new List<ReturnVals>();
            var RetErr = new Exception();
            GlobClassTr gClas = new GlobClassTr();
            JavaScriptSerializer jser = new JavaScriptSerializer();
            try
            {
                prefixText = prefixText.Trim().Replace("'", "''");
                gClas.TrOpen();
                string Qer = "select top 300 a.accessionnumber,a.accessionnumber+'|'+a.booktitle+'|'+replace(b.firstname1+' '+b.middlename1+' '+b.lastname1,'  ',' ')+'|'+cast(copynumber as varchar) book ";
                Qer += " from bookaccessionmaster a join bookauthor b on a.ctrl_no=b.ctrl_no  ";
                Qer += "where a.accessionnumber like '%" + prefixText + "%' or ( a.booktitle+replace(b.firstname1+' '+b.middlename1+' '+b.lastname1,'  ',' ') like '%" + prefixText + "%' )  ";
                Qer += " or (replace(b.firstname1+' '+b.middlename1+' '+b.lastname1,'  ',' ') like '%" + prefixText + "%' ) ";
                Qer += " order by a.accessionnumber ";
                DataTable dtD = gClas.DataT(Qer); gClas.TrClose();
                for (int indX = 0; indX < dtD.Rows.Count; indX++)
                {
                    RetArray.Add(new ReturnVals { value = dtD.Rows[indX]["accessionnumber"].ToString(), label = dtD.Rows[indX]["book"].ToString() });
                }

                var d = jser.Serialize(RetArray);
                return d;
            }
            catch (Exception exx)
            {
                RetArray.Add(new ReturnVals { value = exx.Message, label = exx.Message });

                gClas.TrRollBack();
                var d = jser.Serialize(RetArray);
                return d;

            }
        }
        [WebMethod()]
        public string[] GetNonCatBooks(string prefixText, int count)
        {
            List<string> RetArray = new List<string>();
            var RetErr = new Exception();
            GlobClassTr gClas = new GlobClassTr();
            try
            {
                prefixText = prefixText.Trim().Replace("'", "''");
                gClas.TrOpen();
                string Qer = "select top 300 a.accessionnumber,a.accessionnumber+'|'+a.booktitle book ";
                Qer += " from bookaccessionmaster a ";
                Qer += "where ctrl_no=0 and  a.accessionnumber like '%" + prefixText + "%'  ";
                Qer += " order by a.accessionnumber ";
                DataTable dtD = gClas.DataT(Qer);
                for (int indX = 0; indX < dtD.Rows.Count; indX++)
                    RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dtD.Rows[indX][1].ToString(), dtD.Rows[indX][0].ToString()));



                gClas.TrClose();

            }
            catch (Exception exx)
            {
                gClas.TrRollBack();
                RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Error in Suggestion: " + exx.Message, "0"));
            }

            return RetArray.ToArray();
        }


        //Dim sql As String = "select top 200 accessionnumber, ctrl_no   from bookaccessionmaster  where ctrl_no <>0  and accessionnumber like N'%" + keyword.Trim() + "%' order by accessionnumber"
        [WebMethod()]
        public string[] GetAccnCtrl(string prefixText, int count)
        {
            List<string> RetArray = new List<string>();
            var RetErr = new Exception();
            GlobClassTr gClas = new GlobClassTr();
            try
            {
                prefixText = prefixText.Trim().Replace("'", "''");
                gClas.TrOpen();
                string Qer = "select top 200 accessionnumber, ctrl_no from bookaccessionmaster where ctrl_no <> 0  and accessionnumber like N'%" + prefixText.Trim() + "%' order by accessionnumber";
                DataTable dtD = gClas.DataT(Qer);
                for (int indX = 0; indX < dtD.Rows.Count; indX++)
                    RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dtD.Rows[indX][0].ToString(), dtD.Rows[indX][1].ToString()));


                gClas.TrClose();

            }
            catch (Exception exx)
            {
                gClas.TrRollBack();
                RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Error in Suggestion: " + exx.Message, "0"));
            }

            return RetArray.ToArray();
        }
        [WebMethod()]
        public string[] GetAccNo(string prefixText)
        {
            // Dim _RetArray() As String = Nothing '  
            var RetArray = new List<string>();    // Notice: list of string is used where .add can be used to add string
            if (prefixText.Trim().Length < 2)
                return RetArray.ToArray();
            var GetDDL = new FillDsTables();
            try
            {

                var dsLoc = new DataSet();
                // Dim Err As String = GetDDL.FillDs("select top 200 cast(accessionnumber as varchar) accessionnumber, booktitle from bookaccessionmaster where ctrl_no<>0 and cast(accessionnumber as varchar) like '%" & prefixText.Trim() & "%' order by cast(accessionnumber as varchar)", dsLoc, "AccNo")
                string Err = GetDDL.FillDs("select top 100 accessionnumber , booktitle,ctrl_no from bookaccessionmaster where ctrl_no<>0 and accessionnumber like '%" + prefixText.Trim() + "%' order by accessionnumber ", ref dsLoc, "AccNo");
                if (!string.IsNullOrEmpty(Err))
                {
                    RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Error in Suggestion: " + Err, 0.ToString()));
                    return RetArray.ToArray();
                }
                for (int ii = 0, loopTo = dsLoc.Tables[0].Rows.Count - 1; ii <= loopTo; ii++)
                    RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dsLoc.Tables[0].Rows[ii][0].ToString()+ "|"+ dsLoc.Tables[0].Rows[ii][1].ToString(), dsLoc.Tables[0].Rows[ii][0]+ "|"+dsLoc.Tables[0].Rows[ii][2].ToString()));
                return RetArray.ToArray();
            }
            catch (Exception ex)
            {
                RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Error in Suggestion", 0.ToString()));
                return RetArray.ToArray();
            }
        }
        [WebMethod()]
        public ReturnVals[] GetAccNoJq(string prefixText)
        {
            List<ReturnVals> retArray = new List<ReturnVals>();
            if (prefixText.Trim().Length < 2)
                return retArray.ToArray();
            var GetDDL = new FillDsTables();
            try
            {

                var dsLoc = new DataSet();
                // Dim Err As String = GetDDL.FillDs("select top 200 cast(accessionnumber as varchar) accessionnumber, booktitle from bookaccessionmaster where ctrl_no<>0 and cast(accessionnumber as varchar) like '%" & prefixText.Trim() & "%' order by cast(accessionnumber as varchar)", dsLoc, "AccNo")
                string Err = GetDDL.FillDs("select top 100 accessionnumber , booktitle,ctrl_no from bookaccessionmaster where ctrl_no<>0 and accessionnumber like '%" + prefixText.Trim() + "%' order by accessionnumber ", ref dsLoc, "AccNo");
                if (!string.IsNullOrEmpty(Err))
                {
                    retArray.Add(new ReturnVals { label = "Error in Suggestion: " + Err, value="0" });
                    return retArray.ToArray();
                }
                for (int ii = 0, loopTo = dsLoc.Tables[0].Rows.Count - 1; ii <= loopTo; ii++)
                    retArray.Add(new ReturnVals { label  = dsLoc.Tables[0].Rows[ii][0].ToString() + "|" + dsLoc.Tables[0].Rows[ii][1].ToString(), value = dsLoc.Tables[0].Rows[ii][0].ToString() + "|" + dsLoc.Tables[0].Rows[ii][2].ToString() });
                return retArray.ToArray();
            }
            catch (Exception ex)
            {
                retArray.Add(new ReturnVals { label = "Error in Suggestion: " + ex.Message, value = "0" });
                return retArray.ToArray();
            }
        }
        [WebMethod()]
        public ReturnVals[] GetTitleJq(string prefixText)
        {
            List<ReturnVals> retArray = new List<ReturnVals>();
            if (prefixText.Trim().Length < 3)
                return retArray.ToArray();
            var GetDDL = new FillDsTables();
            try
            {

                var dsLoc = new DataSet();
                // Dim Err As String = GetDDL.FillDs("select top 200 cast(accessionnumber as varchar) accessionnumber, booktitle from bookaccessionmaster where ctrl_no<>0 and cast(accessionnumber as varchar) like '%" & prefixText.Trim() & "%' order by cast(accessionnumber as varchar)", dsLoc, "AccNo")
                string Err = GetDDL.FillDs("select top 100 accessionnumber ,accessionnumber+'|'+ booktitle+'|'+c.firstname1+' '+c.middlename1+' '+c.lastname1 title from bookaccessionmaster a join bookcatalog b on a.ctrl_no=b.ctrl_no join bookauthor c on a.ctrl_no=c.ctrl_no where b.title like N'%" + prefixText.Trim() + "%' order by accessionnumber ", ref dsLoc, "title");
                if (!string.IsNullOrEmpty(Err))
                {
                    retArray.Add(new ReturnVals { label = "Error in Suggestion: " + Err, value = "0" });
                    return retArray.ToArray();
                }
                for (int ii = 0, loopTo = dsLoc.Tables[0].Rows.Count - 1; ii <= loopTo; ii++)
                    retArray.Add(new ReturnVals { label = dsLoc.Tables[0].Rows[ii][1].ToString() , value = dsLoc.Tables[0].Rows[ii][0].ToString()});
                return retArray.ToArray();
            }
            catch (Exception ex)
            {
                retArray.Add(new ReturnVals { label = "Error in Suggestion: " + ex.Message, value = "0" });
                return retArray.ToArray();
            }
        }
        [WebMethod()]
        public ReturnVals[] GetCopyNoJq(string prefixText)
        {
            List<ReturnVals> retArray = new List<ReturnVals>();
            if (prefixText.Trim().Length < 2)
                return retArray.ToArray();
            string SAccNo = HttpContext.Current.Session["AccNo"].ToString();
            int? Ctrl_no;
            string sError = "";
            var GetDDL = new FillDsTables();
            try
            {
                var dsLoc = new DataSet();
                Ctrl_no = Convert.ToInt32(HttpContext.Current.Session["ctrl_no"]);
                if (Ctrl_no == null)
                {
                    sError = GetDDL.FillDs("select ctrl_no from bookaccessionmaster where accessionnumber='" + SAccNo + "'", ref dsLoc, "AccNo");
                }
                if (sError != "")
                {
                    retArray.Add(new ReturnVals { label = "Error in Suggestion: " + sError, value = "0" });
                    return retArray.ToArray();
                }
                if (Ctrl_no > 0)
                {
                    sError = GetDDL.FillDs("select cast(copynumber as varchar),accessionnumber copynumber from bookaccessionmaster where ctrl_no=" + Ctrl_no.ToString() + " and ctrl_no<>0 order by cast(copynumber as integer) ", ref dsLoc, "CopyNo");
                }
                else
                {
                    sError = GetDDL.FillDs("select cast(copynumber as varchar) copynumber,accessionnumber from bookaccessionmaster where ctrl_no="+ dsLoc.Tables[0].Rows[0][0].ToString()+ " and ctrl_no<>0 order by cast(copynumber as integer) ", ref dsLoc, "CopyNo");
                }
                if (sError != "")
                {
                    retArray.Add(new ReturnVals { label = "Error in Suggestion: " + sError, value = "0" });
                    return retArray.ToArray();
                }

                // Dim Err As String = GetDDL.FillDs("select top 200 cast(accessionnumber as varchar) accessionnumber, booktitle from bookaccessionmaster where ctrl_no<>0 and cast(accessionnumber as varchar) like '%" & prefixText.Trim() & "%' order by cast(accessionnumber as varchar)", dsLoc, "AccNo")
                string Err = GetDDL.FillDs("select top 100 accessionnumber , booktitle,ctrl_no from bookaccessionmaster where ctrl_no<>0 and accessionnumber like '%" + prefixText.Trim() + "%' order by accessionnumber ", ref dsLoc, "AccNo");
                if (!string.IsNullOrEmpty(Err))
                {
                    retArray.Add(new ReturnVals { label = "Error in Suggestion: " + Err, value = "0" });
                    return retArray.ToArray();
                }
                for (int ii = 0, loopTo = dsLoc.Tables[0].Rows.Count - 1; ii <= loopTo; ii++)
                    retArray.Add(new ReturnVals { label = dsLoc.Tables[0].Rows[ii][0].ToString() + "|" + dsLoc.Tables[0].Rows[ii][1].ToString(), value = dsLoc.Tables[0].Rows[ii][0].ToString() + "|" + dsLoc.Tables[0].Rows[ii][2].ToString() });
                return retArray.ToArray();
            }
            catch (Exception ex)
            {
                retArray.Add(new ReturnVals { label = "Error in Suggestion: " + ex.Message, value = "0" });
                return retArray.ToArray();
            }
        }
        [WebMethod()]
        public string[] GetTitleCtrl(string prefixText, int count)
        {
            List<string> RetArray = new List<string>();
            var RetErr = new Exception();
            GlobClassTr gClas = new GlobClassTr();
            try
            {
                prefixText = prefixText.Trim().Replace("'", "''");
                gClas.TrOpen();
                //string Qer = "select top 300 booktitle, ctrl_no from bookaccessionmaster where ctrl_no <> 0  and booktitle like N'%" + prefixText.Trim().Replace("'", "''") + "%' order by booktitle";
                string Qer = "select distinct  top 200 dbo.GET_SUBTITLE(title, Subtitle, Paralleltype) + dbo.GET_TITLE('', Volume, part, edition) AS title, cast( bookcatalog.ctrl_no as nvarchar) ctrl_no, '' as A from bookcatalog inner join bookconference on bookcatalog.ctrl_no = bookconference.ctrl_no where (title LIKE N'" + prefixText.Trim().Replace("'", "''") + "%') union all select distinct  top 200 dbo.GET_SUBTITLE(title,Subtitle,Paralleltype) + dbo.GET_TITLE('',Volume,part,edition) AS title, cast( bookcatalog.ctrl_no as nvarchar) ctrl_no, dbo.GET_SUBTITLE(title,Subtitle,Paralleltype) + dbo.GET_TITLE('',Volume,part,edition) as A from bookcatalog inner join bookconference on bookcatalog.ctrl_no=bookconference.ctrl_no where title LIKE N'%" + prefixText.Trim().Replace("'", "''") + "%' and title not like N'" + prefixText.Trim().Replace("'", "''") + "%' order by A,title";
                DataTable dtD = gClas.DataT(Qer);
                for (int indX = 0; indX < dtD.Rows.Count; indX++)
                    RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dtD.Rows[indX][0].ToString(), dtD.Rows[indX][1].ToString()));

                gClas.TrClose();

            }
            catch (Exception exx)
            {
                gClas.TrRollBack();
                RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Error in Suggestion: " + exx.Message, "0"));
            }

            return RetArray.ToArray();
        }


        [WebMethod()]
        public string[] GetVendor(string prefixText, int count)
        {
            List<string> RetArray = new List<string>();
            var RetErr = new Exception();
            GlobClassTr gClas = new GlobClassTr();
            try
            {
                prefixText = prefixText.Trim().Replace("'", "''");
                gClas.TrOpen();
                string Qer = "Select distinct top 200 VendorName+', '+percity as VendorName,Vendorid from  VendorMaster join addresstable on  vendormaster.vendorcode=addresstable.addid and addrelation='vendor' and (vendorName+', '+percity) like N'%" + prefixText + "%' order by vendorname";
                DataTable dtD = gClas.DataT(Qer);
                for (int indX = 0; indX < dtD.Rows.Count; indX++)
                    RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dtD.Rows[indX][0].ToString(), dtD.Rows[indX][1].ToString()));


                gClas.TrClose();

            }
            catch (Exception exx)
            {
                gClas.TrRollBack();
                RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Error in Suggestion: " + exx.Message, "0"));
            }

            return RetArray.ToArray();
        }

        [WebMethod()]
        public string[] GetSubject(string prefixText, int count)
        {
            List<string> RetArray = new List<string>();
            var RetErr = new Exception();
            GlobClassTr gClas = new GlobClassTr();
            try
            {
                prefixText = prefixText.Trim().Replace("'", "''");
                gClas.TrOpen();
                string Qer = "select top 200 subject, subject_id from subject_master where subject like '%" + prefixText + "%' ";
                DataTable dtD = gClas.DataT(Qer);
                for (int indX = 0; indX < dtD.Rows.Count; indX++)
                    RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dtD.Rows[indX][0].ToString(), dtD.Rows[indX][1].ToString()));
                gClas.TrClose();
            }
            catch (Exception exx)
            {
                gClas.TrRollBack();
                RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Error in Suggestion: " + exx.Message, "0"));
            }
            return RetArray.ToArray();
        }
        [WebMethod()]
        public ReturnVals[] GetSubjectJSugg(string prefixText)
        {
            List<ReturnVals> RetArray = new List<ReturnVals>();
            var RetErr = new Exception();
            GlobClassTr gClas = new GlobClassTr();
            try
            {
                prefixText = prefixText.Trim().Replace("'", "''");
                gClas.TrOpen();
                string Qer = "select top 200 subject, subject_id from subject_master where subject like '%" + prefixText + "%' ";
                DataTable dtD = gClas.DataT(Qer);
                for (int indX = 0; indX < dtD.Rows.Count; indX++)
                    RetArray.Add(new ReturnVals { label = dtD.Rows[indX][0].ToString(), value= dtD.Rows[indX][1].ToString() } );
                gClas.TrClose();
            }
            catch (Exception exx)
            {
                gClas.TrRollBack();
                RetArray.Add(new ReturnVals { label = exx.Message, value = "" });
            }
            return RetArray.ToArray();
        }

        [WebMethod()]
        public string[] GetPubl(string prefixText, int count)
        {
            List<string> RetArray = new List<string>();
            var RetErr = new Exception();
            GlobClassTr gClas = new GlobClassTr();
            try
            {
                prefixText = prefixText.Trim().Replace("'", "''");
                gClas.TrOpen();
                string Qer = "Select  top 200 firstname+', '+percity as firstname,publisherid from  publishermaster join addresstable on  publishermaster.publisherid=addresstable.addid and addrelation='publisher' and (firstname+', '+percity) LIKE N'%" + prefixText + "%' order by firstname";
                DataTable dtD = gClas.DataT(Qer);
                for ( int indX = 0; indX < dtD.Rows.Count; indX++)
                    RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dtD.Rows[indX][0].ToString(), dtD.Rows[indX][1].ToString()));
                gClas.TrClose();
            }
            catch (Exception exx)
            {
                gClas.TrRollBack();
                RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Error in Suggestion: " + exx.Message, "0"));
            }

            return RetArray.ToArray();
        }
        [WebMethod()]
        public ReturnVals[] GetPublJsugg(string prefixText)
        {
            List<ReturnVals> RetArray = new List<ReturnVals>();
            var RetErr = new Exception();
            GlobClassTr gClas = new GlobClassTr();
            try
            {
                prefixText = prefixText.Trim().Replace("'", "''");
                gClas.TrOpen();
                string Qer = "Select  top 200 firstname+', '+percity as firstname,publisherid from  publishermaster join addresstable on  publishermaster.publisherid=addresstable.addid and addrelation='publisher' and (firstname+', '+percity) LIKE N'%" + prefixText + "%' order by firstname";
                DataTable dtD = gClas.DataT(Qer);
                for (int indX = 0; indX < dtD.Rows.Count; indX++)
                    RetArray.Add(new ReturnVals { label = dtD.Rows[indX][0].ToString(), value = dtD.Rows[indX][1].ToString() });
                gClas.TrClose();
            }
            catch (Exception exx)
            {
                gClas.TrRollBack();
            }

            return RetArray.ToArray();
        }
        [WebMethod()]
        public  ReturnVals[] GetLocation2Jq(string prefixText)
        {
            var RetArray = new List<ReturnVals>();
            var GetDDL = new FillDsTables();
            var dsLoc = new DataSet();
            string Err;
            try
            {
                string sqer = "select top 200  dbo.getlocation_String(location_path) loc,id from mapped_location where dbo.getlocation_String(location_path) like '%" + prefixText.Trim() + "%'";

                sqer = "select top 200  dbo.LocDecode2(id) loc,id from mapped_location where dbo.LocDecode2(id) like '%" + prefixText.Trim() + "%'  ";

                // templocation is refreshed by a button at top "refresh location"
                sqer = "select top 300    location, locaid from templocation where location like '%" + prefixText.Trim() + "%'  ";

                Err = GetDDL.FillDs(sqer, ref dsLoc, "Loc");
                if (!string.IsNullOrEmpty(Err))
                {
                    throw new ApplicationException(Err);
                }
                var FinalDt = new DataTable();
                for (int ii = 0, loopTo = dsLoc.Tables["Loc"].Rows.Count - 1; ii <= loopTo; ii++)
                    RetArray.Add(new ReturnVals { label = dsLoc.Tables["Loc"].Rows[ii][0].ToString(), value = dsLoc.Tables["Loc"].Rows[ii][0].ToString() + "," + dsLoc.Tables["Loc"].Rows[ii][1].ToString() });
                return RetArray.ToArray();
            }
            catch (Exception ex)
            {
                RetArray.Add(new ReturnVals { label = ex.Message, value = "0" });
                return RetArray.ToArray();
            }
        }
        [WebMethod()]
        [System.Web.Script.Services.ScriptMethod()]
        public  ReturnVals[] GetVendorJSugg(string prefixText)
        {
            // Dim _RetArray() As String = Nothing '  
            var RetArray = new List<ReturnVals>();    
            var GetDDL = new FillDsTables();
            try
            {

                var dsVend = new DataSet();
                prefixText = prefixText.Trim();
                string Err;
                if (prefixText.Contains(","))
                {
                    if (prefixText.Contains(", "))
                    {
                        Err = GetDDL.FillDs("select vendorid,vendorname,percity  from vendormaster a,addresstable b where a.vendorcode=b.addid and addrelation='vendor' and vendorname+', '+percity like N'%" + prefixText.Replace("'", "''") + "%' order by vendorname", ref dsVend, "Vend");
                    }
                    else
                    {
                        Err = GetDDL.FillDs("select vendorid,vendorname,percity  from vendormaster a,addresstable b where a.vendorcode=b.addid and addrelation='vendor' and vendorname+','+percity like N'%" + prefixText.Replace("'", "''") + "%' order by vendorname", ref dsVend, "Vend");
                    }
                }
                else
                {
                    Err = GetDDL.FillDs("select vendorid,vendorname,percity  from vendormaster a,addresstable b where a.vendorcode=b.addid and addrelation='vendor' and vendorname+percity like N'%" + prefixText.Replace("'", "''") + "%' order by vendorname", ref dsVend, "Vend");
                }
                if (!string.IsNullOrEmpty(Err))
                {
                    return RetArray.ToArray();
                }
                //dsVend.Tables(0).Rows(ii)(1) & ", " & dsVend.Tables(0).Rows(ii)(2), dsVend.Tables(0).Rows(ii)(0))
                for (int ii = 0, loopTo = dsVend.Tables[0].Rows.Count - 1; ii <= loopTo; ii++)
                    RetArray.Add(new ReturnVals { label = dsVend.Tables[0].Rows[ii][1].ToString() + ", " + dsVend.Tables[0].Rows[ii][2].ToString(), value= dsVend.Tables[0].Rows[ii][0].ToString() });
                return RetArray.ToArray();
            }
            catch (Exception ex)
            {
                return RetArray.ToArray();
            }
        }

        [WebMethod()]
        public string[] MGetBooks(string prefixText, int count)
        {
            // Dim _RetArray() As String = Nothing '  
            List<string> RetArray = new List<string>();
            var RetErr = new Exception();
            int rblDBOption = 1;

            string ConStringMSSPL = ConfigurationManager.ConnectionStrings["MSSPLConnectionString"].ConnectionString;

            OleDbConnection ConnektionMSSPL = new System.Data.OleDb.OleDbConnection(ConStringMSSPL);

            OleDbCommand Comm;
            try
            {
                //            ConnektionMSSPL.Open();
                prefixText = prefixText.Trim();
                DataSet ds = new DataSet();
                DataSet DSK = new DataSet();
                string SQry;
                SQry = "select  distinct top 250 cast(0 as varchar) Origin,   title ,author,  bookid,  " + "cast(pages as varchar) pages, volume, ";
                SQry += " part,edition,classno, bookno, isbn, issn, lang, subjects, price,currency, publisher, pubcity, editionyr, ";
                SQry += " pubyear,0 copynumber " + " FROM bookcollection " + " where  title+author Like '%" + prefixText + "%'" + " and ltrim(rtrim(title))<>''"; // order by title asc,author asc,copynumber asc,Origin asc,accessionnumber  asc"
                try
                {
                    ConnektionMSSPL.Open();
                    Comm = new OleDbCommand(SQry);
                    Comm.Connection = ConnektionMSSPL; // ConnektionMSSPL
                    Comm.CommandType = CommandType.Text;
                    OleDbDataAdapter da = new OleDbDataAdapter(SQry, ConnektionMSSPL);
                    da.Fill(DSK, "BooksMSSPL");
                    if (rblDBOption == 1)
                    {
                    }
                }
                catch (Exception ex)
                {
                }
                ConnektionMSSPL.Close();


                foreach (DataRow Dr in DSK.Tables[0].Rows)
                    RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(Dr["title"].ToString() + "|" + Dr["author"].ToString() + "|" + Dr["pages"].ToString() + "|" + Dr["volume"].ToString() + "|" + Dr["part"].ToString() + "|" + Dr["edition"].ToString() + "|" + Dr["classno"].ToString() + "|" + Dr["bookno"].ToString() + "|" + Dr["isbn"].ToString() + "|" + Dr["issn"].ToString() + "|" + Dr["lang"].ToString() + "|" + Dr["subjects"].ToString() + "|" + Dr["price"].ToString() + "|" + Dr["currency"].ToString() + "|" + Dr["publisher"].ToString() + "|" + Dr["pubcity"].ToString() + "|" + Dr["editionyr"].ToString() + "|" + Dr["pubyear"].ToString() + "#", Dr["bookid"].ToString()));

                if (RetArray.Count > 0)
                    return RetArray.ToArray();
                else
                    return null;
            }

            catch (Exception ex)
            {
                ConnektionMSSPL.Close();
                RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Error in Suggestion:" + ex.Message, ex.Message));
                return RetArray.ToArray();
            }
        }

        [WebMethod]
        public string SaveHeader(string Value)
        {
            // Dim _RetArray() As String = Nothing '  
            List<string> RetArray = new List<string>();    // Notice: list of string is used where .add can be used to add string
            GlobClassTr gCLas = new GlobClassTr();
            try
            {
                gCLas.TrOpen();
                string Qer = "delete from HeaderMissed";
                gCLas.IUD(Qer);
                string Nows = DateTime.Now.ToLongTimeString();
                SqlParameter P1 = new SqlParameter("Descr", "Only Single  record is maintained when home page is called at " + Nows);
                P1.SqlDbType = System.Data.SqlDbType.NVarChar;
                SqlParameter P2 = new SqlParameter("Data", Value);
                P2.SqlDbType = System.Data.SqlDbType.NVarChar;
                List<SqlParameter> ls = new List<SqlParameter>();
                ls.Add(P1);
                ls.Add(P2);
                Qer = "insert into HeaderMissed values (@Descr,@Data) ";
                gCLas.IUD(Qer, ls.ToArray());

                gCLas.TrClose();
                return "success";
            }
            catch (Exception ex)
            {
                gCLas.TrRollBack();
                return ex.Message;
            }
        }
        [WebMethod]
        public string GetHeader(string Value)
        {
            GlobClassTr gCLas = new GlobClassTr();
            try
            {
                string HeaderS = "";
                gCLas.TrOpen();
                string Qer = "select top 1 htmlheader from HeaderMissed ";
                DataTable dt = gCLas.DataT(Qer);
                if (dt.Rows.Count > 0)
                {
                    HeaderS = dt.Rows[0][0].ToString();
                }
                gCLas.TrClose();
                return HeaderS;
            }
            catch (Exception ex)
            {
                gCLas.TrRollBack();
                return "<div >" + ex.Message + "</div>";
            }
        }
        [WebMethod()]
        public string[] GetIndent(string prefixText, int count)
        {
            List<string> RetArray = new List<string>();
            var RetErr = new Exception();
            GlobClassTr gClas = new GlobClassTr();
            try
            {
                prefixText = prefixText.Trim().Replace("'", "''");
                gClas.TrOpen();
                string Qer = "select '('+ indentnumber+')'+title,indentid from indentmaster  where indentnumber like '%" + prefixText.Trim().Replace("'", "''") + "%' or title like '%" + prefixText.Trim().Replace("'", "''") + "%' order by title";
                DataTable dtD = gClas.DataT(Qer);
                for (int indX = 0; indX < dtD.Rows.Count; indX++)
                    RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dtD.Rows[indX][0].ToString(), dtD.Rows[indX][1].ToString()));


                gClas.TrClose();

            }
            catch (Exception exx)
            {
                gClas.TrRollBack();
                RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Error in Suggestion: " + exx.Message, "0"));
            }

            return RetArray.ToArray();
        }
        //Sachin 24Feb2020
        [WebMethod()]
        public string[] GetMemberID(string prefixText, int count)
        {
            List<string> RetArray = new List<string>();
            var RetErr = new Exception();
            GlobClassTr gClas = new GlobClassTr();
            try
            {
                prefixText = prefixText.Trim().Replace("'", "''");
                gClas.TrOpen();
                string Qer = "select top 200 userid, usercode from circusermanagement  where userid like '%" + prefixText + "%' or usercode like '%" + prefixText + "%'";
                DataTable dtD = gClas.DataT(Qer);
                for (int indX = 0; indX < dtD.Rows.Count; indX++)
                    RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dtD.Rows[indX][0].ToString(), dtD.Rows[indX][1].ToString()));


                gClas.TrClose();

            }
            catch (Exception exx)
            {
                gClas.TrRollBack();
                RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Error in Suggestion: " + exx.Message, "0"));
            }

            return RetArray.ToArray();
        }
        public class ReturnVals
        {
            public string value { get; set; }
            public string label { get; set; }
        }
        [WebMethod()]
        public string GetMemberIDJq(string prefixText)
        {
            List<ReturnVals> RetArray = new List<ReturnVals>();
            var RetErr = new Exception();
            GlobClassTr gClas = new GlobClassTr();
            JavaScriptSerializer jser = new JavaScriptSerializer();
            try
            {
                prefixText = prefixText.Trim().Replace("'", "''");
                gClas.TrOpen();
                string Qer = "select top 200 userid, usercode from circusermanagement  where userid like '%" + prefixText + "%' or usercode like '%" + prefixText + "%'";
                DataTable dtD = gClas.DataT(Qer);
                gClas.TrClose();
                for (int indX = 0; indX < dtD.Rows.Count; indX++)
                {
                    RetArray.Add(new ReturnVals { value = dtD.Rows[indX]["userid"].ToString(), label = dtD.Rows[indX]["usercode"].ToString() });
                }

                var d=jser.Serialize(RetArray);
                return d;
            }
            catch (Exception exx)
            {
                RetArray.Add(new ReturnVals { value = exx.Message, label = exx.Message });

                gClas.TrRollBack();
                var d = jser.Serialize(RetArray);
                return d;

            }

        }
        [WebMethod()]
        public string[] GetAffiliation(string prefixText, int count)
        {
            List<string> RetArray = new List<string>();
            var RetErr = new Exception();
            GlobClassTr gClas = new GlobClassTr();
            try
            {
                prefixText = prefixText.Trim().Replace("'", "''");
                gClas.TrOpen();
                string Qer = "select Name,Name+'|'+cast(ID as nvarchar) from AffilitionMaster where Name like N'%" + prefixText + "%' order by name";
                DataTable dtD = gClas.DataT(Qer);
                for (int indX = 0; indX < dtD.Rows.Count; indX++)
                    RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dtD.Rows[indX][0].ToString(), dtD.Rows[indX][1].ToString()));


                gClas.TrClose();

            }
            catch (Exception exx)
            {
                gClas.TrRollBack();
                RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Error in Suggestion: " + exx.Message, "0"));
            }

            return RetArray.ToArray();
        }
        [WebMethod()]
        public string[] GetCity(string prefixText, int count)
        {
            List<string> RetArray = new List<string>();
            var RetErr = new Exception();
            GlobClassTr gClas = new GlobClassTr();
            try
            {
                prefixText = prefixText.Trim().Replace("'", "''");
                gClas.TrOpen();
                string Qer = "select distinct percity,[percity]name from AddressTable where percity like N'%" + prefixText + "%'  order by percity";
                DataTable dtD = gClas.DataT(Qer);
                for (int indX = 0; indX < dtD.Rows.Count; indX++)
                    RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dtD.Rows[indX][0].ToString(), dtD.Rows[indX][1].ToString()));

                gClas.TrClose();

            }
            catch (Exception exx)
            {
                gClas.TrRollBack();
                RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Error in Suggestion: " + exx.Message, "0"));
            }

            return RetArray.ToArray();
        }
        [WebMethod()]
        public string[] GetState(string prefixText, int count)
        {
            List<string> RetArray = new List<string>();
            var RetErr = new Exception();
            GlobClassTr gClas = new GlobClassTr();
            try
            {
                prefixText = prefixText.Trim().Replace("'", "''");
                gClas.TrOpen();
                string Qer = "select distinct perstate,perstate name from AddressTable where perstate like N'%" + prefixText + "%'   order by perstate";
                DataTable dtD = gClas.DataT(Qer);
                for (int indX = 0; indX < dtD.Rows.Count; indX++)
                    RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dtD.Rows[indX][0].ToString(), dtD.Rows[indX][1].ToString()));

                gClas.TrClose();

            }
            catch (Exception exx)
            {
                gClas.TrRollBack();
                RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Error in Suggestion: " + exx.Message, "0"));
            }

            return RetArray.ToArray();
        }
        [WebMethod()]
        public string[] GetPin(string prefixText, int count)
        {
            List<string> RetArray = new List<string>();
            var RetErr = new Exception();
            GlobClassTr gClas = new GlobClassTr();
            try
            {
                prefixText = prefixText.Trim().Replace("'", "''");
                gClas.TrOpen();
                string Qer = "select distinct perpincode,perpincode name from AddressTable where perpincode like N'%" + prefixText + "%'   order by perpincode";
                DataTable dtD = gClas.DataT(Qer);
                for (int indX = 0; indX < dtD.Rows.Count; indX++)
                    RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dtD.Rows[indX][0].ToString(), dtD.Rows[indX][1].ToString()));

                gClas.TrClose();

            }
            catch (Exception exx)
            {
                gClas.TrRollBack();
                RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Error in Suggestion: " + exx.Message, "0"));
            }

            return RetArray.ToArray();
        }
        [WebMethod()]
        public string[] GetCountry(string prefixText, int count)
        {
            List<string> RetArray = new List<string>();
            var RetErr = new Exception();
            GlobClassTr gClas = new GlobClassTr();
            try
            {
                prefixText = prefixText.Trim().Replace("'", "''");
                gClas.TrOpen();
                string Qer = "select distinct percountry,percountry name from AddressTable where percountry like N'%" + prefixText + "%'   order by percountry";
                DataTable dtD = gClas.DataT(Qer);
                for (int indX = 0; indX < dtD.Rows.Count; indX++)
                    RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dtD.Rows[indX][0].ToString(), dtD.Rows[indX][1].ToString()));

                gClas.TrClose();

            }
            catch (Exception exx)
            {
                gClas.TrRollBack();
                RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Error in Suggestion: " + exx.Message, "0"));
            }

            return RetArray.ToArray();
        }
        //Jayant09Apr2020
        [WebMethod()]
        public string[] GetVendorCode(string prefixText, int count)
        {
            List<string> RetArray = new List<string>();
            var RetErr = new Exception();
            GlobClassTr gClas = new GlobClassTr();
            try
            {
                prefixText = prefixText.Trim().Replace("'", "''");
                gClas.TrOpen();
                string Qer = "Select distinct top 200 vendorcode,Vendorid from  VendorMaster where vendorcode like N'%" + prefixText + "%' order by Vendorid"; ;
                DataTable dtD = gClas.DataT(Qer);
                for (int indX = 0; indX < dtD.Rows.Count; indX++)
                    RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dtD.Rows[indX][0].ToString(), dtD.Rows[indX][1].ToString()));


                gClas.TrClose();

            }
            catch (Exception exx)
            {
                gClas.TrRollBack();
                RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Error in Suggestion: " + exx.Message, "0"));
            }

            return RetArray.ToArray();
        }
        //jayant
        [WebMethod()]
        public string[] mainmenu(string prefixText, int count)
        {
            List<string> RetArray = new List<string>();
            var RetErr = new Exception();
            GlobClassTr gClas = new GlobClassTr();
            try
            {
                prefixText = prefixText.Trim().Replace("'", "''");
                gClas.TrOpen();
                string Qer;
                if (prefixText.Trim() == "")
                {
                    Qer = "Select distinct menu_name,menu_id from popup_new where menu_name Like N'%" + prefixText + "%' Order by menu_name";
                }
                else
                {
                    Qer = "Select distinct menu_name,menu_id from popup_new where menu_name Like N'%" + prefixText + "%' Order by menu_name";
                }

                //End If
                // = "select top 200 userid, usercode from circusermanagement  where userid like '%" + prefixText + "%' or usercode like '%" + prefixText + "%'";
                DataTable dtD = gClas.DataT(Qer);
                for (int indX = 0; indX < dtD.Rows.Count; indX++)
                    RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dtD.Rows[indX][0].ToString(), dtD.Rows[indX][1].ToString()));


                gClas.TrClose();

            }
            catch (Exception exx)
            {
                gClas.TrRollBack();
                RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Error in Suggestion: " + exx.Message, "0"));
            }

            return RetArray.ToArray();
        }
        //jayant
        [WebMethod()]
        public string[] mainmenunew(string prefixText, int count)
        {
            List<string> RetArray = new List<string>();
            var RetErr = new Exception();
            GlobClassTr gClas = new GlobClassTr();
            try
            {
                prefixText = prefixText.Trim().Replace("'", "''");
                gClas.TrOpen();
                string Qer;
                if (prefixText.Trim() != "")
                {

                    Qer = "Select distinct menu_name,menu_id from popup_new where(href is null or href = '') and menu_name Like N'" + prefixText + "%' Order by menu_name";
                }
                else
                {
                    Qer = "Select distinct menu_name,menu_id from popup_new where (href is null or href='') and  menu_name Like N'%' Order by menu_name";
                }

                //End If
                // = "select top 200 userid, usercode from circusermanagement  where userid like '%" + prefixText + "%' or usercode like '%" + prefixText + "%'";
                DataTable dtD = gClas.DataT(Qer);
                for (int indX = 0; indX < dtD.Rows.Count; indX++)
                    RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dtD.Rows[indX][0].ToString(), dtD.Rows[indX][1].ToString()));


                gClas.TrClose();

            }
            catch (Exception exx)
            {
                gClas.TrRollBack();
                RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Error in Suggestion: " + exx.Message, "0"));
            }

            return RetArray.ToArray();
        }
        [WebMethod()]
        public string[] mainmenupg(string prefixText, int count)
        {
                List<string> RetArray = new List<string>();
            var RetErr = new Exception();
            if (prefixText.Trim() == "")
                return RetArray.ToArray();
            GlobClassTr gClas = new GlobClassTr();
            try
            {
                prefixText = prefixText.Trim().Replace("'", "''");
                gClas.TrOpen();
                  string  Qer = "Select distinct menu_name,menu_id from popup_new where href is not null and menu_name Like N'%" + prefixText + "%' Order by menu_name";

                //End If
                // = "select top 200 userid, usercode from circusermanagement  where userid like '%" + prefixText + "%' or usercode like '%" + prefixText + "%'";
                DataTable dtD = gClas.DataT(Qer);
                for (int indX = 0; indX < dtD.Rows.Count; indX++)
                    RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dtD.Rows[indX][0].ToString(), dtD.Rows[indX][1].ToString()));


                gClas.TrClose();

            }
            catch (Exception exx)
            {
                gClas.TrRollBack();
                RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Error in Suggestion: " + exx.Message, "0"));
            }

            return RetArray.ToArray();
        }

        //Jayant25Apr2020
        [WebMethod()]
        public string[] CataTitle(string prefixText, int count)
        {
            List<string> RetArray = new List<string>();
            var RetErr = new Exception();
            GlobClassTr gClas = new GlobClassTr();
            try
            {
                prefixText = prefixText.Trim().Replace("'", "''");
                gClas.TrOpen();
                string Qer = "SELECT distinct top 100 title,0 from CatalogueCardView Where title like '" + (prefixText.Trim() == string.Empty ? string.Empty : prefixText.Replace("'", "''")) + "%'  order by title";
                DataTable dtD = gClas.DataT(Qer);
                for (int indX = 0; indX < dtD.Rows.Count; indX++)
                    RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dtD.Rows[indX][0].ToString(), dtD.Rows[indX][1].ToString()));


                gClas.TrClose();

            }
            catch (Exception exx)
            {
                gClas.TrRollBack();
                RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Error in Suggestion: " + exx.Message, "0"));
            }

            return RetArray.ToArray();
        }
        [WebMethod()]
        public string[] STitle(string prefixText, int count)
        {
            List<string> RetArray = new List<string>();
            var RetErr = new Exception();
            GlobClassTr gClas = new GlobClassTr();
            try
            {
                prefixText = prefixText.Trim().Replace("'", "''");
                gClas.TrOpen();
                string Qer;
                if (prefixText.Trim() == "")
                {
                    Qer = "select distinct  dbo.GET_SUBTITLE(title,Subtitle,Paralleltype) + dbo.GET_TITLE('',Volume,part,edition) AS title,bookcatalog.ctrl_no from bookcatalog inner join bookconference on bookcatalog.ctrl_no=bookconference.ctrl_no where (title LIKE N'" + prefixText + "%')order by title";
                }
                else
                {
                    Qer = "select distinct  dbo.GET_SUBTITLE(title,Subtitle,Paralleltype) + dbo.GET_TITLE('',Volume,part,edition) AS title,bookcatalog.ctrl_no from bookcatalog inner join bookconference on bookcatalog.ctrl_no=bookconference.ctrl_no where (title LIKE N'%')order by title";
                }

                //End If
                // = "select top 200 userid, usercode from circusermanagement  where userid like '%" + prefixText + "%' or usercode like '%" + prefixText + "%'";
                DataTable dtD = gClas.DataT(Qer);
                for (int indX = 0; indX < dtD.Rows.Count; indX++)
                    RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dtD.Rows[indX][0].ToString(), dtD.Rows[indX][1].ToString()));


                gClas.TrClose();

            }
            catch (Exception exx)
            {
                gClas.TrRollBack();
                RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Error in Suggestion: " + exx.Message, "0"));
            }

            return RetArray.ToArray();
        }
        [WebMethod()]
        public string[] AuthorS(string prefixText, int count)
        {
            List<string> RetArray = new List<string>();
            var RetErr = new Exception();
            GlobClassTr gClas = new GlobClassTr();
            try
            {
                prefixText = prefixText.Trim().Replace("'", "''");
                gClas.TrOpen();
                string Qer;
                if (prefixText.Trim() == "")
                {
                    Qer = "Select distinct firstname1,firstname1 as FirstName2 From bookauthor where firstname1 Like N'" + prefixText + "%' Order by firstname1";
                }
                else
                {
                    Qer = "Select distinct firstname1,firstname1 as FirstName2 From bookauthor where firstname1 Like N'%' Order by firstname1";
                }

                //End If
                // = "select top 200 userid, usercode from circusermanagement  where userid like '%" + prefixText + "%' or usercode like '%" + prefixText + "%'";
                DataTable dtD = gClas.DataT(Qer);
                for (int indX = 0; indX < dtD.Rows.Count; indX++)
                    RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dtD.Rows[indX][0].ToString(), dtD.Rows[indX][1].ToString()));


                gClas.TrClose();

            }
            catch (Exception exx)
            {
                gClas.TrRollBack();
                RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Error in Suggestion: " + exx.Message, "0"));
            }

            return RetArray.ToArray();
        }
        [WebMethod()]
        public string[] ClassnoS(string prefixText, int count)
        {
            List<string> RetArray = new List<string>();
            var RetErr = new Exception();
            GlobClassTr gClas = new GlobClassTr();
            try
            {
                prefixText = prefixText.Trim().Replace("'", "''");
                gClas.TrOpen();
                string Qer;
                if (prefixText.Trim() == "")
                {

                    Qer = "Select distinct classnumber,0 from  CatalogueCardView where classnumber  LIKE N'" + prefixText + "%' Order by classnumber";
                }
                else
                {
                    Qer = "Select distinct classnumber,0 from  CatalogueCardView where classnumber  LIKE N'%' Order by classnumber";
                }

                //End If
                // = "select top 200 userid, usercode from circusermanagement  where userid like '%" + prefixText + "%' or usercode like '%" + prefixText + "%'";
                DataTable dtD = gClas.DataT(Qer);
                for (int indX = 0; indX < dtD.Rows.Count; indX++)
                    RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dtD.Rows[indX][0].ToString(), dtD.Rows[indX][1].ToString()));


                gClas.TrClose();

            }
            catch (Exception exx)
            {
                gClas.TrRollBack();
                RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Error in Suggestion: " + exx.Message, "0"));
            }

            return RetArray.ToArray();
        }
        //jayant09May2020
        [WebMethod()]
        public string[] GetBill(string prefixText, int count)
        {
            List<string> RetArray = new List<string>();
            var RetErr = new Exception();
            GlobClassTr gClas = new GlobClassTr();
            try
            {
                prefixText = prefixText.Trim().Replace("'", "''");
                gClas.TrOpen();
                string Qer;
                if (prefixText.Trim() == "")
                {

                    Qer = "Select distinct BillSerial_No,BillSerial_No as BillSerial_No1 from Journalinvoice_master where (status = N'r' OR JournalInvoice_master.Status = N'w') and BillSerial_No LIKE N'" + prefixText + "%' order by BillSerial_No";
                }
                else
                {
                    Qer = "Select distinct BillSerial_No,BillSerial_No as BillSerial_No1 from Journalinvoice_master where (status=N'r' OR JournalInvoice_master.Status =N'w') and BillSerial_No LIKE N'%' order by BillSerial_No";
                }

                //End If
                // = "select top 200 userid, usercode from circusermanagement  where userid like '%" + prefixText + "%' or usercode like '%" + prefixText + "%'";
                DataTable dtD = gClas.DataT(Qer);
                for (int indX = 0; indX < dtD.Rows.Count; indX++)
                    RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dtD.Rows[indX][0].ToString(), dtD.Rows[indX][1].ToString()));


                gClas.TrClose();

            }
            catch (Exception exx)
            {
                gClas.TrRollBack();
                RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Error in Suggestion: " + exx.Message, "0"));
            }

            return RetArray.ToArray();
        }
        [WebMethod()]
        public string[] Jtitle(string prefixText, int count)
        {
            List<string> RetArray = new List<string>();
            var RetErr = new Exception();
            GlobClassTr gClas = new GlobClassTr();
            try
            {
                prefixText = prefixText.Trim().Replace("'", "''");
                gClas.TrOpen();
                string Qer;
                if (prefixText.Trim() == "")
                {

                    Qer = "Select distinct journal_title,Unique_JN  from  JournalMaster where Unique_JN <>'NULL' and journal_title  LIKE N'" + prefixText + "%' order by  journal_title";
                }
                else
                {
                    Qer = "Select distinct journal_title,Unique_JN  from  JournalMaster where Unique_JN <>'NULL' and journal_title LIKE N'%' order by  journal_title";
                }

                //End If
                // = "select top 200 userid, usercode from circusermanagement  where userid like '%" + prefixText + "%' or usercode like '%" + prefixText + "%'";
                DataTable dtD = gClas.DataT(Qer);
                for (int indX = 0; indX < dtD.Rows.Count; indX++)
                    RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dtD.Rows[indX][0].ToString(), dtD.Rows[indX][1].ToString()));


                gClas.TrClose();

            }
            catch (Exception exx)
            {
                gClas.TrRollBack();
                RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Error in Suggestion: " + exx.Message, "0"));
            }

            return RetArray.ToArray();
        }
        //sachin11May2020
        [WebMethod()]
        public string[] GetSuggestionsT(string prefixText, int count)
        {
            List<string> RetArray = new List<string>();
            var RetErr = new Exception();
            GlobClassTr gClas = new GlobClassTr();
            try
            {
                prefixText = prefixText.Trim().Replace("'", "''");
                gClas.TrOpen();
                string Qer;
                Qer = "Select distinct top 50 Title_N,T_id from  NewsPaper_T where Title_N  LIKE N'%" + prefixText + "%' order by  Title_N";

                //End If
                // = "select top 200 userid, usercode from circusermanagement  where userid like '%" + prefixText + "%' or usercode like '%" + prefixText + "%'";
                DataTable dtD = gClas.DataT(Qer);
                for (int indX = 0; indX < dtD.Rows.Count; indX++)
                    RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dtD.Rows[indX][0].ToString(), dtD.Rows[indX][1].ToString()));
                gClas.TrClose();
            }
            catch (Exception exx)
            {
                gClas.TrRollBack();
                RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Error in Suggestion: " + exx.Message, "0"));
            }
            return RetArray.ToArray();
        }
        //Jayant 11May2020
        [WebMethod()]
        public string[] Jtitlesugg(string prefixText, int count)
        {
            List<string> RetArray = new List<string>();
            var RetErr = new Exception();
            GlobClassTr gClas = new GlobClassTr();
            try
            {
                prefixText = prefixText.Trim().Replace("'", "''");
                gClas.TrOpen();
                string Qer;
                if (prefixText.Trim() == "")
                {
                    Qer = "select top 200 (journal_title+'('+ journal_no +')' +'/'+ CONVERT(nvarchar, fromyear,106 )+'-'+ CONVERT(nvarchar, Toyear ,106 ))  as Journal,journal_no from JournalMaster where Process_Stage=N'E' and (journal_title +' '+ CONVERT(nvarchar, fromyear,106 )+'-'+ CONVERT(nvarchar, Toyear ,106 )+'('+ journal_no +')') LIKE N'" + prefixText + "%'  or (CONVERT(nvarchar, fromyear,106 )+'-'+ CONVERT(nvarchar, Toyear ,106 ))  + cast(total_volume as varchar) LIKE N'" + prefixText + "%' order by journal_title+'('+ journal_no +')' +'/'+ CONVERT(nvarchar, fromyear,106 )+'-'+ CONVERT(nvarchar, Toyear ,106 )";

                }
                else
                {
                    Qer = "select top 200 (journal_title+'('+ journal_no +')' +'/'+ CONVERT(nvarchar, fromyear,106 )+'-'+ CONVERT(nvarchar, Toyear ,106 ))  as Journal,journal_no from JournalMaster where Process_Stage=N'E' and (journal_title +' '+ CONVERT(nvarchar, fromyear,106 )+'-'+ CONVERT(nvarchar, Toyear ,106 )+'('+ journal_no +')') LIKE N'%'  or (CONVERT(nvarchar, fromyear,106 )+'-'+ CONVERT(nvarchar, Toyear ,106 ))  + cast(total_volume as varchar) LIKE N'%' order by journal_title+'('+ journal_no +')' +'/'+ CONVERT(nvarchar, fromyear,106 )+'-'+ CONVERT(nvarchar, Toyear ,106 )";
                }

                //End If
                // = "select top 200 userid, usercode from circusermanagement  where userid like '%" + prefixText + "%' or usercode like '%" + prefixText + "%'";
                DataTable dtD = gClas.DataT(Qer);
                for (int indX = 0; indX < dtD.Rows.Count; indX++)
                    RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dtD.Rows[indX][0].ToString(), dtD.Rows[indX][1].ToString()));


                gClas.TrClose();

            }
            catch (Exception exx)
            {
                gClas.TrRollBack();
                RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Error in Suggestion: " + exx.Message, "0"));
            }

            return RetArray.ToArray();
        }
        //jayant 11May2020
        [WebMethod()]
        public string[] JtitleNew(string prefixText, int count)
        {
            List<string> RetArray = new List<string>();
            var RetErr = new Exception();
            GlobClassTr gClas = new GlobClassTr();
            try
            {
                prefixText = prefixText.Trim().Replace("'", "''");
                gClas.TrOpen();
                string Qer;
                if (prefixText.Trim() == "")
                {

                    Qer = "Select distinct journal_title,JournalMaster.Journal_No,Fromyear,Toyear  from JournalMaster,journal_arrival where Subscription_Status <>N'Close' and JournalMaster.Journal_id=journal_arrival.journal_no and journal_title LIKE N'" + prefixText + "%' order by  journal_title";
                }
                else
                {
                    Qer = "Select distinct journal_title,JournalMaster.Journal_No,Fromyear,Toyear  from JournalMaster,journal_arrival where Subscription_Status <>N'Close' and JournalMaster.Journal_id=journal_arrival.journal_no and journal_title LIKE N'%' order by  journal_title";
                }

                //End If
                // = "select top 200 userid, usercode from circusermanagement  where userid like '%" + prefixText + "%' or usercode like '%" + prefixText + "%'";
                DataTable dtD = gClas.DataT(Qer);
                for (int indX = 0; indX < dtD.Rows.Count; indX++)
                    RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dtD.Rows[indX][0].ToString(), dtD.Rows[indX][1].ToString()));


                gClas.TrClose();

            }
            catch (Exception exx)
            {
                gClas.TrRollBack();
                RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Error in Suggestion: " + exx.Message, "0"));
            }

            return RetArray.ToArray();
        }

        [WebMethod(enableSession: true)]
        public string Menu2021()
        {
            string menuData = "";
            try
            {
                PrepareMenu2 mn = new PrepareMenu2();
                var logged = LoggedUser.Logged();
                if (logged == null)
                    return menuData;
                mn.UserTypeId = logged.UserType;
                menuData = mn.MenuHtml;


            }
            catch (Exception ex)
            {
            }
            return menuData;
        }

        public class errordata
        {
            public string title { get; set; }
            public string pagename { get; set; }
            public string linenumber { get; set; }
            public string message { get; set; }
        }
        [WebMethod(enableSession: true)]
        public string errorlogindb(errordata errd)
        {
            string errm = "";
            GlobClassTr clas = new GlobClassTr();
            try
            {
                clas.TrOpen();
                string qer = "select institutename from librarysetupinformation ";
                string insn = clas.ExScaler(qer).ToString();
                qer = "select LogError,RecordOnServer,DeleteLogAfterRec,ServerLogin,ServerPassw,ServerIp,ServerDb from ErrorLogSetting ";
                DataTable dtsett = clas.DataT(qer);
                bool logerr = false;
                if (dtsett.Rows.Count > 0)
                {
                    if (dtsett.Rows[0]["logerror"].ToString().ToUpper() == "Y")
                        logerr = true;
                }
                if (!logerr)
                {
                    clas.TrClose();
                    return errm;
                }
                List<SqlParameter> lsp = new List<SqlParameter>();
                SqlParameter p1 = new SqlParameter("@ErrorId", SqlDbType.Int);
                p1.Value = 0;
                SqlParameter p2 = new SqlParameter("@Project", SqlDbType.VarChar);
                p2.Value = "Library";
                SqlParameter p3 = new SqlParameter("@Instname", SqlDbType.VarChar);
                p3.Value = insn;
                SqlParameter p4 = new SqlParameter("@LineNumber", SqlDbType.VarChar);
                p4.Value = errd.linenumber.Trim();
                SqlParameter p5 = new SqlParameter("@PageName", SqlDbType.VarChar);
                p5.Value = errd.pagename.Trim();
                SqlParameter p6 = new SqlParameter("@Title", SqlDbType.VarChar);
                p6.Value = errd.title.Trim();
                SqlParameter p6m = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar);
                p6m.Value = errd.message.Trim();
                SqlParameter p7 = new SqlParameter("@UserId", SqlDbType.VarChar);
                p7.Value = HttpContext.Current.Session["User_id"].ToString();
                SqlParameter p8 = new SqlParameter("@AsOn", SqlDbType.DateTime);
                p8.Value = DateTime.Now;
                lsp.Add(p1);
                lsp.Add(p2);
                lsp.Add(p3);
                lsp.Add(p4);
                lsp.Add(p5);
                lsp.Add(p6);
                lsp.Add(p6m);
                lsp.Add(p7);
                lsp.Add(p8);
                clas.ExProc("sp_ErrorLog", lsp);

                clas.TrClose();

            }
            catch (Exception excp)
            {
                clas.TrRollBack();
                errm = excp.Message;
            }
            return errm;
        }
        [WebMethod()]
        public string[] GetNewsTitle(string prefixText, int count)
        {
            List<string> RetArray = new List<string>();
            var RetErr = new Exception();
            GlobClassTr gClas = new GlobClassTr();
            try
            {
                prefixText = prefixText.Trim().Replace("'", "''");
                gClas.TrOpen();
                string Qer = "Select distinct top 200  NewsPaperTitle+'|'+VendorName+', '+percity+'|'+Newspaper_id NewsPaperTitle,Newspaper_id from NewsPaper_Master, VendorMaster, addresstable where vendormaster.vendorcode=addresstable.addid and addrelation='vendor' and  NewsPaper_Master.vendorid=vendormaster.vendorid and status='Active' and NewsPaperTitle like N'%" + prefixText + "%' order by NewsPaperTitle";
                DataTable dtD = gClas.DataT(Qer);
                for (int indX = 0; indX < dtD.Rows.Count; indX++)
                    RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dtD.Rows[indX][0].ToString(), dtD.Rows[indX][1].ToString()));


                gClas.TrClose();

            }
            catch (Exception exx)
            {
                gClas.TrRollBack();
                RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Error in Suggestion: " + exx.Message, "0"));
            }

            return RetArray.ToArray();
        }
        //27Sep2021sachin
        [WebMethod()]
        public string[] GetMemberIDWithName(string prefixText, int count)
        {
            List<string> RetArray = new List<string>();
            var RetErr = new Exception();
            GlobClassTr gClas = new GlobClassTr();
            try
            {
                prefixText = prefixText.Trim().Replace("'", "''");
                gClas.TrOpen();
                string Qer = "select top 200 userid+'| '+firstname+' '+middlename+' '+lastname name, usercode from circusermanagement  where userid like '%" + prefixText + "%' or usercode like '%" + prefixText + "%' or firstname like '%" + prefixText + "%' or middlename like '%" + prefixText + "%' or lastname like '%" + prefixText + "%'";
                DataTable dtD = gClas.DataT(Qer);
                for (int indX = 0; indX < dtD.Rows.Count; indX++)
                    RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dtD.Rows[indX][0].ToString(), dtD.Rows[indX][1].ToString()));


                gClas.TrClose();

            }
            catch (Exception exx)
            {
                gClas.TrRollBack();
                RetArray.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Error in Suggestion: " + exx.Message, "0"));
            }

            return RetArray.ToArray();
        }

    }
}
