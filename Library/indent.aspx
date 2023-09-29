<%@ Page Language="C#" AutoEventWireup="true"  Async="true"
    MasterPageFile="~/LibraryMain.master" CodeBehind="indent.aspx.cs" Inherits="Library.indent" %>
<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>
<asp:Content ID="indtHead" runat="server" ContentPlaceHolderID="head">
    <link href="cssDesign/multiselect.css" rel="stylesheet" type="text/css">
  <script src="FormScripts/multiselect.js"></script>
    <style type="text/css">
            .ITitle
            {
                width:500px !important;
                max-height:300px;
                padding:0;
                margin:0;
                overflow:auto;
                font-size:.95em;
                background-color:white;
                border:2px solid green;
                z-index:10000;
            }
        </style>
     <script type="text/javascript">



         function ISelect(src, Ev)
         {
             document.getElementById("[id$=hdnIndId]").value = Ev.get_value();
             document.getElementById("[id$=hdnNonInd]").value = Ev.get_value();
             document.getElementById("[id$=cmdSerach]").click();

         }
         function title_search()
         {
             if (navigator.appName == "Microsoft Internet Explorer") {

                 window.showModalDialog("search_title.aspx?title=" + document.Form1.hd_title.value, "Staff", "dialogHeight:200px; dialogWidth:900px; dialogLeft:200px; dialogTop:200px;dialogHide:true;help:no;scroll:no;status:no;", true);
             } else {
                 window.open("search_title.aspx?title=" + document.getElementById("<%=hd_title.ClientID%>").value, "Staff", "height=200,width=900,left=200px,top=200,hide=true,help=no,scroll=no,status=no");
             }
           
         }
         function fillEntries(Url, btnName, hdField, Hgt, Wdt, Scrl, Lft, Tp)
         {
             var retId;
             if (navigator.appName == "Microsoft Internet Explorer") {
                 retId = window.showModalDialog(Url, "Staff", 'dialogHeight:' + Hgt + ';dialogWidth:' + Wdt + ';scroll:' + Scrl + ';dialogTop:' + Tp + ';dialogLeft:' + Lft);
                 if ("undefined" != typeof (retId)) {
                     SetNameSC1(retId.strName);
                 }
             }
             else if (navigator.appName == "Netscape") {
                 retId = window.show(Url, "Staff", 'height=' + Hgt + ',width=' + Wdt + ',scrollbars=' + Scrl + ',top=' + Tp + ',left=' + Lft + ',modal=' + 'yes');
                 //retId.focus();
             } else if (navigator.appName == "Opera") {
                 retId = window.show(Url, "Staff", 'height=' + Hgt + ',width=' + Wdt + ',scrollbars=' + Scrl + ',top=' + Tp + ',left=' + Lft + ',modal=' + 'yes');
                 //retId.focus();
             } else {
                 retId = window.show(Url, "Staff", 'height=' + Hgt + ',width=' + Wdt + ',scrollbars=' + Scrl + ',top=' + Tp + ',left=' + Lft + ',modal=' + 'yes');
             }
         }
         function SetNameSC1(strName)
         {
             document.getElementById('hdIndentIdR').value = strName;
             __doPostBack('btnFilEntries', '');
         }

         function txtCategory_onkeydown()
         {
             var isNetscape = false;
             var isIE = false;
             var isOpera = false;
             var isWhoKnows = false;
             if (navigator.appName == "Netscape") {
                 isNetscape = true;
             } else if (navigator.appName == "Microsoft Internet Explorer") {
                 isIE = true;
             } else if (navigator.appName == "Opera") {
                 isOpera = true;
             } else {
                 isWhoKnows = true;
             }

             if (isNetscape) {
                 document.captureEvents(Event.KEYUP);
             }
             //            document.onkeyup = checkValue;
         }
         function checkValue(evt)
         {
             var theButtonPressed;
             if (isNetscape)
             {
                 theButtonPressed = evt.which;
             } else if (isIE)
             {
                 theButtonPressed = window.event.keyCode;
             } else if (isOpera)
             {
                 theButtonPressed = evt.which;
                 //	alert("Please hit the submit button to process form");
             } else if (isWhoKnows)
             {
                 //  theButtonPressed = window.event.keyCode;
                 alert("Please hit the submit button to process form");
             }
             if (theButtonPressed == 13) {
                 document.getElementById("<%=btnCategoryFilter.ClientID%>").click();
             }
             //	       if(event.keyCode==13 || event.which==13)
             //			{
             //			alert("l");
             //			window.Form1.btnCategoryFilter.focus();
             //			}		
         }

         function checksame()
         {

             alert('not calling');
             if (document.getElementById("<%=hdTop.ClientID%>").value == "1") {
                document.getElementById("<%=hdTop.ClientID%>").value = 0;
                 document.getElementById("<%=confirmBefSave.ClientID%>").click();
             }
         }

         function chk()
         {

         }
         function txtnoofcopies_OnPropertyChange()
         {
         }
         function txtprice_OnPropertyChange()
         {
         }
         
         
         
         

         

     </script>

</asp:Content>

<asp:Content ID="indtMain" runat="server" ContentPlaceHolderID="MainContent">
     <asp:UpdateProgress ID="UpPorg1" runat="server">
    <ProgressTemplate>
    <NN:Mak ID="FF1" runat="server" />
    </ProgressTemplate>
    </asp:UpdateProgress>
  <div class="container tableborderst" style="padding:0;" >   
         <div class="no-more-tables" style="width:100%">
             <div style="width:100%;display:none" class="title">
                  <div style="width:89%;float:left" >
			<asp:label id="lblTitle" runat="server" Visible="false" Width="100%" ></asp:label>
                  </div>
                   <div style="float:right;vertical-align:top">
                        <a id="lnkHelp" href="#" onclick="ShowHelp('Help/Acquisitioning-Indents-Indent.htm')"><img id="IMG1" alt="Help?" height="15" src="help.jpg"  /></a>
              </div>		</div>		  
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                 <table id="Table1" class="table-condensed GenTable1" style="width: 100%;">
                                    <tr>
                                        <td colspan="5">
                                            <asp:Label ID="msglabel" runat="server" CssClass="err"></asp:Label></td>

                                    </tr>
                                    <tr>

                                        <td 
                                            colspan="5">
                                            <asp:LinkButton ID="lnkModify" runat="server" CausesValidation="False" CssClass="opt" OnClick="lnkModify_Click"
                                                ForeColor="Green" Font-Names="Verdana" Text="<%$ Resources:ValidationResources,ContiSIndModfyCR%>"></asp:LinkButton>

                                            <asp:LinkButton ID="lnkContinue" runat="server" CausesValidation="False" ForeColor="Green" OnClick="lnkContinue_Click" Font-Names="Verdana" Text="<%$ Resources:ValidationResources,ContiSIntENewR%>"></asp:LinkButton>


                                            <asp:LinkButton ID="lnkAsign" runat="server" ForeColor="Blue" Font-Names="Verdana" OnClick="lnkAsign_Click" Text="<%$  Resources:ValidationResources, LinkGetPendReq %>"></asp:LinkButton></td>


                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chkLanguage" runat="server" AutoPostBack="True" Text="<%$ Resources:ValidationResources,IvLang%>"
                                                Visible="False" OnCheckedChanged="chkLanguage_CheckedChanged" /></td>
                                         <td colspan="3">
                                            <asp:DropDownList ID="cboLanguage_new" Height="30" onblur="this.className='blur'" OnSelectedIndexChanged="cboLanguage_new_SelectedIndexChanged" onfocus="this.className='focus'" runat="server" AutoPostBack="True" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'>
                                            </asp:DropDownList></td>
                                        <td></td>

                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label39" runat="server" Text="<%$ Resources:ValidationResources,LDeptm %>"></asp:Label>
                                             </td>
                                        <td colspan="2">
                                            <asp:ListBox ID="cmbdept" Width="100%" runat="server" AutoPostBack="true" SelectionMode="Single" OnSelectedIndexChanged="cmbdept_SelectedIndexChanged"></asp:ListBox>
                                              <asp:Label ID="Label5" runat="server" Width="1px" CssClass="star" Height="3px">*</asp:Label>

                                            <asp:Label ID="lblDept" runat="server" CssClass="opt" Visible="False" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></asp:Label></td>
                                        
                                           <%-- <asp:Label ID="Label5" runat="server" Width="1px" CssClass="star" Height="3px">*</asp:Label>--%>
                                        

                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label6" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LINumber %>"></asp:Label></td>
                                        <td colspan="2">
                                            <input class="txt10" id="txtIndentNumber" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="text" maxlength="16" size="38" name="txtIndentNumber"
                                                runat="server" style="width:98% !important">
                                            <asp:Label ID="lblIndentNo" runat="server" CssClass="opt" Visible="False" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></asp:Label></td>
                                        <td></td>
                                        <td></td>

                                    </tr>
                                         <tr>
                                        <td>
                                            <asp:Label ID="Label49" runat="server" Text="<%$ Resources:ValidationResources,rptIndentDate %>" CssClass="span"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="txtindentdate" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                            <asp:Label ID="Label10" runat="server" Width="1px" CssClass="star" Height="3px">*</asp:Label>

                                        </td>
                                        <td style="text-align:right;">

                                            <asp:Label ID="Label14" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LITime %>"></asp:Label></td>
                                        <td>
                                            <input class="txt10" id="txtindenttime" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="text" name="Text3" runat="server"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
       <asp:Label ID="lblIndentDate" runat="server" CssClass="opt" Visible="False" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></asp:Label></td>
                                        <td></td>
                                        <td>
                                            <asp:Label ID="lblIndentTime" runat="server" CssClass="opt" Visible="False" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></asp:Label></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label51" runat="server" Text="<%$ Resources:ValidationResources,LReque %>"></asp:Label>
       </td>
                                        <td colspan="3">
                                            <asp:DropDownList ID="cmbreq" onblur="this.className='blur'" onfocus="this.className='focus'" runat="server" CssClass="txt10"
                                                Font-Names='<%$ Resources:ValidationResources, TextBox1 %>' ToolTip="Press Alt+R to add New Requester" onchange="GetServer1('cmbreq','hReq') ">
                                            </asp:DropDownList>
                                            <asp:Label ID="Label30" runat="server" CssClass="star" Height="3px" Width="1px">*</asp:Label></td>
                                        <td></td>
                                    </tr>
                                    <tr>

                                        <td colspan="5">
                                            <asp:Label ID="lblRequester" runat="server" CssClass="opt" Visible="False" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></asp:Label></td>

                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label55" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LTitle %>"></asp:Label>
  </td>
                                        <td colspan="4">
                                            <%--OnTextChanged="GetServer(this.id,'txttitle')"--%>
                                            <asp:TextBox ID="txttitle" runat="server" Columns="30" MaxLength="100" Style="width: 87.5%" CssClass="txt10" onblur="this.className='blur'" onfocus="this.className='focus'"></asp:TextBox>
                                            <ajax:AutoCompleteExtender ID="ExtTitle" runat="server" TargetControlID="txttitle"
                                                MinimumPrefixLength="0"
                                                CompletionInterval="50"
                                                CompletionSetCount="50"
                                                CompletionListCssClass="ITitle"
                                                OnClientItemSelected="ISelect"
                                                ServicePath=""
                                                EnableCaching="true"
                                                ServiceMethod="GetTitle">
                                            </ajax:AutoCompleteExtender>
                                            <asp:HiddenField ID="hdnIndId" runat="server" Value="" />
                                            <asp:HiddenField ID="hdnNonInd" runat="server" Value="" />
                                            <asp:Image ID="Image6" runat="server" ImageUrl="~/Images/sugg.png" />
                                            <asp:Label
                                                ID="Label31" runat="server" CssClass="star">*</asp:Label>   
                                            <asp:LinkButton ID="LinkButton1" CssClass="span" Style="font-size: 16px" runat="server" Font-Names="Verdana"
                         OnClick="LinkButton1_Click" ForeColor="Blue" Text="<%$ Resources:ValidationResources,LTitle %>" ValidationGroup="T"></asp:LinkButton>
 </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label21" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LSTitle %>"></asp:Label></td>
                                        <td colspan="3">
                                            <input class="txt10" id="txtSubtitle" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="text" maxlength="200" name="txtSubtitle" runat="server"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:Label ID="Label56" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LStatementofres %>"></asp:Label></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:DropDownList ID="persontype" onblur="this.className='blur'" onfocus="this.className='focus'" Height="30"
                                                runat="server" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'>
                                                <asp:ListItem Value="Author" Selected="True" Text="<%$resources: ValidationResources, Auth %>"></asp:ListItem>
                                                <asp:ListItem Value="Compiler" Text="<%$resources: ValidationResources, LComp %>"></asp:ListItem>
                                                <asp:ListItem Value="Editor" Text="<%$resources: ValidationResources, LEditor %>"></asp:ListItem>
                                                <asp:ListItem Value="Illustrator" Text="<%$resources: ValidationResources, LIllus %>"></asp:ListItem>
                                                <asp:ListItem Value="Translator" Text="<%$resources: ValidationResources, LTrsltr %>"></asp:ListItem>
                                            </asp:DropDownList></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Label ID="Label7" runat="server" CssClass="opt" Text="<%$ Resources:ValidationResources,LFname %>"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="Label11" runat="server" CssClass="opt" Text="<%$ Resources:ValidationResources,LMname %>"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="Label13" runat="server" CssClass="opt" Text="<%$ Resources:ValidationResources,LLname %>"></asp:Label></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LI %>"></asp:Label></td>
                                        <td>
                                            <input class="txt10" id="txtfname1" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="text" maxlength="30" size="16" name="txtau1" runat="server">
                                            <asp:Label ID="Label3" runat="server" Width="1px" CssClass="star" Height="3px">*</asp:Label></td>
                                        <td>
                                            <input class="txt10" id="txtmname1" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="text" maxlength="30" size="15" name="txtau2" 
                                                </td>
                                                <td>
                                            <input class="txt10" id="txtlname1" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="text" maxlength="30" size="16" name="txtau3" runat="server"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label25" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LII %>"></asp:Label></td>
                                        <td>
                                            <input class="txt10" id="txtfname2" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="text" size="16" name="Text9" runat="server"></td>
                                        <td>
                                            <input class="txt10" id="txtmname2" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="text" size="15" name="Text10" runat="server"></td>
                                        <td>
                                            <input class="txt10" id="txtlname2" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="text" size="16" name="Text11" runat="server"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label26" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LIII %>"></asp:Label></td>
                                        <td>
                                            <input class="txt10" id="txtfname3" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="text" size="16" name="Text12" runat="server"></td>
                                        <td>
                                            <input class="txt10" id="txtmname3" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="text" size="15" name="Text13" runat="server"></td>
                                        <td>
                                            <input class="txt10" id="txtlname3" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="text" size="16" name="Text14" runat="server"></td>
                                        <td></td>
                                    </tr>
                                       <tr>
                                        <td>
                                            <asp:Label ID="Label54" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LSeries %>"></asp:Label></td>
                                        <td colspan="3">
                                            <input class="txt10" id="txtSeries" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="text" maxlength="240" name="Text4" runat="server" size="63"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
<asp:Label ID="HypLang" Text="<%$ Resources:ValidationResources,LLanguage %>" runat="server"></asp:Label>
</td>
                                        <td>
                                            <asp:DropDownList ID="cmbLanguage" onblur="this.className='blur'" onfocus="this.className='focus'" runat="server" Height="30"
                                                CssClass="txt10" ToolTip="Press Alt+L to add New Media Type">
                                            </asp:DropDownList>
                                            <asp:Label ID="Label28" runat="server" CssClass="star" Height="3px" Width="1px">*</asp:Label></td>
                                        <td>
                                            <asp:Label ID="Label63" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LEdition %>"></asp:Label></td>
                                        <td>
                                            <input class="txt10" id="txtedition" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="text" maxlength="10" size="8" name="txtedition" runat="server"></td>
                                        <td></td>
                                    </tr>
                                      <tr>
                                        <td>
                                            <asp:Label ID="Label67" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LEditionY %>"></asp:Label></td>
                                        <td>
                                            <input class="txt10" onkeypress="IntegerNumber(this);" id="txtyrofedition" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="text" maxlength="4" size="7" name="txtyrofedition" runat="server"></td>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LPubY %>"></asp:Label></td>
                                        <td>
                                            <input class="txt10" onkeypress="IntegerNumber(this);" id="txtPubYear" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="text" maxlength="4" size="8" name="txtPubYear" runat="server"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label72" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LVP %>"></asp:Label></td>
                                        <td>
                                            <input class="txt10" id="txtvolno" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="text" maxlength="25" style="width: 45%" name="txtvolno" runat="server" size="5">
                                            <input class="txt10" id="txtPart" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="text" maxlength="25" style="width: 44%" name="txtPart" runat="server" size="5"></td>
                                        <td>
                                            <asp:Label ID="Label76" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LISSN %>"></asp:Label></td>
                                        <td>
                                            <input class="txt10" id="txtisbn" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="text" maxlength="20" size="15" name="txtisbn" runat="server"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label65" runat="server" Text="<%$ Resources:ValidationResources,rptcat %>"> </asp:Label>
                                            </td>
                                        <td>
                                            <select class="txt10" id="cmbbookcategory" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" name="cmbbookcategory" runat="server">
                                            </select>
                                            <asp:Label ID="Label15" runat="server" Width="1px" CssClass="star" Height="3px">*</asp:Label></td>
                                        <td>
                                            <asp:Label ID="Label52" runat="server" Text="<%$ Resources:ValidationResources,LbMediaType %>"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="mediatype" onblur="this.className='blur'" onfocus="this.className='focus'" runat="server" Height="30"
                                                CssClass="txt10" ToolTip="Press Alt+M to add New Media Type">
                                            </asp:DropDownList>
                                            <asp:Label ID="Label8" runat="server" CssClass="star">*</asp:Label>
                                        </td>
                                        <td>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label53" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LExRateB %>"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="cmbgocorbank" onblur="this.className='blur'" onfocus="this.className='focus'" Height="30"
                                                runat="server" CssClass="txt10">
                                            </asp:DropDownList></td>
                                        <td>
                                            <asp:Label ID="Label70" runat="server" Text="<%$ Resources:ValidationResources,LCurrency %>"></asp:Label>
                                            </td>
                                        <td>
                                            <asp:DropDownList ID="cmbcurr" onblur="this.className='blur'" onfocus="this.className='focus'" runat="server" AutoPostBack='true' Height="30" OnSelectedIndexChanged="cmbcurr_SelectedIndexChanged" CssClass="txt10" ToolTip="Press Alt+C to add New Currency">
                                            </asp:DropDownList>
                                            <asp:Label ID="Label23" runat="server" CssClass="star">*</asp:Label>
                                        </td>
                                        <td>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label74" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LExRate %>"></asp:Label></td>
                                        <td>
                                            <input class="txt10" id="txtExchangeRate" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="text" size="8" name="txtExchangeRate" runat="server"></td>
                                        <td>
                                            <asp:Label ID="Label18" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LNoCopies %>"></asp:Label></td>
                                        <td>
                                            <input class="txt10" id="txtnoofcopies" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="text" maxlength="4"
                                                name="txtnoofcopies" runat="server">
                                            <asp:Label ID="Label16" runat="server" CssClass="star">*</asp:Label></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label73" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LCouNo %>"></asp:Label></td>
                                        <td>
                                            <input class="txt10" id="txtcoursenm" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="text" maxlength="25" size="8" name="txtcoursenm" runat="server"></td>
                                        <td>
                                            <asp:Label ID="Label17" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LNoStudent %>"></asp:Label></td>
                                        <td>
                                            <input class="txt10" onkeypress="IntegerNumber(this);" id="txtnoofstud" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="text" maxlength="4" size="8" name="txtnoofstud" runat="server"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label69" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LPricePCpy %>"></asp:Label></td>
                                        <td>
                                            <input class="txt10" id="txtprice" onkeypress="decimalNumber(this);" onkeyup="txtprice_OnPropertyChange();" type="text" maxlength="8" size="8" name="txtprice"
                                                runat="server">
                                            <asp:Label ID="Label24" runat="server" CssClass="star">*</asp:Label></td>

                                        <td>
                                            <asp:Label ID="lblCurrency" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LTotAmount %>"></asp:Label></td>
                                        <td>
                                            <input class="txt10" id="txtTotalAmount" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="text" maxlength="25" size="8" name="txtTotalAmount" runat="server" autocomplete="on"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:HyperLink ID="Label64" runat="server" Text="<%$ Resources:ValidationResources,LPubli %>" onclick="CallPub2()"></asp:HyperLink>
                                            </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtCmbPublisher" runat="server" BorderWidth="1px" CssClass="txt10"
                                                Columns="30"></asp:TextBox>
                                            <asp:Image ID="Image7" runat="server" ImageUrl="~/Images/sugg.png" />
                                            <asp:Label ID="Label9" runat="server" CssClass="star" Height="3px" Width="1px">*</asp:Label>
                                            <ajax:AutoCompleteExtender ID="ExtPubl" runat="server" TargetControlID="txtCmbPublisher"
                                                MinimumPrefixLength="0"
                                                CompletionInterval="50"
                                                CompletionSetCount="50"
                                                CompletionListElementID="12"
                                                FirstRowSelected="true"
                                                CompletionListCssClass="ITitle"
                                                OnClientItemSelected="TPublSel"
                                                ServicePath="MssplSugg.asmx"
                                                EnableCaching="true" SkinID="None"
                                                ServiceMethod="GetPubl">
                                            </ajax:AutoCompleteExtender>
                                            <asp:HiddenField ID="hdPubId" runat="server" />



                                        </td>
                                         <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:HyperLink ID="Label68" runat="server" Text="<%$ Resources:ValidationResources,LVen %>" onclick="callVend()"></asp:HyperLink>
                                             </td>
                                        <td colspan="3">

                                            <asp:TextBox ID="txtCmbVendor" runat="server" CssClass="txt10"
                                                Columns="30"></asp:TextBox>
                                            <asp:Image ID="Image8" runat="server" ImageUrl="~/Images/sugg.png" />
                                            <asp:Label ID="Label4" runat="server" CssClass="star">*</asp:Label>
                                            <ajax:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtCmbVendor"
                                                MinimumPrefixLength="0"
                                                CompletionInterval="50"
                                                CompletionSetCount="50"
                                                CompletionListElementID="12"
                                                FirstRowSelected="true"
                                                CompletionListCssClass="ITitle"
                                                OnClientItemSelected="TVendSel"
                                                ServicePath="MssplSugg.asmx"
                                                EnableCaching="true" SkinID="None"
                                                ServiceMethod="GetVendor">
                                            </ajax:AutoCompleteExtender>
                                            <input id="HdVendorid" runat="server" type="hidden" style="width: 32px" />
                                            <asp:HiddenField ID="HiddenField1" runat="server" />

                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>

                                        <td>
                                            <input id="chkapproval" type="checkbox" value="" style="text-align: left" name="chkapproval" runat="server" checked="CHECKED" />
                                            <asp:Label ID="Label71" runat="server" CssClass="opt" Text="<%$Resources:ValidationResources,IchkApp %>"></asp:Label>



                                        </td>
                                           <td>
                                            <input id="chkStanding" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="checkbox" value="" name="chkapproval" runat="server" visible="false">
                                            <asp:Label ID="Label20" runat="server" CssClass="opt" Visible="False" Text="<%$ Resources:ValidationResources,IChkStand %>"></asp:Label></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblRemarks" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,Rmrks %>" Visible="False"></asp:Label></td>
                                        <td colspan="3">
                                            <input id="txtRemarks" runat="server" class="txt10" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="text" visible="false" /></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblVerify" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,IChkVer %>"
                                                Visible="False"></asp:Label></td>
                                        <td colspan="3">
                                            <input id="cmdIndentedBy" runat="server" type="button" value="Indented By" visible="false" onserverclick="cmdIndentedBy_ServerClick" class="btnstyle"/>

                                            <input id="cmdVerifiedBy" runat="server" type="button" value="Verified By" onserverclick="cmdVerifiedBy_ServerClick" visible="false" class="btnstyle" />
                                            <input id="cmdCheckedBy" runat="server" type="button" value="Checked By" onserverclick="cmdCheckedBy_ServerClick" visible="false" class="btnstyle"/>
                                            <input id="cmdPassedBy" runat="server" type="button" value="Passed By" onserverclick="cmdPassedBy_ServerClick"
                                                visible="false" class="btnstyle" /></td>
                                        <td></td>
                                    </tr>

                                    <tr>
                                        <td></td>
                                        <td></td>
                                         <td colspan="3">

                                            <asp:CheckBox ID="chkVerify" runat="server" CssClass="opt" AutoPostBack="True" Text="<%$Resources:ValidationResources,IChkVer %>" OnCheckedChanged="chkVerify_CheckedChanged" Visible="false"></asp:CheckBox>

                                            <input id="cmdIndentData" runat="server" causesvalidation="false" onserverclick="cmdIndentData_ServerClick"
                                                type="button" value="<%$Resources:ValidationResources,bIData %>" class="btnstyle" /></td>
                                    </tr>

                                    <tr>
                                        <td style="text-align: center" colspan="4">

                                            <input id="cmdsave" type="submit" value="<%$Resources:ValidationResources,bSave %>" name="cmdsave" style="display:none" runat="server" class="btnstyle">

                                            <asp:Button ID="cmdsave1" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="cmdsave1_Click"/>

                                            <input id="cmdreset" type="button" value="<%$Resources:ValidationResources,bReset %>" name="cmdreset"
                                    style="display:none"  runat="server" class="btnstyle">

                                            <asp:Button ID="cmdreset1" runat="server" Text="Reset" CssClass="btn btn-primary" OnClick="cmdreset1_Click"/>

                                            <input id="cmddelete" onclick="if (DoConfirmation() == false) return false;" style="display:none" type="button" value="<%$Resources:ValidationResources,bDelete %>" name="cmddelete" runat="server" class="btnstyle">

                                            
                                            <asp:Button ID="cmddelete1" runat="server" Text="Delete" CssClass="btn btn-primary" OnClick="cmddelete1_Click"/>

                                            <asp:Button ID="cmdPrint1" runat="server"  Text="<%$Resources:ValidationResources,iPrint %>" OnClick="cmdPrint1_Click" CssClass="btn btn-primary"></asp:Button>

                                        </td>
                                        <td></td>
                                    </tr>

                                    <tr>
                                        <td>

                                            <asp:CheckBox ID="chkSearch" runat="server" AutoPostBack="True" Text=" " OnCheckedChanged="chkSearch_CheckedChanged" />
                                            <asp:Label ID="Label29" runat="server" Text="<%$ Resources:ValidationResources,bSearch %>" CssClass="opt"></asp:Label></td>
                                        <td>
                                            <asp:RadioButton ID="optIndent1" runat="server" AutoPostBack="True" Checked="True" OnCheckedChanged="optIndent1_CheckedChanged"  GroupName="a" Text=" "/>
                                            <asp:Label ID="lbloptIndent1" runat="server" CssClass="opt" Text="<%$ Resources:ValidationResources,INoBasedSrch %>"></asp:Label></td>
                                        <td colspan="3">
                                              <asp:RadioButton ID="optAdvance1" runat="server" AutoPostBack="True" OnCheckedChanged="optAdvance1_CheckedChanged" GroupName="a" Text=" "/>
                                             <asp:Label ID="lbloptAdvance1" runat="server" CssClass="opt" Text="<%$ Resources:ValidationResources,RBAdSea%>"></asp:Label>
                                        </td>
                                      
                                    </tr>
                                    <tr>
                                       
                                      
                                               <td colspan="2">
                                                        <asp:DropDownList ID="ddl1" onblur="this.className='blur'" onfocus="this.className='focus'" runat="server" Height="30"
                                                            CssClass="txt10">
                                                            <asp:ListItem Value="Title" Selected="True" Text="<%$ Resources:ValidationResources, LITBase %>"> </asp:ListItem>
                                                            <asp:ListItem Value="Author" Text="<%$ Resources:ValidationResources, ACEITrans %>"></asp:ListItem>
                                                            <asp:ListItem Value="Vendor" Text="<%$ Resources:ValidationResources, LIVBase %>"></asp:ListItem>
                                                            <asp:ListItem Value="Publisher" Text="<%$ Resources:ValidationResources, PubBase %>"></asp:ListItem>
                                                            <asp:ListItem Value="Series" Text="<%$ Resources:ValidationResources, LISeriesB %>"></asp:ListItem>
                                                            <asp:ListItem Value="ISBN" Text="<%$ Resources:ValidationResources, LIISBN %>"></asp:ListItem>
                                                        </asp:DropDownList></td>
                                              <td colspan="2">
                                                        <input class="txt10" id="txtCategory" onblur="this.className='blur'"
                                                            onfocus="this.className='focus'" type="text" size="26" name="txtCategory" runat="server" onkeydown="callSubmit()">
                                                        

                                                    </td>
                                          <td>
                                           <input id="btnCategoryFilter" type="button" value="<%$Resources:ValidationResources,bSearch %>" name="btnCategoryFilter" runat="server" onserverclick="btnCategoryFilter_ServerClick" onkeydown="callSubmit()" class="btnstyle">
                                            </td>
                                        </tr>
                                          
                                         <tr>
                                                  <td colspan="2"></td>
                                                    <td colspan="3">
                                                        <asp:ListBox ID="lstAllCategory" onblur="this.className='blur'" onfocus="this.className='focus'" runat="server" CssClass="txt10" AutoPostBack="true" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>' Style="height: 80px!important" Visible="true"></asp:ListBox>
                                                    </td>
                                                </tr>
                                 


                                    <tr>
                                        <td colspan="5">
                                            <input id="hReq" runat="server" type="hidden" />

                                            <div id="MyDiv" runat="server">
                                            </div>
                                            <asp:Button ID="cmdSerach" Visible="false" runat="server" CausesValidation="False" class="btnstyle" OnClick="cmdSerach_Click"
                                                Text="<%$ Resources:ValidationResources, Go %> " UseSubmitBehavior="False" />
                                    </tr>
                                </table>
                                   <div style="display:none">
                                <INPUT id="txtchangeval"  type="hidden" size="2" name="txtchangeval"
										runat="server">
                                     <input id="hdPublisherId" runat="server"  type="hidden" /> 
                                    <input id="Hdventag" runat="server"  type="hidden" /><input id="Hdvenid"
                                        runat="server"  type="hidden" />
                                        <INPUT id="Button1" style="WIDTH: 1px; HEIGHT: 1px; visibility :hidden " onserverclick="Button1_ServerClick" class="btnH" type="button" value="Button"
								name="Button1" runat="server">
                                       <input id="hdCheckBudget" runat="server" style="width: 9px" type="hidden" /><input id="hdOnlineP" runat="server" style="width: 17px" type="hidden" /><input id="hdIndentStage" runat="server" style="width: 14px" type="hidden" />
                                       <input id="hdCmdSave" runat="server" style="width: 1px; height: 1px; visibility:hidden" onserverclick="hdCmdSave_ServerClick" type="button" />

                                       <input id="hdValidUserId" runat="server" style="width: 1px" type="hidden" />
                                   <input id="hdAskValidate" runat="server" style="width: 5px" type="hidden" />
             <input id="hdCulture"  type="hidden" runat="server" /><input id="hdIndentNumber" style="width: 40px" type="hidden" runat="server" /><input
                                id="hdBefSave" runat="server" style="width: 51px" type="hidden" /><input id="hdIndentIdR" runat="server"
                                style="width: 32px" type="hidden" /><INPUT id="hCurrentIndex2" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="hCurrentIndex2"
								runat="server"><INPUT id="hSubmit1" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" value="0"
								name="hSubmit1" runat="server"><INPUT id="hdTop" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="hdTop"
								runat="server"><INPUT id="hdTemp" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="hdTemp"
								runat="server"><INPUT id="hdindentId" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="hdindentId"
								runat="server"><INPUT id="hdMode" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="hdMode"
								runat="server"><INPUT id="hditmeId" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="hditmeId"
								runat="server"><input id="hrDate" value='<%$ Resources:ValidationResources, dateFormat1 %>' type="hidden" runat="server" style="width: 32px" /><input id="HComboSelect" type="hidden" runat="server" value='<%$ Resources:ValidationResources, ComboSelect %>' style="width: 24px" /><input id="hdReport" runat="server" style="width: 16px" type="hidden" /><input id="HNewForm" runat="server" style="width: 16px" type="hidden" /><input id="HWhichFill" runat="server" style="width: 16px" type="hidden" />
                                <input id="HCondition" runat="server" style="width: 24px" type="hidden" /><input id="HdAffCurr" runat="server" style="width: 16px" type="hidden" /><input id="hd_title" runat="server" type="hidden" style="width: 32px" /><INPUT id="Hidden5" style="WIDTH: 16px; HEIGHT: 22px"
				type="hidden" size="1" name="Hidden5" runat="server"><INPUT id="Hidden4" style="WIDTH: 16px; HEIGHT: 22px"
				type="hidden" size="1" name="Hidden4" runat="server">
                            <input id="btnFillPub" runat="server" style="width: 1px; height: 1px;" type="button" value="button" causesvalidation="false" class="btnH" OnServerClick ="btnFillPub_ServerClick" />
                                        <asp:listbox id="hlstAllCategory" runat="server" Width="0px" Height="0px"></asp:listbox>
                             
                                <input id="btnPub" runat="server" onclick="openNewForm('btnFillPub', 'PublisherMaster', 'HNewForm', 'HWhichFill', 'HCondition');" accesskey="P" style="width: 1px; height: 1px; visibility:hidden" type="button"
                                value="button" class="btnH" />
                                <input id="btnReq" runat="server" onclick="openNewForm('btnFillPub', 'UserManagement', 'HNewForm', 'HWhichFill', 'HCondition');" accesskey="R" style="width: 1px; height: 1px; visibility:hidden" type="button"
                                value="button" class="btnH"/>
                                <input id="btnVen" runat="server" onclick="openNewForm('btnFillPub', 'VendorMaster', 'HNewForm', 'HWhichFill', 'HCondition');" accesskey="V" style="width: 1px; height: 1px; visibility:hidden" type="button"
                                value="button" class="btnH" />
                                <input id="btnDep" runat="server" onclick="openNewFormdep('btnFillPub', 'DepartmentMaster', 'HNewForm', 'HWhichFill', 'HCondition', 'Indent', 'hdCheckBudget');" accesskey="T" style="width: 1px; height: 1px ;visibility:hidden" type="button"
                                value="button" class="btnH" />
                                <input id="btnCurrency" runat="server" onclick="openNewForm('btnFillPub', 'ExchangeMaster', 'HNewForm', 'HWhichFill', 'HCondition');" accesskey="C" style="width: 1px; height: 1px; visibility:hidden" type="button"
                                value="button" class="btnH" />
                                <input id="btnMedia" runat="server" onclick="openNewForm('btnFillPub', 'frm_mediatype', 'HNewForm', 'HWhichFill', 'HCondition');" accesskey="M" style="width: 1px; height: 1px; visibility:hidden" type="button"
                                value="button" class="btnH" />
                                <input id="btnLanguage" runat="server" onclick="openNewForm('btnFillPub', 'TranslationLanguages', 'HNewForm', 'HWhichFill', 'HCondition');" accesskey="L" style="width: 1px; height: 1px; visibility:hidden" type="button"
                                value="button" class="btnH"/>
                                <input id="btnCategory" runat="server" onclick="openNewForm('btnFillPub', 'CategoryLoadingStatus', 'HNewForm', 'HWhichFill', 'HCondition');" accesskey="G" style="width: 1px; height: 1px; visibility:hidden" type="button"
                                value="button" class="btnH"/>
                              
                            <input id="btnFilEntries" runat="server" style="width: 1px; height: 1px; visibility:hidden" class="btnH" type="button"
                                value="button" onserverclick="btnFilEntries_ServerClick" />
                                       <asp:TextBox ID="TextBox1" runat="server"  Width="1px" class="btnH" 
                            CssClass="btnH"></asp:TextBox>
			
                                   </div>
                         </ContentTemplate>

                            <Triggers>

                                <asp:PostBackTrigger ControlID="cmdprint1" />


                            </Triggers>
                        </asp:UpdatePanel>
              <div> 
             
             <asp:RequiredFieldValidator ID="RVindentdate" runat="server" ControlToValidate="txtindentdate"
                            Display="None" ErrorMessage='<%$ Resources:ValidationResources, IvDate %>' SetFocusOnError="True" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></asp:RequiredFieldValidator>
                  <asp:requiredfieldvalidator id="TitleFieldValidator" runat="server" Font-Size="11px" ErrorMessage='<%$ Resources:ValidationResources, IvTitle %>'
							Display="None" ControlToValidate="txttitle" SetFocusOnError="True" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></asp:requiredfieldvalidator><asp:requiredfieldvalidator id="firstname" runat="server" Font-Size="11px" ErrorMessage='<%$ Resources:ValidationResources, IvFName %>'
							Display="None" ControlToValidate="txtfname1" SetFocusOnError="True" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></asp:requiredfieldvalidator>
                  <asp:requiredfieldvalidator id="NoofCopies" runat="server" Width="68px" Height="13px" Font-Size="11px" ErrorMessage='<%$ Resources:ValidationResources, IvCopyNo %>'
							Display="None" ControlToValidate="txtnoofcopies" SetFocusOnError="True" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></asp:requiredfieldvalidator>
                  <asp:requiredfieldvalidator id="price" runat="server" Width="31px" Height="13px" Font-Size="11px" ErrorMessage="<%$ Resources:ValidationResources, EPrice %>"
							Display="None" ControlToValidate="txtprice" SetFocusOnError="True" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></asp:requiredfieldvalidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCmbPublisher"
                            Display="None" ErrorMessage='<%$ Resources:ValidationResources, IvPub %>' SetFocusOnError="True"></asp:RequiredFieldValidator>
                        <input id="confirmBefSave" runat="server" causesvalidation="false" style="width: 1px;height: 1px; visibility:hidden" class="btnH" type="button" value="button" onclick="if (ConfirmbefSave('hdBefSave') == false) return false;" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCmbVendor"
                            Display="None" ErrorMessage='<%$ Resources:ValidationResources, IvVen %>' SetFocusOnError="True"></asp:RequiredFieldValidator>
                   <asp:validationsummary id="ValidationSummary1" runat="server" Width="208px" Height="32px" Font-Size="11px"
							BackColor="Window" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></asp:validationsummary>
                        <asp:RequiredFieldValidator ID="rf" runat="server" ErrorMessage='<%$ Resources:ValidationResources, IvTitle %>' ControlToValidate="txttitle" Display="None" SetFocusOnError="True" ValidationGroup="T"></asp:RequiredFieldValidator>
                        <input id="cmdShow" runat="server" type="button" value="button" causesvalidation="false" style="visibility: hidden; width: 1px; height: 1px"/>
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="T"/>
                   
                         
                        </div> 
             </div>
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

            $("[id$=txtindentdate]").datepicker({

                changeMonth: true,//this option for allowing user to select month
                changeYear: true, //this option for allowing user to select from year range
                dateFormat: 'dd-M-yy'  // CHANGE DATE FORMAT.
            });

        }

     </script>
    <script type="text/javascript">
        function clientback(result, context) {
            if (context == 'txtExchangeRate') {
                document.getElementById(context).value = result;
                if (result == "") {
                    //    document.getElementById("txtnoofcopies").value = "";
                    document.getElementById("txtprice").value = "";
                    document.getElementById("txtTotalAmount").value = "";
                }
                var total;
                if (document.Form1.txtExchangeRate.value == "")
                    return false;
                else if (document.Form1.txtnoofcopies.value == "")
                    return false;
                else if (document.Form1.txtprice.value == "")
                    return false;
                else
                    total = ((document.Form1.txtExchangeRate.value) * (document.Form1.txtnoofcopies.value) * (document.Form1.txtprice.value));

                if (total.toFixed) {
                    document.Form1.txtTotalAmount.value = total.toFixed(2);
                }
                else {
                    document.Form1.txtTotalAmount.value = total;

                }
            } else if (context == 'lstAllCategory') {
                var ctrl = document.getElementById('lstAllCategory');
                for (var count = ctrl.options.length - 1; count > -1; count--) {
                    ctrl.options[count] = null;
                }

                var rows = result.split('|');
                for (var i = 0; i < rows.length - 1; ++i) {
                    var values = rows[i].split('^');
                    var option = document.createElement("OPTION");
                    option.value = values[0];
                    option.innerHTML = values[1];
                    ctrl.appendChild(option);
                }
            }

        }
    </script>
		<script  type="text/javascript">
            function txtCategory_OnKeyDown() {
                if (window.event.keyCode == 13) {
                    window.document.Form1.btnCategoryFilter.focus()
                }
            }
            function txtnoofcopies_OnPropertyChange() {
                var total;
                if (document.getElementById("<%=txtExchangeRate.ClientID%>").value == "")
                    return false;
                else if (document.getElementById("<%=txtnoofcopies.ClientID%>").value == "")
                    return false;
                else if (document.getElementById("<%=txtprice.ClientID%>").value == "")
                    return false;
                else
                    total = ((document.getElementById("<%=txtExchangeRate.ClientID%>").value) * (document.getElementById("<%=txtnoofcopies.ClientID%>").value) * (document.getElementById("<%=txtprice.ClientID%>").value));

                if (total.toFixed) {
                    document.getElementById("<%=txtTotalAmount.ClientID%>").value = total.toFixed(2);
                }
                else {
                    document.getElementById("<%=txtTotalAmount.ClientID%>").value = total;

                }
            }
            function txtprice_OnPropertyChange() {
                var t;
                if (document.getElementById("<%=txtExchangeRate.ClientID%>").value == "")
                    return false;
                else if (document.getElementById("<%=txtnoofcopies.ClientID%>").value == "")
                    return false;
                else if (document.getElementById("<%=txtprice.ClientID%>").value == "")
                    return false;
                else
                    t = ((document.getElementById("<%=txtExchangeRate.ClientID%>").value) * (document.getElementById("<%=txtnoofcopies.ClientID%>").value) * (document.getElementById("<%=txtprice.ClientID%>").value));
                if (t.toFixed) {
                    document.getElementById("<%=txtTotalAmount.ClientID%>").value = t.toFixed(2);
                }
                else {
                    document.getElementById("<%=txtTotalAmount.ClientID%>").value = t;
                }
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
             //   $('<thead></thead>').prependTo('#' + grdId).append($('#' + grdId + ' tr:first'));
               // ThreeLevelSearch($('#' + grdId), "[id$=dvgrd]", 200);
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
