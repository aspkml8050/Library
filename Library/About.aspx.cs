using Library.App_Code.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library.Models;
using System.Data;
using System.Data.SqlClient;
using static MoreLinq.Extensions.LagExtension;
using static MoreLinq.Extensions.LeadExtension;
using MoreLinq;
using LibData.Model;
using LibData.Contract;

namespace Library
{
    public partial class About : BaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txm.Text = string.Empty;
        }

        protected void btnreadAccn_Click(object sender, EventArgs e)
        {
            CatalogClass catgdata = new CatalogClass();
            BasicData data = new BasicData();
            ReqdData reqd = new ReqdData();
            reqd.Currency = true;
            reqd.Lang = true;
            reqd.Media = true;
            reqd.CastCateg = true;
            reqd.ItemType = true;
            reqd.Categ = true;
            reqd.ItemStatus = true;
            reqd.Dept = true;
            reqd.Progs=true;
            var reqData= data.GetBasicData(reqd);

            GlobClassTr clas = new GlobClassTr();
            clas.TrOpen();
            string qerrd2 = " select * from itemstatusmaster ; select * from departmentmaster";
            var dsd = clas.DataSetT(qerrd2);

            var dsdata = clas.ExProcDS("tmpRetMultiple", new List<SqlParameter>());
            var itm1 = ExtConvert.ConvertTo<ItemStatusMaster>(dsdata.Tables[0]);
            var itm2 = ExtConvert.ConvertTo<departmentmaster>(dsdata.Tables[1]);
            var itm3 = ExtConvert.ConvertTo<SpResponse>(dsdata.Tables[2]);
            string sql = "select * from bookaccessionmaster where ctrl_no= 74081";
            var result = clas.DataT(sql);
            List<bookaccessionmaster> accn=ExtConvert.ConvertTo<bookaccessionmaster>(result);

            grdorg.DataSource = accn;
            grdorg.DataBind();
            foreach(var x in accn)
            {
                x.booktitle +=": "+DateTime.Now.ToString();

                x.ordernumber = "3221";
                x.BookNumber = "rt420";
            }
            bookaccessionmaster addit = new bookaccessionmaster();
            addit.accessionnumber = "420252";
            addit.BookNumber = "rxt430";
            addit.booktitle= "added new book";
            addit.accessioneddate=DateTime.Now;
            addit.accessionid = 420252;
            addit.srno = -1;
            addit.released = "d";
            addit.bookprice = 419.10M;
            addit.srNoOld = 0;
            addit.Status = "1";
            addit.ReleaseDate=DateTime.Now;
            addit.IssueStatus = "Y";
            addit.LoadingDate= DateTime.Now.AddDays(2)  ;
            addit.CheckStatus = "A";
            addit.ctrl_no = 74081;
            addit.editionyear = 0;
            addit.Copynumber = 11;
            addit.specialprice = 218;
            addit.pubYear = 2015;
            addit.biilNo = "3326";
            addit.billDate = DateTime.Now.AddDays(-20);
            addit.catalogdate= DateTime.Now;
            addit.Item_type = "Books";
            addit.OriginalPrice= 0;
            addit.OriginalCurrency = "Rupees";
            addit.userid = "Admin";
            addit.vendor_source = "NA, Gorakhpur";
            addit.program_id = 3;
            addit.DeptCode = 6;
            addit.DSrno = 0;
            addit.DeptName = "NA";
            addit.ItemCategoryCode = 1;
            addit.ItemCategory = "Book";
            addit.Loc_id = 0;
            addit.AppName= "About";

            accn.Add(addit);
            var d= accn.ToDataTable();
          List<SqlParameter> lsp=new List<SqlParameter>();
            SqlParameter p = new SqlParameter("@BookAccessionMaster", d);
            lsp.Add(p);
            clas.ExProc("usp_InsertOrUpdateBookAccession", lsp);


            clas.TrClose();


            return;

/*            string sqer = "select top 20 * from bookaccessionmaster where ctrl_no <3400";
            var d = clas.DataT(sqer);
            clas.TrClose();
            var list= ExtConvert.ConvertTo<bookaccessionmaster>(d);

            txm.Text = list.Count.ToString();

            */

        }

        protected async void btnsamp_Click(object sender, EventArgs e)
        {
            ApiComm calit = new ApiComm();
           var r= await calit.Sample().ConfigureAwait(false);

            var chkindent = await calit.getIndentById("Nath-2020-21-2");


            var categ1 = await calit.GetCategory();

            var res1 = await calit.DeleteProgram("440");

            var r6 = res1;

            var exch = await calit.GetExchange();

            var usr = await calit.GetCircUserById("000142");

            var dres = usr.isSuccess;

            var prg=new ProgramCmd();
            prg.ProgramName = "b.";
            prg.Shortname = "";
            var resu = await calit.GetProgMaster(prg);

            var indmod=new IndentmasterMod();
            indmod.indentnumber = "";
            indmod.indentnumber = "";

            return;
            var req = new UserMenuItemPerm();
            req.usertypeid = 1;
            req.User_Id =  LoggedUser.Logged().User_Id  ;
            req.submenu_id = 3001;
            var dr = await calit.GetMenuItemPermission(req);

        }

        protected void btntst1_Click(object sender, EventArgs e)
        {
            RunSqlScript scr = new RunSqlScript();
            var r = scr.Start();

            var d=r.ToString();
        }
    }
}