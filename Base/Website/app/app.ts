module App
{
    var app = angular.module("app", ["allors", "ui.router"]);

    app.factory('$exceptionHandler', () => (exception, cause) => {
        var message : any = "Serious error occured, application will restart.\n\n" + cause + "\n";
        alert(message);
        window.location.href = '/';
    });

    app.config(stateConfig);

    stateConfig.$inject = ["$stateProvider", "$urlRouterProvider"];
    function stateConfig($stateProvider: ng.ui.IStateProvider, $urlRouterProvider: ng.ui.IUrlRouterProvider): void {
        
        function applyMixins(derivedConstructor: any, baseConstructors: any[]) {
            baseConstructors.forEach(baseConstructor => {
                Object.getOwnPropertyNames(baseConstructor.prototype).forEach(name => {
                    if (name !== 'constructor') {
                        var propertyDescriptor = Object.getOwnPropertyDescriptor(baseConstructor.prototype, name);
                        Object.defineProperty(derivedConstructor.prototype, name, propertyDescriptor);
                    }
                });
            });
        }

        $urlRouterProvider.otherwise('/home');

        $stateProvider
            // General
            .state('home', {
                url: '/home',
                templateUrl: '/app/general/home/home.html',
                controller: 'homeController',
                controllerAs: 'vm'
            })
            .state('settings', {
                url: '/settings',
                templateUrl: '/app/general/settings/settings.html',
                controller: 'settingsController',
                controllerAs: 'vm'
            })
            .state('test', {
                url: '/test',
                templateUrl: '/app/general/test/test.html',
                controller: 'testController',
                controllerAs: 'vm'
            })
            // Relation
            .state('organisation', {
                url: '/organisation',
                templateUrl: '/app/relation/organisation/organisation.html',
                controller: 'organisationController',
                controllerAs: 'vm'
            })
            .state('person',{
                url: '/person',
                templateUrl: '/app/relation/person/person.html',
                controller: 'personController',
                controllerAs: 'vm'
            })
            // Admin
            .state('export', {
                url: "/admin/export",
                templateUrl: "/app/admin/export/export.html",
                controller: 'exportController',
                controllerAs: 'vm'
            });
    }
}
