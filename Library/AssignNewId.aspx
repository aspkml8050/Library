<%@ Page Language="vb" MasterPageFile="~/LibraryMain.master" AutoEventWireup="false" CodeFile="AssignNewId.aspx.vb" Inherits="CircBookIssue1"%>
<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>


<asp:Content id="Head" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
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
    <script  type="text/javascript">
        function GetVendJs(sender, arg) {
            let id = arg.get_value();
            alert(id);
        }
    </script>
</asp:Content>

<asp:Content ID="Main" runat="server" ContentPlaceHolderID="MainContent">
      <asp:UpdateProgress ID="UpPorg1" runat="server">
    <ProgressTemplate>
    <NN:Mak ID="FF1" runat="server" />
    </ProgressTemplate>
    </asp:UpdateProgress>
          <div class="container tableborderst" >   
        
			 <div style="width:100%;display:none" class="title">
                  <div style="width:89%;float:left" >&nbsp;
                       <asp:Label ID="lblTitle" style="display:none"  runat="server" ></asp:Label>
                      </div><div style="float:right;vertical-align:top"> 
                        <a id="lnkHelp" href="#"  style="display:none" onclick="ShowHelp('Help/Circulation-MemberMngt-Edit-MemberID.htm')">
                            <img height="15" alt="Help" src="help.jpg" /></a>
              </div></div>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <%--<Triggers>
						<asp:PostBackTrigger  ControlID="DataGrid1" />
						</Triggers>--%>
                            <ContentTemplate>
                                         <asp:TextBox ID="txtCmbMemid" runat="server"  />
           <ajax:AutoCompleteExtender ID="ExtVend" runat="server" TargetControlID="txtCmbMemid"
                             CompletionInterval="50"
                             CompletionSetCount="50"
                             MinimumPrefixLength="0"
                             CompletionListCssClass="Publ"
                             ServicePath="MssplSugg.asmx"
                             ServiceMethod="GetMemberID"
                             OnClientItemSelected="GetVendJs">
                         </ajax:AutoCompleteExtender>
                         <asp:HiddenField ID="hdUid" runat="server" />
                                <asp:Button ID="btntst" runat="server" Text="testig" OnClick="btntst_Click" />
                                <table id="Table1" class="no-more-tables table-condensed GenTable1" >
                                    <tr>
                                        <td colspan="4">
                                            <asp:Label ID="msglabel" runat="server" CssClass="err"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td><asp:Label ID="Label1" runat="server"  CssClass="span" Text="<%$ Resources:ValidationResources, MembID %>"></asp:Label></td>
                                        <td>
                                            <asp:TextBox Width="60%" ID="txtuseridBIE" onkeydown="txtuseridBIE_OnKeyDown();" onkeypress="disallowSingleQuote(this);"
                                                onblur="this.className='blur'" onfocus="this.className='focus'" runat="server" CssClass="txt10"
                                               BorderWidth="1px"></asp:TextBox>
                                            <input id="cmdcheck" type="button" value="<%$ Resources:ValidationResources, bEnter %>" name="cmdcheck"
                                                runat="server" onserverclick="cmdcheck_ServerClick" class="btnstyle">
                                        </td>
                                        <td ></td>
                                    </tr>
                                    <tr>
                                        <td><asp:Label ID="Label2" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources, MembName %>"></asp:Label></td>
                                        <td>
                                            <input id="txtusername" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="text" name="txtusername" runat="server" class="txt10" size="45"></td>
                                        <td style="vertical-align:middle;text-align:center" rowspan="6">
                                            <asp:Image ID="Image1" runat="server" BorderColor="Black" BorderWidth="1px" 
                                                BorderStyle="Solid"></asp:Image></td>
                                    </tr>
                                    <tr>
                                        <td><asp:Label ID="Label4" runat="server" Text="<%$ Resources:ValidationResources, LFathName %>"></asp:Label></td>
                                        <td>
                                            <input id="txtfathername" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" type="text" name="txtusername" runat="server" class="txt10" size="45"></td>
                                    </tr>
                                    <tr>
                                        <td style="width:23%"><asp:Label ID="Label3" runat="server" CssClass="span"  Text="<%$ Resources:ValidationResources, LDeptm %>"></asp:Label></td>
                                        <td>
                                            <input id="txtDept" runat="server" class="txt10" name="txtDept" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" readonly="readonly"
                                                type="text" size="45" /></td>
                                    </tr>
                                    <tr>
                                        <td><asp:Label ID="Label10" runat="server" CssClass="span"  Text="<%$ Resources:ValidationResources, MembGp %>"></asp:Label></td>
                                        <td>
                                            <input id="txtMemberGroup" runat="server" class="txt10" name="txtDept" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" readonly="readonly"
                                                type="text" size="45" /></td>
                                    </tr>
                                    <tr>
                                        <td><asp:Label ID="Label6" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources, LCrsDesig %>"></asp:Label></td>
                                        <td>
                                            <input id="txtcourse" runat="server" class="txt10" name="txtDept" onblur="this.className='blur'"
                                                onfocus="this.className='focus'" readonly="readonly"
                                                type="text" size="45" /></td>
                                    </tr>
                                    <tr>
                                        <td><asp:Label ID="Label7" runat="server"  CssClass="span"
                                            Text="<%$ Resources:ValidationResources, NewID %>" BackColor="#FFFF66"></asp:Label></td>
                                        <td>
                                            <input id="txtaccno" class="txt10"  onkeydown="txtaccno_OnKeyDown();" onkeypress="disallowSingleQuote(this);"
                                                onblur="this.className='blur'" style="width:90%"
                                                onfocus="this.className='focus'" type="text" name="txtaccno" runat="server" >
                                        </td>
                                        <td></td>
                                         </tr>
                                    <tr>
                                        <td colspan="3" style="text-align:center">
                                            
                                            <input id="cmdsubmit" type="button" value="<%$ Resources:ValidationResources, bSave %>" name="cmdsubmit"
                                                runat="server" onserverclick="cmdsubmit_ServerClick" class="btnstyle">
                                             <input id="cmdClear" type="button" value="<%$ Resources:ValidationResources, bReset %>" name="cmdClear"
                                                            runat="server" onserverclick="cmdClear_ServerClick" class="btnstyle">
                                        </td>
                                    </tr>
                                   <%-- <tr>
                                        <td colspan="3" align="left">
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server"
                                                AssociatedUpdatePanelID="updatepanel1" DisplayAfter="0" DynamicLayout="false">
                                                <ProgressTemplate>
                                                    <img alt="Loading" src="Images/loading1.gif" />
                                                    Please wait....
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="3" valign="top"></td>
                                    </tr>
                                    <tr>
                                        <td valign="top" colspan="3"></td>
                                    </tr>--%>
                                    <%--<tr>
                                        <td colspan="3">
                                            <table id="Table3" class="table-condensed">
                                                <tr>
                                                    <td style="text-align:center">
                                                       </td>
                                                    
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>--%>
                                </table>
                               
                                <input id="hdCulture" runat="server"  type="hidden" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                       
			<asp:textbox id="txtReturndate"  style="Z-INDEX: 101; LEFT: 520px; POSITION: absolute; TOP: 24px"
				 runat="server" Width="5px" Height="8px" Visible="False"></asp:textbox>
              </div>

<script type="text/javascript">
    //On Page Load.
    /*
    $(function () {
        //SetDatePicker();
    });

    var prm = Sys.WebForms.PageRequestManager.getInstance();
    if (prm != null) {
        prm.add_endRequest(function (sender, e) {

            if (sender._postBackSettings.panelsToUpdate != null) {
                SetDatePicker();
            }
        });
    };
    function SetDatePicker() {
        $("[id$=txtdate]").datepicker({
            changeMonth: true,//this option for allowing user to select month
            changeYear: true, //this option for allowing user to select from year range
            dateFormat: 'dd-M-yy'  // CHANGE DATE FORMAT.
        });

    }
        */
    
</script>

</asp:Content>


