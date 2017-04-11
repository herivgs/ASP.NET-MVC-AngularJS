"use strict";

(function () {
    var app = angular.module('DataMain');

    app.service('DataService', function ($http) {

        this.getAll = function () {
            return $http({
                method: "GET",
                url: "/api/Data/All",
                dataType: "json"
            }).success(function (responseData) {
                JSON.stringify(responseData);
            }).error(function (error) {
                console.log(error);
            });
        };

        this.create = function (data) {
            return $http({
                method: "POST",
                url: "/api/Data/Create",
                data: JSON.stringify(data),
                dataType: "json"
            }).success(function (responseData) {
                JSON.stringify(responseData)
            }).error(function (error) {
                console.log(error);
                alert(error.Message);
            });
        };

        this.edit = function (data) {
            return $http({
                method: "POST",
                url: "/api/Data/Edit",
                data: data,
                dataType: "json"
            }).success(function (responseData) {
                JSON.stringify(responseData)
            }).error(function (error) {
                console.log(error);
                alert(error.Message);
            });
        };

        this.getById = function (id) {
            return $http({
                method: "GET",
                url: "/api/Data/" + id,
                dataType: "json"
            }).success(function (responseData) {
                JSON.stringify(responseData)
            }).error(function (error) {
                console.log(error);
            });
        };

        this.delete = function (id) {
            return $http({
                method: "POST",
                url: "/api/Data/Delete/" + id,
                dataType: "json"
            }).success(function (responseData) {
                JSON.stringify(responseData);
            }).error(function (error) {
                console.log(error);
            });
        };

    });
}());