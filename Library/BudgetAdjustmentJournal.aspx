<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BudgetAdjustmentJournal.aspx.cs"  MasterPageFile="~/LibraryMain.master" Inherits="Library.BudgetAdjustmentJournal" %>
<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>


<asp:Content ID="jAdjHead" runat="server" ContentPlaceHolderID="head">


</asp:Content>

<asp:Content ID="jAdjMain" runat="server" ContentPlaceHolderID="MainContent">
     <asp:UpdateProgress ID="UpPorg1" runat="server">
    <ProgressTemplate>
    <NN:Mak ID="FF1" runat="server" />
    </ProgressTemplate>
    </asp:UpdateProgress>
            <div class="container tableborderst" >   
       
              <div style="width:100%;display:none" class="title">
                  <div style="width:89%;float:left" > &nbsp;
                             
                                          <asp:label id="lblTitle" style="display:none" runat="server"  Width="100%">Budget Information</asp:label><br />
                                          </div>
                   <div style="float:right;vertical-align:top">
                      <a id="lnkHelp" href="#" style="display:none"  onclick="ShowHelp('Help/Masters-adjustments(journals).htm')"><img height="15" src="help.jpg"  /></a>
			           </div>       </div> 
                       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                      <ContentTemplate>     
                                          <div class="no-more-tables" style="width:100%">
                                          <table id="Table1" class="table-condensed GenTable1" style="width:100%;">
                                              <tr>
                                                
                                                  <td colspan="4">
                                                      <asp:Label ID="msglabel" runat="server" CssClass="err"></asp:Label>
                                                      
                                                  <asp:Label ID="Label3" runat="server" CssClass="head1" Text="<%$ Resources:ValidationResources, LCurrSess %>"> </asp:Label></td>
                                                 
                                                  
                                              </tr>
                                              <tr>
                                                
                                                  <td>

                                                      <asp:Label ID="Label2" runat="server" CssClass="span "
                                                          Text="<%$ Resources:ValidationResources, LDate %>"></asp:Label></td>
                                                  <td>



                                                      
                                                      <asp:TextBox ID="txtdate" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                                     

                                                      <asp:Label ID="Label10" runat="server" CssClass="star">*</asp:Label>

                                                  </td>
                                                
                                             
                                                  <td>
                                                      <%--<asp:Label ID="Label1" runat="server" CssClass="span " Text="<%$ Resources:ValidationResources, LDeptm %>"></asp:Label>--%>
                                                      <asp:Label ID="Label1" runat="server" CssClass="span " Text="Department"></asp:Label>
                                                  </td>
                                                  <td>
                                                      <asp:DropDownList ID="cmbdept" Height="30" onblur="this.className='blur'" onfocus="this.className='focus'" runat="server" onchange="GetServer(this.id,'txtallocatedamount')"
                                                          CssClass="txt10" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'>
                                                      </asp:DropDownList><asp:Label ID="Label17" runat="server" CssClass="star">*</asp:Label></td>
                                                  
                                              </tr>
                                              <tr>
                                                
                                                  <td>

                                                      <asp:Label ID="Label7" runat="server" CssClass="span " Text="<%$ Resources:ValidationResources, CurrAllotAmt %>"></asp:Label></td>
                                                  <td>
                                                      <input class="txt10" id="txtallocatedamount" onblur="this.className='blur'"
                                                          onfocus="this.className='focus'" type="text" name="txtallocatedamount" runat="server" size="15" readonly="readOnly">
                                                      <asp:Label ID="Label8" runat="server" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></asp:Label></td>
                                                
                                                  <td>

                                                      <asp:Label ID="Label5" runat="server" CssClass="span " Text="<%$ Resources:ValidationResources, LCurrBal %>"></asp:Label></td>
                                                  <td>
                                                      <input class="txt10" id="txtbalance" onblur="this.className='blur'"
                                                          onfocus="this.className='focus'" type="text" name="txtbalance" runat="server" size="15" readonly="readOnly">
                                                      <asp:Label ID="Label9" runat="server" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></asp:Label></td>
                                                 
                                              </tr>
                                              <tr>
                                                
                                                  <td>

                                                      <asp:Label ID="Label4" runat="server" CssClass="span " Text="<%$ Resources:ValidationResources, LOper %>"></asp:Label></td>
                                                  <td>
                                                      <asp:RadioButton ID="optalc" runat="server" Checked="True" CssClass="opt"
                                                          GroupName="s" Text="<%$ Resources:ValidationResources, RBAll %>" />&nbsp;
                                            <asp:RadioButton ID="optdealc" runat="server" CssClass="opt"
                                                GroupName="s" Text="<%$ Resources:ValidationResources, RBDeAll %>" /></td>
                                                 
                                                  <td>

                                                      <asp:Label ID="Label11" runat="server" CssClass="span " Text="<%$ Resources:ValidationResources, NewAddAmt %>"></asp:Label></td>
                                                  <td>
                                                      <input class="txt10" id="txtamount" onblur="this.className='blur'" onkeypress="decimalNumber(this);"
                                                          onfocus="this.className='focus'" type="text" name="txtamount" runat="server" size="15" maxlength="15"><asp:Label ID="Label6" runat="server" CssClass="star">*</asp:Label><asp:Label ID="lblCurrency" runat="server" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></asp:Label></td>
                                                 
                                              </tr>

                                              <tr>

                                                  <td colspan="4" style="text-align: center">
                                                      <%--<input id="cmdsave" type="button" value="<%$ Resources:ValidationResources, bSave %>" name="cmdsave"
                                                          runat="server" accesskey="S" class="btnstyle">--%>
<asp:Button Id="cmdsave" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="cmdsave_Click"/>
                                                     <%-- <input id="cmdreset" type="button" value="<%$ Resources:ValidationResources, bReset %>" name="cmdreset"
                                                          runat="server" accesskey="E" class="btnstyle">--%>
                                                      <asp:Button ID="cmdreset" runat="server" Text="Reset" CssClass="btn btn-primary" OnClick="cmdreset_Click"/>
                                                  </td>
                                              </tr>

                                              
                                              <tr>
                                                 <td>
                                                     <asp:CheckBox ID="chkSearch" runat="server" OnCheckedChanged="chkSearch_CheckedChanged" AutoPostBack="True" CssClass="opt" Text="<%$ Resources:ValidationResources, bSearch %>" />

                                                  </td>
                                                  <td style="text-align:center">
                                                    <asp:Label ID="lbldeptsearch" runat="server" CssClass="span " Text="<%$ Resources:ValidationResources, LDeptm %>"></asp:Label></td>
                                                  <td colspan="2">
                                                      <asp:DropDownList ID="cmddeptsearch" Height="30"  onblur="this.className='blur'" onfocus="this.className='focus'" runat="server"
                                                          OnSelectedIndexChanged="cmddeptsearch_SelectedIndexChanged"
                                                          CssClass="txt10" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>' AutoPostBack="True">
                                                      </asp:DropDownList></td>
                                                 
                                              </tr>



                                          </table> 
                                              </div>

                                           <div id="dvgrd" runat="server" class="allgriddiv">
                                                 <asp:HiddenField runat="server" ID="hdnGrdId" />
                                            <asp:DataGrid ID="DataGrid1" runat="server" CssClass="allgrid GenTable1" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'>
                                              <Columns>
                                                    <asp:BoundColumn DataField="Date" HeaderText="<%$ Resources:ValidationResources, LDate %>" DataFormatString="<%$ Resources:ValidationResources, GridDateF %>"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="Operation" HeaderText="<%$ Resources:ValidationResources, LOper %>"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="Amount" HeaderText="<%$ Resources:ValidationResources, LAmt %>"></asp:BoundColumn>
                                                </Columns>
                                            </asp:DataGrid>
                   </div>
								                    <INPUT id="hdTop" type="hidden" runat="server" style="width: 40px">
                                                      <input id="hdCulture" runat="server" style="width: 27px" type="hidden" />
                                                      <input id="hrDate" runat="server" type="hidden" value="<%$ Resources:ValidationResources, dateFormat1 %>" style="width: 42px" />
                                                      <input id="HComboSelect" runat="server" type="hidden" value="<%$ Resources:ValidationResources, ComboSelect %>" style="width: 56px" /><input id="js1" runat="server" type="hidden" value="<%$ Resources:ValidationResources, js1 %>" style="width: 47px" /><input id="Hidden3" runat="server" name="Hidden3" style="width: 15px" type="hidden" /><INPUT id="xCoordHolder" style="WIDTH: 8px; HEIGHT: 12px" type="hidden" size="1" value="0" name="xCoordHolder" runat="server"><INPUT id="yCoordHolder" style="WIDTH: 29px; HEIGHT: 16px" type="hidden" size="1" value="0" name="yCoordHolder" runat="server"><INPUT id="Hidden4" style="WIDTH: 16px; HEIGHT: 22px" type="hidden" size="1" name="Hidden1" runat="server"><INPUT id="Hidden5" style="WIDTH: 16px; HEIGHT: 22px" type="hidden" size="1" name="Hidden2" runat="server"><INPUT id="Hidden1" type="hidden" name="Hidden1" runat="server" style="width: 29px"><INPUT id="Hidden7" type="hidden" runat="server" style="width: 26px">
					
                            <asp:RequiredFieldValidator ID="datevalidator" runat="server" ControlToValidate="txtdate"
                                Display="None" ErrorMessage="<%$ Resources:ValidationResources, EnterDate%>" Font-Names="Arial" Font-Size="11px"
                                SetFocusOnError="True"></asp:RequiredFieldValidator>
<%--                            <asp:customvalidator id="CustomValidator1" runat="server" Font-Size="11px" Display="None" ClientValidationFunction="comboValidation"
								ControlToValidate="cmbdept" ErrorMessage="<%$ Resources:ValidationResources, IvDep%>" SetFocusOnError="True" Font-Names="arial"></asp:customvalidator>&nbsp;--%>
                            <asp:RequiredFieldValidator ID="amountvalidator" runat="server" ControlToValidate="txtamount"
                                Display="None" ErrorMessage="<%$ Resources:ValidationResources, ReqEnterNewAddAmt%>" Font-Names="Arial" Font-Size="11px"
                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:validationsummary id="ValidationSummary1" runat="server" Width="134px" Font-Size="11px" ShowSummary="False"
								ShowMessageBox="True" DisplayMode="List"></asp:validationsummary>        
					</ContentTemplate>
                                  </asp:UpdatePanel>
                  </div>
<script type="text/javascript">
    //On Page Load.
    $(function () {
        SetDatePicker();
        ForDataTable();
    });

    //On UpdatePanel Refresh.
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    if (prm != null) {
        prm.add_endRequest(function (sender, e) {
            if (sender._postBackSettings.panelsToUpdate != null) {
                SetDatePicker();
                ForDataTable();
            }
        });
    };

    function ForDataTable() {
        try {
            var grdId = $("[id$=hdnGrdId]").val();
            //alert(grdId);
            $('<thead></thead>').prependTo('#' + grdId).append($('#' + grdId + ' tr:first'));
            ThreeLevelSearch($('#' + grdId), "[id$=dvgrd]");
        }
        catch (err) {
        }
    }
    function SetDatePicker() {

        $("[id$=txtdate]").datepicker({
            changeMonth: true,//this option for allowing user to select month
            changeYear: true, //this option for allowing user to select from year range
            dateFormat: 'dd-M-yy'  // CHANGE DATE FORMAT.
        });
        $('#txtdate').click(function () {
            $('#txtdate').datepicker('show');
        });
        //$('#ui-datepicker-div').show();
        //$('#txtDateFrom').datepicker('show');
    }

</script>	         

</asp:Content>