function shoData() {
    alert('ok');
    let val = $("[id$=txtAccFUC]").val();
    console.log(val);
}
$(function () {
    Mesg();
})

$(document).ready(function () {
    try {
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(Mesg);

    } catch { }
})
function Mesg() {
    $('[id$="txCatDFUC"],[id$="txCatDUUC"] ').datepicker(
        {
            dateFormat: 'dd-M-yy',
            changeMonth: true,
            changeYear: true,
        });
    $('.inpUc').on('keypress', function (event) {
        let ke = event.keyCode;
        if (ke == 13) {
            event.preventDefault();
            PostBac(event);
        }
    });
    try {
        let c = $("[id$='grdDataUC'] > thead > tr").length;
        if (c == 0) {
            $('[id$="grdDataUC"] tbody').before('<thead><tr></tr></thead>');
            $("[id$='grdDataUC'] thead tr").append($("[id$='grdDataUC'] th"));
            $("[id$='grdDataUC'] tbody tr:first").remove();
        }
        let cb = $("[id$='grdDataUC'] tbody tr").length;
        if (cb > 0) {
            $("[id$='grdDataUC']").DataTable();
        }

    } catch (er) {
        alert(er + '; Make sure grid has data');
    }
    $('#dvSearchH').draggable();
    let hsSerH = $('[id$=hdShoUC]').val();//hdShoUC
    if (hsSerH == 'y') {
        $('[id$=hdShoUC]').val('');
        $('#dvSearchH').css('display', 'block');
    }
}

function savMarcd()
{
    $('[id$=btnSaveMarc]').click();

    return false;
}

function PostBac() {
    $(".loader").css('display', 'block');
    $("[id$='btnUCSh']").click();
    return false;
}

function Exp2Exc() {
    $('[id$="grdDataUC"]').DataTable().destroy();
    //        $('[id$="grdData"] thead th').removeClass('UcHideCol');
    //       $('[id$="grdData"] tbody td').removeClass('UcHideCol');

    $("[id$='grdDataUC']").table2excel();
    $('[id$="grdDataUC"]').dataTable().api();
    return false;
}



function shoSerH(chk) {
    let chkv = $(chk).is(':checked');
    if (chkv == true)
        $('#dvSearchH').css('display', 'block');
    else
        $('#dvSearchH').css('display', 'none');

}
function UCclosit(sp) {
    $('#dvSearchH').css('display', 'none');
    $('[id$=chkUCShoSH]').prop('checked', false);
}

function UCAccnSentLocal(ele, e) {
    e.preventDefault();
    UCAccnSent(ele.innerText);
    return false;
}
function ShowHidCols() {
    $('[id$="grdDataUC"]').DataTable().destroy();
    $('[id$="grdDataUC"] thead th').removeClass('UcHideCol');
    $('[id$="grdDataUC"] tbody td').removeClass('UcHideCol');
    $('[id$="grdDataUC"]').dataTable().api();
    return false;
}

function sel10(txt) {
    let digt = $(txt).val();
    if (!isFinite(digt)) {
        $(txt).val('');
        return;
    }
    let d2 = digt * 1;

}