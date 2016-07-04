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
            var PeopleController = (function (_super) {
                __extends(PeopleController, _super);
                function PeopleController(allors, $scope, $state, params) {
                    _super.call(this, "People", allors, $scope);
                    this.$state = $state;
                    this.params = params;
                    this.refresh();
                }
                PeopleController.prototype.delete = function (person) {
                    this.invoke(person.Delete);
                };
                PeopleController.prototype.refresh = function () {
                    var _this = this;
                    return this.load().then(function () {
                        _this.people = _this.collections["people"];
                    });
                    ;
                };
                PeopleController.$inject = ["allorsService", "$scope", "$state", "$stateParams"];
                return PeopleController;
            }(App.Page));
            angular
                .module("app")
                .controller("relationPeopleController", PeopleController);
        })(Person = Relation.Person || (Relation.Person = {}));
    })(Relation = App.Relation || (App.Relation = {}));
})(App || (App = {}));
//# sourceMappingURL=people.js.map