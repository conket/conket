﻿
var associativeArray =
{
    "1": "ア", "2": "イ", "3": "ウ", "4": "エ", "5": "オ", "6": "カ", "7": "キ", "8": "ク", "9": "ケ", "10": "コ",
    "11": "サ", "12": "シ", "13": "ス", "14": "セ", "15": "ソ", "16": "タ", "17": "チ", "18": "ツ", "19": "テ", "20": "ト",
    "21": "ナ", "22": "ニ", "23": "ヌ", "24": "ネ", "25": "ノ", "26": "ハ", "27": "ヒ", "28": "フ", "29": "ヘ", "30": "ホ",
    "31": "マ", "32": "ミ", "33": "ム", "34": "メ", "35": "モ", "36": "ヤ", "37": "ユ", "38": "ヨ", "39": "ラ", "40": "リ",
    "41": "ル", "42": "レ", "43": "ロ", "44": "ワ", "45": "ヲ", "46": "ン", "47": "ガ", "48": "ギ", "49": "グ", "50": "ゲ",
    "51": "ゴ", "52": "ザ", "53": "ジ", "54": "ズ", "55": "ゼ", "56": "ゾ", "57": "ダ", "58": "ヂ", "59": "ヅ", "60": "デ",
    "61": "ド", "62": "バ", "63": "ビ", "64": "ブ", "65": "ベ", "66": "ボ", "67": "パ", "68": "ピ", "69": "プ", "70": "ペ",
    "71": "ポ", "72": "キャ", "73": "キュ", "74": "キョ", "75": "シャ", "76": "シュ", "77": "ショ", "78": "チャ", "79": "チュ", "80": "チョ",
    "81": "ニャ", "82": "ニュ", "83": "ニョ", "84": "ヒャ", "85": "ヒュ", "86": "ヒョ", "87": "ミャ", "88": "ミュ", "89": "ミョ", "90": "リャ",
    "91": "リュ", "92": "リョ", "93": "ギャ", "94": "ギュ", "95": "ギョ", "96": "ジャ", "97": "ジュ", "98": "ジョ", "99": "ビャ", "100": "ビュ",
    "101": "ビョ", "102": "ピャ", "103": "ピュ", "104": "ピョ"
};

var romajiArray =
{
    "1": "a", "2": "i", "3": "u", "4": "e", "5": "o", "6": "ka", "7": "ki", "8": "ku", "9": "ke", "10": "ko",
    "11": "sa", "12": "shi", "13": "su", "14": "se", "15": "so", "16": "ta", "17": "chi", "18": "tsu", "19": "te", "20": "to",
    "21": "na", "22": "ni", "23": "nu", "24": "ne", "25": "no", "26": "ha", "27": "hi", "28": "fu", "29": "he", "30": "ho",
    "31": "ma", "32": "mi", "33": "mu", "34": "me", "35": "mo", "36": "ya", "37": "yu", "38": "yo", "39": "ra", "40": "ri",
    "41": "ru", "42": "re", "43": "ro", "44": "wa", "45": "wo", "46": "n", "47": "ga", "48": "gi", "49": "gu", "50": "ge",
    "51": "go", "52": "za", "53": "ji", "54": "zu", "55": "ze", "56": "zo", "57": "da", "58": "di", "59": "du", "60": "de",
    "61": "do", "62": "ba", "63": "bi", "64": "bu", "65": "be", "66": "bo", "67": "pa", "68": "pi", "69": "pu", "70": "pe",
    "71": "po", "72": "kya", "73": "kyu", "74": "kyo", "75": "sha", "76": "shu", "77": "sho", "78": "cha", "79": "chu", "80": "cho",
    "81": "nya", "82": "nyu", "83": "nyo", "84": "hya", "85": "hyu", "86": "hyo", "87": "mya", "88": "myu", "89": "myo", "90": "rya",
    "91": "ryu", "92": "ryo", "93": "gya", "94": "gyu", "95": "gyo", "96": "ja", "97": "ju", "98": "jo", "99": "bya", "100": "byu",
    "101": "byo", "102": "pya", "103": "pyu", "104": "pyo"
};

var writingWordArray = [];
var messageAudio;

$(document).ready(function () {
    var selectedArray = [];
    var alphabetArray = [];
    var resultArray = [];
    var voiceArray = [];
    var index = 0;
    var usingVoice = false;

    $('#btnOkModal').on('click', function () {
        selectedArray = [];
        alphabetArray = [];
        resultArray = [];
        voiceArray = [];

        $('input:checked[name=chkAlphabet]').each(function () {
            selectedArray.push($(this).attr('id'));
        });
        //clear check
        $('input:checked[name=chkAlphabet]').each(function () {
            $(this).removeAttr('checked');
        });

        $('#chkSelectAllReading').removeAttr('checked');

        $('#loadingModal').modal();

        //create random alphabet
        var numberArray = createAlphabet();

        //merge with selected array
        if (!checkExistAlphabet(selectedArray, 'a')) {
            for (var i = 1; i <= 5; i++) {
                numberArray.remove(i);
            }
        }
        if (!checkExistAlphabet(selectedArray, 'ka')) {
            for (var i = 6; i <= 10; i++) {
                numberArray.remove(i);
            }
        }
        if (!checkExistAlphabet(selectedArray, 'sa')) {
            for (var i = 11; i <= 15; i++) {
                numberArray.remove(i);
            }
        }
        if (!checkExistAlphabet(selectedArray, 'ta')) {
            for (var i = 16; i <= 20; i++) {
                numberArray.remove(i);
            }
        }
        if (!checkExistAlphabet(selectedArray, 'na')) {
            for (var i = 21; i <= 25; i++) {
                numberArray.remove(i);
            }
        }
        if (!checkExistAlphabet(selectedArray, 'ha')) {
            for (var i = 26; i <= 30; i++) {
                numberArray.remove(i);
            }
        }
        if (!checkExistAlphabet(selectedArray, 'ma')) {
            for (var i = 31; i <= 35; i++) {
                numberArray.remove(i);
            }
        }
        if (!checkExistAlphabet(selectedArray, 'ya')) {
            for (var i = 36; i <= 38; i++) {
                numberArray.remove(i);
            }
        }
        if (!checkExistAlphabet(selectedArray, 'ra')) {
            for (var i = 39; i <= 43; i++) {
                numberArray.remove(i);
            }
        }
        if (!checkExistAlphabet(selectedArray, 'wa')) {
            for (var i = 44; i <= 46; i++) {
                numberArray.remove(i);
            }
        }
        if (!checkExistAlphabet(selectedArray, 'ga')) {
            for (var i = 47; i <= 51; i++) {
                numberArray.remove(i);
            }
        }
        if (!checkExistAlphabet(selectedArray, 'za')) {
            for (var i = 52; i <= 56; i++) {
                numberArray.remove(i);
            }
        }
        if (!checkExistAlphabet(selectedArray, 'da')) {
            for (var i = 57; i <= 61; i++) {
                numberArray.remove(i);
            }
        }
        if (!checkExistAlphabet(selectedArray, 'ba')) {
            for (var i = 62; i <= 66; i++) {
                numberArray.remove(i);
            }
        }
        if (!checkExistAlphabet(selectedArray, 'pa')) {
            for (var i = 67; i <= 71; i++) {
                numberArray.remove(i);
            }
        }
        if (!checkExistAlphabet(selectedArray, 'kya')) {
            for (var i = 72; i <= 74; i++) {
                numberArray.remove(i);
            }
        }
        if (!checkExistAlphabet(selectedArray, 'sha')) {
            for (var i = 75; i <= 77; i++) {
                numberArray.remove(i);
            }
        }
        if (!checkExistAlphabet(selectedArray, 'cha')) {
            for (var i = 78; i <= 80; i++) {
                numberArray.remove(i);
            }
        }
        if (!checkExistAlphabet(selectedArray, 'nya')) {
            for (var i = 81; i <= 83; i++) {
                numberArray.remove(i);
            }
        }
        if (!checkExistAlphabet(selectedArray, 'hya')) {
            for (var i = 84; i <= 86; i++) {
                numberArray.remove(i);
            }
        }
        if (!checkExistAlphabet(selectedArray, 'mya')) {
            for (var i = 87; i <= 89; i++) {
                numberArray.remove(i);
            }
        }
        if (!checkExistAlphabet(selectedArray, 'rya')) {
            for (var i = 90; i <= 92; i++) {
                numberArray.remove(i);
            }
        }
        if (!checkExistAlphabet(selectedArray, 'gya')) {
            for (var i = 93; i <= 95; i++) {
                numberArray.remove(i);
            }
        }
        if (!checkExistAlphabet(selectedArray, 'ja')) {
            for (var i = 96; i <= 98; i++) {
                numberArray.remove(i);
            }
        }
        if (!checkExistAlphabet(selectedArray, 'bya')) {
            for (var i = 99; i <= 101; i++) {
                numberArray.remove(i);
            }
        }
        if (!checkExistAlphabet(selectedArray, 'pya')) {
            for (var i = 102; i <= 104; i++) {
                numberArray.remove(i);
            }
        }

        for (var i = 0; i < numberArray.length; i++) {
            alphabetArray.push(associativeArray[numberArray[i].toString()]);
        }
        for (var i = 0; i < numberArray.length; i++) {
            voiceArray.push(romajiArray[numberArray[i].toString()]);
        }

        $('#loadingModal').modal('hide');

        //            var mes = '';
        //            for (var i = 0; i < alphabetArray.length; i++) {
        //                mes += (alphabetArray[i] + ',');
        //            }
        //            alert(mes);
        if (alphabetArray.length > 0) {
            $('#modelReading').modal('hide');


            $('#modelTestReading').modal();
            $('#inputAlphabet').val('');
            $('#inputAlphabet').focus();

            //show the first
            index = 0;
            $('#alphabet').html(alphabetArray[index]);
            if ($('#chkVoiceReading').is(':checked')) {
                usingVoice = true;
                //readAlphabet
                readAlphabet(voiceArray[index]);
            }
            else {
                usingVoice = false;
            }

            $('#divResultReading').html('<strong>0/' + alphabetArray.length + '</strong>');
            $('#divResultMessageReading').html('');
        }
        else {
            alert('Chọn kí tự!');
        }
    });

    $('#btnOkReadingModal').on('click', function () {
        if ($('#inputAlphabet').val() != '') {
            //show error if wrong
            if ($('#inputAlphabet').val().toLowerCase().replace(/\s+/g, '') != voiceArray[index]) {
                $('#divResultMessageReading').html('<strong>SAI. TỪ ĐÚNG: ' + voiceArray[index] + ' - ' + alphabetArray[index] + '</strong>');

                //                    var messageAudio = new Audio("/Content/media/exclamation.wav");
                //                    messageAudio.play();
            }
            else {
                $('#divResultMessageReading').html('<strong>ĐÚNG</strong>');
                //add to result
                resultArray.push($('#inputAlphabet').val());

            }
            $('#divResultReading').html('<strong>' + resultArray.length + '/' + alphabetArray.length + '</strong>');

            $('#inputAlphabet').val('');
            $('#inputAlphabet').focus();

            index++;
            //show the first
            $('#alphabet').html(alphabetArray[index]);
            if (usingVoice) {
                //readAlphabet
                readAlphabet(voiceArray[index]);
            }

            if (index == alphabetArray.length) {

                $('#inputAlphabet').val('');
                if (resultArray.length < alphabetArray.length) {
                    $('#alphabet').html('Tệ!');

                    var mess = '';
                    for (var i = 0; i < voiceArray.length; i++) {
                        var isExist = false;
                        for (var j = 0; j < resultArray.length; j++) {
                            if (voiceArray[i] == resultArray[j]) {
                                isExist = true;
                                break;
                            }
                        }
                        if (!isExist) {
                            mess += (voiceArray[i] + ";");
                        }
                    }
                    $('#divResultMessageReading').html('<strong>Các từ sai: ' + mess + '</strong>');
                }
                else {
                    $('#alphabet').html('Ngon!');
                    $('#divResultMessageReading').html('');
                }


            }
        } else {
            $('#inputAlphabet').val('');
            $('#inputAlphabet').focus();
            $('#divResultMessageReading').html('Gõ kí tự Romaji!');
        }
    });

    $('#speakReading').on('click', function () {
        readAlphabet(voiceArray[index]);
    });

    $('#btnResultReading').on('click', function () {
        if (resultArray.length < alphabetArray.length) {
            alert('Thử thêm lần nữa!');
        }
        else {
            alert('Tốt!');
        }
    });

    $('#btnResetReadingModal').on('click', function () {
        $('#modelTestReading').modal('hide');
        $('#modelReading').modal();
    });

    $('#modelTestReading').keydown(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            $("#btnOkReadingModal").trigger("click");
            return false;
        }
        if (event.ctrlKey) {
            //ctrl space
            if (keycode == 32) {
                readAlphabet(voiceArray[index]);
                return false;
            }
        }
    });

    //$('#inputAlphabet').keypress(function (event) {
    //    var keycode = (event.keyCode ? event.keyCode : event.which);
    //    if (keycode == '13') {
    //        $("#btnOkReadingModal").trigger("click");
    //    }
    //});

    $('#chkSelectAllReading').on('change', function () {
        if ($(this).is(':checked')) {
            checkAllReading(true);
        }
        else {
            //clear check
            checkAllReading(false);
        }
    });

    $('#modelTestReading').on('shown.bs.modal', function () {
        $('#inputAlphabet').focus();
    })

    //reading word
    $('#aWordReading').on('click', function () {
        //clear check
        $('input:checked[name=chkWordType]').each(function () {
            $(this).removeAttr('checked');
        });
        $('#normal').prop('checked', true);
        $('#chkVoiceWordReading').prop('checked', true);
        $('#chkSelectAllWordReading').removeAttr('checked');
    });

    $('#btnOkWordModal').on('click', function () {
        resultArray = [];
        writingWordArray = [];
        index = 0;

        var hasNormal = $('#normal').is(':checked');
        var hasDucAm = $('#ducam').is(':checked');
        var hasXucAm = $('#xucam').is(':checked');
        var hasAoAm = $('#aoam').is(':checked');
        var hasLongSound = $('#truongam').is(':checked');

        getReadings(hasNormal, hasDucAm, hasLongSound, hasXucAm, hasAoAm);

        
    });

    $('#btnOkWordReadingModal').on('click', function () {
        if ($('#inputWord').val() != '' && !!$('#inputWord').val()) {
            //show error if wrong
            if ($('#inputWord').val() != writingWordArray[index].Romaji) {
                $('#divResultMessageWordReading').html('<strong>SAI. TỪ ĐÚNG: ' + writingWordArray[index].Romaji + ' - ' + writingWordArray[index].Katakana + '</strong>');

                //                    var messageAudio = new Audio("/Content/media/exclamation.wav");
                //                    messageAudio.play();
            }
            else {
                $('#divResultMessageWordReading').html('<strong>ĐÚNG</strong>');
                //add to result
                resultArray.push($('#inputWord').val());
            }

            $('#divResultWordReading').html('<strong>' + resultArray.length + '/' + writingWordArray.length + '</strong>');

            $('#inputWord').val('');
            $('#inputWord').focus();

            index++;
            //show the first
            $('#word').html(writingWordArray[index].Katakana);
            if (usingVoice) {
                //readAlphabet
                readAlphabet(writingWordArray[index].Romaji);
            }
        }
        else {
            alert('Gõ kí tự Romaji!');
        }
    });

    $('#modelTestWordReading').keydown(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            $("#btnOkWordReadingModal").trigger("click");
        }
    });

    $('#btnResetWordReadingModal').on('click', function () {
        $('#modelTestWordReading').modal('hide');

        //clear check
        $('input:checked[name=chkWordType]').each(function () {
            $(this).removeAttr('checked');
        });
        $('#normal').prop('checked', true);
        $('#chkVoiceWordReading').prop('checked', true);
        $('#chkSelectAllWordReading').removeAttr('checked');

        $('#modelWordReading').modal();
    });

    $('#chkSelectAllWordReading').on('change', function () {
        if ($(this).is(':checked')) {
            checkAllWordReading(true);
        }
        else {
            //clear check
            checkAllWordReading(false);
        }
    });

    $('#modelTestWordReading').on('shown.bs.modal', function () {
        $('#inputWord').focus();
    })

    //writing
    $('#aWriting').on('click', function () {
        getWritings(true, true, true, true, true);
    });

    var indexWriting = 0;
    $('#btnOkWritingModal').on('click', function () {
        if (indexWriting < writingWordArray.length - 1) {
            indexWriting++;

            $('#chkDisplay').removeAttr('checked');
            $('#alphabetWriting').css('display', 'none');
            $('#meaningWriting').css('display', 'block');

            $('#alphabetWriting').html('<strong>' + writingWordArray[indexWriting].Katakana + '</strong>');
            $('#meaningWriting').html('<strong>' + writingWordArray[indexWriting].VMeaning + '</strong>');
            $('#romajiWriting').val(writingWordArray[indexWriting].Romaji);
            //                var messageAudio = new Audio("/Content/media/alphabet/" + writingWordArray[indexWriting].Romaji + ".mp3");
            //                messageAudio.play();
            readAlphabet(writingWordArray[indexWriting].Romaji);
        }
    });

    $('#btnPreviousWritingModal').on('click', function () {
        if (indexWriting > 0) {
            indexWriting--;

            $('#chkDisplay').removeAttr('checked');
            $('#alphabetWriting').css('display', 'none');
            $('#meaningWriting').css('display', 'block');

            $('#alphabetWriting').html('<strong>' + writingWordArray[indexWriting].Katakana + '</strong>');
            $('#meaningWriting').html('<strong>' + writingWordArray[indexWriting].VMeaning + '</strong>');
            $('#romajiWriting').val(writingWordArray[indexWriting].Romaji);
            //                var messageAudio = new Audio("/Content/media/alphabet/" + writingWordArray[indexWriting].Romaji + ".mp3");
            //                messageAudio.play();
            readAlphabet(writingWordArray[indexWriting].Romaji);
        }
    });

    $('#repeat').on('click', function () {
        readAlphabet($('#romajiWriting').val());
    });

    $('#chkDisplay').on('change', function () {
        if ($(this).is(":checked")) {
            $('#alphabetWriting').css('display', 'block');
            //                $('#meaningWriting').css('display', 'block');
        }
        else {
            $('#alphabetWriting').css('display', 'none');
            //                $('#meaningWriting').css('display', 'none');
        }
    });

    $('#modelWriting').keydown(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13' || keycode == '39') {
            $("#btnOkWritingModal").trigger("click");
        }
        else if (keycode == 37) {
            $("#btnPreviousWritingModal").trigger("click");
        }
        else if (keycode == 32) {
            $("#repeat").trigger("click");
        }
        else if (keycode == 40) {
            $('#chkDisplay').prop("checked", true);
            $('#alphabetWriting').css('display', 'block');
            //                $('#meaningWriting').css('display', 'block');
        }
        else if (keycode == 38) {

            $('#chkDisplay').removeAttr("checked");
            $('#alphabetWriting').css('display', 'none');
            //                $('#meaningWriting').css('display', 'none');
        }
        return false;
    });
});

function checkAllWordReading(isAll) {
    if (isAll) {
        //check all
        $('input[name=chkWordType]').each(function (i, chk) {
            $(chk).prop('checked', true);
        });
    }
    else {
        //clear check
        $('input:checked[name=chkWordType]').each(function (i, chk) {
            $(chk).removeAttr('checked');
        });
    }
}

function checkAllReading(isAll) {
    if (isAll) {
        //check all
        $('input[name=chkAlphabet]').each(function (i, chk) {
            $(chk).prop('checked', true);
        });
    }
    else {
        //clear check
        $('input:checked[name=chkAlphabet]').each(function (i, chk) {
            $(chk).removeAttr('checked');
        });
    }
}

function getReadings(hasNormal, hasDA, hasLongSound, hasTsu, hasAA) {
    writingWordArray = [];
    $.ajax({
        cache: true,
        type: "get",
        async: true,
        url: '/Test/' + $('#gw').val(),
        data: { "hasNormal": hasNormal, "hasDA": hasDA, "hasLongSound": hasLongSound, "hasTsu": hasTsu, "hasAA": hasAA },
        dataType: "json",
        success: function (result) {
            //                    alert(words.words);
            //                    $.each(result.words, function (i, word) {
            //                        
            //                    });
            //                $('#alphabetWriting').html('<strong>' + result.word.Katakana + '</strong>');
            //                $('#romajiWriting').val(result.word.Romaji);
            //                var messageAudio = new Audio("/Content/media/alphabet/" + result.word.Romaji + ".mp3");
            //                messageAudio.play();
            $.each(result.words, function (i, word) {
                writingWordArray.push(word);
            });
            if (writingWordArray.length > 0) {
                $('#modelWordReading').modal('hide');
                $('#modelTestWordReading').modal();
                $('#inputWord').val('');

                //show the first
                index = 0;
                $('#word').html(writingWordArray[index].Katakana);
                if ($('#chkVoiceWordReading').is(':checked')) {
                    usingVoice = true;
                    //readAlphabet
                    readAlphabet(writingWordArray[index].Romaji);
                }
                else {
                    usingVoice = false;
                }

                $('#divResultWordReading').html('<strong>0/' + writingWordArray.length + '</strong>');
                $('#divResultMessageWordReading').html('');
            }
            else {
                alert('Chọn!');
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.responseText);
        }
    });
}

function getWritings(hasNormal, hasDA, hasLongSound, hasTsu, hasAA) {
    writingWordArray = [];
    $.ajax({
        cache: true,
        type: "get",
        async: true,
        url: '/Test/' + $('#gw').val(),
        data: { "hasNormal": hasNormal, "hasDA": hasDA, "hasLongSound": hasLongSound, "hasTsu": hasTsu, "hasAA": hasAA },
        dataType: "json",
        success: function (result) {
            //                    alert(words.words);
            //                    $.each(result.words, function (i, word) {
            //                        
            //                    });
            //                $('#alphabetWriting').html('<strong>' + result.word.Katakana + '</strong>');
            //                $('#romajiWriting').val(result.word.Romaji);
            //                var messageAudio = new Audio("/Content/media/alphabet/" + result.word.Romaji + ".mp3");
            //                messageAudio.play();
            $.each(result.words, function (i, word) {
                writingWordArray.push(word);
            });

            if (writingWordArray.length > 0) {

                indexWriting = 0;
                $('#alphabetWriting').html('<strong>' + writingWordArray[indexWriting].Katakana + '</strong>');
                $('#meaningWriting').html('<strong>' + writingWordArray[indexWriting].VMeaning + '</strong>');
                $('#romajiWriting').val(writingWordArray[indexWriting].Romaji);

                $('#chkDisplay').removeAttr('checked');
                $('#alphabetWriting').css('display', 'none');
                $('#meaningWriting').css('display', 'block');

                //                var messageAudio = new Audio("/Content/media/alphabet/" + writingWordArray[indexWriting].Romaji + ".mp3");
                //                messageAudio.play();
                readAlphabet(writingWordArray[indexWriting].Romaji);
            }
            else {
                alert('Không tìm thấy dữ liệu');
                return false;
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.responseText);
        }
    });
}


function readAlphabet(id) {
    if (messageAudio != null) {
        messageAudio.pause();
    }
    messageAudio = new Audio('/Content/media/alphabet/' + id + ".mp3");
    messageAudio.play();
}

Array.prototype.remove = function () {
    var what, a = arguments, L = a.length, ax;
    while (L && this.length) {
        what = a[--L];
        while ((ax = this.indexOf(what)) !== -1) {
            this.splice(ax, 1);
        }
    }
    return this;
};

function checkExistAlphabet(selectedArray, symbol) {
    for (var i = 0; i < selectedArray.length; i++) {
        if (selectedArray[i] == symbol) {
            return true;
        }
    }
    return false;
};

function createAlphabet() {
    var numberArray = [];

    while (numberArray.length < 104) {
        var number = Math.floor((Math.random() * 104) + 1);

        var isExist = false;
        for (var i = 0; i < numberArray.length; i++) {
            if (numberArray[i] == number) {
                isExist = true;
                break;
            }
        }
        if (!isExist) {
            numberArray.push(number);
        }
    }

    return numberArray;
};
