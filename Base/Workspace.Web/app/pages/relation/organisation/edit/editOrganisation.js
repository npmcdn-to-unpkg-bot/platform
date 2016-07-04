var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var App;
(function (App) {
    var Relation;
    (function (Relation) {
        var Organisation;
        (function (Organisation) {
            var EditOrganisationController = (function (_super) {
                __extends(EditOrganisationController, _super);
                function EditOrganisationController(allors, $scope, $state, params) {
                    _super.call(this, "EditOrganisation", allors, $scope);
                    this.$state = $state;
                    this.params = params;
                    this.refresh();
                }
                EditOrganisationController.prototype.personTypeAhead = function (criteria) {
                    return this.queryResults("PersonTypeAhead", { criteria: criteria });
                };
                EditOrganisationController.prototype.cancel = function () {
                    this.$state.go("relation.organisations");
                };
                EditOrganisationController.prototype.refresh = function () {
                    var _this = this;
                    return this.load({
                        id: this.params.id
                    })
                        .then(function () {
                        _this.organisation = _this.objects["organisation"];
                        _this.people = _this.collections["people"];
                    });
                };
                EditOrganisationController.$inject = ["allorsService", "$scope", "$state", "$stateParams"];
                return EditOrganisationController;
            }(App.Page));
            angular
                .module("app")
                .controller("relationEditOrganisationController", EditOrganisationController);
        })(Organisation = Relation.Organisation || (Relation.Organisation = {}));
    })(Relation = App.Relation || (App.Relation = {}));
})(App || (App = {}));
//# sourceMappingURL=editOrganisation.js.map