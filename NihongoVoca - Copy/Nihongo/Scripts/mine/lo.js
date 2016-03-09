
    
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
                            if (result.User == null) {
                                c++;
                                var mess = "Sai [Tên Đăng Nhập] hoặc [Mật Khẩu]";
                                $('#login-error-message').html(mess);
                                $('#pss').val('');
                                $('#pss').focus();
                                return false;
                            }
                            else {
                                if (result.ReturnUrl != '') {
                                    window.location = result.ReturnUrl;
                                }
                                else {
                                    window.location.reload();
                                }
                            }

                            return false;
                        }
                        else {
                            alert('Xảy ra lỗi!');
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
        FB.logout(function (response) {
            // Person is now logged out
            
        });
    });
});

