<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Receipt.aspx.cs"  
MasterPageFile="~/LibraryMain.master"  Inherits="Library.Receipt" %>
<asp:Content ID="Head" runat="server" ContentPlaceHolderID="head">
      <script type="text/javascript">
          function onTextBoxUpdate1(evt) {

              var textBoxID = evt.source.textBoxID;
              if (evt.selMenuItem != null) {
                  document.getElementById(textBoxID).value = evt.selMenuItem.label; // + " (modified by onTextboxUpdate)";
                  document.getElementById("Button1").click();
              }
              evt.preventDefault();
          }
     </script>
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
                    SetdatePicker();
                    ForDataTable();
                }
            });
        };
        function ForDataTable() {
            try {
                var grdId = $("[id$=hdnGrdId]").val();
                //alert(grdId);
                //$('<thead></thead>').prependTo('#' + grdId).append($('#' + grdId + ' tr:first'));
                //ThreeLevelSearch($('#' + grdId), "[id$=dvgrd]");
            }
            catch (err) {
            }
        }
        function SetDatePicker() {
            $("[id$=txtDate]").datepicker({
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
              <div style="width:100%;display:none" class="title">
                  <div style="width:89%;float:left" >
           <asp:Label ID="Label1" runat="server"  Text="Receipt" 
            Width="100%" ></asp:Label>
          </div></div>
                <table class="table-condensed GenTable1">

                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Transaction Date" ></asp:Label>
                        </td>
                        <td>
 <asp:TextBox ID="txtDate" runat="server" />
<asp:Label ID="Label11" runat="server" CssClass="star">*</asp:Label>
 </td>
                    
                        <td >
                            <asp:Label ID="Label3" runat="server" Text="Item Code"></asp:Label>
                        </td>
                        <td>
                           <asp:TextBox 
                                ID="txtItemCode" runat="server" style="margin-bottom: 0px"></asp:TextBox>
  </td>
                    </tr>
                    <tr>
                        
                    
                        <td >
                            <asp:Label ID="Label5" runat="server" Text="Rate"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRate" runat="server"></asp:TextBox>
                            <asp:Label ID="Label13" runat="server" CssClass="star">*</asp:Label>
                        </td>

                        <td >
    <asp:Label ID="Label4" runat="server" Text="Quantity"></asp:Label>
</td>
<td>
    <asp:TextBox ID="txtQty" runat="server" AutoPostBack="True" OnTextChanged="txtQty_TextChanged"></asp:TextBox>
    <asp:Label ID="Label12" runat="server" CssClass="star">*</asp:Label>
</td>
                    </tr>
                    <tr>
                        <td >
                            <asp:Label ID="Label6" runat="server" Text="Supplier"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCmbVendor" runat="server"></asp:TextBox>
                            <asp:Label ID="Label14" runat="server" CssClass="star">*</asp:Label>
 <ajax:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtCmbVendor"
                                        CompletionInterval="50"
                                        CompletionSetCount="50"
                                        MinimumPrefixLength="0"
                                        CompletionListCssClass="Publ"
                                        ServicePath="MssplSugg.asmx"
                                        ServiceMethod="GetVendor"
                                        OnClientItemSelected="GetVendJs">
                                    </ajax:AutoCompleteExtender>
                              <asp:HiddenField ID="hdVid" runat="server" />
                        </td>
                     
                        <td >
                            <asp:Label ID="Label7" runat="server" Text="Amount"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAmt" runat="server" AutoPostBack="True" OnTextChanged="txtAmt_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            <asp:Label ID="Label8" runat="server" Text="Other Expenses"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtOtherExp" runat="server" AutoPostBack="True" OnTextChanged="txtOtherExp_TextChanged"></asp:TextBox>
                        </td>
                    
                        <td >
                            <asp:Label ID="Label10" runat="server" Text="Remark"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td  style="width:21%">
                            <asp:Label ID="Label9" runat="server" Text="Total"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTotal" runat="server" ></asp:TextBox>
                        </td>
                  
                        <td >
                            <asp:Button ID="Button1" runat="server" Visible="false" Text="Button" 
                                />
                        </td>
                        <td>
                            <asp:HiddenField ID="HiddenField1" runat="server" />
                            <asp:HiddenField ID="HiddenField2" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True"  style="color:black" OnCheckedChanged="CheckBox1_CheckedChanged"
                                Text="Show Existing" />
                        </td>
                        <td style="width:15%"></td>
                        <td style="width:33%"></td>
                        </tr>
                    <tr>
                        <td colspan="4" style="text-align:center">
                            <asp:Button ID="btnsubmit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="btnsubmit_Click"/>
                            <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-primary" Text="Delete" OnClick="btnDelete_Click"/>
                            <asp:Button ID="btnReset" runat="server" CssClass="btn btn-primary" Text="Reset" OnClick="btnReset_Click"/>
                        </td>
                    </tr>
                   
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    
    </div>

            <div class="allgriddiv" id="dvgrd" runat="server">                                  
                    <asp:HiddenField runat="server" ID="hdnGrdId" />
         <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  DataKeyNames="Id" Width="100%" CssClass="allgrid GenTable1" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                <Columns>
                                <asp:ButtonField HeaderText="Id" DataTextField="Id" CommandName="select" />
                                <asp:BoundField DataField="TransactionDate" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                <asp:BoundField DataField="ItemCode" HeaderText="Item Code" />
                                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                <asp:BoundField DataField="Rate" HeaderText="Rate" />
                                <asp:BoundField DataField="SupplierName" HeaderText="Supplier" />
                                </Columns>
                            </asp:GridView></div></div>

     
</asp:Content>