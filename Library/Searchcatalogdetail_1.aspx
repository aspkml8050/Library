<%@ Page Language="C#" MasterPageFile="~/LibraryMain.Master" AutoEventWireup="true" CodeBehind="Searchcatalogdetail_1.aspx.cs" Inherits="Library.Searchcatalogdetail_1" %>


<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>
<%@ Register TagPrefix="CatE" TagName="Catalog" Src="~/UCCatalogShow.ascx" %>


<asp:Content ID="SdHead" runat="server"  ContentPlaceHolderID="head">
    <link href="cssDesign/responsive.css" rel="stylesheet" />
    <script type="text/javascript">
        function AccNoSel(sender, arg) {
            let accn = arg.get_value();
            $('[id$=txtaccno]').val(accn);
            $("[id$=cmdlogin2]").click();
        }
        function UCAccnSent(accn) {  //called from user control
            document.getElementById("<%=txtaccno.ClientID%>").value = accn;
             $("[id$=cmdlogin2]").click();

        }
        $(function () {
            setAutocomp();
        })

        function setAutocomp() {
            $("[id$=txtaccno]").keyup(function () {
                let nombre = $(this).val();
                $.ajax({
                    type: "POST",
                    url: "MssplSugg.asmx/GetAccBooksJq",
                    data: JSON.stringify({ "prefixText": nombre }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        var dataArray = JSON.parse(data.d);
                        var autocompleteObjects = [];
                        for (var i = 0; i < dataArray.length; i++) {
                            var object = {
                                // Used by jQuery Autocomplete to show
                                // autocomplete suggestions as well as
                                // the text in yourInputTextBox upon selection.
                                // Assign them to a value that you want the user to see.
                                value: dataArray[i].value,
                                label: dataArray[i].label,
                                // Put our own custom id here.
                                // If you want to, you can even put the result object.
                                id: dataArray[i].value
                            };

                            autocompleteObjects.push(object);
                        }
                        // Invoke the response callback.
                        //                        response(autocompleteObjects);
                        $('[id$=txtaccno]').autocomplete({
                            clearButton: true,
                            source: autocompleteObjects,
                            selectFirst: true,
                            minLength: 1,
                            select: function (event, ui) {
                                console.log(ui.item);
                                UCAccnSent(ui.item.value);
                                
                            }
                        });
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        console.log('Error1: ' + xhr.responseText);
                        console.log('Error2: ' + textStatus);
                        console.log('Error3: ' + errorThrown);
                    }
                });
            });
        }
    </script>
    <style type="text/css">
                       .TAccno{
       margin-top:0px !important;
    width:700px !important;
    max-height:240px;
    font-size:13px;
    /*overflow-x:scroll;*/
    overflow:auto;
    padding:1px;  
    margin:0;
    border:2px solid green;
    z-index:1000;
}
    </style>
</asp:Content>
<asp:Content id="SdMain" runat="server" ContentPlaceHolderID="MainContent">
          <asp:UpdateProgress ID="UpPorg1" runat="server">
    <ProgressTemplate>
        <NN:Mak ID="FF1" runat="server" />
    </ProgressTemplate>
    </asp:UpdateProgress>
         <div class="container tableborderst">                    

     <div style="width:100%;display:none;" class="title">
     <div style="width:89%;float:left;display:none">
         <asp:label id="lblTitle" runat="server" > Catalogue Data Editing</asp:label></div>
     <div style="float:right;vertical-align:top">
          <a id="lnkHelp" href="#" style="visibility:hidden" onclick="ShowHelp('Help/Catalogue Item DisplayEditingDeletion.htm')"><img src="help.jpg" alt="Help"  height="15" /></a>
          </div>

     </div>
                       
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
              <script type="text/javascript">
                  var prm = Sys.WebForms.PageRequestManager.getInstance();
                  if (prm != null) {
                      prm.add_endRequest(function (sender, e) {
                          if (sender._postBackSettings.panelsToUpdate != null) {
                              setAutocomp();
                          }
                      });
                  };
              </script>
                                <table id="Table2" class="table-condensed no-more-tables GenTable1">
                                    <tr>
                                        <td style="width:25%"></td>
                                        <td>
                                            <asp:Label ID="msglabel" runat="server" CssClass="err" ></asp:Label></td>
                                        
                                    </tr>
                                    <tr>
                                        
                                        <td colspan="2">
                                            <asp:RadioButton ID="optDisp" runat="server" Checked="True"
                                                 Text="<%$ Resources:ValidationResources, DisEdt %>" ValidationGroup="c"  GroupName="a" /><asp:RadioButton ID="optDel" runat="server" 
                                                    Text="<%$ Resources:ValidationResources, optDeCataloging %>" ValidationGroup="c" GroupName="a" />
                                        </td>
                                        
                                    </tr>
                                     
                                    <tr>
                                        <td><asp:Label ID="lblSelect" runat="server" CssClass="opt"
                                            Text="<%$ Resources:ValidationResources, EdtBO %>" ></asp:Label>
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="optTitle" runat="server" AutoPostBack="True"  style="display:none" 
                                                Checked="True"  GroupName="b"
                                                Text="<%$ Resources:ValidationResources, LTitle %>" ValidationGroup="c"
                                               />
                                            <asp:RadioButton ID="optAccNo" runat="server" AutoPostBack="True"  style="display:none" 
                                                 GroupName="b"
                                                Text="<%$ Resources:ValidationResources, Accnos %>" ValidationGroup="c" />
                                        </td>
                                        
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label2" runat="server"  Height="1px"  Text="<%$ Resources:ValidationResources, LSpTitle %>"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="txtSTitle" runat="server" ></asp:TextBox>
<%--                                            <Custom:AutoSuggestBox ID="txtSTitle" runat="server" AutoPostBack="false" BorderWidth="1px"
                                                Columns="30" CssClass="FormCtrl" DataType="City" Font-Names="<%$ Resources:ValidationResources, TextBox1 %>"
                                                 MaxLength="100" ResourcesDir="asb_includes" ></Custom:AutoSuggestBox>
--%>
                                       
                                            <%--<asp:Image ID="Image10" runat="server" ImageUrl="~/Images/sugg.png" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label1" runat="server"   Text="AccNo/(Title) "></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="txtaccno" 
                                                 runat="server" CssClass="txt10"  MaxLength="10" BorderWidth="1px"></asp:TextBox>
                                                  <%--<ajax:AutoCompleteExtender ID="ExtAccNo" runat="server" TargetControlID="Xtxtaccno"
                                                      MinimumPrefixLength="2"
                                                      CompletionInterval="100"
                                                      CompletionSetCount="50"
                                                      FirstRowSelected="true" 
                                                      CompletionListCssClass="TAccno"
                                                      ServicePath="~/MssplSugg.asmx"
                                                      OnClientItemSelected="AccNoSel"
                                                      EnableCaching="true" 
                                                      ServiceMethod="GetAccBooks" >
                                                 </ajax:AutoCompleteExtender>--%>



                                            <asp:TextBox ID="asmJTitle" style="display:none" runat="server" ></asp:TextBox>
<%--                                            <Custom:AutoSuggestMenu ID="asmJTitle" runat="server" Height="18px"
                                                KeyPressDelay="10" MaxSuggestChars="100"
                                                OnClientTextBoxUpdate="onTextBoxUpdate2" OnGetSuggestions="GetSuggestions"
                                                ResourcesDir="~/asm_includes" TargetControlID="txtaccno"
                                                UpdateTextBoxOnUpDown="False"  />--%>
                                        </td>
                                        
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:TextBox ID="TextBox1" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                                                Style="visibility:hidden" ReadOnly="True"></asp:TextBox>

                                        </td>
                                         
                                     <tr>
                                         
                                        <td colspan="2">
                                            <table id="Table3" class="table-condensed GenTable1" style="text-align:center">
                                                <tr>
                                                    <td>
                                                        <input type="button" id="cmdlogin" runat="server" style="display:none" />
                                                         <asp:Button ID="cmdlogin2" runat="server"  CssClass="btn btn-primary" Text="<%$ Resources:ValidationResources, bShwRec %>" OnClick="cmdlogin2_Click" />

                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                         
                                    </tr>
                                </table>
                            <input id="hdItemType" runat="server" name="hdItemType" size="1"  type="hidden" />
						<INPUT id="hdSearch"  type="hidden" size="1" value="0"
							name="hdSearch" runat="server">

          </ContentTemplate>
            </asp:UpdatePanel>
             <asp:UpdatePanel ID="upSearch" runat="server" >
     <ContentTemplate>
       <CatE:Catalog ID="CatgEdit" runat="server" />
     </ContentTemplate>
 </asp:UpdatePanel>
    </div>
</asp:Content>
