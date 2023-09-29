using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace ItemIssueReturn2
{
    public class Trans
    {
        public ReturnStat ReturnBook(TransData request)
        {
            ReturnStat Stat = new ReturnStat();
            if ((string.IsNullOrEmpty(request.IssRet)) ||
                (string.IsNullOrEmpty(request.Accno)) ||
                (string.IsNullOrEmpty(request.UserCode)) ||
                (string.IsNullOrEmpty(request.Opertor)))
            {
                Stat.Status = "Pass All values";
                Stat.IsSuccess = false;
                return Stat;
            }
            request.IssRet = request.IssRet.ToUpper().StartsWith("I") ? "I" : "R";
            request.UserCode = request.UserCode.Trim();
            request.Accno = request.Accno.Trim();
            request.Opertor = request.Opertor.Trim();

            SqlConnection conn = new SqlConnection();
            SqlTransaction tran;
            var constr = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;
            conn.ConnectionString = constr;
            conn.Open();
            tran = conn.BeginTransaction();
            try {
                //select userid,accno,issuedate,duedate,status,IssueId from CircIssueTransaction where userid='' and accno=''
                var cmd = new SqlCommand();

                cmd.Connection = conn;
                cmd.Transaction = tran;
                var sqer = "select a.userid,a.accno,a.issuedate,a.duedate,a.status,a.IssueId,b.itemcategorycode from CircIssueTransaction a join bookaccessionmaster b on a.accno=b.accessionnumber where a.status='Issued' and a.userid='" + request.UserCode+"' and accno='"+request.Accno+"'";
                cmd.CommandType = CommandType.Text;
                cmd.CommandText=sqer;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dtIssd = new DataTable();
                sda.Fill(dtIssd);
                if (dtIssd.Rows.Count == 0)
                {
                    Stat.Status = "Record not found.";
                    throw new ApplicationException(Stat.Status);
                }
                var issDate = Convert.ToDateTime(dtIssd.Rows[0]["issuedate"]);
                var dueDate = Convert.ToDateTime(dtIssd.Rows[0]["duedate"]);
                var issId = Convert.ToInt32(dtIssd.Rows[0]["issueid"]);
                var catid = Convert.ToInt32(dtIssd.Rows[0]["itemcategorycode"]);
                sqer = " select a.usercode,a.classname,b.totalissueddays,b.noofbookstobeissued,b.finperday,valuelimit ";
                sqer += " ,b.Days_1Phase,b.Amt_1Phase,b.Days_2Phase,b.Amt_2Phase,b.policystatus ";
                sqer += " from CircUserManagement  a join CircClassMaster b on a.classname=b.classname  where a.usercode='"+request.UserCode+"'";
                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.Transaction = tran;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqer;
                sda = new SqlDataAdapter(cmd);
                DataTable dtMem = new DataTable();
                sda.Fill(dtMem);
                decimal finepd = 0M;
                int issDays = 0;
                int phas1d = 0;
                int phas2d = 0;
                decimal phas1Amt = 0M;
                decimal phas2Amt = 0M;
                if (dtMem.Rows[0]["policystatus"].ToString().ToUpper() == "D")
                {
                    var rclas2 = " select classname,totalissueddays,noofbookstobeissued,finperday,reservedays,valueLimit ";
                    rclas2 += " ,Days_1Phase,Amt_1Phase,Days_2Phase,Amt_2Phase from classmasterloadingstatus ";
                    rclas2 += " where classname='" + dtMem.Rows[0]["classname"].ToString() + "' and LoadingStatus=" + catid;
                    cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.Transaction = tran;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = rclas2;

                    sda = new SqlDataAdapter(cmd);
                    DataTable dtMem2 = new DataTable();
                    sda.Fill(dtMem2);
                    if (dtMem2.Rows.Count == 0)
                    {
                        throw new ApplicationException("Error: Member issue status is category wise, no category based data found");
                    }
                    finepd = Convert.ToDecimal(dtMem2.Rows[0]["finperday"]);
                    issDays = Convert.ToInt32(dtMem2.Rows[0]["totalissueddays"]);
                    phas1d = Convert.ToInt32(dtMem2.Rows[0]["Days_1Phase"]);
                    phas2d = Convert.ToInt32(dtMem2.Rows[0]["Days_2Phase"]);
                    phas1Amt = Convert.ToDecimal(dtMem2.Rows[0]["Amt_1Phase"]);
                    phas2Amt = Convert.ToDecimal(dtMem2.Rows[0]["Amt_2Phase"]);
                }
                else
                {
                    finepd = Convert.ToDecimal(dtMem.Rows[0]["finperday"]);
                    issDays = Convert.ToInt32(dtMem.Rows[0]["totalissueddays"]);
                    phas1d = Convert.ToInt32(dtMem.Rows[0]["Days_1Phase"]);
                    phas2d = Convert.ToInt32(dtMem.Rows[0]["Days_2Phase"]);
                    phas1Amt = Convert.ToDecimal(dtMem.Rows[0]["Amt_1Phase"]);
                    phas2Amt = Convert.ToDecimal(dtMem.Rows[0]["Amt_2Phase"]);
                }
                decimal FineAmt = 0M;
                int extrDays = 0;
                bool Delayed=false;
                if (dueDate < DateTime.Today)
                {
                    Delayed = true;
                    extrDays = (DateTime.Today - dueDate).Days;
                    int totDays = (DateTime.Today - issDate).Days;
                    if (totDays < phas1d)
                    {
                        FineAmt = extrDays * finepd;
                    }
                    else if (totDays < phas2d)
                    {
                       var d1 =  phas1d-issDays;
                       FineAmt = d1 * finepd;
                        var d2 = phas2d - extrDays   ;
                        FineAmt += d2 * phas1Amt;
                    }
                    else
                    {
                        var d1 = phas1d - issDays;
                        FineAmt = d1 * finepd;
                        var d2 = phas2d - extrDays;
                        FineAmt += d2 * phas1Amt;
                    }
                    Stat.Fine = FineAmt;
                }
                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.Transaction = tran;
                cmd.CommandType = CommandType.StoredProcedure;
    cmd.CommandText = "insert_CircReceiveMaster_1";
                cmd.Parameters.Add(new SqlParameter("@userid_1", SqlDbType.VarChar));
                cmd.Parameters["@userid_1"].Value = request.UserCode;
                cmd.Parameters.Add(new SqlParameter("@totalfine_2", SqlDbType.Decimal));
                cmd.Parameters["@totalfine_2"].Value = FineAmt;
                cmd.Parameters.Add(new SqlParameter("@userid1_3", SqlDbType.VarChar));
                cmd.Parameters["@userid1_3"].Value = request.Opertor;
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.Transaction = tran;
                cmd.CommandType = CommandType.Text;
                sqer = "Select coalesce(max(tran_id),0,max(tran_id)) from CircReceiveTransaction ";
                cmd.CommandText = sqer;
                var tran_id = Convert.ToInt32(cmd.ExecuteScalar());
                sqer = "select isnull(max(id),0)+1 from CircReceiveTransaction";
                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.Transaction = tran;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqer;
                var id = Convert.ToInt32(cmd.ExecuteScalar());

                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = tran;
                cmd.CommandText = "insert_CircReceiveTrans_1";
                cmd.Parameters.Add(new SqlParameter("@userid_1", SqlDbType.VarChar));
                cmd.Parameters["@userid_1"].Value = request.UserCode;

                cmd.Parameters.Add(new SqlParameter("@accno_2", SqlDbType.VarChar));
                cmd.Parameters["@accno_2"].Value = request.Accno;
                cmd.Parameters.Add(new SqlParameter("@receivingdate_3", SqlDbType.Date));
  cmd.Parameters["@receivingdate_3"].Value = DateTime.Now;

                cmd.Parameters.Add(new SqlParameter("@fineamount_4", SqlDbType.Decimal));
                cmd.Parameters["@fineamount_4"].Value = FineAmt;

                cmd.Parameters.Add(new SqlParameter("@fineCause_5", SqlDbType.VarChar));
                cmd.Parameters["@fineCause_5"].Value = "delay";

                cmd.Parameters.Add(new SqlParameter("@isPaid_6", SqlDbType.VarChar));
                cmd.Parameters["@isPaid_6"].Value = "n";

                cmd.Parameters.Add(new SqlParameter("@DueDate_7", SqlDbType.Date));
                cmd.Parameters["@DueDate_7"].Value = dueDate;

                cmd.Parameters.Add(new SqlParameter("@paidon_8", SqlDbType.Date));
                //If Trim[paidon] = String.Empty Then
                cmd.Parameters["@paidon_8"].Value = DBNull.Value;
                //Else
                //      cmd.Parameters["@paidon_8"].Value = Convert.ToDateTime[paidon].ToString["dd-MMM-yyyy"]
                //End If


                cmd.Parameters.Add(new SqlParameter("@amtexp_9", SqlDbType.VarChar));
                cmd.Parameters["@amtexp_9"].Value = "";

                cmd.Parameters.Add(new SqlParameter("@userid1_10", SqlDbType.VarChar));
                cmd.Parameters["@userid1_10"].Value = request.Opertor;

  cmd.Parameters.Add(new SqlParameter("@IssueId_11", SqlDbType.Int));
                cmd.Parameters["@IssueId_11"].Value = issId;// IssueId


                cmd.Parameters.Add(new SqlParameter("@tran_id", SqlDbType.Int)) ;
                cmd.Parameters["@tran_id"].Value = tran_id;// i
                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
                cmd.Parameters["@Id"].Value = id;// Id
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.Transaction = tran;
                if ((!request.ByThumb) && (!request.ByRfid))
                {
                    cmd = conn.CreateCommand();
                    cmd.Connection = conn;
                    cmd.Transaction = tran;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = " update CircReceiveTransaction set modeid=1 where tran_id=" + tran_id;
                    cmd.ExecuteNonQuery();
                }
                if ((!request.ByThumb) && (request.ByRfid))
                {
                    cmd = conn.CreateCommand();
                    cmd.Connection = conn;
                    cmd.Transaction = tran;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "update CircReceiveTransaction set modeid=2 where tran_id=" + tran_id;
                    cmd.ExecuteNonQuery();
                }
                if ((request.ByThumb) && (request.ByRfid))
                {
                    cmd = conn.CreateCommand();
                    cmd.Connection = conn;
                    cmd.Transaction = tran;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "update CircReceiveTransaction set modeid=3 where tran_id=" + tran_id;
                    cmd.ExecuteNonQuery();
                }
                if ((request.ByThumb) && (!request.ByRfid))
                {
                    cmd = conn.CreateCommand();
                    cmd.Connection = conn;
                    cmd.Transaction = tran;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "update CircReceiveTransaction set modeid=6 where tran_id=" + tran_id;
                    cmd.ExecuteNonQuery();
                }
                cmd = conn.CreateCommand();
                cmd.Connection = conn;
                cmd.Transaction = tran;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update CircIssueTransaction set status=N'Received' where accno=N'" +request.Accno + "' and userid='" +request.UserCode + "' and status=N'Issued'";
                cmd.ExecuteNonQuery();
                cmd = conn.CreateCommand();
                cmd.Connection = conn;
                cmd.Transaction = tran;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update CircIssueMaster set currentissuedbooks=currentissuedbooks - 1 where userid='" +request.UserCode + "'";
                cmd.ExecuteNonQuery();
                cmd = conn.CreateCommand();
                cmd.Connection = conn;
                cmd.Transaction = tran;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update CircUSERMANAGEMENT set issuedbookstatus=issuedbookstatus-1 where userid='" +request.UserCode + "'";
                cmd.ExecuteNonQuery();
                cmd = conn.CreateCommand();
                cmd.Connection = conn;
                cmd.Transaction = tran;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from circbookstatus where accno='" +request.Accno + "'";
                cmd.ExecuteNonQuery();
                cmd = conn.CreateCommand();
                cmd.Connection = conn;
                cmd.Transaction = tran;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update bookaccessionmaster set CheckStatus='A' where accessionnumber='" + request.Accno+"'";
                cmd.ExecuteNonQuery();
                if (dtMem.Rows[0]["policystatus"].ToString().ToUpper() == "D")
                {
                    cmd = conn.CreateCommand();
                    cmd.Connection = conn;
                    cmd.Transaction = tran;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "update MemberIssueCategoryWise set issueCount=issueCount-1 where Memberid='" + request.UserCode + "' and  Category_id=" + 0;
                    cmd.ExecuteNonQuery();
                }

                //if delay
                if (Delayed)
                {
                    cmd = conn.CreateCommand();
                    cmd.Connection = conn;
                    cmd.Transaction = tran;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "update AccNoStistics set totalDays=totalDays +" + 1 + " where accno='" + request.Accno + "'";
                    cmd.ExecuteNonQuery();
                    cmd = conn.CreateCommand();
                    cmd.Connection = conn;
                    cmd.Transaction = tran;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "update MemberIssue_Return set total_Days=total_Days +" + 1 + " where User_id='" + request.UserCode + "'";
                    cmd.ExecuteNonQuery();

                }
                tran.Commit();
                conn.Close();
                Stat.IsSuccess = true;
                Stat.Status = "Received";

            }
            catch (Exception ex)
            {
                tran.Rollback();
                conn.Close();
                Stat.IsSuccess=false;
                Stat.Status = ex.Message;
            }

            return Stat;
        }
            public ReturnStat IssueBook(TransData request)
        {
            ReturnStat Stat = new ReturnStat();
            if ((string.IsNullOrEmpty(request.IssRet)) ||
                (string.IsNullOrEmpty(request.Accno)) ||
                (string.IsNullOrEmpty(request.UserCode)) ||
                (string.IsNullOrEmpty(request.Opertor)) )
            {
                Stat.Status = "Pass All values";
                Stat.IsSuccess = false;
                return Stat;
            }
            request.IssRet = request.IssRet.ToUpper().StartsWith("I") ? "I" : "R";
            request.UserCode = request.UserCode.Trim();
            request.Accno= request.Accno.Trim();
            request.Opertor= request.Opertor.Trim();
            try
            {

                SqlConnection conn = new SqlConnection();
            SqlTransaction tran;
            var constr = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;
            conn.ConnectionString = constr;
            conn.Open();
            tran = conn.BeginTransaction();
            string stat = "";
            try
            {
                var usr = "select usercode,classname,issuedbookstatus,validupto,status from CircUserManagement where usercode='" + request.UserCode.Trim() + "'";
                DataTable dtu = new DataTable();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = usr;
                cmd.Connection = conn;
                    cmd.Transaction = tran;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dtu);
                    if (dtu.Rows.Count == 0)
                    {
                        stat = "Member Not found";
                        throw new ApplicationException(stat);
                    }
                    if (dtu.Rows[0]["status"].ToString().ToLower() != "active")
                    {
                        stat = "Member is Not active";
                        throw new ApplicationException(stat);
                    }
                    var dvau = "select StopIssueRetirement from librarysetupinformation";
                    cmd = conn.CreateCommand();
                    cmd.CommandText = dvau;
                    cmd.Connection = conn;
                    cmd.Transaction = tran;
                    int StopIssdays=Convert.ToInt32(cmd.ExecuteScalar());
                    DateTime vu =Convert.ToDateTime( dtu.Rows[0][3]);
                if (vu < DateTime.Today.AddDays(-7))
                {
                    stat = "Member validity is about to expire";
                    throw new Exception(stat);
                }
                    DateTime vu2 = DateTime.Today.AddDays(StopIssdays);
                    if (vu2 > vu)
                    {
                        stat = "Stop Issue days exceeding";

                        throw new Exception(stat);
                    }
                    var itm = "select accessionnumber,ctrl_no,IssueStatus,CheckStatus,itemcategorycode,bookprice from bookaccessionmaster where accessionnumber='" + request.Accno.Trim() + "'";
                    DataTable dtItm = new DataTable();
                    cmd = conn.CreateCommand();
                    cmd.CommandText = itm;
                    cmd.Connection = conn;
                    cmd.Transaction = tran;
                    sda = new SqlDataAdapter(cmd);
                    sda.Fill(dtItm);
                    if (dtItm.Rows.Count==0)
                    {
                        stat = "Book Not found";
                        throw new ApplicationException(stat);
                    }
                    if (dtItm.Rows[0]["issuestatus"].ToString().ToUpper() != "Y")
                    {
                        stat = "Book is not issuable";
                        throw new ApplicationException(stat);
                    }
                    if (dtItm.Rows[0]["CheckStatus"].ToString().ToUpper() != "A")
                    {
                        stat = "Book is not Available for Issue";
                        throw new ApplicationException(stat);
                    }
                    var rclas = "select a.classname,a.totalissueddays,a.noofbookstobeissued,a.finperday,a.reservedays,a.valueLimit ";
                    rclas += " ,a.Days_1Phase,a.Amt_1Phase,a.Days_2Phase,a.Amt_2Phase,a.policystatus from CircClassMaster a join circusermanagement b on a.classname=b.classname ";
                    rclas += " where b.usercode='" + request.UserCode.Trim()+"'";
                    DataTable dtclas1 = new DataTable();
                    cmd = conn.CreateCommand();
                    cmd.CommandText = rclas;
                    cmd.Connection = conn;
                    cmd.Transaction = tran;
                    sda = new SqlDataAdapter(cmd);
                    sda.Fill(dtclas1);
                    if (dtclas1.Rows.Count==0)
                    {
                        stat = "Internal error, member classs not found";
                        throw new ApplicationException(stat);
                    }
                    var rexist = "select accno,issuedate,IssueId,b.bookprice from CircIssueTransaction a join bookaccessionmaster b on a.accno=b.accessionnumber ";
                    rexist +=" where a.userid='" + request.UserCode+"' AND a.status='Issued' ";
                    DataTable dtIssd = new DataTable();
                    cmd = conn.CreateCommand();
                    cmd.CommandText = rexist;
                    cmd.Connection = conn;
                    cmd.Transaction = tran;
                    sda = new SqlDataAdapter(cmd);
                    sda.Fill(dtIssd);
                    int issdn = dtIssd.Rows.Count;// 
                    decimal Issdprice = 0M;
                    foreach (DataRow dr in dtIssd.Rows)
                        Issdprice += Convert.ToDecimal(dr["bookprice"]);
                    // Convert.ToDecimal(issdn);
                    var policy = dtclas1.Rows[0]["policystatus"].ToString().ToUpper();
                    DateTime tday = DateTime.Today;
                    int issdays = 0;
                    if (policy == "S")
                    {
                        int IssLimt= Convert.ToInt32(dtclas1.Rows[0]["noofbookstobeissued"]);
                        if (IssLimt <= issdn)
                        {
                            stat = "Issued limit exceeds, already issued "+issdn.ToString();
                            throw new ApplicationException(stat);
                        }
                         issdays = Convert.ToInt32(dtclas1.Rows[0]["totalissueddays"]);
                        var valUp1=tday.AddDays(issdays);
                        if (valUp1 > vu)
                        {
                            stat = "Member's validity period is exceeding";
                            throw new ApplicationException(stat);
                        }
                        decimal vallimt = Convert.ToDecimal(dtclas1.Rows[0]["valuelimit"]);
                        if (vallimt > 0)
                        {
                            var price = Convert.ToDecimal(dtItm.Rows[0]["bookprice"]);
                            if (vallimt < (Issdprice+price))
                            {
                                stat = "Issued Item Value limit excedds, already issued Rs." + Issdprice.ToString();
                                throw new ApplicationException(stat);
                            }
                        }
                        var rtoday = string.Format("{0:yyyy-MM-dd}", DateTime.Today);

                    }
                    else
                    {
                        DataTable dtclas2 = new DataTable();
                        var rclas2 = " select classname,totalissueddays,noofbookstobeissued,finperday,reservedays,valueLimit ";
                        rclas2 += " ,Days_1Phase,Amt_1Phase,Days_2Phase,Amt_2Phase from classmasterloadingstatus ";
                        rclas2 += " where classname='" + dtu.Rows[0]["classname"].ToString() + "' and LoadingStatus=" + dtItm.Rows[0]["itemcategorycode"].ToString();
                        cmd = conn.CreateCommand();
                        cmd.CommandText = rclas2;
                        cmd.Connection = conn;
                        cmd.Transaction = tran;
                        sda = new SqlDataAdapter(cmd);
                        sda.Fill(dtclas2);
                        if (dtclas2.Rows.Count == 0)
                        {
                            stat = "Book Category is not allowed for Member";
                            throw new ApplicationException(stat);
                        }
                        int IssLimt = Convert.ToInt32(dtclas2.Rows[0]["noofbookstobeissued"]);
                        if (IssLimt <= issdn)
                        {
                            stat = "Issued limit exceeds, already issued " + issdn.ToString();
                            throw new ApplicationException(stat);
                        }
                        decimal vallimt = Convert.ToDecimal(dtclas2.Rows[0]["valuelimit"]);
                        issdays = Convert.ToInt32(dtclas2.Rows[0]["totalissueddays"]);
                        var valUp2 = tday.AddDays(issdays);
                        if (valUp2 > vu)
                        {
                            stat = "Member's validity period is exceeding";
                            throw new ApplicationException(stat);
                        }

                        if (vallimt > 0)
                        {
                            var price = Convert.ToDecimal(dtItm.Rows[0]["bookprice"]);
                            if (vallimt < (Issdprice + price))
                            {
                                stat = "Issued Item Value limit excedds, already issued Rs." + Issdprice.ToString();
                                throw new ApplicationException(stat);
                            }
                        }
                    }
                    var alriss = "select count(*) from CircIssueTransaction where status='Issued'  ";
                    alriss += " and accno in (select accessionnumber from bookaccessionmaster where ctrl_no=" + dtItm.Rows[0]["ctrl_no"].ToString()+" ) ";
                    cmd = conn.CreateCommand();
                    cmd.CommandText = alriss;
                    cmd.Connection = conn;
                    cmd.Transaction = tran;
                    var cn =Convert.ToInt32(cmd.ExecuteScalar());   
                    if (cn > 0)
                    {
                        stat = "Another copy of Book is already Issued to member";
                        throw new ApplicationException(stat);
                    }
                    bool isReserv = false;
                    int Queno = 0;
                    var avaCp = "select count(*) from bookaccessionmaster where accessionnumber='" + request.Accno.Trim() + "' and checkstatus='A' ";
                    cmd = conn.CreateCommand();
                    cmd.CommandText = avaCp;
                    cmd.Connection = conn;
                    cmd.Transaction = tran;
                    var avcn = Convert.ToInt32(cmd.ExecuteScalar());
                    bool res = false;
                    if (avcn>1)
                    {
                        res = true;
                    }
                    if (!res)
                    {
                        res = IsReserved(request.Accno.Trim(), request.UserCode, constr, ref isReserv, ref Queno);
                    }
                    if (!res)
                    {
                        stat = "Book is reserved by another member";
                        throw new ApplicationException(stat);
                    }
                    DateTime retDate = DateTime.Today.AddDays(issdays);
                   DateTime dateTod=DateTime.Today;
                    for (int i = 0; i < issdays; i++)
                    {
                        var strHdays = "select count(*) from circHolidays where h_date='" + dateTod.ToString("yyyy-MM-dd") + "'";
                        cmd = conn.CreateCommand();
                        cmd.CommandText = strHdays;
                        cmd.Connection = conn;
                        cmd.Transaction = tran;
                        var hd=Convert.ToInt32(cmd.ExecuteScalar());
                        if (hd > 0)
                        {
                            retDate = retDate.AddDays(1);
                        }
                        retDate = retDate.AddDays(1);
                    }
                    var strIssid = "select isnull(max(issueid),0)+1 from CircIssueTransaction ";
                    cmd = conn.CreateCommand();
                    cmd.CommandText = strIssid;
                    cmd.Connection = conn;
                    cmd.Transaction = tran;
                    var issId=Convert.ToInt32(cmd.ExecuteScalar());
                    cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "insert_CircIssueMaster_1";
                    cmd.Connection = conn;
                    cmd.Transaction = tran;
                    cmd.Parameters.Add(new SqlParameter("@userid_1", SqlDbType.VarChar));
                    cmd.Parameters["@userid_1"].Value = request.UserCode.Trim();
                    cmd.Parameters.Add(new SqlParameter("@currentissuedbooks_2", SqlDbType.Int));
                    cmd.Parameters["@currentissuedbooks_2"].Value = 1;
                    cmd.Parameters.Add(new SqlParameter("@userid1_3", SqlDbType.VarChar));
                    cmd.Parameters["@userid1_3"].Value = request.Opertor;
                    cmd.ExecuteNonQuery();
                    cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    cmd.Transaction = tran;

                    cmd.CommandText = "insert_CircIssueTransaction_1";
                    cmd.Parameters.Add(new SqlParameter("@userid_1", SqlDbType.VarChar));
                    cmd.Parameters["@userid_1"].Value = request.UserCode.Trim();
                    cmd.Parameters.Add(new SqlParameter("@accno_2", SqlDbType.VarChar));
                    cmd.Parameters["@accno_2"].Value = request.Accno.Trim();
                    cmd.Parameters.Add(new SqlParameter("@issuedate_3", SqlDbType.Date));
                    cmd.Parameters["@issuedate_3"].Value = DateTime.Now;
                    cmd.Parameters.Add(new SqlParameter("@duedate_4", SqlDbType.Date));
                    cmd.Parameters["@duedate_4"].Value = retDate;
                    cmd.Parameters.Add(new SqlParameter("@status_5", SqlDbType.VarChar));
                    cmd.Parameters["@status_5"].Value = "Issued";
                    cmd.Parameters.Add(new SqlParameter("@userid1_7", SqlDbType.VarChar));
                    cmd.Parameters["@userid1_7"].Value = request.Opertor;
                    cmd.Parameters.Add(new SqlParameter("@IssueId_8", SqlDbType.Int));
                    cmd.Parameters["@IssueId_8"].Value = issId;
                    cmd.ExecuteNonQuery();
                    if ((!request.ByThumb) && (!request.ByRfid))
                    {
                        cmd = conn.CreateCommand();
                        cmd.Connection = conn;
                        cmd.Transaction = tran;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "update circissuetransaction set modeid=1 where issueid=" + issId;
                        cmd.ExecuteNonQuery();
                    }
                    if ((!request.ByThumb) && (request.ByRfid))
                    {
                        cmd = conn.CreateCommand();
                        cmd.Connection = conn;
                        cmd.Transaction = tran;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "update circissuetransaction set modeid=2 where issueid=" + issId;
                        cmd.ExecuteNonQuery();
                    }
                    if ((request.ByThumb) && (request.ByRfid))
                    {
                        cmd = conn.CreateCommand();
                        cmd.Connection = conn;
                        cmd.Transaction = tran;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "update circissuetransaction set modeid=3 where issueid=" + issId;
                        cmd.ExecuteNonQuery();
                    }
                    if ((request.ByThumb) && (!request.ByRfid))
                    {
                        cmd = conn.CreateCommand();
                        cmd.Connection = conn;
                        cmd.Transaction = tran;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "update circissuetransaction set modeid=6 where issueid=" + issId;
                        cmd.ExecuteNonQuery();
                    }
                    cmd = conn.CreateCommand();
                    cmd.Connection = conn;
                    cmd.Transaction = tran;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "insert_CircBookStatus_1";
                    cmd.Parameters.Add(new SqlParameter("@accno_1", OleDbType.VarChar));
                    cmd.Parameters["@accno_1"].Value = request.Accno.Trim();
                    cmd.ExecuteNonQuery();
                    cmd = conn.CreateCommand();
                    cmd.Connection = conn;
                    cmd.Transaction = tran;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "update CircUserManagement set issuedbookstatus=issuedbookstatus+1 where userid='" + request.UserCode.Trim() + "'";
                    cmd.ExecuteNonQuery();
                    cmd = conn.CreateCommand();
                    cmd.Connection = conn;
                    cmd.Transaction = tran;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "update bookaccessionmaster set CheckStatus=N'I' where accessionnumber=N'" + request.Accno.Trim() + "'";
                    cmd.ExecuteNonQuery();
                    if (policy == "D")
                    {
                        cmd = conn.CreateCommand();
                        cmd.Connection = conn;
                        cmd.Transaction = tran;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "insert_MemIssCtgWise_1";
                        cmd.Parameters.Add(new SqlParameter("@Memberid_1", SqlDbType.VarChar));
                        cmd.Parameters["@Memberid_1"].Value = request.UserCode.Trim();
                        cmd.Parameters.Add(new SqlParameter("@Category_id_2", SqlDbType.Int));
                        cmd.Parameters["@Category_id_2"].Value = dtItm.Rows[0]["itemcategorycode"].ToString();
                        cmd.ExecuteNonQuery();
                    }
                    if (isReserv)
                    {
                        var ctrlno = dtItm.Rows[0]["ctrl_no"].ToString().Trim();
                        cmd = conn.CreateCommand();
                        cmd.Connection = conn;
                        cmd.Transaction = tran;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "update Circbookque set quenumber=quenumber-1 where ctrlNo='" + ctrlno+"'";
                        cmd.ExecuteNonQuery();

                        cmd = conn.CreateCommand();
                        cmd.Connection = conn;
                        cmd.Transaction = tran;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "update Circbookreservations set reservations=reservations-1 where ctrlNo='" + ctrlno + "'";
                        cmd.ExecuteNonQuery();

                        cmd = conn.CreateCommand();
                        cmd.Connection = conn;
                        cmd.Transaction = tran;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "update Circuserreservations set totalreservations=totalreservations-1 where userid=N'" + request.UserCode.Trim() + "'";
                        cmd.ExecuteNonQuery();

                        cmd = conn.CreateCommand();
                        cmd.Connection = conn;
                        cmd.Transaction = tran;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "delete from circreservationtransaction where userid=N'" + request.UserCode.Trim() + "' and ctrlno='" + ctrlno + "'";
                        cmd.ExecuteNonQuery();

                        cmd = conn.CreateCommand();
                        cmd.Connection = conn;
                        cmd.Transaction = tran;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "update circreservationtransaction set queno=queno-1 where ctrlNo=" + ctrlno + " and queno>" + Queno;
                        cmd.ExecuteNonQuery();
                    }
                    tran.Commit();
                conn.Close();
                   Stat.IsSuccess= true;
                    Stat.Status = "Issued..";
            }
            catch (Exception exc)
            {
                tran.Rollback();
                conn.Close();
                Stat.IsSuccess = false;
                Stat.Status=exc.Message;
            }
            }
            catch (Exception ex)
            {
                Stat.IsSuccess = false;
                Stat.Status ="Data Connection error:"+ ex.Message;

            }

            return Stat;
        }

        public bool IsReserved(string accno,string usercode,string connstr,ref bool IsReserved,ref int Queno)
        {
            bool Allow=false;
            SqlConnection conn=new SqlConnection(connstr);
            conn.Open();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                accno = accno.Trim().ToUpper();
                usercode = usercode.Trim().ToUpper();
                var rQry = "SELECT  userid,cast(ctrlNo as integer) ctrlno,reservationdate,queno,id  from circreservationtransaction  where ctrlNo=(select ctrl_no from bookaccessionmaster ";
                rQry += " where accessionnumber='" + accno + "')  order by reservationdate ";// desc";
                cmd.CommandText = rQry;
                cmd.Connection = conn;
                DataTable dtres1 = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dtres1);
                var sqcateg = "select itemcategorycode from bookaccessionmaster where accessionnumber='" + accno + "'";
                cmd=new SqlCommand(sqcateg);
                cmd.Connection = conn;
                var catid =cmd.ExecuteScalar().ToString();
                IsReserved = false;
                Queno = 0;
                if (dtres1.Rows.Count > 0)
                {
                    Queno = Convert.ToInt32( dtres1.Rows[0]["queno"]);
                    IsReserved = true;
                    var resSmCtrl = "select count(*) from bookaccessionmaster  where ctrl_no=" + dtres1.Rows[0]["ctrlno"].ToString();
                    resSmCtrl += " and checkstatus='A' and issuestatus='Y'";
                    cmd = conn.CreateCommand();
                    cmd.CommandText = resSmCtrl;
                    cmd.Connection = conn;
                    var anoAvail = Convert.ToInt32(cmd.ExecuteScalar());
                    if (anoAvail > 1)
                        Allow = true;
                    if (!Allow)
                    {
                        var usr = "select usercode,classname,issuedbookstatus,validupto,status from CircUserManagement where usercode='" + usercode + "'";
                        cmd = conn.CreateCommand();
                        cmd.CommandText = usr;
                        cmd.Connection = conn;
                        DataTable dtMem = new DataTable();
                        sda = new SqlDataAdapter(cmd);
                        sda.Fill(dtMem);
                        int resdays = 0;
                        if (dtMem.Rows[0]["status"].ToString().ToUpper() == "S")
                        {
                            var rclas = "select classname,totalissueddays,noofbookstobeissued,finperday,reservedays,valueLimit ";
                            rclas += " ,Days_1Phase,Amt_1Phase,Days_2Phase,Amt_2Phase,policystatus from CircClassMaster where classname='" + dtMem.Rows[0]["classname"].ToString() + "'";
                            cmd = conn.CreateCommand();
                            cmd.CommandText = rclas;
                            cmd.Connection = conn;
                            DataTable dtRes1 = new DataTable();
                            sda = new SqlDataAdapter(cmd);
                            sda.Fill(dtRes1);
                            resdays = Convert.ToInt32(dtRes1.Rows[0]["reservedays"]);
                        }
                        else
                        {
                            var rclas2 = " select classname,totalissueddays,noofbookstobeissued,finperday,reservedays,valueLimit ";
                            rclas2 += " ,Days_1Phase,Amt_1Phase,Days_2Phase,Amt_2Phase from classmasterloadingstatus ";
                            rclas2 += " where classname='" + dtMem.Rows[0]["classname"].ToString() + "' and LoadingStatus=" + catid;
                            cmd = conn.CreateCommand();
                            cmd.CommandText = rclas2;
                            cmd.Connection = conn;
                            DataTable dtRes2 = new DataTable();
                            sda = new SqlDataAdapter(cmd);
                            sda.Fill(dtRes2);
                            resdays = Convert.ToInt32(dtRes2.Rows[0]["reservedays"]);
                        }
                        var rlastRecdate = "select max(receivingdate) as receivingdate From CircReceiveTransaction where accno='" + accno + "'";
                        cmd = conn.CreateCommand();
                        cmd.CommandText = rlastRecdate;
                        cmd.Connection = conn;
                        DataTable dtResrv1 = new DataTable();
                        sda = new SqlDataAdapter(cmd);
                        sda.Fill(dtResrv1);
                        DateTime latResDate = DateTime.Today;
                        if (dtResrv1.Rows.Count > 0)
                            latResDate = Convert.ToDateTime(dtResrv1.Rows[0][0]);
                        var dtres1cp = dtres1.Clone();
                        foreach (DataRow r in dtres1.Rows)
                        {
                            if (Convert.ToDateTime(r["reservationdate"]) < latResDate)
                                continue;
                            dtres1cp.Rows.Add(r);
                        }
                        for (int ixd = 0; ixd < dtres1cp.Rows.Count; ixd++)
                        {
                            var rdate = Convert.ToDateTime(dtres1cp.Rows[ixd]["reservationdate"]);
                            var rmem = dtres1cp.Rows[ixd]["userid"].ToString();
                            if (rmem.ToUpper() == usercode)
                            {
                                Allow = true;
                                break;
                            }
                            rdate = rdate.AddDays(resdays);
                            if (rdate.Date <= DateTime.Today.Date)
                            {
                                Allow = false;
                                conn.Close();

                                return Allow;
                            }
                        }
                    }
                }
                Allow = true;
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                throw new ApplicationException(ex.Message);
            }


            return Allow;
        }
    }

    public class TransData
    {
        public string UserCode { get; set; }
        public string Accno { get; set; }
        public string IssRet { get; set; }
        public string Opertor { get; set; }
        public bool ByThumb { get; set; }
        public bool ByRfid { get; set; }
    }
    public class ReturnStat
    {
        public string Status { get; set; }
        public decimal Fine { get; set; }
        public bool IsSuccess { get; set; }
    }
}