module App.Main {
    interface IMainModel {
        title: string;
    }

    interface IMainState extends ng.ui.IState {
    }

    class MainController implements IMainModel {
        title: string;

        static $inject = ["$state", "allorsService"];
        constructor(private $state: IMainState, private allorsService: App.Common.Services.AllorsService) {
            this.title = "Main";
        }
    }
    angular
        .module("app")
        .controller("mainController",
            MainController);

}
