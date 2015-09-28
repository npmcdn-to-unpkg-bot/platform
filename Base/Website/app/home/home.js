var App;
(function (App) {
    var Home;
    (function (Home) {
        var HomeController = (function () {
            function HomeController($state, allorsService) {
                this.$state = $state;
                this.allorsService = allorsService;
                this.title = "Home";
            }
            HomeController.$inject = ["$state", "allorsService"];
            return HomeController;
        })();
        angular
            .module("app")
            .controller("homeController", HomeController);
    })(Home = App.Home || (App.Home = {}));
})(App || (App = {}));
