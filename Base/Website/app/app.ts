module App
{
    var app = angular.module("app", ["ui.router"]);

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
            .state('home',
                {
                    url: '/home',
                    templateUrl: '/app/home/home.html',
                    controller: 'homeController',
                    controllerAs: 'vm',
                })
            .state('organisation',
                {
                    url: '/organisation',
                    templateUrl: '/app/organisations/organisation.html',
                    controller: 'organisationController',
                    controllerAs: 'vm'
                })
            .state('person',
                {
                    url: '/person',
                    templateUrl: '/app/persons/person.html',
                    controller: 'personController',
                    controllerAs: 'vm'
                })
        ;
    }
}
