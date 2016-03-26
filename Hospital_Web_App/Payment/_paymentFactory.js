'use strict';

define([], function () {
    function PaymentService($http) {
        var service = {};
        var response = {};
        var uri = "http://localhost/HospitalWebAPI/api/";
        service.getPatientDataById = function (patientId) {
            response = $http({
                url: uri + 'patientRegstration' + '/' + patientId,
                method: "GET",
                dataType: "json",
                async: false,
            })
            return response;
        };

        //service.getPatientBalanceAmount = function (patientId) {
        //    response = $http({
        //        url: uri + 'BalanceAmount' + '/' + patientId,
        //        method: "GET",
        //        dataType: "json",
        //        async: false,
        //    })
        //    return response;
        //};

        service.addNewPayment = function (payment) {
            response = $http({
                url: uri + 'payment',
                data: JSON.stringify(payment),
                method: "POST",
                dataType: "json",
                async: false,
            });
            return response;
        };

        service.deletePaymentById = function (patientId) {
            response = $http({
                url: uri + 'payment' + '/' + patientId,
                method: "DELETE",
                dataType: "json",
                async: false,
            })
            return response;
        };

        service.updatePayment = function (payment) {
            response = $http({
                url: uri + 'payment',
                data: JSON.stringify(payment),
                method: "PUT",
                dataType: "json",
                async: false,
            });
            return response;
        };

        //service.getReferedByList = function () {
        //    response = $http({
        //        url: uri + 'referedBy',
        //        method: 'GET',
        //        dataType: 'joson'
        //    });
        //    return response;
        //};

        //service.getDeparmentList = function () {
        //    response = $http({
        //        url: uri + 'department',
        //        method: 'GET',
        //        dataType: 'joson'
        //    });
        //    return response;
        //};

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
    PaymentService.$inject = ['$http'];
    return PaymentService;
})