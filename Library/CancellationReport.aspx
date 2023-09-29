<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CancellationReport.aspx.cs"  MasterPageFile="~/LibraryMain.master"Inherits="Library.CancellationReport" %>
<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>


<asp:Content ID="Head" runat="server" ContentPlaceHolderID="head">
         <link href="cssDesign/libresponsive.css" rel="stylesheet" type="text/css" >
     <link href="cssDesign/multiselect.css" rel="stylesheet" type="text/css" >
    <script src="FormScripts/multiselect.js"></script>

</asp:Content>

<asp:Content ID="Main" runat="server" ContentPlaceHolderID="MainContent">
         <asp:UpdateProgress ID="UpPorg1" runat="server">
    <ProgressTemplate>
    <NN:Mak ID="FF1" runat="server" />
    </ProgressTemplate>
    </asp:UpdateProgress>

            <div class="container tableborderst ">  
                   <div class="no-more-tables" style="width:100%">
              <div style="width:100%;display:none" class="title">
                  <div style="width:89%;float:left" >;
			<asp:label id="lblTitle" runat="server" style="display:none"  Width="100%">Order Cancellation Letter Printing</asp:label>
</div>
                     <div style="float:right;vertical-align:top">
                        <a id="lnkHelp" href="#"  style="display:none" onclick="ShowHelp('Help/Acquisitioning-Follow-Up-Cancellation-Report.htm')"><img alt="Help?" height="15" src="help.jpg"  /></a>
                </div></div>
                        
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <Triggers><asp:PostBackTrigger ControlID ="cmdPrint1" /></Triggers>
                                <ContentTemplate>
                       
							<TABLE id="Table1" class="table-condensed"  style="width:100%">
								
                                <tr>
                                    <td style="width:16%">
                                       <asp:label id="Label2" runat="server" CssClass="span" Text="<%$Resources:ValidationResources,LVen %>"></asp:label></td>
                                    <td>
                                        <asp:dropdownlist id="cmbvendor"  onchange="GetServer(this.id,'cmbdept');" onblur="this.className='blur'" onfocus="this.className='focus'" runat="server"
											CssClass="txt10" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'  > </asp:dropdownlist>
                                        <asp:Label ID="Label1" runat="server" CssClass="star" 
                                           >*</asp:Label></td>
                                
									<TD ><asp:label id="Label3" runat="server" CssClass="span" Text="<%$Resources:ValidationResources,LDeptm %>"></asp:label></TD>
									<TD >
                                        <asp:dropdownlist id="cmbdept" onchange="GetServer(this.id,'cmborderno');GetServer1(this.id,'Hidden1');" onblur="this.className='blur'" onfocus="this.className='focus'" runat="server"
											CssClass="txt10" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'  > </asp:dropdownlist><asp:Label ID="Label5" runat="server" CssClass="star" >*</asp:Label>
                                        

									</TD>
								</TR>
								<TR>
									<TD>
                                        <asp:label id="Label4" runat="server" CssClass="span" Text="Order No"></asp:label>
                                       <%-- <asp:label id="Label4" runat="server" CssClass="span" Text="<%$Resources:ValidationResources,LOrderNo %>"></asp:label>--%></TD>
									<TD><asp:dropdownlist id="cmborderno" onchange="GetServer1(this.id,'Hidden2');" onblur="this.className='blur'" onfocus="this.className='focus'"
											runat="server"   Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'  > </asp:dropdownlist>
                                        <asp:Label ID="Label6" runat="server" CssClass="star" 
                                        >*</asp:Label></TD>
								 
									<TD ><asp:label id="lblDate" runat="server" CssClass="span" Text="<%$Resources:ValidationResources,LDate %>"></asp:label></TD>
									<TD>
                                     

<%--pushpendra singh--%>
 <asp:TextBox ID="txtDate" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Width="90%" />
 <%--<ajax:CalendarExtender ID="CalendarExtender1" TargetControlID="txtDate" Format="dd/MMM/yyyy" runat="server"></ajax:CalendarExtender>--%> 
 <%--pushpendra singh--%>   
                                        
                                        
                                      <%--  <INPUT id="txtDate" onblur="this.className='blur';checkdate1(this,document.Form4.hdCulture.value,document.Form4.hrDate.value)" 
											onfocus="this.className='focus'" type="text" size="10" name="txtdate" runat="server" class="txt10"><input id="btnDate" type="button" onclick="pickDate('txtDate');" accesskey="D" style="background-position: center center; background-image: url(cal.gif); width: 27px; background-color: black" />
                                        --%>
                                        
                                        </TD>
								</TR>
                                <tr>
                                    <td colspan="2"></td>
                                    <td style="width:16%"></td>
                                    <td style="width:33%"></td>
                                </tr>
								<TR>
									<TD colspan="4" style="text-align:center" >
                                                <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID ="UpdatePanel1" runat="server">
                                                <progresstemplate>
                                                    <div id="IMGDIV" runat="server" align="center" 
                                                        style="                                                            position: absolute;
                                                            left: 35%;
                                                            top: 25%;
                                                            visibility: visible;
                                                            vertical-align: middle;" 
                                                        valign="middle">
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="Images/ajax-progress.gif" />
                                                    </div>
                                                </progresstemplate>
                                            </asp:UpdateProgress>
                                                <asp:button id="cmdPrint1" runat="server"  Text="<%$Resources:ValidationResources,iPrint %>" UseSubmitBehavior="False" CssClass="btnstyle"></asp:button>
												<asp:button id="Button2" runat="server"  Text="<%$Resources:ValidationResources,bReset %>" UseSubmitBehavior="False" CausesValidation="False" CssClass="btnstyle"></asp:button>
											
									</TD>
								</TR>
								
							</TABLE>
						</P>
                                    <div>
                                        <INPUT id="HdOrderCancellation" style="WIDTH:1px; " type="hidden" size="1"
							name="HdOrderCancellation" runat="server">
                        <input id="Hidden1"  runat="server" type="hidden" />
                        <input id="Hidden2"   runat="server" type="hidden" />
								<input id="hdCulture" runat="server" style="width: 72px" type="hidden" />
                        <input id="js1" runat="server"
                            type="hidden" value="<%$ Resources:ValidationResources, js1 %>" />
                        <input id="hrDate" runat="server"
                            type="hidden" value="<%$ Resources:ValidationResources, dateFormat1 %>" />
                        <input id="HComboSelect" runat="server"
                            type="hidden" value="<%$ Resources:ValidationResources, ComboSelect %>" />
                        <input id="hdReport" runat="server" style="width: 40px" type="hidden" />
                        <INPUT id="Hidden7" type="hidden" runat="server">
                                    </div>
						 </ContentTemplate>
                            </asp:UpdatePanel>
                       </div></div>
               <%--         <cr:CrystalReportViewer id="CrystalReportViewer1" runat="server" AutoDataBind="true">
                        </cr:CrystalReportViewer>--%>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                            ShowSummary="False" DisplayMode="List" />
                     
<%--                        <asp:CustomValidator ID="CustomVendor" runat="server" ClientValidationFunction="comboValidation"
                            ControlToValidate="cmbvendor" Display="None" ErrorMessage="Select Vendor Name"
                            SetFocusOnError="True"></asp:CustomValidator>;
                        <asp:CustomValidator ID="CustomDept" runat="server" ClientValidationFunction="comboValidation"
                            ControlToValidate="cmbdept" Display="None" ErrorMessage="Select Department "
                            SetFocusOnError="True"></asp:CustomValidator>
                        <asp:CustomValidator ID="CustomOrd" runat="server" ClientValidationFunction="comboValidation"
                            ControlToValidate="cmborderno" Display="None" ErrorMessage="Select Order No."
                            SetFocusOnError="True"></asp:CustomValidator>--%>
    <script type="text/javascript">
        //On Page Load.
        $(function () {
            SetDatePicker();
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    SetDatePicker();
                    
                }
            });
        };
        function SetDatePicker() {
            $("[id$=txtDate]").datepicker({
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

          function SetListBox() {

              $('[id*=cmbdept],[id*=cmbvendor],[id*=cmborderno]').multiselect({
                  enableCaseInsensitiveFiltering: true,
                  buttonWidth: '92%',
                  includeSelectAllOption: true,
                  maxHeight: 200,
                  width: 315,
                  enableFiltering: true,
                  filterPlaceholder: 'Search'

              });

          }
      </script>
</asp:Content>


