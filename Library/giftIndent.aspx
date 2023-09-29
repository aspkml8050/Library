<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/LibraryMain.master" CodeBehind="giftIndent.aspx.cs" Inherits="Library.giftIndent" %>


<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>



<asp:Content ID="gIndtHead" runat="server" ContentPlaceHolderID="head">
    <link href="cssDesign/multiselect.css" rel="stylesheet" type="text/css" >
    <script src="FormScripts/multiselect.js"></script>
    <link href="cssDesign/libresponsive.css" rel="stylesheet" type="text/css" />

		<style>
      #divl2{
 overflow:auto;
 
 }
 .LayWidCont{
 margin-bottom : 110px !important;
 margin-top : 40px;
 }
		</style>
		 
		 <script  type="text/javascript">
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
             function onTextBoxUpdate(evt) {

                 var textBoxID = evt.source.textBoxID;
                 if (evt.selMenuItem != null) {
                     document.getElementById(textBoxID).value = evt.selMenuItem.label; // + " (modified by onTextboxUpdate)";                     
                     // document.getElementById("cmdSerach").click();
                 }
                 evt.preventDefault();
             }
             
             function GetpubJs(sender, arg) {
                 let id = arg.get_value();
                 $('[id$=hdUid]').val(id);
 
                 //  $('[id$=cmdcheck]').click();
             }
             function GetVenJs(sender, arg) {
                 let id = arg.get_value();
                 
                 $('[id$=Vendid]').val(id);
                 //  $('[id$=cmdcheck]').click();
             }
             function clientback(result, context) {
                  
                 if (context == 'txtgiftexrate') {
                     document.getElementById(context).value = result;
                     if (result == "") {
                         //    document.getElementById("txtnoofcopies").value = "";
                         document.getElementById("txtgiftprice").value = "";
                         document.getElementById("txtgiftamount").value = "";
                     }
                     var total;
                     if (document.Form1.txtgiftexrate.value == "")
                         return false;
                     else if (document.Form1.txtgiftnoofcopies.value == "")
                         return false;
                     else if (document.Form1.txtgiftprice.value == "")
                         return false;
                     else
                         total = ((document.Form1.txtgiftexrate.value) * (document.Form1.txtgiftnoofcopies.value) * (document.Form1.txtgiftprice.value));

                     if (total.toFixed) {
                         document.Form1.txtgiftamount.value = total.toFixed(2);
                     }
                     else {
                         document.Form1.txtgiftamount.value = total;

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

            function chk() {
                if (document.Form1.Hidden1.value == "2") {
                    window.scrollTo(0, 0);
                    document.Form1.Hidden1.value = "0";
                }

                if (document.Form1.hdTop.value == "top") {
                    window.scrollTo(0, 0);
                    document.Form1.hdTop.value = 0;
                }



                if (document.Form1.Hidden1.value == "10") {
                    document.Form1.cmbgiftcurrency.focus();
                    document.Form1.Hidden1.value = 0;
                }
                if (document.Form1.Hidden1.value == "111") {
                    document.Form1.txtCategory.focus();
                    document.Form1.Hidden1.value = 0;
                }



            }



		</script>
		<script  type="text/javascript">
            function txtCategory_OnKeyDown() {
                if (window.event.keyCode == 13) {
                    window.document.Form1.btnCategoryFilter.focus()
                }
            }
            function txtgiftnoofcopies_OnPropertyChange() {
                var total;
                if (document.getElementById("<%=txtgiftexrate.ClientID%>").value == "")
                    return false;
                else if (document.getElementById("<%=txtgiftnoofcopies.ClientID%>").value == "")
                    return false;
                else if (document.getElementById("<%=txtgiftprice.ClientID%>").value == "")
                    return false;
                else
                    total = ((document.getElementById("<%=txtgiftexrate.ClientID%>").value) * (document.getElementById("<%=txtgiftnoofcopies.ClientID%>").value) * (document.getElementById("<%=txtgiftprice.ClientID%>").value));

                if (total.toFixed) {
                    document.getElementById("<%=txtgiftamount.ClientID%>").value = total.toFixed(2);
                }
                else {
                    document.getElementById("<%=txtgiftamount.ClientID%>").value = total;

                }
            }
            function txtgiftprice_OnPropertyChange() {
                var t;
                if (document.getElementById("<%=txtgiftexrate.ClientID%>").value == "")
                    return false;
                else if (document.getElementById("<%=txtgiftnoofcopies.ClientID%>").value == "")
                    return false;
                else if (document.getElementById("<%=txtgiftprice.ClientID%>").value == "")
                    return false;
                else
                    t = ((document.getElementById("<%=txtgiftexrate.ClientID%>").value) * (document.getElementById("<%=txtgiftnoofcopies.ClientID%>").value) * (document.getElementById("<%=txtgiftprice.ClientID%>").value));
                if (t.toFixed) {
                    document.getElementById("<%=txtgiftamount.ClientID%>").value = t.toFixed(2);
                }
                else {
                    document.getElementById("<%=txtgiftamount.ClientID%>").value = t;
                }
            }
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

<asp:Content ID="gIndtMain" runat="server" ContentPlaceHolderID="MainContent">
   
          <div class="container tableborderst" >   
         <div class="no-more-tables" style="width:100%">
              <div style="width:100%;display:none" class="title">
                  <div style="width:89%;float:left" >&nbsp;
			<asp:label id="lblTitle" runat="server" style ="display:none" Width="100%">Gift Indent</asp:label>
                      </div>
                   <div style="float:right;vertical-align:top">
                        <a id="lnkHelp" href="#" style="display:none" onclick="ShowHelp('Help/Acquisitioning-Indents-Gift-Indent.htm')"><img alt="Help?" height="15" src="help.jpg"  /></a>
               </div></div>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                  <asp:UpdateProgress ID="UpPorg1" runat="server">
    <ProgressTemplate>
    <NN:Mak ID="FF1" runat="server" />
    </ProgressTemplate>
    </asp:UpdateProgress>
                                <asp:HiddenField ID="hdUid" runat="server" />
                               <%-- <asp:HiddenField ID="Vendid" runat="server" />--%>
                                <table id="Table6" class="table-condensed GenTable1">
                                    <tr>
                                        <td colspan="4">
                                            <asp:Label ID="msglabel" runat="server" CssClass="err"></asp:Label></td>

                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td colspan="3">
                                            <asp:LinkButton ID="lnkModify" runat="server" CausesValidation="False" CssClass="note" Width="346px" Text="<%$ Resources:ValidationResources,ContiSIndModfyCR %>" Font-Names="Verdana"></asp:LinkButton></td>

                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td colspan="3">
                                            <asp:LinkButton ID="lnkContinue" runat="server" CausesValidation="False" CssClass="note" Text="<%$ Resources:ValidationResources,ContiSIntENewR %>" Font-Names="Verdana"></asp:LinkButton></td>

                                    </tr>
                                    <tr>
                                        <td><%--<asp:hyperlink id="Label11" runat="server" Text="<%$Resources:ValidationResources,LDeptm%>" onclick="openNewForm('btnFillPub','DepartmentMaster','HNewForm','HWhichFill','HCondition');"></asp:hyperlink>--%>
                                            <asp:Label ID="Label11" runat="server" Text="<%$Resources:ValidationResources,LDeptm%>"></asp:Label>
                                        </td>
                                        <td colspan="2">
                                            <asp:ListBox ID="cmbdept" Width="100%" runat="server" AutoPostBack="true" SelectionMode="Single"></asp:ListBox>
                                          
                                           
                                        </td>
                                        <td> <asp:Label ID="Label26" runat="server" CssClass="star">*</asp:Label></td>

                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label3" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LINumber %>"></asp:Label></td>
                                        <td>
                                            <input class="txt10" id="txtgiftindentno" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="text" maxlength="214" size="18" name="txtgiftindentno" runat="server"></td>
                                        <td></td>
                                        <td></td>

                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:ValidationResources,LGiftIndentDate %>" CssClass="span"></asp:Label></td>
                                        <td>

                                           
                                            <asp:TextBox ID="txtgiftindentdate" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                            






                                            <asp:Label ID="Label41" runat="server" CssClass="star">*</asp:Label></td>
                                        <td>
                                            <asp:Label ID="Label4" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LITime %>"></asp:Label></td>
                                        <td>
                                            <input class="txt10" id="txtgiftindenttime" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="text" name="txtgiftindenttime" runat="server"></td>


                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:HyperLink ID="Label20" runat="server" Text="<%$ Resources:ValidationResources,GiftedBy %>" onclick="callVend()"></asp:HyperLink></td>
                                       
                                        <td colspan="3">
                                            <asp:TextBox ID="txtCmbVendor" runat="server" CssClass="txt10"
                                                Columns="30"></asp:TextBox>
                                            <ajax:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtCmbVendor"
                                                CompletionInterval="50"
                                                CompletionSetCount="50"
                                                MinimumPrefixLength="0"
                                                CompletionListCssClass="Publ"
                                                ServicePath="MssplSugg.asmx"
                                                ServiceMethod="GetVendor"
                                                OnClientItemSelected="GetVenJs">
                                            </ajax:AutoCompleteExtender>
                                            <asp:HiddenField ID="Vendid" runat="server" />
                                            
                                            <asp:Image ID="Image6" runat="server" ImageUrl="~/Images/sugg.png" />
                                            <asp:Label ID="Label13" runat="server" Text="*" Width="1px" CssClass="star"></asp:Label>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label7" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LTitle %>"></asp:Label></td>
                                        <td colspan="3">
                                            <input id="txtgifttitle" class="txt10" type="text" name="txtgifttitle" runat="server">
                                            <asp:Label ID="Label37" runat="server" CssClass="star">*</asp:Label>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label31" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LSTitle %>"></asp:Label></td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtSubtitle" runat="server" CssClass="txt10" Font-Names="<%$ Resources:ValidationResources, TextBox2 %>"></asp:TextBox></td>

                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <asp:Label ID="Label8" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LStatementofres %>"></asp:Label></td>

                                    </tr>
                                    <tr>
                                        <td style="width: 20%"></td>
                                        <td>
                                            <asp:DropDownList ID="cmbgiftpersontype" onblur="this.className='blur'" onfocus="this.className='focus'"
                                                runat="server" CssClass="txt10" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'>
                                                <asp:ListItem Value="Compiler" Text="<%$ Resources:ValidationResources,LComp %>"></asp:ListItem>
                                                <asp:ListItem Value="Editor" Text="<%$ Resources:ValidationResources,LEditor %>"></asp:ListItem>
                                                <asp:ListItem Value="Illustrator" Text="<%$ Resources:ValidationResources,LIllus %>"></asp:ListItem>
                                                <asp:ListItem Value="Translator" Text="<%$ Resources:ValidationResources,LTrsltr %>"></asp:ListItem>
                                                <asp:ListItem Value="Author" Selected="True" Text="<%$ Resources:ValidationResources,Auth %>"></asp:ListItem>
                                            </asp:DropDownList></td>
                                        <td style="width: 20%"></td>
                                        <td style="width: 30%">
                                            <input id="Strgift" type="hidden" size="4" name="Hidden2"
                                                runat="server"></td>

                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Label ID="Label35" runat="server" CssClass="opt" Text="<%$ Resources:ValidationResources,LFname %>"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="Label33" runat="server" CssClass="opt" Text="<%$ Resources:ValidationResources,LMname %>"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" CssClass="opt" Text="<%$ Resources:ValidationResources,LLname %>"></asp:Label></td>

                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label38" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LI %>"></asp:Label></td>
                                        <td>
                                            <input class="txt10" id="txtgiftfname1" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="text" name="txtgiftfname1" runat="server">
                                            <asp:Label ID="Label10" runat="server" CssClass="star">*</asp:Label></td>
                                        <td>
                                            <input class="txt10" id="txtgiftmname1" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="text" size="14" name="txtgiftmname1" runat="server"></td>
                                        <td>
                                            <input class="txt10" id="txtgiftlname1" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="text" name="txtgiftlname1" runat="server"></td>

                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label36" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LII %>"></asp:Label></td>
                                        <td>
                                            <input class="txt10" id="txtgiftfname2" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="text" name="txtgiftfname2" runat="server"></td>
                                        <td>
                                            <input class="txt10" id="txtgiftmname2" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="text" size="14" name="txtgiftmname2" runat="server"></td>
                                        <td>
                                            <input class="txt10" id="txtgiftlname2" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="text" name="txtgiftlname2" runat="server"></td>

                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label39" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LIII %>"></asp:Label></td>
                                        <td>
                                            <input class="txt10" id="txtgiftfname3" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="text" name="txtgiftfname3" runat="server"></td>
                                        <td>
                                            <input class="txt10" id="txtgiftmname3" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="text" name="txtgiftmname3" runat="server" size="14"></td>
                                        <td>
                                            <input class="txt10" id="txtgiftlname3" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="text" name="txtgiftlname3" runat="server"></td>

                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label6" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LSeries %>"></asp:Label></td>
                                        <td colspan="3">
                                            <input class="txt10" id="txtgiftseries" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="text" name="txtgiftseries" runat="server" size="62"></td>

                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label17" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LEdition %>"></asp:Label></td>
                                        <td>
                                            <input class="txt10" id="txtgiftedition" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="text" maxlength="2147" size="8" name="txtgiftedition"
                                                runat="server">
                                            <asp:Label ID="Labeledt" runat="server" CssClass="star">*</asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label18" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LEditionY %>"></asp:Label></td>
                                        <td>
                                            <input class="txt10" id="txtgiftyearofed" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="text" maxlength="4" size="5" name="txtgiftyearofed"
                                                runat="server"></td>

                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label27" runat="server" CssClass="span" Width="144px" Text="<%$ Resources:ValidationResources,LPubY %>"></asp:Label></td>
                                        <td>
                                            <input class="txt10" id="txtPubYear" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="text" maxlength="4" size="8" name="txtPubYear"
                                                runat="server">
                                            <asp:Label ID="Labeledtp" runat="server" CssClass="star">*</asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="HypLang" Text="<%$ Resources:ValidationResources,LLanguage %>" runat="server"></asp:Label>
                                           </td>
                                        <td>
                                            <asp:DropDownList ID="cmbLanguage" runat="server" CssClass="txt10" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'>
                                            </asp:DropDownList>
                                            <asp:Label ID="Label42" runat="server" CssClass="star">*</asp:Label></td>

                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label16" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LVP %>"></asp:Label></td>
                                        <td>
                                            <input class="txt10" id="txtgiftvolno" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="text" size="5" name="txtgiftvolno" runat="server" style="margin-bottom: 5px; width: 45%">
                                            <asp:TextBox ID="txtgiftpart" runat="server" CssClass="txt10" Style="margin-bottom: 5px; width: 44%" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></asp:TextBox></td>
                                        <td>
                                            <asp:Label ID="Label15" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LISSN %>"></asp:Label></td>
                                        <td>
                                            <input class="txt10" id="txtgiftisbn" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="text" name="txtgiftisbn" runat="server"></td>

                                    </tr>
                                    <tr>
                                        <td><%--<asp:hyperlink id="Label21" runat="server" Text="<%$Resources:ValidationResources,LCat%>" onclick="openNewForm('btnFillPub','CategoryLoadingStatus','HNewForm','HWhichFill','HCondition');"></asp:hyperlink>--%>
                                            <asp:Label ID="Label21" runat="server" Text="<%$Resources:ValidationResources,LCat%>"></asp:Label>
                                        </td>
                                        <td>
                                            <select class="txt10" id="cmbgiftcategory" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" name="cmbgiftcategory" runat="server">
                                            </select>
                                            <asp:Label ID="Label30" runat="server" CssClass="star">*</asp:Label></td>
                                        <td>
                                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:ValidationResources,MediaTyp %>"></asp:Label>
                                            <%--<asp:hyperlink id="Label5" runat="server" Text="<%$ Resources:ValidationResources,MediaTyp %>" onclick="openNewForm('btnFillPub','frm_mediatype','HNewForm','HWhichFill','HCondition');" ></asp:hyperlink>--%></td>
                                        <td>
                                            <asp:DropDownList ID="cmbgiftmediatype" CssClass="txt10"
                                                runat="server" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'>
                                            </asp:DropDownList>
                                            <asp:Label ID="Label14" runat="server" CssClass="star">*</asp:Label></td>

                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label22" runat="server" Text="<%$ Resources:ValidationResources,LCurrency %>"></asp:Label>
                                            <%--<asp:hyperlink id="Label22" runat="server" Text="<%$ Resources:ValidationResources,LCurrency %>" onclick="openNewForm('btnFillPub','ExchangeMaster','HNewForm','HWhichFill','HCondition');"></asp:hyperlink>--%></td>
                                        <td>
                                            <asp:DropDownList ID="cmbgiftcurrency" onblur="this.className='blur'" onfocus="this.className='focus'"
                                                runat="server" CssClass="txt10" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>' AutoPostBack="True">
                                            </asp:DropDownList>
                                            <asp:Label ID="Label40" runat="server" CssClass="star">*</asp:Label></td>
                                        <td>
                                            <asp:Label ID="Label23" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LExRate %>"></asp:Label></td>
                                        <td>
                                            <input class="txt10" id="txtgiftexrate" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="text" name="txtgiftexrate" runat="server"></td>

                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label25" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LNoCopies %>"></asp:Label></td>
                                        <td>
                                            <input class="txt10" id="txtgiftnoofcopies" onblur="this.className='blur'"
                                                onkeyup="txtgiftnoofcopies_OnPropertyChange();" onfocus="this.className='focus'"
                                                type="text" maxlength="5" size="5" name="txtgiftnoofcopies" runat="server">
                                            <asp:Label ID="Label29" runat="server" CssClass="star">*</asp:Label></td>
                                        <td>
                                            <asp:Label ID="Label24" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LPrice %>"></asp:Label></td>
                                        <td>
                                            <input class="txt10" onkeypress="decimalNumber(this);" id="txtgiftprice" onblur="this.className='blur'"
                                                onkeyup="txtgiftprice_OnPropertyChange();" onfocus="this.className='focus'"
                                                type="text" maxlength="7" size="6" name="txtgiftprice" runat="server">
                                            <asp:Label ID="Label28" runat="server" CssClass="star">*</asp:Label></td>

                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td>
                                            <asp:Label ID="lblCurrency" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LTotAmount %>"></asp:Label></td>
                                        <td>
                                            <input id="txtgiftamount" runat="server" class="txt10" name="txtgiftamount" onblur="this.className='blur'" onfocus="this.className='focus'" type="text" />

                                            <asp:Label ID="Label9" runat="server" CssClass="star">*</asp:Label></td>

                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:HyperLink ID="Label19" runat="server" Text="<%$ Resources:ValidationResources,LPubli %>" onclick="CallPub2()"></asp:HyperLink></td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtCmbPublisher" runat="server" BorderWidth="1px" CssClass="txt10"
                                                Columns="30"></asp:TextBox>
                                            <ajax:AutoCompleteExtender ID="ExtVend" runat="server" TargetControlID="txtCmbPublisher"
                                                CompletionInterval="50"
                                                CompletionSetCount="50"
                                                MinimumPrefixLength="0"
                                                CompletionListCssClass="Publ"
                                                ServicePath="MssplSugg.asmx"
                                                ServiceMethod="GetPubl"
                                                OnClientItemSelected="GetpubJs">
                                            </ajax:AutoCompleteExtender>
                                            <asp:HiddenField ID="hdPubId" runat="server" />
                                            <%--  <asp:HiddenField ID="HiddenField2" runat="server" />--%>
                                            <%-- <custom:autosuggestmenu  id="asmpublisher" runat="server" keypressdelay="1" maxsuggestchars="100" 
                                            ongetsuggestions="TopSearchService.GetSuggestionsPublisher" resourcesdir="~/asm_includes"
                                            targetcontrolid="txtCmbPublisher" updatetextboxonupdown="false" usepagemethods="false"></custom:autosuggestmenu>--%>

                                            <asp:Image ID="Image7" runat="server" ImageUrl="~/Images/sugg.png" />
                                            <asp:Label ID="Label12" runat="server" CssClass="star">*</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>

                                        <td colspan="4" style="text-align: center">
                                            <input id="cmdsubmit" type="button" value="<%$Resources:ValidationResources,bSave %>" name="cmdsubmit"
                             style="display:none;" runat="server" class="btnstyle">

                                            <asp:Button ID="cmdsave" runat="server" Text="Submit" CssClass="btn btn-primary" />

                                            <input id="cmdreset" type="button" value="<%$Resources:ValidationResources,bReset %>" name="cmdreset"
                             style="display:none;" runat="server" class="btnstyle">

                                            <asp:Button ID="cmdreset1" runat="server" Text="Reset" CssClass="btn btn-primary" />

                                            <input id="cmddelete" onclick="if (DoConfirmation() == false) return false;"  style="display:none;" type="button" value="<%$Resources:ValidationResources,bDelete %>" name="cmdreset" runat="server" class="btnstyle">
                                              <asp:Button ID="cmddelete1" runat="server" Text="Delete" CssClass="btn btn-primary" />

                                            <asp:Button ID="cmdPrint1" runat="server" Text="<%$Resources:ValidationResources, iprint%>" UseSubmitBehavior="False" CssClass="btnstyle"></asp:Button>

                                        </td>
                                     
                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chkSearch1" runat="server" AutoPostBack="True" Text=" " />
                                            <asp:Label ID="Label34" runat="server" CssClass="opt" Text="<%$ Resources:ValidationResources,bSearch %>"></asp:Label></td>
                                        <td >
                                            <asp:RadioButton ID="optindent1" runat="server" AutoPostBack="True" Checked="True" GroupName="a"/>
                                            <asp:Label ID="lbloptIndent1" runat="server" CssClass="opt" Text="<%$ Resources:ValidationResources,GINoBasedSrch %>"></asp:Label></td>
                                        <td colspan="2">
                                            <asp:RadioButton ID="optAdvance1" runat="server" AutoPostBack="True" GroupName="a" Text=" " />
                                            <asp:Label ID="lbloptAdvance1" runat="server" CssClass="opt" Text="<%$ Resources:ValidationResources,RBAdSea%>"></asp:Label>

                                        </td>

                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:DropDownList ID="ddl1" onblur="this.className='blur'" onfocus="this.className='focus'" runat="server"
                                                CssClass="txt10">
                                                <asp:ListItem Value="Title" Selected="True" Text="<%$ Resources:ValidationResources,TitleB%>"></asp:ListItem>
                                                <asp:ListItem Value="Author" Text="<%$ Resources:ValidationResources,ACEITrans%>"></asp:ListItem>
                                                <asp:ListItem Value="Gifted By" Text="<%$ Resources:ValidationResources,GiftBase%>"></asp:ListItem>
                                                <asp:ListItem Value="Publisher" Text="<%$ Resources:ValidationResources,PubBase%>"></asp:ListItem>
                                            </asp:DropDownList></td>
                                        <td colspan="2">
                                            <input class="txt10" onkeypress="disallowSingleQuote(this);" id="txtCategory" type="text" name="txtCategory" runat="server" style="width:65%">
                                           
                                            <input id="btnCategoryFilter" type="button" value="<%$ Resources:ValidationResources,bSearch %>" name="btnCategoryFilter" runat="server" class="btnstyle">

                                        </td>
                                    </tr>

                                                                                        
                                                <tr>
                                                    <td></td>
                                                    <td colspan="3">
                                                        <asp:ListBox ID="lstAllCategory"  AutoPostBack="true" onblur="this.className='blur'" onfocus="this.className='focus'" TabIndex="3" runat="server" CssClass="txt10" Style="height: 80px!important"></asp:ListBox></td>
                                                </tr>
                                           

                                    <tr>
                                        <td colspan="4">
                                            <input id="btnFillPub" runat="server" type="button" value="button" causesvalidation="false" style="display:none;" class="btnH" /></td>

                                    </tr>
                                </table>
                               <div style="display:none">
                                 <INPUT id="txtchangeval"  type="hidden" size="2" name="txtchangeval"
										runat="server">
                                    <input id="hdPublisherId" runat="server" style="width: 56px" type="hidden" />
                                    <input id="Hdventag" runat="server" style="width: 56px" type="hidden" />
                                    <input id="Hdvenid" runat="server" style="width: 50px" type="hidden" />

                        <INPUT id="hdUnableMsg" type="hidden" runat="server" style="width: 32px"><input id="js1" runat="server" 
                            type="hidden" value="<%$ Resources:ValidationResources, js1 %>" style="width: 32px"/><input id="hrDate" runat="server"  
                            type="hidden" value="<%$ Resources:ValidationResources, dateFormat1 %>" style="width: 32px"/><input id="HComboSelect" runat="server" 
                            type="hidden" value="<%$ Resources:ValidationResources, ComboSelect %>" style="width: 48px"/>
                                   <input id="dnewcombo" runat="server" 
                            type="hidden" value="<%$ Resources:ValidationResources, ComboSelect %>" style="width: 48px"/>
                                   <INPUT id="hCurrentIndex2" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="hCurrentIndex2"
							runat="server"><INPUT id="hSubmit1" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" value="0"
							name="hSubmit1" runat="server"><INPUT id="hidden6" style="WIDTH: 28px; HEIGHT: 22px" type="hidden" size="1" value="0"
							name="hidden6" runat="server"><input id="hdCulture" runat="server" style="width: 24px" type="hidden" /><INPUT id="hdTop" type="hidden" runat="server" style="width: 32px"><input id="hdreport" runat="server" name="hdreport" style="width: 35px" type="hidden" /><input id="hdgiftindentId" runat="server" name="hdgiftindentId" style="width: 31px"
                            type="hidden" /><input id="hdgiftitemId" runat="server" name="hdgiftitmeId" style="width: 31px" type="hidden" /><INPUT id="Hidden5" style="WIDTH: 16px; HEIGHT: 22px" type="hidden" size="1" name="Hidden2"
							runat="server"><INPUT id="txtdepartmentcode" onblur="this.className='blur'" style="WIDTH: 16px; HEIGHT: 22px"
							onfocus="this.className='focus'" type="hidden" size="1" name="Hidden2" runat="server"><INPUT id="yCoordHolder" style="WIDTH: 22px; HEIGHT: 22px" type="hidden" size="1" name="Hidden3"
							runat="server"><INPUT id="xCoordHolder" style="WIDTH: 22px; HEIGHT: 22px" type="hidden" size="1" name="Hidden2"
							runat="server"><INPUT id="Hidden4" style="WIDTH: 16px; HEIGHT: 22px" type="hidden" size="1" name="Hidden4"
							runat="server"><INPUT id="Hidden1" style="WIDTH: 24px; HEIGHT: 24px" type="hidden" size="1" name="Hidden1"
							runat="server"><input id="HNewForm" runat="server" style="width: 40px" type="hidden" /><input id="HWhichFill" runat="server" style="width: 24px" type="hidden" /><input id="HCondition" runat="server" style="width: 32px" type="hidden" /><input id="HdVendorid" runat="server" type="hidden" style="width: 40px" /><input id="HdAffCurr" runat="server" style="width: 40px" type="hidden" />
                                <INPUT id="Hidden3" style="WIDTH: 8px; HEIGHT: 22px" type="hidden" size="1" name="Hidden3"
							runat="server">
                        <INPUT id="Button1" style="WIDTH: 1px; HEIGHT:1px;" class="btnH" type="button" value="Button"
							name="Button1" runat="server">
                                        
						<asp:listbox id="hlstAllCategory" runat="server" Width="0px" Height="0px"></asp:listbox>   
                                 <input id="btnPub" runat="server" onclick="openNewForm('btnFillPub', 'PublisherMaster', 'HNewForm', 'HWhichFill', 'HCondition');" accesskey="P" style="width: 1px; height: 1px;" type="button"
                                value="button" class="btnH"/>
                                <input id="Button2" runat="server" onclick="openNewForm('btnFillPub', 'VendorMaster', 'HNewForm', 'HWhichFill', 'HCondition');" accesskey="B" style="width: 1px; height: 1px;" type="button"
                                value="button" class="btnH"/>
                                <input id="btnVen" runat="server" onclick="openNewForm2('btnFillPub', 'VendorMaster', 'HNewForm', 'HWhichFill', 'HCondition', 'txtcmbvendor');" accesskey="V" style="width: 1px; height: 1px;" type="button"
                                value="button" class="btnH" />
                                <input id="btnDep" runat="server" onclick="openNewForm('btnFillPub', 'DepartmentMaster', 'HNewForm', 'HWhichFill', 'HCondition');" accesskey="T" style="width: 1px; height: 1px;" type="button"
                                value="button" class="btnH" />
                                <input id="btnCurrency" runat="server" onclick="openNewForm('btnFillPub', 'ExchangeMaster', 'HNewForm', 'HWhichFill', 'HCondition');" accesskey="C" style="width: 1px; height: 1px;" type="button"
                                value="button" class="btnH" />
                                <input id="btnMedia" runat="server" onclick="openNewForm('btnFillPub', 'frm_mediatype', 'HNewForm', 'HWhichFill', 'HCondition');" accesskey="M" style="width: 1px; height: 1px;" type="button"
                                value="button" class="btnH" />
                                <input id="btnLanguage" runat="server" onclick="openNewForm('btnFillPub', 'TranslationLanguages', 'HNewForm', 'HWhichFill', 'HCondition');" accesskey="L" style="width: 1px; height: 1px;" type="button"
                                value="button" class="btnH" />
                                <input id="btnCategory" runat="server" onclick="openNewForm('btnFillPub', 'CategoryLoadingStatus', 'HNewForm', 'HWhichFill', 'HCondition');" accesskey="g" style="width: 1px; height: 1px;" type="button"
                                value="button" class="btnH" />  
                                   </div>
<%--                       <asp:customvalidator id="CustomValidator2" runat="server" ClientValidationFunction="comboValidation"
							ControlToValidate="cmbdept" ErrorMessage="<%$ Resources:ValidationResources, SelDep %>" Display="None" SetFocusOnError="True"></asp:customvalidator>--%>
                        <asp:RequiredFieldValidator ID="RVgiftindentdate" runat="server" ControlToValidate="txtgiftindentdate"
                            Display="None" ErrorMessage="<%$ Resources:ValidationResources, EtrGftIndtD %>" SetFocusOnError="True"></asp:RequiredFieldValidator>
                       
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCmbVendor"
                            Display="None" ErrorMessage="Enter Gifted By." SetFocusOnError="True"></asp:RequiredFieldValidator>&nbsp;
                                <%--<asp:customvalidator id="CustomValidator4" runat="server" Font-Size="11px" ClientValidationFunction="comboValidation"
							ControlToValidate="cmbgiftpersontype" ErrorMessage="<%$ Resources:ValidationResources, IvPType %>" Display="None" SetFocusOnError="True"></asp:customvalidator>--%>
                                &nbsp;<asp:requiredfieldvalidator id="TitleRequiredFieldValidator3" runat="server" Font-Size="11px" ControlToValidate="txtgifttitle"
							ErrorMessage="<%$ Resources:ValidationResources, IvTitle %>" Display="None" SetFocusOnError="True"></asp:requiredfieldvalidator>
                        <asp:requiredfieldvalidator id="RequiredFieldValidator10" runat="server" CssClass="mm" Font-Size="11px" ControlToValidate="txtgiftfname1"
							ErrorMessage="<%$ Resources:ValidationResources, IvFName %>" Display="None" SetFocusOnError="True"></asp:requiredfieldvalidator>&nbsp;&nbsp;
<%--                                <asp:CustomValidator
                                ID="cvdtLanguage" runat="server" ClientValidationFunction="comboValidation" ControlToValidate="cmbLanguage"
                                Display="None" ErrorMessage="<%$ Resources:ValidationResources, IvLang %>" Font-Size="11px" Height="16px"
                                SetFocusOnError="True" Width="72px"></asp:CustomValidator>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
<%--						<asp:customvalidator id="CValidator5" runat="server" Width="72px" Height="16px" Font-Size="11px" ClientValidationFunction="comboValidation"
							ControlToValidate="cmbgiftcategory" ErrorMessage="<%$ Resources:ValidationResources, IvCat %>" Display="None" SetFocusOnError="True"></asp:customvalidator>--%>
<%--                                <asp:customvalidator id="CustomValidator3" runat="server" Font-Size="11px" ClientValidationFunction="comboValidation"
							ControlToValidate="cmbgiftmediatype" ErrorMessage="<%$ Resources:ValidationResources, IvMedia %>" Display="None" SetFocusOnError="True"></asp:customvalidator>--%>
<%--				<asp:customvalidator id="CustomValidator6" runat="server" CssClass="mm" Width="48px" Height="16px" Font-Size="11px"
							ClientValidationFunction="comboValidation" ControlToValidate="cmbgiftcurrency" ErrorMessage="<%$ Resources:ValidationResources, IvCurr %>" Display="None" SetFocusOnError="True"></asp:customvalidator>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
						<asp:requiredfieldvalidator id="RequiredFieldValidator9" runat="server" Width="88px" Height="16px" Font-Size="11px"
							ControlToValidate="txtgiftnoofcopies" ErrorMessage="<%$ Resources:ValidationResources, IvCopyNo %>" Display="None" SetFocusOnError="True"></asp:requiredfieldvalidator>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<%--                                <asp:CustomValidator
                                ID="CustomValidator7" runat="server" ClientValidationFunction="ZeroValidation"
                                ControlToValidate="txtgiftnoofcopies" Display="None" 
                                SetFocusOnError="True" Width="115px" ErrorMessage="<%$ Resources:ValidationResources, NoCopMGrt %>"></asp:CustomValidator>--%>
                       
						<asp:requiredfieldvalidator id="RequiredFieldValidator4" runat="server" Font-Size="11px" ControlToValidate="txtgiftprice"
							ErrorMessage="<%$ Resources:ValidationResources, EPcPerCpy %>" Display="None" SetFocusOnError="True"></asp:requiredfieldvalidator>
                        <asp:RequiredFieldValidator ID="giftamount" runat="server" ControlToValidate="txtgiftamount"
                            Display="None" ErrorMessage="<%$ Resources:ValidationResources, EtrTotlGftAmts %>" Font-Names="<%$ Resources:ValidationResources, TextBox1 %>"
                            Font-Size="11px" Height="13px" SetFocusOnError="True" Width="31px"></asp:RequiredFieldValidator>
                        <input id="cmdShow" runat="server" causesvalidation="false" style="visibility: hidden;
                            width: 57px; height: 24px" type="button" value="button" class="btn" />
<%--                        <asp:CustomValidator ID="CustomValidator8" runat="server" ClientValidationFunction="ZeroValidation"
                            ControlToValidate="txtgiftprice" Display="None" ErrorMessage="<%$ Resources:ValidationResources, IvPPCopy %>"
                            SetFocusOnError="True"></asp:CustomValidator>--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtcmbpublisher"
                            Display="None" ErrorMessage="<%$ Resources:ValidationResources, SlctPub %>" SetFocusOnError="True"></asp:RequiredFieldValidator>
				                        
                  
                                  <%--  <cr:CrystalReportViewer id="CrystalReportViewer1" runat="server" AutoDataBind="true">
                                    </cr:CrystalReportViewer>--%>
						<asp:validationsummary id="ValidationSummary1" runat="server" Width="249px" Height="7px" Font-Size="11px"
							ShowMessageBox="True" ShowSummary="False" DisplayMode="List"></asp:validationsummary><asp:requiredfieldvalidator id="Validator2" runat="server" Font-Size="11px" ControlToValidate="cmbgiftpersontype"
							ErrorMessage="<%$ Resources:ValidationResources, IvPType %>" Display="None"></asp:requiredfieldvalidator></TD>
			
					                   
                                  </ContentTemplate>
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

        $("[id$=txtgiftindentdate]").datepicker({

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


