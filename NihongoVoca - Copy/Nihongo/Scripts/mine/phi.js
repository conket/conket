
    var associativeArray =
    {
        "1": "あ", "2": "い", "3": "う", "4": "え", "5": "お", "6": "か", "7": "き", "8": "く", "9": "け", "10": "こ",
        "11": "さ", "12": "し", "13": "す", "14": "せ", "15": "そ", "16": "た", "17": "ち", "18": "つ", "19": "て", "20": "と",
        "21": "な", "22": "に", "23": "ぬ", "24": "ね", "25": "の", "26": "は", "27": "ひ", "28": "ふ", "29": "へ", "30": "ほ",
        "31": "ま", "32": "み", "33": "む", "34": "め", "35": "も", "36": "や", "37": "ゆ", "38": "よ", "39": "ら", "40": "り",
        "41": "る", "42": "れ", "43": "ろ", "44": "わ", "45": "を", "46": "ん", "47": "が", "48": "ぎ", "49": "ぐ", "50": "げ",
        "51": "ご", "52": "ざ", "53": "じ", "54": "ず", "55": "ぜ", "56": "ぞ", "57": "だ", "58": "ぢ", "59": "づ", "60": "で",
        "61": "ど", "62": "ば", "63": "び", "64": "ぶ", "65": "べ", "66": "ぼ", "67": "ぱ", "68": "ぴ", "69": "ぷ", "70": "ぺ",
        "71": "ぽ", "72": "きゃ", "73": "きゅ", "74": "きょ", "75": "しゃ", "76": "しゅ", "77": "しょ", "78": "ちゃ", "79": "ちゅ", "80": "ちょ",
        "81": "にゃ", "82": "にゅ", "83": "にょ", "84": "ひゃ", "85": "ひゅ", "86": "ひょ", "87": "みゃ", "88": "みゅ", "89": "みょ", "90": "りゃ",
        "91": "りゅ", "92": "りょ", "93": "ぎゃ", "94": "ぎゅ", "95": "ぎょ", "96": "じゃ", "97": "じゅ", "98": "じょ", "99": "びゃ", "100": "びゅ",
        "101": "びょ", "102": "ぴゃ", "103": "ぴゅ", "104": "ぴょ"
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

    $('[data-toggle="popover"]').popover();

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

        //create random alphabet
        $('#loadingModal').modal();
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

        getWordReadings(hasNormal, hasDucAm, hasLongSound, hasXucAm, hasAoAm);

        
    });

    $('#btnOkWordReadingModal').on('click', function () {
        if ($('#inputWord').val() != '' && !!$('#inputWord').val()) {
            //show error if wrong
            if ($('#inputWord').val() != writingWordArray[index].Romaji) {
                $('#divResultMessageWordReading').html('<strong>SAI. TỪ ĐÚNG: ' + writingWordArray[index].Romaji + ' - ' + writingWordArray[index].Hiragana + '</strong>');

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
            $('#word').html(writingWordArray[index].Hiragana);
            if (usingVoice) {
                //readAlphabet
                readAlphabet(writingWordArray[index].Romaji);
            }
        }
        else {
            $('#divResultMessageWordReading').html('Gõ kí tự Romaji!');
        }
    });

    $('#modelTestWordReading').keydown(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            $("#btnOkWordReadingModal").trigger("click");
        }
        if (event.ctrlKey) {
            //ctrl space
            if (keycode == 32) {
                readAlphabet(writingWordArray[index].Romaji);
                return false;
            }
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
        getWordWritings(true, true, true, true, true);
    });

    var indexWriting = 0;
    $('#btnOkWritingModal').on('click', function () {
        if (indexWriting < writingWordArray.length - 1) {
            indexWriting++;

            $('#chkDisplay').removeAttr('checked');
            $('#alphabetWriting').css('display', 'none');
            $('#meaningWriting').css('display', 'block');

            $('#alphabetWriting').html('<strong>' + writingWordArray[indexWriting].Hiragana + '</strong>');
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

            $('#alphabetWriting').html('<strong>' + writingWordArray[indexWriting].Hiragana + '</strong>');
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

    $('#speakReading').on('click', function () {
        readAlphabet(voiceArray[index]);
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

function getWordReadings(hasNormal, hasDA, hasLongSound, hasTsu, hasAA) {
    writingWordArray = [];
    $.ajax({
        cache: true,
        type: "get",
        async: true,
        url: '/Test/' + $('#gw').val(),
        data: { "hasNormal": hasNormal, "hasDA": hasDA, "hasLongSound": hasLongSound, "hasTsu": hasTsu, "hasAA": hasAA, "displayType": "1" },
        dataType: "json",
        success: function (result) {
            //                    alert(words.words);
            //                    $.each(result.words, function (i, word) {
            //                        
            //                    });
            //                $('#alphabetWriting').html('<strong>' + result.word.Hiragana + '</strong>');
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
                $('#word').html(writingWordArray[index].Hiragana);
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
                alert('Không tìm thấy dữ liệu!');
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.responseText);
        }
    });
}

function getWordWritings(hasNormal, hasDA, hasLongSound, hasTsu, hasAA) {
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
            //                $('#alphabetWriting').html('<strong>' + result.word.Hiragana + '</strong>');
            //                $('#romajiWriting').val(result.word.Romaji);
            //                var messageAudio = new Audio("/Content/media/alphabet/" + result.word.Romaji + ".mp3");
            //                messageAudio.play();
            $.each(result.words, function (i, word) {
                writingWordArray.push(word);
            });

            if (writingWordArray.length > 0) {

                indexWriting = 0;
                $('#alphabetWriting').html('<strong>' + writingWordArray[indexWriting].Hiragana + '</strong>');
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