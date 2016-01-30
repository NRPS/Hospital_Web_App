'use strict';
define([], function () {
    function LoginCtrl($scope, $location,$rootScope) {
        $scope.login = function () {
            if (!$scope.loginForm.$valid) {
                $scope.submitted = true;
            }
            else if ($scope.loginForm.$valid) {
                var uid = $scope.txtEmailId;
                var passowrd = $scope.txtpassword;
                var userName = 'Prem Prakash'
            }
        }
    }
    LoginCtrl.$inject = ['$scope', '$location','$rootScope'];
    return LoginCtrl;
});
