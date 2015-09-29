var App;
(function (App) {
    var Main;
    (function (Main) {
        var MainController = (function () {
            function MainController($rootScope, $scope, $http, allorsService) {
                var _this = this;
                this.$rootScope = $rootScope;
                this.$scope = $scope;
                this.$http = $http;
                this.allorsService = allorsService;
                this.refresh();
                this.$scope.$on("refresh", function () { _this.refresh(); });
            }
            MainController.prototype.refresh = function () {
                var _this = this;
                this.$http.post('/Angular/Main', {}).then(function (response) {
                    _this.allorsService.load(response.data)
                        .then(function (context) {
                        _this.context = context;
                        _this.root = context.objects["root"];
                    });
                });
            };
            MainController.$inject = ["$rootScope", "$scope", "$http", "allorsService"];
            return MainController;
        })();
        angular
            .module("app")
            .controller("mainController", MainController);
    })(Main = App.Main || (App.Main = {}));
})(App || (App = {}));
//# sourceMappingURL=main.js.map