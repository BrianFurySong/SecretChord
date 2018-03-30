var appName = "SecretChord"; //change this name to whatever u need it to be
(function (AppName) {
    'use strict';
    var app = angular.module(AppName, ["ui.bootstrap", "LocalStorageModule", "ngAnimate"]);
    app.factory("authInterceptorService", AuthInterceptorService);
    AuthInterceptorService.$inject = ["$q", "$location", "localStorageService", "$window"];
    function AuthInterceptorService($q, $location, localStorageService, $window) {
        var ais = {
            request: _request,
            responseError: _responseError
        }

        return ais;

        function _request(config) {
            config.headers = config.headers || {};
            var authData = localStorageService.get("authorizationData");
            if (authData) {
                config.headers.Token = authData.token;
            }
            return config;
        }

        function _responseError(rejection) {
            if (rejection.status === 401) {
                $window.location.href = "/";
            }
            return $q.reject(rejection);
        }
    }

    app.config(function ($httpProvider) {
        $httpProvider.interceptors.push("authInterceptorService");
    })


})(appName);
