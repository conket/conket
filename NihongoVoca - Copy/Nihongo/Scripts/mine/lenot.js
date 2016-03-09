

var totalVoca = 0;
var numOfHasLearnt = 0;
var defaultVocas = [];
var vocas = [];
var selectedArray = [];
var currentIndex = 0;
$(document).ready(function () {
    getNotebookVocas();

    var ss = $('#ss').val();
    if (ss == null || ss == "") {
        $('#btnAdd').prop('disabled', true);
        $('#btnCheck').addClass('disabled');
    } else {
        $('#btnAdd').prop('disabled', false);
        $('#btnCheck').removeClass('disabled');
    }

    $('#btnMark').on('click', function () {
        updateHasMarked();

        return false;
    });

    $('#btnLast').on('click', function () {
        currentIndex = vocas.length - 1;
        showFlashCard(currentIndex, true);

        $('.navigator').removeClass("btn-primary");
        $('.navigator').addClass("btn-default");
        $(this).removeClass("btn-default");
        $(this).addClass("btn-primary");
    });

    $('#btnNext').on('click', function () {
        if (currentIndex < vocas.length - 1) {
            currentIndex++;
            showFlashCard(currentIndex, true);

            $('.navigator').removeClass("btn-primary");
            $('.navigator').addClass("btn-default");
            $(this).removeClass("btn-default");
            $(this).addClass("btn-primary");
        }
    });

    $('#btnFirst').on('click', function () {
        currentIndex = 0;
        showFlashCard(currentIndex, true);

        $('.navigator').removeClass("btn-primary");
        $('.navigator').addClass("btn-default");
        $(this).removeClass("btn-default");
        $(this).addClass("btn-primary");
    });

    $('#btnPrevious').on('click', function () {
        if (currentIndex > 0) {
            currentIndex--;
            showFlashCard(currentIndex, true);
            $('.navigator').removeClass("btn-primary");
            $('.navigator').addClass("btn-default");
            $(this).removeClass("btn-default");
            $(this).addClass("btn-primary");
        }
    });

    $(document).keydown(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (event.ctrlKey) {
            //ctrl ->
            if (keycode == 39) {
                $("#btnNext").trigger("click");
                return false;
            }
                //ctrl <-
            else if (keycode == 37) {
                $("#btnPrevious").trigger("click");
                return false;
            }
        }
            //space
        else if (keycode == 32) {
            speak(vocas[currentIndex].UrlAudio);
            return false;
        }
            //enter
        else if (keycode == 13) {
            $("#btnNext").trigger("click");
            return false;
        }
            //->
        else if (keycode == 39) {
            $("#flashNext").trigger("click");
            return false;
        }
            //<-
        else if (keycode == 37) {
            $("#flashPre").trigger("click");
            return false;
        }
    });

});


function updateHasMarked() {
    if (vocas[currentIndex].HasMarked == "0") {
        alert('Từ vựng này đã xóa khỏi Sổ tay');
        return;
    }
    vocas[currentIndex].HasMarked = "0";
    if (vocas.length > 0) {
        $.ajax({
            cache: true,
            type: "post",
            async: true,
            url: '/Library/' + $('#uhm').val(),
            data: JSON.stringify(vocas[currentIndex]),
            dataType: "json",
            contentType: 'application/json',
            success: function (result) {
                if (result.ReturnCode == accessDenied) {
                    vocas[currentIndex].HasMarked = "1";
                    window.location.href = '/Account/RequireLogin';
                }
                else if (result.ReturnCode != 0) {
                    vocas[currentIndex].HasMarked = "1";
                    alert('Có lỗi xảy ra');
                }
                else {
                    alert('Đã xóa khỏi Sổ tay của bạn');

                    var isOver = true;
                    for (var i = 0; i < vocas.length; i++) {
                        if (vocas[i].HasMarked == '1') {
                            isOver = false;
                            break;
                        }
                    }
                    if (isOver) {
                        window.location.href = '/thu-vien-tu-vung-tieng-nhat/so-tay-tu-vung';
                    }
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                console.log(xhr.responseText);
                return false;
            }
        });
    }
}

//type:
//"": all
//1: hasLearnt
    //0: has not learnt
    //2: weak
function getNotebookVocas() {
    vocas = [];
    failArray = [];

    var accessDenied = $('#accessDenied').val();
    $.ajax({
        cache: true,
        type: "get",
        async: true,
        url: ('/Library/' + $('#gnv').val()),
        //data: { "id": id },
        dataType: "json",
        success: function (result) {
            if (result.returnCode == accessDenied) {
                window.location.href = '/Account/RequireLogin';
            } else {
                if (typeof type === 'undefined') {
                    totalVoca = result.vocabularies.length;
                    numOfHasLearnt = result.numOfHasLearnt;
                }
                $.each(result.vocabularies, function (i, voca) {
                    vocas.push(voca);
                    defaultVocas.push(voca);

                    var audio = new Audio(voca.UrlAudio);
                    audio.load();
                });
                var hasVoca = true;
                if (vocas.length <= 0) {
                    alert('Không tìm thấy từ vựng');
                    hasVoca = false;
                }
                if (hasVoca) {
                    currentIndex = 0;
                    showFlashCard(currentIndex, true);
                }

                var progressHtml = '<div class="progress-bar" role="progressbar" aria-valuenow="' + numOfHasLearnt + '" aria-valuemin="0" aria-valuemax="' + (totalVoca == 0 ? vocas.length : totalVoca) + '" style="width: ' + (numOfHasLearnt / (totalVoca == 0 ? vocas.length : totalVoca) * 100) + '%; min-width: 2em;">' + numOfHasLearnt + '/' + (totalVoca == 0 ? vocas.length : totalVoca) + '</div>';
                $('#progress').html(progressHtml);

            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.responseText);
            return false;
        },
        beforeSend: function () {
            $('#loadingModal').modal();
        },
        complete: function () {
            $('#loadingModal').modal('hide');
        }
    });
}

function loadSelectedVocas() {
    $("#tblVoca tr:not(:first)").remove();
    for (var i = 0; i < selectedArray.length; i++) {
        if (selectedArray[i] != null) {
            var html = "<tr>";
            html += "       <td>";
            html += "           <button type='button' class='btn btn-link' onclick='showSelectedFlashCard(" + selectedArray[i].LineNumber + ");'>" + selectedArray[i].Hiragana + ';' + selectedArray[i].Katakana + "</button></td>";
            html += "       </td>";
            html += "       <td>";
            html += "           <span id='" + selectedArray[i].LineNumber + "' class='glyphicon glyphicon-remove' aria-hidden='true' onclick='removeVoca(" + selectedArray[i].LineNumber + ");'></span>";
            html += "       </td>";
            html += "   </tr>";

            $('#tblVoca tr:last').after(html);
        }
    }
};

function getLink(url) {
    return url;
};

var timer;

function draw() {
    var voca = vocas[currentIndex];
    //        speak(voca.UrlAudio);

    $('#draw').html('');
    var text = '';
    if (voca != null) {
        if ($('#rdoHiragana').is(':checked') && voca.Hiragana != '') {
            text = voca.Hiragana;
            //var dmak = new Dmak(voca.Hiragana, {
            //    'element': "draw"
            //});
        }
        else if ($('#rdoKatakana').is(':checked') && voca.Katakana != '') {
            text = voca.Katakana;
        }
        else if ($('#rdoKanji').is(':checked') && voca.Kanji != '') {
            text = voca.Kanji;
        }
    }
    var dmak = new Dmak(text, {
        'element': "draw"
    });
}

function showDrawing() {
    $('#modalDraw').modal();
}

function showFlashCard(index, voice) {
    var voca = searchVoca(index);
    if (voca != null) {
        var html = '';
        html += '<div id="carousel-example-generic" class="carousel slide" data-ride="carousel">';
        html += '   <div class="carousel-inner" role="listbox">';
        html += '       <div class="item active">';
        html += '           <div class="row">';
        html += '               <div class="col-lg-4 col-md-4 col-xs-12">';
        html += '                   <img class="img-responsive" src="' + getLink(voca.UrlImage) + '" alt="Từ vựng tiếng Nhật" style="height: 300px" />';
        html += '               </div>';
        html += '               <div class="col-lg-8 col-md-8 col-xs-12">';
        html += '                   <p class="text-info">Định nghĩa</p>';
        html += '                   <p class="text-default">' + voca.Description + '</p>';
        html += '               </div>';
        html += '           </div>';
        html += '       </div>';
        html += '       <div class="item">';
        html += '           <div class="row">';
        html += '               <div class="col-lg-4 col-md-4 hidden-xs hidden-sm">';
        html += '                   <img class="img-responsive" src="' + getLink(voca.UrlImage) + '" alt="Từ vựng tiếng Nhật" style="height: 300px" />';
        html += '               </div>';
        html += '               <div class="col-lg-8 col-md-8">';
        //kanji
        if (voca.DisplayType == "3") {
            html += '                   <div class="row">';
            html += '                       <div class="col-md-12">';
            html += '                       <div class="col-md-4 col-sm-12 col-xs-12">';
            html += '                           <h2><p class="text-info">' + voca.Kanji + '</p><span class="selected glyphicon glyphicon-volume-up" aria-hidden="true" onclick="speak(\'' + voca.UrlAudio + '\');"></span></h2>';
            html += '                           <p class="text-default"><strong>Nghĩa</strong>: ' + voca.VMeaning + '</p>';
            html += '                           <p class="text-default"><strong>Hán Việt</strong>: ' + voca.Pinyin + '</p>';
            html += '                       </div>';

            html += '                       <div class="col-md-4 col-sm-6 col-xs-6">';
            html += '                           <p class="text-info">On-yomi: </p>';
            if (voca.OnReading != '') {
                html += '                       <p class="text-default">' + voca.OnReading + ' ' + voca.OnRomaji;
                html += '                       </p>';
            }
            if (voca.OnReading2 != '') {
                html += '                       <p class="text-default">' + voca.OnReading2 + ' ' + voca.OnRomaji2;
                html += '                       </p>';
            }
            html += '                       </div>';

            html += '                       <div class="col-md-4 col-sm-6 col-xs-6">';
            html += '                           <p class="text-info">Kun-yomi: </p>';
            if (voca.KunReading != '') {
                html += '                       <p class="text-default">' + voca.KunReading + ' ' + voca.KunRomaji;
                html += '                       </p>';
            }
            if (voca.KunReading2 != '') {
                html += '                       <p class="text-default">' + voca.KunReading2 + ' ' + voca.KunRomaji2;
                html += '                       </p>';
            }
            html += '                       </div>';
            html += '                   </div>';
            html += '                   </div>';

            html += '                   <hr class="divider" />';

            html += '                   <div class="col-md-4 text-center">';
            html += '                   <button onclick="draw();" class="btn btn-default">Cách viết</button>';
            //html += '                   <button onclick="showDrawing();" class="btn btn-default">Tập viết</button>';
            html += '                   </div>';
            html += '                   <div class="col-md-2 text-center">';
            html += '                   <label class="radio-inline"><input type="radio" name="rdoDraw" id="rdoKanji" checked="checked">Kanji</label>';
            html += '                   </div>';
            html += '                   <div class="col-md-12" id="draw"></div>';
        }
        else {
            html += '                   <div class="col-md-12">';
            html += '                   <h4><p class="text-info">' + voca.Hiragana + '</p></h4>';
            html += '                   <h4><p class="text-info">' + voca.Katakana + '</p></h4>';
            html += '                   <h4><p class="text-info">' + voca.Kanji + '</p></h4>';
            html += '                   <p class="text-default">' + voca.Romaji;
            html += '                   <span class="selected glyphicon glyphicon-volume-up" aria-hidden="true" onclick="speak(\'' + voca.UrlAudio + '\');"></span>';
            html += '                   </p>';
            html += '                   <p class="text-default">' + voca.VMeaning + '</p>';
            html += '                   <hr class="divider" />';
            html += '                   </div>';
            html += '                   <div class="col-md-4 text-center">';
            html += '                   <button onclick="draw();" class="btn btn-default">Cách viết</button>';
            //html += '                   <button onclick="showDrawing();" class="btn btn-default">Tập viết</button>';
            html += '                   </div>';
            var hiraChecked = (voca.DisplayType == "1") ? "checked = 'checked'" : "";
            var kataChecked = (voca.DisplayType == "2") ? "checked = 'checked'" : "";
            var kanjiChecked = (voca.DisplayType == "3") ? "checked = 'checked'" : "";
            html += '                   <div class="col-md-6 text-center">';
            html += '                   <label class="radio-inline"><input type="radio" name="rdoDraw" id="rdoHiragana" ' + hiraChecked + '">Hiragana</label>';
            html += '                   <label class="radio-inline"><input type="radio" name="rdoDraw" id="rdoKatakana" ' + kataChecked + '">Katakana</label>';
            html += '                   <label class="radio-inline"><input type="radio" name="rdoDraw" id="rdoKanji" "' + kanjiChecked + '">Kanji</label>';
            html += '                   </div>';
            html += '                   <div class="col-md-12" id="draw"></div>';
            //                html += '                   <canvas id="dline" height="100px"></canvas>';
            //            html += '                   <p class="text-info">Ví dụ:</p>';
            //            html += '                   <p class="text-info">' + voca.ExHiragana1 + '</p>';
            //            html += '                   <p class="text-default">' + voca.ExVMeaning1 + '</p>';
            //            html += '                   <p class="text-info">' + voca.ExHiragana2 + '</p>';
            //            html += '                   <p class="text-default">' + voca.ExVMeaning2 + '</p>';
            //            html += '                   <p class="text-info">' + voca.ExHiragana3 + '</p>';
            //            html += '                   <p class="text-default">' + voca.ExVMeaning3 + '</p>';
        }
        html += '               </div>';
        html += '           </div>';
        html += '       </div>';
        html += '       <a id="flashPre" class="left carousel-control" href="#carousel-example-generic" role="button" data-slide="prev">';
        html += '           <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>';
        html += '           <span class="sr-only">Previous</span>';
        html += '       </a>';
        html += '       <a id="flashNext" class="right carousel-control" href="#carousel-example-generic" role="button" data-slide="next">';
        html += '           <span class="glyphicon glyphicon-chevron-right" aria-hidden="true">';
        html += '           </span><span class="sr-only">Next</span>';
        html += '       </a>';
        html += '   </div>';
        html += '</div>';

        $('#flashCard').html(html);

        clearInterval(timer);

        if (voice) {
            speak(voca.UrlAudio);
        }

        $('.carousel').carousel({
            interval: 0
        })
        //            var container = document.getElementById('canvas');
        //            init(container, 200, 200, '#ddd');
    }
};

function searchVoca(index) {
    for (var i = 0; i < vocas.length; i++) {
        if (i == index) {
            return vocas[i];
        }
    }
};