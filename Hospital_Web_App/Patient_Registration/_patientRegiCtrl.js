'use strict'

define([], function () {
    function PatientRegiCtrl($scope, $http, PatientService) {
        // var patientId = 'P2016020001';
        //var ajaxResponse;
        $scope.female = true;
        $scope.male = false;
        $scope.format = 'dd - MM - yyyy';

        getReferedByList();
        getDeparmentList();
        getPatientTypeList();

        $scope.search = function (event) {
            if (event.which == 13) {
                if (!$scope.searchForm.$valid) {
                    $scope.srchFrmSubmitted = true;
                }
                else if ($scope.searchForm.$valid) {
                    $scope.isLoading = true;
                    var response = PatientService.getPatientDataById($scope.searchItem);
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

            if (!$scope.patientDeatilsForm.$valid) {
                $scope.submitted = true;
            }
            else if ($scope.patientDeatilsForm.$valid) {
                var response = PatientService.addNewPatient(setPatientData());
                response.success(function (data, status, headers, config) {
                    alert('Patient Data Saved Successfully');
                    refreshForm();
                });
                response.error(function (data, status, headers, config) {

                });
            }
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
                $scope.referedByList = data;
                $scope.drName = $scope.referedByList[0].ID;
            })
            ajaxResponse.error(function (data, status, headers, config) {
                alert(status.Message);
            });
        };

        function getDeparmentList() {
            var ajaxResponse = PatientService.getDeparmentList();
            ajaxResponse.success(function (data, status, headers, config) {
                $scope.depName = data[0].ID;
                $scope.departmentList = data;

            })
            ajaxResponse.error(function (data, status, headers, config) {
                alert(status.Message);
            });
        };

        function getPatientTypeList() {
            var ajaxResponse = PatientService.getPatientTypeList();
            ajaxResponse.success(function (data, status, headers, config) {
                $scope.patientType = data[0].ID;
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
                $scope.contactNo =  response.ContactNumber1;
                $scope.email = response.Email;
                $scope.consultantName = response.ConsultantName;
                $scope.Department = response.DepartmentID;
                $scope.consultantFee = response.ConsultantFee;
                $scope.remarks = response.Remarks;
                var dateOfAdmit = new Date(response.RegDate);
                dateOfAdmit = dateOfAdmit.getFullYear() + '-' + (dateOfAdmit.getMonth() + 1) + '-' + dateOfAdmit.getDate();
                $scope.dt = new Date(dateOfAdmit);
                $scope.sex = response.Sex;
                //if ($scope.referedByList) {
                    //var len = $scope.referedByList.length;
                    //for (var i = 0; i < len; i++) {
                        if (response.RefByID===0) {
                               $scope.drName =1;
                          //  break;
                        }
                        else {
                            $scope.drName = response.RefByID;
                        }
                   // }
               // }
                //if ($scope.patientTypeList) {
                 //   var len = $scope.patientTypeList.length;
                 //   for (var i = 0; i < len; i++) {
                         if (response.TypeID===0) {
                             $scope.patientType = 1;
                       //     break;
                         }
                         else {
                             $scope.patientType = response.TypeID;
                         }
                   // }
               // }
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

            if ($scope.patientType) {
                //var len = $scope.patientTypeList.length;
                if (typeof ($scope.patientType) === "object") {
                    patientData.TypeID = $scope.patientType.ID;
                }
                else {
                    patientData.TypeID = $scope.patientType
                }
            }
            if (typeof ($scope.depName) === "object") {
                patientData.DepartmentID = $scope.depName.ID;
            }
            else {
                patientData.DepartmentID = $scope.depName
            }
            if (typeof ($scope.drName) === "object") {
                patientData.RefByID = $scope.drName.ID;
            }
            else {
                patientData.RefByID = $scope.drName
            }
            
            return patientData;
        }

        function refreshForm() {
            $scope.patientId = '';
            $scope.patientName = '';
            $scope.age ='';
            $scope.attendantName = '';
            $scope.address ='';
            $scope.contactNo = '';
            $scope.email = '';
            $scope.sex = '';
            $scope.consultantName ='';
            $scope.consultantFee ='';
            $scope.remarks = '';
            $scope.drName = $scope.referedByList[0].ID;
            $scope.depName = $scope.departmentList[0].ID;
            $scope.patientType = $scope.patientTypeList[0].ID;
          //  $scope.patientDeatilsForm.$setPristine();
        };

        function isArray(x) {
            return x.constructor.toString().indexOf("Array") > -1;
        }
    }
    PatientRegiCtrl.$inject = ['$scope', '$http', 'PatientService'];
    return PatientRegiCtrl;
})