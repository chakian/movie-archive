var app = angular.module('MovieApp', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar', 'gettext']);

app.config(function ($routeProvider) {

    $routeProvider.when("/home", {
        controller: "homeController",
        templateUrl: "/app/views/home.html"
    })
        .when("/login", {
            controller: "loginController",
            templateUrl: "/app/views/login.html"
        })
        .when("/signup", {
            controller: "signupController",
            templateUrl: "/app/views/signup.html"
        })
        .when("/my-lists", {
            controller: "myListsController",
            templateUrl: "/app/views/myLists.html"
        })
        .otherwise({ redirectTo: "/home" });
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);
app.run(function (gettextCatalog) {
    gettextCatalog.currentLanguage = 'tr';
    gettextCatalog.debug = true;
});

var apiUrl = 'https://localhost:40430/';
