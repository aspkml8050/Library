<%@ Page Language="C#"  Async="true" AutoEventWireup="true" MasterPageFile="~/LibraryMain.Master" CodeBehind="DepartmentMaster.aspx.cs" Inherits="Library.DepartmentMaster" %>


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
                <asp:Button ID="btnt" runat="server" Text="testit" OnClick="btnt_Click" />
         <asp:Label ID="msglabel" runat="server"  CssClass="err"></asp:Label>
        <div class="container tableborderst" >
          <div style="width:100%;display:none" class="title">
               <div style="width:89%;float:left; display:none;" >
                     <asp:Label ID="lblt1" runat="server" Visible="false" Width="100%" style="text-align:center;"></asp:Label>
                   </div>
                <div  style="float:right;vertical-align:top;display:none">
                     <a id="lnkHelp" href="#" style="display:none;" onclick="ShowHelp('Help/Masters-Departments.htm')">
                        <img alt="Help?" height="15" src="help.jpg"  /> </a>
                     <a id="helpshow" href="#" style="display:none;" onclick="helpDisp();">
                <img alt="Help?" height="18" src="help.jpg" /></a>&nbsp;
          </div></div>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                          <div class="no-more-tables" style="width:100%">
                                <table id="Table1" class="table-condensed cf GenTable1"    >
                                   
                                    <tr>
                                        <td>
                                      <asp:Label ID="lbldepartmentname" runat="server"  CssClass="span" Text="<%$ Resources:ValidationResources, LDeptName %>"></asp:Label></td>
                                        <td>
                                            <input class="txt10" id="txtdepartmentname" onkeypress="disallowSingleQuote(this);" onblur="this.className='blur'"
                                             onfocus="this.className='focus'" type="text" name="txtdepartmentname" runat="server" maxlength="50" >
                                            <asp:Label ID="Label3" runat="server" CssClass="star" >*</asp:Label></td>
                                     
                                        <td><asp:Label ID="lblshortname" runat="server"  Text="<%$ Resources:ValidationResources, LShortName%>"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="txtshortname" runat="server" BorderColor="Black"  BorderWidth="1px" BorderStyle="Solid" onblur="this.className='blur'" onfocus="this.className='focus'" MaxLength="10"></asp:TextBox>
                                            <asp:Label ID="Label8" runat="server"  CssClass="star">*</asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td >
                                           <asp:Label ID="Label2" runat="server" Text="<%$ Resources:ValidationResources, LInstiName%>" ></asp:Label></td>
                                        <td colspan="3">
                                            <asp:DropDownList ID="cmbInstName" Width="96%" runat="server"  onblur="this.className='blur'" onfocus="this.className='focus'" Height="50" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'>
                                            </asp:DropDownList>
                                            <asp:Label ID="Label1" runat="server"  CssClass="star">*</asp:Label></td>
                                    </tr>
                                    <tr>

                                        <td colspan="4" style="text-align:center">
                                            <asp:Button ID="cmdsave2" runat="server" CssClass="btn btn-primary"  Text="<%$Resources:ValidationResources,bSave %>" OnClick="cmdsave2_Click" />
                                                        <input id="cmdsave"  type="submit"  style="display:none"  value="<%$Resources:ValidationResources,bSave %>" name="cmdsave" onclick=" if ( validateform()==false ) return false; else true ;"
                                                            runat="server" accesskey="S" class="btnstyle">
                                                   <asp:Button  ID="cmdreset2" runat="server" CssClass="btn btn-primary" Text="<%$Resources:ValidationResources,bReset %>"
                                                        OnClick="cmdreset2_Click" />
                                                        <input id="cmdreset"  style="display:none"  type="submit" value="<%$Resources:ValidationResources,bReset %>" name="cmdreset"
                                                            runat="server" accesskey="E" class="btnstyle">
                                                   <asp:Button ID="cmddelete2"  runat="server" CssClass="btn btn-primary" Text="<%$Resources:ValidationResources,bDelete %>"
                                                        OnClick="cmddelete2_Click" />
                                                        <input id="cmddelete" style="display:none"  onclick="if (confirm() == false) return false;"
                                                            type="button" value="<%$Resources:ValidationResources,bDelete %>" name="cmddelete" runat="server" accesskey="X" class="btnstyle">
                                               
                                        </td>
                                    </tr>
                                    <tr style="display:none">

                                        <td colspan="4">
                                            <asp:Label ID="Label14" runat="server"  CssClass="head1" Text="<%$Resources:ValidationResources,LExistDeptDetail %>"></asp:Label></td>
                                    </tr>
                                  
                                </table></div>
                               
                                 <div class="allgriddiv" id="dvgrd"  runat="server">                                  
               <asp:HiddenField runat="server" ID="hdnGrdId" />

                                       <asp:DataGrid ID="DataGrid1" Width="100%"  runat="server" OnItemCommand="DataGrid1_ItemCommand" Font-Names='<%$resources: ValidationResources, TextBox1 %>'  AutoGenerateColumns="False" >
                                                <Columns>
                                                    <asp:ButtonColumn Text=" Department Name" DataTextField="departmentname" 
                                                        HeaderText="<%$ resources:ValidationResources, LDeptName %>" CommandName="Select"></asp:ButtonColumn>
                                                    <asp:BoundColumn Visible="False" DataField="departmentcode" HeaderText="<%$ resources:ValidationResources, GrDeptCod %>"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="shortname" ReadOnly="True" HeaderText="<%$ resources:ValidationResources, LShortName %>"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="institutename" HeaderText="<%$ resources:ValidationResources, LInstiName %>" ></asp:BoundColumn>
                                                </Columns>

                                            </asp:DataGrid>
                                    </div>
                                <input id="HComboSelect" runat="server"
                                    type="hidden" value="<%$ Resources:ValidationResources, ComboSelect %>" style="width: 75px" class="btn" /><input id="hdTop" style="width: 32px; height: 22px" type="hidden" size="1" name="hdTop"
                                        runat="server"><input id="hdUnableMsg" style="width: 32px; height: 22px" type="hidden" size="1" name="hdUnableMsg"
                                            runat="server"><input id="hdopenB" runat="server" style="width: 38px" type="hidden" /><input id="Hidden6" style="width: 72px" type="hidden" runat="server" /><input id="xCoordHolder" style="width: 31px; height: 21px" type="hidden" name="Hidden5"
                                                runat="server"><input id="Hidden5" style="width: 72px" type="hidden" runat="server" /><input id="txtdepartmentcode" style="width: 18px; height: 22px" type="hidden" size="1"
                                                    name="Hidden4" runat="server">
                                <input id="yCoordHolder" runat="server" name="yCoordHolder" size="1" type="hidden"
                                    value="0" />
                            </ContentTemplate>

                        </asp:UpdatePanel>                 
     </div>
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

            //$('<thead></thead>').prependTo('#' + grdId).append($('#' + grdId + ' tr:first'));
            try {
                let grd = $("[id$='MainContent_DataGrid1']");
                let leng = grd.find('tr').length;
                if (leng >0) {
                    $("#MainContent_DataGrid1 tbody").before("<thead><tr></tr></thead>");
                    var cols = $("#MainContent_DataGrid1 tbody tr:first td");
                    let colsh = '';
                    for (let x = 0; x < cols.length; x++) {
                        colsh += '<th>' + $(cols[x]).text() + '</th>';
                    }
                    $("#MainContent_DataGrid1 thead tr").append(colsh);
                    //$("#MainContent_DataGrid1 tbody tr:first").remove();
                    $("#MainContent_DataGrid1").DataTable();
                }
            } catch (er) {
                alert(er + '; Make sure grid has data');
            }

        }
        catch (err) {
        }
    }


</script>
	<INPUT id="Hidden1" style="Z-INDEX: 102; WIDTH: 3px; POSITION: absolute; TOP: 216px; HEIGHT: 22px"
		type="hidden" size="1" name="Hidden1" runat="server" /><INPUT id="Hidden2" style="Z-INDEX: 103; LEFT: 408px; WIDTH: 5px; POSITION: absolute; TOP: 168px; HEIGHT: 22px"
		type="hidden" size="1" name="Hidden2" runat="server" /><INPUT id="Hidden3" style="Z-INDEX: 104; LEFT: 408px; WIDTH: 5px; POSITION: absolute; TOP: 240px; HEIGHT: 22px"
		type="hidden" size="1" name="Hidden3" runat="server" /> <INPUT id="Hidden4" style="Z-INDEX: 105; LEFT: 408px; WIDTH: 5px; POSITION: absolute; TOP: 272px; HEIGHT: 22px"
		type="hidden" size="1" name="Hidden4" runat="server" />
       </ContentTemplate>

    </asp:UpdatePanel>    
</asp:Content>