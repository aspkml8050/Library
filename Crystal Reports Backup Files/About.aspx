<%@ Page Title="About" Language="C#" MasterPageFile="~/LibraryMain.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="Library.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <asp:UpdatePanel ID="upsdas" runat="server">
            <ContentTemplate>
                       <asp:TextBox ID="txm" runat="server" ></asp:TextBox>
 
            </ContentTemplate>
        </asp:UpdatePanel>
    <main aria-labelledby="title">
        <h2 id="title"><%: Title %>.</h2>
        <h3>Your application description page.</h3>
        <p>Use this area to provide additional information.</p>
    </main>
</asp:Content>
