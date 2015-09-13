var App;
(function (App) {
    var Main;
    (function (Main) {
        var MainController = (function () {
            function MainController($state, allorsService) {
                this.$state = $state;
                this.allorsService = allorsService;
                this.title = "Main";
            }
            MainController.$inject = ["$state", "allorsService"];
            return MainController;
        })();
        angular
            .module("app")
            .controller("mainController", MainController);
    })(Main = App.Main || (App.Main = {}));
})(App || (App = {}));
