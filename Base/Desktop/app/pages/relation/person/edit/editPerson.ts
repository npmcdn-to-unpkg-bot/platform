namespace App.Relation.Person {
    interface IEditPersonParams {
        id: string;
    }

    class EditPersonController {

        person: Allors.Domain.Person;

        private allors: Allors.IAllors;
      
        static $inject = ["$scope", "$state", "$stateParams", "notificationService", "allorsService"];
        constructor(
            private $scope: ng.IScope,
            private $state: ng.ui.IStateService,
            private params: IEditPersonParams,
            private notificationService: NotificationService,
            allorsService: Allors.AllorsService) {

            this.allors = allorsService.create("EditPerson", $scope, notificationService);
            this.allors.onRefresh(() => this.refresh());
            this.refresh();
        }
        
        get hasChanges() {
            return this.allors && this.allors.hasChanges;
        }

        cancel(): void {
            this.$state.go("relation.people");
        }

        save(): ng.IPromise<any> {
            return this.allors.save();
        }

        private refresh(): ng.IPromise<any> {
            return this.allors.refresh({
                    id: this.params.id
                })
                .then(() => {
                    this.person = this.allors.objects["person"] as Allors.Domain.Person;
                });
        }
    }
    angular
        .module("app")
        .controller("relationEditPersonController",
            EditPersonController);

}
