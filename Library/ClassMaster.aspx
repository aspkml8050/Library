<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/LibraryMain.master"  CodeBehind="ClassMaster.aspx.cs" Inherits="Library.ClassMaster" %>
<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>

<asp:Content ID="cmHead" runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript" src="Scripts/DataTables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="FormScripts/jquery.table2excel.js"></script>
 <link href="cssDesign/libresponsive.css" rel="stylesheet" type="text/css" />
    <link href="packages/jquery.datatables.1.10.15/Content/Content/DataTables/css/jquery.dataTables.css" rel="stylesheet" />
    <link href="Content/MultiLevelSearch.css" rel="stylesheet" />
             <script type="text/javascript" src="FormScripts/MultiLevelSearch.js"></script>
    <script src="packages/jquery.datatables.1.10.15/Content/Scripts/DataTables/buttons.html5.js"></script>
    <script src="packages/jquery.datatables.1.10.15/Content/Scripts/DataTables/dataTables.fixedColumns.js"></script>
    <script src="packages/jquery.datatables.1.10.15/Content/Scripts/DataTables/dataTables.buttons.js"></script>
    <script src="packages/jquery.datatables.1.10.15/Content/Scripts/DataTables/buttons.print.js"></script>
    <script src="Scripts/pdfmake/pdfmake.min.js"></script>
    <script src="packages/jquery.datatables.1.10.15/Content/Scripts/DataTables/buttons.colVis.js"></script>
    <script src="Scripts/jszip.js"></script>
    <script src="Scripts/pdfmake/vfs_fonts.js"></script>
    <link href="packages/jquery.datatables.1.10.15/Content/Content/DataTables/css/buttons.dataTables.css" rel="stylesheet" />
          
            <script type="text/javascript">
                $(function () {
                    CateWise();
                });

                function CateWise() {
                    let CatW = document.getElementById('<%=hdnMemCate.ClientID%>').value;
                    if (CatW != undefined) {
                        if (CatW == 'Y') {
                            document.getElementById('<%=hdnMemCate.ClientID%>').value = '';
                            callUVal5();
                        }
                    }
                }
        </script>
		<script  type="text/javascript">
            window.history.forward(1)
                function callUVal5() {
                    let AllDeac = document.getElementById('<%=ChkActive.ClientID%>').checked;
                    let CanReq = document.getElementById('<%=chkRequester.ClientID%>').checked;
                    let rbMemTypV = 'Free';
                    let rbSecV = 'Yes';
                    if (AllDeac == true)
                        AllDeac = 'Y';
                    else
                        AllDeac = 'N';
                    if (CanReq == true)
                        CanReq = 'Y';
                    else
                        CanReq = 'N';
                    var rbMT = document.getElementById("<%=RadioButtonList1.ClientID%>");
                    var radio = rbMT.getElementsByTagName("input");
                    var label = rbMT.getElementsByTagName("label");
                    for (var i = 0; i < radio.length; i++) {
                        if (radio[i].checked) {
                            rbMemTypV = label[i].innerHTML;
                            break;
                        }
                    }
                    var rbSec = document.getElementById("<%=RadioButtonList2.ClientID%>");
                    var radio = rbSec.getElementsByTagName("input");
                    var label = rbSec.getElementsByTagName("label");
                    for (var i = 0; i < radio.length; i++) {
                        if (radio[i].checked) {
                            rbSecV = label[i].innerHTML;
                            break;
                        }
                    }
                    document.getElementById('<%=hsName.ClientID%>').value = document.getElementById('<%=txtshortname.ClientID%>').value;
                    document.getElementById('<%=hAct.ClientID%>').value = AllDeac;
                    document.getElementById('<%=hMaxVal.ClientID%>').value = document.getElementById('<%=txtMaxValue.ClientID%>').value;

                    var str;
                    var catId = document.getElementById('<%=txtdesignationid.ClientID%>').value;
                    if (navigator.appName == "Microsoft Internet Explorer") {
                        str = window.showModalDialog("Classmaster1.aspx" + '?cName=' + document.getElementById('<%=hcName.ClientID%>').value + '&sName=' + document.getElementById('<%=hsName.ClientID%>').value + '&id1=' + document.getElementById('<%=txtdesignationid.ClientID%>').value + '&act=' + document.getElementById('<%=hAct.ClientID%>').value + '&hFill=' + document.getElementById('<%=hFill.ClientID%>').value + '&hMax=' + document.getElementById('<%=hMaxVal.ClientID%>').value + '&CanReq=' + CanReq + '&rbMemTypV=' + rbMemTypV + '&rbSecV=' + rbSecV, "User Validation", "dialogHeight:380px;dialogWidth:660px,dialogHide:true;help:no;scroll:no;status:no;");
                    } else {
                        //                alert(document.getElementById('hdnCtrl_no').value);
                        window.open("Classmaster1.aspx" + '?cName=' + document.getElementById("<%=hcName.ClientID%>").value + "&sName=" + document.getElementById("<%=hsName.ClientID%>").value + "&id1=" + document.getElementById("<%=txtdesignationid.ClientID%>").value + "&act=" + document.getElementById("<%=hAct.ClientID%>").value + "&hFill=" + document.getElementById("<%= hFill.ClientID%>").value + '&hMax=' + document.getElementById("<%=hMaxVal.ClientID%>").value + '&CanReq=' + CanReq + '&rbMemTypV=' + rbMemTypV + '&rbSecV=' + rbSecV, "User Validation", "status=no,width=660,height=380,hide=true,help=no,scroll=no");
                    }
                    //if(str != null){
                    //_doPostBack('btnFillgrddetail','')
                    //}
                }



            function txtmaxissueday_onblur() {

            }


            function txtmaxissuedayJ_onblur() {
                //if (window.Form1.txtmaxissuedayJ.value == "0") {
                //    //alert("Must be greater than Zero");
                //    alert(document.Form1.hdjs.value);
                //    window.Form1.txtmaxissuedayJ.focus();
                //}
            }

            function txtfineperday1_onblur() {
<%--                if (document.getElementById("<%=txtfineperday1.ClientID%>").value == "0") {
                    //alert("Must be greater than Zero");
                    alert(document.Form1.hdjs.value);
                    window.Form1.txtfineperday1.focus();
                }--%>
            }
            function txtfineperdayJ1_onblur() {
                //if (window.Form1.txtfineperdayJ1.value == "0") {
                //    //alert("Must be greater than Zero");
                //    alert(document.Form1.hdjs.value);
                //    window.Form1.txtfineperdayJ1.focus();
                //}
            }

            function txtfineperday2_onblur() {
                //if (window.Form1.txtfineperday2.value == "0") {
                //    //alert("Must be greater than Zero");
                //    alert(document.Form1.hdjs.value);
                //    window.Form1.txtfineperday2.focus();
                //}
            }
            function txtfineperdayJ2_onblur() {
                //if (window.Form1.txtfineperdayJ2.value == "0") {
                //    //alert("Must be greater than Zero");
                //    alert(document.Form1.hdjs.value);
                //    window.Form1.txtfineperdayJ2.focus();
                //}
            }

		</script>
		<script type="text/javascript">

            function chk() {



                if (document.Form1.Hidden3.value == "6") {

                }
                if (document.Form1.hdTop.value == "top") {
                    //window.scrollTo(0, 0);
                    //document.Form1.hdTop.value = 0;
                }

            }

            function setfocus() {
 //               document.Form1.txtclassname.focus();
            }

            function validateform() {
                let v1 = $('[id$=txtclassname]').val().trim();
                if (v1 == '') {
                    alert('Class Name required.');
                    return false;
                }
                let v2 = $('[id$=txtshortname]').val().trim();
                if (v2 == '') {
                    alert('Class short Name required.');
                    return false;

                }
                return true;
            }

        </script>


</asp:Content>

<asp:Content ID="cmBody" runat="server" ContentPlaceHolderID="MainContent">
              <asp:UpdateProgress ID="UpPorg1" runat="server">
    <ProgressTemplate>
    <NN:Mak ID="FF1" runat="server" />
    </ProgressTemplate>
    </asp:UpdateProgress>

          

              <div style="width:100%;display:table;display:none" class="title">
                    <div style="width:89%;float:left; display:none" > &nbsp;
             <asp:label id="lblTitle" runat="server" style="text-align:center" Width="100%"></asp:label>
                        </div>
                   <div style="float:right;vertical-align:top">
		<a id="lnkHelp" href="#" onclick="ShowHelp('Help/Masters-membergroup.htm')"><img height="15" src="help.jpg"/></a>
                       </div></div>
			
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
        <asp:HiddenField ID="hdnMemCate" runat="server" />
                                    <script type="text/javascript">
                                            $(function () {
                                                       var prm = Sys.WebForms.PageRequestManager.getInstance();
                                                prm.add_endRequest(function () {
                                                    CateWise();
                                                });
                                            });
                                       
                                    </script>
                                       <div class="container tableborderst">
							<TABLE id="Table4" class="col-md-12 table-condensed no-more-tables GenTable1">
								<TR>
									<TD   colSpan="3" ><asp:label id="msglabel" runat="server" CssClass="err" ></asp:label></TD>
								</TR>
								<TR>
									<TD>
                                        <asp:label id="Label1" runat="server" CssClass="span"  Text ="<%$ Resources:ValidationResources, LMGName %>"></asp:label></TD>
									<TD colSpan="2" ><asp:textbox onkeypress="disallowSingleQuote(this);" id="txtclassname" onblur="this.className='blur'"
											onfocus="this.className='focus'" runat="server" CssClass="txt10"   MaxLength="30" ></asp:textbox>
                                        <asp:label id="Label26" runat="server" CssClass="star" >*</asp:label></TD>
								</TR>
                                <tr>
                                    <td>
                                       <asp:Label ID="Lblshortname" runat="server" CssClass="span" Text ="<%$ Resources:ValidationResources, lShortM %>"></asp:Label></td>
                                    <td colspan="2">
                                         <asp:TextBox ID="txtshortname" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                            CssClass="txt10" onblur="this.className='blur'" onfocus="this.className='focus'"
                                           onkeypress="disallowSingleQuote(this);" style='<%$ Resources:ValidationResources, TextBox2 %>' MaxLength="10" 
                                             ></asp:TextBox>
                                        <asp:Label ID="Label5" runat="server" CssClass="star"
                                              >*</asp:Label></td>
                                </tr>
                                <tr>
                                    <td >
                                        <asp:Label ID="lblValueLimit" runat="server" CssClass="span"  Text ="<%$ Resources:ValidationResources, MaxValLimt %>"></asp:Label></td>
                                    <td colspan="2" >
                                        <asp:TextBox ID="txtMaxValue" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                            CssClass="txt10" onblur="this.className='blur'" onfocus="this.className='focus'"
                                           onkeypress="decimalNumber(this)" style='<%$ Resources:ValidationResources, TextBox2 %>'
                                             MaxLength="10"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td >
                                    </td>
                                    <td >
                                        <asp:checkbox id="ChkActive" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources, AllowDeact %>"></asp:checkbox></td>
                                    <td >
                                        <asp:CheckBox ID="chkRequester" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources, AllowRaiseInd %>" /></td>
                                </tr>
                                <tr>
                                    <td  >
                                        <asp:Label ID="Label10" runat="server" Text="<%$ Resources:ValidationResources,CircPolicy  %>"  ></asp:Label></td>
                                    <td colspan="2" >
                                        <asp:RadioButton ID="optgName" runat="server" AutoPostBack="True" Checked="True"
                                            CssClass="opt" GroupName="a" Text="<%$ Resources:ValidationResources, SameCat %>" OnCheckedChanged="optgName_CheckedChanged"/>
                                       <asp:RadioButton ID="optICategory" runat="server" AutoPostBack="True" OnCheckedChanged="optICategory_CheckedChanged"
                                            CssClass="opt" GroupName="a" Text="<%$ Resources:ValidationResources, DifferCat %>" /></td>
                                </tr>
								<TR>
									<TD >
                                        <asp:Label ID="Label31" runat="server" Text="<%$ Resources:ValidationResources, Membershiptype %>"></asp:Label>
                                    </TD>
									<TD colSpan="2">
                                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" >
                                            <asp:ListItem Selected="True">Free</asp:ListItem>
                                            <asp:ListItem>Paid</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </TD>
								</TR>
								<tr>
                                    <td >
                                        <asp:Label ID="Label32" runat="server" Text='<%$Resources : ValidationResources,security%>'></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:RadioButtonList ID="RadioButtonList2" runat="server" 
                                            RepeatDirection="Horizontal" >
                                            <asp:ListItem Selected="True">Yes</asp:ListItem>
                                            <asp:ListItem>No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
								<TR>
								    <td >
                                    </td>
                                    <td ">
                                        <asp:Label ID="Label24" runat="server"  Font-Bold="True" 
                                            Font-Underline="True" Text="<%$ Resources:ValidationResources, LBK %>" 
                                           ></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label25" runat="server"  Font-Bold="True" 
                                            Font-Underline="True" Text="<%$ Resources:ValidationResources, LJournal %>" 
                                          ></asp:Label>
                                    </td>
								</TR>
								<TR>
									<TD >
                                     <asp:Label ID="Label2" runat="server" CssClass="span" 
                                            Text="<%$ Resources:ValidationResources, LIssuLimt %>" ></asp:Label>
                                        </TD>
									<TD  >
                                        <input id="txtmaxissueday" runat="server" class="txt10" maxlength="3" 
                                            name="txtmaxissueday" onblur="this.className='blur'" 
                                            onfocus="this.className='focus'" onkeypress="IntegerNumber(this);" size="5" 
                                             type="text" />
                                            <asp:Label ID="Label27" runat="server" CssClass="star">*</asp:Label>
                                        </input>
                                    </TD>
									<TD >
                                        <input id="txtmaxissuedayJ" runat="server" class="txt10" maxlength="3" 
                                            name="txtmaxissuedayJ" onblur="this.className='blur'" 
                                            onfocus="this.className='focus'" onkeypress="IntegerNumber(this);" size="5" 
                                             type="text" />
                                    </TD>
								</TR>
                                <tr>
                                    <td>
                                       <asp:label id="Label3" runat="server" CssClass="span" 
                                            Text ="<%$ Resources:ValidationResources, ItemIssue %>"></asp:label></td>
                                    <td >
                                        <INPUT onkeypress="IntegerNumber(this);" id="txtmaxissuebook" 
                                            onblur="this.className='blur'" 
											onfocus="this.className='focus'" type="text" name="txtmaxissuebook" runat="server" class="txt10" size="5" 
                                            maxlength="3">
                                        <asp:label id="Label29" runat="server" CssClass="star" >*</asp:label></td>
                                    <td>
                                        <INPUT onkeypress="IntegerNumber(this);" id="txtmaxissueJournal" 
                                            onblur="this.className='blur'" 
											onfocus="this.className='focus'" type="text" name="txtmaxissueJournal" runat="server" class="txt10" 
                                            size="5" maxlength="3"></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:label id="Label6" runat="server" CssClass="span" 
                                            Text ="<%$ Resources:ValidationResources, ReservLimt %>" 
                                            EnableTheming="False"></asp:label></td>
                                    <td >
                                        <INPUT onkeypress="IntegerNumber(this);" id="txtreservation" 
                                            onblur="this.className='blur'" 
											onfocus="this.className='focus'" type="text" name="txtreservation" runat="server" class="txt10" size="5" 
                                            maxlength="3">
                                        <asp:label id="Label30" runat="server" CssClass="star">*</asp:label></td>
                                    <td >
                                        <INPUT onkeypress="IntegerNumber(this);" id="txtreservationJ" 
                                            onblur="this.className='blur'" 
											onfocus="this.className='focus'" type="text" size="5" name="txtreservationJ" runat="server" class="txt10" 
                                            maxlength="3"></td>
                                </tr>
                                <tr>
                                    <td  >
                                       <asp:Label ID="lblcurr" runat="server" CssClass="span" 
                                            Text ="<%$ Resources:ValidationResources, OverDueCharge %>"></asp:Label></td>
                                    <td  >
                                        <INPUT onkeypress="decimalNumber(this)" id="txtfineperday" 
                                            onblur="this.className='blur'" 
											onfocus="this.className='focus'" type="text" size="5" name="txtfineperday" runat="server" class="txt10" 
                                            maxlength="8">
                                        <asp:Label ID="Label28" runat="server" CssClass="star">*</asp:Label></td>
                                    <td >
                                        <INPUT onkeypress="decimalNumber(this);" id="txtfineperdayJ" 
                                            onblur="this.className='blur'" 
											onfocus="this.className='focus'" type="text" size="5" name="txtfineperdayJ" runat="server" class="txt10" 
                                            maxlength="8"></td>
                                </tr>
                                <tr>
                                    <td  >
                                       <asp:Label ID="lblCurrency" runat="server" CssClass="span" 
                                            Text ="<%$ Resources:ValidationResources, PhasDayLimit1 %>"></asp:Label></td>
                                    <td >
                                        <INPUT onkeypress="IntegerNumber(this);" id="txtDays1" 
                                            onblur="this.className='blur'" 
											onfocus="this.className='focus'" type="text" name="txtDays1" runat="server" class="txt10" size="5" 
                                            maxlength="15"></td>
                                    <td>
                                        <INPUT onkeypress="IntegerNumber(this);" id="txtDays1J" 
                                            onblur="this.className='blur'" 
											onfocus="this.className='focus'" type="text" name="txtDays1J" runat="server" class="txt10" size="5" 
                                            maxlength="15"></td>
                                </tr>
                                <tr>
                                    <td   >
                                        <asp:Label ID="Label7" runat="server" CssClass="span"  
                                            Text ="<%$ Resources:ValidationResources, DueCharge1 %>"></asp:Label></td>
                                    <td>
                                        <INPUT onkeypress="decimalNumber(this)" id="txtfineperday1" 
                                            onblur="this.className='blur'" 
											onfocus="this.className='focus'" type="text" name="txtfineperday1" runat="server" class="txt10" size="5" 
                                            maxlength="15"></td>
                                    <td >
                                        <INPUT onkeypress="decimalNumber(this);" id="txtfineperdayJ1" 
                                            onblur="this.className='blur'" 
											onfocus="this.className='focus'" type="text" name="txtfineperdayJ1" runat="server" class="txt10" size="5" 
                                            maxlength="15"></td>
                                </tr>
                                <tr>
                                    <td  >
                                        <asp:Label ID="Label4" runat="server" CssClass="span" 
                                            Text ="<%$ Resources:ValidationResources, PhasDayLimit2 %>"></asp:Label></td>
                                    <td >
                                        <INPUT onkeypress="IntegerNumber(this);" id="txtDays2" 
                                            onblur="this.className='blur'" 
											onfocus="this.className='focus'" type="text" name="txtDays2" runat="server" class="txt10" size="5" 
                                            maxlength="15"></td>
                                    <td >
                                        <INPUT onkeypress="IntegerNumber(this);" id="txtDays2J" 
                                            onblur="this.className='blur'" 
											onfocus="this.className='focus'" type="text" name="txtDays2J" runat="server" class="txt10" size="5" 
                                            maxlength="15"></td>
                                </tr>
								<TR>
									<TD  >
                                       <asp:label id="Label8" runat="server" 
                                            Text="<%$ Resources:ValidationResources, DueCharge2 %>" CssClass="span">&nbsp;&nbsp;</asp:label></TD>
									<TD>
						                <input id="txtfineperday2" runat="server" class="txt10" maxlength="15" 
                                            name="txtfineperday2" onblur="this.className='blur'" 
                                            onfocus="this.className='focus'" onkeypress="decimalNumber(this)" size="5" 
                                             type="text" />
                                        </TD>
								    <td>
                                        <input id="txtfineperdayJ2" runat="server" class="txt10" maxlength="15" 
                                            name="txtfineperdayJ2" onblur="this.className='blur'" 
                                            onfocus="this.className='focus'" onkeypress="decimalNumber(this);" size="5" 
                                            style="<%$ Resources:ValidationResources, TextBox2 %>" type="text" /></td>
								</TR>
                                </table>
								 
									<div class="allgriddivmid">
                                        <asp:Label ID="lblStatus" runat="server" Visible="false"
                                            Text="<%$ Resources:ValidationResources, IssueCat %>" >&nbsp;&nbsp;</asp:Label>
                                    
										<asp:DataGrid ID="grdDetail" CssClass="allgrid GenTable1" Width="100%" runat="server" AutoGenerateColumns="False" OnItemCommand="grdDetail_ItemCommand"
                                            Font-Names="<%$ Resources:ValidationResources, TextBox1 %>" >
                                            <Columns>
                                                <asp:TemplateColumn>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="Chkselect" runat="server"  />
                                                        <asp:HiddenField ID="hdnCate" runat="server" Value="X" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="Category_LoadingStatus" 
                                                    HeaderText="<%$ Resources:ValidationResources, LCat %>"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="id" 
                                                    HeaderText="<%$ Resources:ValidationResources, GrId %>" Visible="False">
                                                </asp:BoundColumn>
                                                <asp:ButtonColumn CommandName="Policy" 
                                                    HeaderText="<%$ Resources:ValidationResources, GrPolicy %>" Text="Policy">
                                                </asp:ButtonColumn>
                                            </Columns>
                                        </asp:DataGrid>
									</div>
									 
								<table class="table-condensed no-more-tables GenTable1">
								<TR>
									
									<TD colSpan="3" style="text-align:center" >
                                      
                                                    <%--<input id="cmdsubmit" runat="server" class="btnstyle" name="cmdsubmit" onclick=" if ( validateform()==false ) return false; else true ;"
                                                        type="button" 
                                                        value="<%$ Resources:ValidationResources, bSave %>" />
                                                    </input>--%>
                              <asp:Button Id="cmdsubmit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="cmdsubmit_Click"/>                  
                                                    <%--<input id="cmdreset" runat="server" class="btnstyle" name="cmdreset" 
                                                        type="button" 
                                                        value="<%$ Resources:ValidationResources, bReset %>" />
                                                    </input>--%>
                             <asp:Button Id="cmdreset" runat="server" CssClass="btn btn-primary" Text="Reset" OnClick="cmdreset_Click"/>                    
                                                    <%--<input id="cmddelete" runat="server" class="btnstyle" name="cmddelete" 
                                                        onclick="if (DoConfirmation() == false) return false;" 
                                                        type="button" value="<%$ Resources:ValidationResources, bdelete %>" />
                                                    </input>--%>
                             <asp:Button Id="cmddelete" runat="server" CssClass="btn btn-primary" Text="Delete" OnClick="cmddelete_Click"/>                    
                                    </TD>
								</TR>
								<TR>
									<TD  colSpan="3"><asp:Label ID="Label14"  CssClass="showBoldExist" runat="server" 
                                             Text="<%$ Resources:ValidationResources, MGDetail %>" 
                                          ></asp:Label>
                                    </TD>
                                    </TR>
                                   
							</TABLE>
                                          
                                    <div class="allgriddiv">
                                           
                                    <asp:DataGrid ID="DataGrid1" runat="server" CssClass="allgrid GenTable1"   AllowPaging="false" 
                                            AutoGenerateColumns="False" OnItemCommand="DataGrid1_ItemCommand" OnPageIndexChanged="DataGrid1_PageIndexChanged"
                                            Font-Names="<%$ Resources:ValidationResources, TextBox1 %>" Width="100%">
                                            <Columns>
                                                <asp:ButtonColumn CommandName="Select" DataTextField="classname" 
                                                    HeaderText="<%$ Resources:ValidationResources, MembGp %>" Text="Select">
                                                </asp:ButtonColumn>
                                                <asp:BoundColumn DataField="classname" 
                                                    HeaderText="<%$ Resources:ValidationResources, GrMemGrp %>" Visible="False">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="ValueLimit" 
                                                    HeaderText="<%$ Resources:ValidationResources, GrValLimit %>">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="totalissueddays" 
                                                    HeaderText="<%$ Resources:ValidationResources, GrDyLmt %>" Visible="False">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="finperday" 
                                                    HeaderText="<%$ Resources:ValidationResources, GrOBCDay %>" Visible="False">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="days_1phase" 
                                                    HeaderText="<%$ Resources:ValidationResources, GrUpLmt1 %>" Visible="False">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="amt_1phase" 
                                                    HeaderText="<%$ Resources:ValidationResources, GrFine1 %>" Visible="False">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="days_2phase" 
                                                    HeaderText="<%$ Resources:ValidationResources, GrUPLmt %>" Visible="False">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="amt_2phase" 
                                                    HeaderText="<%$ Resources:ValidationResources, GrFine2 %>" Visible="False">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="reservedays" 
                                                    HeaderText="<%$ Resources:ValidationResources, GrResLmt %>" Visible="False">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="noofbookstobeissued" 
                                                    HeaderText="<%$ Resources:ValidationResources, GrBkLmt %>" Visible="False">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="totalissueddays_jour" 
                                                    HeaderText="<%$ Resources:ValidationResources, GrDyLmt %>" Visible="False">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="fineperday_jour" 
                                                    HeaderText="<%$ Resources:ValidationResources, GrOBCDay %>" Visible="False">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="days_1phasej" 
                                                    HeaderText="<%$ Resources:ValidationResources, GrUpLmt1 %>" Visible="False">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="amt_1phasej" 
                                                    HeaderText="<%$ Resources:ValidationResources, GrFine1 %>" Visible="False">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="days_2phasej" 
                                                    HeaderText="<%$ Resources:ValidationResources, GrUPLmt %>" Visible="False">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="amt_2phasej" 
                                                    HeaderText="<%$ Resources:ValidationResources, GrFine2 %>" Visible="False">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="reservedays_jour" 
                                                    HeaderText="<%$ Resources:ValidationResources, GrResLmt %>" Visible="False">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="noofjournaltobeissued" 
                                                    HeaderText="<%$ Resources:ValidationResources, GrJLmt %>" Visible="False">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="canrequest" 
                                                    HeaderText="<%$ Resources:ValidationResources, AllowRaiseInd %>">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="Status" 
                                                    HeaderText="<%$ Resources:ValidationResources, GrDeactStatus %>">
                                                </asp:BoundColumn>
                                            </Columns>
                                        </asp:DataGrid></div>
                                    </div>
                                     <input id="hcName" runat="server" name="Hidden4" style="width: 8px" 
                                                type="hidden" />
                                            <input id="txtdesignationid" runat="server" name="txtdesignationid" 
                                                style="width: 1px" type="hidden" />
                                     <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                            DisplayMode="List" Font-Names="Lucida Sans Unicode" Font-Size="X-Small" 
                                            ShowMessageBox="True" ShowSummary="False"  />
                                        <input id="hdTop" runat="server" type="hidden" />
                                            <input id="Hidden1" runat="server" style="width: 16px" type="hidden" />
                                            <input id="txtCLStatusCode" runat="server" name="Hidden4" style="width: 8px" 
                                                type="hidden" />
                                            <input id="hFill" runat="server" style="width: 8px" type="hidden" />
                                            <input id="hAct" runat="server" style="width: 16px" type="hidden" />
                                            <input id="hsName" runat="server" style="width: 16px" type="hidden" />
                                            <input id="hMaxVal" runat="server" style="width: 16px" type="hidden" />
                                            <input id="Hidden3" runat="server" name="Hidden3" style="width: 16px" 
                                                type="hidden" />
                                            <input id="yCoordHolder" runat="server" name="yCoordHolder" style="width: 16px" 
                                                type="hidden" value="0" />
                                            <input id="xCoordHolder" runat="server" name="xCoordHolder" style="width: 16px" 
                                                type="hidden" value="0" />
                                            <input id="hdflag" runat="server" style="width: 1px" type="hidden" />
                                                <input id="HdMsg" runat="server" name="HdMsg" style="WIDTH: 3px; HEIGHT: 22px" 
                                                    type="hidden" />
                                            <asp:HiddenField runat="server" ID="hdnGrdId" />    
                        <%--<input id="btnFillgrddetail" runat="server" type="button" width="0px" height="0px"
                            value="button" causesvalidation="false" class="btn" style="visibility: hidden"/>--%>

                                    <asp:Button Id="btnFillgrddetail" runat="server" CssClass="btn"  causesvalidation="false"  width="0px" height="0px" Style="display:none;" OnClick="btnFillgrddetail_Click"/>
                                                                   <br />
                                          </ContentTemplate></asp:UpdatePanel>
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
                       $('<thead></thead>').prependTo('#' + grdId).append($('#' + grdId + ' tr:first'));
                       $('#' + grdId + ' tr:first td').contents().unwrap().wrap('<th></th>');
                       ThreeLevelSearch($('#' + grdId), $('#' + grdId), 200);

                   } catch (er) {
                       alert(er + '; Make sure grid has data');
                   }

               }
               catch (err) {
               }
           }


       </script>
</asp:Content>

