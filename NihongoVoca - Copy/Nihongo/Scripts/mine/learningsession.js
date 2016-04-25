
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

var maxLevel = 10;

var myTimer = null;

$(document).ready(function () {

    window.onbeforeunload = function confirmExit() {
        //            return "Kết quả bài kiểm tra sẽ không được ghi nhận. Bạn có muốn thoát trang?";
        if (vocas.length > 0 && currentIndex >= 0) {
            return "Kết quả bài học sẽ không được ghi nhận nếu bạn thoát khỏi trang";
        }
    }

    clearTimeout(myTimer);

    //
    $('.result').hide();
    $('.lesson').show();

    //var f = new Audio('/Content/media/fail.wav');
    //f.load();
    //var o = new Audio('/Content/media/tada.wav');
    //o.load();

    //ion.sound({
    //    sounds: [
    //        {
    //            name: 'boing'
    //        },
    //        {
    //            name: 'success'
    //        }
    //    ],
    //    // main config
    //    path: "/Content/media/",
    //    preload: true,
    //    multiplay: true,
    //    volume: 0.9
    //});


    currentIndex = 0;
    isPractice = false;

    //load datas
    if ($('#lt').val() == '1') {
        getTestVocas();
    }
    else if ($('#lt').val() == '2') {
        getPracticeVocas();
    }
    else if ($('#lt').val() == '3') {
        getNotebookVocas();
    }
    else if ($('#lt').val() == '4') {
        getPracticeCateVocas();
    }

    $('#btnCelebrateReview').on('click', function () {
        if ($('#id-celebrate').val() != '') {
            getPracticeCateVocas($('#id-celebrate').val());
            $('#modal-celebrate').modal('hide');
        }
    });

    $('#btnLearning').on('click', function () {
        if ($('#lt').val() == '4') {
            getPracticeCateVocas();
        }
        else {
            getTestVocas();
        }
    });

    $('#btnReview').on('click', function () {
        getPracticeVocas();
    });

    $('#btnNotebook').on('click', function () {
        getNotebookVocas();
    });

    $('#btnNext').on('click', function () {
        if (isPractice) {
            if (vocas[currentIndex].IsIgnore == '1') {
                if (isFinish()) {
                    sound('success');

                    //show result
                    currentIndex = -1;
                    currentLevel = totalLevel;
                    //showFlashCard(-1, false);

                    //update result
                    isPass = (correctVocas.length >= vocas.length * 8 / 10) ? true : false;
                    updateTestResult();

                    myTimer = setTimeout(showResultPage, 1500);
                    showProgress();
                }
                else {
                    if (isAllLearnt()) {
                        currentIndex = Math.floor((Math.random() * (vocas.length)) + 1) - 1;
                        while (vocas[currentIndex].Level == maxLevel || vocas[currentIndex].IsIgnore == '1') {
                            currentIndex = Math.floor((Math.random() * (vocas.length)) + 1) - 1;
                        }
                    }
                    else {
                        do {
                            currentIndex++;
                            if (currentIndex == vocas.length) {
                                currentIndex = 0;
                            }
                        } while ((vocas[currentIndex].IsDone == '1' && vocas[currentIndex].Level == maxLevel) || vocas[currentIndex].IsIgnore == '1');
                    }
                    if (currentIndex < vocas.length) {

                        quizzVoca = createQuizz(currentIndex);
                        quizzVoca.IsDone = '1';

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

                        myTimer = setTimeout(doWork, 300);
                        //showFlashCard(quizzVoca, false);

                        //if (isPractice) {
                        //    showProgress();
                        //}
                    }
                    else {
                        sound('success');
                        //show result
                        currentIndex = -1;
                        currentLevel = totalLevel;
                        //showFlashCard(-1, false);

                        //update result
                        isPass = (correctVocas.length >= vocas.length * 8 / 10) ? true : false;
                        updateTestResult();

                        myTimer = setTimeout(showResultPage, 1500);
                        showProgress();
                    }
                }

            }
            else {
                return false;
            }
        }
        else {
            if (vocas[currentIndex].IsIgnore == '1') {
                if (isFinish()) {
                    sound('success');

                    //show result
                    currentIndex = -1;
                    currentLevel = totalLevel;
                    //showFlashCard(-1, false);

                    //update result
                    isPass = (correctVocas.length >= vocas.length * 8 / 10) ? true : false;
                    updateTestResult();

                    myTimer = setTimeout(showResultPage, 1500);
                    showProgress();
                }
                else {
                    if (isAllLearnt()) {
                        currentIndex = Math.floor((Math.random() * (vocas.length)) + 1) - 1;
                        while (vocas[currentIndex].Level == maxLevel || vocas[currentIndex].IsIgnore == '1') {
                            currentIndex = Math.floor((Math.random() * (vocas.length)) + 1) - 1;
                        }
                    }
                    else {
                        do {
                            currentIndex++;
                            if (currentIndex == vocas.length) {
                                currentIndex = 0;
                            }
                        } while ((vocas[currentIndex].IsDone == '1' && vocas[currentIndex].Level == maxLevel) || vocas[currentIndex].IsIgnore == '1');
                    }
                    if (currentIndex < vocas.length) {

                        quizzVoca = createQuizz(currentIndex);
                        quizzVoca.IsDone = '1';

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

                        myTimer = setTimeout(doWork, 300);
                        //showFlashCard(quizzVoca, false);

                        //if (isPractice) {
                        //    showProgress();
                        //}
                    }
                    else {
                        sound('success');
                        //show result
                        currentIndex = -1;
                        currentLevel = totalLevel;
                        //showFlashCard(-1, false);

                        //update result
                        isPass = (correctVocas.length >= vocas.length * 8 / 10) ? true : false;
                        updateTestResult();

                        myTimer = setTimeout(showResultPage, 1500);
                        showProgress();
                    }
                }
                
            }
            else {
                isPractice = true;
                quizzVoca = createQuizz(currentIndex);

                myTimer = setTimeout(doWork, 300);
                //showFlashCard(quizzVoca, false);

                //if (isPractice) {
                //    showProgress();
                //}
            }

            //showFlashCard(quizzVoca, false);
        }
        //if (isPractice) {
        //    //if (isFinish()) {
        //    //    //show result
        //    //    currentIndex = -1;
        //    //    currentLevel = totalLevel;
        //    //    //showFlashCard(-1, false);

        //    //    showResultPage();
        //    //    showProgress();

        //    //    //update result
        //    //    isPass = (correctVocas.length >= vocas.length * 8 / 10) ? true : false;
        //    //    updateTestResult();
        //    //}
        //    //else {
        //    //    if (isAllLearnt()) {
        //    //        currentIndex = Math.floor((Math.random() * (vocas.length)) + 1) - 1;
        //    //        while (vocas[currentIndex].Level == maxLevel && vocas[currentIndex].IsIgnore == '1') {
        //    //            currentIndex = Math.floor((Math.random() * (vocas.length)) + 1) - 1;
        //    //        }
        //    //    }
        //    //    else {
        //    //        do {
        //    //            currentIndex++;
        //    //            if (currentIndex == vocas.length) {
        //    //                currentIndex = 0;
        //    //            }
        //    //        } while ((vocas[currentIndex].IsDone == '1' && vocas[currentIndex].Level == maxLevel) || vocas[currentIndex].IsIgnore == '1');
        //    //    }


        //    //}

        //    quizzVoca = createQuizz(currentIndex);
        //    showFlashCard(quizzVoca, false);
        //}
        //else {
        //    showFlashCard(quizzVoca, true);
        //}
        //checkInput();
    });

    $('#modalKanjiImage').on('shown.bs.modal', function () {
        $('#txtUserDefine').val($('#divNote').html());
        $('#txtUserDefine').focus();
    })

    $('#btnSaveNote').on('click', function () {
        var userDefine = $('#txtUserDefine').val();
        if (userDefine) {
            quizzVoca.UserDefine = userDefine;
            vocas[currentIndex].UserDefine = userDefine;

            $('#divNote').html(userDefine);
        }
        $('#modalKanjiImage').modal('hide');
    });

    $('#modalKanjiImage').keydown(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == 13) {
            $("#btnSaveNote").trigger("click");
            return false;
        }
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
                    sound(quizzVoca.UrlAudio);
                    return false;
                }
            }
        }
            //space
        else if (keycode == 32) {
            if (!isPractice) {
                sound(quizzVoca.UrlAudio);
                return false;
            }
        }
            //enter
        else if (keycode == 13) {
            if (currentIndex == -1) {
                var disLe = $("#btnLearning").is(":disabled");
                var disRe = $("#btnReview").is(":disabled");
                var disNo = $("#btnNotebook").is(":disabled");
                var disReCate = $("#btnReviewCate").is(":disabled");
                //load datas
                if ($('#lt').val() == '1') {
                    if (disLe) {
                        if (!disRe) {
                            getPracticeVocas();
                        }
                    }
                    else {
                        getTestVocas();
                    }
                }
                else if ($('#lt').val() == '2') {
                    if (disRe) {
                        if (!disLe) {
                            getTestVocas();
                        }
                    }
                    else {
                        getPracticeVocas();
                    }
                }
                else if ($('#lt').val() == '3') {
                    if (disNo) {
                        if (disLe) {
                            if (!disRe) {
                                getPracticeVocas();
                            }
                        }
                        else {
                            getTestVocas();
                        }
                    }
                    else {
                        getNotebookVocas();
                    }
                }
                else if ($('#lt').val() == '4') {
                    getPracticeCateVocas();
                }
            }
            else {
                $("#btnNext").trigger("click");
            }
            return false;
        }
        else if (keycode == 49 || keycode == 97) {
            if (isPractice) {
                selectChoosingResult(1);
            }
            return false;
        } else if (keycode == 50 || keycode == 98) {
            if (isPractice) {
                selectChoosingResult(2);
            }
            return false;
        } else if (keycode == 51 || keycode == 99) {
            if (isPractice) {
                selectChoosingResult(3);
            }
            return false;
        } else if (keycode == 52 || keycode == 100) {
            if (isPractice) {
                selectChoosingResult(4);
            }
            return false;
        }
        //->
        //else if (keycode == 39) {
        //    $("#flashNext").trigger("click");
        //    return false;
        //}
        //    //<-
        //else if (keycode == 37) {
        //    $("#flashPre").trigger("click");
        //    return false;
        //}
        //space
        //if (keycode == 32) {
        //    if (currentIndex >= 0 && currentIndex < vocas.length) {
        //        if (voca.TestSkill == '1') {
        //            sound(voca.UrlAudio);
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
    //for (var i = 0; i < vocas.length; i++) {
    //    vocas[i].CompletedTime = completedTime;
    //}
    if (vocas.length > 0) {
        $.ajax({
            cache: false,
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
                debugger;
                if (result.Result1 == result.Result2) {
                    $("#btnLearning").attr("disabled", true);
                } else {
                    $("#btnLearning").removeAttr("disabled");
                }
                if (result.Result4 < 4) {
                    $("#btnReview").attr("disabled", true);
                } else {
                    $("#btnReview").removeAttr("disabled");
                    $('#spanReview').html('<strong>ÔN TẬP (' + result.Result4 + ')</strong>');
                }
                if (result.Result3 < 4) {
                    $("#btnNotebook").attr("disabled", true);
                } else {
                    $("#btnNotebook").removeAttr("disabled");
                    $('#spanNotebook').html('<strong>ÔN SỔ TAY (' + result.Result3 + ')</strong>');
                }

                if (result.Result5 != '' && result.Result5 > 0) {
                    
                    $('#id-celebrate').val(result.Result5);
                    $('#info-celebrate').html(result.Result6);
                    $('#modal-celebrate').modal();
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
    $('.result').hide();
    $('.lesson').show();

    vocas = [];
    totalLevel = 0;
    currentLevel = 0;
    isPractice = true;
    currentIndex = 0;
    //        failArray = [];
    showProgress()

    $.ajax({
        cache: false,
        type: "get",
        async: true,
        url: '/Library/' + $('#gtv').val(),
        data: { "id": $('#vsd').val(), "isKanji": $('#ik').val() },
        dataType: "json",
        success: function (result) {
            if (result.returnCode == $('#accessDenied').val()) {
                window.location.href = '/Account/RequireLogin';
            } else {
                var vocaSounds = [];
                $.each(result.vocabularies, function (i, voca) {
                    vocas.push(voca);
                    //                        failArray.push(voca);
                    //if (voca.TestSkill == '3') {
                    if (voca.DisplayType != '3') {
                        //var audio = new Audio(voca.UrlAudio);
                        //audio.load();
                        if (voca.UrlAudio) {
                            var item = {};
                            if (voca.DisplayType == '1') {
                                item = {
                                    name: voca.UrlAudio,
                                    path: "/Content/media/hiragana/"
                                };
                            }
                            else if (voca.DisplayType == '2') {
                                item = {
                                    name: voca.UrlAudio,
                                    path: "/Content/media/katakana/"
                                };
                            }
                            //item.name = voca.UrlAudio;
                            vocaSounds.push(item);
                        }
                    }
                    //}

                    //Calculate total Level
                    if (voca.Level == 10 && voca.IsDone == '0') {
                        totalLevel += 2;
                    }
                    else {
                        totalLevel += parseInt(10 - voca.Level);
                    }
                });

                vocaSounds.push({
                    name: 'boing',
                    path: '/content/media/'
                });
                vocaSounds.push({
                    name: 'success',
                    path: '/content/media/'
                });
                console.log(JSON.stringify(vocaSounds));
                // init bunch of sounds
                ion.sound({
                    sounds: (vocaSounds),
                    // main config
                    //path: "/Content/media/hiragana/",
                    preload: true,
                    multiplay: true,
                    volume: 0.9
                });

                completedTime = 0;
                currentIndex = 0;


                //create quizz voca
                quizzVoca = createQuizz(currentIndex);

                if (quizzVoca.IsDone == "1") {
                    isPractice = true;
                }
                else if (quizzVoca.Level == "0") {
                    isPractice = false;
                }
                else if (quizzVoca.Level < 10) {
                    isPractice = true;
                }
                else if (quizzVoca.Level == 10 && quizzVoca.IsDone == '0') {
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
    $('.result').hide();
    $('.lesson').show();

    vocas = [];
    totalLevel = 0;
    currentLevel = 0;
    isPractice = true;
    currentIndex = 0;
    //        failArray = [];
    showProgress()

    $.ajax({
        cache: false,
        type: "get",
        async: true,
        url: '/Library/' + $('#gpr').val(),
        data: { "id": $('#vsd').val(), "isKanji": $('#ik').val() },
        dataType: "json",
        success: function (result) {
            if (result.returnCode == $('#accessDenied').val()) {
                window.location.href = '/Account/RequireLogin';
            } else {
                $.each(result.vocabularies, function (i, voca) {
                    vocas.push(voca);
                    //                        failArray.push(voca);
                    //if (voca.TestSkill == '3') {
                    if (voca.DisplayType != '3') {
                        //var audio = new Audio(voca.UrlAudio);
                        //audio.load();
                        if (voca.UrlAudio) {
                            var item = {};
                            if (voca.DisplayType == '1') {
                                item = {
                                    name: voca.UrlAudio,
                                    path: "/Content/media/hiragana/"
                                };
                            }
                            else if (voca.DisplayType == '2') {
                                item = {
                                    name: voca.UrlAudio,
                                    path: "/Content/media/katakana/"
                                };
                            }
                            //item.name = voca.UrlAudio;
                            vocaSounds.push(item);
                        }
                    }
                    //}

                    //Calculate total Level
                    if (voca.Level == 10 && voca.IsDone == '0') {
                        totalLevel += 2;
                    }
                    else {
                        totalLevel += parseInt(10 - voca.Level);
                    }
                });

                vocaSounds.push({
                    name: 'boing',
                    path: '/content/media/'
                });
                vocaSounds.push({
                    name: 'success',
                    path: '/content/media/'
                });
                //console.log(JSON.stringify(vocaSounds));
                // init bunch of sounds
                ion.sound({
                    sounds: (vocaSounds),
                    // main config
                    path: "/Content/media/hiragana/",
                    preload: true,
                    multiplay: true,
                    volume: 0.9
                });

                completedTime = 0;
                currentIndex = 0;

                //create quizz voca
                quizzVoca = createQuizz(currentIndex);

                if (quizzVoca.IsDone == "1") {
                    isPractice = true;
                }
                else if (quizzVoca.Level == "0") {
                    isPractice = false;
                }
                else if (quizzVoca.Level < 10) {
                    isPractice = true;
                }
                else if (quizzVoca.Level == 10 && quizzVoca.IsDone == '0') {
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

function getNotebookVocas() {
    $('.result').hide();
    $('.lesson').show();

    vocas = [];
    totalLevel = 0;
    currentLevel = 0;
    isPractice = true;
    currentIndex = 0;
    //        failArray = [];
    showProgress()

    $.ajax({
        cache: true,
        type: "get",
        async: true,
        url: '/Library/' + $('#gnk').val(),
        data: { "id": $('#vsd').val(), "isKanji": $('#ik').val() },
        dataType: "json",
        success: function (result) {
            if (result.returnCode == $('#accessDenied').val()) {
                window.location.href = '/Account/RequireLogin';
            } else {
                $.each(result.vocabularies, function (i, voca) {
                    vocas.push(voca);
                    //                        failArray.push(voca);
                    //if (voca.TestSkill == '3') {
                    if (voca.DisplayType != '3') {
                        //var audio = new Audio(voca.UrlAudio);
                        //audio.load();
                        if (voca.UrlAudio) {
                            var item = {};
                            if (voca.DisplayType == '1') {
                                item = {
                                    name: voca.UrlAudio,
                                    path: "/Content/media/hiragana/"
                                };
                            }
                            else if (voca.DisplayType == '2') {
                                item = {
                                    name: voca.UrlAudio,
                                    path: "/Content/media/katakana/"
                                };
                            }
                            //item.name = voca.UrlAudio;
                            vocaSounds.push(item);
                        }
                    }
                    //}

                    //Calculate total Level
                    if (voca.Level == 10 && voca.IsDone == '0') {
                        totalLevel += 2;
                    }
                    else {
                        totalLevel += parseInt(10 - voca.Level);
                    }
                });
                vocaSounds.push({
                    name: 'boing',
                    path: '/content/media/'
                });
                vocaSounds.push({
                    name: 'success',
                    path: '/content/media/'
                });
                // init bunch of sounds
                ion.sound({
                    sounds: (vocaSounds),
                    // main config
                    path: "/Content/media/hiragana/",
                    preload: true,
                    multiplay: true,
                    volume: 0.9
                });

                completedTime = 0;
                currentIndex = 0;
                
                //create quizz voca
                quizzVoca = createQuizz(currentIndex);

                if (quizzVoca.IsDone == "1") {
                    isPractice = true;
                }
                else if (quizzVoca.Level == "0") {
                    isPractice = false;
                }
                else if (quizzVoca.Level < 10) {
                    isPractice = true;
                }
                else if (quizzVoca.Level == 10 && quizzVoca.IsDone == '0') {
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

function getPracticeCateVocas(id) {
    $('.result').hide();
    $('.lesson').show();

    vocas = [];
    totalLevel = 0;
    currentLevel = 0;
    isPractice = true;
    currentIndex = 0;
    //        failArray = [];
    showProgress()

    $.ajax({
        cache: false,
        type: "get",
        async: true,
        url: '/Library/' + $('#gcv').val(),
        data: { "id": ((typeof id === "undefined" || id == '') ? $('#cid').val() : id), "isKanji": $('#ik').val() },
        dataType: "json",
        success: function (result) {
            if (result.returnCode == $('#accessDenied').val()) {
                window.location.href = '/Account/RequireLogin';
            } else {
                $.each(result.vocabularies, function (i, voca) {
                    vocas.push(voca);
                    //                        failArray.push(voca);
                    //if (voca.TestSkill == '3') {
                    if (voca.DisplayType != '3') {
                        var audio = new Audio(voca.UrlAudio);
                        audio.load();
                    }
                    //}

                    //Calculate total Level
                    if (voca.Level == 10 && voca.IsDone == '0') {
                        totalLevel += 2;
                    }
                    else {
                        totalLevel += parseInt(10 - voca.Level);
                    }
                });

                completedTime = 0;
                currentIndex = 0;

                //create quizz voca
                quizzVoca = createQuizz(currentIndex);

                if (quizzVoca.IsDone == "1") {
                    isPractice = true;
                }
                else if (quizzVoca.Level == "0") {
                    isPractice = false;
                }
                else if (quizzVoca.Level < 10) {
                    isPractice = true;
                }
                else if (quizzVoca.Level == 10 && quizzVoca.IsDone == '0') {
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

function selectChoosingResult(no) {
    switch (no) {
        case 1:
            $('#selectedValue').val(1);
            break;
        case 2:
            $('#selectedValue').val(2);
            break;
        case 3:
            $('#selectedValue').val(3);
            break;
        case 4:
            $('#selectedValue').val(4);
            break;
        default:
    }

    checkInput();
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
//                    //sound
//                    sound('/Content/media/fail.wav');

//                    vocas[currentIndex].IsCorrect = "0";

//                    //inCorrectVocas.push(vocas[currentIndex]);

//                }
//                else {
//                    //sound corrent voca
//                    sound('/Content/media/tada.wav');

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

function clearAnswer() {
    if ($('#result1').hasClass('quizz-wrong')) {
        $('#result1').removeClass('quizz-wrong');
    }
    if ($('#result2').hasClass('quizz-right')) {
        $('#result2').removeClass('quizz-right');
    }

    if ($('#result3').hasClass('quizz-wrong')) {
        $('#result3').removeClass('quizz-wrong');
    }
    if ($('#result3').hasClass('quizz-right')) {
        $('#result3').removeClass('quizz-right');
    }

    if ($('#result4').hasClass('quizz-wrong')) {
        $('#result4').removeClass('quizz-wrong');
    }
    if ($('#result4').hasClass('quizz-right')) {
        $('#result4').removeClass('quizz-right');
    }
}
function checkInput() {

    if (isPractice) {
        var quizzTestSkill = quizzVoca.TestSkill.toString();
        var displayType = quizzVoca.DisplayType.toString();
        //alert(quizzTestSkill);
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
                    //sound
                    sound('boing');

                    quizzVoca.IsCorrect = "0";
                    quizzVoca.NumOfWrong += 1;
                    //inCorrectVocas.push(voca);

                    var level = parseInt(quizzVoca.Level) - 1;
                    if (level < 0) {
                        level = 0;
                    }
                    quizzVoca.Level = level;

                    currentLevel -= 1;
                    if (level < 0) {
                        currentLevel += 1;
                    }
                }
                else {
                    //sound corrent voca
                    //sound('/Content/media/tada.wav');

                    quizzVoca.IsCorrect = "1";
                    //quizzVoca.NumOfWrong -= 1;

                    quizzVoca.Point += 1;

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
                $(".heart1").removeClass("btn-primary btn-default");
                if (quizzVoca.Level > 0) {
                    $(".heart1").addClass("btn-primary");
                }
                else {
                    $(".heart1").addClass("btn-default");
                }
                $(".heart2").removeClass("btn-primary btn-default");
                if (quizzVoca.Level > 2) {
                    $(".heart2").addClass("btn-primary");
                }
                else {
                    $(".heart2").addClass("btn-default");
                }
                $(".heart3").removeClass("btn-primary btn-default");
                if (quizzVoca.Level > 4) {
                    $(".heart3").addClass("btn-primary");
                }
                else {
                    $(".heart3").addClass("btn-default");
                }
                $(".heart4").removeClass("btn-primary btn-default");
                if (quizzVoca.Level > 6) {
                    $(".heart4").addClass("btn-primary");
                }
                else {
                    $(".heart4").addClass("btn-default");
                }
                $(".heart5").removeClass("btn-primary btn-default");
                if (quizzVoca.Level > 8) {
                    $(".heart5").addClass("btn-primary");
                }
                else {
                    $(".heart5").addClass("btn-default");
                }

                //console.log(quizzVoca.Romaji + ' - ' + quizzVoca.Level);

                //next
                //if (currentIndex < vocas.length) {
                if (quizzVoca.IsCorrect == "0") {
                    switch (quizzVoca.CorrectResult) {
                        case 1:
                            $('#result1').addClass('quizz-right');
                            $('#result1').html('1 <span class="glyphicon glyphicon-ok "></span><br>' + quizzVoca.Result1);
                            break;
                        case 2:
                            $('#result2').addClass('quizz-right');
                            $('#result2').html('2 <span class="glyphicon glyphicon-ok "></span><br>' + quizzVoca.Result2);
                            break;
                        case 3:
                            $('#result3').addClass('quizz-right');
                            $('#result3').html('3 <span class="glyphicon glyphicon-ok "></span><br>' + quizzVoca.Result3);
                            break;
                        case 4:
                            $('#result4').addClass('quizz-right');
                            $('#result4').html('4 <span class="glyphicon glyphicon-ok "></span><br>' + quizzVoca.Result4);
                            break;
                        default:
                    }
                    switch (selectedValue) {
                        case "1":
                            $('#result1').addClass('quizz-wrong');
                            $('#result1').html('1 <span class="glyphicon glyphicon-remove "></span><br>' + quizzVoca.Result1);
                            break;
                        case "2":
                            $('#result2').addClass('quizz-wrong');
                            $('#result2').html('2 <span class="glyphicon glyphicon-remove "></span><br>' + quizzVoca.Result2);
                            break;
                        case "3":
                            $('#result3').addClass('quizz-wrong');
                            $('#result3').html('3 <span class="glyphicon glyphicon-remove "></span><br>' + quizzVoca.Result3);
                            break;
                        case "4":
                            $('#result4').addClass('quizz-wrong');
                            $('#result4').html('4 <span class="glyphicon glyphicon-remove "></span><br>' + quizzVoca.Result4);
                            break;
                        default:
                    }

                    $("a[name='resultChoosing']").attr('disabled', true);


                    //switch to mode Learning
                    isPractice = false;

                    myTimer = setTimeout(doWork, 1000);
                    //showFlashCard(currentIndex, false);

                }
                else {
                    //console.log(quizzTestType);
                    switch (quizzVoca.CorrectResult) {
                        case 1:
                            $('#result1').addClass('quizz-right');
                            $('#result1').html('1 <span class="glyphicon glyphicon-ok "></span><br>' + quizzVoca.Result1);
                            if (displayType != '3' && quizzTestSkill == '1' && $('#urlAudio1').val() != '') {
                                sound($('#urlAudio1').val());
                            }
                            break;
                        case 2:
                            $('#result2').addClass('quizz-right');
                            $('#result2').html('2 <span class="glyphicon glyphicon-ok "></span><br>' + quizzVoca.Result2);
                            if (displayType != '3' && quizzTestSkill == '1' && $('#urlAudio2').val() != '') {
                                sound($('#urlAudio2').val());
                            }
                            break;
                        case 3:
                            $('#result3').addClass('quizz-right');
                            $('#result3').html('3 <span class="glyphicon glyphicon-ok "></span><br>' + quizzVoca.Result3);
                            if (displayType != '3' && quizzTestSkill == '1' && $('#urlAudio3').val() != '') {
                                sound($('#urlAudio3').val());
                            }
                            break;
                        case 4:
                            $('#result4').addClass('quizz-right');
                            $('#result4').html('4 <span class="glyphicon glyphicon-ok "></span><br>' + quizzVoca.Result4);
                            if (displayType != '3' && quizzTestSkill == '1' && $('#urlAudio4').val() != '') {
                                sound($('#urlAudio4').val());
                            }
                            break;
                        default:
                    }

                    $("a[name='resultChoosing']").attr('disabled', true);


                    if (isFinish()) {
                        sound('success');

                        //show result
                        currentIndex = -1;
                        currentLevel = totalLevel;
                        //showFlashCard(-1, false);

                        //update result
                        isPass = (correctVocas.length >= vocas.length * 8 / 10) ? true : false;
                        updateTestResult();
        
                        myTimer = setTimeout(showResultPage, 1500);
                        showProgress();

                    }
                    else {
                        if (isAllLearnt()) {
                            currentIndex = Math.floor((Math.random() * (vocas.length)) + 1) - 1;
                            while (vocas[currentIndex].Level == maxLevel || vocas[currentIndex].IsIgnore == '1') {
                                currentIndex = Math.floor((Math.random() * (vocas.length)) + 1) - 1;
                            }
                        }
                        else {
                            do {
                                currentIndex++;
                                if (currentIndex == vocas.length) {
                                    currentIndex = 0;
                                }
                            } while ((vocas[currentIndex].IsDone == '1' && vocas[currentIndex].Level == maxLevel) || vocas[currentIndex].IsIgnore == '1');

                            //while (currentIndex < vocas.length && (vocas[currentIndex].IsDone == '1' && vocas[currentIndex].Level == maxLevel)) {
                            //    currentIndex++;
                            //    if (currentIndex == vocas.length) {
                            //        currentIndex = 0;
                            //    }
                            //}
                        }

                        if (currentIndex < vocas.length) {

                            quizzVoca = createQuizz(currentIndex);
                            quizzVoca.IsDone = '1';

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

                            if (displayType != '3' && quizzTestSkill == '1') {
                                myTimer = setTimeout(doWork, 1500);
                            }
                            else {
                                myTimer = setTimeout(doWork, 1000);
                            }
                            //showFlashCard(quizzVoca, false);
                            showProgress();
                        }
                        else {
                            sound('success');
                            //show result
                            currentIndex = -1;
                            currentLevel = totalLevel;
                            //showFlashCard(-1, false);

                            //update result
                            isPass = (correctVocas.length >= vocas.length * 8 / 10) ? true : false;
                            updateTestResult();

                            myTimer = setTimeout(showResultPage, 1500);
                            showProgress();

                        }
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
        quizzVoca.IsDone = "1";

        isPractice = true;
        showFlashCard(currentIndex, false);
    }

}


function isAllLearnt() {
    var result = true;
    for (var i = 0; i < vocas.length; i++) {
        if ((vocas[i].IsIgnore == '0' && vocas[i].IsDone == '0')) {
            result = false;
            break;
        }
    }
    return result;
}

function isFinish() {
    var result = true;
    for (var i = 0; i < vocas.length; i++) {
        //console.log(vocas[i].Hiragana +  ' - '+ vocas[i].HasLearnt + ' - ' + vocas[i].Level);
        if ((vocas[i].IsDone == '0' || vocas[i].Level < maxLevel) && vocas[i].IsIgnore == '0') {
            result = false;
            break;
        }
    }
    return result;
}

function showProgress() {
    var progressHtml = '<div class="progress-bar progress-bar-primary progress-bar-striped active" role="progressbar" aria-valuenow="' + (currentLevel)
                    + '" aria-valuemin="0" '
                    + '" aria-valuemax="' + (totalLevel)
                    + '" style="width: ' + (totalLevel == 0 ? 0 : (currentLevel / totalLevel * 100)) + '%;">'
                    //+ (index + 1) + '/' + (vocas.length)
                    + '</div>';
    //console.log(currentLevel + '/' + totalLevel);
    $('#progress').html(progressHtml);
}

function showResultPage() {
    //SHOW RESULT
    var urlLearning = $('#ale').val();//'@Url.Action("hoc-tu-vung", "Library", new { id = @ViewBag.CategoryID, urlDisplay = @ViewBag.CategoryUrlDisplay})';
    var urlVoca = $('#av').val();//'@Url.Action("danh-muc", "Library", new { id = @ViewBag.CategoryID, urlDisplay = @ViewBag.CategoryUrlDisplay})';

    //result
    var numOfOK = parseInt(vocas.length * 8 / 10);
    var html = '';

    //html += '<div class="col-md-12 padding-0">';
    //html += '   <button class="btn ripple btn-outline btn-primary require-login" onclick="location.reload(true);">';
    //html += '   <div><span><strong>TIẾP TỤC</strong></span><span class="ink animate" ></span>';
    //html += '   </div>';
    //html += '   </button>';
    //html += '</div>';
    //html += '<div class="col-md-12 padding-0">';
    //html += '   <button class="btn ripple btn-outline btn-primary require-login" onclick="getPracticeVocas(); return false;">';
    //html += '   <div><span><strong>ÔN TẬP</strong></span><span class="ink animate" ></span>';
    //html += '   </div>';
    //html += '   </button>';
    //html += '</div>';
    //html += '<div class="col-md-12 padding-0">';
    //html += '   <button class="btn ripple btn-outline btn-primary require-login" onclick="getNotebookVocas(); return false;">';
    //html += '   <div><span><strong>ÔN SỔ TAY</strong></span><span class="ink animate" ></span>';
    //html += '   </div>';
    //html += '   </button>';
    //html += '</div>';

    //html += '<div id="carousel-example-generic" class="carousel slide" data-ride="carousel">';
    //html += '   <div class="carousel-inner" role="listbox">';
    //html += '       <div class="item active">';
    ////html += '           <div class="row text-center">';
    //////            html += '               <div class="col-lg-4 col-md-4 col-xs-6">';
    //////            html += '                   <img class="img-rounded" src="' + getLink(voca.UrlImage) + '" alt="Từ vựng tiếng Nhật" height="300px" width="100%"/>';
    //////            html += '               </div>';
    ////html += '               <div class="col-lg-12">';
    ////html += '                   <p class="text-info">Số câu đúng: ' + correctVocas.length + '/' + vocas.length + '</p>';
    ////html += '                   <p class="text-info">Thời gian hoàn thành: ' + completedTime + ' giây</p>';
    ////html += '                   <p class="text-info">Kết quả: ' + (correctVocas.length >= numOfOK ? "Chúc mừng bạn đã vượt qua được bài kiểm tra" : "Bạn đã không vượt qua được bài kiểm tra. Hãy ôn lại") + '</p>';
    ////html += '               </div>';
    ////html += '           </div>';
    //html += '           </br>';
    //html += '           <div class="row text-center">';
    //html += '               <div class="col-lg-12">';
    //html += '                   <a class="btn btn-navigator btn-lg require-login" href="#" onclick="location.reload(true); return false;">HỌC TIẾP</a>';
    //html += '                   <a class="btn btn-navigator btn-lg require-login" href="#" onclick="getPracticeVocas(); return false;">ÔN TẬP</a>';
    ////if (correctVocas.length < numOfOK) {
    ////    //html += '                   <a href="#" role="button" class="btn btn-navigator btn-lg" onclick="showResult(0); return false;">Xem kết quả</a>';
    ////    html += '                   <a href="' + urlLearning + '" role="button" class="btn btn-navigator btn-lg" >Ôn lại</a>';
    ////}
    ////else {
    ////    //html += '                   <a href="#" role="button" class="btn btn-navigator btn-lg" onclick="showResult(0); return false;">Xem kết quả</a>';
    ////    html += '                   <a href="' + urlVoca + '" class="btn btn-navigator btn-lg" role="button" >Trở về</a>';
    ////}
    //html += '               </div>';
    //html += '           </div>';
    //html += '           </br>';
    //html += '           <div class="row text-center">';
    //html += '               <div class="col-lg-12">';
    //html += '                   <a class="btn btn-navigator btn-lg require-login" href="' + urlLearning + '">ÔN SỔ TAY</a>';
    //html += '                   <a class="btn btn-navigator btn-lg require-login" href="' + urlVoca + '">TRỞ VỀ</a>';
    //html += '               </div>';
    //html += '           </div>';
    //html += '           <hr>';
    //html += '           <div class="row">';
    //html += '               <div class="col-lg-2">';
    //html += '               </div>';
    //html += '               <div class="col-lg-8">';
    //html += '                   <h3><label>CÁC TỪ VỪA HỌC</label></h3>';
    //html += '<table class="table table-condensed table-hover">';

    $('#tblLearntWord').html('');
    for (var i = 0; i < vocas.length; i++) {
        var row = '';
        if (vocas[i].DisplayType == '3') {
            row += '<tr>';
            row += '<td class="hidden-xs"><strong>' + (i + 1) + '</strong></td>';
            row += '<td>' + vocas[i].Kanji + '</td>';
            row += '<td>' + vocas[i].Pinyin + '</td>';
            row += '<td>' + vocas[i].VMeaning + '</td>';
            row += '<td>';
            row += '<button class=" btn btn-circle btn-mn btn-' + (vocas[i].Level > 1 ? 'primary' : 'default') + '" style="width:15px;height:15px;">';
            row += '<span class="fa fa-heart"></span>';
            row += '</button>';
            row += '<button class=" btn btn-circle btn-mn btn-' + (vocas[i].Level > 3 ? 'primary' : 'default') + '" style="width:15px;height:15px;">';
            row += '<span class="fa fa-heart"></span>';
            row += '</button>';
            row += '<button class=" btn btn-circle btn-mn btn-' + (vocas[i].Level > 5 ? 'primary' : 'default') + '" style="width:15px;height:15px;">';
            row += '<span class="fa fa-heart"></span>';
            row += '</button>';
            row += '<button class=" btn btn-circle btn-mn btn-' + (vocas[i].Level > 7 ? 'primary' : 'default') + '" style="width:15px;height:15px;">';
            row += '<span class="fa fa-heart"></span>';
            row += '</button>';
            row += '<button class=" btn btn-circle btn-mn btn-' + (vocas[i].Level > 9 ? 'primary' : 'default') + '" style="width:15px;height:15px;">';
            row += '<span class="fa fa-heart"></span>';
            row += '</button>';

            row += '</td>';
            row += '</tr>';
        }
        else {
            row += '<tr>';
            row += '<td class="hidden-xs"><strong>' + (i + 1) + '</strong></td>';
            row += '<td>' + (vocas[i].DisplayType == '1' ? vocas[i].Hiragana : vocas[i].Katakana) + '</td>';
            row += '<td class="hidden-xs">' + (vocas[i].DisplayType == '1' ? vocas[i].Romaji : vocas[i].Romaji_Katakana) + '</td>';
            row += '<td>' + vocas[i].Kanji + '</td>';
            row += '<td>' + vocas[i].VMeaning + '</td>';

            //var levelPercent = vocas[i].Level / 10 * 100;
            row += '<td>';
            //row += '<div class="progress progress-small">';
            //row += '    <div class="progress-bar progress-bar-' + (levelPercent == 100 ? 'primary' : 'danger') + '" role="progressbar" aria-valuenow="' + levelPercent + '" aria-valuemin="0" aria-valuemax="100" style="width: ' + levelPercent + '%"></div>';
            //row += '</div>';
            row += '<button class=" btn btn-circle btn-mn btn-' + (vocas[i].Level > 1 ? 'primary' : 'default') + '" style="width:15px;height:15px;">';
            row += '<span class="fa fa-heart"></span>';
            row += '</button>';
            row += '<button class=" btn btn-circle btn-mn btn-' + (vocas[i].Level > 3 ? 'primary' : 'default') + '" style="width:15px;height:15px;">';
            row += '<span class="fa fa-heart"></span>';
            row += '</button>';
            row += '<button class=" btn btn-circle btn-mn btn-' + (vocas[i].Level > 5 ? 'primary' : 'default') + '" style="width:15px;height:15px;">';
            row += '<span class="fa fa-heart"></span>';
            row += '</button>';
            row += '<button class=" btn btn-circle btn-mn btn-' + (vocas[i].Level > 7 ? 'primary' : 'default') + '" style="width:15px;height:15px;">';
            row += '<span class="fa fa-heart"></span>';
            row += '</button>';
            row += '<button class=" btn btn-circle btn-mn btn-' + (vocas[i].Level > 9 ? 'primary' : 'default') + '" style="width:15px;height:15px;">';
            row += '<span class="fa fa-heart"></span>';
            row += '</button>';

            row += '</td>';
            row += '</tr>';
        }
        $('#tblLearntWord').append(row);
    }
    //html += '</table>';
    //html += '               </div>';
    //html += '               <div class="col-lg-2">';
    //html += '               </div>';
    //html += '           </div>';
    //html += '       </div>';
    //html += '   </div>';
    //html += '</div>';

    //$('#flashCard').html(html);

    $('.result').show();
    $('.lesson').hide();
}

function showFlashCard(index, voice) {
    $('#btnNext').show();
    $("#btnNext").removeAttr('disabled');
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

            voca.IsDone = '1';

            //If Learning
            if (!isPractice) {

                voca.HasLearnt = '1';

                var html = showLearning(voca);

                $('#flashCard').html(html);

                sound(voca.UrlAudio);

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
                    sound(voca.UrlAudio);
                }

            }
        }
    }
};

function showFlashCard(voca, voice) {

    $('#flashCard').hide();
    //$("#flashCard").animate({ width: 'toggle' }, 500);
    $('#flashCard').show("slide", { direction: "right" }, 500);

    if (voca != null) {
        //If Learning
        if (!isPractice) {
            $('#btnNext').show();

            vocas[currentIndex].HasLearnt = '1';
            vocas[currentIndex].IsDone = '1';

            var html = showLearning(voca);

            $('#flashCard').html(html);

            if (voca.DisplayType != '3') {
                sound(voca.UrlAudio);
            }

            //isPractice = true;
        }
        else {
            $('#btnNext').hide();

            //If Practice
            var html = showPractise(voca);
            $('#flashCard').html(html);

            //listening skill
            //if (voca.TestSkill == '3') {
            //    sound(voca.UrlAudio);
            //}

        }
    }
};

function doWork() {
    showFlashCard(quizzVoca, false);
    clearTimeout(myTimer);
}

function createQuizz(index) {
    //console.log(index);
    var item = vocas[index];
    if (item) {
        var n1 = Math.floor((Math.random() * 4) + 1);
        var n2 = 1;
        var n3 = 2;
        var n4 = 3;

        item.TestType = 1;
        item.TestSkill = Math.floor((Math.random() * 2) + 1);

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
                    item.Result1 = item.DisplayType == '3'
                            ? (item.Pinyin + '<hr class="hr-quizz">' + item.VMeaning)
                            : item.VMeaning;
                }
                else {
                    //transanlating && listening
                    item.Result1 = item.DisplayType == '3'
                                    ? item.Kanji
                                    : item.DisplayType == "2" ? (item.Katakana + '<hr class="hr-quizz">') : (item.Hiragana + '<hr class="hr-quizz">' + item.Kanji);
                }
                item.ResultUrlAudio1 = item.UrlAudio;

                while (item.VocabularyCode == vocas[n2].VocabularyCode) {
                    n2 = Math.floor((Math.random() * (vocas.length)) + 1) - 1;
                }
                item.Hiragana2 = vocas[n2].Hiragana;
                item.Katakana2 = vocas[n2].Katakana;
                item.Kanji2 = vocas[n2].Kanji;
                if (item.TestSkill == 2) {
                    //reading
                    item.Result2 = vocas[n2].DisplayType == '3'
                            ? (vocas[n2].Pinyin + '<hr class="hr-quizz">' + vocas[n2].VMeaning)
                            : vocas[n2].VMeaning;
                }
                else {
                    //transanlating && listening
                    item.Result2 = vocas[n2].DisplayType == '3'
                                    ? vocas[n2].Kanji
                                    : vocas[n2].DisplayType == "2" ? (vocas[n2].Katakana + '<hr class="hr-quizz">') : (vocas[n2].Hiragana + '<hr class="hr-quizz">' + vocas[n2].Kanji);
                }
                item.ResultUrlAudio2 = vocas[n2].UrlAudio;

                while (item.VocabularyCode == vocas[n3].VocabularyCode || vocas[n2].VocabularyCode == vocas[n3].VocabularyCode) {
                    n3 = Math.floor((Math.random() * (vocas.length)) + 1) - 1;
                }
                item.Hiragana3 = vocas[n3].Hiragana;
                item.Katakana3 = vocas[n3].Katakana;
                item.Kanji3 = vocas[n3].Kanji;
                if (item.TestSkill == 2) {
                    //reading
                    item.Result3 = vocas[n3].DisplayType == '3'
                            ? (vocas[n3].Pinyin + '<hr class="hr-quizz">' + vocas[n3].VMeaning)
                            : vocas[n3].VMeaning;
                }
                else {
                    //transanlating && listening
                    item.Result3 = vocas[n3].DisplayType == '3'
                                    ? vocas[n3].Kanji
                                    : vocas[n3].DisplayType == "2" ? (vocas[n3].Katakana + '<hr class="hr-quizz">') : (vocas[n3].Hiragana + '<hr class="hr-quizz">' + vocas[n3].Kanji);
                }
                item.ResultUrlAudio3 = vocas[n3].UrlAudio;

                while (item.VocabularyCode == vocas[n4].VocabularyCode || vocas[n2].VocabularyCode == vocas[n4].VocabularyCode || vocas[n3].VocabularyCode == vocas[n4].VocabularyCode) {
                    n4 = Math.floor((Math.random() * (vocas.length)) + 1) - 1;
                }
                item.Hiragana4 = vocas[n4].Hiragana;
                item.Katakana4 = vocas[n4].Katakana;
                item.Kanji4 = vocas[n4].Kanji;
                if (item.TestSkill == 2) {
                    //reading
                    item.Result4 = vocas[n4].DisplayType == '3'
                            ? (vocas[n4].Pinyin + '<hr class="hr-quizz">' + vocas[n4].VMeaning)
                            : vocas[n4].VMeaning;
                }
                else {
                    //transanlating && listening
                    item.Result4 = vocas[n4].DisplayType == '3'
                                    ? vocas[n4].Kanji
                                    : vocas[n4].DisplayType == "2" ? (vocas[n4].Katakana + '<hr class="hr-quizz">') : (vocas[n4].Hiragana + '<hr class="hr-quizz">' + vocas[n4].Kanji);
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
                    item.Result2 = item.DisplayType == '3'
                            ? (item.Pinyin + '<hr class="hr-quizz">' + item.VMeaning)
                            : item.VMeaning;
                }
                else {
                    //transanlating && listening
                    item.Result2 = item.DisplayType == '3'
                                    ? item.Kanji
                                    : item.DisplayType == "2" ? (item.Katakana + '<hr class="hr-quizz">') : (item.Hiragana + '<hr class="hr-quizz">' + item.Kanji);
                }
                item.ResultUrlAudio2 = item.UrlAudio;

                while (item.VocabularyCode == vocas[n2].VocabularyCode) {
                    n2 = Math.floor((Math.random() * (vocas.length)) + 1) - 1;
                }
                item.Hiragana1 = vocas[n2].Hiragana;
                item.Katakana1 = vocas[n2].Katakana;
                item.Kanji1 = vocas[n2].Kanji;
                if (item.TestSkill == 2) {
                    //reading
                    item.Result1 = vocas[n2].DisplayType == '3'
                            ? (vocas[n2].Pinyin + '<hr class="hr-quizz">' + vocas[n2].VMeaning)
                            : vocas[n2].VMeaning;
                }
                else {
                    //transanlating && listening
                    item.Result1 = vocas[n2].DisplayType == '3'
                                    ? vocas[n2].Kanji
                                    : vocas[n2].DisplayType == "2" ? (vocas[n2].Katakana + '<hr class="hr-quizz">') : (vocas[n2].Hiragana + '<hr class="hr-quizz">' + vocas[n2].Kanji);
                }
                item.ResultUrlAudio1 = vocas[n2].UrlAudio;

                while (item.VocabularyCode == vocas[n3].VocabularyCode || vocas[n2].VocabularyCode == vocas[n3].VocabularyCode) {
                    n3 = Math.floor((Math.random() * (vocas.length)) + 1) - 1;
                }
                item.Hiragana3 = vocas[n3].Hiragana;
                item.Katakana3 = vocas[n3].Katakana;
                item.Kanji3 = vocas[n3].Kanji;
                if (item.TestSkill == 2) {
                    //reading
                    item.Result3 = vocas[n3].DisplayType == '3'
                            ? (vocas[n3].Pinyin + '<hr class="hr-quizz">' + vocas[n3].VMeaning)
                            : vocas[n3].VMeaning;
                }
                else {
                    //transanlating && listening
                    item.Result3 = vocas[n3].DisplayType == '3'
                                    ? vocas[n3].Kanji
                                    : vocas[n3].DisplayType == "2" ? (vocas[n3].Katakana + '<hr class="hr-quizz">') : (vocas[n3].Hiragana + '<hr class="hr-quizz">' + vocas[n3].Kanji);
                }
                item.ResultUrlAudio3 = vocas[n3].UrlAudio;

                while (item.VocabularyCode == vocas[n4].VocabularyCode || vocas[n2].VocabularyCode == vocas[n4].VocabularyCode || vocas[n3].VocabularyCode == vocas[n4].VocabularyCode) {
                    n4 = Math.floor((Math.random() * (vocas.length)) + 1) - 1;
                }
                item.Hiragana4 = vocas[n4].Hiragana;
                item.Katakana4 = vocas[n4].Katakana;
                item.Kanji4 = vocas[n4].Kanji;
                if (item.TestSkill == 2) {
                    //reading
                    item.Result4 = vocas[n4].DisplayType == '3'
                            ? (vocas[n4].Pinyin + '<hr class="hr-quizz">' + vocas[n4].VMeaning)
                            : vocas[n4].VMeaning;
                }
                else {
                    //transanlating && listening
                    item.Result4 = vocas[n4].DisplayType == '3'
                                    ? vocas[n4].Kanji
                                    : vocas[n4].DisplayType == "2" ? (vocas[n4].Katakana + '<hr class="hr-quizz">') : (vocas[n4].Hiragana + '<hr class="hr-quizz">' + vocas[n4].Kanji);
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
                    item.Result3 = item.DisplayType == '3'
                            ? (item.Pinyin + '<hr class="hr-quizz">' + item.VMeaning)
                            : item.VMeaning;
                }
                else {
                    //transanlating && listening
                    item.Result3 = item.DisplayType == '3'
                                    ? item.Kanji
                                    : item.DisplayType == "2" ? (item.Katakana + '<hr class="hr-quizz">') : (item.Hiragana + '<hr class="hr-quizz">' + item.Kanji);
                }
                item.ResultUrlAudio3 = item.UrlAudio;

                while (item.VocabularyCode == vocas[n2].VocabularyCode) {
                    n2 = Math.floor((Math.random() * (vocas.length)) + 1) - 1;
                }
                item.Hiragana1 = vocas[n2].Hiragana;
                item.Katakana1 = vocas[n2].Katakana;
                item.Kanji1 = vocas[n2].Kanji;
                if (item.TestSkill == 2) {
                    //reading
                    item.Result1 = vocas[n2].DisplayType == '3'
                            ? (vocas[n2].Pinyin + '<hr class="hr-quizz">' + vocas[n2].VMeaning)
                            : vocas[n2].VMeaning;
                }
                else {
                    //transanlating && listening
                    item.Result1 = vocas[n2].DisplayType == '3'
                                    ? vocas[n2].Kanji
                                    : vocas[n2].DisplayType == "2" ? (vocas[n2].Katakana + '<hr class="hr-quizz">') : (vocas[n2].Hiragana + '<hr class="hr-quizz">' + vocas[n2].Kanji);
                }
                item.ResultUrlAudio1 = vocas[n2].UrlAudio;

                while (item.VocabularyCode == vocas[n3].VocabularyCode || vocas[n2].VocabularyCode == vocas[n3].VocabularyCode) {
                    n3 = Math.floor((Math.random() * (vocas.length)) + 1) - 1;
                }
                item.Hiragana2 = vocas[n3].Hiragana;
                item.Katakana2 = vocas[n3].Katakana;
                item.Kanji2 = vocas[n3].Kanji;
                if (item.TestSkill == 2) {
                    //reading
                    item.Result2 = vocas[n3].DisplayType == '3'
                            ? (vocas[n3].Pinyin + '<hr class="hr-quizz">' + vocas[n3].VMeaning)
                            : vocas[n3].VMeaning;
                }
                else {
                    //transanlating && listening
                    item.Result2 = vocas[n3].DisplayType == '3'
                                    ? vocas[n3].Kanji
                                    : vocas[n3].DisplayType == "2" ? (vocas[n3].Katakana + '<hr class="hr-quizz">') : (vocas[n3].Hiragana + '<hr class="hr-quizz">' + vocas[n3].Kanji);
                }
                item.ResultUrlAudio2 = vocas[n3].UrlAudio;

                while (item.VocabularyCode == vocas[n4].VocabularyCode || vocas[n2].VocabularyCode == vocas[n4].VocabularyCode || vocas[n3].VocabularyCode == vocas[n4].VocabularyCode) {
                    n4 = Math.floor((Math.random() * (vocas.length)) + 1) - 1;
                }
                item.Hiragana4 = vocas[n4].Hiragana;
                item.Katakana4 = vocas[n4].Katakana;
                item.Kanji4 = vocas[n4].Kanji;
                if (item.TestSkill == 2) {
                    //reading
                    item.Result4 = vocas[n4].DisplayType == '3'
                            ? (vocas[n4].Pinyin + '<hr class="hr-quizz">' + vocas[n4].VMeaning)
                            : vocas[n4].VMeaning;
                }
                else {
                    //transanlating && listening
                    item.Result4 = vocas[n4].DisplayType == '3'
                                    ? vocas[n4].Kanji
                                    : vocas[n4].DisplayType == "2" ? (vocas[n4].Katakana + '<hr class="hr-quizz">') : (vocas[n4].Hiragana + '<hr class="hr-quizz">' + vocas[n4].Kanji);
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
                    item.Result4 = item.DisplayType == '3'
                            ? (item.Pinyin + '<hr class="hr-quizz">' + item.VMeaning)
                            : item.VMeaning;
                }
                else {
                    //transanlating && listening
                    item.Result4 = item.DisplayType == '3'
                                    ? item.Kanji
                                    : item.DisplayType == "2" ? (item.Katakana + '<hr class="hr-quizz">') : (item.Hiragana + '<hr class="hr-quizz">' + item.Kanji);
                    item.ResultUrlAudio4 = item.UrlAudio;
                }

                while (item.VocabularyCode == vocas[n2].VocabularyCode) {
                    n2 = Math.floor((Math.random() * (vocas.length)) + 1) - 1;
                }
                item.Hiragana1 = vocas[n2].Hiragana;
                item.Katakana1 = vocas[n2].Katakana;
                item.Kanji1 = vocas[n2].Kanji;
                if (item.TestSkill == 2) {
                    //reading
                    item.Result1 = vocas[n2].DisplayType == '3'
                            ? (vocas[n2].Pinyin + '<hr class="hr-quizz">' + vocas[n2].VMeaning)
                            : vocas[n2].VMeaning;
                }
                else {
                    //transanlating && listening
                    item.Result1 = vocas[n2].DisplayType == '3'
                                    ? vocas[n2].Kanji
                                    : vocas[n2].DisplayType == "2" ? (vocas[n2].Katakana + '<hr class="hr-quizz">') : (vocas[n2].Hiragana + '<hr class="hr-quizz">' + vocas[n2].Kanji);
                }
                item.ResultUrlAudio1 = vocas[n2].UrlAudio;

                while (item.VocabularyCode == vocas[n3].VocabularyCode || vocas[n2].VocabularyCode == vocas[n3].VocabularyCode) {
                    n3 = Math.floor((Math.random() * (vocas.length)) + 1) - 1;
                }
                item.Hiragana2 = vocas[n3].Hiragana;
                item.Katakana2 = vocas[n3].Katakana;
                item.Kanji2 = vocas[n3].Kanji;
                if (item.TestSkill == 2) {
                    //reading
                    item.Result2 = vocas[n3].DisplayType == '3'
                            ? (vocas[n3].Pinyin + '<hr class="hr-quizz">' + vocas[n3].VMeaning)
                            : vocas[n3].VMeaning;
                }
                else {
                    //transanlating && listening
                    item.Result2 = vocas[n3].DisplayType == '3'
                                    ? vocas[n3].Kanji
                                    : vocas[n3].DisplayType == "2" ? (vocas[n3].Katakana + '<hr class="hr-quizz">') : (vocas[n3].Hiragana + '<hr class="hr-quizz">' + vocas[n3].Kanji);
                }
                item.ResultUrlAudio2 = vocas[n3].UrlAudio;

                while (item.VocabularyCode == vocas[n4].VocabularyCode || vocas[n2].VocabularyCode == vocas[n4].VocabularyCode || vocas[n3].VocabularyCode == vocas[n4].VocabularyCode) {
                    n4 = Math.floor((Math.random() * (vocas.length)) + 1) - 1;
                }
                item.Hiragana3 = vocas[n4].Hiragana;
                item.Katakana3 = vocas[n4].Katakana;
                item.Kanji3 = vocas[n4].Kanji;
                if (item.TestSkill == 2) {
                    //reading
                    item.Result3 = vocas[n4].DisplayType == '3'
                            ? (vocas[n4].Pinyin + '<hr class="hr-quizz">' + vocas[n4].VMeaning)
                            : vocas[n4].VMeaning;
                }
                else {
                    //transanlating && listening
                    item.Result3 = vocas[n4].DisplayType == '3'
                                    ? vocas[n4].Kanji
                                    : vocas[n4].DisplayType == "2" ? (vocas[n4].Katakana + '<hr class="hr-quizz">') : (vocas[n4].Hiragana + '<hr class="hr-quizz">' + vocas[n4].Kanji);
                }
                item.ResultUrlAudio3 = vocas[n4].UrlAudio;
                break;
            default:

        }
    }
    else {
        console.log('item not found');
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
    if (voca.DisplayType == '3') {
        //kanji
        html += '<div class="col-md-12 padding-0">';
        html += '   <div class="col-lg-3 col-md-3 col-sm-3 col-xs-5">';
        html += '       <p style="font-size: 70px;">' + voca.Kanji + '</p>';
        //html += '       <a href="#" data-toggle="modal" data-target="#kanjiImage"><i class="fa fa-television"></i>Ghi nhớ</a>';
        html += '   </div>';
        html += '   <div class="col-lg-4 col-md-4 col-sm-4 col-xs-7" style="border-right: 1px solid #ccc; border-left: 1px solid #ccc;">';

        html += '       <div class="btn-group pull-right" role="group" onmouseover="expandDetail(this)" >';
        html += '           <button type="button" class="btn btn-circle btn-mn btn-default dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">'
        html += '               <span class="icon-list"></span>';
        html += '           </button>';
        html += '           <ul class="dropdown-menu">';
        if (voca.HasMarked == '1') {
            html += '<li><a href="#" onclick="mark();" class="mark" data-value="marked"><span class="fa fa-book"></span> Đã lưu</a></li>';
        }
        else {
            html += '<li><a href="#" onclick="mark();" class="mark" data-value="unmarked"><span class="fa fa-plus"></span> Lưu sổ tay</a></li>';
        }
        if (voca.IsIgnore == '1') {
            html += '<li><a href="#" onclick="ignore();" class="ignore" data-value="marked"><span class="fa fa-unlock-alt"></span> Đã bỏ</a></li>';
        }
        else {
            html += '<li><a href="#" onclick="ignore();" class="ignore" data-value="unmarked"><span class="fa fa-trash"></span> Bỏ qua</a></li>';
        }
        html += '           </ul>';
        html += '       </div>';

        html += '       <h4><strong><div class="text-uppercase">' + voca.Pinyin + '</div></strong></h4>';
        html += '       <p><strong>' + voca.VMeaning + '</strong></p>';
        html += '       <p>On: ' + voca.OnReading + '</p>';
        html += '       <p>Kun: ' + voca.KunReading + '</p>';
        html += '       <button class=" btn btn-circle btn-mn btn-' + (voca.Level > 1 ? 'primary' : 'default') + '" style="width:15px;height:15px;" data-toggle="tooltip" title="Độ mạnh cấp độ 1">';
        html += '           <span class="fa fa-heart"></span>';
        html += '       </button>';
        html += '       <button class=" btn btn-circle btn-mn btn-' + (voca.Level > 3 ? 'primary' : 'default') + '" style="width:15px;height:15px;" data-toggle="tooltip" title="Độ mạnh cấp độ 2">';
        html += '           <span class="fa fa-heart"></span>';
        html += '       </button>';
        html += '       <button class=" btn btn-circle btn-mn btn-' + (voca.Level > 5 ? 'primary' : 'default') + '" style="width:15px;height:15px;" data-toggle="tooltip" title="Độ mạnh cấp độ 3">';
        html += '           <span class="fa fa-heart"></span>';
        html += '       </button>';
        html += '       <button class=" btn btn-circle btn-mn btn-' + (voca.Level > 7 ? 'primary' : 'default') + '" style="width:15px;height:15px;" data-toggle="tooltip" title="Độ mạnh cấp độ 4">';
        html += '           <span class="fa fa-heart"></span>';
        html += '       </button>';
        html += '       <button class=" btn btn-circle btn-mn btn-' + (voca.Level > 9 ? 'primary' : 'default') + '" style="width:15px;height:15px;" data-toggle="tooltip" title="Độ mạnh cấp độ 5">';
        html += '           <span class="fa fa-heart"></span>';
        html += '       </button>';
        html += '   </div>';
        html += '   <div class="col-lg-5 col-md-5 col-sm-5 col-xs-5 text-center">';
        html += '       <button onclick="draw();" class="btn  btn-xs btn-default" data-toggle="tooltip" title="Nhấp để viết lại"><span class="fa fa-edit"></span>Viết</button>';

        html += '       <div class="col-md-12" id="draw"></div>';
        html += '   </div>';
        html += '</div>';
        html += '<div class="col-md-12 padding-0">';
        html += '<div class="col-lg-5 col-md-5 col-sm-5 col-xs-7 text-center">';
        html += '   <div class="text-center kanjiImage">';
        html += '       <button class="btn  btn-xs btn-default" data-toggle="modal" data-target="#modalKanjiImage"><span class="fa fa-television"></span>Tạo ghi nhớ</button>';
        //html += '   <a href="#" data-toggle="modal" data-target="#modalKanjiImage"><i class="fa fa-television"></i>Tạo ghi nhớ</a>';
        html += '   <h4><p id="divNote">' + (voca.UserDefine ? voca.UserDefine : (voca.Remembering ? voca.Remembering : '')) + '</p></h4>';

        if (voca.UrlImage) {
            html += '   <img class="img-responsive imgExample" src="' + voca.UrlImage + '" alt="">';
        }

        html += '   </div>';
        html += '</div>';
        html += '<div class="col-lg-7 col-md-7 col-sm-7 col-xs-12 tableExample" >';
        //html += '   <label><strong>Ví dụ:</strong></label>';
        html += '   <table class="table table-hover table-responsive" id="tblExample">';
        html += '       <thead><tr><th>Từ</th><th>Hiragana</th><th>HV</th><th>Nghĩa</th></tr></thead>';
        html += '       <tbody>';
        if (voca.KanjiExamples != null) {
            for (var i = 0; i < voca.KanjiExamples.length; i++) {
                var example = voca.KanjiExamples[i];
                html += '<tr>';
                html += '<td><a href="#" class="drawKanji" onclick="drawEx(this); return false;"  data-toggle="tooltip" data-placement="right" title="Nhấp để xem chi tiết">' + example.Kanji + '</a></td>';
                html += '<td>' + example.Hiragana + '</td>';
                html += '<td>' + example.Pinyin + '</td>';
                html += '<td>' + example.VMeaning + '</td>';
                html += '</tr>';
            }
        }
        html += '       </tbody>';
        html += '   </table>';
        html += '</div>';

        html += '</div>';

        if (voca.DisplayType == '3') {
            draw();
        }
    }
    else {
        html += '<div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padding-0">';
        html += '   <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">';
        html += '       <img class="img-responsive" src="' + getLink(voca.UrlImage) + '" alt="Từ vựng tiếng Nhật"  style="height: 200px">';
        html += '   </div>';
        if (voca.Hiragana != '') {
            html += '   <div class="col-lg-7 col-md-7 col-sm-7 col-xs-6"><h3><strong>' + voca.Hiragana + '  </strong><a href="#" onclick="sound(\'' + voca.UrlAudio + '\'); return false;"> <i class="fa fa-volume-up"></i></a></h3>';
            html += '       <p><strong>' + voca.Romaji + '</strong></p>';
        }
        else if (voca.Katakana != '') {
            html += '   <div class="col-lg-7 col-md-7 col-sm-7 col-xs-6"><h3><strong>' + voca.Katakana + '  </strong><a href="#" onclick="sound(\'' + voca.UrlAudio_Katakana + '\'); return false;"><i class="fa fa-volume-up"></i></a></h3>';
            html += '       <p><strong>' + voca.Romaji_Katakana + '</strong></p>';
        }
        html += '       <h3>' + voca.Kanji + '</h3>';
        html += '       <h4>' + voca.VMeaning + '</h4>';

        html += '<button class=" btn btn-circle btn-mn btn-' + (voca.Level > 1 ? 'primary' : 'default') + '" style="width:15px;height:15px;" data-toggle="tooltip" title="Độ mạnh cấp độ 1">';
        html += '<span class="fa fa-heart"></span>';
        html += '</button>';
        html += '<button class=" btn btn-circle btn-mn btn-' + (voca.Level > 3 ? 'primary' : 'default') + '" style="width:15px;height:15px;" data-toggle="tooltip" title="Độ mạnh cấp độ 2">';
        html += '<span class="fa fa-heart"></span>';
        html += '</button>';
        html += '<button class=" btn btn-circle btn-mn btn-' + (voca.Level > 5 ? 'primary' : 'default') + '" style="width:15px;height:15px;" data-toggle="tooltip" title="Độ mạnh cấp độ 3">';
        html += '<span class="fa fa-heart"></span>';
        html += '</button>';
        html += '<button class=" btn btn-circle btn-mn btn-' + (voca.Level > 7 ? 'primary' : 'default') + '" style="width:15px;height:15px;" data-toggle="tooltip" title="Độ mạnh cấp độ 4">';
        html += '<span class="fa fa-heart"></span>';
        html += '</button>';
        html += '<button class=" btn btn-circle btn-mn btn-' + (voca.Level > 9 ? 'primary' : 'default') + '" style="width:15px;height:15px;" data-toggle="tooltip" title="Độ mạnh cấp độ 5">';
        html += '<span class="fa fa-heart"></span>';
        html += '</button>';

        html += '   </div>';
        html += '<div class="col-lg-1 col-md-1 col-sm-1 col-xs-2 text-right">';
        html += '   <div class="btn-group" role="group"  onmouseover="expandDetail(this)">';
        html += '   <button type="button" class="btn btn-circle btn-mn btn-default dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">';
        html += '       <span class="icon-list"></span>';
        html += '   </button>';
        html += '   <ul class="dropdown-menu">';
        if (voca.HasMarked == '1') {
            html += '<li><a href="#" onclick="mark();" class="mark" data-value="marked"><span class="fa fa-book"></span> Đã lưu</a></li>';
        }
        else {
            html += '<li><a href="#" onclick="mark();" class="mark" data-value="unmarked"><span class="fa fa-plus"></span> Lưu sổ tay</a></li>';
        }
        if (voca.IsIgnore == '1') {
            html += '<li><a href="#" onclick="ignore();" class="ignore" data-value="marked"><span class="fa fa-unlock-alt"></span> Đã bỏ</a></li>';
        }
        else {
            html += '<li><a href="#" onclick="ignore();" class="ignore" data-value="unmarked"><span class="fa fa-trash"></span> Bỏ qua</a></li>';
        }
        html += '   </ul>';

        html += '</div>';

        //html += '<div class="progress">';
        //html += '   <div class="progress-bar progress-bar-' + (levelPercent == 100 ? 'primary' : 'danger') + '" role="progressbar" aria-valuenow="' + levelPercent + '" aria-valuemin="0" aria-valuemax="100" style="width: ' + levelPercent + '%"></div>';
        //html += (voca.Level / 10 + '/5');
        //html += '   </div>';
        //html += '</div>';


        //html += '   <div class="col-md-2 text-right">';
        //html += '       <button class=" btn btn-circle btn-outline btn-sm btn-default" value="primary" onclick="mark();">';
        //if (voca.HasMarked == '1') {
        //    html += '           <div id="mark" data-value="marked"><span class="fa fa-book"></span>';
        //}
        //else {
        //    html += '           <div id="mark" data-value="unmarked"><span class="fa fa-plus"></span>';
        //}
        //html += '           </div>';
        //html += '       </button>';
        //html += '       <button class=" btn btn-circle btn-outline btn-sm btn-default" value="primary" onclick="ignore();">';
        //if (voca.IsIgnore == '1') {
        //    html += '           <div id="ignore" data-value="marked"><span class="fa fa-unlock-alt"></span>';
        //}
        //else {
        //    html += '           <div id="ignore" data-value="unmarked"><span class="fa fa-trash"></span>';
        //}
        //html += '           </div>';
        //html += '       </button>';
        //html += '   </div>';
        html += '</div>';
        html += '</div>';
        html += '<div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padding-0">';
        html += '   <div class="col-md-4 col-sm-11 col-xs-1">';
        html += '   </div>';
        html += '   <div class="col-md-8 col-sm-11 col-xs-11">';
        html += '       <button onclick="draw();" class="btn btn-default" data-toggle="tooltip" title="Nhấp để viết lại">Viết</button>';
        var hiraChecked = (voca.DisplayType == "1" && voca.Kanji == '') ? "checked = 'checked'" : "";
        var kataChecked = (voca.DisplayType == "2") ? "checked = 'checked'" : "";
        var kanjiChecked = (voca.DisplayType == '3' || (voca.DisplayType == "1" && voca.Kanji != '')) ? "checked = 'checked'" : "";
        html += '       <input id="rdoHiragana" type="radio" name="rdoDraw" ' + hiraChecked + '>Hiragana';
        html += '       <input id="rdoKatakana" type="radio" name="rdoDraw" ' + kataChecked + '>Katakana';
        html += '       <input id="rdoKanji" type="radio" name="rdoDraw" ' + kanjiChecked + '>Kanji';
        html += '   </div>';
        html += '</div>';
        html += '<div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padding-0">';
        html += '   <div class="col-md-12 text-center" id="draw"></div>';
        html += '</div>';

        //html += '<div id="carousel-example-generic" class="carousel slide" data-ride="carousel">';
        //html += '   <div class="carousel-inner" role="listbox">';
        ////html += '       <div class="item active">';
        ////html += '           <div class="row">';
        ////html += '               <div class="col-lg-4 col-md-4 col-xs-12">';
        ////if (voca.DisplayType == '3') {
        ////    html += '                   <p class="text-info text-center" style="font-size: 150px;">' + voca.Kanji + '</p>';
        ////}
        ////else {
        ////    html += '                   <img class="img-responsive" src="' + getLink(voca.UrlImage) + '" alt="Từ vựng tiếng Nhật" style="height: 300px" />';
        ////}
        ////html += '               </div>';
        ////html += '               <div class="col-lg-8 col-md-8 col-xs-12">';
        ////html += '                   <p class="text-info">Định nghĩa</p>';
        ////html += '                   <p class="text-default">' + voca.Description + '</p>';
        ////if (voca.DisplayType == '3') {
        ////    html += '                   <p class="text-default">' + voca.Remembering + '</p>';
        ////}
        ////html += '               </div>';
        ////html += '           </div>';
        ////html += '       </div>';
        //html += '       <div class="item active">';
        ////html += '           <div class="row">';
        ////kanji
        //if (voca.DisplayType == '3') {
        //    html += '           <div class="row">';
        //    html += '               <div class="col-md-2 col-xs-2">';
        //    html += '               </div>';
        //    html += '               <div class="col-md-3 col-xs-3">';
        //    html += '                   <h3><p class="text-info" style="font-size: 50px;">' + voca.Kanji + '</p><span class="selected glyphicon glyphicon-volume-up" aria-hidden="true" onclick="sound(\'' + voca.UrlAudio + '\');"></span></h3>';
        //    html += '               </div>';
        //    html += '               <div class="col-md-7 col-xs-7">';
        //    html += '                   <p class="text-default"><strong>Hán Việt</strong>: ' + voca.Pinyin + '</p>';
        //    html += '                   <p class="text-default"><strong>Nghĩa</strong>: ' + voca.VMeaning + '</p>';
        //    html += '                   <p class="text-default"><strong>Cách nhớ</strong>: ' + voca.Remembering + '</p>';
        //    html += '               </div>';
        //    html += '           </div>';

        //    html += '           <div class="row">';
        //    html += '               <div class="col-md-2 hidden-xs">';
        //    html += '               </div>';
        //    html += '               <div class="col-md-3 col-xs-3">';
        //    html += '                   <div class="row">';
        //    html += '                       <div class="col-md-12">';
        //    html += '                           <p class="text-default"><strong>On</strong>:</p>';
        //    if (voca.OnReading != '') {
        //        html += '                       <p class="text-default">' + voca.OnReading + '</p>';
        //    }
        //    if (voca.OnReading2 != '') {
        //        html += '                       <p class="text-default">' + voca.OnReading2 + '</p>';
        //    }
        //    html += '                           <p class="text-default"><strong>Kun</strong>:</p>';
        //    if (voca.KunReading != '') {
        //        html += '                       <p class="text-default">' + voca.KunReading + '</p>';
        //    }
        //    if (voca.KunReading2 != '') {
        //        html += '                       <p class="text-default">' + voca.KunReading2 + '</p>';
        //    }
        //    html += '                       </div>';
        //    html += '                   </div>';
        //    html += '               </div>';
        //    html += '               <div class="col-md-7 col-xs-9">';
        //    html += '                   <div class="row">';
        //    html += '                       <div class="col-md-2 col-xs-3">';
        //    html += '                           <p class="text-default"><a href="#" class="drawKanji" data-toggle="tooltip" title="Nhấp để xem cách viết" onclick="drawKanji(event, this);">' + voca.ExKanji1 + '</a></p>';
        //    html += '                       </div>';
        //    html += '                       <div class="col-md-2 col-xs-3">';
        //    html += '                           <p class="text-default">' + voca.ExReading1 + '</p>';
        //    html += '                       </div>';
        //    html += '                       <div class="col-md-3 col-xs-3">';
        //    html += '                           <p class="text-default">' + voca.ExVMeaning1 + '</p>';
        //    html += '                       </div>';
        //    html += '                   </div>';
        //    html += '                   <div class="row">';
        //    html += '                       <div class="col-md-2 col-xs-3">';
        //    html += '                           <p class="text-default"><a href="#" class="drawKanji" data-toggle="tooltip" title="Nhấp để xem cách viết" onclick="drawKanji(event, this);">' + voca.ExKanji2 + '</a></p>';
        //    html += '                       </div>';
        //    html += '                       <div class="col-md-2 col-xs-3">';
        //    html += '                           <p class="text-default">' + voca.ExReading2 + '</p>';
        //    html += '                       </div>';
        //    html += '                       <div class="col-md-3 col-xs-3">';
        //    html += '                           <p class="text-default">' + voca.ExVMeaning2 + '</p>';
        //    html += '                       </div>';
        //    html += '                   </div>';
        //    html += '                   <div class="row">';
        //    html += '                       <div class="col-md-2 col-xs-3">';
        //    html += '                           <p class="text-default"><a href="#" class="drawKanji" data-toggle="tooltip" title="Nhấp để xem cách viết" onclick="drawKanji(event, this);">' + voca.ExKanji3 + '</a></p>';
        //    html += '                       </div>';
        //    html += '                       <div class="col-md-2 col-xs-3">';
        //    html += '                           <p class="text-default">' + voca.ExReading3 + '</p>';
        //    html += '                       </div>';
        //    html += '                       <div class="col-md-3 col-xs-3">';
        //    html += '                           <p class="text-default">' + voca.ExVMeaning3 + '</p>';
        //    html += '                       </div>';
        //    html += '                   </div>';
        //    html += '               </div>';
        //    html += '           </div>';

        //    html += '           <div class="row">';
        //    html += '               <div class="col-md-2 col-xs-2">';
        //    html += '               </div>';
        //    html += '               <div class="col-md-2 col-xs-10">';
        //    html += '                   <button onclick="draw();" class="btn btn-default" data-toggle="tooltip" title="Nhấp để xem cách viết (Ctrl-D)">Cách viết</button>';
        //    html += '               </div>';
        //    html += '               <div class="col-md-6">';
        //    html += '                   <div class="col-md-10 col-xs-10" id="draw"></div>';
        //    html += '               </div>';
        //    html += '               <div class="col-md-1 hidden">';
        //    html += '                   <label class="radio-inline"><input type="radio" name="rdoDraw" id="rdoKanji" checked="checked">Kanji</label>';
        //    html += '               </div>';
        //    html += '           </div>';
        //    //html += '           <div class="row">';
        //    //html += '               <div class="col-md-2 col-xs-2">';
        //    //html += '               </div>';
        //    //html += '               <div class="col-md-10 col-xs-10" id="draw"></div>';
        //    //html += '           </div>';
        //}
        //else {
        //    html += '           <div class="row">';
        //    html += '               <div class="col-lg-4 col-md-4 hidden-xs hidden-sm">';
        //    html += '                   <img class="img-responsive" src="' + getLink(voca.UrlImage) + '" alt="Từ vựng tiếng Nhật" style="height: 300px" />';
        //    html += '               </div>';
        //    html += '               <div class="col-lg-8 col-md-8">';
        //    html += '                   <div class="row">';
        //    html += '                       <div class="col-xs-2">';
        //    html += '                       </div>';
        //    html += '                       <div class="col-md-12 col-xs-10">';
        //    html += '                           <h4><a href="#" onclick="sound(\'' + voca.UrlAudio + '\'); return false;" data-toggle="tooltip" title="Nhấp để nghe đọc"><p class="text-info zoom-content-learning">' + voca.Hiragana + '</p></a></h4>';
        //    if (voca.Hiragana != '') {
        //        html += '                       <p class="text-default">' + voca.Romaji;
        //    }
        //    html += '                           <h4><a href="#" onclick="sound(\'' + voca.UrlAudio_Katakana + '\'); return false;" data-toggle="tooltip" title="Nhấp để nghe đọc"><p class="text-info zoom-content-learning">' + voca.Katakana + '</p></a></h4>';
        //    if (voca.Katakana != '') {
        //        html += '                       <p class="text-default">' + voca.Romaji_Katakana;
        //    }
        //    html += '                           <h4><a href="#" onclick="sound(\'' + voca.UrlAudio + '\'); return false;" data-toggle="tooltip" title="Nhấp để nghe đọc"><p class="text-info zoom-content-learning">' + voca.Kanji + '</p></a></h4>';

        //    //    //html += '                   <span class="selected glyphicon glyphicon-volume-up" aria-hidden="true" onclick="sound(\'' + voca.UrlAudio + '\');"></span>';
        //    //html += '                   </p>';
        //    html += '                           <p class="text-default">' + voca.VMeaning + '</p>';
        //    html += '                       </div>';
        //    html += '                   </div>';
        //    html += '                   <hr class="divider" />';
        //    html += '                   <div class="row">';
        //    html += '                       <div class="col-xs-2">';
        //    html += '                       </div>';
        //    html += '                       <div class="col-md-12 col-xs-10">';
        //    html += '                           <button onclick="draw();" class="btn btn-default" data-toggle="tooltip" title="Nhấp để xem cách viết (Ctrl-D)">Cách viết</button>';
        //    var hiraChecked = (voca.DisplayType == "1") ? "checked = 'checked'" : "";
        //    var kataChecked = (voca.DisplayType == "2") ? "checked = 'checked'" : "";
        //    var kanjiChecked = (voca.DisplayType == '3') ? "checked = 'checked'" : "";
        //    html += '                           <label class="radio-inline"><input type="radio" name="rdoDraw" id="rdoHiragana" ' + hiraChecked + '">Hiragana</label>';
        //    html += '                           <label class="radio-inline"><input type="radio" name="rdoDraw" id="rdoKatakana" ' + kataChecked + '">Katakana</label>';
        //    html += '                           <label class="radio-inline"><input type="radio" name="rdoDraw" id="rdoKanji" "' + kanjiChecked + '">Kanji</label>';
        //    html += '                       </div>';
        //    html += '                   </div>';
        //    html += '                   <div class="row">';
        //    html += '                       <div class="col-md-12" id="draw"></div>';
        //    html += '                   </div>';
        //    html += '               </div>';
        //    html += '           </div>';
        //}
        //html += '       </div>';
        ////html += '       <a id="flashPre" class="left carousel-control" href="#carousel-example-generic" role="button" data-slide="prev">';
        ////html += '           <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>';
        ////html += '           <span class="sr-only">Previous</span>';
        ////html += '       </a>';
        ////html += '       <a id="flashNext" class="right carousel-control" href="#carousel-example-generic" role="button" data-slide="next">';
        ////html += '           <span class="glyphicon glyphicon-chevron-right" aria-hidden="true">';
        ////html += '           </span><span class="sr-only">Next</span>';
        ////html += '       </a>';
        //html += '   </div>';
        //html += '</div>';
    }

    return html;
};

function showPractise(voca) {
    var html = '';

    clearAnswer();
    if (voca.DisplayType != '3') {
        if (voca.TestSkill == '2') {
            sound(voca.UrlAudio);
        }
    }

    var requiredTimePerVoca = parseInt($('#rtp').val());
    var fee = parseFloat($('#vsf').val());

    //html += '<div id="carousel-example-generic" class="carousel slide" data-ride="carousel">';
    //html += '   <div class="carousel-inner" role="listbox">';
    html += '       <div class="item active">';
    html += '           <div class="row text-center" id="item-active">';
    //html += '   <div class="col-lg-1 col-md-1 hidden-xs">';
    //html += '       <div class="c100 p25 small ' + (voca.Level < 10 ? "orange" : "") + '"><span>' + (voca.Level / 10 * 100) + '%</span><div class="slice"><div class="bar"></div><div class="fill"></div></div></div>';
    //html += '   </div>';
    html += '   <div class="col-lg-11 col-md-11 col-xs-10">';
    //html += '               <div class="col-lg-4 col-md-4 col-xs-6  text-center">';
    ////reading
    ////if (voca.TestSkill == '3') {
    ////    html += '                   <img class="img-rounded" src="' + getLink(voca.UrlImage) + '" alt="Từ vựng tiếng Nhật" height="300px" width="100%"/>';
    ////}
    ////    //translating
    if (voca.TestSkill == '2') {
        var displayQuestion = voca.DisplayType == '3' ? ('<div class="text-uppercase">' + voca.Kanji + '</div>') : voca.DisplayType == '1' ? (voca.Hiragana + (voca.Kanji != '' ? '<br>' + voca.Kanji : '')) : voca.Katakana;
        html += '                   <h3><span class="glyphicon glyphicon-question-sign error" aria-hidden="true"></span> <label>' + displayQuestion + '</label></h3>';
    }
    else {
        var displayQuestion = voca.DisplayType == '3' ? ('<div class="text-uppercase">' + voca.Pinyin + '</div><small>' + voca.VMeaning + '</small>') : voca.VMeaning;
        html += '                   <h3><span class="glyphicon glyphicon-question-sign error" aria-hidden="true"></span> <label>' + displayQuestion + '</label></h3>';
    }
    //    //listening
    //else if (voca.TestSkill == '3') {
    //    html += '                   <button type="button" class="btn btn-default btn-lg" onclick="sound(\'' + voca.UrlAudio + '\');"><span class="glyphicon glyphicon-volume-up" aria-hidden="true">Nghe</span></button>';
    //}
    //html += '               </div>';
    //html += '               <div class="col-lg-8 col-md-8 col-xs-6">';
    html += '</div>';
    html += '<div class="col-lg-1 col-md-1 col-xs-2">';
    html += '   <div class="btn-group" role="group" onmouseover="expandDetail(this)">';
    html += '   <button type="button" class="btn btn-circle btn-mn btn-default dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">';
    html += '       <span class="icon-list"></span>';
    html += '   </button>';
    html += '   <ul class="dropdown-menu">';
    if (voca.HasMarked == '1') {
        html += '<li><a href="#" onclick="mark();" class="mark" data-value="marked"><span class="fa fa-book"></span> Đã lưu</a></li>';
    }
    else {
        html += '<li><a href="#" onclick="mark();" class="mark" data-value="unmarked"><span class="fa fa-plus"></span> Lưu sổ tay</a></li>';
    }
    if (voca.IsIgnore == '1') {
        html += '<li><a href="#" onclick="ignore();" class="ignore" data-value="marked"><span class="fa fa-unlock-alt"></span> Đã bỏ</a></li>';
    }
    else {
        html += '<li><a href="#" onclick="ignore();" class="ignore" data-value="unmarked"><span class="fa fa-trash"></span> Bỏ qua</a></li>';
    }
    html += '   </ul>';
    html += '</div>';
    //html += '<button class="  btn btn-circle btn-mn btn-default" value="primary" onclick="mark();">';
    //if (voca.HasMarked == '1') {
    //    html += '   <div id="mark" data-value="marked"><span class="fa fa-book"></span></div>';
    //}
    //else {
    //    html += '   <div id="mark" data-value="unmarked"><span class="fa fa-plus"></span></div>';
    //}
    //html += '</button>';
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

        var result1 = (voca.DisplayType == '3' && voca.TestSkill != '2') ? ('<div style="font-size: 20px;">' + voca.Result1 + '</div>') : voca.Result1;
        var result2 = (voca.DisplayType == '3' && voca.TestSkill != '2') ? ('<div style="font-size: 20px;">' + voca.Result2 + '</div>') : voca.Result2;
        var result3 = (voca.DisplayType == '3' && voca.TestSkill != '2') ? ('<div style="font-size: 20px;">' + voca.Result3 + '</div>') : voca.Result3;
        var result4 = (voca.DisplayType == '3' && voca.TestSkill != '2') ? ('<div style="font-size: 20px;">' + voca.Result4 + '</div>') : voca.Result4;
        html += '<div class="row text-center">';
        html += '   <div class="col-lg-12 col-md-12 col-xs-12">';
        html += '       <a class="btn btn-quizz " href="#" id="result1" name="resultChoosing" onclick="selectValue(this, 1);return false;">1<br>' + result1 + '</a>';
        html += '       <input type="hidden" id="urlAudio1" value="' + voca.ResultUrlAudio1 + '"/>';
        html += '       <a class="btn btn-quizz " href="#" id="result2" name="resultChoosing" onclick="selectValue(this, 2);return false;">2<br>' + result2 + '</a>';
        html += '       <input type="hidden" id="urlAudio2" value="' + voca.ResultUrlAudio2 + '"/>';
        html += '   </div>';
        html += '</div>'
        html += '<div class="row text-center">';
        html += '   <div class="col-lg-12 col-md-12 col-xs-12">';
        html += '       <a class="btn btn-quizz " href="#" id="result3" name="resultChoosing" onclick="selectValue(this, 3);return false;">3<br>' + result3 + '</a>';
        html += '       <input type="hidden" id="urlAudio3" value="' + voca.ResultUrlAudio3 + '"/>';
        html += '       <a class="btn btn-quizz " href="#" id="result4" name="resultChoosing" onclick="selectValue(this, 4);return false;">4<br>' + result4 + '</a>';
        html += '       <input type="hidden" id="urlAudio4" value="' + voca.ResultUrlAudio4 + '"/>';
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
    //html += '   </div>';
    //html += '</div>';

    html += '   <div class="col-lg-12 col-md-12 col-xs-12 text-center">';
    html += '<button id="heart1" class="heart1 btn btn-circle btn-mn btn-' + (voca.Level > 0 ? 'primary' : 'default') + '" style="width:15px;height:15px;">';
    html += '<span class="fa fa-heart"></span>';
    html += '</button>';
    html += '<button id="heart2" class="heart2 btn btn-circle btn-mn btn-' + (voca.Level > 2 ? 'primary' : 'default') + '" style="width:15px;height:15px;">';
    html += '<span class="fa fa-heart"></span>';
    html += '</button>';
    html += '<button id="heart3" class="heart3 btn btn-circle btn-mn btn-' + (voca.Level > 4 ? 'primary' : 'default') + '" style="width:15px;height:15px;">';
    html += '<span class="fa fa-heart"></span>';
    html += '</button>';
    html += '<button id="heart4" class="heart4 btn btn-circle btn-mn btn-' + (voca.Level > 6 ? 'primary' : 'default') + '" style="width:15px;height:15px;">';
    html += '<span class="fa fa-heart"></span>';
    html += '</button>';
    html += '<button id="heart5" class="heart5 btn btn-circle btn-mn btn-' + (voca.Level > 8 ? 'primary' : 'default') + '" style="width:15px;height:15px;">';
    html += '<span class="fa fa-heart"></span>';
    html += '</button>';
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

function drawEx(obj) {
    var text = $(obj).text();
    $('#txtKanji').val(text);
    $('#drawKanji').html('');
    ////var text = $('#draw-text').val();
    if (text != '') {
        var dmak = new Dmak(text, {
            'element': "drawKanji"
        });
    }
    $('#drawingModal').modal();
}
function draw(obj) {
    $('#draw').html('');

    if (obj) {
        var dmak = new Dmak(obj, {
            'element': "draw"
        });
    }
    else {
        var voca = vocas[currentIndex];
        //        sound(voca.UrlAudio);

        var text = '';
        if (voca.DisplayType == '3') {
            text = voca.Kanji;
        }
        else {
            if (voca != null) {
                if ($('#rdoHiragana').is(':checked') && voca.Hiragana != '') {
                    text = voca.Hiragana;
                }
                else if ($('#rdoKatakana').is(':checked') && voca.Katakana != '') {
                    text = voca.Katakana;
                }
                else if ($('#rdoKanji').is(':checked') && voca.Kanji != '') {
                    text = voca.Kanji;
                }
            }
        }
        var dmak = new Dmak(text, {
            'element': "draw"
        });
    }
}

function showDrawing() {
    $('#modalDraw').modal();
}

function mark() {
    var obj = $('.mark');
    if (obj.attr('data-value') == 'unmarked') {
        obj.animate().replaceWith('<a href="#" onclick="mark();" class="mark" data-value="marked"><span class="fa fa-book"></span> Đã lưu</a>');

        vocas[currentIndex].HasMarked = '1';
    }
    else {
        obj.animate().replaceWith('<a href="#" onclick="mark();" class="mark" data-value="unmarked"><span class="fa fa-plus"></span> Lưu sổ tay</a>');

        vocas[currentIndex].HasMarked = '0';
    }
}

function ignore() {
    var obj = $('.ignore');
    if (obj.attr('data-value') == 'unmarked') {
        obj.animate().replaceWith('<a href="#" onclick="ignore();" class="ignore" data-value="marked"><span class="fa fa-unlock-alt"></span> Đã bỏ</a>');

        vocas[currentIndex].IsIgnore = '1';
    }
    else {
        obj.animate().replaceWith('<a href="#" onclick="ignore();" class="ignore" data-value="unmarked"><span class="fa fa-trash"></span> Bỏ qua</a>');

        vocas[currentIndex].IsIgnore = '0';
    }

    $('#btnNext').trigger('click');
}

function expandDetail(obj) {
    $(obj).addClass('open');
}

function sound(name) {
    //console.log(name);
    ion.sound.play(name);
}