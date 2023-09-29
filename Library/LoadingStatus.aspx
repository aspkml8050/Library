<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/LibraryMain.Master" CodeBehind="LoadingStatus.aspx.cs" Inherits="Library.LoadingStatus" %>
<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>
<asp:Content ID="cHead" runat="server" ContentPlaceHolderID="head">
 <%--   <script type="text/javascript" src="JavaScript/messageLibrary.js"></script>--%>
		<%--<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="VBScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">--%>
     <Link href="cssDesign/tdmanage.css" rel="stylesheet" type="text/css" >
		<script  type="text/javascript">

            function chk() {
                if (document.Form1.hdTop.value == "top") {
                    window.scrollTo(0, 0);
                    document.Form1.hdTop.value = 0;
                }

            }


            function txtshortname_OnKeypress() {
                // to disallow - sign
                if (window.event.keyCode == 45) {
                    window.event.keyCode = 0;
                }
            }
            function validateform() {
                let v1 = $('[id$=txtStatus]').val().trim();
                if (v1 == '') {
                    alert('Item Status required.');
                    return false;
                }
                let v2 = $('[id$=txtshortname]').val().trim();
                if (v2 == '') {
                    alert('Item Status short Name required.');
                    return false;

                }
                return true;
            }

        </script>
		<script type="text/javascript">
            window.history.forward(1);
        </script>

</asp:Content>

<asp:Content ID="CMain" runat="server" ContentPlaceHolderID="MainContent">
     <div class="container tableborderst" >

              <div style="width:100%;display:none;display:none" class="title">
                    <div style="width:89%;float:left" >&nbsp;
        <asp:Label ID="lblt1" runat="server" style="text-align:center;display:none" Width="100%"></asp:Label>
                    </div>
                   <div style="float:right;vertical-align:top"> 
                        <a id="lnkHelp" href="#" style="display:none;" onclick="ShowHelp('Help/Masters-loading status.htm')"><img height="15" src="help.jpg" /></a>
            </div></div>
                    <p>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                 <div class="no-more-tables" style="width:100%">
                        <table id="Table1" class="table-condensed GenTable1">
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="msglabel" runat="server" CssClass="err" ></asp:Label></td>
                            </tr>
                            <tr>
                                <td >
                                    <asp:Label ID="lbldepartmentname" runat="server" CssClass="span" 
                                       Text ="<%$ Resources:ValidationResources,LItemSt %>"></asp:Label></td>
                                <td  >
                                    <input id="txtStatus" runat="server" class="txt10" name="txtdepartmentname"
                                        onblur="this.className='blur'" onfocus="this.className='focus'" onkeypress="disallowSingleQuote(this);"  style="<%$ Resources:ValidationResources, TextBox2 %>"
                                        type="text" size="50" />
                                    <asp:Label ID="Label8" runat="server" CssClass="star">*</asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblshortname" runat="server" CssClass="span" Text ="<%$ Resources:ValidationResources,LShortName %>"></asp:Label></td>
                                <td >
                                    <input id="txtshortname" runat="server" class="txt10" maxlength="9" name="txtshortname" onblur="this.className='blur'" onfocus="this.className='focus'" onkeypress="txtshortname_OnKeypress();disallowSingleQuote(this);" style="'<%$Resources:ValidationResources, TextBox2 %>'; width: 95%;" type="text" size="10"/>
                                    <asp:Label ID="Label1" runat="server" CssClass="star" >*</asp:Label></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:CheckBoxList ID="chkboth" runat="server" RepeatDirection="Horizontal"
                                       >
                                        <asp:ListItem Value="<%$ Resources:ValidationResources,BA %>"></asp:ListItem>
                                        <asp:ListItem Value="<%$ Resources:ValidationResources,CBI %>"></asp:ListItem>
                                    </asp:CheckBoxList></td>
                            </tr>
                            <tr>
                               
                                <td colspan="3" style="text-align:center">
                                   
                                                <input id="cmdsave1" runat="server" name="cmdsave" onclick =" if ( validateform()==false ) return false; else true ;" type="button" value="<%$ Resources:ValidationResources,bSave %>" accesskey="S" 
                                         style="display:none;" class="btnstyle" />
                                    
                                    <asp:Button ID="cmdsave2" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="cmdsave2_Click"/>
                                         
                                                <input id="cmdreset" runat="server" name="cmdreset" style="display:none;" type="button" value="<%$ Resources:ValidationResources,bReset %>" accesskey="E" class="btnstyle" />
                                          <asp:Button ID="cmdreset1" runat="server" Text="reset" CssClass="btn btn-primary" OnClick="cmdreset1_Click"/>

                                                <input id="cmddelete" runat="server" name="cmddelete" onclick="if (DoConfirmation() == false) return false;"
                                         style="display:none;"  type="button" value="<%$ Resources:ValidationResources,bDelete %>" accesskey="X" class="btnstyle" />
                                    <asp:Button ID="cmddelete1" runat="server" Text="Delete" CssClass="btn btn-primary" OnClick="cmddelete1_Click"/>

                                     
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                     <asp:Label ID="Label14" CssClass="showBoldExist" runat="server" Text ="<%$ Resources:ValidationResources,Lblassignstatus %>"></asp:Label>
                                </td>
                               
                            </tr>
                           
                            
                        </table></div>
                                <div class="allgrididv" style="margin-top:5px;margin-bottom:5px" id="Div1" runat="server">                                  
                    <asp:HiddenField runat="server" ID="HiddenField1" />
    <asp:DataGrid ID="DataGrid1"  runat="server" OnItemCommand="DataGrid1_ItemCommand1" CssClass="allgrid GenTable1" Width="100%" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'>
                                         <Columns>
                                            <asp:ButtonColumn CommandName="Select" DataTextField="ItemStatus" HeaderText="<%$ Resources:ValidationResources, LItemSt %>"
                                                 Text="<%$ Resources:ValidationResources, GrItemS %>">
                                             </asp:ButtonColumn>
                                            <asp:BoundColumn DataField="ItemStatusID" HeaderText="<%$ Resources:ValidationResources,ItStatid %>" Visible="False">
                                                
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="ItemStatusShort" HeaderText="<%$ Resources:ValidationResources, LShortName %>" ReadOnly="True" >
                                                
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="isBardateApllicable" HeaderText="<%$ Resources:ValidationResources, BA %>">
                                                
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="isIsued" HeaderText="<%$ Resources:ValidationResources, CBI %>">
                                                
                                            </asp:BoundColumn>
                                        </Columns>
                                        
                                    </asp:DataGrid></div>
                                <input id="txtdepartmentcode"
                        runat="server" name="Hidden4" size="1" style="width: 18px; height: 22px" type="hidden" />
                            </ContentTemplate>

                        </asp:UpdatePanel>
                        &nbsp;</p>
               </div>
     <input id="xCoordHolder" runat="server" name="Hidden5" style="width: 39px; height: 6px"
                            type="hidden" /><input id="yCoordHolder" runat="server" name="Hidden5" type="hidden" style="width: 64px" /><input id="hdTop" runat="server" name="hdTop"
                                size="1" style="width: 32px; height: 22px" type="hidden" /><input id="hdUnableMsg"
                                    runat="server" name="hdUnableMsg" size="1" style="width: 32px; height: 22px"
                                    type="hidden" />
        <input id="Hidden2" runat="server" name="Hidden2" size="1" style="width: 5px; height: 22px" type="hidden" />
            <input id="Hidden1" runat="server" name="Hidden1" style="width: 40px; height: 22px" type="hidden" />
        &nbsp;
        <input id="Hidden3" runat="server" name="Hidden3" size="1" style="width: 5px; height: 22px" type="hidden" />
        <input id="Hidden4" runat="server" name="Hidden4" size="1" style="z-index: 105; left: 408px;
            width: 5px; position: absolute; top: 272px; height: 22px" type="hidden" />
  
    <script type="text/javascript">

        //On Page Load.
        $(function () {
            ForDataTable();
        });

        //On UpdatePanel Refresh.
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    ForDataTable();
                }
            });
        };
        function ForDataTable() {
            try {
                var grdId = $("[id$=hdnGrdId]").val();
                //alert(grdId);
                $('<thead></thead>').prependTo('#' + grdId).append($('#' + grdId + ' tr:first'));
                ThreeLevelSearch($('#' + grdId), "[id$=dvgrd]");
            }
            catch (err) {
            }
        }


    </script>
      <style>
         .dataTables_scrollBody
            {
                height:auto!important;
                max-height:300px!important;
                
            }
    </style>
</asp:Content>

