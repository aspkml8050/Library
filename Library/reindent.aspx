<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reindent.aspx.cs"  MasterPageFile="~/LibraryMain.master"  Inherits="Library.reindent" %>
<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>

<asp:Content ID="rIndtHead" runat="server" ContentPlaceHolderID="head">
    <link href="cssDesign/libresponsive.css" rel="stylesheet"
type="text/css" />    <link href="cssDesign/multiselect.css" rel="stylesheet" type="text/css" >
    <script src="FormScripts/multiselect.js"></script>

			 <script type="text/javascript">
                 function GetServer(getCtrlId, ctrlTo) {
                     var value = document.getElementById(getCtrlId).value + "|" + document.getElementById('txtindentdate.').value;
                     UseCallBack(value, ctrlTo);
                 }
                 function CallPub2() {
                     let hdid = $('[id$=hdPubId]').attr('id');
                     let txt = $('[id$=txtCmbPublisher]').attr('id');
                     window.open("PublisherMaster.aspx?title=From Catalog&caller=child&hdid=" + hdid + "&text=" + txt, "Publisher Master", "height=700px,width=800px");

                 }
                 function callVend() {
                     let hdid = $('[id$=Vendid]').attr('id');
                     let txt = $('[id$=txtCmbVendor]').attr('id');
                     window.open("VendorMaster.aspx?title=From Catalog&caller=child&hdid=" + hdid + "&text=" + txt, "Vendor Master", "height=700px,width=800px");

                 }
                 function clientback(result, context) {
                     if (result == "msg") {
                         document.getElementById(context).value = "0";
                         alert("<asp:Literal runat='server' Text='<%$ Resources:ValidationResources,rExRate %>' />");
                         document.getElementById("txtnoofcopies").value = "";
                         document.getElementById("txtprice").value = "";
                         document.getElementById("txtTotalAmount").value = "";
                     } else {
                         if (result == "") {
                             document.getElementById("txtnoofcopies").value = "";
                             document.getElementById("txtprice").value = "";
                             document.getElementById("txtTotalAmount").value = "";
                         }
                         document.getElementById(context).value = result;
                         var tot;
                         if (document.Form1.txtExchangeRate.value == "")
                             return false;
                         else if (document.Form1.txtnoofcopies.value == "")
                             return false;
                         else if (document.Form1.txtprice.value == "")
                             return false;
                         else
                             tot = ((document.Form1.txtExchangeRate.value) * (document.Form1.txtnoofcopies.value) * (document.Form1.txtprice.value));

                         if (tot.toFixed) {
                             document.Form1.txtTotalAmount.value = tot.toFixed(2);
                         }
                         else {
                             document.Form1.txtTotalAmount.value = tot;
                         }
                     }
                 }



</script>
			
		<script type="text/javascript">
            function chk() {


                if (document.Form1.Hidden1.value == "13") {
                    document.Form1.Hidden1.value = 0;
                    document.Form1.txtCategory.focus();

                }

                if (document.Form1.hdTop.value == "top") {
                    window.scrollTo(0, 0);
                    document.Form1.hdTop.value = 0;
                }
                if (document.Form1.Hidden7.value == "101") {

                    document.Form1.cmbcurr.focus();
                    document.Form1.Hidden7.value = 0;
                }


            }


		</script>
		<script type="text/javascript"> //Ramesh 1apr
            function txtCategory_onkeydown() {
                if (window.event.keyCode == 13)
                    window.Form1.btnCategoryFilter.focus();
            }
            function txtnoofcopies_OnPropertyChange() {
                var tot;
                if (document.Form1.txtExchangeRate.value == "")
                    return false;
                else if (document.Form1.txtnoofcopies.value == "")
                    return false;
                else if (document.Form1.txtprice.value == "")
                    return false;
                else
                    tot = ((document.Form1.txtExchangeRate.value) * (document.Form1.txtnoofcopies.value) * (document.Form1.txtprice.value));

                if (tot.toFixed) {
                    document.Form1.txtTotalAmount.value = tot.toFixed(2);
                }
                else {
                    document.Form1.txtTotalAmount.value = tot;
                }
            }
            function txtprice_OnPropertyChange() {
                var val;
                if (document.Form1.txtExchangeRate.value == "")
                    return false;
                else if (document.Form1.txtnoofcopies.value == "")
                    return false;
                else if (document.Form1.txtprice.value == "")
                    return false;
                else
                    val = ((document.Form1.txtExchangeRate.value) * (document.Form1.txtnoofcopies.value) * (document.Form1.txtprice.value));

                if (val.toFixed) {
                    document.Form1.txtTotalAmount.value = val.toFixed(2);
                }
                else {
                    document.Form1.txtTotalAmount.value = val;
                }
            }

            function GetpubJs(sender, arg) {
                let id = arg.get_value();
                $('[id$=hdUid]').val(id);
                $('[id$=Vendid]').val(id);
                //  $('[id$=cmdcheck]').click();
            }
		</script>
        <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
            rel="stylesheet" type="text/css" />
        <script type="text/javascript">
            window.history.forward(1);
		</script>
       <style>
            .Publ
            {
                width:300px;
                max-height:250px;
                overflow:auto;
                margin:0;
                padding:0;
                font-size:13px;
                 color:black;
                border:2px solid black;
            }
       </style>


</asp:Content>

<asp:Content ID="rIndtMain" runat="server" ContentPlaceHolderID="MainContent">
     <asp:UpdateProgress ID="UpPorg1" runat="server">
    <ProgressTemplate>
    <NN:Mak ID="FF1" runat="server" />
    </ProgressTemplate>
    </asp:UpdateProgress>
   <div class="container tableborderst">   
         <div class="no-more-tables" style="width:100%">
              <div style="width:100%;display:none" class="title">
                  <div style="width:89%;float:left" > &nbsp;
		<asp:label id="lblTitle" runat="server" style="display:none" Width="100%" > re indent</asp:label>
</div> <div style="float:right;vertical-align:top">
                        <a id="lnkHelp" href="#"  style="display:none" onclick="ShowHelp('Help/Acquisitioning-Indents-Re-Indent.htm')"><img alt="Help?" height="15" src="help.jpg"  /></a>
             </div></div>
						
                             <asp:UpdatePanel id="UpdatePanel1" runat="server">
                             
                            <contenttemplate>
							<TABLE id="Table2" class="table-condensed GenTable1 " style="width:100%">
								<tr>
									<td colSpan="4">
										<P><asp:label id="msglabel" runat="server"  CssClass="err"></asp:label></P>
									</TD>
								</TR>
								<tr>
									<td><asp:label id="Label1" runat="server"  Text="<%$ Resources:ValidationResources,CanIndNo %>" ></asp:label></TD>
									<td colSpan="2"><input class="txt10" onkeypress="disallowSingleQuote(this);" id="txtCategory" onkeydown="txtCategory_onkeydown();"
											onblur="this.className='blur'"  
											onfocus="this.className='focus'" type="text" name="txtCategory" runat="server">
                                        <asp:Label ID="Label45" runat="server" CssClass="star">*</asp:Label>                                       
                                    </TD>
									<td>
                                        <%--<input id="btnCategoryFilter" type="button" value="<%$Resources:ValidationResources,bsearch %>"  name="btnCategoryFilter" runat="server" class="btnstyle">--%>
<asp:Button Id="btnCategoryFilter" runat="server" Text="Search" CssClass="btn btn-primary"/>
                                        
									</TD>
								</TR>
								<tr>
									<td ></TD>
                                   <%-- OnSelectedIndexChanged="lstAllCategory_SelectedIndexChanged" --%>
									<td colSpan="3"><asp:listbox id="lstAllCategory" onblur="this.className='blur'" AutoPostBack="true" onfocus="this.className='focus'"
											onclick="lstAllCategory_onclick();" tabIndex="3" runat="server"  CssClass="txt10" ></asp:listbox></TD>
								</TR>
								<tr>
									<td>
                                        <%--<asp:hyperlink id="Hyperlink1" runat="server" Text="<%$ Resources:ValidationResources,LDeptm %>" onclick="openNewForm('btnFillPub','DepartmentMaster','HNewForm','HWhichFill','HCondition');"></asp:hyperlink>--%>
                                        <asp:Label id="Hyperlink1" runat="server" Text="<%$ Resources:ValidationResources,LDeptm %>"  ></asp:Label>
									</TD>
									<td colspan="3">
                       <%-- <asp:dropdownlist id="cmbdept" onblur="this.className='blur'" Height="30" onfocus="this.className='focus'" runat="server"
							 AutoPostBack="True" CssClass="txt10" Font-Names="Arial Unicode MS"></asp:dropdownlist>--%>
                                     <asp:ListBox ID="cmbdept"  runat="server"  SelectionMode="Single" ></asp:ListBox>
                                        <asp:label id="Label33" runat="server" CssClass="star">*</asp:label> 
                        <input id="btnDep" runat="server" accesskey="T" class="btnH" onclick="openNewForm('btnFillPub', 'DepartmentMaster', 'HNewForm', 'HWhichFill', 'HCondition');"
                            style="width: 1px; height: 1px" type="button" value="button" /></td>
								</TR>
                                <tr>
                                    <td>
                                       <asp:Label ID="Label48" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LINumber %>"
                                           ></asp:Label></td>
                                    <td colspan="3">
                                        <input id="txtIndentNumber" runat="server" class="txt10" maxlength="16" name="txtIndentNumber"
                                            onblur="this.className='blur'" onfocus="this.className='focus'" readonly="readonly"
                                            size="38"  type="text" /></td>
                                  
                                </tr>
								<tr>
									<td><asp:label id="Label2" runat="server" Text="<%$ Resources:ValidationResources,rptIndentDate %>"></asp:label></TD>
									<td style="vertical-align:top">
                                        


<%--pushpendra singh--%>
 <asp:TextBox ID="txtindentdate" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
 <%--<ajax:CalendarExtender ID="CalendarExtender1" TargetControlID="txtindentdate" Format="dd/MMM/yyyy" runat="server"></ajax:CalendarExtender>--%> 
 <%--pushpendra singh--%>


                                       <%-- 
                                        <input id="txtindentdate" onblur="this.className='blur';checkdate1(this,document.Form1.hdCulture.value,document.Form1.hrDate.value)" 
											onfocus="this.className='focus'" type="text" size="10" name=" txtindentdate" runat="server" class="txt10"><input id="btnDate" type="button" onclick="pickDate('txtindentdate');" accesskey="D" style="background-position: center center; background-image: url(cal.gif); width: 27px; background-color: black; height: 20px;" />
                                        --%>
                                        
                                        
                                        
                                        
                                        <asp:label id="Label34" runat="server" CssClass="star">*</asp:label></td>
									<td  style="width:15%"><asp:label id="Label3" runat="server" Text="<%$ Resources:ValidationResources,LITime %>"></asp:label></TD>
									<td><input id="txtindenttime" onblur="this.className='blur'" style="width:70%"
											onfocus="this.className='focus'"  type="text" name="txtindenttime" runat="server" class="txt10"></TD>
                                    
                                    
								</TR>
								<tr>
									<td >
                                        <%--<asp:hyperlink id="HyperReqester" runat="server" Text="<%$ Resources:ValidationResources,LReque %>" onclick="openNewForm('btnFillPub','UserManagement','HNewForm','HWhichFill','HCondition');"></asp:hyperlink>--%>
                                        <asp:Label id="HyperReqester" runat="server" Text="<%$ Resources:ValidationResources,LReque %>" ></asp:Label>

									</TD>
									<td colSpan="3"><asp:dropdownlist id="cmbreq" Height="30" onblur="this.className='blur'" onfocus="this.className='focus'" runat="server"
											  Font-Names='<%$ Resources:ValidationResources, TextBox1 %>' CssClass="txt10"></asp:dropdownlist>
                                        <asp:label id="Label36" runat="server" CssClass="star" Width="1px">*</asp:label>  
                                    
                                <input id="btnReq" runat="server" onclick="openNewForm('btnFillPub', 'UserManagement', 'HNewForm', 'HWhichFill', 'HCondition');" accesskey="R" style="width: 1px; height: 1px;" type="button"
                                value="button" class="btnH" /></td>
								</TR>
                                <tr>
                                    <td >
                                        <asp:label id="Label12" runat="server"  Text="<%$ Resources:ValidationResources,LTitle %>">Title</asp:label></td>
                                    <td colspan="3" >
                                        <input id="txttitle" onblur="this.className='blur'"  
											onfocus="this.className='focus'" type="text" name="txttitle" runat="server" class="txt10">
                                           
                                        <asp:Image ID="Image6" runat="server" ImageUrl="~/Images/sugg.png" />
                                      <%--  <asp:Label ID="Label41" runat="server" CssClass="star" Width="1px">*</asp:Label>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label35" runat="server"  Text="<%$ Resources:ValidationResources,LSTitle %>">SubTitle</asp:Label></td>
                                    <td colspan="3" >
                                        <input id="txtSubtitle" onblur="this.className='blur'" 
											onfocus="this.className='focus'" type="text" name="txttitle" runat="server" class="txt10"><input
                                                id="cmdinsertAt" accesskey="z" onclick="insertAtCursor(document.Form1.txttitle);" style="width: 1px;
                                                height: 1px;" class="btnH" type="button" value="+" /></td>
                                     
                                </tr>
								<tr>
									<td colSpan="4" >
                                       <asp:label id="Label27" runat="server" Text="<%$ Resources:ValidationResources,LStatementofres %>">Statement Of Responsibility</asp:label></TD>
                                    
								</TR>
								<tr>
									<td ></TD>
									<td colSpan="3" ><asp:dropdownlist id="persontype" Height="30" onblur="this.className='blur'" onfocus="this.className='focus'"
											runat="server"  Font-Names='<%$ Resources:ValidationResources, TextBox1 %>' CssClass="txt10">
											<asp:ListItem Value="Author" Selected="True" Text='<%$ Resources:ValidationResources, Auth %>'></asp:ListItem>
											<asp:ListItem Value="Compiler" Text='<%$ Resources:ValidationResources, LComp %>'></asp:ListItem>
											<asp:ListItem Value="Editor" Text='<%$ Resources:ValidationResources, LEditor %>'></asp:ListItem>
											<asp:ListItem Value="Illustrator" Text='<%$ Resources:ValidationResources, LIllus %>'></asp:ListItem>
											<asp:ListItem Value="Translator" Text='<%$ Resources:ValidationResources, LITransltr %>'></asp:ListItem>
										</asp:dropdownlist>
                                        <asp:label id="Label38" runat="server" CssClass="star">*</asp:label></TD>
                                    
								</TR>
								<tr>
									<td ></TD>
									<td><asp:label id="Label15" runat="server" CssClass="opt" Text="<%$ Resources:ValidationResources,LFname %>" ></asp:label></TD>
									<td ><asp:label id="Label25" runat="server" CssClass="opt" Text="<%$ Resources:ValidationResources,LMname %>"></asp:label></TD>
									<td ><asp:label id="Label11" runat="server" CssClass="opt" Text="<%$ Resources:ValidationResources,LLname %>"></asp:label></TD>
                                    
								</TR>
								<tr>
									<td ><asp:label id="Label32" runat="server" Text="<%$ Resources:ValidationResources,LI %>">I</asp:label></TD>
									<td><input id="txtfname1" onblur="this.className='blur'" 
											onfocus="this.className='focus'" type="text" name="txtfname1" runat="server" class="txt10">
                                        <asp:label id="Label39" runat="server" CssClass="star">*</asp:label></TD>
									<td><input id="txtmname1" onblur="this.className='blur'" 
											onfocus="this.className='focus'" type="text" size="17" name="txtmname1" runat="server" DESIGNTIMEDRAGDROP="78" class="txt10"></TD>
									<td ><input id="txtlname1" onblur="this.className='blur'" 
											onfocus="this.className='focus'" type="text" size="17" name="txtlname1" runat="server" class="txt10"></TD>
                                     
								</TR>
								<tr>
									<td ><asp:label id="Label31" runat="server"  Text="<%$ Resources:ValidationResources,LII %>">II</asp:label></TD>
									<td ><input id="txtfname2" onblur="this.className='blur'" 
											onfocus="this.className='focus'" type="text" name="txtfname2" runat="server" class="txt10"></TD>
									<td ><input id="txtmname2" onblur="this.className='blur'" 
											onfocus="this.className='focus'" type="text" size="17" name="txtmname2" runat="server" class="txt10"></TD>
									<td ><input id="txtlname2" onblur="this.className='blur'" 
											onfocus="this.className='focus'" type="text" size="17" name="txtlname2" runat="server" class="txt10"></TD>
                                     
								</TR>
								<tr>
									<td ><asp:label id="Label28" runat="server"  Text="<%$ Resources:ValidationResources,LIII %>">III</asp:label></TD>
									<td ><input id="txtfname3" onblur="this.className='blur'"  
											onfocus="this.className='focus'" type="text" name="txtfname3" runat="server" class="txt10"></TD>
									<td ><input id="txtmname3" onblur="this.className='blur'" 
											onfocus="this.className='focus'" type="text" size="17" name="txtmname3" runat="server" class="txt10"></TD>
									<td ><input id="txtlname3" onblur="this.className='blur'" 
											onfocus="this.className='focus'" type="text" size="17" name="txtlname3" runat="server" class="txt10"></TD>
                                    
								</TR>
								<tr>
									<td><asp:label id="Label8" runat="server"  Text="<%$ Resources:ValidationResources,LSeries %>"> Series</asp:label></TD>
									<td colSpan="3"><input id="txtseries" onblur="this.className='blur'"  
											onfocus="this.className='focus'" type="text" name="txtseries" runat="server" class="txt10"></TD>
                                     
								</TR>
								<tr>
									<td>
                                        <%--<asp:HyperLink ID="HypLang" Text="<%$ Resources:ValidationResources,LLanguage %>" runat="server" onclick="openNewForm('btnFillPub','TranslationLanguages','HNewForm','HWhichFill','HCondition');"></asp:HyperLink>--%>
                                        <asp:Label ID="HypLang" Text="<%$ Resources:ValidationResources,LLanguage %>" runat="server" ></asp:Label>

									</TD>
									<td >
                                        <asp:DropDownList ID="cmbLanguage" runat="server" Height="30" CssClass="txt10" onblur="this.className='blur'"
                                            onfocus="this.className='focus'"  Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'>
                                        </asp:DropDownList></TD>
									<td >
										<asp:label id="Label23" runat="server"  Text="<%$ Resources:ValidationResources,LEdition %>">Edition</asp:label></TD>
									<td ><input id="txtedition" onblur="this.className='blur'" 
											onfocus="this.className='focus'" type="text" size="8" name="txtedition" runat="server" class="txt10"></TD>
                                    
								</TR>
								<tr>
									<td ><asp:label id="Label24" runat="server"  Text="<%$ Resources:ValidationResources,LEditionY %>">Edition Year</asp:label></TD>
									<td ><input onkeypress="IntegerNumber(this);" id="txtyrofedition" onblur="this.className='blur'"
											
											onfocus="this.className='focus'" type="text" maxLength="4" size="10" name="txtyrofedition" runat="server" class="txt10"></TD>
									<td >
										<asp:label id="Label29" runat="server" CssClass="span"  Text="<%$ Resources:ValidationResources,LPubY %>">Publication Year</asp:label></TD>
									<td ><input class="txt10" onkeypress="IntegerNumber(this);" id="txtPubYear" onblur="this.className='blur'"
											
											onfocus="this.className='focus'" type="text" maxLength="4" size="8" name="txtPubYear" runat="server"></TD>
                                    
								</TR>
								<tr>
									<td><asp:label id="Label14" runat="server"  Text="<%$ Resources:ValidationResources,LVP %>">Volume and part</asp:label></TD>
									<td ><input id="txtvolno" onblur="this.className='blur'" style="width:45%"
											onfocus="this.className='focus'" type="text" name="txtvolno" runat="server" class="txt10" size="6">
                                        <input id="txtP" onblur="this.className='blur'" style="width:45%"
											onfocus="this.className='focus'" type="text" name="txtP" runat="server" class="txt10" size="6"></TD>
									<td ><asp:label id="Label22" runat="server"  Text="<%$ Resources:ValidationResources,LISSN %>"></asp:label></TD>
									<td ><input id="txtisbn" onblur="this.className='blur'"  
											onfocus="this.className='focus'" type="text" name="txtisbn" runat="server" class="txt10"></TD>
                                    
								</TR>
								<tr>
									<td >
                                        <%--<asp:hyperlink id="Label21" runat="server" Text="<%$ Resources:ValidationResources,LCat %>" onclick="openNewForm('btnFillPub','CategoryLoadingStatus','HNewForm','HWhichFill','HCondition');"></asp:hyperlink>--%>
                                        <asp:Label id="Label21" runat="server" Text="<%$ Resources:ValidationResources,LCat %>"  ></asp:Label>

									</TD>
									<td ><asp:dropdownlist id="cmbcategory" onblur="this.className='blur'" Height="30" onfocus="this.className='focus'"
											runat="server"   CssClass="txt10" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>' ToolTip="Press Alt+L to add New Category"></asp:dropdownlist></TD>
																					
									<td >
                                        <%--<asp:hyperlink id="Label345" runat="server" Text="<%$ Resources:ValidationResources,MediaTyp %>" onclick="openNewForm('btnFillPub','frm_mediatype','HNewForm','HWhichFill','HCondition');"></asp:hyperlink>--%>
                                        <asp:Label id="Label345" runat="server" Text="<%$ Resources:ValidationResources,MediaTyp %>"  ></asp:Label>

									</TD>
									<td ><asp:dropdownlist id="mediatype" onblur="this.className='blur'" onfocus="this.className='focus'" Height="30" runat="server"
											 Font-Names='<%$ Resources:ValidationResources, TextBox1 %>' CssClass="txt10" ToolTip="Press Alt+M to add New Media Type">
											<asp:ListItem Value="Print" Text='<%$ Resources:ValidationResources, iprint %>'></asp:ListItem>
											<asp:ListItem Value="OnLine" Text='<%$ Resources:ValidationResources, OnLine %>'></asp:ListItem>
											<asp:ListItem Value="CD/DVD" Text='<%$ Resources:ValidationResources, CdDvd %>'></asp:ListItem>
											<asp:ListItem Value="MicroForm" Text='<%$ Resources:ValidationResources, McroForm %>'></asp:ListItem>
										</asp:dropdownlist>
                                        <asp:label id="Label17" runat="server" CssClass="star">*</asp:label></TD>
                                     
								</TR>
								<tr>
									<td ><asp:label id="Label7" runat="server" Text="<%$ Resources:ValidationResources,LGOC %>"></asp:label></TD>
									<td><asp:dropdownlist id="cmbgoc" onblur="this.className='blur'" Height="30" onfocus="this.className='focus'" runat="server"
											 CssClass="txt10" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></asp:dropdownlist></TD>
									<td>
                                        <%--<asp:hyperlink id="Label13" runat="server"  Text="<%$ Resources:ValidationResources,LCurrency %>" onclick="openNewForm('btnFillPub','ExchangeMaster','HNewForm','HWhichFill','HCondition');"></asp:hyperlink>--%>
                                        <asp:Label id="Label13" runat="server"  Text="<%$ Resources:ValidationResources,LCurrency %>"  ></asp:Label>

									</TD>
									<td ><asp:dropdownlist id="cmbcurr" onblur="this.className='blur'" Height="30" onfocus="this.className='focus'" runat="server" onchange="GetServer(this.id,'txtExchangeRate')"
											  Font-Names='<%$ Resources:ValidationResources, TextBox1 %>' CssClass="txt10" ToolTip="Press Alt+L to add New Currency"></asp:dropdownlist>
                                        <asp:label id="Label46" runat="server" CssClass="star">*</asp:label></TD>
                                     
								</TR>
								<tr>
									<td ><asp:label id="Label19" runat="server" Text="<%$ Resources:ValidationResources,LExRate %>">Exchange Rate</asp:label></TD>
									<td ><input id="txtExchangeRate" onblur="this.className='blur'" 
											onfocus="this.className='focus'"  type="text" size="10" name="txtExchangeRate" runat="server" class="txt10"></TD>
									<td ><asp:label id="Label16" runat="server" Text="<%$ Resources:ValidationResources,LNoCopies %>">No. of Copies</asp:label></TD>
									<td><input onkeypress="IntegerNumber(this);" id="txtnoofcopies" onblur="this.className='blur'"
											
											onkeyup="txtnoofcopies_OnPropertyChange();" onfocus="this.className='focus'" type="text" size="10"
											name="txtnoofcopies" runat="server" class="txt10">
										<asp:label id="Label47" runat="server" CssClass="star">*</asp:label></TD>
                                     
								</TR>
								<tr>
									<td ><asp:label id="Label26" runat="server" Text="<%$ Resources:ValidationResources,LCouNo %>"></asp:label></TD>
									<td ><input id="txtcoursenm" onblur="this.className='blur'" 
											onfocus="this.className='focus'" type="text" size="10" name="txtcoursenm" runat="server" DESIGNTIMEDRAGDROP="137" class="txt10"></TD>
									<td ><asp:label id="Label30" runat="server"  Text="<%$ Resources:ValidationResources,LNoStudent %>">No. of Students</asp:label></TD>
									<td ><input onkeypress="IntegerNumber(this);" id="txtnoofstud" onblur="this.className='blur'"
											
											onfocus="this.className='focus'" type="text" size="10" name="txtnoofstud" runat="server" class="txt10"></TD>
                                     
								</TR>
								<tr>
									<td><asp:label id="Label9" runat="server" Text="<%$ Resources:ValidationResources,LPrice %>"></asp:label></TD>
									<td ><input onkeypress="decimalNumber(this);" id="txtprice" onblur="this.className='blur'" 
											onkeyup="txtprice_OnPropertyChange();" onfocus="this.className='focus'" type="text" maxLength="7" size="10"
											name="txtprice" runat="server" class="txt10">
                                        <asp:label id="Label44" runat="server" CssClass="star">*</asp:label></TD>
									<td ><asp:label id="lblCurrency" runat="server"  Text="<%$ Resources:ValidationResources,LTotAmount %>">Total Amount</asp:label></TD>
									<td ><input id="txtTotalAmount" onblur="this.className='blur'" 
											onfocus="this.className='focus'"  type="text" size="10" name="txtTotalAmount" runat="server" class="txt10"></TD>
                                     
								</TR>
								<tr>
									<td ><asp:hyperlink id="Hyperlink2" runat="server" Text="<%$ Resources:ValidationResources,LPubli %>" onclick="CallPub2()"></asp:hyperlink></TD>
									<td colSpan="3">
                                          <asp:TextBox  ID="txtCmbPublisher" runat="server" BorderWidth="1px" Columns="30"  ></asp:TextBox> 
                                    <ajax:AutoCompleteExtender ID="ExtVend" runat="server" TargetControlID="txtCmbPublisher"
                                        CompletionInterval="50"
                                        CompletionSetCount="50"
                                        MinimumPrefixLength="0"
                                        CompletionListCssClass="Publ"
                                        ServicePath="MssplSugg.asmx"
                                        ServiceMethod="GetPubl"
                                        OnClientItemSelected="GetpubJs">
                                    </ajax:AutoCompleteExtender>
                                    <asp:HiddenField ID="hdUid" runat="server" />
                                           <asp:HiddenField ID="hdPubId" runat="server" />
                                       <%-- <custom:autosuggestbox id="txtCmbPublisher" runat="server" autopostback="false" borderwidth="1px" Font-Names ='<%$ Resources:ValidationResources, TextBox1 %>' 
                                            columns="30" cssclass="FormCtrl" datatype="City"  maxlength="100"
                                            resourcesdir="asb_includes" ></custom:autosuggestbox>--%>
                                        
                                        <asp:Image ID="Image7" runat="server" ImageUrl="~/Images/sugg.png" />
                                        <%--<asp:Label ID="Label42" runat="server" CssClass="star">*</asp:Label>--%></td>
								</TR>
								<tr>
									<td ><asp:HyperLink id="hyper68" runat="server" Text="<%$ Resources:ValidationResources,LVen %>" onclick="callVend()"></asp:HyperLink></TD>
									<td colSpan="3">
                                        <asp:TextBox  ID="txtCmbVendor" runat="server" BorderWidth="1px" Columns="30"></asp:TextBox> 
                                         <ajax:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtCmbVendor"
                                        CompletionInterval="50"
                                        CompletionSetCount="50"
                                        MinimumPrefixLength="0"
                                        CompletionListCssClass="Publ"
                                        ServicePath="MssplSugg.asmx"
                                        ServiceMethod="GetVendor"
                                        OnClientItemSelected="GetpubJs">
                                    </ajax:AutoCompleteExtender>
                                    <asp:HiddenField ID="Vendid" runat="server" />
                                       <%-- <Custom:AutoSuggestBox ID="txtCmbVendor" runat="server" AutoPostBack="false" BorderWidth="1px"
                                        Columns="30" CssClass="FormCtrl" DataType="City" MaxLength="100" ResourcesDir="asb_includes"
                                        Font-Names="<%$ Resources:ValidationResources, TextBox1 %>"  ></Custom:AutoSuggestBox>--%>
											 
                                        <asp:Image ID="Image8" runat="server" ImageUrl="~/Images/sugg.png" />
                                       <%-- <asp:Label ID="Label43" runat="server" CssClass="star">*</asp:Label>--%></td>
								</TR>
                                <tr>
                                    <td >
                                        <asp:label id="Label20" runat="server" Text="<%$ Resources:ValidationResources,LIType %>"></asp:label></td>
                                    <td colspan="3" >
                                        <asp:dropdownlist id="cmbindenttype" onblur="this.className='blur'" Height="30" onfocus="this.className='focus'"
											runat="server"   Font-Names='<%$ Resources:ValidationResources, TextBox1 %>' CssClass="txt10">
											<asp:ListItem Value="Approval" Text="<%$ Resources:ValidationResources,IChkApp %>"></asp:ListItem>
											<asp:ListItem Value="Non-Approval" Text="<%$ Resources:ValidationResources,GrNonApp %>"></asp:ListItem>
										</asp:dropdownlist></td>
                                     
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <input id="chkStanding" runat="server" name="chkapproval" onblur="this.className='blur'"
                                            onfocus="this.className='focus'" type="checkbox"
                                            value="" visible="false"  /><asp:Label ID="Label37" runat="server" CssClass="opt"  Visible="False" Text="<%$Resources:ValidationResources,IChkStand %>"></asp:Label></td>
                                     
                                </tr>
								<tr>
									
									<td colSpan="4" style="text-align:center">
             <%--<input id="cmdsave" type="button" value="<%$Resources:ValidationResources,bSave %>" name="cmdsave"
														runat="server" class="btnstyle">--%>
                                        <asp:Button Id="cmdsave" runat="server" Text="Submit" CssClass="btn btn-primary"/>
												<%--<input id="cmdreset" type="button" value="<%$Resources:ValidationResources,bReset %>" name="cmdreset"
														runat="server" class="btnstyle">--%>
							 <asp:Button Id="cmdreset" runat="server" Text="Reset" CssClass="btn btn-primary"/>				
													<asp:button id="cmdPrint1" runat="server"  Text="<%$Resources:ValidationResources,iPrint %>" CssClass="btn btn-primary"></asp:button>
											 
									</TD>
                                     
								</TR>
							     
							
							</TABLE>
                                <asp:requiredfieldvalidator id="TitleRequired" runat="server" Width="88px" Font-Size="11px" Display="None"
							ControlToValidate="txttitle" ErrorMessage="<%$ Resources:ValidationResources, IvTitle %>" SetFocusOnError="True" style="z-index: 100; left: 200px; position: absolute; top: 920px"></asp:requiredfieldvalidator>
							
 <input id="hReq" runat="server" style="width: 1px" type="hidden" />
                        
                        <input id="js1" runat="server" 
                            type="hidden" value="<%$ Resources:ValidationResources, js1 %>" style="width: 1px" />
                        <input id="hrDate" runat="server" 
                            type="hidden" value="<%$ Resources:ValidationResources, dateFormat1 %>" style="width: 2px" />
                        <input id="HComboSelect" runat="server"
                            type="hidden" value="<%$ Resources:ValidationResources, ComboSelect %>" style="width: 1px" />
                        <input id="hdCulture" runat="server" style="width: 1px" type="hidden" />
                        <input id="Hidden6" style="WIDTH: 8px; HEIGHT: 22px" type="hidden" value="0"
							name="Hidden6" runat="server"><input id="xCoordHolder" style="WIDTH: 1px;" type="hidden" name="Hidden2"
							runat="server"><input id="Hidden4" style="WIDTH: 1px;" type="hidden" size="1" name="Hidden1"
							runat="server"><input id="yCoordHolder" type="hidden" name="Hidden2" runat="server" style="width: 2px">
                        <input id="Hidden3" style="width: 2px;" type="hidden" size="1" name="Hidden3"
                            runat="server"><input id="hdSave" style="WIDTH: 1px; visibility:hidden" type="hidden" size="1" name="hdSave"
							runat="server"> <input id="Hidden2" style="WIDTH: 1px;" type="hidden" name="Hidden2"
							runat="server">
                        <input id="hdIndentId" style="WIDTH: 8px; HEIGHT: 24px" type="hidden" name="hdIndentId"
							runat="server">
                        <input id="hdItemNo" type="hidden" name="hdItemNo"
							runat="server" style="width: 1px">
                        <input id="HdAffCurr" runat="server" style="width: 1px" type="hidden" /><asp:listbox id="hlstAllCategory" runat="server" Width="0px" Height="0px" style="display:none"></asp:listbox>
					<input id="hSubmit1" style="WIDTH: 2px;" type="hidden" size="1" value="0"
							name="hSubmit1" runat="server"> 
							<input id="hdTop" type="hidden" runat="server" style="width: 8px">
						<input id="Button1" style="WIDTH: 1px; HEIGHT: 1px;" class="btnH" type="button" value="Button" runat="server">&nbsp;
                        <input id="hdReport" runat="server" name="Hidden8" style="width: 8px" type="hidden" />
                           <input id="btnPub" runat="server" onclick="openNewForm('btnFillPub', 'PublisherMaster', 'HNewForm', 'HWhichFill', 'HCondition');" accesskey="P" style="width: 1px; height: 1px;" type="button"
                                value="button" class="btnH" />&nbsp;
                                 <input id="btnVen" runat="server" onclick="openNewForm('btnFillPub', 'VendorMaster', 'HNewForm', 'HWhichFill', 'HCondition');" accesskey="V" style="width: 1px; height: 1px;" type="button"
                                value="button" class="btnH"/>
                                <input id="btnCurrency" runat="server" onclick="openNewForm('btnFillPub', 'ExchangeMaster', 'HNewForm', 'HWhichFill', 'HCondition');" accesskey="C" style="width: 1px; height: 1px;" type="button"
                                value="button" class="btnH" />
                                <input id="btnMedia" runat="server" onclick="openNewForm('btnFillPub', 'frm_mediatype', 'HNewForm', 'HWhichFill', 'HCondition');" accesskey="M" style="width: 1px; height: 1px;" type="button"
                                value="button" class="btnH" />
                                <input id="btnLanguage" runat="server" onclick="openNewForm('btnFillPub', 'TranslationLanguages', 'HNewForm', 'HWhichFill', 'HCondition');" accesskey="L" style="width: 1px; height: 1px;" type="button"
                                value="button" class="btnH" />
                                <input id="btnCategory" runat="server" onclick="openNewForm('btnFillPub', 'CategoryLoadingStatus', 'HNewForm', 'HWhichFill', 'HCondition');" accesskey="g" style="width: 1px; height: 1px;" type="button"
                                value="button" class="btnH"/>
                            <input id="HNewForm" runat="server" style="width: 8px" type="hidden" />
                            <input id="HWhichFill" runat="server" style="width: 1px" type="hidden" />
                            <input id="HCondition" runat="server" style="width: 1px" type="hidden" />
                            <input id="btnFillPub" runat="server" style="width: 1px; height: 1px; visibility:hidden " type="button" class="btnH" value="button" causesvalidation="false" />
                        <input id="Hidden7" type="hidden" runat="server" style="width: 16px">
                        <input id="HdVendorid" runat="server" type="hidden" style="width: 22px" />


<asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="ZeroValidation"
                                            ControlToValidate="txtnoofcopies" Display="None" ErrorMessage="<%$Resources:ValidationResources,NoCopMGrt %>" style="z-index: 100; left: 168px; position: absolute; top: 952px"></asp:CustomValidator>
						<asp:requiredfieldvalidator id="FirstRequired" runat="server" Width="114px" Font-Size="11px" Display="None"
							ControlToValidate="txtfname1" ErrorMessage="<%$ Resources:ValidationResources, IvFName %>" SetFocusOnError="True" style="z-index: 102; left: 192px; position: absolute; top: 984px"></asp:requiredfieldvalidator>
									<input id="Hidden5" style="WIDTH: 16px; HEIGHT: 22px" type="hidden" size="1" name="Hidden2"
											runat="server">&nbsp;
                                        <input id="Hidden1" style="WIDTH: 16px; HEIGHT: 22px" type="hidden" size="1" name="Hidden1"
											runat="server">
                                <input id="dnewcombo" style="WIDTH: 16px; HEIGHT: 22px" type="hidden" size="1" name="Hidden1"
											runat="server">
                                        <input id="hdPublisherId" runat="server" style="width: 56px" type="hidden" />

						 <input id="cmdShow" type="button" value="button" runat="server" style="height:1px; width:1px; visibility:hidden "/>
						 <asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" Width="113px" CssClass="opt" ErrorMessage="<%$ Resources:ValidationResources, SIndent %>"
							ControlToValidate="lstAllCategory" Display="None" ForeColor=" " SetFocusOnError="True"></asp:requiredfieldvalidator>
                        &nbsp;
                        <asp:RequiredFieldValidator ID="IndentDateRequired" runat="server" ControlToValidate="txtindentdate"
                            CssClass="opt" Display="None" ErrorMessage="<%$ Resources:ValidationResources, SIndDt %>" ForeColor=" "
                            Width="113px" SetFocusOnError="True"></asp:RequiredFieldValidator>&nbsp;<asp:customvalidator id="CustomIndentType" runat="server" Width="88px" Font-Size="11px" ErrorMessage="<%$ Resources:ValidationResources, SIndT %>"
							ControlToValidate="cmbindenttype" Display="None" ClientValidationFunction="comboValidation" SetFocusOnError="True"></asp:customvalidator>
<%--						<asp:customvalidator id="CustomDept" runat="server" Width="112px" Font-Size="11px" Display="None"
							ControlToValidate="cmbdept" ErrorMessage="<%$ Resources:ValidationResources, IvDep %>" ClientValidationFunction="comboValidation" SetFocusOnError="True"></asp:customvalidator>
                        <asp:CustomValidator ID="CustomRequester" runat="server" ClientValidationFunction="comboValidation"
                            ControlToValidate="cmbreq" Display="None" ErrorMessage="<%$ Resources:ValidationResources, IvRequester %>"
                            Font-Size="11px" SetFocusOnError="True" Width="112px"></asp:CustomValidator>--%>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                        
<%--						<asp:customvalidator id="CustomCategory" runat="server" Width="94px" Font-Size="11px" Display="None"
							ControlToValidate="cmbcategory" ErrorMessage="<%$ Resources:ValidationResources, IvCat %>" ClientValidationFunction="comboValidation" SetFocusOnError="True"></asp:customvalidator>&nbsp;
						<asp:customvalidator id="CustomMediaType" runat="server" Width="94px" Font-Size="11px" Display="None"
							ControlToValidate="mediatype" ErrorMessage="<%$ Resources:ValidationResources, IvMedia %>" ClientValidationFunction="comboValidation" SetFocusOnError="True"></asp:customvalidator>
                                <asp:customvalidator id="CustomCurrency" runat="server" Width="96px" Font-Size="11px" ErrorMessage="<%$ Resources:ValidationResources, IvCurr %>"
							ControlToValidate="cmbcurr" Display="None" ClientValidationFunction="comboValidation" SetFocusOnError="True"></asp:customvalidator>--%>
                                <asp:requiredfieldvalidator id="NoOfCopiesRequired" runat="server" Font-Size="11px" ErrorMessage="<%$ Resources:ValidationResources, IvCopyNo %>"
							ControlToValidate="txtnoofcopies" Display="None" SetFocusOnError="True"></asp:requiredfieldvalidator>
						<asp:requiredfieldvalidator id="PriceRequired" runat="server" Width="112px" Font-Size="11px" Display="None"
							ControlToValidate="txtprice" ErrorMessage="<%$ Resources:ValidationResources, EPrice %>" SetFocusOnError="True"></asp:requiredfieldvalidator>&nbsp;&nbsp;
                                <asp:RequiredFieldValidator
                                ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtcmbpublisher"
                                Display="None" ErrorMessage="<%$ Resources:ValidationResources, SlctPub %>" SetFocusOnError="True"></asp:RequiredFieldValidator>
<%--                        <asp:customvalidator id="Customvendor" runat="server" Width="94px" Font-Size="11px" ErrorMessage="<%$ Resources:ValidationResources, IvVen %>"
							ControlToValidate="txtCmbVendor" Display="None" ClientValidationFunction="comboValidation" SetFocusOnError="True"></asp:customvalidator>&nbsp;
                        <asp:CustomValidator ID="CustomValidator21" runat="server" ClientValidationFunction="ZeroValidation"
                            ControlToValidate="txtprice" Display="None" ErrorMessage="<%$ Resources:ValidationResources, IvPPCopy %>"
                            SetFocusOnError="True"></asp:CustomValidator>
                        <asp:CustomValidator ID="CustomValidator3" runat="server" ClientValidationFunction="ZeroValidation"
                            ControlToValidate="txtnoofcopies" ErrorMessage="CustomValidator" SetFocusOnError="True" Width="118px"></asp:CustomValidator>&nbsp;--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCmbVendor"
                            Display="None" ErrorMessage="<%$ Resources:ValidationResources, IvVen %>" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        <asp:validationsummary id="ValidationSummary1" runat="server" Width="80px" Font-Size="11px" DisplayMode="List"
							ShowSummary="False" ShowMessageBox="True"></asp:validationsummary>
							</contenttemplate> 
							</asp:UpdatePanel> 
							</div></div>


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

        $("[id$=txtindentdate]").datepicker({

            changeMonth: true,//this option for allowing user to select month
            changeYear: true, //this option for allowing user to select from year range
            dateFormat: 'dd-M-yy'  // CHANGE DATE FORMAT.
        });

    }

</script>
      <script>
          $(function () {
              ForDataTable();
              SetListBox();
          });
          var prm = Sys.WebForms.PageRequestManager.getInstance();

          if (prm != null) {
              prm.add_endRequest(function (sender, e) {

                  if (sender._postBackSettings.panelsToUpdate != null) {

                      ForDataTable();
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

