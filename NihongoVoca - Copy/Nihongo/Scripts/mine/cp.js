
$(document).ready(function () {
    $('#modalChangePassword').on('shown.bs.modal', function () {
        $('#npss').focus();

        $('#error-message-change-password').html('');
        $('#rnpss').val('');
        $('#npss').val('');
    });


    $('#changePasswordForm').submit(function () {
        $('#error-message-change-password').html('');
        //            if ($('#CurrentPassword').val() == '') {
        //                $('#error-message-change-password').html("Nhập [Mật khẩu hiện tại]");
        //                return false;
        //            }
        if ($('#npss').val() == '') {
            $('#error-message-change-password').html("Nhập [Mật khẩu mới]");
            $('#npss').focus();
            return false;
        }
        if ($('#npss').val().length < 6) {
            $('#error-message-change-password').html("[Mật khẩu mới] phải có độ dài ít nhất 6 kí tự");
            $('#npss').focus();
            return false;
        }
        if ($('#rnpss').val() == '') {
            $('#error-message-change-password').html("Nhập [Nhập lại mật khẩu mới]");
            $('#rnpss').focus();
            return false;
        }
        if ($('#npss').val() != $('#rnpss').val()) {
            $('#error-message-change-password').html("[Nhập lại mật khẩu mới] phải giống [Mật khẩu mới]");
            $('#rnpss').focus();
            return false;
        }

        $.ajax({
            cache: true,
            type: "post",
            async: false,
            url: '/Account/' + $('#cp').val(),
            data: $(this).serialize(),
            //data: { "Password": $('#npss').val(), "RePassword": $('#rnpss').val() },
            //dataType: "json",
            //contentType: 'application/x-www-form-urlencoded',
            //contentType: 'application/json',
            success: function (result) {
                if (result.ReturnCode != 0) {
                    $('#error-message-change-password').html('Có lỗi!');
                }
                else {
                    alert(result.Message);
                    $('#modalChangePassword').modal('hide');
                    //                        $('#error-message-change-password').html(result.Message);
                    //                        $('#rnpss').val('');
                    //                        $('#npss').val('');
                    //                        $('#npss').focus();
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                console.log(xhr.responseText);
            }
        });

        return false;
    });
});