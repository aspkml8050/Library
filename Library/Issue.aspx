<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Issue.aspx.cs" MasterPageFile="~/LibraryMain.master" Inherits="Library.Issue" %>

<asp:Content ID="Head" runat="server" ContentPlaceHolderID="head">
      <script type="text/javascript" >
          function onTextBoxUpdate1(evt) {

              var textBoxID = evt.source.textBoxID;
              if (evt.selMenuItem != null) {
                  document.getElementById(textBoxID).value = evt.selMenuItem.label; // + " (modified by onTextboxUpdate)";
                  document.getElementById("Button1").click();
              }
              evt.preventDefault();
          }
     </script>
   
     <style type="text/css" >
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
</asp:Content>
<asp:Content ID="Main" runat="server" ContentPlaceHolderID="MainContent">
     <div class="container tableborderst">   
         <div class="no-more-tables">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                 <script type="text/javascript">
                     var prm = Sys.WebForms.PageRequestManager.getInstance();
                     if (prm != null) {
                         prm.add_endRequest(function (sender, e) {
                             if (sender._postBackSettings.panelsToUpdate != null) {
                                // SetdatePicker();
                                // ForDataTable();
                             }
                         });
                     };
                 </script>
        <div style="width:100%;display:none" class="title">
                  <div style="width:89%;float:left" >
           <asp:Label ID="Label1" runat="server"  Text="Issue" 
            Width="100%" ></asp:Label>
            </div></div>
       
                <table class="table-condensed GenTable1">
                   
                    <tr>
                        <td >
                            <asp:Label ID="Label2" runat="server" Text="Issue Type"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList1" Height="30" runat="server">
                                <asp:ListItem Selected="True">---Select---</asp:ListItem>
                                <asp:ListItem>Complimentry</asp:ListItem>
                                <asp:ListItem>Subscription</asp:ListItem>
                                <asp:ListItem>Sale</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                   
                        <td >
                            <asp:Label ID="Label7" runat="server" Text="Document Date"></asp:Label>
                        </td>
                        <td>

                            

 <asp:TextBox ID="txtDocDate" runat="server"  />



                        </td>
                    </tr>
                    <tr>
                        <td >
                            <asp:Label ID="Label8" runat="server" Text="Document Number"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDocNo" runat="server"></asp:TextBox>
                        </td>
                   
                        <td >
                            <asp:Label ID="Label3" runat="server" Text="Item Code"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtItemCode" runat="server"></asp:TextBox>
                             <ajax:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtItemCode"
                                        CompletionInterval="50"
                                        CompletionSetCount="50"
                                        MinimumPrefixLength="0"
                                        CompletionListCssClass="Publ"
                                        ServicePath="~/Issue.aspx"
                                        ServiceMethod="GetItem"
                                        OnClientItemSelected="GetVendJs">
                                    </ajax:AutoCompleteExtender>
                              <asp:HiddenField ID="hdVid" runat="server" />
                
                        </td>
                    </tr>
                    <tr>
                        <td style="width:21%">
                            <asp:Label ID="Label4" runat="server" Text="Quantity"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtQty" runat="server" AutoPostBack="True" OnTextChanged="txtOtherExp_TextChanged"></asp:TextBox>
                        </td>
                     
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Rate"></asp:Label>
                        </td>
                        <td>
                            <%--OnTextChanged="txtRate_TextChanged"--%>
                            <asp:TextBox ID="txtRate" AutoPostBack="true" runat="server" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            <asp:Label ID="Label6" runat="server" Text="Amount"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAmt" runat="server"></asp:TextBox>
                        </td>
                   
                        <td >
                            <asp:Label ID="Label9" runat="server" Text="Other Expense"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtOtherExp" runat="server" AutoPostBack="True" OnTextChanged="txtOtherExp_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            <asp:Label ID="Label10" runat="server" Text="Total Amount"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTotalAmt" runat="server"></asp:TextBox>
                        </td>
                     
                        <td >
                            <asp:Button ID="Button1" runat="server" Visible="false" Text="Button" Height="0px" 
                                Width="0px" Onclick="Button1_Click"/>
                        </td>
                        <td>
                            <asp:HiddenField ID="HiddenField1" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" style="color:black"
                                Text="Show Existing" OnCheckedChanged="CheckBox1_CheckedChanged"/>
                        </td>
                        <td></td>
                        <td></td>
                        </tr>
                    <tr>
                        <td colspan="4" style="text-align:center">
                            <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnsubmit_Click"/>
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-primary" OnClick="btnDelete_Click"/>
                            <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-primary" OnClick="btnReset_Click"/>
                        </td>
                    </tr>
                  
                </table>
                  <script type="text/javascript">
                      //On Page Load.
                      $(function () {
                          SetDatePicker();
                          ForDataTable();
                      });
                      var prm = Sys.WebForms.PageRequestManager.getInstance();
                      if (prm != null) {
                          prm.add_endRequest(function (sender, e) {
                              if (sender._postBackSettings.panelsToUpdate != null) {
                                  SetDatePicker();
                                  ForDataTable();
                              }
                          });
                      };
                      function ForDataTable() {
                          try {
                              var grdId = $("[id$=hdnGrdId]").val();
                              //alert(grdId);
                              //$('<thead></thead>').prependTo('#' + grdId).append($('#' + grdId + ' tr:first'));
                              ThreeLevelSearch($('#' + grdId), "[id$=dvgrd]");
                          }
                          catch (err) {
                          }
                      }

                      function SetDatePicker() {
                          $("[id$=txtDocDate]").datepicker({
                              changeMonth: true,//this option for allowing user to select month
                              changeYear: true, //this option for allowing user to select from year range
                              dateFormat: 'dd-M-yy'  // CHANGE DATE FORMAT.
                          });

                      }
                      function GetVendJs(sender, arg) {
                          let id = arg.get_value();
                          $('[id$=hdVid]').val(id);
                          $('[id$=btnSb]').click();
                      }
                  </script>
                  <div class="allgriddiv" id="dvgrd" runat="server">                                  
                    <asp:HiddenField runat="server" ID="hdnGrdId" />
          <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  DataKeyNames="DocNumber" CssClass="allgrid GenTable1" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" >
                                <Columns>
                                    <asp:ButtonField CommandName="select" DataTextField="DocNumber"  
                                        HeaderText="Doc Number" />
                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code" />
                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                    <asp:BoundField DataField="Rate" HeaderText="Rate" />
                                    <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                    <asp:BoundField DataField="OtherExp" HeaderText="Other Expenses" />
                                    <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" />
                                </Columns>
                            </asp:GridView></div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
       
     </div>
</asp:Content>
 