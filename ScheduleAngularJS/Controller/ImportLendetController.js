
var app = angular.module("ImportLendetApp", ["ui.bootstrap"]);
app.controller("ImportLendetsController", function ($scope, $http, $uibModal) {
   
    $scope.loadImportLendet = function () {
        $http.get('http://localhost:58009/api/ImportLendets').
            then(function (res) {
                $scope.Lendet = res.data;

            }, function (error) {
                alert("error");
            });
    };
    
    $scope.loadImportLendet();
    
    $http.get('http://localhost:58009/api/Degets').
        then(function (res) {
            console.log(res.data);
            $scope.deget = res.data;

        }, function (error) {
            alert("error");
        });
   
    $http.get('http://localhost:58009/api/Lendets').
        then(function (res) {
            console.log(res.data);
            $scope.lendet = res.data;

        }, function (error) {
            alert("error");
        });

    $http.get('http://localhost:58009/api/Pedagogs').
        then(function (res) {
            console.log(res.data);
            $scope.pedagog = res.data;

        }, function (error) {
            alert("erro");
        });
    
    $scope.openModal = function (lenda) {
        console.log(lenda);
        $scope.$modalInstance = $uibModal.open({
            templateUrl: 'EditModal.html',
            controller: 'EditModalController',
            size: 'lg',
            
            resolve: {
                deget: function () {
                    return $scope.deget;
                },
                lendet: function () {
                    return $scope.lendet;
                },
                pedagog: function () {
                    return $scope.pedagog;
                },
                getImportLendet: function () {
                    return {
                        Id: lenda.Id,
                        Dega: lenda.Dega,
                        Lenda: lenda.Lenda,
                        Viti: lenda.Viti.toString(),
                        Paraleli: lenda.Paraleli,
                        Tipi: lenda.Tipi,
                        Pedagog: lenda.Pedagog,
                        Kapur: lenda.Kapur.toString(),
                        Zgjedhje: lenda.Zgjedhje.toString()
                    };
                },
                lenda: function () {
                    return lenda;
                },
                loadImportLendet: function () {
                    return $scope.loadImportLendet;
                }
            }
        });
    }
    
    $scope.AddLende = function () {
        $scope.$modal = $uibModal.open({
            templateUrl: 'AddModal.html',
            controller: 'AddModalController',
            size: 'lg',
            
            resolve: {
                deget: function () {
                    return $scope.deget;
                },
                lendet: function () {
                    return $scope.lendet;
                },
                pedagog: function () {
                    return $scope.pedagog;
                },
                loadImportLendet: function () {
                    return $scope.loadImportLendet;
                }
            }
        });
    }; 

    $scope.Delete = function (lenda) {
        $scope.$modal = $uibModal.open({
            templateUrl: 'DeleteModal.html',
            controller: 'DeleteModalController',
            size: 'lg',
            resolve: {
                lenda: function () {
                    return lenda;
                },
                loadImportLendet: function () {
                    return $scope.loadImportLendet;
                }
            }
        });

           
    };
   
});
//var app = angular.module("ImportLendetApp", ["sampleModalModule"]);
//app.controller("ImportLendetsController", ["$scope", function ($scope) {
//    $scope.openModal = function () {
//        console.log("clicked here...");

  
//   };
//}]);