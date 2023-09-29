<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookArrival.aspx.cs"  MasterPageFile="~/LibraryMain.master" Inherits="Library.BookArrival" %>
<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>


<asp:Content ID="Head" runat="server" ContentPlaceHolderID="head">
         <link href="cssDesign/libresponsive.css" rel="stylesheet" type="text/css" >


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
                      <asp:Label ID="lblTitle" style="display:none" runat="server" Width="100%">Book Arrival and Accessioning</asp:Label>
                      </div>
                   <div style="float:right;vertical-align:top">
                         <a id="lnkHelp" href="#"  style="display:none" onclick="ShowHelp('Help/Acquisitioning-bookArrival.htm')"><img height="15" src="help.jpg" alt="Help" width="20" /></a></td>
              </div></div>
                           
                            
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                         <Triggers>

<asp:PostBackTrigger ControlID="cmdnext" />

</Triggers>

                                <ContentTemplate>
                        
							<TABLE id="Table1" class="table-condensed no-more-tables" >
								<TR>
									<TD  colSpan="4"><asp:label id="msglabel" runat="server"  CssClass="err"></asp:label></TD>
								</TR>
								<TR>
									<TD ><asp:label id="Label7" runat="server"  CssClass="span" Text="<%$ Resources:ValidationResources,LArrType %>"></asp:label></TD>
									<TD >
									
									
                                        <asp:RadioButton ID="singleindent" runat="server" AutoPostBack="True" CssClass="opt"
                                            GroupName="Type" Text="<%$ Resources:ValidationResources,RBIBased %>"  /></TD>
									<TD >
                                        <asp:RadioButton ID="giftindent" runat="server" AutoPostBack="True" CssClass="opt"
                                            GroupName="Type" Text="<%$ Resources:ValidationResources,RBGiftI %>"  /></TD>
									<TD ></TD>
								</TR>
								<TR>
									<TD ><asp:label id="Label5" runat="server"  CssClass="span" Text="<%$ Resources:ValidationResources,LChIN %>"></asp:label></TD>
									<TD ><INPUT class="txt10" onkeypress="disallowSingleQuote(this);" id="docno" onblur="this.className='blur'"
											
											onfocus="this.className='focus'" type="text" name="docno" runat="server" size="15">
										<asp:label id="Label29" runat="server"  CssClass="star" >*</asp:label></TD>
									<TD ><asp:label id="Label10" runat="server"  CssClass="span" Text="<%$ Resources:ValidationResources,LciDate %>"></asp:label></TD>
									<TD >
                                        
                                       
                                        <%--pushpendra singh--%>
 <asp:TextBox ID="docarrivaldate" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
 <%--<ajax:CalendarExtender ID="CalendarExtender1" TargetControlID="docarrivaldate" Format="dd/MMM/yyyy" runat="server"></ajax:CalendarExtender>--%> 
 <%--pushpendra singh--%>
                                        
                                        
                                        
                                        
                                         
                                        
                                      <%--  <INPUT language="javascript" class="txt10" id="docarrivaldate" onblur="this.className='blur';checkdate1(this,document.Form1.hdCulture.value,document.Form1.hrDate.value)"
											
											onfocus="this.className='focus'" type="text" size="11" name="docarrivaldate"
											runat="server"><input id="btnDate" type="button" onclick="pickDate('docarrivaldate');" accesskey="D" style="background-position: center center; background-image: url(cal.gif); width: 23px; background-color: black; height: 21px;" />--%>





										<asp:label id="Label9" runat="server" CssClass="star" >*</asp:label></TD>
								</TR>
								<TR>
									<TD ><asp:label id="Label2" runat="server" CssClass="span"  Text="<%$ Resources:ValidationResources,LVen %>"></asp:label></TD>
									<TD colSpan="3" ><asp:dropdownlist id="cmbvendor" onblur="this.className='blur'" onfocus="this.className='focus'" runat="server"
											CssClass="txt10" AutoPostBack="True"  Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></asp:dropdownlist>
										<asp:label id="Label11" runat="server" CssClass="star" >*</asp:label></TD>
								</TR>
								<TR>
									<TD  ><asp:label id="Label3" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LOrd %>"></asp:label></TD>
									<TD ><asp:listbox id="lstorders" onblur="this.className='blur'" onfocus="this.className='focus'" runat="server"
											 CssClass="txt10" Height="50px"  AutoPostBack="True" ></asp:listbox></TD>
									<TD >
                                        <asp:Label ID="Label20" runat="server" CssClass="star" >*</asp:Label><asp:label id="Label4" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LInd %>"></asp:label></TD>
									<TD ><asp:listbox id="lstindents" Height="50px" onblur="this.className='blur'" onfocus="this.className='focus'"
											runat="server"  CssClass="txt10" AutoPostBack="True" ></asp:listbox>
                                        <asp:Label ID="Label21" runat="server" CssClass="star" >*</asp:Label></td>
								</TR>
								
								<TR>
									<TD><asp:label id="Label12" runat="server"   CssClass="head1" Text="<%$ Resources:ValidationResources,LArrDet %>"></asp:label></TD>
									 <td style="width:30%"></td>
                                    <td style="width:20%"></td>
                                    <td style="width:30%"></td>
								</TR>
								</table> 
                            <div class="allgriddiv " id="dvgrd" runat="server">                                  
                    <asp:HiddenField runat="server" ID="hdnGrdId" />
                            <asp:datagrid id="DataGrid1" runat="server" CssClass="allgrid" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>' AllowPaging="false" AutoGenerateColumns="False">
											
											<Columns>
												<asp:EditCommandColumn UpdateText="Update" HeaderText="<%$ Resources:ValidationResources, GrEdit %>" CancelText="Cancel"
													EditText="Edit">
													
												</asp:EditCommandColumn>
												<asp:TemplateColumn HeaderText="<%$ Resources:ValidationResources, LTitle %>">
													
													<ItemTemplate>
														<asp:Label id=Label1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.title") %>'>
														</asp:Label>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox id=txtbooktitle onblur="this.className='blur'" onfocus="this.className='focus'" runat="server" BorderWidth="1px" Width="161px" Text='<%# DataBinder.Eval(Container, "DataItem.title") %>'>
														</asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
                                                <asp:BoundColumn DataField="volumeno" ReadOnly="True"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="Vpart" ReadOnly="True" Visible="False"></asp:BoundColumn>
												<asp:BoundColumn DataField="orderedcopies" ReadOnly="True" HeaderText="<%$ Resources:ValidationResources, GrOC %>">
													
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="<%$ Resources:ValidationResources, GrCA %>">
													
													<ItemTemplate>
														<asp:Label id=lblcurrarrcopies runat="server" Width="76px" Text='<%# DataBinder.Eval(Container, "DataItem.currarrcopies") %>'>
														</asp:Label>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox id=txtcurrarrcopies onkeypress="IntegerNumber(this);" onblur="this.className='blur'" onfocus="this.className='focus'" runat="server" Width="57px" Text='<%# DataBinder.Eval(Container, "DataItem.currarrcopies") %>' BorderWidth="1px">
														</asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="alreadyarrcopies" ReadOnly="True" HeaderText="<%$ Resources:ValidationResources, GrAAC %>">
													</asp:BoundColumn>
												<asp:BoundColumn DataField="oppc" ReadOnly="True" HeaderText="<%$ Resources:ValidationResources, GrOPCpy %>">
													
												</asp:BoundColumn>
												<asp:BoundColumn DataField="currency" ReadOnly="True" HeaderText="<%$ Resources:ValidationResources, LCurrency %>">
													
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="<%$ Resources:ValidationResources, GrAPCpy %>">
													
													<ItemTemplate>
														<asp:Label id=lblappc runat="server" Width="97px" Text='<%# DataBinder.Eval(Container, "DataItem.appc") %>'>
														</asp:Label>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox id=txtappc onkeypress="decimalNumber(this);" onblur="this.className='blur'" onfocus="this.className='focus'" runat="server" Width="57px" Text='<%# DataBinder.Eval(Container, "DataItem.appc") %>' BorderWidth="1px">
														</asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn Visible="False" DataField="indentnumber" ReadOnly="True" HeaderText="<%$ Resources:ValidationResources, LINumber %>">
												
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="<%$ Resources:ValidationResources, GrPrcP %>">
													
													<ItemTemplate>
														<asp:CheckBox id="chksearch" runat="server" Font-Size="XX-Small" Width="92px"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn Visible="False" DataField="currencycode" HeaderText="<%$ Resources:ValidationResources, GrCurCd %>"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="indent" HeaderText="<%$ Resources:ValidationResources, GrIndt %>" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="yearofedition" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="yearofPublication" Visible="False"></asp:BoundColumn>
											</Columns>											
										</asp:datagrid>
                                </div>
                            <table class="no-more-tables table-condensed" >
								 
                                <tr>
                                    <td colspan="2" >
                                      <asp:CheckBox id="CheckBox1" runat="server" Text="<%$ Resources:ValidationResources,ChkAcc %>" CssClass="span" AutoPostBack="True"></asp:CheckBox><br/>
                                        <asp:radiobutton id="acession" runat="server"  GroupName="r" Text="<%$ Resources:ValidationResources,AccSAPfx %>" AutoPostBack="True" CssClass="opt"></asp:radiobutton><br />                                     
                                        <asp:RadioButton ID="optWOP" runat="server" AutoPostBack="True" GroupName="r" Text="<%$ Resources:ValidationResources,AccWOutAP%>" CssClass="opt" /><br />
                                        <asp:RadioButton ID="optManual" runat="server" AutoPostBack="True" GroupName="r" Text="<%$ Resources:ValidationResources,RBManAccNoEntry %>" CssClass="opt" /></br>
                                          <asp:radiobutton id="Manualaccession" runat="server"  GroupName="r" Visible ="false"  Text="<%$ Resources:ValidationResources,AccBCry %>" AutoPostBack="True" CssClass="opt"></asp:radiobutton><br />
                                    </td>
                                    <td colspan="2" >
                                        
                                        <asp:Label ID="Label6" runat="server" CssClass="note" ForeColor="Green"  Text="<%$ Resources:ValidationResources,OrdCopies %>"></asp:Label></br>
                                        <asp:Label ID="Label81" runat="server" CssClass="note" ForeColor="Green"  Text="<%$ Resources:ValidationResources,CrrArr %>"></asp:Label></br>
                                        <asp:Label ID="Label15" runat="server" CssClass="note" ForeColor="Green"  Text="<%$ Resources:ValidationResources,AlArrCopies %>"></asp:Label></br>
                                        <asp:Label ID="Label16" runat="server" CssClass="note"  ForeColor="Green" Text="<%$ Resources:ValidationResources,OrdPrcPerCpy %>"></asp:Label></br>
                                        <asp:Label ID="Label17" runat="server" CssClass="note" ForeColor="Green"  Text="<%$ Resources:ValidationResources,AcPrcPerCpy %>"></asp:Label></br></td>
                                </tr>
                                <tr>
                                    <td colspan="4" ><asp:Label ID="Label13" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources, AccPfx %>"></asp:Label>
                                        <asp:DropDownList ID="cmbprefix" runat="server" AutoPostBack="True" CssClass="txt10" Font-Names="<%$ Resources:ValidationResources, TextBox1 %>"   >
                                        </asp:DropDownList>
                                        <input id="txtstartno" runat="server" class="txt10" language="javascript" name="docarrivaldate" onblur="this.className='blur'" onchange="return docarrivaldate_onfocus()" onfocus="this.className='focus'" type="text"/>
                                        </input>
                                    </td>
                                </tr>
                                <tr>
                                    <td><asp:CheckBox ID="chkApplyRange" runat="server" AutoPostBack="True" CssClass="opt" Text="<%$ Resources:ValidationResources, applyrange %>"/>
                                    </td>
                                    <td>
                                        <table style="margin:0px;padding:0px">
                                            <tr>
                                                <td>
                                        <asp:Label ID="lblaccrange" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,AccPfx %>" ></asp:Label>
                                        </td>
                                    <td>
                                        <asp:DropDownList ID="cmbaccrange" runat="server" CssClass="txt10" Font-Names="<%$ Resources:ValidationResources, TextBox1 %>"   onblur="this.className='blur'" onfocus="this.className='focus'" >
                                        </asp:DropDownList>
                                    </td>
                                            </tr>
                                        </table>
                                    </td>
                                    
                                    <td colspan="2" ></td>
                                   
                                </tr>
                                <tr>
                                    <td colspan="4" ><asp:Label ID="Label22" runat="server" CssClass="span" Visible="False"></asp:Label>
                                    </td>
                                   
                                </tr>
                                <tr>
                                    <td colspan="4" ><input id="txtaccession" runat="server" class="txt10" name="txtstartno" onblur="this.className='blur'"
                                            onfocus="this.className='focus'" onkeypress="txtaccession_OnKeyPress();"
                                          type="text" /></td>
                                   
                                </tr>
								<TR>
									<TD colSpan="4">
                                        <asp:CheckBox ID="chkCataloging" runat="server" AutoPostBack="True" CssClass="opt" Text="<%$ Resources:ValidationResources,LBcheck %>" />
                                    </TD>
								</TR>
                                <tr>
                                    <td>
                                      <asp:Label ID="Label8" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,Clasno %>" ></asp:Label>
                                    </td>
                                    <td>
                                        <input id="txtClassNo" runat="server" class="txt10" name="docno" onblur="this.className='blur'" onfocus="this.className='focus'" onkeypress="disallowSingleQuote(this);" type="text"/>
                                        </input>
                                    </td>
                                    <td >
                                        <asp:Label ID="Label14" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,BookNo %>"></asp:Label>
                                    </td>
                                    <td >
                                        <input id="txtBookNo" runat="server" class="txt10" name="docno" onblur="this.className='blur'" onfocus="this.className='focus'" onkeypress="disallowSingleQuote(this);" type="text" />
                                        </input>
                                    </td>
                                   
                                </tr>
                                <tr>
                                    <td>
                                       <asp:Label ID="Label18" runat="server" CssClass="span"  Text="<%$ Resources:ValidationResources,LItemSt %>"></asp:Label></td>
                                    <td >
                                        <asp:DropDownList ID="cmbStatus" runat="server" AutoPostBack="True" CssClass="txt10" Font-Names="<%$ Resources:ValidationResources, TextBox1 %>"  >
                                        </asp:DropDownList>
                                    </td>
                                    <td></td>
                                    <td></td>
                                    
                                </tr>
                                <tr>
                                    <td >
                                     <asp:Label ID="Label19" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LBaDa %>"></asp:Label>
                                    </td>
                                    <td >
                                        <asp:TextBox ID="txtrelease" runat="server" BorderStyle="Solid" BorderWidth="1px" CssClass="txt10"  onblur="this.className='blur';checkdate1(this,document.Form1.hdCulture.value,document.Form1.hrDate.value)" onfocus="this.className='focus'" Visible="False" ></asp:TextBox>
                                        <input id="Button1" runat="server" accesskey="D" onclick="pickDate('txtrelease');"
                                            style="background-position: center center; background-image: url(cal.gif); 
                                            background-color: black" type="button" />
                                    </td>
                                    <td ></td>
                                    <td ></td>
                                    
                                </tr>
                                <tr>
                                    
                                  <td colspan="4" style="text-align:center" >
                                        <asp:Label ID="Label23" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources, GrPrefix %>" Visible="False" Width="1px"></asp:Label>
                                    
                                     
                                                    <input id="cmdupdate" runat="server" class="btn btn-primary" name="cmdupdate" type="button" value="<%$Resources:ValidationResources,bsave %>">
                                           
                                                    <input id="cmdreset" runat="server" class="btn btn-primary" name="cmdreset"  type="button" value="<%$Resources:ValidationResources,bReset %>">
                                                   
                                                    <input id="cmdnext" runat="server" class="btn btn-primary" name="cmdnext"  type="button" value="<%$Resources:ValidationResources,bInv %>">
                                                   
                                    </td>
                                  
                                </tr>
                               
							</TABLE>
						
						 </ContentTemplate>
                            </asp:UpdatePanel>                          
						</div>
             <input id="book" runat="server" name="Hidden6" size="1"  type="hidden"/>
                                            <input id="hdGrn" runat="server" name="hdGrn" size="1"  type="hidden"/>
            <input id="hdUnableMsg" runat="server" style="width: 10px" type="hidden"/>
                                            <input id="hdNoofMembers" runat="server" type="hidden" style="width: 9px" />
                                            <input id="Hdmessage" runat="server" name="Hidden6" style="WIDTH: 9px; HEIGHT: 14px" type="hidden"/>
                                                <input id="HdBookAlert" runat="server" designtimedragdrop="321" name="HdBookAlert" style="WIDTH: 14px; HEIGHT: 14px" type="hidden"/>
                                                    <input id="boktitle" runat="server" style="width: 10px;" type="hidden" />
                                                    <input id="hdAcce" style="width: 5px" type="hidden" />
                                                    <input id="Hidden7" runat="server" style="width: 11px" type="hidden"/>
                                                        <input id="HComboSelect" runat="server"
                            type="hidden" value="<%$ Resources:ValidationResources, ComboSelect %>" style="width: 9px" />
                                                        <input id="hrDate" runat="server"
                            type="hidden" value="<%$ Resources:ValidationResources, dateFormat1 %>" style="width: 13px" />
                                                        <input id="js1" runat="server"
                            type="hidden" value="<%$ Resources:ValidationResources, js1 %>" style="width: 13px" />
                                                        <input id="hdCulture" runat="server" style="width: 10px" type="hidden" />
                                                        <input id="xCoordHolder" runat="server" name="Hidden2" style="WIDTH: 12px; HEIGHT: 22px" type="hidden"/>
                                                            <input id="hdaccession" runat="server" style="width: 8px" type="hidden" />
                                                            <input id="hdLanguage" runat="server" style="width: 10px" type="hidden" />
                                                            <input id="hddepartment" runat="server" style="width: 12px" type="hidden" />
                                                            <input id="hdCountry" runat="server" style="width: 9px" type="hidden" />
                                                            <input id="hdState" runat="server" style="width: 9px" type="hidden" />
                                                            <input id="hdcity" runat="server" style="width: 9px" type="hidden" />
                                                            <input id="hdAddress" runat="server" style="width: 10px" type="hidden" />
                                                            <input id="hdFirst" runat="server" style="width: 8px" type="hidden" />
                                                            <INPUT id="hdIsMarc21" type="hidden" name="hdIsMarc21" runat="server"/>
                                                            <input id="hdCtrlNo" runat="server" style="width: 7px" type="hidden" />
                                                            <input id="Hdexistaccession" runat="server" style="width: 9px" type="hidden" />
                                                            <input id="txtactualamt" runat="server" name="txtactualamt" style="WIDTH: 10px; HEIGHT: 22px" type="hidden"/>
                                                                <input id="Text1" runat="server" name="Text1" style="WIDTH: 9px; HEIGHT: 22px" type="hidden"/>
                                                                    <input id="Hidden5" runat="server" name="Hidden2" style="WIDTH: 9px; HEIGHT: 22px" type="hidden">
                                                                        <input id="Hidden1" runat="server" name="Hidden1" style="WIDTH: 9px; HEIGHT: 16px" type="hidden">
                                                                            <input id="Hidden4" runat="server" name="Hidden1" style="WIDTH: 8px; HEIGHT: 22px" type="hidden">
                                                                                <input id="Hidden3" runat="server" name="Hidden3" size="1" style="WIDTH: 8px; HEIGHT: 22px" type="hidden">
                                                                                    <input id="yCoordHolder" runat="server" name="Hidden2" size="1" style="WIDTH: 2px; HEIGHT: 22px" type="hidden">
                                                                                        <input id="txtdepartmentcode" runat="server" name="Hidden2" onblur="this.className='blur'" onfocus="this.className='focus'" size="1" style="WIDTH: 6px; HEIGHT: 22px" type="hidden">
                                                                                            <input id="hdOnlineP" runat="server" style="width: 14px" type="hidden" />
                                                                                       


                       			<INPUT id="Hd_orderno"  type="hidden" size="1" name="Hidden2"
											runat="server">
                                        <input id="hd_edit" runat="server"  type="hidden" />
                                        <input id="hd_arrcopy" runat="server"  type="hidden" />			
						<TABLE class="plain" id="Table4" style ="border:none;border-style:none ; ">
							<TR>
								<TD><asp:requiredfieldvalidator id="docnovalidator1" runat="server" Height="4px"  Font-Names="Arial"
										ControlToValidate="docno" Display="None" ErrorMessage="<%$ Resources:ValidationResources, EInvN %>" SetFocusOnError="True"></asp:requiredfieldvalidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtrelease"
                                        Display="None" ErrorMessage="<%$ Resources:ValidationResources, ReqBarDate %>" Font-Size="11px" Height="14px" SetFocusOnError="True"
                                        Width="40px"></asp:RequiredFieldValidator></TD>
							</TR>
							<TR>
								<TD style="height: 20px"><asp:requiredfieldvalidator id="dateValidator1" runat="server" ControlToValidate="docarrivaldate" Display="None"
										ErrorMessage="<%$ Resources:ValidationResources, EInvD %>" SetFocusOnError="True"></asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD>
<%--                                    <asp:customvalidator id="vendorCustomValidator1" runat="server" Width="136px" Height="16px" Font-Size="11px"
										ControlToValidate="cmbvendor" Display="None" ErrorMessage="<%$ Resources:ValidationResources, IvVen %>" ClientValidationFunction="comboValidation" SetFocusOnError="True"></asp:customvalidator>--%>

								</TD>
							</TR>
							<TR>
								<TD><asp:requiredfieldvalidator id="orderRequiredFieldValidator2" runat="server" Width="112px" Height="32px" Font-Size="11px"
										ControlToValidate="lstorders" Display="None" ErrorMessage="<%$ Resources:ValidationResources, SlctOdr %>" SetFocusOnError="True"></asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD><asp:requiredfieldvalidator id="indentRequiredFieldValidator1" runat="server" Width="102px" Height="24px" Font-Size="11px"
										ControlToValidate="lstindents" Display="None" ErrorMessage="<%$ Resources:ValidationResources, SlctIndt %>" SetFocusOnError="True"></asp:requiredfieldvalidator></TD>
							</TR>
						</TABLE>
                        
			<asp:validationsummary id="ValidationSummary1" style="Z-INDEX: 100; LEFT: 767px; POSITION: absolute; TOP: 292px"
				runat="server" Width="96px" Height="24px" Font-Size="11px" DisplayMode="List" ShowMessageBox="True"
				ShowSummary="False"></asp:validationsummary><INPUT id="txtaccid" onblur="this.className='blur'" style="Z-INDEX: 101; LEFT: 584px; WIDTH: 16px; POSITION: absolute; TOP: 168px; HEIGHT: 22px"
				onfocus="this.className='focus'" type="hidden" size="1" name="Hidden2" runat="server"><INPUT id="Hidden2" style="Z-INDEX: 102; LEFT: 583px; WIDTH: 16px; POSITION: absolute; TOP: 216px; HEIGHT: 22px"
				type="hidden" size="1" name="Hidden2" runat="server">
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

