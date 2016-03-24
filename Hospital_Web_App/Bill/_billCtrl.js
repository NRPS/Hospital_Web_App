'use strict'

define([], function () {
    function BillCtrl($scope, $http, BillService) {

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
                    var response = BillService.getPatientDataById($scope.patientId);
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

        $scope.searchBill = function (event) {
            if (event.which == 13) {
                if (!$scope.form1.$valid) {
                    $scope.submitted = true;
                }
                else if ($scope.form1.$valid) {
                    $scope.isLoading = true;
                    var response = BillService.getPatientBill($scope.billNo);
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
        $scope.addNewBill = function () {
            var response = BillService.addNewBill(setBillData());
            response.success(function (data, status, headers, config) {
                console.log(data);
            });
            response.error(function (data, status, headers, config) {
            });
        };

        $scope.deleteBill = function () {
            var ajaxResponse = BillService.deleteBill($scope.patientId);
            ajaxResponse.success(function (data, status, headers, config) {
            })
            ajaxResponse.error(function (data, status, headers, config) {
                alert(status.Message);
            });
        }

        $scope.updateBill = function () {
            var response = BillService.updateBill(setBillData());
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

        function getPatientTypeList() {
            var ajaxResponse = BillService.getPatientTypeList();
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
                $scope.remarks = response.Remarks;
                $scope.sex = response.Sex;

                var billDate = new Date(response.BillDate);
                billDate = billDate.getFullYear() + '-' + (billDate.getMonth() + 1) + '-' + billDate.getDay();
                $scope.billDate = new Date(billDate);

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

        function setBillData() {
            var patientData = {};
            patientData.PatientID = $scope.patientId;
            patientData.Name = $scope.patientName;
            patientData.Age = $scope.age;
            patientData.AttendentName = $scope.attendantName;
            patientData.Address = $scope.address;
            patientData.ContactNumber1 = $scope.contactNo;
            patientData.Sex = $scope.sex;
            if($scope.patientType){
                if (typeof ($scope.patientType) === "object") {
                    patientData.Type = $scope.patientType.ID;
                    }
                else {
                    patientData.Type = $scope.patientType.Type;
                }
            }
            return patientData;
        }
        function isArray(x) {
            return x.constructor.toString().indexOf("Array") > -1;
        }
    }
    BillCtrl.$inject = ['$scope', '$http', 'PatientService'];
    return BillCtrl;
})