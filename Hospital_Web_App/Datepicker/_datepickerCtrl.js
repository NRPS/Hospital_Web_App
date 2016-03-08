'use strict';
define([], function () {
    function DatePickerCtrl($scope) {
        $scope.popup = {
            opened: false
        };
        $scope.openDatePickerPopup = function () {
            $scope.popup.opened = true;
        };
    }
    DatePickerCtrl.$inject = ['$scope'];
    return DatePickerCtrl;
});