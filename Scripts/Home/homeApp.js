angular.module('homeApp', [])
    .controller('homeCtrl', ["$scope", function ($scope) {
        console.log("APP LOADED");
        $scope.test = "TEST SCOPE";
        this.test = "TEST CTRL"
    }]).config(function () { });