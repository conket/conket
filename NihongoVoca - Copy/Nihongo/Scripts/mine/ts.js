
var timePerVoca = 15;
var completedTime = 0;

var inCorrectVocas = [];
var correctVocas = [];
var vocas = [];

var isPass = false;

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

    $('#test-content').hide();
    $('#test-note').show();
    currentIndex = 0;

    $('#aTest').on('click', function () {
        $('#test-content').show();
        $('#test-note').hide();

        //load datas
        getTestVocas();
    });

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

var timer;
var interval = 1500;

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
    completedTime += (timePerVoca - parseInt($('#timer').html()));
    if (currentIndex < vocas.length) {
        //input romaji
        if (vocas[currentIndex].TestType == "2") {
            var inputValue = $('#inputAlphabet').val().toLowerCase().replace(/\s+/g, '');
            vocas[currentIndex].SelectedValue = inputValue;
            //                vocas[currentIndex].IsCorrect = "1";

            var isCorrect = "1";
            if (vocas[currentIndex].DisplayType == "3") {
                var checkValueOnRomaji = vocas[index].OnRomaji.toLowerCase().replace(/\s+/g, '');
                var checkValueOnReading = vocas[index].OnReading.toLowerCase().replace(/\s+/g, '');
                var checkValueOnRomaji2 = vocas[index].OnRomaji2.toLowerCase().replace(/\s+/g, '');
                var checkValueOnReading2 = vocas[index].OnReading2.toLowerCase().replace(/\s+/g, '');
                var checkValueKunRomaji = vocas[index].KunRomaji.toLowerCase().replace(/\s+/g, '');
                var checkValueKunReading = vocas[index].KunReading.toLowerCase().replace(/\s+/g, '');
                var checkValueKunRomaji2 = vocas[index].KunRomaji2.toLowerCase().replace(/\s+/g, '');
                var checkValueKunReading2 = vocas[index].KunReading2.toLowerCase().replace(/\s+/g, '');
                var checkValueKanji = vocas[index].Kanji.toLowerCase().replace(/\s+/g, '');
                //show error if wrong
                if (inputValue != checkValueOnRomaji
                && inputValue != checkValueOnReading
                && inputValue != checkValueOnRomaji2
                && inputValue != checkValueOnReading2
                && inputValue != checkValueKunRomaji
                && inputValue != checkValueKunReading
                && inputValue != checkValueKunRomaji2
                && inputValue != checkValueKunReading2
                && inputValue != checkValueKanji
                ) {
                    isCorrect = "0";
                }
            }
            else {

                var checkValueRomaji = vocas[currentIndex].Romaji.toLowerCase().replace(/\s+/g, '');
                var checkValueHira = vocas[currentIndex].Hiragana.toLowerCase().replace(/\s+/g, '');
                var checkValueKata = vocas[currentIndex].Katakana.toLowerCase().replace(/\s+/g, '');
                var checkValueKanji = vocas[currentIndex].Kanji.toLowerCase().replace(/\s+/g, '');

                //show error if wrong
                if (inputValue != checkValueRomaji
                && inputValue != checkValueHira
                && inputValue != checkValueKata
                && inputValue != checkValueKanji
                ) {
                    isCorrect = "0";
                }
            }

            vocas[currentIndex].IsCorrect = isCorrect;
            if (isCorrect == "0") {
                //speak
                speak('/Content/media/fail.wav');

                //push to has not learnt list
                inCorrectVocas.push(vocas[currentIndex]);

                $('#result').html();
            }
            else {
                //speak corrent voca
                speak('/Content/media/tada.wav');

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
                speak('/Content/media/fail.wav');

                vocas[currentIndex].IsCorrect = "0";

                inCorrectVocas.push(vocas[currentIndex]);
            }
            else {
                //speak corrent voca
                speak('/Content/media/tada.wav');

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

        //update result
        isPass = (correctVocas.length >= vocas.length * 8 / 10) ? true : false;
        updateTestResult();
    }

    $('#inputAlphabet').attr('disabled', 'disabled');
    document.getElementById("test-content").style.cursor = "not-allowed";
    $("#item-active").html('');
}

function showFlashCard(index, voice) {

    $('#inputAlphabet').removeAttr('disabled');
    document.getElementById("test-content").style.cursor = "auto";
    clearInterval(timer);
    var requiredTimePerVoca = parseInt($('#rtp').val());
    var fee = parseFloat($('#vsf').val());
    if (index == -1) {
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
        
        console.log('voca:' + vocas.length);
        console.log('correct:' + correctVocas.length);
        console.log('completedTime:' + completedTime);
        console.log('requiredTime:' + (requiredTimePerVoca * vocas.length / 2));
        if (fee > 0) {
            if (correctVocas.length == vocas.length && (completedTime <= (requiredTimePerVoca * vocas.length / 2))) {
                html += '                   <p class="text-info">Với việc hoàn thành <strong>TUYỆT VỜI</strong> bài kiểm tra, bạn nhận được <strong>10 ĐIỂM TÍCH LŨY<strong></p>';
            }
            else if (correctVocas.length == vocas.length) {
                html += '                   <p class="text-info">Với việc hoàn thành <strong>XUẤT SẮC</strong> bài kiểm tra, bạn nhận được <strong>5 ĐIỂM TÍCH LŨY<strong></p>';
            }
            else if (correctVocas.length >= numOfOK) {
                html += '                   <p class="text-info">Với việc hoàn thành <strong>TỐT</strong> bài kiểm tra, bạn nhận được <strong>2 ĐIỂM TÍCH LŨY<strong></p>';
            }
            else {
                html += '                   <p class="text-info">Với việc hoàn thành bài kiểm tra, bạn nhận được <strong>1 ĐIỂM TÍCH LŨY<strong></p>';
            }
        }
        else {
            html += '                   <p class="text-info">Với việc hoàn thành bài kiểm tra, bạn nhận được <strong>1 ĐIỂM TÍCH LŨY<strong></p>';
        }

        html += '               </div>';
        html += '           </div>';
        html += '           <div class="row text-center">';
        html += '               <div class="col-lg-12">';
        if (correctVocas.length < numOfOK) {
            html += '                   <a href="#" role="button" class="btn btn-navigator btn-lg" onclick="showResult(0); return false;">Xem kết quả</a>';
            html += '                   <a href="' + urlLearning + '" role="button" class="btn btn-navigator btn-lg" >Ôn lại</a>';
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

    }
    else {
        var voca = searchVoca(index);
        if (voca != null) {
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

        $("#timer").setTimer(function (i, count) {
            $(this).html(count);
            //alert(completedTime + ' += ' + (timePerVoca - count));
            //completedTime += (timePerVoca - count);
        }, 1000, timePerVoca, true, function () {
            if ($("#timer").html() == "0") {
                checkInput();
            }
        });
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