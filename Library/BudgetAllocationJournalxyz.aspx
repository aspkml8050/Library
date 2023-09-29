<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BudgetAllocationJournalxyz.aspx.cs"  MasterPageFile="~/LibraryMain.master" Inherits="Library.BudgetAllocationJournalxyz" %>

<%@ Register TagName="Mak" TagPrefix="NM" Src="~/ProgressControl.ascx" %>


<asp:Content ID="budgSerHead" runat="server" ContentPlaceHolderID="head">
    
        <link href="cssDesign/libresponsive.css" rel="stylesheet"
type="text/css" /> 
</asp:Content>

<asp:Content ID="budgSerMain" runat="server" ContentPlaceHolderID="MainContent">
        <asp:UpdateProgress ID="UpPorg1" runat="server">
            <ProgressTemplate>
                <NM:Mak ID="FF1" runat="server" />
            </ProgressTemplate>
        </asp:UpdateProgress>

      
         
              <div style="width:100%;display:none" class="title"> &nbsp;
                  <div style="width:89%;float:left" >
                        <asp:Label ID="lbltitle" runat="server" style="display:none" Width="100%"></asp:Label>
                      </div>
                   <div style="float:right;vertical-align:top">
                        <a id="lnkHelp" href="#" style="display:none"  onclick="ShowHelp('Help/Masters-allocation(journals).htm')">
                            <img height="15" alt="Help" src="help.jpg" /></a>
                       </div></div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="BtnPrint" />
                                </Triggers>
                                <ContentTemplate>
                                   <div class="container tableborderst">   
                                   
                                        <table id="Table1" class="table-condensed no-more-tables GenTable1" style="width:100%;">
                                            <tr>
                                                <td style="width:12%"></td>
                                                <td colspan="2">
                                                    <asp:Label ID="msglabel" runat="server"  CssClass="err"></asp:Label>  
                                          
                                                    <asp:Label ID="Label1" CssClass="head1" runat="server"  Text="<%$ Resources:ValidationResources, LCurrSess %>"> </asp:Label></td>
                                                <td style="width:12%"></td>
                                                </tr>
                                            <tr>
                                                <td></td>
                                                <td>
                                            <asp:HiddenField ID="hidDeptID" runat="server" />
                                                    <asp:Label ID="lblDepartment" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources, LDeptm %>"></asp:Label></td>
                                                <td>
                                                    <asp:DropDownList ID="cmbdepartment" onblur="this.className='blur'" onfocus="this.className='focus'" onchange="GetServer(this.id,'txtallocatedamount')"
                                                        runat="server" CssClass="txt10" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'>
                                                    </asp:DropDownList>
                                                    <asp:Label ID="Label3" runat="server" CssClass="star">*</asp:Label></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td><asp:Label ID="lblAllocatedAmount" runat="server"  CssClass="span" Text="<%$ Resources:ValidationResources, AlloAmt %>"> </asp:Label></td>
                                                <td>
                                                    <asp:TextBox ID="txtallocatedamount" onkeypress="decimalNumber(this);" onblur="this.className='blur'"
                                                        onfocus="this.className='focus'" runat="server"  CssClass="txt10"
                                                        BorderWidth="1px" MaxLength="15"></asp:TextBox></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td ><asp:Label ID="lblStatus" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources, St_ts %>"></asp:Label></td>
                                                <td >
                                                    <asp:DropDownList ID="cmbstatus" onblur="this.className='blur'" onfocus="this.className='focus'" runat="server"
                                                      CssClass="txt10" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'>
                                                        <asp:ListItem Value="Not Allowed" Text="<%$ Resources:ValidationResources, LINAllwed %>"> </asp:ListItem>
                                                        <asp:ListItem Value="Allowed" Text="<%$ Resources:ValidationResources, LIAlowd %>"> </asp:ListItem>
                                                    </asp:DropDownList></td>
                                                <td></td>
                                            </tr>
                                              <tr>
                                                  <td></td>
                                        <td>
                                        <asp:label id="lable7" runat="server" CssClass="span " Text ="Item Category"> </asp:label></TD>
                                        <td>
                                            <asp:DropDownList ID ="itemC" runat="server" onblur="this.className='blur'" Height="30" CssClass="txt10"  onfocus="this.className='focus'" ></asp:DropDownList>
                                        </td>
                                                  <td></td>
                                    </tr>
                                            <tr>
                                              
                                                <td colspan="4" style="text-align:center">
                                                    
                                                                <%--<input id="cmdsave"  type="button" value="<%$ Resources:ValidationResources, bSave %>" name="cmdsave"
                                                                    runat="server" accesskey="S" class="btnstyle">--%>
                       <asp:Button Id="cmdsave" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="cmdsave_Click"/>                                     
                                                                <%--<input id="cmdreset"  type="button" value="<%$ Resources:ValidationResources, bReset %>" name="cmdreset"
                                                                    runat="server" accesskey="E" class="btnstyle">--%>
                        <asp:Button Id="cmdreset" runat="server" Text="Reset" CssClass="btn btn-primary" OnClick="cmdreset_Click"/>                                  
                                                                <%--<input id="cmddelete"  type="button" value="<%$ Resources:ValidationResources, bDelete %>" onclick="if (DoConfirmation() == false) return false;"
                                                                    name="cmddelete" runat="server" accesskey="X" class="btnstyle">--%>
                        <asp:Button Id="cmddelete" runat="server" Text="Delete" CssClass="btn btn-primary" OnClick="cmddelete_Click"/>                                   
                                                                <asp:Button ID="BtnPrint" runat="server" CssClass="btn btn-primary" Text="Print" Enabled="false" OnClick="BtnPrint_Click"/>
                                                           
                                                </td>
                                            </tr>
                                            <tr style="display:none">
                                                <td></td>
                                                <td colspan="3"><asp:Label ID="Label14" runat="server" CssClass="head1"  Text="<%$ Resources:ValidationResources, LBudDetail %>"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td colspan="3" >
                                                    
                                                    <asp:Label ID="lblCurrency" runat="server" Text="<%$ Resources:ValidationResources, LAllAmt %>" CssClass="Note" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></asp:Label></td>
                                            </tr>
                                        </table>
                              
                                   
                                           <div class="allgriddiv" style="max-height:450px" id="dvgrd" runat="server">                                  
                    <asp:HiddenField runat="server" ID="hdnGrdId" />
          <asp:DataGrid ID="grddetail" runat="server" CssClass="allgrid GenTable1" Width="100%" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>' OnItemCommand="grddetail_ItemCommand">

                                                        <Columns>
                                                            <asp:ButtonColumn DataTextField="departmentname" HeaderText="<%$ Resources:ValidationResources, LDeptm %>"
                                                                CommandName="Select" ></asp:ButtonColumn>
                                                            <asp:BoundColumn Visible="False" DataField="departmentcode"  HeaderText="<%$ Resources:ValidationResources, GrDeptCode %>"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="allocated_amount" HeaderText="<%$ Resources:ValidationResources, GrAlloAmt %>"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="expended_amount" HeaderText="<%$ Resources:ValidationResources, GrExpendedAmt %>"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="committed_amount" HeaderText="<%$ Resources:ValidationResources, GrCommitAmount %>"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="balance" HeaderText="<%$ Resources:ValidationResources, GrBal %>"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="status" HeaderText="<%$ Resources:ValidationResources, LStatus %>"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="Category_LoadingStatus" HeaderText="Item Category"></asp:BoundColumn>
                                                        </Columns>
                                                    </asp:DataGrid>

                 </div>

                                     
                                   </div>
                                      <div style="display:none">
                                           <input id="xCoordHolder" style="WIDTH: 1px; HEIGHT: 22px" type="hidden" size="1" value="0"
                                                name="xCoordHolder" runat="server">
                                    <%--        <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="<%$ Resources:ValidationResources, IvDep%>" ClientValidationFunction="comboValidation"
                                                Display="None" ControlToValidate="cmbdepartment" SetFocusOnError="True"></asp:CustomValidator>
                                   --%> <input id="Hidden2" style="WIDTH: 16px; HEIGHT: 22px" type="hidden" size="1" name="Hidden2"
                                                    runat="server"><input id="Hidden1" style="WIDTH: 8px; HEIGHT: 22px" type="hidden" size="1" name="Hidden1"
                                                        runat="server"><input id="yCoordHolder" style="WIDTH: 8px; HEIGHT: 22px" type="hidden" size="1" value="0"
                                                            name="yCoordHolder" runat="server" designtimedragdrop="64">
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" Width="134px" DisplayMode="List" ShowSummary="False"
                                                ShowMessageBox="True"></asp:ValidationSummary>
                                            <input id="HComboSelect" runat="server"
                                                type="hidden" value="<%$ Resources:ValidationResources, ComboSelect %>" />
                                            <p id="txtdeptcode">
                                                <input id="Hidden11" type="hidden" runat="server"><input id="hdUnableMsg" type="hidden" name="hdUnableMsg" runat="server">
                                                <input id="Hidden3" runat="server" name="Hidden3" style="width: 19px" type="hidden" />
                                                <input id="txtdepartmentcode" style="width: 24px" type="hidden" runat="server" /> 
                                                </div>
                                </ContentTemplate>
                                <%--<Triggers>
                                    <asp:PostBackTrigger ControlID="BtnPrint" />
                                </Triggers>--%>
                            </asp:UpdatePanel>
            
             
    <script type="text/javascript">
        //On Page Load.
        $(function () {
            ForDataTable();
        });
    //On UpdatePanel Refresh.
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    if (prm != null) {
        prm.add_endRequest(function (sender, e) {
            if (sender._postBackSettings.panelsToUpdate != null) {
                ForDataTable();
            }
        });
    };
    function ForDataTable() {
            try {
                var grdId = $("[id$=hdnGrdId]").val();
                //alert(grdId);
                $('<thead></thead>').prependTo('#' + grdId).append($('#' + grdId + ' tr:first'));
                ThreeLevelSearch($('#'+ grdId), "[id$=dvgrd]");
            }
            catch (err) {
            }
        }

    
    </script>
</asp:Content>



