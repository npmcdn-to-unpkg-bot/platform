namespace App.Relation.Organisation {
    import Organisation = Allors.Domain.Organisation;

    class OrganisationsController {
      
        organisations: Organisation[];

        private allors: Allors.IAllors;
      
        static $inject = ["$scope", "notificationService", "allorsService"];
        constructor($scope: ng.IScope, notificationService: NotificationService, allorsService: AllorsService) {

            this.allors = allorsService.create("Organisations", $scope, notificationService);
            this.allors.onRefresh(() => this.refresh());
            this.refresh();
        }

        delete(organisation: Allors.Domain.Organisation) {
            this.allors.invoke(organisation.Delete);
        }
        
        refresh(): ng.IPromise<any> {
            return this.allors.refresh()
                .then(() => {
                    this.organisations = this.allors.collections["organisations"] as Organisation[];
                });
        }
    }
    angular
        .module("app")
        .controller("relationOrganisationsController",
            OrganisationsController);
}
