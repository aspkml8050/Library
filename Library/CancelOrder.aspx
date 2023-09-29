<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/LibraryMain.master" CodeBehind="CancelOrder.aspx.cs" Inherits="Library.CancelOrder" %>
<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>
<asp:Content ID="CancHead" runat="server" ContentPlaceHolderID="head">
 <link href="cssDesign/libresponsive.css" rel="stylesheet" type="text/css" >

</asp:Content>
<asp:Content ID="CancMain" runat="server" ContentPlaceHolderID="MainContent">

    <link href="cssDesign/multiselect.css" rel="stylesheet" type="text/css" >
    <script src="FormScripts/multiselect.js"></script>
      <asp:UpdateProgress ID="UpPorg1" runat="server">
    <ProgressTemplate>
    <NN:Mak ID="FF1" runat="server" />
    </ProgressTemplate>
    </asp:UpdateProgress>
     <div class="container tableborderst">   
        
              <div style="width:100%;display:none" class="title">
                  <div style="width:89%;float:left" >&nbsp;
                      <asp:label id="lblTitle" style="display:none" runat="server" Width="100%">Order/Indent Cancellation</asp:label>
                      </div>
                    <div style="float:right;vertical-align:top">
                        <a id="lnkHelp" href="#"  style="display:none"  onclick="ShowHelp('Help/Acq-Order-Cancel.htm')"><img src="help.jpg" alt="Help" style="height: 16px" /></a>
               </div></div>
         
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                     <div class="no-more-tables" style="width:100%">
                                         <table id="Table2" class="table-condensed" style="width:100%" >
                                             <tr>
                                                 <td colspan="4">
                                                     <asp:Label ID="msglabel" runat="server" CssClass="err"></asp:Label></td>
                                             </tr>
                                             <tr>
                                                 <td style="width:24%">
                                                     <asp:Label ID="Label1" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LVen %>"></asp:Label></td>
                                                 <td colspan="3">
                                                     <asp:DropDownList ID="cmbvendor" onblur="this.className='blur'" onfocus="this.className='focus'" runat="server"
                                                         CssClass="txt10" AutoPostBack="True" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'>
                                                     </asp:DropDownList>
                                                     <asp:Label ID="Label36" runat="server" CssClass="star" Width="1px">*</asp:Label></td>
                                             </tr>
                                             <tr>
                                                 <td>
                                                      <asp:Label ID="Label2" runat="server" CssClass="span" Text="OrderNo."></asp:Label>
                                                 </td>
                                                 <td>
                                                     <asp:DropDownList ID="cmborderno" onblur="this.className='blur'" onfocus="this.className='focus'"
                                                         runat="server" CssClass="txt10" AutoPostBack="True" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'>
                                                     </asp:DropDownList>
                                                     <asp:Label ID="Label3" runat="server" CssClass="star" Width="1px">*</asp:Label></td>
                                                 <td>
                                                     <asp:Label ID="Label5" runat="server" CssClass="span" Text="<%$Resources:ValidationResources,LOrderD %>">Order Date</asp:Label></td>
                                                 <td>
                                                       <asp:TextBox ID="txtorderdate" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Width="80%" />
                                                     
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td>
                                                     <asp:Label ID="lblprice" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LOrderV %>"></asp:Label></td>
                                                 <td colspan="3">
                                                     <input id="txttotorderamt" onblur="this.className='blur'" style='<%$ Resources: ValidationResources, TextBox2 %>'
                                                         onfocus="this.className='focus'" type="text" size="13" name="txttotorderamt" runat="server" class="txt10"></td>

                                             </tr>
                                             
                                             <tr>
                                                 <td>
                                                     <asp:Label ID="Label4" runat="server" CssClass="head1" style="color:black!important;visibility:hidden" Text="<%$Resources:ValidationResources,LOrderDet %>"></asp:Label><input
                                                         id="Hidden6" runat="server"
                                                         type="hidden" value="<%$ Resources:ValidationResources, dateFormat1 %>" /></td>
                                                 <td style="width:31%"></td><td style="width:14%"></td><td style="width:31%"></td>
                                             </tr>
                                             <tr>
                                                 <td colspan="3">
                                                     <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="True" CssClass="opt"
                                                         Text="<%$Resources:ValidationResources,CnkSelectA%>" /></td>
                                                 <td></td>
                                             </tr>
                                         </table>
                              </div>
                                    <div class="allgriddiv" id="dvgrd" runat="server">                                  
                    <asp:HiddenField runat="server" ID="hdnGrdId" />
                                    <asp:datagrid id="grddetail" AllowPaging="false" runat="server" CssClass="allgrid" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>' AutoGenerateColumns="False">
											
											<Columns>
												<asp:TemplateColumn>
													<ItemTemplate>
														<asp:CheckBox id="Chkselect" runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
                                                <asp:BoundColumn DataField="indentnumber" HeaderText="<%$ Resources:ValidationResources, LINumber %>">
												</asp:BoundColumn>
												<asp:BoundColumn DataField="title" HeaderText="<%$ Resources:ValidationResources, LTitle %>">
												</asp:BoundColumn>
												<asp:BoundColumn DataField="indenttype" HeaderText="<%$ Resources:ValidationResources, LIType %>">
												</asp:BoundColumn>
												<asp:BoundColumn DataField="amount" HeaderText="<%$ Resources:ValidationResources, Amt %>">
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Currency" HeaderText="<%$ Resources:ValidationResources,LCurrency %>">
												</asp:BoundColumn>
												<asp:BoundColumn DataField="convertedamount" HeaderText="<%$ Resources:ValidationResources,GrCovrtdAmt %>"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="indentid" Visible="False">
                                                </asp:BoundColumn>
                                                	</Columns>
											</asp:datagrid>
                                        </div>
                                     <div class="no-more-tables" style="width:100%">
                                         <table id="Table4" class="no-more-tables table-condensed">
                                             <tr>
                                                 <td style="text-align: center">
                                                     <input id="cmdsave" type="button" value="<%$Resources:ValidationResources,bSave %>" name="cmdsave"
                                                         runat="server" class="btnstyle">
                                                     <input id="cmdreset" type="button" value="<%$Resources:ValidationResources,bReset %>" name="cmdreset"
                                                         runat="server" class="btnstyle">
                                                     <asp:Button ID="cmdPrint1" runat="server" Text="<%$Resources:ValidationResources,iprint %>" UseSubmitBehavior="False" CssClass="btnstyle"></asp:Button>

                                                     <input id="btnEmail" runat="server" causesvalidation="false" type="button" value="<%$Resources:ValidationResources,bSendMail %>" class="btnstyle" />
                                                        <input id="cmdShow" runat="server" causesvalidation="false" style="visibility: hidden;
                            width: 1px; height: 8px" type="button" value="button" />

                                                 </td>
                                             </tr>
                                         </table>
                                         </div>
                                     <INPUT id="hdUnableMsg" type="hidden" runat="server"><INPUT id="Hidden4" style="WIDTH: 22px; HEIGHT: 18px" type="hidden" size="1" name="Hidden1"
							runat="server"><INPUT id="txtdepartmentcode" onblur="this.className='blur'" style="WIDTH: 32px; HEIGHT: 22px"
							onfocus="this.className='focus'" type="hidden" size="1" name="Hidden2" runat="server"><INPUT id="Hidden7" type="hidden" runat="server"><asp:textbox id="txtdeptcode" onblur="this.className='blur'" onfocus="this.className='focus'"
							runat="server" Width="32px" Visible="False"></asp:textbox><INPUT id="Hidden1" style="WIDTH: 32px; HEIGHT: 24px" type="hidden" size="1" name="Hidden1"
							runat="server"><input id="hrDate" runat="server" 
                            type="hidden" value="<%$ Resources:ValidationResources, dateFormat1 %>" /><INPUT id="hdCancelled" style="WIDTH: 16px; HEIGHT: 22px" type="hidden" size="1" name="hdCancelled"
							runat="server"><input id="Hidden2" style="width: 23px" type="hidden" runat="server" /><INPUT id="Hidden5" style="WIDTH: 16px; HEIGHT: 22px" type="hidden" size="1" name="Hidden2"
							runat="server"><INPUT id="HdOrderAlert" style="WIDTH: 16px; HEIGHT: 22px" type="hidden" size="1" name="Hidden2"
							runat="server"><INPUT id="yCoordHolder" style="WIDTH: 29px; HEIGHT: 16px" type="hidden" size="1" value="0"
							name="yCoordHolder" runat="server">
                                    
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
                ThreeLevelSearch($('#'+ grdId), "[id$=dvgrd]");
            }
            catch (err) {
            }
        }

    function SetDatePicker() {

        $("[id$=txtorderdate]").datepicker({

            changeMonth: true,//this option for allowing user to select month
            changeYear: true, //this option for allowing user to select from year range
            dateFormat: 'dd-M-yy'  // CHANGE DATE FORMAT.
        });

    }

</script>
 <script>
     $(function () {
         //ForDataTable();
         SetListBox();
     });
     var prm = Sys.WebForms.PageRequestManager.getInstance();

     if (prm != null) {
         prm.add_endRequest(function (sender, e) {

             if (sender._postBackSettings.panelsToUpdate != null) {

                 //  ForDataTable();
                 SetListBox();
             }
         });
     };
     function ForDataTable() {
         try {
             var grdId = $("[id$=hdnGrdId]").val();
             //alert(grdId);
             $('<thead></thead>').prependTo('#' + grdId).append($('#' + grdId + ' tr:first'));
             ThreeLevelSearch($('#' + grdId), "[id$=dvgrd]", 200);
         }
         catch (err) {
         }
     }
     function SetListBox() {

         $('[id*=cmbvendor]').multiselect({
             enableCaseInsensitiveFiltering: true,
             buttonWidth: '90%',
             includeSelectAllOption: true,
             maxHeight: 200,
             width: 315,
             enableFiltering: true,
             filterPlaceholder: 'Search'

         });

     }
 </script>
</asp:Content>

