namespace App.Relation.AddPerson {
    class AddPersonController extends Control {

        person: Person;
     
        static $inject = ["allorsService", "$scope", "$state"];
        constructor(allors: AllorsService, $scope: ng.IScope, private $state: ng.ui.IStateService) {
            super("AddPerson", allors, $scope);

            this.refresh()
                .then(() => {
                    this.person = this.create("Person") as Person;
                });
        }
       
        cancel(): void {
            this.$state.go("relation.people");
        }
        
        protected refresh(): ng.IPromise<any> {
            return this.load();
        }
    }
    angular
        .module("app")
        .controller("relationAddPersonController",
            AddPersonController);
}
