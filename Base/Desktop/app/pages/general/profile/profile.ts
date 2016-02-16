namespace App.Profile {
    import Person = Allors.Domain.Custom.Person;

   class ProfileController{

       person: Person;

       context: Allors.Context;
       events: Allors.Events;
       commands: Allors.Commands;

        static $inject = ["$scope", "notificationService", "allorsService"];
        constructor(
            private $scope: ng.IScope,
            private notificationService: NotificationService,
            allorsServices: Allors.AllorsServices) {

            this.context = new Allors.Context("Settings", allorsServices.databaseService, allorsServices.workspaceService);
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

        reset(): void {
            this.refresh();
        }

        save(): ng.IPromise<any> {
            return this.commands.save();
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
        .controller("profileController",
            ProfileController);

}
