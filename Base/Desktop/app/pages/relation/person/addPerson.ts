namespace App.Relation.Person {
    class AddPersonController {
     
        person: Allors.Domain.Person;
     
        context: Allors.Context;
        events: Allors.Events;
        commands: Allors.Commands;

        static $inject = ["$scope", "$state", "notificationService", "allorsService"];
        constructor(
            private $scope: ng.IScope,
            private $state: ng.ui.IStateService,
            private notificationService: NotificationService,
            allorsServices: Allors.AllorsServices) {

            this.context = new Allors.Context("AddPerson", allorsServices.databaseService, allorsServices.workspaceService);
            this.events = new Allors.Events(this.context, allorsServices.eventService, $scope);
            this.commands = new Allors.Commands(this.context, this.events, notificationService);

            this.events.onRefresh(() => {
                this.refresh();
            });
            
            this.refresh()
                .then(() => {
                    this.person = this.context.session.create("Person");
                });
        }
        
        getTopicTypeAhead(criteria: string) {
            //return this.context.queryResults("TopicTypeAhead", { criteria: criteria });
        }

        getLanguageTypeAhead(criteria: string) {
            //return this.context.queryResults("LanguageTypeAhead", { criteria: criteria });
        }

        cancel(): void {
            this.$state.go("relation.people");
        }

        save(): ng.IPromise<any> {
            return this.commands.save();
        }

        private refresh(): ng.IPromise<any> {
            return this.context.refresh()
                .then(() => {
                });
        }
    }
    angular
        .module("app")
        .controller("relationAddPersonController",
            AddPersonController);

}
