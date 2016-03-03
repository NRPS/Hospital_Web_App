'use strict'

define([], function () {
    function PatientRegiCtrl($scope, $http) {
       // var patientId = 'P2016020001';
        var uri = "http://localhost:2054/api/patientRegstration";
        $scope.search = function (event) {
            if (event.which == 13) {
                callAjax();
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
        function setData(data) {
            var response = data;
            if (response) {
                $scope.patientId = response[0].PatientID;
                $scope.patientName = response[0].Name;
                $scope.age = response[0].PatientID;
                $scope.attendantName = response[0].AttendentName;
                $scope.address = response[0].Address;
                $scope.contactNo = response[0].ContactNumber1;
                $scope.email = response[0].Email;
                $scope.consultantName = response[0].ConsultantName;
                $scope.Department = response[0].DepartmentID;
                $scope.consultantFee = response[0].ConsultantFee;
                $scope.remarks = response[0].Remarks;
                var dateOfAdmit = new Date(response[0].RegDate);
                dateOfAdmit = dateOfAdmit.getFullYear() + '-' + (dateOfAdmit.getMonth() + 1) + '-' + dateOfAdmit.getDate();
                $scope.dateOfAdmit = new Date(dateOfAdmit);
                switch(response[0].Sex){
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
    PatientRegiCtrl.$inject = ['$scope', '$http'];
    return PatientRegiCtrl;
})