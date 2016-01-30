
define(
    [
        'angularAMD',
        'Config/config',
        'Login/_login',
        'User_Registration/_signUp'
    ],
    function (angularAMD,Config, Login,SignUp) {
        var hospital = angular.module('hospital', ['ngRoute']);

        hospital.controller('AppCtrl', ['$scope', function ($scope) {

        }]);

        hospital.config(Config);
        hospital.controller('LoginCtrl', Login);
        hospital.controller('UserRegistrationCtrl', SignUp);

        return angularAMD.bootstrap(hospital);
});