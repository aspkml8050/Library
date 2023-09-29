<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/LibraryMain.master"   CodeBehind="IdForm.aspx.cs" Inherits="Library.IdForm" %>
<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>

<asp:Content ID="IdFHead" runat="server" ContentPlaceHolderID="head">
    <Link href="cssDesign/tdmanage.css" rel="stylesheet" type="text/css" >
	    <style type="text/css">
            .style1
            {
                width: 121px;
            }
             .dataTables_scrollBody
            {
                height:auto!important;
                max-height:300px!important;
                
            }
        </style>

</asp:Content>

<asp:Content ID="IdFMain" runat="server" ContentPlaceHolderID="MainContent">
 <asp:UpdateProgress ID="UpPorg1" runat="server">
    <ProgressTemplate>
    <NN:Mak ID="FF1" runat="server" />
    </ProgressTemplate>
    </asp:UpdateProgress>

    <div class="container tableborderst" >   
        
			 <div style="width:100%;display:none" class="title">
                  <div style="width:89%;float:left" >
        <asp:Label ID="lblt1" runat="server"  Width="100%" style="text-align: center">Document Number Settings</asp:Label>
                      </div>
                 <div style="float:right;vertical-align:top"> 
                    <a id="lnkHelp" href="#" onclick="ShowHelp('Help/Masters-Document number.htm')"><img src="help.jpg" alt="Help" height="15" /></a>
           </div>
                 </div>
                       
                    <p>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                       <div class="no-more-tables" style="width:100%">
   <table id="Table1" class="table-condensed tdmgr GenTable1">
                            <tr>
                                <td colspan="2" >
                                    <asp:Label ID="msglabel" runat="server" CssClass="err" ></asp:Label></td>
                            </tr>
                            <tr>
                                 
                                            <td>
                                               <asp:Label ID="lblObjectName" runat="server" Text="<%$ Resources:ValidationResources, LblFormName %>" CssClass="span"></asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txttitle" runat="server" Columns="30" CssClass="txt10" MaxLength="100"
                                                    onblur="this.className='blur'" onfocus="this.className='focus'" ></asp:TextBox>
                                                <asp:Image ID="Image6" runat="server" ImageUrl="~/Images/sugg.png" />
                                                <asp:Label ID="Label2" runat="server" CssClass="star" Height="2px" Width="1px">*</asp:Label><br />
<%--                                                <custom:autosuggestmenu id="asmtitle" runat="server" keypressdelay="10" maxsuggestchars="100"
                                                    onclienttextboxupdate="onTextBoxUpdate" ongetsuggestions="GetSuggestions" resourcesdir="~/asm_includes"
                                                    targetcontrolid="txttitle" updatetextboxonupdown="False" ></custom:autosuggestmenu>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:29%">
                                                <asp:Label ID="LlblPrefix" runat="server" Text="<%$ Resources:ValidationResources, LblPrefix %>" CssClass="span"></asp:Label></td>
                                            <td>
                                                <input id="txtprefix" runat="server" class="txt10" onblur="this.className='blur'"  onfocus="this.className='focus'" type="text" /></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblSuffix" runat="server" Text="<%$ Resources:ValidationResources, LblSuffix %>" CssClass="span"></asp:Label></td>
                                            <td>
                                                <input id="txtsuffix" runat="server" class="txt10"  onblur="this.className='blur'"  onfocus="this.className='focus'" type="text" /></td>
                                        </tr>
                                        <tr>
                                            <td>
                                               <asp:Label ID="lblCurrentPosition" runat="server" Text="<%$ Resources:ValidationResources, LblCurrentPosition %>" CssClass="span" ></asp:Label></td>
                                            <td>
                                                <input id="txtcurrentposition" runat="server" class="txt10"  onblur="this.className='blur'"  onfocus="this.className='focus'" type="text" readonly="readOnly" /></td>
                                        </tr>
                                     
                            <tr>
                                <td colspan="2" style="text-align:center">
                                     
                                            
                                            <input id="Hidden1" runat="server" type="hidden"  />
                                           
                                             
                                               <%-- <input id="cmdsave" runat="server" name="cmdsave" 
                                                    type="button" value="<%$Resources:ValidationResources,bSave %>" 
                                                    accesskey="S" class="btnstyle" />--%>
                                    <asp:Button ID="cmdsave" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="cmdsave_Click"/>
                                                <%--<input id="cmdreset" runat="server" name="cmdreset" 
                                                    type="button" value="<%$Resources:ValidationResources,bReset %>" 
                                                    accesskey="E" class="btnstyle" />--%>
                             <asp:Button ID="cmdreset" runat="server" Text="Reset" CssClass="btn btn-primary" OnClick="cmdreset_Click" />           
                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                            </td>
                                            
                                            
                                        </tr>
                                        <tr>
                                            <td colspan="2" >
                                              <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged"
                                                    Text="Show Existing"  /> 
                                            </td>
                                             
                                        </tr>
                                         
                                    
                        </table>
<asp:Button ID="cmdSerach" runat="server" BackColor="White" CausesValidation="False"
            class="btnH" ForeColor="White" Visible="false" Height="1px" Text="<%$ Resources:ValidationResources, Go %> "
            UseSubmitBehavior="False" Width="1px" /></div>
                                <div class="tdmgr">
                                 <div class="allgriddiv" id="dvgrd" runat="server">                                  
                    <asp:HiddenField runat="server" ID="hdnGrdId" />
   <%-- OnSelectedIndexChanged ="GridView1_SelectedIndexChanged1" --%>
                                     <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"  
                      OnPageIndexChanging="GridView1_PageIndexChanging"                           DataKeyNames="ObjectName" CssClass="allgrid GenTable1" PageSize="1000000">
                                                      <Columns>

                                                          <asp:ButtonField CommandName="select" DataTextField="ObjectName" 
                                                              HeaderText="Form Name" />
                                                          <asp:BoundField DataField="Prefix" HeaderText="Prefix Name" />
                                                          <asp:BoundField DataField="Suffix" HeaderText="Suffix" />
                                                          <asp:BoundField DataField="CurrentPosition" HeaderText="Current Position" />
                                                      </Columns>
                                                  </asp:GridView>

                                 </div>
                                    </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                       </p>
        </div>

      <script type="text/javascript">
   
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
                //$('<thead></thead>').prependTo('#' + grdId).append($('#' + grdId + ' tr:first'));
                ThreeLevelSearch($('#'+ grdId), "[id$=dvgrd]");
            }
            catch (err) {
            }
        }

    
      </script>
</asp:Content>




