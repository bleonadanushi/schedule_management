var app = angular.module("ImportLendetApp");

app.controller('AddModalController', ['$scope', '$uibModalInstance', '$http', "deget", "lendet", "pedagog", "loadImportLendet", function ($scope, $uibModalInstance, $http, deget, lendet, pedagog, loadImportLendet){
    $scope.deget = deget;
    $scope.lendet = lendet;
    $scope.pedagog = pedagog;
   
    $scope.getImportLendet = {
        Dega: "0",
        Lenda: "0",
        Viti: "0",
        Paraleli: "0",
        Tipi: "0",
        Pedagog: "0",
        Kapur: "0",
        Zgjedhje:"0",
    };
 
    $scope.save = function () {
        console.log($scope.getImportLendet);
        $http.post('http://localhost:58009/api/SaveImportLendets', $scope.getImportLendet).then(function (res) {
            alert("Shtimi u krye me sukses");
            loadImportLendet();
        }, function (error) {
            alert("erro");
        });
        $uibModalInstance.close();
    };
    
    $scope.close = function () {
        $uibModalInstance.close();
    };


}]);