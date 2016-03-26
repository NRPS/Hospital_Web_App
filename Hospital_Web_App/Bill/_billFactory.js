'use strict';

define([], function () {
    function BillService($http) {
        var service = {};
        var response = {};
        var uri = "http://localhost/HospitalWebAPI/api/";

        service.getPatientDataById = function (patientId) {
            response = $http({
                url: uri + 'patientRegstration' + '/' + patientId,
                method: "GET",
                dataType: "jsonp",
                async: false,
            })
            return response;
        };

        service.getPatientBill = function (billNo) {
            response = $http({
                url: uri + 'PatientBill' + '/' + billNo,
                method: "GET",
                dataType: "json",
                async: false,
            })
            return response;
        };

        service.addNewBill = function (patient) {
            response = $http({
                url: uri + 'PatientBill',
                data: JSON.stringify(patient),
                method: "POST",
                dataType: "json",
                async: false,
            });
            return response;
        };

        service.deleteBill = function (billNo) {
            response = $http({
                url: uri + 'PatientBill' + '/' + billNo,
                method: "DELETE",
                dataType: "json",
                async: false,
            })
            return response;
        };

        service.updateBill = function (bill) {
            response = $http({
                url: uri + 'PatientBill',
                data: JSON.stringify(bill),
                method: "PUT",
                dataType: "json",
                async: false,
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
    BillService.$inject = ['$http'];
    return BillService;
})