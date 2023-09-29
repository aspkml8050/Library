<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/LibraryMain.master"  CodeBehind="ExchangeMaster.aspx.cs" Inherits="Library.ExchangeMaster" %>
<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>

<asp:Content id="ExchHead" runat="server" ContentPlaceHolderID="head">
    <script  type="text/javascript">
        function GetServer(getCtrlId, ctrlTo) {
            var value;
            if (document.getElementById(getCtrlId).checked) {
                document.getElementById('<%=lstAllCategory.ClientID%>').style.visibility = 'visible';
                    document.getElementById('<%=txtCategory.ClientID%>').style.visibility = 'visible';
                    document.getElementById('<%=txtCategory.ClientID%>').value = "";
                    document.getElementById('<%=chkSearch.ClientID%>').focus();
                    document.getElementById('<%=btnCategoryFilter.ClientID%>').style.visibility = 'visible';
                    document.getElementById('<%=Label8.ClientID%>').style.visibility = 'visible';
                } else {
                    document.getElementById(ctrlTo).style.visibility = 'hidden';
                    document.getElementById('<%=txtCategory.ClientID%>').value = "";
                    document.getElementById('<%=txtCategory.ClientID%>').style.visibility = 'hidden';
                    document.getElementById('<%=btnCategoryFilter.ClientID%>').style.visibility = 'hidden';
                    document.getElementById('<%=Label8.ClientID%>').style.visibility = 'hidden';
                }
                value = "chkS";
                UseCallBack(value, ctrlTo);
            }

            function clientback(result, context) {
                if (context == 'lstAllCategory') {
                    var ctrl = document.getElementById('<%=lstAllCategory%>');
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

            function setfocus() {
                document.Form1.txtcurrencyname.focus();
            }


            function check() {
                var length_shortdesc
                length_shortdesc = document.Form1.txtgocrate.value;

                length_shortdesc = parseInt(length_shortdesc, 10);
                if (length_shortdesc > 0) {

                }
                else {
                    alert("<asp:Literal runat='server' Text='<%$ Resources:ValidationResources,MSGPlsEnterNumericValue %>' />");
                document.Form1.txtgocrate.value = "";
                document.Form1.txtgocrate.focus();
            }
        }

        function chk() {


            if (document.Form1.hdTop.value == "top") {
                window.scrollTo(0, 0);
                document.Form1.hdTop.value = 0;
                document.Form1.txtcurrencyname.focus();
            }

            if (document.Form1.Hidden17.value == "109") {
                document.Form1.txtCategory.focus();
                document.Form1.Hidden17.value = 0;
            }

        }


        function validateform() {
            let v1 = $('[id$=txtcurrencyname]').val().trim();
            if (v1 == '') {
                alert('Currency Name required.');
                return false;
            }
            let v2 = $('[id$=txtshortname]').val().trim();
            if (v2 == '') {
                alert('Short Name required.');
                return false;
            }
            let v3 = $('[id$=txtgocrate]').val().trim();
            if (v3 == '') {
                alert('GOC required.');
                return false;
            }
            let v4 = $('[id$=txtbankrate]').val().trim();
            if (v4 == '') {
                alert('Bank required.');
                return false;
            }
            return true;
        }
    </script>
    

    </asp:Content>

<asp:Content ID="ExchMain" runat="server" ContentPlaceHolderID="MainContent">
     <asp:UpdateProgress ID="UpPorg1" runat="server">
    <ProgressTemplate>
    <NN:Mak ID="FF1" runat="server" />
    </ProgressTemplate>
    </asp:UpdateProgress>


    <div style="width:100%;display:none" class="title">
                  <div style="width:89%;float:left; display:none;" >
                       <asp:label id="lblTitle" runat="server" Width="100%" />        
              </div>
                  <div style="float:right;vertical-align:top"> 
                     <a id="lnkHelp" href="#" style="display:none" onclick="ShowHelp('Help/Masters-exchange rates.htm')"><img height="15" src="help.jpg" alt="Help" /></a>
                        </div></div>

   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>

  <div class="container tableborderst"> 
                                 <div class="no-more-tables" style="width:100%">
                                     <table id="Table2" class="table-condensed GenTable1">
                                         <tr>
                                             <td colspan="4">
                                                 <asp:Label ID="msglabel" runat="server" CssClass="err"></asp:Label></td>
                                         </tr>
                                         <tr>
                                             <td style="width:20%">
                                                 <asp:Label ID="Label4" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LCurrName %>" EnableViewState="False"></asp:Label></td>
                                             <td style="width:30%">
                                                 <input class="txt10" id="txtcurrencyname" onkeypress="disallowSingleQuote(this);" onblur="this.className='blur'"
                                                     onfocus="this.className='focus'" type="text" maxlength="20" size="17" name="txtcurrencyname" runat="server">
                                                 <asp:Label ID="Label1" runat="server" CssClass="star">*</asp:Label></td>
                                         
                                             <td>
                                                 <asp:Label ID="Label3" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LShortName %>" EnableViewState="False"></asp:Label></td>
                                             <td>
                                                
                                                     <input class="txt10" id="txtshortname" onblur="this.className='blur'"
                                                         onfocus="this.className='focus'" type="text" maxlength="5" size="8" name="txtshortname"
                                                         runat="server">
                                                     <asp:Label ID="Label45" runat="server" CssClass="star">*</asp:Label>
                                                
                                             </td>
                                         </tr>
                                         <tr>
                                             <td>
                                                 <asp:Label ID="Label5" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LGRate %>"></asp:Label></td>
                                             <td>
                                                 
                                                     <input class="txt10" onkeypress="decimalNumber(this)" id="txtgocrate" onblur="this.className='blur'"
                                                         onfocus="this.className='focus'" type="text" maxlength="8" size="8" name="txtgocrate"
                                                         runat="server">
                                                     <asp:Label ID="Label9" runat="server" CssClass="star" Visible="False">*</asp:Label>
                                                
                                             </td>
                                         
                                             <td>
                                                 <asp:Label ID="Label6" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LBRate %>"></asp:Label></td>
                                             <td>
                                                 <p>
                                                     <input class="txt10" onkeypress="decimalNumber(this)" id="txtbankrate" onblur="this.className='blur'"
                                                         onfocus="this.className='focus'" type="text" maxlength="8" size="8" name="txtbankrate"
                                                         runat="server">
                                                     <asp:Label ID="Label10" runat="server" CssClass="star" Visible="False">*</asp:Label>
                                                 </p>
                                             </td>
                                         </tr>
                                         <tr style="font-size: 12pt">
                                             <td>
                                                 <asp:Label ID="Label12" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LEffct %>"></asp:Label></td>
                                             <td>

                                                 <%--pushpendra singh--%>
                                                 <asp:TextBox ID="Txtdate" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                                 <%--<ajax:CalendarExtender ID="CalendarExtender1" TargetControlID="Txtdate" Format="dd/MMM/yyyy" runat="server"></ajax:CalendarExtender>--%>
                                                 <%--pushpendra singh--%>
                                                 <%--<INPUT class="txt10" id="Txtdate" onblur="this.className='blur';checkdate1(this,document.Form1.hdCulture.value,document.Form1.hrDate.value)"  width="104px;"
										onfocus="this.className='focus'" type="text" name="Txtdate" runat="server" maxlength="15"><input id="btnDate" accesskey="D" onclick="pickDate('Txtdate');" style="background-position: center center;
                                        background-image: url(cal.gif); width: 24px; height: 21px; background-color: black"
                                        type="button" />--%>






                                                 <asp:Label ID="Label7" runat="server" CssClass="star">*</asp:Label></td>
                                             <td colspan="2"></td>
                                         </tr>
                                         <tr>
                                             <td>
                                                <asp:CheckBox ID="chkSearch" runat="server" AutoPostBack="True" OnCheckedChanged="chkSearch_CheckedChanged"
                                                     Text=" " />
                                                 <asp:Label ID="Label29"
                                                         runat="server" CssClass="opt" Text="<%$ Resources:ValidationResources,bSearch %>"></asp:Label>
                                                 </td>
                                             <td colspan="3" style="text-align: center">
                                                 <%--<input id="cmdsave" type="button" value="<%$ Resources:ValidationResources,bSave %>" name="cmdsave" onclick =" if ( validateform()==false ) return false; else true ;"
                                                     runat="server" accesskey="S" class="btnstyle">--%>
                                                <asp:Button ID="cmdsave" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="cmdsave_Click" />
                                                 
                                                 
                                                 <%--<input id="cmdreset" type="button" value="<%$ Resources:ValidationResources,bReset %>" name="cmdreset"
                                                     runat="server" accesskey="E" class="btnstyle">--%>
                                                 <asp:Button id="cmdreset" runat="server" CssClass="btn btn-primary" Text="Reset" Onclick="cmdreset_Click"/>
                                                 <%--<input id="cmddelete" onclick="if (DoConfirmation() == false) return false;"
                                                     type="button" value="<%$ Resources:ValidationResources,bDelete %>" name="cmddelete" runat="server" accesskey="X" class="btnstyle">--%>

                                                 <asp:Button id="cmddelete" runat="server" CssClass="btn btn-primary" Text="Delete" OnClick="cmddelete_Click"/>
                                             </td>
                                         </tr>
                                         <tr>
                                           
                                             <td colspan="4">
                                                 <table id="Table4" class="table-condensed">
                                                     <tr>
                                                         <td style="vertical-align: middle">
                                                             <asp:Label ID="Label8" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,LCurrency %>"> 

                                                             </asp:Label>

                                                         </td>
                                                         <td>
                                                             <input id="txtCategory" onblur="this.className='blur'" onfocus="this.txtCategory='focus'"
                                                                 type="text" size="12" name="txtCategory" runat="server" visible="true" class="txt10" style="margin-top: 10px">
                                                         </td>
                                                         <td>
                                                            <%-- <input id="btnCategoryFilter" type="button" value="<%$ Resources:ValidationResources,bSearch %>"
                                                                 name="btnCategoryFilter" runat="server" class="btnstyle">--%>
<asp:Button id="btnCategoryFilter" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnCategoryFilter_Click"/>

                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td></td>
                                                         <td colspan="2">
                                                             <asp:ListBox ID="lstAllCategory" AutoPostBack="true" onblur="this.className='blur'" OnSelectedIndexChanged="lstAllCategory_SelectedIndexChanged1" onfocus="this.className='focus'" runat="server" CssClass="txt10" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>' Style="margin-top: 10px; height: 100px !important"></asp:ListBox>
                                                         </td>
                                                     </tr>
                                                 </table>
                                             </td>

                                         </tr>


                                     </table></div> </div>
                                <asp:Button id="Button1" runat="server"  style="WIDTH: 1px; HEIGHT: 4px" class="btnH" Text="Button" OnClick="Button1_ServerClick"/>
        <%--<INPUT id="Button1" style="WIDTH: 1px; HEIGHT: 4px" class="btnH" type="button" value="Button" name="Button1" 
							runat="server">--%>
                                <INPUT id="txtcurrencycode"  width="28px;"
							 type="hidden" name="txtcurrencycode" runat="server"><INPUT id="Hidden3" style="WIDTH: 16px; HEIGHT: 22px" type="hidden" size="1" name="Hidden3"
							runat="server"><INPUT id="Hidden5" style="WIDTH: 16px; HEIGHT: 22px" type="hidden" size="1" name="Hidden2"
							runat="server"><INPUT style="WIDTH: 20px; HEIGHT: 22px" type="hidden"><INPUT id="Hidden6" style="WIDTH: 33px; HEIGHT: 22px" type="hidden" name="Hidden6"
							runat="server"><INPUT id="Hidden2" type="hidden" name="Hidden2" runat="server" DESIGNTIMEDRAGDROP="11" style="width: 18px"><INPUT id="Hidden17" type="hidden" runat="server" style="width: 12px"><INPUT id="hdTop" style="WIDTH: 12px; HEIGHT: 22px" type="hidden" size="1" runat="server"><input id="hdCulture" runat="server" style="width: 22px" type="hidden" /><input id="hrDate"
                            runat="server"  type="hidden"
                            value="<%$ Resources:ValidationResources, dateFormat1 %>" style="width: 23px" /><INPUT id="hdUnableMsg" style="WIDTH: 16px; HEIGHT: 22px" type="hidden" runat="server"><asp:listbox id="hlstAllCategory" runat="server" Width="1px" class="btnH" Height="1px"></asp:listbox><INPUT id="hSubmit1" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" value="0"
							name="hSubmit1" runat="server"><INPUT id="xCoordHolder" style="WIDTH: 16px; HEIGHT: 22px" type="hidden" name="Hidden2"
							runat="server"><INPUT id="yCoordHolder" type="hidden" name="Hidden2" runat="server" style="width: 15px">
                                    <INPUT id="Hidden1" style="WIDTH: 32px; HEIGHT: 24px" type="hidden" name="Hidden1"
							runat="server">
                        <INPUT id="Hidden4" style="WIDTH: 16px; HEIGHT: 22px" type="hidden" size="1" name="Hidden1"
							runat="server" DESIGNTIMEDRAGDROP="111">
					<asp:label id="Label2" runat="server" Width="104px" ForeColor="Navy" Font-Size="X-Small" Visible="False"
							Font-Names="<%resources: ValidationResources, TextBox1 %>" Text ="<%$ Resources:ValidationResources, LCurCode%>"></asp:label>
					
				
			 
    <script type="text/javascript">
        //On Page Load.
        $(function () {
            SetDatePicker();
        });


        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e)
            {
                if (sender._postBackSettings.panelsToUpdate != null)
                {
                    SetDatePicker();
                }
            });
        };

        function SetDatePicker() {
            $("[id$=Txtdate]").datepicker({
                changeMonth: true,//this option for allowing user to select month
                changeYear: true, //this option for allowing user to select from year range
                dateFormat: 'dd-M-yy'  // CHANGE DATE FORMAT.
            });

        }

    </script>

                            </ContentTemplate>
                        </asp:UpdatePanel>

    </asp:Content>
