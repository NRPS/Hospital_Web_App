﻿'use strict';

define([], function () {
    function PatientService($http) {
        var service = {};
        var response = {};
        var uri = "http://localhost/HospitalWebAPI/api/";
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

        service.addNewPatient = function (patient) {
            response = $http({
                url: uri + 'patientRegstration',
                data: JSON.stringify(patient),
                method: "POST",
                dataType: "json",
                async: false,
            });
            return response;
        };

        service.deletePatientById = function (patientId) {
            response = $http({
                url: uri + 'patientRegstration' + '/' + patientId,
                method: "DELETE",
                dataType: "json",
                async: false,
            })
            return response;
        };

        service.updatePatient = function (patient) {
            response = $http({
                url: uri + 'patientRegstration',
                data: JSON.stringify(patient),
                method: "PUT",
                dataType: "json",
                async: false,
            });
            return response;
        };

        service.getReferedByList = function () {
            response = $http({
                url: uri + 'referedBy',
                method: 'GET',
                dataType: 'joson'
            });
            return response;
        };
        service.getDeparmentList = function () {
            response = $http({
                url: uri + 'department',
                method: 'GET',
                dataType: 'joson'
            });
            return response;
        };
        service.getPatientTypeList = function () {
            response = $http({
                url: uri + 'patientType',
                method: 'GET',
                dataType: 'joson'
            });
            return response;
        };
        return service;
    };
    PatientService.$inject = ['$http'];
    return PatientService;
})