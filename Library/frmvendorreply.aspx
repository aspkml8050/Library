<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmvendorreply.aspx.cs" MasterPageFile="~/LibraryMain.master"Inherits="Library.frmvendorreply" %>
<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>


<asp:Content ID="vReplHead" runat="server" ContentPlaceHolderID="head">
         <link href="cssDesign/libresponsive.css" rel="stylesheet" type="text/css" >
	<link href="cssDesign/tdmanage.css" rel="stylesheet" type="text/css" >
     <link href="cssDesign/multiselect.css" rel="stylesheet" type="text/css" >
    <script src="FormScripts/multiselect.js"></script>
</asp:Content>

<asp:Content ID="vReplMain" runat="server" ContentPlaceHolderID="MainContent">
      <asp:UpdateProgress ID="UpPorg1" runat="server">
    <ProgressTemplate>
    <NN:Mak ID="FF1" runat="server" />
    </ProgressTemplate>
    </asp:UpdateProgress>
     <div class="container tableborderst">  
                   <div class="no-more-tables" style="width:100%">
              <div style="width:100%;display:none" class="title">
                  <div style="width:89%;float:left" > &nbsp;
			<asp:label id="lblTitle" runat="server" style="display:none" Width="100%" Font-Bold="True"> Vendor Reply</asp:label>
                      </div>
                    <div style="float:right;vertical-align:top">
                        <a id="lnkHelp" href="#"  style="display:none" onclick="ShowHelp('Help/Acquisitioning-Follow-Up-Frm-Vendor- Reply.htm')"><img alt="Help?" height="15" src="help.jpg" /></a>
               </div></div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                   
							<TABLE id="Table1" class="table-condensed">
								<TR>
									<TD colSpan="4"><asp:label id="msglabel" runat="server" CssClass="err"></asp:label></TD>
								</TR>
								<TR>
									<TD ><asp:label id="lbldepartmentname" runat="server" CssClass="span"  Text="<%$Resources:ValidationResources,LVen %>"></asp:label></TD>
									<TD ><asp:dropdownlist id="cmbvendorname" onblur="this.className='blur'" onfocus="this.className='focus'"
											runat="server" AutoPostBack="True" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>' CssClass="txt10"></asp:dropdownlist>
                                        <asp:label id="Label8" runat="server" CssClass="star" >*</asp:label></TD>
								 
									<TD ><%--<asp:label id="lblshortname" runat="server"  CssClass="span"  Text="<%$Resources:ValidationResources,LOrderNo %>"></asp:label>--%>
										<asp:label id="lblshortname" runat="server"  CssClass="span"  Text="OrderNo."></asp:label>
									</TD>
									<TD ><asp:dropdownlist id="cmborderno" onblur="this.className='blur'" onfocus="this.className='focus'"
											runat="server"  Font-Names='<%$ Resources:ValidationResources, TextBox1 %>' CssClass="txt10" AutoPostBack="True"></asp:dropdownlist>
                                        <asp:label id="Label1" runat="server" CssClass="star">*</asp:label></TD>
								</TR>
								<TR>
									<TD ><asp:label id="Label2" runat="server"  CssClass="span" Text="<%$Resources:ValidationResources,LRepD %>"></asp:label></TD>
									<TD >
                                        
                                       
                                        
                                        
                                        <%--pushpendra singh--%>
 <asp:TextBox ID="txtreplydate" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
 <%--<ajax:CalendarExtender ID="CalendarExtender1" TargetControlID="txtreplydate" Format="dd/MMM/yyyy" runat="server"></ajax:CalendarExtender>--%> 
 <%--pushpendra singh--%> 
                                        
                                        <%--<asp:textbox id="txtreplydate" onblur="this.className='blur';checkdate1(this,document.Form2.hdCulture.value,document.Form2.hrDate.value)" onfocus="this.className='focus'"
											runat="server" Width="97" BorderWidth="1px" BorderStyle="Solid"  CssClass="txt10"></asp:textbox><input id="btnDate" type="button" onclick="pickDate('txtreplydate');" accesskey="D" style="background-position: center center; background-image: url(cal.gif); width: 23px; background-color: black; height: 20px;" />--%>






									</TD>
								 
									<TD ><asp:label id="Label4" runat="server" CssClass="span"  Text="<%$Resources:ValidationResources,LLetterNum %>"></asp:label></TD>
									<TD ><asp:textbox id="txtletterno" onblur="this.className='blur'" onfocus="this.className='focus'"
											runat="server"  CssClass="txt10"></asp:textbox>
                                        <asp:Label ID="Label3" runat="server" CssClass="star" >*</asp:Label></TD>
								</TR>
								<TR>
									<TD colSpan="4" ><asp:radiobutton id="RadioButton2" runat="server"  CssClass="opt" Text="<%$Resources:ValidationResources,RBReplyI %>" AutoPostBack="True"
											Checked="True" GroupName="r1"></asp:radiobutton></TD>
								</TR>
								<TR>
									<TD colSpan="4"><asp:radiobutton id="RadioButton1" runat="server" CssClass="opt" Text="<%$Resources:ValidationResources,RBReplyIdI %>" AutoPostBack="True" GroupName="r1"></asp:radiobutton></TD>
								</TR>
								<TR>
									<TD>
                                       <asp:label id="Lblindentno" runat="server" CssClass="span" Text="<%$Resources:ValidationResources,LINumber %>"></asp:label></TD>
									<TD ><asp:dropdownlist id="cmbindentno" onblur="this.className='blur'" onfocus="this.className='focus'"
											runat="server" CssClass="txt10"  AutoPostBack="True" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></asp:dropdownlist></TD>
								 
									<TD>
                                        <asp:label id="Lblbook" runat="server"  CssClass="span"  Text="<%$Resources:ValidationResources,LTitle %>"></asp:label></TD>
									<TD  ><asp:textbox id="txttitle" onblur="this.className='blur'" onfocus="this.className='focus'" runat="server"
											 CssClass="txt10" BorderWidth="1px" ReadOnly="True" ></asp:textbox></TD>
								</TR>
								<TR>
									<TD>
                                        <asp:label id="Lblmessreceived" runat="server" CssClass="span"  Text="<%$Resources:ValidationResources,LMessRec %>"></asp:label></TD>
									<TD ><asp:dropdownlist id="cmbmessage" onblur="this.className='blur'" onfocus="this.className='focus'"
											runat="server"  CssClass="txt10"  AutoPostBack="True" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'>
											<asp:ListItem Value="Ordered but publ. is out of print." Text="<%$Resources:ValidationResources,Ordpub%>"></asp:ListItem>
											<asp:ListItem Value="Publication is in press." Text="<%$Resources:ValidationResources,PUBinPrs%>"></asp:ListItem>
											<asp:ListItem Value="Book does not exist." Text="<%$Resources:ValidationResources,BKNexst%>"></asp:ListItem>
											<asp:ListItem Value="Ordered but publ. is not available." Text="<%$Resources:ValidationResources,ORDnopub%>"></asp:ListItem>
											<asp:ListItem Value="Publication mailed by vendor." Text="<%$Resources:ValidationResources,PUBmalven%>"></asp:ListItem>
											<asp:ListItem Value="Extend expected date of order." Text="<%$Resources:ValidationResources,EXdatord%>"></asp:ListItem>
										</asp:dropdownlist></TD>
								 
									<TD >
                                        <asp:label id="Lblexdate" runat="server"  CssClass="span"  Text="<%$Resources:ValidationResources,LExtDate %>"></asp:label></TD>
									<TD ><asp:textbox id="txtdate" onblur="this.className='blur';checkdate1(this,document.Form1.hdCulture.value,document.Form1.hrDate.value)" onfocus="this.className='focus'"
											runat="server"  CssClass="txt10" BorderWidth="1px"></asp:textbox>
                                        <input id="Button1" type="button" onclick="pickDate('txtdate');" accesskey="D" style="background-position: center center; background-image: url(cal.gif); background-color: black;"  runat="server" /><A id="A2" tabIndex="0" href="javascript:NewCal('txtdate','ddmmmyyyy',false,24)" runat="server"></A></TD>
								</TR>
								<TR>
									
									<TD colspan="4" style="text-align:center">
										<INPUT id="cmdsave"  type="button" value="<%$Resources:ValidationResources,bSave %>" name="cmdsave"
														runat="server" class="btnstyle">
												<INPUT id="cmdreset" type="button" value="<%$Resources:ValidationResources,bReset %>" name="cmdreset"
														runat="server" class="btnstyle">
											
									</TD>
								</TR>
                                
								
							</TABLE>
					 </ContentTemplate>
                            </asp:UpdatePanel>
                               </div></div>
                               <INPUT id="hdTop" type="hidden" runat="server">
                        <input id="hdCulture" runat="server" style="width: 72px" type="hidden" />
                        <input id="hrDate" runat="server" 
                            type="hidden" value="<%$ Resources:ValidationResources, dateFormat1 %>" />
                        <input id="js1" runat="server" 
                            type="hidden" value="<%$ Resources:ValidationResources, js1 %>" />
                        <input id="HComboSelect" runat="server" 
                            type="hidden" value="<%$ Resources:ValidationResources, ComboSelect %>" />
                        <INPUT id="Hidden7" type="hidden" runat="server"><INPUT id="hdUnableMsg" type="hidden" runat="server">
							   
                       <%-- <asp:customvalidator id="vendorCustomValidator1" runat="server" ControlToValidate="cmbvendorname" Display="None"
							ErrorMessage="<%$ Resources:ValidationResources, SlctVdrN %>" ClientValidationFunction="comboValidation" SetFocusOnError="True"></asp:customvalidator>--%>
	<%-- <asp:customvalidator id="orderCustomValidator2" runat="server" ControlToValidate="cmborderno" Display="None"
							ErrorMessage="<%$ Resources:ValidationResources, SlctOdrN %>" ClientValidationFunction="comboValidation" SetFocusOnError="True"></asp:customvalidator>
	--%><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ControlToValidate="txtletterno" Display="None"
							ErrorMessage="<%$ Resources:ValidationResources, LEnter %>" SetFocusOnError="True"></asp:requiredfieldvalidator>
<%--                        <asp:CustomValidator ID="Indentvalidator1" runat="server" ClientValidationFunction="comboValidation"
                            ControlToValidate="cmbindentno" Display="None" ErrorMessage="<%$ Resources:ValidationResources, GrIndntN %>" SetFocusOnError="True"></asp:CustomValidator>--%>
                               
                                    <INPUT id="yCoordHolder" style="WIDTH: 22px; HEIGHT: 22px" type="hidden" size="1" name="yCoordHolder"
											runat="server"><INPUT id="xCoordHolder" style="WIDTH: 16px; HEIGHT: 22px" type="hidden" size="1" name="xCoordHolder"
											runat="server"> <INPUT id="Hidden1" style="WIDTH: 24px; HEIGHT: 8px" type="hidden" size="1" name="Hidden1"
											runat="server">
                                        <asp:validationsummary id="ValidationSummary2" runat="server" Width="88px" Height="8px" Font-Size="11px"
							DisplayMode="List" ShowSummary="False" ShowMessageBox="True"></asp:validationsummary>
					
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
            $("[id$=txtreplydate]").datepicker({
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

            $('[id*=cmbvendor]').multiselect({
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


        

