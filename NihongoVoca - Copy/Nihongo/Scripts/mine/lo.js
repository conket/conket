

$(document).ready(function () {
    var c = 1;
    $('#modalLogin2').on('shown.bs.modal', function () {
        c = 1;
        $('#usr').focus();

        $('#login-error-message').html('');
        $('#usr').val('');
        $('#pss').val('');
    });

    $('#loginForm').submit(function () {

        $('#login-error-message').html("");
        if ($('#usr').val() == '') {
            $('#login-error-message').html("Nhập [Tên đăng nhập]!");
            $('#usr').focus();
            return false;
        }
        if ($('#pss').val() == '') {
            $('#login-error-message').html("Nhập [Mật khẩu]!");
            $('#pss').focus();
            return false;
        }
        if ($('#pss').val().length < 6) {
            $('#login-error-message').html("Sai [Mật khẩu]!");
            $('#pss').focus();
            return false;
        }

        if (c < 5) {
            if ($('#loginForm').valid()) {
                $.ajax({
                    url: '/Account/' + $('#log').val(),
                    type: "POST",
                    async: false,
                    data: $(this).serialize(),
                    //contentType: 'application/json',
                    success: function (result) {
                        if (result.ReturnCode == 0) {
                            if (result.ID == 0) {
                                c++;
                                var mess = "Sai [Tên Đăng Nhập] hoặc [Mật Khẩu]";
                                $('#login-error-message').html(mess);
                                $('#pss').val('');
                                $('#pss').focus();
                                return false;
                            }
                            else {
                                //if (result.ReturnUrl != '') {
                                //    window.location = result.ReturnUrl;
                                //}
                                //else {
                                //window.location.reload();
                                window.location = '/trang-ca-nhan/HomePage/' + result.ID;
                                //}
                            }

                            return false;
                        }
                        else {
                            console.log('Xảy ra lỗi!');
                            //alert('Xảy ra lỗi!');
                        }
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        console.log(xhr.responseText);
                        return false;
                    }
                });
            }

        }
        else {
            alert('Bạn đã đăng nhập quá 5 lần. Hãy thử lại sau vài phút!');
            location.reload();
        }
        return false;
    });

    $('#btnLogout').on('click', function () {

        FB.getLoginStatus(function (response) {
            if (response.status === 'connected') {
                // the user is logged in and has authenticated your
                // app, and response.authResponse supplies
                // the user's ID, a valid access token, a signed
                // request, and the time the access token 
                // and signed request each expire
                var uid = response.authResponse.userID;
                var accessToken = response.authResponse.accessToken;

                FB.api('/' + uid + '/permissions', 'delete', function (response) { });

            } else if (response.status === 'not_authorized') {
                // the user is logged in to Facebook, 
                // but has not authenticated your app
            } else {
                // the user isn't logged in to Facebook.
            }
        });
        //FB.logout(function (response) {
        //    // Person is now logged out

        //});
    });
});

