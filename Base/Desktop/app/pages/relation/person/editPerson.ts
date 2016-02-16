namespace App.Relation.Person {
    interface IEditPersonParams {
        id: string;
    }

    class EditPersonController {

        person: Allors.Domain.Person;

        context: Allors.Context;
        events: Allors.Events;
        commands: Allors.Commands;
      
        static $inject = ["$scope", "$state", "$stateParams", "notificationService", "allorsService"];
        constructor(
            private $scope: ng.IScope,
            private $state: ng.ui.IStateService,
            private params: IEditPersonParams,
            private notificationService: NotificationService,
            allorsServices: Allors.AllorsServices) {

            this.context = new Allors.Context("EditPerson", allorsServices.databaseService, allorsServices.workspaceService);
            this.events = new Allors.Events(this.context, allorsServices.eventService, $scope);
            this.commands = new Allors.Commands(this.context, this.events, notificationService);

            this.events.onRefresh(() => {
                this.refresh();
            });

            this.refresh();
        }
        
        get hasChanges() {
            return this.context && this.context.session.hasChanges;
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
            return this.context.refresh({
                    id: this.params.id
                })
                .then(() => {
                    this.person = this.context.objects["person"] as Allors.Domain.Person;
                });
        }
    }
    angular
        .module("app")
        .controller("relationEditPersonController",
            EditPersonController);

}
