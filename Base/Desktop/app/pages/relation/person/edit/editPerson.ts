namespace App.Relation.Person {
    class EditPersonController extends Page {

        person: Allors.Domain.Person;

        static $inject = ["allorsService", "$scope", "$state", "$stateParams"];
        constructor(allors: AllorsService, $scope: ng.IScope, private $state: ng.ui.IStateService, private params: { id: string }) {
            super("EditPerson", allors, $scope);

            this.refresh();
        }
        
        cancel(): void {
            this.$state.go("relation.people");
        }
        
        protected refresh(): ng.IPromise<any> {
            return this.load({
                    id: this.params.id
                })
                .then(() => {
                    this.person = this.objects["person"] as Person;
                });
        }
    }
    angular
        .module("app")
        .controller("relationEditPersonController",
            EditPersonController);

}
