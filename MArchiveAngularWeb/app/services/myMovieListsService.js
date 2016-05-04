'use strict';
app.factory('myMovieListsService', ['$http', function ($http) {

    var serviceBase = 'http://localhost:65274/';
    var movielistsServiceFactory = {};

    var _getMyLists = function () {

        return $http.get(serviceBase + 'api/movielists').then(function (results) {
            return results;
        });
    };

    movielistsServiceFactory.getMyLists = _getMyLists;

    return movielistsServiceFactory;

}]);