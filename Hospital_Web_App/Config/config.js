'use strict';

define([], function () {
    function config($routeProvider) {
        $routeProvider.when('/Home', {
            templateUrl: 'View/_home.html'
        })
       .when('/SignUp', {
            templateUrl: 'User_Registration/_signUp.html',
            controller: 'UserRegistrationCtrl'
        })
       .when('/Login', {
            templateUrl: 'Login/_login.html',
            controller: 'LoginCtrl'
        })
       .when('/RegisterGifts', {
            templateUrl: 'Gift/_registerYourGift.html'
        });
    }

    config.$inject = ['$routeProvider'];
    return config;
});