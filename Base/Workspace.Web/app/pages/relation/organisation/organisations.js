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
            var OrganisationsController = (function (_super) {
                __extends(OrganisationsController, _super);
                function OrganisationsController(allors, $scope, $state, params) {
                    _super.call(this, "Organisations", allors, $scope);
                    this.$state = $state;
                    this.params = params;
                    this.refresh();
                }
                OrganisationsController.prototype.delete = function (organisation) {
                    this.invoke(organisation.Delete);
                };
                OrganisationsController.prototype.refresh = function () {
                    var _this = this;
                    return this.load()
                        .then(function () {
                        _this.organisations = _this.collections["organisations"];
                    });
                };
                OrganisationsController.$inject = ["allorsService", "$scope", "$state", "$stateParams"];
                return OrganisationsController;
            }(App.Page));
            angular
                .module("app")
                .controller("relationOrganisationsController", OrganisationsController);
        })(Organisation = Relation.Organisation || (Relation.Organisation = {}));
    })(Relation = App.Relation || (App.Relation = {}));
})(App || (App = {}));
//# sourceMappingURL=organisations.js.map