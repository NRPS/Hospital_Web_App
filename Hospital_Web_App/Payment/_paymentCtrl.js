'use strict'

define([], function () {
    function PaymentCtrl($scope, $http, PaymentService, PatientService) {

        $scope.female = false;
        $scope.male = false;

        $scope.search = function (event) {
            if (event.which == 13) {
                if (!$scope.searchForm.$valid) {
                    $scope.srchFrmSubmitted = true;
                }
                else if ($scope.searchForm.$valid) {
                    $scope.isLoading = true;
                    var Pid = $scope.searchItem;
                    if (Pid.charAt(4) === 'C') {
                        var responseReceipt = PaymentService.getPaymentDetails(Pid);

                        responseReceipt.success(function (data, status, headers, config) {
                            getPaymentData(data);

                            var response = PatientService.getPatientDataById(data.PatientID);
                            $scope.isLoading = false;

                            response.success(function (data, status, headers, config) {
                                setData(data);
                                $scope.isLoading = false;
                            })
                            response.error(function (data, status, headers, config) {
                                alert(status.Message);
                            });

                            var res = PaymentService.getPatientBalanceAmount(data.PatientID);
                            res.success(function (data, status, headers, config) {
                                $scope.balanceAmount = data.Balance;
                            })
                            res.error(function (data, status, headers, config) {
                                alert(status.Message);
                            });

                        })
                        responseReceipt.error(function (data, status, headers, config) {
                            alert(status.Message);
                        });
                    }
                    else {
                        var response = PatientService.getPatientDataById(Pid);
                        $scope.isLoading = false;

                        response.success(function (data, status, headers, config) {
                            setData(data);
                            $scope.isLoading = false;
                        })
                        response.error(function (data, status, headers, config) {
                            alert(status.Message);
                        });

                        var res = PaymentService.getPatientBalanceAmount(Pid);
                        res.success(function (data, status, headers, config) {
                            $scope.balanceAmount = data.Balance;
                        })
                        res.error(function (data, status, headers, config) {
                            alert(status.Message);
                        });
                    }
                }
            }
        }


        $scope.GetAmountDue = function () {
            $scope.amountDue = $scope.balanceAmount - $scope.payNow;
        }
        $scope.addNewPayment = function () {
            var response = PaymentService.addNewPayment(setPaymentData());
            response.success(function (data, status, headers, config) {
                alert('Payment Details Saved');
                refreshForm();
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


        function setData(response) {
            if (response) {
                $scope.patientId = response.PatientID;
                $scope.patientName = response.Name;
                $scope.age = response.Age;
                $scope.attendantName = response.AttendentName;
                $scope.address = response.Address;
                $scope.contactNo = response.ContactNumber1;
                var paymentDate = new Date(response.paymentDate);
                paymentDate = paymentDate.getDay() + '-' + (paymentDate.getMonth() + 1) + '-' + paymentDate.getFullYear();
                $scope.paymentDate = new Date(paymentDate);

                $scope.sex = response.Sex;

                if (response.TypeID === 1)
                    $scope.patientType = "OPD";
                else
                    $scope.patientType = "IPD";

            }
            else {
                alert('Some thing was wrong');
            }
        }


        function getPaymentData(response) {
            if (response) {
                $scope.patientId = response.PatientID;
                $scope.remarks = response.Remarks;
                $scope.recieptNo = response.PaymentReceiptNo;
                $scope.payNow = response.Amount;
            }
            else {
                alert('Some thing was wrong');
            }
        };

        function setPaymentData() {
            var paymentData = {};
            paymentData.PaymentReceiptNo = $scope.recieptNo;
            paymentData.PatientID = $scope.patientId;
            paymentData.Amount = $scope.payNow;
            paymentData.PaymentMode = "C";
            paymentData.PaymentDate = $scope.dt;
            paymentData.Remarks = $scope.remarks;

            return paymentData;
        };

        function refreshForm() {
            $scope.patientId = '';
            $scope.patientName = '';
            $scope.age ='';
            $scope.attendantName = '';
            $scope.address = '';
            $scope.contactNo = '';
            $scope.sex ='';
            $scope.patientType = '';
            $scope.payNow = '';
            $scope.amountDue = '';
            $scope.balanceAmount = '';
            $scope.remarks = '';
            $scope.paymentForm.$setPristine();

        };

        function isArray(x) {
            return x.constructor.toString().indexOf("Array") > -1;
        }
    }
    PaymentCtrl.$inject = ['$scope', '$http', 'PaymentService', 'PatientService'];
    return PaymentCtrl;
})