var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var Allors;
(function (Allors) {
    var Bootstrap;
    (function (Bootstrap) {
        var EnumTemplate = (function () {
            function EnumTemplate() {
            }
            EnumTemplate.createDefaultView = function () {
                return "\n<select class=\"form-control\" \n            ng-model=\"$ctrl.role\" \n            ng-disabled=\"!$ctrl.canWrite\" \n            ng-required=\"$ctrl.roleType.isRequired\"\n            ng-options=\"enum.value as enum.name for enum in $ctrl.enums\">\n    <option ng-if=\"!$ctrl.roleType.isRequired\" value=\"\"></option>     \n</select>\n";
            };
            EnumTemplate.register = function (templateCache, view) {
                if (view === void 0) { view = EnumTemplate.createDefaultView(); }
                templateCache.put(EnumTemplate.name, view);
            };
            EnumTemplate.name = "allors/bootstrap/enum";
            return EnumTemplate;
        }());
        Bootstrap.EnumTemplate = EnumTemplate;
        var Enum = (function () {
            function Enum(value, name) {
                this.value = value;
                this.name = name;
            }
            return Enum;
        }());
        Bootstrap.Enum = Enum;
        var EnumController = (function (_super) {
            __extends(EnumController, _super);
            function EnumController($log, $translate) {
                var _this = this;
                _super.call(this, $log, $translate);
                var type = eval(this.fullTypeName);
                if (type) {
                    var lastIndex = this.fullTypeName.lastIndexOf(".");
                    var typeName = this.fullTypeName.substr(lastIndex + 1);
                    this.enums = [];
                    for (var k in type) {
                        if (type.hasOwnProperty(k)) {
                            var value = type[k];
                            if (typeof value === "number") {
                                var name_1 = type[value];
                                var humanizedName = Allors.Filters.Humanize.filter(name_1);
                                var enumeration = new Enum(value, humanizedName);
                                this.enums.push(enumeration);
                                (function (enumeration, key1, key2) {
                                    _this.translate(key1, key2, function (translatedName) {
                                        if (translatedName) {
                                            enumeration.name = translatedName;
                                        }
                                    });
                                })(enumeration, "enum_" + typeName + "_" + value, "enum_" + typeName + "_" + name_1);
                            }
                        }
                    }
                }
            }
            EnumController.bindings = {
                object: "<",
                relation: "@",
                fullTypeName: "@enum"
            };
            EnumController.$inject = ["$log", "$translate"];
            return EnumController;
        }(Bootstrap.Field));
        Bootstrap.EnumController = EnumController;
        angular
            .module("allors")
            .component("bEnum", {
            controller: EnumController,
            templateUrl: EnumTemplate.name,
            require: Bootstrap.FormController.require,
            bindings: EnumController.bindings
        });
    })(Bootstrap = Allors.Bootstrap || (Allors.Bootstrap = {}));
})(Allors || (Allors = {}));
//# sourceMappingURL=Enum.js.map