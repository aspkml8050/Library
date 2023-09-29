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
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Net;
using System.Net.Mail;
//using System.Net.Sockets.TcpClient;
//using System.Net.Mail.SmtpPermission;
//using System.Configuration.ConfigurationManager;
using System.Security.Cryptography;
//using HTMLReportEngine;
//using MSS;
using System.Web.Services;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Collections;
using Library.App_Code.CSharp;
//using MailSMS;
namespace Library.App_Code.MultipleFramworks
{
    public class MemSecu
    {
       // private DBIStructure DBI = new DBIStructure();
        // Call this function when being refunded amount is not 0! 
        // Security amt will be adjusted against one or all security deposits based on classmasterloadinstatus values
        // SecAmt is amount being refunded
     /*   public string AdjustS(string MemId, string AccNo, double SecAmt, OleDbCommand Commnd = null)
        {
            if (SecAmt <= 0)
                return "";
            BaseClass.FillDsTables gtDt = new BaseClass.FillDsTables();
            string Qry, Err;
            DataTable dtSec = new DataTable(), dtPercSet = new DataTable(), dtPrice = new DataTable();
            Qry = "select issueid, isnull(bbsecurity,0) SAmt from circissuetransaction where userid='" + MemId + "' ";
            if (Commnd == null)
                Err = gtDt.FillDs(Qry, ref dtSec);
            else
                Err = gtDt.FillDs(Qry, ref dtSec, Commnd);
            if (Err != "")
                return Err;
            if (dtSec.Rows.Count == 0)
                return "";
            var Sums = dtSec.AsEnumerable().Sum(sM =>Convert.ToDecimal( sM["SAmt"]));
            if (Sums == 0)
                return "No Security Amount Deposited.";
            // When member group is set then these values are set and if refund percent is not set the deposit percent is taken
            Qry = "select isnull(a.SecDep,0) secdep,isnull(a.SecRef,0) secref from classmasterloadingstatus a,CircClassMaster b,CircUserManagement c,bookaccessionmaster d ";
            Qry += " where a.classname=b.classname and a.LoadingStatus=d.ItemCategoryCode and b.classname=c.classname ";
            Qry += "  and d.accessionnumber='" + AccNo + "' and c.usercode='" + MemId + "'";
            if (Commnd == null)
                Err = gtDt.FillDs(Qry, ref dtPercSet);
            else
                Err = gtDt.FillDs(Qry, ref dtPercSet, Commnd);
            if (Err != "")
                return Err;
            if (dtPercSet.Rows.Count == 0)
                return "";
            if (dtPercSet.Rows[0][0].ToString() == "0")
                return "";
            // Qry = "select bookprice*b.BankRate Price from bookaccessionmaster a,exchangemaster b "
            // Qry += " where a.OriginalCurrency=b.CurrencyName and accessionnumber='" & AccNo & "' "
            // If Commnd Is Nothing Then
            // Err = gtDt.FillDs(Qry, dtPrice)
            // Else
            // Err = gtDt.FillDs(Qry, dtPrice, Commnd)
            // End If
            // If Err <> "" Then
            // Return Err
            // End If
            // If dtPrice.Rows.Count = 0 Then
            // Return ""
            // End If

            double AppSec, RefPerc;
            if (dtPercSet.Rows[0][1].ToString() == "0")
                RefPerc = Convert.ToDouble(dtPercSet.Rows[0][0]); // refund percentage
            else
                RefPerc = Convert.ToDouble(dtPercSet.Rows[0][1]);
            // applicable sec amt
            AppSec =Convert.ToDouble( Sums) * RefPerc / (double)100;
            string appSecS = string.Format("{0:0.00}", AppSec);
            if (SecAmt > AppSec)
            {
                Err = "Being refunded amount " + SecAmt.ToString() + " is more than applicable amount " + appSecS + ", Not accepted.";
                return Err;
            }
            if (Commnd != null)
            {
                for (Int16 ii = 0; ii <= dtSec.Rows.Count - 1; ii++)
                {
                    if (dtSec.Rows[ii][1].ToString() == "0")
                        continue;
                    if (SecAmt <= 0)
                        break;
                    double DedAmt;
                    if (SecAmt > Convert.ToDouble(dtSec.Rows[ii][1]))
                        DedAmt = Convert.ToDouble(dtSec.Rows[ii][1]);
                    else
                        DedAmt = SecAmt;
                    SecAmt -= DedAmt;
                    Qry = "update circissuetransaction set bbsecurity=bbsecurity-" + DedAmt + " where issueid=" + dtSec.Rows[ii][0];
                    Err = gtDt.InsUpd(Qry, Commnd);
                    if (Err != "")
                        return Err;
                }
            }
            if (Commnd == null)
            {
                Err = "Parameter CommandType is missing and NOT implemented yet.";
                return Err;
            }
            return "";
        }
   */ }
    public class libGeneralFunctions
    {
        private DBIStructure DBI = new DBIStructure();

        public bool IsValidAccNo(string AccNo)
        {
            Int16 ii = 0;
            bool retStat = false;
            while (ii < AccNo.Length & AccNo.Trim() != "")
            {
                retStat = false;
                int asi = int.Parse(AccNo.Substring(ii)); // .Substring(ii, 1);
                if (asi >= 48 & asi <= 57)
                    retStat = true;
                if (asi >= 65 & asi <= 90)
                    retStat = true;
                if (asi >= 97 & asi <= 122)
                    retStat = true;
                if (retStat == false)
                    break;
                ii += 1;
            }
            if (AccNo.Length > 16)
                retStat = false;
            return retStat;
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
        public string GenerateAccFinal(string connstr, DropDownList cmbaccessionprefix, RadioButton Rbaccession1, RadioButton RBSuffix, RadioButton RBPreSuff, RadioButton Rbmanual1, int noofcopy, string txtaccessionstr, CheckBox chkApplyRange, DropDownList cmbaccrange)
        {
            try
            {
                string strreturn = string.Empty;
                string startno = string.Empty;
                string curr_startno = string.Empty;

                int icunt, icount;
                bool ctrue;
                string istr;

                string accno_str, acc_str, no_str, start_str, pre_string = "", suf_string;
                suf_string = string.Empty;
                // If cmdsave.Value = Resources.ValidationResources.bSave.ToString Then
                if (Rbaccession1.Checked == true)
                {
                    curr_startno = ExecuteScalar("select currentposition from prefixmaster where prefixid=" + cmbaccessionprefix.SelectedItem.Value, connstr);
                    pre_string = cmbaccessionprefix.SelectedItem.Text;
                }
                else if (RBSuffix.Checked == true)
                {
                    curr_startno = ExecuteScalar("select currentposition from prefixmaster where prefixid=" + cmbaccessionprefix.SelectedItem.Value, connstr);
                    suf_string = cmbaccessionprefix.SelectedItem.Text;
                }
                else if (RBPreSuff.Checked == true)
                {
                    curr_startno = ExecuteScalar("select currentposition from prefixmaster where prefixid=" + cmbaccessionprefix.SelectedItem.Value, connstr);
                    pre_string = (cmbaccessionprefix.SelectedItem.Text.Split('-')).GetValue(0).ToString();
                    suf_string = (cmbaccessionprefix.SelectedItem.Text.Split('-')).GetValue(1).ToString();
                }
                else
                {
                    curr_startno = ExecuteScalar("select currentposition from prefixmaster where prefixid=0", connstr);
                    curr_startno = curr_startno;
                }
                // End If

                if (Rbmanual1.Checked == false)
                {
                    no_str = curr_startno.Length.ToString();
                    accno_str = pre_string == null ? "" : pre_string.ToString();
                    start_str = no_str;
                    if (start_str == "1")
                        acc_str = "000";
                    else if (start_str == "2")
                        acc_str = "00";
                    else if (start_str == "3")
                        acc_str = "0";
                    else
                        acc_str = string.Empty;

                    string accno = string.Empty;
                    for (int k = 0; k <= noofcopy - 1; k++)
                    {
                        if (accno.Trim() == string.Empty)
                            accno = (pre_string + acc_str + curr_startno + k + suf_string);
                        else
                            accno = accno + "," + (pre_string + acc_str + curr_startno + k + suf_string);
                    }
                    strreturn = accno;
                }

                System.Array arr;

                // On Mannual Entry-----------------------------------------------------------------------
                int first = 0;
                int last = 0;
                string prefix = string.Empty;
                string temp = string.Empty;
                if (Rbmanual1.Checked == true & chkApplyRange.Checked == false)
                {
                    if (txtaccessionstr == string.Empty)
                    {
                        return "K," + 
                            Resources.ValidationResources.AccCheck.ToString();
                    }
                    else
                    {
                        bool flg;
                        flg = ValidateAccNo(txtaccessionstr);
                        if (flg == false)
                        {
                            return "K, "+ Resources.ValidationResources.EAccNoInCrtFmt.ToString();
                        }
                    }
                    strreturn = txtaccessionstr;
                    arr = txtaccessionstr.Trim().Split(',');
                    for (icunt = 0; icunt <= arr.Length - 1; icunt++)
                    {
                        for (icount = 0; icount <= arr.Length - 1; icount++)
                        {
                            istr = arr.GetValue(icunt).ToString();
                            if (icunt != icount)
                            {
                                if (istr == arr.GetValue(icount).ToString())
                                {
                                    ctrue = true;
                                    return "K, " + Resources.ValidationResources.AccNoAlradyExt.ToString();
                                }
                            }
                        }
                    }
                }
                else if (Rbmanual1.Checked == true & chkApplyRange.Checked == true)
                {
                    if (txtaccessionstr == string.Empty)
                    {
                        return "K, " + Resources.ValidationResources.Accrange;//  System.Resources.ValidationResources.Accrange.ToString;
                    }
                    else if (ValidateAccNoRange(txtaccessionstr) == false)
                    {
                        return "K," + Resources.ValidationResources.EAccNoInCrtFmt;
                    }
                    arr = txtaccessionstr.Split('-', '3');// string.Split(txtaccessionstr, "-", 3);
                    first = Convert.ToInt32(arr.GetValue(0));
                    last = Convert.ToInt32(arr.GetValue(1));
                    prefix = cmbaccrange.SelectedItem.Text;
                    if ((last - first + 1) > 100)
                    {
                        return "K," + Resources.ValidationResources.only100opralwd;

                    }
                    if (prefix == "---Select---")
                        temp = AccNoRange(first, last, "");
                    else
                        temp = AccNoRange(first, last, prefix);
                    strreturn = temp.ToString();
                }
                return strreturn;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        public bool Is_MultipleCopiesIssueTosameUser(OleDbCommand cmd)
        {
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SElect Multi_Issue from librarysetupinformation";
                string s = cmd.ExecuteScalar().ToString();
                cmd.Parameters.Clear();
                if (s == "Y")
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
              public void moreinfoDIgitalAura(DataSet ds, OleDbCommand cmd)
              {
                  try
                  {
                      if (ds.Tables[0].Rows.Count != 0)
                      {
                          cmd.CommandType = CommandType.StoredProcedure;
                          cmd.CommandText = "insert_DirectArchMoreInfo_1";

                          cmd.Parameters.Add(new OleDbParameter("@Daid_1", OleDbType.Integer));
                          cmd.Parameters["@Daid_1"].Value = Convert.ToInt32(ds.Tables[0].Rows[0]["Daid"]);

                          cmd.Parameters.Add(new OleDbParameter("@Author_2", OleDbType.VarWChar));
                          cmd.Parameters["@Author_2"].Value = ds.Tables[0].Rows[0]["Author"].ToString();

                          cmd.Parameters.Add(new OleDbParameter("@Volume_3", OleDbType.VarWChar));
                          cmd.Parameters["@Volume_3"].Value = ds.Tables[0].Rows[0]["Volume"].ToString();

                          cmd.Parameters.Add(new OleDbParameter("@IssueNo_4", OleDbType.VarWChar));
                          cmd.Parameters["@IssueNo_4"].Value = ds.Tables[0].Rows[0]["IssueNo"].ToString();

                          cmd.Parameters.Add(new OleDbParameter("@PubDate_5", OleDbType.VarWChar));
                          cmd.Parameters["@PubDate_5"].Value = ds.Tables[0].Rows[0]["PubDate"].ToString();

                          cmd.Parameters.Add(new OleDbParameter("@Part_6", OleDbType.VarWChar));
                          cmd.Parameters["@Part_6"].Value = ds.Tables[0].Rows[0]["Part"].ToString();

                          cmd.Parameters.Add(new OleDbParameter("@Edition_7", OleDbType.VarWChar));
                          cmd.Parameters["@Edition_7"].Value = ds.Tables[0].Rows[0]["Edition"].ToString();

                          cmd.Parameters.Add(new OleDbParameter("@Publisher_8", OleDbType.VarWChar));
                          cmd.Parameters["@Publisher_8"].Value = ds.Tables[0].Rows[0]["Publisher"].ToString();

                          cmd.Parameters.Add(new OleDbParameter("@PageNo_9", OleDbType.Integer));
                          cmd.Parameters["@PageNo_9"].Value = Convert.ToInt32(ds.Tables[0].Rows[0]["PageNo"]);

                          cmd.Parameters.Add(new OleDbParameter("@Noofpage_10", OleDbType.Integer));
                          cmd.Parameters["@Noofpage_10"].Value = Convert.ToInt32(ds.Tables[0].Rows[0]["Noofpage"]);

                          cmd.Parameters.Add(new OleDbParameter("@FPubDate_11", OleDbType.Date));
                          cmd.Parameters["@FPubDate_11"].Value = Convert.ToDateTime(ds.Tables[0].Rows[0]["FromPubDate"]);

                          cmd.Parameters.Add(new OleDbParameter("@TPubdate_12", OleDbType.Date));
                          cmd.Parameters["@TPubdate_12"].Value = Convert.ToDateTime(ds.Tables[0].Rows[0]["ToPubDate"]);

                          cmd.Parameters.Add(new OleDbParameter("@Sourcetype", OleDbType.VarWChar));
                          cmd.Parameters["@Sourcetype"].Value = ds.Tables[0].Rows[0]["SourceType"].ToString();
                          cmd.ExecuteNonQuery();
                      }
                  }
                  catch (Exception ex)
                  {
                  }
              }
      
        // --CAS & CAD services Digital Aura
        /*public bool CAS_CAD(OleDbConnection circmesconk, System.Web.UI.Page ref_page, DateTime Dt, string Item_type, string DAid, string Title)
        {
            bool @bool;
            try
            {
                string mem_id = "select distinct member_id from Current_awairness_service where item='News Clipping(s)' ";
                DataSet memds = PopulateDataset(mem_id, "tbl", circmesconk);
                if (memds.Tables[0].Rows.Count > 0)
                {
                    for (var i = 0; i <= memds.Tables[0].Rows.Count - 1; i++)
                    {
                        string item_str = "select item,frequency, from_date,to_date,subject,sent_upto from Current_awairness_service where member_id=N'" + memds.Tables[0].Rows[i].Item[0] + "'";
                        DataSet cadds = PopulateDataset(item_str, "tbl", circmesconk);
                        if (cadds.Tables[0].Rows.Count > 0)
                        {
                            for (var j = 0; j <= cadds.Tables[0].Rows.Count - 1; j++)
                            {
                                if (cadds.Tables[0].Rows[j].Item["frequency"].ToString() == "Immediate" & cadds.Tables[0].Rows[j].Item["Item"].ToString() == Item_type)
                                {
                                    if (Convert.ToDateTime(cadds.Tables[0].Rows[j].Item["from_date"]) <= Dt & Convert.ToDateTime(cadds.Tables[0].Rows[j].Item["to_date"]) >= Dt)
                                    {
                                        string sqlJrnl = "select DAid,Type_NO,Title,UserId from digitalarchiveinfo where Daid='" + DAid + "' and  (Title like '%" + cadds.Tables[0].Rows[j].Item["subject"] + "%' Or synopsis like '%" + cadds.Tables[0].Rows[j].Item["subject"] + "%')";
                                        DataSet jrnlds = PopulateDataset(sqlJrnl, "tbl", circmesconk);
                                        if (jrnlds.Tables[0].Rows.Count > 0)
                                        {
                                            string sqlstr2 = "select firstname,middlename,lastname,email1,email2 from circusermanagement where userid=N'" + memds.Tables[0].Rows[i].Item[0] + "'";
                                            DataSet userds = PopulateDataset(sqlstr2, "tbl", circmesconk);
                                            string eMailId = null;
                                            string eMailId1 = null;
                                            string userName;
                                            if (userds.Tables[0].Rows.Count > 0)
                                            {
                                                eMailId = userds.Tables[0].Rows[0].Item["email1"];
                                                eMailId1 = userds.Tables[0].Rows[0].Item["email2"];
                                                userName = userds.Tables[0].Rows[0].Item["firstname"] + " " + userds.Tables[0].Rows[0].Item["middlename"] + " " + userds.Tables[0].Rows[0].Item["lastname"];

                                                string mailContent2 = string.Empty;


                                                mailContent2 = mailContent2 + "<br/>" + popmailing_msg("TT").ToString() + "  " + Title + "   <B> " + popmailing_msg("JPDK").ToString() + " </B> " + Dt.ToString("dd-MMM-yyyy") + ""; // <B>  Exp.Dt: </B>" & txtexdate.Text & ""

                                                mailContent2 = "<B>" + Item_type + " Information (Digital Aura):</B>" + mailContent2 + "<br/>";
                                                string message = popmailing_msg("DSK").ToString() + "<br/>" + "         " + userName + "<br/>" + popmailing_msg("FIRA").ToString() + "<br/>" + mailContent2;
                                                if (eMailId != string.Empty | eMailId1 == string.Empty)
                                                {
                                                    sendMailToTable(popmailing_msg("CAIK").ToString(), message, ref_page, eMailId, eMailId1);
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return @bool;
        }
      */
        public bool msgPopUp()
        {
            bool flag;
            OleDbConnection con = new OleDbConnection(ConfigurationManager.ConnectionStrings[System.Web.HttpContext.Current.Session["LibWiseDBConn"].ToString()].ToString());
            con.Open();
            OleDbCommand cmd = new OleDbCommand("select msgPopUp from librarysetupinformation", con);
            string msg = cmd.ExecuteScalar().ToString();
            if (msg == "Y")
                flag = true;
            else
                flag = false;
            con.Close();
            con.Dispose();
            return flag;
        }
        // BY Kaushal 03-01-2013  making condition to Showing record particular about login user
/*        public string usersrecordband(string user, string colnm)
        {
            OleDbConnection con = new OleDbConnection(ConfigurationManager.ConnectionStrings[System.Web.HttpContext.Current.Session["LibWiseDBConn"].ToString()]).ToString();
            con.Open();
            OleDbCommand cmd = new OleDbCommand("select usersdataband from Librarysetupinformation", con);
            string chkflg = cmd.ExecuteScalar().ToString();
            string usercondi = string.Empty;
            if (chkflg != "N")
            {
                if (user.ToLower() != "admin")
                    usercondi = " and " + colnm + "= '" + user + " '";
            }
            con.Close();
            return usercondi;
        }
  */      // By Kaushal 12.Aug.12 For Mailing msg Services
        // ---------------------------------------------------------
          public string popmailing_msg(string msgcode)
          {
              OleDbConnection con = new OleDbConnection(ConfigurationManager.ConnectionStrings[System.Web.HttpContext.Current.Session["LibWiseDBConn"].ToString()].ToString());
              con.Open();
              OleDbCommand cmd = new OleDbCommand("select Message from MailingMessages where code='" + msgcode + "'", con);
              string msg = cmd.ExecuteScalar().ToString();
              con.Close();
              con.Dispose();
              return msg;
          }
          public void DefultMaster_check(OleDbConnection con, OleDbCommand cmd)
          {
              try
              {
                  // BY Kaushal
                  // -----------------------------------------------

                  cmd.CommandType = CommandType.Text;
                  cmd.CommandText = "Select  primaryDescType from  librarysetupinformation ";
                  string primary = cmd.ExecuteScalar().ToString();
                  cmd.ExecuteNonQuery();
                  cmd.Parameters.Clear();
                  if (primary == string.Empty)
                  {
                      cmd.CommandText = "Update librarysetupinformation set primaryDescType='I'";
                      cmd.ExecuteNonQuery();
                      cmd.Parameters.Clear();
                  }

                  // ------------------------------------
                  // F0r E-Files Inserting Auto NA
                  cmd.CommandType = CommandType.Text;
                  cmd.CommandText = "Select Status from StatusMaster where status='NA'";
                  string status = cmd.ExecuteScalar().ToString();
                  cmd.ExecuteNonQuery();
                  cmd.Parameters.Clear();
                  if (status == string.Empty)
                  {
                      cmd.CommandText = "Insert into statusmaster values(0,'NA','Na')";
                      cmd.ExecuteNonQuery();
                      cmd.Parameters.Clear();
                  }
                  // For Register MAster
                  cmd.CommandText = "Select Register from Registermaster where Register='NA'";
                  string REgister = cmd.ExecuteScalar().ToString();
                  cmd.ExecuteNonQuery();
                  if (REgister == string.Empty)
                  {
                      cmd.CommandText = "Insert into Registermaster values(0,'NA','na',1,1,1,'Kaushal')";
                      cmd.ExecuteNonQuery();
                      cmd.Parameters.Clear();
                  }
                  // For Item Category------------------------------------
                  cmd.CommandText = "Select itemstatusID from itemStatusMaster where itemstatusID=0 ";
                  string @int = cmd.ExecuteScalar().ToString();
                  cmd.ExecuteNonQuery();
                  if (@int == string.Empty | @int == "")
                  {
                      cmd.CommandText = "Insert into itemStatusMaster values(0,'NA','NA','N','N','Admin')";
                      cmd.ExecuteNonQuery();
                      cmd.Parameters.Clear();
                  }
                  // -For ItemCategoryLoading Status----------------------
                  cmd.CommandText = "Select Category_LoadingStatus from CategoryLoadingStatus where Id='0' ";
                  string int1 = cmd.ExecuteScalar().ToString();
                  cmd.ExecuteNonQuery();
                  if (int1 == string.Empty | int1 == "")
                  {
                      cmd.CommandText = "Insert into CategoryLoadingStatus values(0,'None','NA','0x01010000','Admin')";
                      cmd.ExecuteNonQuery();
                      cmd.Parameters.Clear();
                  }
                  // For Member Group
                  cmd.CommandText = "Select Classname from circclassmaster where classname='NA' ";
                  string member_grp = cmd.ExecuteScalar().ToString();
                  cmd.ExecuteNonQuery();
                  if (member_grp == string.Empty | member_grp == "")
                  {
                      cmd.CommandText = "Insert Into circclassmaster values('NA',0,0,0.00,0,0,0,0,0,'N','N',0.00,0,0,0,0,0,0,0,0.00,'NA','Admin','S','Free','No')";
                      cmd.ExecuteNonQuery();
                      cmd.Parameters.Clear();
                  }
                  // For programme_Course
                  cmd.CommandText = "Select Program_name from Program_master where program_id='0' ";
                  string int2 = cmd.ExecuteScalar().ToString();
                  cmd.ExecuteNonQuery();
                  if (int2 == string.Empty | int2 == "")
                  {
                      cmd.CommandText = "Insert into program_master values(0,'NA','NA','0','Admin')";
                      cmd.ExecuteNonQuery();
                      cmd.Parameters.Clear();
                  }
                  //// ---Buildingamster
                  //cmd.CommandText = "Select Buildingname from buildingmaster where buildingid=0";
                  //int2 = cmd.ExecuteScalar().ToString();
                  //cmd.ExecuteNonQuery();
                  //if (int2 == string.Empty | int2 == "")
                  //{
                  //    cmd.CommandText = "Insert Into Buildingmaster Values(0,'0','NA','Admin')";
                  //    cmd.ExecuteNonQuery();
                  //    cmd.Parameters.Clear();
                  //}
                  //// FloorMaster
                  //cmd.CommandText = "Select floorname from floormaster where floorId=0 ";
                  //int2 = cmd.ExecuteScalar();
                  //cmd.ExecuteNonQuery();
                  //if (int2 == string.Empty | int2 == "")
                  //{
                  //    cmd.CommandText = "Insert into Floormaster Values(0,'0','NA','Admin')";
                  //    cmd.ExecuteNonQuery();
                  //    cmd.Parameters.Clear();
                  //}
                  //// RackMaster 
                  //cmd.CommandText = "Select RackName from Rackmaster where rackId=0 ";
                  //int2 = cmd.ExecuteScalar();
                  //cmd.ExecuteNonQuery();
                  //if (int2 == string.Empty | int2 == "")
                  //{
                  //    cmd.CommandText = "InSert Into Rackmaster Values(0,'0','NA','Admin')";
                  //    cmd.ExecuteNonQuery();
                  //    cmd.Parameters.Clear();
                  //}
                  //// Almira MAster
                  //cmd.CommandText = "Select AlmiraName from AlmiraMaster where Almiraid=0 ";
                  //int2 = cmd.ExecuteScalar();
                  //cmd.ExecuteNonQuery();
                  //if (int2 == string.Empty | int2 == "")
                  //{
                  //    cmd.CommandText = "Insert into AlmiraMaster Values(0,'0','NA','Admin')";
                  //    cmd.ExecuteNonQuery();
                  //    cmd.Parameters.Clear();
                  //}
                  // ------------------------------------for department master

                  cmd.CommandText = "Select departmentcode from departmentmaster where departmentcode='0' ";
                  string cod = cmd.ExecuteScalar().ToString();
                  cmd.ExecuteNonQuery();
                  cmd.Parameters.Clear();

                  if (cod == string.Empty)
                  {
                      cmd.CommandText = "Insert into departmentmaster values(0,'NA','NA',1,0,0,'Admin') ";
                      cmd.ExecuteNonQuery();
                      cmd.Parameters.Clear();
                  }
                  else
                  {
                      cmd.CommandText = "Select departmentcode from departmentmaster where departmentname='NA' ";
                      cod = cmd.ExecuteScalar().ToString();
                      cmd.ExecuteNonQuery();
                      cmd.Parameters.Clear();
                      if (cod == string.Empty)
                      {
                          cmd.CommandText = "Select max(departmentcode)+1 from departmentmaster  ";
                          int id = Convert.ToInt32( cmd.ExecuteScalar());
                          cmd.ExecuteNonQuery();
                          cmd.Parameters.Clear();

                          cmd.CommandText = "Insert into departmentmaster values(" + id + ",'NA','NA',1,0,0,'Admin') ";
                          cmd.ExecuteNonQuery();
                          cmd.Parameters.Clear();
                      }
                  }
              }
              catch (Exception ex)
              {
                  return;
              }
          }
          // kaushal : 01-Sept-2011(Part Payment)

        public void Insert_PartPaypent(int id, string user_id, string accession_no, decimal pay_amt, DateTime dt, string source, string Rec_no, string main_id, OleDbCommand cmd, OleDbConnection con)
        {
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "insert_Circpartpayment";

                cmd.Parameters.Add(new OleDbParameter("@id", OleDbType.Integer));
                cmd.Parameters["@id"].Value = id;

                cmd.Parameters.Add(new OleDbParameter("@user_id", OleDbType.VarWChar));
                cmd.Parameters["@user_id"].Value = user_id;

                cmd.Parameters.Add(new OleDbParameter("@Accession_no", OleDbType.VarWChar));
                cmd.Parameters["@Accession_no"].Value = accession_no;

                cmd.Parameters.Add(new OleDbParameter("@Pay_amt", OleDbType.Decimal));
                cmd.Parameters["@pay_amt"].Value = pay_amt;

                cmd.Parameters.Add(new OleDbParameter("@pay_date", OleDbType.Date));
                cmd.Parameters["@pay_date"].Value = dt.ToString("dd-MMM-yyyy");

                cmd.Parameters.Add(new OleDbParameter("@source", OleDbType.VarWChar));
                cmd.Parameters["@source"].Value = source;

                cmd.Parameters.Add(new OleDbParameter("@Rec_no", OleDbType.VarWChar));
                cmd.Parameters["@Rec_no"].Value = Rec_no;

                cmd.Parameters.Add(new OleDbParameter("@main_id", OleDbType.VarWChar));
                cmd.Parameters["@main_id"].Value = main_id;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message + "; Insert_PartPaypent failed");
            }
        }
        // ----------------

        // ************these routines r also in sqldataaccess


        // Public Sub sendmailaIssue(ByVal UserID As string, ByVal DataGrid1 As DataGrid, ByVal chkmaila As string, ByVal msgmailtype As string, ByVal refP As Page, ByVal myConnection As OleDbConnection)
        // Dim _user As New mailing()
        // Dim mailOper As New mailOperation()
        // _user = mailOper.CheckUser(UserID, chkmaila, myConnection)
        // If Not _user Is Nothing Then
        // 'If _user.MailT1 Is Nothing And _user.MailT2 Is Nothing Then
        // '    libobj1.MsgBox1(Resources.ValidationResources.NoRecFndInSpeciGroup.ToString, Me)
        // '    Exit Sub
        // 'End If
        // If _user.IsmailAllowed = "Y" And _user.IsemAllow = "1" Then   'pp
        // Dim accnolist As string = Nothing
        // Dim accnoloop As Integer
        // For accnoloop = 0 To DataGrid1.Items.Count - 1
        // If accnolist = Nothing Then
        // accnolist = Trim(DataGrid1.Items(accnoloop).Cells(0).Text)
        // Else
        // accnolist = accnolist & ", " & Trim(DataGrid1.Items(accnoloop).Cells(0).Text)
        // End If
        // Next
        // Dim message As string
        // message = "Dear Member," & "<br/>" & " " & msgmailtype & "<br/>" & "Member ID :" & " " & UserID & "<br/>" & "Accession number of issued item(s) :" & " " & accnolist & "<br/>" & " Thanking you " & "<br/>" & " Librarian"

        // Dim sendDate As Date = Date.Now()
        // If SmtpServer(myConnection) = True Then
        // If sendmailSmtp(_user.MailT1, _user.MailF, message, "Issued Item(s)", _user.MailT2, _user.SmtpAdd, _user.Uid, _user.Pwd, myConnection, "") = True Then
        // MsgBox1(Resources.ValidationResources.msgSendMail.ToString, refP)
        // Else
        // If InsertDeleteSEND(_user.MailT1, _user.MailF, message, "Issued Item(s)", _user.MailT2, _user.SmtpAdd, "Y", sendDate, myConnection) = True Then
        // 'missing to mail id
        // MsgBox1(Resources.ValidationResources.MailNotSentDueSerCF.ToString, refP)
        // End If
        // End If
        // Else
        // If InsertDeleteSEND(_user.MailT1, _user.MailF, message, "Issued Item(s)", _user.MailT2, _user.SmtpAdd, "Y", sendDate, myConnection) = True Then
        // MsgBox1(Resources.ValidationResources.MailNotSentDueSerCF.ToString, refP)
        // End If
        // End If
        // End If 'pp
        // Else
        // End If
        // _user = Nothing
        // mailOper = Nothing
        // End Sub
        // ----------By Kaushal 09-Aug-2011
        public void save_itemtype(OleDbConnection CategoryLoadingStatusConcon, string uid)
        {
            string item = string.Empty;
            OleDbCommand cmdk = new OleDbCommand();
            cmdk.CommandType = CommandType.Text;
            cmdk.Connection = CategoryLoadingStatusConcon;
            cmdk.CommandText = "Select item_type from Item_type where item_type ='Articles' ";
            item = cmdk.ExecuteScalar().ToString();
            cmdk.ExecuteNonQuery();
            int id = 0;
            if (item == "")
            {
                cmdk.CommandText = "Select coalesce(max(id),0) from Item_type";
                id = Convert.ToInt32(cmdk.ExecuteScalar());
                cmdk.ExecuteNonQuery();
                cmdk.CommandText = " insert into Item_type values('" + id + 1 + "','Articles','Arr','0x01010000','" + uid + "')";
                cmdk.ExecuteNonQuery();
            }

            // --------
            cmdk.CommandText = "Select item_type from Item_type where item_type ='Project Reports' ";
            item = cmdk.ExecuteScalar().ToString();
            cmdk.ExecuteNonQuery();
            if (item == "")
            {
                cmdk.CommandText = "Select coalesce(max(id),0) from Item_type";
                id = Convert.ToInt32(cmdk.ExecuteScalar());
                cmdk.ExecuteNonQuery();
                cmdk.CommandText = " insert into Item_type values('" + id + 1 + "','Project Reports','PR','0x01010000','" + uid + "')";
                cmdk.ExecuteNonQuery();
            }
            // ------------------
            cmdk.CommandText = "Select item_type from Item_type where item_type ='Thesis' ";
            item = cmdk.ExecuteScalar().ToString();
            cmdk.ExecuteNonQuery();
            if (item == "")
            {
                cmdk.CommandText = "Select coalesce(max(id),0) from Item_type";
                id = Convert.ToInt32(cmdk.ExecuteScalar());
                cmdk.ExecuteNonQuery();
                cmdk.CommandText = " insert into Item_type values('" + id + 1 + "','Thesis','ths','0x01010000','" + uid + "')";
                cmdk.ExecuteNonQuery();
            }
            // -------
            // cmdk.CommandText = "Select item_type from Item_type where item_type ='Journals' "
            // item = cmdk.ExecuteScalar()
            // cmdk.ExecuteNonQuery()
            // If item = "" Then

            // cmdk.CommandText = "Delete from Item_type where  "
            // id = cmdk.ExecuteScalar()
            // cmdk.ExecuteNonQuery()
            // cmdk.CommandText = " insert into Item_type values('" & id + 1 & "','Journals','jrnl','0x01010000','" & uid & "')"
            // cmdk.ExecuteNonQuery()
            // End If
            // ---


            cmdk.CommandText = "Select item_type from Item_type where item_type ='Books' ";
            item = cmdk.ExecuteScalar().ToString();
            cmdk.ExecuteNonQuery();
            if (item == "")
            {
                cmdk.CommandText = "Select coalesce(max(id),0) from Item_type";
                id = Convert.ToInt32(cmdk.ExecuteScalar());
                cmdk.ExecuteNonQuery();
                cmdk.CommandText = " insert into Item_type values('" + id + 1 + "','Books','bks','0x01010000','" + uid + "')";
                cmdk.ExecuteNonQuery();
            }
            // ---------
            cmdk.CommandText = "Select item_type from Item_type where item_type ='E-Books' ";
            item = cmdk.ExecuteScalar().ToString();
            cmdk.ExecuteNonQuery();
            if (item == "")
            {
                cmdk.CommandText = "Select coalesce(max(id),0) from Item_type";
                id = Convert.ToInt32(cmdk.ExecuteScalar());
                cmdk.ExecuteNonQuery();
                cmdk.CommandText = " insert into Item_type values('" + id + 1 + "','E-Books','E-Bks','0x01010000','" + uid + "')";
                cmdk.ExecuteNonQuery();
            }
        }
        /*public void OpenIssueSlip(string MembId, string MembName, DataTable MyTab, string IssueSlipUri, string constr)
        {
            Report report = new Report();
            StringBuilder HTMLStr = new StringBuilder();
            OleDbCommand cmd = new OleDbCommand();
            OleDbConnection con = new OleDbConnection(constr);

            string title;
            // Dim obj As New CampClass

            report.reportHeading = "Book Issue Slip";
            HTMLStr.Append("<table align='left' width='33%' bgcolor='whitesmoke'><tr ><td align='Center' style='font-family=arial;font-size:1em'>Issue Slip<td></tr></table></br>");

            // HTMLStr.Append("<div style='width:33%;background-color:whitesmoke;border:solid 1px #000;MARGIN-LEFT=-67%'><table border=0 cellpadding=0 cellspacing=1><tr><td width=40% colspan=2><b>ID:</b></td><td width=60%> " & MembId & "</td></tr>" & _
            // "<tr><td width=40% style='font:bold'>Date: </td><td width=60%> " & DateTime.Now().Date.ToString("MMM dd yyyy") & "</td></tr>" & _
            // "<tr ><td width=40% colspan='3'><b>Name:</b></td><td width=60%>" & MembName & "</td></tr><tr style='height:5px'><td colspan='4' ></td></tr>")

            HTMLStr.Append("<div style='width:33%;background-color:whitesmoke;border:solid 1px #000;MARGIN-LEFT=-67%'><table border=0 cellpadding=0 cellspacing=1><tr><td width=40% colspan=2><b>ID:</b><td width=60%> " + MembId + "</td></tr>" + "<tr><td width=40% colspan=2><b>Date:</b><td width=60%> " + DateTime.Now().Date.ToString("MMM dd yyyy") + "</td></tr>" + "<tr><td width=40% colspan=2><b>Name:</b><td width=60%> " + MembName + "</td></tr>");

            DataSet ds = new DataSet();
            // ds.Tables.Remove(MyTab)
            ds.Tables.Add(MyTab);
            report.ReportSource = ds;
            con.Open();
            foreach (DataRow rw in MyTab.Rows)
            {
                cmd.Parameters.Clear();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select booktitle from bookaccessionmaster where accessionnumber=N'" + rw.Item["accno"].ToString() + "'";
                title = cmd.ExecuteScalar();

                // HTMLStr.Append(" <tr ><td width=40% style='font:bold'>Acc.No: </td><td width=60% colspan='2' style='font:bold'>Title:</td></tr>" & _
                // "<tr><td width=40%>" & rw.Item("accno").ToString() & "</td><td width=60% colspan='3'>" & title & "</td></tr><tr style='height:5px'><td colspan='4' ></td></tr>")

                HTMLStr.Append("<tr><td width=40% colspan=2><b>Acc.No:</b><td width=60%> " + rw.Item["accno"].ToString() + "</td></tr>" + "<tr><td width=40% colspan='2' style='font:bold'>Title:</td><td width=60% colspan='3'>" + title + "</td></tr><tr style='height:5px'><td colspan='4' ></td></tr>");
            }
            HTMLStr.Append("<tr ><td colspan='4' style='text-align: right;'><hr height=1/><b>Total Issued:</b>" + MyTab.Rows.Count + "</td></tr></table></div>");
            report.ReportTitle = HTMLStr.ToString();

            if (File.Exists(IssueSlipUri) == false)
                File.Create(IssueSlipUri);
            report.SaveReport(IssueSlipUri);

            // ScriptManager.RegisterStartupScript(Me, GetType(Page), "aa", "Owin('IssueSlip.html');", True)

            ds.Dispose();
        }

        public void OpenIssueSlip_BackLog(string MembId, string MembName, DataTable MyTab, string dateBLog, string IssueSlipUri, string constr)
        {
            Report report = new Report();
            StringBuilder HTMLStr = new StringBuilder();
            OleDbCommand cmd = new OleDbCommand();
            OleDbConnection con = new OleDbConnection(constr);

            string title;
            // Dim obj As New CampClass

            report.reportHeading = "Book Issue Slip";
            HTMLStr.Append("<table align='left' width='33%' bgcolor='whitesmoke'><tr ><td align='Center' style='font-family=arial;font-size:1em'>Issue Slip<td></tr></table></br>");

            HTMLStr.Append("<div style='width:33%;background-color:whitesmoke;border:solid 1px #000;MARGIN-LEFT=-67%'><table border=0 cellpadding=0 cellspacing=1><tr><td width=40% colspan=2><b>ID:</b><td width=60%> " + MembId + "</td></tr>" + "<tr><td width=40% colspan=2><b>Date:</b><td width=60%> " + string.Format(Convert.ToDateTime(dateBLog), "MMM dd yyyy") + "</td></tr>" + "<tr><td width=40% colspan=2><b>Name:</b><td width=60%> " + MembName + "</td></tr>");
            // "<tr ><td style='font:bold' colspan='2'>Name:</td><td>" & MembName & "</td></tr><tr style='height:5px'><td colspan='4' ></td></tr>")

            DataSet ds = new DataSet();
            // ds.Tables.Remove(MyTab)
            ds.Tables.Add(MyTab);
            report.ReportSource = ds;
            con.Open();
            foreach (DataRow rw in MyTab.Rows)
            {
                cmd.Parameters.Clear();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select booktitle from bookaccessionmaster where accessionnumber=N'" + rw.Item["accno"].ToString() + "'";
                title = cmd.ExecuteScalar();

                HTMLStr.Append("<tr><td width=40% colspan=2><b>Acc.No:</b><td width=60%> " + rw.Item["accno"].ToString() + "</td></tr>" + "<tr><td width=40% colspan='2' style='font:bold'>Title:</td><td width=60% colspan='3'>" + title + "</td></tr><tr style='height:5px'><td colspan='4' ></td></tr>");
            }
            HTMLStr.Append("<tr ><td width=100% colspan='4' style='text-align: right;'><hr height=1/><b>Total Issued:</b>" + MyTab.Rows.Count + "</td></tr></table></div>");
            report.ReportTitle = HTMLStr.ToString();

            if (File.Exists(IssueSlipUri) == false)
                File.Create(IssueSlipUri);
            report.SaveReport(IssueSlipUri);

            // ScriptManager.RegisterStartupScript(Me, GetType(Page), "aa", "Owin('IssueSlip.html');", True)

            ds.Dispose();
        }
        // By Kaushal:03-03-2012
       */ // -------------------------------------
        /*      public void displayMsg(GridView grdMsg, string memberID, OleDbConnection con)
              {
                  string getMsg = string.Empty;
                  if (string.Trim(memberID) == string.Empty)
                      getMsg = "select cid,circularmessageposting.userid as postedby,Convert(VARCHAR(11), mesdate, 106) as mesdate,matter,subject from circularmessageposting where  verified=1";
                  else
                      getMsg = "select cid,circularmessageposting.userid as postedby,Convert(VARCHAR(11), mesdate, 106) as mesdate,matter,subject from circularmessageposting where (tomemberid in ('','" + memberID + "') and msgTypeId  in (1,2,3)) and verified=1 and ( effectiveFrom >='" + DateTime.Now.ToString("dd-MMM-yyyy") + "' and to_dt <='" + DateTime.Now.ToString("dd-MMM-yyyy") + "')";

                  DataSet dsmsg = new DataSet();
                  OleDbDataAdapter damsg = new OleDbDataAdapter(getMsg, con);
                  damsg.Fill(dsmsg);
                  System.Web.HttpContext.Current.Session["msgPost"] = dsmsg;
                  if (dsmsg.Tables[0].Rows.Count > 0)
                  {
                      grdMsg.DataSource = dsmsg;
                      grdMsg.DataBind();
                  }
                  else
                  {
                      grdMsg.DataSource = null;
                      grdMsg.DataBind();
                  }
                  damsg.Dispose();
                  dsmsg.Dispose();
              }
          */
        public string decrypt(string val, string seed)
        {
            byte[] KEY_64 = Convert.FromBase64String(seed);
            byte[] IV_64 = new byte[] { 55, 103, 246, 79, 36, 99, 167, 3 };
            if (val != string.Empty)
            {
                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                byte[] buffer = Convert.FromBase64String(val);
                MemoryStream ms = new MemoryStream(buffer);
                CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateDecryptor(KEY_64, IV_64), CryptoStreamMode.Read);
                StreamReader sr = new StreamReader(cs);
                return sr.ReadToEnd();
            }
            else
                return "";
        }
        /*     public void sendmailaIssue1(string UserID, OleDbConnection myConnection, string message, string chkmaila, string msgT, Page refP, string coltomId1, string coltomId2, string tabName, string filterCol)
             {
                 mailing _user = new mailing();
                 mailOperation mailOper = new mailOperation();
                 _user = mailOper.CheckUser(UserID, chkmaila, myConnection, coltomId1, coltomId2, tabName, filterCol);
                 if (!_user == null)
                 {
                     if (_user.IsmailAllowed == "Y" & _user.IsemAllow == "1")
                     {
                         // Dim message As string
                         // message = "Dear Member," & "<br/>" & " Issue details are as follows -" & "<br/>" & "Member ID :" & " " & Me.txtuseridBIE.Text & "" & "<br/>" & "Accession number of issued item(s) :" & " " & accnolist & "" & "<br/>" & " Thanking you " & "<br/>" & " Librarian"
                         DateTime sendDate = DateTime.Now();
                         if (SmtpServer(myConnection) == true)
                         {
                             if (sendmailSmtp(_user.MailT1, _user.MailF, message, msgT, _user.MailT2, _user.SmtpAdd, _user.Uid, _user.Pwd, myConnection, "") == true)
                                 MsgBox1(System.Resources.ValidationResources.msgSendMail.ToString, refP);
                             else if (InsertDeleteSEND(_user.MailT1, _user.MailF, message, msgT, _user.MailT2, _user.SmtpAdd, "Y", sendDate, myConnection) == true)
                                 // missing to mail id
                                 MsgBox1(System.Resources.ValidationResources.MailNotSentDueSerCF.ToString, refP);
                         }
                         else if (InsertDeleteSEND(_user.MailT1, _user.MailF, message, msgT, _user.MailT2, _user.SmtpAdd, "Y", sendDate, myConnection) == true)
                             MsgBox1(System.Resources.ValidationResources.MailNotSentDueSerCF.ToString, refP);
                     } // pp
                 }
                 else
                 {
                 }
                 _user = null
                 mailOper = null
             }
             public void sendmailaIssue2(string UserID, OleDbConnection myConnection, string message, string chkmaila, string msgT, Page refP, string coltomId1, string coltomId2, string tabName, string filterCol)
             {
                 mailing _user = new mailing();
                 mailOperation mailOper = new mailOperation();
                 _user = mailOper.CheckUser(UserID, chkmaila, myConnection, coltomId1, coltomId2, tabName, filterCol);
                 if (!_user == null)
                 {
                     if (_user.IsmailAllowed == "Y")
                     {
                         // Dim message As string
                         // message = "Dear Member," & "<br/>" & " Issue details are as follows -" & "<br/>" & "Member ID :" & " " & Me.txtuseridBIE.Text & "" & "<br/>" & "Accession number of issued item(s) :" & " " & accnolist & "" & "<br/>" & " Thanking you " & "<br/>" & " Librarian"
                         DateTime sendDate = DateTime.Now();
                         if (SmtpServer(myConnection) == true)
                         {
                             if (sendmailSmtp(_user.MailT1, _user.MailF, message, msgT, _user.MailT2, _user.SmtpAdd, _user.Uid, _user.Pwd, myConnection, "") == true)
                                 MsgBox1(System.Resources.ValidationResources.recsave.ToString + "," + System.Resources.ValidationResources.msgSendMail.ToString, refP);
                             else if (InsertDeleteSEND(_user.MailT1, _user.MailF, message, msgT, _user.MailT2, _user.SmtpAdd, "Y", sendDate, myConnection) == true)
                                 // missing to mail id
                                 MsgBox1(System.Resources.ValidationResources.recsave.ToString + "," + System.Resources.ValidationResources.MailNotSentDueSerCF.ToString, refP);
                         }
                         else if (InsertDeleteSEND(_user.MailT1, _user.MailF, message, msgT, _user.MailT2, _user.SmtpAdd, "Y", sendDate, myConnection) == true)
                             MsgBox1(System.Resources.ValidationResources.recsave.ToString + "," + System.Resources.ValidationResources.MailNotSentDueSerCF.ToString, refP);
                     } // pp
                 }
                 else
                 {
                 }
                 _user = null
                 mailOper = null
             }
      */
        public void populateGetData2(ListBox ctrlID, string sqlStr, bool optAdv, string txtCatVal, string lstAllCatTFld, string lstAllCatVFld, OleDbConnection Conn)
        {
            ListItem lstItem = new ListItem();
            if (optAdv == true & txtCatVal == "")
            {
                lstItem.Text = Resources.ValidationResources.EntrSrchCrt.ToString();
                lstItem.Value = "";
                ctrlID.Items.Insert(0, lstItem);
                ctrlID.SelectedIndex = -1;
            }
            else
            {
                OleDbCommand cmd = new OleDbCommand(sqlStr, Conn);
                OleDbDataReader genDr = cmd.ExecuteReader();
                if (genDr.HasRows)
                {
                    ctrlID.DataSource = genDr;
                    ctrlID.DataValueField = lstAllCatVFld;
                    ctrlID.DataTextField = lstAllCatTFld;
                    ctrlID.DataBind();
                }
                else
                {
                    lstItem.Text = Resources.ValidationResources.NRcdFound.ToString();
                    lstItem.Value = "";
                    ctrlID.Items.Insert(0, lstItem);
                    ctrlID.SelectedIndex = -1;
                }
                cmd.Dispose();
                genDr.Close();
            }
        }
        public void populateGetData2(ListBox ctrlID, string sqlStr, string lstAllCatTFld, string lstAllCatVFld, OleDbConnection Conn)
        {
            ListItem lstItem = new ListItem();
            OleDbCommand cmd = new OleDbCommand(sqlStr, Conn);
            OleDbDataReader genDr = cmd.ExecuteReader();
            if (genDr.HasRows)
            {
                ctrlID.DataSource = genDr;
                ctrlID.DataValueField = lstAllCatVFld;
                ctrlID.DataTextField = lstAllCatTFld;
                ctrlID.DataBind();
            }
            else
            {
                lstItem.Text = Resources.ValidationResources.NRcdFound.ToString();
                lstItem.Value = "";
                ctrlID.Items.Insert(0, lstItem);
                ctrlID.SelectedIndex = -1;
            }
            cmd.Dispose();
            genDr.Close();
        }
        // *********************
        public void PrepareMARC(int ctrlno, OleDbConnection cn)
        {
            try
            {
                DataSet ds1 = new DataSet();
                OleDbDataAdapter da1 = new OleDbDataAdapter("select distinct tag,tag_indicator from marc order by tag", cn);
                da1.Fill(ds1, "Unique");
                if (ds1.Tables["unique"].Rows.Count > 0)
                {
                    int i;
                    for (i = 0; i <= ds1.Tables["Unique"].Rows.Count - 1; i++)
                    {
                        string Tag;
                        string Indicator;
                        Tag = ds1.Tables["Unique"].Rows[i]["Tag"].ToString();
                        Indicator = ds1.Tables["Unique"].Rows[i]["Tag_indicator"].ToString();
                        DataSet ds2 = new DataSet();
                        OleDbDataAdapter da2 = new OleDbDataAdapter("select * from MARC where tag=N'" + ds1.Tables["unique"].Rows[i][0].ToString() + "' order by tag_subfield", cn);
                        da2.Fill(ds2, "Tags");
                        if (ds2.Tables["Tags"].Rows.Count > 0)
                        {
                            string subField;
                            string TagValue = string.Empty;
                            int iCounter;
                            for (iCounter = 0; iCounter <= ds2.Tables["Tags"].Rows.Count - 1; iCounter++)
                            {
                                string tagvalue1 = string.Empty;
                                subField = ds2.Tables["Tags"].Rows[iCounter]["tag_subField"].ToString();
                                DataSet ds3 = new DataSet();
                                OleDbDataAdapter da3 = new OleDbDataAdapter("select *  from marc_child where id=" + ds2.Tables["Tags"].Rows[iCounter]["id"].ToString() + " order by sequence", cn);
                                da3.Fill(ds3, "Child");
                                if (ds3.Tables["child"].Rows.Count > 0)
                                {
                                    int iCounter1;
                                    bool blnExists = false;
                                    for (iCounter1 = 0; iCounter1 <= ds3.Tables["child"].Rows.Count - 1; iCounter1++)
                                    {
                                        string tbl;
                                        string pFix;
                                        string fld;
                                        string sFix;
                                        string ConditionFld;
                                        tbl = ds3.Tables["Child"].Rows[iCounter1]["table_name"].ToString();
                                        fld = ds3.Tables["Child"].Rows[iCounter1]["Field_name"].ToString();
                                        pFix = ds3.Tables["Child"].Rows[iCounter1]["prefix"].ToString();
                                        sFix = ds3.Tables["Child"].Rows[iCounter1]["suffix"].ToString();
                                        ConditionFld = ds3.Tables["Child"].Rows[iCounter1]["ConditionFld"].ToString();
                                        DataSet ds4 = new DataSet();
                                        string CompareValue = string.Empty;
                                        CompareValue = ctrlno.ToString();
                                        OleDbDataAdapter da4 = new OleDbDataAdapter("select " + fld + " from " + tbl + " where " + ConditionFld + " ='" + CompareValue + "'", cn);
                                        da4.Fill(ds4, "data");
                                        if (ds4.Tables["data"].Rows.Count > 0)
                                        {
                                            if (Convert.ToString(ds4.Tables["data"].Rows[0][0]) != string.Empty)
                                            {
                                                tagvalue1 = tagvalue1 + pFix + ds4.Tables["data"].Rows[0][0].ToString() + sFix;
                                                blnExists = true;
                                            }
                                        }
                                    }
                                    if (blnExists == true)
                                    {
                                        OleDbCommand cmd = new OleDbCommand();
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Connection = cn;
                                        cmd.CommandText = "insert_MARC_Data_1";
                                        cmd.Parameters.Add(new OleDbParameter("@AccessionNumber_1", OleDbType.VarWChar));
                                        cmd.Parameters["@AccessionNumber_1"].Value = ctrlno;
                                        cmd.Parameters.Add(new OleDbParameter("@tag_no_2", OleDbType.VarWChar));
                                        cmd.Parameters["@tag_no_2"].Value = Tag;
                                        cmd.Parameters.Add(new OleDbParameter("@tag_indicator_3", OleDbType.VarWChar));
                                        cmd.Parameters["@tag_indicator_3"].Value = Indicator;
                                        cmd.Parameters.Add(new OleDbParameter("@tag_subField_4", OleDbType.VarWChar));
                                        cmd.Parameters["@tag_subField_4"].Value = subField;
                                        cmd.Parameters.Add(new OleDbParameter("@tag_value_5", OleDbType.VarWChar));
                                        cmd.Parameters["@tag_value_5"].Value = tagvalue1;
                                        cmd.ExecuteNonQuery();
                                        blnExists = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            finally
            {
                cn.Close();
            }
        }


        public string TitleCase(string thePhrase)
        {
            StringBuilder newString = new StringBuilder();
            StringBuilder nextString = new StringBuilder();
            string[] phraseArray;
            string theWord;
            string returnValue;
            // Dim len As string = thePhrase.Length.ToString()
            phraseArray = thePhrase.Split(null);
            for (int i = 0; i <= phraseArray.Length - 1; i++)
            {
                theWord = phraseArray[i].ToLower();
                if (theWord.Length > 1)
                {
                    if (theWord.Substring(1, 1) == "'")
                    {
                        // Process word with apostrophe at position 1 in 0 based string.
                        if (nextString.Length > 0)
                            nextString.Replace(nextString.ToString(), null);
                        nextString.Append(theWord.Substring(0, 1).ToUpper());
                        nextString.Append("'");
                        nextString.Append(theWord.Substring(2, 1).ToUpper());
                        nextString.Append(theWord.Substring(3).ToLower());
                        nextString.Append(" ");
                    }
                    else if (theWord.Length > 1 && theWord.Substring(0, theWord.Length) == theWord)
                    {
                        // Process McName.

                        // 
                        if (nextString.Length > 0)
                            nextString.Replace(nextString.ToString(), null);
                        if (theWord.Substring(0) == "of")
                        {
                            nextString.Append("of");
                            nextString.Append(" ");
                        }
                        else
                        {
                            // nextString.Append("Mc");
                            nextString.Append(theWord.Substring(0, 1).ToUpper());
                            nextString.Append(theWord.Substring(1).ToLower());
                            nextString.Append(" ");
                        }
                    }
                    else if (theWord.Length > 2 && theWord.Substring(0, theWord.Length) == theWord)
                    {
                        // Process MacName.
                        if (nextString.Length > 0)
                            nextString.Replace(nextString.ToString(), null);
                        if (theWord.Substring(0) == "of")
                        {
                            nextString.Append("of");
                            nextString.Append(" ");
                        }
                        else
                        {
                            // nextString.Append("Mac");
                            nextString.Append(theWord.Substring(0, 1).ToUpper());
                            nextString.Append(theWord.Substring(1).ToLower());
                            nextString.Append(" ");
                        }
                    }
                    else
                    {
                        // Process normal word (possible apostrophe near end of word.
                        if (nextString.Length > 0)
                            nextString.Replace(nextString.ToString(), null);
                        nextString.Append(theWord.Substring(0, theWord.Length).ToUpper());
                        nextString.Append(theWord.Substring(1).ToLower());
                        nextString.Append(" ");
                    }
                }
                else
                {
                    // Process normal single character length word.

                    if (nextString.Length > 0)
                        nextString.Replace(nextString.ToString(), null);
                    nextString.Append(theWord.ToUpper());

                    nextString.Append(" ");
                }
                newString.Append(nextString);
            }
            returnValue = newString.ToString();
            return returnValue.Trim();
        }
        /*
        public void populateDDLInput(object ctrlID, string qryString, string TextField, string ValueField, string setSelect, OleDbConnection cn)
        {
            DataSet ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter(qryString, cn);
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ctrlID.DataSource = ds.Tables[0];
                ctrlID.DataTextField = TextField;
                ctrlID.DataValueField = ValueField;
                ctrlID.DataBind();
            }
            else
                ctrlID.Items.Clear();
            ctrlID.Items.Add(setSelect);
            ctrlID.SelectedIndex = ctrlID.Items.Count - 1;
        }
        */
        public void MsgBox(string msg, Page refP)
        {
            Label lbl = new Label();
            lbl.Text = "<script language='javascript'>" + Environment.NewLine + "window.alert('" + msg + "')</script>";
            refP.Controls.Add(lbl);
        }
        public void MsgBox1(string msg, Page refP)
        {
            Label lbl = new Label();
            string lb = "window.alert('" + msg + "')";
            ScriptManager.RegisterClientScriptBlock(refP, refP.GetType(), "UniqueKey", lb, true);
            refP.Controls.Add(lbl);
        }
        public void ConfirmBox(string msg, Page refP)
        {
            Label lbl = new Label();
            string lb = "window.confirm('" + msg + "')";
            ScriptManager.RegisterClientScriptBlock(refP, refP.GetType(), "UniqueKey", lb, true);
            refP.Controls.Add(lbl);
        }
        public void setResult(DropDownList ddl, Label lbl, string lblText, string hValue, string setSelect)
        {
            if (ddl.SelectedItem.Text != setSelect)
                lbl.Text = "Price" + "(" + hValue + ")";
            else
                lbl.Text = lbl.Text;
        }
        // The function is created to check the existance of child record of a master table at the time of deletion of record
        public bool checkChildExistance(string idFld, string tablename, string condition, string connectString)
        {
            bool result;
            System.Data.OleDb.OleDbConnection chkCon = new System.Data.OleDb.OleDbConnection(connectString);
            chkCon.Open();
            OleDbDataAdapter chkda = new OleDbDataAdapter("select " + idFld + " from " + tablename + " where " + condition, chkCon);
            DataSet chkds = new DataSet();
            chkda.Fill(chkds, "result");
            chkCon.Close();
            chkCon.Dispose();
            chkda.Dispose();
            chkds.Dispose();
            if (chkds.Tables[0].Rows.Count > 0)
                result = true; // child exists
            else
                result = false;
            return result;
        }
        public bool checkChildExistancewc(string idFld, string tablename, string condition, OleDbConnection myConnection)
        {
            bool result;
//            OleDbDataAdapter chkda = new OleDbDataAdapter("select " + idFld + " from " + tablename + " where " + condition, myConnection);
            OleDbDataAdapter chkda = new OleDbDataAdapter("select count(*) from " + tablename + " where " + condition, myConnection);
            DataSet chkds = new DataSet();
            chkda.Fill(chkds, "result");
            string qer = "select count(*) from " + tablename + " where " + condition;
            OleDbCommand cmd = new OleDbCommand(qer, myConnection);
            cmd.CommandTimeout = 120;
            var cn=Convert.ToInt16( cmd.ExecuteScalar());
            result = false;
            if (cn > 0)
                result = true;
            chkda.Dispose();
            chkds.Dispose();
            return result;
        }
        public void fillIssueGrid(DataGrid g, OleDbConnection cn, string mid)
        {
            DataSet ds = new DataSet();
            // Dim da As New OleDbDataAdapter("select  from circclassmaster where circclassmaster.classname=circusermanagement.classname and circusermanagement.userid='" & mid & "'", cn)
            OleDbDataAdapter da = new OleDbDataAdapter("select categoryloadingstatus.Id,categoryloadingstatus.Category_LoadingStatus,classmasterloadingstatus.totalissueddays,classmasterloadingstatus.noofbookstobeissued, alreadyissued=0 from categoryloadingstatus,classmasterloadingstatus,circusermanagement where classmasterloadingstatus.classname=circusermanagement.classname and  classmasterloadingstatus.loadingstatus=categoryloadingstatus.Id and  circusermanagement.userid=N'" + mid + "'", cn);
            da.Fill(ds, "cat");
            if (ds.Tables["cat"].Rows.Count > 0)
            {
                g.DataSource = ds.Tables["Cat"];
                g.DataBind();
                int i;
                for (i = 0; i <= g.Items.Count - 1; i++)
                {
                    DataSet ds1 = new DataSet();
                    da.Dispose();

                    // 'follwo is giving wrong result
                    // da = New OleDbDataAdapter("select issueCount from MemberIssueCategoryWise where category_id=" & Val(g.Items(i).Cells(0).Text) & " and memberid=N'" & mid & "'", cn)
                    // da.Fill(ds1, "issued")
                    string Qr = "select COUNT(*) from CircIssueTransaction a,bookaccessionmaster b";
                    Qr += " where a.accno=b.accessionnumber and b.ItemCategoryCode=" + g.Items[i].Cells[0].Text;
                    Qr += "  and a.userid='" + mid + "' and a.status='Issued'";
                    OleDbCommand oCom = new OleDbCommand();
                    oCom.CommandTimeout = 200;
                    oCom.Connection = cn;
                    oCom.CommandType = CommandType.Text;
                    oCom.CommandText = Qr;
                    OleDbDataAdapter Oda = new OleDbDataAdapter(oCom);

                    Oda.Fill(ds1);

                    g.Items[i].Cells[3].Text = ds1.Tables[0].Rows[0][0].ToString();
                    // If ds1.Tables(0).Rows(0)(0) > 0 Then
                    // g.Items(i).Cells(3).Text = ds1.Tables("issued").Rows(0).Item(0)
                    // Else
                    // g.Items(i).Cells(3).Text = "0"
                    // End If
                    ds1.Dispose();
                }
            }
            ds.Dispose();
            da.Dispose();
        }

        public void populateAfterDeletion(DataGrid Grid, string qryString, OleDbConnection cn)
        {
            // Dim connStr As New OleDb.OleDbConnection(ConnectionStrings("ConnectionString").ConnectionString)
            // connStr.Open()
            int count_value, f, pg;
            pg = Grid.PageSize;
            f = Grid.CurrentPageIndex;
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter(qryString, cn);
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                count_value = ds.Tables[0].Rows.Count;
                if (f < count_value / pg)
                    Grid.CurrentPageIndex = f;
                else
                    Grid.CurrentPageIndex = count_value / pg - 1;
                Grid.DataSource = ds.Tables[0].DefaultView;
                Grid.DataBind();
            }
            else
            {
                Grid.DataSource = dt;
                Grid.DataBind();
            }
            da.Dispose();
            ds.Dispose();
        }
        public void populateGridView(GridView Grid, string qryString, OleDbConnection cn)
        {
            // Dim connStr As New OleDb.OleDbConnection(ConnectionStrings("ConnectionString").ConnectionString)
            // connStr.Open()
            int count_value, f, pg;
            pg = Grid.PageSize;
            f = Grid.PageIndex;
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter(qryString, cn);
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                count_value = ds.Tables[0].Rows.Count;
                Grid.DataSource = ds.Tables[0].DefaultView;
                Grid.DataBind();
            }
            else
            {
                Grid.DataSource = dt;
                Grid.DataBind();
            }
            da.Dispose();
            ds.Dispose();
        }

        /*   public double GetMaximumNoIncrementedByOne(string strSql, OleDbConnection conMaximum)
           {
               OleDbCommand comMax = new OleDbCommand();
               try
               {
                   comMax.Connection = conMaximum;
                   comMax.CommandType = CommandType.Text;
                   comMax.CommandText = strSql;
                   double dblMax;
                   dblMax = comMax.ExecuteScalar();
                   return dblMax;
               }
               finally
               {
                   comMax.Dispose();
               }
           }
      */
        public bool ValidateAccPrefix(string str)
        {
            int j = 0;
            bool flag = false;
            bool flag1 = false;
            int count = str.Length;
            for (j = 0; j <= count - 1; j++)
            {
                int x = int.Parse(str.Substring(j, 1));
                if (j == 0)
                {
                    if (x >= 48 & x <= 57)
                        flag = true;
                }
                else if (flag == true)
                {
                    if (x < 48 | x > 57)
                    {
                        return false;
                    }
                }
                else if (j <= 3 & flag1 == false)
                {
                    if (x >= 48 & x <= 57)
                        flag1 = true;
                }
                else if (j <= 3 & flag1 == true)
                {
                    if ((x >= 65 & x <= 90) || (x >= 97 & x <= 122))
                    {
                        return false;
                    }
                }
                else if (j > 3 || flag1 == true)
                {
                    if ((x >= 65 & x <= 90) | (x >= 97 & x <= 122))
                    {
                        return false;

                    }
                }
            }
            return true;
        }
        public bool ValidateAccNo(string AccNumber)
        {
            System.Array arr5;
            arr5 = AccNumber.Split(',');
            int counter = 0;
            int i1 = 0;
            counter = arr5.Length;
            bool flag = false;
            bool flag1 = false;
            for (i1 = 0; i1 <= counter - 1; i1++)
            {
                int count = 0;
                int j = 0;
                int cntsuf = 0;
                string tag = "p";
                string str = "";
                count = arr5.GetValue(i1).ToString().Length;
                str = arr5.GetValue(i1).ToString();

                if (count < 4)
                {
                    return false;

                }

                string chk0 = str.Replace("0", "");
                if (chk0 == null)
                {
                    return false;
                }
                for (j = 0; j <= count - 1; j++)
                {
                    int x = int.Parse(str.Substring(j, 1));
                    if (j == 0)
                    {
                        if (x >= 48 & x <= 57)
                            flag = true;
                    }
                    else if (flag == true)
                    {
                        if (j == count - 1)
                        {
                            if (x >= 48 & x <= 57)
                                flag = true;
                        }
                        else if (x < 48 || x > 57)
                        {
                            return false;

                        }
                    }
                    else if (j <= 3 & flag1 == false)
                    {
                        if (x >= 48 & x <= 57)
                            flag1 = true;
                    }
                    else if (j <= 3 & flag1 == true)
                    {
                        if ((x >= 65 & x <= 90) || (x >= 97 & x <= 122))
                        {
                            tag = "p";
                            return false;

                        }
                    }
                    else if (j > 3 | flag1 == true)
                    {
                        if (((tag == "p") & x >= 65 & x <= 90) | (x >= 97 & x <= 122))
                        {
                            return false;

                        }
                        else if (((tag == "m" | tag == "s") & x >= 65 & x <= 90) | (x >= 97 & x <= 122))
                        {
                            cntsuf = cntsuf + 1;
                            tag = "s";
                            if (tag == "s" & cntsuf > 4)
                            {
                                return false;
                            }
                        }
                        else
                        {
                            tag = "m";
                            cntsuf = 0;
                        }
                    }
                }
                flag1 = false;
                flag = false;
            }
            return true;
        }

        public bool ValidateAccNoRange(string AccNumber)
        {
            System.Array arr;
            string f, l;
            string chk = string.Empty;
            int i, j;
            arr = AccNumber.Split('-');
            f = arr.GetValue(0).ToString  ();

            if (arr.Length <= 1)
            {
                return false;

            }
            else if (arr.Length > 2)
            {
                return false;

            }

            for (j = 0; j <= 1; j++)
            {
                int x = int.Parse(arr.GetValue(j).ToString());
                for (i = 0; i <= arr.GetValue(j).ToString().Length - 1; i++)
                {
                    if (x < 48 || x > 57)
                    {
                        return false;
                    }
                }
                chk = arr.GetValue(j).ToString().Replace("0", "");
                if (chk == string.Empty)
                {
                    return false;

                }
            }

            if (Convert.ToInt32(arr.GetValue(0)) >= Convert.ToInt32(arr.GetValue(1)))
            {
                return false;
            }

            return true;
        }

        public string AccNoRange(int first, int last, string prefix)
        {
            int i = 0;
            int j = 0;
            string temp = string.Empty;
            string str1 = string.Empty;
            int Pre = 0;
            Pre = prefix.Length;
            for (i = first; i <= last; i++)
            {
                for (j = i.ToString().Length + 1 + Pre; j <= 4; j++)
                    temp = temp + "0";
                str1 = str1 + prefix + temp + i + ",";
                temp = string.Empty;
            }
            str1 =  str1.Substring(0, str1.Length - 1);
            return str1;
        }

        // *********************JITENDRA DWIVEDI/Date 09/JAN/2008
  /*      public bool SmtpServer(OleDbConnection cn)
        {
            // Dim server As System.Net.Mail.SmtpAccess
            bool flag = true;
            string ServerName;
            System.Data.OleDb.OleDbDataAdapter Serverad = new System.Data.OleDb.OleDbDataAdapter("Select smptp_IPadd from librarysetupinformation ", cn);
            DataSet ds = new DataSet();
            Serverad.Fill(ds, "server");
            ServerName = ds.Tables["server"].Rows[0].Item["smptp_IPadd"];
            System.Net.Sockets.TcpClient Server = new System.Net.Sockets.TcpClient();
            try
            {
                Server.Connect(ServerName, 25);
                if (Server.Connected())
                    return flag;
            }
            catch (Exception ex)
            {
                flag = false;
                return flag;
            }
        }
*/
        // *************JITENDRA DWIVEDI/Date 09/JAN/2008
   /*     public bool sendmailSmtp(string mailTo, string mailFrom, string msgBody, string msgSubject, string toCC, string smtp, string uid, string pwd, OleDbConnection cn, string AttachmentPath)
        {
            // Namespace used System.Net.Mail.MailMessage in place of System.Web.Mail.MailMessage & System.Net.Mail.SmtpClient in place of System.Web.Mail.SmtpMail
            bool SendBool = true;
            MailMessage mymessage = new MailMessage();
            SmtpClient SmtpMail = new SmtpClient();
            MailAddress fromAddress = new MailAddress(mailFrom);
            NetworkCredential credential = new NetworkCredential(uid, pwd);
            // specify the host name or ipaddress of server,Default in IIS will be localhost 
            try
            {
                SmtpMail.Host = smtp;
                // Default port will be 25
                SmtpMail.Port = 25;
                SmtpMail.Credentials = credential;
                mymessage.From = fromAddress;
                mymessage.To.Add(mailTo);
                if (string.Trim(toCC) != string.Empty)
                    mymessage.CC.Add(toCC);
                mymessage.Subject = msgSubject;
                // Body can be Html or text format
                mymessage.IsBodyHtml = true;
                mymessage.Body = msgBody;
                if (!AttachmentPath == "")
                    mymessage.Attachments.Add(new Attachment(AttachmentPath));
                SmtpMail.Send(mymessage);
            }
            catch (Exception ex)
            {
                string p;
                p = ex.Message;
                SendBool = false;
                return SendBool;
            }
            return SendBool;
        }
 */       // *****************************************
        // by aamir 20-11-2010

  /*      public bool sendMail(string mailTo, string cc, string subject, string body, string attachments = "")
        {
            bool flag;
            try
            {
                string constr = ConfigurationManager.ConnectionStrings(System.Web.HttpContext.Current.Session["LibWiseDBConn"]).ToString();
                OleDbConnection conobj = new OleDbConnection(constr);
                conobj.Open();
                OleDbCommand cmdobj = new OleDbCommand("select * from librarysetupinformation", conobj);
                OleDbDataReader drdobj;
                drdobj = cmdobj.ExecuteReader();
                string iphost, senderUser, senderPwd;
                int ipport;
                while ((drdobj.Read()))
                {
                    iphost = drdobj["smptp_Ipadd"].ToString();
                    ipport = Convert.ToInt32(drdobj["smtp_Port"]);
                    senderUser = drdobj["iUser"].ToString();
                    senderPwd = drdobj["iPwd"].ToString();
                }
                conobj.Close();
                MailMessage mymessage = new MailMessage();
                mymessage.Subject = subject;
                mymessage.Body = body;
                mymessage.IsBodyHtml = true;
                mymessage.From = new MailAddress(mailTo);
                mymessage.To.Add(new MailAddress(mailTo));
                if (cc != "")
                    mymessage.CC.Add(cc);
                if (attachments != "")
                {
                    Attachment attachedfile = new Attachment(attachments);
                    mymessage.Attachments.Add(attachedfile);
                }
                SmtpClient myc = new SmtpClient();
                NetworkCredential credentials = new NetworkCredential(senderUser, senderPwd);
                myc.Host = iphost;
                myc.Port = ipport;
                myc.DeliveryMethod = SmtpDeliveryMethod.Network;
                myc.EnableSsl = true;
                myc.Credentials = credentials;
                myc.Send(mymessage);
                flag = true;
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return flag;
        }
*/
  /*      public void sendMailToTable(string tablename, string columnname, string userid, string subject, string body, Page emailPage, string mailfirst, string mailsecond)
        {
            string mailto, cc;
            string newbody = body.Replace("<br/>", @"\r\n");
            string constr = ConfigurationManager.ConnectionStrings(System.Web.HttpContext.Current.Session["LibWiseDBConn"]).ToString();
            OleDbConnection con = new OleDbConnection(constr);
            con.Open();

            mailto = mailfirst;
            cc = mailsecond;

            subject = subject.Replace("'", "");
            body = body.Replace("'", "");
            string cmdstr = "Insert Into MailStatus(MailTo,CC,Subject,Body,EmailPage,Status,MailGenerateDT) Values('" + mailto + "','" + cc + "','" + subject + "','" + newbody + "','" + emailPage.ToString() + "','P','" + System.DateTime.Now.ToString("dd-MMM-yyyy") + "')";
            OleDbCommand cmd = new OleDbCommand(cmdstr, con);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            con.Close();
        }
*/
     /*   public bool sendMailToTable(string subject, string body, Page emailPage, string mailfirst, string mailsecond)
        {
            bool flag = false;
            try
            {
                string mailto, cc;
                mailto = mailfirst;
                cc = mailsecond;
                string constr = ConfigurationManager.ConnectionStrings(System.Web.HttpContext.Current.Session["LibWiseDBConn"]).ToString();
                OleDbConnection con = new OleDbConnection(constr);
                con.Open();
                subject = subject.Replace("'", "");
                body = body.Replace("'", "");
                string cmdstr = "Insert Into MailStatus(MailTo,CC,Subject,Body,EmailPage,Status,MailGenerateDT) Values('" + mailto + "','" + cc + "','" + subject + "','" + body + "','" + emailPage.ToString() + "','P','" + System.DateTime.Now.ToString() + "')";
                OleDbCommand cmd = new OleDbCommand(cmdstr, con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                con.Close();
                flag = true;
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return flag;
        }
   */     // By aamir 25-11-2011
        // --------------------------------------------------------------------------------------------------------------------
  /*      public int CheckSMSPageEnable(string PageName)
        {
            int flag = 0;

            PageName = PageName.Substring(PageName.IndexOf(".") + 1);
            PageName = PageName.Replace("_", ".").ToLower();

            DataSet ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter("select * from SMSEnablePages where Lower(PageName)='" + PageName + "'", ConfigurationManager.ConnectionStrings(System.Web.HttpContext.Current.Session["LibWiseDBConn"]).ToString());
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
                flag = Convert.ToInt32(ds.Tables[0].Rows[0]["Status"]);
            return flag;
        }
*/
       /* public DataSet GetQueryResult(string query, string TableName)
        {
            string constr = ConfigurationManager.ConnectionStrings(System.Web.HttpContext.Current.Session["LibWiseDBConn"]).ToString();
            OleDbDataAdapter da = new OleDbDataAdapter(query, constr);
            DataSet ds = new DataSet();
            da.Fill(ds, TableName);
            return ds;
        }
        */
   /*     public bool sendSMSToTable(string SMSMessage, string SenderUser, string SendTo, Page PageName)
        {
            bool flag = false;
            try
            {
                OleDbConnection con = new OleDbConnection(ConfigurationManager.ConnectionStrings(System.Web.HttpContext.Current.Session["LibWiseDBConn"]).ToString());
                con.Open();

                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "insert_SMSMessage";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 320;
                cmd.Connection = con;

                cmd.Parameters.Add("@Message", OleDbType.VarChar).Value = SMSMessage;
                cmd.Parameters.Add("@SendDate", OleDbType.Date).Value = DateTime.Now.ToString();
                cmd.Parameters.Add("@SendBy", OleDbType.VarChar).Value = SenderUser;
                cmd.Parameters.Add("@SendTo", OleDbType.VarChar).Value = SendTo;
                cmd.Parameters.Add("@Status", OleDbType.VarChar).Value = "P";
                string PN = PageName.ToString().Substring(PageName.ToString().IndexOf(".") + 1);
                cmd.Parameters.Add("@PageName", OleDbType.VarChar).Value = PN.ToString().Replace("_", ".");

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
        */

      /*  public bool sendScheduleSMSToTable(string SMSMessage, string SenderUser, string SendTo, Page PageName, DateTime ScheduleDate, string ScheduleGroup)
        {
            bool flag = false;
            try
            {
                OleDbConnection con = new OleDbConnection(ConfigurationManager.ConnectionStrings(System.Web.HttpContext.Current.Session["LibWiseDBConn"]).ToString());
                con.Open();

                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "insert_ScheduleSMS";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 320;
                cmd.Connection = con;

                cmd.Parameters.Add("@Message", OleDbType.VarChar).Value = SMSMessage;
                cmd.Parameters.Add("@SendDate", OleDbType.Date).Value = DateTime.Now.ToString();
                cmd.Parameters.Add("@SendBy", OleDbType.VarChar).Value = SenderUser;
                cmd.Parameters.Add("@SendTo", OleDbType.VarChar).Value = SendTo;
                cmd.Parameters.Add("@Status", OleDbType.VarChar).Value = "S";
                string PN = PageName.ToString().Substring(PageName.ToString().IndexOf(".") + 1);
                cmd.Parameters.Add("@PageName", OleDbType.VarChar).Value = PN.ToString().Replace("_", ".");
                cmd.Parameters.Add("@ScheduleDate", OleDbType.Date).Value = ScheduleDate;
                cmd.Parameters.Add("@ScheduleGroup", OleDbType.VarChar).Value = ScheduleGroup;
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
        */
      /*  public bool sendScheduleEmailToTable(string Subject, string EmailMessage, string SendTo, Page PageName, DateTime ScheduleDate, string ScheduleGroup)
        {
            bool flag = false;
            try
            {
                OleDbConnection con = new OleDbConnection(ConfigurationManager.ConnectionStrings(System.Web.HttpContext.Current.Session["LibWiseDBConn"]).ToString());
                con.Open();

                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "insert_ScheduleEmail";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 320;
                cmd.Connection = con;

                cmd.Parameters.Add("@Subject", OleDbType.VarChar).Value = Subject;
                cmd.Parameters.Add("@Body", OleDbType.VarChar).Value = EmailMessage;
                cmd.Parameters.Add("@mailGenerateDT", OleDbType.Date).Value = DateTime.Now.ToString();
                cmd.Parameters.Add("@MailTo", OleDbType.VarChar).Value = SendTo;
                cmd.Parameters.Add("@Status", OleDbType.VarChar).Value = "S";
                string PN = PageName.ToString().Substring(PageName.ToString().IndexOf(".") + 1);
                cmd.Parameters.Add("@EmailPage", OleDbType.VarChar).Value = PN.ToString().Replace("_", ".");
                cmd.Parameters.Add("@ScheduleDate", OleDbType.Date).Value = ScheduleDate;
                cmd.Parameters.Add("@ScheduleGroup", OleDbType.VarChar).Value = ScheduleGroup;
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
        */
        // --------------------------------------------------------------------------------------------------------------------
        // *****************************************

        // *****JITENDRA DWIVEDI/Date 09/JAN/2008
     /*   public bool InsertDeleteSEND(string mailTo, string mailFrom, string msgBody, string msgSubject, string toCC, string smtp, string Flag, DateTime sendDate, OleDbConnection cn)
        {
            string tempId;
            bool ReturnFlg = true;
            string tmpstr = string.Empty;
            tmpstr = populateCommandText("select coalesce(max(userid),0,max(userid)) from BouncemailRec", cn);
            tempId = Interaction.IIf(Conversion.Val(tmpstr) == 0, 1, Conversion.Val(tmpstr) + 1);
            OleDbCommand SendCmd = new OleDbCommand();
            SendCmd.Connection = cn;
            SendCmd.CommandType = CommandType.StoredProcedure;
            SendCmd.CommandText = "insert_BouncemailRec_1";

            SendCmd.Parameters.Add("@eMailId_1", OleDbType.VarWChar);
            SendCmd.Parameters["@eMailId_1"].Value = mailTo;

            SendCmd.Parameters.Add("@fromEmail_2", OleDbType.VarWChar);
            SendCmd.Parameters["@fromEmail_2"].Value = mailFrom;

            SendCmd.Parameters.Add("@message_3", OleDbType.VarWChar);
            SendCmd.Parameters["@message_3"].Value = msgBody;

            SendCmd.Parameters.Add("@Status_4", OleDbType.VarWChar);
            SendCmd.Parameters["@Status_4"].Value = msgSubject;

            SendCmd.Parameters.Add("@eMailId1_5", OleDbType.VarWChar);
            SendCmd.Parameters["@eMailId1_5"].Value = toCC;

            SendCmd.Parameters.Add("@smtpAdd_6", OleDbType.VarWChar);
            SendCmd.Parameters["@smtpAdd_6"].Value = smtp;


            SendCmd.Parameters.Add("@userid_7", OleDbType.VarWChar);
            SendCmd.Parameters["@userid_7"].Value = tempId;

            SendCmd.Parameters.Add("@flag_8", OleDbType.VarWChar);
            SendCmd.Parameters["@flag_8"].Value = Flag;


            SendCmd.Parameters.Add("@sendDate", OleDbType.Date);
            SendCmd.Parameters["@sendDate"].Value = sendDate;
            try
            {
                SendCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string msg;
                msg = ex.Message;
                ReturnFlg = false;
                return ReturnFlg;
            }
            return ReturnFlg;
        }
        */
        // jitendra
        /// <summary>
        ///         ''' To Split Database Field
        ///         ''' </summary>
        ///         ''' <param name="cn">Connection</param>
        ///         ''' <param name="Delimiter">Delimiter</param>
        ///         ''' <param name="Tablename">Table Name</param>
        ///         ''' <param name="Datafield">Database Field</param>
        ///         ''' <param name="DatafiledToupdate">Database Field To Update</param>
        ///         ''' <param name="IdColumn">ID According to which Field updated</param>
        ///         ''' <returns>Null</returns>
        ///         ''' <remarks>Jitendra</remarks>
    /*    public string SplitDatabaseField(OleDbConnection cn, string Delimiter, string Tablename, string Datafield, string DatafiledToupdate, string IdColumn)
        {
            DataSet ds = new DataSet();
            string Qry = "Select " + Datafield + "," + DatafiledToupdate + "," + IdColumn + " from " + Tablename;
            OleDbDataAdapter da = new OleDbDataAdapter(Qry, cn);
            OleDbCommand com = new OleDbCommand();
            da.Fill(ds, "cat");
            int rowcount;
            string SQLstr;
            System.Array arr;
            for (rowcount = 0; rowcount <= ds.Tables[0].Rows.Count - 1; rowcount++)
            {
                arr = string.Split(ds.Tables[0].Rows[rowcount].ItemArray[0], Delimiter[0], '-1', 0);
                if ((arr.Length == 2))
                {
                    SQLstr = "Update " + Tablename + " Set " + DatafiledToupdate + "='" + arr.GetValue(1) + "'" + " ," + Datafield + "='" + arr.GetValue(0) + "'" + "where " + IdColumn + "=" + ds.Tables[0].Rows[rowcount].ItemArray[2].ToString();
                    com.CommandType = CommandType.Text;
                    com.CommandText = SQLstr;
                    com.Connection = cn;
                    com.ExecuteNonQuery();
                }
                SQLstr = string.Empty;
            }
        }
        */
        // ------
    /*    public DataView populategridpageIndex(string sqlStr, string tableName, OleDbConnection myConnection)
        {
            DataSet Classmasterds = new DataSet();
            Classmasterds = PopulateDataset(sqlStr, tableName, myConnection);
            DataTable dt = Classmasterds.Tables[tableName];
            DataView dv = new DataView(dt);
            return dv;
            Classmasterds.Dispose();
        }*/
        public string populateCommandText(string sqlStr, System.Data.OleDb.OleDbConnection connection)
        {
            OleDbCommand CategoryLoadingStatuscom3 = new OleDbCommand();
            CategoryLoadingStatuscom3.Connection = connection;
            CategoryLoadingStatuscom3.CommandType = CommandType.Text;
            CategoryLoadingStatuscom3.CommandText = sqlStr;
            CategoryLoadingStatuscom3.CommandTimeout = 120;
            string tmpstr;
            tmpstr = CategoryLoadingStatuscom3.ExecuteScalar().ToString();
            return tmpstr;
        }
        public void populateDDL(DropDownList ctrlID, string qryString, string TextField, string ValueField, string setSelect, OleDbConnection cn)
        {
            // Dim connStr As New OleDb.OleDbConnection(ConnectionStrings("ConnectionString").ConnectionString)
            // connStr.Open()
            DataSet ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter(qryString, cn);
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ctrlID.DataSource = ds.Tables[0];
                ctrlID.DataTextField = TextField;
                ctrlID.DataValueField = ValueField;
                ctrlID.DataBind();
            }
            else
                ctrlID.Items.Clear();
            ctrlID.Items.Add(setSelect);
            ctrlID.SelectedIndex = ctrlID.Items.Count - 1;
        }
        public void populateDDL2(DropDownList ctrlID, DataSet ds, string TextField, string ValueField, string setSelect, OleDbConnection cn)
        {
            // Dim connStr As New OleDb.OleDbConnection(ConnectionStrings("ConnectionString").ConnectionString)
            // connStr.Open()
            // Dim ds As New DataSet
            // Dim da As New OleDbDataAdapter(qryString, cn)
            // da.Fill(ds)
            if (ds.Tables[0].Rows.Count > 0)
            {
                ctrlID.DataSource = ds.Tables[0];
                ctrlID.DataTextField = TextField;
                ctrlID.DataValueField = ValueField;
                ctrlID.DataBind();
            }
            else
                ctrlID.Items.Clear();
            ctrlID.Items.Add(setSelect);
            ctrlID.SelectedIndex = ctrlID.Items.Count - 1;
        }

        public void populateLstBox(ListBox ctrlID, string qryString, string TextField, string ValueField, string setSelect, OleDbConnection myConnection)
        {
            DataSet ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter(qryString, myConnection);
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ctrlID.DataSource = ds.Tables[0];
                ctrlID.DataTextField = TextField;
                ctrlID.DataValueField = ValueField;
                ctrlID.DataBind();
            }
            else
            {
                ctrlID.Items.Clear();
                ctrlID.Items.Add(setSelect);
                ctrlID.SelectedIndex = ctrlID.Items.Count - 1;
            }
            // ctrlID.Items.Add(setSelect)
            // ctrlID.SelectedIndex = ctrlID.Items.Count - 1
            da.Dispose();
            ds.Dispose();
        }
        public void populateLstBox1(ListBox ctrlID, string qryString, string TextField, string ValueField, OleDbConnection myConnection)
        {
            // Dim connStr As New OleDb.OleDbConnection(ConnectionStrings("ConnectionString").ConnectionString)
            // connStr.Open()
            DataSet ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter(qryString, myConnection);
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ctrlID.DataSource = ds.Tables[0];
                ctrlID.DataTextField = TextField;
                ctrlID.DataValueField = ValueField;
                ctrlID.DataBind();
            }
            else
                ctrlID.Items.Clear();
            da.Dispose();
            ds.Dispose();
        }
        public bool isDulicate(string sqlStr, System.Data.OleDb.OleDbConnection connection)
        {
            DataSet duplicateCom = new DataSet();
            OleDbDataAdapter duplicateda = new OleDbDataAdapter(sqlStr, connection);
            duplicateda.Fill(duplicateCom, "ch");
            if (duplicateCom.Tables["ch"].Rows.Count > 0)
                return true;
            else
                return false;
            duplicateCom.Dispose();
            duplicateda.Dispose();
        }
        public bool isIssuable(string accNo, string userid, System.Data.OleDb.OleDbConnection connection)
        {
            string className = string.Empty;
            int cat = 0;
            DataSet ds = new DataSet();
            System.Data.OleDb.OleDbDataAdapter daClass =
                new System.Data.OleDb.OleDbDataAdapter("select classname from circusermanagement where userid=N'" + userid + "'", connection);
            daClass.Fill(ds, "Class");
            className = ds.Tables["Class"].Rows[0][0].ToString();
            // Dim daCatgory As New OleDb.OleDbDataAdapter("select booktype from bookcatalog,bookaccessionmaster where bookaccessionmaster.ctrl_no =  bookcatalog.ctrl_no and bookaccessionmaster.accessionnumber=N'" & accNo & "'", connection)
            System.Data.OleDb.OleDbDataAdapter daCatgory 
                = new System.Data.OleDb.OleDbDataAdapter("select ItemCategoryCode from bookaccessionmaster where accessionnumber=N'" + accNo + "'", connection);
            daCatgory.Fill(ds, "Catgory");
            cat =Convert.ToInt32( ds.Tables["Catgory"].Rows[0][0]);
            System.Data.OleDb.OleDbDataAdapter catstatus = new System.Data.OleDb.OleDbDataAdapter("select policystatus from CircClassMaster where classname=N'" + className + "'", connection);
            catstatus.Fill(ds, "catstatus");
            if (ds.Tables["catstatus"].Rows.Count > 0)
            {
                if (ds.Tables["catstatus"].Rows[0]["policystatus"].ToString() == "S")
                    return true;
                else
                {
                    System.Data.OleDb.OleDbDataAdapter daFinal = new System.Data.OleDb.OleDbDataAdapter("select classname from classmasterloadingstatus where classname=N'" + className + "' and LoadingStatus=" + cat, connection);
                    daFinal.Fill(ds, "Final");
                    daFinal.Dispose();
                    if (ds.Tables["Final"].Rows.Count > 0)
                        return true;
                    else
                        return false;
                }
            }

            daClass.Dispose();
            daCatgory.Dispose();

            ds.Dispose();
            return false;
        }
        /*
        public int getCategory(string accno, OleDbConnection cn)
        {
            OleDbCommand Com = new OleDbCommand();
            int catId;
            Com.Connection = cn;
            Com.CommandText = "select booktype from bookcatalog,bookaccessionmaster where bookcatalog.ctrl_no=bookaccessionmaster.ctrl_no and bookaccessionmaster.accessionnumber=N'" + accno + "'";
            catId = Com.ExecuteScalar();
            Com.Dispose();
            if (catId != default(Integer))
                return catId;
        }*/
        public string UseIdTable(string objectName, string session, string fmt, OleDbConnection con)
        {
            OleDbCommand cmd1 = new OleDbCommand("select prefix,CurrentPosition,suffix from idtable where objectName=N'" + objectName + "'", con);
            OleDbDataReader dr1;
            dr1 = cmd1.ExecuteReader();
            dr1.Read();
            string strRetId = dr1.GetValue(0) + session + fmt + dr1.GetValue(1) + 1 + dr1.GetValue(2);
            dr1.Close();
            cmd1.Dispose();
            return strRetId;
        }
        /*
        public string UseIdTable1(string sqlStr, string objectName, string session, string fmt)
        {
            OleDbConnection tmpcon2 = new OleDbConnection(
                DBI.GetConnectionString(System.Web.HttpContext.Current.Session["LibWiseDBConn"].ToString()));
            tmpcon2.Open();
            OleDbCommand cmd = new OleDbCommand(sqlStr, tmpcon2);
            OleDbDataReader dr = cmd.ExecuteReader();
            dr.Read();
            string sName = dr[0].ToString();
            int cPoss = dr[1];
            cmd.Dispose();
            dr.Close();
            OleDbCommand cmd1 = new OleDbCommand("select prefix,CurrentPosition,suffix from idtable where objectName=N'" + objectName + "'", tmpcon2);
            OleDbDataReader dr1;
            dr1 = cmd1.ExecuteReader();
            dr1.Read();
            string strRetId = dr1.GetValue(0) + sName + fmt + session + fmt + cPoss + 1 + dr1.GetValue(2);
            dr1.Close();
            cmd1.Dispose();
            tmpcon2.Close();
            tmpcon2.Dispose();
            return strRetId;
        }*/
        public DataSet PopulateDataset(string sqlStr, string tableName, OleDbConnection myConnection)
        {
            OleDbDataAdapter da = null;
            DataSet ds = null;
            try
            {
                da = new OleDbDataAdapter(sqlStr, myConnection);
                ds = new DataSet();
                da.Fill(ds, tableName);
                return ds;
            }
            catch (Exception ex)
            {
            }

            finally
            {
                da.Dispose();
            }
            return null;
        }
        public DataSet PopulateDataset1(DataSet ds, string sqlStr, string tableName, OleDbConnection myConnection)
        {
            OleDbDataAdapter da = null;
            // Dim ds As DataSet = Nothing
            try
            {
                da = new OleDbDataAdapter(sqlStr, myConnection);
                // ds = New DataSet()
                da.Fill(ds, tableName);
                return ds;
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
            return null;
        }
        /*
        public void SendMailLib(string mailTo, string mailFrom, string msgBody, string msgSubject, string toCC, string smtp, OleDbConnection cn)
        {
            // *************Praveen
            // Namespace used System.Net.Mail.MailMessage in place of System.Web.Mail.MailMessage & System.Net.Mail.SmtpClient in place of System.Web.Mail.SmtpMail
            string uid = string.Empty;
            string pwd = string.Empty;
            DataSet ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter("select iUser,iPwd from librarysetupinformation", cn);
            da.Fill(ds, "imp");
            if (ds.Tables["imp"].Rows.Count > 0)
            {
                uid = ds.Tables["imp"].Rows[0].Item["iUser"];
                pwd = ds.Tables["imp"].Rows[0].Item["iPwd"];
            }
            ds.Dispose();
            da.Dispose();
            MailMessage mymessage = new MailMessage();
            SmtpClient SmtpMail = new SmtpClient();
            MailAddress fromAddress = new MailAddress(mailFrom);
            NetworkCredential credential = new NetworkCredential(uid, pwd);
            // specify the host name or ipaddress of server,Default in IIS will be localhost 
            SmtpMail.Host = smtp;
            // Default port will be 25
            SmtpMail.Port = 25;
            SmtpMail.Credentials = credential;
            mymessage.From = fromAddress;
            mymessage.To.Add(mailTo);
            if (string.Trim(toCC) != string.Empty)
                mymessage.CC.Add(toCC);
            mymessage.Subject = msgSubject;
            // Body can be Html or text format
            mymessage.IsBodyHtml = true;
            mymessage.Body = msgBody;
            SmtpMail.Send(mymessage);
        }
        */
        /*
        public void SendMailLib1(string mailTo, string mailFrom, string msgBody, string msgSubject, string toCC, string smtp, string uid, string pwd)
        {
            // *************Praveen
            // Namespace used System.Net.Mail.MailMessage in place of System.Web.Mail.MailMessage & System.Net.Mail.SmtpClient in place of System.Web.Mail.SmtpMail
            MailMessage mymessage = new MailMessage();
            SmtpClient SmtpMail = new SmtpClient();
            MailAddress fromAddress = new MailAddress(mailFrom);
            NetworkCredential credential = new NetworkCredential(uid, pwd);
            SmtpMail.Host = smtp;
            SmtpMail.Port = 25;
            SmtpMail.Credentials = credential;
            mymessage.From = fromAddress;
            mymessage.To.Add(mailTo);
            if (string.Trim(toCC) != string.Empty)
                mymessage.CC.Add(toCC);
            mymessage.Subject = msgSubject;
            mymessage.IsBodyHtml = true;
            mymessage.Body = msgBody;
            SmtpMail.Send(mymessage);
        }
        */
        public string SearchValue(string requiredField, string tablename, string condition, string connectString)
        {
            string result;
            System.Data.OleDb.OleDbConnection chkCon = new System.Data.OleDb.OleDbConnection(connectString);
            chkCon.Open();
            OleDbDataAdapter chkda = new OleDbDataAdapter("select " + requiredField + " from " + tablename + " where " + condition, chkCon);
            DataSet chkds = new DataSet();
            chkda.Fill(chkds, "result");
            if (chkds.Tables[0].Rows.Count > 0)
                result = chkds.Tables[0].Rows[0][0].ToString();
            else
                result = string.Empty;
            chkCon.Close();
            chkCon.Dispose();
            chkda.Dispose();
            chkds.Dispose();
            return result;
        }

        public string SearchFieldValue(string requiredField, string tablename, string condition, OleDbCommand command)
        {
            string result;
            command.CommandText = "select " + requiredField + " from " + tablename + " where " + condition;
            result = command.ExecuteScalar().ToString();
            return result;
        }

        public string getCurrency(string con)
        {
            string result;
            System.Data.OleDb.OleDbConnection chkCon = new System.Data.OleDb.OleDbConnection(con);
            chkCon.Open();
            OleDbDataAdapter chkda = new OleDbDataAdapter("select currency from librarysetupinformation", chkCon);
            DataSet chkds = new DataSet();
            chkda.Fill(chkds, "result");
            if (chkds.Tables[0].Rows.Count > 0)
                result = chkds.Tables[0].Rows[0][0].ToString();
            else
                result = Resources.ValidationResources.Lanmt.ToString();
            chkCon.Close();
            chkCon.Dispose();
            chkda.Dispose();
            chkds.Dispose();
            return result;
        }
        public string logo(OleDbConnection con)
        {
            string logok = string.Empty;
            DataSet dss = PopulateDataset("select * from FeaturesPer where FID=11", "tbl", con);
            if (dss.Tables[0].Rows.Count > 0)
                logok = " ,  Organization_Picture   as logo ";
            return logok;
        }
        /*
        public string getCurrency1(OleDbConnection con)
        {
            string result;
            // chkCon.Open()
            // Dim chkda As New OleDbDataAdapter("select currency from librarysetupinformation", con)
            DataSet chkds = new DataSet();
            // chkda.Fill(chkds, "result")
            chkds = PopulateDataset("select currency from librarysetupinformation", "result", con);
            if (chkds.Tables["result"].Rows.Count > 0)
                result = chkds.Tables[0].Rows[0].Item[0];
            else
                result = "Rs.";
            return result;
            chkds.Dispose();
        }
        */
        public int getNoOfRecords(string idField, string tablename, string condition, string connectString)
        {
            int result;
            System.Data.OleDb.OleDbConnection chkCon = new System.Data.OleDb.OleDbConnection(connectString);
            chkCon.Open();
            OleDbDataAdapter chkda = new OleDbDataAdapter("select " + idField + " from " + tablename + " where " + condition, chkCon);
            DataSet chkds = new DataSet();
            chkda.Fill(chkds, "result");
            if (chkds.Tables[0].Rows.Count > 0)
                result = chkds.Tables[0].Rows.Count;
            else
                result = 0;
            chkCon.Close();
            chkCon.Dispose();
            chkda.Dispose();
            chkds.Dispose();
            return result;
        }
        public void showPDF(string ActualFileName, string AliasFileName, Page refP)
        {
            string linc = "'DocumentViewer.aspx?fname=' " + ActualFileName + " '&aliasName=' " + AliasFileName + ";";
            string s = "<script language='javascript'>" + Environment.NewLine + "window.Open(" + linc + ",'_blank', 'top=0,left=0,width=' + screen.width + ',height=' + screen.height)" + Environment.NewLine + "</script>";
            ScriptManager.RegisterClientScriptBlock(refP, refP.GetType(), "pdfFile", "openPDF('" + ActualFileName + "','" + AliasFileName + "');", true);
        }
        /*
        public void SetFocus(string ControlName, System.Web.UI.Page aPage)
        {
            // character 34 = "
            string script = "<script language=" + string.Chr(34) + "javascript" + string.Chr(34)
                                  + ">" + "  var control = document.getElementById(" + string.Chr(34) + ControlName + string.Chr(34) + ");" + "  if( control != null ){control.focus();}" + "</script>";
            aPage.RegisterStartupScript("Focus", script);
        }
        */
    }
    public class MenuGen
    {
        /*
        public bool GetUserTypePermissions(string UserID, ref OleDbDataReader UserTypePermissions, ref string ErrorDescription, ref string ErrorMessage, OleDbConnection OLEDBConn)
        {
            bool flag;
            flag = true;
            OleDbCommand myCommand = new OleDbCommand("sp_GetUserTypePermissionsfrmUserID", OLEDBConn);
            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;
            // Add Parameters to SPROC
            myCommand.Parameters.Add(new OleDbParameter("@UserID", OleDbType.Integer));
            myCommand.Parameters["@UserID"].Value = UserID;
            // Try
            UserTypePermissions = myCommand.ExecuteReader();
            string o;
            o = UserTypePermissions[0].ToString();
            // Catch e As Exception
            // flag = False
            // ErrorDescription = e.Message
            // Dim msg As string
            // Dim objApplicationConstants As DigitalArtAsset.ApplicationConstants = New DigitalArtAsset.ApplicationConstants
            // objApplicationConstants.ErrorMessages(0, msg)
            // ErrorMessage = Trim(msg)
            // objApplicationConstants = Nothing
            // End Try
            myCommand = null;
        }
        */
        // Public Function check(ByVal txtcheck As TextBox)
        // If txtcheck.Text.Length > 0 Then
        // Dim word_server As New Word.Application
        // word_server.Visible = False
        // Dim doc As Word.Document = word_server.Documents.Add()
        // Dim rng As Word.Range
        // rng = doc.Range()
        // rng.Text = txtcheck.Text
        // doc.Activate()
        // doc.CheckSpelling()
        // Dim chars() As Char = {CType(vbCr, Char), CType(vbLf, Char)}
        // txtcheck.Text = doc.Range().Text.Trim(chars)
        // doc.Close(SaveChanges:=False)
        // word_server.Quit()
        // End If
        // End Function
        // Naushad
        public DataSet GetMenuList(int aUserID, OleDbConnection aConn, char aOnlineflag)
        {
            // aOnlineflag is generally passed a value Y
            OleDbCommand myCommand;
            DataSet ds = new DataSet();
            OleDbDataAdapter da;
            myCommand = new OleDbCommand("GET_MENU_LIST");
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@mUserID", System.Data.OleDb.OleDbType.Integer));
            myCommand.Parameters["@mUserID"].Value = aUserID;
            myCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@mTree", System.Data.OleDb.OleDbType.VarWChar));
            myCommand.Parameters["@mTree"].Value = aOnlineflag;
            myCommand.Connection = aConn;
            da = new OleDbDataAdapter(myCommand);
            da.Fill(ds);
            int p;
            p = ds.Tables[0].Rows.Count;
            return ds;
        }
    }
    public class MenuItem
    {
        private string MenuItemName;
        private string MenuItemLinkUrl;
        private string MenuItemDisplayName;
        private string MenuItemCaption;
        public MenuItem(string newName, string newLinkUrl, string newDisplayName, string newCaption)
        {
            this.MenuItemName = newName;
            this.MenuItemLinkUrl = newLinkUrl;
            this.MenuItemDisplayName = newDisplayName;
            this.MenuItemCaption = newCaption;
        }
        public string Name
        {
            get
            {
                return MenuItemName;
            }
        }
        public string LinkUrl
        {
            get
            {
                return MenuItemLinkUrl;
            }
        }
        public string DisplayName
        {
            get
            {
                return MenuItemDisplayName;
            }
        }
        public string Caption
        {
            get
            {
                return MenuItemCaption;
            }
        }
    }
    public class MenuItemDisplay
    {
        private string MenuItemDisplayName;
        private string MenuItemLinkUrl;
        public MenuItemDisplay(string newName, string newLinkUrl)
        {
            this.MenuItemDisplayName = newName;
            this.MenuItemLinkUrl = newLinkUrl;
        }
        public string Name
        {
            get
            {
                return MenuItemDisplayName;
            }
        }
        public string LinkUrl
        {
            get
            {
                return MenuItemLinkUrl;
            }
        }
    }
    public class insertLogin
    {
        public string IpAddress()
        {
            string text = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (text == null)
            {
                text = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }

//            aaa = text.ToString();
            return text.ToString();
        }
        // Kaushal:for Digital Aura File Downloading Audit
        public void insertLoginFuncF(string sessionstr1, string tablename, string sessionstr2, string txtstring, string type, string connStr, string fromsource, string itm_type)
        {
            System.Data.OleDb.OleDbConnection chkCon = new System.Data.OleDb.OleDbConnection(connStr);
            chkCon.Open();


            OleDbCommand classcom11 = new OleDbCommand("insert_LoginMaster_1", chkCon);
            classcom11.CommandType = CommandType.StoredProcedure;

            classcom11.Parameters.Add(new OleDbParameter("@loginname_1", OleDbType.VarWChar));
            classcom11.Parameters["@loginname_1"].Value = sessionstr1;
            classcom11.Parameters.Add(new OleDbParameter("@logindate_2", OleDbType.Date));
            classcom11.Parameters["@logindate_2"].Value = DateTime.Now.Date;
            string tm = DateTime.Now.Date.ToString("T");
            classcom11.Parameters.Add(new OleDbParameter("@logintime_3", OleDbType.VarWChar));
            classcom11.Parameters["@logintime_3"].Value = tm;


            classcom11.Parameters.Add(new OleDbParameter("@tablename_4", OleDbType.VarWChar));
            classcom11.Parameters["@tablename_4"].Value = tablename;


            classcom11.Parameters.Add(new OleDbParameter("@useraction_5", OleDbType.VarWChar));
            classcom11.Parameters["@useraction_5"].Value = type;


            classcom11.Parameters.Add(new OleDbParameter("@id_6", OleDbType.VarWChar));
            classcom11.Parameters["@id_6"].Value = txtstring;


            classcom11.Parameters.Add(new OleDbParameter("@sessionyr_7", OleDbType.VarWChar));
            classcom11.Parameters["@sessionyr_7"].Value = sessionstr2;
//            DataLayer.SqlDataAccess obj = new DataLayer.SqlDataAccess();
            string ip = "";
            ip = IpAddress();
            classcom11.Parameters.Add(new OleDbParameter("@IpAddress_8", OleDbType.VarWChar));
            classcom11.Parameters["@IpAddress_8"].Value = ip;
            classcom11.ExecuteNonQuery();
            classcom11.Parameters.Clear();
            classcom11.CommandType = CommandType.Text;
            classcom11.CommandText = "Update loginmaster set memberid='" + fromsource + "' where LoginTime='" + tm + "' and loginDate='" + DateTime.Now.Date.ToString("dd-MMM-yyyy") + "' and id='" + txtstring + "'";
            classcom11.ExecuteNonQuery();
            classcom11.CommandText = "Update loginmaster set Item='" + itm_type + "' where LoginTime='" + tm + "' and loginDate='" + DateTime.Now.Date.ToString("dd-MMM-yyyy") + "' and id='" + txtstring + "'";
            classcom11.ExecuteNonQuery();
            chkCon.Close();
            classcom11.Dispose();
        }

        // --------

        public void insertLoginFunc(string sessionstr1, string tablename, string sessionstr2, string txtstring, string type, string connStr)
        {
            System.Data.OleDb.OleDbConnection chkCon = new System.Data.OleDb.OleDbConnection(connStr);
            chkCon.Open();
            DateTime dt;

            OleDbCommand classcom11 = new OleDbCommand("insert_LoginMaster_1", chkCon);
            classcom11.CommandType = CommandType.StoredProcedure;

            classcom11.Parameters.Add(new OleDbParameter("@loginname_1", OleDbType.VarWChar));
            classcom11.Parameters["@loginname_1"].Value = sessionstr1;
            classcom11.Parameters.Add(new OleDbParameter("@logindate_2", OleDbType.Date));
            classcom11.Parameters["@logindate_2"].Value = DateTime.Now.Date;

            classcom11.Parameters.Add(new OleDbParameter("@logintime_3", OleDbType.VarWChar));
            classcom11.Parameters["@logintime_3"].Value = DateTime.Now .ToString("T");


            classcom11.Parameters.Add(new OleDbParameter("@tablename_4", OleDbType.VarWChar));
            classcom11.Parameters["@tablename_4"].Value = tablename;


            classcom11.Parameters.Add(new OleDbParameter("@useraction_5", OleDbType.VarWChar));
            classcom11.Parameters["@useraction_5"].Value = type;


            classcom11.Parameters.Add(new OleDbParameter("@id_6", OleDbType.VarWChar));
            classcom11.Parameters["@id_6"].Value = txtstring;


            classcom11.Parameters.Add(new OleDbParameter("@sessionyr_7", OleDbType.VarWChar));
            classcom11.Parameters["@sessionyr_7"].Value = sessionstr2;

//            DataLayer.SqlDataAccess obj = new DataLayer.SqlDataAccess();
            string ip = "";
            ip = IpAddress();
            classcom11.Parameters.Add(new OleDbParameter("@IpAddress_8", OleDbType.VarWChar));
            classcom11.Parameters["@IpAddress_8"].Value = ip;
            classcom11.ExecuteNonQuery();
            chkCon.Close();
            classcom11.Dispose();
        }

        public void insertLoginFunc1(string sessionstr1, string tablename, string sessionstr2, string txtstring, string type, OleDbConnection chkCon)
        {
            // Dim chkCon As New OleDb.OleDbConnection(connStr)
            // chkCon.Open()
            DateTime dt;

            OleDbCommand classcom11 = new OleDbCommand("insert_LoginMaster_1", chkCon);
            classcom11.CommandType = CommandType.StoredProcedure;

            classcom11.Parameters.Add(new OleDbParameter("@loginname_1", OleDbType.VarWChar));
            classcom11.Parameters["@loginname_1"].Value = sessionstr1;
            classcom11.Parameters.Add(new OleDbParameter("@logindate_2", OleDbType.Date));
            classcom11.Parameters["@logindate_2"].Value = DateTime.Now.Date;

            classcom11.Parameters.Add(new OleDbParameter("@logintime_3", OleDbType.VarWChar));
            classcom11.Parameters["@logintime_3"].Value = DateTime.Now .ToString("T");


            classcom11.Parameters.Add(new OleDbParameter("@tablename_4", OleDbType.VarWChar));
            classcom11.Parameters["@tablename_4"].Value = tablename;


            classcom11.Parameters.Add(new OleDbParameter("@useraction_5", OleDbType.VarWChar));
            classcom11.Parameters["@useraction_5"].Value = type;


            classcom11.Parameters.Add(new OleDbParameter("@id_6", OleDbType.VarWChar));
            classcom11.Parameters["@id_6"].Value = txtstring;


            classcom11.Parameters.Add(new OleDbParameter("@sessionyr_7", OleDbType.VarWChar));
            classcom11.Parameters["@sessionyr_7"].Value = sessionstr2;
            classcom11.ExecuteNonQuery();
            // chkCon.Close()
            classcom11.Dispose();
        }

        // ************************himanshu 15 dec*************************************
        public void insertLoginFunc2(string sessionstr1, string tablename, string sessionstr2, string txtstring, string type, string memberid, string item, string connStr)
        {
            System.Data.OleDb.OleDbConnection chkCon = new System.Data.OleDb.OleDbConnection(connStr);
            chkCon.Open();
            DateTime dt;

            OleDbCommand classcom11 = new OleDbCommand("insert_LoginMaster_3", chkCon);
            classcom11.CommandType = CommandType.StoredProcedure;

            classcom11.Parameters.Add(new OleDbParameter("@loginname_1", OleDbType.VarWChar));
            classcom11.Parameters["@loginname_1"].Value = sessionstr1;
            classcom11.Parameters.Add(new OleDbParameter("@logindate_2", OleDbType.Date));
            classcom11.Parameters["@logindate_2"].Value = DateTime.Now.Date;

            classcom11.Parameters.Add(new OleDbParameter("@logintime_3", OleDbType.VarWChar));
            classcom11.Parameters["@logintime_3"].Value = DateTime.Now .ToString("T");


            classcom11.Parameters.Add(new OleDbParameter("@tablename_4", OleDbType.VarWChar));
            classcom11.Parameters["@tablename_4"].Value = tablename;


            classcom11.Parameters.Add(new OleDbParameter("@useraction_5", OleDbType.VarWChar));
            classcom11.Parameters["@useraction_5"].Value = type;


            classcom11.Parameters.Add(new OleDbParameter("@id_6", OleDbType.VarWChar));
            classcom11.Parameters["@id_6"].Value = txtstring;


            classcom11.Parameters.Add(new OleDbParameter("@sessionyr_7", OleDbType.VarWChar));
            classcom11.Parameters["@sessionyr_7"].Value = sessionstr2;
//            DataLayer.SqlDataAccess obj = new DataLayer.SqlDataAccess();
            string ip = "";
            ip = IpAddress();
            classcom11.Parameters.Add(new OleDbParameter("@IpAddress_8", OleDbType.VarWChar));
            classcom11.Parameters["@IpAddress_8"].Value = ip;

            classcom11.Parameters.Add(new OleDbParameter("@memberid", OleDbType.VarChar));
            classcom11.Parameters["@memberid"].Value = memberid;

            classcom11.Parameters.Add(new OleDbParameter("@item", OleDbType.VarChar));
            classcom11.Parameters["@item"].Value = item;
            classcom11.ExecuteNonQuery();
            chkCon.Close();
            classcom11.Dispose();
        }
        // ************************himanshu 15 dec2010*********************************

        public void insertBudgetAudit(string UserId, string Process, DateTime OperationDate, DateTime OperationTime, string type, double Expenditure, double CommitMentApp, double CommitMentNApp, double IndentsApp, double IndentNapp, string documentNo, string connStr)
        {
            System.Data.OleDb.OleDbConnection chkCon = new System.Data.OleDb.OleDbConnection(connStr);
            chkCon.Open();
            DateTime dt;

            OleDbCommand classcom11 = new OleDbCommand("insert_BudgetAudit_1", chkCon);
            classcom11.CommandType = CommandType.StoredProcedure;

            classcom11.Parameters.Add(new OleDbParameter("@UserID_1", OleDbType.VarWChar));
            classcom11.Parameters["@UserID_1"].Value = UserId;
            classcom11.Parameters.Add(new OleDbParameter("@Process_2", OleDbType.VarWChar));
            classcom11.Parameters["@Process_2"].Value = Process;

            classcom11.Parameters.Add(new OleDbParameter("@OperationDate_3", OleDbType.Date));
            classcom11.Parameters["@OperationDate_3"].Value = OperationDate;

            classcom11.Parameters.Add(new OleDbParameter("@OperationTime_4", OleDbType.VarWChar));
            classcom11.Parameters["@OperationTime_4"].Value = OperationTime;

            classcom11.Parameters.Add(new OleDbParameter("@Operation_5", OleDbType.VarWChar));
            classcom11.Parameters["@Operation_5"].Value = type;

            classcom11.Parameters.Add(new OleDbParameter("@Expenditure_6", OleDbType.Decimal));
            classcom11.Parameters["@Expenditure_6"].Value = Expenditure;


            classcom11.Parameters.Add(new OleDbParameter("@CommitMentApp_7", OleDbType.Decimal));
            classcom11.Parameters["@CommitMentApp_7"].Value = CommitMentApp;

            // classcom11.Parameters.Add(New OleDbParameter("@CommitMentApp_7", OleDbType.Decimal))
            // classcom11.Parameters("@CommitMentApp_7").Value = CommitMentApp
            classcom11.Parameters.Add(new OleDbParameter("@CommitMentNApp_8", OleDbType.Decimal));
            classcom11.Parameters["@CommitMentNApp_8"].Value = CommitMentNApp;
            classcom11.Parameters.Add(new OleDbParameter("@IndentsApp_9", OleDbType.Decimal));
            classcom11.Parameters["@IndentsApp_9"].Value = IndentsApp;
            classcom11.Parameters.Add(new OleDbParameter("@IndentNapp_10", OleDbType.Decimal));
            classcom11.Parameters["@IndentNapp_10"].Value = IndentNapp;
            classcom11.Parameters.Add(new OleDbParameter("@DocumentNo_11", OleDbType.VarWChar));
            classcom11.Parameters["@DocumentNo_11"].Value = documentNo;
            // classcom11.Parameters.Add(New OleDbParameter("@id_6", OleDbType.VarWChar))
            // classcom11.Parameters("@id_6").Value = txtstring


            // classcom11.Parameters.Add(New OleDbParameter("@sessionyr_7", OleDbType.VarWChar))
            // classcom11.Parameters("@sessionyr_7").Value = sessionstr2
            classcom11.ExecuteNonQuery();
        }
        public void insertFinancialFunc(string sessionstr1, string tablename, string sessionstr2, string txtstring, string type, double financialValue, string connStr)
        {
            System.Data.OleDb.OleDbConnection chkCon = new System.Data.OleDb.OleDbConnection(connStr);
            chkCon.Open();
            DateTime dt;

            OleDbCommand classcom11 = new OleDbCommand("insert_LoginMaster_2", chkCon);
            classcom11.CommandType = CommandType.StoredProcedure;

            classcom11.Parameters.Add(new OleDbParameter("@loginname_1", OleDbType.VarWChar));
            classcom11.Parameters["@loginname_1"].Value = sessionstr1;

            classcom11.Parameters.Add(new OleDbParameter("@logindate_2", OleDbType.Date));
            classcom11.Parameters["@logindate_2"].Value = DateTime.Now.Date;

            classcom11.Parameters.Add(new OleDbParameter("@logintime_3", OleDbType.VarWChar));
            classcom11.Parameters["@logintime_3"].Value = DateTime.Now.ToString("T");


            classcom11.Parameters.Add(new OleDbParameter("@tablename_4", OleDbType.VarWChar));
            classcom11.Parameters["@tablename_4"].Value = tablename;


            classcom11.Parameters.Add(new OleDbParameter("@useraction_5", OleDbType.VarWChar));
            classcom11.Parameters["@useraction_5"].Value = type;


            classcom11.Parameters.Add(new OleDbParameter("@id_6", OleDbType.VarWChar));
            classcom11.Parameters["@id_6"].Value = txtstring;


            classcom11.Parameters.Add(new OleDbParameter("@sessionyr_7", OleDbType.VarWChar));
            classcom11.Parameters["@sessionyr_7"].Value = sessionstr2;

            classcom11.Parameters.Add(new OleDbParameter("@financialValue_8", OleDbType.Double));
            classcom11.Parameters["@financialValue_8"].Value = financialValue;
//            DataLayer.SqlDataAccess obj = new DataLayer.SqlDataAccess();
            string ip = "";
            ip = IpAddress();
            classcom11.Parameters.Add(new OleDbParameter("@IpAddress_9", OleDbType.VarWChar));
            classcom11.Parameters["@IpAddress_9"].Value = ip;

            classcom11.ExecuteNonQuery();
        }
        // Sachin 25Aug2020 EPCAudit
        public void insertEPC(string AccNumber, string Location, string Rfid, string userName, string sessionyr, string UserAction, string ChkCon)
        {
            System.Data.OleDb.OleDbConnection chkCon1 = new System.Data.OleDb.OleDbConnection(ChkCon);
            chkCon1.Open();
            DateTime dt;

            OleDbCommand classcom11 = new OleDbCommand("Sp_insertEPC", chkCon1);
            classcom11.CommandType = CommandType.StoredProcedure;

            classcom11.Parameters.Add(new OleDbParameter("@AccNumber", OleDbType.VarWChar));
            classcom11.Parameters["@AccNumber"].Value = AccNumber;
            classcom11.Parameters.Add(new OleDbParameter("@Location", OleDbType.VarWChar));
            classcom11.Parameters["@Location"].Value = Location;
            classcom11.Parameters.Add(new OleDbParameter("@Rfid", OleDbType.VarWChar));
            classcom11.Parameters["@Rfid"].Value = Rfid;
            classcom11.Parameters.Add(new OleDbParameter("@userName", OleDbType.VarWChar));
            classcom11.Parameters["@userName"].Value = userName;
            classcom11.Parameters.Add(new OleDbParameter("@LoginDate", OleDbType.Date));
            classcom11.Parameters["@LoginDate"].Value = DateTime.Now.Date;
            classcom11.Parameters.Add(new OleDbParameter("@LoginTime", OleDbType.VarWChar));
            classcom11.Parameters["@LoginTime"].Value = DateTime.Now.ToString("T");
            classcom11.Parameters.Add(new OleDbParameter("@sessionyr", OleDbType.VarWChar));
            classcom11.Parameters["@sessionyr"].Value = sessionyr;
          //  DataLayer.SqlDataAccess obj = new DataLayer.SqlDataAccess();
            string ip = "";
            ip = IpAddress();
            classcom11.Parameters.Add(new OleDbParameter("@IpAddress", OleDbType.VarWChar));
            classcom11.Parameters["@IpAddress"].Value = ip;
            classcom11.Parameters.Add(new OleDbParameter("@UserAction", OleDbType.VarWChar));
            classcom11.Parameters["@UserAction"].Value = UserAction;
            classcom11.ExecuteNonQuery();
            chkCon1.Close();
            classcom11.Dispose();
        }
    }
    public class insertFinancialValue
    {
    }
    public class Getinwords
    {
     /*  public void SpellNumber(string MyNumber)
        {
            var Dollars, Cents, Temp;
            var DecimalPlace, Count;

            string[] Place = new string[10];
            Place[2] = " Thousand ";
            Place[3] = " Million ";
            Place[4] = " Billion ";
            Place[5] = " Trillion ";

            // string representation of amount
            MyNumber = Convert.ToString(MyNumber);

            // Position of decimal place 0 if none
            DecimalPlace = string.InStr(MyNumber, ".");
            // Convert cents and set MyNumber to dollar amount
            if (DecimalPlace > 0)
            {
                Cents = GetTens(string.Left(string.Mid(MyNumber, DecimalPlace + 1) + "00", 2));
                MyNumber = string.Trim(string.Left(MyNumber, DecimalPlace - 1));
            }

            Count = 1;
            while (MyNumber != "")
            {
                Temp = GetHundreds(string.Right(MyNumber, 3));
                if (Temp != "")
                    Dollars = Temp + Place[Count] + Dollars;
                if (string.Len(MyNumber) > 3)
                    MyNumber = string.Left(MyNumber, string.Len(MyNumber) - 3);
                else
                    MyNumber = "";
                Count = Count + 1;
            }

            switch (Dollars)
            {
                case "":
                    {
                        Dollars = "zero";
                        break;
                    }

                case "One":
                    {
                        Dollars = "One";
                        break;
                    }

                default:
                    {
                        Dollars = Dollars; // & " Dollars"
                        break;
                    }
            }

            switch (Cents)
            {
                case "":
                    {
                        Cents = " and zero Cents";
                        break;
                    }

                case "One":
                    {
                        Cents = " and One Cent";
                        break;
                    }

                default:
                    {
                        Cents = " and " + Cents + " Cents";
                        break;
                    }
            }

            SpellNumber = Dollars; // & Cents
        }
        public void GetHundreds(string MyNumber)
        {
            string Result;
            if (Conversion.Val(MyNumber) == 0)
                return;
            MyNumber = string.Right("000" + MyNumber, 3);
            // Convert the hundreds place
            if (string.Mid(MyNumber, 1, 1) != "0")
                Result = GetDigit(string.Mid(MyNumber, 1, 1)) + " Hundred ";
            // Convert the tens and ones place
            if (string.Mid(MyNumber, 2, 1) != "0")
                Result = Result + GetTens(string.Mid(MyNumber, 2));
            else
                Result = Result + GetDigit(string.Mid(MyNumber, 3));
            GetHundreds = Result;
        }
        // Converts a number from 10 to 99 into text
        public void GetTens(string TensText)
        {
            string Result;
            Result = ""; // null out the temporary function value
            if (Conversion.Val(string.Left(TensText, 1)) == 1)
            {
                switch (Conversion.Val(TensText))
                {
                    case 10:
                        {
                            Result = "Ten";
                            break;
                        }

                    case 11:
                        {
                            Result = "Eleven";
                            break;
                        }

                    case 12:
                        {
                            Result = "Twelve";
                            break;
                        }

                    case 13:
                        {
                            Result = "Thirteen";
                            break;
                        }

                    case 14:
                        {
                            Result = "Fourteen";
                            break;
                        }

                    case 15:
                        {
                            Result = "Fifteen";
                            break;
                        }

                    case 16:
                        {
                            Result = "Sixteen";
                            break;
                        }

                    case 17:
                        {
                            Result = "Seventeen";
                            break;
                        }

                    case 18:
                        {
                            Result = "Eighteen";
                            break;
                        }

                    case 19:
                        {
                            Result = "Nineteen";
                            break;
                        }

                    default:
                        {
                            break;
                        }
                }
            }
            else
            {
                switch (Conversion.Val(string.Left(TensText, 1)))
                {
                    case 2:
                        {
                            Result = "Twenty ";
                            break;
                        }

                    case 3:
                        {
                            Result = "Thirty ";
                            break;
                        }

                    case 4:
                        {
                            Result = "Forty ";
                            break;
                        }

                    case 5:
                        {
                            Result = "Fifty ";
                            break;
                        }

                    case 6:
                        {
                            Result = "Sixty ";
                            break;
                        }

                    case 7:
                        {
                            Result = "Seventy ";
                            break;
                        }

                    case 8:
                        {
                            Result = "Eighty ";
                            break;
                        }

                    case 9:
                        {
                            Result = "Ninety ";
                            break;
                        }

                    default:
                        {
                            break;
                        }
                }
                Result = Result + GetDigit(string.Right(TensText, 1)); // Retrieve ones place
            }
            GetTens = Result;
        }

        // Converts a number from 1 to 9 into text
        public void GetDigit(string Digit)
        {
            switch (Conversion.Val(Digit))
            {
                case 1:
                    {
                        GetDigit = "One";
                        break;
                    }

                case 2:
                    {
                        GetDigit = "Two";
                        break;
                    }

                case 3:
                    {
                        GetDigit = "Three";
                        break;
                    }

                case 4:
                    {
                        GetDigit = "Four";
                        break;
                    }

                case 5:
                    {
                        GetDigit = "Five";
                        break;
                    }

                case 6:
                    {
                        GetDigit = "Six";
                        break;
                    }

                case 7:
                    {
                        GetDigit = "Seven";
                        break;
                    }

                case 8:
                    {
                        GetDigit = "Eight";
                        break;
                    }

                case 9:
                    {
                        GetDigit = "Nine";
                        break;
                    }

                default:
                    {
                        GetDigit = "";
                        break;
                    }
            }
        }
        */
        public void LogStatus(string userid, string Lstatus)
        {
            OleDbConnection Lcon = new OleDbConnection(ConfigurationManager.ConnectionStrings[System.Web.HttpContext.Current.Session["LibWiseDBConn"].ToString()].ToString());
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
    }

    public class messageLibrary
    {
        enum msgType
        {
            Warning,
            Success,
            Failure
        }

/*        public void showHtml_Message(msgType msgType, string msg, Page page)
        {
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "msgHtml", "showHTML_Message(" + msgType + ",'" + msg.Replace("'", @"\'").Trim() + "');", true);
        }

        public void showHtml_MessageDelay(msgType msgType, string msg, Page page)
        {
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "msgHtmlDelay", "setTimeout(" + string.Chr(34) + "showHTML_MessageDelay(" + msgType + ",'" + msg.Replace("'", @"\'").Trim() + "');" + string.Chr(34) + ",600);", true);
        }
*/
    }
}