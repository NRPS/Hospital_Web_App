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
        'Patient_Registration/_patientFactory'
    ],
    function (Config, RunApp, Login, SignUp, Base64Encoder, logInfactory, SignOutCtrl, PatientRegiCtrl, PatientService) {
        
        var app = angular.module('hospital', ['ngRoute', 'ngCookies']);

        app.config(Config);
        app.run(RunApp);
        app.factory('Base64', Base64Encoder);
        app.factory('LoginService', logInfactory);
        app.factory('PatientService', PatientService)
        app.controller('LoginCtrl', Login);
        app.controller('UserRegistrationCtrl', SignUp);
        app.controller('SignOutCtrl', SignOutCtrl)
        app.controller('PatientRegiCtrl', PatientRegiCtrl)
        app.controller('AppCtrl', ['$scope', '$rootScope', '$cookieStore', function ($scope, $rootScope, $cookieStore) {
            if ($cookieStore.get('globals')!=undefined) {
                $scope.login = false;
                $scope.logOut = true;
                $scope.loggedInUser = 'Welcome:' + ' ' + $cookieStore.get('globals').currentUser.username;
            }
            else {
                $scope.login = true;
                $scope.logOut = false;
            }
        }]);

       // return angularAMD.bootstrap(app);
});