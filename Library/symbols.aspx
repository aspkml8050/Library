<%@ Page Language="C#" MasterPageFile="~/LibraryMain.master" AutoEventWireup="true" CodeBehind="symbols.aspx.cs" Inherits="Library.symbols" %>
<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>

<asp:Content ID="SymHead" runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript">

        function validateform() {
            let v1 = $('[id$=txtshortname]').val().trim();
            if (v1 == '') {
                alert('Symbol Name required.');
                return false;
            }
            let v2 = $('[id$=ddsymbol]').val().trim();
            if (v2 == '0') {
                alert('Select Type required.');
                return false;

            }
            return true;
        }
    </script>
 </asp:Content>
<asp:Content ID="symBody" runat="server" ContentPlaceHolderID="MainContent">
 <asp:UpdateProgress ID="UpPorg1" runat="server">
    <ProgressTemplate>
    <NN:Mak ID="FF1" runat="server" />
    </ProgressTemplate>
    </asp:UpdateProgress>

     <div>
        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
            Font-Size="11px" Height="90px" ShowMessageBox="True" ShowSummary="False" Style="z-index: 101;
            left: 541px; position: absolute; top: 676px" Width="8px" />
        <div class="container tableborderst" >     
      <div style="width:100%;display:none" class="title">
                    <div style="width:89%;float:left" >
                     <asp:Label ID="lblt1" runat="server" style="text-align:center" Width="100%"></asp:Label>
                        </div>
          <div style="float:right;vertical-align:top"> 
                    <a id="lnkHelp" href="#" onclick="ShowHelp('Help/Masters-symbols.htm')"><img height="15" src="help.jpg" alt="Help"  /></a>
          </div></div>
                    <p>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                   <div class="no-more-tables" style="width:100%">
                        <table id="Table1" class="table-condensed">
                            <tr>
                                <td colspan="4">
                        <asp:Label ID="msglabel" runat="server" CssClass="err" ></asp:Label>

                                </td>
                            </tr>
                            <tr>
                                <td>
                              <asp:Label ID="lblshortname" runat="server" CssClass="span"  Text="<%$ Resources:ValidationResources, LSym %>"></asp:Label></td>
                                <td style="width:35%">
                                    <input id="txtshortname" runat="server"  maxlength="3" name="txtshortname"
                                        onblur="this.className='blur'" onfocus="this.className='focus'"  type="text" />
                                    <asp:Label ID="Label1" runat="server" CssClass="star" >*</asp:Label></td>
                            
                                <td style="width:15%">
                                  <asp:Label ID="lbldepartmentname" runat="server" CssClass="span"
                                        Text ="<%$ Resources:ValidationResources, LSymT %>"></asp:Label></td> 
                                <td>
                                    <asp:DropDownList ID="ddsymbol" runat="server"  Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'>
                                       <asp:ListItem Text="<%$ Resources:ValidationResources, ComboSelect %>" value="0" ></asp:ListItem>
                                        <asp:ListItem Value="<%$ Resources:ValidationResources, LGener %>"></asp:ListItem>
                                        <asp:ListItem Value="<%$ Resources:ValidationResources, Spl %>"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="Label2" runat="server" CssClass="star" >*</asp:Label></td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align:center">
                               
                                                <%--<input id="cmdsave" runat="server" name="cmdsave" 	onclick =" if ( validateform()==false ) return false; else true ;"
                                                    type="button" value="<%$ Resources:ValidationResources, bSave %>" accesskey="S" class="btnstyle" />--%>

                       <asp:Button id="cmdsave" runat="server" CssClass="btn btn-primary" text= "Submit" OnClick="cmdsave_Click"/>
                                           
                                                <%--<input id="cmdreset" runat="server" name="cmdreset"
                                                    type="button" value="<%$ Resources:ValidationResources, bReset %>" accesskey="E" class="btnstyle" />--%>
                                    <asp:Button ID="cmdreset" runat="server" CssClass="btn btn-primary" Text="Reset" OnClick="cmdreset_Click"/>
                                         
                                                <%--<input id="cmddelete" runat="server" name="cmddelete" onclick="if (DoConfirmation() == false) return false;"
                                                  type="button" value="<%$ Resources:ValidationResources, bdelete %>" accesskey="D" class="btnstyle" />--%>
                               <asp:Button Id="cmddelete" runat="server" Text="Delete" OnClick="cmddelete_Click" CssClass="btn btn-primary"/>         
                                </td>
                            </tr>
                            <tr>
                                <td  colspan="4" >
                                    <asp:Label ID="Label14" runat="server" CssClass="showBoldExist" Text ="<%$ Resources:ValidationResources, LSymExist %>"></asp:Label></td>
                           
                             </table></div>
                        <input id="xCoordHolder" runat="server" name="Hidden5" style="width: 32px; height: 16px"
                            type="hidden" /><input id="hdShort" runat="server" type="hidden" style="width: 16px; height: 16px;" /><input id="hdSymbol" runat="server" type="hidden" style="width: 24px; height: 16px;" /><input id="yCoordHolder" runat="server" name="Hidden5" type="hidden" style="width: 24px; height: 16px;" /><input id="txtdepartmentcode"
                        runat="server" name="Hidden4" style="width: 1px; height: 22px" type="hidden" /><input id="hdTop" runat="server" name="hdTop" style="width: 24px; height: 22px" type="hidden" /><input id="hdUnableMsg"
                                    runat="server" name="hdUnableMsg" style="width: 24px; height: 22px"
                                    type="hidden" /><input id="Hidden1" runat="server" name="Hidden1" size="1" style="width: 3px; height: 22px" type="hidden" /><input id="Hidden4" runat="server" name="Hidden4" size="1" style="width: 5px; height: 22px" type="hidden" />
        <input id="Hidden2" runat="server" name="Hidden2" size="1" style="width: 5px; height: 22px" type="hidden" />
        <input id="Hidden3" runat="server" name="Hidden3" size="1" style="width: 5px; height: 22px" type="hidden" />
<%--                                    <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="comboValidation"
                        Display="None" SetFocusOnError="True" ControlToValidate="ddsymbol"></asp:CustomValidator>--%>

                                
                            
                        <div class="tdmgr">
                                <div class="allgriddiv" id="dvgrd" runat="server">                                  
                    <asp:HiddenField runat="server" ID="hdnGrdId" />
        <asp:DataGrid ID="DataGrid1"  runat="server" CssClass="allgrid GenTable1"  Font-Names='<%$ Resources:ValidationResources, TextBox1 %>' OnItemCommand="DataGrid1_ItemCommand1" OnpageIndexchanged="DataGrid1_PageIndexChanged1" >
                                        
                                        <Columns>
                                            <asp:BoundColumn DataField="SymbolTypeId" HeaderText="<%$ Resources:ValidationResources, SyblTpId %>" Visible="False">
                                            </asp:BoundColumn>
                                            <asp:ButtonColumn CommandName="Select" DataTextField="Symbol" HeaderText="<%$ Resources:ValidationResources, LSym%>"
                                               Text="Symbol">                                                                                    
                                            </asp:ButtonColumn>
                                            <asp:BoundColumn DataField="SymbolType" HeaderText="<%$ Resources:ValidationResources,LSymT %>" >                                                
                                            </asp:BoundColumn>
                                        </Columns>                                        
                                    </asp:DataGrid></div>
                         </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        
                        </p>
                        </div>
          
    </div>
    </asp:Content>





   
