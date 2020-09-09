
var app = angular.module("ImportLendetApp");
app.controller('EditModalController', ['$scope', '$http', '$uibModalInstance', "deget", "lendet", "getImportLendet", "pedagog", "lenda", "loadImportLendet", function ($scope, $http, $uibModalInstance, deget, lendet, getImportLendet, pedagog, lenda, loadImportLendet) {

    $scope.deget = deget;
    $scope.lendet = lendet;
    $scope.pedagog = pedagog;
  
    $scope.getImportLendet = getImportLendet;
    $scope.close = function () {
        $uibModalInstance.close();
    };
    
    $scope.save = function () {
        $http.put("http://localhost:58009/api/ImportLendets/" + lenda.Id, $scope.getImportLendet).then(function (res) {
            alert("Editimi u krye me sukses");
            loadImportLendet();
        }, function (error) {
            alert("erro");
        });
        $uibModalInstance.close();
    };

}]);