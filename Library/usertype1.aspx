<%@ Page Language="C#"  AutoEventWireup="true" MasterPageFile="~/LibraryMain.master"  CodeBehind="usertype1.aspx.cs" Inherits="Library.usertype1" %>
<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>

 <asp:Content id="Head" runat="server" ContentPlaceHolderID="head">

     <style>
         .confirmPanel {
    background-color: #f6f6f6 ;padding:10px ;box-shadow:0px 0px 8px 0px #a1a1a1 ;border:solid 1px #cccccc;border:0
}
         label{
             font-weight:bold;
             font-size:16px
         }
     </style>
   </asp:Content>

   <asp:Content ID="Main" runat="server" ContentPlaceHolderID="MainContent">
       <asp:UpdateProgress ID="UpPorg1" runat="server">
    <ProgressTemplate>
    <NN:Mak ID="FF1" runat="server" />
    </ProgressTemplate>
    </asp:UpdateProgress>
                   <div class="container tableborderst">                    
              <div style="width:100%;display:none" class="title">
                  <div style="width:89%;float:left">&nbsp;
                        <asp:Label ID="lblTitle" runat="server"  style="display:none"  Font-Names="Lucida Sans Unicode"
                                BorderColor="Black"  Font-Bold="True" 
                                Font-Size="X-Small"></asp:Label></div>
                    <div style="float:right;vertical-align:top">
                        <a id="lnkHelp" href="#"   style="display:none" onclick="ShowHelp('Help/Administration-Security-FormLevelPermissionforUserType.htm')">
                            <img height="15" src="help.jpg" alt="help" /></a></div></div>
                       
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                              

                                    <table id="Table1" class="no-more-tables table-condensed">
                                        <tr>
                                            <td style="width:20%"></td>
                                            <td>
                                                <asp:Label ID="msglabel" runat="server" Font-Names="Lucida Sans Unicode" Font-Bold="True"
                                                    Font-Size="X-Small" CssClass="err"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td><asp:Label ID="lblusertype" runat="server"  
                                            CssClass="span" Text="<%$ Resources:ValidationResources, LUserType %>"></asp:Label></td>
                                            <td>
                                                <input id="txtusertype" onblur="this.className='blur'" onfocus="this.className='focus'" type="text" runat="server" class="txt10" style='font-family: Arial Unicode MS'>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align:top" colspan="2">
                                                <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" CssClass="opt" Text="<%$ Resources:ValidationResources, CnkSelectA %>" OnCheckedChanged="chkAll_CheckedChanged"/></td>
                                        </tr>
                                        </table>


                                                <div class="allgriddiv confirmPanel" style="height:350px;background-color:#f1e7e7">
                                                 
                                                    <asp:TreeView ID="TreeView1" runat="server"  style="color:black" ShowCheckBoxes="All" CssClass="allgrid" ShowLines="True" ExpandDepth="2" 
OnTreeNodeCheckChanged="TreeView1_TreeNodeCheckChanged" MaxDataBindDepth="2" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'>
                                                    </asp:TreeView>
                                                </div>
                                          <table class="no-more-tables table-condensed">
                                        <tr>
                                            <td colspan="2" style="text-align:center">
                                         
                                                    <asp:Button ID="cmdSave" runat="server" Text="<%$ Resources:ValidationResources, bSave %>" UseSubmitBehavior="False" CssClass="btn btn-primary" OnClick="cmdSave_Click"></asp:Button>
                                                    <asp:Button ID="cmdreset" runat="server" Text="<%$ Resources:ValidationResources, bReset %>" UseSubmitBehavior="False" CssClass="btn btn-primary" OnClick="cmdreset_Click"/>
                                                    <asp:Button ID="cmddelete" runat="server" Text="<%$ Resources:ValidationResources, bdelete %>"  UseSubmitBehavior="False" CssClass="btn btn-primary" OnClick="cmddelete_Click"/> 
                                             
                                                    <asp:Button ID="cmdnext" runat="server" Text="<%$ Resources:ValidationResources, Next %>"  Visible="False" UseSubmitBehavior="False" CssClass="btnstyle" OnClick="cmdnext_Click"></asp:Button></td>
                                            </tr>
                                        
                                        <tr>
                                            <td colspan="2">
                                                <asp:CheckBox ID="Chksearch" runat="server" AutoPostBack="True" 
OnCheckedChanged="Chksearch_CheckedChanged" Text="<%$ Resources:ValidationResources, bSearch %>" CssClass="span" /></td>
                                             
                                        </tr>
                                        <tr>
                                            <td style="text-align:right">
                                                <asp:Label ID="Label2" runat="server" Text="<%$ Resources:ValidationResources, LUserType %>"  CssClass="span"></asp:Label></td>
                                            <td>
                                                <input id="txtCategory" onblur="this.className='blur'" onfocus="this.className='focus'" name="txtCategory" type="text" runat="server" class="txt10" style='font-family: Arial Unicode MS' />
                                             
                                                <asp:Button Id="btnCategoryFilter" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnCategoryFilter_Click"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:ListBox ID="lstAllCategory" runat="server" AutoPostBack="true"  Style='font-family: Arial Unicode MS' OnSelectedIndexChanged="lstAllCategory_SelectedIndexChanged"></asp:ListBox></td>
                                        </tr>
                                       
                                        <tr>
                                            <td style="vertical-align:top">
                                                <asp:HiddenField ID="Hidden1" runat="server" />
                                                <input id="Hidden2" type="hidden" runat="server" />&nbsp;
                        <input id="txtusertypecode" name="txtusertypecode" type="hidden" runat="server" />
                                                <input id="yCoordHolder" runat="server" name="Hidden3" size="1" style="width:22px; height:22px"
                                                    type="hidden" />
                                                <input id="xCoordHolder" runat="server" name="Hidden2" size="1" style="width:22px; height:22px"
                                                    type="hidden" />
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                                    ShowMessageBox="True" ShowSummary="False" />
                                                &nbsp;
                        </td>
                                        </tr>
                                        <input id="hSubmit1" runat="server" name="hSubmit1" size="1" style="width: 24px; height: 22px"
                                            type="hidden" value="0" />
                                        <asp:ListBox ID="hlstAllCategory" runat="server" Height="0px" Width="32px" Style="visibility: hidden"></asp:ListBox>
                                        <input id="Button1" runat="server" name="Button1" size="20" style="width: 1px; height: 1px"
                                            type="button" value="Button" class="btnH" />
                                    </table>
                                </ContentTemplate>
                                <Triggers>

                                    <asp:AsyncPostBackTrigger ControlID="TreeView1" EventName="TreeNodeCheckChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="Chksearch" EventName="CheckedChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                   </div> 

    </asp:Content>

