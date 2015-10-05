var App;
(function (App) {
    var Organisation;
    (function (Organisation) {
        var OrganisationController = (function () {
            function OrganisationController($http, $state, allorsService) {
                this.$http = $http;
                this.$state = $state;
                this.allorsService = allorsService;
                this.title = "Organisation";
            }
            OrganisationController.$inject = ["$http", "$state", "allorsService"];
            return OrganisationController;
        })();
        angular
            .module("app")
            .controller("organisationController", OrganisationController);
    })(Organisation = App.Organisation || (App.Organisation = {}));
})(App || (App = {}));
//# sourceMappingURL=organisation.js.map