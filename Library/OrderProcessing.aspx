<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/LibraryMain.master" CodeBehind="OrderProcessing.aspx.cs" Inherits="Library.OrderProcessing" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>
<asp:Content ID="OrderPHead" runat="server" ContentPlaceHolderID="head">
     <script src="FormScripts/multiselect.js"></script>

		 <link href="cssDesign/libresponsive.css" rel="stylesheet" type="text/css" >
    <script type="text/javascript">

            function OrderStage(stage) {
                var retValue;
                //change to window.open
                retValue = window.showModalDialog("userValidation.aspx?authority=" + stage, "User Validation", "dialogHeight:170px;dialogWidth:275px,dialogHide:true;help:no;scroll:no;status:no;");
                if (retValue != null) {
                    document.getElementById('hdtxtuserid').value = retValue;
                    document.Form1.btnOrderStage1.click();
                }
            }

            function signature() {
                var retValue;
                retValue = window.showModalDialog("signature.aspx", "Signature", "dialogHeight:170px;dialogWidth:275px,dialogHide:true;help:no;scroll:no;status:no;");
                if (retValue != null) {
                    document.getElementById("hdSignature").value = retValue;
                    document.Form1.cmdSignature.click();
                }
            }

            function GetServer(getCtrlId, ctrlTo) {

                //var getCtrlId=ctrlRef.id;
                var value = document.getElementById(getCtrlId).value;
                if (getCtrlId == 'chkSearch') {
                    var st;


                    if (document.getElementById(getCtrlId).checked) {
                        value = "Order"; //document.getElementById(getCtrlId).checked;
                        document.getElementById('lstAllCategory').style.visibility = 'visible';
                        document.getElementById('txtCategory').style.visibility = 'visible';
                        document.getElementById('txtCategory').value = "";
                        //       document.getElementById('txtCategory').focus();
                        document.getElementById('btnCategoryFilter').style.visibility = 'visible';
                        document.getElementById('ddl1').style.visibility = 'hidden';
                        document.getElementById('optOrder').style.visibility = 'visible';
                        document.getElementById('optAdvance').style.visibility = 'visible';
                        document.getElementById('lbloptOrder1').style.visibility = 'visible';
                        document.getElementById('lbloptAdvance1').style.visibility = 'visible';
                        document.getElementById('optOrder').checked = true;
                        document.getElementById('optAdvance').checked = false;
                    } else {
                        value = "Order"; //document.getElementById(getCtrlId).checked;
                        document.getElementById(ctrlTo).style.visibility = 'hidden';
                        document.getElementById('txtCategory').value = "";
                        document.getElementById('txtCategory').style.visibility = 'hidden';
                        document.getElementById('btnCategoryFilter').style.visibility = 'hidden';
                        document.getElementById('ddl1').style.visibility = 'hidden';
                        document.getElementById('optOrder').style.visibility = 'hidden';
                        document.getElementById('optAdvance').style.visibility = 'hidden';
                        document.getElementById('lbloptOrder1').style.visibility = 'hidden';
                        document.getElementById('lbloptAdvance1').style.visibility = 'hidden';
                    }
                    value = value + "|" + "chkS";
                } else if (getCtrlId == 'optOrder' || getCtrlId == 'optAdvance') {
                    if (document.getElementById('optOrder').checked) {
                        document.getElementById('ddl1').style.visibility = 'hidden';
                        document.getElementById('optAdvance').checked = false;
                        value = "Order";
                    } else {
                        value = "Advance";
                        document.getElementById('optAdvance').checked = true;
                        document.getElementById('ddl1').style.visibility = 'visible';
                        //        document.getElementById('ddl1').focus();
                    }
                    document.getElementById('txtCategory').value = "";
                    //       document.getElementById('txtCategory').focus();
                    value = value + "|" + "chkS";
                }
                UseCallBack(value, ctrlTo);
            }
            function clientback(result, context) {


                if (context == 'lstAllCategory') {
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
                } else {
                    var ctrl = document.getElementById(context);
                    if (!ctrl) {
                        return;
                    }
                    ctrl.length = 0;
                    if (!result) {
                        return;
                    }

                    var rows = result.split('|');
                    for (var i = 0; i < rows.length - 1; ++i) {
                        var values = rows[i].split('^');
                        var option = document.createElement("OPTION");
                        option.value = values[0];
                        option.innerHTML = values[1];
                        ctrl.appendChild(option);

                    }
                    document.getElementById("hReq").value = document.getElementById(context).value;
                    //         ctrl.selectedIndex=eval(parseInt(rows.length)-2);
                }
            }
            function clientback1(result, context) {


                if (context == 'lstindent') {
                    var ctrl = document.getElementById('lstindent');
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
                } else {
                    var ctrl = document.getElementById(context);
                    if (!ctrl) {
                        return;
                    }
                    ctrl.length = 0;
                    if (!result) {
                        return;
                    }

                    var rows = result.split('|');
                    for (var i = 0; i < rows.length - 1; ++i) {
                        var values = rows[i].split('^');
                        var option = document.createElement("OPTION");
                        option.value = values[0];
                        option.innerHTML = values[1];
                        ctrl.appendChild(option);

                    }
                    document.getElementById("hReq").value = document.getElementById(context).value;
                    //         ctrl.selectedIndex=eval(parseInt(rows.length)-2);
                }
            }
            function chk() {
                if (document.Form1.hdTop.value == "top") {

                    window.scrollTo(0, 0);
                    document.Form1.hdTop.value = 0;
                }
                if (document.Form1.Hidden7.value == "101") {
                    document.Form1.Hidden7.value = 0;

                }
                if (document.Form1.Hidden7.value == "102") {
                    document.Form1.Hidden7.value = 0;

                }
                if (document.Form1.Hidden7.value == "103") {
                    document.Form1.Hidden7.value = 0;

                }
            }


		</script>
		
        <script  type="text/javascript">
            window.history.forward(1);
            function btnOrderStage_onclick() {

            }

        </script>
    </asp:Content>
<asp:Content ID="OrderPMain" runat="server" ContentPlaceHolderID="MainContent">
    <asp:UpdateProgress ID="UpPorg1" runat="server">
        <ProgressTemplate>
             <NN:Mak ID="FF1" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="container tableborderst">
        <div style="width:100%;display:none" class="title">
            <div style="width:89%;float:left">
                <asp:label id="lblTitle" runat="server" Width="100%"> Order Processing</asp:label>

            </div>
            <div style="float:right;vertical-align:top">
                <a id="lnkHelp" href="#" onclick="ShowHelp('Help/Acq-Orders-Processing.htm')"><img src="help.jpg" alt="Help" style="height: 16px" /></a>

            </div>

        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <%-- <Triggers>
                <asp:PostBackTrigger ControlID="btnOrderStage1" />
                <asp:PostBackTrigger ControlID="cmdSignature" />

            </Triggers>--%>
            <ContentTemplate>
                <table id="Table3" class="table-condensed no-more-tables" >
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="msglabel" runat="server" ForeColor="Red"  Font-Names="Lucida Sans Unicode"
                                                     Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="Label11" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LOrderT %>"></asp:Label></td>
                        <td colspan="1"><asp:RadioButton ID="optNormal" runat="server" CssClass="opt" Text="<%$Resources:ValidationResources,RBIBased %>"  AutoPostBack="True" Checked="True" GroupName="r"></asp:RadioButton></td>
                        <td><asp:RadioButton ID="r1" runat="server" CssClass="opt"  AutoPostBack="True" Text="<%$Resources:ValidationResources,RBGiftI %>"
                                                     GroupName="r"></asp:RadioButton></td>
                        <td>
                            <asp:TextBox ID="curr_id" runat="server" Visible="False"></asp:TextBox>
                                                 <input id="Hidden4" type="hidden" size="1" name="Hidden1"
                                                     runat="server">
                        </td>
                    </tr>
                    <tr>
                        <td style="width:17%"> <asp:Label ID="Label1" runat="server" CssClass="span" Text="<%$Resources:ValidationResources,LVen %>"></asp:Label></td>
                        <td colspan="3">
                            <asp:DropDownList ID="cmbvendor" onblur="this.className='blur'" onfocus="this.className='focus'" runat="server"
                                                     CssClass="txt10"  AutoPostBack="True" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'>
                                                 </asp:DropDownList>
                            <asp:Label ID="Label16" runat="server" CssClass="star">*</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="Label2" runat="server" CssClass="span" Text="Department"></asp:Label></td>
                        <td colspan="3">
                            <asp:ListBox ID="cmbdept" runat="server"  AutoPostBack="true" SelectionMode="Single"></asp:ListBox>

                                                 <asp:Label ID="Label5" runat="server" CssClass="star">*</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label6" runat="server" CssClass="span" Text="<%$Resources:ValidationResources,LOrderD %>"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtorderedon" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                            <asp:Label ID="Label8" runat="server" CssClass="star">*</asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label3" style="color:black;" runat="server"   Text="<%$Resources:ValidationResources,LIndentNum %>"></asp:Label>
                        </td>
                        <td style="vertical-align:top"  rowspan="4"> 
                            <asp:ListBox ID="lstindent" style="height:60px!important" runat="server" CssClass="txt10" SelectionMode="Multiple" AutoPostBack="True"></asp:ListBox>
                                                 <asp:Label ID="Label12" runat="server" CssClass="star">*</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LOrderNo %>"></asp:Label>
                        </td>
                        <td>
                            <input class="txt10" id="txtorderno" onblur="this.className='blur'" onfocus="this.className='focus'" type="text" size="28" name="txtorderno" runat="server" >

                        </td>
                    </tr>
                    <tr>
                        <td style="width:20%"></td>
                        <td style="width:30%">
                            <input id="Hd_orderno" style="width: 16px; height: 13px" type="hidden" size="1" name="Hidden2"
                                                     runat="server">
                        </td>
                        <td style="width:20%"></td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <div>
                                 <asp:Label ID="Label9" runat="server" style="color:black"  CssClass="head1" Text="<%$ Resources:ValidationResources,LIndDetails %>"></asp:Label>

                            </div>
                        </td>
                    </tr>
                </table>
                 <div class="allgriddiv" style="max-height:300px" id="dvgrd" runat="server">  
                      <asp:HiddenField runat="server" ID="hdnGrdId" />
                                <asp:datagrid id="grddetail" runat="server" CssClass="allgrid"  Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'>
                                    </asp:datagrid>
                 </div>
                <div div class="no-more-tables" style="width:100%">
                    <table>
                        <tr>
                            <td>
                                <p>
                                    <asp:label id="Label14" runat="server" CssClass="span"  Text="<%$Resources:ValidationResources,LExArrDate %>"> </asp:label>

                                </p>
                            </td>
                            <td>
                                <asp:TextBox ID="txtdatenon" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"/>
                                <asp:Label ID="Label10" runat="server"
                                                    CssClass="star">*</asp:Label>

                            </td>
                            <td>
                                <asp:label id="Label7" runat="server" CssClass="span"  Text="<%$Resources:ValidationResources,LTotAmount %>">Total Amount(Rs)</asp:label>
                            </td>
                            <td>
                                <INPUT class="txt10" id="txttotalamount" onblur="this.className='blur'" 
												onfocus="this.className='focus'"  type="text" size="12" name="txttotalamount" runat="server">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <input id="chkChecked" runat="server" type="checkbox" visible="false" /><asp:Label ID="lblChecked" runat="server" Text="<%$ Resources:ValidationResources,Checked %>" Visible="False"></asp:Label><input id="chkPassed" runat="server" type="checkbox" visible="false" /><asp:Label ID="lblPassed" runat="server" Text="<%$ Resources:ValidationResources,Passed %>" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                        <td>
                            <asp:Label ID="lblRemarks" runat="server" CssClass="span" Text="<%$Resources:ValidationResources,Rmrks %>"></asp:Label>
                        </td>
                        <td colspan="3">
                            <input id="txtRemarks" runat="server" class="txt10" style="width: 558px" type="text" onblur="this.className='blur'" onfocus="this.className='focus'" />
                        </td>
                            </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblVerify" runat="server" CssClass="span" Text="<%$Resources:ValidationResources,IChkVer %>"></asp:Label>
                            </td>
                            <td colspan="3">
                                 <input id="btnChecked" runat="server" type="button" value="Checked By" causesvalidation="false" style="width: 100px;" class="btn" /><input id="btnPassed" runat="server" type="button" value="Passed By" causesvalidation="false" style="width: 100px;" class="btn" />
                            </td>
                        </tr>
                        <tr>
                            <td colSpan="4" style="text-align:center">
                                		
                                <input id="btnOrderStage" runat="server"  type="button"
                                    value="<%$Resources:ValidationResources,bSave %>" visible="false" class="btnstyle" onclick="return btnOrderStage_onclick()" />
                                        <INPUT id="cmdsave"  type="button" value="<%$Resources:ValidationResources,prepare %>" name="cmdsave"
															runat="server" class="btnstyle">
													<INPUT id="cmdreset"   type="button" value="<%$Resources:ValidationResources,bReset %>" name="cmdreset"
															runat="server" class="btnstyle">
													<INPUT id="cmddelete"  onclick="if (DoConfirmation() == false) return false;"
															type="button" value="<%$Resources:ValidationResources,reindent %>" name="cmddelete" runat="server" class="btnstyle">
													<INPUT id="cmdprint" type="button" value="<%$Resources:ValidationResources,iprint %>" name="cmdPrint"
															runat="server" class="btnstyle" >
                                                   
                                                        <input id="btnEmail" runat="server" causesvalidation="false" type="button" value="<%$Resources:ValidationResources,bSendMail %>"  class="btnstyle"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkSearch" runat="server" AutoPostBack="True"     Text=" " />
                                <asp:Label ID="Label29" runat="server" CssClass="opt" Text="<%$ Resources:ValidationResources,bSearch %>"></asp:Label>
                            </td>
                            <td colSpan="2">
                                        <asp:RadioButton ID="optOrder" runat="server" AutoPostBack="True" Checked="True"
                                            GroupName="a" Text=" " /><asp:Label ID="lbloptOrder1" runat="server" CssClass="opt"
                                                Text="<%$ Resources:ValidationResources,RBOrNoBaSea %>"></asp:Label></td>
                            <td ></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="3">
                                <table style="width:100%">
                                    <tr>
                                        <td><asp:RadioButton ID="optAdvance" runat="server" AutoPostBack="True" GroupName="a" Text=" " /><asp:Label ID="lbloptAdvance1" runat="server" CssClass="opt" Text="<%$ Resources:ValidationResources,RBAdSea%>"></asp:Label></td>
                                        <td>
                                            <asp:dropdownlist id="ddl1" onblur="this.className='blur'" onfocus="this.className='focus'" runat="server"
											CssClass="txt10" Font-Names ="<%$Resources:ValidationResources,TextBox1 %>" AutoPostBack="True">
											<asp:ListItem Text="<%$ Resources:ValidationResources,BkTtl %>" Value="Title" Selected="True"></asp:ListItem>
											<asp:ListItem Text="<%$ Resources:ValidationResources,rptDepartment %>" Value="Department" ></asp:ListItem>
											<asp:ListItem Text="<%$ Resources:ValidationResources,lbvendor %>" Value="Vendor"></asp:ListItem>
										</asp:dropdownlist>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:26%"></td>
                                        <td><INPUT class="txt10" onkeypress="disallowSingleQuote(this);" id="txtCategory" onblur="this.className='blur'"
											
											onfocus="this.className='focus'" type="text" name="txtCategory" runat="server" enableviewstate="true"><INPUT id="btnCategoryFilter" type="button" value="<%$Resources:ValidationResources,bSearch %>" name="btnCategoryFilter" runat="server"  size="" causesvalidation="false" class="btnstyle"></td>

                                    </tr>
                                    <tr>
                                        <td> <asp:Label ID="Label13" runat="server" Text="<%$ Resources:ValidationResources,LallTotal %>" Visible="False" ></asp:Label>
</td>
                                        <td> <asp:listbox id="lstAllCategory"  style="height:80px!important" AutoPostBack="true"
												tabIndex="3" runat="server" Font-Names ="<%$Resources:ValidationResources,TextBox1 %>" CssClass="txt10"  ></asp:listbox></td>

                                    </tr>

                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                 <INPUT id="txtchangeval"  type="hidden" size="1" name="txtchangeval"
					runat="server">
                            <INPUT id="Button1" style="WIDTH: 1px; HEIGHT: 1px; visibility :hidden " class="btnH" type="button" value="Button" name="Button1"
							runat="server">
                    <INPUT id="hdUnableMsg" type="hidden" runat="server" style="width: 16px"><INPUT id="hdTop" type="hidden" runat="server" style="width: 16px"><INPUT id="Hidden7" type="hidden" runat="server" style="width: 16px">
                        <input id="HComboSelect" runat="server" 
                            type="hidden" value="<%$ Resources:ValidationResources, ComboSelect %>" style="width: 16px" />
                        <input id="hrDate" runat="server" 
                            type="hidden" value="<%$ Resources:ValidationResources, dateFormat1 %>" style="width: 8px" /><input
                                id="js1" runat="server" type="hidden"
                                value="<%$ Resources:ValidationResources, js1 %>" style="width: 8px" />
                        <input id="hdCulture" runat="server" style="width: 8px" type="hidden" />
                        <INPUT id="hCurrentIndex2" style="WIDTH: 8px; HEIGHT: 22px" type="hidden" name="hCurrentIndex2"
							runat="server">
                                <INPUT id="yCoordHolder" style="WIDTH: 8px; HEIGHT: 16px" type="hidden" value="0"
							name="yCoordHolder" runat="server">
                        <INPUT id="xCoordHolder" style="WIDTH: 8px; HEIGHT: 12px" type="hidden" size="1" value="0"
							name="xCoordHolder" runat="server">
                                <INPUT id="hidden6" style="WIDTH: 28px; HEIGHT: 22px" type="hidden" size="1" value="0"
							name="hidden6" runat="server"><INPUT id="hSubmit1" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" value="0"
							name="hSubmit1" runat="server"> 
                        <asp:listbox id="hlstAllCategory" runat="server" Width="8px" Height="19px" Visible="False"></asp:listbox>
                        <input id="hdReport" runat="server" style="width: 8px" type="hidden" />
                                <input id="hdOnlineP" runat="server" style="width: 17px" type="hidden" />
                                <input id="hdOrderStage" runat="server" style="width: 14px" type="hidden" />
                                <input id="hdtxtuserid" runat="server" style="width: 13px" type="hidden" />
                                <input id="btnOrderStage1" runat="server" style="width: 1px; height: 1px; visibility :hidden" type="button"
                                    value="button" />
                                <input id="hdOrderNo" runat="server" style="width: 10px" type="hidden" />
                                <input id="hdSignature" runat="server" style="width: 12px" type="hidden" />
                                <input id="cmdSignature" runat="server" style="width: 1px; height: 1px; visibility :hidden" type="button" />&nbsp;
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="cmdprint" />
            </Triggers>
        </asp:UpdatePanel>

    </div>
    <INPUT id="Hidden5" style="Z-INDEX: 102; LEFT: 699px; WIDTH: 8px; POSITION: absolute; TOP: 81px; HEIGHT: 22px"
				type="hidden" size="1" name="Hidden2" runat="server">&nbsp; <INPUT id="Hidden1" style="Z-INDEX: 101; LEFT: 674px; WIDTH: 8px; POSITION: absolute; TOP: 88px; HEIGHT: 20px"
				type="hidden" size="1" name="Hidden1" runat="server">&nbsp;
   <%-- <INPUT id="Text1" style="Z-INDEX: 103; LEFT: 604px; WIDTH: 8px; POSITION: absolute; TOP: 80px; HEIGHT: 22px"
				type="text" size="1" name="Text1" runat="server">--%>
                                <table id="Table2" style="z-index: 104; left: 408px; width: 8px; position: absolute; top: 1080px; height: 1px"
                                    cellspacing="0" cellpadding="0" border="0" runat="server">
                                    <tr>
                                        <td></td>
                                        <td><asp:CheckBox ID="CheckBox2" runat="server" ForeColor="Navy" Font-Size="11px" Font-Bold="True"
                                                AutoPostBack="True" Text="<% resources: ValidationResources, SgleTleInd %>" Checked="True" Visible="False"></asp:CheckBox>
                                            <p>
                                                <asp:CheckBox ID="CheckBox1" runat="server" Width="157px" ForeColor="Navy" Font-Size="11px" Font-Bold="True"
                                                    AutoPostBack="True" Text="<% resources: ValidationResources, MulTleInd %>" Visible="False"></asp:CheckBox>
                                            </p>
                                        </td>
                                    </tr>
                                    </table>
    <asp:radiobuttonlist id="Select_order" style="Z-INDEX: 105; LEFT: 112px; POSITION: absolute; TOP: 1048px"
				runat="server" Width="8px" Visible="False" Height="56px">
				<asp:ListItem Value="1" Text ="&lt;% resources: ValidationResources, LIGiftO %&gt;"></asp:ListItem>
				<asp:ListItem Value="2" Selected="True" Text ="&lt;% resources: ValidationResources, LIIndO %&gt;"></asp:ListItem>
			</asp:radiobuttonlist>	
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
                //var grdId = $("[id$=hdnGrdId]").val();
                ////alert(grdId);
                //$('<thead></thead>').prependTo('#grddetail').append($('#grddetail' tr:first'));
                //ThreeLevelSearch($('#grddetail'), "[id$=dvgrd]");
            }
            catch (err) {
            }
        }
        function SetDatePicker() {
            $("[id$=txtorderedon],[id$=txtdatenon]").datepicker({
                changeMonth: true,//this option for allowing user to select month
                changeYear: true, //this option for allowing user to select from year range
                dateFormat: 'dd-M-yy'  // CHANGE DATE FORMAT.
            });

        }

    </script>
<script>
    $(function () {
        //ForDataTable();
        SetListBox();
    });
    var prm = Sys.WebForms.PageRequestManager.getInstance();

    if (prm != null) {
        prm.add_endRequest(function (sender, e) {

            if (sender._postBackSettings.panelsToUpdate != null) {

                //  ForDataTable();
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

        $('[id*=cmbdept],[id*=cmbvendor]').multiselect({
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