"use strict";

(function () {
    var app = angular.module("DataMain");

    app.controller("DataController", function ($scope, DataService) {
        $scope.data = {};
        $scope.isDisable = false;

        $scope.getAll = function () {
            $scope.data = DataService.getAll()
                .then(function (responseData) {
                    $scope.data = responseData.data;
                }, function (ex, status) {
                    console.log(ex);
                    alert('An error occurred');
                });
        };

        $scope.create = function () {
            $scope.isDisable = true;

            DataService.create($scope.data)
                .then(function (responseData) {
                    $scope.isDisable = false;
                    window.location.replace("/Data/List");
                }), function (ex, status) {
                    $scope.isDisable = false;
                    console.log(ex);
                    alert(ex);
                };
        };

        $scope.edit = function (id) {
            $scope.isDisable = true;

            DataService.edit($scope.data)
                .then(function (responseData) {
                    $scope.isDisable = false;
                    window.location.replace("/Data/List");
                }), function (ex, status) {
                    $scope.isDisable = false;
                    console.log(ex);
                    alert(ex);
                };
        };

        $scope.editRoute = function (id) {
            window.location.replace('/Data/Edit/' + id);
        };

        $scope.getById = function (id) {
            DataService.getById(id)
                .then(function (responseData) {
                    $scope.data = responseData.data;
                }, function (ex, status) {
                    console.log(ex);
                    alert(ex);
                });
        };

        $scope.delete = function (id) {
            if (window.confirm("Are you sure you want to delete this item?"))
            {
                DataService.delete(id)
                    .then(function (responseData) {
                        window.location.reload(true);
                    }, function (ex, status) {
                        console.log(ex);
                        alert(ex);
                    });
            }
        };
    });
}());