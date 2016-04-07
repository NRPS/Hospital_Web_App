'use strict'

define([], function () {
    function BillCtrl($scope, $rootScope, $http, BillService, PatientService) {

        $scope.female = false;
        $scope.male = false;
        $scope.format = 'dd - MM - yyyy';
        $rootScope.billItems = [];
        $scope.listOfHeaders = [{ field: 'Lab', col: 'lab' }, { field: 'Description', col: 'desc' }, { field: 'Quantity', col: 'quantity' }, { field: 'Rate', col: 'rate' }, { field: 'Discount', col: 'discount' }, { field: 'Amount', col: 'amount' }, { field: 'Net Amount', col: 'netAmount' }
            , { field: '', col: 'edit' }, { field: '', col: 'delete' }];

        getBillItem();
        getLabList();
       
        $scope.addBillItem = function () {
            
            if (!$scope.billForm.$valid) {
                $scope.billItemFrmSubmitted = true;
            }
            else if ($scope.billForm.$valid) {
                if ($scope.discount!==undefined){
                    $rootScope.billItems.push({
                        id: $rootScope.billItems.length, itemCode: $scope.chargeName, labCode: $scope.labName,
                        lab: getTestLabName(), desc: getBillDescription(), quantity: $scope.quantity, rate: $scope.rate,
                        discount: $scope.discount, amount: $scope.rate * $scope.quantity, netAmount: $scope.rate * $scope.quantity - $scope.discount
                    })
                }
                else {
                    $scope.discount = 0;
                    $rootScope.billItems.push({
                        id: $rootScope.billItems.length, itemCode: $scope.chargeName, labCode: $scope.labName,
                        lab: getTestLabName(), desc: getBillDescription(), quantity: $scope.quantity, rate: $scope.rate,
                        discount: $scope.discount, amount: $scope.rate * $scope.quantity, netAmount: $scope.rate * $scope.quantity - $scope.discount
                    })
                }
            if ($scope.totalAmount === undefined) {
                $scope.totalAmount = 0;
                $scope.totalAmount += $scope.rate * $scope.quantity - $scope.discount;
            }
            else {
                $scope.totalAmount += $scope.rate * $scope.quantity - $scope.discount;
            }
        }
        };

        $scope.search = function (event) {
            if (event.which == 13) {
                if (!$scope.searchForm.$valid) {
                    $scope.submitted = true;
                }
                else if ($scope.searchForm.$valid) {
                    $scope.isLoading = true;

                    var Pid = $scope.searchItem;
                    if (Pid.charAt(4) === 'B') {
                        var responseBillDetails = BillService.getBillDetails(Pid);

                        responseBillDetails.success(function (data, status, headers, config) {
                            getBillData(data);

                            var response = PatientService.getPatientDataById(data.PatientID);
                            $scope.isLoading = false;

                            response.success(function (data, status, headers, config) {
                                setData(data);
                                $scope.isLoading = false;
                            })
                            response.error(function (data, status, headers, config) {
                                alert(status.Message);
                            });
                        })
                        responseBillDetails.error(function (data, status, headers, config) {
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
                    }
                }
            }
        }

        function getChargeList() {
            var ajaxResponse = BillService.getChargeList();
            ajaxResponse.success(function (data, status, headers, config) {
                $scope.chargeName = data[0].Code;
                $scope.chargeList = data;
            })
            ajaxResponse.error(function (data, status, headers, config) {
                alert(status.Message);
            });
        };

        function getLabList() {
            var ajaxResponse = BillService.getLabList();
            ajaxResponse.success(function (data, status, headers, config) {
                $scope.labName = data[0].Code;
                $scope.labList = data;
            })
            ajaxResponse.error(function (data, status, headers, config) {
                alert(status.Message);
            });
        };


        $scope.GetBillTotal = function () {
            $scope.totalAmount = ($scope.rate * $scope.quantity) - $scope.discount;
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
            if (!$scope.billForm.$valid) {
                $scope.billFrmSubmitted = true;
            }
            else if ($scope.billForm.$valid) {
                var response = BillService.addNewBill(setBillData());
                response.success(function (data, status, headers, config) {
                     alert('Bill Added')
                });
                response.error(function (data, status, headers, config) {
               });
           }
        };

        $scope.deleteBill = function () {
            var ajaxResponse = BillService.deleteBill($scope.billNo);
            ajaxResponse.success(function (data, status, headers, config) {
                alert('Patient Data has deleted');
            })
            ajaxResponse.error(function (data, status, headers, config) {
                alert(status.Message);
            });
        }

        $scope.updateBill = function () {
            var response = BillService.updateBill(setBillData());
            response.success(function (data, status, headers, config) {
                alert('Patient Data Updated');
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

        $scope.getRate = function () {

            var len = $scope.chargeList.length;
            for (var i = 0; i < len;i++){
              if ($scope.chargeList[i].Code === $scope.chargeName) {
                  $scope.rate = $scope.chargeList[i].Rate;
                  break;
               }
            }

        };

        function getBillDescription(){
            var len = $scope.chargeList.length;
            var description='';
            for (var i = 0; i < len;i++){
                if ($scope.chargeList[i].Code === $scope.chargeName) {
                    description  = $scope.chargeList[i].Description;
                    break;
                }
            }
            return description;
        };

        function getTestLabName() {
            var len = $scope.labList.length;
            var labName = '';
            for (var i = 0; i < len; i++) {
                if ($scope.labList[i].Code === $scope.labName) {
                    labName = $scope.labList[i].Name;
                    break;
                }
            }
            return labName;
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
                $scope.contactNo = response.ContactNumber1;
                $scope.remarks = response.Remarks;
                $scope.sex = response.Sex;
                $scope.quantity = 1;
                var billDate = new Date(response.BillDate);
                billDate = billDate.getFullYear() + '-' + (billDate.getMonth() + 1) + '-' + billDate.getDay();
                $scope.billDate = new Date(billDate);
               /* if (response.ItemCode===0) {
                    $scope.chargeName =1;
                }
               else{
                $scope.chargeName = response.ItemCode;
                }
                if (response.LabCode === 0) {
                    $scope.labName = 1;
                }
                else {
                    $scope.labName = response.LabCode;
                }*/
               
                if (response.TypeID === 1)
                    $scope.patientType = "OPD";
                else
                    $scope.patientType = "IPD";

            }
            else {
                alert('Some thing was wrong');
            }
        }

        function setBillData() {
            var billData = {};
            billData.PatientID = $scope.patientId;
            billData.BillNo = $scope.billNo;
            billData.AmountPaid = $scope.payNow;
            billData.BillTotal = $scope.totalAmount;
            billData.BillDate = $scope.dt;
            billData.Remarks = $scope.remarks;
            billData.PaymentMode = "C";

            var billDetails = {};

            billDetails.ItemCode = $scope.chargeName
            billDetails.LabCode = $scope.labName;
            billDetails.Rate = $scope.rate;
            billDetails.Quantity = $scope.quantity;
            billDetails.Amount = $scope.rate * $scope.quantity;
            billDetails.Discount = $scope.discount;
            billDetails.NetAmount = billDetails.Amount - billDetails.Discount;
           // billDetails = JSON.stringify(billDetails);

            billData.BillDetails = $rootScope.billItems;
            return billData;
        }

        function getBillData(response) {
            if (response) {
                $scope.patientId = response.PatientID;
                // $scope.remarks = response.Remarks;
                $scope.billNo = response.BillNo;
                $scope.payNow = response.AmountPaid;
                $scope.totalAmount = response.BillTotal;
                $scope.billDate = response.BillDate;
                $scope.rate = response.BillDetails.Rate;
                $scope.quantity = response.BillDetails.Quantity;
                $scope.discount = response.BillDetails.Discount;
                if ($scope.chargeList) {
                    var len = $scope.chargeList.length;
                    for (var i = 0; i < len; i++) {
                        if ($scope.chargeList[i].Code ===response.BillDetails.ItemCode) {
                            $scope.chargeName = $scope.chargeList[i].Code;
                            break;
                        }
                    }
                }
                $scope.labName = response.BillDetails.LabCode;
            }
            else {
                alert('Some thing was wrong');
            }
        };

        function getBillItem() {
            var response = BillService.getChargeList();
            response.success(function (data, status, headers, config) {
                $scope.chargeName = data[0].Code;
                $scope.chargeList = data;
                $scope.rate = data[0].Rate;
            })
            response.error(function (data, status, headers, config) {
                alert('Please start web api server');
            });
        };

        function isArray(x) {
            return x.constructor.toString().indexOf("Array") > -1;
        }

    }
    BillCtrl.$inject = ['$scope', '$rootScope', '$http', 'BillService', 'PatientService'];
    return BillCtrl;
})