'use strict';

define([], function () {
    function DataGridCtrl($scope, $rootScope) {
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
            console.log($rootScope.billItems);
            $rootScope.billItems.splice(index,1);
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

        $scope.editContact = function (editedData) {
            $scope.model.selected = angular.copy(editedData);
        };

        $scope.saveContact = function (idx) {
            console.log("Saving contact");
            $scope.model.billItem[idx] = angular.copy($scope.model.selected);
            $rootScope.billItems = $scope.model.billItem;
            $scope.reset();
        };

        $scope.reset = function () {
            $scope.model.selected = {};
        };
    };
    DataGridCtrl.$inject = ['$scope','$rootScope'];
    return DataGridCtrl;
});