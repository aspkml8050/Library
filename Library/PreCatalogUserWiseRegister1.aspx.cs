using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Library.App_Code.CSharp;
using Library.App_Code.MultipleFramworks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Library
{
    public partial class PreCatalogUserWiseRegister1 : BaseClass
    {
        string tmpcondition;
        libGeneralFunctions libGeneral = new libGeneralFunctions();
        messageLibrary msgLibrary = new messageLibrary();
        libGeneralFunctions LibObj = new libGeneralFunctions();
        DBIStructure DBI = new DBIStructure();
        dbUtilities message = new dbUtilities();
        protected void Page_Load(object sender, EventArgs e)
        {
            string i = 0.ToString();
            this.msglabel.Visible = false;
            // cmdreturn.CausesValidation = False
            // Me.craccessionreg.Visible = False


            try
            {

                // Dim UControl As Control = LoadControl("mainControl.ascx")
                // UControl.ID = "MainControl1"
                // Me.PanelTopCont.Controls.Add(UControl)

                if (!IsPostBack)
                {
                    this.pnl.Visible = false;
                    Session["Filtersuggest"] = "AccBoth";
                    this.SetFocus(this.txtfromdate);
                    this.lblTitle.Text = Request.QueryString["title"];
                    this.chkSelectAll.Attributes.Add("onchange", "doPB()");
                    tmpcondition = Request.QueryString["condition"];
                    // txttodate.Text = Format(Date.Today, hrDate.Value)
                   // this.hdCulture.Value = Request.Cookies["UserCulture"].Value;
                    // If txtfromdate.Text = "" Then
                    // txtfromdate.Text = Format(Date.Now.AddDays(-30), Me.hrDate.Value)
                    // End If
                    // txtfromdate.Text = DateTime.Now.AddYears(-1).ToString(hrDate.Value)
                    // txttodate.Text = DateTime.Now.ToString(hrDate.Value)
                    // Dim indentcon As New OleDbConnection(retConstr(Session("LibWiseDBConn")))
                    // indentcon.Open()
                    // Dim cmd As OleDbCommand = New OleDbCommand()
                    string queryStr = "select i.ShortName+'-'+d.departmentname as dept   from departmentmaster d inner join institutemaster i on i.InstituteCode =d.institutecode order by d.departmentname ";

                    DBI.BindDropDownList(queryStr, "dept", "dept", DBI.DefaultConnectionString(), this.cmbdept, "---Select---");

                    this.txtIndentSearch.Attributes.Add("onkeydown", "txtIndentSearch_onkeydown();");
                   // this.cmdShow.Attributes.Add("onclick", "return cmdshowonclick();");
                    var dt = new DataTable();
                    this.grdUsers.DataSource = null;
                    this.grdUsers.DataBind();
                    // '''''''''''''''''


                    var dscheck = DBI.GetDataSet("select RememberList from UserWiseAccRegReport1 where RememberList = N'Y'", DBI.DefaultConnectionString(), "tbl");
                    // dacheck.Fill(dscheck)
                    if (dscheck.Tables[0].Rows.Count > 0)
                    {
                        this.chk_List.Checked = true;
                    }

                    if (this.chk_List.Checked == true)
                    {



                        var dsListBox2 = DBI.GetDataSet("select distinct colNameheader,colName,id from UserWiseAccRegReport1 where RememberList != N'Y' order by id", DBI.DefaultConnectionString(), "tbl");
                        var dsListBox1 = DBI.GetDataSet("select distinct colNameheader,colName,id,Cast(SerialList as bigint) as SerialList from UserWiseAccRegReport1 where RememberList = N'Y' order by SerialList", DBI.DefaultConnectionString(), "tbl");

                        try
                        {
                            // daListBox2.Fill(dsListBox2)
                            // daListBox1.Fill(dsListBox1)
                            this.ListBox2.Items.Clear();
                            this.ListBox1.Items.Clear();
                            this.ListBox2.DataSource = dsListBox2;
                            this.ListBox2.DataTextField = "colNameheader";
                            this.ListBox2.DataValueField = "colName";
                            this.ListBox1.DataSource = dsListBox1;
                            this.ListBox1.DataTextField = "colNameheader";
                            this.ListBox1.DataValueField = "colName";
                            this.ListBox2.DataBind();
                            this.ListBox1.DataBind();
                        }
                        catch (Exception ex)
                        {
                            // this.msglabel.Visible = true;
                            //this.msglabel.Text = Information.Err().Description;
                            message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
                        }
                        finally
                        {

                            dsListBox1.Dispose();

                        }
                    }
                    else
                    {
                        // Dim da As New OleDbDataAdapter("", indentcon)
                        var ds = DBI.GetDataSet("select distinct colNameheader,colName,id from UserWiseAccRegReport1 order by id", DBI.DefaultConnectionString(), "tbl");
                        try
                        {
                            // da.Fill(ds)
                            this.ListBox2.Items.Clear();
                            this.ListBox1.Items.Clear();
                            this.ListBox2.DataSource = ds;
                            this.ListBox2.DataTextField = "colNameheader";
                            this.ListBox2.DataValueField = "colName";
                            this.ListBox2.DataBind();
                        }
                        catch (Exception ex)
                        {
                            this.msglabel.Visible = true;
//                            this.msglabel.Text = Information.Err().Description;
                            message.PageMesg(ex.Message, this, dbUtilities.MsgLevel.Failure);
                        }
                        finally
                        {
                            // da.Dispose()
                            ds.Dispose();
                            // indentcon.Close()
                            // indentcon.Dispose()
                        }
                    }

                }
                this.msglabel.Visible = false;
            }
            catch (Exception ex)
            {
                this.msglabel.Visible = true;
                this.msglabel.Text = ex.Message;
            }
            finally
            {
            }
        }
        protected void ddl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // AutoCompleteExtender1.ContextKey = ddl1.SelectedValue
            Session["SearchKey"] = this.ddl1.SelectedValue;
        }
        protected void btnaddall_Click(object sender, EventArgs e)
        {
            this.AddRemoveAll(this.ListBox2, this.ListBox1);

            this.SetFocus(this.cmdShow);
        }

        protected void btnadditem_Click(object sender, EventArgs e)
        {
            this.AddRemoveItem(this.ListBox2, this.ListBox1);
        }

        protected void btnremoveitem_Click(object sender, EventArgs e)
        {
            this.AddRemoveItem(this.ListBox1, this.ListBox2);
        }

        protected void btnremoveall_Click(object sender, EventArgs e)
        {
            this.AddRemoveAll(this.ListBox1, this.ListBox2);
        }
        protected void btnDn_Click(object sender, EventArgs e)
        {
            movedown();
        }
        private void movedown()
        {
            try
            {
                int iIndex;
                int iCount;
                int iOffset;
                int iInsertAt;
                int iIndexSelectedMarker = -1;
                string lItemData;
                string lItemval;
                iCount = this.ListBox1.Items.Count;
                iIndex = iCount - 1;
                iOffset = 1;
                while (iIndex >= 0)
                {
                    if (this.ListBox1.SelectedIndex >= 0)
                    {
                        lItemData = this.ListBox1.SelectedItem.Text.ToString();
                        lItemval = this.ListBox1.SelectedItem.Value.ToString();
                        iIndexSelectedMarker = this.ListBox1.SelectedIndex;
                        if (!(-1 == iIndexSelectedMarker))
                        {
                            int iIndex2 = 0;
                            while (iIndex2 < iCount - 1)
                            {
                                if ((lItemval ?? "") == (this.ListBox1.Items[iIndex2].Value.ToString() ?? ""))
                                {
                                    this.ListBox1.Items.RemoveAt(iIndex2);
                                    iInsertAt = (iIndex2 + iOffset) < 0? 0: iIndex2 + iOffset;
                                    var li = new ListItem(lItemData, lItemval);
                                    this.ListBox1.Items.Insert(iInsertAt, li);
                                    break;
                                }
                                System.Threading.Interlocked.Increment(ref iIndex2);
                            }
                        }
                    }
                    iIndex = iIndex - 1;
                }
                if (iIndexSelectedMarker == this.ListBox1.Items.Count - 1)
                {
                    this.ListBox1.SelectedIndex = iIndexSelectedMarker;
                }
                else
                {
                    this.ListBox1.SelectedIndex = iIndexSelectedMarker + 1;
                }
            }
            catch (Exception ex)
            {
                this.msglabel.Visible = true;
                this.msglabel.Text = ex.Message;
            }
        }
        private void AddRemoveAll(ListBox aSource, ListBox aTarget)
        {
            try
            {
                foreach (ListItem item in aSource.Items)
                    aTarget.Items.Add(item);
                aSource.Items.Clear();
            }
            catch (Exception ex)
            {
                this.msglabel.Visible = true;
                this.msglabel.Text = ex.Message;
            }
        }
        private void AddRemoveItem(ListBox aSource, ListBox aTarget)
        {
            ListItemCollection licCollection;
            try
            {
                licCollection = new ListItemCollection();
                int intCount = 0;
                while (intCount < aSource.Items.Count)
                {
                    if (aSource.Items[intCount].Selected == true)
                    {
                        licCollection.Add(aSource.Items[intCount]);
                    }
                    Math.Min(System.Threading.Interlocked.Increment(ref intCount), intCount - 1);
                }
                int intCount1 = 0;
                while (intCount1 < licCollection.Count)
                {
                    aSource.Items.Remove(licCollection[intCount1]);
                    aTarget.Items.Add(licCollection[intCount1]);
                    Math.Min(System.Threading.Interlocked.Increment(ref intCount1), intCount1 - 1);
                }
            }
            catch (Exception ex)
            {
                this.msglabel.Visible = true;
                this.msglabel.Text = ex.Message;
            }
            finally
            {
                licCollection = null;
            }
        }

        protected void btnUp_Click(object sender, EventArgs e)
        {
            moveup();
        }
        private void moveup()
        {
            int iIndex;
            int iCount;
            int iOffset;
            int iInsertAt;
            int iIndexSelectedMarker = -1;
            string lItemData;
            string lItemval;
            try
            {
                iCount = this.ListBox1.Items.Count;
                iIndex = 0;
                iOffset = -1;
                while (iIndex < iCount)
                {
                    if (this.ListBox1.SelectedIndex > 0)
                    {
                        lItemval = this.ListBox1.SelectedItem.Value.ToString();
                        lItemData = this.ListBox1.SelectedItem.Text.ToString();
                        iIndexSelectedMarker = this.ListBox1.SelectedIndex;
                        if (!(-1 == iIndexSelectedMarker))
                        {
                            int iIndex2 = 0;
                            while (iIndex2 < iCount)
                            {
                                if ((lItemval ?? "") == (this.ListBox1.Items[iIndex2].Value.ToString() ?? ""))
                                {
                                    this.ListBox1.Items.RemoveAt(iIndex2);
                                    iInsertAt =(iIndex2 + iOffset) < 0? 0: iIndex2 + iOffset;
                                    var li = new ListItem(lItemData, lItemval);
                                    this.ListBox1.Items.Insert(iInsertAt, li);
                                }
                                System.Threading.Interlocked.Increment(ref iIndex2);
                            }
                        }
                    }
                    else if (-1 == iIndexSelectedMarker)
                    {
                        iIndexSelectedMarker = iIndex;
                    }
                    iIndex = iIndex + 1;
                }
                if (iIndexSelectedMarker == 0)
                {
                    this.ListBox1.SelectedIndex = iIndexSelectedMarker;
                }
                else
                {
                    this.ListBox1.SelectedIndex = iIndexSelectedMarker - 1;
                }
            }
            catch (Exception ex)
            {
                this.msglabel.Visible = true;
                this.msglabel.Text = ex.Message;
            }
        }

        protected void optprePubwise_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void optPreDept_CheckedChanged(object sender, EventArgs e)
        {
            this.tt1.Visible = true;
        }

        protected void optPreAccNo_CheckedChanged(object sender, EventArgs e)
        {
            this.tt1.Visible = true;
        }

        protected void OptpreUserWise_CheckedChanged(object sender, EventArgs e)
        {
            this.tt1.Visible = true;
        }
        protected void chk_List_CheckedChanged(object sender, EventArgs e)
        {
            var con = new OleDbConnection(retConstr(""));



        }

        private bool isNumeric(string str)
        {
            bool isnum = true;
            try
            {
                double n=Convert .ToDouble(str);
            }
            catch
            {
                isnum = false;
            }
            return isnum;
        }
        protected void cmdPrint_ServerClick(object sender, EventArgs e)
        {
            string strUser = string.Empty;
            string filterqry = string.Empty;
            string strActivity = string.Empty;
            bool isSelected = false;
            int iCounter;
            this.msglabel.Visible = false;
            var myConnection = new OleDbConnection(retConstr(""));
            var OorderBy = default(string);
            // If RadioButtonList1.SelectedItem.Text <> "Book" Then
            // 'LibObj.MsgBox1("Report is available Only for Books.", Me)
            // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, "Report is available Only for Books.", Me)
            // Exit Sub
            // End If

            if (this.optPreAccNo.Checked == true)
            {
                OorderBy = " order by dbo.pleft1(accno) , cast (dbo.pright1(accno) as numeric ) ,dbo.Suffix(accno) ";
            }
            // If RadioButtonList1.SelectedItem.Text <> "Book" Then
            if (this.optPreDept.Checked == true)
            {
                OorderBy = " order by new_acc_reg.dept";

            }
            if (this.optprePubwise.Checked == true)
            {
                OorderBy = " order by new_acc_reg.pubname";

            }
            if (this.OptpreUserWise.Checked)
            {
                OorderBy = " order by new_acc_reg.userid";

            }
            if (this.RBBill.Checked == true)
            {
                OorderBy = " order by new_acc_reg.DocNo";

            }
            // Else
            // If optPreDept.Checked = True Then
            // OorderBy = " order by new_acc_regTHesis.dept"

            // End If
            // If optprePubwise.Checked = True Then
            // OorderBy = " order by new_acc_regTHesis.pubname"

            // End If
            // If OptpreUserWise.Checked Then
            // OorderBy = " order by new_acc_regTHesis.userid"

            // End If
            // If RBBill.Checked = True Then
            // OorderBy = " order by new_acc_regTHesis.DocNo"

            // End If
            // End If

            // '''''''''''''''''''''''''''''''''''
            // Dim cmd2 As New OleDbCommand
            var cmdRembList = new OleDbCommand();

            DateTime dtn = DateTime.Now;

            if (!string.IsNullOrEmpty(this.txttodate.Text))
            {
                DateTime todt = Convert.ToDateTime(this.txttodate.Text);
                if (todt > dtn)
                {
                    message.PageMesg("Given Date should not be Max then to Current System Date! ", this, dbUtilities.MsgLevel.Warning);
                    // LibObj.MsgBox("Given Date should not be Max then to Current System Date! ", Me)
//                    msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, "Given Date should not be Max then to Current System Date! ", this);
                    return;
                }
            }
            if (this.chk_List.Checked == true)
            {
                try
                {
                    cmdRembList.Connection = myConnection;
                    cmdRembList.CommandText = "update UserwiseAccRegReport1 set RememberList='' , SerialList='' where rememberList=N'Y'";
                    cmdRembList.Connection.Open();
                    cmdRembList.ExecuteNonQuery();

                    cmdRembList.Parameters.Clear();

                    // cmd2.Connection = myConnection

                    string ID1 = "0";
                    string str1 = null;
                    string strs = null;
                    int js = 0;
                    foreach (ListItem i1 in this.ListBox1.Items)
                    {
                        ID1 = (Convert.ToDouble(ID1) + 1d).ToString();
                        // If strs <> Nothing Then
                        // strs &= ",'" & i1.Text & "'"
                        // Else
                        // strs = "'" & i1.Text & "'"
                        // End If
                        str1 = "N'" + i1.Text + "'";
                        cmdRembList.CommandText = "update UserwiseAccRegReport1 set RememberList = N'Y', SerialList=N'" + ID1 + "' where ColNameHeader in(" + str1 + ")";
                        cmdRembList.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    this.msglabel.Visible = true;
                    this.msglabel.Text = ex.Message;
                }
                finally
                {
                    cmdRembList.Connection.Close();
                    cmdRembList.Dispose();
                    // cmd2.Connection.Close()
                    // cmd2.Dispose()
                }
            }
            else
            {
                var cmd1 = new OleDbCommand();
                try
                {
                    cmd1.Connection = myConnection;
                    cmd1.CommandText = "update UserwiseAccRegReport1 set RememberList='' , SerialList='' where rememberList=N'Y'";
                    cmd1.Connection.Open();
                    cmd1.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    this.msglabel.Visible = true;
                    this.msglabel.Text = ex.Message;
                }
                finally
                {
                    cmd1.Connection.Close();
                    cmd1.Dispose();
                }
            }
            // '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            if (this.grdUsers.Items.Count > 0)
            {
                var loopTo = this.grdUsers.Items.Count - 1;
                for (iCounter = 0; iCounter <= loopTo; iCounter++)
                {
                    var ctl = new CheckBox();
                    ctl = (CheckBox)this.grdUsers.Items[iCounter].Cells[0].FindControl("Chkselect");
                    if (ctl.Checked == true)
                    {
                        isSelected = true;
                        if (string.IsNullOrEmpty(strUser))
                        {
                            strUser += "N'" + this.grdUsers.Items[iCounter].Cells[1].Text + "'";
                        }
                        else
                        {
                            strUser += ",N'" + this.grdUsers.Items[iCounter].Cells[1].Text + "'";
                        }
                    }
                }
            }

            // Commented By Aamir on 23-06-2015
            // Dim acno_Range_Filter As String = String.Empty
            // If IsNumeric(txtFAcNo.Value.Trim()) Or IsNumeric(txtTAcNo.Value.Trim()) Then
            // acno_Range_Filter = " where 1=1 "
            // If IsNumeric(txtFAcNo.Value.Trim()) Then
            // acno_Range_Filter = acno_Range_Filter & " and Cast( REPLACE(REPLACE(LTRIM(RTRIM(accno)),'" & txtAcc_Prefix.Value.Trim() & "',''),'" & txtAcc_Suffix.Value.Trim() & "','') as bigint) >= " & txtFAcNo.Value.Trim()
            // End If
            // If IsNumeric(txtTAcNo.Value.Trim()) Then
            // acno_Range_Filter = acno_Range_Filter & " and Cast( REPLACE(REPLACE(LTRIM(RTRIM(accno)),'" & txtAcc_Prefix.Value.Trim() & "',''),'" & txtAcc_Suffix.Value.Trim() & "','') as bigint) <= " & txtTAcNo.Value.Trim()
            // End If
            // filterqry = filterqry & " new_acc_reg.accno like '" & txtAcc_Prefix.Value.Trim() & "%" & txtAcc_Suffix.Value.Trim() & "' "
            // End If
            string dtcolstr = "";
            // If RadioButtonList1.SelectedItem.Value = "Book" Then
            dtcolstr = "new_acc_reg.accdt";
            if (this.ddldttype.SelectedValue == "1")
            {
                dtcolstr = "new_acc_reg.catalogdate";
            }
            if (this.cmbdept.SelectedItem.Text != "---Select---")
            {
                if (!string.IsNullOrEmpty(filterqry))
                {
                    filterqry = filterqry + this.DropDownList1.SelectedItem.Text + " (new_acc_reg.dept=N'" + this.cmbdept.SelectedItem.Text + "')";
                }
                else
                {
                    filterqry = "(new_acc_reg.dept=N'" + this.cmbdept.SelectedItem.Text + "')";
                }
            }
            if (!string.IsNullOrEmpty(filterqry))
            {
                filterqry = filterqry + " and new_acc_reg.flg='" + this.RadioButtonList1.SelectedValue + "'";
            }
            else
            {
                filterqry = "  new_acc_reg.flg='" + this.RadioButtonList1.SelectedValue + "'";
            }
            if (this.chkreport.Items[0].Selected == true & this.chkreport.Items[1].Selected == false)
            {
                if (!string.IsNullOrEmpty(filterqry))
                {
                    filterqry = filterqry + " and (new_acc_reg.classnumber is  null )";
                }
                else
                {
                    filterqry = "  (new_acc_reg.classnumber is  null )";
                }
            }
            if (this.chkreport.Items[1].Selected == true & this.chkreport.Items[0].Selected == false)
            {
                if (!string.IsNullOrEmpty(filterqry))
                {
                    filterqry = filterqry + " and (new_acc_reg.classnumber is not null )";
                }
                else
                {
                    filterqry = "  (new_acc_reg.classnumber is not null )";
                }
            }
            // Else
            // dtcolstr = "new_acc_regTHesis.accdt"
            // If ddldttype.SelectedValue = "1" Then
            // dtcolstr = "new_acc_regTHesis.catalogdate"
            // End If
            // If Me.cmbdept.SelectedItem.Text <> "---Select---" Then
            // If filterqry <> "" Then
            // filterqry = filterqry & Me.DropDownList1.SelectedItem.Text & " (new_acc_regTHesis.dept=N'" & Me.cmbdept.SelectedItem.Text & "')"
            // Else
            // filterqry = "(new_acc_regTHesis.dept=N'" & Me.cmbdept.SelectedItem.Text & "')"
            // End If
            // End If
            // If filterqry <> String.Empty Then
            // filterqry = filterqry & " and new_acc_regTHesis.flg='" & RadioButtonList1.SelectedValue & "'"
            // Else
            // filterqry = "  new_acc_regTHesis.flg='" & RadioButtonList1.SelectedValue & "'"
            // End If
            // If chkreport.Items(0).Selected = True And chkreport.Items(1).Selected = False Then
            // If filterqry <> String.Empty Then
            // filterqry = filterqry & " and (new_acc_regTHesis.classnumber is  null )"
            // Else
            // filterqry = "  (new_acc_regTHesis.classnumber is  null )"
            // End If
            // End If
            // If chkreport.Items(1).Selected = True And chkreport.Items(0).Selected = False Then
            // If filterqry <> String.Empty Then
            // filterqry = filterqry & " and (new_acc_regTHesis.classnumber is not null )"
            // Else
            // filterqry = "  (new_acc_regTHesis.classnumber is not null )"
            // End If
            // End If
            // End If

            if (!string.IsNullOrEmpty(this.txtfromdate.Text))
            {
                if (!string.IsNullOrEmpty(filterqry))
                {
                    filterqry = filterqry + " and (" + dtcolstr + " >='" + txtfromdate.Text.Trim() + "')";
                }
                else
                {
                    filterqry = "(" + dtcolstr + ">='" + txtfromdate.Text.Trim() + "')";
                }
            }
            if (!string.IsNullOrEmpty(this.txttodate.Text))
            {
                if (!string.IsNullOrEmpty(filterqry))
                {
                    filterqry = filterqry + " and ( " + dtcolstr + "<='" + txttodate.Text + "')";
                }
                else
                {
                    filterqry = "(" + dtcolstr + "<='" + txttodate.Text + "')";
                }
            }

            // For filter Selcted Item' s Condition
            if (ddl1.SelectedItem.Text.Trim() == "Title")
            {
                if (!string.IsNullOrEmpty(filterqry))
                {
                    filterqry = filterqry + this.DropDownList7.SelectedItem.Text + "(Title='" + this.txttitle.Text + "')   ";
                }
                else
                {
                    filterqry = "(Title='" + this.txttitle.Text + "')   ";
                }
            }
            else if (this.ddl1.SelectedItem.Text == "Accession No.")
            {
                if (!string.IsNullOrEmpty(filterqry))
                {
                    filterqry = filterqry + this.DropDownList7.SelectedItem.Text + "(accno='" + this.txttitle.Text + "')   ";
                }
                else
                {
                    filterqry = "(accno='" + this.txttitle.Text + "')   ";
                }
            }

            // ----------------------------------------------------------------------------------------------------------------------------------------------
            // By Aamir on 23-06-2015
            if (string.IsNullOrEmpty(filterqry.Trim()))
            {
                filterqry = " 1=1 ";
            }

            if (!string.IsNullOrEmpty(this.txtAcc_Prefix.Value.Trim()) && string.IsNullOrEmpty(this.txtAcc_Suffix.Value.Trim()))
            {
                filterqry += " and UPPER(LTRIM(RTRIM(REPLACE(accno,acnonumeric,'-')))) = '" + this.txtAcc_Prefix.Value.Trim().ToUpper() + "-'";
            }
            if (string.IsNullOrEmpty(this.txtAcc_Prefix.Value.Trim()) && !string.IsNullOrEmpty(this.txtAcc_Suffix.Value.Trim()))
            {
                filterqry += " and UPPER(LTRIM(RTRIM(REPLACE(accno,acnonumeric,'-')))) = '-" + this.txtAcc_Suffix.Value.Trim().ToUpper() + "'";
            }
            if (!string.IsNullOrEmpty(this.txtAcc_Prefix.Value.Trim()) && !string.IsNullOrEmpty(this.txtAcc_Suffix.Value.Trim()))
            {
                filterqry += " and UPPER(LTRIM(RTRIM(REPLACE(accno,acnonumeric,'-')))) = '" + this.txtAcc_Prefix.Value.Trim().ToUpper() + "-" + this.txtAcc_Suffix.Value.Trim().ToUpper() + "'";
            }

            if (string.IsNullOrEmpty(this.txtAcc_Prefix.Value.Trim()) && string.IsNullOrEmpty(this.txtAcc_Suffix.Value.Trim()) && isNumeric(this.txtFAcNo.Value.Trim()))
            {
                filterqry += " and UPPER(LTRIM(RTRIM(REPLACE(accno,acnonumeric,'-')))) = '-' and acnoNUMERIC >= Cast('" + this.txtFAcNo.Value.Trim() + "' as bigint)";
            }
            else if (isNumeric( txtFAcNo.Value.Trim()))
            {
                filterqry += " and acnoNUMERIC >= Cast('" + this.txtFAcNo.Value.Trim() + "' as bigint)";
            }

            if (string.IsNullOrEmpty(this.txtAcc_Prefix.Value.Trim()) && string.IsNullOrEmpty(this.txtAcc_Suffix.Value.Trim()) && isNumeric( txtTAcNo.Value.Trim()))
            {
                filterqry += " and UPPER(LTRIM(RTRIM(REPLACE(accno,acnonumeric,'-')))) = '-' and acnoNUMERIC <= Cast('" + this.txtTAcNo.Value.Trim() + "' as bigint)";
            }
            else if (isNumeric(this.txtTAcNo.Value.Trim()))
            {
                filterqry += " and acnoNUMERIC <= Cast('" + this.txtTAcNo.Value.Trim() + "' as bigint)";
            }

            if (!string.IsNullOrEmpty(this.txtAcc.Text.Trim()))
            {

                string[] query = this.txtAcc.Text.Trim().Split(',');
                string str1 = string.Empty;
                for (int a = 0, loopTo1 = query.Length - 1; a <= loopTo1; a++)
                {
                    query[a] = query[a].Trim();
                    if (Convert.ToBoolean(query[a]))
                    {
                        str1 += "'" + query[a] + "'" + ",";
                    }
                }
                string Accession = str1.Remove(str1.Length - 1);
                // Dim query = From val In mystring.Split(",")Integer.Parse(val)
                filterqry += " and accno in (" + Accession + ")";
            }
            // ----------------------------------------------------------------------------------------------------------------------------------------------

            if (string.IsNullOrEmpty(filterqry.Trim()))
            {
                filterqry = " 1=1 ";
            }
            if (!string.IsNullOrEmpty(this.txtBnoF.Text.Trim()))
            {
                filterqry += " and biilno>='" + this.txtBnoF.Text.Trim() + "' ";
            }
            if (!string.IsNullOrEmpty(this.txtBnoU.Text.Trim()))
            {
                filterqry += " and biilno<='" + this.txtBnoU.Text.Trim() + "' ";
            }


            string searchqry;
            bool prc = false;
            bool orgprc = false;
            var arr = new ArrayList();
            var arr1 = new ArrayList();
            string str = null;
            int j = 0;
            // If RadioButtonList1.SelectedItem.Value = "Book" Then
            foreach (ListItem i in this.ListBox1.Items)
            {
                arr.Add(i.Value);
                arr1.Add(i.Text);
                if (i.Value == "new_acc_reg.bookprice")
                {
                    prc = true;
                }

                if (i.Value == "new_acc_reg.originalprice")
                {
                    orgprc = true;
                }
                if (!string.IsNullOrEmpty(str))
                {
                    str += "," + i.Value;
                }
                else
                {
                    str = i.Value;
                }
            }
            // Else
            // For Each i In ListBox1.Items
            // arr.Add(i.Value)
            // arr1.Add(i.Text)
            // If (i.Value = "new_acc_regTHesis.bookprice") Then
            // prc = True
            // End If

            // If (i.Value = "new_acc_regTHesis.originalprice") Then
            // orgprc = True
            // End If
            // If str <> Nothing Then
            // str &= "," & i.Value
            // Else
            // str = i.Value
            // End If
            // Next
            // End If


            if (!string.IsNullOrEmpty(str))
            {
                if (this.RadioButtonList1.SelectedItem.Value == "Book")
                {
                    searchqry = "select new_acc_reg.accno,new_acc_reg.title,new_acc_reg.authortype,new_acc_reg.Dept,new_acc_reg.Pubname,new_acc_reg.vendorname,new_acc_reg.bookprice,new_acc_reg.originalprice,new_acc_reg.originalcurrency,new_acc_reg.*,dbo.pleft1(accno) as a1, cast (dbo.pright1(accno) as numeric ) as a2,dbo.Suffix(accno) as suffix,institutename,libraryname,address,city,pincode,state,phoneno,fax,email,gram  " + LibObj.logo(myConnection) + " from new_acc_reg cross join librarysetupinformation where " + filterqry + (strUser == ""? "": " and (userId in(" + strUser + "))") + OorderBy;
                }
                else
                {
                    searchqry = " select  new_acc_reg.accno,new_acc_reg.title,new_acc_reg.authortype,new_acc_reg.Dept,new_acc_reg.Pubname,new_acc_reg.vendorname,new_acc_reg.bookprice,new_acc_reg.originalprice,new_acc_reg.originalcurrency,new_acc_reg.*,dbo.pleft1(accno) as a1, cast (dbo.pright1(accno) as numeric ) as a2,dbo.Suffix(accno) as suffix,institutename,libraryname,address,city,pincode,state,phoneno,fax,email,gram  " + LibObj.logo(myConnection) + " from  new_acc_regTHesis new_acc_reg cross join librarysetupinformation where " + filterqry + (strUser == ""? "": " and (userId in(" + strUser + "))") + OorderBy;
                }
            }


            else if (!(this.optprePubwise.Checked == true))
            {
                LibObj.MsgBox("Select fields.", this);
                return;
            }
            if (this.optprePubwise.Checked == true)
            {
                if (this.RadioButtonList1.SelectedItem.Value == "Book")
                {
                    searchqry = "select new_acc_reg.accno,new_acc_reg.title,new_acc_reg.authortype,new_acc_reg.Dept,new_acc_reg.Pubname,new_acc_reg.bookprice,new_acc_reg.originalprice,new_acc_reg.originalcurrency,new_acc_reg.*,dbo.pleft1(accno) as a1, cast (dbo.pright1(accno) as numeric ) as a2,dbo.Suffix(accno) as suffix,institutename,libraryname,address,city,pincode,state,phoneno,fax,email,gram " + LibObj.logo(myConnection) + " from new_acc_reg cross join librarysetupinformation where " + filterqry + (strUser == "" ? "" : " and (userId in(" + strUser + "))") + OorderBy;
                }
                else
                {
                    searchqry = "select new_acc_reg.accno,new_acc_reg.title,new_acc_reg.authortype,new_acc_reg.Dept,new_acc_reg.Pubname,new_acc_reg.bookprice,new_acc_reg.originalprice,new_acc_reg.originalcurrency,new_acc_reg.*,dbo.pleft1(accno) as a1, cast (dbo.pright1(accno) as numeric ) as a2,dbo.Suffix(accno) as suffix,institutename,libraryname,address,city,pincode,state,phoneno,fax,email,gram " + LibObj.logo(myConnection) + " from new_acc_regTHesis new_acc_reg cross join librarysetupinformation where " + filterqry + (strUser == "" ? "" : " and (userId in(" + strUser + "))") + OorderBy;
                }

            }
            try
            {
                if (this.optprePubwise.Checked == true)   // ////////////////////////////
                {

                    myConnection.Open();
                    if (this.RadioButtonList1.SelectedItem.Value == "Book")
                    {
                        if (!this.chkWritOff.Checked)
                        {
                            filterqry += " and new_acc_reg.wroffaccno='' ";
                        }
                        searchqry = "select new_acc_reg.accno,new_acc_reg.title,new_acc_reg.authortype,new_acc_reg.Dept,new_acc_reg.Pubname,new_acc_reg.bookprice,new_acc_reg.originalprice,new_acc_reg.originalcurrency,new_acc_reg.*,dbo.pleft1(accno) as a1, cast (dbo.pright1(accno) as numeric ) as a2,dbo.Suffix(accno) as suffix,institutename,libraryname,address,city,pincode,state,phoneno,fax,email,gram  " + LibObj.logo(myConnection) + "  from new_acc_reg cross join librarysetupinformation where " + filterqry + (strUser == ""? "": " and (userId in(" + strUser + "))") + OorderBy;
                    }
                    else
                    {
                        if (!this.chkWritOff.Checked)
                        {
                            filterqry += " and new_acc_reg.wroffaccno='' ";
                        }
                        searchqry = "select new_acc_reg.accno,new_acc_reg.title,new_acc_reg.authortype,new_acc_reg.Dept,new_acc_reg.Pubname,new_acc_reg.bookprice,new_acc_reg.originalprice,new_acc_reg.originalcurrency,new_acc_reg.*,dbo.pleft1(accno) as a1, cast (dbo.pright1(accno) as numeric ) as a2,dbo.Suffix(accno) as suffix,institutename,libraryname,address,city,pincode,state,phoneno,fax,email,gram  " + LibObj.logo(myConnection) + "  from new_acc_regTHesis new_acc_reg cross join librarysetupinformation where " + filterqry + (strUser == String.Empty ? "" : " and (userId in(" + strUser + "))") + OorderBy;
                    }

                    var da = new OleDbDataAdapter(searchqry, myConnection);
                    var ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        // Hidden1.Value = "Show"
                        // LibObj.MsgBox1(Resources.ValidationResources.NRecToPnt.ToString, Me)
                        //msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.NRecToPnt.ToString(), this);
                        message.PageMesg(Resources.ValidationResources.NRecToPnt.ToString(), this, dbUtilities.MsgLevel.Warning);
                        return;
                    }
                    var MyCommand = new OleDbCommand();
                    MyCommand.Connection = myConnection;
                    MyCommand.CommandTimeout = 1000;
                    MyCommand.CommandText = searchqry;
                    MyCommand.CommandType = CommandType.Text;
                    var MyDA = new OleDbDataAdapter();
                    MyDA.SelectCommand = MyCommand;
                    var myDS = new DataSet();// JournalFrequencyDataset();
                    MyDA.Fill(myDS, "ccp");

                    myDS.Tables["ccp"].Columns.Add("isbn");

                    int P = 0;
                    P = myDS.Tables["ccp"].Rows.Count;
                    var myReportDocument = new ReportDocument();
                    myReportDocument.Load(Server.MapPath(@"Reports\rptaccessionregisterPubwise.rpt"));
                    myReportDocument.SetDataSource(myDS.Tables["ccp"]);
                    // Field
                    // If a3.Checked = True Then
                    // myReportDocument.PrintOptions.PaperSize = PaperSize.PaperA4
                    // Else
                    // myReportDocument.PrintOptions.PaperSize = PaperSize.PaperA3
                    // End If

                    ((FieldObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["gram1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Bold));
                    ((FieldObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["email1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Bold));
                    ((FieldObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["fax1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Bold));
                    ((FieldObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["phoneno1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Bold));
                    ((FieldObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["libraryname1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Bold));

                    ((FieldObject)myReportDocument.ReportDefinition.Sections["GroupHeaderSection2"].ReportObjects["dept1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Bold));
                    ((FieldObject)myReportDocument.ReportDefinition.Sections["GroupHeaderSection2"].ReportObjects["isbn1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Bold));
                    ((FieldObject)myReportDocument.ReportDefinition.Sections["GroupFooterSection2"].ReportObjects["Countofaccno1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Bold));
                    ((FieldObject)myReportDocument.ReportDefinition.Sections["GroupFooterSection1"].ReportObjects["DistinctCountofBooktitle1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Bold));
                    ((FieldObject)myReportDocument.ReportDefinition.Sections["Section4"].ReportObjects["DistinctCountofBooktitle2"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Bold));
                    // Formula
                    ((FieldObject)myReportDocument.ReportDefinition.ReportObjects["rpheader1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 12.0f));
                    ((FieldObject)myReportDocument.ReportDefinition.ReportObjects["address1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 9.5f));
                    ((FieldObject)myReportDocument.ReportDefinition.ReportObjects["publisherformula1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 9.5f));
                    ((FieldObject)myReportDocument.ReportDefinition.ReportObjects["BookTitleVP1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 9.5f));
                    ((FieldObject)myReportDocument.ReportDefinition.ReportObjects["totcount1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 9.5f));
                    ((FieldObject)myReportDocument.ReportDefinition.ReportObjects["imp1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 9.5f));
                    // Textbox
                    ((TextObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["Text10"]).Text = Resources.ValidationResources.rptGram.ToString();
                    ((TextObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["Text10"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Regular));

                    ((TextObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["Text11"]).Text = Resources.ValidationResources.rptMail.ToString();
                    ((TextObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["Text11"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Regular));

                    ((TextObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["Text12"]).Text = Resources.ValidationResources.rptFax.ToString();
                    ((TextObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["Text12"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Regular));

                    ((TextObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["Text13"]).Text = Resources.ValidationResources.PhNo.ToString();
                    ((TextObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["Text13"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Regular));

                    ((TextObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["Text14"]).Text = this.lblTitle.Text;
                    ((TextObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["Text14"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Regular));

                    ((TextObject)myReportDocument.ReportDefinition.Sections["Section2"].ReportObjects["Text4"]).Text = Resources.ValidationResources.LPubN.ToString();
                    ((TextObject)myReportDocument.ReportDefinition.Sections["Section2"].ReportObjects["Text4"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Regular));

                    ((TextObject)myReportDocument.ReportDefinition.Sections["Section2"].ReportObjects["Text8"]).Text = Resources.ValidationResources.LTitle.ToString();
                    ((TextObject)myReportDocument.ReportDefinition.Sections["Section2"].ReportObjects["Text8"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Regular));

                    ((TextObject)myReportDocument.ReportDefinition.Sections["Section2"].ReportObjects["Text1"]).Text = Resources.ValidationResources.Title_department.ToString();
                    ((TextObject)myReportDocument.ReportDefinition.Sections["Section2"].ReportObjects["Text1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Regular));

                    ((TextObject)myReportDocument.ReportDefinition.Sections["Section2"].ReportObjects["Text16"]).Text = Resources.ValidationResources.LIsn.ToString();
                    ((TextObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["Text16"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Regular));

                    ((TextObject)myReportDocument.ReportDefinition.Sections["Section2"].ReportObjects["Text18"]).Text = Resources.ValidationResources.LQuantity.ToString();
                    ((TextObject)myReportDocument.ReportDefinition.Sections["Section2"].ReportObjects["Text18"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Regular));

                    ((TextObject)myReportDocument.ReportDefinition.Sections["Section4"].ReportObjects["Text2"]).Text = Resources.ValidationResources.LTotalRecordsitle.ToString();
                    ((TextObject)myReportDocument.ReportDefinition.Sections["Section4"].ReportObjects["Text2"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Regular));

                    ((TextObject)myReportDocument.ReportDefinition.Sections["Section4"].ReportObjects["Text3"]).Text = Resources.ValidationResources.rptTotalAccession.ToString();
                    ((TextObject)myReportDocument.ReportDefinition.Sections["Section4"].ReportObjects["Text3"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Regular));

                    ((TextObject)myReportDocument.ReportDefinition.Sections["Section4"].ReportObjects["Text7"]).Text = Resources.ValidationResources.LTotalRecordsw.ToString();
                    ((TextObject)myReportDocument.ReportDefinition.Sections["Section4"].ReportObjects["Text7"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Regular));




                    // craccessionreg.ReportSource = myReportDocument
                    // craccessionreg.DataBind()
                    // craccessionreg.RefreshReport()
                    var exportOpts1 = myReportDocument.ExportOptions;
                    Session["Rptdoc"] = myReportDocument;
                    myReportDocument.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    myReportDocument.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    myReportDocument.ExportOptions.DestinationOptions = new DiskFileDestinationOptions();
                    ((DiskFileDestinationOptions)myReportDocument.ExportOptions.DestinationOptions).DiskFileName = Server.MapPath(@"reportTemp\rptaccessionregisterPubwise.pdf");
                    myReportDocument.Export();
                    // craccessionreg.Visible = False
                    // LibObj.showPDF("rptaccessionregisterPubwise", "rptaccessionregisterPubwise", Me)
                    myReportDocument.Export();
                    myReportDocument.Close();
                    myReportDocument.Dispose();
                    Response.ClearContent();
                    Response.ClearHeaders();
                    // If rblOPt.SelectedValue = "pdf" Then
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("Content-Disposition", @"attachment; filename=reporttemp\rptaccessionregisterPubwise.pdf");
                    Response.WriteFile(@"reporttemp\rptaccessionregisterPubwise.pdf");
                    // End If
                    // If rblOPt.SelectedValue = "excel" Then
                    // Response.AppendHeader("Content-Disposition", "attachment; reporttemp\\filename=AttendenceRpt.xlsx")
                    // Response.AddHeader("Content-Type", "application/ms-excel")
                    // Response.ContentType = ("application/vnd.ms-excel")
                    // Response.WriteFile("reporttemp\AttendenceRpt.xlsx")

                    // test code for excel abhishek jaiswal
                    // Response.Clear()
                    // Response.Buffer = True
                    // Response.AppendHeader("Content-Disposition", "attachment; reporttemp\\filename=AttendenceRpt.xlsx")
                    // ' Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls")
                    // Response.Charset = ""
                    // Response.ContentType = "application/vnd.ms-excel"
                    // Response.WriteFile("reporttemp\AttendenceRpt.xlsx")

                    // End If
                    Response.End();
                    Response.Flush();
                    Response.Close();
                    // craccessionreg.Visible = False


                    da.Dispose();
                    ds.Dispose();
                }
                else
                {
                    myConnection.Open();
                    var orderrepds = new DataSet();
                    if (this.RadioButtonList1.SelectedItem.Value == "Book")
                    {
                        if (!this.chkWritOff.Checked)
                        {
                            filterqry += " and new_acc_reg.wroffaccno='' ";
                        }

                        // searchqry = "select * from (select new_acc_reg.*,dbo.pleft1(accno) as a1, cast (dbo.pright1(accno) as numeric ) as a2,dbo.Suffix(accno) as suffix,institutename,libraryname,address,city,pincode,state,phoneno,fax,email,gram " & LibObj.logo(myConnection) & " from new_acc_reg cross join librarysetupinformation where " & filterqry & IIf(strUser = String.Empty, "", " and (userId in(" & strUser & "))") & ") as new_acc_reg " & acno_Range_Filter & OorderBy
                        searchqry = "select * from (select new_acc_reg.*,dbo.pleft1(accno) as a1, cast (dbo.pright1(accno) as numeric ) as a2,dbo.Suffix(accno) as suffix,institutename,libraryname,address,city,pincode,state,phoneno,fax,email,gram " + LibObj.logo(myConnection) + " from new_acc_reg cross join librarysetupinformation where " + filterqry + (strUser == String.Empty? "": " and (userId in(" + strUser + "))") + ") as new_acc_reg " + OorderBy;
                    }
                    else
                    {

                        if (!this.chkWritOff.Checked)
                        {
                            filterqry += " and new_acc_reg.wroffaccno='' ";
                        }

                        // searchqry = "select * from (select new_acc_reg.*,dbo.pleft1(accno) as a1, cast (dbo.pright1(accno) as numeric ) as a2,dbo.Suffix(accno) as suffix,institutename,libraryname,address,city,pincode,state,phoneno,fax,email,gram " & LibObj.logo(myConnection) & " from new_acc_reg cross join librarysetupinformation where " & filterqry & IIf(strUser = String.Empty, "", " and (userId in(" & strUser & "))") & ") as new_acc_reg " & acno_Range_Filter & OorderBy
                        searchqry = "select * from (select new_acc_reg.*,dbo.pleft1(accno) as a1, cast (dbo.pright1(accno) as numeric ) as a2,dbo.Suffix(accno) as suffix,institutename,libraryname,address,city,pincode,state,phoneno,fax,email,gram " + LibObj.logo(myConnection) + " from new_acc_regTHesis new_acc_reg cross join librarysetupinformation where " + filterqry + (strUser == String.Empty? "": " and (userId in(" + strUser + "))") + ") as new_acc_reg " + OorderBy;
                    }

                    var orderrepda = new OleDbDataAdapter(searchqry, myConnection);
                    orderrepda.SelectCommand.CommandTimeout = 3500;
                    orderrepda.Fill(orderrepds, "orderrep");
                    var myReportDocument = new ReportDocument();
                    if (orderrepds.Tables[0].Rows.Count <= 0)
                    {
                        // LibObj.MsgBox(Resources.ValidationResources.MSGnorecordstodisplayreport.ToString, Me)
                        //                        msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Warning, Resources.ValidationResources.MSGnorecordstodisplayreport.ToString(), this);
                        message.PageMesg(Resources.ValidationResources.MSGnorecordstodisplayreport.ToString(), this, dbUtilities.MsgLevel.Warning);
                        return;
                    }
                    else
                    {
                        FormulaFieldDefinition thisFormulaField;
                        int icount = 1;
                        myReportDocument.Load(Server.MapPath(@"Reports\PreCatalogUserWiseRegister1.rpt"));
                        myReportDocument.SetDataSource(orderrepds.Tables["orderrep"]);
                        if (!(this.optPreAccNo.Checked == true | this.optprePubwise.Checked == true))
                        {
                            foreach (GroupNameFieldDefinition thisFormulaField1 in myReportDocument.DataDefinition.GroupNameFields)
                            {
                                foreach (FormulaFieldDefinition currentThisFormulaField in myReportDocument.DataDefinition.FormulaFields)
                                {
                                    thisFormulaField = currentThisFormulaField;
                                    if (thisFormulaField1.GroupNameFieldName == "Group #1 Name: @gp1")
                                    {
                                        if (thisFormulaField.FormulaName == "{@gp1}")
                                        {
                                            if (this.optPreDept.Checked == true)
                                            {
                                                thisFormulaField.Text = "{new_acc_reg.Dept}";
                                            }
                                            else if (this.OptpreUserWise.Checked == true)
                                            {
                                                thisFormulaField.Text = "{new_acc_reg.UserId}";
                                            }
                                            else if (this.RBBill.Checked == true)
                                            {
                                                thisFormulaField.Text = "{new_acc_reg.biilno}";
                                            }
                                        }
                                        if (thisFormulaField.FormulaName == "{@gp1T}")
                                        {
                                            if (this.optPreDept.Checked == true)
                                            {
                                                thisFormulaField.Text = "'Department Name'";
                                            }
                                            else if (this.OptpreUserWise.Checked == true)
                                            {
                                                thisFormulaField.Text = "'User Name'";
                                            }
                                            else if (this.RBBill.Checked == true)
                                            {
                                                thisFormulaField.Text = "'Bill No'";
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (!(this.optprePubwise.Checked == true))
                        {
                            var loopTo2 = arr.Count;
                            for (icount = 1; icount <= loopTo2; icount++)
                            {
                                foreach (FormulaFieldDefinition currentThisFormulaField1 in myReportDocument.DataDefinition.FormulaFields)
                                {
                                    thisFormulaField = currentThisFormulaField1;
                                    if ((thisFormulaField.FormulaName ?? "") == ("{@Formula" + icount + "}" ?? ""))
                                    {
                                        if (arr[icount - 1].ToString() == "new_acc_reg.volumeno,new_acc_reg.Vpart")
                                        {
                                            thisFormulaField.Text = "{@volpart}";
                                        }
                                        else if (arr[icount - 1].ToString() == "new_acc_reg.accdt")
                                        {
                                            thisFormulaField.Text = "{@dt1}";
                                        }
                                        else if (arr[icount - 1].ToString() == "new_acc_reg.dateofarrival") // accRegister1.Firstname
                                        {
                                            thisFormulaField.Text = "{@dt2}";
                                        }
                                        else if (arr[icount - 1].ToString() == "new_acc_reg.authortype")
                                        {
                                            thisFormulaField.Text = "{@AuthorName}"; // & arr(icount - 1) & "," & "' & city  & '" & "}" '& "," & "{" & "' & city  & '" & "}"
                                        }
                                        else if (arr[icount - 1].ToString() == "new_acc_reg.Pubname")
                                        {
                                            thisFormulaField.Text = "{@Publisher}";
                                        }
                                        else if (arr[icount - 1].ToString() == "new_acc_reg.Item_type")
                                        {
                                            thisFormulaField.Text = "{@Itemtype}";
                                        }
                                        else if (arr[icount - 1].ToString() == "new_acc_reg.bookprice")
                                        {
                                            thisFormulaField.Text = "{@bookprice}";
                                        }
                                        else if (arr[icount - 1].ToString() == "new_acc_reg.originalprice")
                                        {
                                            thisFormulaField.Text = "{@orgPrice}";
                                        }
                                        // ElseIf arr(icount - 1).ToString = "new_acc_reg.Docno" Then
                                        // thisFormulaField.Text = "{@orgPrice}"
                                        else
                                        {
                                            thisFormulaField.Text = "{" + arr[icount - 1] + "}";
                                        }
                                    }
                                }
                            }
                            int icount1;
                            var loopTo3 = arr1.Count;
                            for (icount1 = 1; icount1 <= loopTo3; icount1++)
                            {
                                foreach (FormulaFieldDefinition currentThisFormulaField2 in myReportDocument.DataDefinition.FormulaFields)
                                {
                                    thisFormulaField = currentThisFormulaField2;
                                    if ((thisFormulaField.FormulaName ?? "") == ("{@ForH" + icount1 + "}" ?? ""))
                                    {
                                        thisFormulaField.Text = "'" + arr1[icount1 - 1] + "'";
                                    }
                                }
                            }
                        }
                    }
                    var orderrepcon1 = new OleDbConnection(retConstr(""));
                    orderrepcon1.Open();
                    var cmd = new OleDbCommand();
                    cmd.Connection = orderrepcon1;
                    int lef = 1500;
                    for (int k = 0, loopTo4 = this.ListBox1.Items.Count - 1; k <= loopTo4; k++)
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "Select SerialList  from UserwiseAccRegReport1 where ColNameHeader= '" + this.ListBox1.Items[k].Text + "' ";
                        string s = cmd.ExecuteScalar().ToString();
                        int widt = (int)Convert.ToInt64(s);
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "Select field_width  from UserwiseAccRegReport1 where SerialList= '" + widt + "'  ";
                        widt = (int)Convert.ToInt64(cmd.ExecuteScalar());
                        cmd.ExecuteNonQuery();
                        if (widt != 0)
                        {
                            if (k != 20)
                            {
                                // Head Column
                                ((FieldObject)myReportDocument.ReportDefinition.ReportObjects["ForH" + (k + 1) + "1"]).Width = widt;
                                ((FieldObject)myReportDocument.ReportDefinition.ReportObjects["ForH" + (k + 1) + "1"]).Left = lef;
                                // Records Column
                                ((FieldObject)myReportDocument.ReportDefinition.ReportObjects["Formula" + (k + 1) + "1"]).Width = widt;
                                ((FieldObject)myReportDocument.ReportDefinition.ReportObjects["Formula" + (k + 1) + "1"]).Left = lef;
                                ((FieldObject)myReportDocument.ReportDefinition.ReportObjects["Formula" + (k + 1) + "1"]).ApplyFont(new Font("Mangal", Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Regular));
                            }
                            lef = lef + widt;
                        }

                    } ((FieldObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["gram1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 8.5f));
                    ((FieldObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["email1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 8.5f));
                    ((FieldObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["fax1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 8.5f));
                    ((FieldObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["phoneno1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 8.5f));

                    ((FieldObject)myReportDocument.ReportDefinition.Sections["GroupHeaderSection1"].ReportObjects["GroupNamegp11"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Bold));
                    ((FieldObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["PageNofM1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 8.5f));
                    ((FieldObject)myReportDocument.ReportDefinition.Sections["Section3"].ReportObjects["RecordNumber1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), 8.5f));
                    // Formula
                    ((FieldObject)myReportDocument.ReportDefinition.ReportObjects["add1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Regular));
                    ((FieldObject)myReportDocument.ReportDefinition.ReportObjects["gp1T1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Regular));
                    ((FieldObject)myReportDocument.ReportDefinition.ReportObjects["totcount1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Regular));
                    ((FieldObject)myReportDocument.ReportDefinition.ReportObjects["imp1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Regular));

                    ((FieldObject)myReportDocument.ReportDefinition.ReportObjects["ForH01"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Bold));
                    ((FieldObject)myReportDocument.ReportDefinition.ReportObjects["ForH11"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Bold));
                    ((FieldObject)myReportDocument.ReportDefinition.ReportObjects["ForH21"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Bold));
                    ((FieldObject)myReportDocument.ReportDefinition.ReportObjects["ForH31"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Bold));
                    ((FieldObject)myReportDocument.ReportDefinition.ReportObjects["ForH41"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Bold));
                    ((FieldObject)myReportDocument.ReportDefinition.ReportObjects["ForH51"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Bold));
                    ((FieldObject)myReportDocument.ReportDefinition.ReportObjects["ForH61"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Bold));
                    ((FieldObject)myReportDocument.ReportDefinition.ReportObjects["ForH71"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Bold));
                    ((FieldObject)myReportDocument.ReportDefinition.ReportObjects["ForH81"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Bold));
                    ((FieldObject)myReportDocument.ReportDefinition.ReportObjects["ForH91"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Bold));
                    ((FieldObject)myReportDocument.ReportDefinition.ReportObjects["ForH101"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Bold));
                    ((FieldObject)myReportDocument.ReportDefinition.ReportObjects["ForH111"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Bold));
                    ((FieldObject)myReportDocument.ReportDefinition.ReportObjects["ForH121"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Bold));
                    ((FieldObject)myReportDocument.ReportDefinition.ReportObjects["ForH131"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Bold));
                    ((FieldObject)myReportDocument.ReportDefinition.ReportObjects["ForH141"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Bold));
                    ((FieldObject)myReportDocument.ReportDefinition.ReportObjects["ForH151"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Bold));
                    ((FieldObject)myReportDocument.ReportDefinition.ReportObjects["ForH161"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Bold));

                    ((TextObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["Text1"]).Text = Resources.ValidationResources.rptGram.ToString();
                    ((TextObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["Text1"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Regular));
                    ((TextObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["Text2"]).Text = Resources.ValidationResources.rptMail.ToString();
                    ((TextObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["Text2"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Regular));
                    ((TextObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["Text3"]).Text = Resources.ValidationResources.rptFax.ToString();
                    ((TextObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["Text3"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Regular));
                    ((TextObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["Text4"]).Text = Resources.ValidationResources.PhNo.ToString();
                    ((TextObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["Text4"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Regular));
                    ((TextObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["Text15"]).Text = this.lblTitle.Text;
                    ((TextObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["Text15"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Regular));
                    ((TextObject)myReportDocument.ReportDefinition.Sections["Section4"].ReportObjects["Text11"]).Text = Resources.ValidationResources.LTotalRecords.ToString();
                    ((TextObject)myReportDocument.ReportDefinition.Sections["Section4"].ReportObjects["Text11"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Regular));
                    ((TextObject)myReportDocument.ReportDefinition.Sections["Section4"].ReportObjects["Text12"]).Text = Resources.ValidationResources.LTotalRecordsw.ToString();
                    ((TextObject)myReportDocument.ReportDefinition.Sections["Section4"].ReportObjects["Text12"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Regular));

                    ((TextObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["Text7"]).Text = this.txtfromdate.Text;
                    ((TextObject)myReportDocument.ReportDefinition.Sections["Section1"].ReportObjects["Text9"]).Text = this.txttodate.Text;
                    if (prc == true)
                    {
                        ((TextObject)myReportDocument.ReportDefinition.Sections["Section4"].ReportObjects["Text17"]).Text = Resources.ValidationResources.LallTotal.ToString() + "(Price):";
                        ((TextObject)myReportDocument.ReportDefinition.Sections["Section4"].ReportObjects["Text17"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Regular));
                    }
                    else
                    {
                        ((TextObject)myReportDocument.ReportDefinition.Sections["Section4"].ReportObjects["Text17"]).Text = "";
                        ((FieldObject)myReportDocument.ReportDefinition.ReportObjects["TotalAmount1"]).Width = 0;
                    }
                    if (orgprc == true)
                    {
                        ((TextObject)myReportDocument.ReportDefinition.Sections["Section4"].ReportObjects["Text6"]).Text = Resources.ValidationResources.LallTotal.ToString() + "(Org.Price):";
                        ((TextObject)myReportDocument.ReportDefinition.Sections["Section4"].ReportObjects["Text6"]).ApplyFont(new Font(Resources.ValidationResources.TextBox1.ToString(), Convert.ToSingle(this.ddlsize.SelectedValue), FontStyle.Regular));
                    }
                    else
                    {
                        ((TextObject)myReportDocument.ReportDefinition.Sections["Section4"].ReportObjects["Text6"]).Text = "";
                        ((FieldObject)myReportDocument.ReportDefinition.ReportObjects["totorgprice1"]).Width = 0;
                    }

                    // craccessionreg.ReportSource = myReportDocument
                    // craccessionreg.DataBind()
                    // craccessionreg.RefreshReport()
                    // myReportDocument.PrintOptions.PaperSize = PaperSize.PaperLedger
                    Session["Rptdoc"] = myReportDocument;
                    var exportOpts1 = myReportDocument.ExportOptions;
                    myReportDocument.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    myReportDocument.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    myReportDocument.ExportOptions.DestinationOptions = new DiskFileDestinationOptions();
                    ((DiskFileDestinationOptions)myReportDocument.ExportOptions.DestinationOptions).DiskFileName = Server.MapPath(@"reportTemp\PreCatalogUserWiseRegister1.pdf");
                    myReportDocument.Export();
                    // craccessionreg.Visible = False
                    // craccessionreg.Visible = False
                    // LibObj.showPDF("rptaccessionregisterPubwise", "rptaccessionregisterPubwise", Me)
                    // myReportDocument.Export()
                    myReportDocument.Close();
                    myReportDocument.Dispose();
                    Response.ClearContent();
                    Response.ClearHeaders();
                    // If rblOPt.SelectedValue = "pdf" Then
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("Content-Disposition", @"attachment; filename=reporttemp\PreCatalogUserWiseRegister1.pdf");
                    Response.WriteFile(@"reporttemp\PreCatalogUserWiseRegister1.pdf");
                    // End If
                    // If rblOPt.SelectedValue = "excel" Then
                    // Response.AppendHeader("Content-Disposition", "attachment; reporttemp\\filename=AttendenceRpt.xlsx")
                    // Response.AddHeader("Content-Type", "application/ms-excel")
                    // Response.ContentType = ("application/vnd.ms-excel")
                    // Response.WriteFile("reporttemp\AttendenceRpt.xlsx")

                    // test code for excel abhishek jaiswal
                    // Response.Clear()
                    // Response.Buffer = True
                    // Response.AppendHeader("Content-Disposition", "attachment; reporttemp\\filename=AttendenceRpt.xlsx")
                    // ' Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls")
                    // Response.Charset = ""
                    // Response.ContentType = "application/vnd.ms-excel"
                    // Response.WriteFile("reporttemp\AttendenceRpt.xlsx")

                    // End If
                    Response.End();
                    Response.Flush();
                    Response.Close();
                    // craccessionreg.Visible = False

                    orderrepda.Dispose();
                    orderrepds.Dispose();


                }
            }


            // Field
            // If ListBox1.Items.Count < 8 = True Then
            // myReportDocument.PrintOptions.PaperSize = PaperSize.PaperA4
            // ElseIf ListBox1.Items.Count >= 8 And ListBox1.Items.Count < 14 Then
            // myReportDocument.PrintOptions.PaperSize = PaperSize.PaperA4
            // Else
            // myReportDocument.PrintOptions.PaperSize = PaperSize.PaperA4
            // End If

            // fields
            // Kaushal -Add Dynamic width of Selected Listbox items


            catch (Exception ex)
            {
                this.msglabel.Visible = true;
                // LibObj.MsgBox1(ex.Message, Me)
                // LibObj.MsgBox1("Data is going too much, so pls enter a short date range period", Me)
                // SetFocus("txttodate")
                this.msglabel.Text = ex.Message;
            }
            finally
            {
                myConnection.Close();
                myConnection.Dispose();

            }

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            return;
        }

        protected void chkreport_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.chkreport.Items[0].Selected == true & this.chkreport.Items[1].Selected == false)
            {
                Session["Filtersuggest"] = "Acc";
            }
            else if (this.chkreport.Items[0].Selected == false & this.chkreport.Items[1].Selected == true)
            {
                Session["Filtersuggest"] = "AccNO";
            }
            else
            {
                Session["Filtersuggest"] = "AccBoth";
            }
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            var indentcon = new OleDbConnection(retConstr(""));
            indentcon.Open();
            if (this.pnl.Visible == false)
            {
                this.pnl.Visible = true;
            }
            else
            {
                this.pnl.Visible = false;
                return;
            }
            var da = new OleDbDataAdapter("select ColNameHeader ,field_width from UserwiseAccRegReport1", indentcon);
            var dsw = new DataSet();
            da.Fill(dsw);
            if (dsw.Tables[0].Rows.Count > 0)
            {
                this.grdfield.DataSource = dsw.Tables[0];
                this.grdfield.DataBind();

                for (int k = 0, loopTo = dsw.Tables[0].Rows.Count - 1; k <= loopTo; k++)
                {
                    TextBox txt = (TextBox)this.grdfield.Items[k].FindControl("txtwidth");
                    txt.Text = dsw.Tables[0].Rows[k][1].ToString();
                }
            }
            else
            {
                var dt = new DataTable();
                this.grdfield.DataSource = null;
                this.grdfield.DataBind();
            }

        }

        protected void cmdupdatewidth_Click(object sender, EventArgs e)
        {
            try
            {


                var indentcon = new OleDbConnection(retConstr(""));
                indentcon.Open();
                // Dim da As OleDbDataAdapter = New OleDbDataAdapter("select ColNameHeader ,field_width from UseAccRegReport", indentcon)
                // Dim dsw As DataSet = New DataSet()
                // da.Fill(dsw)
                // If dsw.Tables(0).Rows.Count > 0 Then
                var cmd = new OleDbCommand();
                cmd.Connection = indentcon;
                for (int k = 0, loopTo = this.grdfield.Items.Count - 1; k <= loopTo; k++)
                {
                    TextBox txt = (TextBox)this.grdfield.Items[k].FindControl("txtwidth");
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "Update UserwiseAccRegReport1 set field_width='" + txt.Text + "' where ColNameHeader ='" + this.grdfield.Items[k].Cells[0].Text + "'";
                    cmd.ExecuteNonQuery();
                }
                // End If
                // LibObj.MsgBox1("Column Width Updated Successfully!", Me)
                // msgLibrary.showHtml_Message(Library.messageLibrary.msgType.Success, "Column Width Updated Successfully!", this);
                message.PageMesg("Column Width Updated Successfully!", this);
                this.pnl.Visible = false;
            }
            catch (Exception ex)
            {

            }
        }

        protected void cmdShow_Click(object sender, EventArgs e)
        {
            cmdPrint_ServerClick(sender, e);
        }

        protected void cmdreset2_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.ToString());
        }
    }
}