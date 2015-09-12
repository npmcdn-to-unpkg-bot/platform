var App;
(function (App) {
    var app = angular.module("app", ["ui.router"]);
    app.config(stateConfig);
    stateConfig.$inject = ["$stateProvider", "$urlRouterProvider"];
    function stateConfig($stateProvider, $urlRouterProvider) {
        $urlRouterProvider.otherwise('/home');
        $stateProvider
            .state('home', {
            url: '/home',
            templateUrl: '/app/home/home.html',
            controller: 'homeController',
            controllerAs: 'vm'
        })
            .state('organisation', {
            url: '/organisation',
            templateUrl: '/app/organisations/organisation.html',
            controller: 'organisationController',
            controllerAs: 'vm'
        })
            .state('person', {
            url: '/person',
            templateUrl: '/app/persons/person.html',
            controller: 'personController',
            controllerAs: 'vm'
        });
    }
})(App || (App = {}));
//# sourceMappingURL=app.js.map