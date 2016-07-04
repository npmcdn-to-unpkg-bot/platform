var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var App;
(function (App) {
    var Home;
    (function (Home) {
        var RelationController = (function (_super) {
            __extends(RelationController, _super);
            function RelationController(allors, $scope, $state, params) {
                _super.call(this, "Relation", allors, $scope);
                this.$state = $state;
                this.params = params;
                this.refresh();
            }
            RelationController.prototype.refresh = function () {
                var _this = this;
                return this.load()
                    .then(function () {
                    _this.person = _this.objects["person"];
                });
            };
            RelationController.$inject = ["allorsService", "$scope", "$state", "$stateParams"];
            return RelationController;
        }(App.Page));
        angular
            .module("app")
            .controller("relationController", RelationController);
    })(Home = App.Home || (App.Home = {}));
})(App || (App = {}));
//# sourceMappingURL=relation.js.map