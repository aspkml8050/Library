using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Library
{
    public partial class UCCatalogShow : System.Web.UI.UserControl
    {
        public GridView grdItems
        {
            get { return this.grdDataUC; }
        }

        public List<string> GetSelectAccn
        {
            get { return getAccns(); }
        }

        private List<string> getAccns()
        {
            List<string> accns = new List<string>();
            foreach (GridViewRow item in grdDataUC.Rows)
            {
                var chk = (CheckBox)item.FindControl("chkSav");
                if (chk.Checked)
                {
                    var accn = (LinkButton)item.FindControl("lnkAccn");
                    accns.Add(accn.Text);
                }
            }
            return accns;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string se = sender.GetType().FullName;
            try
            {
                Button b = (Button)sender;
            }
            catch
            {
                if (Session["ucAccnData"] != null)
                {
                     grdDataUC.DataSource = Session["ucAccnData"];
                    grdDataUC.DataBind();
                }
            }
            if (!IsPostBack)
            {
                fillSearch();
            }
        }
        private void fillSearch()  //this code is duplicated with patron and is common
        {
            GlobClassTr gClas = new GlobClassTr();
            gClas.TrOpen();
            try
            {

                string dold = string.Format("{0:dd-MMM-yyyy}", DateTime.Today.AddMonths(-3));
                string qer;
                qer = "delete from searchhistory where sdate <'" + dold + "'";
                gClas.IUD(qer);
                gClas.TrClose(); //commit 
                gClas.TrOpen();
                qer = "select count(*)  from SearchHistory ";
                int c = (int)gClas.ExScaler(qer);
                if (c > 3000)
                {
                    qer = "select SearchText,SearchCondition,InternCond,Sdate,SPage  from SearchHistory  ";
                    qer += " /*where SPage='" + spage + "' */ order by Sdate  ";
                    DataTable dtHist = gClas.DataT(qer);
                    qer = "delete from searchhistory ";
                    gClas.IUD(qer);
                    int id;
                    qer = "select isnull(max(searchid),0)+1 from SearchHistory ";
                    id = (int)gClas.ExScaler(qer);
                    for (int indX = 0; indX < dtHist.Rows.Count; indX++)
                    {
                        string st = dtHist.Rows[indX][0].ToString();
                        string sc = dtHist.Rows[indX][1].ToString().Replace("'", "''");
                        string ic = dtHist.Rows[indX][2].ToString();
                        SqlParameter p = new SqlParameter("@dat", SqlDbType.DateTime);
                        p.Value = Convert.ToDateTime(dtHist.Rows[indX][3]);
                        string sp = dtHist.Rows[indX][4].ToString();
                        List<SqlParameter> lsp = new List<SqlParameter>();
                        lsp.Add(p);
                        qer = " insert into SearchHistory ";
                        qer += " (SearchId, SearchText, SearchCondition, InternCond, Sdate, SPage) values(";
                        qer += id + ",'" + st + "',N'" + sc + "','" + ic + "',@dat,'" + spage + "' )";
                        gClas.IUD(qer, lsp.ToArray());
                        id++;
                    }
                    gClas.TrClose();
                    gClas.TrOpen();
                }

                qer = "select top 2000 row_number() over(order by sdate desc) sno, * from searchhistory where spage='" + spage + "'  ";
                DataTable dts = gClas.DataT(qer);
                grdSHUC.DataSource = dts;
                grdSHUC.DataBind();
                for (int inX = 0; inX < grdSHUC.Rows.Count; inX++)
                {
                    //  TextBox t = (TextBox)grdSHUC.Rows[inX].FindControl("txSel10");
                    //t.Attributes.Add("onblur", "sel10(this);");
                }
            }
            catch (Exception exp)
            {
                string s = exp.Message;
            }
            gClas.TrClose();

        }
        int histid = 0; //if this has value, search condition is applied and being searched
        string stext = ""; //these will be saved search cond
        string scond = "";
        string sIntCond = "";
        //table SearchHistory/InternCond has 2 conditions here
        //'setno'/'onlyaccn'/'emptycallno'/'emptysubj'/''  - see below
        //page is uccatalogshow
        string spage = "uccatalogshow"; //applicable for this user control
        string orgStext = ""; //if histid >0, these have values from search history saved previously
        string orgScond = "";

        protected void btnUCSh_Click(object sender, EventArgs e)
        {
            string sQer = "";
            labUCMesg.Text = "";
            GlobClassTr gCl = new GlobClassTr();
            Session["ucAccnData"] = null;
            if ((chkAccnUC.Checked) && (txtAccFUC.Text.Trim() == ""))
                chkAccnUC.Checked = false;

            try
            {

                if (txSetNoUC.Text.Trim() == "") //when search hist clicked, and set of rec applied, this will have value
                {
                    sQer = "select distinct top 500 a.accessionnumber,a.booktitle,a.copynumber, replace( convert(varchar, a.catalogdate,106),' ','-') catdate, REPLACE(f.firstname1+' '+f.middlename1+' '+f.lastname1,'  ',' ') author, ";
                    sQer += "b.edition,REPLACE(e.classnumber+' '+e.booknumber,'  ',' ') callno,c.firstname+', '+d.percity publisher,'' keywords, /*skipped */  ";
                    sQer += " REPLACE(f.firstname2+' '+f.middlename2+' '+f.lastname2,'  ',' ') author2,";
                    sQer += " REPLACE(f.firstname3+' '+f.middlename3+' '+f.lastname3,'  ',' ') author3,isnull(b.subject1,'' )sub1,isnull(b.subject2,'') sub2,isnull(b.subject3,'') sub3,'' subject, ";
                    sQer += " h.departmentname, i.Category_LoadingStatus,j.ItemStatus,a.ctrl_no ";
                    sQer += " from bookaccessionmaster a join BookCatalog b on a.ctrl_no=b.ctrl_no ";
                    sQer += " join publishermaster c on b.publishercode=c.PublisherId  ";
                    sQer += " join AddressTable d on c.PublisherId =d.addid and d.addrelation='publisher' ";
                    sQer += " join CatalogData e on a.ctrl_no=e.ctrl_no join BookAuthor f on a.ctrl_no=f.ctrl_no ";
                    sQer += " join departmentmaster h on a.DeptCode=h.departmentcode ";
                    sQer += "      join CategoryLoadingStatus i on a.ItemCategoryCode = i.Id ";
                    sQer += " join ItemStatusMaster j on a.Status=j.ItemStatusID ";
                    sQer += " left join Journal_Keyword g on a.ctrl_no=g.Journal_No "; //this causes dup rows as multiple keywords
                    sQer += " where 1=1 ";
                    string sWhe = "";
                    if (!chkAccnUC.Checked)
                    {

                    }
                    if (txtAccnNoUC.Text.Trim() == "")
                    {

                    if (txtAccFUC.Text.Trim() != "")
                    {
                        stext = " accno >= " + txtAccFUC.Text.Trim() + "; ";
                        sWhe += " and a.accessionnumber>='" + txtAccFUC.Text.Trim() + "' ";

                    }
                    if (txtAccTUC.Text.Trim() != "")
                    {
                        stext += " accno <= " + txtAccTUC.Text.Trim() + "; ";
                        sWhe += " and a.accessionnumber<='" + txtAccTUC.Text.Trim() + "' ";

                    }
                    if (txTitleUC.Text.Trim() != "")
                    {
                        stext += " title contains " + txTitleUC.Text.Trim() + "; ";
                        sWhe += " and a.booktitle like N'%" + txTitleUC.Text.Trim().Replace("'", "''") + "%' ";
                    }
                    if (txCatDFUC.Text.Trim() != "")
                    {
                        stext += " catalog date >= " + txCatDFUC.Text.Trim() + "; ";
                        sWhe += " and a.catalogdate>='" + txCatDFUC.Text.Trim() + "' ";

                    }
                    if (txCatDUUC.Text.Trim() != "")
                    {
                        stext += " catalog date <= " + txCatDFUC.Text.Trim() + "; ";
                        sWhe += " and a.catalogdate<='" + txCatDUUC.Text.Trim() + "' ";

                    }
                    if (txSubjUC.Text.Trim() != "")
                    {
                        stext += " subject contains " + txSubjUC.Text.Trim() + "; ";
                        sWhe += " and (b.subject1 like N'%" + txSubjUC.Text.Trim() + "%' or b.subject2 like N'%" + txSubjUC.Text.Trim() + "%' )";

                    }
                    if (txDeptUC.Text.Trim() != "")
                    {
                        stext += " department name contains " + txDeptUC.Text.Trim() + "; ";
                        sWhe += " and h.departmentname like N'%" + txDeptUC.Text.Trim() + "%' ";

                    }
                    if (txCategUC.Text.Trim() != "")
                    {
                        stext += " category contains " + txCategUC.Text.Trim() + "; ";
                        sWhe += " and i.Category_LoadingStatus like N'%" + txCategUC.Text.Trim() + "%' ";

                    }
                    if (txPublUC.Text.Trim() != "")
                    {
                        stext += " publisher name city contains " + txPublUC.Text.Trim() + "; ";
                        sWhe += " and (c.firstname like N'%" + txPublUC.Text.Trim().Replace("'", "''") + "%' or d.percity=N'%" + txPublUC.Text.Trim() + "%' ";
                        sWhe += " or (c.firstname+', '+d.percity) like N'" + txPublUC.Text.Trim().Replace("'", "''") + "' )";

                    }
                    if (txAuthUC.Text.Trim() != "")
                    {
                        stext += " author contains " + txAuthUC.Text + "; ";
                        sWhe += " and (REPLACE(f.firstname1+' '+f.middlename1+' '+f.lastname1,'  ',' ') like N'%" + txAuthUC.Text.Trim() + "%' ";
                        sWhe += " or REPLACE(f.firstname2+' '+f.middlename2+' '+f.lastname2,'  ',' ') like N'%" + txAuthUC.Text.Trim() + "%'  ";
                        sWhe += " or REPLACE(f.firstname3+' '+f.middlename3+' '+f.lastname3,'  ',' ') like N'%" + txAuthUC.Text.Trim() + "%' ) ";
                    }
                    if (txSearchTextUC.Text.Trim() != "")
                    {
                        if (chkExactUC.Checked)
                            sWhe += " and a.searchtext=N'" + txSearchTextUC.Text.Trim().Replace("'", "''") + "' ";
                        else
                            sWhe += " and a.searchtext like N'%" + txSearchTextUC.Text.Trim().Replace("'", "''") + "%' ";
                    }
                    if (txKeysUC.Text.Trim() != "")
                    {
                        string[] k = txKeysUC.Text.Trim().Split(',');
                        string kIn = " and g.keyword in (";
                        foreach (string a in k)
                        {
                            kIn += "'" + a + "',";
                        }
                        kIn = kIn.Substring(0, kIn.Length - 1) + ")";
                        sWhe += kIn;
                        stext += " keywords " + txKeysUC + "; ";

                    }
                    //table SearchHistory/InternCond has 2 conditions here
                    //'setno'/'onlyaccn'/'emptycallno'/'emptysubj'/''
                    if (chkEmptSubj.Checked)  //this is set when search history clicked
                    {
                        sIntCond = "emptysubj,";
                        sWhe += " and ltrim(rtrim(b.subject1))='' ";
                        stext += "Empty subject; ";

                    }
                    if (chkEmptCallNo.Checked)
                    {
                        sIntCond += "emptycallno,";
                        sWhe += " and ltrim(rtrim(e.classnumber))='' and ltrim(rtrim(e.booknumber))=''";
                        stext += "Empty call no; ";

                    }

                    if (chkAccnUC.Checked) //if this appied - other conditions ignored
                    {
                        sIntCond = "onlyaccn"; //only this cond applied
                        stext = " search only accn contains " + txtAccFUC.Text.Trim();
                        scond = " and a.accessionnumber like '%" + txtAccFUC.Text.Trim() + "%' ";
                        sWhe = " and a.accessionnumber like '%" + txtAccFUC.Text.Trim() + "%' ";

                    }
                    sWhe += " order by a.accessionnumber ";
                    scond = sWhe; //saved in hist
                    if (histid > 0)
                    {
                        sWhe = orgScond;
                        stext = orgStext;
                        scond = orgScond;
                    }
                    }
                    else
                    {
                        sWhe += "and a.accessionnumber like '%" + txtAccnNoUC.Text.Trim() + "%' ";
                    }
                    sQer += sWhe;
                    GlobClassTr clas = new GlobClassTr();
                    try
                    {
                        clas.TrOpen();
                        string aqer = " select ISNULL(max(searchid),0)+1 from SearchHistory ";
                        string id = clas.ExScaler(aqer).ToString();
                        aqer = " insert into SearchHistory ";
                        aqer += " (SearchId, SearchText, SearchCondition, InternCond, Sdate, SPage) values(";
                        aqer += id + ",'" + stext + "',N'" + scond.Replace("'", "''") + "','" + sIntCond + "',@dat,'" + spage + "' )";
                        SqlParameter p = new SqlParameter("@dat", SqlDbType.DateTime);
                        p.Value = DateTime.Now;
                        List<SqlParameter> lsp = new List<SqlParameter>();
                        lsp.Add(p);
                        // clas.IUD(aqer, lsp.ToArray());
                        clas.TrClose();
                    }
                    catch (Exception exp)
                    {
                        clas.TrRollBack();
                        string er = exp.Message;
                    }
                }
                else
                {
                    try
                    {
                        int Sn;
                        Sn = Convert.ToInt32(txSetNoUC.Text);
                        stext = " Set No = " + Sn + " of 500";
                        sIntCond = "setno";
                        scond = Sn.ToString();

                    }
                    catch
                    {
                        gCl.TrRollBack();

                    }
                    sQer = "select * from ( ";
                    sQer += "select ROW_NUMBER() over(order by a.accessionnumber) rn, a.accessionnumber,a.booktitle,a.copynumber, replace( convert(varchar, a.catalogdate,106),' ','-') catdate, REPLACE(f.firstname1+' '+f.middlename1+' '+f.lastname1,'  ',' ') author, ";
                    sQer += "b.edition,REPLACE(e.classnumber+' '+e.booknumber,'  ',' ') callno,c.firstname+', '+d.percity publisher,'' keywords, /*skipped */  ";
                    sQer += " REPLACE(f.firstname2+' '+f.middlename2+' '+f.lastname2,'  ',' ') author2,";
                    sQer += " REPLACE(f.firstname3+' '+f.middlename3+' '+f.lastname3,'  ',' ') author3,isnull(b.subject1,'' )sub1,isnull(b.subject2,'') sub2,isnull(b.subject3,'') sub3,'' subject, ";
                    sQer += " h.departmentname, i.Category_LoadingStatus,j.ItemStatus,a.ctrl_no ";
                    sQer += " from bookaccessionmaster a join BookCatalog b on a.ctrl_no=b.ctrl_no ";
                    sQer += " join publishermaster c on b.publishercode=c.PublisherId  ";
                    sQer += " join AddressTable d on c.PublisherId =d.addid and d.addrelation='publisher' ";
                    sQer += " join CatalogData e on a.ctrl_no=e.ctrl_no join BookAuthor f on a.ctrl_no=f.ctrl_no ";
                    sQer += " join departmentmaster h on a.DeptCode=h.departmentcode ";
                    sQer += "      join CategoryLoadingStatus i on a.ItemCategoryCode = i.Id ";
                    sQer += " join ItemStatusMaster j on a.Status=j.ItemStatusID ";
                    sQer += " left join Journal_Keyword g on a.ctrl_no=g.Journal_No ) ts ";
                    sQer += " where 1=1 ";

                    sQer += " and ts.rn>(" + txSetNoUC.Text + "-1)*500 and ts.rn <=(" + txSetNoUC.Text + ")*500 ";

                }
                gCl.TrOpen();
                DataTable dtD = gCl.DataT(sQer);
                for (int indX = 0; indX < dtD.Rows.Count; indX++)
                {
                    string Auth2 = dtD.Rows[indX].IsNull("author2") ? "" : dtD.Rows[indX]["author2"].ToString().Trim();
                    string Auth3 = dtD.Rows[indX].IsNull("author3") ? "" : dtD.Rows[indX]["author3"].ToString().Trim();
                    if (Auth2 != "")
                        dtD.Rows[indX]["author"] = dtD.Rows[indX]["author"] + "," + Auth2;
                    if (Auth3 != "")
                        dtD.Rows[indX]["author"] = dtD.Rows[indX]["author"] + "," + Auth3;
                    string sub1 = dtD.Rows[indX]["sub1"].ToString().Trim();
                    string sub2 = dtD.Rows[indX]["sub2"].ToString().Trim();
                    string sub3 = dtD.Rows[indX]["sub3"].ToString().Trim();
                    if ((sub1 != "") && (sub2 != ""))
                        sub1 = sub1 + "," + sub2;
                    if ((sub1 != "") && (sub3 != ""))
                        sub1 = sub1 + "," + sub3;
                    dtD.Rows[indX]["subject"] = sub1;
                }
                try
                {
                    string aqer = " select ISNULL(max(searchid),0)+1 from SearchHistory ";
                    string id = gCl.ExScaler(aqer).ToString();
                    aqer = " insert into SearchHistory ";
                    aqer += " (SearchId, SearchText, SearchCondition, InternCond, Sdate, SPage) values(";
                    aqer += id + ",'" + stext + "','" + scond.Replace("'", "''") + "','" + sIntCond + "',@dat,'" + spage + "' )";
                    SqlParameter p = new SqlParameter("@dat", SqlDbType.DateTime);
                    p.Value = DateTime.Now;
                    List<SqlParameter> lsp = new List<SqlParameter>();
                    lsp.Add(p);
                    gCl.IUD(aqer, lsp.ToArray());

                }
                catch { }
                grdDataUC.DataSource = dtD;
                grdDataUC.DataBind();

                Session["ucAccnData"] = dtD;
                gCl.TrClose();
                chkAccnUC.Checked = true;
                //            fillSearch();
            }
            catch (Exception exc)
            {
                gCl.TrRollBack();
                labUCMesg.Text = exc.Message;
            }


        }

        protected void btnUCRes_Click(object sender, EventArgs e)
        {
            grdDataUC.DataSource = null;
            grdDataUC.DataBind();
            Session["ucAccnData"] = null;
            labUCMesg.Text = "";
            txtAccnNoUC.Text = "";
            txSearchTextUC.Text = "";
            txtAccFUC.Text = "";
            txtAccTUC.Text = "";
            txSetNoUC.Text = "";
            txCatDFUC.Text = "";
            txCatDUUC.Text = "";
            txSubjUC.Text = "";
            txPublUC.Text = "";
            txTitleUC.Text = "";
            txDeptUC.Text = "";
            txAuthUC.Text = "";
            txCategUC.Text = "";
            chkAccnUC.Checked = true;
            chkEmptCallNo.Checked = false;
            chkEmptSubj.Checked = false;
            fillSearch();
        }
        //not working
        protected void btnUCSavMarc_Click(object sender, EventArgs e)
        {
            var accns = this.GetSelectAccn;
//            MarcAddMod marcsv = new MarcAddMod();
            int cn = 0;
    //        foreach (var accn in accns)
      //      {
        //        marcsv.MarcSave(accn);
          //      cn++;
            //}
            labUCMesg.Text = cn.ToString() + " Accns Marc 21 Data Saved";

        }
        protected void chkSav_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox c = (CheckBox)sender;
            GridViewRow r = (GridViewRow)c.NamingContainer;
            var lnk = (LinkButton)r.FindControl("lnkAccn");
        //    MarcAddMod marcsv = new MarcAddMod();
          //  marcsv.MarcSave(lnk.Text);
            labUCMesg.Text = "Accn: " + lnk.Text + " Marc 21 Data Saved";

        }
        protected void btnDel_Click(object sender, EventArgs e)
        {

        }

        protected void btnDelSelUC_Click(object sender, EventArgs e)
        {
            labUCMesg.Text = "";
            int sn = 0, en = 0;
            try
            {
                sn = Convert.ToInt32(txSlFrUC.Text);
                en = Convert.ToInt32(txSlToUC.Text);

                if (en <= sn)
                    throw new ApplicationException();
            }
            catch
            {
                labUCMesg.Text = "Not deleted, enter valid values";
                return;
            }

            GlobClassTr gclas = new GlobClassTr();
            gclas.TrOpen();
            string qer = "select top 2000 row_number() over(order by sdate desc) sno, searchid from searchhistory where spage='" + spage + "'  ";
            DataTable dts = gclas.DataT(qer);
            var delr = (from a in dts.AsEnumerable()
                        where Convert.ToInt32(a[0]) >= sn && Convert.ToInt32(a[0]) <= en
                        select a).ToList();
            for (int iNX = 0; iNX < delr.Count; iNX++)
            {
                string id = delr[iNX][1].ToString();
                qer = "delete from searchhistory where searchid=" + id;
                gclas.IUD(qer);
            }
            gclas.TrClose();
            btnUCRes_Click(sender, e);
            hdShoUC.Value = "y";

        }

        protected void btnDelChkUC_Click(object sender, EventArgs e)
        {
            string swher = " where searchid in (";
            for (int inX = 0; inX < grdSHUC.Rows.Count; inX++)
            {
                GridViewRow r = grdSHUC.Rows[inX];
                CheckBox c = (CheckBox)r.FindControl("chkDelete");
                if (c.Checked)
                {
                    HiddenField h = (HiddenField)r.FindControl("hdhistid");
                    swher += h.Value + ",";
                }
            }
            if (swher == " where searchid in (")
                return;
            swher = swher.Substring(0, swher.Length - 1) + ")";
            GlobClassTr gclas = new GlobClassTr();
            gclas.TrOpen();
            string qer = "delete from SearchHistory " + swher;
            gclas.IUD(qer);
            gclas.TrClose();
            fillSearch();
            hdShoUC.Value = "y";

        }

        protected void btnSer_Click(object sender, EventArgs e)
        {
            /*
  *     string orgStext = ""; //if histid >0, these have values from search history saved previously
string orgScond = "";

  * */
            Button b = (Button)sender;
            GridViewRow r = (GridViewRow)b.NamingContainer;
            HiddenField hdid = (HiddenField)r.FindControl("hdhistid");
            histid = Convert.ToInt32(hdid.Value); //it is checked where search hist clicked
            Label labstxt = (Label)r.FindControl("labStext");
            HiddenField h = (HiddenField)r.FindControl("hdSerchCond");
            HiddenField hintCond = (HiddenField)r.FindControl("hdInternCond");
            orgScond = h.Value;
            orgStext = labstxt.Text;
            //'setno'/'onlyaccn'/'emptycallno'/'emptysubj'/''  - see below

            string[] sIntCond = hintCond.Value.Split(',');
            txSetNoUC.Text = "";
            chkAccnUC.Checked = false;
            chkEmptCallNo.Checked = false;
            chkEmptSubj.Checked = false;
            for (int inX = 0; inX < sIntCond.Length; inX++)
            {
                switch (sIntCond[inX])
                {
                    case "setno":
                        txSetNoUC.Text = orgScond;
                        break;
                    case "onlyaccn":
                        chkAccnUC.Checked = true;
                        break;
                    case "emptycallno":
                        chkEmptCallNo.Checked = true;
                        break;
                    case "emptysubj":
                        chkEmptSubj.Checked = true;
                        break;
                }
            }

            btnUCSh_Click(sender, e);



        }

    }
}