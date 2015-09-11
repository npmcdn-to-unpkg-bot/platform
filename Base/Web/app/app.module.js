(function () {
    'use strict';

    var app = angular.module('app', ['ui.router']);

    app.config(function ($stateProvider, $urlRouterProvider) {

        $urlRouterProvider.otherwise('/home');

        $stateProvider
            .state('home',
            {
                url: '/home',
                templateUrl: 'app/templates/home.html',
                controller: 'homeController',
                controllerAs: 'vm'
            })
            .state('person',
            {
                url: '/person',
                templateUrl: 'app/templates/person.html',
                controller: 'personController',
                controllerAs: 'vm'
            })
        ;
    });

})();