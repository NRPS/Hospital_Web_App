
'use strict';
define([],function(){
    function UserRegistrationCtrl($scope){
        console.log('hello Sign Up');
        $scope.signUp = function () {
            if (!$scope.myform.$valid) {
                $scope.submitted = true;
            }
        }
    }
    UserRegistrationCtrl.$inject = ['$scope'];
    return UserRegistrationCtrl;
});