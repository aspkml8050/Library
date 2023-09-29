<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="edit_bookarrival.aspx.cs"  MasterPageFile="~/LibraryMain.master" Inherits="Library.edit_bookarrival" %>
<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>


<asp:Content ID="Head" runat="server" ContentPlaceHolderID="head">
<%--        <link href="cssDesign/libresponsive.css" rel="stylesheet" type="text/css" >--%>


</asp:Content>

<asp:Content ID="Main" runat="server" ContentPlaceHolderID="MainContent">
      <asp:UpdateProgress ID="UpPorg1" runat="server">
    <ProgressTemplate>
    <NN:Mak ID="FF1" runat="server" />
    </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="container tableborderst">  
                   
              <div style="width:100%;display:none" class="title">
                  <div style="width:89%;float:left" > &nbsp;
			<asp:label id="lblTitle" runat="server" style="display:none" Width="100%" ></asp:label>
                      </div>
                   <div style="float:right;vertical-align:top">
                        <a id="lnkHelp" href="#"  style="display:none" onclick="ShowHelp('Help/Acq-edit_bookarrival.htm')"> <img height="15" alt="Help" src="help.jpg"  /></a>
                </div></div>
						
                            
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            
                                <ContentTemplate>
                                 
							<TABLE id="Table1" class="table-condensed no-more-tables GenTable1" >
								<TR>
									<TD colSpan="4"><asp:label id="msglabel" runat="server" CssClass="err"  Visible="False"></asp:label></TD>
								</TR>
								<TR>
									<TD ><asp:label id="Label7" runat="server" Text="<%$ Resources:ValidationResources,LArrType %>"></asp:label></TD>
									<TD >
										<asp:radiobutton id="singleindent" runat="server"   GroupName="r1"
											Checked="True" Text="<%$ Resources:ValidationResources, NmlOrdBased %>" AutoPostBack="True"></asp:radiobutton></TD>
									<TD colspan="2">
										<asp:radiobutton id="GiftIndent" runat="server"    GroupName="r1"
											Text="<%$ Resources:ValidationResources,GftOrdBased %>" AutoPostBack="True"></asp:radiobutton></TD>
									
								</TR>
								<TR>
									<TD><asp:label id="Label5" runat="server"  Text="<%$ Resources:ValidationResources,LChIN %>"></asp:label></TD>
									<td>
										<table style="margin:-4px!important;padding:-4px;width:100%">
											<tr>
												<TD ><INPUT class="txt10" id="txtCategory" onblur="this.className='blur'"  
											onfocus="this.className='focus'" type="text" name="txtCategory" runat="server">
										</td>
									<td style="width:20%">
                                        <asp:button id="btnCategoryFilter" runat="server" Text="<%$ Resources:ValidationResources,bSearch %>"
											CausesValidation="False" UseSubmitBehavior="False" CssClass="btnstyle"></asp:button></td>
											</tr>
										</table>
									</td>
									
                                    <td>
                                        <asp:Label ID="Label10" runat="server" Text="<%$ Resources:ValidationResources,LDocDt %>"></asp:Label></td>
									<TD>
                                        

                                        <%--pushpendra singh--%>
 <asp:TextBox ID="docarrivaldate" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
 <%--<ajax:CalendarExtender ID="CalendarExtender1" TargetControlID="docarrivaldate" Format="dd/MMM/yyyy" runat="server"></ajax:CalendarExtender>--%> 
 <%--pushpendra singh--%>



                                        
                                      <%--  
                                        <INPUT language="javascript" class="txt10" id="docarrivaldate" onblur="this.className='blur';checkdate1(this,document.Form1.hdCulture.value,document.Form1.hrDate.value)"
											 width="96px;"
											onfocus="this.className='focus'" type="text" onchange="return docarrivaldate_onfocus()" name="docarrivaldate"
											runat="server" tabindex="0"><input id="btnDate" type="button" onclick="pickDate('docarrivaldate');" accesskey="D" style="background-position: center center; background-image: url(cal.gif); width: 27px; background-color: black; height: 20px;" />
                                        
                                        --%>
                                        
                                        
                                        <asp:label id="Label9" runat="server" CssClass="star" >*</asp:label></TD>
								</TR>
								<TR>
									<TD ></TD>
									<TD><asp:listbox id="lstAllCategory"  AutoPostBack="true"
                                            onblur="this.className='blur'" onfocus="this.className='focus'"
											runat="server" CssClass="txt10" 
                                             ></asp:listbox> 
                                     <asp:Label ID="Label1" runat="server" CssClass="star">*</asp:Label>

									</TD>
									<TD colspan="2"></TD>
								</TR>
								
								<TR>
									<TD colSpan="4" ><asp:label id="Label12" runat="server" CssClass="head1"  Text="<%$ Resources:ValidationResources,LArrDet %>"></asp:label></TD>
									 
								</TR>
                                <tr>
                                <td colspan="4" style="text-align:center">
										<INPUT id="cmdupdate" type="button" value="<%$ Resources:ValidationResources,bSave %>" name="cmdupdate"
														runat="server" class="btnstyle">
												<INPUT id="cmdreset" type="button" value="<%$ Resources:ValidationResources,bReset %>" name="cmdreset"
														runat="server" class="btnstyle">
											
                                    </td></tr>
								</table>
                                    
                                    <div class="allgriddiv" style="max-height:300px" id="dvgrd" runat="server">                                  
                    <asp:HiddenField runat="server" ID="hdnGrdId" />
                                        <asp:datagrid id="DataGrid1" runat="server" Width="100%" CssClass="GenTable1"  Font-Names='<%$ Resources:ValidationResources, TextBox1 %>' >
											
											<Columns>
												<asp:EditCommandColumn UpdateText="Update" HeaderText="<%$ Resources:ValidationResources, GrEdit %>" CancelText="Cancel"
													EditText="Edit">                                                    
                                                </asp:EditCommandColumn>
												<asp:TemplateColumn HeaderText="<%$ Resources:ValidationResources, LTitle %>">
													<ItemTemplate>
														<asp:Label id=Label2 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.title") %>'>
														</asp:Label>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox id=txtbooktitle onblur="this.className='blur'" onfocus="this.className='focus'" runat="server" BorderWidth="1px" Width="161px" Text='<%# DataBinder.Eval(Container, "DataItem.title") %>'>
														</asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
                                                <asp:BoundColumn DataField="vpe" ReadOnly="True"></asp:BoundColumn>
												<asp:BoundColumn DataField="orderedcopies" ReadOnly="True" HeaderText="<%$ Resources:ValidationResources, GrOC %>">
													</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="<%$ Resources:ValidationResources, GrCA %>">
													
													<ItemTemplate>
														<asp:Label id=lblcurrarrcopies runat="server" Width="76px" Text='<%# DataBinder.Eval(Container, "DataItem.currarrcopies") %>'>
														</asp:Label>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox id=txtcurrarrcopies onkeypress="IntegerNumber(this);" onblur="this.className='blur'" onfocus="this.className='focus'" runat="server" BorderWidth="1px" Width="57px" Text='<%# DataBinder.Eval(Container, "DataItem.currarrcopies") %>'>
														</asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="alreadyarrcopies" ReadOnly="True" HeaderText="<%$ Resources:ValidationResources, GrAAC %>">
													
												</asp:BoundColumn>
												<asp:BoundColumn DataField="oppc" ReadOnly="True" HeaderText="<%$ Resources:ValidationResources, GrOPCpy %>">
													
												</asp:BoundColumn>
												<asp:BoundColumn DataField="currency" ReadOnly="True" HeaderText="<%$ Resources:ValidationResources, LCurrency %>"></asp:BoundColumn>
												<asp:TemplateColumn HeaderText="<%$ Resources:ValidationResources, GrAPCpy %>">
													<ItemTemplate>
														<asp:Label id=lblappc runat="server" Width="97px" Text='<%# DataBinder.Eval(Container, "DataItem.appc") %>'>
														</asp:Label>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox id=txtappc onkeypress="decimalNumber(this);" onblur="this.className='blur'" onfocus="this.className='focus'" runat="server" BorderWidth="1px" Width="57px" Text='<%# DataBinder.Eval(Container, "DataItem.appc") %>'>
														</asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn Visible="False" DataField="indentnumber" ReadOnly="True" HeaderText="<%$ Resources:ValidationResources, LINumber %>">
													
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="<%$ Resources:ValidationResources, GrPrcP %>">
													<ItemTemplate>
														<asp:CheckBox id="chksearch" runat="server" Width="92px" Font-Size="XX-Small"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
                                                <asp:BoundColumn DataField="srno" ReadOnly="True" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="arr" ReadOnly="True" Visible="False"></asp:BoundColumn>
											</Columns>
											
										</asp:datagrid>

                                       <table class="table-condensed no-more-tables">
										   <tr>
											   <td>
                                        <asp:Label ID="Label3" runat="server"  Text="<%$ Resources:ValidationResources,OrdCopies %>"></asp:Label>

											   </td>
											   <td>
                                        <asp:Label ID="Label4" runat="server"  Text="<%$ Resources:ValidationResources,CrrArr %>"></asp:Label>

											   </td>
											   <td>
                                        <asp:Label ID="Label6" runat="server"  Text="<%$ Resources:ValidationResources,AlArrcopies %>"></asp:Label>

											   </td>
											   
										   </tr>
										   <tr>
											   <td>
                                        <asp:Label ID="Label8" runat="server"  Text="<%$ Resources:ValidationResources,OrdPrcPerCpy %>"></asp:Label>

											   </td>
											   <td>
                                        <asp:Label ID="Label11" runat="server"  Text="<%$ Resources:ValidationResources,AcPrcPerCpy  %>"></asp:Label>&nbsp;&nbsp;

											   </td>
											   <td></td>
										   </tr>
                                       </table>
                                      
                         
									<div style="display:none">
                                    &nbsp;<asp:ListBox ID="hlstAllCategory" runat="server" Height="0px" Width="0px">
                                    </asp:ListBox>
                                    <asp:Button ID="Button1" runat="server" BackColor="Transparent" 
                                        BorderColor="Transparent" BorderWidth="0px" CausesValidation="False" 
                                        ForeColor="AliceBlue" Height="0px" Text="Button" Width="0px" />
                                    &nbsp;
                                    
                                                &nbsp;<asp:RequiredFieldValidator ID="datevalidator" runat="server" 
                                                    ControlToValidate="docarrivaldate" Display="None" 
                                                    ErrorMessage="<%$ Resources:ValidationResources, EDocmtD %>" 
                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                           
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                                DisplayMode="List" Font-Size="11px" Height="22px" ShowMessageBox="True" 
                                                ShowSummary="False" Width="112px" />
                                        <INPUT id="hdvalue1" style="WIDTH: 24px; HEIGHT: 17px" type="hidden" size="1" name="hdvalue1"
											runat="server"><INPUT id="txtaccid" onblur="this.className='blur'" style="WIDTH: 16px; HEIGHT: 22px"
				onfocus="this.className='focus'" type="hidden" size="1" name="Hidden2" runat="server"><INPUT id="Hidden2" style="WIDTH: 16px; HEIGHT: 14px"
				type="hidden" size="1" name="Hidden2" runat="server"><INPUT id="hd_no" type="hidden" name="hCurrentIndex2" runat="server" style="width: 13px"><INPUT id="hCurrentIndex2" type="hidden" name="hCurrentIndex2" runat="server" style="width: 13px"><INPUT id="hdvalue" style="WIDTH: 16px; HEIGHT: 22px" type="hidden" name="hdvalue"
											runat="server"><INPUT id="hdmessage" style="WIDTH: 16px; HEIGHT: 22px" type="hidden" name="hdmessage"
											runat="server"><INPUT type="hidden" id="hdUnableMsg" runat="server" style="width: 24px"><input id="js1" runat="server"  width="16px;"
                            type="hidden" value="<%$ Resources:ValidationResources, js1 %>" /><input id="hrDate" runat="server"  width="32px;"
                            type="hidden" value="<%$ Resources:ValidationResources, dateFormat1 %>" /><input id="hdCulture" runat="server" style="width: 32px" type="hidden" /><INPUT id="hdTop" type="hidden" runat="server" style="WIDTH: 24px; HEIGHT: 22px"><INPUT id="HdBookAlert" style="WIDTH: 26px; HEIGHT: 22px" type="hidden" size="1" name="HdBookAlert"
							runat="server"><INPUT id="txtactualamt" style="WIDTH: 22px; HEIGHT:1px" type="hidden" size="1" name="txtactualamt"
							runat="server"><INPUT id="hSubmit1" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" value="0"
								name="hSubmit1" runat="server"><INPUT id="xCoordHolder" style="WIDTH: 22px; HEIGHT:1px" type="hidden" size="1" name="Hidden2"
							runat="server"><INPUT id="Hidden4" style="WIDTH: 16px; HEIGHT:1px" type="hidden" size="1" name="Hidden1"
							runat="server"><INPUT id="Hidden1" style="WIDTH: 20px; HEIGHT:1px" type="hidden" size="1" name="Hidden1"
							runat="server"><INPUT id="Hidden5" style="WIDTH: 16px; HEIGHT:1px" type="hidden" size="1" name="Hidden2"
							runat="server"><INPUT id="Text1" style="							                       WIDTH: 22px;
							                       HEIGHT: 1px
							               " type="hidden" size="1" name="Text1"
							runat="server"><INPUT id="Hidden3" style="WIDTH: 8px; HEIGHT:1px" type="hidden" size="1" name="Hidden3"
							runat="server"><INPUT id="yCoordHolder" style="WIDTH: 2px; HEIGHT:1px" type="hidden" size="1" name="Hidden2"
							runat="server"><INPUT id="txtdepartmentcode" onblur="this.className='blur'" style="WIDTH: 6px; HEIGHT:1px"
							onfocus="this.className='focus'" type="hidden" size="1" name="Hidden2" runat="server"><INPUT id="book" style="WIDTH: 8px; HEIGHT:1px"
				type="hidden" size="1" name="Hidden6" runat="server"></div>
                                </div>
                             </ContentTemplate>
                            </asp:UpdatePanel>
                </div>
	<style>
        SPAN.head1
        {
            font-weight:normal!important;
            font-size:16px!important;
            color:black!important;

        }
    </style>
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

        $("[id$=docarrivaldate]").datepicker({

            changeMonth: true,//this option for allowing user to select month
            changeYear: true, //this option for allowing user to select from year range
            dateFormat: 'dd-M-yy'  // CHANGE DATE FORMAT.
        });

    }

</script>
</asp:Content>

