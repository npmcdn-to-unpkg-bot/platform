var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var App;
(function (App) {
    var Relation;
    (function (Relation) {
        var AddOrganisation;
        (function (AddOrganisation) {
            var AddOrganisationController = (function (_super) {
                __extends(AddOrganisationController, _super);
                function AddOrganisationController(allors, $scope, $state) {
                    var _this = this;
                    _super.call(this, "AddOrganisation", allors, $scope);
                    this.$state = $state;
                    this.refresh()
                        .then(function () {
                        _this.organisation = _this.session.create("Organisation");
                    });
                }
                AddOrganisationController.prototype.cancel = function () {
                    this.$state.go("relation.organisations");
                };
                AddOrganisationController.prototype.refresh = function () {
                    return this.load();
                };
                AddOrganisationController.$inject = ["allorsService", "$scope", "$state"];
                return AddOrganisationController;
            }(App.Page));
            angular
                .module("app")
                .controller("relationAddOrganisationController", AddOrganisationController);
        })(AddOrganisation = Relation.AddOrganisation || (Relation.AddOrganisation = {}));
    })(Relation = App.Relation || (App.Relation = {}));
})(App || (App = {}));
//# sourceMappingURL=addOrganisation.js.map