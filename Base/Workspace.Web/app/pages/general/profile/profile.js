var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var App;
(function (App) {
    var Profile;
    (function (Profile) {
        var ProfileController = (function (_super) {
            __extends(ProfileController, _super);
            function ProfileController(allors, $scope, $state, params) {
                _super.call(this, "Profile", allors, $scope);
                this.$state = $state;
                this.params = params;
                this.refresh();
            }
            ProfileController.prototype.reset = function () {
                this.refresh();
            };
            ProfileController.prototype.refresh = function () {
                var _this = this;
                return this.load()
                    .then(function () {
                    _this.person = _this.objects["person"];
                });
            };
            ProfileController.$inject = ["allorsService", "$scope", "$state", "$stateParams"];
            return ProfileController;
        }(App.Page));
        angular
            .module("app")
            .controller("profileController", ProfileController);
    })(Profile = App.Profile || (App.Profile = {}));
})(App || (App = {}));
//# sourceMappingURL=profile.js.map