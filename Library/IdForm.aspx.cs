using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Configuration.ConfigurationManager;
using System.Data;
using System.Data.OleDb;
using System.Web.Services;
using Library.App_Code.CSharp;
using Library.App_Code.MultipleFramworks;


namespace Library
{
    public partial class IdForm : BaseClass
    {

        private libGeneralFunctions LibObj = new libGeneralFunctions();
        private insertLogin LibObj1 = new insertLogin();
        private static string tmpcondition;
        //   private global::newclass1 myclass1 = new global::newclass1();
         //newclass1 myclass1 = new newclass1();
        private messageLibrary msgLibrary = new messageLibrary();
        private dbUtilities message = new dbUtilities();
        GlobClassTr clas = new GlobClassTr();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                hdnGrdId.Value = GridView1.ClientID;
                //SSA.MakeAccessible(GridView1);
                msglabel.Text = string.Empty;
                msglabel.Visible = false;
               // SetFocus(txttitle);
                // Dim UControl As Control = LoadControl("mainControl.ascx")
                // UControl.ID = "MainControl1"
                // Me.PanelTopCont.Controls.Add(UControl)
                // cmdsave.Disabled = False
                // txtcurrentposition.Disabled = True
                if (!Page.IsPostBack)
                {
                   // SetFocus(this.txttitle);
                    lblt1.Text = Request.QueryString["title"];

                    tmpcondition = Request.QueryString["condition"];
                    if (tmpcondition == "Y")
                    {
                        //this.cmdsave.Visible = false;
                    }
                    else
                    {
                        this.cmdsave.Visible = true;
                    }
                    Hidden1.Value = "0";
                    cmdsave.Visible = true;
                    // cmdReturn.CausesValidation = False
                    cmdreset.CausesValidation = false;
                }
            }
            catch (Exception ex)
            {
                // Me.msglabel.Visible = True
                // Me.msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Warning);
            }
        }


        protected void cmdsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (HiddenField1.Value == "1")
                {
                    update();
                    Fillgrid();
                    HiddenField1.Value = "";
                    return;
                }
                if (txttitle.Text == string.Empty)
                {
                    // LibObj.MsgBox1(Resources.ValidationResources.MsgFName.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.MsgFName.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.MsgFName.ToString(), this, dbUtilities.MsgLevel.Warning);
                    SetFocus(txttitle);
                    return;
                }
                if (this.Hidden1.Value == "0")
                {
                    // LibObj.MsgBox1(Resources.ValidationResources.MsgFName.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.MsgFName.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.MsgFName.ToString(), this, dbUtilities.MsgLevel.Warning);
                    SetFocus(txttitle);
                    return;
                }
                else
                {
                    this.Hidden1.Value = "0";
                }


                var con = new OleDbConnection(retConstr(""));
                con.Open();
                var cmd = new OleDbCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                message.PageMesg(Resources.ValidationResources.js1.ToString(), this, dbUtilities.MsgLevel.Success);

                SetFocus(txttitle);
                cmdreset_Click(sender, e);
            }
            catch (Exception ex)
            {
                message.PageMesg(Resources.ValidationResources.UnToDplay.ToString(), this, dbUtilities.MsgLevel.Failure);

            }
        }

        public void Fillgrid()
        {
            string str = "Select * from IdTable";
            clas.TrOpen();
            var dtd = clas.DataT(str);
            clas.TrClose();
       //     myclass1.filladapter(str);
         //   if (myclass1.ds.Tables[0].Rows.Count > 0)
           // {
                GridView1.DataSource = dtd;
                GridView1.DataBind();
                hdnGrdId.Value = GridView1.ClientID;
                //SSA.MakeAccessible(GridView1);
            //}
        }

        public void update()
        {
            var con = new OleDbConnection(retConstr(""));
            con.Open();
            var cmd = new OleDbCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update IdTable set Prefix='" + txtprefix.Value + "', Suffix='" + txtsuffix.Value + "', CurrentPosition='" + txtcurrentposition.Value + "' where ObjectName =N'" + txttitle.Text + "'";
            cmd.ExecuteNonQuery();
            // LibObj.MsgBox1(Resources.ValidationResources.js1.ToString, Me)
            // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, Resources.ValidationResources.js1.ToString, Me)
            message.PageMesg(Resources.ValidationResources.js1.ToString(), this, dbUtilities.MsgLevel.Success);
        }

        protected void cmdreset_Click(object sender, EventArgs e)
        {
            txtprefix.Value = string.Empty;
            txtsuffix.Value = string.Empty;
            txtcurrentposition.Value = string.Empty;
            SetFocus(txttitle);
            txttitle.Text = string.Empty;
            Hidden1.Value = "0";
        }

        protected void CheckBox1_CheckedChanged(object sender, System.EventArgs e)
        {
            if (CheckBox1.Checked == true)
            {
                GridView1.Visible = true;
                Fillgrid();
            }
            else
            {
                GridView1.Visible = false;
            }
            hdnGrdId.Value = GridView1.ClientID;
            //SSA.MakeAccessible(GridView1);
        }

        protected void GridView1_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            Fillgrid();
            hdnGrdId.Value = GridView1.ClientID;
            //SSA.MakeAccessible(GridView1);
        }


        //protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
        //{

        //    HiddenField1.Value = "1";
        //    string str = "select * from IdTable where ObjectName='" + GridView1.DataKeys[GridView1.SelectedIndex].Values["ObjectName"].ToString() + "'";


        //    myclass1.filladapter(str);
        //    if (myclass1.ds.Tables[0].Rows.Count > 0)
        //    {
        //        txttitle.Text = myclass1.ds.Tables[0].Rows[0]["ObjectName"].ToString();
        //        txtprefix.Value = myclass1.ds.Tables[0].Rows[0]["Prefix"].ToString();
        //        txtsuffix.Value = myclass1.ds.Tables[0].Rows[0]["Suffix"].ToString();
        //        txtcurrentposition.Value = myclass1.ds.Tables[0].Rows[0]["CurrentPosition"].ToString();
        //    }
        //}
    }
}