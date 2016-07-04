var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var App;
(function (App) {
    var Layout;
    (function (Layout) {
        var MainController = (function (_super) {
            __extends(MainController, _super);
            function MainController(allors, $scope, $state, params) {
                _super.call(this, "Main", allors, $scope);
                this.$state = $state;
                this.params = params;
                this.refresh();
            }
            MainController.prototype.refresh = function () {
                var _this = this;
                return this.load()
                    .then(function () {
                    _this.person = _this.objects["person"];
                });
            };
            MainController.$inject = ["allorsService", "$scope", "$state", "$stateParams"];
            return MainController;
        }(App.Page));
        angular
            .module("app")
            .controller("mainController", MainController);
    })(Layout = App.Layout || (App.Layout = {}));
})(App || (App = {}));
//# sourceMappingURL=main.js.map