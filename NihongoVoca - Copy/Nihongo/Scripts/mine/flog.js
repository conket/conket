
    // This is called with the results from from FB.getLoginStatus().
    function statusChangeCallback(response) {
        console.log('statusChangeCallback');
        console.log(response);
        // The response object is returned with a status field that lets the
        // app know the current login status of the person.
        // Full docs on the response object can be found in the documentation
        // for FB.getLoginStatus().
        //fLogin();
        if (response.status === 'connected') {
        
            // Logged into your app and Facebook.
            
            fLogin();
        } else if (response.status === 'not_authorized') {
            // The person is logged into Facebook, but not your app.
            $('#login-error-message').innerHTML = 'Please log ' +
              'into this app.';
        } else {
            // The person is not logged into Facebook, so we're not sure if
            // they are logged into this app or not.
            $('#login-error-message').innerHTML = 'Please log ' +
              'into Facebook.';
        }
    
    }

    // This function is called when someone finishes with the Login
    // Button.  See the onlogin handler attached to it in the sample
    // code below.
    function checkLoginState() {
        FB.login(function(response) {
            statusChangeCallback(response);
        }, { scope: 'public_profile,email' });
    }

    window.fbAsyncInit = function () {
        FB.init({
            appId: '224835707865231',
            cookie: true,  // enable cookies to allow the server to access 
            // the session
            xfbml: true,  // parse social plugins on this page
            version: 'v2.5' // use graph api version 2.5
        });

        // Now that we've initialized the JavaScript SDK, we call 
        // FB.getLoginStatus().  This function gets the state of the
        // person visiting this page and can return one of three states to
        // the callback you provide.  They can be:
        //
        // 1. Logged into your app ('connected')
        // 2. Logged into Facebook, but not your app ('not_authorized')
        // 3. Not logged into Facebook and can't tell if they are logged into
        //    your app or not.
        //
        // These three cases are handled in the callback function.

        FB.getLoginStatus(function (response) {
            statusChangeCallback(response);
        });

    };

    // Load the SDK asynchronously
    (function (d, s, id) {
        var js, fjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) { return; }
        js = d.createElement(s); js.id = id;
        js.src = "//connect.facebook.net/en_US/sdk.js";
        fjs.parentNode.insertBefore(js, fjs);
    }(document, 'script', 'facebook-jssdk'));

    // Here we run a very simple test of the Graph API after login is
    // successful.  See statusChangeCallback() for when this call is made.

    function fLogin() {
        console.log($('#u').val());
        if (!$('#u').val()) {
            
            FB.api('/me', function (response) {
                console.log(JSON.stringify(response));
                $.ajax({
                    url: '/Account/' + $('#flog').val(),
                    type: "POST",
                    async: false,
                    //data: { id: 'admin', email: 'admin@conket.com', first_name: 'Admin' },
                    data: JSON.stringify(response),
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
            });
        }
    }
