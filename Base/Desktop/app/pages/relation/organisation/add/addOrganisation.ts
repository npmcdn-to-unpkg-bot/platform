namespace App.Relation.AddOrganisation {
    class AddOrganisationController {
     
        organisation: Organisation;
     
        private allors: Allors.IAllors;

        static $inject = ["$state", "$scope", "notificationService", "allorsService"];
        constructor(private $state: ng.ui.IStateService, private $scope: ng.IScope, notificationService: NotificationService, allorsService: AllorsService) {

            this.allors = allorsService.create("AddOrganisation", $scope, notificationService);
            this.allors.onRefresh(() => this.refresh());
            this.refresh()
                .then(() => {
                    this.organisation = this.allors.create("Organisation") as Organisation;
                });
        }

        cancel(): void {
            this.$state.go("relation.organisations");
        }

        save(): ng.IPromise<any> {
            return this.allors.save();
        }

        private refresh(): ng.IPromise<any> {
            return this.allors.refresh();
        }
    }
    angular
        .module("app")
        .controller("relationAddOrganisationController",
            AddOrganisationController);

}
