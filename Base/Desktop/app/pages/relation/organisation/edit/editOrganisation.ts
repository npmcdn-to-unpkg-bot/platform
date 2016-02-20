namespace App.Relation.Organisation {
    interface IEditOrganisationParams {
        id: string;
    }

    class EditOrganisationController {

        organisation: Organisation;

        private allors: Allors.IAllors;
      
        static $inject = ["$scope", "$state", "$stateParams", "notificationService", "allorsService"];
        constructor(private $scope: ng.IScope, private $state: ng.ui.IStateService, private params: IEditOrganisationParams, private notificationService: NotificationService, allorsService: AllorsService) {

            this.allors = allorsService.create("EditOrganisation", $scope, notificationService);
            this.allors.onRefresh(() => this.refresh());
            this.refresh();
        }
        
        get hasChanges() {
            return this.allors && this.allors.hasChanges;
        }

        personTypeAhead(criteria: string) {
            return this.allors.queryResults("PersonTypeAhead", {criteria: criteria});
        }

        cancel(): void {
            this.$state.go("relation.organisations");
        }

        save(): ng.IPromise<any> {
            return this.allors.save();
        }

        private refresh(): ng.IPromise<any> {
            return this.allors.refresh({
                    id: this.params.id
                })
                .then(() => {
                    this.organisation = this.allors.objects["organisation"] as Organisation;
                });
        }
    }
    angular
        .module("app")
        .controller("relationEditOrganisationController",
            EditOrganisationController);

}
