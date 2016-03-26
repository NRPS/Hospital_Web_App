'use strict';

define(
    [
        'Config/config',
        'Config/run',
        'Login/_login',
        'User_Registration/_signUp',
        'Login/base64Encoder',
        'Login/loginFactory',
        'Login/_signOut',
        'Patient_Registration/_patientRegiCtrl',
        'Patient_Registration/_patientFactory',
        'Payment/_paymentCtrl',
        'Payment/_paymentFactory',
        'Bill/_BillCtrl',
        'Bill/_BillFactory',
        'bower_components/angular-ui-bootstrap/ui-bootstrap',
    ],
    function (Config, RunApp, Login, SignUp, Base64Encoder, logInfactory, SignOutCtrl, PatientRegiCtrl, PatientService, PaymentCtrl, PaymentService,BillCtrl, BillService) {

        var app = angular.module('hospital', ['ngRoute', 'ngCookies', 'ui.bootstrap']);
        
        app.config(Config);
        app.run(RunApp);
        app.factory('Base64', Base64Encoder);
        app.factory('LoginService', logInfactory);
        app.factory('PatientService', PatientService)
        app.factory('PaymentService', PaymentService)
        app.factory('BillService', BillService)
        app.controller('LoginCtrl', Login);
        app.controller('UserRegistrationCtrl', SignUp);
        app.controller('SignOutCtrl', SignOutCtrl)
        app.controller('PatientRegiCtrl', PatientRegiCtrl)
        app.controller('PaymentCtrl', PaymentCtrl)
        app.controller('BillCtrl', BillCtrl)
        app.controller('AppCtrl', ['$scope', '$rootScope', '$cookieStore', function ($scope, $rootScope, $cookieStore) {
            if ($cookieStore.get('globals') != undefined) {
                $scope.login = false;
                $scope.logOut = true;
                $scope.loggedInUser = 'Welcome:' + ' ' + $cookieStore.get('globals').currentUser.username;
            }
            else {
                $scope.login = true;
                $scope.logOut = false;
            }
        }]);
    });