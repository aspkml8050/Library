$(document).ready(function (e) {
    $.ajax({
        type: "POST",
        url: "generateDynmICard.aspx/getPageSize",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            document.getElementById("selPageSize").innerHTML = data.d;
        },
        failure: function (response) {
        },
        error: function (xhr, ajaxOptions, thrownError) {
        }
    });

    $.ajax({
        type: "POST",
        url: "generateDynmICard.aspx/getIDCardFormat",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            document.getElementById("selCardFormat").innerHTML = data.d;
        },
        failure: function (response) {
        },
        error: function (xhr, ajaxOptions, thrownError) {
        }
    });

    $.ajax({
        type: "POST",
        url: "generateDynmICard.aspx/getAcedemicSession",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            document.getElementById("selSession").innerHTML = data.d;
        },
        failure: function (response) {
        },
        error: function (xhr, ajaxOptions, thrownError) {
        }
    });

    $.ajax({
        type: "POST",
        url: "generateDynmICard.aspx/getMemberGroup",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            document.getElementById("selMemberGroup").innerHTML = data.d;
        },
        failure: function (response) {
        },
        error: function (xhr, ajaxOptions, thrownError) {
        }
    });

    $.ajax({
        type: "POST",
        url: "generateDynmICard.aspx/getDepartments",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            document.getElementById("selDept").innerHTML = data.d;
        },
        failure: function (response) {
        },
        error: function (xhr, ajaxOptions, thrownError) {
        }
    });

    $.ajax({
        type: "POST",
        url: "generateDynmICard.aspx/getPrograms",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            document.getElementById("selCourseDesg").innerHTML = data.d;
        },
        failure: function (response) {
        },
        error: function (xhr, ajaxOptions, thrownError) {
        }
    });
});

function btnSearch_Click() {
    document.getElementById("grdDetails").innerHTML = '<img src="img/progress.gif" style="height:30px;" /> Loading...';
    var memId = document.getElementById("txtMemberId").value.trim();

    var session = document.getElementById("selSession").value.trim();
    var memGrp = document.getElementById("selMemberGroup").value.trim();
    var dept = document.getElementById("selDept").value.trim();
    var courseDesg = document.getElementById("selCourseDesg").value.trim();

    $.ajax({
        type: "POST",
        url: "generateDynmICard.aspx/btnSearch_Click",
        data: '{"memId":"' + memId + '","session":"' + session + '","memGrp":"' + memGrp + '","dept":"' + dept + '","courseDesg":"' + courseDesg + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var strTable = '<table style="width:100%; border-collapse:collapse;" border="1" cellspacing="0" cellpadding="5">' +
                            '<tr style="background-color:#0C4B6A;color:#ffffff;height:30px">' +
                                '<th style="text-align:left;"><input id="chkAll" onchange="chkAll_Change();" type="checkbox" /><label for="chkAll" style="color:#ffffff;">Select All</label></th>' +
                                '<th style="text-align:left;">S.No.</th>' +
                                '<th style="text-align:left;">User Code</th>' +
                                '<th style="text-align:left;">Name</th>' +
                                '<th style="text-align:left;">Father Name</th>' +
                            '</tr>';
            if (data.d.length > 0) {
                for (i = 0; i < data.d.length; i++) {
                    strTable += '<tr>' +
                                    '<td style="text-align:left;"><input id="chk_' + i + '" value="' + data.d[i].usercode_P + '" name="chk" type="checkbox" /><label for="chk_' + i + '"><img src="' + data.d[i].photoUrl_P + '" style="height:30px; width:30px;" /></label></td>' +
                                    '<td style="text-align:left;">' + (i + 1) + '</td>' +
                                    '<td style="text-align:left;">' + data.d[i].usercode_P + '</td>' +
                                    '<td style="text-align:left;">' + data.d[i].memberName_P + '</td>' +
                                    '<td style="text-align:left;">' + data.d[i].fatherName_P + '</td>' +
                                '</tr>';
                }
            }
            strTable += "</table>";
            document.getElementById("grdDetails").innerHTML = strTable;
        },
        failure: function (response) {
        },
        error: function (xhr, ajaxOptions, thrownError) {
        }
    });
}

function chkAll_Change() {
    var chkCtrl = document.getElementsByName("chk");
    for (i = 0; i < chkCtrl.length; i++) {
        chkCtrl[i].checked = document.getElementById("chkAll").checked;
    }
}

function btnGenerate_Click() {
    var memberList = '';
    var chkCtrl = document.getElementsByName("chk");
    for (i = 0; i < chkCtrl.length; i++) {
        if (chkCtrl[i].checked == true) {
            if (memberList.trim() == '') {
                memberList = chkCtrl[i].value.trim();
            }
            else {
                memberList += ',' + chkCtrl[i].value.trim();
            }
        }
    }   
    
    var cardFormat = document.getElementById("selCardFormat").value.trim();
    if (cardFormat == 0) {
        showHTML_MessageDelay(0, "Select the ID Card Format !");
        document.getElementById("selCardFormat").focus();
        return false;
    }
    var pSize = document.getElementById("selPageSize").value.trim();
    if (pSize == 0) {
        showHTML_MessageDelay(0, "Select the Page Size !");
        document.getElementById("selPageSize").focus();
        return false;
    }    
    if (memberList.trim() == '') {
        showHTML_MessageDelay(0, "Select the Members first to Generate the ID-Card!");
        return false;
    }

    $.ajax({
        type: "POST",
        url: "generateDynmICard.aspx/btnGenerate_Click",
        data: '{"memberList":"' + memberList + '","pSize":"' + pSize + '","cardFormat":"' + cardFormat + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
                var alink = document.createElement("a");
                alink.target = "_blank";
                alink.href = data.d;
                alink.click();
        },
        failure: function (response) {
        },
        error: function (xhr, ajaxOptions, thrownError) {
        }
    });
}

function btnReset_Click() {
    document.getElementById("txtMemberId").value = '';
    document.getElementById("selCardFormat").selectedIndex = 0;
    document.getElementById("selPageSize").selectedIndex = 0;

    document.getElementById("selSession").selectedIndex = 0;
    document.getElementById("selMemberGroup").selectedIndex = 0;
    document.getElementById("selDept").selectedIndex = 0;
    document.getElementById("selCourseDesg").selectedIndex = 0;

    document.getElementById("grdDetails").innerHTML = '';
}