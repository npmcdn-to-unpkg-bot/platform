module App
{
    var app = angular.module("app", ["allors", "ngSanitize", "ngAnimate", "ui.router", "ui.bootstrap", "ui.tinymce", "angular-loading-bar", "toastr", "ngFileUpload", "toggle-switch"]);

    app.factory('$exceptionHandler', () => (exception, cause) => {
        var message = {
            exception: exception,
            cause: cause
        };

        var jsonMessage = angular.toJson(message);
        var uriEncodedMessage = encodeURIComponent(jsonMessage);

        alert(exception)
        //window.location.href = "/Error?error=" +uriEncodedMessage;
    });

    app.config(stateConfig);

    stateConfig.$inject = ["$stateProvider", "$urlRouterProvider", "cfpLoadingBarProvider"];
    function stateConfig($stateProvider: ng.ui.IStateProvider, $urlRouterProvider: ng.ui.IUrlRouterProvider, loadingBar: any): void {

        // Comment this line if yo want a spinner
        loadingBar.includeSpinner = true;

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
                url: '/relation/organisation',
                templateUrl: '/app/relation/organisation/edit.html',
                controller: 'organisationEditController',
                controllerAs: 'vm'
            })
            .state('organisationAdd', {
                url: '/relation/add/organisation',
                templateUrl: '/app/relation/organisation/add.html',
                controller: 'organisationAddController',
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
