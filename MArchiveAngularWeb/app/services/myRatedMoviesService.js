'use strict';
app.factory('myRatedMoviesService', ['$http', function ($http) {

    var serviceBase = 'http://localhost:65274/';
    var ordersServiceFactory = {};

    var _getOrders = function () {

        return $http.get(serviceBase + 'api/orders').then(function (results) {
            return results;
        });
    };

    ordersServiceFactory.getOrders = _getOrders;

    return ordersServiceFactory;

}]);