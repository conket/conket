$(function () {
    if ($('#divVocaSet').length) {
        $('#loading').show();
        $('#divVocaSet').load('/Library/' + $('#ls').val(), function (response, status, xhr) {
            $('#loading').hide();
            if (status == "error") {
                console.log("Sorry but there was an error: " + xhr.status + " " + xhr.statusText);
            }
        });
        loadS();
        loadC();
    }
    if ($('#divVocaCate').length) {
        $('#loading').show();
        $('#divVocaCate').load('/Library/' + $('#vs').val() + '/' + $('#vsd').val(), function (response, status, xhr) {
            $('#loading').hide();
            if (status == "error") {
                console.log("Sorry but there was an error: " + xhr.status + " " + xhr.statusText);
            }
        });
    }
    if ($('#divVoca').length) {
        $('#loading').show();
        $('#divVoca').load('/Library/' + $('#vc').val() + '/' + $('#vcd').val(), function (response, status, xhr) {
            $('#loading').hide();
            if (status == "error") {
                console.log("Sorry but there was an error: " + xhr.status + " " + xhr.statusText);
            }
        });
    }
    if ($('#divNotebookVoca').length) {
        $('#loading').show();
        $('#divNotebookVoca').load('/Library/' + $('#nvc').val(), function (response, status, xhr) {
            $('#loading').hide();
            if (status == "error") {
                console.log("Sorry but there was an error: " + xhr.status + " " + xhr.statusText);
            }
        });
    }
});

function loadS() {
    var s = $('#s').val();
    $.ajax({
        type: "get",
        url: '/Home/' + s,
        global: false,
        success: function (result) {
            if (result.returnCode != 0) {
                //alert('Có lỗi xảy ra!');
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.responseText);
            return false;
        }
    });
}

function loadC() {
    var c = $('#c').val();
    $.ajax({
        type: "get",
        url: '/Home/' + c,
        global: false,
        success: function (result) {
            if (result.returnCode != 0) {
                //alert('Có lỗi xảy ra!');
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.responseText);
            return false;
        }
    });
}