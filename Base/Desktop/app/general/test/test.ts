module App.Home
{
    class TestController {
        private context: Allors.Context;
        
        static $inject = ["$scope", "databaseService", "workspaceService"];
        constructor(
            private $scope: ng.IScope,
            databaseService: Allors.DatabaseService,
            workspaceService: Allors.WorkspaceService) {

            this.context = new Allors.Context("Test", databaseService, workspaceService);

            this.refresh(true)
                .then(() => {
                    this.$scope.$on("refresh", (event, args) => {
                        this.refresh(args !== this.context.name);
                    });
                });
        }

        getPersonTypeAhead(criteria: string) {
            return this.context.queryResults("PersonTypeAhead", { criteria: criteria });
        }

        private refresh(init: boolean): ng.IPromise<any> {
            return this.context.refresh()
                .then(() => {
                });
        }
    }
    angular
        .module("app")
        .controller("testController",
			TestController);

}