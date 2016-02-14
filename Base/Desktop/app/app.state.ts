namespace App
{
    var app = angular.module("app");
    
    app.config(config);

    config.$inject = ["$stateProvider", "$urlRouterProvider"];
    function config($stateProvider: ng.ui.IStateProvider, $urlRouterProvider: ng.ui.IUrlRouterProvider): void {

        /////////////////////////////
        // Redirects and Otherwise //
        /////////////////////////////

        // Use $urlRouterProvider to configure any redirects (when) and invalid urls (otherwise).
        $urlRouterProvider

            // The `when` method says if the url is ever the 1st param, then redirect to the 2nd param
            // Here we are just setting up some convenience urls.
            //.when("/user/:id', '/contacts/:id')

            // If the url is ever invalid, e.g. '/asdf', then redirect to '/' aka the home state
            .otherwise("/");
        
        //////////////////////////
        // State Configurations //
        //////////////////////////
        $stateProvider


            // General
            .state("home", {
                url: "/",
                templateUrl: "/app/general/home/home.html",
                controller: "homeController",
                controllerAs: "vm"
            })
            .state("settings", {
                url: "/settings",
                templateUrl: "/app/general/settings/settings.html",
                controller: "settingsController",
                controllerAs: "vm"
            })
            .state("test", {
                url: "/test",
                templateUrl: "/app/general/test/test.html",
                controller: "testController",
                controllerAs: "vm"
            })

            // Relation
            .state("organisation", {
                url: "/relation/organisation",
                templateUrl: "/app/relation/organisation/edit.html",
                controller: "organisationEditController",
                controllerAs: "vm"
            })
            .state("organisationAdd", {
                url: "/relation/add/organisation",
                templateUrl: "/app/relation/organisation/add.html",
                controller: "organisationAddController",
                controllerAs: "vm"
            });
    }
}
