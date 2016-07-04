var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var App;
(function (App) {
    var Relation;
    (function (Relation) {
        var AddPerson;
        (function (AddPerson) {
            var AddPersonController = (function (_super) {
                __extends(AddPersonController, _super);
                function AddPersonController(allors, $scope, $state) {
                    var _this = this;
                    _super.call(this, "AddPerson", allors, $scope);
                    this.$state = $state;
                    this.refresh()
                        .then(function () {
                        _this.person = _this.session.create("Person");
                    });
                }
                AddPersonController.prototype.cancel = function () {
                    this.$state.go("relation.people");
                };
                AddPersonController.prototype.refresh = function () {
                    return this.load();
                };
                AddPersonController.$inject = ["allorsService", "$scope", "$state"];
                return AddPersonController;
            }(App.Page));
            angular
                .module("app")
                .controller("relationAddPersonController", AddPersonController);
        })(AddPerson = Relation.AddPerson || (Relation.AddPerson = {}));
    })(Relation = App.Relation || (App.Relation = {}));
})(App || (App = {}));
//# sourceMappingURL=addPerson.js.map