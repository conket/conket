
var timePerVoca = 15;
var completedTime = 0;

var inCorrectVocas = [];
var correctVocas = [];
var vocas = [];

var isPass = false;
var totalLevel = 0;
var currentLevel = 0;

//Mode Practice or Learning
var isPractice = true;

var currentIndex = 0;
var quizzVoca = [];

$(document).ready(function () {

    window.onbeforeunload = function confirmExit() {
        //            return "Kết quả bài kiểm tra sẽ không được ghi nhận. Bạn có muốn thoát trang?";
        if (vocas.length > 0 && currentIndex >= 0) {
            return "Kết quả bài học sẽ không được ghi nhận nếu bạn thoát khỏi trang";
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
        if (isPractice) {
            quizzVoca = createQuizz(currentIndex);
            showFlashCard(quizzVoca, false);
        }
        else {
            showFlashCard(quizzVoca, true);
        }
        //checkInput();
    });

    $(document).keydown(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (event.ctrlKey) {
            //ctrl-d (draw)
            if (keycode == 68) {
                draw();
                return false;
            }
            else if (keycode == 32) {
                if (!isPractice) {
                    speak(quizzVoca.UrlAudio);
                    return false;
                }
            }
        }
            //space
        else if (keycode == 32) {
            if (!isPractice) {
                speak(quizzVoca.UrlAudio);
                return false;
            }
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
        //space
        //if (keycode == 32) {
        //    if (currentIndex >= 0 && currentIndex < vocas.length) {
        //        if (voca.TestSkill == '1') {
        //            speak(voca.UrlAudio);
        //        }
        //    }
        //    return false;
        //}
    });


});

//function updateFastTestVoca() {
//    $.ajax({
//        cache: true,
//        type: "post",
//        async: true,
//        url: '/Library/' + $('#uft').val(),
//        data: JSON.stringify(voca),
//        dataType: "json",
//        contentType: 'application/json',
//        success: function (result) {
//            if (result.ReturnCode != 0) {
//            }
//        },
//        error: function (xhr, ajaxOptions, thrownError) {
//            console.log(xhr.responseText);
//            return false;
//        }
//    });
//}

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
                    alert('Có lỗi xảy ra!');
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
    totalLevel = 0;
    currentLevel = 0;
    isPractice = true;
    currentIndex = 0;
    //        failArray = [];

    $.ajax({
        cache: true,
        type: "get",
        async: true,
        url: '/Library/' + $('#gtv').val(),
        data: { "id": $('#vsd').val() },
        dataType: "json",
        success: function (result) {
            if (result.returnCode == $('#accessDenied').val()) {
                window.location.href = '/Account/RequireLogin';
            } else {
                $.each(result.vocabularies, function (i, voca) {
                    vocas.push(voca);
                    //                        failArray.push(voca);
                    //if (voca.TestSkill == '3') {
                        var audio = new Audio(voca.UrlAudio);
                        audio.load();
                    //}

                    //Calculate total Level
                    totalLevel += parseInt(10 - voca.Level);
                });

                completedTime = 0;
                currentIndex = 0;


                //create quizz voca
                quizzVoca = createQuizz(currentIndex);
                
                if (quizzVoca.HasLearnt == "1") {
                    isPractice = true;
                }
                else if (quizzVoca.Level == "0") {
                    isPractice = false;
                }
                else if (quizzVoca.Level < 10) {
                    isPractice = true;
                }
                else {
                    isPractice = false;
                }
                showFlashCard(quizzVoca, false);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.responseText);
            return false;
        },
        //beforeSend: function () {
        //    $('#loadingModal').modal();
        //},
        //complete: function () {
        //    $('#loadingModal').modal('hide');
        //}
    });
}


function getPracticeVocas() {
    vocas = [];
    totalLevel = 0;
    currentLevel = 0;
    isPractice = true;
    currentIndex = 0;
    //        failArray = [];

    $.ajax({
        cache: true,
        type: "get",
        async: true,
        url: '/Library/' + $('#gpr').val(),
        data: { "id": $('#vsd').val() },
        dataType: "json",
        success: function (result) {
            if (result.returnCode == $('#accessDenied').val()) {
                window.location.href = '/Account/RequireLogin';
            } else {
                $.each(result.vocabularies, function (i, voca) {
                    vocas.push(voca);
                    //                        failArray.push(voca);
                    //if (voca.TestSkill == '3') {
                    var audio = new Audio(voca.UrlAudio);
                    audio.load();
                    //}

                    //Calculate total Level
                    totalLevel += parseInt(10 - voca.Level);
                });

                completedTime = 0;
                currentIndex = 0;


                //create quizz voca
                quizzVoca = createQuizz(currentIndex);

                if (quizzVoca.HasLearnt == "1") {
                    isPractice = true;
                }
                else if (quizzVoca.Level == "0") {
                    isPractice = false;
                }
                else if (quizzVoca.Level < 10) {
                    isPractice = true;
                }
                else {
                    isPractice = false;
                }
                showFlashCard(quizzVoca, false);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.responseText);
            return false;
        },
        //beforeSend: function () {
        //    $('#loadingModal').modal();
        //},
        //complete: function () {
        //    $('#loadingModal').modal('hide');
        //}
    });
}

function getLink(url) {
    return url;
};


//function checkInput() {

//    if (isPractice) {
//        //completedTime += (timePerVoca - parseInt($('#timer').html()));
//        if (currentIndex < vocas.length) {
//            //choosing
//            if (vocas[currentIndex].TestType == "1") {

//                var selectedValue = $('#selectedValue').val().toLowerCase().replace(/\s+/g, '');
//                if (selectedValue == '') {
//                    alert('Vui lòng chọn 1 giá trị!');
//                    return false;
//                }

//                //var selectedValue = $('#selectedValue').val().toLowerCase().replace(/\s+/g, '');
//                var resultValue = $('#correctValue').val().toLowerCase().replace(/\s+/g, ''); //vocas[index].Hiragana.toLowerCase().replace(/\s+/g, '');

//                vocas[currentIndex].SelectedValue = selectedValue;
//                vocas[currentIndex].IsCorrect = "1";

//                //show error if wrong
//                if (selectedValue != resultValue) {
//                    //speak
//                    speak('/Content/media/fail.wav');

//                    vocas[currentIndex].IsCorrect = "0";

//                    //inCorrectVocas.push(vocas[currentIndex]);

//                }
//                else {
//                    //speak corrent voca
//                    speak('/Content/media/tada.wav');

//                    vocas[currentIndex].IsCorrect = "1";

//                    var level = parseInt(vocas[currentIndex].Level) + 2;
//                    if (level > 10) {
//                        level = 10;
//                    }
//                    vocas[currentIndex].Level = level;

//                    currentLevel += 2;
//                    if (level > 10) {
//                        currentLevel -= 1;
//                    }
//                    //add to correct list

//                }

//                //next
//                //if (currentIndex < vocas.length) {
//                if (vocas[currentIndex].IsCorrect == "0") {
//                    switch (vocas[currentIndex].CorrectResult) {
//                        case 1:
//                            $('#result1').html('1<br>' + vocas[currentIndex].Result1 + '<br><span class="glyphicon glyphicon-ok ok"></span>');
//                            break;
//                        case 2:
//                            $('#result2').html('2<br>' + vocas[currentIndex].Result2 + '<br><span class="glyphicon glyphicon-ok ok"></span>');
//                            break;
//                        case 3:
//                            $('#result3').html('3<br>' + vocas[currentIndex].Result3 + '<br><span class="glyphicon glyphicon-ok ok"></span>');
//                            break;
//                        case 4:
//                            $('#result4').html('4<br>' + vocas[currentIndex].Result4 + '<br><span class="glyphicon glyphicon-ok ok"></span>');
//                            break;
//                        default:
//                    }
//                    switch (selectedValue) {
//                        case "1":
//                            $('#result1').html('1<br>' + vocas[currentIndex].Result1 + '<br><span class="glyphicon glyphicon-remove error"></span>');
//                            break;
//                        case "2":
//                            $('#result2').html('2<br>' + vocas[currentIndex].Result2 + '<br><span class="glyphicon glyphicon-remove error"></span>');
//                            break;
//                        case "3":
//                            $('#result3').html('3<br>' + vocas[currentIndex].Result3 + '<br><span class="glyphicon glyphicon-remove error"></span>');
//                            break;
//                        case "4":
//                            $('#result4').html('4<br>' + vocas[currentIndex].Result4 + '<br><span class="glyphicon glyphicon-remove error"></span>');
//                            break;
//                        default:
//                    }

//                    $("a[name='resultChoosing']").attr('disabled', true);
//                    //switch to mode Learning
//                    isPractice = false;
//                    //showFlashCard(currentIndex, false);

//                }
//                else {
//                    $(".c100").trigger('hover');

//                    //get practice voca
//                    currentIndex++;
//                    if (currentIndex == vocas.length) {
//                        currentIndex = 0;
//                    }
//                    while (currentIndex < vocas.length && vocas[currentIndex].Level == 4) {
//                        currentIndex++;
//                    }

//                    if (currentIndex < vocas.length) {
//                        if (vocas[currentIndex].HasLearnt == "1") {
//                            isPractice = true;
//                        }
//                        else if (vocas[currentIndex].Level == "0") {
//                            isPractice = false;
//                        }
//                        else if (vocas[currentIndex].Level < 10) {
//                            isPractice = true;
//                        }
//                        else {
//                            isPractice = false;
//                        }

//                        showFlashCard(currentIndex, false);

//                        if (isPractice) {
//                            showProgress();
//                        }
//                    }
//                    else {
//                        //show result
//                        currentIndex = -1;
//                        currentLevel = totalLevel;
//                        showFlashCard(-1, false);
//                        showProgress();

//                        //update result
//                        isPass = (correctVocas.length >= vocas.length * 8 / 10) ? true : false;
//                        updateTestResult();
//                    }

//                }
//                //}
//                //else {
//                //    //show result
//                //    currentIndex = -1;
//                //    showFlashCard(-1, false);

//                //    //update result
//                //    isPass = (correctVocas.length >= vocas.length * 8 / 10) ? true : false;
//                //    updateTestResult();
//                //}

//                //$('#inputAlphabet').attr('disabled', 'disabled');
//                //document.getElementById("test-content").style.cursor = "not-allowed";
//                //$("#item-active").html('');
//            }

//            //update db
//            //updateFastTestVoca();
//        }
//        else {
//            //    //show result
//            currentIndex = -1;
//            showFlashCard(-1, false);
//        }
//    }
//    else {
//        //Update to HasLearnt
//        vocas[currentIndex].HasLearnt = "1";

//        isPractice = true;
//        showFlashCard(currentIndex, false);
//    }

//}


function checkInput() {

    if (isPractice) {
        //completedTime += (timePerVoca - parseInt($('#timer').html()));
        if (quizzVoca != null) {
            //choosing
            if (quizzVoca.TestType == "1") {

                var selectedValue = $('#selectedValue').val().toLowerCase().replace(/\s+/g, '');
                if (selectedValue == '') {
                    alert('Vui lòng chọn 1 giá trị!');
                    return false;
                }

                //var selectedValue = $('#selectedValue').val().toLowerCase().replace(/\s+/g, '');
                var resultValue = $('#correctValue').val().toLowerCase().replace(/\s+/g, ''); //vocas[index].Hiragana.toLowerCase().replace(/\s+/g, '');

                quizzVoca.SelectedValue = selectedValue;
                quizzVoca.IsCorrect = "1";

                //show error if wrong
                if (selectedValue != resultValue) {
                    //speak
                    speak('/Content/media/fail.wav');

                    quizzVoca.IsCorrect = "0";

                    //inCorrectVocas.push(voca);

                }
                else {
                    //speak corrent voca
                    speak('/Content/media/tada.wav');

                    quizzVoca.IsCorrect = "1";

                    var level = parseInt(quizzVoca.Level) + 2;
                    if (level > 10) {
                        level = 10;
                    }
                    quizzVoca.Level = level;

                    currentLevel += 2;
                    if (level > 10) {
                        currentLevel -= 1;
                    }
                    //add to correct list

                }

                //sleep(1000);

                //next
                //if (currentIndex < vocas.length) {
                if (quizzVoca.IsCorrect == "0") {
                    switch (quizzVoca.CorrectResult) {
                        case 1:
                            $('#result1').html('1 <span class="glyphicon glyphicon-ok ok"></span><br>' + quizzVoca.Result1);
                            break;
                        case 2:
                            $('#result2').html('2 <span class="glyphicon glyphicon-ok ok"></span><br>' + quizzVoca.Result2);
                            break;
                        case 3:
                            $('#result3').html('3 <span class="glyphicon glyphicon-ok ok"></span><br>' + quizzVoca.Result3);
                            break;
                        case 4:
                            $('#result4').html('4 <span class="glyphicon glyphicon-ok ok"></span><br>' + quizzVoca.Result4);
                            break;
                        default:
                    }
                    switch (selectedValue) {
                        case "1":
                            $('#result1').html('1 <span class="glyphicon glyphicon-remove error"></span><br>' + quizzVoca.Result1);
                            break;
                        case "2":
                            $('#result2').html('2 <span class="glyphicon glyphicon-remove error"></span><br>' + quizzVoca.Result2);
                            break;
                        case "3":
                            $('#result3').html('3 <span class="glyphicon glyphicon-remove error"></span><br>' + quizzVoca.Result3);
                            break;
                        case "4":
                            $('#result4').html('4 <span class="glyphicon glyphicon-remove error"></span><br>' + quizzVoca.Result4);
                            break;
                        default:
                    }

                    $("a[name='resultChoosing']").attr('disabled', true);
                    //switch to mode Learning
                    isPractice = false;
                    //showFlashCard(currentIndex, false);

                }
                else {
                    currentIndex++;
                    if (currentIndex == vocas.length) {
                        currentIndex = 0;
                    }
                    while (currentIndex < vocas.length && vocas[currentIndex].Level == 4) {
                        currentIndex++;
                    }

                    if (currentIndex < vocas.length) {

                        quizzVoca = createQuizz(currentIndex);
                        if (quizzVoca.HasLearnt == "1") {
                            isPractice = true;
                        }
                        else if (quizzVoca.Level == "0") {
                            isPractice = false;
                        }
                        else if (quizzVoca.Level < 10) {
                            isPractice = true;
                        }
                        else {
                            isPractice = false;
                        }

                        showFlashCard(quizzVoca, false);

                        if (isPractice) {
                            showProgress();
                        }
                    }
                    else {
                        //show result
                        currentIndex = -1;
                        currentLevel = totalLevel;
                        //showFlashCard(-1, false);

                        showResultPage();
                        showProgress();
                        
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
        quizzVoca.HasLearnt = "1";

        isPractice = true;
        showFlashCard(currentIndex, false);
    }

}

function sleep(milliseconds) {
    var start = new Date().getTime();
    for (var i = 0; i < 1e7; i++) {
        if ((new Date().getTime() - start) > milliseconds) {
            break;
        }
    }
}

function showProgress() {
    var progressHtml = '<div class="progress-bar" role="progressbar" aria-valuenow="' + (currentLevel)
                    + '" aria-valuemin="0" '
                    + '" aria-valuemax="' + (totalLevel)
                    + '" style="width: ' + (totalLevel == 0 ? 0 : (currentLevel / totalLevel * 100)) + '%; min-width: 2em;">'
                    //+ (index + 1) + '/' + (vocas.length)
                    + '</div>';
    $('#progress').html(progressHtml);
}

function showResultPage()
{
    //SHOW RESULT
    var urlLearning = $('#ale').val();//'@Url.Action("hoc-tu-vung", "Library", new { id = @ViewBag.CategoryID, urlDisplay = @ViewBag.CategoryUrlDisplay})';
    var urlVoca = $('#av').val();//'@Url.Action("danh-muc", "Library", new { id = @ViewBag.CategoryID, urlDisplay = @ViewBag.CategoryUrlDisplay})';

    //result
    var numOfOK = parseInt(vocas.length * 8 / 10);
    var html = '';
    html += '<div id="carousel-example-generic" class="carousel slide" data-ride="carousel">';
    html += '   <div class="carousel-inner" role="listbox">';
    html += '       <div class="item active">';
    //html += '           <div class="row text-center">';
    ////            html += '               <div class="col-lg-4 col-md-4 col-xs-6">';
    ////            html += '                   <img class="img-rounded" src="' + getLink(voca.UrlImage) + '" alt="Từ vựng tiếng Nhật" height="300px" width="100%"/>';
    ////            html += '               </div>';
    //html += '               <div class="col-lg-12">';
    //html += '                   <p class="text-info">Số câu đúng: ' + correctVocas.length + '/' + vocas.length + '</p>';
    //html += '                   <p class="text-info">Thời gian hoàn thành: ' + completedTime + ' giây</p>';
    //html += '                   <p class="text-info">Kết quả: ' + (correctVocas.length >= numOfOK ? "Chúc mừng bạn đã vượt qua được bài kiểm tra" : "Bạn đã không vượt qua được bài kiểm tra. Hãy ôn lại") + '</p>';
    //html += '               </div>';
    //html += '           </div>';
    html += '           </br>';
    html += '           <div class="row text-center">';
    html += '               <div class="col-lg-12">';
    html += '                   <a class="btn btn-navigator btn-lg require-login" href="#" onclick="location.reload(true); return false;">HỌC TIẾP</a>';
    html += '                   <a class="btn btn-navigator btn-lg require-login" href="#" onclick="getPracticeVocas(); return false;">ÔN TẬP</a>';
    //if (correctVocas.length < numOfOK) {
    //    //html += '                   <a href="#" role="button" class="btn btn-navigator btn-lg" onclick="showResult(0); return false;">Xem kết quả</a>';
    //    html += '                   <a href="' + urlLearning + '" role="button" class="btn btn-navigator btn-lg" >Ôn lại</a>';
    //}
    //else {
    //    //html += '                   <a href="#" role="button" class="btn btn-navigator btn-lg" onclick="showResult(0); return false;">Xem kết quả</a>';
    //    html += '                   <a href="' + urlVoca + '" class="btn btn-navigator btn-lg" role="button" >Trở về</a>';
    //}
    html += '               </div>';
    html += '           </div>';
    html += '           </br>';
    html += '           <div class="row text-center">';
    html += '               <div class="col-lg-12">';
    html += '                   <a class="btn btn-navigator btn-lg require-login" href="' + urlLearning + '">ÔN SỔ TAY</a>';
    html += '                   <a class="btn btn-navigator btn-lg require-login" href="' + urlVoca + '">TRỞ VỀ</a>';
    html += '               </div>';
    html += '           </div>';
    html += '           <hr>';
    html += '           <div class="row">';
    html += '               <div class="col-lg-2">';
    html += '               </div>';
    html += '               <div class="col-lg-8">';
    html += '                   <h3><label>CÁC TỪ VỪA HỌC</label></h3>';
    html += '<table class="table table-condensed table-hover">';
    for (var i = 0; i < vocas.length; i++) {
        html += '<tr>';
        html += '<td><strong>' + (i + 1) + '</strong></td>';
        html += '<td>' + (vocas[i].DisplayType == '1' ? vocas[i].Hiragana : vocas[i].Katakana) + '</td>';
        html += '<td>' + vocas[i].Kanji + '</td>';
        html += '<td>' + vocas[i].VMeaning + '</td>';
        html += '<td>' + vocas[i].Level + '</td>';
        html += '</tr>';
    }
    html += '</table>';
    html += '               </div>';
    html += '               <div class="col-lg-2">';
    html += '               </div>';
    html += '           </div>';
    html += '       </div>';
    html += '   </div>';
    html += '</div>';

    $('#flashCard').html(html);
    $('#btnNext').hide();

}

function showFlashCard(index, voice) {
    $('#btnNext').show();
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
        html += '                   <a class="btn btn-navigator btn-lg require-login" href="' + urlLearning + '">HỌC TIẾP</a>';
        html += '                   <a class="btn btn-navigator btn-lg require-login" href="' + urlVoca + '">ÔN TẬP</a>';
        //if (correctVocas.length < numOfOK) {
        //    //html += '                   <a href="#" role="button" class="btn btn-navigator btn-lg" onclick="showResult(0); return false;">Xem kết quả</a>';
        //    html += '                   <a href="' + urlLearning + '" role="button" class="btn btn-navigator btn-lg" >Ôn lại</a>';
        //}
        //else {
        //    //html += '                   <a href="#" role="button" class="btn btn-navigator btn-lg" onclick="showResult(0); return false;">Xem kết quả</a>';
        //    html += '                   <a href="' + urlVoca + '" class="btn btn-navigator btn-lg" role="button" >Trở về</a>';
        //}
        html += '               </div>';
        html += '           </div>';
        html += '</br>';
        html += '           <div class="row text-center">';
        html += '               <div class="col-lg-12">';
        html += '                   <a class="btn btn-navigator btn-lg require-login" href="' + urlLearning + '">ÔN SỔ TAY</a>';
        html += '                   <a class="btn btn-navigator btn-lg require-login" href="' + urlVoca + '">TRỞ VỀ</a>';
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

                speak(voca.UrlAudio);

                isPractice = true;
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
                if (voca.TestSkill == '3') {
                    speak(voca.UrlAudio);
                }

            }
        }
    }
};

function showFlashCard(voca, voice) {
    $('#btnNext').show();

    if (voca != null) {

        //If Learning
        if (!isPractice) {

            var html = showLearning(voca);

            $('#flashCard').html(html);

            speak(voca.UrlAudio);

            isPractice = true;
        }
        else {
            //If Practice
            var html = showPractise(voca);
            $('#flashCard').html(html);

            //listening skill
            //if (voca.TestSkill == '3') {
            //    speak(voca.UrlAudio);
            //}

        }
    }
};

function createQuizz(index) {
    var item = vocas[index];
    var n1 = Math.floor((Math.random() * 4) + 1);
    var n2 = 1;
    var n3 = 2;
    var n4 = 3;

    item.TestType = 1;
    item.TestSkill = Math.floor((Math.random() * 3) + 1);


    //tìm vị trí đặt kq đúng
    switch (n1) {
        case 1:
            item.CorrectResult = 1;
            item.CorrectUrlAudio = item.UrlAudio;

            item.Hiragana1 = item.Hiragana;
            item.Katakana1 = item.Katakana;
            item.Kanji1 = item.Kanji;
            if (item.TestSkill == 2) {
                //reading
                item.Result1 = item.VMeaning;
            }
            else {
                //transanlating && listening
                item.Result1 = item.DisplayType == "3"
                                ? item.Kanji
                                : item.DisplayType == "2" ? item.Katakana + '<hr>' : (item.Hiragana + '<hr>' + item.Kanji);
            }
            item.ResultUrlAudio1 = item.UrlAudio;

            while (item.VocabularyCode == vocas[n2].VocabularyCode) {
                n2 = Math.floor((Math.random() * (vocas.length - 1)) + 0);
            }
            item.Hiragana2 = vocas[n2].Hiragana;
            item.Katakana2 = vocas[n2].Katakana;
            item.Kanji2 = vocas[n2].Kanji;
            if (item.TestSkill == 2) {
                //reading
                item.Result2 = vocas[n2].VMeaning;
            }
            else {
                //transanlating && listening
                item.Result2 = vocas[n2].DisplayType == "3" ? vocas[n2].Kanji
                                : vocas[n2].DisplayType == "2" ? vocas[n2].Katakana + '<hr>' : (vocas[n2].Hiragana + '<hr>' + vocas[n2].Kanji);
            }
            item.ResultUrlAudio2 = vocas[n2].UrlAudio;

            while (item.VocabularyCode == vocas[n3].VocabularyCode || vocas[n2].VocabularyCode == vocas[n3].VocabularyCode) {
                n3 = Math.floor((Math.random() * (vocas.length - 1)) + 0);
            }
            item.Hiragana3 = vocas[n3].Hiragana;
            item.Katakana3 = vocas[n3].Katakana;
            item.Kanji3 = vocas[n3].Kanji;
            if (item.TestSkill == 2) {
                //reading
                item.Result3 = vocas[n3].VMeaning;
            }
            else {
                //transanlating && listening
                item.Result3 = vocas[n3].DisplayType == "3" ? vocas[n3].Kanji
                                : vocas[n3].DisplayType == "2" ? vocas[n3].Katakana + '<hr>' : (vocas[n3].Hiragana + '<hr>' + vocas[n3].Kanji);
            }
            item.ResultUrlAudio3 = vocas[n3].UrlAudio;

            while (item.VocabularyCode == vocas[n4].VocabularyCode || vocas[n2].VocabularyCode == vocas[n4].VocabularyCode || vocas[n3].VocabularyCode == vocas[n4].VocabularyCode) {
                n4 = Math.floor((Math.random() * (vocas.length - 1)) + 0);
            }
            item.Hiragana4 = vocas[n4].Hiragana;
            item.Katakana4 = vocas[n4].Katakana;
            item.Kanji4 = vocas[n4].Kanji;
            if (item.TestSkill == 2) {
                //reading
                item.Result4 = vocas[n4].VMeaning;
            }
            else {
                //transanlating && listening
                item.Result4 = vocas[n4].DisplayType == "3" ? vocas[n4].Kanji
                                : vocas[n4].DisplayType == "2" ? vocas[n4].Katakana + '<hr>' : (vocas[n4].Hiragana + '<hr>' + vocas[n4].Kanji);
            }
            item.ResultUrlAudio4 = vocas[n4].UrlAudio;
            break;
        case 2:
            item.CorrectResult = 2;
            item.CorrectUrlAudio = item.UrlAudio;

            item.Hiragana2 = item.Hiragana;
            item.Katakana2 = item.Katakana;
            item.Kanji2 = item.Kanji;
            if (item.TestSkill == 2) {
                //reading
                item.Result2 = item.VMeaning;
            }
            else {
                //transanlating && listening
                item.Result2 = item.DisplayType == "3"
                                ? item.Kanji
                                : item.DisplayType == "2" ? item.Katakana + '<hr>' : (item.Hiragana + '<hr>' + item.Kanji);
            }
            item.ResultUrlAudio2 = item.UrlAudio;

            while (item.VocabularyCode == vocas[n2].VocabularyCode) {
                n2 = Math.floor((Math.random() * (vocas.length - 1)) + 0);
            }
            item.Hiragana1 = vocas[n2].Hiragana;
            item.Katakana1 = vocas[n2].Katakana;
            item.Kanji1 = vocas[n2].Kanji;
            if (item.TestSkill == 2) {
                //reading
                item.Result1 = vocas[n2].VMeaning;
            }
            else {
                //transanlating && listening
                item.Result1 = vocas[n2].DisplayType == "3" ? vocas[n2].Kanji
                                : vocas[n2].DisplayType == "2" ? vocas[n2].Katakana + '<hr>' : (vocas[n2].Hiragana + '<hr>' + vocas[n2].Kanji);
            }
            item.ResultUrlAudio1 = vocas[n2].UrlAudio;

            while (item.VocabularyCode == vocas[n3].VocabularyCode || vocas[n2].VocabularyCode == vocas[n3].VocabularyCode) {
                n3 = Math.floor((Math.random() * (vocas.length - 1)) + 0);
            }
            item.Hiragana3 = vocas[n3].Hiragana;
            item.Katakana3 = vocas[n3].Katakana;
            item.Kanji3 = vocas[n3].Kanji;
            if (item.TestSkill == 2) {
                //reading
                item.Result3 = vocas[n3].VMeaning;
            }
            else {
                //transanlating && listening
                item.Result3 = vocas[n3].DisplayType == "3" ? vocas[n3].Kanji
                                : vocas[n3].DisplayType == "2" ? vocas[n3].Katakana + '<hr>' : (vocas[n3].Hiragana + '<hr>' + vocas[n3].Kanji);
            }
            item.ResultUrlAudio3 = vocas[n3].UrlAudio;

            while (item.VocabularyCode == vocas[n4].VocabularyCode || vocas[n2].VocabularyCode == vocas[n4].VocabularyCode || vocas[n3].VocabularyCode == vocas[n4].VocabularyCode) {
                n4 = Math.floor((Math.random() * (vocas.length - 1)) + 0);
            }
            item.Hiragana4 = vocas[n4].Hiragana;
            item.Katakana4 = vocas[n4].Katakana;
            item.Kanji4 = vocas[n4].Kanji;
            if (item.TestSkill == 2) {
                //reading
                item.Result4 = vocas[n4].VMeaning;
            }
            else {
                //transanlating && listening
                item.Result4 = vocas[n4].DisplayType == "3" ? vocas[n4].Kanji
                                : vocas[n4].DisplayType == "2" ? vocas[n4].Katakana + '<hr>' : (vocas[n4].Hiragana + '<hr>' + vocas[n4].Kanji);
            }
            item.ResultUrlAudio4 = vocas[n4].UrlAudio;
            break;
        case 3:
            item.CorrectResult = 3;
            item.CorrectUrlAudio = item.UrlAudio;

            item.Hiragana3 = item.Hiragana;
            item.Katakana3 = item.Katakana;
            item.Kanji3 = item.Kanji;
            if (item.TestSkill == 2) {
                //reading
                item.Result3 = item.VMeaning;
            }
            else {
                //transanlating && listening
                item.Result3 = item.DisplayType == "3"
                                ? item.Kanji
                                : item.DisplayType == "2" ? item.Katakana + '<hr>' : (item.Hiragana + '<hr>' + item.Kanji);
            }
            item.ResultUrlAudio3 = item.UrlAudio;

            while (item.VocabularyCode == vocas[n2].VocabularyCode) {
                n2 = Math.floor((Math.random() * (vocas.length - 1)) + 0);
            }
            item.Hiragana1 = vocas[n2].Hiragana;
            item.Katakana1 = vocas[n2].Katakana;
            item.Kanji1 = vocas[n2].Kanji;
            if (item.TestSkill == 2) {
                //reading
                item.Result1 = vocas[n2].VMeaning;
            }
            else {
                //transanlating && listening
                item.Result1 = vocas[n2].DisplayType == "3" ? vocas[n2].Kanji
                                : vocas[n2].DisplayType == "2" ? vocas[n2].Katakana + '<hr>' : (vocas[n2].Hiragana + '<hr>' + vocas[n2].Kanji);
            }
            item.ResultUrlAudio1 = vocas[n2].UrlAudio;

            while (item.VocabularyCode == vocas[n3].VocabularyCode || vocas[n2].VocabularyCode == vocas[n3].VocabularyCode) {
                n3 = Math.floor((Math.random() * (vocas.length - 1)) + 0);
            }
            item.Hiragana2 = vocas[n3].Hiragana;
            item.Katakana2 = vocas[n3].Katakana;
            item.Kanji2 = vocas[n3].Kanji;
            if (item.TestSkill == 2) {
                //reading
                item.Result2 = vocas[n3].VMeaning;
            }
            else {
                //transanlating && listening
                item.Result2 = vocas[n3].DisplayType == "3" ? vocas[n3].Kanji
                                : vocas[n3].DisplayType == "2" ? vocas[n3].Katakana + '<hr>' : (vocas[n3].Hiragana + '<hr>' + vocas[n3].Kanji);
            }
            item.ResultUrlAudio2 = vocas[n3].UrlAudio;

            while (item.VocabularyCode == vocas[n4].VocabularyCode || vocas[n2].VocabularyCode == vocas[n4].VocabularyCode || vocas[n3].VocabularyCode == vocas[n4].VocabularyCode) {
                n4 = Math.floor((Math.random() * (vocas.length - 1)) + 0);
            }
            item.Hiragana4 = vocas[n4].Hiragana;
            item.Katakana4 = vocas[n4].Katakana;
            item.Kanji4 = vocas[n4].Kanji;
            if (item.TestSkill == 2) {
                //reading
                item.Result4 = vocas[n4].VMeaning;
            }
            else {
                //transanlating && listening
                item.Result4 = vocas[n4].DisplayType == "3" ? vocas[n4].Kanji
                                : vocas[n4].DisplayType == "2" ? vocas[n4].Katakana + '<hr>' : (vocas[n4].Hiragana + '<hr>' + vocas[n4].Kanji);
            }
            item.ResultUrlAudio4 = vocas[n4].UrlAudio;
            break;
        case 4:
            item.CorrectResult = 4;
            item.CorrectUrlAudio = item.UrlAudio;

            item.Hiragana4 = item.Hiragana;
            item.Katakana4 = item.Katakana;
            item.Kanji4 = item.Kanji;
            if (item.TestSkill == 2) {
                //reading
                item.Result4 = item.VMeaning;
            }
            else {
                //transanlating && listening
                item.Result4 = item.DisplayType == "3"
                                ? item.Kanji
                                : item.DisplayType == "2" ? item.Katakana + '<hr>' : (item.Hiragana + '<hr>' + item.Kanji);
                item.ResultUrlAudio4 = item.UrlAudio;
            }

            while (item.VocabularyCode == vocas[n2].VocabularyCode) {
                n2 = Math.floor((Math.random() * (vocas.length - 1)) + 0);
            }
            item.Hiragana1 = vocas[n2].Hiragana;
            item.Katakana1 = vocas[n2].Katakana;
            item.Kanji1 = vocas[n2].Kanji;
            if (item.TestSkill == 2) {
                //reading
                item.Result1 = vocas[n2].VMeaning;
            }
            else {
                //transanlating && listening
                item.Result1 = vocas[n2].DisplayType == "3" ? vocas[n2].Kanji
                                : vocas[n2].DisplayType == "2" ? vocas[n2].Katakana + '<hr>' : (vocas[n2].Hiragana + '<hr>' + vocas[n2].Kanji);
            }
            item.ResultUrlAudio1 = vocas[n2].UrlAudio;

            while (item.VocabularyCode == vocas[n3].VocabularyCode || vocas[n2].VocabularyCode == vocas[n3].VocabularyCode) {
                n3 = Math.floor((Math.random() * (vocas.length - 1)) + 0);
            }
            item.Hiragana2 = vocas[n3].Hiragana;
            item.Katakana2 = vocas[n3].Katakana;
            item.Kanji2 = vocas[n3].Kanji;
            if (item.TestSkill == 2) {
                //reading
                item.Result2 = vocas[n3].VMeaning;
            }
            else {
                //transanlating && listening
                item.Result2 = vocas[n3].DisplayType == "3" ? vocas[n3].Kanji
                                : vocas[n3].DisplayType == "2" ? vocas[n3].Katakana + '<hr>' : (vocas[n3].Hiragana + '<hr>' + vocas[n3].Kanji);
            }
            item.ResultUrlAudio2 = vocas[n3].UrlAudio;

            while (item.VocabularyCode == vocas[n4].VocabularyCode || vocas[n2].VocabularyCode == vocas[n4].VocabularyCode || vocas[n3].VocabularyCode == vocas[n4].VocabularyCode) {
                n4 = Math.floor((Math.random() * (vocas.length - 1)) + 0);
            }
            item.Hiragana3 = vocas[n4].Hiragana;
            item.Katakana3 = vocas[n4].Katakana;
            item.Kanji3 = vocas[n4].Kanji;
            if (item.TestSkill == 2) {
                //reading
                item.Result3 = vocas[n4].VMeaning;
            }
            else {
                //transanlating && listening
                item.Result3 = vocas[n4].DisplayType == "3" ? vocas[n4].Kanji
                                : vocas[n4].DisplayType == "2" ? vocas[n4].Katakana + '<hr>' : (vocas[n4].Hiragana + '<hr>' + vocas[n4].Kanji);
            }
            item.ResultUrlAudio3 = vocas[n4].UrlAudio;
            break;
        default:

    }

    
    return item;
}

function calculateProgress() {
    var total = 0;
    for (var i = 0; i < vocas.length; i++) {
        total += parseInt(vocas[i].Level);
    }
    return total;
}


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
    //html += '       <div class="item active">';
    //html += '           <div class="row">';
    //html += '               <div class="col-lg-4 col-md-4 col-xs-12">';
    //if (voca.DisplayType == '3') {
    //    html += '                   <p class="text-info text-center" style="font-size: 150px;">' + voca.Kanji + '</p>';
    //}
    //else {
    //    html += '                   <img class="img-responsive" src="' + getLink(voca.UrlImage) + '" alt="Từ vựng tiếng Nhật" style="height: 300px" />';
    //}
    //html += '               </div>';
    //html += '               <div class="col-lg-8 col-md-8 col-xs-12">';
    //html += '                   <p class="text-info">Định nghĩa</p>';
    //html += '                   <p class="text-default">' + voca.Description + '</p>';
    //if (voca.DisplayType == '3') {
    //    html += '                   <p class="text-default">' + voca.Remembering + '</p>';
    //}
    //html += '               </div>';
    //html += '           </div>';
    //html += '       </div>';
    html += '       <div class="item active">';
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
    //html += '       <a id="flashPre" class="left carousel-control" href="#carousel-example-generic" role="button" data-slide="prev">';
    //html += '           <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>';
    //html += '           <span class="sr-only">Previous</span>';
    //html += '       </a>';
    //html += '       <a id="flashNext" class="right carousel-control" href="#carousel-example-generic" role="button" data-slide="next">';
    //html += '           <span class="glyphicon glyphicon-chevron-right" aria-hidden="true">';
    //html += '           </span><span class="sr-only">Next</span>';
    //html += '       </a>';
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
    html += '           <div class="row text-center" id="item-active">';
    //html += '   <div class="col-lg-1 col-md-1 hidden-xs">';
    //html += '       <div class="c100 p25 small ' + (voca.Level < 10 ? "orange" : "") + '"><span>' + (voca.Level / 10 * 100) + '%</span><div class="slice"><div class="bar"></div><div class="fill"></div></div></div>';
    //html += '   </div>';
    html += '   <div class="col-lg-12 col-md-12 col-xs-12">';
    //html += '               <div class="col-lg-4 col-md-4 col-xs-6  text-center">';
    ////reading
    ////if (voca.TestSkill == '3') {
    ////    html += '                   <img class="img-rounded" src="' + getLink(voca.UrlImage) + '" alt="Từ vựng tiếng Nhật" height="300px" width="100%"/>';
    ////}
    ////    //translating
    if (voca.TestSkill == '2') {
        var displayQuestion = voca.DisplayType == '3' ? voca.Kanji : voca.DisplayType == '1' ? (voca.Hiragana + (voca.Kanji != '' ? ' | ' + voca.Kanji : '')) : voca.Katakana;
        html += '                   <h3><span class="glyphicon glyphicon-question-sign error" aria-hidden="true"></span> <label>' + displayQuestion + '</label></h3>';
    }
    else  {
        html += '                   <h3><span class="glyphicon glyphicon-question-sign error" aria-hidden="true"></span> <label>' + voca.VMeaning + '</label></h3>';
    }
    //    //listening
    //else if (voca.TestSkill == '3') {
    //    html += '                   <button type="button" class="btn btn-default btn-lg" onclick="speak(\'' + voca.UrlAudio + '\');"><span class="glyphicon glyphicon-volume-up" aria-hidden="true">Nghe</span></button>';
    //}
    //html += '               </div>';
    //html += '               <div class="col-lg-8 col-md-8 col-xs-6">';
    html += '</div>';

    html += '</div>';
    //choosing
    if (voca.TestType == "1") {
        html += '<div class="row text-center">';
        html += '<label>Chọn từ đúng</label>';
        html += '<input type="hidden" id="correctValue" value="' + voca.CorrectResult + '"/>';
        html += '<input type="hidden" id="correctUrlAudio" value="' + voca.CorrectUrlAudio + '"/>';
        html += '<input type="hidden" id="selectedValue" />';
        html += '</div>'
        html += '</hr>';
        html += '<div class="row text-center">';
        html += '   <div class="col-lg-12 col-md-12 col-xs-12">';
        html += '       <a class="btn btn-quizz " href="#" id="result1" name="resultChoosing" onclick="selectValue(this, 1);return false;">1<br>' + voca.Result1 + '</a>';
        html += '       <input type="hidden" id="urlAudio1" />';
        html += '       <a class="btn btn-quizz " href="#" id="result2" name="resultChoosing" onclick="selectValue(this, 2);return false;">2<br>' + voca.Result2 + '</a>';
        html += '       <input type="hidden" id="urlAudio2" />';
        html += '   </div>';
        html += '</div>'
        html += '<div class="row text-center">';
        html += '   <div class="col-lg-12 col-md-12 col-xs-12">';
        html += '       <a class="btn btn-quizz " href="#" id="result3" name="resultChoosing" onclick="selectValue(this, 3);return false;">3<br>' + voca.Result3 + '</a>';
        html += '       <input type="hidden" id="urlAudio3" />';
        html += '       <a class="btn btn-quizz " href="#" id="result4" name="resultChoosing" onclick="selectValue(this, 4);return false;">4<br>' + voca.Result4 + '</a>';
        html += '       <input type="hidden" id="urlAudio4" />';
        html += '   </div>';
        html += '</div>'
        //html += '<div class="list-group">';
        //html += '   <a href="#" id="result1" class="list-group-item" name="resultChoosing" onclick="selectValue(this, 1);return false;">' + voca.Result1 + '</a><input type="hidden" id="urlAudio1" />';
        //html += '   <a href="#" id="result2" class="list-group-item" name="resultChoosing" onclick="selectValue(this, 2);return false;">' + voca.Result2 + '</a><input type="hidden" id="urlAudio2" />';
        //html += '   <a href="#" id="result3" class="list-group-item" name="resultChoosing" onclick="selectValue(this, 3);return false;">' + voca.Result3 + '</a><input type="hidden" id="urlAudio3" />';
        //html += '   <a href="#" id="result4" class="list-group-item" name="resultChoosing" onclick="selectValue(this, 4);return false;">' + voca.Result4 + '</a><input type="hidden" id="urlAudio4" />'
        //html += '</div>';
    }

    html += '               </div>';
    html += '           </div>';
    html += '       </div>';
    html += '   </div>';
    html += '</div>';

    return html;
};

function selectValue(obj, value) {
    if ($(obj).attr('disabled') == 'disabled') {
        return;
    }
    $('#selectedValue').val(value);
    checkInput(quizzVoca);
};

//function clearChoosing() {
//    $('a[name=resultChoosing]').each(function () {
//        $(this).prop('class', 'list-group-item');
//    });
//};

function searchVoca(index) {
    for (var i = 0; i < vocas.length; i++) {
        if (i == index) {
            return vocas[i];
        }
    }
};


function draw() {
    var voca = voca;
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