'use strict'

define([], function () {
    function PatientRegiCtrl($scope, $http, PatientService) {
        // var patientId = 'P2016020001';
        $scope.female = false;
        $scope.male = false;
        $scope.search = function (event) {
            if (event.which == 13) {
                if (!$scope.form1.$valid) {
                    $scope.submitted = true;
                }
                else if ($scope.form1.$valid) {
                    var response = PatientService.getPatientDataById($scope.patientId);
                    response.success(function (data, status, headers, config) {
                        setData(data);
                    })
                    response.error(function (data, status, headers, config) {
                        alert(status.Message);
                    });
                }
            }
        }
        
        $scope.addNewPatient = function () {
            $scope.alert = { msg: 'Hello alert!' };
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
            if ($scope.female == true) {
                patientData.Sex='F'
            }
            if ($scope.male == true) {
                patientData.Sex = 'M'
            }
            var response = PatientService.addNewPatient(JSON.stringify(patientData));
            response.success(function (data, status, headers, config) {

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
                $scope.age = response.PatientID;
                $scope.attendantName = response.AttendentName;
                $scope.address = response.Address;
                $scope.contactNo ='+91'+ response.ContactNumber1;
                $scope.email = response.Email;
                $scope.consultantName = response.ConsultantName;
                $scope.Department = response.DepartmentID;
                $scope.consultantFee = response.ConsultantFee;
                $scope.remarks = response.Remarks;
                var dateOfAdmit = new Date(response.RegDate);
                dateOfAdmit = dateOfAdmit.getFullYear() + '-' + (dateOfAdmit.getMonth() + 1) + '-' + dateOfAdmit.getDate();
                $scope.dt = new Date(dateOfAdmit);
                switch (response.Sex) {
                    case 'F': $scope.female = true;
                        break
                    case 'M': $scope.male = true;
                        break;
                }

            }
            else {
                alert('Some thing was wrong');
            }
        }
    }
    PatientRegiCtrl.$inject = ['$scope', '$http', 'PatientService'];
    return PatientRegiCtrl;
})