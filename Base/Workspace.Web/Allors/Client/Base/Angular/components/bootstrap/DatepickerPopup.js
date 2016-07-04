var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var Allors;
(function (Allors) {
    var Bootstrap;
    (function (Bootstrap) {
        var DatepickerPopupTemplate = (function () {
            function DatepickerPopupTemplate() {
            }
            DatepickerPopupTemplate.createDefaultView = function () {
                return "\n<p class=\"input-group\">\n    <input type=\"date\" placeholder=\"{{$ctrl.placeholder}}\" class=\"form-control\" datepicker-append-to-body=\"true\"\n            uib-datepicker-popup \n            is-open=\"$ctrl.opened\" \n            ng-model=\"$ctrl.role\"\n            ng-model-options=\"$ctrl.modelOptions\"\n            ng-disabled=\"!$ctrl.canWrite\"\n            ng-required=\"$ctrl.roleType.isRequired\">\n    <span class=\"input-group-btn\">\n        <button type=\"button\" class=\"btn btn-default\" ng-click=\"$ctrl.opened = true\"><i class=\"glyphicon glyphicon-calendar\"></i></button>\n    </span>\n</p>\n";
            };
            DatepickerPopupTemplate.register = function (templateCache, view) {
                if (view === void 0) { view = DatepickerPopupTemplate.createDefaultView(); }
                templateCache.put(DatepickerPopupTemplate.name, view);
            };
            DatepickerPopupTemplate.name = "allors/bootstrap/datepicker-popup";
            return DatepickerPopupTemplate;
        }());
        Bootstrap.DatepickerPopupTemplate = DatepickerPopupTemplate;
        var DatepickerPopupController = (function (_super) {
            __extends(DatepickerPopupController, _super);
            function DatepickerPopupController($log, $translate) {
                _super.call(this, $log, $translate);
                this.timezone = "UTC";
                this.modelOptions = {};
            }
            DatepickerPopupController.prototype.$onInit = function () {
                this.modelOptions.timezone = this.timezone;
            };
            DatepickerPopupController.bindings = {
                object: "<",
                relation: "@",
                timezone: "@"
            };
            DatepickerPopupController.$inject = ["$log", "$translate"];
            return DatepickerPopupController;
        }(Bootstrap.Field));
        Bootstrap.DatepickerPopupController = DatepickerPopupController;
        angular
            .module("allors")
            .component("bDatepickerPopup", {
            controller: DatepickerPopupController,
            templateUrl: DatepickerPopupTemplate.name,
            require: Bootstrap.FormController.require,
            bindings: DatepickerPopupController.bindings
        });
    })(Bootstrap = Allors.Bootstrap || (Allors.Bootstrap = {}));
})(Allors || (Allors = {}));
//# sourceMappingURL=DatepickerPopup.js.map