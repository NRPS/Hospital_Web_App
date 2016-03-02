'use strict';

define([], function () {
    function config($routeProvider) {
        $routeProvider.when('/PatientReg', {
            templateUrl: 'Patient_Registration/_patientRegistration.html',
            controller: 'PatientRegiCtrl'
        })
       .when('/PaymentRecvForm', {
           templateUrl: 'Payment/_paymentRecieveForm.html'
       })
       .when('/BillEntry', {
           templateUrl: 'Payment/_billEntryForm.html'
       })
       .when('/SignUp', {
            templateUrl: 'User_Registration/_signUp.html',
            controller: 'UserRegistrationCtrl'
        })
       .when('/Login', {
            templateUrl: 'Login/_login.html',
            controller: 'LoginCtrl'
        })
       .when('/SignOut', {
           templateUrl: 'Login/_signOut.html',
           controller:'SignOutCtrl'
        });
    }

    config.$inject = ['$routeProvider'];
    return config;
});