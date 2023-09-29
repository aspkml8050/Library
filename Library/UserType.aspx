<%@ Page Language="C#" Async="true" AutoEventWireup="true" MasterPageFile="~/LibraryMain.Master" CodeBehind="UserType.aspx.cs" Inherits="Library.UserType" %>


<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>

<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">

        <style>
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:HiddenField ID="hdMast" runat="server" />
         <asp:UpdateProgress ID="UpPorg1" runat="server">
    <ProgressTemplate>
    <NN:Mak ID="FF1" runat="server" />

    </ProgressTemplate>
    </asp:UpdateProgress>
     <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                 <div class="container tableborderst" >
                     <div class="row">
                         <div class="col-md-5">
                             <span>User Type</span>
                         </div>
                         <div class="col-md-7">
                             <asp:TextBox ID="txutype" runat="server" ></asp:TextBox>
                             <asp:HiddenField ID="hdid" runat="server" />
                         </div>
                     </div>
                     <div class="row" style="padding-bottom:25px;">
                         <div class="col-md-12">
                             <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" AccessKey="S" OnClick="btnSave_Click" />
                             <asp:Button ID="btnRes" runat="server" CssClass="btn btn-primary" Text="Reset" AccessKey="R" OnClick="btnRes_Click" />
                             <asp:Button ID="btnDel" runat="server" CssClass="btn btn-primary" Text="Delete" Enabled="false" AccessKey="D" OnClick="btnDel_Click" />
                         </div>
                     </div>
                     <asp:DataList ID="dlUtype" runat="server" RepeatLayout="Flow" BackColor="White" RepeatDirection="Horizontal" >
                         <ItemTemplate>
                             <p style="padding:3px 8px;display:inline; width:200px;">
                                <asp:LinkButton ID="lnktypeDl"  runat="server" Text='<%# Eval("usertypename") %>' OnClick="lnktypeDl_Click" ></asp:LinkButton>
                                 <asp:HiddenField id="hdid" runat="server" Value='<%# Eval("usertypeid") %>' />
                             </p>
                         </ItemTemplate>
                     </asp:DataList>
       </div>
           </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>