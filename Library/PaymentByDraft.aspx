<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/LibraryMain.master" CodeBehind="PaymentByDraft.aspx.cs" Inherits="Library.PaymentByDraft" %>
<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>
<asp:Content ID="Head" runat="server" ContentPlaceHolderID="head">
	        <link href="cssDesign/libresponsive.css" rel="stylesheet" type="text/css" />
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
                  <div style="width:89%;float:left" > &nbsp;
			<asp:label id="lblTitle" runat="server" style="display:none"  Width="100%" Font-Bold="True" >Payment Processing</asp:label>
                      </div>
                  <div style="float:right;vertical-align:top">
                        <a id="lnkHelp" href="#" style="display:none"  onclick="ShowHelp('Help/Acq-Payment-ByDraft.htm')"><img src="help.jpg" alt="Help" style="height: 16px"/></a>
               </div></div>
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <Triggers><asp:PostBackTrigger ControlID="cmdprint" /></Triggers>
                                <ContentTemplate>
						 <div class="no-more-tables" style="width:100%">
						<TABLE id="Table1"  class="table-condensed">
							<TR>
								<TD colSpan="4"><asp:label id="msglabel" runat="server" CssClass="err" ></asp:label></TD>
							</TR>
							<TR>
								<TD ><asp:label id="Label4" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LVen %>"></asp:label></TD>
								<TD  colSpan="3"><asp:dropdownlist id="cmbvendor" onblur="this.className='blur'" onfocus="this.className='focus'" runat="server"
										 CssClass="txt10" AutoPostBack="True"  Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></asp:dropdownlist>
                                    <asp:label id="Label10" runat="server"  CssClass="star"  Font-Size="11px">*</asp:label></TD>
							</TR>
							<TR>
								<TD ><asp:label id="Label2" runat="server"  CssClass="span" Text="<%$ Resources:ValidationResources,PmtTyp %>"></asp:label></TD>
								<TD ><asp:dropdownlist id="Cmbpaymenttype" onblur="this.className='blur'" onfocus="this.className='focus'" runat="server"
										CssClass="txt10" AutoPostBack="True"  Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'>
                                </asp:DropDownList>
                                    <asp:label id="Label1" runat="server" CssClass="star"  Font-Size="11px">*</asp:label></TD>
								<TD><asp:label id="Label13" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LDocNo %>" ></asp:label></TD>
								<TD ><INPUT class="txt10" onkeypress="IntegerNumber(this);" id="txtdocno" onblur="this.className='blur'"
										
										onfocus="this.className='focus'"  type="text" name="txtdraft" runat="server"></TD>
							</TR>
                            <TR>
								<TD ><asp:label id="Label6" runat="server"  CssClass="span" Text="<%$ Resources:ValidationResources,PmtDt %>"></asp:label></TD>
								<TD>
                                     <asp:TextBox ID="txtpaymentdate" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                    </TD>
								<TD><asp:label id="Label11" runat="server" CssClass="head1" Text="<%$ Resources:ValidationResources,bInv %>"></asp:label></TD>
							<TD rowSpan="3"><asp:listbox id="lstinvoice" onblur="this.className='blur'" onfocus="this.className='focus'"
										runat="server" AutoPostBack="True"  SelectionMode="Multiple" CssClass="txt10" style="height:80px!important" ></asp:listbox></TD>
							</TR>
							<TR>
								<TD ><asp:label id="Label9" runat="server" CssClass="span"  Text="<%$ Resources:ValidationResources,LAmt%>"></asp:label></TD>
								<TD><INPUT class="txt10" onkeypress="decimalNumber(this);" id="txtdraftamt" onblur="this.className='blur'"
										
										onfocus="this.className='focus'" type="text" name="txtdraft" runat="server" maxlength="15"></TD>
								<td></td>
							</TR>
							<TR>
								<TD ><asp:label id="Label7" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LDftDt%>"></asp:label></TD>
								<TD>
									<asp:TextBox ID="txtdraftdate" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
									
                                    <asp:Label ID="Label16" runat="server" CssClass="star" Font-Size="11px" 
                                        >*</asp:Label></TD>
								<td></td>
							</TR>
							<TR>
								<TD ><asp:label id="Label3" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LDftNo%>"></asp:label></TD>
								<TD><INPUT class="txt10" onkeypress="IntegerNumber(this);" id="txtdraft" onblur="this.className='blur'"
										 
										onfocus="this.className='focus'" type="text" name="txtdraft" runat="server" maxlength="12">
                                    <asp:Label ID="Label15" runat="server" CssClass="star" Font-Size="11px" 
                                       >*</asp:Label></TD>
								<td colspan="2"></td>
							</TR>
							<TR>
								<TD ><asp:label id="Label5" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,PAtBank%>"></asp:label></TD>
								<TD colSpan="3"><INPUT class="txt10" id="txtbank" onblur="this.className='blur'"  
										onfocus="this.className='focus'" type="text" maxLength="50" name="txtbank" runat="server">
                                    <asp:Label ID="Label17" runat="server" CssClass="star" Font-Size="11px" 
                                       >*</asp:Label></TD>
							</TR>
							<TR>
								<TD colSpan="4"><asp:label id="Label12" runat="server" CssClass="head1" Text="<%$ Resources:ValidationResources,InvDtl%>"></asp:label></TD>
								</TR>
							</table></div>
                                    <div class="allgriddiv" id="dvgrd" runat="server">                                  
                    <asp:HiddenField runat="server" ID="hdnGrdId" />
                              <asp:datagrid id="grdinvoice" runat="server" CssClass="allgrid" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'>
										
										<Columns>
											<asp:EditCommandColumn UpdateText="Update" HeaderText="<%$ Resources:ValidationResources, GrEdit %>" CancelText="Cancel"
												EditText="Edit">
												
											</asp:EditCommandColumn>
											<asp:BoundColumn DataField="invoicenumber" ReadOnly="True" HeaderText="<%$ Resources:ValidationResources, LInvNum %>">
												
											</asp:BoundColumn>
											<asp:BoundColumn DataField="billno" ReadOnly="True" HeaderText="<%$ Resources:ValidationResources, GrBilSN %>">
												
											</asp:BoundColumn>
											<asp:BoundColumn DataField="amount" HeaderText="<%$ Resources:ValidationResources, Amt %>"  DataFormatString="{0:F}">
											</asp:BoundColumn>
											<asp:BoundColumn Visible="False" DataField="id" HeaderText="<%$ Resources:ValidationResources, GrId %>"></asp:BoundColumn>
											<asp:BoundColumn DataField="paycurrency" ReadOnly="True" HeaderText="<%$ Resources:ValidationResources, LCurrency %>">
												
											</asp:BoundColumn>
											<asp:BoundColumn Visible="False" DataField="amount" ReadOnly="True" HeaderText="<%$ Resources:ValidationResources, GrChkAmt %>"></asp:BoundColumn>
										</Columns>										
									</asp:datagrid>
                          </div>
									 <table class="no-more-tables table-condensed" >
							<TR>
								<TD  colSpan="2" ></TD>
								<TD style="text-align:right">
									<asp:label id="l1" runat="server" CssClass="span"  Text="<%$ Resources:ValidationResources,LTotAmount %>"></asp:label></TD>
								<TD ><INPUT class="txt10" id="txtamount" onblur="this.className='blur'" 
										onfocus="this.className='focus'"  type="text" name="txtamount" runat="server"></TD>
							</TR>
							
							<TR>
								<TD  colSpan="4">
									<asp:textbox id="txtpayid" runat="server" Visible="False"></asp:textbox><asp:textbox id="txtvendorid" runat="server"  Visible="False"></asp:textbox></TD>
								</TR>
							
							<TR>
								<TD  colSpan="4" style="text-align:center" >
									<INPUT id="cmdsave"  type="button" value="<%$Resources:ValidationResources,bSave %>" name="cmdsave"
													runat="server" class="btnstyle">
									<INPUT id="cmdreset"  type="button" value="<%$Resources:ValidationResources,bReset %>" name="cmdreset"
													runat="server" causesvalidation="false" class="btnstyle">
											<INPUT id="cmddelete"  onclick="if (DoConfirmation() == false) return false;"
													type="button" value="<%$Resources:ValidationResources,bDelete %>" name="cmddelete" runat="server" class="btnstyle">
											<INPUT id="cmdprint"  type="button" value="<%$Resources:ValidationResources,iprint %>" name="cmdreset"
													runat="server" class="btnstyle">
												
                                                        <asp:Button ID="cmdemail" runat="server" CssClass="btnstyle"
                                                            text="<%$Resources:ValidationResources,bSendMail %>" />
									<hr />
                                           
								</TD>
							</TR>
							 
							<tr>
								<TD  colSpan="2" >
                                    <asp:checkbox id="chkSearch" runat="server" CssClass="opt" AutoPostBack="True"
										 Text="<%$Resources:ValidationResources,bSearch %>"></asp:checkbox>
                                    <asp:label id="lblIndentNo" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LDocNo %>"></asp:label></TD>
								<TD ><INPUT class="txt10" onkeypress="disallowSingleQuote(this);" id="txtCategory" onblur="this.className='blur'"
										
										onfocus="this.className='focus'" type="text" size="20" name="txtCategory" runat="server"></TD>
								<td> 
									<INPUT id="btnCategoryFilter" type="button" value="<%$Resources:ValidationResources,bSearch %>" name="btnCategoryFilter" runat="server" class="btnstyle">
									</td>
							</tr>
							<tr
								<td  colSpan="2" ></td>
								<td colSpan="2" ><asp:listbox id="lstAllCategory" AutoPostBack="true" onblur="this.className='blur'" onfocus="this.className='focus'"
										tabIndex="3" runat="server"  CssClass="txt10" ></asp:listbox></td>
							</tr>
						</TABLE>
</ContentTemplate>
			  </asp:UpdatePanel></div>
            <input id="hrDate" runat="server" 
                            type="hidden" value="<%$ Resources:ValidationResources, dateFormat1 %>" style="width: 16px"/><input id="js1" runat="server" 
                            type="hidden" value="<%$ Resources:ValidationResources, js1 %>" style="width: 8px" /><input id="hdCulture" runat="server" style="width: 16px" type="hidden" /><input id="HComboSelect" runat="server"
                            type="hidden" value="<%$ Resources:ValidationResources, ComboSelect %>" style="width: 16px" /><INPUT id="Hidden7" type="hidden" runat="server" style="width: 16px"><INPUT id="hdUnableMsg" type="hidden" runat="server" ><INPUT id="hCurrentIndex2" style="WIDTH: 16px; HEIGHT: 22px" type="hidden" name="hCurrentIndex2"
							runat="server"><INPUT id="hSubmit1" style="WIDTH: 8px; HEIGHT: 22px" type="hidden" value="0"
							name="hSubmit1" runat="server"><INPUT id="Hidden2" style="WIDTH: 16px; HEIGHT: 22px" type="hidden" value="0"
							name="hidden6" runat="server">

                                    <INPUT id="Hidden6" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" name="Hidden6"
										runat="server"><INPUT id="txtchangeval" style="WIDTH: 31px; HEIGHT: 22px" type="hidden" size="1" name="txtchangeval"
										runat="server"><INPUT id="hddocno" style="WIDTH: 19px; HEIGHT: 22px" type="hidden" size="1" name="txtchange"
										runat="server">
						<%--<INPUT id="Button1" style="WIDTH: 1px; HEIGHT: 1px" class="btnH" type="button" value="Button" name="Button1"
							runat="server">--%><INPUT id="Hidden1" style="WIDTH: 22px; HEIGHT: 24px"
				type="hidden" size="1" name="Hidden1" runat="server"><INPUT id="Hidden4" style="WIDTH: 14px; HEIGHT: 27px"
				type="hidden" size="1" name="Hidden1" runat="server"><INPUT id="Hidden3" style="WIDTH: 54px; HEIGHT: 12px"
				type="hidden" size="3" name="Hidden3" runat="server"><INPUT id="xCoordHolder" style="WIDTH: 16px; HEIGHT: 16px"
				type="hidden" size="1" name="Hidden2" runat="server"><INPUT id="yCoordHolder" style="WIDTH: 24px; HEIGHT: 8px"
				type="hidden" size="1" name="Hidden2" runat="server">
						 
				
                       
							<%--<INPUT id="cmdsearch" type="button" value="Display" name="cmdsearch" runat="server"><asp:customvalidator id="CustomValidatorforpayment" runat="server" Font-Size="11px" ErrorMessage="<%$ Resources:ValidationResources, VPlzSelPayT %>"
							ClientValidationFunction="comboValidation" ControlToValidate="Cmbpaymenttype" Display="None" SetFocusOnError="True"></asp:customvalidator></TD>--%>
							<INPUT id="Hidden5" style="Z-INDEX: 106; LEFT: 568px; WIDTH: 4px; POSITION: absolute; TOP: 280px; HEIGHT: 1px"
				type="hidden" size="1" name="Hidden2" runat="server">
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
                ForDataTable();
                            SetDatePicker();

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
            $("[id$=txtpaymentdate],[id$=txtdraftdate]").datepicker({
                changeMonth: true,//this option for allowing user to select month
                changeYear: true, //this option for allowing user to select from year range
                dateFormat: 'dd-M-yy'  // CHANGE DATE FORMAT.
            });

        }

    </script> 
</asp:Content>

				
                                    
