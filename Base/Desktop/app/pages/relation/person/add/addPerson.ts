namespace App.Relation.Person {
    class AddPersonController {
     
        person: Person;
     
        private allors: Allors.IAllors;

        static $inject = ["$scope", "$state", "notificationService", "allorsService"];
        constructor(private $scope: ng.IScope, private $state: ng.ui.IStateService, private notificationService: NotificationService, allorsService: AllorsService) {

            this.allors = allorsService.create("AddPerson", $scope, notificationService);
            this.allors.onRefresh(() => this.refresh());
            this.refresh();            
            this.refresh()
                .then(() => {
                    this.person = this.allors.create("Person") as Person;
                });
        }
       
        cancel(): void {
            this.$state.go("relation.people");
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
        .controller("relationAddPersonController",
            AddPersonController);
}
