function addCssMast(filename) {
    var head = document.getElementsByTagName('head')[0];
    var style = document.createElement('link');
    style.href = filename;
    style.type = 'text/css';
    style.rel = 'stylesheet';
    head.append(style);
}
function MasterMesg(mesg, msglvl) {
    addCssMast('cssDesign/MasterMesgStyle.css');
    $('.MasterMessg').remove();

    let dvCont;
    switch (msglvl) {
        case 1:
            dvCont = '<div class="MasterMessg MasterSucc " >';

            break;
        case 2:
            dvCont = '<div class="MasterMessg MasterWarn " >';
            break;
        case 3:
            dvCont = '<div class="MasterMessg MasterErr " >';
            setTimeout(recordErr(mesg ), 500);
            break;
        case 4:
            $('.MasterMessg').css('display', 'none');
            return;
    }
    dvCont += mesg + '</div>';
   $('.LayWidCont').prepend(dvCont);

//    $('#spTitle').prepend(dvCont);
    setTimeout('MastMesgDimIt();', 20000);
    setTimeout('MastMesgDimItClos();', 150000);
    $('.MasterMessg').on('click', function () {
        $(this).css('display', 'none');
    });
}

function MastMesgDimIt() {
    $('.MasterMessg').addClass('MasterDimdv');
}
function MastMesgDimItClos() {
    $('.MasterMessg').css('display', 'none');
}

var errordata ={
    title :"",
        pagename:"",
        linenumber:""
    }
function recordErr(message) {
    errordata.title = $('[id$=spTitle]').text();
    let urls = window.location.href;
    urls = urls.substring(0, urls.indexOf('.aspx') + 5);
    errordata.pagename = urls;
    errordata.message = message;
    errordata.linenumber = "0";
    let mesgd = JSON.stringify(errordata);
    console.log(mesgd);
    $.ajax({
        type: "POST",
        url: "./MssplSugg.asmx/errorlogindb",
        data: '{errd:' + mesgd + '}',

        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            console.log(data.d);

        }
        ,
        failure: function (response) {

            console.log('Header failed');
        },
        error:
            function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(xhr.responseText);
                alert(thrownError);
            }
    });
}

function getAutoPageSearchExtender(source, eventArgs) {
    var src = eventArgs.get_value();
    src = src.toLowerCase();
    //src = src.substring(0, src.indexOf('.aspx') + 5);
    var title = eventArgs.get_text();
    let ancLis = $('.nav').find('a');
    sessionStorage.setItem('pgtitle', title);

    location.href = src + '?title=' + title + '&condition=Y';
    let hre = src + '?title=' + title + '&condition=Y';
    let anc;
    // let ancLis = $('.nav').find('a');
    for (let idx = 0; idx < ancLis.length; idx++) {
        let an = $(ancLis[idx]).attr('href').toLowerCase();
        if (an == undefined)
            continue;
        an = an.substring(0, an.indexOf('.aspx') + 5);
        if (an == src) {
            anc = $(ancLis[idx]);
            break;
        }
    }
    try {
//        showLinksMpage(anc);
    } catch { alert('shomlinst:mufunc failed '); }
    let id = '';
    $.ajax({
        type: "POST",
        url: "MssplSugg.asmx/LogOpenPage",
        data: "{'hrefLink':'" + hre + "','pgid':'" + id + "','Uid':''}",
        contentType: "application/json",
        datatype: "json",
        success: function (responseFromServer) {
            //alert(responseFromServer.d)
            console.log('logOpenPage-Called-success'); //print may not be available as page redirects 
            console.log(responseFromServer);
            ClosWin('');
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(thrownError + ';x');
            console.log(xhr.status);//print may not be available as page redirects 
            console.log(xhr.responseText);
            console.log(thrownError);
        }
    });
    return true;


    //        document.getElementById("doc").src = src + '?title=' + title + '&condition=Y'
    //   document.getElementById("headerPart2_txttitletop").value = '';
}

function openVisited() {
    $('#dvVisited').css('display', 'block');
}

function IntegerNumber(ele,e) {
    var d = ele.value;
    let v = isFinite(d);
    if ((v == undefined) || (v == false)) {
        ele.value = '';
        e.preventDefault();
        return false;
    }
    else
        return true;
    
}
function decimalNumber(ele) {
    let v = isFinite($(ele).val());
    if ((v == undefined) || (v == false))
        return false;
    else
        return true;

}

