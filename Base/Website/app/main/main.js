var App;
(function (App) {
    var Main;
    (function (Main) {
        var MainController = (function () {
            function MainController($http, allorsService) {
                this.$http = $http;
                this.allorsService = allorsService;
                this.refresh();
            }
            MainController.prototype.refresh = function () {
                var _this = this;
                this.$http.post('/Angular/Main', {}).then(function (response) {
                    _this.allorsService.load(response.data)
                        .then(function (context) { return _this.context = context; });
                });
            };
            MainController.$inject = ["$http", "allorsService"];
            return MainController;
        })();
        angular
            .module("app")
            .controller("mainController", MainController);
    })(Main = App.Main || (App.Main = {}));
})(App || (App = {}));
//# sourceMappingURL=main.js.map