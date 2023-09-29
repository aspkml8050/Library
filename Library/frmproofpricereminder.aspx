<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmproofpricereminder.aspx.cs" MasterPageFile="~/LibraryMain.master" Inherits="Library.frmproofpricereminder" %>
<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>

<asp:Content ID="Head" runat="server" ContentPlaceHolderID="head">
	         <link href="cssDesign/libresponsive.css" rel="stylesheet" type="text/css" >
    <style>
        SPAN.head1
        {
            font-weight:normal!important;
            font-size:16px!important;
            color:black!important;

        }
    </style>
	  
</asp:Content>

<asp:Content ID="Main" runat="server" ContentPlaceHolderID="MainContent">
      <asp:UpdateProgress ID="UpPorg1" runat="server">
    <ProgressTemplate>
    <NN:Mak ID="FF1" runat="server" />
    </ProgressTemplate>
    </asp:UpdateProgress>
    		<div class="container tableborderst">  
                  
              <div style="width:100%;display:none" class="title">
                  <div style="width:89%;float:left" >&nbsp;
                       <asp:label id="lblTitle" runat="server" style="display:none"  Width="100%" Font-Bold="True"> Reminder Letter for Price Proof</asp:label>
                      </div>
                   <div style="float:right;vertical-align:top">
                         <a id="lnkHelp" href="#"  style="display:none" onclick="ShowHelp('Help/Acq-frmproofpricereminder.htm')"><img height="15" alt="Help" src="help.jpg"  /></a>
               </div></div>
                        
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <Triggers><asp:PostBackTrigger ControlID ="cmdsave" /></Triggers>
                                <ContentTemplate>
                                     <div class="no-more-tables" style="width:100%">
                                         <table id="Table2" class="table-condensed">
                                             <tr>
                                                 <td colspan="4">
                                                     <asp:Label ID="msglabel" runat="server" CssClass="err"></asp:Label></td>
                                             </tr>
                                             <tr>
                                                 
                                                 <td  style="width: 15%">
                                                     <asp:Label ID="Label1" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LVen %>"></asp:Label></td>
                                                 <td>
                                                     <asp:DropDownList ID="cmbvendor" onblur="this.className='blur'" onfocus="this.className='focus'" runat="server"
                                                         CssClass="txt10" AutoPostBack="True" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'>
                                                     </asp:DropDownList>
                                                     <asp:Label ID="Label9" runat="server" CssClass="star">*</asp:Label><span style="font-size: 10pt; font-family: Arial"></span></td>
                                               
                                                 <td style="width: 16%">
                                                     <asp:Label ID="Label2" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LOrderNumb %>"></asp:Label></td>
                                                 <td>
                                                     <asp:DropDownList ID="cmborderno" onblur="this.className='blur'" onfocus="this.className='focus'"
                                                         runat="server" CssClass="txt10" AutoPostBack="True" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'>
                                                     </asp:DropDownList>
                                                     <asp:Label ID="Label3" runat="server" CssClass="star">*</asp:Label></td>
                                                
                                             </tr>
                                             <tr>
                                                 <td colspan="4" style="text-align: center">
                                                     <input id="cmdsave" type="button" value="<%$ Resources:ValidationResources,iprint %>" name="cmdsave"
                                                         runat="server" class="btnstyle">
                                                     <input id="cmdreset" type="button" value="<%$ Resources:ValidationResources,bReset %>" name="cmdreset"
                                                         runat="server" class="btnstyle">

                                                     <input id="btnEmail" runat="server" causesvalidation="false"
                                                         type="button" value="<%$Resources:ValidationResources,bSendMail %>" class="btnstyle" />

                                                 </td>
                                             </tr>
                                             <tr>
                                                
                                                 <td>
                                                     <asp:Label ID="Label4" runat="server" CssClass="showBoldExist" Text="<%$ Resources:ValidationResources,LOrderDet %>"></asp:Label></td>

                                                 <td>
                                                     <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="True" CssClass="opt"
                                                         Text="<%$ Resources:ValidationResources,CnkSelectA %>" /></td>
                                                 <td colspan="2"></td>

                                             </tr>

                                         </table></div>
							

                                    <div class="allgriddiv" id="dvgrd" runat="server">                                  
                    <asp:HiddenField runat="server" ID="hdnGrdId" />
                <asp:datagrid id="grddetail" runat="server"  CssClass="allgrid" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'>
										
										<Columns>
											<asp:TemplateColumn HeaderText="<%$ Resources:ValidationResources, GrSel %>">
												<ItemTemplate>
													<asp:CheckBox id="Chkselect" runat="server" AutoPostBack="True"></asp:CheckBox>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="indentnumber" HeaderText="<%$ Resources:ValidationResources, LINumber %>"></asp:BoundColumn>
											<asp:BoundColumn DataField="title" HeaderText="<%$ Resources:ValidationResources,LTitle %>"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="indentno" Visible="False"></asp:BoundColumn>
										</Columns>										
									</asp:datagrid>
                    </div>

										
						<INPUT id="Hidden3" style="WIDTH: 1px; HEIGHT: 22px" type="hidden" name="Hidden3"
							runat="server">&nbsp;</P>
                                      <INPUT id="Hidden5" style="WIDTH: 8px; HEIGHT: 22px" type="hidden" name="Hidden2"
							runat="server"><INPUT id="Hidden1" style="WIDTH: 24px; HEIGHT: 24px" type="hidden" name="Hidden1"
							runat="server"><INPUT id="Hidden4" style="WIDTH: 16px; HEIGHT: 18px" type="hidden" name="Hidden1"
							runat="server"><asp:textbox id="txtdeptcode" onblur="this.className='blur'" onfocus="this.className='focus'"
							runat="server" Width="24px" Visible="False"></asp:textbox><INPUT id="Hidden7" type="hidden" runat="server" style="width: 16px"><input id="HComboSelect" runat="server" 
                            type="hidden" value="<%$ Resources:ValidationResources, ComboSelect %>" style="width: 16px" />

						  </ContentTemplate>
                            </asp:UpdatePanel>
                           </div>
                
			
<%--          
						<asp:customvalidator id="deptCustomValidator1" runat="server" Font-Size="11px" SetFocusOnError="true" ErrorMessage="<%$ Resources:ValidationResources, IvVen %>"
							ClientValidationFunction="comboValidation" ControlToValidate="cmbvendor" Display="None"></asp:customvalidator><asp:customvalidator id="orderCustomValidator1" runat="server" Font-Size="11px" SetFocusOnError="true" ErrorMessage="<%$ Resources:ValidationResources, SlctOdrN %>"
							ClientValidationFunction="comboValidation" ControlToValidate="cmborderno" Display="None"></asp:customvalidator>--%>
                        &nbsp;
						<asp:validationsummary id="ValidationSummary1" runat="server" Width="96px" Font-Size="11px" DisplayMode="List"
							ShowSummary="False" ShowMessageBox="True"></asp:validationsummary>
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



	

