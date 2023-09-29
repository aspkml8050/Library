<%@ Page Language="C#" MasterPageFile="~/LibraryMain.Master" AutoEventWireup="true" CodeBehind="CataloguedBookEntry.aspx.cs" Inherits="Library.CataloguedBookEntry" %>

<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>

<asp:Content ID="hd" runat="server" ContentPlaceHolderID="head">
        <style type="text/css">

        .TAccno {
            width: 250px !important;
            font-size: 13px;
            overflow: auto;
            margin: 0px;
            padding: 1px;
            max-height: 250px;
            border: 1px solid green;
            z-index:1000;
        }

       
        .head {
          padding:8px;
          background-color:#dcd8f5 !important;
         

        }


        .stylebox {
            background-color: White;
            display: block;
            color: Black;
            azimuth: inherit;
            /*width: 100px;*/
            text-decoration: none;
            background-repeat: repeat-x;
            font-style: normal;
            height:32px;
        }

        .vScrollBarRight {
            height: 200px;
            position: absolute;
            z-index: 99999;
            overflow-y: auto;
            border: solid 1px #333;
            font-size: small;
        }

        .stylebox1 {
            background-color: White;
            display: block;
            color: Black;
            azimuth: inherit;
        }

        .stylebox:Hover {
            background: Purple;
            color: Purple;
            cursor: pointer;
        }

        .stylebox1:Hover {
            background: #0099CC;
        }
    </style>
  <script type="text/javascript" >

      function callUValkeywords() {
          var str;
          str = window.open("Journal_Addkeyword.aspx?id=" + document.getElementById("hdCtrlNo").value + "&txtsTitle=" + document.getElementById("txtacc").value, "User Validation", "dialogHeight:380px;dialogWidth:660px,dialogHide:true;help:no;status:no;");
      }

  </script>

<script  type="text/javascript">

    function PrepareMARC_21() {
        var ctrl_no = document.getElementById('hdCtrlNo').value;
        var IsMarc21 = document.getElementById('hdIsMarc21').value;

        if (ctrl_no != '' && IsMarc21 == 'Y') {
            CallMarcWebMethod('ctrl_no:' + ctrl_no);
        }
        else { return false; }
    }
    //+++++++++++++++++++Binds function "starttime" on page load+++++++++++++++++++++
</script>

<script  type="text/javascript">

    function onCtlgAjaxExLoc(source, eventArgs) {


        var selInd = $find("CtlgAjaxExLoc")._selectIndex;
        if (selInd != -1) {

            //getting the value of the selected
            var strWithComma = $find("CtlgAjaxExLoc").get_completionList().childNodes[selInd]._value;

            var splitStr = strWithComma.split(",")
            $find("CtlgAjaxExLoc").get_element().value = splitStr[0];
            document.getElementById("hidLocId").value = splitStr[1];

            //$find("ctl00_MasterContent_studentIdSuggestion").get_element().value = $find("ctl00_MasterContent_studentIdSuggestion").get_completionList().childNodes[selInd]._value;
            //Calling the button click event after select change to perform an action  
            //document.getElementById("ctl00_MasterContent_Button1").click();

        }
        else {
            $find("hidLocId").get_element().value = "-1";
        }
    }

    function callEditor(ctrlId) {
        document.Form1.hIdentifyTxt.value = ctrlId;
        document.Form1.btnEditor1.click();

    }
    function chk() {
        if (document.Form1.Hidden1.value == "177") {
            var strReturn;
            strReturn = window.showModalDialog("Editor.aspx" + '?edval=' + document.getElementById('txtSubtitle').value, "Editor", "status:no;dialogWidth:350px;dialogHeight:150px;dialogHide:true;help:no;scroll:no;");
            if (strReturn != null)
                //document.getElementById(document.Form1.hIdentifyTxt.value).value=strReturn; //
                document.getElementById("txtSubtitle").value = strReturn;
        }
    }
</script>

<script  type="text/javascript">
    function ExecuteCmdClearSession(eventType) {
        if (eventType == 'close') {
            document.getElementById('cmdClearSession').click();
        }
    }

    function txtForeignPrice_OnPropertyChange() {
        var Price;
        if (document.Form1.txtExchangeRate.value == "")
            return false;
        else if (document.Form1.txtForeignPrice.value == "")
            return false;
        else {
            Price = ((document.Form1.txtExchangeRate.value) * (document.Form1.txtForeignPrice.value));
            if (Price.toFixed)
                document.Form1.txtbookprice.value = Price.toFixed(2);
            else
                document.Form1.txtbookprice.value = Price;
        }
    }

    function txtExchangeRate_OnPropertyChange() {
        var Price;
        if (document.Form1.txtExchangeRate.value == "")
            return false;
        else if (document.Form1.txtForeignPrice.value == "")
            return false;
        else {
            Price = ((document.Form1.txtExchangeRate.value) * (document.Form1.txtForeignPrice.value));
            if (Price.toFixed)
                document.Form1.txtbookprice.value = Price.toFixed(2);
            else
                document.Form1.txtbookprice.value = Price;
        }
    }


    function CallPub() {
        try {
            var pubNm = document.getElementById('txtCmbPublisher');
            var pubId = document.getElementById('hdPublisherId');
            var swid = screen.width / 1.2;
            var sheit = screen.height / 1.2;
            if (navigator.appName == "Microsoft Internet Explorer") {
                var retVal = window.showModalDialog("publishermaster.aspx?fldName=" + pubNm.id + "&&fldId=" + pubId.id, "Enter New Publisher", "dialogWidth=" + swid + "px;dialogHeight=" + sheit + "px");
                var retVal2 = retVal.split(':');
                pubId.value = retVal2[1];
                pubNm.value = retVal2[0];
            }
            else {
                window.open("publishermaster.aspx?fldName=" + pubNm.id + "&&fldId=" + pubId.id, "Enter New Publisher", "left=100,top=70,width=" + swid + ",height=" + sheit + ",modal=yes");
            }
        } catch (ex) {

            alert(ex.message);
        }
    }
    function AccSel(sen, arg) {
        document.getElementById('cmdgo1').click();

    }
    //                                                        hdPublisherId PublSel
    function PublSel(sender, arg) {
        document.getElementById("hdPublisherId").value = arg.get_value();
    }
    //                                                        HdVendorid vendSel
    function vendSel(sender, arg) {
        document.getElementById("HdVendorid").value = arg.get_value();
    }
    function Titleselect(sender, arg) {
        document.getElementById("hdCtrlNo").value = arg.get_value();
        document.getElementById('cmdgo1').click();
    }
    function txtaccno_OnKeyDown() {

        document.getElementById('cmdgo1').click();
        //window.document.Form1.cmdgo1;

    }
    function onTextBoxUpdatetop(evt) {
        var textBoxID = evt.source.textBoxID;
        if (evt.selMenuItem != null) {
            document.getElementById(textBoxID).value = evt.selMenuItem.label; // + " (modified by onTextboxUpdate)";                     
            document.getElementById('cmdgo1').click();
            //  document.all('MainControl1_cmdgo1').click();
        }
        evt.preventDefault();

    }
    //For E-resources
    function confirm1(marc) {
        var answer = confirm("Want to Add E-Resources." + marc);
        if (answer) {
            window.location = "EBookMaster.aspx?title=E-Resources"
        }
        return false;
    }
    function funC() {
        alert("s");
        if (navigator.appName == 'Microsoft Internet Explorer') {
            strReturn = window.showModalDialog("DDCMainMenuShow.aspx", "Editor", "status:no;dialogWidth:900px;dialogHeight:400px;dialogHide:true;help:no;scroll:no;");
        } else {
            strReturn = window.open("DDCMainMenuShow.aspx", "Editor", "status:no;dialogWidth:900px;dialogHeight:400px;dialogHide:true;help:no;scroll:no;");
        }

    }

    function pop() {
        alert(navigator.appName);
        if (navigator.appName == 'Microsoft Internet Explorer') {
            alert(navigator.appName)
            strReturn = window.showModalDialog("AddCopy.aspx" + '?volno=' + document.getElementById('txtvolumeNoMAK').value + "&price=" + document.getElementById('txtbookprice').value + "&cat=" + document.getElementById('DDList1').value + "&CtrlNo=" + document.getElementById('hdCtrlNo').value + "&AccNo=" + document.getElementById('accnotxt').value, "Editor", "status:no;dialogWidth:600px;dialogHeight:600px;dialogHide:true;help:no;scroll:no;");
        } else {
            //                alert('Hi');
            alert(navigator.appName + ':2');
            window.open("AddCopy.aspx" + '?volno=' + document.getElementById('txtvolumeNoMAK').value + "&price=" + document.getElementById('txtbookprice').value + "&cat=" + document.getElementById('DDList1').value + "&CtrlNo=" + document.getElementById('hdCtrlNo').value + "&AccNo=" + document.getElementById('accnotxt').value, "Editor", "status=no,width=600,height=600,help=no,scroll=no;");
            //window.open("AddCopy.aspx" + '?volno=' + document.getElementById('txtVolume').value + "&price=" + document.getElementById('txtPrice').value + "&cat=" + ddlCat.options[ddlCat.selectedIndex].value + "&CtrlNo=" + document.getElementById('hdnCtrl_no').value + "&AccNo=" + document.getElementById('txtAccNo').value, "Editor", "width=600,height=500");
        }

        document.getElementById('btnShowCount').click();
    }

</script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:HiddenField ID="hdMast" runat="server" />
         <asp:UpdateProgress ID="UpPorg1" runat="server">
    <ProgressTemplate>
    <NN:Mak ID="FF1" runat="server" />

    </ProgressTemplate>
    </asp:UpdateProgress>
     <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
<div class="container tableborderst" >                    
              <div style="width:100%;display:none" class="title">
                  <div style="width:89%;float:left">&nbsp;
                       <asp:Label ID="lblTitleCatalogue"  style="display:none"  runat="server" Text="<%$ Resources:ValidationResources, Title_Catdet %>"></asp:Label>
                      </div>
                    <div style="float:right;vertical-align:top">
                      <a id="lnkHelp" href="#"  style="display:none" onclick="ShowHelp('Help/Catalogue-technical processing-direct cataloging data entry.htm')">
                                                <img src="help.jpg" height="15" /></a>
                        </div></div>
    <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtCmbVendor" ErrorMessage="Vendor Not Selected"></asp:RequiredFieldValidator>--%>
              
                <asp:HiddenField ID="HiddenField1" runat="server" />
       
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="upload" />
                            <asp:AsyncPostBackTrigger ControlID="cmdsave" />
<%--                            <asp:PostBackTrigger ControlID="cmdsave1" />
                            <asp:PostBackTrigger ControlID="cmdsave2" />
                            <asp:PostBackTrigger ControlID="cmdsave3" />
                            <asp:PostBackTrigger ControlID="cmdsave4" />
                            <asp:PostBackTrigger ControlID="cmdsave5" />
                            <asp:PostBackTrigger ControlID="cmdsave7" />
                            <asp:PostBackTrigger ControlID="cmdsaveT" />
                            <asp:AsyncPostBackTrigger ControlID="cmdAddCopy" />
                            <asp:PostBackTrigger ControlID="cmdAddItem" />
                            <asp:PostBackTrigger ControlID="cmdReset" />
                            <asp:PostBackTrigger ControlID="cmdReset1" />
                            <asp:PostBackTrigger ControlID="cmdReset2" />
                            <asp:PostBackTrigger ControlID="cmdReset3" />
                            <asp:PostBackTrigger ControlID="cmdReset4" />
                            <asp:PostBackTrigger ControlID="cmdReset5" />
                            <asp:PostBackTrigger ControlID="cmdReset7" />
                            <asp:PostBackTrigger ControlID="cmdResetT" />
                            <asp:PostBackTrigger ControlID="grdcopy" />
                            <asp:PostBackTrigger ControlID="btnShowCount" />
                            <asp:PostBackTrigger ControlID="LinkButton1" />
                            <asp:PostBackTrigger ControlID="LinkButton2" />
                            <asp:PostBackTrigger ControlID="LinkButton3" />
                            <asp:PostBackTrigger ControlID="LinkButton4" />
                            <asp:PostBackTrigger ControlID="LinkButton5" />
                            <asp:PostBackTrigger ControlID="LinkButton6" />
                            <asp:PostBackTrigger ControlID="LinkButton7" />
                            <asp:PostBackTrigger ControlID="LinkButton8" />--%>
                        </Triggers>
                        <ContentTemplate>
                            <asp:HiddenField ID="hdBookNumAccn" runat="server" />
<%--                            This field is not emptied - stores whether booknumber to be stored in accessionmaster and so on--%>
                            <div style="display: none;">
                                <asp:Button ID="btnShowCount" runat="server" />
                            </div>
                             <div class="no-more-tables" style="width:100%;z-index:1 !important;">
                                 <div>
                                      <asp:Label ID="msglabel" runat="server" Width="100%" CssClass="err" Height="8px"></asp:Label>
                                     </div>
                            <div id="RFidProcess" runat="server" visible="false" style="width:50%;">
                <table style="width:100%;">
                    <tr>
                        <td>
                                            <asp:Label ID="lblantena" runat="server" Text="Select Antenna"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlantenna" runat="server">
                                                <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="2" Value="2" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                 <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td><input id="btnscan" runat="server" class="btnstyle" name="btnscan"
                                                type="submit" value="Scan" />
                                        </td>
                    </tr>
                </table>
            </div>
                                            <table id="Table10" class="table-condensed GenTable1">
                                                

                                                <tr>
                                                    <td colspan="6">
                                                        <table class="table-condensed GenTable1">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="LabelA" runat="server" Text="<%$ Resources:ValidationResources, LblTtlAccsnd %>"
                                                                        ></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblAccession" runat="server" BackColor="Transparent" BorderColor="Black"
                                                                        BorderWidth="1px" Font-Bold="True" ForeColor="Red" Height="20px" ></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbltot1" runat="server" Text="<%$ Resources:ValidationResources, LblTtlCat %>"
                                                                        ></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbltotentry" runat="server" BackColor="Transparent" BorderColor="Black"
                                                                        BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="Red" Height="20px"
                                                                       ></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbltot0" runat="server" Text="<%$ Resources:ValidationResources, Uncatalogued_book %>"
                                                                      ></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbltotentryun" runat="server" BackColor="Transparent" BorderColor="Black"
                                                                        BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="Red" Height="20px"
                                                                        ></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td ><asp:Label ID="Label2" runat="server" CssClass="span"  Text="<%$ Resources:ValidationResources, ItemCats %>"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="cmbtype" runat="server" Height="30" onblur="this.className='blur'" onfocus="this.className='focus'"
                                                           >
                                                            <asp:ListItem Value="Books" Text="<%$ Resources:ValidationResources,LIBook %>"></asp:ListItem>
                                                            <asp:ListItem Value="Articles" Text="<%$ Resources:ValidationResources,LArt %>"></asp:ListItem>
                                                            <asp:ListItem Value="Project Reports" Text="<%$ Resources:ValidationResources,LPP %>"></asp:ListItem>
                                                            <asp:ListItem Value="Thesis" Text="<%$ Resources:ValidationResources,LThesis %>"></asp:ListItem>
                                                            <asp:ListItem Value="Journals" Text="<%$ Resources:ValidationResources, Journals %>"></asp:ListItem>
                                                            <asp:ListItem Value="Non Print" Text="<%$ Resources:ValidationResources, NonPrint %>"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label6jk" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources, lblstatus %>"></asp:Label>
                                                    </td>
                                                    <td >
                                                        <asp:DropDownList ID="cmbStatus" runat="server" onblur="this.className='blur'" Height="30"
                                                            onfocus="this.className='focus'" 
                                                            AutoPostBack="True">
                                                            <asp:ListItem Value="D" Text="<%$ Resources:ValidationResources,bDispalys %>"></asp:ListItem>
                                                            <asp:ListItem Value="Rs" Text="<%$ Resources:ValidationResources,LIResvSec %>"></asp:ListItem>
                                                            <asp:ListItem Value="Rf" Text="<%$ Resources:ValidationResources,LIRefBook %>"></asp:ListItem>
                                                            <asp:ListItem Value="Sh" Text="<%$ Resources:ValidationResources,LISelf %>"></asp:ListItem>
                                                            <asp:ListItem Value="TB" Text="<%$ Resources:ValidationResources,LITxtBkSec %>"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td ></td>
                                                    <td ></td>
                                                </tr>
                                                <tr>
                                                    <td ><asp:Label ID="Label4" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LBCatlg %>"><u>D</u>ate :</asp:Label>
                                                    </td>
                                                    <td >

                                                        <%--pushpendra singh--%>
 <asp:TextBox ID="txtdate" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
 <%--<ajax:CalendarExtender ID="CalendarExtender1" TargetControlID="txtdate" Format="dd/MMM/yyyy" runat="server"></ajax:CalendarExtender>--%> 
 <%--pushpendra singh--%>

                                                        <%--<input id="txtdate" onblur="this.className='blur';checkdate1(this,document.Form1.hdCulture.value,document.Form1.hrDate.value)"
                                                            onfocus="this.className='focus'" type="text" name="txtdate" runat="server" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; height: 20px; width: 112px;"><input id="btnDate" type="button" onclick="pickDate('txtdate');"
                                                                runat="server" accesskey="D" style="background-position: center center; background-image: url(cal.gif); width: 25px; background-color: black; height: 21px;" />--%>


                                                        <asp:Label ID="Label131" runat="server" Width="1px" CssClass="star" Height="3px">*</asp:Label>
                                                    </td>
                                                    <td ><asp:Label ID="Labelb" runat="server" CssClass="span"  Text="<%$ Resources:ValidationResources,LBaDa %>"></asp:Label>
                                                    </td>
                                                    <td >

                                                        
                                                        
                                                        <%--pushpendra singh--%>
 <asp:TextBox ID="txtrelease" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
 <%--<ajax:CalendarExtender ID="CalendarExtender2" TargetControlID="txtrelease" Format="dd/MMM/yyyy" runat="server"></ajax:CalendarExtender>--%> 
 <%--pushpendra singh--%>
                                                        
<%--                                                        <input id="txtrelease" runat="server" name="txtdate" onblur="this.className='blur';checkdate1(this,document.Form1.hdCulture.value,document.Form1.hrDate.value)"
                                                            onfocus="this.className='focus'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; width: 104px; border-bottom: black 1px solid; height: 20px"
                                                            type="text" /><input id="Button1" runat="server" accesskey="D" onclick="pickDate('txtrelease');"
                                                                style="background-position: center center; background-image: url(cal.gif); width: 25px; height: 21px; background-color: black"
                                                                type="button" />--%>

                                                    </td>
                                                    <td ></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td ><asp:Label ID="LabelN" runat="server" CssClass="span"  Text="<%$ Resources:ValidationResources,rptBillNo %>"></asp:Label>
                                                    </td>
                                                    <td >
                                                        <input id="txtDocNo" onblur="this.className='blur'" onfocus="this.className='focus'"
                                                            type="text" name="txtacc" runat="server" >
                                                    </td>
                                                    <td ><asp:Label ID="Label9N" runat="server" CssClass="span"  Text="<%$ Resources:ValidationResources,BillD %>"></asp:Label>
                                                    </td>
                                                    <td >

                                                        <%--pushpendra singh--%>
 <asp:TextBox ID="txtDocDate" runat="server" />
 <%--<ajax:CalendarExtender ID="CalendarExtender3" TargetControlID="txtDocDate" Format="dd/MMM/yyyy" runat="server"></ajax:CalendarExtender>--%> 
 <%--pushpendra singh--%>


                                                       <%-- <input id="txtDocDate" onblur="this.className='blur';checkdate1(this,document.Form1.hdCulture.value,document.Form1.hrDate.value)"
                                                            onfocus="this.className='focus'" type="text" name="txtdate" runat="server" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; height: 20px; width: 104px;"><input id="btnDate1" type="button" onclick="pickDate('txtDocDate');"
                                                                runat="server" accesskey="D" style="background-position: center center; background-image: url(cal.gif); width: 25px; background-color: black; height: 21px;" />--%>



                                                    </td>
                                                    <td ></td>
                                                    <td ></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:HyperLink ID="HyperlinkD" Visible="true" runat="server" Text="<%$ Resources:ValidationResources,LCat %>"
                                                            onclick="openNewForm('btnFillPub','CategoryLoadingStatus','HNewForm','HWhichFill','HCondition');"></asp:HyperLink>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="DDList1" Height="30" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblLoc" runat="server" Text="Location " CssClass="span"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtLocation" runat="server" BorderStyle="Solid" BorderColor="Black" BorderWidth="1px"></asp:TextBox>
                                                        <ajax:AutoCompleteExtender
                                                            runat="server"
                                                            ID="CtlgAjaxExLoc"
                                                            TargetControlID="txtLocation"
                                                              CompletionListCssClass="TAccno"
                                                            ServicePath="Json/json_service.asmx"
                                                            ServiceMethod="GetLocaton2"
                                                            MinimumPrefixLength="0"
                                                            CompletionInterval="0"
                                                            EnableCaching="true"
                                                            OnClientItemSelected="onCtlgAjaxExLoc"
                                                            CompletionListElementID="Div1">
                                                        </ajax:AutoCompleteExtender>
                                                        <div id="Div1" class="vScrollBarRight"></div>
                                                        <asp:HiddenField ID="hidLocId" runat="server" Value="-1" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:CheckBox ID="opttitle" runat="server" Visible="false" Checked="true" CssClass="opt" Text="<%$ Resources:ValidationResources,ChkSrchExtRcd %>"
                                                         />
                                                    </td>
                                                    <td ></td>
                                                    <td ></td>
                                                    <td ></td>
                                                    <td> </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" >
                                                        <hr />
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td ><asp:Label ID="Label5" runat="server"  CssClass="span" Text="<%$ Resources:ValidationResources,AccNo %>"></asp:Label>
                                                    </td>
                                                    <td >

                                                        <asp:TextBox ID="txtacc" onfocus="this.className='focus'"
                                                            runat="server" BorderWidth="1px" Columns="30"
                                                           ></asp:TextBox>
                                    <ajax:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtacc"
                                        MinimumPrefixLength="0"
                                        CompletionInterval="50"
                                        CompletionSetCount="50"
                                        FirstRowSelected="true"
                                        CompletionListCssClass="TAccno"
                                        ServicePath="MssplSugg.asmx"
                                         OnClientItemSelected="AccSel"
                                        EnableCaching="true" SkinID="None"
                                        ServiceMethod="GetAccnCtrl" UseContextKey="false">
                                    </ajax:AutoCompleteExtender>


                                                        <%--<Custom:AutoSuggestMenu ID="asmacc" runat="server" KeyPressDelay="0" MaxSuggestChars="100" OnClientTextBoxUpdate="onTextBoxUpdatetop"
                                                            OnGetSuggestions="TopSearchService.GetSuggestionsAcc" ResourcesDir="~/asm_includes" 
                                                            TargetControlID="txtacc" UsePageMethods="false"></Custom:AutoSuggestMenu>--%>
                                                        <asp:Label ID="Label3" runat="server" CssClass="star"
                                                            Height="3px" Width="1px">*</asp:Label>
                                                        <input id="cmdgo1" style=" display: none"
                                                                type="submit" value="<%$ Resources:ValidationResources,NGo %>" runat="server"
                                                                name="cmdgo1" causesvalidation="false">
                                                    </td>
                                                    <td ><asp:Label ID="Label1" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,CpyNo %>"></asp:Label>
                                                    </td>
                                                    <td >
                                                        <input id="txtCopyNo" onblur="this.className='blur'" onkeypress="IntegerNumber(this);"
                                                            onfocus="this.className='focus'" type="text" name="txtCopyNo" runat="server"
                                                            style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                                                            maxlength="4">
                                                    </td>
                                                    <td ></td>
                                                    <td ></td>
                                                </tr>
                                                <tr>
                                                    <td ><asp:Label ID="Label3ja" runat="server" CssClass="span"  Text="<%$ Resources:ValidationResources,ClasNo %>"></asp:Label>
                                                    </td>
                                                    <td >
                                                        <input id="txtclassno" onblur="this.className='blur'"
                                                            style="border: 1px solid black; "
                                                            onfocus="this.className='focus'" type="text" runat="server">
                                                        <asp:Button ID="Button2" runat="server" CssClass="btnstyle" OnClientClick="funC();"
                                                            TabIndex="2" Text="Open" UseSubmitBehavior="False"  />
                                                    </td>
                                                    <td ><asp:Label ID="Labe92" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LBBookNo %>"></asp:Label>
                                                    </td>
                                                    <td  colspan="1">
                                                        <input id="txtbookno" onblur="this.className='blur'"   style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                                                            onfocus="this.className='focus'" type="text" runat="server"> <asp:Label ID="labBookN" tooltip="Based on feature setting this value can be stored with individual Accession copy " runat="server" Text="Note*" ></asp:Label>
                                                    </td>
                                                    <td ></td>
                                                    <td ></td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        <asp:Label ID="Label1234" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LTitle %>"></asp:Label>
                                                    </td>
                                                    <td colspan="3" >
                                                        <asp:TextBox ID="txtSTitle" onfocus="this.className='focus'"
                                                            runat="server" BorderWidth="1px" Columns="30"
                                                           ></asp:TextBox>
                                                        <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/sugg.png" />
                                                         <asp:Label ID="Label6" runat="server" CssClass="star" Height="3px">*</asp:Label>
                                                          <ajax:AutoCompleteExtender ID="AutoCompleteExtender7" runat="server" TargetControlID="txtSTitle"
                                        MinimumPrefixLength="0"
                                        CompletionInterval="50"
                                        CompletionSetCount="50"
                                        FirstRowSelected="true"
                                        CompletionListCssClass="TAccno"
                                        ServicePath="MssplSugg.asmx"
                                         OnClientItemSelected="Titleselect"
                                        EnableCaching="true" SkinID="None"
                                        ServiceMethod="GetTitleCtrl" UseContextKey="false">
                                    </ajax:AutoCompleteExtender>
                                                        
                                                        <%--hidden field hdCtrlNo is avoided accno is selected--%>

                                                       <%--hdCtrlNo <Custom:AutoSuggestMenu ID="asmtitletop" runat="server" KeyPressDelay="10" MaxSuggestChars="100" OnClientTextBoxUpdate="onTextBoxUpdatetop"
                                                            OnGetSuggestions="TopSearchService.GetSuggestionsBookTitle" ResourcesDir="~/asm_includes"
                                                            TargetControlID="txtSTitle" UpdateTextBoxOnUpDown="false" UsePageMethods="false"></Custom:AutoSuggestMenu>--%>

                                                    </td>
                                                    <td colspan="2">
                                                        
                                                    </td>
                                                    
                                                    <td >
                                                        <asp:Button ID="cmdSerach" runat="server" Text="<%$ Resources:ValidationResources,NGo %>" Style="display: none"
                                                             CausesValidation="False" UseSubmitBehavior="False"
                                                            AccessKey="g" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td ><asp:Label ID="Label61" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LSTitle %>"></asp:Label>
                                                    </td>
                                                    <td colspan="3" >
                                                        <%--<asp:TextBox ID="txtSubtitle"  onfocus="this.className='focus'"  onblur="this.className='blur'" runat="server" Height="20px" Width="238px" CssClass="txt10"></asp:TextBox>--%><input
                                                            class="txt10" id="txtSubtitle" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                                                            onfocus="this.className='focus'" type="text" runat="server">
                                                    </td>
                                                    <td ></td>
                                                    <td ></td>
                                                    <td >
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td ><asp:Label ID="Label63" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LBParlTtl %>"></asp:Label>
                                                    </td>
                                                    <td  colspan="3" >
                                                        <input class="txt10" id="txtParallelTitle" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                            onfocus="this.className='focus'" type="text" runat="server">
                                                    </td>
                                                    <td ></td>
                                                    <td ></td>
                                                    <td ></td>
                                                </tr>
                                                <tr>
                                                    <td ><asp:Label ID="Label1jai" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LBUnifTtl %>"></asp:Label>
                                                    </td>
                                                    <td  colspan="3">
                                                        <input class="text1" id="txtUniformTitle" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                                                            onfocus="this.className='focus'" type="text" runat="server" name="txtUniformTitle">
                                                    </td>
                                                    <td ></td>
                                                    <td ></td>
                                                    <td ></td>
                                                </tr>
                                                <tr>
                                                    <td ><asp:Label ID="Label65" runat="server" CssClass="span"  Text="<%$ Resources:ValidationResources,LVolume %>"></asp:Label>
                                                    </td>
                                                    <td >
                                                        <input type="text" id="txtvolumeNoMAK" runat="server" />
                                                        <%--<input onkeypress="IntegerNumber(this)" id="txtvolumeNoMAK" onblur="this.className='blur'"
                                                        style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;
                                                        width: 133px; border-bottom: black 1px solid; height: 20px" onfocus="this.className='focus'"
                                                        type="text" runat="server">--%>
                                                    </td>
                                                    <td ><asp:Label ID="Label10" runat="server" CssClass="span" 
                                                        Text="<%$ Resources:ValidationResources,LPart %>"></asp:Label>
                                                    </td>
                                                    <td >
                                                        <input onkeypress="IntegerNumber(this)" id="txtPart" onblur="this.className='blur'"
                                                            style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid;"
                                                            onfocus="this.className='focus'"
                                                            type="text" runat="server" name="txtPart">
                                                    </td>
                                                    <td ></td>
                                                    <td ></td>
                                                    <td ></td>
                                                </tr>
                                                <tr>
                                                    <td ><asp:Label ID="Label129" runat="server" CssClass="span" 
                                                        Text="<%$ Resources:ValidationResources,LEdition %>"></asp:Label>
                                                    </td>
                                                    <td >
                                                        <input id="txtedition" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                                                            onfocus="this.className='focus'" type="text" runat="server">
                                                    </td>
                                                    <td ><asp:Label ID="Label130" runat="server" CssClass="span" 
                                                        Text="<%$ Resources:ValidationResources,LEditionY %>"></asp:Label>
                                                    </td>
                                                    <td >
                                                        <input id="txteditionyear" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                            onfocus="this.className='focus'" type="text" onkeypress="IntegerNumber(this);"
                                                            runat="server" maxlength="4">
                                                    </td>
                                                    <td ></td>
                                                    <td ></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td ><asp:Label ID="Label140" runat="server" CssClass="span" 
                                                        Text="<%$ Resources:ValidationResources,LPubY %>"></asp:Label>
                                                    </td>
                                                    <td >
                                                        <input id="txtPubYear" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                                                            onfocus="this.className='focus'" onkeypress="IntegerNumber(this);"
                                                            type="text" runat="server" maxlength="4">
                                                    </td>
                                                    <td ><asp:Label ID="lblCourse1" runat="server" Text="<%$ Resources:ValidationResources,RBCourse %>"></asp:Label>
                                                    </td>
                                                    <td >
                                                        <asp:DropDownList ID="cmbCourse1" runat="server" onfocus="this.className='focus'" Height="30"
                                                            onblur="this.className='blur'">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td ></td>
                                                    <td ></td>
                                                    <td ></td>
                                                </tr>
                                                <tr>
                                                    <td ><asp:HyperLink ID="Hyperlink22" runat="server" Text="<%$ Resources:ValidationResources,LPubli %>"
                                                        onclick="CallPub();"></asp:HyperLink>
                                                        <!-- onclick="openNewForm('btnFillPub','PublisherMaster','HNewForm','HWhichFill','HCondition');" -->
                                                        <%--<asp:label id="Label128" runat="server" CssClass="span" ToolTip="Press Alt+P to add New Publisher" Text="<%$ Resources:ValidationResources, LnbspPublisher %>" Width="70px"></asp:label>--%>
                                                        <%--<asp:TextBox ID="txtSubtitle"  onfocus="this.className='focus'"  onblur="this.className='blur'" runat="server" Height="20px" Width="238px" CssClass="txt10"></asp:TextBox>--%>
                                                    </td>
                                                    <td  colspan="3" >
                                                        <asp:TextBox ID="txtCmbPublisher" runat="server" BorderWidth="1px"
                                                            Columns="30" ></asp:TextBox>
                                    <ajax:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtCmbPublisher"
                                        MinimumPrefixLength="0"
                                        CompletionInterval="50"
                                        CompletionSetCount="50"
                                        FirstRowSelected="true"
                                        CompletionListCssClass="TAccno"
                                        ServicePath="MssplSugg.asmx"
                                         OnClientItemSelected="PublSel"
                                        EnableCaching="true" SkinID="None"
                                        ServiceMethod="GetPubl" UseContextKey="false">
                                    </ajax:AutoCompleteExtender>
                                           
                                                         <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/sugg.png" />
                                                         <asp:Label ID="Label1009" runat="server" CssClass="star" Height="3px">*</asp:Label>

                                                        <%--<Custom:AutoSuggestMenu ID="asmpublisher" runat="server" KeyPressDelay="1" MaxSuggestChars="100"
                                                            OnGetSuggestions="TopSearchService.GetSuggestionsPublisher" ResourcesDir="~/asm_includes"
                                                            TargetControlID="txtCmbPublisher" UpdateTextBoxOnUpDown="false" UsePageMethods="false"></Custom:AutoSuggestMenu>--%>
                                                    </td>
                                                    <td colspan="3">
                                                       
                                                    </td>
                                                   
                                                </tr>
                                                <tr>
                                                    <td ><asp:HyperLink ID="hyper68" runat="server" Text="<%$ Resources:ValidationResources,LVen %>"
                                                        onclick="openNewForm('btnFillPub','VendorMaster','HNewForm','HWhichFill','HCondition');"></asp:HyperLink>
                                                    </td>
                                                    <td  colspan="3">
                                                        <asp:TextBox ID="txtCmbVendor" runat="server" BorderWidth="1px"
                                                            Columns="30"></asp:TextBox>
                                    <ajax:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" TargetControlID="txtCmbVendor"
                                        MinimumPrefixLength="0"
                                        CompletionInterval="50"
                                        CompletionSetCount="50"
                                        FirstRowSelected="true"
                                        CompletionListCssClass="TAccno"
                                        ServicePath="MssplSugg.asmx"
                                         OnClientItemSelected="vendSel"
                                        EnableCaching="true" SkinID="None"
                                        ServiceMethod="GetVendor" UseContextKey="false">
                                    </ajax:AutoCompleteExtender>
<%--                                                        <Custom:AutoSuggestMenu ID="asmvendor" runat="server" KeyPressDelay="1" MaxSuggestChars="100"
                                                            OnGetSuggestions="TopSearchService.GetSuggestionsVendor" ResourcesDir="~/asm_includes"
                                                            TargetControlID="txtCmbVendor" UpdateTextBoxOnUpDown="false" UsePageMethods="false"></Custom:AutoSuggestMenu>--%>
                                                  <asp:Image ID="Image6" runat="server" ImageUrl="~/Images/sugg.png"
                                                            Width="16px" />
                                                        </td>
                                                    <td colspan="3">
                                                       
                                                    </td>
                                                   
                                                </tr>
                                                <tr>
                                                    <td ><asp:HyperLink ID="Label13" runat="server" Text="<%$ Resources:ValidationResources,LCurrency %>"
                                                        onclick="openNewForm('btnFillPub','ExchangeMaster','HNewForm','HWhichFill','HCondition');"></asp:HyperLink>
                                                    </td>
                                                    <td >
                                                        <asp:DropDownList ID="cmbcurr" runat="server" Height="30"
                                                            onchange="GetServer(this.id,'txtExchangeRate')" onblur="this.className='blur'" onfocus="this.className='focus'"
                                                          >
                                                        </asp:DropDownList>
                                                    <asp:Label ID="lbl74" runat="server" CssClass="span"  Text="<%$ Resources:ValidationResources,GrExR %>"></asp:Label>
                                                    </td>
                                                    <td >
                                                    <input id="txtExchangeRate" runat="server" class="txt10" name="txtExchangeRate" onblur="this.className='blur'"
                                                        onfocus="this.className='focus'" onkeypress="decimalNumber(this);" onpropertychange="txtExchangeRate_OnPropertyChange();"
                                                        style="resources: ValidationResources, TextBox2 %>; " type="text" />&nbsp;
                                                    <asp:Label ID="lbl35" runat="server"  Text="<%$ Resources:ValidationResources,LPrc %>"></asp:Label>
                                                    </td>
                                                    <td >
                                                        <input id="txtForeignPrice" runat="server" class="txt10" maxlength="7" name="txtForeignPrice"
                                                            onblur="this.className='blur'" onfocus="this.className='focus'" onkeypress="decimalNumber(this);"
                                                            onpropertychange="txtForeignPrice_OnPropertyChange();" style="resources: ValidationResources, TextBox2 %>; "
                                                            type="text" />
                                                    </td>
                                                    <td ></td>
                                                    <td ></td>
                                                    <td ></td>
                                                </tr>
                                                <tr>
                                                    <td ><asp:Label ID="Label82" runat="server" CssClass="span"  Text="<%$ Resources:ValidationResources,LPrc %>"></asp:Label>
                                                    </td>
                                                    <td >
                                                        <input id="txtbookprice" onkeypress="decimalNumber(this);" onblur="this.className='blur'"
                                                            style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                            value="0.00" onfocus="this.className='focus'"
                                                            type="text" runat="server"><asp:Label ID="Label8" runat="server" CssClass="star"
                                                                Height="3px" Width="1px">*</asp:Label>
                                                    </td>
                                                    <td ><asp:Label ID="Label190" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LSPrice %>"></asp:Label>
                                                    </td>
                                                    <td >
                                                        <input id="txtSpecialPrice" value="0.00" onkeypress="decimalNumber(this);" onblur="this.className='blur'"
                                                            style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                                                            onfocus="this.className='focus'"
                                                            type="text" runat="server" name="txtSpecialPrice">
                                                    </td>
                                                    <td ></td>
                                                    <td ></td>
                                                    <td ></td>
                                                </tr>
                                               
                                                <tr>
                                                    
                                                    <td style="text-align:center" colspan="7"  >
                                                       
                                                                    <asp:Button ID="cmdAddCopy" runat="server" Text="<%$ Resources:ValidationResources,LAddCpy %>"
                                                                        UseSubmitBehavior="False" CssClass="btnstyle" AccessKey="A" Enabled="False" />
                                                                    
                                                                    <input id="cmdKeywords" OnClientClick="callUValkeywords();" runat="server" accesskey="Y"  class="btnstyle" name="cmdKeywords"
                                                                       type="submit" value="<%$ Resources:ValidationResources, AddKeyWd %>"/>
                                                                   
                                                               
                                                                    <asp:Button ID="cmdAddCopy0" CssClass="btnstyle" runat="server" AccessKey="A" Enabled="False"
                                                                        OnClientClick="pop()" Text="Add Copy New" UseSubmitBehavior="False"  />
                                                                    <input id="cmdAddItem" type="submit" value="<%$ Resources:ValidationResources,BAddvolE %>"
                                                                        runat="server" name="cmdAddItem"  class="btnstyle">
                                                                    <input id="accnotxt" type="text" runat="server" visible="false" disabled="disabled" />
                                                                
                                                    </td>
                                                   
                                                </tr>
                                               
                                            </table>
                                       
                                 </div>
                                 <div style="width:100%; overflow: auto;">
                                            <table  class="GenTable1" style="width:100%">
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="LinkButton1" runat="server" class="stylebox" Text="Genral Info."  BorderStyle="None" ToolTip="<%$ Resources:ValidationResources,Lblgeneralinformation%>" BackColor="#0099CC" ForeColor="White"></asp:Button></td>
                                                    <td >
                                                        <asp:Button ID="LinkButton2" runat="server" class="stylebox" Text="Statement of Resp."  BorderStyle="None" ToolTip="Statement of Responsibility" BackColor="#0099CC" ForeColor="White"></asp:Button></td>
                                                    <td >
                                                        <asp:Button ID="LinkButton3" runat="server" class="stylebox" Text="Physical Desc."  BorderStyle="None" ToolTip="Physical Description" BackColor="#0099CC" ForeColor="White"></asp:Button></td>
                                                    <td>
                                                        <asp:Button ID="LinkButton4" runat="server" class="stylebox" Text="Series Desc."  BorderStyle="None" ToolTip="Series Description" BackColor="#0099CC" ForeColor="White"></asp:Button></td>
                                                    <td>
                                                        <asp:Button ID="LinkButton5" runat="server" class="stylebox" Text="Notes Section"  BorderStyle="None" ToolTip="Notes Section" BackColor="#0099CC" ForeColor="White"></asp:Button></td>
                                                    <td>
                                                        <asp:Button ID="LinkButton6" runat="server" class="stylebox" Text="Standard No.(s)&Subject(s)"  BorderStyle="None" ToolTip="Standard No.(s)&amp; Subject(s)" BackColor="#0099CC" ForeColor="White"></asp:Button></td>
                                                    <td>
                                                        <asp:Button ID="LinkButton7" runat="server" class="stylebox" Text="Copies Info."  BorderStyle="None" ToolTip="Copies Information" BackColor="#0099CC" ForeColor="White"></asp:Button></td>
                                                    <td>
                                                        <asp:Button ID="LinkButton8" runat="server" class="stylebox" Text="Thesis Related Info."  BorderStyle="None" ToolTip="Thesis Related Information" BackColor="#0099CC" ForeColor="White"></asp:Button></td>

                                                </tr>
                                            </table>
                                            </div>
                                
                                 
                                   <asp:Panel ID="grdPanel" runat="server" ScrollBars="Both">
                                       <div class="no-more-tables">
                                                <asp:Panel ID="panel1" runat="server">
                                                    <table id="Table11" class="table-condensed GenTable1" >
                                                         <tr>
                                                            <td colspan="4"></td>
                                                             </tr>
                                                        <tr>
                                                            <td colspan="4" style="text-align:center;">
                                                                <asp:Label ID="Label60" runat="server" CssClass="head"  Text="<%$ Resources:ValidationResources,LBGenlDescpt %>"></asp:Label>
                                                            </td>
                                                            <td ></td>
                                                        </tr>
                                                        <tr>
                                                            <td  colspan="4">
                                                            </td>
                                                            <td ></td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:HyperLink ID="Hyperlink1" Visible="false" runat="server" Text="<%$ Resources:ValidationResources,LCat %>"
                                                                onclick="openNewForm('btnFillPub','CategoryLoadingStatus','HNewForm','HWhichFill','HCondition');"></asp:HyperLink>
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:DropDownList ID="cmbbookcategory" Height="30" Visible="false" runat="server">
                                                                </asp:DropDownList>
                                                                <%--<select id="cmbbookcategory" visible="false" runat="server" class="txt10"
                                  onblur="this.className='blur'" onfocus="this.className='focus'" style="font-size: 12px;
                                width:398px; height: 22px">--%>
                                                            </select>
                                                            <asp:Label ID="Label1008" runat="server" Text="*" Visible="false" CssClass="star"></asp:Label>
                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:HyperLink ID="HypLang" Text="<%$ Resources:ValidationResources,LLanguage %>"
                                                                runat="server" onclick="openNewForm('btnFillPub','TranslationLanguages','HNewForm','HWhichFill','HCondition');"></asp:HyperLink>
                                                            </td>
                                                            <td colspan="2">
                                                                <asp:DropDownList ID="cmbLanguage" runat="server" Height="30" onblur="this.className='blur'"
                                                                    onfocus="this.className='focus'">
                                                                </asp:DropDownList>
                                                                <asp:Label ID="Label7" runat="server" CssClass="star" Height="3px">*</asp:Label>
                                                            </td>
                                                            <td></td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                            <asp:Label ID="Label6ja" runat="server" CssClass="span" 
                                                                Text="<%$ Resources:ValidationResources, LDeptm %>"></asp:Label>
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:DropDownList ID="cmbdept" runat="server" CssClass="text10" Height="30" Font-Size="X-Small"
                                                                     onblur="this.className='blur'" onfocus="this.className='focus'">
                                                                </asp:DropDownList>
                                                                <asp:Label ID="Label59" runat="server" Width="1px" CssClass="star" Height="3px">*</asp:Label>
                                                            </td>
                                                            <td colspan="1"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            <asp:Label ID="Label141" runat="server" CssClass="span"  Text="<%$ Resources:ValidationResources, LMediaType %>"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="cmbMediaType" runat="server" onblur="this.className='blur'" Height="30"
                                                                    onfocus="this.className='focus'" >
                                                                </asp:DropDownList>
                                                                <asp:Label ID="Label9" runat="server" CssClass="star" Height="3px" Width="1px">*</asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label58" runat="server" CssClass="span" 
                                                                    Text="<%$ Resources:ValidationResources,Lform %>"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="cmbboundind" onblur="this.className='blur'" Height="30" onfocus="this.className='focus'"
                                                                    runat="server" CssClass="text1">
                                                                    <asp:ListItem Value="Soft Bound"></asp:ListItem>
                                                                    <asp:ListItem Value="Hard Bound"></asp:ListItem>
                                                                    <asp:ListItem Value="PBK"></asp:ListItem>
                                                                    <asp:ListItem Text="<%$ Resources:ValidationResources,Otr %>"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td ></td>
                                                        </tr>
                                                        <tr>
                                                            <td rowspan="6" style="vertical-align:top">
                                                            <asp:Label ID="Label1876" runat="server" CssClass="span" 
                                                                Text="<%$ Resources:ValidationResources,LBCverPgImg %>"></asp:Label>
                                                            </td>
                                                            <td colspan="3" >
                                                                <table class="GenTable1" style="width: 100%">
                                                                    <tr>
                                                                        <td >
                                                                            <asp:FileUpload ID="Fileupload" runat="server" onfocus="this.className='focus'" Width="100%" onblur="this.className='blur';InputValidation(this,'Fileupload');" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td >
                                                                            <img id="image1" runat="server"  height="78" width="99" />
                                                                            <input id="upload" runat="server" class="btnstyle" type="submit" value="<%$ Resources:ValidationResources,bDisplay %>"
                                                                                causesvalidation="False" />
                                                                        </td>
                                                                    </tr>
                                                                    
                                                                </table>
                                                            </td>
                                                            <td colspan="1" rowspan="6" ></td>
                                                            <td rowspan="6"></td>
                                                        </tr>
                                                        
                                                        <tr>
                                                            <td></td>
                                                        </tr>
                                                        
                                                        <tr>
                                                            <td colspan="6" >
                                                                <table id="Table8" class="table-condensed GenTable1">
                                                                   
                                                                    <tr>
                                                                        <td >
                                                                            <input id="cmdsave1" class="btnstyle" type="submit" value="<%$ Resources:ValidationResources,bSave %>" runat="server">
                                                                       
                                                                            <input id="cmdReset1" class="btnstyle" type="submit" value="<%$ Resources:ValidationResources,bReset %>" runat="server">
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td  colspan="6"></td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                           </div>
                                        <div class="no-more-tables">
                                                <asp:Panel ID="panel2" runat="server">
                                                    <table id="Table6" class="table-condensed GenTable1">
                                                        <tr>
                                                            <td colspan="4" ></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align:center" colspan="4">
                                                                <asp:Label ID="Label70" runat="server"  CssClass="head" Text="<%$ Resources:ValidationResources,LBStmntResA %>"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        
                                                        <tr>
                                                            <td >
                                                                <asp:Label ID="Label55" runat="server"  CssClass="span"
                                                                    Text="<%$ Resources:ValidationResources,LBetal %>"></asp:Label>
                                                                <asp:DropDownList ID="cboAuthorETAL" onblur="this.className='blur'" Height="30" onfocus="this.className='focus'"
                                                                    runat="server" >
                                                                    <asp:ListItem Value="N"></asp:ListItem>
                                                                    <asp:ListItem Value="Y"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td >
                                                            <asp:Label ID="Label110" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,Auth %>"></asp:Label>
                                                            </td>
                                                            <td ></td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td ></td>
                                                            <td >
                                                                <asp:Label ID="Label45" runat="server" CssClass="opt" Text="<%$ Resources:ValidationResources,LFname %>"></asp:Label>
                                                            </td>
                                                            <td >
                                                                <asp:Label ID="Label46" runat="server"  CssClass="opt" Text="<%$ Resources:ValidationResources,LMname %>"></asp:Label>
                                                            </td>
                                                            <td >
                                                                <asp:Label ID="Label47" runat="server" CssClass="opt" Text="<%$ Resources:ValidationResources,LLname %>"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                                <asp:Label ID="Label111" runat="server" CssClass="span">1.</asp:Label>
                                                            </td>
                                                            <td >
                                                                <asp:TextBox ID="txtau1firstnm" runat="server" ></asp:TextBox>
<%--                                                                <Custom:AutoSuggestBox ID="txtau1firstnm" runat="server" BorderWidth="1px" Columns="30"
                                                                    CssClass="FormCtrl" DataType="City" ResourcesDir="asb_includes"
                                                                 IncludeMoreMenuItem="False" KeyPressDelay="300" MaxSuggestChars="100"
                                                                    MenuCSSClass="asbMenu" MenuItemCSSClass="asbMenuItem" MoreMenuItemLabel="..."
                                                                    NumMenuItems="10" SelMenuItemCSSClass="asbSelMenuItem" UseIFrame="True"></Custom:AutoSuggestBox>--%>

                                                                <%-- <Custom:AutoSuggestBox ID="txtau1firstnm" runat="server" BorderWidth="1px" Columns="30"
                                                                CssClass="FormCtrl" DataType="City"  ResourcesDir="asb_includes"
                                                                Width="145px"   MaxSuggestChars="100"
                                                                MenuCSSClass="asbMenu" MenuItemCSSClass="asbMenuItem" 
                                                                NumMenuItems="10" SelMenuItemCSSClass="asbSelMenuItem" ></Custom:AutoSuggestBox>--%>
                                                            </td>
                                                            <td >
                                                                <%--<INPUT class="text1" id="txtau1midnm" onblur="this.className='blur'" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; WIDTH: 136px; BORDER-BOTTOM: black 1px solid; HEIGHT: 20px"
														onfocus="this.className='focus'" type="text" size="21" runat="server">--%>
                                                                <asp:TextBox ID="txtau1midnm" runat="server" ></asp:TextBox>
                                                               <%-- <Custom:AutoSuggestBox ID="txtau1midnm" runat="server" BorderWidth="1px" Columns="30"
                                                                    CssClass="FormCtrl" DataType="City" ResourcesDir="asb_includes"
                                                                     IncludeMoreMenuItem="False" KeyPressDelay="300" MaxSuggestChars="100"
                                                                    MenuCSSClass="asbMenu" MenuItemCSSClass="asbMenuItem" MoreMenuItemLabel="..."
                                                                    NumMenuItems="10" SelMenuItemCSSClass="asbSelMenuItem" UseIFrame="True"></Custom:AutoSuggestBox>--%>
                                                            </td>
                                                            <td >
                                                                <%--<INPUT class="text1" id="txtau1surnm" onblur="this.className='blur'" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; WIDTH: 136px; BORDER-BOTTOM: black 1px solid; HEIGHT: 20px"
														onfocus="this.className='focus'" type="text" size="21" runat="server">--%>
                                                                <asp:TextBox ID="txtau1surnm" runat="server"  ></asp:TextBox>
<%--                                                                <Custom:AutoSuggestBox ID="txtau1surnm" runat="server" BorderWidth="1px" Columns="30"
                                                                    CssClass="FormCtrl" DataType="City" MaxLength="100" ResourcesDir="asb_includes"
                                                                  IncludeMoreMenuItem="False" KeyPressDelay="300" MaxSuggestChars="100"
                                                                    MenuCSSClass="asbMenu" MenuItemCSSClass="asbMenuItem" MoreMenuItemLabel="..."
                                                                    NumMenuItems="10" SelMenuItemCSSClass="asbSelMenuItem" UseIFrame="True"></Custom:AutoSuggestBox>--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                                <asp:Label ID="Label112" runat="server" CssClass="span">2.</asp:Label>
                                                            </td>
                                                            <td >
                                                                <%--<INPUT class="text1" id="txtau2firstnm" onblur="this.className='blur'" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; WIDTH: 142px; BORDER-BOTTOM: black 1px solid; HEIGHT: 20px"
														onfocus="this.className='focus'" type="text" size="26" runat="server">--%>
                                                                <asp:TextBox ID="txtau2firstnm" runat="server" ></asp:TextBox>
                                                               <%-- <Custom:AutoSuggestBox ID="txtau2firstnm" runat="server" BorderWidth="1px" Columns="30"
                                                                    CssClass="FormCtrl" DataType="City" MaxLength="100" ResourcesDir="asb_includes"
                                                                  IncludeMoreMenuItem="False" KeyPressDelay="300" MaxSuggestChars="100"
                                                                    MenuCSSClass="asbMenu" MenuItemCSSClass="asbMenuItem" MoreMenuItemLabel="..."
                                                                    NumMenuItems="10" SelMenuItemCSSClass="asbSelMenuItem" UseIFrame="True"></Custom:AutoSuggestBox>--%>
                                                            </td>
                                                            <td >
                                                                <%--<INPUT class="text1" id="txtau2midnm" onblur="this.className='blur'" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; WIDTH: 136px; BORDER-BOTTOM: black 1px solid; HEIGHT: 20px"
														onfocus="this.className='focus'" type="text" size="20" runat="server">--%>
                                                                <asp:TextBox ID="txtau2midnm" runat="server" ></asp:TextBox>
                                                               <%-- <Custom:AutoSuggestBox ID="txtau2midnm" runat="server" BorderWidth="1px" Columns="30"
                                                                    CssClass="FormCtrl" DataType="City" MaxLength="100" ResourcesDir="asb_includes"
                                                                 IncludeMoreMenuItem="False" KeyPressDelay="300" MaxSuggestChars="100"
                                                                    MenuCSSClass="asbMenu" MenuItemCSSClass="asbMenuItem" MoreMenuItemLabel="..."
                                                                    NumMenuItems="10" SelMenuItemCSSClass="asbSelMenuItem" UseIFrame="True"></Custom:AutoSuggestBox>--%>
                                                            </td>
                                                            <td >
                                                                <%--&nbsp;<INPUT class="text1" id="txtau2surnm" onblur="this.className='blur'" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; WIDTH: 136px; BORDER-BOTTOM: black 1px solid; HEIGHT: 20px"
														onfocus="this.className='focus'" type="text" size="16" runat="server">--%>
                                                                <asp:TextBox ID="txtau2surnm" runat="server" ></asp:TextBox>
<%--                                                                <Custom:AutoSuggestBox ID="txtau2surnm" runat="server" BorderWidth="1px" Columns="30"
                                                                    CssClass="FormCtrl" DataType="City" MaxLength="100" ResourcesDir="asb_includes"
                                                                    IncludeMoreMenuItem="False" KeyPressDelay="300" MaxSuggestChars="100"
                                                                    MenuCSSClass="asbMenu" MenuItemCSSClass="asbMenuItem" MoreMenuItemLabel="..."
                                                                    NumMenuItems="10" SelMenuItemCSSClass="asbSelMenuItem" UseIFrame="True"></Custom:AutoSuggestBox>--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                                <asp:Label ID="Label113" runat="server" CssClass="span">3.</asp:Label>
                                                            </td>
                                                            <td>
                                                                <%--<INPUT class="text1" id="txtau3firstnm" onblur="this.className='blur'" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; WIDTH: 142px; BORDER-BOTTOM: black 1px solid; HEIGHT: 20px"
														onfocus="this.className='focus'" type="text" size="26" runat="server">--%>

                                                                <asp:TextBox ID="txtau3firstnm" runat="server"></asp:TextBox>
<%--                                                                <Custom:AutoSuggestBox ID="txtau3firstnm" runat="server" BorderWidth="1px" Columns="30"
                                                                    CssClass="FormCtrl" DataType="City" MaxLength="100" ResourcesDir="asb_includes"
                                                                   IncludeMoreMenuItem="False" KeyPressDelay="300" MaxSuggestChars="100"
                                                                    MenuCSSClass="asbMenu" MenuItemCSSClass="asbMenuItem" MoreMenuItemLabel="..."
                                                                    NumMenuItems="10" SelMenuItemCSSClass="asbSelMenuItem" UseIFrame="True"></Custom:AutoSuggestBox>--%>
                                                            </td>
                                                            <td >
                                                                <%--<INPUT class="text1" id="txtau3midnm" onblur="this.className='blur'" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; WIDTH: 136px; BORDER-BOTTOM: black 1px solid; HEIGHT: 20px"
														onfocus="this.className='focus'" type="text" size="17" runat="server">--%>
                                                                <asp:TextBox ID="txtau3midnm" runat="server" ></asp:TextBox>
<%--                                                                <Custom:AutoSuggestBox ID="txtau3midnm" runat="server" BorderWidth="1px" Columns="30"
                                                                    CssClass="FormCtrl" DataType="City" MaxLength="100" ResourcesDir="asb_includes"
                                                                  IncludeMoreMenuItem="False" KeyPressDelay="300" MaxSuggestChars="100"
                                                                    MenuCSSClass="asbMenu" MenuItemCSSClass="asbMenuItem" MoreMenuItemLabel="..."
                                                                    NumMenuItems="10" SelMenuItemCSSClass="asbSelMenuItem" UseIFrame="True"></Custom:AutoSuggestBox>--%>
                                                            </td>
                                                            <td >
                                                                <%--&nbsp;<INPUT class="text1" id="txtau3surnm" onblur="this.className='blur'" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; WIDTH: 136px; BORDER-BOTTOM: black 1px solid; HEIGHT: 20px"
														onfocus="this.className='focus'" type="text" size="16" runat="server">--%>
                                                                <asp:TextBox ID="txtau3surnm" runat="server" ></asp:TextBox>
<%--                                                                <Custom:AutoSuggestBox ID="txtau3surnm" runat="server" BorderWidth="1px" Columns="30"
                                                                    CssClass="FormCtrl" DataType="City" MaxLength="100" ResourcesDir="asb_includes"
                                                                     IncludeMoreMenuItem="False" KeyPressDelay="300" MaxSuggestChars="100"
                                                                    MenuCSSClass="asbMenu" MenuItemCSSClass="asbMenuItem" MoreMenuItemLabel="..."
                                                                    NumMenuItems="10" SelMenuItemCSSClass="asbSelMenuItem" UseIFrame="True"></Custom:AutoSuggestBox>--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                                <asp:Label ID="Label62" runat="server"  CssClass="span" Text="<%$ Resources:ValidationResources,LBetal %>"></asp:Label>
                                                                <asp:DropDownList ID="cboEditorETAL" onblur="this.className='blur'" Height="30" onfocus="this.className='focus'"
                                                                    runat="server" >
                                                                    <asp:ListItem Value="N"></asp:ListItem>
                                                                    <asp:ListItem Value="Y"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td >
                                                            <asp:Label ID="Label43" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LEditor %>"></asp:Label>
                                                            </td>
                                                            <td ></td>
                                                            <td ></td>
                                                        </tr>
                                                        
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label44" runat="server" CssClass="span">1.</asp:Label>
                                                            </td>
                                                            <td >
                                                                <input class="text1" id="editor1Fname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;" onfocus="this.className='focus'" type="text" size="21" runat="server">
                                                            </td>
                                                            <td >
                                                                <input class="text1" id="editor1Mname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                                                                    onfocus="this.className='focus'" type="text" size="26" runat="server">
                                                            </td>
                                                            <td ><input class="text1" id="editor1Lname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                                                                onfocus="this.className='focus'" type="text" size="26" runat="server">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                                <asp:Label ID="Label48" runat="server" CssClass="span">2.</asp:Label>
                                                            </td>
                                                            <td >
                                                                <input class="text1" id="editor2fname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                                                                    onfocus="this.className='focus'" type="text" size="26" runat="server">
                                                            </td>
                                                            <td >
                                                                <input class="text1" id="editor2Mname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                                                                    onfocus="this.className='focus'" type="text" size="26" runat="server">
                                                            </td>
                                                            <td ><input class="text1" id="editor2Lname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                                                                onfocus="this.className='focus'" type="text" size="26" runat="server">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                                <asp:Label ID="Label49" runat="server" CssClass="span">3.</asp:Label>
                                                            </td>
                                                            <td >
                                                                <input class="text1" id="editor3Fname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                                                                    onfocus="this.className='focus'" type="text" size="26" runat="server">
                                                            </td>
                                                            <td >
                                                                <input class="text1" id="editor3Mname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"     onfocus="this.className='focus'" type="text" size="26" runat="server">
                                                            </td>
                                                            <td ><input class="text1" id="editor3Lname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                                                                onfocus="this.className='focus'" type="text" size="17" runat="server">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label42" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LBetal %>"></asp:Label>
                                                                <asp:DropDownList ID="cboCompilerETAL" onblur="this.className='blur'" Height="30" onfocus="this.className='focus'"
                                                                    runat="server" >
                                                                    <asp:ListItem Value="N"></asp:ListItem>
                                                                    <asp:ListItem Value="Y"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td >
                                                            <asp:Label ID="Label50" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LComp %>"></asp:Label>
                                                            </td>
                                                            <td ></td>
                                                            <td ></td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                                <asp:Label ID="Label21" runat="server" CssClass="span">1.</asp:Label>
                                                            </td>
                                                            <td >
                                                                <input class="text1" id="compiler1Fname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                                                                    onfocus="this.className='focus'" type="text" size="26" runat="server">
                                                            </td>
                                                            <td >
                                                                <input class="text1" id="compiler1Mname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                                                                    onfocus="this.className='focus'" type="text" size="26" runat="server">
                                                            </td>
                                                            <td ><input class="text1" id="compiler1Lname" onblur="this.className='blur'" 
                                                                onfocus="this.className='focus'" type="text" size="26" runat="server">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                                <asp:Label ID="Label22" runat="server" CssClass="span">2.</asp:Label>
                                                            </td>
                                                            <td >
                                                                <input class="text1" id="compiler2Fname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                                                                    onfocus="this.className='focus'" type="text" size="26" runat="server">
                                                            </td>
                                                            <td >
                                                                <input class="text1" id="compiler2Mname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                                                                    onfocus="this.className='focus'" type="text" size="26" runat="server">
                                                            </td>
                                                            <td ><input class="text1" id="compiler2Lname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                                                                onfocus="this.className='focus'" type="text" size="26" runat="server">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                                <asp:Label ID="Label27" runat="server" CssClass="span">3.</asp:Label>
                                                            </td>
                                                            <td >
                                                                <input class="text1" id="compiler3Fname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid;"
                                                                    onfocus="this.className='focus'" type="text" size="26" runat="server">
                                                            </td>
                                                            <td >
                                                                <input class="text1" id="compiler3Mname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                                                                    onfocus="this.className='focus'" type="text" size="26" runat="server">
                                                            </td>
                                                            <td ><input class="text1" id="compiler3Lname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                                                                onfocus="this.className='focus'" type="text" size="26" runat="server">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                                <asp:Label ID="Label72" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LBetal %>"></asp:Label>
                                                                <asp:DropDownList ID="cboILLustratorETAL" onblur="this.className='blur'" Height="30" onfocus="this.className='focus'"
                                                                    runat="server">
                                                                    <asp:ListItem Value="N"></asp:ListItem>
                                                                    <asp:ListItem Value="Y"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td >
                                                            <asp:Label ID="Label51" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LIllus %>"></asp:Label>
                                                            </td>
                                                            <td ></td>
                                                            <td ></td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                                <asp:Label ID="Label66" runat="server" CssClass="span">1.</asp:Label>
                                                            </td>
                                                            <td >
                                                                <input class="text1" id="Illustrator1Fname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;border-bottom: black 1px solid;"
                                                                    onfocus="this.className='focus'" type="text" size="26" runat="server">
                                                            </td>
                                                            <td >
                                                                <input class="text1" id="Illustrator1Mname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;border-bottom: black 1px solid;"
                                                                    onfocus="this.className='focus'" type="text" size="26" runat="server">
                                                            </td>
                                                            <td ><input class="text1" id="Illustrator1Lname" onblur="this.className='blur'"
                                                                style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid;"
                                                                onfocus="this.className='focus'"
                                                                type="text" size="26" runat="server">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                                <asp:Label ID="Label67" runat="server" CssClass="span">2.</asp:Label>
                                                            </td>
                                                            <td >
                                                                <input class="text1" id="Illustrator2Fname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;border-bottom: black 1px solid; "
                                                                    onfocus="this.className='focus'" type="text" size="26" runat="server">
                                                            </td>
                                                            <td >
                                                                <input class="text1" id="Illustrator2Mname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                                                                    onfocus="this.className='focus'" type="text" size="26" runat="server">
                                                            </td>
                                                            <td ><input class="text1" id="Illustrator2Lname" onblur="this.className='blur'"
                                                                style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                                onfocus="this.className='focus'"
                                                                type="text" size="26" runat="server">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                                <asp:Label ID="Label68" runat="server" CssClass="span">3.</asp:Label>
                                                            </td>
                                                            <td >
                                                                <input class="text1" id="Illustrator3Fname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid;"
                                                                    onfocus="this.className='focus'" type="text" size="26" runat="server">
                                                            </td>
                                                            <td >
                                                                <input class="text1" id="Illustrator3Mname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                                    onfocus="this.className='focus'" type="text" size="26" runat="server">
                                                            </td>
                                                            <td ><input class="text1" id="Illustrator3lname" onblur="this.className='blur'"
                                                                style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                                                                onfocus="this.className='focus'"
                                                                type="text" size="26" runat="server">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                                <asp:Label ID="Label73" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LBetal %>"></asp:Label>
                                                                <asp:DropDownList ID="cboTranslatorETAL" onblur="this.className='blur'" Height="30" onfocus="this.className='focus'"
                                                                    runat="server" >
                                                                    <asp:ListItem Value="N"></asp:ListItem>
                                                                    <asp:ListItem Value="Y"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td >
                                                            <asp:Label ID="Label35" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LITransltr %>"></asp:Label>
                                                            </td>
                                                            <td ></td>
                                                            <td ></td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                                <asp:Label ID="Label69" runat="server" CssClass="span">1.</asp:Label>
                                                            </td>
                                                            <td >
                                                                <input class="text1" id="Translator1Fname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                                                                    onfocus="this.className='focus'" type="text" size="26" runat="server">
                                                            </td>
                                                            <td >
                                                                <input class="text1" id="Translator1Mname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                                                                    onfocus="this.className='focus'" type="text" size="26" runat="server">
                                                            </td>
                                                            <td ><input class="text1" id="Translator1Lname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                                onfocus="this.className='focus'" type="text" size="26" runat="server">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                                <asp:Label ID="Label29" runat="server" CssClass="span">2.</asp:Label>
                                                            </td>
                                                            <td >
                                                                <input class="text1" id="Translator2Fname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                                                                    onfocus="this.className='focus'" type="text" size="26" runat="server">
                                                            </td>
                                                            <td >
                                                                <input class="text1" id="Translator2Mname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;border-bottom: black 1px solid; "
                                                                    onfocus="this.className='focus'" type="text" size="26" runat="server">
                                                            </td>
                                                            <td ><input class="text1" id="Translator2Lname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                                                                onfocus="this.className='focus'" type="text" size="26" runat="server">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                                <asp:Label ID="Label37" runat="server" CssClass="span">3.</asp:Label>
                                                            </td>
                                                            <td >
                                                                <input class="text1" id="Translator3Fname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                                                                    onfocus="this.className='focus'" type="text" size="26" runat="server">
                                                            </td>
                                                            <td >
                                                                <input class="text1" id="Translator3Mname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                                                                    onfocus="this.className='focus'" type="text" size="26" runat="server">
                                                            </td>
                                                            <td ><input class="text1" id="Translator3Lname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                                onfocus="this.className='focus'" type="text" size="26" runat="server">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">
                                                            <asp:Label ID="Label64" runat="server" CssClass="span"  Text="<%$ Resources:ValidationResources,LBCorpAutM %>"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                            </td>
                                                            <td  colspan="3">
                                                                <textarea id="txtConferenceName" runat="server" onfocus="this.className='focus'"
                                                                    onblur="this.className='blur'" class="txt10" ></textarea>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td  colspan="4">
                                                            <asp:Label ID="Label126" runat="server"  CssClass="span" Text="<%$ Resources:ValidationResources,LPlcAndDt %>"></asp:Label>
                                                            </td>
                                                            <td ></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td colspan="3">
                                                                <input class="text1" id="txtConferenceYear" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                                                                    onfocus="this.className='focus'" type="text" runat="server">
                                                            </td>
                                                            <td ></td>
                                                        </tr>
                                                        <tr>
                                                            <td   colspan="5" ></td>
                                                        </tr>
                                                        <tr>
                                                            <td  colspan="4" ></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">
                                                                <table id="Table15" class="GenTable1" style="width:100%">
                                                                    <tr>
                                                                        <td  style="text-align:center">
                                                                            <input id="cmdsave4"  type="submit" value="<%$ Resources:ValidationResources,bSave %>"
                                                                                runat="server" class="btnstyle">
                                                                        
                                                                            <input id="cmdReset4" class="btnstyle" type="submit" value="<%$ Resources:ValidationResources,bReset %>"
                                                                                runat="server">
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        
                                                    </table>
                                                </asp:Panel>
                                            </div>   
                                        <div class="no-more-tables">
                                       <asp:Panel ID="panel3" runat="server">
                                                    <table id="Table3" class="GenTable1" style="width:100%">
                                                         <tr>
                                                            <td colspan="4"></td>
                                                             </tr>
                                                        <tr>
                                                            <td colspan="4" style="text-align:center">
                                                                <asp:Label ID="Label843" runat="server" CssClass="head" Text="<%$ Resources:ValidationResources,LPhyDescCAr %>"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td  colspan="4">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            <asp:Label ID="Label9rt" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LInPage %>"></asp:Label>
                                                            </td>
                                                            <td >
                                                                <input id="txtinitpages" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid;"
                                                                    onfocus="this.className='focus'" type="text" runat="server">
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label123412" runat="server"  CssClass="span"
                                                                    Text="<%$ Resources:ValidationResources,LTotlPage %>"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <input id="txtpages" onkeypress="IntegerNumber(this)" onblur="this.className='blur'"
                                                                    style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                                                                    onfocus="this.className='focus'"
                                                                    type="text" runat="server">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            <asp:Label ID="Label33" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LVolPage %>"></asp:Label>
                                                            </td>
                                                            <td >
                                                                <input id="txtvolpages" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid;"
                                                                    onfocus="this.className='focus'" type="text" runat="server">
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label32" runat="server" CssClass="span"  Text="<%$ Resources:ValidationResources,LBibliPages %>"></asp:Label>
                                                            </td>
                                                            <td >
                                                                <input id="txtbiblpages" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                                    onfocus="this.className='focus'" type="text" runat="server">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            <asp:Label ID="Label30" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LBkSize %>"></asp:Label>
                                                            </td>
                                                            <td >
                                                                <input id="txtbooksize" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                                                                    onfocus="this.className='focus'" type="text" runat="server">
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Labelt338" runat="server" CssClass="span"  Text="<%$ Resources:ValidationResources,LVols %>"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <input onkeypress="IntegerNumber(this)" id="txtvolno" onblur="this.className='blur'"
                                                                    style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                                                                    onfocus="this.className='focus'"
                                                                    type="text" runat="server">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            <asp:Label ID="Label13e34" runat="server"  CssClass="span"
                                                                Text="<%$ Resources:ValidationResources,Prts %>"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <input id="txtparts" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                                                                    onfocus="this.className='focus'" type="text" runat="server">
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label56" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LMaps %>"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <input id="txtmaps" onkeypress="IntegerNumber(this)" onblur="this.className='blur'"
                                                                    style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                                                                    onfocus="this.className='focus'"
                                                                    type="text" runat="server"></>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            <asp:Label ID="Label40" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LILLust %>"></asp:Label>
                                                            </td>
                                                            <td >
                                                                <asp:DropDownList ID="cboIllistration" onblur="this.className='blur'" Height="30" onfocus="this.className='focus'"
                                                                    runat="server">
                                                                    <asp:ListItem Value="N"></asp:ListItem>
                                                                    <asp:ListItem Value="Y"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label36" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LBkIndx %>"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="cboBookIndex" onblur="this.className='blur'" Height="30" onfocus="this.className='focus'"
                                                                    runat="server">
                                                                    <asp:ListItem Value="N"></asp:ListItem>
                                                                    <asp:ListItem Value="Y"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            <asp:Label ID="Label41" runat="server" CssClass="span"  Text="<%$ Resources:ValidationResources, LVarPagings %>"></asp:Label>
                                                            </td>
                                                            <td >
                                                                <asp:DropDownList ID="cbovariouspaging" onblur="this.className='blur'" Height="30" onfocus="this.className='focus'"
                                                                    runat="server">
                                                                    <asp:ListItem Value="N"></asp:ListItem>
                                                                    <asp:ListItem Value="Y"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label34" runat="server"  CssClass="span" Text="<%$ Resources:ValidationResources,LFldLeaves %>"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <input id="txtleaves" onkeypress="IntegerNumber(this)" onblur="this.className='blur'"
                                                                    style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                                    onfocus="this.className='focus'"
                                                                    type="text" runat="server">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">
                                                            <asp:Label ID="Label81" runat="server"  CssClass="span" Text="<%$ Resources:ValidationResources,LAccMtrInfo %>"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td  colspan="4">
                                                                <textarea class="txt10" id="txtmaterialinfo" onblur="this.className='blur'" 
                                                                    onfocus="this.className='focus'" rows="5" cols="70" runat="server"></textarea>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="5" ></td>
                                                        </tr>
                                                        <tr>
                                                            <td   colspan="6" ></td>
                                                        </tr>
                                                        <tr>
                                                            <td  colspan="5" ></td>
                                                        </tr>
                                                        <tr>
                                                            <td  colspan="5"></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="5">
                                                                <table id="Table16" class="table-condensed GenTable1">
                                                                    <tr>
                                                                        <td style="text-align:center">
                                                                            <input id="cmdsave3" class="btnstyle"  type="submit" value="<%$ Resources:ValidationResources,bSave %>"
                                                                                runat="server">
                                                                       
                                                                            <input id="cmdReset3" class="btnstyle"  type="submit" value="<%$ Resources:ValidationResources,bReset %>"
                                                                                runat="server">
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                          </div>     
                                        <div class="no-more-tables">
                                       <asp:Panel ID="panel4" runat="server">
                                                    <table id="Table5" class="table-condensed GenTable1">
                                                        <tr>
                                                            <td  colspan="4"></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4" style="text-align:center">
                                                                <asp:Label ID="Label91" runat="server"  BorderWidth="1px" CssClass="head"
                                                                    Text="<%$ Resources:ValidationResources,LSerAndSerEd %>"></asp:Label>
                                                            </td>
                                                        </tr>
                                                       
                                                        <tr>
                                                            <td  colspan="4">
                                                            <asp:Label ID="Label14" runat="server" CssClass="head1" Text="<%$ Resources:ValidationResources,LMnSeriesDt %>"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                            <asp:Label ID="Label52" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LSerTtl %>"></asp:Label>
                                                            </td>
                                                            <td  colspan="3">
                                                                <input class="text1" id="txtseriesname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid;"
                                                                    onfocus="this.className='focus'" type="text" size="65" runat="server">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            <asp:Label ID="Label84" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LBParlTtl %>"></asp:Label>
                                                            </td>
                                                            <td  colspan="3">
                                                                <input class="text1" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                                    onfocus="this.className='focus'" type="text" size="65" runat="server"
                                                                    id="txtPTitle">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                            <asp:Label ID="Label53" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LSeriesNo %>"></asp:Label>
                                                            </td>
                                                            <td >
                                                                <input id="txtseriesno" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                                                                    onfocus="this.className='focus'" type="text" size="13" runat="server"><asp:Label
                                                                        ID="Label54" runat="server"  CssClass="span" Text="<%$ Resources:ValidationResources,LSersPart %>"></asp:Label>
                                                            </td>
                                                            <td >
                                                                <input id="txtseriespart" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                                                                    onfocus="this.className='focus'" type="text" size="5" runat="server"><asp:Label
                                                                        ID="Label85" runat="server"  CssClass="span" Text="<%$ Resources:ValidationResources,LSerVol %>"></asp:Label>
                                                            </td>
                                                            <td  colspan="3">
                                                                <input id="txtSVolume" onkeypress="IntegerNumber(this)" onblur="this.className='blur'"
                                                                    style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid;"
                                                                    onfocus="this.className='focus'"
                                                                    type="text" size="2" runat="server">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                            <asp:Label ID="Label123" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LISSNNo %>"></asp:Label>
                                                            </td>
                                                            <td colspan="2">
                                                                <input id="txtMainissn" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                                                                    onfocus="this.className='focus'" type="text" size="42" runat="server">
                                                            </td>
                                                            <td colspan="3"></td>
                                                        </tr>
                                                        <tr>
                                                            <td  colspan="4">
                                                            <asp:Label ID="Label90" runat="server" CssClass="opt" Text="<%$ Resources:ValidationResources,LEdits %>"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                            <asp:Label ID="Label86" runat="server"  CssClass="span" Text="<%$ Resources:ValidationResources,LBetal %>"></asp:Label>
                                                                <asp:DropDownList ID="status" onblur="this.className='blur'" Height="30" onfocus="this.className='focus'"
                                                                    runat="server">
                                                                    <asp:ListItem Value="N"></asp:ListItem>
                                                                    <asp:ListItem Value="Y"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td >
                                                                <asp:Label ID="Label87" runat="server" CssClass="opt" Text="<%$ Resources:ValidationResources,LFname %>"></asp:Label>
                                                            </td>
                                                            <td >
                                                                <asp:Label ID="Label88" runat="server" CssClass="opt" Text="<%$ Resources:ValidationResources,LMname %>"></asp:Label>
                                                            </td>
                                                            <td >
                                                                <asp:Label ID="Label89" runat="server"  CssClass="opt" Text="<%$ Resources:ValidationResources,LLname %>"></asp:Label>
                                                            </td>
                                                            <td ></td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                                <asp:Label ID="Label93" runat="server" CssClass="span">1.</asp:Label>
                                                            </td>
                                                            <td >
                                                                <input class="txt10" id="af1" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                                                                    onfocus="this.className='focus'" type="text" size="14" runat="server">
                                                            </td>
                                                            <td >
                                                                <input class="txt10" id="am1" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                                                                    onfocus="this.className='focus'" type="text" size="14" runat="server">
                                                            </td>
                                                            <td >
                                                                <input class="txt10" id="al1" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                                    onfocus="this.className='focus'" type="text" size="14" runat="server">
                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                                <asp:Label ID="Label94" runat="server"  CssClass="span">2.</asp:Label>&nbsp;
                                                            </td>
                                                            <td >
                                                                <input class="txt10" id="af2" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                                                                    onfocus="this.className='focus'" type="text" size="14" runat="server">
                                                            </td>
                                                            <td >
                                                                <input class="txt10" id="am2" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                                    onfocus="this.className='focus'" type="text" size="14" runat="server">
                                                            </td>
                                                            <td>
                                                                <input class="txt10" id="al2" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                                                                    onfocus="this.className='focus'" type="text" size="14" runat="server">
                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                                <asp:Label ID="Label95" runat="server"  CssClass="span">3.</asp:Label>&nbsp;
                                                            </td>
                                                            <td >
                                                                <input class="txt10" id="af3" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                                    onfocus="this.className='focus'" type="text" size="14" runat="server">
                                                            </td>
                                                            <td >
                                                                <input class="txt10" id="am3" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid;"
                                                                    onfocus="this.className='focus'" type="text" size="14" runat="server">
                                                            </td>
                                                            <td >
                                                                <input class="text1" id="al3" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                                                                    onfocus="this.className='focus'" type="text" size="14" runat="server">
                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td  bgcolor="black" colspan="5" ></td>
                                                        </tr>
                                                        <tr>
                                                            <td  colspan="5">
                                                            <asp:Label ID="Label15" runat="server"  CssClass="head1" Text="<%$ Resources:ValidationResources,LSubSeDt %>"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                            <asp:Label ID="Label16" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LSerTtl %>"></asp:Label>
                                                            </td>
                                                            <td  colspan="3">
                                                                <input class="text1" id="txtSubseriesname" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid;"
                                                                    onfocus="this.className='focus'" type="text" size="65" runat="server">
                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                            <asp:Label ID="Label17" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LBParlTtl %>"></asp:Label>
                                                            </td>
                                                            <td  colspan="3">
                                                                <input class="text1" id="txtSubPTitle" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                                    onfocus="this.className='focus'" type="text" size="65" runat="server">
                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                            <asp:Label ID="Label18" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LSeriesNo %>"></asp:Label>
                                                            </td>
                                                            <td >
                                                                <input id="txtSubseriesno" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid;"
                                                                    onfocus="this.className='focus'" type="text" size="13" runat="server">&nbsp;&nbsp;<asp:Label
                                                                        ID="Label19" runat="server" Width="72px" CssClass="span" Text="<%$ Resources:ValidationResources,LSersPart %>"></asp:Label>
                                                            </td>
                                                            <td style="width: 150px; height: 21px" align="left">
                                                                <input id="txtSubseriespart" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                                                                    onfocus="this.className='focus'" type="text" size="5" runat="server"><asp:Label
                                                                        ID="Label114" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LSerVol %>"></asp:Label>
                                                            </td>
                                                            <td >
                                                                <input id="txtSubSVolume" onkeypress="IntegerNumber(this)" onblur="this.className='blur'"
                                                                    style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                                                                    onfocus="this.className='focus'"
                                                                    type="text" size="2" runat="server">
                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                            <asp:Label ID="Label124" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LISSNNo %>"></asp:Label>
                                                            </td>
                                                            <td  colspan="2">
                                                                <input id="txtSubissn" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid;"
                                                                    onfocus="this.className='focus'" type="text" size="42" runat="server">
                                                            </td>
                                                            <td ></td>
                                                            <td ></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            <asp:Label ID="Label115" runat="server"  CssClass="opt" Text="<%$ Resources:ValidationResources,LEdits %>"></asp:Label>
                                                            </td>
                                                            <td ></td>
                                                            <td></td>
                                                            <td ></td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                                <asp:Label ID="Label122" runat="server"  CssClass="span" Text="<%$ Resources:ValidationResources,LBetal %>"></asp:Label>
                                                                <asp:DropDownList ID="Substatus" onblur="this.className='blur'" Height="30" onfocus="this.className='focus'"
                                                                    runat="server" >
                                                                    <asp:ListItem Value="N"></asp:ListItem>
                                                                    <asp:ListItem Value="Y"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td >
                                                                <asp:Label ID="Label119" runat="server" CssClass="opt" Text="<%$ Resources:ValidationResources,LFname %>"></asp:Label>
                                                            </td>
                                                            <td >
                                                                <asp:Label ID="Label120" runat="server" CssClass="opt" Text="<%$ Resources:ValidationResources,LMname %>"></asp:Label>
                                                            </td>
                                                            <td >
                                                                <asp:Label ID="Label121" runat="server"  CssClass="opt" Text="<%$ Resources:ValidationResources,LLname %>"></asp:Label>
                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                                <asp:Label ID="Label116" runat="server" Width="4px" CssClass="span">1.</asp:Label>
                                                            </td>
                                                            <td >
                                                                <input class="txt10" id="Subaf1" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                                                                    onfocus="this.className='focus'" type="text" size="14" runat="server">
                                                            </td>
                                                            <td >
                                                                <input class="txt10" id="Subam1" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                                    onfocus="this.className='focus'" type="text" size="14" runat="server">
                                                            </td>
                                                            <td >
                                                                <input class="txt10" id="Subal1" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                                                                    onfocus="this.className='focus'" type="text" size="14" runat="server">
                                                            </td>
                                                            <td ></td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                                <asp:Label ID="Label117" runat="server" Width="4px" CssClass="span">2.</asp:Label>
                                                            </td>
                                                            <td>
                                                                <input class="txt10" id="Subaf2" onblur="this.className='blur'" style="                                                                        border-right: black 1px solid;
                                                                        border-top: black 1px solid;
                                                                        border-left: black 1px solid;
                                                                        border-bottom: black 1px solid;"
                                                                    onfocus="this.className='focus'" type="text" size="14" runat="server">
                                                            </td>
                                                            <td >
                                                                <input class="txt10" id="Subam2" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;border-bottom: black 1px solid;"
                                                                    onfocus="this.className='focus'" type="text" size="14" runat="server">
                                                            </td>
                                                            <td >
                                                                <input class="txt10" id="Subal2" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                                                                    onfocus="this.className='focus'" type="text" size="14" runat="server">
                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label118" runat="server" Width="4px" CssClass="span">3.</asp:Label>
                                                            </td>
                                                            <td >
                                                                <input class="txt10" id="Subaf3" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid;"
                                                                    onfocus="this.className='focus'" type="text" size="14" runat="server">
                                                            </td>
                                                            <td >
                                                                <input class="txt10" id="Subam3" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                                                                    onfocus="this.className='focus'" type="text" size="14" runat="server">
                                                            </td>
                                                            <td >
                                                                <input class="text1" id="Subal3" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                                                                    onfocus="this.className='focus'" type="text" size="14" runat="server">
                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td  bgcolor="black" colspan="4"></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4" >
                                                            <asp:Label ID="Label101" runat="server" CssClass="head1" Text="<%$ Resources:ValidationResources,LSecdSrDt %>"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                            <asp:Label ID="Label96" runat="server"  CssClass="span" Text="<%$ Resources:ValidationResources,LSerTtl %>"></asp:Label>
                                                            </td>
                                                            <td  colspan="3">
                                                                <input class="text1" id="txtSecondSeriesTitle" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid;"
                                                                    onfocus="this.className='focus'" type="text" size="64" runat="server">
                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                            <asp:Label ID="Label97" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LBParlTtl %>"></asp:Label>
                                                            </td>
                                                            <td colspan="3">
                                                                <input class="text1" id="txtSecondParallelTitle" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;border-bottom: black 1px solid; "
                                                                    onfocus="this.className='focus'" type="text" size="64" runat="server">
                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                            <asp:Label ID="Label98" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LSeriesNo %>"></asp:Label>
                                                            </td>
                                                            <td >
                                                                <input class="text1" id="txtSecondSeriesNo" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                                    onfocus="this.className='focus'" type="text" size="16" runat="server">&nbsp;&nbsp;<asp:Label
                                                                        ID="Label99" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LSersPart %>"></asp:Label>
                                                            </td>
                                                            <td >
                                                                <input class="text1" id="txtSecondSeriesPart" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                                    onfocus="this.className='focus'" type="text" size="4" runat="server"><asp:Label
                                                                        ID="Label100" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LSerVol %>"></asp:Label>
                                                            </td>
                                                            <td  colspan="3">
                                                                <input class="text1" id="txtsecSeriesVol" onkeypress="IntegerNumber(this)" onblur="this.className='blur'"
                                                                    style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                                                                    onfocus="this.className='focus'"
                                                                    type="text" size="2" runat="server">
                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                            <asp:Label ID="Label125" runat="server" CssClass="span"  Text="<%$ Resources:ValidationResources,LISSNNo %>"></asp:Label>
                                                            </td>
                                                            <td  colspan="2">
                                                                <input id="txtSecondissn" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid;"
                                                                    onfocus="this.className='focus'" type="text" size="41" runat="server">
                                                            </td>
                                                            <td colspan="3"></td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td  colspan="5">
                                                            <asp:Label ID="Label102" runat="server"  CssClass="opt" Text="<%$ Resources:ValidationResources,LEdits %>"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                                <asp:Label ID="Label103" runat="server"  CssClass="span" Text="<%$ Resources:ValidationResources,LBetal %>"></asp:Label>
                                                                <asp:DropDownList ID="cmbSecetal" onblur="this.className='blur'" Height="30" onfocus="this.className='focus'"
                                                                    runat="server" >
                                                                    <asp:ListItem Value="N" Text="N"></asp:ListItem>
                                                                    <asp:ListItem Value="Y" Text="Y"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td >
                                                                <asp:Label ID="Label104" runat="server" CssClass="opt" Text="<%$ Resources:ValidationResources,LFname %>"></asp:Label>
                                                            </td>
                                                            <td >
                                                                <asp:Label ID="Label105" runat="server" CssClass="opt" Text="<%$ Resources:ValidationResources,LMname %>"></asp:Label>
                                                            </td>
                                                            <td  colspan="3">
                                                                <asp:Label ID="Label106" runat="server" CssClass="opt" Text="<%$ Resources:ValidationResources,LLname %>"></asp:Label>
                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label107" runat="server" Width="4px" CssClass="span">1.</asp:Label>
                                                            </td>
                                                            <td >
                                                                <input class="txt10" id="txtSecFirstName1" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                                    onfocus="this.className='focus'" type="text" size="14" runat="server">
                                                            </td>
                                                            <td >
                                                                <input class="txt10" id="txtSecMidName1" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                                                                    onfocus="this.className='focus'" type="text" size="14" runat="server">
                                                            </td>
                                                            <td  colspan="3">
                                                                <input class="txt10" id="txtSecLastName1" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid;"
                                                                    onfocus="this.className='focus'" type="text" size="14" runat="server">
                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                                <asp:Label ID="Label108" runat="server" Width="4px" CssClass="span">2.</asp:Label>
                                                            </td>
                                                            <td >
                                                                <input class="txt10" id="txtSecFirstName2" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;border-bottom: black 1px solid;"
                                                                    onfocus="this.className='focus'" type="text" size="14" runat="server">
                                                            </td>
                                                            <td >
                                                                <input class="txt10" id="txtSecMidName2" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;  border-bottom: black 1px solid; "
                                                                    onfocus="this.className='focus'" type="text" size="14" runat="server">
                                                            </td>
                                                            <td  colspan="3">
                                                                <input class="txt10" id="txtSecLastName2" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                                                                    onfocus="this.className='focus'" type="text" size="14" runat="server">
                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                                <asp:Label ID="Label109" runat="server" Width="4px" CssClass="span">3.</asp:Label>
                                                            </td>
                                                            <td >
                                                                <input class="txt10" id="txtSecFirstName3" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                                    onfocus="this.className='focus'" type="text" size="14" runat="server">
                                                            </td>
                                                            <td >
                                                                <input class="txt10" id="txtSecMidName3" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                                    onfocus="this.className='focus'" type="text" size="14" runat="server">
                                                            </td>
                                                            <td  colspan="3">
                                                                <input class="txt10" id="txtSecLastName3" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                                    onfocus="this.className='focus'" type="text" size="14" runat="server">
                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                        
                                                        <tr>
                                                            <td align="center" bgcolor="black" colspan="7" height="1"></td>
                                                        </tr>
                                                       
                                                        <tr>
                                                            <td  colspan="7">
                                                                <table id="Table18" class="GenTable1" width="100%">
                                                                    <tr>
                                                                        <td align="center">
                                                                            <input id="cmdsave" class="btnstyle" type="submit" value="<%$ Resources:ValidationResources,bSave %>"
                                                                                runat="server">
                                                                       
                                                                            <input id="cmdReset" class="btnstyle" type="submit" value="<%$ Resources:ValidationResources,bReset %>"
                                                                                runat="server">
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                        </div>       
                                        <div class="no-more-tables">
                                       <asp:Panel ID="Panel5" runat="server">
                                                    <table id="Table2" class="table-condensed GenTable1">
                                                        <tr>
                                                            <td  colspan="4"></td>
                                                        </tr>
                                                        <tr>
                                                            <td  colspan="4" style="text-align:center">
                                                                <asp:Label ID="Label57" runat="server"  CssClass="head" Text="<%$ Resources:ValidationResources,TabNoteSec %>"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td  colspan="4"></td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                            <asp:Label ID="Label75" runat="server"  CssClass="span" Text="<%$ Resources:ValidationResources,LBibliography %>"></asp:Label>
                                                            </td>
                                                            <td  colspan="3">
                                                                <textarea id="txtBN" onblur="this.className='blur'" onfocus="this.className='focus'"
                                                                    style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid"
                                                                    rows="2" cols="56" runat="server" class="txt10"></textarea>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            <asp:Label ID="Label76" runat="server"  CssClass="span" Text="<%$ Resources:ValidationResources,LContent %>"></asp:Label>
                                                            </td>
                                                            <td  colspan="3">
                                                                <textarea id="txtcn" onblur="this.className='blur'" onfocus="this.className='focus'"
                                                                    style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid"
                                                                    rows="2" cols="56" runat="server" class="txt10"></textarea>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                            <asp:Label ID="Label77" runat="server"  CssClass="span" Text="<%$ Resources:ValidationResources,LBBkInfo %>"></asp:Label>
                                                            </td>
                                                            <td  colspan="3">
                                                                <textarea id="txtgn" onblur="this.className='blur'" onfocus="this.className='focus'"
                                                                    style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid"
                                                                    rows="2" cols="56" runat="server" class="txt10"></textarea>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                            <asp:Label ID="Label78" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LVolume %>"></asp:Label>
                                                            </td>
                                                            <td  colspan="3">
                                                                <textarea id="txtvn" onblur="this.className='blur'" onfocus="this.className='focus'"
                                                                    style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid"
                                                                    rows="2" cols="56" runat="server" class="txt10"></textarea>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                            <asp:Label ID="Label79" runat="server"  CssClass="span" Text="<%$ Resources:ValidationResources,LSponsor %>"></asp:Label>
                                                            </td>
                                                            <td  colspan="3">
                                                                <textarea id="txtsn" onblur="this.className='blur'" onfocus="this.className='focus'"
                                                                    style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid"
                                                                    rows="2" cols="56" runat="server" class="txt10"></textarea>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                            <asp:Label ID="Label92" runat="server"  CssClass="span" Text="<%$ Resources:ValidationResources,LAnalytical %>"></asp:Label>
                                                            </td>
                                                            <td  colspan="3">
                                                                <textarea id="txtan" onblur="this.className='blur'" onfocus="this.className='focus'"
                                                                    style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid"
                                                                    rows="2" cols="56" runat="server" class="txt10"></textarea>
                                                            </td>
                                                        </tr>
                                                      
                                                        <tr>
                                                            <td  colspan="5" ></td>
                                                        </tr>
                                                      
                                                        <tr>
                                                            <td  colspan="5">
                                                                <table id="Table19" class="table-condensed GenTable1">
                                                                    <tr>
                                                                        <td style="text-align:center">
                                                                            <input id="cmdsave2" class="btnstyle"  type="submit" value="<%$ Resources:ValidationResources,bSave %>"
                                                                                runat="server">
                                                                        
                                                                            <input id="cmdReset2" class="btnstyle" type="submit" value="<%$ Resources:ValidationResources,bReset %>"
                                                                                runat="server">
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                          </div>     
                                             <div class="no-more-tables">
                                            <asp:Panel ID="panel6" runat="server">
                                               
                                                    <table id="Table7" class="table-condensed GenTable1">
                                                       <tr>
                                                            <td  colspan="2"></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" style="text-align:center">
                                                                <asp:Label ID="Label83" runat="server"  CssClass="head" Text="<%$ Resources:ValidationResources,LStdnosub %>"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td  colspan="2">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="1" rowspan="1" >
                                                            <asp:Label ID="Label71" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LISBN %>"></asp:Label>
                                                            </td>
                                                            <td >
                                                                <input id="txtisbn" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                                                                    onfocus="this.className='focus'" type="text" runat="server">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                            <asp:Label ID="Label24" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LSub1 %>"></asp:Label>
                                                            </td>
                                                            <td >
                                                                <asp:TextBox ID="txtsub11" runat="server" ></asp:TextBox>
                                    <ajax:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" TargetControlID="txtsub11"
                                        MinimumPrefixLength="0"
                                        CompletionInterval="50"
                                        CompletionSetCount="50"
                                        FirstRowSelected="true"
                                        CompletionListCssClass="TAccno"
                                        ServicePath="MssplSugg.asmx"
                                        EnableCaching="true" SkinID="None"
                                        ServiceMethod="GetSubject" UseContextKey="false">
                                    </ajax:AutoCompleteExtender>

<%--                                                                <Custom:AutoSuggestBox ID="txtsub11" onblur="this.className='blur'" onfocus="this.className='focus'"
                                                                    runat="server" AutoPostBack="True" BorderWidth="1px" Columns="30" CssClass="FormCtrl"
                                                                    DataType="City" ResourcesDir="asb_includes"></Custom:AutoSuggestBox>--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            <asp:Label ID="Label25" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LSub2 %>"></asp:Label>
                                                            </td>
                                                            <td >
                                                                <asp:TextBox ID="txtsub2" runat="server"></asp:TextBox>
                                    <ajax:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" TargetControlID="txtsub2"
                                        MinimumPrefixLength="0"
                                        CompletionInterval="50"
                                        CompletionSetCount="50"
                                        FirstRowSelected="true"
                                        CompletionListCssClass="TAccno"
                                        ServicePath="MssplSugg.asmx"
                                        EnableCaching="true" SkinID="None"
                                        ServiceMethod="GetSubject" UseContextKey="false">
                                    </ajax:AutoCompleteExtender>

<%--                                                                <Custom:AutoSuggestBox ID="txtsub2" runat="server" BorderWidth="1px" Columns="30"
                                                                    CssClass="FormCtrl" DataType="City" MaxLength="100" ResourcesDir="asb_includes"
                                                                   IncludeMoreMenuItem="False" KeyPressDelay="300" MaxSuggestChars="100"
                                                                    MenuCSSClass="asbMenu" MenuItemCSSClass="asbMenuItem" MoreMenuItemLabel="..."
                                                                    NumMenuItems="10" SelMenuItemCSSClass="asbSelMenuItem" UseIFrame="True"></Custom:AutoSuggestBox>--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                            <asp:Label ID="Label28" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LSub3 %>"></asp:Label>
                                                            </td>
                                                            <td >
                                                                <asp:TextBox ID="txtsub3" runat="server" ></asp:TextBox>
                                    <ajax:AutoCompleteExtender ID="AutoCompleteExtender6" runat="server" TargetControlID="txtsub3"
                                        MinimumPrefixLength="0"
                                        CompletionInterval="50"
                                        CompletionSetCount="50"
                                        FirstRowSelected="true"
                                        CompletionListCssClass="TAccno"
                                        ServicePath="MssplSugg.asmx"
                                        EnableCaching="true" SkinID="None"
                                        ServiceMethod="GetSubject" UseContextKey="false">
                                    </ajax:AutoCompleteExtender>

<%--                                                                <Custom:AutoSuggestBox ID="txtsub3" runat="server" BorderWidth="1px" Columns="30"
                                                                    CssClass="FormCtrl" DataType="City" MaxLength="100" ResourcesDir="asb_includes"
                                                                    IncludeMoreMenuItem="False" KeyPressDelay="300" MaxSuggestChars="100"
                                                                    MenuCSSClass="asbMenu" MenuItemCSSClass="asbMenuItem" MoreMenuItemLabel="..."
                                                                    NumMenuItems="10" SelMenuItemCSSClass="asbSelMenuItem" UseIFrame="True"></Custom:AutoSuggestBox>--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                            <asp:Label ID="Label31" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LLccn %>"></asp:Label>
                                                            </td>
                                                            <td >
                                                                <input id="txtlccn" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                                                                    onfocus="this.className='focus'" type="text" runat="server">
                                                                
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                         
                                                            <td >
                                                                 <asp:Label ID="Label74" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LIsn %>"></asp:Label>
                                                            </td>
                                                            <td >
                                                                 <input id="txtIssnNo" onblur="this.className='blur'" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; "
                                                                runat="server" onfocus="this.className='focus'" type="text">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" style="display:none">
                                                                <font style="background-color: #ffffff"></font>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <table id="Table17" class="table-condensed GenTable1">
                                                                    <tr>
                                                                        <td style="text-align:center">
                                                                            <input id="cmdsave5" class="btnstyle" type="submit" value="<%$ Resources:ValidationResources,bSave %>"
                                                                                runat="server">
                                                                      
                                                                            <input id="cmdReset5"  class="btnstyle" type="submit" value="<%$ Resources:ValidationResources,bReset %>"
                                                                                runat="server">
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>

                                                </asp:Panel>
                                                 </div>
                                                <asp:Panel ID="panel7" runat="server">
                                                     <div class="allgriddivmid" id="dvgrd" runat="server">                                  
                    <asp:HiddenField runat="server" ID="hdnGrdId" />
                                                                    <asp:DataGrid ID="grdcopy" OnItemCommand="grdcopy_ItemCommand" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'
                                                                        runat="server" Width="100%" Height="0px" BorderWidth="0px" BorderStyle="None" CssClass="GenTable1"
                                                                        AutoGenerateColumns="False" CellPadding="0" GridLines="Horizontal" BorderColor="#E7E7FF"
                                                                        BackColor="White">
                                                                        <SelectedItemStyle CssClass="GridSelectedItemStyle"></SelectedItemStyle>
                                                                        <EditItemStyle CssClass="GridEditedItemStyle"></EditItemStyle>
                                                                        <AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
                                                                        <ItemStyle CssClass="GridItem"></ItemStyle>
                                                                        <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                                        <Columns>
                                                                            <asp:BoundColumn DataField="accessionnumber" HeaderText="<%$ Resources:ValidationResources,rptAccNo %>"></asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="DocNo" HeaderText="<%$ Resources:ValidationResources,BillNo %>"></asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="DocDate" HeaderText="<%$ Resources:ValidationResources,GrDocDt %>"
                                                                                DataFormatString="<%$ Resources:ValidationResources, GridDateF %>"></asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="copyno" HeaderText="<%$ Resources:ValidationResources,CpyNo %>"></asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="year" HeaderText="<%$ Resources:ValidationResources,LEditionY %>"></asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="pubyear" HeaderText="<%$ Resources:ValidationResources,GrPubYr %>"></asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="OriginalPrice" HeaderText="<%$ Resources:ValidationResources,LBOrigPrice %>"></asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="bookprice" HeaderText="<%$ Resources:ValidationResources,LPrc %>"></asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="specialprice" HeaderText="<%$ Resources:ValidationResources,LSPrice %>"></asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="catalogdate" HeaderText="<%$ Resources:ValidationResources,GrCatalogD %>"
                                                                                DataFormatString="<%$ Resources:ValidationResources, GridDateF %>"></asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="vendor_source" HeaderText="<%$ Resources:ValidationResources,LCatSource %>"
                                                                                DataFormatString="<%$ Resources:ValidationResources, GridDateF %>"></asp:BoundColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$ Resources:ValidationResources,RBCourse %>">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="cmbCourse" runat="server" Height="30" onblur="this.className='blur'" onfocus="this.className='focus'">
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$ Resources:ValidationResources,ItemCats %>">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="CmbItemType" runat="server" Height="30" onblur="this.className='blur'"
                                                                                        onfocus="this.className='focus'">
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="Item Category">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="DDLItemCategory" Height="30" runat="server" onblur="this.className='blur'"
                                                                                        onfocus="this.className='focus'">
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                               <asp:TemplateColumn HeaderText="Author">
                                                                <ItemTemplate>
                                                        <asp:TextBox runat ="server"  ID="txtauthorgrd" placeholder="Author" Width ="150px" Text='<%# Eval("Author") %>' ></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                                        </Columns>
                                                                        <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C83" BackColor="AliceBlue" Mode="NumericPages"></PagerStyle>
                                                                    </asp:DataGrid>
                                                                </div>
                                                    <div class="no-more-tables">
                                                                <table id="Table12" class="table-condensed GenTable1">
                                                                    <tr>
                                                                        <td colspan="7" style="text-align:center">
                                                                            <span title="Save Catalog Data in Marc 21 also as set in Admin Permission" >Note*</span>
                                                                            <input id="cmdSave7" class="btnstyle" type="submit" value="<%$ Resources:ValidationResources,bSave %>"
                                                                                runat="server">
                                                                       
                                                                            <input id="cmdreset7" class="btnstyle" type="submit" value="<%$ Resources:ValidationResources,bReset %>"
                                                                                runat="server">
                                                        <asp:Button ID="btnRes" Text="Reset2" class="btnstyle" runat="server" />

                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                           </div> 
                                                </asp:Panel>
                                       <div class="no-more-tables">
                                                <asp:Panel ID="panel8" runat="server">
                                                    
                                                    <table id="Table13" class="table-condensed GenTable1">
                                                        <tbody>
                                                            <tr>
                                                                <td colspan="4" ></td>
                                                            </tr>
                                                            <tr>
                                                                <td  colspan="4" style="text-align:center">
                                                                    <asp:Label ID="Label80" runat="server" CssClass="head"  Text="<%$ Resources:ValidationResources,TabTsRltInfo %>"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4" ></td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td ><asp:Label ID="Label19803" runat="server" CssClass="span"  Text="<%$ Resources:ValidationResources,LProCourse %>"></asp:Label>
                                                                </td>
                                                                <td colspan="2" >
                                                                    <input id="txtProgram" runat="server" class="txt10" onblur="this.className='blur'"
                                                                        onfocus="this.className='focus'" type="text" />
                                                                </td>
                                                                <td ></td>
                                                            </tr>
                                                            <tr>
                                                                <td ><asp:Label ID="Label132" runat="server" CssClass="span"  Text="<%$ Resources:ValidationResources,AdvrOrGuide %>"></asp:Label>
                                                                </td>
                                                                <td ></td>
                                                                <td></td>
                                                                <td></td>
                                                            </tr>
                                                            <tr>
                                                                <td ></td>
                                                                <td >
                                                                    <asp:Label ID="Label133" runat="server" CssClass="opt" Text="<%$ Resources:ValidationResources,LFname %>"></asp:Label>
                                                                </td>
                                                                <td >
                                                                    <asp:Label ID="Label134" runat="server" CssClass="opt" Text="<%$ Resources:ValidationResources,LMname %>"></asp:Label>
                                                                </td>
                                                                <td >
                                                                    <asp:Label ID="Label135" runat="server" CssClass="opt" Text="<%$ Resources:ValidationResources,LLname %>"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td >
                                                                    <asp:Label ID="Label136" runat="server" CssClass="span" >1</asp:Label>
                                                                </td>
                                                                <td >
                                                                    <input id="txtaname1" runat="server" class="txt10" onblur="this.className='blur'"
                                                                        onfocus="this.className='focus'" size="13" type="text" />
                                                                </td>
                                                                <td >
                                                                    <input id="txtaname2" runat="server" class="txt10" onblur="this.className='blur'"
                                                                        onfocus="this.className='focus'" size="13" type="text" />
                                                                </td>
                                                                <td >
                                                                    <input id="txtaname3" runat="server" class="txt10" onblur="this.className='blur'"
                                                                        onfocus="this.className='focus'" size="11" type="text" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td >
                                                                    <asp:Label ID="Label137" runat="server" CssClass="span">2</asp:Label>
                                                                </td>
                                                                <td >
                                                                    <input id="txtfname2" runat="server" class="txt10" onblur="this.className='blur'"
                                                                        onfocus="this.className='focus'" size="13" type="text" />
                                                                </td>
                                                                <td >
                                                                    <input id="txtmname2" runat="server" class="txt10" onblur="this.className='blur'"
                                                                        onfocus="this.className='focus'" size="13" type="text" />
                                                                </td>
                                                                <td >
                                                                    <input id="txtlname2" runat="server" class="txt10" onblur="this.className='blur'"
                                                                        onfocus="this.className='focus'" size="11" type="text" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td >
                                                                    <asp:Label ID="Label138" runat="server" CssClass="span">3</asp:Label>
                                                                </td>
                                                                <td >
                                                                    <input id="txtfname3" runat="server" class="txt10" onblur="this.className='blur'"
                                                                        onfocus="this.className='focus'" size="13" type="text" />
                                                                </td>
                                                                <td >
                                                                    <input id="txtmname3" runat="server" class="txt10" onblur="this.className='blur'"
                                                                        onfocus="this.className='focus'" size="13" type="text" />
                                                                </td>
                                                                <td >
                                                                    <input id="txtaname9" runat="server" class="txt10" onblur="this.className='blur'"
                                                                        onfocus="this.className='focus'" size="11" type="text" />
                                                                </td>
                                                            </tr>
                                                          
                                                            <tr>
                                                                <td ><asp:Label ID="Label139" runat="server" CssClass="span" Text="<%$ resources:ValidationResources, Abst %>"></asp:Label>
                                                                </td>
                                                                <td colspan="3" >
                                                                    <textarea id="txtnarration" onfocus="this.className='focus'" onblur="this.className='blur'"
                                                                        runat="server" class="txt10" cols="55"></textarea>
                                                                </td>
                                                            </tr>
                                                           
                                                            <tr>
                                                                <td colspan="4" >
                                                                    <table id="Table14" class="table-condensed GenTable1">
                                                                        <tr>
                                                                            <td  style="text-align:center">
                                                                                <input id="cmdSaveT" runat="server" class="btnstyle"  type="submit"
                                                                                    value="<%$ resources:ValidationResources,bSave %>" />
                                                                          
                                                                                <input id="cmdResetT" runat="server" class="btnstyle"  type="submit"
                                                                                    value="<%$ resources:ValidationResources,bReset %>" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </asp:Panel>
                                             </div>
                                           </asp:Panel>
                          
                                        
                                 
                            <input id="HComboSelect" runat="server" style="width: 127px; height: 9px;" resources= "ValidationResources, TextBox2 %>"
                                            type="hidden" value="<%$ Resources:ValidationResources, ComboSelect %>" />
                            <input id="hdIsMarc21" type="hidden" name="hdIsMarc21" runat="server" />
         <input
                                            id="cmdEditor" runat="server" accesskey="E" causesvalidation="false" style="width: 0px; height: 0px; visibility: hidden" type="submit" value="button" class="btnH" onclick="callEditor('txtSubtitle')" />
                                            <input id="cmdinsertAt" accesskey="Z" onclick="insertAtCursor(document.Form1.txtSTitle);"
                                                style="width: 0px; height: 0px; visibility: hidden" class="btnH" type="submit" />
                                            <input id="btnPub" runat="server" accesskey="P" style="width: 0px; height: 0px; visibility: hidden"
                                                class="btnH" type="submit" onclick="openNewForm('btnFillPub', 'PublisherMaster', 'HNewForm', 'HWhichFill', 'HCondition');"
                                                value="button" /><input id="btnven" runat="server" accesskey="V" onclick="openNewForm('btnFillPub', 'VendorMaster', 'HNewForm', 'HWhichFill', 'HCondition');"
                                                    style="width: 0px; height: 0px; visibility: hidden" type="submit" value="button" class="btnH" />
                   <input id="xCoordHolder" type="hidden" size="1" value="0" name="xCoordHolder" runat="server"><input
                                                id="Hidden5" type="hidden" size="1" name="Hidden2" runat="server"><input id="Hidden4"
                                                    type="hidden" size="1" name="Hidden1" runat="server"><input id="Hidden1" type="hidden"
                                                        size="1" name="Hidden1" runat="server"><input id="yCoordHolder" type="hidden" size="1"
                                                            value="0" name="yCoordHolder" runat="server"><input id="hdForMesage" type="hidden"
                                                                size="1" name="hdForMesage" runat="server">
                            <%--<asp:RequiredFieldValidator ID="Requiredfieldvalidator3"
                                                                    runat="server" Width="144px" ErrorMessage="<%$ Resources:ValidationResources,SpCtlgDt%>"
                                                                    ControlToValidate="txtdate" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator><asp:RequiredFieldValidator
                                                                        ID="rvdtTitle" runat="server" ControlToValidate="txtSTitle" Display="None" ErrorMessage="<%$ Resources:ValidationResources,IvTitle%>"
                                                                        SetFocusOnError="True" Width="144px"></asp:RequiredFieldValidator><asp:RequiredFieldValidator
                                                                            ID="rvdtPrice" runat="server" ControlToValidate="txtbookprice" Display="None"
                                                                            ErrorMessage="<%$ Resources:ValidationResources,EBkPrc%>" Width="144px" SetFocusOnError="True"></asp:RequiredFieldValidator>--%><asp:RegularExpressionValidator
                                                                                ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtacc" Display="None"
                                                                                ErrorMessage="Accession Number should contain only numeric digits and alphabets"
                                                                                ValidationExpression="[A-Z]*[0-9]+[A-Z]*"></asp:RegularExpressionValidator><%--<asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtacc" Display="None"
                                      Width="144px" SetFocusOnError="True"></asp:RequiredFieldValidator>--%><%--<asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="CopyNoValidation"
                                    ControlToValidate="txtCopyNo" Display="None" ErrorMessage="<%$ Resources:ValidationResources,CNoGrtT1%>"
                                    Font-Size="11px" SetFocusOnError="True" Width="136px"></asp:CustomValidator>--%>
                            <%--<asp:CustomValidator
                                        ID="cvdtCategory" runat="server" ClientValidationFunction="comboValidation" ControlToValidate="cmbbookcategory"
                                        Display="None" ErrorMessage="<%$ Resources:ValidationResources,SelItemCat%>"
                                        Font-Size="11px" Width="136px" SetFocusOnError="True"></asp:CustomValidator>
                            --%><asp:Label
                                            ID="lblTemp" runat="server" CssClass="err" Width="16px" Visible="False"></asp:Label>
                            <%--<asp:CustomValidator
                                                ID="cvdtDept" runat="server" ClientValidationFunction="comboValidation" ControlToValidate="cmbdept"
                                                Display="None" ErrorMessage="<%$ Resources:ValidationResources,IvDep%>" Font-Size="11px"
                                                Width="136px" SetFocusOnError="True"></asp:CustomValidator>
                            --%><%--<asp:CustomValidator ID="cvdtMtype"
                                                    runat="server" ClientValidationFunction="comboValidation" ControlToValidate="cmbMediaType"
                                                    Display="None" ErrorMessage="<%$ Resources:ValidationResources,SpMedTyp%>" Font-Size="11px"
                                                    SetFocusOnError="True" Width="136px"></asp:CustomValidator>--%><%--<asp:CustomValidator ID="cvdtLanguage"
                                                        runat="server" ClientValidationFunction="comboValidation" ControlToValidate="cmbLanguage"
                                                        Display="None" ErrorMessage="<%$ Resources:ValidationResources,IvLang%>" Font-Size="11px"
                                                        Width="136px" SetFocusOnError="True"></asp:CustomValidator>--%>

                 <asp:ValidationSummary
                                                        ID="ValidationSummary3" runat="server" DisplayMode="List" ShowSummary="False"
                                                        ShowMessageBox="True"></asp:ValidationSummary>
                                               <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtcmbpublisher"
                                                    Display="None" ErrorMessage="<%$ Resources:ValidationResources,IvPub%>" SetFocusOnError="True"
                                                    Width="75px"></asp:RequiredFieldValidator>--%><input id="hdCtrlNo" runat="server" name="hdCtrlNo"
                                                        style="width: 20px" type="hidden" /><input id="hdCheck" runat="server" style="width: 16px"
                                                            type="hidden" /><input id="hdctrlStatus" runat="server" name="hdctrlStatus" style="width: 21px"
                                                                type="hidden" /><input id="hrDate" runat="server" style="<%$ Resources: ValidationResources, TextBox2 %>; width: 23px;"
                                                                    type="hidden" value="<%$ Resources:ValidationResources, dateFormat1 %>" /><input
                                                                        id="js1" runat="server" style="<%$ Resources: ValidationResources, TextBox2 %>; width: 46px;"
                                                                        type="hidden" value="<%$ Resources:ValidationResources, js1 %>" />
                                                <input id="hdCulture" runat="server" style="width: 16px" type="hidden" /><input id="btnEditor1"
                                                    runat="server" style="width: 0px; height: 0px;" class="btnH" type="submit" accesskey="S" /><input
                                                        id="hIdentifyTxt" runat="server" style="width: 22px" type="hidden" /><input id="hdPublisherId"
                                                            runat="server" style="width: 17px" type="hidden" />
                          <%--  <CR:CrystalReportViewer ID="CrystalReportViewer1"
                                                                runat="server" AutoDataBind="true" />--%>
                                                <input id="hdDsrno" runat="server" type="hidden" style="width: 16px" /><input id="hdCatalogCard"
                                                    runat="server" type="hidden" style="width: 32px" /><input id="HDfirstnameN" runat="server"
                                                        type="hidden" style="width: 11px" /><input id="Hidden7" runat="server" type="hidden"
                                                            style="width: 31px" /><input id="Hidden2" runat="server" type="hidden" style="width: 13px" /><input
                                                                id="btncurrency" runat="server" accesskey="C" onclick="openNewForm('btnFillPub', 'ExchangeMaster', 'HNewForm', 'HWhichFill', 'HCondition');"
                                                                style="width: 8px; height: 1px" type="submit" value="button" class="btnH" /><input
                                                                    id="btnFillPub" runat="server" style="width: 1px; height: 1px;" type="submit"
                                                                    class="btnH" value="button" causesvalidation="false" /><input id="HCondition" runat="server"
                                                                        style="width: 13px" type="hidden" /><input id="HWhichFill" runat="server" style="width: 21px"
                                                                            type="hidden" />&nbsp;<input id="HNewForm" runat="server" style="width: 18px" type="hidden" /><input
                                                                                id="HdVendorid" runat="server" style="width: 19px" type="hidden" />
                                                <input id="Hdvenid" runat="server" style="width: 16px" type="hidden" />&nbsp;&nbsp;<input
                                                    id="cmdClearSession" runat="server" causesvalidation="false" type="submit" value="button"
                                                    style="visibility: hidden" />&nbsp;&nbsp;<input id="BtnDAddCopy" runat="server" type="submit"
                                                        style="visibility: hidden" value="button" /><input id="hdOrgCurrency" runat="server"
                                                            type="hidden" style="width: 31px" />&nbsp;&nbsp;&nbsp;&nbsp;<input id="HdPubCode"
                                                                runat="server" type="hidden" style="width: 34px" />&nbsp;&nbsp;<input id="F1" runat="server"
                                                                    style="width: 31px" type="hidden" />&nbsp;&nbsp;<input id="M1" runat="server" style="width: 32px"
                                                                        type="hidden" /><input id="L1" runat="server" style="width: 23px" type="hidden" />
                                                <input id="F2" runat="server" style="width: 17px"
                                                    type="hidden" />&nbsp;&nbsp;<input id="M2" runat="server" style="width: 17px" type="hidden" /><input
                                                        id="L2" runat="server" style="width: 34px" type="hidden" />&nbsp;&nbsp;<input id="F3" runat="server"
                                                            style="width: 26px" type="hidden" />&nbsp;&nbsp;&nbsp;&nbsp;<input id="M3" runat="server" style="width: 28px"
                                                                type="hidden" /><input id="L3" runat="server" style="width: 25px" type="hidden" /><input
                                                                    id="HDAuthorType" runat="server" type="hidden" />

                        </ContentTemplate>
                        <%-- <Triggers >
                        
                   <asp:AsyncPostBackTrigger ControlID ="LinkButton1" EventName ="Click" />
                        <asp:AsyncPostBackTrigger ControlID ="LinkButton2" EventName ="Click" />
                        <asp:AsyncPostBackTrigger ControlID ="LinkButton3" EventName ="Click" />
                        <asp:AsyncPostBackTrigger ControlID ="LinkButton4" EventName ="Click" />
                        <asp:AsyncPostBackTrigger ControlID ="LinkButton5" EventName ="Click" />
                        <asp:AsyncPostBackTrigger ControlID ="LinkButton6" EventName ="Click" />
                        <asp:AsyncPostBackTrigger ControlID ="LinkButton7" EventName ="Click" />
                        <asp:AsyncPostBackTrigger ControlID ="LinkButton8" EventName ="Click" />
                    </Triggers>--%>
                    </asp:UpdatePanel>
               </div>
           </ContentTemplate>
          </asp:UpdatePanel>                 
<script type="text/javascript">
    //On Page Load.
    $(function () {
        ForDataTable();
    });

    //On UpdatePanel Refresh.
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

            //$('<thead></thead>').prependTo('#' + grdId).append($('#' + grdId + ' tr:first'));
            try {
                let grd = $("[id$='MainContent_DataGrid1']");
                let leng = grd.find('tr').length;
                if (leng >0) {
                    $("#MainContent_DataGrid1 tbody").before("<thead><tr></tr></thead>");
                    var cols = $("#MainContent_DataGrid1 tbody tr:first td");
                    let colsh = '';
                    for (let x = 0; x < cols.length; x++) {
                        colsh += '<th>' + $(cols[x]).text() + '</th>';
                    }
                    $("#MainContent_DataGrid1 thead tr").append(colsh);
                    //$("#MainContent_DataGrid1 tbody tr:first").remove();
                    $("#MainContent_DataGrid1").DataTable();
                }
            } catch (er) {
                alert(er + '; Make sure grid has data');
            }

        }
        catch (err) {
        }
    }


</script>
</asp:Content>