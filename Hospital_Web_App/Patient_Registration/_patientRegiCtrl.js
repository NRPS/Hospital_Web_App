'use strict'

define([], function () {
    function PatientRegiCtrl($scope, $http, PatientService) {
        // var patientId = 'P2016020001';
        //var ajaxResponse;
        $scope.female = false;
        $scope.male = false;

        getReferedByList();
        getDeparmentList();
        getPatientTypeList();

        $scope.search = function (event) {
            if (event.which == 13) {
                if (!$scope.form1.$valid) {
                    $scope.submitted = true;
                }
                else if ($scope.form1.$valid) {
                    $scope.isLoading = true;
                    var response = PatientService.getPatientDataById($scope.patientId);
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

        $scope.addNewPatient = function () {
            var response = PatientService.addNewPatient(setPatientData());
            response.success(function (data, status, headers, config) {
                console.log(data);
            });
            response.error(function (data, status, headers, config) {

            });
        };

        $scope.deletePatient = function () {
            var ajaxResponse = PatientService.deletePatientById($scope.patientId);
            ajaxResponse.success(function (data, status, headers, config) {
                
            })
            ajaxResponse.error(function (data, status, headers, config) {
                alert(status.Message);
            });
        }

        $scope.updatePatient = function () {
            var response = PatientService.updatePatient(setPatientData());
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

        function getReferedByList() {
           var ajaxResponse = PatientService.getReferedByList();
            ajaxResponse.success(function (data, status, headers, config) {
                $scope.drName = data[0];
                $scope.referedByList = data;

            })
            ajaxResponse.error(function (data, status, headers, config) {
                alert(status.Message);
            });
        };

        function getDeparmentList() {
            var ajaxResponse = PatientService.getDeparmentList();
            ajaxResponse.success(function (data, status, headers, config) {
                $scope.depName = data[0];
                $scope.departmentList = data;

            })
            ajaxResponse.error(function (data, status, headers, config) {
                alert(status.Message);
            });
        };

        function getPatientTypeList() {
            var ajaxResponse = PatientService.getPatientTypeList();
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
                $scope.email = response.Email;
                $scope.consultantName = response.ConsultantName;
                $scope.Department = response.DepartmentID;
                $scope.consultantFee = response.ConsultantFee;
                $scope.remarks = response.Remarks;
                var dateOfAdmit = new Date(response.RegDate);
                dateOfAdmit = dateOfAdmit.getFullYear() + '-' + (dateOfAdmit.getMonth() + 1) + '-' + dateOfAdmit.getDate();
                $scope.dt = new Date(dateOfAdmit);
                $scope.sex = response.Sex;
                if ($scope.referedByList) {
                    var len = $scope.referedByList.length;
                    for (var i = 0; i < len; i++) {
                        if ($scope.referedByList[i].RefID === response.RefDrID) {
                            $scope.drName = $scope.referedByList[i];
                            break;
                        }
                    }
                }
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

        function setPatientData() {
            var patientData = {};
            patientData.PatientID = $scope.patientId;
            patientData.Name = $scope.patientName;
            patientData.Age = $scope.age;
            patientData.AttendentName = $scope.attendantName;
            patientData.Address = $scope.address;
            patientData.ContactNumber1 = $scope.contactNo;
            patientData.Email = $scope.email;
            patientData.ConsultantName = $scope.consultantName;
            patientData.DepartmentID = $scope.department;
            patientData.ConsultantFee = $scope.consultantFee;
            patientData.Remarks = $scope.remarks;
            patientData.RegDate = $scope.dt;
            patientData.Sex = $scope.sex;
            if($scope.patientType){
                //var len = $scope.patientTypeList.length;
                if (typeof ($scope.patientType) === "object") {
                    patientData.Type = $scope.patientType.ID;
                    }
                else {
                    patientData.Type = $scope.patientType.Type;
                }
            }
            if (typeof ($scope.depName) === "object") {
                patientData.DepartmentID = $scope.depName.ID;
            }
            else{
                patientData.DepartmentID = $scope.depName
            }
           
            return patientData;
        }
        function isArray(x) {
            return x.constructor.toString().indexOf("Array") > -1;
        }
    }
    PatientRegiCtrl.$inject = ['$scope', '$http', 'PatientService'];
    return PatientRegiCtrl;
})