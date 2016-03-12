namespace App.Relation.Organisation {
    class EditOrganisationController extends Page {

        organisation: Organisation;
        people: Person[];

        static $inject = ["allorsService", "$scope", "$state", "$stateParams"];
        constructor(allors: AllorsService, $scope: ng.IScope, private $state: ng.ui.IStateService, private params: { id: string }) {
            super("EditOrganisation", allors, $scope);

            this.refresh();
        }

        personTypeAhead(criteria: string): angular.IPromise<any> {
            return this.queryResults("PersonTypeAhead", {criteria: criteria});
        }

        cancel(): void {
            this.$state.go("relation.organisations");
        }

        protected refresh(): ng.IPromise<any> {
            return this.load({
                    id: this.params.id
                })
                .then(() => {
                    this.organisation = this.objects["organisation"] as Organisation;
                    this.people = this.collections["people"] as Person[];
                });
        }
    }
    angular
        .module("app")
        .controller("relationEditOrganisationController",
            EditOrganisationController);

}
