namespace App.Layout {
    import Person = Allors.Domain.Custom.Person;

    class MainController {

        person: Person;

        private context: Allors.Context;
        private events: Allors.Events;
        private commands: Allors.Commands;
        
        static $inject = ["$scope", "notificationService", "allorsService"];
        constructor(
            $scope: ng.IScope,
            notificationService: NotificationService,
            allorsServices: Allors.AllorsServices) {

            this.context = new Allors.Context("Main", allorsServices.databaseService, allorsServices.workspaceService);
            this.events = new Allors.Events(this.context, allorsServices.eventService, $scope);
            this.commands = new Allors.Commands(this.context, this.events, notificationService);

            this.events.onRefresh(() => {
                this.refresh();
            });

            this.refresh();
        }
        
        private refresh(): ng.IPromise<any> {
            return this.context.refresh()
                .then(() => {
                    this.person = this.context.objects["person"] as Person;
                });
        }

    }
    angular
        .module("app")
        .controller("mainController",
        MainController);

}
