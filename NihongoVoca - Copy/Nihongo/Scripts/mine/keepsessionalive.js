﻿var _keepSessionAlive = false;
var _keepSessionAliveUrl = null;

function setupSessionUpdater(actionUrl) {
    _keepSessionAliveUrl = actionUrl;
    var container = $("#body");
    container.mousemove(function () { _keepSessionAlive = true; });
    container.keydown(function () { _keepSessionAlive = true; });
    checkToKeepSessionAlive();
}

function checkToKeepSessionAlive() {
    setTimeout("keepSessionAlive()", 300000);
}

function keepSessionAlive() {
    if (_keepSessionAlive && _keepSessionAliveUrl != null) {
        $.ajax({
            type: "POST",
            url: _keepSessionAliveUrl,
            success: function () { _keepSessionAlive = false; }
        });
    }
    checkToKeepSessionAlive();
}

setupSessionUpdater('/Home/KeepSessionAlive');