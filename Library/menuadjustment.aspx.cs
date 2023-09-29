using AjaxControlToolkit;
using Library.App_Code.CSharp;
using Library.App_Code.MultipleFramworks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Library
{
    public partial class menuadjustment :  BaseClass
    {
        private libGeneralFunctions LibObj = new libGeneralFunctions();
        private insertLogin LibObj1 = new insertLogin();
        private string tmpcondition;
        private messageLibrary msgLibrary = new messageLibrary();
        //private SendEmailSMSData SSA = new SendEmailSMSData();
        private dbUtilities message = new dbUtilities();
         GlobClassTr gclas=new GlobClassTr();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            txthref.Text=txthref.Text.Trim();
            txtorderno.Text=txtorderno.Text.Trim();
            txtpagemenu.Text=txtpagemenu.Text.Trim().Replace("'","''");
            txthref.Text= txthref.Text.Trim();

            txttitle.Text=txttitle.Text.Trim().Replace("'", "''");
            if ((hdUid.Value=="") || (txthref.Text=="") || (txtorderno.Text=="") 
                || (txtpagemenu.Text=="") && (txttitle.Text == ""))
            {
                message.PageMesg("Add all values",this,dbUtilities.MsgLevel.Warning); return;
            }
            var pg = txthref.Text.Split('.');
            if (pg.Length == 1)
                txthref.Text = txthref.Text + ".aspx";
            try
            {
                gclas.TrOpen();
                string sqer = "select count(*) from popup_new where menu_name=N'" + txttitle.Text + "'";
                var exi1=Convert.ToInt32(gclas.ExScaler(sqer));

                if (exi1 > 0)
                {
                    throw new ApplicationException("Name of page already exists");
                }
                sqer = "select count(*) from popup_new where trim(href)='" + txthref.Text + "'";
                var exi2 = Convert.ToInt32(gclas.ExScaler(sqer));

                if (exi2 > 0)
                {
                    throw new ApplicationException("Page link already exists");
                }
                sqer = " select isnull(max(menu_id),0)+1 from popup_new";
                var id=Convert.ToInt32(gclas.ExScaler(sqer));
                sqer = "insert into popup_new(menu_id,menu_name,parentid,href,status,ordno,helpref) values ";
                 sqer += "('" + id + "','" + txttitle.Text.Trim() + "',"+hdUid.Value+" ,'" + txthref.Text.Trim() + "','1','" + txtorderno.Text + "','"+txtHelp.Text.Trim()+ "')";
                gclas.IUD(sqer);
                gclas.TrClose();
                btnRes_Click(sender, e);
                message.PageMesg("Menu Page added", this);
            }
            catch (Exception ex)
            {
                gclas.TrRollBack();
                message.PageMesg(ex.Message,this,dbUtilities.MsgLevel.Failure)  ;
            }

        }

        protected void btnRes_Click(object sender, EventArgs e)
        {
            hdUid.Value = "";
            txtHelp.Text = "";
            txtHelp2.Text = "";
            txtorderno.Text = "";
            txtpagemenu.Text = "";
            txthref.Text = "";
            txthref2.Text = "";
            txttitle.Text = "";
            txttitle2.Text = "";
            hdid.Value = "";
            labMenu.Text = "";
            hdPmenuId.Value = "";
            txParentMn.Text = "";
            txOrderno2.Text = "";
            grdMorder.DataSource = null;
            grdMorder.DataBind();
            hdmenuid.Value = "";
            txFindMenu.Text = "";
        }

        protected void txttitle_TextChanged(object sender, EventArgs e)
        {

        }

        protected void rbladdupd_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void AddUpd_ActiveTabChanged(object sender, EventArgs e)
        {

        }

        protected void txttitle2_TextChanged(object sender, EventArgs e)
        {
            gclas.TrOpen();

            string sqer = "select count(*) from popup_new where menu_name=N'" + txttitle2.Text + "'";
            var exi1 = Convert.ToInt32(gclas.ExScaler(sqer));

            gclas.TrClose();

        }

        protected void btnGetMenu_Click(object sender, EventArgs e)
        {
            gclas.TrOpen();

            string sqer = "select * from popup_new where menu_id='" + hdid.Value + "'";
            DataTable dtMid = gclas.DataT(sqer);
            string menuid = dtMid.Rows[0]["parentid"].ToString();
            sqer = "select * from popup_new where menu_id='" + menuid + "'";
            DataTable dtMid2 = gclas.DataT(sqer);
            labMenu.Text = dtMid2.Rows[0]["menu_name"].ToString();
            txParentMn.Text = labMenu.Text;
            hdPmenuId.Value = dtMid.Rows[0]["parentid"].ToString();
            txOrderno2.Text = dtMid.Rows[0]["ordno"].ToString();
            gclas.TrClose();
            txtHelp2.Text = dtMid.Rows[0].IsNull("helpref")?"": dtMid.Rows[0]["helpref"].ToString();
            txthref2.Text = dtMid.Rows[0]["href"].ToString();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            txthref2.Text = txthref2.Text.Trim();
           txOrderno2  .Text = txOrderno2.Text.Trim();
//            txParentMn.Text = txParentMn.Text.Trim().Replace("'", "''");
            txthref2.Text = txthref2.Text.Trim();
            txttitle2.Text= txttitle2.Text.Trim();
            if ((hdid.Value == "") || (txthref2.Text == "") || (txOrderno2.Text == "")
                || (hdPmenuId.Value == "") )
            {
                message.PageMesg("Add all values", this, dbUtilities.MsgLevel.Warning); return;
            }
            var pg = txthref2.Text.Split('.');//hdPmenuId
            if (pg.Length == 1)
                txthref2.Text = txthref2.Text + ".aspx";
            try
            {
                gclas.TrOpen();
                string sqer = "select count(*) from popup_new where menu_id<>"+hdid.Value+" and  menu_name=N'" + txttitle2.Text + "'";
                var exi1 = Convert.ToInt32(gclas.ExScaler(sqer));

                if (exi1 > 0)
                {
                    throw new ApplicationException("Name of page already exists");
                }
                sqer = "select count(*) from popup_new where menu_id<>"+hdid.Value+" and  trim(href)='" + txthref2.Text + "'";
                var exi2 = Convert.ToInt32(gclas.ExScaler(sqer));

                if (exi2 > 0)
                {
                    throw new ApplicationException("Page link already exists");
                }
                sqer = "update popup_new set menu_name='" + txttitle2.Text + "' ,parentid=" + hdPmenuId.Value + " ";
                sqer +=" ,href='"+txthref2.Text+"' ,ordno="+txOrderno2.Text+" ,helpref='"+txtHelp2.Text+"' where menu_id= "+hdid.Value;
                gclas.IUD(sqer);
                gclas.TrClose();
                btnRes_Click(sender, e);
                message.PageMesg("Menu Page Updated", this);
            }
            catch (Exception ex)
            {
                gclas.TrRollBack();
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (hdid.Value == "")
            {
                message.PageMesg("Find Page to delete", this, dbUtilities.MsgLevel.Warning);
                return;
            }
            try
            {
                gclas.TrOpen();
                string sqer = "select count(*) from menu_perm where menuid=" + hdid.Value;
                var exi = Convert.ToInt32(gclas.ExScaler(sqer));
                if (exi>0)
                {
                    throw new ApplicationException("Form has been given permission, cannot be deleted");
                }
                sqer = "delete from popup_new where menu_id=" + hdid.Value;
                gclas.IUD(sqer);
                gclas.TrClose();
                btnRes_Click(sender, e);
                
                message.PageMesg("Form is deleted.", this);

            }catch(Exception ex)
            {
                gclas.TrRollBack();
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
        }

        protected void btnShMEnu_Click(object sender, EventArgs e)
        {
            string sqer= "select row_number() over(order by ordno) slno, Menu_id,Menu_name,ParentId,OrdNo from Popup_new where ParentId= "+hdmenuid.Value+" order by ordno";
            gclas.TrOpen() ;
            DataTable dtm = gclas.DataT(sqer);
            gclas.TrClose() ;
            grdMorder.DataSource= dtm;
            grdMorder.DataBind();
            GridViewRow row = (GridViewRow)grdMorder.Rows[0];
            Button btu = (Button)row.FindControl("btnmoveup");
            btu.Enabled = false;
            GridViewRow rowb = (GridViewRow)grdMorder.Rows[grdMorder.Rows.Count-1];
            Button btd = (Button)rowb.FindControl("btnmovedown");
            btd.Enabled = false;
        }
        private DataTable getTable()
        {
            DataTable dtgrid = new DataTable();
            dtgrid.Columns.Add("menu_id");
            dtgrid.Columns.Add("menu_name");
            dtgrid.Columns.Add("parentid");
            dtgrid.Columns.Add("ordno");
            dtgrid.Columns.Add("slno",typeof(int));
            int coln = 1;
            foreach(GridViewRow row in grdMorder.Rows)
            {
                DataRow r=dtgrid.NewRow();
                Label labfrm = (Label)row.FindControl("labfrm");
                Label ldno = (Label)row.FindControl("hdordno");
                HiddenField hdmid = (HiddenField)row.FindControl("hdmid");
                HiddenField hdpid = (HiddenField)row.FindControl("hdpid");
                r["menu_id"] = hdmid.Value;
                r["menu_name"] = labfrm.Text;
                r["parentid"]=hdpid.Value;
                r["ordno"]=ldno.Text;
                r["slno"] = coln++;
                dtgrid.Rows.Add(r);
            }
            return dtgrid;
        }

        protected void btnmoveup_Click(object sender, EventArgs e)
        {
            DataTable dtgrd = getTable();
            Button btnmoveup = (Button)sender;
            GridViewRow r = (GridViewRow)btnmoveup.NamingContainer;
            Label labslno = (Label)r.FindControl("labslno");
            int slno = Convert.ToInt32(labslno.Text);
            DataTable dt2=dtgrd.Clone();
//            slno--;
           List<int> list = new List<int>();
            int sno = 1;
            DataRow drins=dt2.NewRow();
            foreach (DataRow row in dtgrd.Rows)
            {
                int slnocur = Convert.ToInt32(row["slno"]);
                if (slnocur == slno)
                {
                    drins [0] = row[0];
                    drins[1] = row[1];
                    drins[2] = row[2];
                    drins[3] = row[3];
                    drins[4] = row[4];
                }
            }
            foreach (DataRow row in dtgrd.Rows)
            {
                int slnocur = Convert.ToInt32( row["slno"]);
                if (slnocur != slno)
                {
                    dt2.ImportRow(row);
                }
            }
            dt2.Rows.InsertAt(drins, slno - 2);
            foreach (DataRow row in dt2.Rows)
            {
                row["slno"] = sno++;
            }

//            DataView dvnew = dt2.DefaultView;
  //          dvnew.Sort = "slno asc";
    //        DataTable dtNew=dvnew.ToTable(); 
            
            grdMorder.DataSource = dt2;
            grdMorder.DataBind();
            GridViewRow rowd = (GridViewRow)grdMorder.Rows[0];
            Button btu = (Button)rowd.FindControl("btnmoveup");
            btu.Enabled = false;
            GridViewRow rowb = (GridViewRow)grdMorder.Rows[grdMorder.Rows.Count - 1];
            Button btd = (Button)rowb.FindControl("btnmovedown");
            btd.Enabled = false;

        }

        protected void btnmovedown_Click(object sender, EventArgs e)
        {
            DataTable dtgrd = getTable();
            Button btnmoveup = (Button)sender;
            GridViewRow r = (GridViewRow)btnmoveup.NamingContainer;
            Label labslno = (Label)r.FindControl("labslno");
            int slno = Convert.ToInt32(labslno.Text);
            DataTable dt2 = dtgrd.Clone();
            //            slno--;
            List<int> list = new List<int>();
            int sno = 1;
            DataRow drins = dt2.NewRow();
            foreach (DataRow row in dtgrd.Rows)
            {
                int slnocur = Convert.ToInt32(row["slno"]);
                if (slnocur == slno)
                {
                    drins[0] = row[0];
                    drins[1] = row[1];
                    drins[2] = row[2];
                    drins[3] = row[3];
                    drins[4] = row[4];
                }
            }
            foreach (DataRow row in dtgrd.Rows)
            {
                int slnocur = Convert.ToInt32(row["slno"]);
                if (slnocur != slno)
                {
                    dt2.ImportRow(row);
                }
            }
            dt2.Rows.InsertAt(drins, slno );
            foreach (DataRow row in dt2.Rows)
            {
                row["slno"] = sno++;
            }

            //            DataView dvnew = dt2.DefaultView;
            //          dvnew.Sort = "slno asc";
            //        DataTable dtNew=dvnew.ToTable(); 

            grdMorder.DataSource = dt2;
            grdMorder.DataBind();
            GridViewRow rowd = (GridViewRow)grdMorder.Rows[0];
            Button btu = (Button)rowd.FindControl("btnmoveup");
            btu.Enabled = false;
            GridViewRow rowb = (GridViewRow)grdMorder.Rows[grdMorder.Rows.Count - 1];
            Button btd = (Button)rowb.FindControl("btnmovedown");
            btd.Enabled = false;
        }

        protected void btnSavOrd_Click(object sender, EventArgs e)
        {
            try
            {
                gclas.TrOpen();
                DataTable dtget = getTable();
                foreach (DataRow row in dtget.Rows)
                {
                    int menid = Convert.ToInt32(row["menu_id"]);
                    int ord = Convert.ToInt32(row["slno"]);
                    string supd = "update popup_new set ordno=" + ord + " where menu_id=" + menid;
                    gclas.IUD(supd);
                }
                gclas.TrClose();
                btnRes_Click(sender, e);
                message.PageMesg("Order are Set", this);
            }catch(Exception ex)
            {
                gclas.TrRollBack();
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
        }
    }
}