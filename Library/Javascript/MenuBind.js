let mnopen = false;
$(function (doc) {
    let usr = $('[id$=hdUser').val();
    $('#spUser').text(usr);
     getMenu();
    let menuop = false;
    let pgTitle = sessionStorage.getItem('pgtitle');
    //    
    if (pgTitle != undefined) {
        try {
            $('#spSelTitle').text(pgTitle);
        } catch {

        }
    }
    $('#svgOpclose').on('click', function () {
        let x = $('.sidebar').hasClass('sidebar-close');
        if (x) {
            $('.sidebar').removeClass('sidebar-close');
            $('.sidebar').addClass('sidebar-open');
        } else {
            $('.sidebar').addClass('sidebar-close');
            $('.sidebar').removeClass('sidebar-open');
        }
        menuop = true;
    });
    $('body').on('click', function (e) {
        $('a').on('click', function () {
            menuop = true;
        });
        if (!menuop) {
            $('.sidebar').addClass('sidebar-close');
            $('.sidebar').removeClass('sidebar-open');
        }
        menuop = false;
//        $('#myDropdown').removeClass('show');
        $('#dvVisited').css('display', 'none');

    });
    /*
    $('#svgOpclose').on('click', function () {
        console.log($(this));
        let x = $('.sidebar').hasClass('sidebar-close');
        mopen = true;
        if (x) {
            $('.sidebar').removeClass('sidebar-close');
            $('.sidebar').addClass('sidebar-open');
        } else {
            $('.sidebar').addClass('sidebar-close');
            $('.sidebar').removeClass('sidebar-open');

        }
        return;
    });
*/
    
})

function CalledPages(pg) {
    let title = $(pg).text();
    let href = $(pg).attr('href');
    sessionStorage.setItem('pgtitle', title);
    let d = href + '|' + title;
    CalledPagesLM(d);
}
function CalledPagesLM(pg) {
    let d = pg + '';
    let d2 = d.split('|');
    $.ajax({
        type: "POST",
        url: "MssplSugg.asmx/LogOpenPage",
        data: "{'hrefLink':'" + d2[0] + "','pgid':'" + d2[1] + "','Uid':''}",
        contentType: "application/json",
        datatype: "json",
        success: function (responseFromServer) {
            //alert(responseFromServer.d)
            console.log('logOpenPage-Called-success'); //print may not be available as page redirects 
            console.log(responseFromServer);
            //ClosWin('');
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(thrownError + ';x');
            console.log(xhr.status);//print may not be available as page redirects 
            console.log(xhr.responseText);
            console.log(thrownError);
        }
    });
    let lnk = pg.substring(pg.indexOf('title=') + 6, 100);
    lnk = lnk.substring(0, lnk.indexOf('&'));
    sessionStorage.setItem('pgtitle', lnk);
    window.location.href = pg;
//    sessionStorage.setItem('pgtitle', pg);

}

function StorPg(ele) {
    //spSelTitle
    CalledPages(ele);
    return false;
}
function setMenu() {
    $("#menu").metisMenu();
//    const mm = new MetisMenu("#menu");
  //  mm.dispose();
    ////    $("#menu").metisMenu('dispose');
    //mm.update();

}
function UnsetMenu() {
    const mm = new MetisMenu("#menu");
    mm.dispose();
//    $("#menu").metisMenu('dispose');
    mm.update();
}
function getMenu() {
    $.ajax({
        type: "POST",
        url: "./MssplSugg.asmx/Menu2021",
        data: '',

        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            let mitsm = $('#navmn').html();
            $('#navmn').html(data.d);
            setTimeout(setMenu(), 350);
            return;
            /*
            try {
                let firstload = sessionStorage.getItem('firstload');
//                $('#navmn').empty();
                
                if (firstload!=undefined ) {
                    alert('1');
                    $('#navmn').html(data.d);
                    const mm = new MetisMenu("#menu");
                    alert('2');
                    mm.dispose();
                    alert('3');

                    $('#navmn').empty();
                    alert('4');

                    $('#navmn').html(data.d);
                    alert('5');

                    //    $("#menu").metisMenu('dispose');
                    mm.update();
                } else {
                    sessionStorage.setItem('firstload','y');
                    $('#navmn').html(data.d);
                    setTimeout(setMenu(), 350);

                }
            } catch(ers) {
                console.log(ers);
            }
            */
/*
            $('#navmn').empty();
            $('#navmn').html(data.d);
            try {
                $("#menu").metisMenu('dispose');

            } catch {

            }
            try {
                $("#menu").metisMenu();

            } catch (ers) {
                console.log('bind menu plugin failed: ' + ers);
            }
            */
//            linksManagement();
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
function togglMenu(event) { //not in use
    if (mnopen) {
        alert(mnopen);
        let x = $('.sidebar').hasClass('sidebar-close');
        if (x) {
            $('.sidebar').removeClass('sidebar-close');
            $('.sidebar').addClass('sidebar-open');
        } else {
            $('.sidebar').addClass('sidebar-close');
            $('.sidebar').removeClass('sidebar-open');

        }
        mnopen = false;
    }
}
$(function () {
//    $("#menu").metisMenu({
//        //// enabled/disable the auto collapse.
//        // toggle: true,
//        //       // prevent default event
//               preventDefault: true,
//        //       // default classes
//        //       activeClass: 'mm-active',
//        //       collapseClass: 'collapse',
//        //       collapseInClass: 'in',
//        //       collapsingClass: 'collapsing',
//        //       // .nav-link for Bootstrap 4
//        //       triggerElement: 'a',
//        //       // .nav-item for Bootstrap 4
//        //       parentTrigger: 'li',
//        //       // .nav.flex-column for Bootstrap 4
//        //       subMenu: 'ul'
//    });
//    $("#menu a").each(function () {
////        console.log($(this));
//  //      $(this).on('click', function (anc,e) {
//   //         subClick($(this,e));
//    //    });
////        $(this).preventDefault();
//    })
////    jQuery('#menu').metisMenu();

//   // $("#menu").show.metisMenu();
})


function subClick(anc,e){
    alert($(anc).attr('id'));
    e.preventDefault();
}