namespace App.Relation.Organisation {
    class AddOrganisationController {
     
        organisation: Allors.Domain.Organisation;
     
        context: Allors.Context;
        events: Allors.Events;
        commands: Allors.Commands;

        static $inject = ["$scope", "$state", "notificationService", "allorsService"];
        constructor(
            private $scope: ng.IScope,
            private $state: ng.ui.IStateService,
            private notificationService: NotificationService,
            allorsServices: Allors.AllorsServices) {

            this.context = new Allors.Context("AddOrganisation", allorsServices.databaseService, allorsServices.workspaceService);
            this.events = new Allors.Events(this.context, allorsServices.eventService, $scope);
            this.commands = new Allors.Commands(this.context, this.events, notificationService);

            this.events.onRefresh(() => {
                this.refresh();
            });
            
            this.refresh()
                .then(() => {
                    this.organisation = this.context.session.create("Organisation");
                });
        }
        
        getTopicTypeAhead(criteria: string) {
            //return this.context.queryResults("TopicTypeAhead", { criteria: criteria });
        }

        getLanguageTypeAhead(criteria: string) {
            //return this.context.queryResults("LanguageTypeAhead", { criteria: criteria });
        }

        cancel(): void {
            this.$state.go("relation.organisations");
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
        .controller("relationAddOrganisationController",
            AddOrganisationController);

}
