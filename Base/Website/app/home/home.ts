module App.Home
{
    interface IHomeModel {
        title: string;
    }

    interface IHomeState extends ng.ui.IState {
    }

    class HomeController implements IHomeModel {
        title: string;
 
        static $inject = ["$state", "allorsService"];
        constructor(private $state: IHomeState, private allorsService: Allors.Service) {
            this.title = "Home";
        }
    }
    angular
        .module("app")
        .controller("homeController",
			HomeController);

}