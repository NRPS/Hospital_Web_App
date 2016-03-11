'use strict';

define([], function () {
    function PatientService($http) {
        var service = {};
        var response = {};
        var uri = "http://localhost:2054/api/";
        service.getPatientDataById = function (patientId) {
            response = $http({
                url: uri + 'patientRegstration' + '/' + patientId,
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
            return response;
        };
        service.getReferedByList = function () {
            response = $http({
                url: uri + 'referedBy',
                method: 'GET',
                dataType:'joson'
            });
            return response;
        };
        return service;
    };
    PatientService.$inject = ['$http'];
    return PatientService;
})