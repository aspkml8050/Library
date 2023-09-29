<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Item_master.aspx.cs"  MasterPageFile="~/LibraryMain.master" Inherits="Library.Item_master" %>


<asp:Content ID="Head" runat="server" ContentPlaceHolderID="head">
    <script language="JavaScript" src="datetimepicker.js" type="text/javascript"></script><script language="JavaScript" src="datetimepicker1.js" type="text/javascript"></script><script language="JavaScript" src="datetimepicker2.js" type="text/javascript"></script>
    <script type="text/javascript" >
        function onTextBoxUpdate(evt) {
            var textBoxID = evt.source.textBoxID;
            if (evt.selMenuItem != null) {
                document.getElementById(textBoxID).value = evt.selMenuItem.label; // + " (modified by onTextboxUpdate)";
                document.getElementById("cmdsearch").click();
            }
            evt.preventDefault();
        }
        function CallPub2() {
            let hdid = $('[id$=hdnPublId]').attr('id');
            let txt = $('[id$=txtCmbPublisher]').attr('id');
            window.open("PublisherMaster.aspx?title=From Catalog&caller=child&hdid=" + hdid + "&text=" + txt, "Publisher Master", "height=700px,width=800px");

        }
        function CallPub3() {
            let hdid = $('[id$=hdnVendorId]').attr('id');
            let txt = $('[id$=txtCmbVendor]').attr('id');
            window.open("VendorMaster.aspx?title=From Catalog&caller=child&hdid=" + hdid + "&text=" + txt, "Vendor Master", "height=700px,width=800px");

        }
          </script>
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
            $("[id$=txtFromdate],[id$=txtdateto]").datepicker({
                changeMonth: true,//this option for allowing user to select month
                changeYear: true, //this option for allowing user to select from year range
                dateFormat: 'dd/M/yy'  // CHANGE DATE FORMAT.
            });

        }
        function openNewForm(btnName, formName, hdCtrl, hdctrlFill, permission) {
          
            var s = document.getElementById(permission).value;
          
            if (document.getElementById(permission).value == "Y") {
                var tmpStr = formName + ".aspx";
                var retId;
                document.getElementById(hdctrlFill).value = formName;
                if (navigator.appName == "Microsoft Internet Explorer") {

                    retId = window.showModalDialog(tmpStr, "DWindow", "status:no;dialogWidth:800px;dialogHeight:800px;dialogHide:true;help:no;scroll:yes;");
                    if ("undefined" != typeof (retId)) {
                        SetNameSC(retId.strName);
                    }

                }
                else if (navigator.appName == "Netscape") {
                    retId = window.open(tmpStr, "DWindow", "modal=yes,status=no,width=800px,height=800px,hide=true,help=no,scrollbars=yes,screenX=100px,screenY=100px");
                    //retId.focus();
                } else if (navigator.appName == "Opera") {
                    retId = window.open(tmpStr, "DWindow", "modal=yes,status=no,width=800px,height=800px,hide=true,help=no,scrollbars=yes,toolbars=no,screenX=200px,screenY=300px");
                    //retId.focus();
                } else {
                    retId = window.open(tmpStr, "DWindow", "modal=yes,status=no,width=800px,height=800px,hide=true,help=no,scrollbars=yes,screenX=100px,screenY=100px");
                }

                return false;
             
            }
            else {
                alert("You are not allowed to perform this operation.");
            }
        }

</script>
    <script type="text/javascript">

    function chk() {
        if (document.Form1.HdTransaction.value == "Top") {
            window.scrollTo(0, 0);
            document.Form1.HdTransaction.value = 0;
        }

    }
    function txtsearch_OnKeyDown() {
        if (window.event.keyCode == 13) {
            window.document.Form1.cmdsearch.focus();
        }
    }
		</script>
		
		<script language="javascript" type="text/javascript">
            window.history.forward(1);
        </script>
</asp:Content>

<asp:Content ID="Main" runat="server" ContentPlaceHolderID="MainContent">

                <div class="container tableborderst" style="width:60%;margin-top:20px">   
         <div class="no-more-tables" style="width:100%">
			 <div style="width:100%;display:none" class="title">
                  <div style="width:89%;float:left" >
                      <asp:label id="lblTitle" runat="server"  Width="100%"></asp:label>
                      </div>
                  <div style="float:right;vertical-align:top">
                        <a id="lnkHelp" href="#" onclick="ShowHelp('Help/Serials-Journal Accessioning-Existing – Bound Journal Accessioning.htm')"><img src="help.jpg"  height="15" /></a>
              </div></div>

              <asp:UpdatePanel id="UpdatePanel1" runat="server">
                            <contenttemplate>
	<div class="no-more-tables">
						<TABLE id="Table1" class="table-condensed GenTable1">
							<TR>
								<TD colSpan="4"><asp:label id="msglabel" runat="server" CssClass="err" Width="100%" Visible=false ></asp:label></TD>
							</TR>
							<TR>
								<TD><asp:label id="Lbltitle1" runat="server" CssClass="span" Text ="<%$ Resources:ValidationResources, LTitle%>"></asp:label></TD>
								<TD  colSpan="3">
                                    <asp:TextBox ID="txtcmbJTitle" runat="server" Columns="30" CssClass="txt10" MaxLength="100" onblur="this.className='blur'" onfocus="this.className='focus'" Style="<%$resources: ValidationResources, TextBox2 %>" ></asp:TextBox>
                                    </TD>
							</TR>
							<TR>
								<TD>
                                    <asp:Label ID="Lbltitle2" runat="server" CssClass="span" 
                                        Text="Sub Title" ></asp:Label>
                                    </TD>
								<TD colSpan="3">
                                    <asp:TextBox ID="txtcmbJTitle0" runat="server" Columns="30" CssClass="txt10" 
                                        MaxLength="100" onblur="this.className='blur'" onfocus="this.className='focus'" 
                                        ></asp:TextBox>
                                    </TD>
							</TR>
                           
                            <tr>
                                <td>
                                    <asp:Label ID="Label3" runat="server" CssClass="span"  Text ="Publication Day"></asp:Label></td>
                                <td>




                                    
<%--pushpendra singh--%>
 <asp:TextBox ID="txtFromdate" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />

                                </td>
                                <td>
                                    </td>
                                <td >
                                   </td>
                            </tr>
							<TR>
								<TD >
                                    <asp:Label ID="Label7" runat="server" CssClass="span"
                                        Text="<%$ Resources:ValidationResources, LISSNNo %>" ></asp:Label>
                                </TD>
								<TD>
                                    <input id="txtIssueN" runat="server" class="txt10" name="txtVolume" 
                                        onblur="this.className='blur'" onfocus="this.className='focus'" />
                                        
                                    </input>
                                </TD>
								<TD >
                                    <asp:Label ID="Lbltoissue" runat="server" CssClass="span"   Text="Issue" ></asp:Label>
                                </TD>
								<TD >
                                    <input id="txtIssue" runat="server" class="txt10" name="txtVolume3" 
                                        onblur="this.className='blur'" onfocus="this.className='focus'" size="12" 
                                        /></input></TD>
							</TR>
							<TR>
								<TD >
                                    <asp:Label ID="lblvol" runat="server" CssClass="span"  Text="Volume"></asp:Label>
                                </TD>
								<TD >
                                    <input id="txtVolume" runat="server" class="txt10" name="txtVolume1" 
                                        onblur="this.className='blur'" onfocus="this.className='focus'" size="12" />
                                       
                                    </input>
                                </TD>
								<TD >
                                    <asp:Label ID="Lblpart" runat="server" CssClass="span"  Text="<%$ Resources:ValidationResources, LPartNo %>" ></asp:Label>
                                </TD>
								<TD >
                                   <input id="txtpart" runat="server" class="txt10" name="txtPart" 
                                        onblur="this.className='blur'" onfocus="this.className='focus'" /></input></TD>
							</TR>
                            <tr>
                                <td >
                                    <asp:Label ID="Label141" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources, CpyNo %>" ></asp:Label>
                                </td>
                                <td >
                                  <input ID="txtCpyN" runat="server" class="txt10" name="txtVolume0" 
                                        onblur="this.className='blur'" onfocus="this.className='focus'" 
                                        style="<%$ Resources:ValidationResources, TextBox2 %>;" /></input></td>
                                <td >
                                    <asp:Label ID="Label6" runat="server" CssClass="span" 
                                        Text="<%$ Resources:ValidationResources, LLackN %>" ></asp:Label>
                                </td>
                                <td >
          <input id="Txtlackno" runat="server" class="txt10" name="Txtlackno" 
                                        onblur="this.className='blur'" onfocus="this.className='focus'" size="12" />
                                    </input>
                                </td>
                            </tr>
							<tr>
                                <td>
                                    <asp:Label ID="Label129" runat="server" CssClass="span"  Text="<%$ Resources:ValidationResources,LEdition %>"></asp:Label>
                                </td>
                                <td>
                                    <input id="txtedition" runat="server" onblur="this.className='blur'" 
                                        onfocus="this.className='focus'" 
                                        style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid; " 
                                        type="text"/>
                                    </input>
                                </td>
                                <td colspan="1">
                                    <asp:Label ID="Label130" runat="server" CssClass="span"  Text="<%$ Resources:ValidationResources,LEditionY %>" ></asp:Label>
                                </td>
                                    <td >
                                        <input id="txteditionyear" runat="server" maxlength="4" 
                                            onblur="this.className='blur'" onfocus="this.className='focus'" 
                                            onkeypress="IntegerNumber(this);" 
                                           
                                            type="text"/>
                                        </input>
                                    </td>
                            </tr>
							<tr>
                                <td>
                                    <asp:Label ID="HypLang" runat="server" Text="<%$ Resources:ValidationResources,LLanguage %>"></asp:Label>
                               
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmbLanguage" runat="server" CssClass="txt10" Height="30" 
                                        onblur="this.className='blur'" onfocus="this.className='focus'" >
                                    </asp:DropDownList>
                                </td>
                                <td colspan="2">
                                  </td>
                            </tr>
							<tr>
							    <td colspan="1" >
                                    <input id="hdnPublId" type="hidden" runat="server" value="" />
                                  <asp:LinkButton ID="lnkPubl" runat="server" Text="Publisher" CssClass="lnkB" ForeColor="Blue" Font-Underline="true" Font-Bold="true" ToolTip="Enter Publisher in Master with Details." OnClientClick="CallPub2();"></asp:LinkButton>
                                
                                 
                                </td>
                                <td colspan="3" >
                                   <%-- <Custom1:AutoSuggestBox ID="txtCmbPublisher" runat="server" 
                                        AutoPostBack="false" BorderWidth="1px" Columns="30" CssClass="FormCtrl" 
                                        DataType="City"  MaxLength="100" ResourcesDir="asb_includes" 
                                       ></Custom1:AutoSuggestBox>--%>
                                </td>
							</tr> 
							<tr>
                                <td colspan="1" >
                                    <input id="hdnVendorId" type="hidden" runat="server" value="" />
                                      <asp:LinkButton ID="lnkVendor" runat="server" Text="Vendor" CssClass="lnkB" ForeColor="Blue" Font-Underline="true" Font-Bold="true" ToolTip="Enter Vendor in Master with Details." OnClientClick="CallPub3();"></asp:LinkButton>
                                   <%-- <asp:HyperLink ID="hyper68" runat="server" 
                                        onclick="openNewForm('btnFillPub','VendorMaster','HNewForm','HWhichFill','HCondition');" 
                                        Text="<%$ Resources:ValidationResources,LVen %>"></asp:HyperLink>--%>
                                </td>
                                <td colspan="3">
                                    <%--<Custom1:AutoSuggestBox ID="txtCmbVendor" runat="server" AutoPostBack="false" 
                                        BorderWidth="1px" Columns="30" CssClass="FormCtrl" DataType="City" 
                                          MaxLength="100" ResourcesDir="asb_includes"></Custom1:AutoSuggestBox>--%>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="1" >
                                    <asp:Label ID="Label13" Text="<%$ Resources:ValidationResources,LCurrency %>" runat="server"></asp:Label>
                                  <%--  <asp:HyperLink ID="Label13" runat="server" 
                                        onclick="openNewForm('btnFillPub','ExchangeMaster','HNewForm','HWhichFill','HCondition');" 
                                        Text="<%$ Resources:ValidationResources,LCurrency %>"></asp:HyperLink>--%>
                                </td>
                                <td colspan="1">
                                    <asp:DropDownList ID="cmbcurr" runat="server" CssClass="txt10" Height="30" 
                                        Font-Names="<%$ Resources:ValidationResources, TextBox1 %>"  
                                          onblur="this.className='blur'" 
                                        onchange="GetServer(this.id,'txtExchangeRate')" 
                                        onfocus="this.className='focus'" >
                                    </asp:DropDownList>
                                </td>
                                    <td>
                                        <asp:Label ID="lbl35" runat="server" 
                                            Text="<%$ Resources:ValidationResources,LPrc %>" ></asp:Label>
                                    </td>
                                    <td >
                                        <input ID="txtForeignPrice" runat="server" class="txt10" maxlength="7" 
                                            name="txtForeignPrice" onblur="this.className='blur'" 
                                            onfocus="this.className='focus'" onkeypress="decimalNumber(this);" 
                                            onpropertychange="txtForeignPrice_OnPropertyChange();" 
                                            
                                            type="text" /></td>
                            </tr>
							
                            <tr>
                                <td  colspan="4" style="text-align:center">
                                   
                              
                                                <%--<input ID="cmdadd" runat="server" class="btnstyle" name="cmdadd" 
                                                    type="button" value="<%$ Resources:ValidationResources, bSave %>"/></input>--%> 
                        <asp:Button Id="cmdadd" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="cmdadd_Click"/>            
                                           
                                                <%--<input ID="cmdreset" runat="server" class="btnstyle" name="cmdreset" 
                                                     type="button" 
                                                    value="<%$ Resources:ValidationResources, bReset %>"/> </input>--%>
                                    <asp:Button id="cmdreset" runat="server" CssClass="btn btn-primary" Text="Reset" OnClick="cmdreset_Click"/>
                                            </td>
                                        </tr>
                                 
                               
                            <tr>
                                <td colspan="2">
                                    
                                    <asp:CheckBox ID="chkSearch1" runat="server" AutoPostBack="True" Text="Search" OnCheckedChanged ="chkSearch1_CheckedChanged" />
                                </td>
                                <td colspan="2" >
                                   </td>
                            </tr>
                            <tr><td  colspan="1">
                                    <asp:Label ID="Lblsearch" runat="server" CssClass="span"  
                                        Text="Select Title" ></asp:Label>
                                </td>
                                
                                <td colspan ="3">
                                    
                                    <asp:DropDownList ID="DropDownList1" runat="server" Height="30" AutoPostBack="true" OnSelectedIndexChanged ="DropDownList1_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
          
                            </tr>
                           
		
							
							
						</TABLE>
                                </div>	
						</contenttemplate>
                                   
                        </asp:UpdatePanel>

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
                      $("[id$=txtFromdate]").datepicker({
                          changeMonth: true,//this option for allowing user to select month
                          changeYear: true, //this option for allowing user to select from year range
                          dateFormat: 'dd-M-yy'  // CHANGE DATE FORMAT.
                      });

                  }
                  function openNewForm(btnName, formName, hdCtrl, hdctrlFill, permission) {

                      var s = document.getElementById(permission).value;

                      if (document.getElementById(permission).value == "Y") {
                          var tmpStr = formName + ".aspx";
                          var retId;
                          document.getElementById(hdctrlFill).value = formName;
                          if (navigator.appName == "Microsoft Internet Explorer") {

                              retId = window.showModalDialog(tmpStr, "DWindow", "status:no;dialogWidth:800px;dialogHeight:800px;dialogHide:true;help:no;scroll:yes;");
                              if ("undefined" != typeof (retId)) {
                                  SetNameSC(retId.strName);
                              }

                          }
                          else if (navigator.appName == "Netscape") {
                              retId = window.open(tmpStr, "DWindow", "modal=yes,status=no,width=800px,height=800px,hide=true,help=no,scrollbars=yes,screenX=100px,screenY=100px");
                              //retId.focus();
                          } else if (navigator.appName == "Opera") {
                              retId = window.open(tmpStr, "DWindow", "modal=yes,status=no,width=800px,height=800px,hide=true,help=no,scrollbars=yes,toolbars=no,screenX=200px,screenY=300px");
                              //retId.focus();
                          } else {
                              retId = window.open(tmpStr, "DWindow", "modal=yes,status=no,width=800px,height=800px,hide=true,help=no,scrollbars=yes,screenX=100px,screenY=100px");
                          }

                          return false;
                          //strReturn=window.showModalDialog(tmpStr,"DWindow","status:no;dialogWidth:800px;dialogHeight:400px;dialogHide:true;help:no;scroll:yes;"); 
                          //if(strReturn != null){ 
                          //document.getElementById(hdctrlFill).value=formName;
                          //document.getElementById(hdCtrl).value=strReturn;
                          //__doPostBack(btnName,'');

                          //}
                      }
                      else {
                          alert("You are not allowed to perform this operation.");
                      }
                  }

              </script>
             </div></div>

    <input id="Hidden3" runat="server" name="Hidden2" size="1" type="hidden"/>
                                        <input id="Hidden2" runat="server" name="Hidden2" size="1" type="hidden"/>
                                    <input id="xCoordHolder" runat="server" name="xCoordHolder" size="1" 
                                        style="WIDTH: 8px; HEIGHT: 12px" type="hidden" value="0"/>
                                        <input id="yCoordHolder" runat="server" name="yCoordHolder" 
                                            style="WIDTH: 16px; HEIGHT: 16px" type="hidden" value="0"/>
                                            <input id="Hidden1" runat="server" name="Hidden1" 
                                                style="WIDTH: 8px; HEIGHT: 24px" type="hidden"/>
                                                <input id="HdTransaction" runat="server" name="HdTransaction" 
                                                    style="WIDTH: 8px; HEIGHT: 22px" type="hidden"/>
                                                    <input id="hidden6" runat="server" name="hidden6" 
                                                        style="WIDTH: 1px; HEIGHT: 22px" type="hidden" value="0"/>
                                                        <input id="hCurrentIndex2" runat="server" name="hCurrentIndex2" 
                                                            style="WIDTH: 8px; HEIGHT: 22px" type="hidden"/>
                                                            <input id="Hdexistaccession" runat="server" style="width: 8px" type="hidden" />
                                                            <input id="hddoc_id" runat="server" style="width: 22px" type="hidden" />
                                                            <input id="HdBind" runat="server" style="width: 29px" type="hidden" />
                                                            <p align="center">
                                                            </p>
                                                            <asp:Button ID="cmdsearch" runat="server" BackColor="White" BorderStyle="None"                      CssClass="btn btn-primary" CausesValidation="False" ForeColor="White" Visible="false" Text="Go" UseSubmitBehavior="False" />
                                                            <input id="hdJournalTitle" runat="server" style="width: 1px" type="hidden" />
                                                            <input id="Hidden5" runat="server" style="width: 8px" type="hidden" />
                                                            <input id="txtstartno" runat="server" name="txtstartno" 
                                                                style="WIDTH: 1px; HEIGHT: 22px" type="hidden"/>
                                                                <input id="hdmacc" runat="server" style="width: 80px" type="hidden" />
                                                                <input id="HDDeptcodeForPub" runat="server" style="width: 39px" type="hidden" />
                                                                <input id="HDJournalTitleNew" runat="server" type="hidden" />
                                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                                                    DisplayMode="List" Height="10px" ShowMessageBox="True" ShowSummary="False" 
                                                                    Width="144px" />
                                                                <input id="hdacc" runat="server" style="width: 64px" type="hidden" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                                                                    ControlToValidate="txtcmbJTitle" Display="None" EnableViewState="False" 
                                                                    ErrorMessage="<%$Resources:ValidationResources,SlctTitle %>" Font-Size="11px" 
                                                                    SetFocusOnError="True" Width="128px"></asp:RequiredFieldValidator>
                                                                &nbsp;
                                                                <input id="Hdissue" runat="server" name="Hdissue" size="1" 
                                                                    style="WIDTH: 16px; HEIGHT: 13px" type="hidden"/>&nbsp;
                                                                    <input id="hSubmit1" runat="server" name="hSubmit1" 
                                                                        style="WIDTH: 8px; HEIGHT: 22px" type="hidden" value="0"/>
                                                                        <input id="Button1" runat="server" class="btnH" name="Button1"
                                                                            style="width: 1px; height: 1px" type="button" value="Button" />
                    <asp:TextBox ID="Txtlackno1" runat="server" Height="10px" Visible="False" 
                                              Width="8px"></asp:TextBox>
           <asp:ListBox ID="hlstAllCategory" Visible="false" runat="server" >
                        </asp:ListBox>
              <input id="txtaccid" runat="server" name="txtaccid" onblur="this.className='blur';checkdate1(this,document.Form1.hdCulture.value,document.Form1.hrDate.value)" onfocus="this.className='focus'" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; WIDTH: 1px; BORDER-BOTTOM: black 1px solid; HEIGHT: 21px" visible="false"/>
                                                                        <input id="HComboSelect" runat="server" style="width: 1px" type="hidden" 
                                                                            value="<%$ Resources:ValidationResources, ComboSelect %>" />
                                                                        <input id="Hidden4" style="width: 1px" type="hidden" />
                                                                        <input id="hdCulture" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hrDate" runat="server" style="width: 1px" type="hidden" 
                                                                            value="<%$ Resources:ValidationResources, dateFormat1 %>" />
                                                                        <input 
                                                                            style="Z-INDEX: 101; LEFT: 536px; WIDTH: 24px; POSITION: absolute; TOP: -56px; HEIGHT: 24px" 
                                                                            type="hidden"/>
</asp:Content>