namespace App.Relation.Organisation {
    class OrganisationsController extends Control {
      
        organisations: Organisation[];

        static $inject = ["allorsService", "$scope", "$state", "$stateParams"];
        constructor(allors: AllorsService, $scope: ng.IScope, private $state: ng.ui.IStateService, private params: { id: string }) {
            super("Organisations", allors, $scope);

            this.refresh();
        }

        delete(organisation: Organisation) {
            this.invoke(organisation.Delete);
        }
        
        refresh(): ng.IPromise<any> {
            return this.load()
                .then(() => {
                    this.organisations = this.collections["organisations"] as Organisation[];
                });
        }
    }
    angular
        .module("app")
        .controller("relationOrganisationsController",
            OrganisationsController);
}
