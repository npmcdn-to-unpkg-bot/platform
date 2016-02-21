namespace App.Profile {
    class ProfileController extends Control {

        person: Person;

        static $inject = ["allorsService", "$scope", "$state", "$stateParams"];
        constructor(allors: AllorsService, $scope: ng.IScope, private $state: ng.ui.IStateService, private params: { id: string }) {
            super("Settings", allors, $scope);

            this.refresh();
        }

        reset(): void {
            this.refresh();
        }

        protected refresh(): ng.IPromise<any> {
            return this.load()
                .then(() => {
                    this.person = this.objects["person"] as Person;
                });
        }
    }
    angular
        .module("app")
        .controller("profileController",
            ProfileController);

}
