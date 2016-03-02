'use strict'

define([], function () {
    function PatientRegiCtrl($scope, $http) {
        $http.get('http://localhost:2054/api/patientRegstration').success(function (response) {
            console.log(response);
            var patientData = response;
            $scope.patientId = patientData[0].PatientID;
            $scope.patientName = patientData[0].Name;
        });
    }
    PatientRegiCtrl.$inject = ['$scope', '$http'];
    return PatientRegiCtrl;
})