using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using Library.App_Code.MultipleFramworks;
using Library.App_Code.CSharp;
using System.Data;

namespace Library
{
    public partial class usertype1 : BaseClass
    {
        private libGeneralFunctions libObj = new libGeneralFunctions();
        private static string tmpcondition;
        private messageLibrary msgLibrary = new messageLibrary();
        private dbUtilities message = new dbUtilities();
        ApiComm apiCall = new ApiComm();

        protected  void Page_Load(object sender, EventArgs e)
        {
            try
            {
                msglabel.Visible = false;
                if (!IsPostBack)
                {
                    // ---------
                    tmpcondition = Request.QueryString["condition"];
                    if (tmpcondition == "Y")
                    {
                        this.cmdSave.Enabled = true;
                    }
                    else
                    {
                        this.cmdSave.Enabled = false;
                    }
                    this.btnCategoryFilter.CausesValidation = false;
                    this.cmdreset.CausesValidation = false;
                    this.cmdnext.CausesValidation = false;
                    // Me.cmdreturn.CausesValidation = False
                    this.cmddelete.Enabled = false;
                    // libObj.SetFocus("txtusertype", Me)
                   // SetFocus(txtusertype);
                    treeview();
                    txtCategory.Attributes.Add("onkeyup", "txtCategory_onkeyup();");
                    txtCategory.Attributes.Add("onkeydown", "txtCategory_onkeydown();");
                    // lstAllCategory.Attributes.Add("onclick", "return lstAllCategory_onclick();")
                    var lstItem = new ListItem();
                    clear();

                }
                if (IsPostBack == false) // || Request.Form["hSubmit1"] == "2")
                {
                    this.lstAllCategory.Items.Clear();
                    var indentcon1 = new OleDbConnection(retConstr(""));
                    indentcon1.Open();
                    GetData2(indentcon1);
                    indentcon1.Close();
                    indentcon1.Dispose();
                }
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);

            }
        }

        public void treeview()
        {
            string str_query1;
            string str_query2;
            string str3; 
            
            str_query1 = "select distinct menu_name,menu_id,ordno from popup_new where menu_id in(select menuid from menu_perm where usertypeid=1) and parentid is null order by ordno";
            str3 = "select distinct menu_name as mid_menuname,menu_id as mid_menuid,href,parentid as menu_id,ordno from popup_new where  menu_id in(select menuid from menu_perm where usertypeid=1) and  parentid in (select menu_id from popup_new where parentid is null ) order by ordno";
            str_query2 = "select distinct menu_name as submenu_text,href as submenu_href,menu_id as submenu_id,parentid as menu_id,ordno from popup_new where  menu_id in(select menuid from menu_perm where usertypeid=1) and parentid in (select menu_id from popup_new where parentid in (select menu_id from popup_new where parentid is null)) order by ordno";

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
                    nodeSupp.Text = Convert.ToString(rowSupp["menu_name"]);
                    nodeSupp.Value = Convert.ToString(rowSupp["menu_id"]);
                    TreeView1.Nodes.Add(nodeSupp);
                    foreach (var rowmid in rowSupp.GetChildRows("SuppTomid"))
                    {
                        nodemid = new TreeNode();
                        nodemid.Text = Convert.ToString(rowmid["mid_menuname"]);
                        nodemid.Value = Convert.ToString(rowmid["mid_menuid"]);
                        nodeSupp.ChildNodes.Add(nodemid);

                        foreach (var rowProd in rowmid.GetChildRows("midTosub"))
                        {
                            nodeProd = new TreeNode();
                            nodeProd.Text = Convert.ToString(rowProd["submenu_text"]);
                            nodeProd.Value = Convert.ToString(rowProd["submenu_id"]);
                            nodemid.ChildNodes.Add(nodeProd);
                        }
                    }
                }
                TreeView1.ExpandAll();
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

        protected void TreeView1_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
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
        
        public void PopulateFirstLevel(TreeNode node)
        {
            int i, j, k, l, m, FirstLevelCounter;
            FirstLevelCounter = 0;
            var loopTo = TreeView1.Nodes.Count - 1;
            for (i = 0; i <= loopTo; i++)
            {
                if (TreeView1.Nodes[i].Checked)
                {
                    var loopTo1 = TreeView1.Nodes[i].ChildNodes.Count - 1;
                    for (j = 0; j <= loopTo1; j++)
                    {
                        var loopTo2 = TreeView1.Nodes[i].ChildNodes.Count - 1;
                        for (l = 0; l <= loopTo2; l++)
                        {
                            if (TreeView1.Nodes[i].ChildNodes[j].Checked == false)
                            {
                                FirstLevelCounter = FirstLevelCounter + 1;
                            }
                        }
                        if (FirstLevelCounter == TreeView1.Nodes[i].ChildNodes.Count)
                        {
                            var loopTo3 = TreeView1.Nodes[i].ChildNodes.Count - 1;
                            for (m = 0; m <= loopTo3; m++)
                            {
                                if (TreeView1.Nodes[i].ChildNodes[m].Checked == false)
                                {
                                    TreeView1.Nodes[i].ChildNodes[m].Checked = true;
                                    if (TreeView1.Nodes[i].ChildNodes[m].Checked == true)
                                    {
                                        var loopTo4 = TreeView1.Nodes[i].ChildNodes[m].ChildNodes.Count - 1;
                                        for (k = 0; k <= loopTo4; k++)
                                            TreeView1.Nodes[i].ChildNodes[m].ChildNodes[k].Checked = true;
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
                    var loopTo5 = TreeView1.Nodes[i].ChildNodes.Count - 1;
                    for (j = 0; j <= loopTo5; j++)
                    {
                        TreeView1.Nodes[i].ChildNodes[j].Checked = false;
                        var loopTo6 = TreeView1.Nodes[i].ChildNodes[j].ChildNodes.Count - 1;
                        for (k = 0; k <= loopTo6; k++)
                            TreeView1.Nodes[i].ChildNodes[j].ChildNodes[k].Checked = false;
                    }
                }
            }
        }
        public void PopulateSecondLevel(TreeNode node)
        {
            int i, j, k, SecondLevelCounter, m;
            var loopTo = TreeView1.Nodes.Count - 1;
            for (i = 0; i <= loopTo; i++)
            {
                SecondLevelCounter = 0;
                var loopTo1 = TreeView1.Nodes[i].ChildNodes.Count - 1;
                for (j = 0; j <= loopTo1; j++)
                {
                    if (TreeView1.Nodes[i].ChildNodes[j].Checked)
                    {
                        TreeView1.Nodes[i].Checked = true;
                        m = 0;
                        var loopTo2 = TreeView1.Nodes[i].ChildNodes[j].ChildNodes.Count - 1;
                        for (k = 0; k <= loopTo2; k++)
                        {
                            if (TreeView1.Nodes[i].ChildNodes[j].ChildNodes[k].Checked == false)
                            {
                                m = m + 1;
                            }
                        }
                        if (m == TreeView1.Nodes[i].ChildNodes[j].ChildNodes.Count)
                        {
                            var loopTo3 = TreeView1.Nodes[i].ChildNodes[j].ChildNodes.Count - 1;
                            for (k = 0; k <= loopTo3; k++)
                                TreeView1.Nodes[i].ChildNodes[j].ChildNodes[k].Checked = true;
                        }
                    }
                    else
                    {
                        var loopTo4 = TreeView1.Nodes[i].ChildNodes[j].ChildNodes.Count - 1;
                        for (k = 0; k <= loopTo4; k++)
                            TreeView1.Nodes[i].ChildNodes[j].ChildNodes[k].Checked = false;
                        SecondLevelCounter = SecondLevelCounter + 1;
                        if (SecondLevelCounter == TreeView1.Nodes[i].ChildNodes.Count)
                        {
                            TreeView1.Nodes[i].Checked = false;
                        }
                    }
                }
            }
        }
        public void PopulateThirdLevel(TreeNode node)
        {
            int i, j, k, ThirdLevelcounter3, ThirdLevelcounter2;
            var loopTo = TreeView1.Nodes.Count - 1;
            for (i = 0; i <= loopTo; i++)
            {
                ThirdLevelcounter2 = 0;
                var loopTo1 = TreeView1.Nodes[i].ChildNodes.Count - 1;
                for (j = 0; j <= loopTo1; j++)
                {
                    ThirdLevelcounter3 = 0;
                    if (TreeView1.Nodes[i].ChildNodes[j].Checked)
                    {
                        TreeView1.Nodes[i].ChildNodes[j].Checked = true;
                        var loopTo2 = TreeView1.Nodes[i].ChildNodes[j].ChildNodes.Count - 1;
                        for (k = 0; k <= loopTo2; k++)
                        {
                            if (TreeView1.Nodes[i].ChildNodes[j].ChildNodes[k].Checked)
                            {
                                TreeView1.Nodes[i].Checked = true;
                            }
                            else
                            {
                                ThirdLevelcounter3 = ThirdLevelcounter3 + 1;
                                if (ThirdLevelcounter3 == TreeView1.Nodes[i].ChildNodes[j].ChildNodes.Count)
                                {
                                    TreeView1.Nodes[i].Checked = false;
                                    TreeView1.Nodes[i].ChildNodes[j].Checked = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        ThirdLevelcounter2 = ThirdLevelcounter2 + 1;
                        if (ThirdLevelcounter2 == TreeView1.Nodes[i].ChildNodes.Count)
                        {
                            // TreeView1.Nodes(i).Checked = False
                            // TreeView1.Nodes(i).ChildNodes(j).Checked = False
                        }
                        var loopTo3 = TreeView1.Nodes[i].ChildNodes[j].ChildNodes.Count - 1;
                        for (k = 0; k <= loopTo3; k++)
                        {
                            if (TreeView1.Nodes[i].ChildNodes[j].ChildNodes[k].Checked)
                            {
                                TreeView1.Nodes[i].Checked = true;
                                TreeView1.Nodes[i].ChildNodes[j].Checked = true;
                            }
                        }
                    }
                }
            }
        }
        
        protected void cmdSave_Click(object sender, EventArgs e)
        {
            var DBConnection = new OleDbConnection(retConstr(""));
            var com2 = new OleDbCommand();
            int i, j, l;
            
            if (txtusertype.Value == Resources.ValidationResources.txtAdmin)
            {
                var loopTo = TreeView1.Nodes.Count - 1;
                for (i = 0; i <= loopTo; i++)
                {
                    if (TreeView1.Nodes[i].Checked == true)
                    {
                        var loopTo1 = TreeView1.Nodes[i].ChildNodes.Count - 1;
                        for (j = 0; j <= loopTo1; j++)
                        {
                            if (TreeView1.Nodes[i].ChildNodes[j].Checked == true)
                            {
                                var loopTo2 = TreeView1.Nodes[i].ChildNodes[j].ChildNodes.Count - 1;
                                for (l = 0; l <= loopTo2; l++)
                                {
                                    if (TreeView1.Nodes[i].ChildNodes[j].ChildNodes[l].Checked == false)
                                    {
                                        // libObj.MsgBox1(Resources.ValidationResources.APerCantMfy.ToString, Me)
                                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.APerCantMfy.ToString, Me)
                                        message.PageMesg(Resources.ValidationResources.APerCantMfy.ToString(), this, dbUtilities.MsgLevel.Warning);
                                        return;
                                    } // Next
                                }
                            }
                            else
                            {
                                // libObj.MsgBox1(Resources.ValidationResources.APerCantMfy.ToString, Me)
                                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.APerCantMfy.ToString, Me)
                                message.PageMesg(Resources.ValidationResources.APerCantMfy.ToString(), this, dbUtilities.MsgLevel.Warning);
                                return;
                            }
                            // libObj.MsgBox1(Resources.ValidationResources.APerCantMfy.ToString, Me)
                            // Exit Sub
                        }
                    }
                    else
                    {
                        // libObj.MsgBox1(Resources.ValidationResources.APerCantMfy.ToString, Me)
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.APerCantMfy.ToString, Me)
                        message.PageMesg(Resources.ValidationResources.APerCantMfy.ToString(), this, dbUtilities.MsgLevel.Warning);
                        return;
                    }
                }

            }
            
            // Dim tmpStr As String
            
            try
            {
                DBConnection.Open();
                com2.Connection = DBConnection;
                // ---------
                OleDbTransaction tran;
                tran = DBConnection.BeginTransaction();
                com2.Transaction = tran;
                try
                {
                    // chkAll.Focus()
                    SetFocus(chkAll);
                    // -----
                    var dataS1 = new DataSet();
                    var dataS2 = new DataSet();
                    if (this.cmdSave.Text == Resources.ValidationResources.bSave.ToString())
                    {
                        string tmpstr2;
                        com2.CommandType = CommandType.Text;
                        com2.CommandText = "select usertypename from usertypepermissions where usertypename=N'" + txtusertype.Value + "'";
                        tmpstr2 = Convert.ToString(com2.ExecuteScalar());
                        com2.Parameters.Clear();
                        if (tmpstr2 != default)
                        {
                            
                            message.PageMesg(Resources.ValidationResources.UTypAlrE.ToString(), this, dbUtilities.MsgLevel.Warning);
                            treeview();
                            this.txtusertype.Value = null;
                            clear();
                            return;
                        }
                        else
                        {
                            // *****************************jeetu
                            var usertypeCon = new OleDbConnection(retConstr(""));
                            string tmpstr1, tmpstr3;
                            OleDbCommand usertypeCom;
                            usertypeCon.Open();
                            usertypeCom = new OleDbCommand();
                            usertypeCom.Connection = usertypeCon;
                            usertypeCom.CommandType = CommandType.Text;
                            usertypeCom.CommandText = "select coalesce(max(convert(int,usertypeid)),0,max(usertypeid)) from UserTypePermissions";
                            tmpstr1 = Convert.ToString(usertypeCom.ExecuteScalar());
                            tmpstr3 =Convert.ToDouble(tmpstr1) == 0d ? "1" : tmpstr1 + 1d;
                            usertypeCon.Close();
                            usertypeCon.Close();
                            usertypeCom.Dispose();
                            // **********************************
                            com2.CommandType = CommandType.StoredProcedure;
                            com2.CommandText = "insert_usertypepermissions_1";
                            com2.Parameters.Add(new OleDbParameter("@usertypeid_2", OleDbType.VarWChar));
                            com2.Parameters["@usertypeid_2"].Value = tmpstr3;
                            com2.Parameters.Add(new OleDbParameter("@usertypename_2", OleDbType.VarWChar));
                            com2.Parameters["@usertypename_2"].Value = txtusertype.Value.Trim().ToString();
                            com2.ExecuteNonQuery();
                            com2.Parameters.Clear();
                        }
                    }
                    else if (cmdSave.Text == Resources.ValidationResources.bUpdate.ToString())
                    {
                        string tmpstr1;
                        com2.CommandType = CommandType.Text;
                        com2.CommandText = "select usertypeid from usertypepermissions where usertypename=N'" + lstAllCategory.SelectedValue + "'";
                        tmpstr1 = Convert.ToString(com2.ExecuteScalar());
                        com2.Parameters.Clear();
                        com2.CommandType = CommandType.Text;
                        com2.CommandText = "update usertypepermissions set usertypename=N'" + txtusertype.Value.Trim().ToString() + "' where usertypeid='" + tmpstr1 + "'";
                        com2.ExecuteNonQuery();
                        com2.Parameters.Clear();

                        // ****************
                        var dataA1 = new OleDbDataAdapter();
                        string sqlS1;
                        sqlS1 = "select * from menu_perm where usertypeid='" + tmpstr1 + "'";
                        com2.CommandType = CommandType.Text;
                        com2.CommandText = sqlS1;
                        dataA1.SelectCommand = com2;
                        com2.Parameters.Clear();
                        dataA1.Fill(dataS1);
                        dataA1.Dispose();


                    }

                    string tmpStr;
                    com2.CommandType = CommandType.Text;
                    com2.CommandText = "select usertypeid from usertypepermissions where usertypename=N'" + txtusertype.Value.Trim().ToString() + "'";
                    tmpStr = Convert.ToString(com2.ExecuteScalar());
                    // If txtusertype.Value = "Admin" Then
                    // 'Me.chkAll.Checked = True
                    // libObj.MsgBox1(Resources.ValidationResources.RcdCantUpdt.ToString, Me)
                    // Me.chkAll.Checked = True
                    // Exit Sub
                    // End If
                    com2.CommandType = CommandType.Text;
                    com2.CommandText = "delete from menu_perm where usertypeid=" + tmpStr;
                    com2.ExecuteNonQuery();
                    
                    if (TreeView1.CheckedNodes.Count > 0)
                    {
                        foreach (TreeNode node in TreeView1.CheckedNodes)
                        {
                            string sqlstr1 = "select * from popup_new where menu_id='" + node.Value + "'";
                            // ;select * from actiontable where submenu_id='" & node.Value & "'"
                            var da1 = new OleDbDataAdapter();
                            var ds1 = new DataSet();
                            com2.CommandType = CommandType.Text;
                            com2.CommandText = sqlstr1;
                            da1.SelectCommand = com2;
                            com2.Parameters.Clear();
                            da1.Fill(ds1);

                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                com2.CommandType = CommandType.StoredProcedure;
                                com2.CommandText = "insert_UserTypeLavel";
                                com2.Parameters.Add(new OleDbParameter("@UserTypeId_1", OleDbType.Integer));
                                com2.Parameters["@UserTypeId_1"].Value = tmpStr; // CType(Trim(txtusertype.Value), Integer)
                                com2.Parameters.Add(new OleDbParameter("@Menu_Id_2", OleDbType.Integer));
                                com2.Parameters["@Menu_Id_2"].Value = node.Value;
                                com2.Parameters.Add(new OleDbParameter("@Permission_3", OleDbType.VarWChar));
                                com2.Parameters["@Permission_3"].Value = "Y";
                                com2.ExecuteNonQuery();
                                com2.Parameters.Clear();
                            }
                        }
                    }
                    else
                    {
                        // Hidden1.Value = "1"
                        // libObj.MsgBox1(Resources.ValidationResources.AlwPmsTSpUTyp.ToString, Me)
                        // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.AlwPmsTSpUTyp.ToString, Me)
                        message.PageMesg(Resources.ValidationResources.AlwPmsTSpUTyp.ToString(), this, dbUtilities.MsgLevel.Warning);
                        chkAll.Focus();
                        DBConnection.Close();
                        return;
                    }
                    
                    // **************


                    if (cmdSave.Text == Resources.ValidationResources.bUpdate.ToString())
                    {
                        int i1;
                        var loopTo3 = dataS1.Tables[0].Rows.Count - 1;
                        for (i1 = 0; i1 <= loopTo3; i1++)
                        {
                            com2.CommandType = CommandType.Text;
                            com2.CommandText = Convert.ToString("update menu_perm set premission=N'Y' where usertypeid='" + dataS1.Tables[0].Rows[i1][0] + "' and menuid='" + dataS1.Tables[0].Rows[i1][1] + "'");
                            com2.ExecuteNonQuery();
                            com2.Parameters.Clear();
                        }

                        SetFocus(txtusertype);
                    }
                    // ***************
                    tran.Commit();
                    dataS1.Dispose();
                    dataS2.Dispose();
                    this.lstAllCategory.Items.Clear();
                    GetData2(DBConnection);
                    DBConnection.Close();
                    this.cmdSave.Text = Resources.ValidationResources.bSave.ToString();
                    this.cmddelete.Enabled = false;
                    TreeView1.Nodes.Clear();
                    treeview();
                    this.chkAll.Checked = false;
                    this.txtusertype.Value = null;
                    clear();

                    this.Hidden1.Value = Resources.ValidationResources.RBTop;
                    // Me.Hidden2.Value = "2" 'Record saved
                    // libObj.MsgBox1(Resources.ValidationResources.RecSaveSuccess.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, Resources.ValidationResources.RecSaveSuccess.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.RecSaveSuccess, this, dbUtilities.MsgLevel.Success);
                }

                catch (Exception ex1)
                {
                    try
                    {
                        tran.Rollback();
                        // msglabel.Visible = True
                        // msglabel.Text = ex1.Message
                        Hidden1.Value = "s"; // unable to save user type
                        message.PageMesg(ex1.Message, this, dbUtilities.MsgLevel.Failure);
                    }

                    catch (Exception ex2)
                    {
                        // msglabel.Visible = True
                        // msglabel.Text = ex2.Message
                        message.PageMesg(ex2.Message, this, dbUtilities.MsgLevel.Failure);

                        Hidden1.Value = "s";  // unable to save user type
                    }
                }
                DBConnection.Close();
                DBConnection.Dispose();
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message

                // Me.Hidden2.Value = "s"
                // libObj.MsgBox1(Resources.ValidationResources.UToSvPmsI.ToString, Me)
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Failure, Resources.ValidationResources.UToSvPmsI.ToString, Me)
                message.PageMesg(Resources.ValidationResources.UToSvPmsI.ToString(), this, dbUtilities.MsgLevel.Failure);
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

        protected void Chksearch_CheckedChanged(object sender, EventArgs e)
        {
            
            try
            {
                if (this.Chksearch.Checked == true)
                {
                    // Chksearch.Focus()
                    SetFocus(Chksearch);
                    this.Label2.Visible = true;
                    this.txtCategory.Value = string.Empty;
                    this.btnCategoryFilter.Visible = true;
                    this.txtCategory.Visible = true;
                    this.lstAllCategory.Visible = true;
                    this.txtusertype.Value = null;
                    TreeView1.Nodes.Clear();
                    treeview();
                    this.Hidden2.Value = "5";
                }
                else
                {
                    // Chksearch.Focus()
                    SetFocus(Chksearch);
                    this.Label2.Visible = false;
                    this.txtCategory.Value = string.Empty;
                    this.btnCategoryFilter.Visible = false;
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
                SetFocus(Chksearch);
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
            if (!string.IsNullOrEmpty(txtCategory.Value.Trim().ToString()))
            {
                sqlStr = "select distinct usertypename from usertypepermissions where usertypename LIKE N'%" + txtCategory.Value + "%' order by usertypename";
            }
            else
            {
                sqlStr = "select distinct usertypename from usertypepermissions order by usertypename";
            }
            libObj.populateGetData2(lstAllCategory, sqlStr, "usertypename", "usertypename", Conn);
        }

        protected void cmdnext_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("usertype2.aspx?title=" + lblTitle.Text);
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);

            }
        }

        private void Button1_ServerClick(object sender, System.EventArgs e)
        {
            var DBConnection = new OleDbConnection(retConstr(""));
            
            try
            {
                DBConnection.Open();

                if (lstAllCategory.SelectedValue == default)
                {
                    TreeView1.Nodes.Clear();
                    this.txtusertype.Value = null;
                    treeview();
                    TreeView1.ExpandAll();
                 
                    SetFocus(txtusertype);
                    return;
                }

                txtusertype.Value = lstAllCategory.SelectedValue;
                TreeView1.Nodes.Clear();
                var com3 = new OleDbCommand();
                com3.Connection = DBConnection;
                com3.CommandType = CommandType.Text;
                com3.CommandText = "select usertypeid from usertypepermissions where usertypename=N'" + lstAllCategory.SelectedValue + "'";
                string tmpVal;
                tmpVal = Convert.ToString(com3.ExecuteScalar());

                string str_query1;
                string str_query2;
                string str3;
               
                str_query1 = "select distinct menu_name,menu_id,ordno from popup_new where  menu_id in(select menuid from menu_perm where usertypeid=1) and parentid is null order by ordno";
                str3 = "select distinct menu_name as mid_menuname,menu_id as mid_menuid,href,parentid as menu_id,ordno from popup_new where  menu_id in(select menuid from menu_perm where usertypeid=1) and parentid in (select menu_id from popup_new where parentid is null ) order by ordno";
                str_query2 = "select distinct menu_name as submenu_text,href as submenu_href,menu_id as submenu_id,parentid as menu_id,ordno from popup_new where  menu_id in(select menuid from menu_perm where usertypeid=1) and parentid in (select menu_id from popup_new where parentid in (select menu_id from popup_new where parentid is null)) order by ordno";

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
                    nodeSupp.Text = Convert.ToString(rowSupp["menu_name"]);
                    nodeSupp.Value = Convert.ToString(rowSupp["menu_id"]);
                    // ---------
                    var ds = new DataSet();
                    var da = new OleDbDataAdapter(Convert.ToString("select menuid from menu_perm where menuid='"+ rowSupp["menu_id"]+ "' and usertypeid='"+ tmpVal+ "'"), DBConnection);
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        nodeSupp.Checked = true;
                    }
                    TreeView1.Nodes.Add(nodeSupp);
                    foreach (var rowmid in rowSupp.GetChildRows("SuppTomid"))
                    {
                        nodemid = new TreeNode();
                        nodemid.Text = Convert.ToString(rowmid["mid_menuname"]);
                        nodemid.Value = Convert.ToString(rowmid["mid_menuid"]);
                        var ds1 = new DataSet();
                        var da1 = new OleDbDataAdapter(Convert.ToString("select menuid from menu_perm where menuid='"+ rowmid["mid_menuid"]+ "' and usertypeid='"+ tmpVal+ "'"), DBConnection);
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
                            nodeProd.Text = Convert.ToString(rowProd["submenu_text"]);
                            nodeProd.Value = Convert.ToString(rowProd["submenu_id"]);
                            var ds3 = new DataSet();
                            var da3 = new OleDbDataAdapter(Convert.ToString("select menuid from menu_perm where menuid='"+ rowProd["submenu_id"]+ "' and usertypeid='"+ tmpVal+ "'"), DBConnection);
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
                TreeView1.ExpandAll();
                DBConnection.Close();
                this.cmdSave.Text = Resources.ValidationResources.bUpdate.ToString();
                this.cmddelete.Enabled = true;
                clear();
                txtusertype.Disabled = true;
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
                message.PageMesg(Resources.ValidationResources.UToSvPmsI, this, dbUtilities.MsgLevel.Failure);
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

        public void clear()
        {
            
            txtusertype.Disabled = false;
            this.Chksearch.Checked = false;
            this.txtCategory.Visible = false;
            this.txtCategory.Value = string.Empty;
            this.btnCategoryFilter.Visible = false;
            Label2.Visible = false;
            this.lstAllCategory.Visible = false;
            
        }

        protected void btnCategoryFilter_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (txtCategory.Value == string.Empty)
                {
                    // Hidden2.Value = "12"
                    // libObj.MsgBox1(Resources.ValidationResources.ESrhCrta.ToString, Me)
                    // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.ESrhCrta.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.ESrhCrta, this, dbUtilities.MsgLevel.Warning);

                    SetFocus(txtCategory);
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
                    SetFocus(txtCategory);
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
                this.txtusertype.Value = null;
                TreeView1.Nodes.Clear();
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

        protected void cmddelete_Click(object sender, EventArgs e)
        {
            
            try
            {

                if (txtusertype.Value == Resources.ValidationResources.txtAdmin)
                {
                    // libObj.MsgBox1(Resources.ValidationResources.ARecCantDel.ToString, Me)
                    message.PageMesg(Resources.ValidationResources.ARecCantDel, this, dbUtilities.MsgLevel.Warning);

                    return;
                }
                var DBConnection1 = new OleDbConnection(retConstr(""));
                DBConnection1.Open();
                var com2 = new OleDbCommand();
                var com3 = new OleDbCommand();
                int tmpUserTypeId = 0;
                com3.Connection = DBConnection1;
                com3.CommandType = CommandType.Text;
                com3.CommandText = "select usertypeid from usertypepermissions where usertypename=N'" + txtusertype.Value.Trim().ToString().Replace("'", "''") + "'";
                tmpUserTypeId = Convert.ToInt32(com3.ExecuteScalar());

                if (tmpUserTypeId != default)
                {
                    com2.Connection = DBConnection1;
                    OleDbTransaction tran;
                    tran = DBConnection1.BeginTransaction();
                    com2.Transaction = tran;
                    try
                    {
                        com2.CommandType = CommandType.Text;
                        com2.CommandText = "delete from usertypepermissions where usertypeid=" + tmpUserTypeId;
                        com2.ExecuteNonQuery();
                        com2.Parameters.Clear();
                        com2.CommandType = CommandType.Text;
                        com2.CommandText = "delete from userdetails where usertype=" + tmpUserTypeId;
                        com2.ExecuteNonQuery();
                        com2.Parameters.Clear();
                        com2.CommandType = CommandType.Text;
                        com2.CommandText = "delete from menu_perm where usertypeid=" + tmpUserTypeId;
                        com2.ExecuteNonQuery();
                        com2.Parameters.Clear();
                        // com2.CommandText = "delete from usertypelavel2 where usertypeid=" & tmpUserTypeId
                        // com2.ExecuteNonQuery()
                        // com2.Parameters.Clear()
                        // com2.CommandText = "delete from usertypelavel3 where usertypeid=" & tmpUserTypeId
                        // com2.ExecuteNonQuery()
                        // com2.Parameters.Clear()
                        com2.Dispose();
                        tran.Commit();
                        DBConnection1.Close();
                    }
                    catch (Exception ex1)
                    {
                        try
                        {
                            tran.Rollback();
                            // msglabel.Visible = True
                            // msglabel.Text = ex1.Message
                            // Hidden1.Value = "5" 'unable to Delete user type
                            // libObj.MsgBox1(Resources.ValidationResources.UToDelSpUTyp.ToString, Me)
                            // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.UToDelSpUTyp.ToString, Me)
                            message.PageMesg(Resources.ValidationResources.UToDelSpUTyp, this, dbUtilities.MsgLevel.Warning);
                        }

                        catch (Exception ex2)
                        {
                            // msglabel.Visible = True
                            // msglabel.Text = ex2.Message
                            // Hidden1.Value = "5"  'unable to Delete user type
                            // libObj.MsgBox1(Resources.ValidationResources.UToDelSpUTyp.ToString, Me)
                            // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Failure, Resources.ValidationResources.UToDelSpUTyp.ToString, Me)
                            message.PageMesg(Resources.ValidationResources.UToDelSpUTyp, this, dbUtilities.MsgLevel.Warning);

                        }
                    }
                    finally
                    {
                        if (DBConnection1.State == ConnectionState.Open)
                        {
                            DBConnection1.Close();
                        }
                        com2.Dispose();
                    }
                }
                else
                {
                    
                    message.PageMesg(Resources.ValidationResources.SpUTypDNotExst, this, dbUtilities.MsgLevel.Warning);

                }
                this.txtusertype.Value = null;
                TreeView1.Nodes.Clear();
                this.chkAll.Checked = false;
                treeview();
                
                message.PageMesg(Resources.ValidationResources.RecDSful.ToString(), this, dbUtilities.MsgLevel.Success);

                this.Hidden2.Value = Resources.ValidationResources.RBTop;
                this.cmdSave.Text = Resources.ValidationResources.bSave.ToString();
                this.cmddelete.Enabled = false;
            }
            catch (Exception ex)
            {
                // msglabel.Visible = True
                // msglabel.Text = ex.Message
                // Hidden1.Value = "5"
                // libObj.MsgBox1(Resources.ValidationResources.UToDelSpUTyp.ToString, Me)
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Failure, Resources.ValidationResources.UToDelSpUTyp.ToString, Me)
                message.PageMesg(Resources.ValidationResources.UToDelSpUTyp, this, dbUtilities.MsgLevel.Warning);

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
                    var loopTo = TreeView1.Nodes.Count - 1;
                    for (i = 0; i <= loopTo; i++)
                    {
                        var loopTo1 = TreeView1.Nodes[i].ChildNodes.Count - 1;
                        for (j = 0; j <= loopTo1; j++)
                        {
                            var loopTo2 = TreeView1.Nodes[i].ChildNodes[j].ChildNodes.Count - 1;
                            for (k = 0; k <= loopTo2; k++)
                                TreeView1.Nodes[i].ChildNodes[j].ChildNodes[k].Checked = true;
                            TreeView1.Nodes[i].ChildNodes[j].Checked = true;
                        }
                        TreeView1.Nodes[i].Checked = true;
                    }
                }
                else
                {
                    int i, j, k;
                    var loopTo3 = TreeView1.Nodes.Count - 1;
                    for (i = 0; i <= loopTo3; i++)
                    {
                        var loopTo4 = TreeView1.Nodes[i].ChildNodes.Count - 1;
                        for (j = 0; j <= loopTo4; j++)
                        {
                            var loopTo5 = TreeView1.Nodes[i].ChildNodes[j].ChildNodes.Count - 1;
                            for (k = 0; k <= loopTo5; k++)
                                // chkAll.Focus()
                                TreeView1.Nodes[i].ChildNodes[j].ChildNodes[k].Checked = false;
                            TreeView1.Nodes[i].ChildNodes[j].Checked = false;
                        }
                        TreeView1.Nodes[i].Checked = false;
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

        protected void lstAllCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            Button1_ServerClick(sender, e);
            SetFocus(txtusertype);

        }
    }
}