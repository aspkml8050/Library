<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/LibraryMain.master"  CodeBehind="reminderReport.aspx.cs" Inherits="Library.reminderReport" %>

<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>


<asp:Content ID="remiHead" runat="server" ContentPlaceHolderID="head">
    <link href="cssDesign/libresponsive.css" rel="stylesheet" type="text/css" >
	<style>
	 .no-more-tables > table tr td{
        padding:3px!important
    }
	</style>

</asp:Content>

<asp:Content ID="remiMain" runat="server" ContentPlaceHolderID="MainContent">

     <link href="cssDesign/multiselect.css" rel="stylesheet" type="text/css" >
    <script src="FormScripts/multiselect.js"></script>

	      <asp:UpdateProgress ID="UpPorg1" runat="server">
    <ProgressTemplate>
    <NN:Mak ID="FF1" runat="server" />
    </ProgressTemplate>
    </asp:UpdateProgress>
   <div class="container tableborderst">   
        
              <div style="width:100%;display:none" class="title">
                  <div style="width: 89%; float: left" > ;
            <asp:label id="lblTitle" runat="server" style="display:none" > Order Reminder Letter </asp:label>
                  </div>
                  <div style="float:right;vertical-align:top">
            <a id="lnkHelp" href="#"  style="display:none" onclick="ShowHelp('Help/Acquisitioning-Follow-Up-Reminder-Report.htm')"><img alt="Help?" height="15" src="help.jpg"  /></a>
               </div></div>
					
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <Triggers><%--<asp:PostBackTrigger ControlID="cmdprint" />--%></Triggers>
                            <ContentTemplate>
                                <div class="no-more-tables" style="width: 100%">
						<TABLE id="Table1" class="table-condensed" >
							<TR>
								<TD colSpan="4"><asp:label id="msglabel" runat="server" CssClass="err"></asp:label></TD>
							</TR>
							<TR>
								<TD><asp:label id="Label2" runat="server" CssClass="span" Text="<%$Resources:ValidationResources,LVen %>"></asp:label></TD>
								<TD  colSpan="3"><asp:dropdownlist id="cmbvendor" onblur="this.className='blur'" onfocus="this.className='focus'" runat="server"
									 CssClass="txt10" AutoPostBack="True" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></asp:dropdownlist>
                                    <asp:label id="Label11" runat="server"  CssClass="star">*</asp:label></TD>
							</TR>
							<TR>
								<TD ><asp:label id="Label3" runat="server" CssClass="span" Text="<%$Resources:ValidationResources,LDeptm %>"></asp:label></TD>
								<TD colSpan="3"><asp:dropdownlist id="cmbdept" onblur="this.className='blur'" onfocus="this.className='focus'" runat="server"
										 CssClass="txt10" AutoPostBack="True" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></asp:dropdownlist>
                                    <asp:label id="Label7" runat="server" CssClass="star">*</asp:label></TD>
								 
							</TR>
							<TR>
								<TD style="width:20%"><asp:label id="Label4" runat="server" CssClass="span" Text="Order No"></asp:label></TD>
								<TD><asp:dropdownlist id="cmborderno" onblur="this.className='blur'" onfocus="this.className='focus'" 
										runat="server"  CssClass="txt10" AutoPostBack="True" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></asp:dropdownlist>
                                    <asp:label id="Label8" runat="server"  CssClass="star">*</asp:label></TD>
								<TD style="width:15%"><asp:label id="Label1" runat="server" CssClass="span" Text="<%$Resources:ValidationResources,LOrderD %>"></asp:label></TD>
								<TD ><INPUT id="txtDate" onblur="this.className='blur';checkdate1(this,document.Form1.hdCulture.value,document.Form1.hrDate.value)" 
										onfocus="this.className='focus'" type="text" size="12" name="txtdate" runat="server" class="txt10" readonly="readOnly">
                                    </TD>
							</TR>
							<TR>
								<TD> <asp:label id="Label5" runat="server" Text="Reminder Letter No."></asp:label></TD>
								<Td><INPUT id="txtreminderno" onblur="this.className='blur'" 
										onfocus="this.className='focus'"  type="text" size="40" name="txtdate" runat="server" class="txt10"></Td>
								<td></td>
                                <TD style="width:32.5%"></TD>
							</TR>
                            <tr>
                                <td  colspan ="4" >
                                   <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="True" CssClass="opt"
                                        Text="<%$ Resources:ValidationResources, CnkSelectA %>" /></td>
                               
                            </tr>
							</table>
                                    </div>
                                <div class="allgriddiv" style="max-height:300px">
                                <asp:datagrid id="grddetail" runat="server" CssClass="allgrid" Font-Names="<%$ Resources:ValidationResources, TextBox1 %>"> 										
										<Columns>
											<asp:TemplateColumn HeaderText="<%$ Resources:ValidationResources, GrSel %>">
												<ItemTemplate>
													<asp:CheckBox id="chkcheck" runat="server"></asp:CheckBox>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="indentnumber" ReadOnly="True" HeaderText="<%$ Resources:ValidationResources, LINumber %>"></asp:BoundColumn>
											<asp:BoundColumn DataField="title" HeaderText="<%$ Resources:ValidationResources, LTitle %>" DataFormatString="<%$ Resources:ValidationResources, GridDateF %>"></asp:BoundColumn>
											<asp:BoundColumn DataField="author" HeaderText="<%$ Resources:ValidationResources, Authors%>"></asp:BoundColumn>
											<asp:BoundColumn DataField="status" HeaderText="<%$ Resources:ValidationResources, LStatus %>"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="indentid" Visible="False"></asp:BoundColumn>
										</Columns>
										
									</asp:datagrid>
                                    </div>
                                 
                                <table class="no-more-tables" style="width:100%"  >
							<TR>
								<TD colspan="4" style="text-align:center" >	  
                                            <asp:button id="cmdprint" runat="server" Text="<%$Resources:ValidationResources,iprint %>" UseSubmitBehavior="False" CssClass="btn btn-primary"></asp:button>
											<asp:button id="cmdReset" runat="server" Text="<%$Resources:ValidationResources,bReset %>" UseSubmitBehavior="False" CssClass="btn btn-primary"></asp:button>
                                           
                                                <input id="btnEmail" runat="server" causesvalidation="false" 
                                                    type="button" value="<%$Resources:ValidationResources,bSendMail %>" class="btn btn-primary" /></TD>
										
							</TR>
							
                            <tr>
                                <td colspan="4" style="height: 24px">
                        <input id="hdStr" runat="server" style="width: 29px" type="hidden" /><INPUT id="Hidden7" type="hidden" runat="server" style="width: 42px"><INPUT id="hdTop" style="WIDTH: 19px; HEIGHT: 22px" type="hidden" size="1" name="hdTop"
							runat="server"><input id="HComboSelect" runat="server"
                            type="hidden" value="<%$ Resources:ValidationResources, ComboSelect %>" style="width: 39px" /><input id="hdReport" style="width: 32px" type="hidden" runat="server" /><INPUT id="txtremi" style="WIDTH: 21px; HEIGHT: 22px" type="hidden" name="Hidden1"
							runat="server">
                                    <input id="hrDate1" runat="server" style="width: 24px" type="hidden" /></td>
                            </tr>
						</table>
                                     
                             <div class="allgriddiv" style="max-height:300px"  id="dvgrd" runat="server">                <%--  OnDeleteCommand="grdsearch_Delete" 
                                 OnSelectedIndexChanged="grdsearch_SelectedIndexChanged"--%>
                    <asp:HiddenField runat="server" ID="hdnGrdId" /> 
                             <asp:datagrid id="grdsearch" runat="server"  BorderWidth="1px" CssClass="allgrid"
										PageSize="7"  CellPadding="2" AutoGenerateColumns="False" AllowSorting="True" Font-Size="X-Small"
										Font-Names="Courier New" GridLines="None" >
										<SelectedItemStyle CssClass="GridSelectedItemStyle" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></SelectedItemStyle>
										<EditItemStyle CssClass="GridEditedItemStyle" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></EditItemStyle>
										<AlternatingItemStyle CssClass="GridAltItem" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></AlternatingItemStyle>
										<ItemStyle CssClass="GridItem" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></ItemStyle>
										<HeaderStyle CssClass="GridHeader" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></HeaderStyle>
										<Columns>
											<asp:ButtonColumn Visible="False" Text="<%$ Resources:ValidationResources, GrSel %>" HeaderText="<%$ Resources:ValidationResources, GrSel %>" CommandName="Select"></asp:ButtonColumn>
											<asp:BoundColumn DataField="remindernumber" ReadOnly="True" HeaderText="<%$ Resources:ValidationResources, RemdrN %>"></asp:BoundColumn>
											<asp:BoundColumn DataField="reminderdate" HeaderText="<%$ Resources:ValidationResources, RemdrD %>" DataFormatString="<%$ Resources:ValidationResources, GridDateF %>"></asp:BoundColumn>
											<asp:ButtonColumn Text="<%$ Resources:ValidationResources, iprint %>" ButtonType="PushButton" HeaderText="<%$ Resources:ValidationResources, iprint %>" CommandName="Delete" ></asp:ButtonColumn>
										</Columns>
										<PagerStyle BorderColor="AliceBlue" HorizontalAlign="Right" ForeColor="Red" Position="TopAndBottom"
											Mode="NumericPages" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></PagerStyle>
									</asp:datagrid>
                                 </div>
                                <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID ="UpdatePanel1" runat="server">
                                                <progresstemplate>
                                                    <div id="IMGDIV" runat="server" align="center" 
                                                        style="position: absolute;left: 35%;top: 25%;visibility:visible;vertical-align:middle;" 
                                                        valign="middle">
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="Images/ajax-progress.gif" />
                                                    </div>
                                                </progresstemplate>
                                            </asp:UpdateProgress>
            <INPUT id="txthddate" style="WIDTH: 16px; HEIGHT: 22px" type="hidden" size="1" name="txthddate"
										runat="server"><input id="hrDate" runat="server" 
                                        type="hidden" value="<%$ Resources:ValidationResources, dateFormat1 %>" />
<%--								<asp:customvalidator id="vendoralidator1" runat="server" Width="128px" ControlToValidate="cmbvendor"
										ClientValidationFunction="comboValidation" Display="None" ErrorMessage="<%$ Resources:ValidationResources, SlctVdrN %>" SetFocusOnError="True"></asp:customvalidator>--%>
							<INPUT id="hdletterno" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" name="Hidden1"
										runat="server"><INPUT id="Hidden1" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" name="Hidden1"
										runat="server"><INPUT id="hdreminder" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" name="hdreminder"
										runat="server"><INPUT id="Hidden3" style="WIDTH: 8px; HEIGHT: 22px" type="hidden" size="1" name="Hidden3"
										runat="server"><input id="hdCulture" runat="server" style="width: 72px" type="hidden" />
                                   
                            </ContentTemplate>
                        </asp:UpdatePanel>
                </div>
            

                       
                        <%--<asp:CustomValidator ID="deptalidator2" runat="server" ClientValidationFunction="comboValidation"
                            ControlToValidate="cmbdept" Display="None" ErrorMessage="<%$ Resources:ValidationResources, IvDep %>"
                            SetFocusOnError="True" Width="128px"></asp:CustomValidator>--%>
                        <input id="cmdShow" runat="server" causesvalidation="false" style="width: 1px; height: 18px" type="button" value="button" class="btnH" />
				
			<asp:validationsummary id="ValidationSummary1" runat="server" Width="264px" Height="46px" ShowSummary="False"
							DisplayMode="List" ShowMessageBox="True"></asp:validationsummary>
                         
                        <asp:Button ID="cmdgridprint" runat="server" Text="Button" style="visibility: hidden" />
                        <%--<CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True" Visible="False" />--%>
                        
<%--                        <asp:customvalidator id="CustomValidator1" runat="server" Width="128px" ControlToValidate="cmborderno"
							ClientValidationFunction="comboValidation" Display="None" ErrorMessage="<%$ Resources:ValidationResources, SelONum %>" SetFocusOnError="True"></asp:customvalidator>--%>
		
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

