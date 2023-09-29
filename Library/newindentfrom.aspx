<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="newindentfrom.aspx.cs"  MasterPageFile="~/LibraryMain.master" Inherits="Library.newindentfrom" %>
<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>

<asp:Content ID="nIndtHead" runat="server" ContentPlaceHolderID="head">
        <link href="cssDesign/libresponsive.css" rel="stylesheet"
type="text/css" /> 
<style>
    .btn-group > .btn, .btn-group-vertical > .btn{
        width: 93% !important;
    }
    </style>

</asp:Content>

<asp:Content ID="nIndtMain" runat="server" ContentPlaceHolderID="MainContent">
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
			<asp:label id="lblTitle" runat="server" style="display:none"  Width="100%">Print Indent</asp:label>
                      </div>
                  <div style="float:right;vertical-align:top">
                        <a id="lnkHelp" href="#" style="display:none"  onclick="ShowHelp('Help/Acquisitioning-Indents-IndentComb-Form.htm')"><img alt="Help?" height="15" src="help.jpg" /></a>
               </div></div>                                             
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table id="Table2" class="no-more-tables table-condensed" >
                                    <tr>
                                        <td colspan="3">
                                            <asp:Label ID="msglabel" runat="server" CssClass="err"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lbloption" Text="Report Option"
                                                Font-Bold="True" ForeColor="#009900"></asp:Label>
                                        </td>
                                        <td colspan="2">
                                            <asp:RadioButtonList ID="optreport" runat="server"
                                                RepeatDirection="Horizontal"  >
                                                <asp:ListItem Selected="True" Text="Default" Value="D"></asp:ListItem>
                                                <asp:ListItem Text="Requester Wise List" Value="R"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                     
                                    <tr >
                                        <td>
                                            <asp:Label runat="server" ID="lblrange" Text="Date Range"></asp:Label>
                                        </td>
                                        <td>

                                             <asp:Label ID="Label7" runat="server" CssClass="head1" style="color:black" Font-Bold="True"
                                                Text="<%$ Resources:ValidationResources,LFromD %>"></asp:Label><br />
                                           
                                            <asp:TextBox ID="txtfromdate" runat="server" style="margin-top:3px" CssClass="txt10" />
                                           




                                        </td>
                                        <td>
                                             <asp:Label ID="Label6" runat="server" CssClass="head1"  style="color:black"
                                                Text="<%$ Resources:ValidationResources,LToD %>"></asp:Label><br />
                                           
                                            <asp:TextBox ID="txttodate" runat="server" style="margin-top:3px;width:80%" CssClass="txt10" />
                                           
                                        </td>
                                    </tr>
                                    <tr >
                                        <td>
                                               <asp:Label ID="Label39" runat="server" Text="<%$ Resources:ValidationResources,LDeptm %>"></asp:Label>
                                          
                                        </td>
                                        <td colspan="2">
                                         
                                             <asp:ListBox ID="cmbdept"  runat="server"  AutoPostBack="true" SelectionMode="Single" ></asp:ListBox>
                                        </td>

                                    </tr>
                                    <tr >
                                        <td>
                                            <asp:HyperLink ID="Label51" runat="server" style="color:blue!important"
                                                onclick="openNewForm('btnFillPub','UserManagement','HNewForm','HWhichFill','HCondition');"
                                                Text="<%$ Resources:ValidationResources,LReque %>"></asp:HyperLink>
                                        </td>
                                        <td colspan="2">
                                            <asp:DropDownList ID="cmbreq" onblur="this.className='blur'" onfocus="this.className='focus'" runat="server"
                                                CssClass="txt10"  Font-Names='<%$ Resources:ValidationResources, TextBox1 %>' ToolTip="Press Alt+R to add New Requester" onchange="GetServer1('cmbreq','hReq') ">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr >
                                        <td style="width:20%">
                                            <asp:Label ID="Label55" runat="server" CssClass="span"
                                                Text="<%$ Resources:ValidationResources,LTitle %>"></asp:Label>
                                        </td>
                                        <td colspan="2">
                                            <asp:TextBox ID="txttitle" runat="server" Columns="30" CssClass="txt10"
                                                MaxLength="100" onblur="this.className='blur'" onfocus="this.className='focus'"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr >
                                        <td>
                                            <asp:Label ID="Label10" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LINumber %>">Indent Number</asp:Label></td>
                                        <td colspan="2">
                                            <asp:TextBox onkeypress="disallowSingleQuote(this);" ID="txtindentnumber" onkeydown="txtindentnumber_OnKeydown()"
                                                onblur="this.className='blur'" onfocus="this.className='focus'" runat="server" style="width:60%"></asp:TextBox>
                                            <asp:Button ID="Button4" runat="server"
                                                Text="<%$ Resources:ValidationResources,SR %>" CssClass="btn btn-primary" />
                                            <input id="cmdGet" type="button" value="<%$Resources:ValidationResources,BEnter %>" name="cmdGet" runat="server" style="visibility: hidden" class="btnstyle"></td>
                                    </tr>
                                      <tr>
                                        <td>   <asp:CheckBox ID="chkSearch" runat="server" AutoPostBack="True" CssClass="opt" Text="<%$Resources:ValidationResources,bSearch %>" /></td>
                                        <td colspan="2" >

                                           <%-- <input id="cmdprint" type="button" value="<%$Resources:ValidationResources,iprint %>" name="cmdSearch"
                                                runat="server" class="btnstyle">--%>
                                             <asp:Button Id="cmdprint" runat="server" Text="Print" CssClass="btn btn-primary"/>
                                            <%--<input id="cmdreset"
                                                type="button" value="<%$Resources:ValidationResources,bReset %>" name="cmdreset" runat="server" causesvalidation="false" class="btnstyle">--%>
                                             <asp:Button Id="cmdreset" runat="server" Text="Reset" CssClass="btn btn-primary"/>
                                        </td>
                                    </tr>
                                  
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LRefNo %>">Indent Number</asp:Label></td>
                                        <td colspan="2">
                                            <input id="txtCategory" runat="server" style="width:75%" name="txtCategory" onblur="this.className='blur'" onfocus="this.className='focus'" onkeypress="disallowSingleQuote(this);" type="text" />
                                           <%-- <input id="btnCategoryFilter" runat="server" name="btnCategoryFilter" type="button" value="<%$Resources:ValidationResources,bSearch %>" class="btnstyle" />--%>
                                            <asp:Button Id="btnCategoryFilter" runat="server" Text="Search" CssClass="btn btn-primary"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td colspan="2">
                                            <asp:ListBox ID="lstAllCategory" runat="server" CssClass="txt10" onblur="this.className='blur'"
                                                onfocus="this.className='focus'"
                                                TabIndex="3"></asp:ListBox></td>
                                    </tr>
                                    <tr><td colspan="3">
                                         <asp:CheckBox ID="chkSelectAll" runat="server" CssClass="opt" Text="<%$ Resources:ValidationResources,CnkSelectA %>"
                                                AutoPostBack="True"></asp:CheckBox>
                                        </td></tr>
                                </table>
                                 <div class="allgriddiv" style="max-height:300px;margin-top:10px" id="dvgrd" runat="server">                                  
                    <asp:HiddenField runat="server" ID="hdnGrdId" />
                  <asp:datagrid id="grdOrder" runat="server" CssClass="allgrid" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'>
											
											<Columns>
											<asp:TemplateColumn HeaderText="<%$ Resources:ValidationResources, GrSel %>">
													<HeaderStyle Width="2%"></HeaderStyle>													
													<ItemTemplate>
														&nbsp;
														<asp:CheckBox id="Chkselect" runat="server" AutoPostBack="True"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="indentnumber" HeaderText="<%$ Resources:ValidationResources, LINumber %>">
													<HeaderStyle Width="5%"></HeaderStyle>
													
												</asp:BoundColumn>
												<asp:BoundColumn DataField="indentdate" DataFormatString="<%$ Resources:ValidationResources, GridDateF %>" HeaderText="<%$ Resources:ValidationResources, IDt %>">
													<HeaderStyle Width="3%"></HeaderStyle>
												</asp:BoundColumn>
                                                <asp:BoundColumn DataField="IndentId" HeaderText="IndentId" Visible="False"></asp:BoundColumn>
											</Columns>
											
										</asp:datagrid>
              </div>
                                 <input id="Hidden2" runat="server" style="width: 1px" type="hidden" />
             <INPUT id="Hidden3" style="WIDTH: 8px; HEIGHT: 22px" type="hidden" size="1" name="Hidden3"
								runat="server"><input id="hSubmit1" runat="server" name="hSubmit1" size="1" style="width: 24px;
                                height: 22px" type="hidden" value="0" /><INPUT id="hdForMesage" style="WIDTH: 16px; HEIGHT: 22px" type="hidden" name="hdForMesage"
								runat="server"><input id="HdRefNo" runat="server" style="width: 16px" type="hidden" /><INPUT id="Hidden7" type="hidden" runat="server" style="width: 16px"><input id="HComboSelect" runat="server" 
                            type="hidden" value="<%$ Resources:ValidationResources, ComboSelect %>" style="width: 16px" /><input id="hrDate" runat="server" 
                            type="hidden" value="<%$ Resources:ValidationResources, dateFormat1 %>" style="width: 16px" /><input id="hdCulture" runat="server" style="width: 16px" type="hidden" /><input id="js1" runat="server" 
                            type="hidden" value="<%$ Resources:ValidationResources, js1 %>" style="width: 8px" /><input id="hdCheck" runat="server" style="width: 16px" type="hidden" />
                            <input id="Button1" runat="server" class="btnH" name="Button1" style="width: 1px;  height: 1px; visibility:hidden"
                               type="button" value="Button" />
			<INPUT id="Hidden1" style="WIDTH: 24px; HEIGHT: 22px"
				type="hidden" size="1" name="Hidden1" runat="server">

                      
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="cmdprint" />
                            </Triggers>
                        </asp:UpdatePanel>  
                    <input id="cmdShow" runat="server" causesvalidation="false" style="visibility: hidden;
                            width: 2px; height: 8px" type="button" value="button" class="btnh" />&nbsp;<asp:validationsummary id="ValidationSummary1" runat="server" DisplayMode="List" ShowSummary="False" ShowMessageBox="True"></asp:validationsummary>
				<%--<CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True" Visible="False" />--%>

                       
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
                $('<thead></thead>').prependTo('#ctl00_MainContent_grdOrder').append($('#ctl00_MainContent_grdOrder'+' tr:first'));
                ThreeLevelSearch($('#ctl00_MainContent_grdOrder'), "[id$=dvgrd]");
            }
            catch (err) {
            }
        }
        
    function SetDatePicker() {

        $("[id$=txtfromdate],[id$=txttodate]").datepicker({
            changeMonth: true,//this option for allowing user to select month
            changeYear: true, //this option for allowing user to select from year range
            dateFormat: 'dd-M-yy'  // CHANGE DATE FORMAT.
        });
        $('#txtfromdate').click(function () {
            $('#txtfromdate').datepicker('show');
        });
        //$('#ui-datepicker-div').show();
        //$('#txtDateFrom').datepicker('show');
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

            $('[id*=cmbdept]').multiselect({
                enableCaseInsensitiveFiltering: true,
                buttonWidth: '80%',
                includeSelectAllOption: true,
                maxHeight: 200,
                width: 315,
                enableFiltering: true,
                filterPlaceholder: 'Search'

            });

        }
    </script>
</asp:Content>

