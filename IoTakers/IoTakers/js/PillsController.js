function getScope(ctrlName) {
    var sel = 'section[ng-controller="' + ctrlName + '"]';
    return angular.element(sel).scope();
}
/* Angular Inicialization */
var appModule = angular.module('IoTakers', []);

appModule.controller('Pills', function ($scope) {
    $scope.historicRequests = [], $scope.pillsRegistered = [], $scope.renovationRequests = [];
    $scope.indexOfRequest;
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

    }

    $scope.AddPill = function () {
        console.log("in");
        if ($('#newPillName').val() != "") {
            console.log("in if");
            var lead = {
                name: $('#newPillName').val()
            }
            $.ajax({
                type: 'POST',
                url: '/Home/AddPill',
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
        else toastr.error("Please name your new pill.");
    }
    $scope.RemovePill = function (id) {
        console.log("a remover " + id);

        var lead = {
            index: id
        };
        console.log(lead);
        $.ajax({
            type: 'POST',
            url: '/Home/RemovePill',
            dataType: 'json',
            data: JSON.stringify(lead),
            contentType: 'application/json; charset=utf-8',
            success: function (model) { //return can be sucess but not response:0,1,2
                console.log("removeu");
                $scope.pillsRegistered.splice(id, 1);
                $scope.$apply();
            },
            error: function (jqXHR, textStatus, err) {
            }
        });
    }

});  