var App;
(function (App) {
    var Organisation;
    (function (Organisation) {
        var OrganisationController = (function () {
            function OrganisationController($state, allorsService) {
                this.$state = $state;
                this.allorsService = allorsService;
                this.title = "Organisation";
            }
            OrganisationController.$inject = ["$state", "allorsService"];
            return OrganisationController;
        })();
        angular
            .module("app")
            .controller("organisationController", OrganisationController);
    })(Organisation = App.Organisation || (App.Organisation = {}));
})(App || (App = {}));
