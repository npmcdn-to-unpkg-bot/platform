namespace App.Layout {
    class MainController extends Page {

        person: Person;
        
        static $inject = ["allorsService", "$scope", "$state", "$stateParams"];
        constructor(allors: AllorsService, $scope: ng.IScope, private $state: ng.ui.IStateService, private params: { id: string }) {
            super("Main", allors, $scope);

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
        .controller("mainController",
        MainController);

}
