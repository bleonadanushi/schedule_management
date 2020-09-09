
var app = angular.module("OrariApp", []);
app.controller("OrariController", function ($scope, $http) {
    $scope.selectedPetagog = "0";
    $scope.afisho = function () {
        
        $http.get(`http://localhost:58009/api/getOrarPedagog?pedagog=${$scope.selectedPetagog}`).
            then(function (res) {
                $scope.orari = {};
             
                angular.forEach(res.data, function (item) {
                    $scope.orari[`${item.Dita}|${item.Ora}`] = item;
                });
                
            }, function (error) {
                alert("error");
            });
    }
    $scope.selectedDega = "0";
    $scope.selectedViti = "0";
    $scope.selectedParaleli = "0";
    $scope.afishoStudent = function () {
        $http.get(`http://localhost:58009/api/getOrarStudent?dega=${$scope.selectedDega}&&viti=${$scope.selectedViti}&&paraleli=${$scope.selectedParaleli}`).
            then(function (res) {
                $scope.orariStudent = {};
                angular.forEach(res.data, function (item) {
                    $scope.orariStudent[`${item.Dita}|${item.Ora}`] = item;
                });

            }, function (error) {
                alert("erro");
            });
    }

    $http.get('http://localhost:58009/api/Pedagogs').
        then(function (res) {
            console.log(res.data);
            $scope.pedagog = res.data;

        }, function (error) {
            alert("erro");
        });
    $http.get('http://localhost:58009/api/Degets').
        then(function (res) {
            $scope.deget = res.data;

        }, function (error) {
            alert("erro");
        });
});