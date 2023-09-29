<%@ Page Language="C#" MasterPageFile="~/LibraryMain.Master" AutoEventWireup="true" CodeBehind="menuadjustment.aspx.cs" Inherits="Library.menuadjustment" %>

<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>

<asp:Content ID="Head" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .Publ {
            width: 300px;
            max-height: 250px;
            overflow: auto;
            margin: 0;
            padding: 0;
            font-size: 13px;
            color: black;
            padding: 1px;
            border: 2px solid green;
            z-index: 1000;
        }
    </style>
    <script type="text/javascript">
        function GetVendJs(sender, arg) {
            let id = arg.get_value();
            // alert('0');
            $('[id$=hdUid]').val(id);
            //            $('[id$=button5]').click();


        }
        function getPg(sender, arg) {
            let id = arg.get_value();
            $('[id$=hdid]').val(id);
            $('[id$=btnGetMenu]').click();
        }
        function getMenu(sender, arg) {
            if (arg == undefined)
                return;
            let id = arg.get_value();
            $('[id$=hdmenuid]').val(id);
            $('[id$=btnShMEnu]').click();

        }
        function getPerentid(sender, arg) {
            let id = arg.get_value();
            $('[id$=hdPmenuId]').val(id);
        }


        function GetVendJs1(sender, arg) {
            let id = arg.get_value();
            //alert('1');
            $('[id$=hidM]').val(id);
        }
        function GetVendJs2(sender, arg) {
            let id = arg.get_value();
            // alert('2');
            $('[id$=hidmenu]').val(id);

            $('[id$=Go]').click();
        }
    </script>

</asp:Content>
<asp:Content ID="Main" runat="server" ContentPlaceHolderID="MainContent">
    <asp:UpdateProgress ID="UpPorg1" runat="server">
        <ProgressTemplate>
            <NN:Mak ID="FF1" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="container tableborderst">
        <asp:UpdatePanel ID="up11" runat="server">
            <ContentTemplate>
                <table class="no-more-tables table-condensed">
                    <tr>
                        <td colspan="4">
                            <ajax:TabContainer ID="AddUpd" runat="server" OnActiveTabChanged="AddUpd_ActiveTabChanged">
                                <ajax:TabPanel ID="tabAdd" Width="200" runat="server" HeaderText="Add">
                                    <ContentTemplate>
                                        <table style="width: 800px;">
                                            <tr>
                                                <td>Parent Menu
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtpagemenu" runat="server"> </asp:TextBox>
                                                    <ajax:AutoCompleteExtender ID="ExtVend" runat="server" TargetControlID="txtpagemenu"
                                                        CompletionInterval="20"
                                                        CompletionSetCount="50"
                                                        MinimumPrefixLength="0"
                                                        CompletionListCssClass="Publ"
                                                        ServicePath="MssplSugg.asmx"
                                                        ServiceMethod="mainmenunew"
                                                        OnClientItemSelected="GetVendJs">
                                                    </ajax:AutoCompleteExtender>
                                                    <asp:HiddenField ID="hdUid" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Title
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txttitle" runat="server" Width="300" AutoPostBack="true" OnTextChanged="txttitle_TextChanged">      </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Page Link
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txthref" runat="server" Width="300">      </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Order No
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtorderno" runat="server" Width="300">      </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Help Link
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtHelp" runat="server" Width="300">      </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td colspan="2">
                                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="btnSave_Click" />
                                                    <asp:Button ID="btnRes" runat="server" CssClass="btn btn-primary" Text="Reset" OnClick="btnRes_Click" />
                                                </td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </ajax:TabPanel>
                                <ajax:TabPanel ID="tabUpdDel" Width="200" HeaderText="Update/Delete" runat="server">
                                    <ContentTemplate>
                                        <table style="width: 800px;">
                                            <tr>
                                                <td>Find Exising Title
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txttitle2" runat="server" Width="300">      </asp:TextBox>
                                                    <ajax:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txttitle2"
                                                        CompletionInterval="20"
                                                        CompletionSetCount="50"
                                                        MinimumPrefixLength="0"
                                                        CompletionListCssClass="Publ"
                                                        ServicePath="MssplSugg.asmx"
                                                        ServiceMethod="mainmenupg"
                                                        OnClientItemSelected="getPg">
                                                    </ajax:AutoCompleteExtender>
                                                    <asp:HiddenField ID="hdid" runat="server" />
                                                    Parent Menu : &nbsp;&nbsp;
                 <asp:Label ID="labMenu" Font-Bold="true" runat="server"></asp:Label>
                                                    <asp:Button ID="btnGetMenu" runat="server" Style="display: none" OnClick="btnGetMenu_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Page Link
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txthref2" runat="server" Width="300">      </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Parent Menu
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txParentMn" runat="server"> </asp:TextBox>
                                                    <ajax:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txParentMn"
                                                        CompletionInterval="20"
                                                        CompletionSetCount="50"
                                                        MinimumPrefixLength="0"
                                                        CompletionListCssClass="Publ"
                                                        ServicePath="MssplSugg.asmx"
                                                        ServiceMethod="mainmenunew"
                                                        OnClientItemSelected="getPerentid">
                                                    </ajax:AutoCompleteExtender>
                                                    <asp:HiddenField ID="hdPmenuId" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Order No
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txOrderno2" runat="server" Width="300">      </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Help Link
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtHelp2" runat="server" Width="300">      </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td colspan="2">
                                                    <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary" Text="Update" OnClick="btnUpdate_Click" />
                                                    <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-primary" Text="Delete" OnClick="btnDelete_Click" />
                                                    <asp:Button ID="Button2" runat="server" CssClass="btn btn-primary" Text="Reset" OnClick="btnRes_Click" />
                                                </td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </ajax:TabPanel>
                                <ajax:TabPanel ID="tabreord" runat="server" HeaderText="Reorder Pages">
                                    <ContentTemplate>
                                        Save Form in Order of Slno
                                        <table style="width: 800px;">
                                            <tr>
                                                <td>Find Menu
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txFindMenu" runat="server"> </asp:TextBox>
                                                    <ajax:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" TargetControlID="txFindMenu"
                                                        CompletionInterval="50"
                                                        CompletionSetCount="50"
                                                        MinimumPrefixLength="0"
                                                        CompletionListCssClass="Publ"
                                                        ServicePath="MssplSugg.asmx"
                                                        ServiceMethod="mainmenunew"
                                                        OnClientItemSelected="getMenu">
                                                    </ajax:AutoCompleteExtender>
                                                    <asp:HiddenField ID="hdmenuid" runat="server" />
                                                    <asp:Button ID="btnShMEnu" runat="server" style="display:none" OnClick="btnShMEnu_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <asp:GridView ID="grdMorder" runat="server" AutoGenerateColumns="false">
                                                        <Columns>
                                                                <asp:TemplateField HeaderText="Sl No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="labslno" runat="server" Text='<%# Eval("slno") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Form Page">
                                                                <ItemTemplate>
                                                                    <asp:HiddenField ID="hdmid" runat="server" Value='<%# Eval("menu_id") %>' /> 
                                                                    <asp:HiddenField ID="hdpid" runat="server" Value='<%# Eval("parentid") %>' />
                                                                   <asp:Label ID="labfrm" runat="server" Text='<%# Eval("Menu_name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Order No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="hdordno" runat="server" Text='<%# Eval("ordno") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Move Up">
                                                                <ItemTemplate>
                                                                    <asp:Button ID="btnmoveup" runat="server" Text="Move Up" OnClick="btnmoveup_Click" /> 
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Move Down">
                                                                <ItemTemplate>
                                                                    <asp:Button ID="btnmovedown" runat="server" Text="Move Down" OnClick="btnmovedown_Click" /> 
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                   <asp:Button ID="btnSavOrd" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="btnSavOrd_Click" />
                                                   <asp:Button ID="Button3" runat="server" CssClass="btn btn-primary" Text="Reset" OnClick="btnRes_Click" />
 
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </ajax:TabPanel>
                            </ajax:TabContainer>

                        </td>
                    </tr>
                </table>



            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
