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

            .state("home", {
                url: "/",
                templateUrl: "/app/pages/general/home/home.html",
                controller: "homeController",
                controllerAs: "vm"
            })
            // Relation
            .state("relation", {
                url: "/relation",
                abstract: true,
                templateUrl: "/app/pages/relation/relation.html",
                controller: "relationController",
                controllerAs: "vm"
            })
            .state("relation.people", {
                url: "/people",
                templateUrl: "/app/pages/relation/person/people.html",
                controller: "relationPeopleController",
                controllerAs: "vm"
            })
            .state("relation.addPerson", {
                url: "/addPerson",
                templateUrl: "/app/pages/relation/person/addPerson.html",
                controller: "relationAddPersonController",
                controllerAs: "vm"
            })
            .state("relation.editPerson", {
                url: "/editPerson/:id",
                templateUrl: "/app/pages/relation/person/editPerson.html",
                controller: "relationEditPersonController",
                controllerAs: "vm"
            });
    }
}
