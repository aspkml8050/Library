<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Invoice.aspx.cs" MasterPageFile="~/LibraryMain.master" Inherits="Library.Invoice" %>
<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>


<asp:Content ID="Head" runat="server" ContentPlaceHolderID="head">
<%--          <link href="cssDesign/libresponsive.css" rel="stylesheet" type="text/css" >--%>
	<script type="text/javascript">


			function CalcA() {
		    if (document.getElementById("<%=txttotalamount.ClientID%>").value == "") {
		        document.getElementById("<%=txttotalamount.ClientID%>").value = 0;
		    }
		    if (document.getElementById("<%=txtdiscount.ClientID%>").value == "") {
		        document.getElementById("<%=txtdiscount.ClientID%>").value = 0;
		    }
		    if (document.getElementById("<%=txtdiscamt.ClientID%>").value == "") {
		        document.getElementById("<%=txtdiscamt.ClientID%>").value = 0;
		    }
		    if (document.getElementById("<%=txtpostage.ClientID%>").value == "") {
		        document.getElementById("<%=txtpostage.ClientID%>").value = 0;
		    }
		    var dAmt = document.getElementById("<%=txttotalamount.ClientID%>").value * document.getElementById("<%=txtdiscount.ClientID%>").value / 100;
		    document.getElementById("<%=txtdiscamt.ClientID%>").value = dAmt.toFixed(2);
		    if (document.getElementById("<%=txthandlingcharges.ClientID%>").value == "") {
		        document.getElementById("<%=txthandlingcharges.ClientID%>").value = 0;
		    }
		    var Tamt = document.getElementById("<%=txttotalamount.ClientID%>").value;
		    var Pamt = document.getElementById("<%=txtpostage.ClientID%>").value;
		    var Hamt = document.getElementById("<%=txthandlingcharges.ClientID%>").value;
		    var nAmt = Tamt * 1 + Pamt * 1 + Hamt * 1 - dAmt * 1;
		    //alert(Tamt);
		    //alert(Pamt);
		    //alert(Hamt);
		    //alert(dAmt);
		    //alert(nAmt);
		    document.getElementById("<%=txtnetamt1.ClientID%>").value = nAmt.toFixed(2);
		}
		function totAmt(totAmt) {
		    CalcA();
		}

	</script>

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
			<asp:label id="lblTitle" runat="server" style="display:none"  Width="100%">Invoice Entry</asp:label>
                      </div>
                   <div style="float:right;vertical-align:top">
                        <a id="lnkHelp" href="#"  style="display:none" onclick="ShowHelp('Help/Acq-Invoice.htm')"> <img height="15" alt="Help" src="help.jpg"  /></a>
               </div></div>
                           
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
						 <div class="no-more-tables" style="width:100%">   
              	<table id="Table1" class="table-condensed GenTable1"  style="border:0;">
							<tr>
								<td colspan="4"><asp:label id="msglabel" runat="server" CssClass="err" ></asp:label></TD>
							</tr>
							<tr>
								<td ><asp:label id="Label10" runat="server" Text="<%$ Resources:ValidationResources,InvType %>"></asp:label></TD>
								<td  colspan="1"><asp:radiobutton id="optNormal" runat="server" CssClass="opt"  GroupName="r1"
										AutoPostBack="True" Text="<%$ Resources:ValidationResources, Nml %>" Checked="True"></asp:radiobutton></TD>
                                <td colspan="2">
                                    <asp:RadioButton ID="optperforma" runat="server" AutoPostBack="True" CssClass="opt"
                                        GroupName="r1"   Text="<%$ Resources:ValidationResources,Prma %>"  /></td>
							</tr>
							<tr>
								<td >
                                   <asp:label id="Label4" runat="server"  Text="<%$ Resources:ValidationResources,LBillSN %>"></asp:label></TD>
								<td  colspan="3"><INPUT onkeypress="disallowSingleQuote(this);" id="txtbillserialno" onblur="this.className='blur'"
										
										onfocus="this.className='focus'"  type="text" maxLength="25" name="txtbillserialno" runat="server" class="txt10">
									<asp:label id="Label21" runat="server" CssClass="star" Font-Size="11px">*</asp:label></TD>
							</tr>
							<tr>
								<td ><asp:label id="Label2" runat="server" Text="<%$ Resources:ValidationResources,LInvNum %>"></asp:label></TD>
								<td><input onkeypress="disallowSingleQuote(this);" id="txtinvno" onblur="this.className='blur'"
										
										onfocus="this.className='focus'" type="text" size="12" name="txtinvno" runat="server" class="txt10">
									<asp:label id="Label19" runat="server" CssClass="star" Font-Size="11px">*</asp:label></TD>
								<TD style="vertical-align:top" ><asp:label id="Label5" runat="server"  Text="<%$ Resources:ValidationResources,InvD %>"></asp:label></TD>
								<TD>
                                    
                                    
<%--pushpendra singh--%>
 <asp:TextBox ID="txtinvdate" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Width="77%" />
 <%--<ajax:CalendarExtender ID="CalendarExtender1" TargetControlID="txtinvdate" Format="dd/MMM/yyyy" runat="server"></ajax:CalendarExtender>--%> 
 <%--pushpendra singh--%>
                                    
                                    
                                    <%--<input id="txtinvdate" onblur="this.className='blur';checkdate1(this,document.Form1.hdCulture.value,document.Form1.hrDate.value)" 
										onfocus="this.className='focus'" type="text" size="11" name="txtinvdate" runat="server" class="txt10"><input id="btnDate" type="button" onclick="pickDate('txtinvdate');" accesskey="D" style="background-position: center center; background-image: url(cal.gif); width: 23px; background-color: black; height: 20px;" />--%>

								</TD>
							</TR>
							<TR>
								<TD ><asp:label id="Label1" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,VenOrd %>"  ></asp:label></TD>
								<TD colSpan="3">
									<asp:dropdownlist id="cmbvendor" Height="30" onblur="this.className='blur'" onfocus="this.className='focus'" runat="server"
										CssClass="txt10" AutoPostBack="True" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></asp:dropdownlist>
                                    <asp:label id="Label11" runat="server" CssClass="star" Font-Size="11px">*</asp:label>

								</TD>
							</TR>
							<TR>
								<TD><INPUT id="Hd_orderno" type="hidden" name="Hidden2"
										runat="server">

								</TD>
								<TD  style="vertical-align:top" colSpan="2" ><asp:listbox id="lstorders" onblur="this.className='blur'" onfocus="this.className='focus'" runat="server"
										CssClass="txt10"   AutoPostBack="True"  Rows="7"></asp:listbox>

								</TD>
								<TD style="vertical-align:middle" class="style1"></TD>
							</TR>
							 
                      </table>

						</div>
                                    <div class="allgriddivmid">
								<asp:datagrid id="grdorders" runat="server" CssClass="allgrid GenTable1" Width="100%" 
                                        Font-Names='<%$ Resources:ValidationResources, TextBox1 %>' 
                                        AutoGenerateColumns="False">
										
										<Columns>
											<asp:EditCommandColumn UpdateText="Update" HeaderText="<%$ Resources:ValidationResources, GrEdit %>" CancelText="Cancel"
												EditText="Edit">                                                
                                            </asp:EditCommandColumn>
											<asp:TemplateColumn>
												<ItemTemplate>
													<asp:checkbox id="chksearch" runat="server" Font-Names="Lucida Sans Unicode" ForeColor="Black"
														Width="20px" Font-Size="X-Small" Height="12px" AutoPostBack="True" Text="" BorderStyle="None"
                                                       tooltip="Amount, discount etc. will be shown on checked/unchecked." ></asp:checkbox>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn Visible="False" DataField="indentnumber" ReadOnly="True" HeaderText="<%$ Resources:ValidationResources, GrIndntN %>">
												</asp:BoundColumn>
											<asp:BoundColumn DataField="indenttitle" ReadOnly="True" HeaderText="<%$ Resources:ValidationResources, LTitle %>"></asp:BoundColumn>
											<asp:BoundColumn DataField="noofcopies" ReadOnly="True" HeaderText="<%$ Resources:ValidationResources, GrCops %>"></asp:BoundColumn>
											<asp:BoundColumn Visible="False" DataField="oprice" ReadOnly="True" HeaderText="<%$ Resources:ValidationResources, GrOrdPrice %>"></asp:BoundColumn>
											<asp:BoundColumn DataField="price" ReadOnly="True" HeaderText="<%$ Resources:ValidationResources, LPrice %>"></asp:BoundColumn>
											<asp:BoundColumn DataField="totalamount" ReadOnly="True" HeaderText="<%$ Resources:ValidationResources, Amt %>"></asp:BoundColumn>
											<asp:BoundColumn DataField="currencyname" ReadOnly="True" HeaderText="<%$ Resources:ValidationResources, LCurrency %>"></asp:BoundColumn>
											<asp:BoundColumn DataField="bankrate" ReadOnly="True" HeaderText="<%$ Resources:ValidationResources, GrExRate %>"></asp:BoundColumn>
											<asp:BoundColumn DataField="indenttype" ReadOnly="True" HeaderText="<%$ Resources:ValidationResources, GrType %>"></asp:BoundColumn>
											<asp:BoundColumn Visible="False" DataField="sr" ReadOnly="True" HeaderText="<%$ Resources:ValidationResources, GrSerial %>"></asp:BoundColumn>
											<asp:BoundColumn Visible="False" DataField="er" ReadOnly="True" HeaderText="<%$ Resources:ValidationResources, GrExRate %>"></asp:BoundColumn>
											<asp:TemplateColumn HeaderText="<%$ Resources:ValidationResources, DiscPct%>">
												<ItemTemplate>
													<asp:Label id="Label15" runat="server" Width="65px" Text='<%# DataBinder.Eval(Container, "DataItem.discount") %>'>
													</asp:Label>
												</ItemTemplate>
												<EditItemTemplate>
													<asp:TextBox id="txtcurr" runat="server" MaxLength="5" BorderWidth="1px" onkeypress="decimalNumber(this);" Width="65px" Text='<%# DataBinder.Eval(Container, "DataItem.discount") %>'>
													</asp:TextBox>
												</EditItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="departmentcode" HeaderText="<%$ Resources:ValidationResources, GrDpt %>" Visible="False"></asp:BoundColumn>
										</Columns>										
									</asp:datagrid>
							</div>
                                    <table class=" no-more-tables table-condensed GenTable1"  style="border:0;margin-top:5px">
							 
							<TR>
								<TD><asp:label id="l1" runat="server" Text="<%$ Resources:ValidationResources,LTotAmount %>"></asp:label></TD>
								<TD><INPUT class="txt10" id="txttotalamount" onblur="this.className='blur'"  onkeyup="CalcA();"
										onfocus="this.className='focus'"  type="text" name="txttotalamount"
										runat="server" size="15"></TD>
								<TD ><asp:label id="Label3" runat="server"  Text="<%$ Resources:ValidationResources,DiscPct %>"></asp:label></TD>
								<TD style="vertical-align:bottom" class="style1"><INPUT onkeypress="decimalNumber(this);" id="txtdiscount" onblur="this.className='blur'"
										 onkeyup="CalcA();"
										 onfocus="this.className='focus'" type="text" maxLength="12"
										size="15" name="txtdiscount" runat="server" class="txt10"></TD>
							</TR>
							<TR>
								<TD><asp:label id="l2" runat="server" Text="<%$ Resources:ValidationResources,Pstg %>"></asp:label></TD>
								<TD><INPUT onkeypress="decimalNumber(this);" id="txtpostage" onblur="this.className='blur'"
										 onkeyup="CalcA();"
										 onfocus="this.className='focus'"
										type="text" maxLength="12" size="15" name="txtpostage" runat="server" class="txt10"></TD>
								<TD ><asp:label id="l4" runat="server"  Text="<%$ Resources:ValidationResources,DiscAmt %>"></asp:label></TD>
								<TD ><INPUT id="txtdiscamt" onblur="this.className='blur'"  onkeyup="CalcA();"
										onfocus="this.className='focus'" type="text" name="txtdiscamt" runat="server" class="txt10" size="15"></TD>
							</TR>
							<TR>
								<TD><asp:label id="l3" runat="server"  Text="<%$ Resources:ValidationResources,HdlgChgs%>"></asp:label></TD>
								<TD><INPUT onkeypress="decimalNumber(this);" id="txthandlingcharges" onblur="this.className='blur'"
										
										 onkeyup="CalcA();" onfocus="this.className='focus'"
										type="text" maxLength="12" size="15" name="txthandlingcharges" runat="server" class="txt10"></TD>
								<TD><asp:label id="l6" runat="server"  Text="<%$ Resources:ValidationResources,NetPyleAmt %>"></asp:label></TD>
								<TD><INPUT class="txt10" id="txtnetamt1" onblur="this.className='blur'" 
										onfocus="this.className='focus'"  type="text" size="5" name="txtnetamt1" runat="server"></TD>
							</TR>
							 
							
							<TR>
								
								<TD  ><asp:label id="l5" runat="server" Visible="False" Text="<%$ Resources:ValidationResources,AdvAmt%>"></asp:label></TD>
								<TD ><asp:textbox id="TextBox1" onblur="this.className='blur'" onfocus="this.className='focus'" runat="server"
										CssClass="txt10" BorderWidth="1px" Visible="False" ReadOnly="True" ></asp:textbox></TD>
								<td colspan="2"></td>
							</TR>
							<TR>
								<TD><asp:label id="Label6" runat="server" Text="<%$ Resources:ValidationResources,PaybleCrr %>"></asp:label></TD>
								<TD><asp:dropdownlist id="cmbcurr" onblur="this.className='blur'" Height="30" onfocus="this.className='focus'" runat="server"
										CssClass="txt10"  Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></asp:dropdownlist>
                                    <asp:Label ID="Label7" runat="server" CssClass="star" Font-Size="11px" 
                                        >*</asp:Label></TD>
								<TD ><asp:label id="l7" runat="server"  Visible="False" Text="<%$ Resources:ValidationResources,PaybleAmt %>"></asp:label></TD>
								<TD></TD>
							</TR>
										 
							
							<TR>
								<TD colspan="4" style="text-align:center">
                                   
                                    <asp:checkbox id="CheckBox2" runat="server"  AutoPostBack="True" Text="<%$ Resources:ValidationResources,MulTleInd %>"
										Font-Size="11px" Visible="False" Font-Bold="True" ForeColor="Navy"></asp:checkbox>
									<INPUT id="cmdsave" type="button" value="<%$Resources:ValidationResources,bSave %>" name="cmdsave" runat="server"  class="btnstyle">
										<INPUT id="cmdreset"  type="button" value="<%$Resources:ValidationResources,bReset %>" name="cmdreset"
													runat="server" class="btnstyle">
										
								</TD>
							</TR>
						</TABLE> 
                                    <INPUT id="sun"  type="hidden" name="Hidden6"
								runat="server">
                                    <INPUT id="txtdis"  type="hidden" size="2" name="Hidden6"
										runat="server"><INPUT id="txtchangeval"  type="hidden" size="2" name="txtchangeval"
										runat="server"><asp:checkbox id="CheckBox1" runat="server" AutoPostBack="True" Text="<%$ Resources:ValidationResources,SgleTleInd %>"
										Checked="True" Font-Size="11px" Visible="False" Font-Bold="True" ForeColor="Navy"></asp:checkbox>
						<INPUT id="txtvalue"  type="hidden" size="1" name="txtvalue"
										runat="server"><INPUT id="indi_discount" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" name="Hidden6"
										runat="server"><INPUT id="xCoordHolder" style="WIDTH: 16px; HEIGHT: 22px" type="hidden" name="Hidden2"
							runat="server"><input id="Hidden1" style="WIDTH: 24px; HEIGHT: 16px" type="hidden" name="Hidden1"
							runat="server"><input id="yCoordHolder" style="WIDTH: 16px; HEIGHT: 22px" type="hidden" name="Hidden2"
							runat="server"><input id="txtinvid" onblur="this.className='blur'" style="WIDTH: 18px; HEIGHT: 22px" onfocus="this.className='focus'"
							type="hidden" size="1" name="Hidden2" runat="server"><INPUT id="Hidden4" style="WIDTH: 16px; HEIGHT: 22px" type="hidden" size="1" name="Hidden1"
							runat="server"><input id="Hidden5" style="WIDTH: 16px; HEIGHT: 22px" type="hidden" size="1" name="Hidden2"
							runat="server"><input id="Hidden3" style="WIDTH: 8px; HEIGHT: 22px" type="hidden" size="1" name="Hidden3"
							runat="server"><input id="Hidval" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" name="Hidden6"
							runat="server"><input id="Hidden7" type="hidden" runat="server" style="width: 16px"><input id="HComboSelect" runat="server" 
                                type="hidden" value="<%$ Resources:ValidationResources, ComboSelect %>" style="width: 16px" /><input
                                    id="hrDate" runat="server" 
                                    type="hidden" value="<%$ Resources:ValidationResources, dateFormat1 %>" style="width: 16px" /><input id="js1" runat="server"
                                type="hidden" value="<%$ Resources:ValidationResources, js1 %>" style="width: 16px" /><input id="hdCulture" runat="server" style="width: 16px" type="hidden" />
				

                                </ContentTemplate>
                            </asp:UpdatePanel>
                         </div>  
            
						
						<P><asp:requiredfieldvalidator id="invoicenoRequiredFieldValidator1" runat="server" Font-Size="11px" ErrorMessage="<%$ Resources:ValidationResources, EInvNo %>"
								Display="None" ControlToValidate="txtinvno" SetFocusOnError="true">invoiceno</asp:requiredfieldvalidator><asp:requiredfieldvalidator id="invdateRequiredFieldValidator1" runat="server" Font-Size="11px" ErrorMessage="<%$ Resources:ValidationResources, PlzEInvD %>"
								Display="None" ControlToValidate="txtinvdate" SetFocusOnError="true">invoicedate</asp:requiredfieldvalidator><TR>
					
<%--						<asp:customvalidator id="cvdtPaycurrency" runat="server" Font-Size="11px" ErrorMessage="<%$ Resources:ValidationResources, SlctVenOdr %>"
								Display="None" ControlToValidate="cmbvendor" ClientValidationFunction="comboValidation"></asp:customvalidator>
									<asp:customvalidator id="CustomValidator1" runat="server" SetFocusOnError="True" ErrorMessage="<%$ Resources:ValidationResources, SlctPayCurncy %>"
								Display="None" ControlToValidate="cmbcurr" ClientValidationFunction="comboValidation"></asp:customvalidator>--%>
									<asp:validationsummary id="ValidationSummary1" runat="server" Width="106px" Font-Size="11px" ShowMessageBox="True"
								ShowSummary="False" DisplayMode="List"></asp:validationsummary>
                            
				
			<asp:textbox id="txtdeptcode" onblur="this.className='blur'" 
				onfocus="this.className='focus'" runat="server" Width="10px" Visible="False"></asp:textbox>
<script type="text/javascript">
    //On Page Load.
    $(function () {
        SetDatePicker();
    });

    //On UpdatePanel Refresh.
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    if (prm != null) {
        prm.add_endRequest(function (sender, e) {
            if (sender._postBackSettings.panelsToUpdate != null) {
                SetDatePicker();
            }
        });
    };


    function SetDatePicker() {

        $("[id$=txtinvdate]").datepicker({

            changeMonth: true,//this option for allowing user to select month
            changeYear: true, //this option for allowing user to select from year range
            dateFormat: 'dd-M-yy'  // CHANGE DATE FORMAT.
        });

    }

</script>
</asp:Content>





