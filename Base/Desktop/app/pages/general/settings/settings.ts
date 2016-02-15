namespace App.Settings {
   class SettingsController{

       user: Allors.Domain.Person;

        private context: Allors.Context;

        static $inject = ["$rootScope", "$scope", "$http", "notificationService", "profileService", "databaseService", "workspaceService"];
        constructor(
            private $rootScope: ng.IRootScopeService,
            private $scope: ng.IScope,
            private $http: ng.IHttpService,
            private toaster: NotificationService,
            databaseService: Allors.DatabaseService,
            workspaceService: Allors.WorkspaceService) {

            this.context = new Allors.Context("Settings", databaseService, workspaceService);

            this.refresh()
                .then(() => {
                    this.$scope.$on("refresh", () => {
                        this.refresh();
                    });
                });
        }

        get hasChanges() {
            return this.context && this.context.session.hasChanges;
        }

        reset(): void {
            this.refresh();
        }

        save(): void {
            this.context.save()
                .then(() => {
                    this.toaster.info("Successfully saved.");
                    this.$rootScope.$broadcast("refresh", this.context.name);
                });
        }

        private refresh(): ng.IPromise<any> {
            return this.context.refresh()
                .then(() => {
                    this.user = this.context.objects["user"] as Allors.Domain.Person;
                });
        }

        private error(responseError: Allors.Data.ResponseError): void {
            this.toaster.responseError(responseError);
        }
    }
    angular
        .module("app")
        .controller("settingsController",
            SettingsController);

}
