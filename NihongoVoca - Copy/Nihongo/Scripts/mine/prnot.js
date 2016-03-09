
var vocas = [];
var failArray = [];
var updateVocas = [];
var selectedVocaIDs = [];
//    var resultArray = [];
var index = 1;
var count = 1;

$(document).ready(function () {

    $('#modalChoosingWord').on('show.bs.modal', function (e) {
        $('#choosingWords').load('/Library/' + $('#gcv').val());
    })

    $('#btnOkChoosingWordModal').on('click', function (e) {
        $('#modalChoosingWord').modal('hide');
        setChosenWordList();
    })

    //reading
    $('.aReading').on('click', function () {
        getReadingVocas();
        return false;
    });

    $('#btnOkReadingModal').on('click', function () {
        var inputValue = $('#inputAlphabet').val().toLowerCase().replace(/\s+/g, '');

        //            vocas[index].IsCorrect = "1";

        var isCorrect = "1";
        if (inputValue != '') {
            if (vocas[index].DisplayType == "3") {
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
                var checkValueRomaji = vocas[index].Romaji.toLowerCase().replace(/\s+/g, '');
                var checkValueHira = vocas[index].Hiragana.toLowerCase().replace(/\s+/g, '');
                var checkValueKata = vocas[index].Katakana.toLowerCase().replace(/\s+/g, '');
                var checkValueKanji = vocas[index].Kanji.toLowerCase().replace(/\s+/g, '');

                //show error if wrong
                if (inputValue != checkValueRomaji
                && inputValue != checkValueHira
                && inputValue != checkValueKata
                && inputValue != checkValueKanji
                ) {
                    isCorrect = "0";
                }
            }

            if (isCorrect == "0") {
                if (count < 3) {
                    count++;
                    $('#divResultMessageReading').html('<strong>SAI. Lại lần nữa!</strong>');

                    speak(vocas[index].UrlAudio);
                    $('#inputAlphabet').val('');
                    $('#inputAlphabet').focus();
                    return false;
                }
            }
            else {

                $('#divResultMessageReading').html('<strong>ĐÚNG</strong>');
            }

            count = 0;
            vocas[index].IsCorrect = isCorrect;

            //
            updateVocas.push(vocas[index]);

            //update db
            //updateFastTestVoca();

            $('#divResultReading').html('<strong>' + (index + 1) + '/' + vocas.length + '</strong>');

            $('#inputAlphabet').val('');
            $('#inputAlphabet').focus();

            if (index == vocas.length - 1) {
                alert('Xong!');
                $('#alphabet').html("XONG!");
                $('#image').removeAttr('src');
                //                    $('#btnCloseReadingModal').focus();
                $('#modelTestReading').modal('hide');
                //alert('Xong!');
            }
            else {
                index++;
                $('#alphabet').html(vocas[index].Description);
                $('#image').prop('src', getLink(vocas[index].UrlImage));
                //                speak(vocas[index].UrlAudio);
            }

        } else {
            alert('Nhập từ vựng!');
        }
    });

    $('#modelTestReading').keydown(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            $("#btnOkReadingModal").trigger("click");
            return false;
        }
    });

    $('#modelTestReading').on('hidden.bs.modal', function () {
        updateUserVocas(true);
    })

    $('#modelTestReading').on('shown.bs.modal', function () {
        $('#inputAlphabet').focus();
    })

    //choosing
    $('#chkHiraganaChoosing, #chkKatakanaChoosing, #chkKanjiChoosing').on('change', function () {
        displayChoosingResult();
    });
    $('a[name=resultChoosing]').on('click', function () {
        clearChoosing();
        $(this).prop('class', 'list-group-item active');

        switch (this.id) {
            case 'result1':
                speak($('#urlAudio1').val());
                $('#selectedValue').val(1);
                break;
            case 'result2':
                speak($('#urlAudio2').val());
                $('#selectedValue').val(2);
                break;
            case 'result3':
                speak($('#urlAudio3').val());
                $('#selectedValue').val(3);
                break;
            case 'result4':
                speak($('#urlAudio4').val());
                $('#selectedValue').val(4);
                break;
            default:

        }

        return false;
    });

    $('.aChoosing').on('click', function () {
        
        $('#hiraganaChoosing').show();
        $('#divHiraganaChoosing').show();
        //$('#katakanaChoosing').show();
        //$('#divKatakanaChoosing').show();
        $('#kanjiChoosing').show();
        $('#divKanjiChoosing').show();
        $('#chkHiraganaChoosing').prop('checked', true);
        //$('#chkKatakanaChoosing').prop('checked', true);
        $('#chkKanjiChoosing').prop('checked', false);

         (getChoosingVocas());

         return false;
    });

    $('#btnOkChoosingModal').on('click', function () {
        var selectedValue = $('#selectedValue').val().toLowerCase().replace(/\s+/g, '');
        var resultValue = $('#correctValue').val().toLowerCase().replace(/\s+/g, ''); //vocas[index].Hiragana.toLowerCase().replace(/\s+/g, '');

        vocas[index].IsCorrect = "1";

        if (selectedValue != '') {
            //show error if wrong
            if (selectedValue != resultValue) {
                //update to fail list
                //updateLevel(vocas[index], false);
                vocas[index].IsCorrect = "0";

                $('#divResultMessageChoosing').html('<strong>SAI. Lại lần nữa!</strong>');
                return false;
            }
            else {
                $('#divResultMessageChoosing').html('<strong>ĐÚNG</strong>');
                //add to result
                //                    resultArray.push(selectedValue);

                //update to fail list
                updateLevel(vocas[index], true);
            }

            //
            updateVocas.push(vocas[index]);
            //update db
            //                updateFastTestVoca();

            $('#divResultChoosing').html('<strong>' + (index + 1) + '/' + vocas.length + '</strong>');

            if (index == vocas.length - 1) {
                alert('Xong!');
                //                    $('#btnCloseChoosingModal').focus();
                $('#modelTestChoosing').modal('hide');
            }
            else {
                index++;

                //if (vocas[index].DisplayType == '3') {
                //    $('#divHiraganaChoosing').hide();
                //    //$('#divKatakanaChoosing').hide();
                //    $('#divKanjiChoosing').hide();
                //    //$('#chkHiraganaChoosing').prop('checked', false);
                //    //$('#chkKatakanaChoosing').prop('checked', false);
                //    //$('#chkKanjiChoosing').prop('checked', true);
                //}
                //else {
                //    if (vocas[index].Hiragana == '' && vocas[index].Katakana == '') {
                //        $('#divHiraganaChoosing').hide();
                //        //$('#chkHiraganaChoosing').prop('checked', false);
                //    }
                //    else {
                //        $('#divHiraganaChoosing').show();
                //    }
                //    if (vocas[index].Kanji == '') {
                //        $('#divKanjiChoosing').hide();
                //        //$('#chkKanjiChoosing').prop('checked', false);
                //    }
                //    else {
                //        $('#divKanjiChoosing').show();
                //    }
                //}

                $('#imageChoosing').prop('src', getLink(vocas[index].UrlImage));
                $('#descriptionChoosing').html(vocas[index].Description);
                $('#correctValue').val(vocas[index].CorrectResult);
                displayChoosingResult();
                //$('#result1').html(vocas[index].Result1);
                $('#urlAudio1').val(vocas[index].ResultUrlAudio1);

                //$('#result2').html(vocas[index].Result2);
                $('#urlAudio2').val(vocas[index].ResultUrlAudio2);

                //$('#result3').html(vocas[index].Result3);
                $('#urlAudio3').val(vocas[index].ResultUrlAudio3);

                //$('#result4').html(vocas[index].Result4);
                $('#urlAudio4').val(vocas[index].ResultUrlAudio4);

            }

            clearChoosing();
            $('#selectedValue').val('');

        } else {
            alert('Chọn 1 giá trị!');
        }
    });

    $('#modelTestChoosing').keydown(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);

        if (keycode == '13') {
            $("#btnOkChoosingModal").trigger("click");
            return false;
        } else if (keycode == 65 || keycode == 97) {
            selectChoosingResult(1);
            return false;
        } else if (keycode == 66 || keycode == 98) {
            selectChoosingResult(2);
            return false;
        } else if (keycode == 67 || keycode == 99) {
            selectChoosingResult(3);
            return false;
        } else if (keycode == 68 || keycode == 100) {
            selectChoosingResult(4);
            return false;
        }
        else if (keycode == 38) {
            if ($('#selectedValue').val() == "1") {
                selectChoosingResult(4);
            } else if ($('#selectedValue').val() == "2") {
                selectChoosingResult(1);
            } else if ($('#selectedValue').val() == "3") {
                selectChoosingResult(2);
            } else if ($('#selectedValue').val() == "4") {
                selectChoosingResult(3);
            } else {
                selectChoosingResult(1);
            }

            return false;
        } else if (keycode == 40) {
            if ($('#selectedValue').val() == "1") {
                selectChoosingResult(2);
            } else if ($('#selectedValue').val() == "2") {
                selectChoosingResult(3);
            } else if ($('#selectedValue').val() == "3") {
                selectChoosingResult(4);
            } else if ($('#selectedValue').val() == "4") {
                selectChoosingResult(1);
            } else {
                selectChoosingResult(1);
            }
            return false;
        }
    });

    $('#modelTestChoosing').on('hidden.bs.modal', function () {
        updateUserVocas(true);
    })

    //translating
    $('#chkHiraganaTranslating').on('change', function () {
        if ($(this).is(":checked")) {
            $('#hiraganaTranslating').show();
            $('#katakanaTranslating').show();
        }
        else {
            $('#hiraganaTranslating').hide();
            $('#katakanaTranslating').hide();
        }
    });
    //$('#chkKatakanaTranslating').on('change', function () {
    //    if ($(this).is(":checked")) {
    //        $('#katakanaTranslating').show();
    //    }
    //    else {
    //        $('#katakanaTranslating').hide();
    //    }
    //});
    $('#chkKanjiTranslating').on('change', function () {
        if ($(this).is(":checked")) {
            $('#kanjiTranslating').show();
        }
        else {
            $('#kanjiTranslating').hide();
        }
    });

    $('#speakTranslating').on('click', function () {
        speak(vocas[index].UrlAudio);
        return false;
    });

    $('a[name=resultTranslating]').on('click', function () {
        clearTranslating();
        $(this).prop('class', 'list-group-item active');

        switch (this.id) {
            case 'result1Translating':
                $('#selectedValueTranslating').val(1);
                break;
            case 'result2Translating':
                $('#selectedValueTranslating').val(2);
                break;
            case 'result3Translating':
                $('#selectedValueTranslating').val(3);
                break;
            case 'result4Translating':
                $('#selectedValueTranslating').val(4);
                break;
            default:

        }

        return false;
    });

    $('.aTranslating').on('click', function () {
        $('#hiraganaTranslating').show();
        $('#katakanaTranslating').show();
        $('#kanjiTranslating').hide();

        $('#divHiraganaTranslating').show();
        //$('#divKatakanaTranslating').show();
        $('#divKanjiTranslating').show();

        $('#chkHiraganaTranslating').prop('checked', true);
        //$('#chkKatakanaTranslating').prop('checked', true);
        $('#chkKanjiTranslating').prop('checked', false);

        (getTranslatingVocas());

        return false;
    });

    $('#btnOkTranslatingModal').on('click', function () {
        var selectedValue = $('#selectedValueTranslating').val().toLowerCase().replace(/\s+/g, '');
        var resultValue = $('#correctValueTranslating').val().toLowerCase().replace(/\s+/g, ''); //vocas[index].Hiragana.toLowerCase().replace(/\s+/g, '');

        vocas[index].IsCorrect = "1";

        if (selectedValue != '') {
            //show error if wrong
            if (selectedValue != resultValue) {
                //update to fail list
                //updateLevel(vocas[index], false);
                vocas[index].IsCorrect = "0";

                $('#divResultMessageTranslating').html('<strong>SAI. Lại lần nữa!</strong>');
                return false;
            }
            else {
                $('#divResultMessageTranslating').html('<strong>ĐÚNG</strong>');
                //add to result
                //                    resultArray.push(selectedValue);

                //update to fail list
                updateLevel(vocas[index], true);
            }

            //
            updateVocas.push(vocas[index]);
            //update db
            //                updateFastTestVoca();

            $('#divResultTranslating').html('<strong>' + (index + 1) + '/' + vocas.length + '</strong>');

            if (index == vocas.length - 1) {
                alert('Xong!');
                //                    $('#btnCloseChoosingModal').focus();
                $('#modelTestTranslating').modal('hide');
            }
            else {
                index++;
                //speak(vocas[index].UrlAudio);

                $('#hiraganaTranslating').html(vocas[index].Hiragana);
                $('#katakanaTranslating').html(vocas[index].Katakana);
                $('#kanjiTranslating').html(vocas[index].Kanji);
                $('#correctValueTranslating').val(vocas[index].CorrectResult);
                $('#result1Translating').html(vocas[index].Result1);
                $('#result2Translating').html(vocas[index].Result2);
                $('#result3Translating').html(vocas[index].Result3);
                $('#result4Translating').html(vocas[index].Result4);

                if (vocas[index].DisplayType == '3') {
                    $('#divHiraganaTranslating').hide();
                    //$('#divKatakanaChoosing').hide();
                    $('#divKanjiTranslating').hide();
                }
                else {
                    $('#divHiraganaTranslating').show();
                    $('#divKanjiTranslating').show();
                }
            }

            clearTranslating();
            $('#selectedValueTranslating').val('');

        } else {
            alert('Chọn 1 giá trị!');
        }
    });

    $('#modelTestTranslating').keydown(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);

        if (keycode == '13') {
            $("#btnOkTranslatingModal").trigger("click");
            return false;
        } else if (keycode == 32) {
            speak(vocas[index].UrlAudio);
            return false;
        }
        else if (keycode == 65 || keycode == 97) {
            selectTranslatingResult(1);
            return false;
        } else if (keycode == 66 || keycode == 98) {
            selectTranslatingResult(2);
            return false;
        } else if (keycode == 67 || keycode == 99) {
            selectTranslatingResult(3);
            return false;
        } else if (keycode == 68 || keycode == 100) {
            selectTranslatingResult(4);
            return false;
        }
        else if (keycode == 38) {
            if ($('#selectedValueTranslating').val() == "1") {
                selectTranslatingResult(4);
            } else if ($('#selectedValueTranslating').val() == "2") {
                selectTranslatingResult(1);
            } else if ($('#selectedValueTranslating').val() == "3") {
                selectTranslatingResult(2);
            } else if ($('#selectedValueTranslating').val() == "4") {
                selectChoosingResult(3);
            } else {
                selectTranslatingResult(1);
            }

            return false;
        } else if (keycode == 40) {
            if ($('#selectedValueTranslating').val() == "1") {
                selectTranslatingResult(2);
            } else if ($('#selectedValueTranslating').val() == "2") {
                selectTranslatingResult(3);
            } else if ($('#selectedValueTranslating').val() == "3") {
                selectTranslatingResult(4);
            } else if ($('#selectedValueTranslating').val() == "4") {
                selectTranslatingResult(1);
            } else {
                selectTranslatingResult(1);
            }
            return false;
        }
    });

    $('#modelTestTranslating').on('hidden.bs.modal', function () {
        updateUserVocas(true);
    })
});

function setChosenWordList() {
    selectedVocaIDs = [];
    $("#choosingWords :checkbox:checked").each(function (j, obj) {
        selectedVocaIDs.push($(this).val());
    });
}

function displayChoosingResult() {
    var result1 = '';
    var result2 = '';
    var result3 = '';
    var result4 = '';
    if ($('#chkHiraganaChoosing').is(":checked")) {
        result1 += vocas[index].Hiragana1;
        result2 += vocas[index].Hiragana2;
        result3 += vocas[index].Hiragana3;
        result4 += vocas[index].Hiragana4;

        result1 += (vocas[index].Katakana1 == '' ? '' : ' 。 ' + vocas[index].Katakana1);
        result2 += (vocas[index].Katakana2 == '' ? '' : ' 。 ' + vocas[index].Katakana2);
        result3 += (vocas[index].Katakana3 == '' ? '' : ' 。 ' + vocas[index].Katakana3);
        result4 += (vocas[index].Katakana4 == '' ? '' : ' 。 ' + vocas[index].Katakana4);
    }
            
    if ($('#chkKanjiChoosing').is(":checked")) {
        result1 += (result1 == '' ? vocas[index].Kanji1 : ' 。 ' + vocas[index].Kanji1);
        result2 += (result2 == '' ? vocas[index].Kanji2 : ' 。 ' + vocas[index].Kanji2);
        result3 += (result3 == '' ? vocas[index].Kanji3 : ' 。 ' + vocas[index].Kanji3);
        result4 += (result4 == '' ? vocas[index].Kanji4 : ' 。 ' + vocas[index].Kanji4);
    }
    $('#result1').html(result1);
    $('#result2').html(result2);
    $('#result3').html(result3);
    $('#result4').html(result4);
}
function selectChoosingResult(no) {
    clearChoosing();
    switch (no) {
        case 1:
            $('#result1').prop('class', 'list-group-item active');
            speak($('#urlAudio1').val());
            $('#selectedValue').val(1);
            break;
        case 2:
            $('#result2').prop('class', 'list-group-item active');
            speak($('#urlAudio2').val());
            $('#selectedValue').val(2);
            break;
        case 3:
            $('#result3').prop('class', 'list-group-item active');
            speak($('#urlAudio3').val());
            $('#selectedValue').val(3);
            break;
        case 4:
            $('#result4').prop('class', 'list-group-item active');
            speak($('#urlAudio4').val());
            $('#selectedValue').val(4);
            break;
        default:

    }
};
function selectTranslatingResult(no) {
    clearTranslating();
    switch (no) {
        case 1:
            $('#result1Translating').prop('class', 'list-group-item active');
            $('#selectedValueTranslating').val(1);
            break;
        case 2:
            $('#result2Translating').prop('class', 'list-group-item active');
            $('#selectedValueTranslating').val(2);
            break;
        case 3:
            $('#result3Translating').prop('class', 'list-group-item active');
            $('#selectedValueTranslating').val(3);
            break;
        case 4:
            $('#result4Translating').prop('class', 'list-group-item active');
            $('#selectedValueTranslating').val(4);
            break;
        default:

    }
};

function updateLevel(voca, isOk) {
    for (var i = 0; i < failArray.length; i++) {
        if (failArray[i].ID == voca.ID) {
            if (isOk) {
                failArray[i].Level += 1;
                if (failArray[i].Level > 10) {
                    failArray[i].Level = 10;
                }
            } else {
                failArray[i].Level -= 2;
                if (failArray[i].Level < 0) {
                    failArray[i].Level = 0;
                }
            }
            break;
        }
    }
};

function updateUserVocas(isAsyns) {
    if (updateVocas.length > 0) {
        $.ajax({
            cache: true,
            type: "post",
            async: isAsyns,
            url: '/Library/' + $('#uuv').val(),
            data: JSON.stringify(updateVocas),
            dataType: "json",
            contentType: 'application/json',
            success: function (result) {
                if (result.ReturnCode == accessDenied) {
                    window.location.href = '/Account/RequireLogin';
                }
                else if (result.ReturnCode != 0) {
                    alert('Có lỗi xảy ra');
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                console.log(xhr.responseText);
                return false;
            }
        });
    }
}

function updateFastTestVoca() {
    $.ajax({
        cache: true,
        type: "post",
        async: true,
        url: '/Library/UpdateFastTestVoca',
        data: JSON.stringify(vocas[index]),
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

function clearChoosing() {
    $('a[name=resultChoosing]').each(function () {
        $(this).prop('class', 'list-group-item');
    });
};

function clearTranslating() {
    $('a[name=resultTranslating]').each(function () {
        $(this).prop('class', 'list-group-item');
    });
};

function getReadingVocas() {
    vocas = [];
    failArray = [];
    updateVocas = [];
    var id = $('#vcd').val();
    var accessDenied = $('#accessDenied').val();
    $.ajax({
        cache: true,
        type: "get",
        async: true,
        url: '/Library/' + $('#grl').val(),
        //data: { "id": id },
        dataType: "json",
        success: function (result) {
            if (result.returnCode == accessDenied) {
                window.location.href = '/Account/RequireLogin';
            } else {
                $.each(result.vocabularies, function (i, voca) {
                    if (selectedVocaIDs.length > 0) {
                        for (var j = 0; j < selectedVocaIDs.length; j++) {
                            if (voca.ID == selectedVocaIDs[j]) {
                                vocas.push(voca);
                                failArray.push(voca);
                                break;
                            }
                        }
                    } else {
                        vocas.push(voca);
                        failArray.push(voca);
                    }

                    var audio = new Audio(voca.UrlAudio);
                    audio.load();
                });

                $('#modelTestReading').modal();

                if (vocas.length > 0) {
                    $('#inputAlphabet').val('');
                    $('#inputAlphabet').focus();

                    //show the first
                    index = 0;
                    $('#alphabet').html(vocas[index].Description);
                    $('#image').prop('src', getLink(vocas[index].UrlImage));
                    //                speak(vocas[index].UrlAudio);

                    $('#divResultReading').html('<strong>0/' + vocas.length + '</strong>');
                    $('#divResultMessageReading').html('');
                }
                else {
                    alert('Không tìm thấy từ vựng!');
                    //$('#modelTestReading').modal('hide');
                    return false;
                }
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

function getChoosingVocas() {
    var result = true;
    vocas = [];
    failArray = [];
    updateVocas = [];
    $('#selectedValue').val('');
    clearChoosing();
    var id = $('#vcd').val();
    var accessDenied = $('#accessDenied').val();
    $.ajax({
        cache: true,
        type: "get",
        async: true,
        url: '/Library/' + $('#gcl').val(),
        //data: { "id": id },
        dataType: "json",
        success: function (result) {
            if (result.returnCode == accessDenied) {
                window.location.href = '/Account/RequireLogin';
            } else {
                $.each(result.vocabularies, function (i, voca) {
                    if (selectedVocaIDs.length > 0) {
                        for (var j = 0; j < selectedVocaIDs.length; j++) {
                            if (voca.ID == selectedVocaIDs[j]) {
                                vocas.push(voca);
                                failArray.push(voca);
                                break;
                            }
                        }
                    } else {
                        vocas.push(voca);
                        failArray.push(voca);
                    }

                    var audio = new Audio(voca.UrlAudio);
                    audio.load();
                });

                if (vocas.length >= 4) {
                    $('#modelTestChoosing').modal();

                    //show the first
                    index = 0;
                    $('#imageChoosing').prop('src', getLink(vocas[index].UrlImage));
                    $('#descriptionChoosing').html(vocas[index].Description);
                    $('#correctValue').val(vocas[index].CorrectResult);
                    $('#result1').html(vocas[index].Result1);
                    $('#urlAudio1').val(vocas[index].ResultUrlAudio1);

                    $('#result2').html(vocas[index].Result2);
                    $('#urlAudio2').val(vocas[index].ResultUrlAudio2);

                    $('#result3').html(vocas[index].Result3);
                    $('#urlAudio3').val(vocas[index].ResultUrlAudio3);

                    $('#result4').html(vocas[index].Result4);
                    $('#urlAudio4').val(vocas[index].ResultUrlAudio4);

                    $('#chkHiraganaChoosing').prop('checked', true);
                    //$('#chkKatakanaChoosing').prop('checked', true);
                    $('#chkKanjiChoosing').prop('checked', false);
                    if (vocas[index].DisplayType == '3') {
                        $('#divHiraganaChoosing').hide();
                        //$('#divKatakanaChoosing').hide();
                        $('#divKanjiChoosing').hide();
                        $('#chkHiraganaChoosing').prop('checked', false);
                        //$('#chkKatakanaChoosing').prop('checked', false);
                        $('#chkKanjiChoosing').prop('checked', true);
                    }
                    //else {
                    //    if (vocas[index].Hiragana == '' && vocas[index].Katakana == '') {
                    //        $('#divHiraganaChoosing').hide();
                    //        $('#chkHiraganaChoosing').prop('checked', false);
                    //    }
                    //    else {
                    //        $('#divHiraganaChoosing').show();
                    //    }
                    //    //if (vocas[index].Katakana == '') {
                    //    //    $('#divKatakanaChoosing').hide();
                    //    //    $('#chkKatakanaChoosing').prop('checked', false);
                    //    //}
                    //    //else {
                    //    //    $('#divKatakanaChoosing').show();
                    //    //}
                    //    if (vocas[index].Kanji == '') {
                    //        $('#divKanjiChoosing').hide();
                    //        $('#chkKanjiChoosing').prop('checked', false);
                    //    }
                    //    else {
                    //        $('#divKanjiChoosing').show();
                    //    }
                    //}

                    $('#divResultChoosing').html('<strong>0/' + vocas.length + '</strong>');
                    $('#divResultMessageChoosing').html('');
                }
                else {
                    alert('Không tìm thấy từ vựng hoặc không đủ để tạo trắc nghiệm!');
                    //$('#modelTestChoosing').modal('hide');
                    result = false;
                }
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.responseText);
            result = false;
        },
        beforeSend: function () {
            $('#loadingModal').modal();
        },
        complete: function () {
            $('#loadingModal').modal('hide');
        }
    });

    return result;
}

function getTranslatingVocas() {
    var result = true;
    vocas = [];
    failArray = [];
    updateVocas = [];
    $('#selectedValueTranslating').val('');
    clearTranslating();
    var id = $('#vcd').val();
    var accessDenied = $('#accessDenied').val();
    $.ajax({
        cache: true,
        type: "get",
        async: true,
        url: '/Library/' + $('#gtl').val(),
        //data: { "id": id },
        dataType: "json",
        success: function (result) {
            if (result.returnCode == accessDenied) {
                window.location.href = '/Account/RequireLogin';
            } else {
                $.each(result.vocabularies, function (i, voca) {
                    if (selectedVocaIDs.length > 0) {
                        for (var j = 0; j < selectedVocaIDs.length; j++) {
                            if (voca.ID == selectedVocaIDs[j]) {
                                vocas.push(voca);
                                failArray.push(voca);
                                break;
                            }
                        }
                    } else {
                        vocas.push(voca);
                        failArray.push(voca);
                    }

                    var audio = new Audio(voca.UrlAudio);
                    audio.load();
                });

                if (vocas.length >= 4) {
                    $('#modelTestTranslating').modal();

                    //show the first
                    index = 0;
                    //speak(vocas[index].UrlAudio);
                    $('#hiraganaTranslating').html(vocas[index].Hiragana);
                    $('#katakanaTranslating').html(vocas[index].Katakana);
                    $('#kanjiTranslating').html(vocas[index].Kanji);
                    $('#correctValueTranslating').val(vocas[index].CorrectResult);
                    $('#result1Translating').html(vocas[index].Result1);
                    $('#result2Translating').html(vocas[index].Result2);
                    $('#result3Translating').html(vocas[index].Result3);
                    $('#result4Translating').html(vocas[index].Result4);

                    if (vocas[index].DisplayType == '3') {
                        $('#divHiraganaTranslating').hide();
                        //$('#divKatakanaChoosing').hide();
                        $('#divKanjiTranslating').hide();
                        $('#chkHiraganaTranslating').prop('checked', false);
                        //$('#chkKatakanaChoosing').prop('checked', false);
                        $('#chkKanjiTranslating').prop('checked', true);
                    }
                    else {
                        $('#divHiraganaTranslating').show();
                        $('#divKanjiTranslating').show();
                        $('#chkHiraganaTranslating').prop('checked', true);
                        //$('#chkKatakanaChoosing').prop('checked', false);
                        $('#chkKanjiTranslating').prop('checked', false);
                    }

                    //if (vocas[index].Hiragana == '' && vocas[index].Katakana == '') {
                    //    $('#hiraganaTranslating').hide();
                    //    $('#divHiraganaTranslating').hide();
                    //}
                    //else {
                    //    $('#hiraganaTranslating').show();
                    //    $('#divHiraganaTranslating').show();
                    //}
                    ////if (vocas[index].Katakana == '') {
                    ////    $('#katakanaTranslating').hide();
                    ////    $('#divKatakanaTranslating').hide();
                    ////}
                    ////else {
                    ////    $('#katakanaTranslating').show();
                    ////    $('#divKatakanaTranslating').show();
                    ////}
                    //if (vocas[index].Kanji == '') {
                    //    $('#kanjiTranslating').hide();
                    //    $('#divKanjiTranslating').hide();
                    //}
                    //else {
                    //    $('#kanjiTranslating').show();
                    //    $('#divKanjiTranslating').show();
                    //}

                    $('#divResultTranslating').html('<strong>0/' + vocas.length + '</strong>');
                    $('#divResultMessageTranslating').html('');
                }
                else {
                    alert('Không tìm thấy từ vựng hoặc không đủ để tạo trắc nghiệm!');
                    //$('#modelTestTranslating').modal('hide');
                    result = false;
                }
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.responseText);
            result = false;
        },
        beforeSend: function () {
            $('#loadingModal').modal();
        },
        complete: function () {
            $('#loadingModal').modal('hide');
        }
    });

    return result;
}

//var messageAudio;
//function speak(url) {
//    if (messageAudio != null) {
//        messageAudio.pause();
//    }
//    messageAudio = new Audio(url);
//    messageAudio.play();
//};

function getLink(url) {
    return url;
};