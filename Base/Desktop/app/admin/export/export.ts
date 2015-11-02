module App.Admin.Export {
    class ExportController {
       private context: Allors.Context;
       profile: App.Profile;
        
       exportType = "Users";

       static $inject = ["$rootScope", "$scope", "$http", "notificationService", "Upload", "profileService", "allorsService"];
       constructor(private $rootScope: ng.IRootScopeService, private $scope: ng.IScope, private $http: ng.IHttpService, private notificationService: App.NotificationService, private Upload: angular.angularFileUpload.IUploadService, private profileService: App.ProfileService, private service: Allors.Service) {
            this.context = service.createContext("Export");
            this.profile = profileService.profile;

            this.refresh(true)
                .then(() => {
                    this.$scope.$on("refresh", (event, args) => {
                        this.refresh(args !== this.context.name);
                    });
                });
        }
        
       export() {
           var url = '/Export/' + this.exportType;
           window.location.href = url;
       }

        private refresh(init: boolean): ng.IPromise<any> {
           return this.context.refresh()
               .then(() => {
                   if (init) {
                   }
               });
        }

        private error(responseError: Allors.Data.ResponseError): void {
            this.notificationService.responseError(responseError);
        }
    }
    angular
        .module("app")
        .controller("exportController",
            ExportController);

}
