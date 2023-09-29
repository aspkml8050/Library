<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfirmationReport.aspx.cs"  MasterPageFile="~/LibraryMain.master" Inherits="Library.ConfirmationReport" %>
<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>


<asp:Content ID="confirmHead" runat="server" ContentPlaceHolderID="head">
        <link href="cssDesign/libresponsive.css" rel="stylesheet" type="text/css" >
     <link href="cssDesign/multiselect.css" rel="stylesheet" type="text/css" >
    <script src="FormScripts/multiselect.js"></script>

</asp:Content>

<asp:Content ID="confirmMain" runat="server" ContentPlaceHolderID="MainContent">
         <asp:UpdateProgress ID="UpPorg1" runat="server">
    <ProgressTemplate>
    <NN:Mak ID="FF1" runat="server" />
    </ProgressTemplate>
    </asp:UpdateProgress>
  <div class="container tableborderst">  
                   <div class="no-more-tables" style="width:100%">
              <div style="width:100%;display:none" class="title">
                  <div style="width:89%;float:left" >&nbsp;
			 <asp:label id="lblTitle" runat="server" style="display:none" Width="100%" >Order Confirmation Letter</asp:label>
                        </div>
                   <div style="float:right;vertical-align:top">
                      <a id="lnkHelp" href="#"  style="display:none" onclick="ShowHelp('Help/Acquisitioning-Follow-Up-Confirmation-Report.htm')"><img alt="Help?" height="15" src="help.jpg"  /></a>
                </div></div>
                          
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <Triggers><asp:PostBackTrigger ControlID ="cmdPrint1" /></Triggers>
                                <ContentTemplate>
                       
                           
						<TABLE id="Table1" class="table-condensed GenTable1" >
							<TR>
								<TD colSpan="2" ><asp:label id="msglabel" runat="server"  CssClass="err"></asp:label></TD>
							</TR>
							<TR>
								<TD style="width:16%">&nbsp<asp:label id="Label2" runat="server" CssClass="span" Text="<%$Resources:ValidationResources,LVen %>"></asp:label></TD>
								<TD ><asp:dropdownlist id="cmbvendor" Height="30"  onblur="this.className='blur'" onfocus="this.className='focus'" runat="server"
										CssClass="txt10" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>' AutoPostBack="True"></asp:dropdownlist>
                                    <asp:Label ID="Label1" runat="server" CssClass="star"
                                    >*</asp:Label></TD>
							</TR>
							<TR>
								<TD >&nbsp<asp:label id="Label3" runat="server" CssClass="span" Text="<%$Resources:ValidationResources,LDeptm %>"></asp:label></TD>
								<TD ><asp:dropdownlist id="cmbdept" Height="30" onblur="this.className='blur'" onfocus="this.className='focus'" runat="server"
										CssClass="txt10" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>' AutoPostBack="True"></asp:dropdownlist>
                                    <asp:Label ID="Label5" runat="server" CssClass="star" 
                                   >*</asp:Label></TD>
							</TR>
							<TR>
								<TD ><asp:label id="Label4" runat="server" CssClass="span" Text="<%$Resources:ValidationResources,LOrderNo %>"></asp:label></TD>
								<TD ><asp:dropdownlist id="cmborderno" Height="30" onblur="this.className='blur'" onfocus="this.className='focus'"
										runat="server" CssClass="txt10" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></asp:dropdownlist>
                                    <asp:Label ID="Label6" runat="server" CssClass="star"
                                  >*</asp:Label></TD>
							</TR>
							<TR>
								<TD id="ZZ" colspan="2" style="text-align:center">
								<input id="hrDate"
                                    runat="server" 
                                    type="hidden" value="<%$ Resources:ValidationResources, dateFormat1 %>"  />
								
								
 
									
                                            <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID ="UpdatePanel1" runat="server">
                                                <progresstemplate>
                                                    <div id="IMGDIV" runat="server" align="center" 
                                                        style="position: absolute;left: 35%;top: 25%;visibility:visible;vertical-align:middle;" 
                                                        valign="middle">
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="Images/ajax-progress.gif" />
                                                    </div>
                                                </progresstemplate>
                                            </asp:UpdateProgress>
                                            <asp:button id="cmdPrint1" runat="server"  Text="<%$Resources:ValidationResources,iprint %>" UseSubmitBehavior="False" CssClass="btnstyle"></asp:button>
                                           
                                                <asp:Button ID="cmdreset" runat="server" Text="<%$Resources:ValidationResources,bReset %>"  UseSubmitBehavior="False" CssClass="btnstyle" />
                                           
                                                <input id="btnEmail" runat="server" causesvalidation="true" type="button" value="<%$Resources:ValidationResources,bSendMail %>"  class="btnstyle" />
										
								</TD>
							</TR>
							 <INPUT id="Hidden1" type="hidden" name="Hidden1" runat="server" style="WIDTH: 8px; HEIGHT: 22px" size="1">
							<input id="HComboSelect" runat="server"
                            type="hidden" value="<%$ Resources:ValidationResources, ComboSelect %>" />
                        <INPUT id="Hidden7" type="hidden" runat="server">
       <%--                 <asp:CustomValidator ID="vendorvalidator1" runat="server" ClientValidationFunction="comboValidation"
                            ControlToValidate="cmbvendor" Display="None" ErrorMessage="<%$ Resources:ValidationResources, IvVen %>" Width="119px" SetFocusOnError="True"></asp:CustomValidator>
                        <asp:CustomValidator ID="deptValidator1" runat="server" ClientValidationFunction="comboValidation"
                            ControlToValidate="cmbdept" Display="None" ErrorMessage="<%$ Resources:ValidationResources, SelDep %>"
                            Width="119px" SetFocusOnError="True"></asp:CustomValidator>
                        <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="comboValidation"
                            ControlToValidate="cmborderno" Display="None" ErrorMessage="<%$ Resources:ValidationResources, SelONum %>"
                            Width="119px" SetFocusOnError="True"></asp:CustomValidator>--%>
                            <input id="hdReport" runat="server" style="width: 16px" type="hidden" />
                        <input id="Hidden2" runat="server" type="hidden" style="width: 37px" />
                        <input id="Hidden3" runat="server" type="hidden" style="width: 17px" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                            ShowMessageBox="True" ShowSummary="False" />
						</TABLE>
						 </ContentTemplate>
                            </asp:UpdatePanel>
                        </div></div>
<%--                        <cr:CrystalReportViewer id="CrystalReportViewer1" runat="server" AutoDataBind="true">
                        </cr:CrystalReportViewer>--%>


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
     
         function SetListBox() {

             $('[id*=cmbdept],[id*=cmbvendor]').multiselect({
                 enableCaseInsensitiveFiltering: true,
                 buttonWidth: '95%',
                 includeSelectAllOption: true,
                 maxHeight: 200,
                 width: 315,
                 enableFiltering: true,
                 filterPlaceholder: 'Search'

             });

         }
     </script>


</asp:Content>

