$(function () {
    
    //$(document).ajaxStart(function () {
    //    $('#loadingModal').modal();
    //    //clearconsole();
    //});

    //$(document).ajaxStop(function () {
    //    $('#loadingModal').modal('hide');
    //    //clearconsole();
    //});

    //$(document).ajaxComplete(function () {
    //    $('#loadingModal').modal('hide');
    //    //clearconsole();
    //});

    $('.require-login').on('click', function () {
        if ($('#u').val() == '') {
            $('#modalLogin2').modal();
            return false;
        }
    });

    $('nav.filter ul li a').each(function (index) {
        $(this).click(function () {
            $('nav.filter ul li a').each(function (index) {
                $(this).removeClass("current");
            });
            $(this).addClass("current");
        });
    });
});

function clearconsole() {
    console.log(window.console);
    if (window.console || window.console.firebug) {
        console.clear();
    }
}

var audio;
function speak(url) {
    if (audio != null) {
        audio.pause();
    }
    audio = new Audio(url);
    //audio.load();
    audio.play();
}

