<%@ Page Title="About" Language="C#" Async="true" MasterPageFile="~/LibraryMain.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="Library.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <asp:UpdatePanel ID="upsdas" runat="server">
            <ContentTemplate>
                       <asp:TextBox ID="txm" runat="server" ></asp:TextBox>
 
                <asp:Button ID="btnreadAccn" runat="server" Text="Read Accn" OnClick="btnreadAccn_Click" />

                <asp:GridView ID="grdorg" runat="server" ></asp:GridView>

              <asp:Button ID="btnsamp" runat="server" Text="Api test" OnClick="btnsamp_Click" />


                <asp:Button ID="btntst1" runat="server" Text="Test 1 sql script" OnClick="btntst1_Click" />
            </ContentTemplate>
        </asp:UpdatePanel>
    <main aria-labelledby="title">
        <h2 id="title"><%: Title %>.</h2>
        <h3>Your application description page.</h3>
        <p>Use this area to provide additional information.</p>
    </main>
</asp:Content>
