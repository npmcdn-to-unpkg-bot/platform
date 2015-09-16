var App;
(function (App) {
    var app = angular.module("app", ["ui.router"]);
    app.factory('$exceptionHandler', function () { return function (exception, cause) {
        var message = "Serious error occured, application will restart.\n\n" + cause + "\n";
        alert(message);
        window.location.href = '/';
    }; });
    app.config(stateConfig);
    stateConfig.$inject = ["$stateProvider", "$urlRouterProvider"];
    function stateConfig($stateProvider, $urlRouterProvider) {
        $urlRouterProvider.otherwise('/home');
        $stateProvider
            .state('home', {
            url: '/home',
            templateUrl: '/app/home/home.html',
            controller: 'homeController',
            controllerAs: 'vm',
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