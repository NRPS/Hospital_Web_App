'use strict';
define([], function () {
    function LoginCtrl($scope, $http, $cookieStore, $rootScope, $timeout, $location, LoginService) {

        $scope.login = function () {
            LoginService.login($scope.txtEmailId, $scope.txtpassword, function (response) {
                if (response.success) {
                    LoginService.SetCredentials($scope.txtEmailId, $scope.txtpassword);
                    $location.path('/');
                    if ($rootScope.globals.currentUser != undefined) {
                        $rootScope.loggedInUser = 'Welcome:' + ' ' + $rootScope.globals.currentUser.username;
                    }
                    else {
                        $rootScope.loggedInUser = '';
                    }
                } else {
                    $scope.error = response.message;
                    $scope.dataLoading = false;
                }
            });
        }
    }
    LoginCtrl.$inject = ['$scope', '$http', '$cookieStore', '$rootScope', '$timeout', '$location', 'LoginService'];
    return LoginCtrl;
});
