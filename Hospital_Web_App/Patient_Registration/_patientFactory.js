'use strict';

define([], function () {
    function PatientService($http) {
        var service = {};
        var response = {};
        var uri = "http://localhost:2054/api/patientRegstration";
        service.getPatientDataById = function (patientId) {
            response = $http({
                url: uri + '/' + patientId,
                method: "GET",
                // data: "{'studentData':'" + studentData + "'}",
                //headers: { 'Content-Type': "application/json; charset=utf-8" },
                dataType: "json",
                async: false,
            })
            return response;
        };

        service.addNewPatient = function (patientData) {
            response = $http({
                url: uri,
                data: "{'PatientData':'" + patientData + "'}",
                method: "POST",
                dataType: "json",
                async:false,
            });
        };

        return service;
    };
    PatientService.$inject = ['$http'];
    return PatientService;
})