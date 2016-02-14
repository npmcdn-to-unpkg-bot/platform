module App.Settings {
   class SettingsController{
        profile: Profile;
        filter: Filter;
        person: Allors.Domain.Person;

        private context: Allors.Context;

        static $inject = ["$rootScope", "$scope", "$http", "notificationService", "profileService", "databaseService", "workspaceService"];
        constructor(
            private $rootScope: ng.IRootScopeService,
            private $scope: ng.IScope,
            private $http: ng.IHttpService,
            private notificationService: NotificationService,
            profileService: ProfileService,
            databaseService: Allors.DatabaseService,
            workspaceService: Allors.WorkspaceService) {

            this.context = new Allors.Context("Settings", databaseService, workspaceService);
           
            this.profile = profileService.profile;
            this.filter = new Filter(this.context, this.profile);

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

        save(): ng.IPromise<any> {
            return this.context.finalize(
                this.context.save()
                    .then((saveResponse: Allors.Data.SaveResponse) => {
                        this.notificationService.saveResponse(saveResponse);
                    }))
                .then(() => {
                    this.$rootScope.$broadcast("refresh", this.context.name);
                }, e => {
                    throw new Error(String(e));
                });;
        }

        private refresh(): ng.IPromise<any> {
            return this.context.finalize(
                this.context.refresh()
                    .then(() => {
                        this.person = this.context.objects["person"] as Allors.Domain.Person;
                    }));
        }
    }
    angular
        .module("app")
        .controller("settingsController",
            SettingsController);

}
