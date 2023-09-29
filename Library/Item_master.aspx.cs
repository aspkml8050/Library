using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Runtime.CompilerServices;
using Library.App_Code.CSharp;
using Microsoft.SqlServer.Management.Sdk.Sfc;
using Library.App_Code.MultipleFramworks;

namespace Library
{
    public partial class Item_master : BaseClass
    {

        private insertLogin LibObj1 = new insertLogin();
        private libGeneralFunctions LibObj = new libGeneralFunctions();
        private DataTable tmptab = new DataTable();
        private static string tmpcondition;
        private ClassFileCatelog ObjCatalog = new ClassFileCatelog();
        private ClassFileBookAccession ObjBookAccesssion = new ClassFileBookAccession();
        private static string dateF;
        private static string Binding;
        //private LibraryRestriction LibRescObj = new LibraryRestriction();
        private static bool AllowIn = false;
        private static int jrn_no_lg;
        private bool fromdirectacc;
        private messageLibrary msgLibrary = new messageLibrary();
        private dbUtilities message = new dbUtilities();

        protected void Page_Load(object sender, EventArgs e)
        {
            cmdreset.CausesValidation = false;
            cmdadd.CausesValidation = false;
            var tmpcon = new OleDbConnection(retConstr(""));
            tmpcon.Open();
            try
            {
                msglabel.Text = string.Empty;
                msglabel.Visible = false;
                if (!IsPostBack)
                {
                    Lblsearch.Visible = false;
                    DropDownList1.Visible = false;
                    dateF = hrDate.Value;
                    this.SetFocus(txtcmbJTitle);
                    LibObj.populateDDL(cmbLanguage, "Select Language_Id,Language_name from Translation_Language order by Language_Name", "Language_name", "Language_Id", HComboSelect.Value, tmpcon);

                    LibObj.populateDDL(cmbcurr, "Select distinct currencycode,currencyname from exchangemaster order by currencyname", "currencyname", "currencycode", HComboSelect.Value, tmpcon);
                    lblTitle.Text = Request.QueryString["title"];

                    // __________By kaushal +++++++++++++++ for Direct Jrnl Accessioning
                    var lstItem = new System.Web.UI.WebControls.ListItem();

                }
            }

            // fail:
            catch (Exception ex)
            {
                // msglabel.Text = Err.Description
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
        }

        


        protected void cmdadd_Click(object sender, EventArgs e)
        {
            if (this.txtcmbJTitle.Text == string.Empty)
            {
               
                message.PageMesg(Resources.ValidationResources.SlctTitle.ToString(), this, dbUtilities.MsgLevel.Warning);
                this.SetFocus(txtcmbJTitle);
                return;
            }
            if (this.txtFromdate.Text == string.Empty & this.txtVolume.Value == string.Empty)
            {
               
                message.PageMesg(Resources.ValidationResources.EtrPubDtOthEtrV.ToString(), this, dbUtilities.MsgLevel.Warning);
                this.SetFocus(txtFromdate);
                return;
            }

            if (cmbcurr.SelectedValue == "---Select---")
            {
               
                message.PageMesg("Select Currency!", this, dbUtilities.MsgLevel.Warning);
                this.SetFocus(cmbcurr);
                return;
            }
            if (cmbLanguage.SelectedValue == "---Select---")
            {
                
                message.PageMesg("Select Language!", this, dbUtilities.MsgLevel.Warning);
                this.SetFocus(cmbLanguage);
                return;
            }

            // Dim icunt As Integer
            var accmastercon = new OleDbConnection(retConstr(""));
            accmastercon.Open();
            OleDbTransaction tran;
            tran = accmastercon.BeginTransaction();
            var accmastercom = new OleDbCommand();
            accmastercom.Connection = accmastercon;
            accmastercom.Transaction = tran;
            string Pre_String = string.Empty;
            string ac_id = string.Empty;
            bool @bool = true;
            try
            {
                // Generate Accession Id
                string flg = string.Empty;
                if (cmdadd.Text == Resources.ValidationResources.bSave.ToString())
                {
                    accmastercom.CommandType = CommandType.Text;
                    accmastercom.CommandText = "select coalesce(max(convert(int,id)),0,max(id)) from Item_Master";
                    ac_id = Convert.ToString(accmastercom.ExecuteScalar());
                    ac_id = Convert.ToString(Convert.ToDouble(ac_id) == 0d ? 1 : Convert.ToDouble(ac_id) + 1d);
                    accmastercom.Parameters.Clear();
                    flg = "N";
                    hddoc_id.Value = ac_id;
                }
                else
                {
                    flg = "U";
                    ac_id = hddoc_id.Value;
                }


                accmastercom.Parameters.Clear();

                accmastercom.CommandType = CommandType.StoredProcedure;
                accmastercom.CommandText = "Insert_Item_Master";

                accmastercom.Parameters.Add(new OleDbParameter("@Id", OleDbType.Integer));
                accmastercom.Parameters["@Id"].Value = hddoc_id.Value;
                accmastercom.Parameters.Add(new OleDbParameter("@Title", OleDbType.VarWChar));
                accmastercom.Parameters["@Title"].Value = txtcmbJTitle.Text;

                accmastercom.Parameters.Add(new OleDbParameter("@sub_Title", OleDbType.VarWChar));
                accmastercom.Parameters["@sub_Title"].Value = txtcmbJTitle0.Text;

                accmastercom.Parameters.Add(new OleDbParameter("@Pub_Day", OleDbType.Date));
                accmastercom.Parameters["@Pub_Day"].Value = Convert.ToDateTime(txtFromdate.Text);


                accmastercom.Parameters.Add(new OleDbParameter("@IssN_No", OleDbType.VarWChar));
                accmastercom.Parameters["@IssN_No"].Value = txtIssueN.Value;


                accmastercom.Parameters.Add(new OleDbParameter("@Issue_No", OleDbType.VarWChar));
                accmastercom.Parameters["@Issue_No"].Value = txtIssue.Value;

                accmastercom.Parameters.Add(new OleDbParameter("@Volume", OleDbType.VarWChar));
                accmastercom.Parameters["@Volume"].Value = txtVolume.Value;

                accmastercom.Parameters.Add(new OleDbParameter("@Part_No", OleDbType.VarWChar));
                accmastercom.Parameters["@Part_No"].Value = txtpart.Value;

                accmastercom.Parameters.Add(new OleDbParameter("@Copy_No", OleDbType.VarWChar));
                accmastercom.Parameters["@Copy_No"].Value = txtCpyN.Value;

                accmastercom.Parameters.Add(new OleDbParameter("@Lack_No", OleDbType.VarWChar));
                accmastercom.Parameters["@Lack_No"].Value = Txtlackno.Value;


                accmastercom.Parameters.Add(new OleDbParameter("@Edition ", OleDbType.VarWChar));
                accmastercom.Parameters["@Edition "].Value = txtedition.Value;

                accmastercom.Parameters.Add(new OleDbParameter("@Edition_Year", OleDbType.VarWChar));
                accmastercom.Parameters["@Edition_Year"].Value = txteditionyear.Value;

                accmastercom.Parameters.Add(new OleDbParameter("@Language", OleDbType.Integer));
                accmastercom.Parameters["@Language"].Value = cmbLanguage.SelectedValue;

                accmastercom.Parameters.Add(new OleDbParameter("@Publisher", OleDbType.VarWChar));
               // accmastercom.Parameters["@Publisher"].Value = txtCmbPublisher.Text;

                accmastercom.Parameters.Add(new OleDbParameter("@Vendor", OleDbType.VarWChar));
                //accmastercom.Parameters["@Vendor"].Value = txtCmbVendor.Text;
                accmastercom.Parameters.Add(new OleDbParameter("@Currency", OleDbType.Integer));
                accmastercom.Parameters["@Currency"].Value = cmbcurr.SelectedValue;

                accmastercom.Parameters.Add(new OleDbParameter("@Price", OleDbType.Decimal));
               // accmastercom.Parameters["@Price"].Value = (txtForeignPrice.Value != "") ? txtForeignPrice.Value: 0;

                accmastercom.Parameters.Add(new OleDbParameter("@flg", OleDbType.VarWChar));
                accmastercom.Parameters["@flg"].Value = flg;

                accmastercom.ExecuteNonQuery();
                accmastercom.Parameters.Clear();

                tran.Commit();
                // LibObj.MsgBox1("Saved Completed Successfully ", Me)
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, "Saved Completed Successfully", Me)
                message.PageMesg("Saved Completed Successfully", this, dbUtilities.MsgLevel.Success);
                cmdreset_Click(sender, e);
            }

            catch (Exception exMain)
            {
                tran.Rollback();
                // msglabel.Visible = True
                // msglabel.Text = exMain.Message
                // LibObj.MsgBox1("Unable to Complete Process!", Me)
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Failure, "Unable to Complete Process!", Me)
                message.PageMesg("Unable to Complete Process!", this, dbUtilities.MsgLevel.Failure);
            }

        }

        public void refreshFileds()
        {
            try
            {
                hddoc_id.Value = "";
                HdBind.Value = "";
                txtcmbJTitle0.Text = "";
                txtIssueN.Value = "";
                txtIssueN.Value = "";
                this.txtVolume.Value = "";
                txtIssueN.Value = "";
                txtCpyN.Value = "";
                txtpart.Value = "";
                this.txtFromdate.Text = "";
                this.Txtlackno.Value = "";
                // Call GetData2("default")
                txtCpyN.Value = "";
                txtedition.Value = "";
                txteditionyear.Value = "";
                cmbcurr.SelectedValue = "---Select---";
                cmbLanguage.SelectedValue = "---Select---";
                //txtCmbPublisher.Text = "";
                //txtCmbVendor.Text = "";
                txtForeignPrice.Value = "";
                txtIssue.Value = "";
                Lblsearch.Visible = false;
                DropDownList1.Visible = false;
                chkSearch1.Checked = false;
                return;
            }
            catch(Exception ex)
            {

                // msglabel.Text = Err.Description
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
        }

        protected void cmdreset_Click(object sender, EventArgs e)
        {
            refreshFileds();
            tmptab.Clear();
            tmptab.AcceptChanges();
            Session["Obj"] = tmptab;
            cmdadd.Text = "Submit";
            this.SetFocus(txtcmbJTitle);

            txtcmbJTitle.Text = string.Empty;


            HdTransaction.Value = "Top";
        }


        protected void chkSearch1_CheckedChanged(object sender, System.EventArgs e)
        {
            if (chkSearch1.Checked == true)
            {
                Lblsearch.Visible = true;
                DropDownList1.Visible = true;
                var tmpcon = new OleDbConnection(retConstr(""));
                tmpcon.Open();
                DropDownList1.Items.Clear();
                LibObj.populateDDL(DropDownList1, "Select Id,(Title +'/PubDay:'+convert(nvarchar,Pub_Day,106)) as Title from  Item_Master order By Id Desc", "Title", "Id", HComboSelect.Value, tmpcon);
            }
            else
            {
                Lblsearch.Visible = false;
                DropDownList1.Visible = false;

            }
            return;
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var tmpcon = new OleDbConnection(retConstr(""));
            tmpcon.Open();
            var adp = new OleDbDataAdapter("select * from item_master where id='" + DropDownList1.SelectedValue + "'", tmpcon);
            var ds = new DataSet();
            adp.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                hddoc_id.Value = DropDownList1.SelectedValue;
                txtcmbJTitle.Text = ds.Tables[0].Rows[0]["title"].ToString();

                txtcmbJTitle0.Text = ds.Tables[0].Rows[0]["sub_title"].ToString();

                txtFromdate.Text = String.Format("{0:dd-MMM-yyyy}",Convert.ToDateTime(ds.Tables[0].Rows[0]["pub_day"].ToString()));


                txtIssueN.Value = ds.Tables[0].Rows[0]["IssN_no"].ToString();


                txtIssue.Value = ds.Tables[0].Rows[0]["issue_no"].ToString();

                txtVolume.Value = ds.Tables[0].Rows[0]["volume"].ToString();
                txtpart.Value = ds.Tables[0].Rows[0]["part_no"].ToString();

                txtCpyN.Value = ds.Tables[0].Rows[0]["copy_no"].ToString();

                Txtlackno.Value = ds.Tables[0].Rows[0]["lack_no"].ToString();


                txtedition.Value = ds.Tables[0].Rows[0]["edition"].ToString();

                txteditionyear.Value = ds.Tables[0].Rows[0]["edition_year"].ToString();

                cmbLanguage.SelectedValue = ds.Tables[0].Rows[0]["language"].ToString();

               // txtCmbPublisher.Text = ds.Tables[0].Rows[0]["publisher"].ToString();
                //txtCmbVendor.Text = ds.Tables[0].Rows[0]["vendor"].ToString();
                cmbcurr.SelectedValue = ds.Tables[0].Rows[0]["currency"].ToString();
                if (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["price"].ToString().Trim()))
                {
                    txtForeignPrice.Value = "0.00";
                }
                else
                {
                    txtForeignPrice.Value = ds.Tables[0].Rows[0]["price"].ToString();

                }
                cmdadd.Text = "Update";
            }
            else
            {

            }

        }
    }
}