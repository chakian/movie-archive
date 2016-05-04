'use strict';
app.controller('myListsController', ['$scope', 'myMovieListsService', function ($scope, myMovieListsService) {
    $scope.lists = [];

    myMovieListsService.getMyLists().then(function (results) {
        $scope.lists = results.data;
    }, function (error) {
        //alert(error.data.message);
    });
}]);
