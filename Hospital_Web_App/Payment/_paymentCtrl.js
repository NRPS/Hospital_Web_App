'use strict'

define([], function () {
    function PaymentCtrl($scope, $http, PaymentService) {

        $scope.female = false;
        $scope.male = false;


        getPatientTypeList();

        $scope.search = function (event) {
            if (event.which == 13) {
                if (!$scope.form1.$valid) {
                    $scope.submitted = true;
                }
                else if ($scope.form1.$valid) {
                    $scope.isLoading = true;
                    var response = PaymentService.getPatientDataById($scope.patientId);
                    response.success(function (data, status, headers, config) {
                        setData(data);
                        $scope.isLoading = false;
                    })
                    response.error(function (data, status, headers, config) {
                        alert(status.Message);
                    });
                }
            }
        }

        $scope.addNewPayment = function () {
            var response = PaymentService.addNewPayment(setPaymentData());
            response.success(function (data, status, headers, config) {
                console.log(data);
            });
            response.error(function (data, status, headers, config) {
            });
        };

        $scope.deletePayment = function () {
            var ajaxResponse = PaymentService.deletePaymentById($scope.patientId);
            ajaxResponse.success(function (data, status, headers, config) {
                
            })
            ajaxResponse.error(function (data, status, headers, config) {
                alert(status.Message);
            });
        }

        $scope.updatePayment = function () {
            var response = PaymentService.updatePayment(setPaymentData());
            response.success(function (data, status, headers, config) {
                console.log(data);
            });
            response.error(function (data, status, headers, config) {

            });
        };

        $scope.today = function () {
            $scope.dt = new Date();
        };
        $scope.today();

        $scope.popup = {
            opened: false
        };
        $scope.openDatePickerPopup = function () {
            $scope.popup.opened = true;
        };

        //function getReferedByList() {
        //    var ajaxResponse = PaymentService.getReferedByList();
        //    ajaxResponse.success(function (data, status, headers, config) {
        //        $scope.drName = data[0];
        //        $scope.referedByList = data;
        //    })
        //    ajaxResponse.error(function (data, status, headers, config) {
        //        alert(status.Message);
        //    });
        //};

        //function getDeparmentList() {
        //    var ajaxResponse = PaymentService.getDeparmentList();
        //    ajaxResponse.success(function (data, status, headers, config) {
        //        $scope.depName = data[0];
        //        $scope.departmentList = data;
        //    })
        //    ajaxResponse.error(function (data, status, headers, config) {
        //        alert(status.Message);
        //    });
        //};

        function getPatientTypeList() {
            var ajaxResponse = PaymentService.getPatientTypeList();
            ajaxResponse.success(function (data, status, headers, config) {
                $scope.patientType = data[0];
                $scope.patientTypeList = data;

            })
            ajaxResponse.error(function (data, status, headers, config) {
                alert(status.Message);
            });
        };

        function setData(response) {
            if (response) {
                $scope.patientId = response.PatientID;
                $scope.patientName = response.Name;
                $scope.age = response.Age;
                $scope.attendantName = response.AttendentName;
                $scope.address = response.Address;
                $scope.contactNo = '+91' + response.ContactNumber1;
              //  $scope.remarks = response.Remarks;

                var paymentDate = new Date(response.paymentDate);
                paymentDate = paymentDate.getFullYear() + '-' + (paymentDate.getMonth() + 1) + '-' + paymentDate.getDay();
                $scope.paymentDate = new Date(paymentDate);

                $scope.sex = response.Sex;


                if ($scope.patientTypeList) {
                    var len = $scope.patientTypeList.length;
                    for (var i = 0; i < len; i++) {
                        if ($scope.patientTypeList[i].ID === response.ID) {
                            $scope.patientType = $scope.patientTypeList[i];
                            break;
                        }
                    }
                }
            }
            else {
                alert('Some thing was wrong');
            }
        }

        function setPaymentData() {
            var paymentData = {};

            paymentData.PatientID = $scope.patientId;
            paymentData.Amount = $scope.payNow;
            paymentData.PaymentMode = "1";
            paymentData.PaymentDate = $scope.dt;
            paymentData.Remarks = $scope.remarks;

            return paymentData;
        }
        function isArray(x) {
            return x.constructor.toString().indexOf("Array") > -1;
        }
    }
    PaymentCtrl.$inject = ['$scope', '$http', 'PatientService'];
    return PaymentCtrl;
})