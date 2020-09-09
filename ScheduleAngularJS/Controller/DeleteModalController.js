
var app = angular.module("ImportLendetApp");
app.controller('DeleteModalController', ['$scope', '$uibModalInstance', '$http', 'lenda', 'loadImportLendet', function ($scope, $uibModalInstance, $http, lenda, loadImportLendet) {

    $scope.close = function () {
        $uibModalInstance.close();
    };
    
    $scope.save = function () {
        $http.delete("http://localhost:58009/api/ImportLendets/" + lenda.Id).then(function (res) {
            alert("Fshirja u krye me sukses");
            loadImportLendet();
        }, function (error) {
            alert("erro");
        });
        $uibModalInstance.close();
    };

}]);