<%@ Page Language="C#" Async="true" AutoEventWireup="true" MasterPageFile="~/LibraryMain.Master" CodeBehind="HomePage.aspx.cs" Inherits="Library.HomePage" %>

<asp:Content ID="cmHead" runat="server" ContentPlaceHolderID="head">
    <link href="/cssDesign/floatlabels.css" rel="stylesheet" />
    <style type="text/css">
        #MainContent_grdSearchItems > thead > tr >th {
            font-size:11px;
        } 
        #MainContent_grdSearchItems > tbody > tr >td > span {
            font-size:11px;
        } 

        #dataTables_info {
            font-size:11px;
        }
        #MainContent_grdSearchItems_paginate {
            font-size:11px;
        }
        #MainContent_grdSearchItems_paginate > a {
            font-size:11px;
        }
        .dataTables_info {
            font-size:11px;
        }
        .paginate_button {
            font-size:11px;
        }
        .paginate_button > a{
            font-size:11px;
        }
    </style>
    <script type="text/javascript">
           $(document).ready(function () {
               try {
                   Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(setTable);

               } catch { }
           })
        function setTable() {
            try {
                let c = $("[id$='grdSearchItems'] > thead > tr").length;
                if (c == 0) {
                    $('[id$="grdSearchItems"] tbody').before('<thead><tr></tr></thead>');
                    $("[id$='grdSearchItems'] thead tr").append($("[id$='grdSearchItems'] th"));
                    $("[id$='grdSearchItems'] tbody tr:first").remove();
                }
                let cb = $("[id$='grdSearchItems'] tbody tr").length;
                if (cb > 0) {
                    $("[id$='grdSearchItems']").addClass('display compact');
                    $("[id$='grdSearchItems']").DataTable();
                }

            } catch (er) {
                alert(er + '; Make sure grid has data');
            }
        }
    </script>
</asp:Content>

<asp:Content ID="cmBody" runat="server" ContentPlaceHolderID="MainContent">
    <h4>Library Home Page</h4>
    <div class="container dashboard-main">
        <asp:UpdatePanel ID="updash1" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hdshowItems" runat="server" />
                <div class="container ">
                    <div class="d-flex flex-wrap">
                        <div class=" col-md-3 input-wrapper">
                            <asp:TextBox ID="txTitle" class="form-control2" runat="server" placeholder="...Title"></asp:TextBox>
                            <label for="MainContent_txTitle" class="control-label">Title</label>
                        </div>
                        <div class="col-md-3 input-wrapper">
                            <asp:TextBox ID="txVendor" class="form-control2" runat="server" placeholder="...Vendor"></asp:TextBox>
                            <label for="MainContent_txVendor" class="control-label">Vendor</label>
                        </div>
                        <div class="col-md-3 input-wrapper">
                            <asp:TextBox ID="txAuthor" class="form-control2" runat="server" placeholder="...Author"></asp:TextBox>
                            <label for="MainContent_txAuthor" class="control-label">Author</label>
                        </div>
                        <div class="col-md-3  input-wrapper">
                            <asp:TextBox ID="txPageSize" class="form-control2" runat="server" placeholder="...No Of Items" TextMode="Number"></asp:TextBox>
                            <label for="MainContent_txPageSize" class="control-label">No of Items</label>
                        </div>
                        <div class="col-md-3  input-wrapper">
                            <asp:TextBox ID="txNoofSet" class="form-control2" runat="server" placeholder="...Set No(1,2 ..)" TextMode="Number"></asp:TextBox>
                            <label for="MainContent_txNoofSet" class="control-label">Set No</label>
                        </div>
                        <div class="col-md-3">
                            <asp:Button ID="btnShowItems" runat="server" CssClass="btn btn-secondary btn-sm" Text="Find" OnClick="btnShowItems_Click" />
                        </div>
                    </div>
                    <asp:GridView ID="grdSearchItems" runat="server" AutoGenerateColumns="false">
                        <Columns>
                   <asp:TemplateField HeaderText="">
                  <ItemTemplate>

                  </ItemTemplate>
              </asp:TemplateField>
                            <asp:TemplateField HeaderText="Accn No">
                                <ItemTemplate>
<asp:Label ID="txaccessionnumber" runat="server" Text='<%# Eval("accessionnumber")%>'></asp:Label> 
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Form">
                                <ItemTemplate>
<asp:Label ID="txform" runat="server" Text='<%# Eval("form")%>'></asp:Label> 
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Title">
                                <ItemTemplate>
<asp:Label ID="txtitle" runat="server" Width="250" Text='<%# Eval("title")%>'></asp:Label> 
                                    <asp:Label ID="txSubtitle" runat="server" Text='<%# Eval("Subtitle")%>'></asp:Label> 
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Catalog Date">
                                <ItemTemplate>
<asp:Label ID="txcatalogdate1" runat="server" Text='<%# Eval("catalogdate1")%>'></asp:Label> 
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pages">
                                <ItemTemplate>
<asp:Label ID="txpages" runat="server" Text='<%# Eval("pages")%>'></asp:Label> 
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Edition">
                                <ItemTemplate>
<asp:Label ID="txedition" runat="server" Text='<%# Eval("edition")%>'></asp:Label> 
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ISBN">
                                <ItemTemplate>
<asp:Label ID="txisbn" runat="server" Text='<%# Eval("isbn")%>'></asp:Label> 
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Language">
                                <ItemTemplate>
<asp:Label ID="txLanguage_Name" runat="server" Text='<%# Eval("Language_Name")%>'></asp:Label> 
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Publisher">
                                <ItemTemplate>
<asp:Label ID="txPublisher" runat="server" Text='<%# Eval("Publisher")%>'></asp:Label> 
                                    <asp:Label ID="txPublCity" runat="server" Text='<%# Eval("PublCity")%>'></asp:Label> 
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Call No">
                                <ItemTemplate>
<asp:Label ID="txclassnumber" runat="server" Text='<%# Eval("classnumber")%>'></asp:Label> 
                                    <asp:Label ID="txbooknumber" runat="server" Text='<%# Eval("booknumber")%>'></asp:Label> 
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Author">
                                <ItemTemplate>
<asp:Label ID="txAuthor1" runat="server" Text='<%# Eval("Author1")%>'></asp:Label> 
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>

                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
</asp:Content>
