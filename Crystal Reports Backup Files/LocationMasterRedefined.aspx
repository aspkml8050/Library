<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/LibraryMain.Master" CodeBehind="LocationMasterRedefined.aspx.cs" Inherits="Library.LocationMasterRedefined" %>
<%@ Register TagName="Mak" TagPrefix="NN" Src="~/ProgressControl.ascx" %>

<asp:Content ID="locaMastHead" ClientIDMode="Static" runat="server" ContentPlaceHolderID="head">
        <style type="text/css">
        
        fieldset
        {
           height:235px!important;
          /* background-color:#ffffff!important*/
        }
        legend 
        {
            background:gray;
            color:white!important;
            font-size:small!important
        }
        .ButtonCss {
            height: 25px;
            width: 100px;
            border: 1px solid black;
            background-color: white;
            -moz-box-shadow: 3px 3px 5px 6px #a1a1a1;
            -webkit-box-shadow: 3px 3px 5px 6px #a1a1a1;
            box-shadow: 0px 0px 2px 0px #a1a1a1;
            cursor: pointer;
        }

        .ButtonCss:hover {
            color: white;
            background-color: #666666;
            border: 1px solid white;
        }

        .ForTrMargin {
            height: 40px;
        }

        .ButtonCss2 {
            height: 20px;
            width: 100px;
            border: 1px solid black;
            background-color: white;
            -moz-box-shadow: 3px 3px 5px 6px #a1a1a1;
            -webkit-box-shadow: 3px 3px 5px 6px #a1a1a1;
            box-shadow: 0px 0px 2px 0px #a1a1a1;
        }

            .ButtonCss2:hover {
                color: white;
                background-color: #666666;
                border: 1px solid white;
            }





        .ForTable {
            font-family: "Lucida Sans Unicode", "Lucida Grande", Sans-Serif;
            font-size: 12px;
            width: 100%;
            text-align: left;
            border-collapse: collapse;
        }

            .ForTable th {
                padding: 6px 2px;
                font-weight: normal;
                font-size: 14px;
                border-bottom: 2px solid #6678b1;
                
                color: #039;
            }

            .ForTable td {
                padding: 12px 2px 0px 2px;
                
                color: #669;
            }

            .ForTable tr {
            }

                .ForTable tr:hover {
                    background-color: #eeeeee;
                }


        .ForTextBox {
            border: 1px solid gray;
            width: 80%;
        }

            .ForTextBox:focus {
                border: 1px solid black;
                background-color: #f0f0f0;
                outline: none;
            }

            .ForTextBox:hover {
                border: 1px solid black;
            }
    </style>
        <style type="text/css">
        .GetSuggestion {
            position: absolute;
            background-color: white;
            width: 350px;
            z-index: 100000;
            overflow: auto;
            border:1px solid grey;
        }

        .suggestion {
            width: 100%;
            color: black;
        }

            .suggestion:hover {
                
                background-color: #e2e2e2;
            }
            .ajax__tab_xp .ajax__tab_tab {
                padding: 0px !important
            }
            @media only screen and (max-width:100%) {
                .objl {
                    background-color: white;
                    height: 227px;
                    overflow: auto
                }
            }
            @media only screen and (max-width: 600px) {
  .objl{
      width:100%;
    background-color: white;
    height:227px;
    overflow:auto
  }
}
    </style>
     <script type="text/javascript">
     //On Document Ready
         $(document).ready(function (e) {

         GetLocationObjectDataForTab2();
         GetLocationObjectDataForTab3();
         GetMappedLocationForTab3(1);
         getTPageCount();
     });

         function  testasmx() {
             $.ajax({
                 type: "POST",
                 url: "LocationMasterRedefined1.asmx/HelloWorld",
                 data: '{}',
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 success: function (data) {
                     if (data.d != "") {
                         return data.d;
                     }
                 },
                 failure: function (response) {
                     showMessage('1', response.d);
                     return "failed";
                 },
                 error: function (response) {
                     return "failed";
                 }
             });
         }

     function selPageNo_Change() {
         GetMappedLocationForTab3(document.getElementById("selPageNo").value);
     }

     function getTPageCount() {
         $.ajax({
             type: "POST",
             url: "LocationMasterRedefined1.asmx/getTPageCount",
             data: '{}',
             contentType: "application/json; charset=utf-8",
             dataType: "json",
             success: function (data) {
                 if (data.d != "") {
                     var selPageNo = document.getElementById("selPageNo");
                     for (c = 0; c < selPageNo.length; i++) {
                         selPageNo.remove(c);
                     }
                     for (i = 1; i <= data.d; i++) {
                         var op = document.createElement("option");
                         op.text = i;
                         op.value = i;
                         selPageNo.add(op);
                     }
                 }
                 else {
                     var selPageNo = document.getElementById("selPageNo");
                     var op = document.createElement("option");
                     op.text = 1;
                     op.value = 1;
                     selPageNo.add(op);
                 }
             },
             failure: function (response) {
                 showMessage('1', response.d);
             },
             error: function (response) {
             }
         });
     }

     // For TabPanel1
     function btnSaveLocationObject_Click() {

         var ObjectName = document.getElementById("txtLocationObject").value;
         var Abbr = document.getElementById("txtLocationObjectShortname").value;
         var Operation = document.getElementById("btnSaveLocationObject").value;
         var Flag = '';
         var IdUpdate = document.getElementById("HidLocationObjectId").value;
         var Order = document.getElementById("txtOrderNo").value;
         if (ObjectName == '') {

             Flag = 'Called';
         }
         else {

             $.ajax({
                 type: "POST",
                 url: "LocationMasterRedefined1.asmx/SaveLocationObject",
                 data: '{"ObjectName":' + JSON.stringify(ObjectName) + ',"Abbr":' + JSON.stringify(Abbr) + ',"Operation":' + JSON.stringify(Operation) + ',"IdUpdate":' + JSON.stringify(IdUpdate) + ',"Order":' + JSON.stringify(Order) + '}',
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 success: function (response) {

                     if (response.d == "Save") {
                         alert("Record Saved Successfully ......!");
                         //showMessage('1', 'Record Saved Successfully ......!');
                         btnResetLocationObject_Click();
                         GetLocationObjectDataForTab3();
                     }
                     if (response.d == "Update") { alert("Record Updated Successfully ......!"); btnResetLocationObject_Click(); GetLocationObjectDataForTab3(); }
                     if (response.d == "Existed") { alert("Record already present .....!"); }

                 }
                 ,
                 failure: function (response) {

                     showMessage('1', response.d);
                 },
                 error:

                     function (xhr, ajaxOptions, thrownError) {
                         alert(xhr.status);
                         alert(xhr.responseText);
                         alert(thrownError);


                     }
             });
         }
         if (Flag == "Called") { alert("Location Object is required .....!") }
     }

     function btnDeleteLocationObject_Click() {
         var Id = document.getElementById("HidLocationObjectId").value;

         $.ajax({
             type: "Post",
             url: "LocationMasterRedefined1.asmx/DeleteLocationObject",
             data: '{"Id":' + JSON.stringify(Id) + '}',
             contentType: "application/json; charset=utf-8",
             dataType: "json",
             success: function (response) {
                 if (response.d == "Success") {
                     alert("Record Deleted Successfully .....!");
                     //showMessage('1', 'Record Deleted Successfully .....!');
                     btnResetLocationObject_Click(); GetLocationObjectDataForTab3();
                 }
                 if (response.d == 'Existed') {
                     alert("Object is in use , can not delete it.....!")
                     //showMessage('1', 'Object is in use , can not delete it.....!');
                 }
             },
             failure: function (response) { showMessage('1', response.d); },
             error: function (xhr, ajaxOptions, thrownError) {
                 alert(xhr.status);
                 alert(xhr.responseText);
                 alert(thrownError);
             }
         });
     }

     function btnResetLocationObject_Click() {
         document.getElementById("txtLocationObject").value = '';
         document.getElementById("txtLocationObjectShortname").value = '';
         document.getElementById("txtOrderNo").value = '';
         document.getElementById("btnSaveLocationObject").value = 'Save';
         document.getElementById("HidLocationObjectId").value = -1;
         document.getElementById("btnDeleteLocationObject").disabled = true;
         GetDataForLocationObject();
         GetLocationObjectDataForTab2();
         document.getElementById("selPageNo_Change").selectedIndex = 0;
     }

     function GetDataForLocationObject() {
         var ChkBx = document.getElementById("ChkForLocationObject")
         if (ChkBx.checked == true) {
             $.ajax({
                 type: "POST",
                 url: "LocationMasterRedefined1.asmx/GetLocationObjectData",
                 data: '{}',
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 success: function (data) {
                     var data1 = data.d;
                     if (data1 == '') {
                         alert("No Record Found ......!")
                         //showMessage('1', 'No Record Found ......!');
                         document.getElementById("ShowDataLocationObject").style.display = "none";
                     }
                     else {
                         document.getElementById("ShowDataLocationObject").innerHTML = data1;
                         document.getElementById("ShowDataLocationObject").style.display = "block";
                     }
                 },
                 failure: function (response) { showMessage('1', response.d); },
                 error: function (response) {

                 }
             });
         }
         else {
             document.getElementById("ShowDataLocationObject").style.display = "none";
         }
     }

     function EditRecordLocationObject(Id) {

         $.ajax({
             type: "Post",
             url: "LocationMasterRedefined1.asmx/EditLocationObjectData",
             data: '{"Id":' + JSON.stringify(Id) + '}',
             contentType: "application/json; charset=utf-8",
             dataType: "json",
             success: function (response) {
                 var data = response.d;
                 var dataa = new Array();
                 dataa = data.split(",")
                 document.getElementById("txtLocationObject").value = dataa[0];
                 document.getElementById("txtLocationObjectShortname").value = dataa[1];
                 document.getElementById("txtOrderNo").value = dataa[2];
                 document.getElementById("btnSaveLocationObject").value = 'Update';
                 document.getElementById("HidLocationObjectId").value = Id;
                 document.getElementById("btnDeleteLocationObject").disabled = false;
             },
             failure: function (response) { showMessage('1', response.d); },
             error: function (xhr, ajaxOptions, thrownError) {
                 alert(xhr.status);
                 alert(xhr.responseText);
                 alert(thrownError);
             }
         });
     }

     function CopytxtLocationObject_txt() {
         var txt1 = document.getElementById("txtLocationObject").value;
         var txt2 = document.getElementById("txtLocationObjectShortname").value;
         if (txt2 == '') {
             document.getElementById("txtLocationObjectShortname").value = txt1;
         }
     }
     </script>
      <script type="text/javascript">
          // Script for Tab 2
          //$(document).ready = GetLocationObjectDataForTab2();


          function GetLocationObjectDataForTab2() {
              $.ajax({
                  type: "POST",
                  url: "LocationMasterRedefined1.asmx/GetLocationObjectDataForTab2",
                  data: '{}',
                  contentType: "application/json; charset=utf-8",
                  dataType: "json",

                  success: function (data) {

                      var returnData = data.d;
                      document.getElementById("divSelectLocationObject").innerHTML = returnData;
                  }
                  ,
                  failure: function (response) {

                      alert("Failure : " + response.d);
                  },
                  error: function (response) {

                      alert("Error : " + response.d);
                  }
              });
          }

          function btnSaveAddItem_Click() {
              var LocationObject = document.getElementById("dropLocationObject").value;
              var Item = document.getElementById("txtAddItem").value;

              var Operation = document.getElementById("btnSaveAddItem").value;
              var ItemId = document.getElementById("itemID").value;
              if (LocationObject == '-1') {
                  alert("Please select Location Object ......!");

                  return;
              }
              if (Item == '') {

                  alert("Add Item is a required field ......!");

                  return;
              }


              $.ajax({
                  type: "POST",
                  url: "LocationMasterRedefined1.asmx/btnSaveAddItem",
                  data: '{"LocationObject":' + JSON.stringify(LocationObject) + ',"Item":' + JSON.stringify(Item) + ',"Operation":' + JSON.stringify(Operation) + ',"ItemId":' + JSON.stringify(ItemId) + '}',
                  contentType: "application/json; charset=utf-8",
                  dataType: "json",

                  success: function (data) {

                      var returnData = data.d;
                      if (returnData == 'Save') { alert("Record Saved Successfully ........!"); GetDataForLoacationItem(); ResetItem(); GetLocationObjectDataForTab3(); }
                      if (returnData == 'Update') { alert("Record Updated Successfully ........!"); GetDataForLoacationItem(); ResetItem(); GetLocationObjectDataForTab3(); }
                      if (returnData == 'Existed') { alert("Record already existed ........!"); }
                  }
                  ,
                  failure: function (response) {

                      alert("Failure : " + response.d);
                  },
                  error: function (response) {

                      alert("Error : " + response.d);
                  }
              });
          }

          function btnSaveAddMultiItem_Click() {
              var LocationObject = document.getElementById("dropLocationObject").value;
              var AliasName = document.getElementById("txtAlias").value;
              var Fromm = document.getElementById("txtFromItem").value;
              var Too = document.getElementById("txtToItem").value;
              if (LocationObject == '-1') {
                  alert("Please select Location Object ......!");

                  return;
              }

              if (Fromm == '') {
                  alert("From Item is a required field ......!");

                  return;
              }
              if (Too == '') {
                  alert("To Item is a required field ......!");

                  return;
              }
              var re = /^-?\d\d*$/;
              if (re.test(Fromm) == false) {
                  alert("From is a integer(numeric) field ......!");

                  return;
              }

              if (re.test(Too) == false) {
                  alert("To is a integer(numeric) field ......!");

                  return;
              }

              if (Fromm >= Too) {
                  alert("From  can not be greater than or equales to To  ......!");

                  return;
              }

              $.ajax({
                  type: "POST",
                  url: "LocationMasterRedefined1.asmx/btnSaveAddMultiItem",
                  data: '{"LocationObject":' + JSON.stringify(LocationObject) + ',"AliasName":' + JSON.stringify(AliasName) + ',"Fromm":' + JSON.stringify(Fromm) + ',"Too":' + JSON.stringify(Too) + '}',
                  contentType: "application/json; charset=utf-8",
                  dataType: "json",

                  success: function (data) {

                      var returnData = data.d;
                      if (returnData == 'Save') { alert("Record Saved Successfully ........!"); GetDataForLoacationItem(); ResetItem(); GetLocationObjectDataForTab3(); }

                      if (returnData == 'Existed') { alert("Record already existed ........!"); }

                  }
                  ,
                  failure: function (response) {

                      alert("Failure : " + response.d);
                  },
                  error: function (response) {

                      alert("Error : " + response.d);
                  }
              });

          }

          function GetDataForLoacationItem() {
              var CheckBoxForData = document.getElementById("ChkShowLocationItemData");
              if (CheckBoxForData.checked) {
                  $.ajax({
                      type: "POST",
                      url: "LocationMasterRedefined1.asmx/GetDataForLoacationItem",
                      data: '{}',
                      contentType: "application/json; charset=utf-8",
                      dataType: "json",

                      success: function (data) {

                          var returnData = data.d;
                          if (returnData == '') {
                              //showMessage('1', 'No Record Found ........!');
                              alert("No Record Found ........!");
                              document.getElementById("DivShowItemData").style.display = "none";

                          }
                          else {
                              document.getElementById("DivShowItemData").innerHTML = returnData;
                              document.getElementById("DivShowItemData").style.display = "block";

                          }


                      }
                      ,
                      failure: function (response) {

                          alert("Failure : " + response.d);
                      },
                      error: function (response) {

                          alert("Error : " + response.d);
                      }
                  });
              }
              else {

                  document.getElementById("DivShowItemData").style.display = "none";

              }

          }

          function ChkShowLocationItemData_Change() { GetDataForLoacationItem(); }

          function DeleteRecordLocationObjectItem(Id) {
              $.ajax({
                  type: "POST",
                  url: "LocationMasterRedefined1.asmx/DeleteRecordLocationObjectItem",
                  data: '{"Id":' + Id + '}',
                  contentType: "application/json; charset=utf-8",
                  dataType: "json",

                  success: function (data) {

                      var returnData = data.d;
                      if (returnData == 'Delete') {
                          alert("Record Deleted Successfully .....!");
                          //showMessage('1', 'Record Deleted Successfully .....!');
                          GetDataForLoacationItem();
                          ResetItem(); btnMapReset_Click();
                      }
                      if (returnData == 'Existed') {
                          //showMessage('1', 'Object item is in use, can not be deleted.....!');
                          alert("Object item is in use, can not be deleted.....!");
                      }


                  }
                  ,
                  failure: function (response) {

                      alert("Failure : " + response.d);
                  },
                  error: function (response) {

                      alert("Error : " + response.d);
                  }
              });

          }

          function EditRecordLocationObjectItem(Id) {
              $.ajax({
                  type: "POST",
                  url: "LocationMasterRedefined1.asmx/EditRecordLocationObjectItem",
                  data: '{"Id":' + Id + '}',
                  contentType: "application/json; charset=utf-8",
                  dataType: "json",

                  success: function (data) {

                      var returnData = data.d;
                      var dataa = new Array();
                      dataa = returnData.split(",")

                      document.getElementById("dropLocationObject").value = dataa[0];
                      document.getElementById("txtAddItem").value = dataa[1];



                      document.getElementById("btnSaveAddItem").value = 'Update';
                      document.getElementById("itemID").value = Id;


                  }
                  ,
                  failure: function (response) {

                      alert("Failure : " + response.d);
                  },
                  error: function (response) {

                      alert("Error : " + response.d);
                  }
              });
          }

          function ResetItem() {

              document.getElementById("txtAlias").value = '';
              document.getElementById("txtFromItem").value = '';
              document.getElementById("txtToItem").value = '';
              document.getElementById("txtAddItem").value = '';
              document.getElementById("btnSaveAddItem").value = 'Save';
              document.getElementById("itemID").value = '-1';
              document.getElementById("dropLocationObject").value = '-1';
          }
      </script>
  <script type="text/javascript">
      // For TabPanel3 

      //$(document).ready = GetLocationObjectDataForTab3();

      function GetLocationObjectDataForTab3() {
          $.ajax({
              type: "POST",
              url: "LocationMasterRedefined1.asmx/GetLocationObjectDataForTab3",
              data: '{}',
              contentType: "application/json; charset=utf-8",
              dataType: "json",

              success: function (data) {

                  var returnData = data.d;
                  if (returnData == '') {

                  }
                  else {

                      document.getElementById("DivShowingObjects").innerHTML = returnData;
                  }

              }
              ,
              failure: function (response) {

                  alert("Failure : " + response.d);
              },
              error: function (response) {

                  alert("Error : " + response.d);
              }
          });
      }

      function ChkSelectAllForTab3(Id) {
          var chkbxx = document.getElementById("chkbxMain" + Id + "")

          $.ajax({
              type: "POST",
              url: "LocationMasterRedefined1.asmx/SelectAllFunction",
              data: '{"Id":' + Id + '}',
              contentType: "application/json; charset=utf-8",
              dataType: "json",

              success: function (data) {

                  var returnData = data.d;
                  var dataa = new Array();
                  dataa = returnData.split(",");

                  for (var i = 0; i <= dataa.length - 1; i++) {
                      if (chkbxx.checked) {

                          var Chk = document.getElementById("chkbx" + dataa[i] + "");

                          Chk.checked = true;
                      }
                      else {
                          document.getElementById("chkbx" + dataa[i] + "").checked = false;
                      }
                  }

              }
              ,
              failure: function (response) {

                  alert("Failure : " + response.d);
              },
              error: function (response) {

                  alert("Error : " + response.d);
              }
          });

      }

      function btnMap_Click() {
          document.getElementById("DivMappedItems").innerHTML = "<center><img src='images/ajax_loader.gif' />Processing...</center>"
          $.ajax({
              type: "POST",
              url: "LocationMasterRedefined1.asmx/MapRecords1",
              data: '{}',
              contentType: "application/json; charset=utf-8",
              dataType: "json",

              success: function (data) {

                  var returnData = data.d;

                  var TableId = new Array();
                  TableId = returnData.split(",");


                  $.ajax({
                      type: "POST",
                      url: "LocationMasterRedefined1.asmx/MapRecords2",
                      data: '{"LocationObject":' + JSON.stringify(returnData) + '}',
                      contentType: "application/json; charset=utf-8",
                      dataType: "json",
                      success: function (data) {
                          var returnData1 = data.d;
                          var ChkbxId = new Array();
                          ChkbxId = returnData1.split(",");
                          ArrayForMap.splice(0, ArrayForMap.length);

                          for (var j = 0; j <= ChkbxId.length - 1; j++) {
                              var Ckbx = document.getElementById("chkbx" + ChkbxId[j] + "");
                              if (Ckbx.checked) {
                                  ArrayForMap.push(ChkbxId[j]);
                              }
                          }
                          GetGEt(ArrayForMap);
                      },
                      failure: function (response) {

                          alert("Failure : " + response.d);
                      },
                      error: function (response) {

                          alert("Error : " + response.d);
                      }
                  });


                  // alert("Last");
                  //GetGEt(ArrayForMap);


              }
              ,
              failure: function (response) {

                  alert("Failure : " + response.d);
              },
              error: function (response) {

                  alert("Error : " + response.d);
              }
          });



      }

      function GetGEt(ArrayForMap) {
          $.ajax({
              type: "POST",
              url: "LocationMasterRedefined1.asmx/MapRecord3",
              data: '{"MapArray":' + arrayToString(ArrayForMap) + '}',
              contentType: "application/json; charset=utf-8",
              dataType: "json",

              success: function (data) {
                  var returnData1 = data.d;
                  if (returnData1 == 'Success') {
                      alert("Record Mapped Successfully .....!");
                      //showMessage('1', 'Record Mapped Successfully .....!')
                      btnMapReset_Click();
                  }
                  if (returnData1 == 'Duplicate') {
                      GetMappedLocationForTab3(1);
                      alert("Location is already defined.....!");
                      //showMessage('1', 'Location is already defined.....!')
                  }

              }
              ,
              failure: function (response) {

                  alert("Failure : " + response.d);
              },
              error: function (response) {

                  alert("Error : " + response.d);
              }
          });
      }

      //$(document).ready = GetMappedLocationForTab3();

      function GetMappedLocationForTab3(pageNo) {
          document.getElementById("DivMappedItems").innerHTML = "<center><img src='images/ajax_loader.gif' />Processing...</center>"
          $.ajax({
              type: "POST",
              url: "LocationMasterRedefined1.asmx/GetMappedRecord",
              data: '{"pageNo":"' + pageNo + '"}',
              contentType: "application/json; charset=utf-8",
              dataType: "json",
              success: function (data) {
                  var returnData1 = data.d;
                  document.getElementById("DivMappedItems").innerHTML = returnData1;
              },
              failure: function (response) {
                  alert("Failure : " + response.d);
              },
              error: function (response) {
                  alert("Error : " + response.d);
              }
          });
      }

      function ChkSelectAllForLocation() {
          var chk = document.getElementsByName("chkLoc");
          for (i = 0; i < chk.length; i++) {
              chk[i].checked = chkbxAllLocation.checked;
          }
      }

      function ChkSelectForLocation(Id) {
          //var chkk = document.getElementById("chkbxLocation"+ Id +"")
          //if (chkk.checked) {
          //    document.getElementById("btnUnMap").disabled = false;
          //}
          $.ajax({
              type: "POST",
              url: "LocationMasterRedefined1.asmx/CheckAllLocations",
              data: '{"KeywORD":""}',
              contentType: "application/json; charset=utf-8",
              dataType: "json",

              success: function (data) {
                  var Chj = 0;
                  var returnData1 = data.d;
                  if (returnData1 != '') {
                      var Rew = new Array();
                      Rew = returnData1.split(",")
                      for (var i = 0; i <= Rew.length - 1; i++) {
                          var chkbx = document.getElementById("chkbxLocation" + Rew[i] + "");
                          if (chkbx.checked) {
                              Chj = 1;
                          }
                          else {

                          }
                      }

                      if (Chj == 1) {
                          //document.getElementById("btnUnMap").disabled = false;
                      }
                      else {
                          //document.getElementById("btnUnMap").disabled = true;
                      }
                  }
              }
              ,
              failure: function (response) {

                  alert("Failure : " + response.d);
              },
              error: function (response) {

                  alert("Error : " + response.d);
              }
          });
      }

      function btnUnMap_Click() {
          var strList = '';
          var chkbx = document.getElementsByName("chkLoc");
          for (i = 0; i < chkbx.length; i++) {
              if (chkbx[i].checked == true) {
                  if (strList == '')
                      strList = chkbx[i].value;
                  else
                      strList = strList + ',' + chkbx[i].value;
              }
          }
          if (chkbx.length == 0) {
              alert("Select the Locations first to Un-Map");
              return false;
          }
          $.ajax({
              type: "POST",
              url: "LocationMasterRedefined1.asmx/UnMapped_Click",
              data: '{"strList":"' + strList + '"}',
              contentType: "application/json; charset=utf-8",
              dataType: "json",
              success: function (data) {
                  var returnData1 = data.d;
                  if (returnData1 == 'Success') {
                      alert("Record Unmapped Successfully .....!");
                      btnMapReset_Click();
                  }
                  else {
                      alert("Unable to Unmap the location(s)");
                  }
              },
              failure: function (response) {
                  alert("Failure : " + response.d);
              },
              error: function (response) {
                  alert("Error : " + response.d);
              }
          });
      }

      function btnMapReset_Click() {
          GetLocationObjectDataForTab3();

          GetMappedLocationForTab3(1);

          getTPageCount();

          //document.getElementById("btnUnMap").disabled = true;
          document.getElementById("locsearchtxtbx").value = '';
          document.getElementById("div_btn1").style.display = 'block';
          document.getElementById("div_btn2").style.display = 'none';

      }

      function AddAliasName(Id) {

          var data = document.getElementById("txtAliasName" + Id + "").value;

          $.ajax({
              type: "POST",
              url: "LocationMasterRedefined1.asmx/AddAliasName",
              data: '{"data":' + JSON.stringify(data) + ',"id":' + JSON.stringify(Id) + '}',
              contentType: "application/json; charset=utf-8",
              dataType: "json",

              success: function (data) {

                  var returnData1 = data.d;
                  if (returnData1 == 'Success') {
                      alert("Alias Name saved successfully ....!");
                      //showMessage('1', 'Alias Name saved successfully ....!')
                  }
              }
              ,
              failure: function (response) {

                  alert("Failure : " + response.d);
              },
              error: function (response) {

                  alert("Error : " + response.d);
              }
          });
      }

      function GetSearchedRecords() {
          var PrefixText = document.getElementById("locsearchtxtbx").value;
          $.ajax({
              type: "POST",
              url: "LocationMasterRedefined1.asmx/GetSearchedRecords",
              data: '{"PrefixText":' + JSON.stringify(PrefixText) + '}',
              contentType: "application/json; charset=utf-8",
              dataType: "json",

              success: function (data) {

                  var returnData = data.d;
                  document.getElementById("DivMappedItems").innerHTML = returnData;
              }
              ,
              failure: function (response) {
                  showMessage('1', response.d);

              },
              error:

                  function (xhr, ajaxOptions, thrownError) {
                      alert(xhr.status);
                      alert(xhr.responseText);
                      alert(thrownError);


                  }
          });
      }
  </script>
       <script type="text/javascript">
           var ArrayForMap = new Array();
       </script>
   <script type="text/javascript">
       function arrayToString(ary) {

           var str = '';
           for (var i = 0; i < ary.length; i++) {
               if (str == '') {
                   str = '"' + ary[i] + '"';
               }
               else {
                   str = str + ',"' + ary[i] + '"';
               }
           }
           var fStr = "[" + str + "]";
           return fStr;
       }
   </script>
          <style type="text/css">
.CastCat
{
	width:100%;
	/*margin-top:10px;*/
    display:block
}
.leftCastCat
{
 width:52%;
 float:left;
  font-size:15px;
   display:block
}
.rightCastCat
{
	width:48%;
 float:right;
  font-size:15px;
  display:block
 
}
.ForTable
{
   height:178px;
   
}
@media screen and (max-width:90%)
{
	.CastCat
	{
		width:100%;
       
	}
	.leftCastCat
	{
		width:52%;
         font-size:15px;

	}
	.rightCastCat
	{
		width:48%;
         font-size:15px;
	}

}
@media screen and (max-width:720px)
{
	
	.leftCastCat, table tr td
	{
		width:100%!important;
        margin-top:10px!important;
        
	}
	.rightCastCat
	{
		width:100%;
        
	}

}

  @media screen and (max-width:1126px) 
      {
      .leftCastCat
	        {
		       font-size:12px;
	        }
	        .rightCastCat
	        {
		        font-size:12px;
	        }
        }
</style>
      <script type="text/javascript">
          //For Location Suggestion
          function GetSuggestion() {

              var PrefixText = document.getElementById("locsearchtxtbx").value;

              $.ajax({
                  type: "POST",
                  url: "LocationMasterRedefined1.asmx/GetSuggestion",
                  data: '{"PrefixText":' + JSON.stringify(PrefixText) + '}',
                  contentType: "application/json; charset=utf-8",
                  dataType: "json",

                  success: function (data) {

                      var returnData = data.d;
                      document.getElementById("divSuggestion").innerHTML = returnData;
                  }
                  ,
                  failure: function (response) {
                      showMessage('1', response.d);

                  },
                  error:

                      function (xhr, ajaxOptions, thrownError) {
                          alert(xhr.status);
                          alert(xhr.responseText);
                          alert(thrownError);


                      }
              });
          }

          function SelectRecord(Path) {

              document.getElementById("locsearchtxtbx").value = Path;
          }

          function ShowDiv() {
              document.getElementById("divSuggestion").style.height = "200px";
              document.getElementById("divSuggestion").style.display = "block";
          }

          function HideDiv2() {

              document.getElementById("divSuggestion").style.display = "none";

          }
          function HideDiv() {

              setTimeout("HideDiv2();", 150);

          }

          var M_LocID_for_Update = '-1';

          function EditMappedLocation(map_loc_id) {
              GetLocationObjectDataForTab3();
              setTimeout("EditMappedLocation1(" + map_loc_id + ")", 100);
          }

          function EditMappedLocation1(map_loc_id) {
              document.getElementById("div_btn1").style.display = 'none';
              document.getElementById("div_btn2").style.display = 'block';
              M_LocID_for_Update = map_loc_id;
              $.ajax({
                  type: "POST",
                  url: "LocationMasterRedefined1.asmx/EditMappedLocation",
                  data: '{"map_loc_id":' + JSON.stringify(map_loc_id) + '}',
                  contentType: "application/json; charset=utf-8",
                  dataType: "json",

                  success: function (data) {
                      if (data.d != '') {
                          var FirstAr = data.d.split(",");
                          if (FirstAr.length > 0) {

                              for (var i = 0; i <= FirstAr.length - 1; i++) {
                                  document.getElementById("chkbx" + FirstAr[i] + "").checked = true;

                              }
                          }
                      }

                  }
                  ,
                  failure: function (response) {
                      showMessage('1', response.d);

                  },
                  error:

                      function (xhr, ajaxOptions, thrownError) {
                          alert(xhr.status);
                          alert(xhr.responseText);
                          alert(thrownError);
                      }
              });
          }

          function btnMapSave_Click() {
              var ArryTo_Map = new Array();
              $.ajax({
                  type: "POST",
                  url: "LocationMasterRedefined1.asmx/btnMapSave_Click1",
                  data: '{}',
                  contentType: "application/json; charset=utf-8",
                  dataType: "json",
                  async: false,
                  success: function (data) {
                      if (data.d != '') {
                          var FirstArr = data.d.split(",");
                          if (FirstArr.length > 0) {
                              for (var i = 0; i <= FirstArr.length - 1; i++) {
                                  if (document.getElementById("chkbx" + FirstArr[i] + "").checked == true) {
                                      ArryTo_Map.push(FirstArr[i]);
                                  }
                              }
                          }
                      }
                  }
                  ,
                  failure: function (response) {
                      showMessage('1', response.d);

                  },
                  error:

                      function (xhr, ajaxOptions, thrownError) {
                          alert(xhr.status);
                          alert(xhr.responseText);
                          alert(thrownError);
                      }
              });
              if (ArryTo_Map.length > 0) {
                  $.ajax({
                      type: "POST",
                      url: "LocationMasterRedefined1.asmx/btnMapSave_Click2",
                      data: '{"ArryTo_Map":' + arrayToString(ArryTo_Map) + ',"M_LocID_for_Update":' + M_LocID_for_Update + '}',
                      contentType: "application/json; charset=utf-8",
                      dataType: "json",
                      async: false,
                      success: function (data) {
                          alert(data.d);
                          if (data.d == 'Updated .....!!') {
                              btnMapReset_Click();
                          }
                      }
                      ,
                      failure: function (response) {
                          showMessage('1', response.d);

                      },
                      error:

                          function (xhr, ajaxOptions, thrownError) {
                              alert(xhr.status);
                              alert(xhr.responseText);
                              alert(thrownError);
                          }
                  });
              }
              else {
                  alert("please select item's from Location object  ....!!");
              }

          }

      </script>
 <style>
    .ajax__tab_xp .ajax__tab_tab
    {
        height:auto!important
    }
 </style>
</asp:Content>
<asp:Content ID="locaMastMain" runat="server" ContentPlaceHolderID="MainContent">
        <asp:UpdateProgress ID="UpPorg1" runat="server">
            <ProgressTemplate>
                <NN:Mak ID="FF1" runat="server" />
            </ProgressTemplate>
        </asp:UpdateProgress>
      <asp:UpdatePanel ID="MainUpdatePanel" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
      <ContentTemplate>
         <div class="container tableborderst" >   
 
        <div style="width: 100%; display:none" class="title"> 
              <div style="display:none">
                  <asp:Literal ID="lblt1" runat="server"  Text="Location Master"></asp:Literal>

              </div>
              </div>
       
          <div style="width: 100%; text-align: left;">
              <ajax:TabContainer ID="AjaxTabContainer1"  runat="server" Width="100%" ActiveTabIndex="0">
                  <ajax:TabPanel ID="TabPanel1" runat="server" CssClass="ajax__tab_tab" Width="100%" HeaderText="Location Object">
                      <ContentTemplate>

                          <asp:Panel ID="Panle1" runat="server" Width="100%">
                             <div class="" style="width:100%">
                             
                                  <table class="table-condensed no-more-tables" style="width:100%">
                                      <tr>
                                          <td >
                                              <asp:Label ID="Label14" runat="server"  Text="Location Object"></asp:Label> 
                                          </td>
                                          <td >
                                              <input type="text" id="txtLocationObject"  onchange="CopytxtLocationObject_txt();" /><span style="color: red">*</span>
                                          </td>
                                      </tr>
                                      <tr>
                                          <td > <asp:Label ID="Label1" runat="server"  Text="Abbreviation"></asp:Label>
                                          </td>
                                          <td >
                                              <input type="text" id="txtLocationObjectShortname"  />
                                          </td>
                                      </tr>
                                      <tr>
                                          <td > <asp:Label ID="Label2" runat="server"  Text="Order No"></asp:Label>
                                          </td>
                                          <td >
                                              <input type="text" id="txtOrderNo"  />
                                          </td>
                                      </tr>
                                      <tr>
                                          <td colspan="2" style="text-align:center">
                                              <input type="button" id="btnSaveLocationObject" value="Save" class="btnstyle" onclick="btnSaveLocationObject_Click();" />
                                              <input type="button" id="btnDeleteLocationObject" value="Delete" disabled="disabled" class="btnstyle" onclick="btnDeleteLocationObject_Click()" />
                                              <input type="button" id="btnResetLocationObject" value="Reset" class="btnstyle" onclick="btnResetLocationObject_Click()" />
                                              <input type="hidden" id="HidLocationObjectId" value="-1" />
                                          </td>
                                      </tr>
                                  </table>
                                 </div>
                                  <div style="width: 100%; ">
                                      <input type="checkbox" id="ChkForLocationObject" onchange="GetDataForLocationObject();" /><label for="ChkForLocationObject" >Show Existing</label>
                                  </div>
                                  <div style="border: 1px solid black; width: 100%; display: none; background-color: white; overflow:auto; height:300px;" id="ShowDataLocationObject"></div>
                             
                          </asp:Panel>

                      </ContentTemplate>
                  </ajax:TabPanel>
                  <ajax:TabPanel HeaderText="Location Object Items" Width="100%" runat="server" ID="TabPanel2">
                      <ContentTemplate>
                          <asp:Panel ID="Panel2" runat="server"  Width="100%" BackColor="Silver" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">
                             
                                 
                                  <div style="width: 100%; border:1px solid white;">
                                      <div style="width:100%; background-color: darkgrey; ">
                                             
                                          <table class="table-condensed no-more-tables" >
                                              <tr>
                                          <td ><asp:Label ID="Label3" runat="server"  Text="Location Object"></asp:Label>
                                             
                                          </td>
                                          <td>
                                              <div id="divSelectLocationObject"  ></div>
                                          </td>
                                           <td>
                                              <span style="color: red; ">*</span>
                                          </td>
                                                  
                                      </tr>
                                              <tr>
                                                  <td ><asp:Label ID="Label4" runat="server"  Text="Add Item"></asp:Label> 
                                                  </td>
                                                  <td >
                                                      <input type="text" id="txtAddItem" style="border: 1px solid black" /><span style="color: red">*</span>
                                                  </td>
                                                  <td >
                                                      <input type="button" id="btnSaveAddItem" value="Save" class="btnstyle" onclick="btnSaveAddItem_Click();" />
                                                  </td>
                                              </tr>
                                          </table>
                                                
                                      </div>
                                      <input type="hidden" id="itemID" value="-1" />

                                      <div style=" background-color: #4d4d4d; text-align: center; color: white; margin-top: 5px; margin-bottom: 5px;">Or</div>

                                      <div style="width:100%; background-color:darkgrey;">
                                           
                                          <table class="table-condensed no-more-tables">
                                              <tr>
                                                  <td ><asp:Label ID="Label5" runat="server"  Text="Alias"></asp:Label>  
                                                  </td>
                                                  <td >
                                                      <input type="text" id="txtAlias" style="border: 1px solid black; width: 80%" />
                                                  </td>
                                                  <td >  <asp:Label ID="Label6" runat="server"  Text="From"></asp:Label>   
                                                  </td>
                                                  <td >
                                                      <input type="text" id="txtFromItem" style="border: 1px solid black; width: 80%" /><span style="color: red">*</span>
                                                  </td>
                                                  <td > <asp:Label ID="Label7" runat="server"  Text="To"></asp:Label>  
                                                  </td>
                                                  <td >
                                                      <input type="text" id="txtToItem" style="border: 1px solid black; width: 80%" /><span style="color: red">*</span>
                                                  </td>
                                                  <td >
                                                      <input type="button" id="btnSaveAddMultiItem" value="Generate" class="btnstyle" onclick="btnSaveAddMultiItem_Click();" />
                                                  </td>
                                              </tr>
                                          </table>
                                              
                                      </div>
                                  </div>
                                  <div style="width: 100%; background-color: #666666; text-align: left; margin-top: 20px;">
                                      <input type="checkbox" id="ChkShowLocationItemData" onchange="ChkShowLocationItemData_Change();" /><label for="ChkShowLocationItemData" style="color: white"> Show Existing</label>
                                  </div>
                                  <div style="border: 1px solid black; width: 100%; display: none; background-color: white; overflow: auto; height: 280px" id="DivShowItemData"></div>
                              
                              
                          </asp:Panel>
                      </ContentTemplate>
                  </ajax:TabPanel>
                  <ajax:TabPanel HeaderText="Mapping" Width="100%" runat="server" ID="TabPanel3">
                      <ContentTemplate>
                          <asp:Panel ID="Panel1" runat="server" Width="100%" BackColor="Silver" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ScrollBars="Auto">
                            <div class="CastCat">
                              <div class="leftCastCat">
                                     
                                  <table class="table-condensed no-more-tables" style="width:100%">
                                      <tr>
                                          <td style="border: 1px solid #696969;">
                                              <div class="confirmPanel" style="width:100%;background-color:white;height:40px">
                                               <asp:Label ID="Label8" runat="server"  Text="Location Objects"></asp:Label>  
                                                  </div>
                                                  <div id="DivShowingObjects"  class="objl" style="overflow:auto; background-color:white;border:0">
                                                                                                                  
                                                          <img src="images/ajax_loader.gif" />Processing...
                                                   
                                                  </div>
                                              
                                                 
                                                 </td>
                              </tr>
                              </table>
                                          
                                 </div>
                                       <div class="rightCastCat">
                                          
                                              <div class="confirmPanel" style="width:100%;background-color:white;height:40px">
                                              <asp:Label ID="Label9" runat="server"  Text="Mapped Locations"></asp:Label>   
                                                  </div>
                                               
                                                  <div style="width: 100%; background-color: #4d4d4d; text-align: center; color: white; margin-bottom: 5px">
                                                      <table class="table-condensed">
                                                          <tr>
                                                              <td> <asp:Label ID="Label10" runat="server"  Text="Search Location"></asp:Label></td>
                                                              <td>
                                                                  <input type="text" id="locsearchtxtbx" onkeyup="GetSuggestion();" onblur="HideDiv();" onfocus="ShowDiv();" />
                                                                  <div id="divSuggestion" style="display:none;" class="GetSuggestion">
                                                                      <center>                                                                
                                                                          <img src="images/ajax_loader.gif" />Processing...
                                                                      </center>
                                                                  </div>
                                                              </td>
                                                              <td >
                                                                  <input type="button" id="btnLocationSearch" class="btnstyle" value="Search" onclick="GetSearchedRecords()" /></td>
                                                          </tr>
                                                      </table>
                                                  </div>
                                                  <div id="DivMappedItems" style="height:410px; width:100%; overflow:auto; background-color: white">
                                                      <center>                                                                
                                                          <img src="images/ajax_loader.gif" />Processing...
                                                      </center>
                                                  </div>
                                              
                                           </div>
                                    </div>    
                              <table class="table-condensed no-more-tables" border="1">
                                  <tr>
                                      <td>
                              
                                              <div id="DivOptions" style="width:100%">
                                                  <table class="table-condensed">
                                                      <tr>
                                                          <td  style="text-align:center">
                                                              <div id="div_btn1">
                                                  <input type="button" class="btnstyle" value="Map" id="btnMap" onclick="btnMap_Click()" />
                                                  <input type="button" class="btnstyle" value="UnMap" id="btnUnMap" onclick="btnUnMap_Click()" />
                                                      
                                                     
                                                    <input type="button" class="btnstyle" value="Reset" id="btnMapReset" onclick="btnMapReset_Click()" /> 
                                                                  </div>  
                                                     <div id="div_btn2" style="display:none;">
                                                      <input type="button" class="btnstyle" value="Update" id="btnMapSave" onclick="btnMapSave_Click();" />
                                                  </div></td>
                                                           </tr>
                                                  </table>
                                                  
                                                  
                                                  
                                              </div>

                                          </td>
                                          <td style="text-align:right">
                                              <asp:Label ID="Label11" runat="server"  Text="Page No."></asp:Label>
                                          </td>
                                          <td> 
                                              <select id="selPageNo" onchange="selPageNo_Change();"></select>
                                          </td>
                                      </tr>
                                  </table>
                                        
                             
                          </asp:Panel>
                      </ContentTemplate>
                  </ajax:TabPanel>
              </ajax:TabContainer>
          </div>
             </div>
      </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
