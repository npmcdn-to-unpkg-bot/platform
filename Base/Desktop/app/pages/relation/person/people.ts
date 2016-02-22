namespace App.Relation.Person {
    class PeopleController extends Page {
      
        people: Person[];

        static $inject = ["allorsService", "$scope", "$state", "$stateParams"];
        constructor(allors: AllorsService, $scope: ng.IScope, private $state: ng.ui.IStateService, private params: { id: string }) {
            super("People", allors, $scope);

            this.refresh();
        }

        delete(person: Allors.Domain.Person) {
            this.invoke(person.Delete);
        }
        
        refresh(): ng.IPromise<any> {
            return this.load().then(() => {
                this.people = this.collections["people"] as Person[];
            });;
        }
    }
    angular
        .module("app")
        .controller("relationPeopleController",
            PeopleController);
}
