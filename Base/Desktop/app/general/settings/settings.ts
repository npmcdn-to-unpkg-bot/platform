module App.Settings {
   class SettingsController{
        profile: Profile;
        filter: Filter;
        user: Allors.Domain.Person;

        private context: Allors.Context;

        static $inject = ["$rootScope", "$scope", "$http", "notificationService", "profileService", "allorsService"];
        constructor(private $rootScope: ng.IRootScopeService, private $scope: ng.IScope, private $http: ng.IHttpService, private toaster: NotificationService, private profileService: ProfileService, private service: Allors.Service) {
            this.context = service.createContext("Settings");
            this.profile = profileService.profile;
            this.filter = new Filter(this.context, this.profile);

            this.refresh(true)
                .then(() => {
                    this.$scope.$on("refresh", (event, args) => {
                        this.refresh(args !== this.context.name);
                    });
                });
        }

        get hasChanges() {
            return this.context && this.context.session.hasChanges;
        }

        reset(): void {
            this.refresh(true).then(() => {});
        }

        save(): void {
            this.context.save()
                .then(() => {
                    this.toaster.info("Successfully saved.");
                    this.$rootScope.$broadcast("refresh", this.context.name);
                })
                .catch(responseError => this.error(<Allors.Data.ResponseError>responseError));
        }

        private refresh(init: boolean): ng.IPromise<any> {
            return this.context.refresh()
                .then(() => {
                    this.user = <Allors.Domain.Person>this.context.objects["user"];
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
