namespace App.Home
{
    class HomeController {
        
        private context: Allors.Context;
        
        static $inject = ["$rootScope", "$scope", "$http", "$log", "databaseService", "workspaceService"];
        constructor(
            private $rootScope: ng.IRootScopeService,
            private $scope: ng.IScope,
            private $http: ng.IHttpService,
            private $log: ng.ILogService,
            databaseService: Allors.DatabaseService,
            workspaceService: Allors.WorkspaceService) {

            this.context = new Allors.Context("Home", databaseService, workspaceService);
        }

    }
    angular
        .module("app")
        .controller("homeController",
			HomeController);

}