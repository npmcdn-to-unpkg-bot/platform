var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var App;
(function (App) {
    var Relation;
    (function (Relation) {
        var Person;
        (function (Person) {
            var EditPersonController = (function (_super) {
                __extends(EditPersonController, _super);
                function EditPersonController(allors, $scope, $state, params) {
                    _super.call(this, "EditPerson", allors, $scope);
                    this.$state = $state;
                    this.params = params;
                    this.refresh();
                }
                EditPersonController.prototype.cancel = function () {
                    this.$state.go("relation.people");
                };
                EditPersonController.prototype.save = function () {
                    var _this = this;
                    return _super.prototype.save.call(this).then(function () { return _this.$state.go("relation.people"); });
                };
                EditPersonController.prototype.refresh = function () {
                    var _this = this;
                    return this.load({
                        id: this.params.id
                    })
                        .then(function () {
                        _this.person = _this.objects["person"];
                    });
                };
                EditPersonController.$inject = ["allorsService", "$scope", "$state", "$stateParams"];
                return EditPersonController;
            }(App.Page));
            angular
                .module("app")
                .controller("relationEditPersonController", EditPersonController);
        })(Person = Relation.Person || (Relation.Person = {}));
    })(Relation = App.Relation || (App.Relation = {}));
})(App || (App = {}));
//# sourceMappingURL=editPerson.js.map