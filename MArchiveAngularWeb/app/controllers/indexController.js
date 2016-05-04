'use strict';
app.controller('indexController', ['$scope', '$location', 'authService', 'gettextCatalog', function ($scope, $location, authService, gettextCatalog) {

    $scope.logOut = function () {
        authService.logOut();
        $location.path('/home');
    }

    $scope.authentication = authService.authentication;

}]);