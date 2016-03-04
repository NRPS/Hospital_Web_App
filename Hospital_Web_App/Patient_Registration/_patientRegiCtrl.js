'use strict'

define([], function () {
    function PatientRegiCtrl($scope, $http, PatientService) {
       // var patientId = 'P2016020001';
        $scope.search = function (event) {
            if (event.which == 13) {
                var response = PatientService.getPatientDataById($scope.patientId);
                response.success(function (data, status, headers, config) {
                    setData(data);
                })
                response.error(function (data, status, headers, config) {
                    alert(status.Message);
                });
            }
        }
        function callAjax() {
            $http.get(uri + '/' + $scope.patientId)
              .success(function (data) {
                  setData(data);
              })
            .error(function (jqXHR, textStatus, err) {
                $('#product').text('Error: ' + err);
            });
        };
        function setData(response) {
            if (response) {
                $scope.patientId = response.PatientID;
                $scope.patientName = response.Name;
                $scope.age = response.PatientID;
                $scope.attendantName = response.AttendentName;
                $scope.address = response.Address;
                $scope.contactNo = response.ContactNumber1;
                $scope.email = response.Email;
                $scope.consultantName = response.ConsultantName;
                $scope.Department = response.DepartmentID;
                $scope.consultantFee = response.ConsultantFee;
                $scope.remarks = response.Remarks;
                var dateOfAdmit = new Date(response.RegDate);
                dateOfAdmit = dateOfAdmit.getFullYear() + '-' + (dateOfAdmit.getMonth() + 1) + '-' + dateOfAdmit.getDate();
                $scope.dateOfAdmit = new Date(dateOfAdmit);
                switch(response.Sex){
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