<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit_invoice.aspx.cs"  MasterPageFile="~/LibraryMain.master" Inherits="Library.Edit_invoice" %>
<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>

<asp:Content ID="Head" runat="server" ContentPlaceHolderID="head">
<%--        <link href="cssDesign/libresponsive.css" rel="stylesheet" type="text/css" >--%>
<script  type="text/javascript">
		<!--


    function DoConfirmation() {
        if (window.confirm("<asp:Literal runat='server' Text='<%$ Resources:ValidationResources,MSGdeleterecord %>' />") == true) {
            return true;
        }
        else {
            return false;
        }
    }

		</script>
		<script  type="text/javascript">


            function txtdiscount_onpropertychange() {
                var a;
                var b;
                var c;
                var d;
                var e;
                var f;
                var tot;
                if (document.getElementById("<%=txttotalamount.ClientID%>").value == "")
                    a = 0;
                else
                    a = document.getElementById("<%=txttotalamount.ClientID%>").value;

                if (document.getElementById("<%=txtdiscamt.ClientID%>").value == "")
                    b = 0;
                else
                    b = document.getElementById("<%=txtdiscamt.ClientID%>").value;

                if (document.getElementById("<%=cal_amt.ClientID%>").value == 0)
                    c = 0;
                else
                    c = document.getElementById("<%=cal_amt.ClientID%>").value;

                if (document.getElementById("<%=txtdiscount.ClientID%>").value == "")
                    d = 0;
                else
                    d = document.getElementById("<%=txtdiscount.ClientID%>").value;

                if (document.getElementById("<%=txtpostage.ClientID%>").value == "")
                    e = 0;
                else
                    e = document.getElementById("<%=txtpostage.ClientID%>").value;

                if (document.getElementById("<%=txthandlingcharges.ClientID%>").value == "")
                    f = 0;
                else
                    f = document.getElementById("<%=txthandlingcharges.ClientID%>").value;

                //		 tot=eval(parseFloat(c) + (parseFloat(a)* parseFloat(d)/100));
                tot = eval(parseFloat(a) * parseFloat(d) / 100);
                //alert(document.Form1.cal_amt.value);
                if (tot.toFixed) {
                    document.getElementById("<%=txtdiscamt.ClientID%>").value = tot.toFixed(2);
                    b = document.getElementById("<%=txtdiscamt.ClientID%>").value;
                    //			document.Form1.txtnetamt1.value=eval(parseFloat(a) + parseFloat(dis) - parseFloat(b) + parseFloat(e) + parseFloat(f)).toFixed(2);
                    document.getElementById("<%=txtnetamt1.ClientID%>").value = eval(parseFloat(a) - parseFloat(b) + parseFloat(e) + parseFloat(f)).toFixed(2);
                    if (document.getElementById("<%=txtnetamt1.ClientID%>").value == "NaN")
                        document.getElementById("<%=txtnetamt1.ClientID%>").value = ""
                }
                else {
                    document.getElementById("<%=txtdiscamt.ClientID%>").value = tot;
                    b = document.getElementById("<%=txtdiscamt.ClientID%>").value;
                    //			document.Form1.txtnetamt1.value=eval(parseFloat(a) + parseFloat(dis) - parseFloat(b) + parseFloat(e) + parseFloat(f))
                    document.getElementById("<%=txtnetamt1.ClientID%>").value = eval(parseFloat(a) - parseFloat(b) + parseFloat(e) + parseFloat(f))
                    if (document.getElementById("<%=txtnetamt1.ClientID%>").value == "NaN")
                        document.getElementById("<%=txtnetamt1.ClientID%>").value = ""

                    //document.Form1.txtnetamt1.value=eval(parseFloat(a) - parseFloat(b) + parseFloat(e) + parseFloat(f));
                }

                //		var Damt;
                //		Damt=eval(parseFloat(c) + (parseFloat(a) * parseFloat(d))/100);
                //		document.Form1.txtdiscamt.value=Damt
                //		
                //		if (Damt.toFixed)
                //		{
                //		document.Form1.txtdiscamt.value=Damt.toFixed(2);
                //			b=document.Form1.txtdiscamt.value;
                //			document.Form1.txtnetamt1.value=eval(parseFloat(a) - parseFloat(b) + parseFloat(e) + parseFloat(f)).toFixed(2);
                //			if (document.Form1.txtnetamt1.value=="NaN")
                //			document.Form1.txtnetamt1.value="";
                //			//document.Form1.txtdiscamt.value=Damt.toFixed(2);
                //		}
                //		else
                //		{
                //		document.Form1.txtdiscamt.value=Damt;
                //			b=document.Form1.txtdiscamt.value;
                //			document.Form1.txtnetamt1.value=eval(parseFloat(a) - parseFloat(b) + parseFloat(e) + parseFloat(f));
                //			if (document.Form1.txtnetamt1.value=="NaN") 
                //			document.Form1.txtnetamt1.value="";
                //		}
                //document.Form1.txtdiscamt.value=Damt;//

                //		b=document.Form1.txtdiscamt.value;
                //		//document.Form1.txtnetamt1.value=eval(parseFloat(a) - parseFloat(b) + parseFloat(e) + parseFloat(f)).toFixed(2);
                //		var r;
                //		r=eval(parseFloat(a) - parseFloat(b) + parseFloat(e) + parseFloat(f));
                //		if(r.toFixed)
                //		{
                //			document.Form1.txtnetamt1.value=r.toFixed(2);
                //		}
                //		else
                //		{
                //			document.Form1.txtnetamt1.value=r;
                //		}
            }

            function txtpostage_onpropertychange() {
                var a;
                var b;
                var c;
                var d;
                var e;
                var dis;
                var tot;
                if (document.getElementById("<%=txttotalamount.ClientID%>").value == "")
                    a = 0;
                else
                    a = document.getElementById("<%=txttotalamount.ClientID%>").value;
                if (document.getElementById("<%=txtdiscamt.ClientID%>").value == "")
                    b = 0;
                else
                    b = document.getElementById("<%=txtdiscamt.ClientID%>").value;


                if (document.getElementById("<%=txtpostage.ClientID%>").value == "")
                    c = 0;
                else
                    c = document.getElementById("<%=txtpostage.ClientID%>").value;

                if (document.getElementById("<%=txthandlingcharges.ClientID%>").value == "")
                    d = 0;
                else
                    d = document.getElementById("<%=txthandlingcharges.ClientID%>").value;
                //

                if (document.getElementById("<%=cal_amt.ClientID%>").value == 0)
                    dis = 0;
                else
                    dis = document.getElementById("<%=cal_amt.ClientID%>").value;


                //document.Form1.txtnetamt1.value=eval(parseFloat(a) - parseFloat(b) + parseFloat(c) + parseFloat(d)).toFixed(2);
                var r;
                r = eval(parseFloat(a) - parseFloat(b) + parseFloat(c) + parseFloat(d));
                if (r.toFixed) {
                    document.getElementById("<%=txtnetamt1.ClientID%>").value = r.toFixed(2);
                }
                else {
                    document.getElementById("<%=txtnetamt1.ClientID%>").value = r;
                }

            }

            function txthandlingcharges_onpropertychange() {

                var a;
                var b;
                var c;
                var d;
                if (document.getElementById("<%=txttotalamount.ClientID%>").value == "")
                    a = 0;
                else
                    a = document.getElementById("<%=txttotalamount.ClientID%>").value;

                if (document.getElementById("<%=txtdiscamt.ClientID%>").value == "")
                    b = 0;
                else
                    b = document.getElementById("<%=txtdiscamt.ClientID%>").value;


                if (document.getElementById("<%=txtpostage.ClientID%>").value == "")
                    c = 0;
                else
                    c = document.getElementById("<%=txtpostage.ClientID%>").value;

                if (document.getElementById("<%=txthandlingcharges.ClientID%>").value == "")
                    d = 0;
                else
                    d = document.getElementById("<%=txthandlingcharges.ClientID%>").value;

                var r;
                //document.Form1.txtnetamt1.value=eval(parseFloat(a) - parseFloat(b) + parseFloat(c) + parseFloat(d)).toFixed(2);
                r = eval(parseFloat(a) - parseFloat(b) + parseFloat(c) + parseFloat(d));
                if (r.toFixed) {
                    document.getElementById("<%=txtnetamt1.ClientID%>").value = r.toFixed(2);
                }
                else {
                    document.getElementById("<%=txtnetamt1.ClientID%>").value = r;
                }
            }

		</script>
		<script  type="text/javascript">
            function decimalNumber(no) {  //onkeypress
                let ke = no.keyCode ? no.keyCode : event.which;
                if ((ke >= 46) && (ke <= 57)) {
                    return true;
                }
                event.preventDefault();
                return false;
            }
            function txtdiscount1_onpropertychange() {
                var a;
                var b;
                var c;
                var d;
                var e;
                var f;
                var tot;
                var dis;
                var amt;
                if (document.Form1.txttotalamount.value == "")
                    a = 0;
                else
                    a = document.Form1.txttotalamount.value;

                if (document.Form1.txtdiscamt.value == "")
                    b = 0;
                else
                    b = document.Form1.txtdiscamt.value;

                if (document.Form1.pp.value == 0)
                    c = 0;
                else
                    c = document.Form1.pp.value;

                if (document.Form1.txtdiscount.value == "")
                    d = 0;
                else
                    d = document.Form1.txtdiscount.value;

                if (document.Form1.txtpostage.value == "")
                    e = 0;
                else
                    e = document.Form1.txtpostage.value;

                if (document.Form1.txthandlingcharges.value == "")
                    f = 0;
                else
                    f = document.Form1.txthandlingcharges.value;
                b = document.Form1.txtdiscamt.value;

                if (document.Form1.indi_discount.value == 0)
                    dis = 0;
                else
                    dis = document.Form1.indi_discount.value;

                //amt=eval(parseFloat(a)- parseFloat(dis));

                tot = eval(parseFloat(c) + (parseFloat(a) * parseFloat(d) / 100));
                if (tot.toFixed) {
                    document.Form1.txtdiscamt.value = tot.toFixed(2);
                    b = document.Form1.txtdiscamt.value;
                    //			document.Form1.txtnetamt1.value=eval(parseFloat(a) + parseFloat(dis) - parseFloat(b) + parseFloat(e) + parseFloat(f)).toFixed(2);
                    document.Form1.txtnetamt1.value = eval(parseFloat(a) - parseFloat(b) + parseFloat(e) + parseFloat(f)).toFixed(2);
                    if (document.Form1.txtnetamt1.value == "NaN")
                        document.Form1.txtnetamt1.value = ""
                }
                else {
                    document.Form1.txtdiscamt.value = tot;
                    b = document.Form1.txtdiscamt.value;
                    //			document.Form1.txtnetamt1.value=eval(parseFloat(a) + parseFloat(dis) - parseFloat(b) + parseFloat(e) + parseFloat(f))
                    document.Form1.txtnetamt1.value = eval(parseFloat(a) - parseFloat(b) + parseFloat(e) + parseFloat(f))
                    if (document.Form1.txtnetamt1.value == "NaN")
                        document.Form1.txtnetamt1.value = ""

                    //document.Form1.txtnetamt1.value=eval(parseFloat(a) - parseFloat(b) + parseFloat(e) + parseFloat(f));
                }

            }

            function txtpostage1_onpropertychange() {
                var a;
                var b;
                var c;
                var d;
                var tot;
                var dis;
                var cal;
                if (document.Form1.txttotalamount.value == "")
                    a = 0;
                else
                    a = document.Form1.txttotalamount.value;

                if (document.Form1.txtdiscount.value == "")
                    dis = 0;
                else
                    dis = document.Form1.txtdiscount.value;


                if (document.Form1.txtdiscamt.value == "")
                    b = 0;
                else
                    b = document.Form1.txtdiscamt.value;

                if (document.Form1.txtpostage.value == "")
                    c = 0;
                else
                    c = document.Form1.txtpostage.value;

                if (document.Form1.txthandlingcharges.value == "")
                    d = 0;
                else
                    d = document.Form1.txthandlingcharges.value;


                if (document.Form1.indi_discount.value == 0)
                    dis = 0;
                else
                    dis = document.Form1.indi_discount.value;


                // cal=eval(parseFloat(a)* parseFloat(dis)/100)  + parseFloat(cal)
                tot = eval(parseFloat(a) + parseFloat(dis) - parseFloat(b) + parseFloat(c) + parseFloat(d));
                if (tot.toFixed) {
                    document.Form1.txtnetamt1.value = tot.toFixed(2);
                    if (document.Form1.txtnetamt1.value == "NaN")
                        document.Form1.txtnetamt1.value = ""
                }
                else {
                    document.Form1.txtnetamt1.value = tot;
                    if (document.Form1.txtnetamt1.value == "NaN")
                        document.Form1.txtnetamt1.value = ""
                }
            }

            function txthandlingcharges1_onpropertychange() {
                var a;
                var b;
                var c;
                var d;
                var tot;
                var dis;
                var cal;
                if (document.Form1.txttotalamount.value == "")
                    a = 0;
                else
                    a = document.Form1.txttotalamount.value;

                if (document.Form1.txtdiscount.value == "")
                    dis = 0;
                else
                    dis = document.Form1.txtdiscount.value;

                if (document.Form1.txtdiscamt.value == "")
                    b = 0;
                else
                    b = document.Form1.txtdiscamt.value;

                if (document.Form1.txtpostage.value == "")
                    c = 0;
                else
                    c = document.Form1.txtpostage.value;

                if (document.Form1.txthandlingcharges.value == "")
                    d = 0;
                else
                    d = document.Form1.txthandlingcharges.value;

                if (document.Form1.indi_discount.value == 0)
                    dis = 0;
                else
                    dis = document.Form1.indi_discount.value;

                // cal=eval(parseFloat(a)* parseFloat(dis)/100)  + parseFloat(cal)

                tot = eval(parseFloat(a) + parseFloat(dis) - parseFloat(b) + parseFloat(c) + parseFloat(d));
                if (tot.toFixed) {
                    document.Form1.txtnetamt1.value = tot.toFixed(2);
                    if (document.Form1.txtnetamt1.value == "NaN")
                        document.Form1.txtnetamt1.value = ""
                }
                else {
                    document.Form1.txtnetamt1.value = tot;
                    if (document.Form1.txtnetamt1.value == "NaN")
                        document.Form1.txtnetamt1.value = ""
                }
            }
       

		</script>

</asp:Content>

<asp:Content ID="Main" runat="server" ContentPlaceHolderID="MainContent">
          <asp:UpdateProgress ID="UpPorg1" runat="server">
    <ProgressTemplate>
    <NN:Mak ID="FF1" runat="server" />
    </ProgressTemplate>
    </asp:UpdateProgress>
     <div class="container tableborderst ">  
                   
              <div style="width:100%;display:none" class="title">
                  <div style="width:89%;float:left" > &nbsp;
			<asp:label id="lblTitle" runat="server" style="display:none" Width="100%"  Text="<%$ Resources:ValidationResources,LInvMody %>"></asp:label>
                      </div>
                    <div style="float:right;vertical-align:top">
                         <a id="lnkHelp" href="#"  style="display:none" onclick="ShowHelp('Help/Acq-Edit_Invoice.htm')"><img height="15" alt="Help" src="help.jpg"  /></a>
                </div></div>
                   
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                   				 <div class="no-more-tables" style="width:100%">   
						<TABLE id="Table1" class="table-condensed GenTable1" >
							<TR>
								<TD colSpan="4"><asp:label id="msglabel" runat="server" CssClass="err"></asp:label></TD>
							</TR>
							<TR>
								<TD style="width:35%"><asp:label id="Label10" runat="server"  CssClass="span" Text="<%$Resources:ValidationResources,InvType %>"></asp:label></TD>
								<TD >
									<asp:radiobutton id="optNormal" runat="server"  CssClass="opt" Checked="True" Text="<%$Resources:ValidationResources,Nml %>"
										AutoPostBack="True" GroupName="r1" ></asp:radiobutton></TD>
								<TD style="vertical-align:top" colSpan="2"><asp:radiobutton id="optperforma" runat="server" CssClass="opt" Text="<%$Resources:ValidationResources,Prma %>" AutoPostBack="True"
										GroupName="r1" ></asp:radiobutton></TD>
							</TR>
							
							<TR>
								<TD style="vertical-align:top"><asp:radiobutton id="optbill" runat="server" CssClass="opt" Checked="True" Text="<%$Resources:ValidationResources,BSNoBasedSrh %>"
										AutoPostBack="True" GroupName="s"></asp:radiobutton></TD>
								<TD  colSpan="3">
								</TD>
							</TR>
							<TR>
								<TD  style="vertical-align:top"><asp:radiobutton id="optinvoice" runat="server" CssClass="opt" Text="<%$Resources:ValidationResources,InvNoBasedSrh %>"
										AutoPostBack="True" GroupName="s"></asp:radiobutton></TD>
								<TD  colSpan="2"><INPUT class="txt10" id="txtCategory" onblur="this.className='blur'" 
										onfocus="this.className='focus'" type="text" name="txtCategory" runat="server">
                                    </TD>
                                    <td>
									<INPUT id="btnCategoryFilter" type="button" value="<%$Resources:ValidationResources,bSearch %>"
										name="btnCategoryFilter" runat="server" class="btnstyle"></TD>
							</TR>
							<TR>
								
								<TD colspan="2" ><asp:listbox id="lstAllCategory" onblur="this.className='blur'" onfocus="this.className='focus'" runat="server" AutoPostBack="true" CssClass="txt10"></asp:listbox></TD>
								<TD ><asp:label id="Label5" runat="server"  CssClass="span" Text="<%$Resources:ValidationResources,InvD %>"></asp:label></TD>
								<TD >
                                    
                                    
                                    
                                    <%--pushpendra singh--%>
 <asp:TextBox ID="txtinvdate" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
 <%--<ajax:CalendarExtender ID="CalendarExtender1" TargetControlID="txtinvdate" Format="dd/MMM/yyyy" runat="server"></ajax:CalendarExtender>--%> 
 <%--pushpendra singh--%>
                                    
                                    
                                    
                                    <%--<INPUT class="txt10" id="txtinvdate" onblur="this.className='blur';checkdate1(this,document.Form1.hdCulture.value,document.Form1.hrDate.value)" 
										onfocus="this.className='focus'" type="text" size="11" name="txtinvdate" runat="server"><input id="btnDate" type="button" onclick="pickDate('txtinvdate');" accesskey="D" style="background-position: center center; background-image: url(cal.gif); width: 27px; background-color: black" />&nbsp;--%>






								</TD>
							</TR>
							 
							
							</table>
                                        </div>
                                    <hr />
                                    <div class="allgriddivmid">
								<asp:datagrid id="grdorders" runat="server" CssClass="allgrid GenTable1" Width="100%" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>' AutoGenerateColumns="False">
										
											<Columns>
												<asp:EditCommandColumn UpdateText="Update" HeaderText="<%$ Resources:ValidationResources, GrEdit %>" CancelText="Cancel"
													EditText="Edit">
                                                    
                                                </asp:EditCommandColumn>
												<asp:TemplateColumn Visible="False">
													<ItemTemplate>
														<asp:checkbox id="chksearch" runat="server" Font-Size="X-Small" Width="20px" ForeColor="Black"
															Font-Names="Lucida Sans Unicode" Height="1px" Text="" AutoPostBack="True" BorderStyle="None"></asp:checkbox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="title" ReadOnly="True" HeaderText="<%$ Resources:ValidationResources, LTitle %>"></asp:BoundColumn>
												<asp:BoundColumn DataField="noofcopies" ReadOnly="True" HeaderText="<%$ Resources:ValidationResources, GrCops %>"></asp:BoundColumn>
												<asp:BoundColumn DataField="totalamount" ReadOnly="True" HeaderText="<%$ Resources:ValidationResources, Amt %>"></asp:BoundColumn>
												<asp:BoundColumn DataField="currencyname" ReadOnly="True" HeaderText="<%$ Resources:ValidationResources, LCurrency %>"></asp:BoundColumn>
												<asp:BoundColumn DataField="er" ReadOnly="True" HeaderText="<%$ Resources:ValidationResources, LExRate %>"></asp:BoundColumn>
												<asp:TemplateColumn HeaderText="<%$ Resources:ValidationResources, DiscPct %>">
													<ItemTemplate>
														<asp:Label id=Label15 runat="server" Width="65px" Text='<%# DataBinder.Eval(Container, "DataItem.discount") %>'>
														</asp:Label>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox id="txtcurr" runat="server" Width="65px" MaxLength="5" onkeypress="decimalNumber(this);" Text='<%# DataBinder.Eval(Container, "DataItem.discount") %>' BorderWidth="1px">
														</asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn Visible="False" DataField="indentnumber" ReadOnly="True" HeaderText="<%$ Resources:ValidationResources, GrIndt %>"></asp:BoundColumn>
											</Columns>											
										</asp:datagrid>
                                        </div>
                                    <hr />
								<table class="no-more-tables table-condensed GenTable1" >
							 
							<TR>
								<TD ><asp:label id="l1" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LTotAmount %>"></asp:label></TD>
								<TD><INPUT class="txt10" id="txttotalamount" onblur="this.className='blur'" 
										onfocus="this.className='focus'"  type="text" name="txttotalamount" runat="server"></TD>
								<TD ><asp:label id="Label3" runat="server"  CssClass="span" Text="<%$Resources:ValidationResources,DiscPct %>"></asp:label></TD>
								<TD ><INPUT class="txt10" onkeypress="decimalNumber(this);" id="txtdiscount" onblur="this.className='blur'"
										
										onpropertychange="txtdiscount_onpropertychange();" onfocus="this.className='focus'" type="text" maxLength="12" name="txtdiscount"
										runat="server"></TD>
								
							</TR>
							<TR>
								<TD ><asp:label id="l2" runat="server"  CssClass="span" Text="<%$Resources:ValidationResources,Pstg %>"></asp:label></TD>
								<TD ><INPUT class="txt10" onkeypress="decimalNumber(this);" id="txtpostage" onblur="this.className='blur'"
										
										onpropertychange="txtpostage_onpropertychange();" onfocus="this.className='focus'" type="text" maxLength="12" size="5"
										name="txtpostage" runat="server"></TD>
								<TD ><asp:label id="l4" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,DiscAmt %>"></asp:label></TD>
								<TD ><INPUT class="txt10" id="txtdiscamt" onblur="this.className='blur'" 
										onfocus="this.className='focus'"  type="text" name="txtdiscamt" runat="server"></TD>
							
							</TR>
							<TR>
								<TD ><asp:label id="l3" runat="server"  CssClass="span" Text="<%$Resources:ValidationResources,HdlgChgs %>"></asp:label></TD>
								<TD ><INPUT class="txt10" onkeypress="decimalNumber(this);" id="txthandlingcharges" onblur="this.className='blur'" 
										
										onpropertychange="txthandlingcharges_onpropertychange();" onfocus="this.className='focus'" type="text" maxLength="12"
										size="5" name="txthandlingcharges" runat="server"></TD>
								<TD ><asp:label id="l5" runat="server"  CssClass="span" Visible="False" Text="<%$ Resources:ValidationResources,AdvAmt %>"></asp:label></TD>
								<TD ><asp:textbox id="TextBox1" onblur="this.className='blur'" onfocus="this.className='focus'" runat="server"
										  CssClass="txt10" BorderWidth="1px" Visible="False" ReadOnly="True" ></asp:textbox></TD>
								
							</TR>
							 
							<TR>
								<TD ><asp:label id="l6" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,NetPyleAmt %>"></asp:label></TD>
								<TD ><INPUT class="txt10" id="txtnetamt1" onblur="this.className='blur'" 
										onfocus="this.className='focus'"  type="text" size="5" name="txtnetamt1" runat="server"></TD>
								<TD ></TD>
								<TD ></TD>
							</TR>
							<TR>
								<TD ><asp:label id="Label6" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,PmtCrr %>"></asp:label></TD>
								<TD ><asp:dropdownlist id="cmbcurr" Height="30" onblur="this.className='blur'" onfocus="this.className='focus'" runat="server"
										CssClass="txt10" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></asp:dropdownlist></TD>
								<TD><asp:label id="l7" runat="server"  CssClass="span" Visible="False" Text="<%$ Resources:ValidationResources,PaybleAmt %>"></asp:label></TD>
								<TD></TD>
							
							</TR>
							 
							<TR>
								
								<TD  colSpan="4" style="text-align:center">
									<INPUT id="cmdsave" type="button" value="<%$Resources:ValidationResources,bSave %>" name="cmdsave" runat="server"  class="btnstyle">
										<INPUT id="cmdreset" type="button" value="<%$Resources:ValidationResources,bReset %>" name="cmdreset" runat="server"  class="btnstyle">
											<INPUT id="cmddelete" onclick="if (DoConfirmation() == false) return false;" type="button"
											value="<%$Resources:ValidationResources,bDelete %>" name="cmdreset" runat="server"  class="btnstyle">
										
								</TD>
								
							</TR>
						</table>

                                    <div style="display:none">
                                    <INPUT id="cal_amt" type="hidden" size="2" name="Hidden6"
										runat="server"><INPUT id="reportingtype"  type="hidden" size="2" name="Hidden6"
										runat="server"><INPUT id="hddept"  type="hidden" size="1" name="Hidden2"
										runat="server"><INPUT id="txtinvoiceid"  type="hidden" size="1" name="Hidden6"
										runat="server">
						<INPUT id="sun" style="WIDTH: 24px; HEIGHT:1px" type="hidden" name="Hidden6"
								runat="server">
                                    <asp:requiredfieldvalidator id="invdateRequiredFieldValidator1" runat="server" Font-Size="11px" ErrorMessage="<%$ Resources:ValidationResources, PlzEInvD %>"
								Display="None" ControlToValidate="txtinvdate" SetFocusOnError="True">invoicedate</asp:requiredfieldvalidator>
                            <input id="pp" type="hidden" runat="server" style="width: 24px" />
                            <INPUT id="indi_discount" style="WIDTH: 24px; HEIGHT:1px" type="hidden" size="1" name="Hidden6"
								runat="server"> <INPUT id="Hidval" style="WIDTH: 34px; HEIGHT:1px" type="hidden" size="1" name="Hidden6"
							runat="server">
						<INPUT id="Hidden3" style="WIDTH: 8px; HEIGHT: 22px" type="hidden" size="1" name="Hidden3"
							runat="server"> <INPUT id="Hidden5" style="WIDTH: 16px; HEIGHT:1px" type="hidden" size="1" name="Hidden2"
							runat="server">
                            <INPUT id="Hidden4" style="WIDTH: 16px; HEIGHT: 22px" type="hidden" size="1" name="Hidden1"
							runat="server"> 
                            <input id="hdCulture" runat="server" style="width: 16px" type="hidden" />
                            <INPUT id="hSubmit1" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" value="0"
								name="hSubmit1" runat="server"> <INPUT id="Button1" style="WIDTH: 40px; HEIGHT: 1px" class="btnH" type="button" value="Button" name="Button1"
								runat="server">
                            <asp:listbox id="hlstAllCategory" runat="server" Width="1px" Height="0px"></asp:listbox>
									<P> <INPUT id="Hidden1" style="WIDTH: 40px; HEIGHT:1px" type="hidden" size="1" name="Hidden1"
							runat="server">
                            <INPUT id="txtinvid" onblur="this.className='blur'" style="WIDTH: 18px; HEIGHT:1px" onfocus="this.className='focus'"
							type="hidden" size="1" name="Hidden2" runat="server">
                            <INPUT id="txtvalue" style="WIDTH: 32px; HEIGHT: 18px" type="hidden" size="1" name="txtvalue"
										runat="server">&nbsp;</P>
                            <input id="js1" runat="server" 
                                type="hidden" value="<%$ Resources:ValidationResources, js1 %>" style="width: 24px" />
									<INPUT id="hdorderno" style="WIDTH: 59px; HEIGHT: 22px" type="hidden" size="4" name="Hidden2"
										runat="server">
                            <INPUT id="xCoordHolder" style="WIDTH: 38px; HEIGHT:1px" type="hidden" size="1" name="Hidden2"
							runat="server">
                            <INPUT id="hdTop" style="WIDTH: 40px; HEIGHT:1px" type="hidden" size="1" name="hdTop"
								runat="server">
                            <input id="HComboSelect" runat="server" 
                                type="hidden" value="<%$ Resources:ValidationResources, ComboSelect %>" style="width: 24px" />
                            <input id="hrDate" runat="server" 
                                type="hidden" value="<%$ Resources:ValidationResources, dateFormat1 %>" style="width: 24px" />
                            <INPUT id="yCoordHolder" style="WIDTH: 38px; HEIGHT:1px" type="hidden" size="1" name="Hidden2"
							runat="server"> <INPUT id="txtchangeval" style="WIDTH: 48px; HEIGHT:1px" type="hidden" size="2" name="txtchangeval"
										runat="server">
                                        </div>
						   </ContentTemplate>
                            </asp:UpdatePanel>
                  </div>
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

