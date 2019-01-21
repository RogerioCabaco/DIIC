function getScope(ctrlName) {
    var sel = 'section[ng-controller="' + ctrlName + '"]';
    return angular.element(sel).scope();
}
/* Angular Inicialization */
var appModule = angular.module('IoTakers', []);

appModule.controller('Account', function ($scope) {
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

    $scope.stockRequest = function () {
    }


});  