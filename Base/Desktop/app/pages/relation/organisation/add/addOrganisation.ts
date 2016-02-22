namespace App.Relation.AddOrganisation {
    class AddOrganisationController extends Page {
     
        organisation: Organisation;
     
        static $inject = ["allorsService", "$scope", "$state"];
        constructor(allors: AllorsService, $scope: ng.IScope, private $state: ng.ui.IStateService) {
            super("AddOrganisation", allors, $scope);

            this.refresh()
                .then(() => {
                    this.organisation = this.session.create("Organisation") as Organisation;
                });
        }

        cancel(): void {
            this.$state.go("relation.organisations");
        }
        
        protected refresh(): ng.IPromise<any> {
            return this.load();
        }
    }
    angular
        .module("app")
        .controller("relationAddOrganisationController",
            AddOrganisationController);

}
