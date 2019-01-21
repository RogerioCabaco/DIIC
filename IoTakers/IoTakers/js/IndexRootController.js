function getScope(ctrlName) {
    var sel = 'section[ng-controller="' + ctrlName + '"]';
return angular.element(sel).scope();
    }
    /* Angular Inicialization */
    var appModule = angular.module('IoTakers', []);

appModule.controller('Index', function ($scope) {
    $scope.historicRequests = [], $scope.pillsRegistered = [], $scope.renovationRequests = [];
    $scope.indexOfRequest;
    $scope.makeRequest = true;
    $scope.confirmRequest = false;
    $scope.ViewGeneralActive = true;
    $scope.ViewDoctorsActive = false;
    $scope.ViewMedicationActive = false;

    $scope.setViewMedicationActive = function () {
        $scope.ViewMedicationActive = true;
        $scope.ViewDoctorsActive = false;
        $scope.ViewGeneralActive = false;
        $scope.$apply();
    }
    $scope.setViewDoctorsActive = function () {
        $scope.ViewMedicationActive = false;
        $scope.ViewDoctorsActive = true;
        $scope.ViewGeneralActive = false;
        $scope.$apply();
    }
    $scope.setViewGeneralActive = function () {
        $scope.ViewMedicationActive = false;
        $scope.ViewDoctorsActive = false;
        $scope.ViewGeneralActive = true;
        $scope.$apply();
    }

    $scope.init = function (model) {
        $.each(model.historialRequests, function (index, e) {
            $scope.historicRequests.push(e);
        });
        var count = 0;
        $.each(model.renovationRequests, function (index, e) {
            $scope.renovationRequests.push(e);
            count++;
        });
        $.each(model.pillsRegistered, function (index, e) {
            $scope.pillsRegistered.push(e);
        });

        $('.notificationStockRequests').text(count);
        if (model.arduinoRequest) {
            $(window).load(function () {
                $scope.stockRequest();
            });

            toastr.warning("Novo pedido de renovação de stock ! Por favor consulte a tabela correspondente.")
            setTimeout(function () {
                $.ajax({
                    type: 'POST',
                    url: '/Home/FlagFalse',
                    dataType: 'json',
                    data: JSON.stringify(),
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) { //return can be sucess but not response:0,1,2
                       
                    },
                    error: function (jqXHR, textStatus, err) {
                    }
                });
            },2000);
       {}
        }

    }

    $scope.actualizeViewModel = function () {
        console.log("ok");
        $.ajax({
            type: 'GET',
            url: '/Home/SendDataFrontEnd',
            dataType: 'json',
            data: JSON.stringify(),
            contentType: 'application/json; charset=utf-8',
            success: function (model) { //return can be sucess but not response:0,1,2
                console.log(model);
                $scope.historicRequests = [];
                $scope.renovationRequests = [];
                $scope.pillsRegistered = [];
                $.each(model.historialRequests, function (index, e) {
                    $scope.historicRequests.push(e);
                });
                var count = 0;
                $.each(model.renovationRequests, function (index, e) {
                    $scope.renovationRequests.push(e);
                    count++;
                });
                $.each(model.pillsRegistered, function (index, e) {
                    $scope.pillsRegistered.push(e);
                });
                $scope.$apply();
            },
            error: function (jqXHR, textStatus, err) {
            }
        });
    }

    $scope.sendIndexOfRequest = function (index) {
        $scope.indexOfRequest = index;
        $('#confirmNewRequest').modal();
    }
    $scope.sendAddHistoricRequestPOST = function (string, int, ix) {
        var lead = {
            name: string,
            quantidade: int,
            index: ix
        }
        console.log(lead);
        $.ajax({
            type: 'POST',
            url: '/Home/AddHistoricRequest',
            dataType: 'json',
            data: JSON.stringify(lead),
            contentType: 'application/json; charset=utf-8',
            success: function (model) { //return can be sucess but not response:0,1,2
                console.log("adicionou");
                $scope.pillsRegistered.push({
                    Name: $('#newPillName').val()
                });
                $scope.$apply();
                $('#newPillName').val('');
            },
            error: function (jqXHR, textStatus, err) {
            }
        });
    }

    $scope.ActualizeViewModel = function () {

    }

    $scope.DoARequest = function () {
        if ($('#numerOfBoxesInput').val() != "") {
            $scope.historicRequests.push({
                'medicamento': $scope.renovationRequests[$scope.indexOfRequest].pill.Name,
                'quantidade': parseInt($('#numerOfBoxesInput').val())
            });
            $scope.sendAddHistoricRequestPOST($scope.renovationRequests[$scope.indexOfRequest].pill.Name, $('#numerOfBoxesInput').val(), $scope.indexOfRequest);
            $scope.renovationRequests.splice($scope.indexOfRequest, 1);
            $scope.makeRequest = !$scope.makeRequest;
            $scope.confirmRequest = !$scope.confirmRequest;
        }
        else return;
    }
});  