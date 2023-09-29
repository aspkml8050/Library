<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/LibraryMain.master"  CodeBehind="generateDynmICard.aspx.cs" Inherits="Library.generateDynmICard" %>
<asp:Content ID="genICHead" runat="server" ContentPlaceHolderID="head">
  <%--  <script src="JavaScript/messageLibrary.js"></script>--%>
    <script src="FormScripts/generateDynmICard.js"></script>
<%--     <link href="cssDesign/libresponsive.css" rel="stylesheet" type="text/css" />--%>


</asp:Content>

<asp:Content ID="genICMain" runat="server" ContentPlaceHolderID="MainContent">
       <link href="cssDesign/multiselect.css" rel="stylesheet" type="text/css" >
    <script src="FormScripts/multiselect.js"></script>
        <link href="cssDesign/libresponsive.css" rel="stylesheet"
type="text/css" /> 
    <div class="container tableborderst" style="width:60%;margin-top:20px; padding:0;">   
         <div class="no-more-tables" style="width:100%">
              <div style="width:100%;display:none" class="title">
            <asp:Label ID="lblTitle" runat="server" style="display:none" EnableTheming="False" Width="100%"></asp:Label><br />
            </div>
                  <table class="table-condensed GenTable1" style=" width:100%" >
                <tr>
                    <td >Member Id</td>
                    <td >
                        <input id="txtMemberId" type="text" />
                    </td>
                    <td ></td>
                    <td ></td>
                </tr>
                <tr>
                    <td >Session :</td>
                    <td >
                        <select id="selSession" ></select>
                    </td>
                    <td >Member Group</td>
                    <td >
                        <select id="selMemberGroup" ></select>
                    </td>
                </tr>
                <tr>
                    <td >Department</td>
                    <td >
                        <select id="selDept" ></select>
                    </td>
                    <td >Course/Desg</td>
                    <td >
                        <select id="selCourseDesg" ></select>
                    </td>
                </tr>
                <tr>
                    <td >ID Card Format</td>
                    <td>
                        <select id="selCardFormat" ></select>
                    </td>
                    <td>Page Size</td>
                    <td >
                        <select id="selPageSize" ></select>
                    </td>                    
                </tr>
               
                <tr>
                    <td style=" text-align: center;" colspan="4">
                        <input id="btnSearch" onclick="btnSearch_Click();" class="btn btn-primary" type="button" value="Search"  />
                        <input id="btnGenerate" onclick="btnGenerate_Click();" class="btn btn-primary" type="button" value="Generate"  />
                        <input id="btnReset" onclick="btnReset_Click();" class="btn btn-primary" type="button" value="Reset"  />
                    </td>
                </tr>
            </table>
        </div>
            </div>
         <div class="container "  style="width:80%;margin-top:20px;border:none">   
             <div style="width:100%;overflow:auto; ">
        <div id="grdDetails"  style="width:100%;max-height:250px;  text-align:center"></div>
                 </div>
      </div>

       <script>
           $(function () {
               //ForDataTable();
               SetListBox();
           });
           var prm = Sys.WebForms.PageRequestManager.getInstance();

           if (prm != null) {
               prm.add_endRequest(function (sender, e) {

                   if (sender._postBackSettings.panelsToUpdate != null) {

                       //  ForDataTable();
                       SetListBox();
                   }
               });
           };
           function ForDataTable() {
               try {
                   var grdId = $("[id$=hdnGrdId]").val();
                   //alert(grdId);
                   $('<thead></thead>').prependTo('#' + grdId).append($('#' + grdId + ' tr:first'));
                   ThreeLevelSearch($('#' + grdId), "[id$=dvgrd]", 200);
               }
               catch (err) {
               }
           }
           function SetListBox() {

               $('[id*=cmbdept]').multiselect({
                   enableCaseInsensitiveFiltering: true,
                   buttonWidth: '80%',
                   includeSelectAllOption: true,
                   maxHeight: 200,
                   width: 315,
                   enableFiltering: true,
                   filterPlaceholder: 'Search'

               });

           }
       </script>

</asp:Content>




