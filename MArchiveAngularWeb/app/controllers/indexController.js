'use strict';
app.controller('indexController', ['$scope', '$location', 'authService', 'gettextCatalog', function ($scope, $location, authService, gettextCatalog) {

    $scope.logOut = function () {
        authService.logOut();
        $location.path('/home');
    }

    $scope.authentication = authService.authentication;

    // Language switcher
    $scope.languages = {
        current: gettextCatalog.currentLanguage,
        available: {
            'tr': 'Türkçe',
            'en': 'English'
        }
    };
    $scope.$watch('languages.current', function (lang) {
        if (!lang) {
            return;
        }
        gettextCatalog.setCurrentLanguage(lang);
    });

}]);