namespace App.Relation.Organisation {
    import Organisation = Allors.Domain.Organisation;

    class OrganisationsController {
      
        organisations: Organisation[];

        private context: Allors.Context;
        private events: Allors.Events;
        private commands: Allors.Commands;
      
        static $inject = ["$scope", "notificationService", "allorsService"];
        constructor(
            $scope: ng.IScope,
            notificationService: NotificationService,
            allorsServices: Allors.AllorsServices) {

            this.context = new Allors.Context("Organisations", allorsServices.databaseService, allorsServices.workspaceService);
            this.events = new Allors.Events(this.context, allorsServices.eventService, $scope);
            this.commands = new Allors.Commands(this.context, this.events, notificationService);
            
            this.events.onRefresh(() => {
                this.refresh();
            });

            this.refresh();
        }

        delete(organisation: Allors.Domain.Organisation) {
            this.commands.invoke(organisation.Delete);
        }
        
        refresh(): ng.IPromise<any> {
            return this.context.refresh()
                .then(() => {
                    this.organisations = this.context.collections["organisations"] as Organisation[];
                });
        }
    }
    angular
        .module("app")
        .controller("relationOrganisationsController",
            OrganisationsController);
}
