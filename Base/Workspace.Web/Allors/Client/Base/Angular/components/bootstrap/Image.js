var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var Allors;
(function (Allors) {
    var Bootstrap;
    (function (Bootstrap) {
        var ImageTemplate = (function () {
            function ImageTemplate() {
            }
            ImageTemplate.createDefaultView = function () {
                return "\n<div ng-if=\"!$ctrl.role\">\n    <button type=\"button\" class=\"btn btn-default\" ng-click=\"$ctrl.add()\">Add new image</button>\n</div>\n        \n<div ng-if=\"$ctrl.role.InDataUri\">\n    <a ng-click=\"$ctrl.add()\">\n        <img ng-src=\"{{$ctrl.role.InDataUri}}\" ng-class=\"$ctrl.imgClass\"/>\n    </a>\n</div>\n\n<div ng-if=\"!$ctrl.role.InDataUri && $ctrl.role\">\n    <a ng-click=\"$ctrl.add()\">\n        <img ng-src=\"/media/display/{{$ctrl.role.UniqueId}}?revision={{$ctrl.role.Revision}}\" ng-class=\"$ctrl.imgClass\"/>\n    </a>\n</div>\n";
            };
            ImageTemplate.register = function (templateCache, view) {
                if (view === void 0) { view = ImageTemplate.createDefaultView(); }
                templateCache.put(ImageTemplate.name, view);
            };
            ImageTemplate.name = "allors/bootstrap/image";
            return ImageTemplate;
        }());
        Bootstrap.ImageTemplate = ImageTemplate;
        var ImageController = (function (_super) {
            __extends(ImageController, _super);
            function ImageController($scope, $uibModal, $log, $translate) {
                _super.call(this, $log, $translate);
                this.$scope = $scope;
                this.$uibModal = $uibModal;
                this.imgClass = "img-responsive";
            }
            ImageController.prototype.add = function () {
                var _this = this;
                var modalInstance = this.$uibModal.open({
                    templateUrl: Bootstrap.ImageModalTemplate.name,
                    controller: Bootstrap.ImageModalController,
                    controllerAs: "$ctrl",
                    resolve: {
                        size: this.size,
                        format: this.format,
                        quality: this.quality
                    }
                });
                modalInstance.result.then(function (selectedItem) {
                    if (!_this.role) {
                        _this.role = _this.object.session.create("Media");
                    }
                    var media = _this.role;
                    media.InDataUri = selectedItem;
                });
            };
            ImageController.bindings = {
                object: "<",
                relation: "@",
                imgClass: "@",
                size: "<",
                format: "<",
                quality: "<"
            };
            ImageController.$inject = ["$scope", "$uibModal", "$log", "$translate"];
            return ImageController;
        }(Bootstrap.Field));
        Bootstrap.ImageController = ImageController;
        angular
            .module("allors")
            .component("bImage", {
            controller: ImageController,
            templateUrl: ImageTemplate.name,
            require: Bootstrap.FormController.require,
            bindings: ImageController.bindings
        });
    })(Bootstrap = Allors.Bootstrap || (Allors.Bootstrap = {}));
})(Allors || (Allors = {}));
//# sourceMappingURL=Image.js.map