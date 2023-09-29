<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/LibraryMain.Master" CodeBehind="LibPermissions.aspx.cs" Inherits="Library.LibPermissions" %>

<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>
<asp:Content id="Head" runat="server" ContentPlaceHolderID="head">
    <script  type="text/javascript">
        function comboValidation(sender, args) {
            if (args.Value == "---Select---") {
                args.IsValid = false;
                return;
            }
            else {
                args.IsValid = true;
            }
        }

        function selectAllorNone(selectAll, ctrlId) {
            if (ctrlId != null) {
                var ctrl = document.getElementById(ctrlId);
                var chkBoxes = ctrl.getElementsByTagName("input");
                if (document.getElementById(selectAll).checked) {
                    for (i = 0; i < chkBoxes.length; i++) {
                        if (chkBoxes[i].type == "checkbox" || chkBoxes[i].type == "CHECKBOX") {
                            chkBoxes[i].checked = true;
                        }
                    }
                } else {
                    for (i = 0; i < chkBoxes.length; i++) {
                        if (chkBoxes[i].type == "checkbox" || chkBoxes[i].type == "CHECKBOX") {
                            chkBoxes[i].checked = false;

                        }
                    }
                }
                document.getElementById(selectAll).focus();
            }
        }
        window.history.forward(1);
    </script>
    <link href="cssDesign/tdmanage.css" rel="stylesheet" type="text/css" >
</asp:Content>
<asp:Content ID="Main" runat="server" ContentPlaceHolderID="MainContent">
    <asp:UpdateProgress ID="UpPorg1" runat="server">
    <ProgressTemplate>
    <NN:Mak ID="FF1" runat="server" />
    </ProgressTemplate>
    </asp:UpdateProgress>
       <div class="container tableborderst">                    
              <div style="width:100%;display:none" class="title">
                  <div style="width:89%;float:left">
                      <asp:Label ID="Label1" runat="server" Text="<%$ Resources:ValidationResources, LibPerm%>" ></asp:Label></div>
                   <div style="float:right;vertical-align:top">
                        <a id="lnkHelp" href="#" onclick="ShowHelp('Help/Administration-LibraryParameter-LibraryAccessRightsManagement.htm')"><img height="15" src="help.jpg" alt="help"  /></a>
                        </div>
                        <%--<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" LoadScriptsBeforeUI="false">
                        <Services><asp:ServiceReference Path="TopSearchService.asmx" /></Services>
                        </asp:ScriptManager>--%>
                        <br />
                        </div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                        <table id="table2" class="no-more-tables table-condensed tdmgr">
                            <tr>
                                <td style="width:25%"></td>
                                <td>
                                    <asp:Label ID="msglabel" runat="server" CssClass="err" ></asp:Label>

                                </td>
                            </tr>
                            <tr>
                                <td>
                                   <asp:Label ID="Label3" runat="server" Text="<%$ Resources:ValidationResources, LibName%>" ></asp:Label>

                                </td>
                                <td>
                                    <asp:DropDownList ID="DdlLibraries" runat="server"  AutoPostBack="True"  Font-Names="<%$ Resources:ValidationResources, TextBox1 %>">
                                    </asp:DropDownList>

                                </td>
                            </tr>
                            <tr>
                                 
                                <td colspan="2">
                                    <input id="chkSelectAll" runat="server" onclick="selectAllorNone('chkSelectAll', 'grdDetail');" type="checkbox" />
                                    <asp:Label ID="Label4" runat="server" Text="<%$ Resources:ValidationResources, CnkSelectA %>" Font-Bold="true"  ></asp:Label>

                                </td>
                            </tr>
                            </table>
                                    <div class="allgriddiv tdmgr" >
                                    <asp:DataGrid ID="grdDetail" runat="server" Height="1px" CssClass="allgrid" Font-Names="<%$ Resources:ValidationResources, TextBox1 %>">
                                        
                                        <Columns>
                                            <asp:TemplateColumn>
                                                
                                                <ItemTemplate>
                                                    <input id="Chkselect" runat="server"  style="width: 16px;
                                                        height: 14px" type="checkbox" />
                                                </ItemTemplate>
                                                
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="LibraryName" HeaderText="<%$ Resources:ValidationResources,Lib_name %>"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="ConnectionStringName" HeaderText="<%$ Resources:ValidationResources,LConStrNme %>" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="Id" HeaderText="<%$ Resources:ValidationResources,GrId %>" Visible="False"></asp:BoundColumn>
                                            <asp:TemplateColumn>
                                               <ItemTemplate>
                                                    <asp:Label ID="lblid" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                                </asp:TemplateColumn>
                                                
                                                <asp:TemplateColumn>
                                               <ItemTemplate>
                                                    <asp:Label ID="LibPerm" runat="server" Text='<%# Eval("LibPermission").ToString() %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                                </asp:TemplateColumn>
                                                
                                        </Columns>
                                    </asp:DataGrid>
                                </div>
                                   
                                    
                                    <input id="hSubmit1" runat="server" name="hSubmit1" 
                                        type="hidden" value="0" /></td>
                                 
                                    <table id="table7" class="table-condensed">
                                        <tr>
                                            <td  style="text-align:center">
                                                <asp:Button ID="cmdCreateLib" runat="server" Text="<%$ Resources:ValidationResources,bSave%>" UseSubmitBehavior="False" CssClass="btnstyle" />
                                                <input id="cmdreset" causesvalidation="false" 
                                                  type="button" value="<%$ Resources:ValidationResources,bReset%>" runat="server" class="btnstyle" /></td>
                                        </tr>
                                    </table>
                               
                            <input id="hdLibrariesId" runat="server"  type="hidden" />
                        <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="comboValidation"
                            ControlToValidate="DdlLibraries" Display="None" ErrorMessage="<%$ Resources:ValidationResources,SelLib%>"
                            SetFocusOnError="True"></asp:CustomValidator>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                            ShowMessageBox="True" ShowSummary="False" />
                        
                        </ContentTemplate>
                            </asp:UpdatePanel>
                           
						 
        </div>

</asp:Content>