

var totalVoca = 0;
var numOfHasLearnt = 0;
var defaultVocas = [];
var vocas = [];
var selectedArray = [];
var currentIndex = 0;
$(document).ready(function () {
    getVocas();
    //$('#dline').sketch();
    $("#modalDraw").draggable({
        handle: ".modal-header"
    });
    //        defaultVocas = Html.Raw(Json.Encode(Model));
    //        vocas = defaultVocas;
    $('#btnOkChoosingWordTypeModal').on('click', function () {

        if ($('#rdoSelect').is(':checked')) {
            $('#modalChoosingWord').modal();
        }
        else if ($('#rdoHasLearnt').is(':checked')) {
            getVocas(1);
            //if (vocas.length <= 0) {
            //    alert('Không có từ đã thuộc');
            //    return false;
            //}
            //currentIndex = 0;
            //showFlashCard(currentIndex, true);
        }
        else if ($('#rdoHasNotLearnt').is(':checked')) {
            getVocas(0);
            //if (vocas.length <= 0) {
            //    alert('Không có từ chưa thuộc');
            //    return false;
            //}
            //currentIndex = 0;
            //showFlashCard(currentIndex, true);
        }
        else if ($('#rdoWeakVoca').is(':checked')) {
            getVocas(2);
        }
        $('#modalChoosingWordType').modal('hide');
    });


    $('#modalChoosingWord').on('show.bs.modal', function (e) {
        var id = $('#vcd').val();
        $('#choosingWords').load('/Library/' + $('#gcv').val() + '/' + id);
    })

    $('#btnOkChoosingWordModal').on('click', function (e) {
        $('#modalChoosingWord').modal('hide');
        setChosenWordList();

        currentIndex = 0;
        showFlashCard(currentIndex, true);
    })

    var ss = $('#ss').val();
    if (ss == null || ss == "") {
        $('#btnAdd').prop('disabled', true);
        $('#btnCheck').addClass('disabled');
    } else {
        $('#btnAdd').prop('disabled', false);
        $('#btnCheck').removeClass('disabled');
    }

    //currentIndex = 0;
    //showFlashCard(currentIndex, true);
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
            //ctrl-d (draw)
            else if (keycode == 68) {
                draw();
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
    if (vocas[currentIndex].HasMarked == "1") {
        alert('Từ vựng này đã có trong Sổ tay');
        return;
    }
    vocas[currentIndex].HasMarked = "1";
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
                    vocas[currentIndex].HasMarked = "0";
                    window.location.href = '/Account/RequireLogin';
                }
                else if (result.ReturnCode != 0) {
                    vocas[currentIndex].HasMarked = "0";
                    alert('Có lỗi xảy ra');
                }
                else {
                    alert('Đã thêm vào Sổ tay của bạn');
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
function getVocas(type) {
    vocas = [];
    failArray = [];

    var id = $('#vcd').val();
    var accessDenied = $('#accessDenied').val();
    $.ajax({
        cache: true,
        type: "get",
        async: true,
        url: ('/Library/' + $('#gv').val()),
        data: { "id": id, "type": type },
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
                if (type == 1) {
                    if (vocas.length <= 0) {
                        alert('Không có từ đã thuộc');
                        hasVoca = false;
                    }
                }
                else if (type == 0) {
                    if (vocas.length <= 0) {
                        alert('Không có từ chưa thuộc');
                        hasVoca = false;
                    }
                }
                else if (vocas.length <= 0) {
                    alert('Không tìm thấy từ vựng');
                    hasVoca = false;
                }
                if (hasVoca) {
                    currentIndex = 0;
                    showFlashCard(currentIndex, true);
                }

                //var progressHtml = '<div class="progress-bar" role="progressbar" aria-valuenow="' + numOfHasLearnt
                //    + '" aria-valuemin="0" '
                //    + '" aria-valuemax="' + (totalVoca == 0 ? vocas.length : totalVoca)
                //    + '" style="width: ' + (numOfHasLearnt / (totalVoca == 0 ? vocas.length : totalVoca) * 100) + '%; min-width: 2em;">'
                //    + numOfHasLearnt + '/' + (totalVoca == 0 ? vocas.length : totalVoca)
                //    + '</div>';
                //var progressHtml = '<div class="progress-bar" role="progressbar" aria-valuenow="' + 1
                //    + '" aria-valuemin="0" '
                //    + '" aria-valuemax="' + (vocas.length)
                //    + '" style="width: ' + (1 / (vocas.length) * 100) + '%; min-width: 2em;">'
                //    + 1 + '/' + (vocas.length)
                //    + '</div>';
                //$('#progress').html(progressHtml);

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

function setChosenWordList() {
    vocas = [];
    $("#choosingWords :checkbox:checked").each(function (j, obj) {
        for (var i = 0; i < defaultVocas.length; i++) {
            if ($(obj).val() == defaultVocas[i].ID) {
                vocas.push(defaultVocas[i]);
                break;
            }
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
        if (voca.DisplayType == '3') {
            html += '                   <p class="text-info text-center" style="font-size: 150px;">' + voca.Kanji + '</p>';
        }
        else {
            html += '                   <img class="img-responsive" src="' + getLink(voca.UrlImage) + '" alt="Từ vựng tiếng Nhật" style="height: 300px" />';
        }
        html += '               </div>';
        html += '               <div class="col-lg-8 col-md-8 col-xs-12">';
        html += '                   <p class="text-info">Định nghĩa</p>';
        html += '                   <p class="text-default">' + voca.Description + '</p>';
        if (voca.DisplayType == '3') {
            html += '                   <p class="text-default">' + voca.Remembering + '</p>';
        }
        html += '               </div>';
        html += '           </div>';
        html += '       </div>';
        html += '       <div class="item">';
        //html += '           <div class="row">';
        //kanji
        if (voca.DisplayType == '3') {
            html += '           <div class="row">';
            html += '               <div class="col-md-2 col-xs-2">';
            html += '               </div>';
            html += '               <div class="col-md-3 col-xs-3">';
            html += '                   <h3><p class="text-info" style="font-size: 50px;">' + voca.Kanji + '</p><span class="selected glyphicon glyphicon-volume-up" aria-hidden="true" onclick="speak(\'' + voca.UrlAudio + '\');"></span></h3>';
            html += '               </div>';
            html += '               <div class="col-md-7 col-xs-7">';
            html += '                   <p class="text-default"><strong>Hán Việt</strong>: ' + voca.Pinyin + '</p>';
            html += '                   <p class="text-default"><strong>Nghĩa</strong>: ' + voca.VMeaning + '</p>';
            html += '                   <p class="text-default"><strong>Cách nhớ</strong>: ' + voca.Remembering + '</p>';
            html += '               </div>';
            html += '           </div>';

            html += '           <div class="row">';
            html += '               <div class="col-md-2 hidden-xs">';
            html += '               </div>';
            html += '               <div class="col-md-3 col-xs-3">';
            html += '                   <div class="row">';
            html += '                       <div class="col-md-12">';
            html += '                           <p class="text-default"><strong>On</strong>:</p>';
            if (voca.OnReading != '') {
                html += '                       <p class="text-default">' + voca.OnReading + '</p>';
            }
            if (voca.OnReading2 != '') {
                html += '                       <p class="text-default">' + voca.OnReading2 + '</p>';
            }
            html += '                           <p class="text-default"><strong>Kun</strong>:</p>';
            if (voca.KunReading != '') {
                html += '                       <p class="text-default">' + voca.KunReading + '</p>';
            }
            if (voca.KunReading2 != '') {
                html += '                       <p class="text-default">' + voca.KunReading2 + '</p>';
            }
            html += '                       </div>';
            html += '                   </div>';
            html += '               </div>';
            html += '               <div class="col-md-7 col-xs-9">';
            html += '                   <div class="row">';
            html += '                       <div class="col-md-2 col-xs-3">';
            html += '                           <p class="text-default"><a href="#" class="drawKanji" data-toggle="tooltip" title="Nhấp để xem cách viết" onclick="drawKanji(event, this);">' + voca.ExKanji1 + '</a></p>';
            html += '                       </div>';
            html += '                       <div class="col-md-2 col-xs-3">';
            html += '                           <p class="text-default">' + voca.ExReading1 + '</p>';
            html += '                       </div>';
            html += '                       <div class="col-md-3 col-xs-3">';
            html += '                           <p class="text-default">' + voca.ExVMeaning1 + '</p>';
            html += '                       </div>';
            html += '                   </div>';
            html += '                   <div class="row">';
            html += '                       <div class="col-md-2 col-xs-3">';
            html += '                           <p class="text-default"><a href="#" class="drawKanji" data-toggle="tooltip" title="Nhấp để xem cách viết" onclick="drawKanji(event, this);">' + voca.ExKanji2 + '</a></p>';
            html += '                       </div>';
            html += '                       <div class="col-md-2 col-xs-3">';
            html += '                           <p class="text-default">' + voca.ExReading2 + '</p>';
            html += '                       </div>';
            html += '                       <div class="col-md-3 col-xs-3">';
            html += '                           <p class="text-default">' + voca.ExVMeaning2 + '</p>';
            html += '                       </div>';
            html += '                   </div>';
            html += '                   <div class="row">';
            html += '                       <div class="col-md-2 col-xs-3">';
            html += '                           <p class="text-default"><a href="#" class="drawKanji" data-toggle="tooltip" title="Nhấp để xem cách viết" onclick="drawKanji(event, this);">' + voca.ExKanji3 + '</a></p>';
            html += '                       </div>';
            html += '                       <div class="col-md-2 col-xs-3">';
            html += '                           <p class="text-default">' + voca.ExReading3 + '</p>';
            html += '                       </div>';
            html += '                       <div class="col-md-3 col-xs-3">';
            html += '                           <p class="text-default">' + voca.ExVMeaning3 + '</p>';
            html += '                       </div>';
            html += '                   </div>';
            html += '               </div>';
            html += '           </div>';
            
            html += '           <div class="row">';
            html += '               <div class="col-md-2 col-xs-2">';
            html += '               </div>';
            html += '               <div class="col-md-2 col-xs-10">';
            html += '                   <button onclick="draw();" class="btn btn-default" data-toggle="tooltip" title="Nhấp để xem cách viết (Ctrl-D)">Cách viết</button>';
            html += '               </div>';
            html += '               <div class="col-md-6">';
            html += '                   <div class="col-md-10 col-xs-10" id="draw"></div>';
            html += '               </div>';
            html += '               <div class="col-md-1 hidden">';
            html += '                   <label class="radio-inline"><input type="radio" name="rdoDraw" id="rdoKanji" checked="checked">Kanji</label>';
            html += '               </div>';
            html += '           </div>';
            //html += '           <div class="row">';
            //html += '               <div class="col-md-2 col-xs-2">';
            //html += '               </div>';
            //html += '               <div class="col-md-10 col-xs-10" id="draw"></div>';
            //html += '           </div>';
        }
        else {
            html += '           <div class="row">';
            html += '               <div class="col-lg-4 col-md-4 hidden-xs hidden-sm">';
            html += '                   <img class="img-responsive" src="' + getLink(voca.UrlImage) + '" alt="Từ vựng tiếng Nhật" style="height: 300px" />';
            html += '               </div>';
            html += '               <div class="col-lg-8 col-md-8">';
            html += '                   <div class="row">';
            html += '                       <div class="col-xs-2">';
            html += '                       </div>';
            html += '                       <div class="col-md-12 col-xs-10">';
            html += '                           <h4><a href="#" onclick="speak(\'' + voca.UrlAudio + '\'); return false;" data-toggle="tooltip" title="Nhấp để nghe đọc"><p class="text-info zoom-content-learning">' + voca.Hiragana + '</p></a></h4>';
            if (voca.Hiragana != '') {
                html += '                       <p class="text-default">' + voca.Romaji;
            }
            html += '                           <h4><a href="#" onclick="speak(\'' + voca.UrlAudio_Katakana + '\'); return false;" data-toggle="tooltip" title="Nhấp để nghe đọc"><p class="text-info zoom-content-learning">' + voca.Katakana + '</p></a></h4>';
            if (voca.Katakana != '') {
                html += '                       <p class="text-default">' + voca.Romaji_Katakana;
            }
            html += '                           <h4><a href="#" onclick="speak(\'' + voca.UrlAudio + '\'); return false;" data-toggle="tooltip" title="Nhấp để nghe đọc"><p class="text-info zoom-content-learning">' + voca.Kanji + '</p></a></h4>';

            //    //html += '                   <span class="selected glyphicon glyphicon-volume-up" aria-hidden="true" onclick="speak(\'' + voca.UrlAudio + '\');"></span>';
            //html += '                   </p>';
            html += '                           <p class="text-default">' + voca.VMeaning + '</p>';
            html += '                       </div>';
            html += '                   </div>';
            html += '                   <hr class="divider" />';
            html += '                   <div class="row">';
            html += '                       <div class="col-xs-2">';
            html += '                       </div>';
            html += '                       <div class="col-md-12 col-xs-10">';
            html += '                           <button onclick="draw();" class="btn btn-default" data-toggle="tooltip" title="Nhấp để xem cách viết (Ctrl-D)">Cách viết</button>';
            var hiraChecked = (voca.DisplayType == "1") ? "checked = 'checked'" : "";
            var kataChecked = (voca.DisplayType == "2") ? "checked = 'checked'" : "";
            var kanjiChecked = (voca.DisplayType == "3") ? "checked = 'checked'" : "";
            html += '                           <label class="radio-inline"><input type="radio" name="rdoDraw" id="rdoHiragana" ' + hiraChecked + '">Hiragana</label>';
            html += '                           <label class="radio-inline"><input type="radio" name="rdoDraw" id="rdoKatakana" ' + kataChecked + '">Katakana</label>';
            html += '                           <label class="radio-inline"><input type="radio" name="rdoDraw" id="rdoKanji" "' + kanjiChecked + '">Kanji</label>';
            html += '                       </div>';
            html += '                   </div>';
            html += '                   <div class="row">';
            html += '                       <div class="col-md-12" id="draw"></div>';
            html += '                   </div>';
            html += '               </div>';
            html += '           </div>';

            //html += '           <div class="row">';
            //html += '               <div class="col-md-12">';
            
            
            //html += '                   <hr class="divider" />';
            //html += '                   </div>';
            //html += '                   <div class="col-md-4 text-center">';
            //html += '                   <button onclick="draw();" class="btn btn-default">Cách viết</button>';
            ////html += '                   <button onclick="showDrawing();" class="btn btn-default">Tập viết</button>';
            //html += '                   </div>';
            //var hiraChecked = (voca.DisplayType == "1") ? "checked = 'checked'" : "";
            //var kataChecked = (voca.DisplayType == "2") ? "checked = 'checked'" : "";
            //var kanjiChecked = (voca.DisplayType == "3") ? "checked = 'checked'" : "";
            //html += '                   <div class="col-md-6 text-center">';
            //html += '                   <label class="radio-inline"><input type="radio" name="rdoDraw" id="rdoHiragana" ' + hiraChecked + '">Hiragana</label>';
            //html += '                   <label class="radio-inline"><input type="radio" name="rdoDraw" id="rdoKatakana" ' + kataChecked + '">Katakana</label>';
            //html += '                   <label class="radio-inline"><input type="radio" name="rdoDraw" id="rdoKanji" "' + kanjiChecked + '">Kanji</label>';
            //html += '                   </div>';
            //html += '                   <div class="col-md-12" id="draw"></div>';
            //}
            //html += '               </div>';
            //html += '           </div>';
        }
        //html += '           </div>';
        //html += '           <div class="row">';
        //html += '               <div class="col-lg-4 col-md-4 hidden-xs hidden-sm">';
        //html += '                   <img class="img-responsive" src="' + getLink(voca.UrlImage) + '" alt="Từ vựng tiếng Nhật" style="height: 300px" />';
        //html += '               </div>';
        //html += '               <div class="col-lg-8 col-md-8">';
        ////kanji
        //if (voca.DisplayType == "3") {
        //    html += '                   <div class="row">';
        //    html += '                       <div class="col-md-12">';
        //    html += '                       <div class="col-md-4 col-sm-12 col-xs-12">';
        //    html += '                           <h2><p class="text-info">' + voca.Kanji + '</p><span class="selected glyphicon glyphicon-volume-up" aria-hidden="true" onclick="speak(\'' + voca.UrlAudio + '\');"></span></h2>';
        //    html += '                           <p class="text-default"><strong>Nghĩa</strong>: ' + voca.VMeaning + '</p>';
        //    html += '                           <p class="text-default"><strong>Hán Việt</strong>: ' + voca.Pinyin + '</p>';
        //    html += '                       </div>';

        //    html += '                       <div class="col-md-4 col-sm-6 col-xs-6">';
        //    html += '                           <p class="text-info">On-yomi: </p>';
        //    if (voca.OnReading != '') {
        //        html += '                       <p class="text-default">' + voca.OnReading + ' ' + voca.OnRomaji;
        //        html += '                       </p>';
        //    }
        //    if (voca.OnReading2 != '') {
        //        html += '                       <p class="text-default">' + voca.OnReading2 + ' ' + voca.OnRomaji2;
        //        html += '                       </p>';
        //    }
        //    html += '                       </div>';

        //    html += '                       <div class="col-md-4 col-sm-6 col-xs-6">';
        //    html += '                           <p class="text-info">Kun-yomi: </p>';
        //    if (voca.KunReading != '') {
        //        html += '                       <p class="text-default">' + voca.KunReading + ' ' + voca.KunRomaji;
        //        html += '                       </p>';
        //    }
        //    if (voca.KunReading2 != '') {
        //        html += '                       <p class="text-default">' + voca.KunReading2 + ' ' + voca.KunRomaji2;
        //        html += '                       </p>';
        //    }
        //    html += '                       </div>';
        //    html += '                   </div>';
        //    html += '                   </div>';

        //    html += '                   <hr class="divider" />';

        //    html += '                   <div class="col-md-4 text-center">';
        //    html += '                   <button onclick="draw();" class="btn btn-default">Cách viết</button>';
        //    //html += '                   <button onclick="showDrawing();" class="btn btn-default">Tập viết</button>';
        //    html += '                   </div>';
        //    html += '                   <div class="col-md-2 text-center">';
        //    html += '                   <label class="radio-inline"><input type="radio" name="rdoDraw" id="rdoKanji" checked="checked">Kanji</label>';
        //    html += '                   </div>';
        //    html += '                   <div class="col-md-12" id="draw"></div>';
        //}
        //else {
        //    html += '                   <div class="col-md-12">';
        //    html += '                   <h4><a href="#" onclick="speak(\'' + voca.UrlAudio + '\'); return false;" data-toggle="tooltip" title="Nhấp để nghe đọc"><p class="text-info zoom-content-learning">' + voca.Hiragana + '</p></a></h4>';
        //    if (voca.Hiragana != '') {
        //        html += '                   <p class="text-default">' + voca.Romaji;
        //    }
        //    html += '                   <h4><a href="#" onclick="speak(\'' + voca.UrlAudio_Katakana + '\'); return false;" data-toggle="tooltip" title="Nhấp để nghe đọc"><p class="text-info zoom-content-learning">' + voca.Katakana + '</p></a></h4>';
        //    if (voca.Katakana != '') {
        //        html += '                   <p class="text-default">' + voca.Romaji_Katakana;
        //    }
        //    html += '                   <h4><a href="#" onclick="speak(\'' + voca.UrlAudio + '\'); return false;" data-toggle="tooltip" title="Nhấp để nghe đọc"><p class="text-info zoom-content-learning">' + voca.Kanji + '</p></a></h4>';
            
        //    //html += '                   <span class="selected glyphicon glyphicon-volume-up" aria-hidden="true" onclick="speak(\'' + voca.UrlAudio + '\');"></span>';
        //    html += '                   </p>';
        //    html += '                   <p class="text-default">' + voca.VMeaning + '</p>';
        //    html += '                   <hr class="divider" />';
        //    html += '                   </div>';
        //    html += '                   <div class="col-md-4 text-center">';
        //    html += '                   <button onclick="draw();" class="btn btn-default">Cách viết</button>';
        //    //html += '                   <button onclick="showDrawing();" class="btn btn-default">Tập viết</button>';
        //    html += '                   </div>';
        //    var hiraChecked = (voca.DisplayType == "1") ? "checked = 'checked'" : "";
        //    var kataChecked = (voca.DisplayType == "2") ? "checked = 'checked'" : "";
        //    var kanjiChecked = (voca.DisplayType == "3") ? "checked = 'checked'" : "";
        //    html += '                   <div class="col-md-6 text-center">';
        //    html += '                   <label class="radio-inline"><input type="radio" name="rdoDraw" id="rdoHiragana" ' + hiraChecked + '">Hiragana</label>';
        //    html += '                   <label class="radio-inline"><input type="radio" name="rdoDraw" id="rdoKatakana" ' + kataChecked + '">Katakana</label>';
        //    html += '                   <label class="radio-inline"><input type="radio" name="rdoDraw" id="rdoKanji" "' + kanjiChecked + '">Kanji</label>';
        //    html += '                   </div>';
        //    html += '                   <div class="col-md-12" id="draw"></div>';
        //    //                html += '                   <canvas id="dline" height="100px"></canvas>';
        //    //            html += '                   <p class="text-info">Ví dụ:</p>';
        //    //            html += '                   <p class="text-info">' + voca.ExHiragana1 + '</p>';
        //    //            html += '                   <p class="text-default">' + voca.ExVMeaning1 + '</p>';
        //    //            html += '                   <p class="text-info">' + voca.ExHiragana2 + '</p>';
        //    //            html += '                   <p class="text-default">' + voca.ExVMeaning2 + '</p>';
        //    //            html += '                   <p class="text-info">' + voca.ExHiragana3 + '</p>';
        //    //            html += '                   <p class="text-default">' + voca.ExVMeaning3 + '</p>';
        //}
        //html += '               </div>';
        //html += '           </div>';
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

        var progressHtml = '<div class="progress-bar" role="progressbar" aria-valuenow="' + (index + 1)
                    + '" aria-valuemin="0" '
                    + '" aria-valuemax="' + (vocas.length)
                    + '" style="width: ' + ((index + 1) / (vocas.length) * 100) + '%; min-width: 2em;">'
                    + (index + 1) + '/' + (vocas.length)
                    + '</div>';
        $('#progress').html(progressHtml);
    }
};

function searchVoca(index) {
    for (var i = 0; i < vocas.length; i++) {
        if (i == index) {
            return vocas[i];
        }
    }
};

function drawKanji(e, obj) {
    e.preventDefault();

    $('#txtKanji').val($(obj).text());
    dr($(obj).text());
    $('#drawingModal').modal();
}