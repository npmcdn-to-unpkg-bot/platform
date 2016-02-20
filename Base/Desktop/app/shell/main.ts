namespace App.Layout {
    class MainController {

        person: Person;
        
        private allors: Allors.IAllors;

        static $inject = ["$scope", "notificationService", "allorsService"];
        constructor(private $scope: ng.IScope, notificationService: NotificationService, allorsService: Allors.AllorsService) {

            this.allors = allorsService.create("Main", $scope, notificationService);
            this.allors.onRefresh(() => this.refresh());
            this.refresh();
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
        .controller("mainController",
        MainController);

}
