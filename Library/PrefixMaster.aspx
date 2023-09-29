<%@ Page Language="C#" MasterPageFile="~/LibraryMain.master" AutoEventWireup="true" CodeBehind="PrefixMaster.aspx.cs" Inherits="Library.PrefixMaster" %>
<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>

<asp:Content ID="pmasHead" runat="server" ContentPlaceHolderID="head">
     <script type="text/javascript">

            function setfocus() { //not in use
                document.Form1.txtprefixname.focus(); //
            }
            function check() {
                var length_shortdesc
                length_shortdesc = document.Form1.txtstartno.value;

                length_shortdesc = parseInt(length_shortdesc, 10);
                if (length_shortdesc > 0) {

                }
                else {
                    alert("<asp:Literal runat='server' Text='<%$ Resources:ValidationResources,MSGPlsEnterNumericValue %>' />");
                    document.Form1.txtstartno.value = "";
                    document.Form1.txtstartno.focus();

                }
            }

            function cmdSave1_Click() {
                if (document.Form1.txtprefixname.value.length == 0 && document.Form1.txtsuffix.value.length == 0) {
                    alert("<asp:Literal runat='server' Text='Enter Prefix Or Suffix' />");
                    //document.Form1.txtprefixname.value="";
                    document.Form1.txtprefixname.focus();
                    return false;
                }
                else { return true; }
            }
            function chk() {
                if (document.Form1.hdTop.value == "33") {
                    window.scrollTo(0, 0);
                    document.Form1.hdTop.value = 0;
                }

            }

            function IntegerNumber(ele,e) {
                let ent = $(ele).val();
                let int = ent * 1;
                if (isNaN(int)==true) {
                    e.preventDefault();
                    return false;
                }
                return true;
            }
     </script>
    <script type="text/javascript">
        function txtprefixname_OnKeyPress() {
            if ((window.event.keyCode >= 97) && (window.event.keyCode <= 122)) {
                window.event.keyCode = window.event.keyCode - 32;
            }
        }
    </script>
		<script type="text/javascript">
            window.history.forward(1);
        </script>

</asp:Content>

<asp:Content ID="pmasBody" runat="server" ContentPlaceHolderID="MainContent">
         <asp:UpdateProgress ID="UpPorg1" runat="server">
    <ProgressTemplate>
    <NN:Mak ID="FF1" runat="server" />
    </ProgressTemplate>
    </asp:UpdateProgress>

             
				
						 
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
 <div style="width:100%;display:none;display:none" class="title">
                    <div style="width:89%;float:left" >
                                    <asp:label id="lblTitle" runat="server"  Width ="100%" style="text-align:center"></asp:label>
                        </div> <div style="float:right;vertical-align:top">           
                        <a id="lnkHelp" href="#" onclick="ShowHelp('Help/Masters-Accession prefix.htm')"><img height="15" src="help.jpg"  alt="Help"/></a>
							</div></div>

   <div class="container tableborderst">
                    <table id="Table1" class="table-condensed no-more-tables GenTable1">
								<tr>
									<td style="width:21%"></td>
                                    <td colSpan="3"><%--<asp:label id="msglabel" runat="server" CssClass="err">msglabel</asp:label>--%></td>
                                     
								</tr>
								<tr>
									<td >
                                         <asp:label id="Label3" runat="server" CssClass="span" Text ="<%$ Resources:ValidationResources,Lprifix %>" ></asp:label></td>
									<td  ><INPUT onkeypress="disallowSingleQuote(this);txtprefixname_OnKeyPress();"
											id="txtprefixname" onblur="this.className='blur'" style='<%$ Resources:ValidationResources, TextBox2 %>'
											onfocus="this.className='focus'" type="text" maxLength="4" size="9" name="txtprefixname" runat="server">
										</td>
                                    <td >
                                        <asp:Label ID="lblsuffix" runat="server" CssClass="span" Text="<%$ Resources:ValidationResources,Lsuffix %>" ></asp:Label>

                                    </td>
                                    <td>
                                        <INPUT class="txt10" onkeypress="disallowSingleQuote(this);txtprefixname_OnKeyPress();"
											id="txtsuffix" onblur="this.className='blur'" style='<%$ Resources:ValidationResources, TextBox2 %>'
											onfocus="this.className='focus'" type="text" maxLength="4" size="9" name="txtprefixname" runat="server">

                                    </td>
                                    
								</tr>
                       
								<tr>
									<td > <asp:label id="Label5" runat="server" CssClass="span"  Text ="<%$ Resources:ValidationResources,LStart %>"></asp:label> 
                                    </td>
									<td ><INPUT class="txt10"  id="txtstartno"  style='<%$ Resources:ValidationResources, TextBox2 %>;height:30px;width:90%'
											onblur="this.className='blur'" onfocus="this.className='focus'" type="number"  maxLength="10" size="9" name="txtstartno" runat="server">
										<asp:label id="Label1" runat="server" CssClass="star">*</asp:label></td>
                                    <td style="width:19%">
                                    </td>
                                    <td style="width:30%">
                                    </td>
                                     
								</tr>
                                    
								<tr>
									<td >
                                        <asp:label id="Label2" runat="server" CssClass="span" 
                                            Text ="<%$ Resources:ValidationResources,LCat %>" Visible="False"></asp:label></td>
                                    <td colspan="3">
                                        <asp:dropdownlist id="cmbCategory" onblur="this.className='blur'" onfocus="this.className='focus'"
											runat="server" CssClass="txt10"  DESIGNTIMEDRAGDROP="92" 
                                            Font-Names='<%$ Resources:ValidationResources, TextBox1 %>' Visible="False">
											</asp:dropdownlist>
                                        <asp:Label ID="Label4" runat="server" CssClass="star" Text="*" Width="1px" 
                                            Visible="False"></asp:Label></td>
                                   
                                    
								</tr>
								<tr>
									<td colspan="4" >
                                    <INPUT id="txtprefixid" readOnlytype="text" size="1" name="txtprefixid" runat="server" style="display:none"/>
                                    <div  style="text-align:center;" >

										<%--<INPUT id="cmdSave"  type="button" value="<%$ Resources:ValidationResources,bSave %>" name="cmdSave" runat="server" accesskey="S" class="btnstyle">--%>
                                        <asp:Button id="cmdSave1" runat="server"  Text="Submit" class="btn btn-primary" OnClick="cmdSave1_Click"/>

											<%--<INPUT id="cmdreset" type="button" value="<%$ Resources:ValidationResources,bReset %>" name="cmdreset"
														runat="server" accesskey="E" class="btnstyle">--%>

                                <asp:Button id="cmdreset1" runat="server" Text="Reset" class="btn btn-primary" OnClick="cmdreset1_Click"/>
												<%--<INPUT id="cmddelete"  onclick="if (DoConfirmation() == false) return false;"
														type="button" value="<%$ Resources:ValidationResources,bdelete %>" name="cmddelete" runat="server" accesskey="X" class="btnstyle">--%>
							<asp:Button id="cmddelete1" runat="server" Text="Delete" class="btn btn-primary" OnClick="cmddelete1_Click"/>				
                                    </div>
                                        </td>
                                   
								</tr>
								<tr>
									<td colSpan="4">
                                            <asp:label id="Label14" runat="server" CssClass="showBoldExist" Text ="<%$ Resources:ValidationResources,LPrefixDetail %>"></asp:label>
									</td>
                                     
								</tr>
							
							</table>

                      <%--  <div class="allgriddiv" style="display:none;">  not in use
                   <asp:datagrid id="DataGrid1" AllowPaging="false" runat="server" CssClass="allgrid GenTable1"  Font-Names='<%$ Resources:ValidationResources, TextBox1 %>' HorizontalAlign="Justify">
												<Columns>
                                                    <asp:TemplateColumn>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkPS" runat="server" Text='<%# Eval("prefixname") %>' ></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    
													<asp:BoundColumn Visible="False" DataField="prefixid" HeaderText="<%$ Resources:ValidationResources, GrPrefixId %>"></asp:BoundColumn>
													<asp:BoundColumn Visible="False" DataField="suffixname"  HeaderText="<%$ Resources:ValidationResources, GrSuffix %>"></asp:BoundColumn>
                                                    <asp:ButtonColumn Text="StartNo" DataTextField="startno"  HeaderText="<%$ Resources:ValidationResources, LStart %>" CommandName="Select"></asp:ButtonColumn>
													<asp:BoundColumn DataField="startno" SortExpression="startno" HeaderText="<%$ Resources:ValidationResources, LStart %>"></asp:BoundColumn>
													<asp:BoundColumn DataField="currentposition"  HeaderText="<%$ Resources:ValidationResources, GrCurrPosition %>"></asp:BoundColumn>
													<asp:BoundColumn DataField="Category" HeaderText="<%$ Resources:ValidationResources, LCat %>"></asp:BoundColumn>
												</Columns>
												</asp:datagrid>

                                     </div>--%>
                                 
                                    <div class="allgriddiv" style="max-height:300px" id="dvgrd" runat="server">                                  
                    <asp:HiddenField runat="server" ID="hdnGrdId" />

                                    <asp:GridView ID="grdPS" Width="100%" AllowPaging="false" runat="server"  AutoGenerateColumns="false" >
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton id="lnkSel" runat="server" Text="Select" OnClick="lnkSel_Click" style="color:blue"></asp:LinkButton>
                                                    <asp:HiddenField ID="hdPid" runat="server" Value='<%# Eval("prefixid") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="prefixname" HeaderText="Prefix" />
                                            <asp:BoundField DataField="suffixname" HeaderText="Suffix" />
                                            <asp:BoundField DataField="startno" HeaderText="Start No" />
                                        </Columns>
                                    </asp:GridView>
                                                                           
                                        </div>
                                 </div>
<INPUT id="hdTop" type="hidden" runat="server" style="width: 19px"/>
<INPUT id="Hidden2" style="WIDTH: 40px; HEIGHT: 16px" type="hidden" size="1" name="Hidden2"
							runat="server"/>




                             </ContentTemplate>
                                </asp:UpdatePanel>
    <INPUT id="hdUnableMsg" type="hidden" runat="server" style="width: 15px"/>
    
    <INPUT id="Hidden1" style="WIDTH: 13px; HEIGHT: 22px" type="hidden" name="Hidden1"
							runat="server"/>
                        <INPUT id="Hidden3" style="WIDTH: 10px; HEIGHT: 22px" type="hidden" size="1" name="Hidden3"
							runat="server"/><input id="Hidden6" runat="server" style="width: 16px" type="hidden" />
    <input id="hdcurrentP" runat="server" type="hidden" style="width: 32px; height: 16px;" />
                                    <INPUT id="Hidden4" style="WIDTH: 16px; HEIGHT: 22px"
					type="hidden" size="1" name="Hidden1" runat="server"/>
			<INPUT id="Hidden5" style="WIDTH: 16px; HEIGHT: 22px"
					type="hidden" size="1" name="Hidden2" runat="server"/>
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
                 //$('<thead></thead>').prependTo('#' + grdId).append($('#' + grdId + ' tr:first'));
                 //$('#' + grdId + ' tr:first td').contents().unwrap().wrap('<th></th>');
                 ThreeLevelSearch("[id$=grdPS]", "[id$=dvgrd]", 200);
             }
             catch (err) {
             }
         }


     </script>

    </asp:Content>
