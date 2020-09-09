angular.module("ImportLendetApp", ['ui.bootstrap']).config(function ($routeProvider) {
    $routeProvider.when("/", {
        templateUrl: "Orari.html",
        controller: "OrariController"
    });
});