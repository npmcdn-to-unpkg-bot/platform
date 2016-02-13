module App.Home
{
    class HomeController {
        
        private context: Allors.Context;
        
        static $inject = ["databaseService", "workspaceService"];
        constructor(
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