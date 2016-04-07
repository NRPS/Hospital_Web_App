'use strict';

define([], function () {
    function datagrid() {
        var directive = {};
        directive.restrict = 'E'; /* restrict this directive to elements */
        directive.scope = {
            headerName: '=headers',
            tblData: '=data'
        };
        directive.templateUrl = "Custom_Grid/grid.html";
        directive.controller = 'DataGridCtrl';
        return directive;
    };
    return datagrid;
});