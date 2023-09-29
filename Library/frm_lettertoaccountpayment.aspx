<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/LibraryMain.master" CodeBehind="frm_lettertoaccountpayment.aspx.cs" Inherits="Library.frm_lettertoaccountpayment" %>
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
                      <asp:label id="lblTitle" runat="server" style="display:none" Width="100%" >Recommendation to Account for Payment</asp:label>
                      </div>
                   <div style="float:right;vertical-align:top">
                        <a id="lnkHelp" href="#"  style="display:none" onclick="ShowHelp('Help/Acq-Payment-frmletterToAcc.htm')"><img src="help.jpg" alt="Help" style="height: 16px"/></a>
                </div></div>
        
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <Triggers><asp:PostBackTrigger ControlID="cmdprint" /></Triggers>
                            <ContentTemplate>
						<div class="no-more-tables" style="width:100%">
                            <TABLE id="Table1" class="table-condensed" >
								<TR>
									<TD colSpan="4" >
										<asp:label id="msglabel" runat="server" CssClass="err" ></asp:label></TD>
								</TR>
                                <TR>
									<TD style="width:20%"><asp:label id="Label4" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LVen %>"></asp:label></TD>
									<TD  colSpan="3">
										<asp:dropdownlist id="cmbvendor" onblur="this.className='blur'" onfocus="this.className='focus'" runat="server"
										  AutoPostBack="True" CssClass="txt10" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></asp:dropdownlist>
										<asp:label id="Label5" runat="server" CssClass="star" Font-Size="11px">*</asp:label></TD>
								</TR>
								<TR>
									<TD><asp:label id="Label13" runat="server" CssClass="span" DESIGNTIMEDRAGDROP="32" Text="<%$ Resources:ValidationResources,LDocNo %>"></asp:label></TD>
                                    <TD  ><INPUT id="txtdocno" onblur="this.className='blur'" 
											onfocus="this.className='focus'"  type="text" size="21" name="txtdraft" runat="server" class="txt10">
									</TD>
									<TD>
										<asp:label id="Label11" runat="server" CssClass="head1"  DESIGNTIMEDRAGDROP="37" Text="<%$ Resources:ValidationResources,bInv %>"></asp:label></TD>
								<TD  rowspan="2" >
                                    <asp:listbox id="lstinvoice" onblur="this.className='blur'" onfocus="this.className='focus'" style="height:60px!important"
											runat="server"  AutoPostBack="True"  SelectionMode="Multiple" DESIGNTIMEDRAGDROP="46" CssClass="txt10" ></asp:listbox></TD>
                                </TR>
								<TR>
									<TD><asp:label id="Label2" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,PmtTyp %>"></asp:label></TD>
									<TD><SELECT id="Cmbpaymenttype" onblur="this.className='blur'"  
											onfocus="this.className='focus'" name="Cmbpaymenttype" runat="server" class="txt10">
											<OPTION></OPTION>
										</SELECT></TD>
									<td style="width:14%"></td>
									
								</TR>
								<TR>
									<TD><asp:label id="Label6" runat="server" CssClass="span"  Text="<%$ Resources:ValidationResources,PmtDt %>"></asp:label></TD>
									<TD>
										 <asp:TextBox ID="txtpaymentdate" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
										</TD> 
									<td></td>
									<td style="width:33%"></td>
								</TR>
								
								
								<TR>
									<TD colSpan="4" >
										<asp:TextBox id="txtpayid" runat="server"  Visible="False"></asp:TextBox>
										<asp:TextBox id="txtvendorid" runat="server"  Visible="False"></asp:TextBox></TD>
									
								</TR>
								</table></div>
                            <div class="allgriddiv" id="dvgrd" runat="server">                                  
                    <asp:HiddenField runat="server" ID="hdnGrdId" />
                            <asp:datagrid id="grdinvoice" runat="server"  CssClass="allgrid" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>' AutoGenerateColumns="False">
								<Columns>
												<asp:EditCommandColumn UpdateText="Update" HeaderText="<%$ Resources:ValidationResources,GrEdit %>" CancelText="Cancel"
													EditText="Edit">
                                               </asp:EditCommandColumn>
												<asp:BoundColumn DataField="invoicenumber" ReadOnly="True" HeaderText="<%$ Resources:ValidationResources,LInvNum %>"></asp:BoundColumn>
												<asp:BoundColumn DataField="billno" ReadOnly="True" HeaderText="<%$ Resources:ValidationResources,GrBilSN %>"></asp:BoundColumn>
												<asp:BoundColumn DataField="amount" HeaderText="<%$ Resources:ValidationResources,Amt %>" DataFormatString="{0:F}">
													<HeaderStyle Width="40px"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn Visible="False" DataField="id" HeaderText="<%$ Resources:ValidationResources,GrId %>"></asp:BoundColumn>
												<asp:BoundColumn DataField="paycurrency" ReadOnly="True" HeaderText="<%$ Resources:ValidationResources,LCurrency %>"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="amount" ReadOnly="True" HeaderText="<%$ Resources:ValidationResources,GrChkAmt %>"></asp:BoundColumn>
											</Columns>											
										
							</asp:datagrid>
                                </div>
								 <table class="no-more-tables table-condensed" >
								 
								<TR>
									<TD ></TD>
									<TD class="aligntxt">
										<asp:label id="l1" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LTotAmount %>"></asp:label></TD>
									<TD ><INPUT id="txtamount" onblur="this.className='blur'" 
											onfocus="this.className='focus'"  type="text" size="14" name="txtamount" runat="server" class="txt10"></TD>
									
								</TR>
									 <TR>
									<TD  colSpan="3" style="text-align:center">
									<INPUT id="cmdsave"  type="button" value="<%$Resources:ValidationResources,bSave %>" name="cmdsave"
														runat="server" class="btnstyle">
												<INPUT id="cmdreset"  type="button" value="<%$Resources:ValidationResources,bReset %>" name="cmdreset"
														runat="server" class="btnstyle">
												<INPUT id="cmddelete"  onclick="if (DoConfirmation() == false) return false;"
														type="button" value="<%$Resources:ValidationResources,bDelete %>" name="cmddelete" runat="server" class="btnstyle">
												<INPUT id="cmdprint"  type="button" value="<%$Resources:ValidationResources,iPrint %>" name="cmdreset"
														runat="server" visible="true" class="btnstyle">
													
														    <asp:Button ID="cmdemail" runat="server" CssClass="btnstyle"
                                                                text="<%$Resources:ValidationResources,bSendMail %>" />
													
											</TD>
								</TR>
									 	<TR>
									<TD colSpan="3" >
										<hr />
										<asp:checkbox id="chkSearch" runat="server" CssClass="opt" AutoPostBack="True"
											Text="<%$Resources:ValidationResources,bSearch %>"></asp:checkbox>
									</TD>
								</TR>
								<TR>
									<TD style="vertical-align:top">
										<asp:label id="lblDocumentNo" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LDocNo %>"></asp:label></TD>
									<TD  colSpan="2" ><INPUT onkeypress="disallowSingleQuote(this);" id="txtCategory" onblur="this.className='blur'"
											
											onfocus="this.className='focus'" type="text" size="20" name="txtCategory" runat="server" class="txt10"><INPUT id="btnCategoryFilter"  type="button" value="<%$Resources:ValidationResources,bSearch %>"
											name="btnCategoryFilter" runat="server" class="btnstyle"></TD>
								</TR>
									 <TR>
									<TD style="width:20%">
                                    </TD>
									<TD  colSpan="2">
										<asp:listbox id="lstAllCategory" onblur="this.className='blur'" AutoPostBack="true"  onfocus="this.className='focus'" runat="server"   CssClass="txt10" style="height:60px!important" ></asp:listbox></TD>
								</TR>
									 </table>
                                <INPUT id="Hidden3" 
				type="hidden" size="1" name="Hidden3" runat="server"><INPUT id="Hidden5" 
				type="hidden" size="1" name="Hidden2" runat="server">
            <INPUT id="txtchange" style="WIDTH: 40px; HEIGHT: 22px" type="hidden" size="1" name="txtchange"
											runat="server"><INPUT id="Hidden6" type="hidden" name="Hidden6" runat="server">
								<asp:RequiredFieldValidator ID="dateValidator1" runat="server" ControlToValidate="txtpaymentdate"
                                            Display="None" ErrorMessage="<%$Resources:ValidationResources,VEPayD %>" SetFocusOnError="True"></asp:RequiredFieldValidator><INPUT id="hdletter" style="WIDTH: 8px; HEIGHT: 22px" type="hidden" size="1" name="Hidden9"
											runat="server" DESIGNTIMEDRAGDROP="64"><input id="Hdgridaction" runat="server" style="width: 7px" type="hidden" /><INPUT id="Hidden2" style="WIDTH: 24px; HEIGHT: 8px" type="hidden" size="1" name="Hidden2"
							runat="server"><INPUT id="Hidden1" style="WIDTH: 22px; HEIGHT: 24px" type="hidden" size="1" name="Hidden1"
							runat="server"><INPUT id="Hidden4" style="WIDTH: 14px; HEIGHT: 27px" type="hidden" size="1" name="Hidden1"
							runat="server"><INPUT id="yCoordHolder" style="WIDTH: 24px; HEIGHT: 16px" type="hidden" size="1" name="yCoordHolder"
							runat="server"><INPUT id="xCoordHolder" style="WIDTH: 16px; HEIGHT: 16px" type="hidden" size="1" name="xCoordHolder"
							runat="server"><input id="HComboSelect" runat="server" 
                            type="hidden" value="<%$ Resources:ValidationResources, ComboSelect %>" style="width: 16px" /><input id="hrDate" runat="server" 
                            type="hidden" value="<%$ Resources:ValidationResources, dateFormat1 %>" style="width: 16px" /><input id="js1" runat="server" 
                            type="hidden" value="<%$ Resources:ValidationResources, js1 %>" style="width: 16px" /><input id="hdCulture" runat="server" style="width: 16px" type="hidden" /><INPUT id="hSubmit1" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" value="0"
							name="hSubmit1" runat="server"><INPUT id="Hidden17" type="hidden" runat="server" style="width: 16px">
						</ContentTemplate>
                            </asp:UpdatePanel>
		</div>
	<style>
	 .aligntxt
	 {
		 text-align:right;
	 }
	 @media only screen (max-width:600px)
	 {
		.aligntxt
	 {
		 text-align:left;
	 } 
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

        $("[id$=txtpaymentdate]").datepicker({

            changeMonth: true,//this option for allowing user to select month
            changeYear: true, //this option for allowing user to select from year range
            dateFormat: 'dd-M-yy'  // CHANGE DATE FORMAT.
        });

    }

</script>
	<style>
        SPAN.head1
        {
            font-weight:normal!important;
            font-size:16px!important;
            color:black!important;

        }
    </style>
</asp:Content>

