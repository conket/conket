
var timePerVoca = 15;
var completedTime = 0;

var inCorrectVocas = [];
var correctVocas = [];
var vocas = [];

var isPass = false;

//Mode Practice or Learning
var isPractice = true;

var currentIndex = 0;
$(document).ready(function () {

    window.onbeforeunload = function confirmExit() {
        //            return "Kết quả bài kiểm tra sẽ không được ghi nhận. Bạn có muốn thoát trang?";
        if (vocas.length > 0 && currentIndex >= 0) {
            return "Kết quả bài kiểm tra sẽ không được ghi nhận nếu bạn thoát khỏi trang";
        }
    }

    var f = new Audio('/Content/media/fail.wav');
    f.load();
    var o = new Audio('/Content/media/tada.wav');
    o.load();

    currentIndex = 0;
    isPractice = false;

    //load datas
    getTestVocas();

    $('#btnNext').on('click', function () {
        //input romaji
        //if (vocas[currentIndex].TestType == "2") {
        //    var inputValue = $('#inputAlphabet').val().toLowerCase().replace(/\s+/g, '');
        //    if (inputValue == '') {
        //        alert('Vui lòng nhập giá trị!');
        //        $('#inputAlphabet').val('');
        //        $('#inputAlphabet').focus();
        //        return false;
        //    }
        //}
        //    //choosing
        //else if (vocas[currentIndex].TestType == "1") {
        //    var selectedValue = $('#selectedValue').val().toLowerCase().replace(/\s+/g, '');
        //    if (selectedValue == '') {
        //        alert('Vui lòng chọn 1 giá trị!');
        //        return false;
        //    }
        //}
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
        //if (keycode == 32) {
        //    if (currentIndex >= 0 && currentIndex < vocas.length) {
        //        if (vocas[currentIndex].TestSkill == '1') {
        //            speak(vocas[currentIndex].UrlAudio);
        //        }
        //    }
        //    return false;
        //}
    });


});

function updateFastTestVoca() {
    $.ajax({
        cache: true,
        type: "post",
        async: true,
        url: '/Library/' + $('#uft').val(),
        data: JSON.stringify(vocas[currentIndex]),
        dataType: "json",
        contentType: 'application/json',
        success: function (result) {
            if (result.ReturnCode != 0) {
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.responseText);
            return false;
        }
    });
}

function updateTestResult() {
    for (var i = 0; i < vocas.length; i++) {
        vocas[i].CompletedTime = completedTime;
    }
    if (vocas.length > 0) {
        $.ajax({
            cache: true,
            type: "post",
            async: true,
            url: '/Library/' + $('#utr').val(),
            data: JSON.stringify(vocas),
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
}


function getTestVocas() {
    vocas = [];
    //        failArray = [];

    $.ajax({
        cache: true,
        type: "get",
        async: true,
        url: '/Library/' + $('#gtv').val(),
        data: { "id": $('#vcd').val() },
        dataType: "json",
        success: function (result) {
            if (result.returnCode == $('#accessDenied').val()) {
                window.location.href = '/Account/RequireLogin';
            } else {
                $.each(result.vocabularies, function (i, voca) {
                    vocas.push(voca);
                    //                        failArray.push(voca);
                    if (voca.TestSkill == '1') {
                        var audio = new Audio(voca.UrlAudio);
                        audio.load();
                    }
                });

                completedTime = 0;
                currentIndex = 0;

                if (vocas[currentIndex].HasLearnt == "1") {
                    isPractice = true;
                }
                else if (vocas[currentIndex].Level == "0") {
                    isPractice = false;
                }
                else if (vocas[currentIndex].Level < 10) {
                    isPractice = true;
                }
                else {
                    isPractice = false;
                }
                showFlashCard(currentIndex, false);
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


function getLink(url) {
    return url;
};


function checkInput() {
    if (isPractice) {
        //completedTime += (timePerVoca - parseInt($('#timer').html()));
        if (currentIndex < vocas.length) {
            //choosing
            if (vocas[currentIndex].TestType == "1") {

                var selectedValue = $('#selectedValue').val().toLowerCase().replace(/\s+/g, '');
                if (selectedValue == '') {
                    alert('Vui lòng chọn 1 giá trị!');
                    return false;
                }

                //var selectedValue = $('#selectedValue').val().toLowerCase().replace(/\s+/g, '');
                var resultValue = $('#correctValue').val().toLowerCase().replace(/\s+/g, ''); //vocas[index].Hiragana.toLowerCase().replace(/\s+/g, '');

                vocas[currentIndex].SelectedValue = selectedValue;
                vocas[currentIndex].IsCorrect = "1";

                //show error if wrong
                if (selectedValue != resultValue) {
                    //speak
                    speak('/Content/media/fail.wav');

                    vocas[currentIndex].IsCorrect = "0";

                    //inCorrectVocas.push(vocas[currentIndex]);

                }
                else {
                    //speak corrent voca
                    speak('/Content/media/tada.wav');

                    vocas[currentIndex].IsCorrect = "1";

                    var level = parseInt(vocas[currentIndex].Level) + 2;
                    if (level > 10) {
                        level = 10;
                    }
                    vocas[currentIndex].Level = level;

                    //add to correct list
                    //correctVocas.push(vocas[currentIndex]);

                }

                //next
                //if (currentIndex < vocas.length) {
                if (vocas[currentIndex].IsCorrect == "0") {

                    //switch to mode Learning
                    isPractice = false;
                    showFlashCard(currentIndex, false);

                }
                else {

                    //get practice voca
                    currentIndex++;
                    if (currentIndex == vocas.length) {
                        currentIndex = 0;
                    }
                    while (currentIndex < vocas.length && vocas[currentIndex].Level == 4) {
                        currentIndex++;
                    }
                    
                    if (currentIndex < vocas.length) {
                        if (vocas[currentIndex].HasLearnt == "1") {
                            isPractice = true;
                        }
                        else if (vocas[currentIndex].Level == "0") {
                            isPractice = false;
                        }
                        else if (vocas[currentIndex].Level < 10) {
                            isPractice = true;
                        }
                        else {
                            isPractice = false;
                        }

                        showFlashCard(currentIndex, false);
                    }
                    else {
                        //show result
                        currentIndex = -1;
                        showFlashCard(-1, false);

                        //update result
                        isPass = (correctVocas.length >= vocas.length * 8 / 10) ? true : false;
                        updateTestResult();
                    }

                }
                //}
                //else {
                //    //show result
                //    currentIndex = -1;
                //    showFlashCard(-1, false);

                //    //update result
                //    isPass = (correctVocas.length >= vocas.length * 8 / 10) ? true : false;
                //    updateTestResult();
                //}

                //$('#inputAlphabet').attr('disabled', 'disabled');
                //document.getElementById("test-content").style.cursor = "not-allowed";
                //$("#item-active").html('');
            }

            //update db
            //updateFastTestVoca();
        }
        else {
            //    //show result
            currentIndex = -1;
            showFlashCard(-1, false);
        }
    }
    else {
        //Update to HasLearnt
        vocas[currentIndex].HasLearnt = "1";

        isPractice = true;
        showFlashCard(currentIndex, false);
    }

}

function showFlashCard(index, voice) {

    //$('#inputAlphabet').removeAttr('disabled');
    //document.getElementById("test-content").style.cursor = "auto";

    if (index == -1) {

        //SHOW RESULT
        var urlLearning = $('#ale').val();//'@Url.Action("hoc-tu-vung", "Library", new { id = @ViewBag.CategoryID, urlDisplay = @ViewBag.CategoryUrlDisplay})';
        var urlVoca = $('#av').val();//'@Url.Action("danh-muc", "Library", new { id = @ViewBag.CategoryID, urlDisplay = @ViewBag.CategoryUrlDisplay})';

        //result
        var numOfOK = parseInt(vocas.length * 8 / 10);
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
        html += '                   <p class="text-info">Thời gian hoàn thành: ' + completedTime + ' giây</p>';
        html += '                   <p class="text-info">Kết quả: ' + (correctVocas.length >= numOfOK ? "Chúc mừng bạn đã vượt qua được bài kiểm tra" : "Bạn đã không vượt qua được bài kiểm tra. Hãy ôn lại") + '</p>';
        html += '               </div>';
        html += '           </div>';
        html += '           <div class="row text-center">';
        html += '               <div class="col-lg-12">';
        if (correctVocas.length < numOfOK) {
            //html += '                   <a href="#" role="button" class="btn btn-navigator btn-lg" onclick="showResult(0); return false;">Xem kết quả</a>';
            html += '                   <a href="' + urlLearning + '" role="button" class="btn btn-navigator btn-lg" >Ôn lại</a>';
        }
        else {
            //html += '                   <a href="#" role="button" class="btn btn-navigator btn-lg" onclick="showResult(0); return false;">Xem kết quả</a>';
            html += '                   <a href="' + urlVoca + '" class="btn btn-navigator btn-lg" role="button" >Trở về</a>';
        }
        html += '               </div>';
        html += '           </div>';
        html += '       </div>';
        html += '   </div>';
        html += '</div>';

        $('#flashCard').html(html);
        $('#btnNext').hide();

    }
    else {
        var voca = searchVoca(index);
        if (voca != null) {

            //If Learning
            if (!isPractice) {

                var html = showLearning(voca);

                $('#flashCard').html(html);
            }
            else {
                //If Practice
                var html = showPractise(voca);
                $('#flashCard').html(html);

                //if (voca.TestType == "2") {
                //    $('#inputAlphabet').val('');
                //    $('#inputAlphabet').focus();

                //    $('#btnNext').show();
                //}
                //else if (voca.TestType == "1") {
                //    $('#btnNext').hide();
                //}

                //listening skill
                if (voca.TestSkill == '1') {
                    speak(voca.UrlAudio);
                }

            }
        }
    }
};


function showResult(index) {
    //var urlLearning = '@Url.Action("hoc-tu-vung", "Library", new { id = @ViewBag.CategoryID, urlDisplay = @ViewBag.CategoryUrlDisplay})';
    var urlLearning = $('#ale').val();
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
        html += '                   <a href="' + urlLearning + '" role="button" class="btn btn-navigator btn-lg" >Quay về</a>';
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

function showLearning(voca) {
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
    }
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

    return html;
};

function showPractise(voca) {

    var requiredTimePerVoca = parseInt($('#rtp').val());
    var fee = parseFloat($('#vsf').val());

    var html = '';
    html += '<div id="carousel-example-generic" class="carousel slide" data-ride="carousel">';
    html += '   <div class="carousel-inner" role="listbox">';
    html += '       <div class="item active">';
    html += '           <div class="row">';
    html += '               <div class="col-lg-3">';
    html += '                   <p class="text-info">Thời gian: <strong><span id="timer">' + requiredTimePerVoca + '</span></strong></p>';
    html += '               </div>';
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
    //reading
    if (voca.TestSkill == '3') {
        html += '                   <img class="img-rounded" src="' + getLink(voca.UrlImage) + '" alt="Từ vựng tiếng Nhật" height="300px" width="100%"/>';
    }
        //translating
    else if (voca.TestSkill == '5') {
        html += '                   <h3>' + voca.VMeaning + '</h3>';
    }
        //listening
    else if (voca.TestSkill == '1') {
        html += '                   <button type="button" class="btn btn-default btn-lg" onclick="speak(\'' + voca.UrlAudio + '\');"><span class="glyphicon glyphicon-volume-up" aria-hidden="true">Nghe</span></button>';
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

    return html;
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