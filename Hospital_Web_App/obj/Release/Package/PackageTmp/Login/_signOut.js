'use strict'

define([], function () {
    function SignOutCtrl($scope, $rootScope,$location, LoginService) {
        LoginService.ClearCredentials();
        $location.path('/Login');
    }
    SignOutCtrl.$inject = ['$scope', '$rootScope', '$location', 'LoginService'];
    return SignOutCtrl;
});