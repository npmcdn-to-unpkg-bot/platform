namespace App.Relation.Person {
    interface PersonAddParams {
        id: string;
    }

    class EditPersonController {
        person: Allors.Domain.Person;

        private context: Allors.Context;
        private events: Allors.Events;
        private commands: Allors.Commands;
      
        static $inject = ["$scope", "$state", "$stateParams", "notificationService", "allorsService"];
        constructor(
            private $scope: ng.IScope,
            private $state: ng.ui.IStateService,
            private params: PersonAddParams,
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
            this.$state.go("relation.peoples");
        }

        save(): ng.IPromise<any> {
            return this.commands.save();
        }

        private refresh(): ng.IPromise<any> {
            return this.context.refresh({
                    personId: this.params.id
                })
                .then(() => {
                    this.person = this.context.objects["person"] as Allors.Domain.Person;
                });
        }

        private error(responseError: Allors.Data.ResponseError): void {
            this.notificationService.responseError(responseError);
        }
    }
    angular
        .module("app")
        .controller("relationEditPersonController",
            EditPersonController);

}
