<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IssueReturnSubscriptionRpt.aspx.cs"  MasterPageFile="~/LibraryMain.master" Inherits="Library.IssueReturnSubscriptionRpt" %>

<asp:Content ID="Head" runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript">
        function onTextBoxUpdate1(evt) {

            var textBoxID = evt.source.textBoxID;
            if (evt.selMenuItem != null) {
                document.getElementById(textBoxID).value = evt.selMenuItem.label; // + " (modified by onTextboxUpdate)";
                //            document.getElementById("Button1").click();
            }
            evt.preventDefault();
        }
    </script>
    
</asp:Content>

<asp:Content ID="Main" runat="server" ContentPlaceHolderID="MainContent">
     <div class="container tableborderst">   
         <div class="no-more-tables">
              <div style="width:100%;display:none" class="title">
                  <div style="width:89%;float:left" >
               <asp:Label ID="Label1" runat="server" Text="Subscription Report"></asp:Label>
                      </div></div>
    <asp:UpdatePanel ID="Update1" runat="server">
    <ContentTemplate>
            <table class="table-condensed GenTable1">
         
                <tr>
                    <td >
                        <asp:Label ID="Label2" runat="server" Text="Report Option"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
                            RepeatDirection="Horizontal" >
                            <asp:ListItem Selected="True">Receipt Subscription</asp:ListItem>
                            <asp:ListItem>Issue Subscription</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td >
                        <asp:Label ID="Label3" runat="server" Text="From Date"></asp:Label>
                    </td>
                    <td>


                     
 <asp:TextBox ID="txtFromDate" runat="server"  />



                    </td>
                    <td >
                    <asp:Label ID="Label4" runat="server" Text="To Date"></asp:Label>
                </td>
                <td>


                  
 <asp:TextBox ID="txtToDate" runat="server"  />





                </td>
                </tr>
            
                <tr>
                    <td >
                        <asp:Label ID="Label5" runat="server" Text="Item Code"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtItemCode" runat="server"></asp:TextBox>
                    
                    </td>
                </tr>
               
                <tr>
                    
                    <td colspan="4" style="text-align:center">
                        <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn btn-primary" OnClick="btnPrint_Click"/>
                        <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-primary" OnClick="btnReset_Click"/>
                    </td>
                </tr>
               
        </table>
    </ContentTemplate>
    <Triggers>
    <asp:PostBackTrigger ControlID="btnPrint" />
    </Triggers>
    </asp:UpdatePanel>
             </div></div>
    <script type="text/javascript">
        //On Page Load.
        $(function () {
            SetDatePicker();
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    SetdatePicker();

                }
            });
        };

        function SetDatePicker() {
            $("[id$=txtToDate],[id$=txtFromDate]").datepicker({
                changeMonth: true,//this option for allowing user to select month
                changeYear: true, //this option for allowing user to select from year range
                dateFormat: 'dd-M-yy'  // CHANGE DATE FORMAT.
            });

        }

    </script>
</asp:Content>
 
