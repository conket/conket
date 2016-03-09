

var timePerVoca = 15;
var completedTime = 0;
var correctVocas = [];
var vocas = [];
var currentIndex = 0;
$(document).ready(function () {
    //load datas
    getFastTestVocas();

    completedTime = 0;
    currentIndex = 0;
    if (vocas.length > 0) {
        showFlashCard(currentIndex, false);
    }
    else {
        //no content
        showFlashCard(-2, false);
    }

    $('#btnNext').on('click', function () {
        //input romaji
        if (vocas[currentIndex].TestType == "2") {
            var inputValue = $('#inputAlphabet').val().toLowerCase().replace(/\s+/g, '');
            if (inputValue == '') {
                alert('Vui lòng nhập giá trị!');
                $('#inputAlphabet').val('');
                $('#inputAlphabet').focus();
                return false;
            }
        }
            //choosing
        else if (vocas[currentIndex].TestType == "1") {
            var selectedValue = $('#selectedValue').val().toLowerCase().replace(/\s+/g, '');
            if (selectedValue == '') {
                alert('Vui lòng chọn 1 giá trị!');
                return false;
            }
        }
        checkInput();
    });

    $(document).keydown(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);

        //enter
        if (keycode == 13) {
            if (currentIndex >= 0 && currentIndex < vocas.length) {
                $("#btnNext").trigger("click");
            }
            return false;
        }
        //space
        if (keycode == 32) {
            if (currentIndex >= 0 && currentIndex < vocas.length) {
                if (vocas[currentIndex].TestSkill == '1') {
                    speak(vocas[currentIndex].UrlAudio);
                }
            }
            return false;
        }
    });


});

var timer;
var interval = 1500;

function updateFastTestVoca() {
    $.ajax({
        cache: false,
        type: "post",
        async: true,
        url: '@Href("~/Library/UpdateFastTestVoca")',
        data: JSON.stringify(vocas[currentIndex]),
        dataType: "json",
        contentType: 'application/json',
        success: function (result) {
            if (result.ReturnCode != 0) {
                alert('Có lỗi khi update db');
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.responseText);
            return false;
        }
    });
}


function getFastTestVocas() {
    vocas = [];
    failArray = [];

    $.ajax({
        cache: true,
        type: "get",
        async: false,
        url: '@Href("~/Library/GetFastTestVocas")',
        data: { "id": '@ViewBag.CategoryID' },
        dataType: "json",
        success: function (result) {
            if (result.returnCode == '@Ivs.Core.Common.CommonData.DbReturnCode.AccessDenied') {
                window.location.href = '@Href("~/Home/Index")';
            } else {
                $.each(result.vocabularies, function (i, voca) {
                    vocas.push(voca);
                    failArray.push(voca);
                });
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.responseText);
            return false;
        }
    });
}

function getLink(url) {
    var baseSiteURL = '@Url.Content("~/")';
    return url;
};

var messageAudio;
function speak(url) {
    if (messageAudio != null) {
        messageAudio.pause();
    }
    var baseSiteURL = '@Url.Content("~/")';
    messageAudio = new Audio(url);
    messageAudio.play();
};

function checkInput() {
    completedTime += (timePerVoca - parseInt($('#timer').html()));
    if (currentIndex < vocas.length) {
        //input romaji
        if (vocas[currentIndex].TestType == "2") {
            var inputValue = $('#inputAlphabet').val().toLowerCase().replace(/\s+/g, '');
            var checkValueRomaji = vocas[currentIndex].Romaji.toLowerCase().replace(/\s+/g, '');
            var checkValueHira = vocas[currentIndex].Hiragana.toLowerCase().replace(/\s+/g, '');
            var checkValueKata = vocas[currentIndex].Katakana.toLowerCase().replace(/\s+/g, '');
            var checkValueKanji = vocas[currentIndex].Kanji.toLowerCase().replace(/\s+/g, '');

            vocas[currentIndex].SelectedValue = inputValue;
            vocas[currentIndex].IsCorrect = "1";

            //show error if wrong
            if (inputValue != checkValueRomaji
                && inputValue != checkValueHira
                && inputValue != checkValueKata
                && inputValue != checkValueKanji
                ) {

                vocas[currentIndex].IsCorrect = "0";

                //speak
                speak('@Href("~/Content/media/fail.wav")');

                $('#result').html();
            }
            else {
                //speak corrent voca
                speak('@Href("~/Content/media/tada.wav")');

                //add to correct list
                correctVocas.push(vocas[currentIndex]);
            }
        }
            //choosing
        else if (vocas[currentIndex].TestType == "1") {
            var selectedValue = $('#selectedValue').val().toLowerCase().replace(/\s+/g, '');
            var resultValue = $('#correctValue').val().toLowerCase().replace(/\s+/g, ''); //vocas[index].Hiragana.toLowerCase().replace(/\s+/g, '');

            vocas[currentIndex].SelectedValue = selectedValue;
            vocas[currentIndex].IsCorrect = "1";

            //show error if wrong
            if (selectedValue != resultValue) {
                //speak
                speak('@Href("~/Content/media/fail.wav")');

                vocas[currentIndex].IsCorrect = "0";
                //                    speak($('#correctUrlAudio').val());
            }
            else {
                //speak corrent voca
                speak('@Href("~/Content/media/tada.wav")');

                //add to correct list
                correctVocas.push(vocas[currentIndex]);
            }
        }

        //update db
        updateFastTestVoca();
    }

    //next
    clearInterval(timer);
    if (currentIndex < vocas.length - 1) {
        timer = setInterval(function () {
            currentIndex++;
            showFlashCard(currentIndex, false);
        }, interval);
    }
    else {
        //show result
        currentIndex = -1;
        showFlashCard(-1, false);
    }

    $('#inputAlphabet').attr('disabled', 'disabled');
    document.getElementById("test-content").style.cursor = "not-allowed";
    $("#item-active").html('');
}

function showFlashCard(index, voice) {

    $('#inputAlphabet').removeAttr('disabled');
    document.getElementById("test-content").style.cursor = "auto";
    clearInterval(timer);

    if (index == -2) {
        //result
        var html = '';
        html += '<div id="carousel-example-generic" class="carousel slide" data-ride="carousel">';
        html += '   <div class="carousel-inner" role="listbox">';
        html += '       <div class="item active">';
        html += '           <div class="row text-center">';
        //            html += '               <div class="col-lg-4 col-md-4 col-xs-6">';
        //            html += '                   <img class="img-rounded" src="' + getLink(voca.UrlImage) + '" alt="Từ vựng tiếng Nhật" height="300px" width="100%"/>';
        //            html += '               </div>';
        html += '               <div class="col-lg-12">';
        html += '                   <p class="text-info">Bạn đã thuộc các từ vựng trong bài này.</p>';
        html += '                   <p class="text-info">Hãy hoàn thành bài kiểm tra và tiếp tục các bài học tiếp theo</p>';
        html += '               </div>';
        html += '           </div>';
        html += '           <div class="row text-center">';
        html += '               <div class="col-lg-12">';
        html += '                   <button type="button" class="btn btn-default btn-lg" onclick="window.history.back();">Trở về</button></p>';
        html += '               </div>';
        html += '           </div>';
        html += '       </div>';
        html += '   </div>';
        html += '</div>';

        $('#flashCard').html(html);
        $('#btnNext').hide();

    }
    else if (index == -1) {

        var urlVoca = '@Url.Action("danh-muc", "Library", new { id = @ViewBag.CategoryID, urlDisplay = @ViewBag.CategoryUrlDisplay})';

        //result
        var html = '';
        html += '<div id="carousel-example-generic" class="carousel slide" data-ride="carousel">';
        html += '   <div class="carousel-inner" role="listbox">';
        html += '       <div class="item active">';
        html += '           <div class="row text-center">';
        //            html += '               <div class="col-lg-4 col-md-4 col-xs-6">';
        //            html += '                   <img class="img-rounded" src="' + getLink(voca.UrlImage) + '" alt="Từ vựng tiếng Nhật" height="300px" width="100%"/>';
        //            html += '               </div>';
        html += '               <div class="col-lg-12">';
        html += '                   <p class="text-info">Số câu đúng: ' + correctVocas.length + '/' + vocas.length + '</p>';
        html += '                   <p class="text-info">Quá trình sàng lọc từ vựng hoàn tất</p>';

        html += '               </div>';
        html += '           </div>';
        html += '           <div class="row text-center">';
        html += '               <div class="col-lg-12">';
        if (correctVocas.length < vocas.length * 8 / 10) {
            html += '                   <a href="#" role="button" class="btn btn-navigator btn-lg" onclick="showResult(0); return false;">Xem kết quả</a>';
            html += '                   <a href="' + urlVoca + '" role="button" class="btn btn-navigator btn-lg" >Học bài</a>';
        }
        else {
            html += '                   <a href="#" role="button" class="btn btn-navigator btn-lg" onclick="showResult(0); return false;">Xem kết quả</a>';
            html += '                   <a href="' + urlVoca + '" class="btn btn-navigator btn-lg" role="button" >Trở về</a>';
        }
        html += '               </div>';
        html += '           </div>';
        html += '       </div>';
        html += '   </div>';
        html += '</div>';

        $('#flashCard').html(html);
        $('#btnNext').hide();

    } else {

        var voca = searchVoca(index);
        if (voca != null) {
            var html = '';
            html += '<div id="carousel-example-generic" class="carousel slide" data-ride="carousel">';
            html += '   <div class="carousel-inner" role="listbox">';
            html += '       <div class="item active">';
            html += '           <div class="row">';
            //                html += '               <div class="col-lg-3">';
            //                html += '                   <p class="text-info">Thời gian: <strong><span id="timer">15</span></strong></p>';
            //                html += '               </div>';
            //                html += '               <div class="col-lg-4">';
            //                html += '                   <p class="text-info"><strong>ĐÁP ÁN: <span id="result"></span></strong></p>';
            //                html += '               </div>';
            html += '               <div class="col-lg-3">';
            html += '                   <p class="text-info">Câu: <strong><span id="currentVoca">' + (currentIndex + 1) + '/' + vocas.length + '</span></strong></p>';
            html += '               </div>';
            html += '               <div class="col-lg-3">';
            html += '                   <p class="text-info">Câu đúng: <strong><span id="correctVoca">' + (correctVocas.length) + '/' + vocas.length + '</span></strong></p>';
            html += '               </div>';
            html += '               <div class="col-lg-3">';
            html += '                   <p class="text-info">Câu sai: <strong><span id="correctVoca">' + (currentIndex - correctVocas.length) + '/' + vocas.length + '</span></strong></p>';
            html += '               </div>';
            html += '           </div>';
            html += '           <div class="row" id="item-active">';

            html += '               <div class="col-lg-4 col-md-4 col-xs-6  text-center">';
            if (voca.TestSkill != '1') {
                html += '                   <img class="img-rounded" src="' + getLink(voca.UrlImage) + '" alt="Từ vựng tiếng Nhật" height="300px" width="100%"/>';
                html += '<label>' + voca.VMeaning + '</label>';
            }
            else {
                html += '                   <button type="button" class="btn btn-default btn-lg"><span class="glyphicon glyphicon-volume-up" aria-hidden="true">Nghe</span></button>';
            }
            html += '               </div>';
            html += '               <div class="col-lg-8 col-md-8 col-xs-6">';

            //choosing
            if (voca.TestType == "1") {
                html += '<label>Chọn từ đúng</label>';
                html += '<input type="hidden" id="correctValue" value="' + voca.CorrectResult + '"/>';
                html += '<input type="hidden" id="correctUrlAudio" value="' + voca.CorrectUrlAudio + '"/>';
                html += '<input type="hidden" id="selectedValue" />';
                html += '<div class="list-group">';
                html += '   <a href="#" id="result1" class="list-group-item" name="resultChoosing" onclick="selectValue(this, 1);return false;">' + voca.Result1 + '</a><input type="hidden" id="urlAudio1" />';
                html += '   <a href="#" id="result2" class="list-group-item" name="resultChoosing" onclick="selectValue(this, 2);return false;">' + voca.Result2 + '</a><input type="hidden" id="urlAudio2" />';
                html += '   <a href="#" id="result3" class="list-group-item" name="resultChoosing" onclick="selectValue(this, 3);return false;">' + voca.Result3 + '</a><input type="hidden" id="urlAudio3" />';
                html += '   <a href="#" id="result4" class="list-group-item" name="resultChoosing" onclick="selectValue(this, 4);return false;">' + voca.Result4 + '</a><input type="hidden" id="urlAudio4" />'
                html += '</div>';
            }
                //input romaji
            else if (voca.TestType == "2") {
                html += '<label>Gõ từ đúng</label>';
                html += '<input type="text" class="form-control" id="inputAlphabet" maxlength="30">';
            }
            html += '               </div>';
            html += '           </div>';
            html += '       </div>';
            html += '   </div>';
            html += '</div>';

            $('#flashCard').html(html);

            if (voca.TestType == "2") {
                $('#inputAlphabet').val('');
                $('#inputAlphabet').focus();

                $('#btnNext').show();
            }
            else if (voca.TestType == "1") {
                $('#btnNext').hide();
            }

            //listening skill
            if (voca.TestSkill == '1') {
                speak(voca.UrlAudio);
            }
        }

        //            $("#timer").setTimer(function (i, count) {
        //                $(this).html(count);
        //                //alert(completedTime + ' += ' + (timePerVoca - count));
        //                //completedTime += (timePerVoca - count);
        //            }, 1000, timePerVoca, true, function () {
        //                if ($("#timer").html() == "0") {
        //                    checkInput();
        //                }
        //            });
    }
};


function showResult(index) {
    var urlVoca = '@Url.Action("danh-muc", "Library", new { id = @ViewBag.CategoryID, urlDisplay = @ViewBag.CategoryUrlDisplay})';
    var voca = searchVoca(index);
    if (voca != null) {
        var html = '';
        html += '<div id="carousel-example-generic" class="carousel slide" data-ride="carousel">';
        html += '   <div class="carousel-inner" role="listbox">';
        html += '       <div class="item active">';
        html += '           <div class="row">';
        html += '               <div class="col-lg-3">';
        html += '                   <p class="text-info">Câu: <strong><span id="currentVoca">' + (index + 1) + '/' + vocas.length + '</span></strong></p>';
        html += '               </div>';
        html += '           </div>';
        html += '           <div class="row">';
        //            if (voca.TestSkill != '1') {
        html += '               <div class="col-lg-4 col-md-4 col-xs-6">';
        html += '                   <img class="img-rounded" src="' + getLink(voca.UrlImage) + '" alt="Từ vựng tiếng Nhật" height="300px" width="100%"/>';
        html += '               </div>';
        html += '               <div class="col-lg-8 col-md-8 col-xs-6">';
        //            }
        //            else {
        //                html += '               <div class="col-lg-12 col-md-12 col-xs-12">';
        //            }
        //choosing
        if (voca.TestType == "1") {
            html += '<label>Chọn từ đúng</label>';
            //                html += '<input type="hidden" id="correctValue" value="' + voca.CorrectResult + '"/>';
            //                html += '<input type="hidden" id="correctUrlAudio" value="' + voca.CorrectUrlAudio + '"/>';
            //                html += '<input type="hidden" id="selectedValue" />';
            html += '<div class="list-group">';
            html += '   <a href="#" id="result1" class="list-group-item" name="resultChoosing" onclick="return false;">' + voca.Result1 + '</a><span id="span1"></span>';
            html += '   <a href="#" id="result2" class="list-group-item" name="resultChoosing" onclick="return false;">' + voca.Result2 + '</a><span id="span2"></span>';
            html += '   <a href="#" id="result3" class="list-group-item" name="resultChoosing" onclick="return false;">' + voca.Result3 + '</a><span id="span3"></span>';
            html += '   <a href="#" id="result4" class="list-group-item" name="resultChoosing" onclick="return false;">' + voca.Result4 + '</a><span id="span4"></span>';
            html += '</div>';
        }
            //input romaji
        else if (voca.TestType == "2") {
            html += '<div class="alert alert-primary" role="alert"><span class="glyphicon glyphicon-ok" aria-hidden="true"></span><span class="sr-only">Error:</span> Từ vựng: ' + voca.Hiragana + '&nbsp;&nbsp;' + voca.Katakana + '&nbsp;&nbsp;' + voca.Kanji + '&nbsp;&nbsp;' + voca.Romaji + '</div>';
            html += '<div id="divInputResult" class="" role="alert"><span id="spanInputResult" class="" aria-hidden="true"></span><span class="sr-only">Error:</span> Từ của bạn: ' + voca.SelectedValue + '</div>';
            //                html += '<h4><span class="label label-primary">Từ đúng: ' + voca.Hiragana + '  ' + voca.Katakana + '  ' + voca.Kanji + '  ' + voca.Romaji + '</span><h4>';
            //html += '<h4><span class="label label-danger">Từ của bạn: ' + voca.SelectedValue + '</span></h4>';
        }
        html += '               </div>';
        html += '           </div>';
        html += '           <div class="row text-center">';
        html += '               <div class="col-lg-12">';
        html += '                   <a href="#" role="button" class="btn btn-navigator btn-lg" onclick="showResult(' + (index - 1) + '); return false;">Trước</a>';
        html += '                   <a href="#" role="button" class="btn btn-navigator btn-lg" onclick="showResult(' + (index + 1) + '); return false;">Tiếp</a>';
        html += '                   <a href="' + urlVoca + '" role="button" class="btn btn-navigator btn-lg" >Học bài</a>';
        html += '               </div>';
        html += '           </div>';
        html += '       </div>';
        html += '   </div>';
        html += '</div>';

        $('#flashCard').html(html);

        if (voca.TestType == "1") {
            switch (voca.CorrectResult) {
                case 1:
                    $('#result1').addClass('list-group-item active');
                    $('#span1').addClass('glyphicon glyphicon-ok ok');
                    break;
                case 2:
                    $('#result2').addClass('list-group-item active');
                    $('#span2').addClass('glyphicon glyphicon-ok ok');
                    break;
                case 3:
                    $('#result3').addClass('list-group-item active');
                    $('#span3').addClass('glyphicon glyphicon-ok ok');
                    break;
                case 4:
                    $('#result4').addClass('list-group-item active');
                    $('#span4').addClass('glyphicon glyphicon-ok ok');
                    break;
                default:
            }
            if (voca.IsCorrect == "0") {
                switch (voca.SelectedValue) {
                    case "1":
                        $('#result1').addClass('list-group-item-danger');
                        $('#span1').addClass('glyphicon glyphicon-remove error');
                        break;
                    case "2":
                        $('#result2').addClass('list-group-item-danger');
                        $('#span2').addClass('glyphicon glyphicon-remove error');
                        break;
                    case "3":
                        $('#result3').addClass('list-group-item-danger');
                        $('#span3').addClass('glyphicon glyphicon-remove error');
                        break;
                    case "4":
                        $('#result4').addClass('list-group-item-danger');
                        $('#span4').addClass('glyphicon glyphicon-remove error');

                        break;
                    default:
                }
            }
        }
        else if (voca.TestType == "2") {
            if (voca.IsCorrect == "0") {
                $('#divInputResult').addClass('alert alert-danger');
                $('#spanInputResult').addClass('glyphicon glyphicon-remove');
            }
            else {
                $('#divInputResult').addClass('alert alert-primary');
                $('#spanInputResult').addClass('glyphicon glyphicon-ok');
            }
        }
    }
};

function selectValue(obj, value) {
    $('#selectedValue').val(value);
    //        clearChoosing();
    //        $(obj).prop('class', 'list-group-item active');

    $('#btnNext').trigger('click');
};

function clearChoosing() {
    $('a[name=resultChoosing]').each(function () {
        $(this).prop('class', 'list-group-item');
    });
};

function searchVoca(index) {
    for (var i = 0; i < vocas.length; i++) {
        if (i == index) {
            return vocas[i];
        }
    }
};
