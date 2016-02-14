module App.Layout {
    class LayoutController {

        person: Allors.Domain.Person;

        private context: Allors.Context;

        get showTest(): boolean {
            if (this.person && this.person.UserName.toLowerCase() === "administrator") {
                return true;
            }

            return false;
        }

        static $inject = ["$rootScope", "$scope", "$http", "$log", "databaseService", "workspaceService"];
        constructor(
            private $rootScope: ng.IRootScopeService,
            private $scope: ng.IScope,
            private $http: ng.IHttpService,
            private $log: ng.ILogService,
            databaseService: Allors.DatabaseService,
            workspaceService: Allors.WorkspaceService) {

            this.context = new Allors.Context("Main", databaseService, workspaceService);

            this.refresh()
                .then(() => {
                    this.$scope.$on("refresh", () => {
                        this.refresh();
                    });
                });
        }
        
        private refresh(): ng.IPromise<any> {
            return this.context.finalize(
                this.context.refresh()
                    .then(() => {
                        this.person = this.context.objects["person"] as Allors.Domain.Person;
                }));
        }
    }
    angular
        .module("app")
        .controller("layoutController",
        LayoutController);

}
