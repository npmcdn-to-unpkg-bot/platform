namespace App.Relation.Person {
    import Person = Allors.Domain.Person;

    class PeopleController {
      
        people: Person[];

        private context: Allors.Context;
        private events: Allors.Events;
        private commands: Allors.Commands;
      
        static $inject = ["$scope", "notificationService", "allorsService"];
        constructor(
            $scope: ng.IScope,
            notificationService: NotificationService,
            allorsServices: Allors.AllorsServices) {

            this.context = new Allors.Context("People", allorsServices.databaseService, allorsServices.workspaceService);
            this.events = new Allors.Events(this.context, allorsServices.eventService, $scope);
            this.commands = new Allors.Commands(this.context, this.events, notificationService);
            
            this.events.onRefresh(() => {
                this.refresh();
            });

            this.refresh();
        }

        delete(person: Allors.Domain.Person) {
            this.commands.invoke(person.Delete);
        }
        
        refresh(): ng.IPromise<any> {
            return this.context.refresh()
                .then(() => {
                    this.people = this.context.collections["people"] as Person[];
                });
        }
    }
    angular
        .module("app")
        .controller("relationPeopleController",
            PeopleController);
}
