﻿module App.Organisation {
    class OrganisationEditController{
        profile: Profile;
        filter: Filter;

        filterOrganisations: Allors.Domain.Organisation[];

        organisation: Allors.Domain.Organisation;

        private context: Allors.Context;

        static $inject = ["$rootScope", "$scope", "$http", "notificationService", "profileService", "databaseService", "workspaceService"];
        constructor(
            private $rootScope: ng.IRootScopeService,
            private $scope: ng.IScope,
            private $http: ng.IHttpService,
            private notificationService: NotificationService,
            private profileService: ProfileService,
            databaseService: Allors.DatabaseService,
            workspaceService: Allors.WorkspaceService) {

            this.context = new Allors.Context("OrganisationEdit", databaseService, workspaceService);

            this.profile = profileService.profile;
            this.filter = new Filter(this.context, this.profile);

            this.refresh(true)
                .then(() => {
                    this.$scope.$on("refresh", (event, args) => {
                        this.refresh(args !== this.context.name);
                    });
                });
        }
        
        getPersonTypeAhead(criteria: string) {
            return this.context.queryResults("PersonTypeAhead", { criteria: criteria });
        }
        
        get hasChanges() {
            return this.context && this.context.session.hasChanges;
        }
        
        save(): void {
            this.context.save()
                .then(() => {
                    this.notificationService.info("Successfully saved.");
                    this.$rootScope.$broadcast("refresh", this.context.name);
                })
                .catch(responseError => this.error(<Allors.Data.ResponseError>responseError));
        }

        reset(): void {
            this.refresh(true);
        }

        private refresh(init: boolean): ng.IPromise<any> {
            return this.context.refresh()
                .then(() => {
                    if (init) {
                        this.filterOrganisations = <Allors.Domain.Organisation[]>this.context.collections["filterOrganisations"];
                    }

                    this.filter.enabled = true;
                });
        }

        private error(responseError: Allors.Data.ResponseError): void {
            this.notificationService.responseError(responseError);
        }
    }
    angular
        .module("app")
        .controller("organisationEditController",
            OrganisationEditController);

}
