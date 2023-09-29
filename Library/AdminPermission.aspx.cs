using Library.App_Code.CSharp;
using Library.App_Code.MultipleFramworks;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Security.Cryptography;

namespace Library
{
    public partial class AdminPermission : BaseClass
    {
        GlobClassTr GData = new GlobClassTr();
        libGeneralFunctions libObj = new libGeneralFunctions();
        DBIStructure DBI = new DBIStructure();
        string tmpcondition;
        messageLibrary msgLibrary = new messageLibrary();
        dbUtilities message = new dbUtilities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Button1_Click(sender, e);
                // this.lblTitle.Text = Request.QueryString["title"];
                if (this.lblTitle.Text == "You have no Permission to this Feature as  'MSSPL CATALOGING' , Contact MSSPL as per your requirement!")
                {
                    this.lblTitle.BackColor = System.Drawing.Color.White;
                    this.lblTitle.ForeColor = System.Drawing.Color.Green;
                    //                    this.lblTitle.Font.Size = (FontUnit)FontSize.XLarge;
                    this.lbluser.Visible = false;
                    this.Label1.Visible = false;
                    this.txtpassword.Visible = false;
                    this.txtusername.Visible = false;
                    this.cmdlogin.Visible = false;
                }
                              clear();
                this.Chksearch.Visible = false;
                          ShowFeatures();
                var GetDDL = new GlobClassTr();
                try
                {
                    GetDDL.TrOpen();
                    string showda = "select CallNo,CpyInform,Reserve,Content,AddKey,AddCart,AuthorIfo,BookInfo from  librarysetupinformation";
                    var sdata = GetDDL.DataT(showda);
                    if (sdata.Rows.Count > 0)
                    {
                        if (sdata.Rows[0]["CallNo"].ToString() == "Y")
                        {
                            this.rblCallNo.Checked = true;
                        }
                        else
                        {
                            this.rblCallNo.Checked = false;
                        }
                        if (sdata.Rows[0]["CpyInform"].ToString() == "Y")
                        {
                            this.CpyInform.Checked = true;
                        }
                        else
                        {
                            this.CpyInform.Checked = false;
                        }
                        if (sdata.Rows[0]["Reserve"].ToString() == "Y")
                        {
                            this.Reserve.Checked = true;
                        }
                        else
                        {
                            this.Reserve.Checked = false;
                        }
                        if (sdata.Rows[0]["Content"].ToString() == "Y")
                        {
                            this.Content.Checked = true;
                        }
                        else
                        {
                            this.Content.Checked = false;
                        }
                        if (sdata.Rows[0]["AddKey"].ToString() == "Y")
                        {
                            this.AddKey.Checked = true;
                        }
                        else
                        {
                            this.AddKey.Checked = false;
                        }
                        if (sdata.Rows[0]["AddCart"].ToString() == "Y")
                        {
                            this.AddCart.Checked = true;
                        }
                        else
                        {
                            this.AddCart.Checked = false;
                        }
                        if (sdata.Rows[0]["AuthorIfo"].ToString() == "Y")
                        {
                            this.AuthorIfo.Checked = true;
                        }
                        else
                        {
                            this.AuthorIfo.Checked = false;
                        }
                        if (sdata.Rows[0]["BookInfo"].ToString() == "Y")
                        {
                            this.BookInfo.Checked = true;
                        }
                        else
                        {
                            this.BookInfo.Checked = false;
                        }
                    }
                    GetDDL.TrClose();
                }
                catch (Exception ex)
                {
                    message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
                }


            }
        }
        public void clear()
        {
            this.Chksearch.Checked = false;
            this.txtCategory.Visible = false;
            this.txtCategory.Value = string.Empty;
            this.btnCategoryFilter2.Visible = false;
            this.Label2.Visible = false;
            this.lstAllCategory.Visible = false;
        }
        public void ShowFeatures()
        {
            var ds = new DataSet();
            ds = DBI.GetDataSet("select * from FeaturesPer", DBI.GetConnectionString(DBI.GetConnectionName()));
            // For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            // If FPer.Items.IndexOf(FPer.Items.FindByValue(ds.Tables(0).Rows(i)("FID"))) <> -1 Then
            // FPer.Items(FPer.Items.IndexOf(FPer.Items.FindByValue(ds.Tables(0).Rows(i)("FID")))).Selected = True
            // End If
            // Next
            foreach (Control chk in this.pnlPer.Controls)
            {
                string nm = chk.GetType().FullName;
                if (nm.ToLower().Contains("checkbox"))
                {
                    CheckBox c = (CheckBox)chk;
                    c.Checked = false;
                }

            }
            for (int i = 0, loopTo = ds.Tables[0].Rows.Count - 1; i <= loopTo; i++)
            {
                string p = ds.Tables[0].Rows[i]["fid"].ToString().PadLeft(2, '0');
                foreach (Control chk in this.pnlPer.Controls)
                {
                    string nm = chk.GetType().FullName;
                    if (nm.ToLower().Contains("checkbox"))
                    {
                        CheckBox c = (CheckBox)chk;
                        if ((c.Text ?? "") == (p ?? ""))
                        {
                            c.Checked = true;
                        }

                    }

                }

            }

        }

        protected void cmdlogin_Click(object sender, EventArgs e)
        {
            var Oledbcon = new OleDbConnection(retConstr(""));
            Oledbcon.Open();
            var cmd = new OleDbCommand();
            cmd.Connection = Oledbcon;
            cmd.CommandType = CommandType.Text;
            if (this.txtusername.Text.Trim().ToUpper() == "SECUREMSSPL")
            {


                cmd.CommandText = "select password,SaltVc from security where userid=N'" + txtusername.Text + "'  ";

                var dr = cmd.ExecuteReader();

                dr.Read();
                if (dr.HasRows)
                {

                    string decPassword = decrypt(dr.GetValue(0).ToString(), dr.GetValue(1).ToString());
                    if ((decPassword ?? "") == (txtpassword.Text ?? ""))
                    {
                        this.msglabel.Visible = false;
                        this.txtpassword.Text = string.Empty;
                        this.plogin.Visible = false;
                        this.padmin.Visible = true;
                        return;
                    }
                    else
                    {
                        // Me.msglabel.Visible = True
                        // Me.msglabel.Text = Resources.ValidationResources.InvalidPwd.ToString
                        // libObj.MsgBox1(Resources.ValidationResources.InvalidPwd.ToString, Me)
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.InvalidPwd.ToString, Me)
                        message.PageMesg(Resources.ValidationResources.InvalidPwd.ToString(), this, dbUtilities.MsgLevel.Warning);

                    }
                }
                else
                {
                    // libObj.MsgBox1(Resources.ValidationResources.InvaliUserID.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.InvaliUserID.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.InvaliUserID.ToString(), this, dbUtilities.MsgLevel.Warning);

                }
            }
            else
            {
                // libObj.MsgBox1(Resources.ValidationResources.InvaliUserID.ToString, Me)
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.InvaliUserID.ToString, Me)
                message.PageMesg(Resources.ValidationResources.InvaliUserID.ToString(), this, dbUtilities.MsgLevel.Warning);
            }

        }
        public string decrypt(string val, string seed)
        {
            byte[] KEY_64 = Convert.FromBase64String(seed);
            byte[] IV_64 = new byte[] { 55, 103, 246, 79, 36, 99, 167, 3 };
            if (!string.IsNullOrEmpty(val))
            {
                var cryptoProvider = new DESCryptoServiceProvider();
                byte[] buffer = Convert.FromBase64String(val);
                var ms = new MemoryStream(buffer);
                var cs = new CryptoStream(ms, cryptoProvider.CreateDecryptor(KEY_64, IV_64), CryptoStreamMode.Read);
                var sr = new StreamReader(cs);
                return sr.ReadToEnd();
            }
            else
            {
                return "";
            }
        }

        protected void TreeView1_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
        {
            {
                try
                {
                    switch (e.Node.Depth)
                    {
                        case 0:
                            {
                                // Populate the first-level nodes.
                                PopulateFirstLevel(e.Node);
                                break;
                            }

                        case 1:
                            {
                                // Populate the second-level nodes.
                                PopulateSecondLevel(e.Node);
                                break;
                            }
                        case 2:
                            {
                                // Populate the third-level nodes.
                                PopulateThirdLevel(e.Node);
                                break;
                            }
                    }
                }
                catch (Exception ex)
                {
                    // msglabel.Visible = True
                    // msglabel.Text = ex.Message
                    message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
                }
            }
        }
        public void PopulateFirstLevel(TreeNode node)
        {
            int i, j, k, l, m, FirstLevelCounter;
            FirstLevelCounter = 0;
            var loopTo = this.TreeView1.Nodes.Count - 1;
            for (i = 0; i <= loopTo; i++)
            {
                if (this.TreeView1.Nodes[i].Checked)
                {
                    var loopTo1 = this.TreeView1.Nodes[i].ChildNodes.Count - 1;
                    for (j = 0; j <= loopTo1; j++)
                    {
                        var loopTo2 = this.TreeView1.Nodes[i].ChildNodes.Count - 1;
                        for (l = 0; l <= loopTo2; l++)
                        {
                            if (this.TreeView1.Nodes[i].ChildNodes[j].Checked == false)
                            {
                                FirstLevelCounter = FirstLevelCounter + 1;
                            }
                        }
                        if (FirstLevelCounter == this.TreeView1.Nodes[i].ChildNodes.Count)
                        {
                            var loopTo3 = this.TreeView1.Nodes[i].ChildNodes.Count - 1;
                            for (m = 0; m <= loopTo3; m++)
                            {
                                if (this.TreeView1.Nodes[i].ChildNodes[m].Checked == false)
                                {
                                    this.TreeView1.Nodes[i].ChildNodes[m].Checked = true;
                                    if (this.TreeView1.Nodes[i].ChildNodes[m].Checked == true)
                                    {
                                        var loopTo4 = this.TreeView1.Nodes[i].ChildNodes[m].ChildNodes.Count - 1;
                                        for (k = 0; k <= loopTo4; k++)
                                            this.TreeView1.Nodes[i].ChildNodes[m].ChildNodes[k].Checked = true;
                                    }
                                    else
                                    {
                                    }
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    var loopTo5 = this.TreeView1.Nodes[i].ChildNodes.Count - 1;
                    for (j = 0; j <= loopTo5; j++)
                    {
                        this.TreeView1.Nodes[i].ChildNodes[j].Checked = false;
                        var loopTo6 = this.TreeView1.Nodes[i].ChildNodes[j].ChildNodes.Count - 1;
                        for (k = 0; k <= loopTo6; k++)
                            this.TreeView1.Nodes[i].ChildNodes[j].ChildNodes[k].Checked = false;
                    }
                }
            }
        }
        public void PopulateSecondLevel(TreeNode node)
        {
            int i, j, k, SecondLevelCounter, m;
            var loopTo = this.TreeView1.Nodes.Count - 1;
            for (i = 0; i <= loopTo; i++)
            {
                SecondLevelCounter = 0;
                var loopTo1 = this.TreeView1.Nodes[i].ChildNodes.Count - 1;
                for (j = 0; j <= loopTo1; j++)
                {
                    if (this.TreeView1.Nodes[i].ChildNodes[j].Checked)
                    {
                        this.TreeView1.Nodes[i].Checked = true;
                        m = 0;
                        var loopTo2 = this.TreeView1.Nodes[i].ChildNodes[j].ChildNodes.Count - 1;
                        for (k = 0; k <= loopTo2; k++)
                        {
                            if (this.TreeView1.Nodes[i].ChildNodes[j].ChildNodes[k].Checked == false)
                            {
                                m = m + 1;
                            }
                        }
                        if (m == this.TreeView1.Nodes[i].ChildNodes[j].ChildNodes.Count)
                        {
                            var loopTo3 = this.TreeView1.Nodes[i].ChildNodes[j].ChildNodes.Count - 1;
                            for (k = 0; k <= loopTo3; k++)
                                this.TreeView1.Nodes[i].ChildNodes[j].ChildNodes[k].Checked = true;
                        }
                    }
                    else
                    {
                        var loopTo4 = this.TreeView1.Nodes[i].ChildNodes[j].ChildNodes.Count - 1;
                        for (k = 0; k <= loopTo4; k++)
                            this.TreeView1.Nodes[i].ChildNodes[j].ChildNodes[k].Checked = false;
                        SecondLevelCounter = SecondLevelCounter + 1;
                        if (SecondLevelCounter == this.TreeView1.Nodes[i].ChildNodes.Count)
                        {
                            this.TreeView1.Nodes[i].Checked = false;
                        }
                    }
                }
            }
        }
        public void PopulateThirdLevel(TreeNode node)
        {
            int i, j, k, ThirdLevelcounter3, ThirdLevelcounter2;
            var loopTo = this.TreeView1.Nodes.Count - 1;
            for (i = 0; i <= loopTo; i++)
            {
                ThirdLevelcounter2 = 0;
                var loopTo1 = this.TreeView1.Nodes[i].ChildNodes.Count - 1;
                for (j = 0; j <= loopTo1; j++)
                {
                    ThirdLevelcounter3 = 0;
                    if (this.TreeView1.Nodes[i].ChildNodes[j].Checked)
                    {
                        this.TreeView1.Nodes[i].ChildNodes[j].Checked = true;
                        var loopTo2 = this.TreeView1.Nodes[i].ChildNodes[j].ChildNodes.Count - 1;
                        for (k = 0; k <= loopTo2; k++)
                        {
                            if (this.TreeView1.Nodes[i].ChildNodes[j].ChildNodes[k].Checked)
                            {
                                this.TreeView1.Nodes[i].Checked = true;
                            }
                            else
                            {
                                ThirdLevelcounter3 = ThirdLevelcounter3 + 1;
                                if (ThirdLevelcounter3 == this.TreeView1.Nodes[i].ChildNodes[j].ChildNodes.Count)
                                {
                                    this.TreeView1.Nodes[i].Checked = false;
                                    this.TreeView1.Nodes[i].ChildNodes[j].Checked = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        ThirdLevelcounter2 = ThirdLevelcounter2 + 1;
                        if (ThirdLevelcounter2 == this.TreeView1.Nodes[i].ChildNodes.Count)
                        {
                            // TreeView1.Nodes(i).Checked = False
                            // TreeView1.Nodes(i).ChildNodes(j).Checked = False
                        }
                        var loopTo3 = this.TreeView1.Nodes[i].ChildNodes[j].ChildNodes.Count - 1;
                        for (k = 0; k <= loopTo3; k++)
                        {
                            if (this.TreeView1.Nodes[i].ChildNodes[j].ChildNodes[k].Checked)
                            {
                                this.TreeView1.Nodes[i].Checked = true;
                                this.TreeView1.Nodes[i].ChildNodes[j].Checked = true;
                            }
                        }
                    }
                }
            }
        }
        public void treeview()
        {
            string str_query1;
            string str_query2;
            string str3;
            // str_query1 = "select menu_name ,menu_id  from menu_master1 order by menu_pos"
            // str3 = "select mid_menuname,mid_menuid,menu_id,href,child from mid_menu order by midmenu_pos"
            // str_query2 = "select submenu_text ,submenu_href ,submenu_id ,menu_id from menu_detail1 order by submenu_pos"
            str_query1 = "select distinct menu_name,menu_id,ordno from popup_new where parentid is null order by ordno";
            str3 = "select distinct menu_name as mid_menuname,menu_id as mid_menuid,href,parentid as menu_id,ordno from popup_new where  parentid in (select menu_id from popup_new where parentid is null ) order by ordno";
            str_query2 = "select distinct menu_name as submenu_text,href as submenu_href,menu_id as submenu_id,parentid as menu_id,ordno from popup_new where parentid in (select menu_id from popup_new where parentid in (select menu_id from popup_new where parentid is null)) order by ordno";

            var DBConnection = new OleDbConnection(retConstr(""));
            DBConnection.Open();

            var AdvPayds1 = new DataSet();
            try
            {
                var AdvPayda1 = new OleDbDataAdapter(str_query1, DBConnection);
                var AdvPayda2 = new OleDbDataAdapter(str3, DBConnection);
                var AdvPayda3 = new OleDbDataAdapter(str_query2, DBConnection);
                AdvPayda1.Fill(AdvPayds1, "dtmenu");
                AdvPayda2.Fill(AdvPayds1, "dtmidmenu");
                AdvPayda3.Fill(AdvPayds1, "dtsubmenu");
                DBConnection.Close();
                AdvPayds1.Relations.Add("SuppTomid", AdvPayds1.Tables["dtmenu"].Columns["menu_id"], AdvPayds1.Tables["dtmidmenu"].Columns["menu_id"]);
                AdvPayds1.Relations.Add("midTosub", AdvPayds1.Tables["dtmidmenu"].Columns["mid_menuid"], AdvPayds1.Tables["dtsubmenu"].Columns["menu_id"]);
                TreeNode nodeSupp, nodemid, nodeProd;
                foreach (DataRow rowSupp in AdvPayds1.Tables["dtmenu"].Rows)
                {
                    nodeSupp = new TreeNode();
                    nodeSupp.Text = rowSupp["menu_name"].ToString();
                    nodeSupp.Value = rowSupp["menu_id"].ToString();
                    this.TreeView1.Nodes.Add(nodeSupp);
                    foreach (var rowmid in rowSupp.GetChildRows("SuppTomid"))
                    {
                        nodemid = new TreeNode();
                        nodemid.Text = rowmid["mid_menuname"].ToString();
                        nodemid.Value = rowmid["mid_menuid"].ToString();
                        nodeSupp.ChildNodes.Add(nodemid);

                        foreach (var rowProd in rowmid.GetChildRows("midTosub"))
                        {
                            nodeProd = new TreeNode();
                            nodeProd.Text = rowProd["submenu_text"].ToString();
                            nodeProd.Value = rowProd["submenu_id"].ToString();
                            nodemid.ChildNodes.Add(nodeProd);
                        }
                    }
                }
                this.TreeView1.ExpandAll();
            }
            catch (Exception ex)
            {
                if (DBConnection.State == ConnectionState.Open)
                {
                    DBConnection.Close();
                }
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var DBConnection = new OleDbConnection(retConstr(""));
            try
            {
                DBConnection.Open();

                // If lstAllCategory.SelectedValue = Nothing Then
                // TreeView1.Nodes.Clear()
                // Me.txtusertype.Value = "Admin"
                // treeview()
                // TreeView1.ExpandAll()
                // 'txtusertype.Focus()
                // SetFocus(txtusertype)
                // Exit Sub
                // End If
                // txtusertype.Value = lstAllCategory.SelectedValue

                this.TreeView1.Nodes.Clear();
                var com3 = new OleDbCommand();
                com3.Connection = DBConnection;
                com3.CommandType = CommandType.Text;
                com3.CommandText = "select usertypeid from usertypepermissions where usertypename=N'" + this.txtusertype.Value.Trim() + "'";
                string tmpVal;
                tmpVal = com3.ExecuteScalar().ToString();

                string str_query1;
                string str_query2;
                string str3;
                // str_query1 = "select menu_name ,menu_id  from menu_master1 order by menu_pos"
                // str3 = "select mid_menuname,mid_menuid,menu_id,href,child from mid_menu order by midmenu_pos"
                // str_query2 = "select submenu_text ,submenu_href ,submenu_id ,menu_id from menu_detail1 order by submenu_pos"
                // str_query1 = "select menu_name,menu_id from popup_new inner join menu_perm on menu_id=menuid where parentid is null and usertypeid='" & tmpVal & "' order by ordno"
                // str3 = "select menu_name as mid_menuname,menu_id as mid_menuid,href,parentid as menu_id from popup_new inner join menu_perm on menu_id=menuid  where   usertypeid= '" & tmpVal & "' and parentid in (select menu_id from popup_new inner join menu_perm on menu_id=menuid where parentid is null and usertypeid='" & tmpVal & "' ) order by ordno"
                // str_query2 = "select menu_name as submenu_text,href as submenu_href,menu_id as submenu_id,parentid as menu_id from popup_new  inner join menu_perm on menu_id=menuid where usertypeid= '" & tmpVal & "' and  parentid in (select menu_id from popup_new inner join menu_perm on menu_id=menuid  where   usertypeid= '" & tmpVal & "' and parentid in (select menu_id from popup_new inner join menu_perm on menu_id=menuid where parentid is null and usertypeid='" & tmpVal & "' )) order by ordno"
                str_query1 = "select distinct menu_name,menu_id,ordno from popup_new where parentid is null order by ordno";
                str3 = "select distinct menu_name as mid_menuname,menu_id as mid_menuid,href,parentid as menu_id,ordno from popup_new where  parentid in (select menu_id from popup_new where parentid is null ) order by ordno";
                str_query2 = "select distinct menu_name as submenu_text,href as submenu_href,menu_id as submenu_id,parentid as menu_id,ordno from popup_new where parentid in (select menu_id from popup_new where parentid in (select menu_id from popup_new where parentid is null)) order by ordno";

                var AdvPayds1 = new DataSet();
                var AdvPayda1 = new OleDbDataAdapter(str_query1, DBConnection);
                var AdvPayda2 = new OleDbDataAdapter(str3, DBConnection);
                var AdvPayda3 = new OleDbDataAdapter(str_query2, DBConnection);
                AdvPayda1.Fill(AdvPayds1, "dtmenu");
                AdvPayda2.Fill(AdvPayds1, "dtmidmenu");
                AdvPayda3.Fill(AdvPayds1, "dtsubmenu");
                AdvPayds1.Relations.Add("SuppTomid", AdvPayds1.Tables["dtmenu"].Columns["menu_id"], AdvPayds1.Tables["dtmidmenu"].Columns["menu_id"]);
                AdvPayds1.Relations.Add("midTosub", AdvPayds1.Tables["dtmidmenu"].Columns["mid_menuid"], AdvPayds1.Tables["dtsubmenu"].Columns["menu_id"]);
                TreeNode nodeSupp, nodemid, nodeProd;
                foreach (DataRow rowSupp in AdvPayds1.Tables["dtmenu"].Rows)
                {
                    nodeSupp = new TreeNode();
                    nodeSupp.Text = rowSupp["menu_name"].ToString();
                    nodeSupp.Value = rowSupp["menu_id"].ToString();
                    // ---------
                    var ds = new DataSet();
                    var da = new OleDbDataAdapter("select menuid from menu_perm where menuid='" + rowSupp["menu_id"].ToString() + "' and usertypeid='" + tmpVal + "'", DBConnection);
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        nodeSupp.Checked = true;
                    }
                    this.TreeView1.Nodes.Add(nodeSupp);
                    foreach (var rowmid in rowSupp.GetChildRows("SuppTomid"))
                    {
                        nodemid = new TreeNode();
                        nodemid.Text = rowmid["mid_menuname"].ToString();
                        nodemid.Value = rowmid["mid_menuid"].ToString();
                        var ds1 = new DataSet();
                        var da1 = new OleDbDataAdapter("select menuid from menu_perm where menuid='" + rowmid["mid_menuid"].ToString() + "' and usertypeid='" + tmpVal + "'", DBConnection);
                        da1.Fill(ds1);
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            nodemid.Checked = true;
                        }
                        // ---------
                        nodeSupp.ChildNodes.Add(nodemid);
                        foreach (var rowProd in rowmid.GetChildRows("midTosub"))
                        {
                            nodeProd = new TreeNode();
                            nodeProd.Text = rowProd["submenu_text"].ToString();
                            nodeProd.Value = rowProd["submenu_id"].ToString();
                            var ds3 = new DataSet();
                            var da3 = new OleDbDataAdapter("select menuid from menu_perm where menuid='" + rowProd["submenu_id"].ToString() + "' and usertypeid='" + tmpVal + "'", DBConnection);
                            da3.Fill(ds3);
                            if (ds3.Tables[0].Rows.Count > 0)
                            {
                                nodeProd.Checked = true;
                            }
                            // ----------
                            nodemid.ChildNodes.Add(nodeProd);
                        }
                    }
                }
                AdvPayda1 = null;
                AdvPayda2 = null;
                AdvPayda3 = null;
                AdvPayds1.Dispose();
                this.TreeView1.ExpandAll();
                DBConnection.Close();
                // Me.cmdSave.Text = Resources.ValidationResources.bS.ToString
                this.cmddelete.Visible = false;
                this.cmdreset.Visible = false;
                clear();
                this.Hidden2.Value = Resources.ValidationResources.RBTop;
                DBConnection.Close();
            }

            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                // Me.Hidden2.Value = "d"
                // libObj.MsgBox1(Resources.ValidationResources.UToRtrvPmsI.ToString, Me)
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Failure, Resources.ValidationResources.UToRtrvPmsI.ToString, Me)
                message.PageMesg(Resources.ValidationResources.UToRtrvPmsI.ToString(), this, dbUtilities.MsgLevel.Failure);
            }
            finally
            {
                if (DBConnection.State == ConnectionState.Open)
                {
                    DBConnection.Close();
                }
                DBConnection.Dispose();
            }

        }

        protected void TreeView1_TreeNodeCheckChanged1(object sender, TreeNodeEventArgs e)
        {
            try
            {
                switch (e.Node.Depth)
                {
                    case 0:
                        {
                            // Populate the first-level nodes.
                            PopulateFirstLevel(e.Node);
                            break;
                        }

                    case 1:
                        {
                            // Populate the second-level nodes.
                            PopulateSecondLevel(e.Node);
                            break;
                        }
                    case 2:
                        {
                            // Populate the third-level nodes.
                            PopulateThirdLevel(e.Node);
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }

        }
        protected void cmdreset_Click(object sender, EventArgs e)
        {
            try
            {
                this.txtusertype.Value = (string)null;
                this.TreeView1.Nodes.Clear();
                treeview();
                this.chkAll.Checked = false;
                clear();

                this.cmdSave.Text = Resources.ValidationResources.bSave.ToString();
                this.cmddelete.Enabled = false;
                this.Hidden2.Value = Resources.ValidationResources.RBTop;
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
        }

        protected void Chksearch_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.Chksearch.Checked == true)
                {
                    // Chksearch.Focus()
                    this.SetFocus(this.Chksearch);
                    this.Label2.Visible = true;
                    this.txtCategory.Value = string.Empty;
                    this.btnCategoryFilter2.Visible = true;
                    this.txtCategory.Visible = true;
                    this.lstAllCategory.Visible = true;
                    this.txtusertype.Value = (string)null;
                    this.TreeView1.Nodes.Clear();
                    treeview();
                    this.Hidden2.Value = "5";
                }
                else
                {
                    // Chksearch.Focus()
                    this.SetFocus(this.Chksearch);
                    this.Label2.Visible = false;
                    this.txtCategory.Value = string.Empty;
                    this.btnCategoryFilter2.Visible = false;
                    this.txtCategory.Visible = false;
                    this.lstAllCategory.Visible = false;
                }
                this.lstAllCategory.Items.Clear();
                var indentcon1 = new OleDbConnection(retConstr(""));
                indentcon1.Open();
                GetData2(indentcon1);
                indentcon1.Close();
                indentcon1.Dispose();
                // Chksearch.Focus()
                this.SetFocus(this.Chksearch);
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }

        }
        public void GetData2(OleDbConnection Conn)
        {

            string sqlStr = null;
            if (!string.IsNullOrEmpty(txtCategory.Value))
            {
                sqlStr = "select distinct usertypename from usertypepermissions where usertypename LIKE N'%" + this.txtCategory.Value + "%' order by usertypename";
            }
            else
            {
                sqlStr = "select distinct usertypename from usertypepermissions order by usertypename";
            }
            libObj.populateGetData2(this.lstAllCategory, sqlStr, "usertypename", "usertypename", Conn);
            // Dim conn As New OleDbConnection(retConStr(Session("LibWiseDBConn")))
            // Dim ds As DataSet = New DataSet
            // Try
            // conn.Open()
            // Dim sqlStr, sqlStr1 As String
            // Dim lstItem As System.Web.UI.WebControls.ListItem = New System.Web.UI.WebControls.ListItem
            // Dim da As OleDbDataAdapter = New OleDbDataAdapter
            // Dim myCommand As OleDbCommand
            // Dim myCommand1 As OleDbCommand

            // Dim RecordCount As Integer
            // If txtCategory.Value <> String.Empty Then
            // sqlStr = "select count(usertypename) from usertypepermissions where usertypename LIKE N'%" & txtCategory.Value & "%'"
            // Else
            // sqlStr = "select count(usertypename) from usertypepermissions"
            // End If
            // myCommand = New OleDbCommand(sqlStr, conn)
            // myCommand.CommandType = CommandType.Text
            // RecordCount = CInt(myCommand.ExecuteScalar())
            // If txtCategory.Value <> String.Empty Then
            // sqlStr1 = "select distinct usertypename from usertypepermissions where usertypename LIKE N'%" & txtCategory.Value & "%' order by usertypename"
            // Else
            // sqlStr1 = "select distinct usertypename from usertypepermissions order by usertypename"
            // End If
            // myCommand1 = New OleDbCommand(sqlStr1, conn)
            // myCommand1.CommandType = CommandType.Text

            // da.SelectCommand = myCommand1
            // da.Fill(ds)
            // lstAllCategory.DataSource = ds
            // lstAllCategory.DataValueField = "usertypename"
            // lstAllCategory.DataTextField = "usertypename"
            // lstAllCategory.DataBind()
            // If RecordCount < 1 Then
            // lstItem.Text = Resources.ValidationResources.NRcdFound.ToString
            // 'txtCategory.Focus()
            // SetFocus(txtCategory)
            // lstItem.Value = ""
            // lstAllCategory.Items.Insert(0, lstItem)
            // lstAllCategory.SelectedIndex = -1
            // End If
            // da = Nothing
            // conn.Close()
            // Catch ex As Exception
            // msglabel.Visible = True
            // msglabel.Text = ex.Message
            // Finally
            // If conn.State = ConnectionState.Open Then
            // conn.Close()
            // End If
            // conn.Dispose()
            // ds.Dispose()
            // End Try
        }

        protected void cmdnext_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("usertype2.aspx?title=" + this.lblTitle.Text);
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }

        }

        protected void btnCategoryFilter_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txtCategory.Value))
                {
                    // Hidden2.Value = "12"
                    // libObj.MsgBox1(Resources.ValidationResources.ESrhCrta.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.ESrhCrta.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.ESrhCrta, this, dbUtilities.MsgLevel.Warning);
                    this.SetFocus(this.txtCategory);
                }
                else
                {
                    this.lstAllCategory.Items.Clear();
                    var indentcon1 = new OleDbConnection(retConstr(""));
                    indentcon1.Open();
                    GetData2(indentcon1);
                    indentcon1.Close();
                    indentcon1.Dispose();
                    // txtCategory.Focus()
                    this.SetFocus(this.txtCategory);
                }
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
        }

        protected void cmdSave_Click(object sender, EventArgs e)
        {


            var DBConnection = new OleDbConnection(retConstr(""));
            var com2 = new OleDbCommand();
            int i, j, l;
            if ((this.txtusertype.Value ?? "") == (Resources.ValidationResources.txtAdmin ?? ""))
            {
                var loopTo = this.TreeView1.Nodes.Count - 1;
                for (i = 0; i <= loopTo; i++)
                {
                    if (this.TreeView1.Nodes[i].Checked == false)
                    {
                        var loopTo1 = this.TreeView1.Nodes[i].ChildNodes.Count - 1;
                        for (j = 0; j <= loopTo1; j++)
                        {
                            if (this.TreeView1.Nodes[i].ChildNodes[j].Checked == false)
                            {
                                var loopTo2 = this.TreeView1.Nodes[i].ChildNodes[j].ChildNodes.Count - 1;
                                for (l = 0; l <= loopTo2; l++)
                                {
                                    if (this.TreeView1.Nodes[i].ChildNodes[j].ChildNodes[l].Checked == true)
                                    {
                                        // libObj.MsgBox1(Resources.ValidationResources.forSelectingAchild.ToString, Me)
                                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.forSelectingAchild.ToString, Me)
                                        message.PageMesg(Resources.ValidationResources.forSelectingAchild, this, dbUtilities.MsgLevel.Warning);
                                        return;
                                    } // Next
                                }
                            }
                        }

                    }
                }
            }
            else
            {
                // libObj.MsgBox1(Resources.ValidationResources.onlyAdminPerMfy.ToString, Me)
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.onlyAdminPerMfy.ToString, Me)
                message.PageMesg(Resources.ValidationResources.onlyAdminPerMfy, this, dbUtilities.MsgLevel.Warning);

                return;
            }
            // Dim tmpStr As String
            try
            {
                DBConnection.Open();
                com2.Connection = DBConnection;
                // ---------
                com2.CommandText = "select usertypeid from usertypepermissions where usertypename=N'" + this.txtusertype.Value.Trim() + "'";
                string tmpVal;
                tmpVal = com2.ExecuteScalar().ToString();
                OleDbTransaction tran;
                tran = DBConnection.BeginTransaction();
                com2.Transaction = tran;
                try
                {
                    // chkAll.Focus()
                    com2.CommandText = "Delete from Menu_Perm where usertypeid=" + tmpVal;
                    com2.ExecuteNonQuery();
                    bool flag1 = false;
                    bool flag2 = false;
                    var loopTo3 = this.TreeView1.Nodes.Count - 1;
                    for (i = 0; i <= loopTo3; i++)
                    {
                        if (this.TreeView1.Nodes[i].Checked == true)
                        {
                            var loopTo4 = this.TreeView1.Nodes[i].ChildNodes.Count - 1;
                            for (j = 0; j <= loopTo4; j++)
                            {
                                if (this.TreeView1.Nodes[i].ChildNodes[j].Checked == true)
                                {
                                    var loopTo5 = this.TreeView1.Nodes[i].ChildNodes[j].ChildNodes.Count - 1;
                                    for (l = 0; l <= loopTo5; l++)
                                    {
                                        if (this.TreeView1.Nodes[i].ChildNodes[j].ChildNodes[l].Checked == true)
                                        {
                                            com2.CommandText = "insert into menu_perm values('" + tmpVal.Trim() + "','" + this.TreeView1.Nodes[i].ChildNodes[j].ChildNodes[l].Value + "','Y',	NULL)";
                                            com2.ExecuteNonQuery();
                                            flag1 = true;
                                        } // Next
                                    }
                                    if (flag1 == true)
                                    {
                                        com2.CommandText = "insert into menu_perm values('" + tmpVal.Trim() + "','" + this.TreeView1.Nodes[i].ChildNodes[j].Value + "','Y',	NULL)";
                                        com2.ExecuteNonQuery();
                                        flag2 = true;
                                    }

                                }

                            }
                            if (flag2 == true)
                            {
                                com2.CommandText = "insert into menu_perm values('" + tmpVal.Trim() + "','" + this.TreeView1.Nodes[i].Value + "','Y',	NULL)";
                                com2.ExecuteNonQuery();

                            }

                        }
                    }
                    tran.Commit();
                    DBConnection.Close();
                    DBConnection.Dispose();
                    // IdentityImpersonateDisable();
                    // libObj.MsgBox1(Resources.ValidationResources.RecSaveSuccess.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, Resources.ValidationResources.RecSaveSuccess.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.RecSaveSuccess, this, dbUtilities.MsgLevel.Success);
                }

                catch (Exception ex1)
                {
                    tran.Rollback();
                    DBConnection.Close();
                    this.msglabel.Visible = true;
                    this.msglabel.Text = ex1.Message;
                }
            }

            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                // Me.Hidden2.Value = "s"
                // libObj.MsgBox1(Resources.ValidationResources.UToSvPmsI.ToString, Me)
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Failure, Resources.ValidationResources.UToSvPmsI.ToString, Me)
                message.PageMesg(Resources.ValidationResources.UToSvPmsI, this, dbUtilities.MsgLevel.Failure);
            }
            finally
            {
                if (DBConnection.State == ConnectionState.Open)
                {
                    DBConnection.Close();
                }
                com2.Dispose();
            }

        }

        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.chkAll.Checked == true)
                {
                    // chkAll.Focus()
                    int i, j, k;
                    var loopTo = this.TreeView1.Nodes.Count - 1;
                    for (i = 0; i <= loopTo; i++)
                    {
                        var loopTo1 = this.TreeView1.Nodes[i].ChildNodes.Count - 1;
                        for (j = 0; j <= loopTo1; j++)
                        {
                            var loopTo2 = this.TreeView1.Nodes[i].ChildNodes[j].ChildNodes.Count - 1;
                            for (k = 0; k <= loopTo2; k++)
                                this.TreeView1.Nodes[i].ChildNodes[j].ChildNodes[k].Checked = true;
                            this.TreeView1.Nodes[i].ChildNodes[j].Checked = true;
                        }
                        this.TreeView1.Nodes[i].Checked = true;
                    }
                }
                else
                {
                    int i, j, k;
                    var loopTo3 = this.TreeView1.Nodes.Count - 1;
                    for (i = 0; i <= loopTo3; i++)
                    {
                        var loopTo4 = this.TreeView1.Nodes[i].ChildNodes.Count - 1;
                        for (j = 0; j <= loopTo4; j++)
                        {
                            var loopTo5 = this.TreeView1.Nodes[i].ChildNodes[j].ChildNodes.Count - 1;
                            for (k = 0; k <= loopTo5; k++)
                                // chkAll.Focus()
                                this.TreeView1.Nodes[i].ChildNodes[j].ChildNodes[k].Checked = false;
                            this.TreeView1.Nodes[i].ChildNodes[j].Checked = false;
                        }
                        this.TreeView1.Nodes[i].Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }

        }

        protected void MMenu1_MenuItemClick(object sender, MenuEventArgs e)
        {
            this.MV1.ActiveViewIndex = Convert.ToInt32(e.Item.Value);
        }

        protected void btnsubmitF_Click(object sender, EventArgs e)
        {
            // For a1 As Integer = 0 To FPer.Items.Count - 1

            // Next
            // Dim rfidDb As Boolean = FPer.Items.FindByValue(13).Selected
            // Dim rfidUserArea As Boolean = FPer.Items.FindByValue(14).Selected
            // If rfidDb = True And rfidUserArea = True Then
            // 'msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, "Only one type Rfid operation is required to be selected.", Me)
            // message.PageMesg("Only one type Rfid operation is required to be selected.", Me, DBUTIL.dbUtilities.MsgLevel.Warning)
            // Return
            // End If


            if (this.FPCheckBox13.Checked & this.FPCheckBox14.Checked)
            {
                message.PageMesg("Only one type Rfid operation is required to be selected.", this, dbUtilities.MsgLevel.Warning);                return;

            }
            if (this.FPCheckBox21.Checked & !this.FPCheckBox20.Checked)
            {
                message.PageMesg("If RFID EPC Read/Delete at Server,Use server Conn IS Only applicable IF previous line 20 is checked.", this, dbUtilities.MsgLevel.Warning);
                return;

            }




            if (!DBI.ExecuteQueryOnDB("Delete from FeaturesPer", DBI.GetConnectionString(DBI.GetConnectionName())))
            {
                return;
            }
            bool flag = true;
            // For i As Integer = 0 To FPer.Items.Count - 1
            // If FPer.Items(i).Selected = True Then
            // Dim parameters As New ParameterCollection()
            // parameters.Add("@Features", DbType.String, FPer.Items(i).Text)
            // parameters.Add("@FID", DbType.Int32, FPer.Items(i).Value)
            // If Not DBI.ExecuteProcedure("Insert_FeaturePer", parameters, DBI.GetConnectionString(DBI.GetConnectionName())) Then
            // flag = False
            // Exit For
            // End If
            // End If
            // Next
            foreach (Control chk in this.pnlPer.Controls)
            {
                string nm = chk.GetType().FullName;
                if (nm.ToLower().Contains("checkbox"))
                {
                    CheckBox c = (CheckBox)chk;
                    if (c.Checked)
                    {
                        string labid = "lab" + c.Text;
                        Label lbabl = (Label)this.pnlPer.FindControl(labid);
                        var parameters = new ParameterCollection();
                        parameters.Add("@Features", DbType.String, lbabl.Text);
                        int it = Convert.ToInt32(c.Text);
                        parameters.Add("@FID", DbType.Int32, it.ToString());
                        if (!DBI.ExecuteProcedure("Insert_FeaturePer", parameters, DBI.GetConnectionString(DBI.GetConnectionName())))
                        {
                            flag = false;
                            break;
                        }
                    }

                }

            }
            // libraryset Opac
            var GetDDL = new GlobClassTr();
            try
            {
                GetDDL.TrOpen();
                string stropac = "update librarysetupinformation set ";
                if (this.rblCallNo.Checked == true)
                {
                    stropac = stropac + " CallNo='Y' ,";
                }
                else
                {
                    stropac = stropac + " CallNo='N' ,";
                }

                if (this.CpyInform.Checked == true)
                {
                    stropac = stropac + " CpyInform='Y' ,";
                }
                else
                {
                    stropac = stropac + " CpyInform='N' ,";
                }

                if (this.Reserve.Checked == true)
                {
                    stropac = stropac + " Reserve='Y' ,";
                }
                else
                {
                    stropac = stropac + " Reserve='N' ,";
                }

                if (this.Content.Checked == true)
                {
                    stropac = stropac + " Content='Y' ,";
                }
                else
                {
                    stropac = stropac + " Content='N' ,";
                }

                if (this.AddKey.Checked == true)
                {
                    stropac = stropac + " AddKey='Y' ,";
                }
                else
                {
                    stropac = stropac + " AddKey='N' ,";
                }

                if (this.AddCart.Checked == true)
                {
                    stropac = stropac + " AddCart='Y' ,";
                }
                else
                {
                    stropac = stropac + " AddCart='N' ,";
                }

                if (this.AuthorIfo.Checked == true)
                {
                    stropac = stropac + " AuthorIfo='Y' ,";
                }
                else
                {
                    stropac = stropac + " AuthorIfo='N' ,";
                }

                if (this.BookInfo.Checked == true)
                {
                    stropac = stropac + " BookInfo='Y' ";
                }
                else
                {
                    stropac = stropac + " BookInfo='N' ";
                }

                GetDDL.IUD(stropac);
            }
            catch (Exception ex)
            {
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Failure, ex.Message, Me)
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
            }
            GetDDL.TrClose();
            if (flag == true)
            {
                // DBI.AlertBox("Features Enable/Disable Successfully!", Me)
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, "Features Enable/Disable Successfully!", Me)
                message.PageMesg("Features Enable/Disable Successfully!", this, dbUtilities.MsgLevel.Success);
            }

            else
            {
                // DBI.AlertBox("Failed!", Me)
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Failure, "Failed!", Me)
                message.PageMesg("Failed!", this, dbUtilities.MsgLevel.Failure);
            }

        }
    }

}