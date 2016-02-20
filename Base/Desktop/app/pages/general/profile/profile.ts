namespace App.Profile {
    import Person = Allors.Domain.Custom.Person;

   class ProfileController{

       person: Person;

       private allors: Allors.IAllors;

        static $inject = ["$scope", "notificationService", "allorsService"];
        constructor(private $scope: ng.IScope, notificationService: NotificationService, allorsService: AllorsService) {

            this.allors = allorsService.create("Settings", $scope, notificationService);
            this.allors.onRefresh(() => this.refresh());
            this.refresh();
        }

        get hasChanges() {
            return this.allors && this.allors.hasChanges;
        }

        reset(): void {
            this.refresh();
        }

        save(): ng.IPromise<any> {
            return this.allors.save();
        }

        private refresh(): ng.IPromise<any> {
            return this.allors.refresh()
                .then(() => {
                    this.person = this.allors.objects["person"] as Person;
                });
        }
    }
    angular
        .module("app")
        .controller("profileController",
            ProfileController);

}
