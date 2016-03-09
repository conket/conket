

var messageAudio;

$(document).ready(function () {

    $('#romaji-tenten-maru').on('click', function () {
        reShowAlphabet();
    });

    $('#romaji').on('click', function () {
        reShowAlphabet();
    });

    $('#romaji-aoam').on('click', function () {
        reShowAlphabet();
    });

    $("span[name=tenten-maru]").click(function () {
        showAlphabet(this.id);
        $('#romaji-tenten-maru').html('Romaji: <strong>' + this.id + '</strong>');
        $('#tenten-maru-image').html($(this).text());
    });

    $("span[name=alphabet-aoam]").click(function () {
        showAlphabet(this.id);
        $('#romaji-aoam').html('Romaji: <strong>' + this.id + '</strong>');
        $('#aoam-image').html($(this).text());
    });

    $("span[name=alphabet]").click(function () {
        showAlphabet(this.id);
        //            $('#alphabet-image').attr('src', 'Href("~/Images/alphabet/hiragana/")' + this.id + '.gif');
        $('#alphabet-image').html($('#' + $('#selected-alphabet').val()).text());
        $('#romaji').html('Romaji: <strong>' + this.id + '</strong>');
    });

    $('#btnNextTentenMaru').on('click', function () {
        showNextAlphabet();
        $('#romaji-tenten-maru').html('Romaji: <strong>' + $('#selected-alphabet').val() + '</strong>');
        $('#tenten-maru-image').html($('#' + $('#selected-alphabet').val()).text());
    });

    $('#btnNextAoAm').on('click', function () {
        showNextAlphabet();
        $('#romaji-aoam').html('Romaji: <strong>' + $('#selected-alphabet').val() + '</strong>');
        $('#aoam-image').html($('#' + $('#selected-alphabet').val()).text());
    });

    $('#btnNextAlphabet').on('click', function () {
        showNextAlphabet();
        //            $('#alphabet-image').attr('src', 'Href("~/Images/alphabet/hiragana/")' + $('#selected-alphabet').val() + '.gif');
        $('#alphabet-image').html($('#' + $('#selected-alphabet').val()).text());
        $('#romaji').html('Romaji: <strong>' + $('#selected-alphabet').val() + '</strong>');
    });
});

function readAlphabet(id) {
    if (messageAudio != null) {
        messageAudio.pause();
    }
    messageAudio = new Audio('/Content/media/alphabet/' + id + ".mp3");
    messageAudio.play();
}

function showAlphabet(id) {
    //remove focus
    $("#" + $('#selected-alphabet').val()).parent('td').removeClass("info");
    //change focus
    $("#" + id).parent('td').addClass("info");

    $('#selected-alphabet').val(id);

    readAlphabet(id);
};

function reShowAlphabet() {
    var id = $('#selected-alphabet').val();
    showAlphabet(id);
};

function showAlphabetDraw() {
    $('#alphabet-image').html('');
    var text = $('#' + $('#selected-alphabet').val()).text();
    if (text != '') {
        var dmak = new Dmak(text, {
            'element': "alphabet-image"
        });
    }
    //        $('#alphabet-image').attr('src', 'Href("~/Images/alphabet/hiragana/")' + $('#selected-alphabet').val() + '-draw.gif');
};

function showTentenMaruDraw() {
    $('#tenten-maru-image').html('');
    var text = $('#' + $('#selected-alphabet').val()).text();
    if (text != '') {
        var dmak = new Dmak(text, {
            'element': "tenten-maru-image"
        });
    }
};

function showAoAmDraw() {
    $('#aoam-image').html('');
    var text = $('#' + $('#selected-alphabet').val()).text();
    if (text != '') {
        var dmak = new Dmak(text, {
            'element': "aoam-image"
        });
    }
};

function showNextAlphabet() {
    //next
    var nextAl = nextAlphabet($('#selected-alphabet').val());
    showAlphabet(nextAl);
};


function nextAlphabetLine(al) {
    var result = al;
    switch (al) {
        case 'a':
            result = 'i';
            break;
        case 'i':
            result = 'u';
            break;
        case 'u':
            result = 'e';
            break;
        case 'e':
            result = 'o';
            break;
        case 'o':
            result = 'a';
            break;
        default:
            result = 'a';
            break;
    }

    return result;
};

function nextAlphabet(selectedAlphabet) {
    var nextAlphabet = selectedAlphabet;
    switch (selectedAlphabet) {
        case 'o':
            nextAlphabet = 'ka';
            break;
        case 'ko':
            nextAlphabet = 'sa';
            break;
        case 'so':
            nextAlphabet = 'ta';
            break;
        case 'to':
            nextAlphabet = 'na';
            break;
        case 'no':
            nextAlphabet = 'ha';
            break;
        case 'ho':
            nextAlphabet = 'ma';
            break;
        case 'mo':
            nextAlphabet = 'ya';
            break;
        case 'yo':
            nextAlphabet = 'ra';
            break;
        case 'ro':
            nextAlphabet = 'wa';
            break;
        case 'wo':
            nextAlphabet = 'n';
            break;
        case 'n':
            nextAlphabet = 'a';
            break;
        case 'go':
            nextAlphabet = 'za';
            break;
        case 'zo':
            nextAlphabet = 'da';
            break;
        case 'do':
            nextAlphabet = 'ba';
            break;
        case 'bo':
            nextAlphabet = 'pa';
            break;
        case 'po':
            nextAlphabet = 'ga';
            break;
        case 'kya':
            nextAlphabet = 'kyu';
            break;
        case 'kyu':
            nextAlphabet = 'kyo';
            break;
        case 'kyo':
            nextAlphabet = 'sha';
            break;
        case 'sha':
            nextAlphabet = 'shu';
            break;
        case 'shu':
            nextAlphabet = 'sho';
            break;
        case 'sho':
            nextAlphabet = 'cha';
            break;
        case 'cha':
            nextAlphabet = 'chu';
            break;
        case 'chu':
            nextAlphabet = 'cho';
            break;
        case 'cho':
            nextAlphabet = 'nya';
            break;
        case 'nya':
            nextAlphabet = 'nyu';
            break;
        case 'nyu':
            nextAlphabet = 'nyo';
            break;
        case 'nyo':
            nextAlphabet = 'hya';
            break;
        case 'hya':
            nextAlphabet = 'hyu';
            break;
        case 'hyu':
            nextAlphabet = 'hyo';
            break;
        case 'hyo':
            nextAlphabet = 'mya';
            break;
        case 'mya':
            nextAlphabet = 'myu';
            break;
        case 'myu':
            nextAlphabet = 'myo';
            break;
        case 'myo':
            nextAlphabet = 'rya';
            break;
        case 'rya':
            nextAlphabet = 'ryu';
            break;
        case 'ryu':
            nextAlphabet = 'ryo';
            break;
        case 'ryo':
            nextAlphabet = 'gya';
            break;
        case 'gya':
            nextAlphabet = 'gyu';
            break;
        case 'gyu':
            nextAlphabet = 'gyo';
            break;
        case 'gyo':
            nextAlphabet = 'ja';
            break;
        case 'ja':
            nextAlphabet = 'ju';
            break;
        case 'ju':
            nextAlphabet = 'jo';
            break;
        case 'jo':
            nextAlphabet = 'bya';
            break;
        case 'bya':
            nextAlphabet = 'byu';
            break;
        case 'byu':
            nextAlphabet = 'byo';
            break;
        case 'byo':
            nextAlphabet = 'pya';
            break;
        case 'pya':
            nextAlphabet = 'pyu';
            break;
        case 'pyu':
            nextAlphabet = 'pyo';
            break;
        case 'pyo':
            nextAlphabet = 'kya';
            break;
        default:
            var length = selectedAlphabet.length;
            if (length == 1) {
                nextAlphabet = nextAlphabetLine(selectedAlphabet);
            }
            else if (length == 2) {
                if (selectedAlphabet == 'sa') {
                    nextAlphabet = 'shi';
                }
                else if (selectedAlphabet == 'ta') {
                    nextAlphabet = 'chi';
                }
                else if (selectedAlphabet == 'hi') {
                    nextAlphabet = 'fu';
                }
                else if (selectedAlphabet == 'fu') {
                    nextAlphabet = 'he';
                }
                else if (selectedAlphabet == 'ya') {
                    nextAlphabet = 'yu';
                }
                else if (selectedAlphabet == 'yu') {
                    nextAlphabet = 'yo';
                }
                else if (selectedAlphabet == 'wa') {
                    nextAlphabet = 'wo';
                }
                else if (selectedAlphabet == 'za') {
                    nextAlphabet = 'ji';
                }
                else if (selectedAlphabet == 'ji') {
                    nextAlphabet = 'zu';
                }
                else {
                    nextAlphabet = selectedAlphabet[0] + '' + nextAlphabetLine(selectedAlphabet[1]);
                }
            }
            else if (length == 3) {
                if (selectedAlphabet == 'shi') {
                    nextAlphabet = 'su';
                }
                else if (selectedAlphabet == 'chi') {
                    nextAlphabet = 'tsu';
                }
                else if (selectedAlphabet == 'tsu') {
                    nextAlphabet = 'te';
                }
                else {
                    nextAlphabet = selectedAlphabet.substr(0, 2) + '' + nextAlphabetLine(selectedAlphabet[2]);
                }
            }
            break;
    }
    return nextAlphabet;
};
