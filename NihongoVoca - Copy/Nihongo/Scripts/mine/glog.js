var googleUser = {};
var auth2;

var startApp = function () {
    gapi.load('auth2', function () {
        // Retrieve the singleton for the GoogleAuth library and set up the client.
        auth2 = gapi.auth2.init({
            client_id: '486695708439-9ou47gpomoc4n3t1c4cdrivfeaj8u6oh.apps.googleusercontent.com',
            cookiepolicy: 'single_host_origin',
            // Request scopes in addition to 'profile' and 'email'
            scope: 'profile email'
        });

        //_Login2Partial view's btnGLog
        attachSignin(document.getElementById('btnGLog'));
    });
};

function attachSignin(element) {
    
    auth2.attachClickHandler(element, {},
        function (googleUser) {
            var profile = googleUser.getBasicProfile();
            console.log('ID: ' + profile.getId()); // Do not send to your backend! Use an ID token instead.
            console.log('Name: ' + profile.getName());
            console.log('Image URL: ' + profile.getImageUrl());
            console.log('Email: ' + profile.getEmail());
            var id_token = googleUser.getAuthResponse().id_token;

            var user =
                {
                    id: googleUser.getAuthResponse().id_token,
                    name: profile.getName(),
                    email: profile.getEmail(),

                };
            //var user =
            //    {
            //        id: 'admin1',
            //        name: 'Admin 1',
            //        email: 'admin1@conket.com',

            //    };
            gLogin(user);
            //document.getElementById('name').innerText = "Signed in: " +
            //    googleUser.getBasicProfile().getName();
        }, function (error) {
            console.log(JSON.stringify(error, undefined, 2));
        });
}

function gLogin(user) {

    $.ajax({
        url: '/Account/' + $('#glog').val(),
        type: "POST",
        async: false,
        //data: { id: 'admin', email: 'admin@conket.com', first_name: 'Admin' },
        data: JSON.stringify(user),
        contentType: 'application/json; charset=utf-8',
        success: function (result) {
            if (result.ReturnCode == 0) {
                if (result.ReturnUrl) {
                    window.location = result.ReturnUrl;
                }
                else {
                    window.location.reload();
                }
                return false;
            }
            else {
                console.log(result.Message);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            jsonValue = jQuery.parseJSON(xhr.responseText);
            console.log(jsonValue.Message);
            return false;
        }
    });
}

startApp();