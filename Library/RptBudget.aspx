<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptBudget.aspx.cs"  MasterPageFile="~/LibraryMain.master" Inherits="Library.RptBudget" %>

<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>

<asp:Content ID="brepHead" runat="server" ContentPlaceHolderID="head">
		<style type="text/css">.CellStyle { FONT-SIZE: 10px; COLOR: #000000; FONT-FAMILY: Verdana }
	.HeaderStyle { FONT-SIZE: 11px; COLOR: #000000; FONT-FAMILY: Verdana }
    #txtSearch{
            background-image: url(~/Images/search.png) ;
            padding-left: 18px;
            }
		</style>
		
         <script language="javascript" type="text/javascript">
             window.history.forward(1);
        </script>
       
        <script type="text/javascript">

            function pdfClick() {
                $("[id$='nreco']").click();
                setTimeout('delayt();', 6000);

            }

            function delayt() {
                ExportToPDF($('[id$=divshow]'), [], 'Budget Report', PDFPageType.Portrait, 'BudgetReport');
            }


        </script>
        
         <script src="FormScripts/ExportToPDF.js" type="text/javascript"></script>

        <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
            rel="stylesheet" type="text/css" />
       

</asp:Content>

<asp:Content ID="brepMain" runat="server" ContentPlaceHolderID="MainContent">
     <asp:UpdateProgress ID="UpPorg1" runat="server">
    <ProgressTemplate>
    <NN:Mak ID="FF1" runat="server" />
    </ProgressTemplate>
    </asp:UpdateProgress>

           <div class="container tableborderst" >   
        
			 <div style="width:100%;display:none" class="title">
                  <div style="width:89%;float:left" > &nbsp;
		<asp:label id="lblTitle" runat="server" style="display:none" >Budget Report</asp:label><br />
                      </div>  <div style="float:right;vertical-align:top"> 
                       <a id="lnkHelp" href="#" style="display:none"  onclick="ShowHelp('Help/Acquisitioning-BudgetReport.htm')"> <img alt="Help?" height="15" src="help.jpg"/></a>
              </div></div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <Triggers><asp:PostBackTrigger ControlID="cmdSearch" />
						
						</Triggers>
                                <ContentTemplate>
                            <div class="no-more-tables" style="width:100%">

                            <table id="Table3"  class=" no-more-tables GenTable1">
                                <tr>
                                    <td colspan="4">
                                        <asp:Label ID="msglabel" runat="server" CssClass="err" ></asp:Label></td>
                                </tr>
                                <tr>
                                    <td >
                                      
                                        <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="True" CssClass="opt" OnCheckedChanged="chkSelectAll_CheckedChanged"
                                           Text="<%$Resources:ValidationResources,CnkSelectA %>"/></td>
                                    <td>
                                     </td>
                                    <td >
                                    </td>
                                </tr>
                                
                              </table></div>
                                     <asp:TextBox ID="txtSearch" placeholder="Search" Ontextchanged="txtSearch_TextChanged" style="width:32%"  AutoPostBack="true"  runat="server"></asp:TextBox>
                                    <div  class="allgriddiv" style="max-height:329px">
                                       
                                         

                                         <asp:DataGrid ID="dgbudget" runat="server" CssClass="allgrid GenTable1" Width="100%" Font-Names='<%$ Resources:ValidationResources, TextBox1 %>'>
                                           
                                            <Columns>
                                                <asp:BoundColumn DataField="id" HeaderText="<%$ Resources:ValidationResources, GrId %>" Visible="False"></asp:BoundColumn>
                                                <asp:TemplateColumn >                                                    
                                                    <ItemTemplate>
                                                        &nbsp;
                                                        <asp:CheckBox ID="Chkselect" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="departmentname" HeaderText="<%$ Resources:ValidationResources, LDeptm %>"></asp:BoundColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </div>
                                    <table class="no-more-tables table-condensed GenTable1">
                                <tr>
                                    
                                    <td colspan="4" style="text-align:center" >
                                       
                                                   <%-- <input id="cmdSearch" runat="server" name="cmdSearch" 
                                                        type="button" value="<%$Resources:ValidationResources,bPrintR %>" class="btnstyle" />--%>
                          <asp:Button id="cmdSearch" runat="server" CssClass="btn btn-primary" Text="Print Report" OnClick="cmdSearch_Click"/>                  
                                                    <%--<input id="cmdReset" runat="server" name="cmdReset"
                                                        type="button" value="<%$Resources:ValidationResources,bReset %>" class="btnstyle" />--%>
<asp:Button id="cmdReset" runat="server" CssClass="btn btn-primary" Text="Reset" OnClick="cmdReset_Click"/>


                                             <input type="button" id="btnPrint" value="PRINT" onclick="pdfClick();" class="btnstyle"/>
<%--<asp:Button id="btnPrint" runat="server" Text="Print" CssClass="btn btn-primary" OnClick="btnPrint_Click"/>--%>
                                        <%--<input id="nreco" runat="server" class="btnstyle" name="ItextReport" type="button" 
                                            value="NRECO" style="display:none" />--%>
<asp:Button id="nreco" runat="server" CssClass="btn btn-primary" Text="NRECO" style="display:none;" OnClick="nreco_Click"/>
                                    </td>
                                   
                                </tr>
                            </table>
                              
                                        <div id="divshow" runat="server" style="display:none"> 
            <div style="width:100%;float:left" id="hedd" >
             <table class="GenTable1" style="width:100%">
                <tr>
                    <td style="font-size:14px;width:41%">
                        Gram : <asp:Label runat="server" ID="grm"></asp:Label>

                    </td>
                    <td style="font-size:14px; width:29%">
                        Fax: <asp:Label runat="server" ID="fx"></asp:Label>

                    </td>
                    <td style="font-size:14px; width:30%;text-align:right">
                        PhoneNo: <asp:Label runat="server" ID="phn"></asp:Label>

                    </td>

                </tr> 
 <tr>
     <td style="font-size:14px;">
         E-Mail :<asp:Label runat="server" ID="eml"></asp:Label> 

     </td>
     <td>&nbsp;</td>
     <td>&nbsp;</td> 

 </tr> 
 <tr>
     <td style="font-size:18px;" colspan="3">
         <div style="text-align:center">
             <asp:Label runat="server" ID="ins"></asp:Label> <br /> </div>

     </td>

 </tr> 
 <tr>
     <td style="font-size:15px;" colspan="3">
         <div  style="text-align:center">
             <asp:Label runat="server" ID="libranm"></asp:Label>

         </div>

     </td>

 </tr> 
 <tr>
     <td style="font-size:13px;" colspan="3">
     <div  style="text-align:right">
         <asp:Label runat="server" ID="addr"></asp:Label> &nbsp <asp:Label runat="server" ID="cit"></asp:Label> &nbsp <asp:Label runat="server" ID="pin"></asp:Label> &nbsp <asp:Label runat="server" ID="stat"></asp:Label>

     </div>

     </td>

 </tr> 
 <%--<tr>
     <td style="font-size:14px;" colspan="3">
         <div  style="text-align:center">Budget Report</div>

     </td>
        
 </tr>  --%>
 <tr>
     <td> 
         Budget Statement:<span style="color:green"> <asp:Label runat="server" ID="sess"></asp:Label> </span>

     </td>
     <td> 
         (As on) :<span style="color:green"> <asp:Label runat="server" ID="curdt"></asp:Label> </span>

       </td>
     <td>&nbsp</td> 
 </tr>
                 <tr>
                     <td colspan="3">
                         <hr />
                     </td>
                 </tr>
               </table>
           </div>
          <asp:GridView runat="server" ID="grdrep" CssClass="GenTable1" OnRowDataBound="grdrep_RowDataBound" AutoGenerateColumns="false" BorderStyle="None" Width="100%"  ShowFooter="true" EnableTheming="false" HeaderStyle-BackColor="#336699" HeaderStyle-ForeColor="White" FooterStyle-BackColor="#336699" FooterStyle-ForeColor="White" >
              <Columns>
                  <asp:TemplateField>
                      <HeaderTemplate>
                          <thead>
                       <tr style="page-break-inside:avoid;width:100%;border-bottom:1px solid black;border-left:1px solid black;border-right:1px solid black;border-top:1px solid black;font-family:Arial;background-color:#336699;color:white;padding:5px">
                          
    <th style="font-size:13px;font-weight:bold;height:20px;width:18%;border-right:1px solid black;border-top:1px solid black;padding:5px"  rowspan="2">Department</th> 
                           <th style="font-size:13px; font-weight:bold;height:20px;width:14%;border-right:1px solid black;border-top:1px solid black;padding:5px"  rowspan="2">Allocation</th>
                           <th style="font-size:13px;text-align:center;font-weight:bold;width:20%;border-right:1px solid black;border-top:1px solid black;padding:5px" colspan ="2"  >Indents In Hand</th>
                           <th style="font-size:13px;text-align:center;font-weight:bold;width:20%;border-right:1px solid black;border-top:1px solid black;padding:5px" colspan ="2">Committed Amount</th>
                           <th style="font-size:13px; font-weight:bold;height:20px;width:14%;border-right:1px solid black;border-top:1px solid black;padding:5px"  rowspan="2">Expenditure</th>
                           <th style="font-size:13px; font-weight:bold;height:20px;width:14%;border-right:1px solid black;border-top:1px solid black;padding:5px"  rowspan="2">Balance</th>

                       </tr> 
               
    <tr style="page-break-inside:avoid;border-bottom:1px solid black;border-left:1px solid black;border-right:1px solid black;border-top:1px solid black;font-family:Arial;background-color:#336699;color:white;padding:5px">
                 <th style="font-size:13px; font-weight:bold;height:20px;width:10%;border-right:1px solid black;border-left:1px solid black;padding:5px" >Approval </th>
                 <th style="font-size:13px;font-weight:bold;height:20px;width:10%;border-right:1px solid black;border-left:1px solid black;padding:5px">Non-Approval</th>
                <th style="font-size:13px;font-weight:bold;height:20px;width:10%;border-right:1px solid black;border-left:1px solid black;padding:5px" >Approval</th>  
                <th style="font-size:13px;font-weight:bold;height:20px;width:10%;border-right:1px solid black;border-left:1px solid black;padding:5px" >Non-Approval</th>
    </tr>
                            
                              </thead>
                          </HeaderTemplate>
                 
                      <ItemTemplate>
                          
                      <tr style="page-break-inside:avoid;border:1px solid black;font-family:Arial;"> 
                             <%-- <td>
                                  <table style="width:100%" border="1">
                                  <tr>--%>
                       <td style="font-size:13px; height:20px;width:18%;border-right:1px solid;margin-bottom:4px;margin-top:4px;padding:5px">
                          <%# Eval("deptname") %>
                      </td>
                         <td style="font-size:13px; height:20px;width:14%;border-right:1px solid black;margin-bottom:4px;margin-top:4px;padding:5px">
                              <%# Eval("allocatedamount") %>
                              </td>
                         <td style="font-size:13px; height:20px;width:10%;border-right:1px solid black;margin-bottom:4px;margin-top:4px;padding:5px">
                          <%# Eval("approvalcommitedamt") %>
                      </td>
                         <td style="font-size:13px; height:20px;width:10%;border-right:1px solid black;margin-bottom:4px;margin-top:4px;padding:5px">
                              <%# Eval("nonapprovalcommitedamt") %>
                              </td>
                               <td style="font-size:13px; height:20px;width:10%;border-right:1px solid black;margin-bottom:4px;margin-top:4px;padding:5px">
                          <%# Eval("approvalindentinhandamt") %>
                      </td>
                         <td style="font-size:13px; height:20px;width:10%;border-right:1px solid black; margin-bottom:4px;margin-top:4px;padding:5px">
                              <%# Eval("nonapprovalindentinhandamt") %>
                              </td>
                               <td style="font-size:13px; height:20px;width:14%;border-right:1px solid;margin-bottom:4px;margin-top:4px;padding:5px">
                          <%# Eval("expendedamount") %>
                      </td>
                         <td style="font-size:13px; height:20px;width:14%;margin-bottom:4px;margin-top:4px;padding:5px">
                              <%# Eval("balance") %>
                              </td>
                                <%--  </tr>    
                                  </table>
                              </td>--%>
                      
                          </tr>
                          </ItemTemplate>
                      <FooterTemplate>
                          <tr style="border:1px solid black ;font-family:Arial;background-color:#336699;color:white">
                    <td style="font-size:13px;width:18%;height:20px;font:bold;border-right:1px solid black;padding:5px"><b> Total  </b></td>
                 <td style="font-size:13px;width:14%;height:20px;border-right:1px solid black;padding:5px"> 
                   <b>  <asp:Label runat="server" ID="alloc" ></asp:Label></b>
                    <%-- <%# Eval("AllocatedAmount") %>--%>
                    
                   </td> <td colspan='2'  style="font-size:13px;width:20%;height:20px;border-right:1px solid black;padding:5px"> 
                      <b>  <asp:Label runat="server" ID="IndentIn" ></asp:Label></b>
                      <%--   <%# Eval("IndentInHand") %>
                  --%>
                 </td> <td colspan='2'  style="font-size:13px;width:20%;border-right:1px solid black;height:20px;padding:5px"> 
     <b> <asp:Label runat="server" ID="Commit" ></asp:Label></b>
                  <%--   <%# Eval("CommittedAmmount") %>--%>
               
                </td> 
                   <td  style="font-size:13px;width:14%;border-right:1px solid black;height:20px;padding:5px"> 
                    <b>    <asp:Label runat="server" ID="Expen"></asp:Label></b>
                     <%--  <%# Eval("Expenditure") %>
                --%>
                 </td> <td style="font-size:13px;width:14%;height:20px;padding:5px"> 
                  <b>   <asp:Label runat="server" ID="ball" ></asp:Label></b>
                     <%-- <%# Eval("Balance") %>--%>
                  
                    </td> 
 			</tr> 
                   

                      </FooterTemplate>
                      </asp:TemplateField>
                  </Columns>
              </asp:GridView>
               <table align = 'right' ><tr><td style=font-size:14px;>All Amount In Rupees. </td></tr></table>
           </div>
                              
                            	         </ContentTemplate>
                            </asp:UpdatePanel>
                             </div>
						<input id="Hidden1" runat="server" name="Hidden1" size="1" style="width: 8px; height: 22px"
                                            type="hidden" />
</asp:Content>


