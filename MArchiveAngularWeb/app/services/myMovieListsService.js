'use strict';
app.factory('myMovieListsService', ['$http', function ($http) {

    var serviceBase = 'http://localhost:65274/';
    var movielistsServiceFactory = {};

    var _getMovieLists = function () {

        return $http.get(serviceBase + 'api/movielists').then(function (results) {
            return results;
        });
    };

    movielistsServiceFactory.getMovieLists = _getMovieLists;

    return movielistsServiceFactory;

}]);