namespace App.Relation.Person {
    import Person = Allors.Domain.Person;

    class PeopleController {
      
        people: Person[];

        private allors: Allors.IAllors;
      
        static $inject = ["$scope", "notificationService", "allorsService"];
        constructor(private $scope: ng.IScope, notificationService: NotificationService, allorsService: AllorsService) {

            this.allors = allorsService.create("People", $scope, notificationService);
            this.allors.onRefresh(() => this.refresh());
            this.refresh();
        }

        delete(person: Allors.Domain.Person) {
            this.allors.invoke(person.Delete);
        }
        
        refresh(): ng.IPromise<any> {
            return this.allors.refresh().then(() => {
                this.people = this.allors.collections["people"] as Person[];
            });;
        }
    }
    angular
        .module("app")
        .controller("relationPeopleController",
            PeopleController);
}
