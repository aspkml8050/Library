<%@ Page Language="C#" MasterPageFile="~/LibraryMain.Master" AutoEventWireup="true" CodeBehind="CataLogingStartScreen.aspx.cs" Inherits="Library.CataLogingStartScreen" %>
 <%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>
     
<asp:Content ID="Head" runat="server" ContentPlaceHolderID="head">
		<style>
		#divl2{
	margin-top: 20px;
		}
		
      .AccTitle{
                      width:400px !important;
            font-size:13px;
            max-height:200px !important;    
            margin:0;
            overflow:auto;
            padding:1px;  
            border:2px solid black;

      }
		</style>
		
		<script type="text/javascript" language="javascript">
//            function onTextBoxUpdate(evt)
//        {  
//                
//            var textBoxID=evt.source.textBoxID;
//            if(evt.selMenuItem!=null)
//            {
//            document.getElementById(textBoxID).value=evt.selMenuItem.label; // + " (modified by onTextboxUpdate)";
//            document.getElementById("cmdsearch").click();
//            }
//            evt.preventDefault();
//        }   

            function readAcc(sender, arg) {
                let acc = arg.get_value();
                document.getElementById("<%=txtcmbJTitle.ClientID%>").value = acc;
                document.getElementById("<%=cmdsearch.ClientID%>").click();
            }
        </script>
		<script language="javascript" type="text/javascript">


            function ConfirmbeforeSave() {
                if (window.confirm("<asp:Literal runat="server" Text="<%$ Resources:ValidationResources,diffItm %>" />" + "\n" + "<asp:Literal runat="server" Text="<%$ Resources:ValidationResources,ArYsure %>" />") == true) {
                    document.getElementById("<%=hdConfirm.ClientID%>").value = "Y";
                    document.getElementById("<%=confirmBefSave.ClientID%>").click();
                    return true;
                }
                else {
                    document.getElementById("<%=hdConfirm.ClientID%>").value = "N";
                    document.getElementById("<%=txtClassNumber.ClientID%>").focus();
                    return false;
                }
            }



            function txtBookNumber_onkeydown() {
                if (window.event.keyCode == 13) {
                    window.Form1.cmdNext.focus();
                }
            }


            function chk() {
                if (document.Form1.hdForMesage.value == "1") {
                    //alert("Entries already found under specifed Class Number and Book Number." + "\n" + "Click on Continue button to proceed further.");

                    alert("<asp:Literal runat="server" Text="<%$ Resources:ValidationResources,EntryallRexit %>" />" + "\n" + "<asp:Literal runat="server" Text="<%$ Resources:ValidationResources,Clickconbt %>" />");
                    document.Form1.hdForMesage.value = 0;
                    document.Form1.txtClassNumber.focus();
                }

                if (document.Form1.hdForMesage.value == "X") {
                    document.Form1.hdForMesage.value = "";
                    document.Form1.confirmBefSave.click();

                }
            }

            function txttitle_onkeydown() {
                var isNetscape = false;
                var isIE = false;
                var isOpera = false;
                var isWhoKnows = false;
                if (navigator.appName == "Netscape") {
                    isNetscape = true;
                    document.captureEvents(Event.KEYDOWN);
                    document.onkeydown = checkValue;
                } else if (navigator.appName == "Microsoft Internet Explorer") {
                    isIE = true;
                    document.onkeydown = checkValue;
                } else if (navigator.appName == "Opera") {
                    isOpera = true;
                    document.captureEvents(Event.KEYDOWN);
                    document.onkeydown = checkValue;
                } else {
                    isWhoKnows = "true";
                    //    isNetscape = true;
                    //    document.captureEvents(Event.KEYDOWN);
                    //    document.onkeydown = checkValue;
                }

                function checkValue(evt) {
                    var theButtonPressed;
                    if (isNetscape) {
                        if (evt.target.name == "txtcmbJTitle") {
                            theButtonPressed = evt.which;
                        }
                    }
                    else if (isIE) {
                        theButtonPressed = window.event.keyCode;
                    }
                    else if (isOpera) {
                        if (evt.target.name == "txtcmbJTitle") {
                            theButtonPressed = evt.which;
                        }
                    }
                    else if (isWhoKnows) {
                        //  theButtonPressed = window.event.keyCode;
                        alert("Please hit the submit button to process form");
                    }
                    if (theButtonPressed == 13) {
                        theButtonPressed = "0";
                        document.Form1.cmdsearch.click();
                        isNetscape = false;
                        isOpera = false;
                    }
                }
            }



            //function onTextBoxUpdate(evt)
            //    {
            //     theButtonPressed="0";
            //  	    document.Form1.cmdsearch.click();
            //  	    isNetscape=false;
            //  	    isOpera=false;
            //    }
            function onTextBoxUpdate(evt) {

                var textBoxID = evt.source.textBoxID;
                if (evt.selMenuItem != null) {
                    document.getElementById(textBoxID).value = evt.selMenuItem.label; // + " (modified by onTextboxUpdate)";  
                    //            PageMethods.populateSub();                   
                    document.getElementById("cmdsearch").click();
                }
                evt.preventDefault();
            }

            function funC() {
                strReturn = window.showModalDialog("DDCMainMenuShow.aspx", "Editor", "status:no;dialogWidth:900px;dialogHeight:400px;dialogHide:true;help:no;scroll:no;");
            }
        </script>
		
			
				 <%--<script language="javascript">
		    window.history.forward(1);
        </script>--%>
				
<%--		 <link href="cssDesign/libresponsive.css" rel="stylesheet" type="text/css" >--%>
        


</asp:Content>


<asp:Content ID="Main" runat="server" ContentPlaceHolderID="MainContent">
     <asp:UpdateProgress ID="UpPorg1" runat="server">
    <ProgressTemplate>
    <NN:Mak ID="FF1" runat="server" />
    </ProgressTemplate>
    </asp:UpdateProgress>
       <div class="container tableborderst" style="width:60%;margin-top:20px">   
             <div style="width:100%;display:none" class="title">
            <div style="width:89%;float:left">&nbsp;
          <asp:label id="lblt1" style="display:none"  runat="server" ForeColor="White" Text ="<%$ Resources:ValidationResources, LCatlogDEn %>"></asp:label>
          </div>
            <div style="float:right;vertical-align:top">
           <a id="lnkHelp" href="#"  style="display:none" onclick="ShowHelp('Help/Cataloging-Techical processing-catalogue data entry.htm')"><img src="help.jpg"  height="15" /></a>
            </div>
         </div>
           						<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                 <div class="no-more-tables" style="width:100%">
													<TABLE id="Table1" class="no-more-tables GenTable1" width="100%">
								<TR>
									<TD colSpan="2"><asp:label id="msglabel" runat="server" CssClass="err"  Visible="False" ></asp:label></TD>
                                    <td colspan="1" >
                                    </td>
                                    <td colspan="1">
                                    </td>
								</TR>
                                <tr>
                                    <td colspan="1" rowspan="1">
                                      <asp:Label ID="Label3" runat="server" CssClass="span"  Text ="<%$ Resources:ValidationResources, AccNo %>"></asp:Label></td>
                                    <td colspan="3">
                                       <%-- <asp:TextBox ID="txtcmbJTitle" runat="server" Columns="30" CssClass="txt10" MaxLength="100"
                                            onblur="this.className='blur'" onfocus="this.className='focus'" Style="resources: ValidationResources, TextBox2 %>"
                                            Width="232px"></asp:TextBox>--%>
                                            <asp:TextBox id="txtcmbJTitle"  runat="server" class="txt10" onblur="this.className='blur'"
                                        onfocus="this.className='focus'" style='<%$ Resources:ValidationResources, TextBox2 %>'
                                        type="text" size="55" onkeydown="txttitle_onkeydown();" />
                                        <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/sugg.png" />
                           <%--<Custom:AutoSuggestMenu ID="asmJTitle" 
                                        runat="server" 
                                        TargetControlID="txtcmbJTitle" 
                                        usepagemethods="false" 
                                        UsePaging="false"
                                        KeyPressDelay="10" 
                                        MaxSuggestChars="100" 
                                        OnClientTextBoxUpdate="onTextBoxUpdate" 
                                        OnGetSuggestions="SuggService.GetSuggestSearch" 
                                        ResourcesDir="~/asm_includes" 
                                        UpdateTextBoxOnUpDown="false" >
                        </Custom:AutoSuggestMenu> --%>
                                           <ajax:AutoCompleteExtender ID="ExtAcc" runat="server" TargetControlID="txtcmbJTitle"
          MinimumPrefixLength="0"
          CompletionInterval="50"   
          CompletionSetCount="50"
          FirstRowSelected="true"
                                                   
          CompletionListCssClass="AccTitle"
          ServicePath="MssplSugg.asmx"
                               OnClientItemSelected="readAcc"
          ServiceMethod="GetNonCatBooks" >
     </ajax:AutoCompleteExtender>
                                        &nbsp;&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="1" rowspan="1" >
                                        <asp:Label ID="Label1" runat="server" CssClass="span" Text ="<%$ Resources:ValidationResources,lblClsNo %>"></asp:Label></td>
                                    <td>
                                       <%-- <INPUT id="txtClassNumber" onkeypress="disallowSingleQuote(this);" onblur="this.className='blur'"
											onfocus="this.className='focus'" style='<%$ Resources:ValidationResources, TextBox2 %>; width: 105px;'
											type="text" name="txtClassNumber" runat="server" class="txt10">--%>
											<asp:TextBox ID="txtClassNumber" Width="60%"   runat="server"></asp:TextBox>
											
                                            <asp:Button ID="Button1" CssClass="btnstyle" runat="server" OnClientClick="funC();" Text="Open" 
                                               UseSubmitBehavior="True"  />
                                    </td>
                                    <td >
                                      <asp:Label ID="Label2" runat="server" CssClass="span" Text ="<%$ Resources:ValidationResources,BookNo %>"></asp:Label></td>
                                    <td>
                                        <INPUT id="txtBookNumber" onkeypress="disallowSingleQuote(this);" onblur="this.className='blur'"
											onfocus="this.className='focus'" style="<%$ Resources:ValidationResources, TextBox2 %>;"
											accessKey="" type="text" name="txtBookNumber" runat="server" class="txt10">
                                    </td>
                                </tr>
                                                        <tr>
                                                            <td colspan="1" rowspan="1">
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td >
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="chkDifferent" runat="server" AutoPostBack="True" CssClass="opt" Text="<%$ Resources:ValidationResources,AssNClNoBkNo %>" Visible="False" /></td>
                                                        </tr>
                                                        <tr>
                                                           <td>
                     <asp:Label ID="Label13" runat="server" CssClass="span"  Text="<%$ Resources:ValidationResources,rptDepartment%>"></asp:Label>

</td>
                    <td colspan="3">
                        <asp:DropDownList ID="cmbdept" runat="server" CssClass="text10" Height="30"
                            Font-Size="Small" onblur="this.className='blur'" onfocus="this.className='focus'"
                            Width="511px"></asp:DropDownList>

</td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="1" rowspan="1">
                                                               <asp:Label ID="Label11" runat="server" CssClass="span" Text ="<%$ Resources:ValidationResources,LTitle %>"></asp:Label></td>
                                                            <td colspan="3" ><INPUT id="txtTitle" onkeypress="disallowSingleQuote(this);" onblur="this.className='blur'"
											onfocus="this.className='focus'" style='<%$ Resources:ValidationResources, TextBox2 %>'
											accessKey="" type="text" name="txtTitle" runat="server" class="txt10" readonly="readOnly" size="80"></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="1" rowspan="1" >
                                                               <asp:Label ID="Label12" runat="server" CssClass="span" Text ="<%$ Resources:ValidationResources,Authors %>"></asp:Label></td>
                                                            <td colspan="3" ><INPUT id="txtAuthor" onkeypress="disallowSingleQuote(this);" onblur="this.className='blur'"
											onfocus="this.className='focus'" style='<%$ Resources:ValidationResources, TextBox2 %>'
											accessKey="" type="text" name="txtAuthor" runat="server" class="txt10" readonly="readOnly" size="80"></td>
                                                        </tr>
                                <tr>
                                    <td colspan="1" rowspan="1" >
                                       <asp:Label ID="Label5" runat="server" CssClass="span" Text ="<%$ Resources:ValidationResources, LVolume %>"></asp:Label></td>
                                    <td >
                                        <INPUT id="txtVolume" onkeypress="disallowSingleQuote(this);" onblur="this.className='blur'"
											onfocus="this.className='focus'" style='<%$ Resources:ValidationResources, TextBox2 %>'
											accessKey="" type="text" name="txtVolume" runat="server" class="txt10" readonly="readOnly"></td>
                                    <td >
                                        <asp:Label ID="Label8" runat="server" CssClass="span" text ="<%$ Resources:ValidationResources, LPart %>"> </asp:Label></td>
                                    <td >
                                        <INPUT id="txtPart" onkeypress="disallowSingleQuote(this);" onblur="this.className='blur'"
											onfocus="this.className='focus'" style="<%$ Resources:ValidationResources, TextBox2 %>; "
											accessKey="" type="text" name="txtPart" runat="server" class="txt10" readonly="readOnly"></td>
                                </tr>
                                <tr>
                                    <td colspan="1" rowspan="1">
                                       <asp:Label ID="Label10" runat="server" CssClass="span" Text ="<%$ Resources:ValidationResources, LEdition %>"></asp:Label></td>
                                    <td  >
                                        <INPUT id="txtEdition" onkeypress="disallowSingleQuote(this);" onblur="this.className='blur'"
											onfocus="this.className='focus'" style='<%$ Resources:ValidationResources, TextBox2 %>'
											accessKey="" type="text" name="txtEdition" runat="server" class="txt10" readonly="readOnly"></td>
                                    <td >
                                       <asp:Label ID="Label4" runat="server" CssClass="span"  Text ="<%$ Resources:ValidationResources, LLanguage %>"></asp:Label></td>
                                    <td ><INPUT id="txtLanguage" onkeypress="disallowSingleQuote(this);" onblur="this.className='blur'"
											onfocus="this.className='focus'" style="<%$ Resources:ValidationResources, TextBox2 %>;"
											accessKey="" type="text" name="txtLanguage" runat="server" class="txt10" readonly="readOnly"></td>
                                </tr>
                                <tr>
                                    <td colspan="1" rowspan="1" >
                                      <asp:Label ID="Label9" runat="server" CssClass="span"  Visible="False" Text ="<%$ Resources:ValidationResources, ConfYear %>"></asp:Label></td>
                                    <td >
                                        <INPUT id="txtYear" onkeypress="disallowSingleQuote(this);" onblur="this.className='blur'"
											onfocus="this.className='focus'" style='<%$ Resources:ValidationResources, TextBox2 %>'
											accessKey="" type="text" name="txtYear" runat="server" class="txt10" visible="false"></td>
                                    <td >
                                       <asp:Label ID="Label6" runat="server" CssClass="span"  Visible="False" Text ="<%$ Resources:ValidationResources,LControlNo %>">.</asp:Label></td>
                                    <td >
                                        <INPUT id="txtCtrlNo" onkeypress="disallowSingleQuote(this);" onblur="this.className='blur'"
											onfocus="this.className='focus'" style="<%$ Resources:ValidationResources, TextBox2 %>; "
											accessKey="" type="text" name="txtCtrlNo" runat="server" class="txt10" disabled="disabled" visible="false">
                                        <asp:CheckBox ID="chkNewCtrl" runat="server" AutoPostBack="True" CssClass="opt" Text="<%$ Resources:ValidationResources, AssNCtrlNo %>"
                                            Visible="False"  /></td>
                                </tr>
								<TR>
									<TD colSpan="1">
									<asp:Label ID="Label14" runat="server" CssClass="span" 
                                            Text ="<%$ Resources:ValidationResources, slctsrc %>"></asp:Label>
                                        </TD>
								    <td>
                                        <asp:RadioButtonList ID="RadioButtonList1" RepeatDirection="Horizontal" runat="server" AutoPostBack="True">
                                            <asp:ListItem Selected="True">Existing Source</asp:ListItem>
                                            <asp:ListItem>External Source</asp:ListItem>
                                        </asp:RadioButtonList>
                                       </td>
                                    <td>
                                      </td>
                                    <td >
                                      </td>
								</TR>
								<TR>
									<TD  colSpan="4">
                                       <asp:label id="lblTitle" runat="server" CssClass="span"  Text ="<%$ Resources:ValidationResources, DeAlCatCpsAvble %>"></asp:label></TD>
								</TR>
                                                        </table></div>
                                <div style="overflow:auto;width:100%" id="dvgrd" runat="server">                                  
                    <asp:HiddenField runat="server" ID="hdnGrdId" />
                                    <asp:datagrid id="grdcopy" runat="server"  Width="100%" BorderWidth="1px"  CssClass="GenTable1"
                                            CellPadding="2" 
											AutoGenerateColumns="False" 
                                            >
											<SelectedItemStyle CssClass="GridSelectedItemStyle"></SelectedItemStyle>
											<EditItemStyle CssClass="GridEditedItemStyle"></EditItemStyle>
											<AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
											<ItemStyle CssClass="GridItem"></ItemStyle>
											<HeaderStyle CssClass="GridHeader"></HeaderStyle>
											<Columns>
											 <asp:ButtonColumn CommandName="Select" DataTextField="title" HeaderText="<%$ Resources:ValidationResources,LTitle %>">
                                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                            Font-Underline="True" ForeColor="Blue" />
                                                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                            Font-Underline="False" /></asp:ButtonColumn>
												<asp:BoundColumn DataField="accessionnumber" HeaderText="<%$ Resources:ValidationResources,AccNo %>"></asp:BoundColumn>
												<%--<asp:BoundColumn DataField="title" HeaderText="<%$ Resources:ValidationResources,LTitle %>"></asp:BoundColumn>--%>
												<asp:BoundColumn DataField="Author" HeaderText="<%$ Resources:ValidationResources,Authors %>"></asp:BoundColumn>
												<asp:BoundColumn DataField="classnumber" HeaderText="<%$ Resources:ValidationResources,ClasNo %>"></asp:BoundColumn>
												<asp:BoundColumn DataField="booknumber" HeaderText="<%$ Resources:ValidationResources,LBBookNo %>"></asp:BoundColumn>
												<asp:BoundColumn DataField="volume" HeaderText="<%$ Resources:ValidationResources,LVols %>"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="part" HeaderText="<%$ Resources:ValidationResources,LPart %>"></asp:BoundColumn>
												<asp:BoundColumn DataField="edition" HeaderText="<%$ Resources:ValidationResources,LEdition %>"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="Language_name" HeaderText="<%$ Resources:ValidationResources,LLanguage %>"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="ctrl_no" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="departmentname" HeaderText="<%$ Resources:ValidationResources,rptDepartment %>"></asp:BoundColumn>
											   </Columns>
											<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C83" BackColor="AliceBlue" Mode="NumericPages"></PagerStyle>
										</asp:datagrid>
                                </div>
                                <table class="no-more-tables GenTable1" width="100%" >
								                  <tr>
                                                            <td>
                                                 
                                                    <input id="BtnNextGrid" runat="server" type="submit" value="<%$ Resources:ValidationResources, Next %>" 
                                                                               onserverclick="BtnNextGrid_ServerClick" class="btnstyle" 
                                                                                visible="False" />
                                                                </td>
                                                        </tr>
                                                      
                                                        <tr>
                                                            <td>
                                                              <asp:Label ID="Label7" runat="server" CssClass="span"
                                                                    Text ="<%$ Resources:ValidationResources, DAvMatReexSources %>" Visible="False"></asp:Label></td>
                                                        </tr>
                                                       </table>
                                <div style="overflow:auto;width:100%">
                                    <asp:datagrid id="grdImported" runat="server" 
                                                                    Width="100%" BorderWidth="1px" CellPadding="2" BorderStyle="None"
											AutoGenerateColumns="False" Font-Names='Continue' GridLines="None" AllowPaging="True" PageSize="10" Visible="False">
                                                                <EditItemStyle CssClass="GridEditedItemStyle" />
                                                                <SelectedItemStyle CssClass="GridSelectedItemStyle" />
                                                                <PagerStyle BackColor="AliceBlue" ForeColor="#4A3C83" HorizontalAlign="Right" Mode="NumericPages" />
                                                                <AlternatingItemStyle CssClass="GridAltItem" />
                                                                <ItemStyle CssClass="GridItem" />
                                                                <HeaderStyle CssClass="GridHeader" />
                                                                <Columns>
                                                                    <asp:ButtonColumn CommandName="Select" DataTextField="title" HeaderText="<%$ Resources:ValidationResources,LTitle %>">
                                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                            Font-Underline="True" ForeColor="Blue" />
                                                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                            Font-Underline="False" />
                                                                    </asp:ButtonColumn>
                                                                    <asp:BoundColumn DataField="Author" HeaderText="<%$ Resources:ValidationResources,Authors %>"></asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="volume" HeaderText="<%$ Resources:ValidationResources,LVolume %>"></asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="part" HeaderText="<%$ Resources:ValidationResources,LPart %>"></asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="edition" HeaderText="<%$ Resources:ValidationResources,LEdition %>"></asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="Language_name" HeaderText="<%$ Resources:ValidationResources,LLanguage %>"></asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="ctrl_no" Visible="False"></asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="classnumber" Visible="False"></asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="booknumber" Visible="False"></asp:BoundColumn>
                                                                </Columns>
                                                            </asp:DataGrid>
                                </div>
                                <table class="no-more-tables GenTable1" width="100%">
                                <tr>
                                    <td colspan="4" align="center">
                                     
                                        <asp:Button ID="cmdNext2" runat="server" CssClass="bnt btn-primary"
                                             OnClick="cmdNext2_Click" Text="<%$ Resources:ValidationResources, LContinue %>"
                                            />
                                                    <INPUT id="cmdNext"  style="display:none" accessKey="N" type="button" value="<%$ Resources:ValidationResources, LContinue %>"
														name="cmdNext" runat="server" class="btnstyle" tabindex="0">
                                              
                                                    <input id="cmdreset" runat="server" name="cmdreset"  type="button"
                                                        value="<%$ Resources:ValidationResources, bReset %>" class="btnstyle" 
                                                        tabindex="1" />
                                    </td>
                                </tr>
                                                     
							</table>
                                 <input id="hSubmit1" runat="server" name="hSubmit1" size="1" style="width: 24px;
                            height: 22px" type="hidden" value="0" />&nbsp;
                            
                            <INPUT id="hdLanguageId" style="WIDTH: 29px; HEIGHT: 16px" type="hidden" size="1" value="0"
							name="hdLanguageId" runat="server"><INPUT id="yCoordHolder" style="WIDTH: 29px; HEIGHT: 16px" type="hidden" size="1" value="0"
							name="yCoordHolder" runat="server"><INPUT id="hdItemType" style="WIDTH: 29px; HEIGHT: 16px" type="hidden" size="1"
							name="hdItemType" runat="server"><INPUT id="hdConfirm" style="WIDTH: 29px; HEIGHT: 16px" type="hidden" size="1"
							name="hdConfirm" runat="server"><INPUT id="xCoordHolder" style="WIDTH: 8px; HEIGHT: 12px" type="hidden" size="1" value="0"
							name="xCoordHolder" runat="server"><INPUT id="hdForMesage" style="WIDTH: 6px; HEIGHT: 14px" type="hidden" size="1" name="hdForMesage"
							runat="server"><input id="hCurrentIndex2" runat="server" name="hCurrentIndex2" size="1" style="width: 32px;
                            height: 22px" type="hidden" />
                           	
                        <input id="confirmBefSave" runat="server" causesvalidation="false" style="width: 1px; height: 16px" type="button" value="" class="btnH"/>
                        <asp:ListBox ID="hlstAllCategory" runat="server" style="display:none" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></asp:ListBox>
                                                                <input id="Hdcounter" runat="server" style="width: 71px" type="hidden" />
                                                                <input id="hdDeptmentcode" runat="server" type="hidden" />
                                                                <input id="HComboSelect" runat="server" style="<%$ resources: ValidationResources, TextBox2 %>"
                                                                    type="hidden" value="<%$ Resources:ValidationResources, ComboSelect %>" />
                        <asp:Button ID="cmdsearch" runat="server" CausesValidation="False" OnClick="cmdsearch_Click"
                                    Height="1px" Text="Go" UseSubmitBehavior="False" class="btnH"
                                    Width="1px" />					
<%--                             <asp:customvalidator id="classCustomValidator1" runat="server" Display="None" ControlToValidate="txtClassNumber"
							ErrorMessage="<%$ Resources:ValidationResources, SInFPosNAll %>" SetFocusOnError="True"></asp:customvalidator>
                                <asp:customvalidator id="BookCustomValidator2" runat="server" ErrorMessage="CustomValidator" SetFocusOnError="True"></asp:customvalidator>
--%>
                        <asp:validationsummary id="ValidationSummary1" runat="server" Width="178px" Height="8px" DisplayMode="List"
							ShowSummary="False" ShowMessageBox="True"></asp:validationsummary>
                                
					  </ContentTemplate>
					  <Triggers><asp:PostBackTrigger ControlID="confirmBefSave" /></Triggers>
                        </asp:UpdatePanel>

       </div>
        <script type="text/javascript">
            //On Page Load.
            $(function () {
                ForDataTable();
            });
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
                    ThreeLevelSearch($('#' + grdId), "[id$=dvgrd]");
                }
                catch (err) {
                }
            }


        </script>
</asp:Content>