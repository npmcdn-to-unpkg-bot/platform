module App.Home
{
    class HomeController {
        
        private context: Allors.Context;
        
        static $inject = ["allorsService"];
        constructor(private allorsService: Allors.Service) {
            this.context = allorsService.createContext("Home");
        }

    }
    angular
        .module("app")
        .controller("homeController",
			HomeController);

}