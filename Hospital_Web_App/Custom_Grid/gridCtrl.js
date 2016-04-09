'use strict';

define([], function () {
    function DataGridCtrl($scope, $rootScope, $http, BillService, PatientService) {
        $scope.gridData = $scope.tblData;
        $scope.edit = false;
        var count = 0;
        $scope.doSorting = function (input) {
            if (count == 0) {
                $scope.input = input.toLowerCase().replace(/ +/g, "");
                count++
            }

            else if (count == 1) {
                $scope.input = '-' + (input.toLowerCase().replace(/ +/g, ""));
                count = -1;
            }

            else if (count == -1) {
                $scope.input = '';
                count = 0;
            }
        }
        $scope.deleteRow = function (index) {
            //console.log($rootScope.billItems);
            $scope.$emit('billItemsChanged', [index,'-']);
        };

        $scope.model = {
            billItem:$rootScope.billItems, 
            selected: {}
        };

        // gets the template to ng-include for a table row / item
        $scope.getTemplate = function (contact) {
            if (contact.id === $scope.model.selected.id) return 'edit';
            else return 'display';
        };

        $scope.edit = function (editedData,index) {
            $scope.model.selected = angular.copy(editedData);
            getChargeList(index);
            getLabList(index);
            $scope.colWidth = 'col-width';
        };

        $scope.save = function (idx) {
            $scope.colWidth = '';
            $scope.model.billItem[idx] = angular.copy($scope.model.selected);
            $rootScope.billItems = $scope.model.billItem;
            for (var i = 0; i < $scope.chargeList.length;i++){
                if ($scope.chargeList[i].Code === $scope.model.selected.desc) {
                    $rootScope.billItems[idx].desc = $scope.chargeList[i].Description;
                    break;
                }
            }
            for (var i = 0; i < $scope.labList.length; i++) {
                if ($scope.labList[i].Code === $scope.model.selected.lab) {
                    $rootScope.billItems[idx].lab = $scope.labList[i].Name;
                    break;
                }
            }
            $scope.$emit('billItemsChanged', [idx, '+']);
            $scope.reset();
        };

        $scope.reset = function () {
            $scope.model.selected = {};
        };

        $scope.calAmoumt=function(){
            $scope.model.selected.amount= $scope.model.selected.quantity* $scope.model.selected.rate;
            $scope.model.selected.netAmount = $scope.model.selected.amount - $scope.model.selected.discount;
        };

        function getChargeList(index) {
            var ajaxResponse = BillService.getChargeList();
            ajaxResponse.success(function (data, status, headers, config) {
                if ($rootScope.billItems) {
                    $scope.model.selected.desc = $rootScope.billItems[index].itemCode;
                }
                $scope.chargeList = data;
            })
            ajaxResponse.error(function (data, status, headers, config) {
                alert(status.Message);
            });
        };

        function getLabList(index) {
            var ajaxResponse = BillService.getLabList();
            ajaxResponse.success(function (data, status, headers, config) {
                if ($rootScope.billItems) {
                    $scope.model.selected.lab = $rootScope.billItems[index].labCode;
                }
                $scope.labList = data;
            })
            ajaxResponse.error(function (data, status, headers, config) {
                alert(status.Message);
            });
        };
    };
    DataGridCtrl.$inject = ['$scope', '$rootScope', '$http', 'BillService', 'PatientService'];
    return DataGridCtrl;
});