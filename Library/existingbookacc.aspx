<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/LibraryMain.Master"  CodeBehind="existingbookacc.aspx.cs" Inherits="Library.existingbookacc" %>

<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
         <style type="text/css">
            		    .PubVend
		    {
            width:350px !important;
            font-size:13px;
            max-height:270px !important;    
            margin:0;
            overflow:auto;
            padding:1px;  
            border:2px solid black;
        }
   		    .PubClas
		    {
            width:350px !important;
            font-size:13px;
            max-height:270px !important;    
            margin:0;
            overflow:auto;
            padding:1px;  
            border:2px solid black;
        }

        </style>

<script  type="text/javascript">
    function showPublSearch() {
        let oppbl = $('[id$=hdFindPbl]');
        if (oppbl.val() != '') {
            openPbl();
        }
    }
    function closPbl() {
        $('#dvpubl').css('display', 'none');

    }
    function openPbl() {
        $('#dvpubl').css('display', 'block');
    }
    function readPubl(sender, arg) {
        
        document.getElementById("<%=hdPublisherId.ClientID%>").value = arg.get_value();
    }

    function onTextBoxUpdate2(evt) {

        var textBoxID = evt.source.textBoxID;
        if (evt.selMenuItem != null) {
            document.getElementById(textBoxID).value = evt.selMenuItem.label; // + " (modified by onTextboxUpdate)";
            document.getElementById("<%=cmdsearch.ClientID%>").click();
        }
        evt.preventDefault();
    }
    function PrepareMARC_21(ctrlno) {
        var ctrl_no = ctrlno;//document.getElementById('hdCtrlNo').value;
        var IsMarc21 = document.getElementById("<%=hdIsMarc21.ClientID%>").value;

        if (ctrl_no != '' && IsMarc21 == 'Y') {
            CallMarcWebMethod('ctrl_no:' + ctrl_no);
        }
        else { return false; }
    }
    function callEditor(ctrlId) {
        document.getElementById.hIdentifyTxt.value = ctrlId; //non existence fields
        document.Form1.btnEditor1.click();

    }
    function clientback(result, context) {
        if (result != "") {
            var values = result.split('|');
            document.getElementById(context).value = values[0];
            if (values[1] != "") {
                document.getElementById("HCurrName").value = "";
                document.getElementById("HCurrName").value = values[1];
                document.getElementById("Label35").innerText = "<asp:Literal runat='server' Text='<%$ Resources:ValidationResources,LPricePCpy %>' />" + "(" + values[1] + ")";
            } else {
                document.getElementById("Label35").innerText = "Price/Copy";
            }
        } else {
            document.getElementById("HCurrName").value = "";
            document.getElementById(context).value = result;
            document.getElementById("Label35").innerText = "Price/Copy";
            document.getElementById("txtForeignPrice").value = "";
            document.getElementById("txtprice").value = "";
        }
        var Price;
        if (document.Form1.txtExchangeRate.value == "")
            return false;
        else if (document.Form1.txtForeignPrice.value == "")
            return false;
        else {
            Price = ((document.Form1.txtExchangeRate.value) * (document.Form1.txtForeignPrice.value));
            if (Price.toFixed)
                document.Form1.txtprice.value = Price.toFixed(2);
            else
                document.Form1.txtprice.value = Price;
        }
    }


    function msgrecordsaved(ctrlno) {
        PrepareMARC_21(ctrlno);
        alert("<asp:Literal runat='server' Text='<%$ Resources:ValidationResources,recsave %>' />");
    }

            //+++++++++++++++++++Binds function "starttime" on page load+++++++++++++++++++++ 
</script>		
    <script  type="text/javascript">
                     function GetServer(getCtrlId, ctrlTo) {
                         var value = document.getElementById(getCtrlId).value;
                         UseCallBack(value, ctrlTo);
                     }
    </script>		
    <script  type="text/javascript">
        function txtForeignPrice_OnPropertyChange() {
            var Price;
            if (document.getElementById("<%=txtExchangeRate.ClientID%>").value == "")
            return false;
        else if (document.getElementById("<%=txtForeignPrice.ClientID%>").value == "")
            return false;
        else {
            Price = ((document.getElementById("<%=txtExchangeRate.ClientID%>").value) * (document.getElementById("<%=txtForeignPrice.ClientID%>").value));
            if (Price.toFixed)
                document.getElementById("<%=txtprice.ClientID%>").value = Price.toFixed(2);
            else
                document.getElementById("<%=txtprice.ClientID%>").value = Price;
        }
    }

    function txtExchangeRate_OnPropertyChange() {
        var Price;
        if (document.getElementById("<%=txtExchangeRate.ClientID%>").value == "")
            return false;
        else if (document.getElementById("<%=txtForeignPrice.ClientID%>").value == "")
            return false;
        else {
            Price = ((document.getElementById("<%=txtExchangeRate.ClientID%>").value) * (document.getElementById("<%=txtForeignPrice.ClientID%>").value));
            if (Price.toFixed)
                document.getElementById("<%=txtprice.ClientID%>").value = Price.toFixed(2);
            else
                document.getElementById("<%=txtprice.ClientID%>").value = Price;
            }
        }

        $(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(MastCall);

        })
        function MastCall() {
            var al = 0;
            $(this).on("keydown", function (e) {
                if ((e.altKey) && (e.keyCode == 80)) {
                    //  CallPub();
                    CallPub2();
                    return false;
                }
                if ((e.altKey) && (e.keyCode == 68)) {
                    openNewForm('btnFillPub', 'DepartmentMaster', 'HNewForm', 'HWhichFill', 'HCondition');
                    return false;
                }
                if ((e.altKey) && (e.keyCode == 67)) {
                    //alert("C");
                    document.getElementById("HyperLink1").click();
                    return false;
                }
                if ((e.altKey) && (e.keyCode == 86)) {
                    document.getElementById("hyper68").click();
                    return false;
                }

            });
            $(this).on("keyup", function (e) {
                al = 0;
                if (e.altKey)
                    $("[id$='txtAl']").val("0");
            });
        }
        $(document).ready(MastCall());


        function selVend(sender, arg) {
            $('[id$=HdVendorid]').val(arg.get_value());
        }
        function CallPub2() {
            let hdid = $('[id$=hdPublisherId]').attr('id');
            let txt = $('[id$=txtCmbPublisher]').attr('id');
            window.open("PublisherMaster.aspx?title=From Catalog&caller=child&hdid=" + hdid + "&text=" + txt, "Publisher Master", "height=700px,width=800px");

        }
        //

        //
        function callVend() {
            let hdid = $('[id$=HdVendorid]').attr('id');
            let txt = $('[id$=txtCmbVendor]').attr('id');
            window.open("VendorMaster.aspx?title=From Catalog&caller=child&hdid=" + hdid + "&text=" + txt, "Vendor Master", "height=700px,width=800px");

        }
        function GetTitle(sender, arg) {
            let id = arg.get_value();
            $('[id$=stite]').val(id);
            //$('[id$=cmdcheck]').click();
        }
    </script>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
       <asp:UpdateProgress ID="UpPorg1" runat="server">
<ProgressTemplate>
<NN:Mak ID="FF1" runat="server" />
</ProgressTemplate>
</asp:UpdateProgress>
        <asp:UpdatePanel ID="upsdas" runat="server">
            <ContentTemplate>

                       <div class="container tableborderst">                    
              <div style="width:100%;display:none" class="title">
                  <div style="width:89%;float:left"> &nbsp;
		
			<INPUT id="Hdexistaccession" type="hidden" size="1" name="Hidden7" runat="server"> 
			<asp:label id="lblTitle" style="display:none"  runat="server" Width="100%">Accessioning(Non-Ordered Books)</asp:label>
                      </div>
                   <div style="float:right;vertical-align:top">
                         <a id="lnkHelp" style="display:none"  href="#" onclick="ShowHelp('Help/Acqexistingbookacc.htm')"><img height="15" alt="Help" src="help.jpg" /></a>
                      </div></div>

                      
                             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="no-more-tables" style="width:100%">
						<TABLE id="Table4" class="table-condensed GenTable1" style="width:100%">
                              
							<TR>
								<TD  colSpan="4"><asp:label id="msglabel" runat="server" CssClass="err" ></asp:label>
                                    <input id="Button10" runat="server"  style="width: 0px; height: 0px;visibility:hidden "
                                                type="button" onclick="openNewForm('btnFillPub', 'PublisherMaster', 'HNewForm', 'HWhichFill', 'HCondition');"
                                                value="button" /><input id="Button11" runat="server" accesskey="V" onclick="openNewForm('btnFillPub', 'VendorMaster', 'HNewForm', 'HWhichFill', 'HCondition');"
                                                    style="width: 0px; height: 0px; visibility: hidden" type="button" value="button" class="btnH" />
                                    <br />
                                    <asp:LinkButton ID="lnkContinue" runat="server" CausesValidation="False" ForeColor="Green"  OnClick="lnkContinue_Click"
                                        Visible="False"  Text="<%$ Resources:ValidationResources,LBConSamRec %>" CssClass="note" Font-Names="Verdana" ></asp:LinkButton>
								 
                                    <asp:LinkButton ID="lnkModify" runat="server" CausesValidation="False" CssClass="opt"
                                        ForeColor="Green" Visible="False"  Text="<%$ Resources:ValidationResources,LBConSamMCRec %>" Font-Names="Verdana" ></asp:LinkButton> 
                                 
                            
                                    <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CssClass="opt" OnClick="lnkDelete_Click"
                                        Font-Names="Verdana"  ForeColor="Green" Text="<%$ Resources:ValidationResources,LBConDelRec %>"
                                        Visible="False" ></asp:LinkButton></td>
                                    
                            </tr>
							<TR>
								<TD style="width:16%"><asp:label id="Label34" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,rptBillNo %>"></asp:label></TD>
								<TD ><INPUT class="txt10" id="txtkeywords" onblur="this.className='blur'" 
										onfocus="this.className='focus'" type="text" maxLength="200" size="18" name="txtkeywords" runat="server"></TD>
								<TD style="width:18%"><asp:label id="Label27" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,BillD %>"></asp:label></TD>
								<TD style="width:32%">
                                    
                                    
                                    
<%--pushpendra singh--%>
 <asp:TextBox ID="txtAccDate" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
 <%--<ajax:CalendarExtender ID="CalendarExtender1" TargetControlID="txtAccDate" Format="dd/MMM/yyyy" runat="server"></ajax:CalendarExtender>--%> 
 <%--pushpendra singh--%>
                                    
                                  <%--  <INPUT class="txt10" id="txtAccDate" onblur="this.className='blur';checkdate1(this,document.Form1.hdCulture.value,document.Form1.hrDate.value)" style='<%$ Resources:ValidationResources, TextBox2 %>'
										onfocus="this.className='focus'" type="text" size="10" name="txtAccDate" runat="server"><input id="btnDate" type="button" onclick="pickDate('txtAccDate');" style="background-position: center center; background-image: url(cal.gif); width: 27px; background-color: black; height: 21px;" />--%>

									</TD>
                                 
							</TR>
							<TR>
								<TD >
                                    <asp:Label ID="HyperLink2" runat="server" Text="<%$ Resources:ValidationResources,department %>" ></asp:Label>
                                   <%-- <asp:HyperLink ID="HyperLink2" runat="server" Text="<%$ Resources:ValidationResources,department %>"
                                                        onclick="openNewForm('btnFillPub','DepartmentMaster','HNewForm','HWhichFill','HCondition');"></asp:HyperLink>--%></TD>
								<TD colSpan="3" >
									<asp:dropdownlist id="cmbdept" onblur="this.className='blur'" onfocus="this.className='focus'" runat="server"
									 CssClass="txt10" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></asp:dropdownlist>
									 
									<asp:label id="Label16" runat="server" Font-Size="11px"  CssClass="star" >*</asp:label></td>
							     
							</TR>
							<TR>
								<TD ><asp:label id="Label1" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,BkTtl %>"></asp:label></TD>
								<TD colSpan="3"> 
                                    <asp:TextBox ID="txtSTitle" runat="server" CssClass="txt10" ></asp:TextBox>
                                     <ajax:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtSTitle"
                                      MinimumPrefixLength="0"
                                      CompletionInterval="350"   
                                      CompletionSetCount="50"
                                      FirstRowSelected="true"
                                      CompletionListCssClass="PubVend"
                                      ServicePath="MssplSugg.asmx"
                                      OnClientItemSelected="GetTitle"
                                      ServiceMethod="STitle" >
                                 </ajax:AutoCompleteExtender>
                                    <input id="stite" runat="server" style="width: 26px" type="hidden" />
    
<%--                                    <Custom:AutoSuggestBox ID="txtSTitle" runat="server" AutoPostBack="false" 
                                        BorderWidth="1px" Columns="30" CssClass="FormCtrl" DataType="City" 
                                        Font-Names="<%$ Resources:ValidationResources, TextBox1 %>" 
                                        MaxLength="100" ResourcesDir="asb_includes" ></Custom:AutoSuggestBox>--%>
                                     
									<asp:Image ID="Image3" runat="server" ImageUrl="~/Images/sugg.png" />
                                 
                                    <asp:Label ID="Label32" runat="server" CssClass="star" Font-Size="11px" 
                                      >*</asp:Label>
                                </td>
							</TR>
							<TR >
								<TD><asp:label id="txtsubtitle" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LSTitle %>"></asp:label></TD>
								<TD><INPUT class="txt10" id="txtSubbooktitle" onblur="this.className='blur'"  
										onfocus="this.className='focus'" type="text" name="txtSubbooktitle" runat="server" >&nbsp;</TD>
                                <td>
                                     <asp:label id="lItem_type" runat="server" CssClass="span" Text="Item Type"></asp:label>                                   
                                </td>
							    <td>
                                <asp:DropDownList ID="ddIType" runat="server" CssClass="txt10" ></asp:DropDownList>   
                                </td>
							</TR>
							<TR>
								<TD ><asp:label id="Label26" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LSName %>"></asp:label></TD>
								<TD colSpan="3"><INPUT class="txt10" id="txtseriesname" onblur="this.className='blur'"  
										onfocus="this.className='focus'" type="text" name="txtseriesname" runat="server" >
                                     
                                    <INPUT id="hdvol" type="hidden" name="Hidden1"
										runat="server"></td>
							    
							</TR>
                            <tr>
                                <td >
                                    <asp:HyperLink ID="Hyperlink22" runat="server" Text="<%$ Resources:ValidationResources,LPubli %>"
                                                        onclick="CallPub2();"></asp:HyperLink></td>
                                <td colspan="3">
                                   <asp:TextBox  ID="txtCmbPublisher" runat="server" BorderWidth="1px"
                                                      Columns="30"   ></asp:TextBox> <span style="font-size:16px;" onclick="openPbl()" class="bi bi-folder2-open"></span>
                                         
     <%-- <ajax:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" TargetControlID="txtCmbPublisher"
     MinimumPrefixLength="0"
     CompletionInterval="10"
           CompletionSetCount="50" 
     FirstRowSelected="true"
     ServicePath="MssplSugg.asmx"
       OnClientItemSelected="readPubl"                   
     ServiceMethod="GetPubl" >
</ajax:AutoCompleteExtender>--%>
                                    <div id="dvpubl" style="display:none;width:380px; height:350px; background-color:white; position:absolute;top:20%; left:30%; border:1px solid grey; padding:5px 6px;">
                                        <asp:HiddenField ID="hdFindPbl" runat="server" />
                                        <i onclick="closPbl()" style="cursor:pointer; color:red; font-size:18px;" class="bi bi-x-circle"></i>
                                        <asp:TextBox ID="txfindPbl" runat="server" Width="200" ></asp:TextBox> <asp:Button ID="btnfindPbl" runat="server" CssClass="btn btn-secondary btn-sm" Text="Find" OnClick="btnfindPbl_Click" />
                                        <div style="max-height:95%; overflow:auto;">
                                            <asp:GridView ID="grdPubl" runat="server" AutoGenerateColumns="false" ShowHeader="false">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Label ID="labpbl" runat="server" Text='<%# Eval("publisher") %>'></asp:Label>
                                                            <asp:HiddenField ID="hdpublid" runat="server"  Value='<%# Eval("publisherid") %>' />
                                                            
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnselit" runat="server" Text="Sel" OnClick="btnselit_Click" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <input id="hdPublisherId" runat="server"  type="hidden" />
    
                                    
                               
                                    <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/sugg.png" />
                                     
                                    <asp:Label ID="Label29" runat="server" CssClass="star">*</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:label id="Label24" runat="server" CssClass="span" Text="<%$Resources:ValidationResources,LMediaT %>"></asp:label></td>
                                <td>
                                    <asp:dropdownlist id="cmbmediatype" onblur="this.className='blur'" onfocus="this.className='focus'"
										runat="server"  CssClass="txt10"  Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'>
										<asp:ListItem Value="Print" Text="<%$Resources:ValidationResources,iprint %>"> </asp:ListItem>
										<asp:ListItem Value="CD/DVD" Text="<%$Resources:ValidationResources,LCDDVD %>"></asp:ListItem>
										<asp:ListItem Value="OnLine" Text="<%$Resources:ValidationResources,OnLine %>"></asp:ListItem>
										<asp:ListItem Value="MicroForm" Text="<%$Resources:ValidationResources,McroForm %>"></asp:ListItem>
									</asp:dropdownlist>
                                    <asp:label id="Label30" runat="server" Font-Size="11px" CssClass="star" >*</asp:label></td>
                                <td >
                                    <asp:Label ID="Label39" runat="server" Text="<%$Resources:ValidationResources,LLangu%>"></asp:Label></td>
                                <td >
                                    <asp:DropDownList
                                        ID="cmbLanguage" runat="server" CssClass="txt10" 
                                       Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'>
                                    </asp:DropDownList>
                                     
                                    <asp:Label ID="Label33" runat="server" CssClass="star" Font-Size="11px" 
                                      >*</asp:Label></td>
                                   
                                     
                            </tr>
							<TR>
								<TD ><asp:label id="fromvolume" runat="server" CssClass="span"  Text="<%$ Resources:ValidationResources,LVolume %>"></asp:label></TD>
								<TD><INPUT class="txt10" id="txtvolumeno" onblur="this.className='blur'"
										
										onfocus="this.className='focus'" type="text" size="8" name="txtvolumeno" runat="server"></TD>
								<TD ><asp:label id="tovolume" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LPart %>"></asp:label></TD>
								<TD ><INPUT class="txt10"  id="txttovlume" onblur="this.className='blur'"
										
										onfocus="this.className='focus'" type="text" name="txttovlume" runat="server"></TD>
                                 
							</TR>
							<TR>
								<TD ><asp:label id="Label13" runat="server" CssClass="span"  Text="<%$ Resources:ValidationResources,LISSN %>"></asp:label></TD>
								<TD ><INPUT class="txt10" id="txtisbn" onblur="this.className='blur'" 
										onfocus="this.className='focus'" type="text" size="13" name="txtisbn" runat="server"></TD>
								<TD>
									<asp:label id="Label7" runat="server" CssClass="span" text="<%$ Resources:ValidationResources,LEdition %>"></asp:label></TD>
								<TD ><INPUT class="txt10" id="txtedition" onblur="this.className='blur'" 
										onfocus="this.className='focus'" type="text" name="txtedition" runat="server"></TD>
                                
							</TR>
							<TR>
								<TD ><asp:label id="Label8" runat="server"  CssClass="span" text="<%$ Resources:ValidationResources,LEditionY%>">Edition  Year</asp:label></TD>
								<TD ><INPUT class="txt10" onkeypress="return IntegerNumber(this)" id="txteditionyear" onblur="this.className='blur'"
										
										onfocus="this.className='focus'" type="text" maxLength="4" size="9" name="txteditionyear" runat="server"></TD>
								<TD>
									<asp:label id="Label38" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LPubY %>">Publication  Year</asp:label></TD>
								<TD ><input class="txt10" onkeypress="return IntegerNumber(this)" id="txtPubYear" onblur="this.className='blur'"
									
										onfocus="this.className='focus'" type="text" maxLength="4" name="txtPubYear"
										runat="server"/></TD>
                                
							</TR>
							<TR>
								<TD ><asp:label id="Label10" runat="server" CssClass="span" text="<%$ Resources:ValidationResources,LCate %>"></asp:label></TD>
								<TD ><asp:dropdownlist id="cmbcategory" onblur="this.className='blur'" onfocus="this.className='focus'"
										runat="server"  CssClass="txt10"  Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'>
									</asp:dropdownlist>
                                    <asp:label id="Label23" runat="server" Font-Size="11px" CssClass="star" >*</asp:label>
								</TD>
								<TD ><asp:label id="Label9" runat="server" CssClass="span"  text="<%$ Resources:ValidationResources,Lform %>"></asp:label></TD>
								<TD ><asp:dropdownlist id="cmbform" onblur="this.className='blur'" onfocus="this.className='focus'" runat="server"
										  Font-Names='<%$ Resources:ValidationResources, TextBox1 %>' CssClass="txt10">
                                    <asp:ListItem Value="<%$ Resources:ValidationResources,LISftBound %>" text="<%$ Resources:ValidationResources,LISftBound %>" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="<%$ Resources:ValidationResources,LIHrdBound %>" text="<%$ Resources:ValidationResources,LIHrdBound %>"></asp:ListItem>
                                    <asp:ListItem Value="<%$ Resources:ValidationResources,LIPBK %>" Text="<%$ Resources:ValidationResources,LIPBK %>"></asp:ListItem>
                                    <asp:ListItem Value="<%$ Resources:ValidationResources,Otr %>" text="<%$ Resources:ValidationResources,Otr %>"></asp:ListItem>
									
									</asp:dropdownlist></TD>
                                 
							</TR>
                            <tr>
                                <td >
                                   <asp:Label ID="Label20" runat="server" CssClass="span" text="<%$ Resources:ValidationResources,LNoOfPages %>"></asp:Label></td>
                                <td >
                                    <input id="txtcopy"   class="txt10"  onblur="this.className='blur'"
										
										onfocus="this.className='focus'" onkeypress="IntegerNumber(this)" type="text" maxLength="10" size="8" name="txtpagesize" runat="server"></td>
                                <td >
                                    <asp:Label ID="Label25" runat="server" CssClass="span"  text="<%$ Resources:ValidationResources,LSize %>"></asp:Label></td>
                                <td >
                                    <INPUT id="Txtpagesize"  class="txt10"  onblur="this.className='blur'"
										
										onfocus="this.className='focus'" type="text"  size="9" name="txtpagesize" runat="server"></td>
                                
                            </tr>
							<tr>
								<TD colSpan="4">
                                    <table style="width:100%">
                                        <tr>
                                            <td class="col-md-4" style="margin:0px;padding:0px">
                                    <asp:label id="Label17" runat="server" CssClass="span"  text="<%$ Resources:ValidationResources,LStatementofres %>"></asp:label></TD>

                                            </td>
                               
                                            
                                        </tr>
                                    </table>
								
							     
							</TR>
							 <tr>
                                  <TD colspan="2"><asp:dropdownlist id="cmbpersontype" onblur="this.className='blur'" onfocus="this.className='focus'" 
										runat="server" CssClass="txt10"  Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'>
										<asp:ListItem Value="Author" text="<%$ Resources:ValidationResources,Auth %>"></asp:ListItem>
										<asp:ListItem Value="Compiler" text="<%$ Resources:ValidationResources,LComp %>"></asp:ListItem>
										<asp:ListItem Value="Editor" text="<%$ Resources:ValidationResources,LEditor %>"></asp:ListItem>
										<asp:ListItem Value="Illustrator" text="<%$ Resources:ValidationResources,LIllus %>"></asp:ListItem>
										<asp:ListItem Value="Translater" text="<%$ Resources:ValidationResources,LITransltr %>"></asp:ListItem>
									</asp:dropdownlist></TD>
                                 <td colspan="2"></td>
							 </tr>
							<tr>
								<TD ><asp:textbox id="txtcurr_pos" runat="server" Visible="False" ></asp:textbox></TD>
								<TD ><asp:label id="Label18" runat="server" CssClass="opt" text="<%$ Resources:ValidationResources,LFName %>"></asp:label></TD>
								<TD><asp:label id="Label2" runat="server" CssClass="opt"  text="<%$ Resources:ValidationResources,LMName %>"></asp:label></TD>
								<TD ><asp:label id="Label21" runat="server" CssClass="opt" text="<%$ Resources:ValidationResources,LLName %>"></asp:label></TD>
                                 
                                 
							</TR>
							<tr>
								<TD >
									<asp:label id="Label4" runat="server" CssClass="span" text="<%$ Resources:ValidationResources,LI %>"></asp:label></TD>
								<TD ><INPUT class="txt10" id="txtfname1" onblur="this.className='blur'"  
										onfocus="this.className='focus'" type="text" name="txtfname1" runat="server">
									<asp:label id="Label19" runat="server" Font-Size="11px" CssClass="star">*</asp:label></TD>
								<TD ><INPUT class="txt10" id="txtmname1" onblur="this.className='blur'"  
										onfocus="this.className='focus'" type="text" name="txtmname1" runat="server"></TD>
								<TD ><INPUT id="txtlname1" onblur="this.className='blur'" 
										onfocus="this.className='focus'" type="text" name="txtlname1" runat="server" class="txt10"></TD>
                                 
							</TR>
							<tr>
								<TD>
									<asp:label id="Label5" runat="server" CssClass="span" text="<%$ Resources:ValidationResources,LII %>"></asp:label></TD>
								<TD ><INPUT class="txt10" id="txtfname2" onblur="this.className='blur'"  
										onfocus="this.className='focus'" type="text" name="txtfname2" runat="server"></TD>
								<TD ><INPUT class="txt10" id="txtmname2" onblur="this.className='blur'"  
										onfocus="this.className='focus'" type="text" name="txtmname2" runat="server"></TD>
								<TD ><INPUT id="txtlname2" onblur="this.className='blur'" 
										onfocus="this.className='focus'" type="text" name="txtlname2" runat="server" class="txt10"></TD>
                                 
							</TR>
							<tr>
								<TD >
									<asp:label id="Label6" runat="server" CssClass="span" text="<%$ Resources:ValidationResources,LIII %>"></asp:label></TD>
								<TD ><INPUT class="txt10" id="txtfname3" onblur="this.className='blur'"  
										onfocus="this.className='focus'" type="text" name="txtfname3" runat="server"></TD>
								<TD ><INPUT class="txt10" id="txtmname3" onblur="this.className='blur'" 
										onfocus="this.className='focus'" type="text" name="txtmname3" runat="server"></TD>
								<TD ><INPUT class="txt10" id="txtlname3" onblur="this.className='blur'" 
										onfocus="this.className='focus'" type="text" size="11" name="txtlname3" runat="server"></TD>
                                 
							</TR>
							<tr>
								<TD >
                                    <asp:Label ID="HyperLink1" runat="server" Text="<%$ Resources:ValidationResources,LCurrency %>" ></asp:Label>
                                    <%--<asp:HyperLink ID="HyperLink1" runat="server" Text="<%$ Resources:ValidationResources,LCurrency %>"
                                                        onclick="openNewForm('btnFillPub','ExchangeMaster','HNewForm','HWhichFill','HCondition');"></asp:HyperLink>--%></TD>
								<TD ><asp:dropdownlist id="cmbcurr"   onblur="this.className='blur'" onfocus="this.className='focus'" runat="server"
										 CssClass="txt10"  Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></asp:dropdownlist>
                                    <asp:Label ID="Label46" runat="server" CssClass="star" Font-Size="11px"
                                       >*</asp:Label></TD>
								<TD ><asp:label id="Label35" runat="server" text="<%$ Resources:ValidationResources,LPrInForinCurr %>"></asp:label></TD>
								<TD ><INPUT class="txt10" onkeypress="decimalNumber(this);" id="txtForeignPrice" onblur="this.className='blur'"
										
										onpropertychange="txtForeignPrice_OnPropertyChange();" onfocus="this.className='focus'" type="text" maxLength="7" size="9"
										name="txtForeignPrice" runat="server"></TD>
                                
							</TR>
							<tr>
								<TD ><asp:label id="Label74" runat="server" CssClass="span"  text="<%$ Resources:ValidationResources,LExRate %>"></asp:label></TD>
								<TD ><INPUT class="txt10" onkeypress="decimalNumber(this);" id="txtExchangeRate" onblur="this.className='blur'"
										
										onpropertychange="txtExchangeRate_OnPropertyChange();" onfocus="this.className='focus'" type="text" size="8" name="txtExchangeRate"
										runat="server"></TD>
								<TD ><asp:label id="Label15" runat="server" text="<%$ Resources:ValidationResources,LPrice %>"></asp:label></TD>
								<TD ><INPUT class="txt10" onkeypress="decimalNumber(this);" id="txtprice" onblur="this.className='blur'"
										
										onfocus="this.className='focus'" type="text" size="9" name="txtprice" runat="server" maxlength="8">
									<asp:label id="lblForeignprice" runat="server" Font-Size="11px" CssClass="star" 
										>*</asp:label></TD>
                                 
							</TR>
							<tr>
								<TD ><asp:label id="lbl1" runat="server" CssClass="span" text="<%$ Resources:ValidationResources,LNoCopies %>"></asp:label></TD>
								<TD><INPUT onkeypress="IntegerNumber(this)" id="txtnoofcopies" onblur="this.className='blur'"
										
										onfocus="this.className='focus'" type="text" maxLength="4" size="8" name="txtnoofcopies" runat="server" class="txt10">
									<asp:label id="lblexchangerate" runat="server" Font-Size="11px" CssClass="star" 
										>*</asp:label></TD>
								<TD ><asp:label id="Label3" runat="server"  text="<%$ Resources:ValidationResources,LSPrice %>"></asp:label></TD>
								<TD ><INPUT class="txt10" onkeypress="decimalNumber(this);" id="txtspecialprice" onblur="this.className='blur'"
										
										onfocus="this.className='focus'" type="text" size="9" name="txtspecialprice" runat="server" maxlength="8">
									</TD>
                                 
							</TR>
                            <tr>
                                <td >
                                    <asp:HyperLink ID="hyper68" runat="server" Text="<%$ Resources:ValidationResources,LVen %>"
                                               onclick="callVend()"         ></asp:HyperLink>
<%--                                    onclick="openNewForm('btnFillPub','VendorMaster','HNewForm','HWhichFill','HCondition');"--%>
                                </td>
                                <td colspan="3" >
                                        <asp:TextBox  ID="txtCmbVendor" runat="server" BorderWidth="1px"
                                                      Columns="30" ></asp:TextBox> 
                                              <ajax:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtCmbVendor"
          MinimumPrefixLength="0"
          CompletionInterval="20"   
          CompletionSetCount="50"   
          FirstRowSelected="true"
                                                   CompletionListCssClass="PubVend"
          ServicePath="MssplSugg.asmx"
            OnClientItemSelected="selVend"                   
          ServiceMethod="GetVendor" >
     </ajax:AutoCompleteExtender>
                                    
                                                      <%-- <custom:autosuggestmenu  id="asmvendor" runat="server" keypressdelay="1" maxsuggestchars="100" 
                                            ongetsuggestions="TopSearchService.GetSuggestionsVendor" resourcesdir="~/asm_includes"
                                            targetcontrolid="txtCmbVendor" updatetextboxonupdown="false" usepagemethods="false"></custom:autosuggestmenu>--%>
                                      
                                    <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/sugg.png" />
                                    
                                    <asp:Label ID="lblvendor" runat="server" ForeColor="Red"  Text="*" 
                                       ></asp:Label>
                                </td>
                            </tr>
							<tr>
								<TD colSpan="4"><asp:radiobutton id="Rbaccession1" runat="server" CssClass="opt"  AutoPostBack="True" OnCheckedChanged="Rbaccession1_CheckedChanged"
										Text="<%$ Resources:ValidationResources,RBAutoAccNoGenASAPf%>" Checked="True" GroupName="rb"></asp:radiobutton></TD>
                                 
							</TR>
                            <tr>
                                <td colspan="4" >
                                    <asp:RadioButton ID="OptAccWOP" runat="server" AutoPostBack="True" CssClass="opt" OnCheckedChanged="OptAccWOP_CheckedChanged"
                                        GroupName="rb" Text="<%$ Resources:ValidationResources,RBAutoAccNoGenWOAPf%>"  /><br/>
                                 
                                    <asp:RadioButton ID="RBPreSuff" runat="server" AutoPostBack="True" CssClass="opt" OnCheckedChanged="RBPreSuff_CheckedChanged"
                                        GroupName="rb" Text="<%$ Resources:ValidationResources,RBAutoAccNoGenPreSuff%>"
                                       /> <br/>
                             
                                    <asp:RadioButton ID="RBSuffix" runat="server" AutoPostBack="True" CssClass="opt"  OnCheckedChanged="RBSuffix_CheckedChanged"
                                        GroupName="rb" Text="<%$ Resources:ValidationResources,RBAutoAccNoGenSuffix%>"
                                       /><br/>
                                           <asp:radiobutton id="Rbmanual1" runat="server" CssClass="opt"  AutoPostBack="True" OnCheckedChanged="Rbmanual1_CheckedChanged"
										Text="<% $ Resources:ValidationResources,RBManAccNoEntry %>" GroupName="rb"></asp:radiobutton></TD>
                                 
							</TR>
                            <tr>
                                <td  >
                                    <asp:CheckBox ID="chkApplyRange" runat="server" OnCheckedChanged="chkApplyRange_CheckedChanged" AutoPostBack="True" CssClass="opt" Text="<%$ Resources:ValidationResources, applyrange %>" /></td>
                                <td >
                                    <asp:Label ID="lblaccrange" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,AccPfx %>"></asp:Label></td>
                                <td >
                                    <asp:dropdownlist id="cmbaccrange" onblur="this.className='blur'" onfocus="this.className='focus'"
										runat="server"  CssClass="txt10"  Font-Names='<%$ Resources:ValidationResources, TextBox1 %>' AutoPostBack="True">
                                    </asp:DropDownList></td>
                                <td >
                                    <asp:Label ID="Label75" runat="server" CssClass="span" 
                                        Text="<%$ Resources:ValidationResources,lblaccnovalidate %>"></asp:Label>
                                </td>
                                 
                            </tr>
                            <tr>
                                <td colspan="4">
                                  <asp:label id="Label22" runat="server" Visible="False" CssClass="span" ></asp:label></td>
                                 
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <input class="txt10"  id="txtaccession" 
										
										 type="text" size="94" name="txtstartno" runat="server"/></td>
                                 
                            </tr>
                            <tr>
                                <td>
                                    <asp:label id="Label12" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,AccPfx %>"></asp:label></td>
                                <td colspan="2">
                                    <asp:dropdownlist id="cmbaccessionprefix" OnSelectedIndexChanged="cmbaccessionprefix_SelectedIndexChanged" onblur="this.className='blur'" onfocus="this.className='focus'"
										runat="server"  CssClass="txt10" AutoPostBack="True" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'></asp:dropdownlist><asp:label id="Label43" runat="server" Font-Size="11px" CssClass="star" >*</asp:label></td>
                                <td>
                                    <INPUT class="txt10" id="txtstartno" onblur="this.className='blur'" 
										onfocus="this.className='focus'"  type="text" size="8" name="txtstartno" runat="server" visible="false"></td>
                                
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:CheckBox ID="ChkCataloging" runat="server" OnCheckedChanged="ChkCataloging_CheckedChanged" AutoPostBack="True" CssClass="opt"  Text="<% $ Resources:ValidationResources,LBcheck%>" /></td>
                                 
                            </tr>
                            <tr>
                                <td >
                                   <asp:Label ID="Label40" runat="server" CssClass="span" Text="<% $ Resources:ValidationResources,ClasNo %>"
                                      ></asp:Label></td>
                                <td >
                                    <INPUT class="txt10"  id="txtClass" onblur="this.className='blur'"
										
										onfocus="this.className='focus'" type="text"
										runat="server"></td>
                                <td >
                                   
                                    <asp:Label ID="Label41" runat="server" Text="<%$ Resources:ValidationResources,BookNo %>"
                                       ></asp:Label></td>
                                <td >
                                    <INPUT class="txt10" id="txtBook" onblur="this.className='blur'"
										
										onfocus="this.className='focus'" type="text" size="9" name="txtprice" runat="server" maxlength="8"></td>
                                
                            </tr>
                            <tr>
                                <td >
                                   <asp:Label ID="Label47" runat="server" CssClass="span" Text="<% $ Resources:ValidationResources,LItemSt %>"
                                      ></asp:Label></td>
                                <td >
                                    <asp:dropdownlist id="cmbStatus" OnSelectedIndexChanged="cmbStatus_SelectedIndexChanged"  onchange="GetServer(this.id,'txtExchangeRate')" onblur="this.className='blur'" onfocus="this.className='focus'" runat="server"
										 CssClass="txt10" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>' AutoPostBack="True">
                                    </asp:DropDownList></td>
                                <td> </td>
                                <td> </td>
                                 
                            </tr>
                            <tr>
                                <td >
                                   <asp:Label ID="Label48" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LBaDa %>"
                                       ></asp:Label></td>
                                <td colspan="3" >
                                    <asp:TextBox ID="txtrelease" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                        CssClass="txt10"  onblur="this.className='blur';checkdate1(this,document.Form1.hdCulture.value,document.Form1.hrDate.value)"
                                        onfocus="this.className='focus'" 
                                        Visible="False" ></asp:TextBox>
                                    <input id="Button2" runat="server"  onclick="pickDate('txtrelease');"
                                        style="background-position: center center; background-image:url(cal.gif); 
                                         background-color: black" type="button" /></td>
                              
                         
                            <tr>
                                <td colspan="4">
                                   <asp:CheckBox ID="chkcopy" runat="server" AutoPostBack="True" OnCheckedChanged="chkcopy_CheckedChanged" CssClass="opt"  Text="<% $ Resources:ValidationResources,CBCpyNo%>" Visible="False" /> 
                                 
                          <br />
                                  <asp:Label ID="lblcopy" runat="server" CssClass="span" Visible="False"></asp:Label></td>
                                 
                            </tr>
                            <tr>
                                <td id="txtcopy_no" colspan="4">
                                  <INPUT class="txt10" onkeypress="txtaccession_OnKeyPress();Integerandcoma();" id="txtcopyno"   onblur="this.className='blur'"
										
										onfocus="this.className='focus'" type="text" name="txtstartno" runat="server" visible="false"></td>
                                 
                            </tr>
                             
                            <tr>
                                <td colspan="4">
                                   <asp:Label ID="Label36" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,lbdelaccNo %>"
                                        Font-Bold="True"></asp:Label></td>
                                 
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:CheckBoxList ID="chkAccDel" runat="server" Font-Names="Verdana" 
                                        ForeColor="Purple" RepeatDirection="Horizontal" CellPadding="3" CellSpacing="2" RepeatColumns="8">
                                    </asp:CheckBoxList></td>
                                 
                            </tr>
							<tr>
								<td>
                                   <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged" CssClass="opt" Text="<% $ Resources:ValidationResources,bSearch %>" /></td> 
                                <td colspan="3" style="text-align: center">
                                    <!--<INPUT id="cmdsave"  style="WIDTH: 65px;" type="button" value="<% $ Resources:ValidationResources,bSave %>" name="cmdsubmit"
													runat="server" class="btn">-->
                                    <asp:Button ID="btnS" CssClass="btn btn-primary" OnClick="btnS_Click" runat="server" Text="Submit" />

                                    <asp:Button ID="cmdreset2" runat="server" CssClass="btn btn-primary" Text="<% $ Resources:ValidationResources,bReset %>"
                                         OnClick="cmdreset2_Click" />
                                    <input id="cmdreset" type="button" style="display:none" value="<% $ Resources:ValidationResources,bReset %>" name="cmdreset"
                                        runat="server" class="btnstyle">

                                    <asp:Button ID="cmddelete2" runat="server" CssClass="btn btn-primary" Text="<% $ Resources:ValidationResources,bDelete %>"
                                         OnClick="cmddelete2_Click" />
                                    <input id="cmddelete" type="button" value="<% $ Resources:ValidationResources,bDelete %>" name="cmdreset"
                                        runat="server" class="btnstyle">
                                    <input id="Button1" runat="server" name="Button1" style="visibility: hidden"
                                        type="button" class="btnH" />
                                    <input id="Hidden7" type="hidden" size="1" name="Hidden7"
                                        runat="server">
                                </td>
                               
							</tr>
                          
                            <tr >
                                <td >
                                   <asp:Label ID="Label44" runat="server" CssClass="span"  text="<% $ Resources:ValidationResources,SrchCrtria %>"></asp:Label></td>
                                <td  colspan="3">
                                    <asp:RadioButton ID="optpre" runat="server" Checked="True" CssClass="opt" GroupName="p"
                                        Text="<% $ Resources:ValidationResources,PreCtlSrch %>" AutoPostBack="True" />
                                    <asp:RadioButton ID="optpost" runat="server" CssClass="opt" GroupName="p" Text="<% $ Resources:ValidationResources,PostCtlSrch %>" AutoPostBack="True" /></td>
                                 
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label45" runat="server" CssClass="span"  text="<% $ Resources:ValidationResources,SrchType %>"></asp:Label></td>
                                <td colspan="3" rowspan="1" valign="top" >
                                    <asp:RadioButton ID="optaccno" runat="server" Checked="True" CssClass="opt" GroupName="t" OnCheckedChanged="optaccno_CheckedChanged"
                                        Text="<% $ Resources:ValidationResources,LaccNoBased %>" AutoPostBack="True" />
                                    <asp:RadioButton ID="opttitle" runat="server" CssClass="opt" OnCheckedChanged="opttitle_CheckedChanged"
                                        GroupName="t" Text="<% $ Resources:ValidationResources,LITBase %>" AutoPostBack="True" /></td>
                                
                            </tr>
                            <tr >
                                <td >
                                    <asp:Label ID="Label42" runat="server" CssClass="span"  text="<% $ Resources:ValidationResources,AccNo %>"></asp:Label></td>
                                <td colspan="3" >
                                    <input id="txtCategory"  runat="server" style="width:70% !important" name="txtCategory" onfocus="this.className='focus'"
                                       
                                       />
                                   
                                    <asp:Button ID="btnCategoryFilter2" runat="server" Text="<% $ Resources:ValidationResources,bSearch %>"
                                         CssClass="btn btn-primary" OnClick="btnCategoryFilter2_Click" />   
                                    <input id="btnCategoryFilter"
                                            runat="server" name="cmdsearch" type="submit"
                                            value="<% $ Resources:ValidationResources,bSearch %>" class="btnstyle" />XD</td>
                               
                            </tr>
                            
						</TABLE>
                                    </div>
                                <div class="allgriddiv" id="dvgrd" runat="server">                                  
                    <asp:HiddenField runat="server" ID="hdnGrdId" />
                                     <asp:DataGrid ID="grddetail" runat="server" OnItemCommand="grddetail_ItemCommand"  AutoGenerateColumns="False" CellPadding="2" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'
                                         CssClass="allgrid"  GridLines="None" BorderStyle="None" >
                                        <EditItemStyle CssClass="GridEditedItemStyle"/>
                                        <SelectedItemStyle CssClass="GridSelectedItemStyle"/>
                                        <PagerStyle BorderColor="AliceBlue" ForeColor="Red" HorizontalAlign="Right" Mode="NumericPages"
                                            Position="TopAndBottom"/>
                                        <AlternatingItemStyle CssClass="GridAltItem"/>
                                        <ItemStyle CssClass="GridItem"/>
                                        <HeaderStyle CssClass="GridHeader"/>
                                        <Columns>
                                            <asp:ButtonColumn CommandName="Select" HeaderText="<%$ Resources:ValidationResources, AccNo %>" Text="<%$ Resources:ValidationResources, AccNo %>" DataTextField="accno">
                                                <HeaderStyle Width="30%"/>
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" ForeColor="Blue"/>
                                            </asp:ButtonColumn>
                                            <asp:BoundColumn DataField="accno" HeaderText="<%$ Resources:ValidationResources, AccNo %>" Visible="False">
                                                <HeaderStyle Width="30%"/>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="booktitle" DataFormatString="<%$ Resources:ValidationResources, GridDateF %>" HeaderText="<%$ Resources:ValidationResources, LTitle %>">
                                                <HeaderStyle Width="60%"/>
                                            </asp:BoundColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </div>
						  </ContentTemplate>
                                 
                        </asp:UpdatePanel>
                </div>
                <INPUT id="hdIsMarc21" type="hidden" name="hdIsMarc21" runat="server"/>
								<INPUT id="xCoordHolder"  type="hidden" size="1" value="0"
										name="xCoordHolder" runat="server"><INPUT id="yCoordHolder" style="WIDTH: 16px; HEIGHT: 22px" type="hidden" size="1" value="0"
										name="yCoordHolder" runat="server">
                                    <input id="hSubmit1" runat="server" name="hSubmit1" size="1" style="width: 24px;
                                        height: 22px" type="hidden" value="0" />
                                    <asp:ListBox ID="hlstAllCategory" runat="server" Height="0px" Width="32px" style=" visibility:hidden "></asp:ListBox>
                                    <input id="hCurrentIndex2" runat="server" name="hCurrentIndex2" style="width: 16px;
                                        height: 16px" type="hidden" />&nbsp;
                                    <input id="hdval" runat="server" style="width: 8px; height: 16px" type="hidden" />
                                    <input id="hdval1" runat="server" style="width: 8px; height: 16px" type="hidden" />
                                    <input id="HdVendorid" runat="server" style="width: 12px" type="hidden" />

					<INPUT id="txtaccid" onblur="this.className='blur'" style="WIDTH: 18px; HEIGHT: 22px" onfocus="this.className='focus'"
							type="hidden" size="1" name="Hidden1" runat="server"> <input id="Hidden8" runat="server" style="width: 24px" type="hidden" /><INPUT id="Hidden2" style="WIDTH: 20px; HEIGHT: 22px" type="hidden" size="1" name="Hidden1"
							runat="server"><INPUT style="WIDTH: 29px; HEIGHT: 22px" type="hidden"><INPUT id="Hidden6" style="WIDTH: 20px; HEIGHT: 22px" type="hidden" size="1" name="Hidden1"
							runat="server"><INPUT id="Hidden5" style="WIDTH: 18px; HEIGHT: 22px" type="hidden" size="1" name="Hidden5"
							runat="server"> <INPUT id="Hidden17" type="hidden" runat="server" style="width: 28px">
                        <input id="hdchkcategory" runat="server" name="hdchkcategory" style="width: 10px"
                            type="hidden" />
                        <input id="HComboSelect" runat="server" 
                            type="hidden" value="<%$ Resources:ValidationResources, ComboSelect %>" style="width: 42px" />
                        <input id="hrDate" runat="server" 
                            type="hidden" value="<%$ Resources:ValidationResources, dateFormat1 %>" style="width: 36px" />
                        <input id="js1" runat="server" 
                            type="hidden" value="<%$ Resources:ValidationResources, js1 %>" style="width: 57px" />
                        <input id="hdCulture" runat="server" style="width: 39px" type="hidden" />
						
						<%--				<asp:customvalidator id="cvdtDept" runat="server" ControlToValidate="cmbdept" Display="None" ErrorMessage="<%$ Resources:ValidationResources, IvDep %>"
											ClientValidationFunction="comboValidation" SetFocusOnError="True"></asp:customvalidator>
                        --%>                <%--<asp:requiredfieldvalidator id="title" runat="server" Font-Size="11px" Width="18px" Height="8px" ErrorMessage="<%$ Resources:ValidationResources, IvTitle %>"
											Display="None" ControlToValidate="txtSTitle" SetFocusOnError="True"></asp:requiredfieldvalidator>--%>
                                        <%--<asp:RequiredFieldValidator ID="txtcmbpub" runat="server" ControlToValidate="txtCmbPublisher"
                                            ErrorMessage="<%$ Resources:ValidationResources, SlctPub %>" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                   
									
                                        <input id="Hidden1" runat="server" style="width: 23px" type="hidden" />
                                       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtrelease"
                                            Display="None" ErrorMessage="<%$ Resources:ValidationResources, ReqBarDate %>" Font-Size="11px" Height="14px" SetFocusOnError="True"
                                            Width="40px"></asp:RequiredFieldValidator>--%>
                                        <input id="HDAccesionIdNew" runat="server" type="hidden" />
                                        <input id="HDAccesionNew" runat="server" type="hidden" />
                                        <input id="HDPubCode" runat="server" type="hidden" />
                                     <%--   <asp:validationsummary id="ValidationSummary1" runat="server" Font-Size="11px" Width="286px" Height="16px"
											DisplayMode="List" ShowMessageBox="True" ShowSummary="False"></asp:validationsummary>
									--%>
                             <%--           <asp:CustomValidator ID="mediavalidator1" runat="server" ClientValidationFunction="comboValidation"
                                            ControlToValidate="cmbmediatype" Display="None" ErrorMessage="<%$ Resources:ValidationResources, IvMedia %>" SetFocusOnError="True"></asp:CustomValidator>&nbsp;
                                        <asp:CustomValidator ID="languagevalidator1" runat="server" ClientValidationFunction="comboValidation"
                                            ControlToValidate="cmbLanguage" Display="None" ErrorMessage="<%$ Resources:ValidationResources, IvLang %>" SetFocusOnError="True"></asp:CustomValidator>
                                        <asp:CustomValidator ID="categoryvalidator1" runat="server" ClientValidationFunction="comboValidation"
                                            ControlToValidate="cmbcategory" Display="None" ErrorMessage="<%$ Resources:ValidationResources, IvCat %>" SetFocusOnError="True"></asp:CustomValidator>
                                        --%>
									<%--<asp:requiredfieldvalidator id="Requiredfieldvalidator3" runat="server" Font-Size="11px" Width="152px" ErrorMessage="<%$ Resources:ValidationResources, EtrFstN %>"
											Display="None" ControlToValidate="txtfname1" SetFocusOnError="True"></asp:requiredfieldvalidator>&nbsp;
                                        <asp:RequiredFieldValidator ID="Requiredfieldvalidator1" runat="server" ControlToValidate="txtAccDate"
                                            Display="None" ErrorMessage="<%$ Resources:ValidationResources, EtrDocD %>" Font-Size="11px" SetFocusOnError="True"
                                            Width="152px"></asp:RequiredFieldValidator>--%>
                                        &nbsp;
                                       <%-- <asp:CustomValidator ID="Currencyvalidator1" runat="server" ClientValidationFunction="comboValidation"
                                            ControlToValidate="cmbcurr" Display="None" ErrorMessage="<%$ Resources:ValidationResources, IvCurr %>" SetFocusOnError="True"></asp:CustomValidator>
                                        <asp:CustomValidator ID="CVforeignprice" runat="server" BorderStyle="None" ClientValidationFunction="ZeroValidation"
                                            ControlToValidate="txtForeignPrice" Display="None" ErrorMessage="<%$ Resources:ValidationResources, PCurMGrtr0 %>"
                                            SetFocusOnError="True"></asp:CustomValidator>--%>
                                        <%--<asp:requiredfieldvalidator id="RequiredFieldValidator4" runat="server" ErrorMessage="<%$ Resources:ValidationResources, EtrPrRs %>" Display="None"
											ControlToValidate="txtprice" SetFocusOnError="True"></asp:requiredfieldvalidator>--%>
									
                                       <%-- <asp:CustomValidator ID="CVprice" runat="server" ClientValidationFunction="ZeroValidation"
                                            ControlToValidate="txtprice" Display="None" ErrorMessage="<%$ Resources:ValidationResources, PCurMGrtr0 %>"
                                            SetFocusOnError="True"></asp:CustomValidator>--%>
                                        <%--<asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" Font-Size="11px" Width="40px" Height="14px"
											ErrorMessage="<%$ Resources:ValidationResources, EtrNofCopy %>" Display="None" ControlToValidate="txtnoofcopies" SetFocusOnError="True"></asp:requiredfieldvalidator>
                                        <asp:RequiredFieldValidator ID="Vendorvalidator" runat="server" ControlToValidate="txtCmbVendor"
                                            SetFocusOnError="True" ErrorMessage="<%$ Resources:ValidationResources, IvVen %>" Display="None"></asp:RequiredFieldValidator>--%>
                                        &nbsp;
                                       
<%--                                        <asp:CustomValidator ID="CustomValidator11" runat="server" ClientValidationFunction="ZeroValidation"
                                            ControlToValidate="txtnoofcopies" Display="None" ErrorMessage="<%$ Resources:ValidationResources, NoCopMGrt %>" SetFocusOnError="True"></asp:CustomValidator>&nbsp;
                                        <asp:CustomValidator ID="CVspprice" runat="server" ClientValidationFunction="ZeroValidation"
                                            ControlToValidate="txtspecialprice" Display="None" ErrorMessage="<%$ Resources:ValidationResources, SpclPricMGtr0 %>"
                                            SetFocusOnError="True"></asp:CustomValidator>--%>
                                            <input id="btnPub" runat="server" onclick="openNewForm('btnFillPub', 'PublisherMaster', 'HNewForm', 'HWhichFill', 'HCondition');"  style="width: 1px; height: 1px; display:none  " type="button"
                                value="button" class="btnH"/>
                                        <input id="hdCtrlNo" runat="server" type="hidden" />
                                <input id="btnVen" runat="server" onclick="openNewForm('btnFillPub', 'VendorMaster', 'HNewForm', 'HWhichFill', 'HCondition');" accesskey="V" style="width: 1px; height: 1px; display:none " type="button"
                                value="button" class="btnH"/>
                                <input id="btnDep" runat="server" onclick="openNewFormdep('btnFillPub', 'DepartmentMaster', 'HNewForm', 'HWhichFill', 'HCondition');" accesskey="T" style="width: 1px; height: 1px;display:none" type="button"
                                value="button" class="btnH"/>
                                <input id="btnCurrency" runat="server" onclick="openNewForm('btnFillPub', 'ExchangeMaster', 'HNewForm', 'HWhichFill', 'HCondition');" accesskey="C" style="width: 1px; height: 1px;display:none " type="button"
                                value="button" class="btnH"/>
                                <input id="btnMedia" runat="server" onclick="openNewForm('btnFillPub', 'frm_mediatype', 'HNewForm', 'HWhichFill', 'HCondition');" accesskey="M" style="width: 1px; height: 1px;display:none" type="button"
                                value="button" class="btnH"/>
                                <input id="btnLanguage" runat="server" onclick="openNewForm('btnFillPub', 'TranslationLanguages', 'HNewForm', 'HWhichFill', 'HCondition');" accesskey="L" style="width: 1px; height: 1px;display:none " type="button"
                                value="button" class="btnH"/>
                                <input id="btnCategory" runat="server" onclick="openNewForm('btnFillPub', 'CategoryLoadingStatus', 'HNewForm', 'HWhichFill', 'HCondition');" accesskey="G" style="width: 1px; height: 1px;display:none " type="button"
                                value="button" class="btnH"/>
                                                          <input id="HNewForm" runat="server" style="width: 9px" type="hidden" />
                            <input id="HWhichFill" runat="server" style="width: 23px" type="hidden" />
                            <input id="HCondition" runat="server" style="width: 5px" type="hidden" />
                            <input id="btnFillPub" runat="server" style="width: 1px; height: 1px;display:none " class="btnH" type="button" value="button" causesvalidation="false" />
                                       
								
                        <input id="HCurrName" runat="server" style="width: 56px" type="hidden" />
                        <input id="hdFirst" runat="server" style="width: 53px" type="hidden" />
                        <input id="hdCity" runat="server" style="width: 53px" type="hidden" />
                        <input id="hdstate" runat="server" style="width: 53px" type="hidden" />
                        <input id="hdCountry" runat="server" style="width: 53px" type="hidden" />
                        <input id="hdAddress" runat="server" style="width: 53px" type="hidden" />
                        <asp:Button ID ="cmdsearch" runat ="server" style="visibility:hidden "  />
                                    <input id="cmdinsertAt" accesskey="Z" onclick="insertAtCursor(document.Form1.txtSTitle);"
                                        style="width: 1px; height: 1px;display:none " class="btnH" type="button" />
				           	<script type="text/javascript">
                                   function myHotKey(value1) {
                                       if (value1 == "department") {
                                           window.showModalDialog("departmentmaster.aspx", "Staff", "dialogHeight:600px; dialogWidth:650px; left:600px; top:300px;menubar:no; resizable:yes;scroll=yes", true);
                                       }
                                       else if (value1 == "publisher") {
                                           window.showModalDialog("publishermaster.aspx", "Staff", "dialogHeight:600px; dialogWidth:850px; left:600px; top:300px;menubar:no; resizable:yes;scroll=yes", true);
                                       }
                                       else if (value1 == "media") {
                                           window.showModalDialog("frm_mediatype.aspx", "Staff", "dialogHeight:600px; dialogWidth:650px; left:0px; top:300px;menubar:no; resizable:yes;scroll=yes", true);
                                       }
                                       else if (value1 == "lang") {
                                           window.showModalDialog("translationlanguages.aspx", "Staff", "dialogHeight:600px; dialogWidth:650px; left:0px; top:300px;menubar:no; resizable:yes;scroll=yes", true);
                                       }
                                       else if (value1 == "category") {
                                           window.showModalDialog("categoryloadingstatus.aspx", "Staff", "dialogHeight:600px; dialogWidth:650px; left:0px; top:300px;menubar:no; resizable:yes;scroll=yes", true);
                                       }
                                       else if (value1 == "exchange") {
                                           window.showModalDialog("exchangemaster.aspx", "Staff", "dialogHeight:600px; dialogWidth:650px; left:0px; top:300px;menubar:no; resizable:yes;scroll=yes", true);
                                       }
                                       else if (value1 == "vendor") {
                                           window.showModalDialog("vendormaster.aspx", "Staff", "dialogHeight:600px; dialogWidth:850px; left:0px; top:300px;menubar:no; resizable:yes;scroll=yes", true);
                                       }
                                   }
                               </script>
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
                            showPublSearch();
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
                      //  ThreeLevelSearch($('#' + grdId), "[id$=dvgrd]");
                    }
                    catch (err) {
                    }
                }

                function SetDatePicker() {
                    try {

                        $("[id$=txtAccDate]").datepicker({

                            changeMonth: true,//this option for allowing user to select month
                            changeYear: true, //this option for allowing user to select from year range
                            dateFormat: 'dd-M-yy'  // CHANGE DATE FORMAT.
                        });
                    } catch (errs) {
                        console.log('jqueryui failed on content page:' + errs);
                    }

                }

            </script>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>


