var Allors;
(function (Allors) {
    var Bootstrap;
    (function (Bootstrap) {
        var ImageModalTemplate = (function () {
            function ImageModalTemplate() {
            }
            ImageModalTemplate.register = function (templateCache) {
                templateCache.put(ImageModalTemplate.name, ImageModalTemplate.view);
            };
            ImageModalTemplate.name = "allors/bootstrap/image/modal";
            ImageModalTemplate.view = "\n<div class=\"modal-header\">\n    <h3 class=\"modal-title\">Image</h3>\n</div>\n\n<div class=\"modal-body\">\n    \n    <div class=\"row\" style=\"height:20vw;\">\n        <div class=\"col-sm-6\" style=\"height:100%;\">\n            <img-crop   image=\"$ctrl.image\" \n                        area-min-size=\"$ctrl.size\"\n                        result-image=\"$ctrl.croppedImage\" \n                        result-image-size=\"$ctrl.size\"\n                        result-image-format=\"$ctrl.format\"\n                        result-image-quality=\"$ctrl.quality\">\n            </img-crop>\n        </div>\n\n        <div class=\"col-sm-6 center-block\" style=\"height:100%;\">\n            <img ng-if=\"$ctrl.croppedImage\" ng-src=\"{{$ctrl.croppedImage}}\" class=\"img-responsive img-thumbnail\" style=\"vertical-align: middle; height: 90%\"/>\n        </div>\n    </div>\n\n</div>\n\n<div class=\"modal-footer\">\n    <div class=\"pull-left\">\n        <label class=\"btn btn-default\" for=\"file-selector\">\n            <input id=\"file-selector\" type=\"file\" style=\"display:none;\" model-data-uri=\"$ctrl.image\">\n            Select file\n        </label>\n    </div>\n\n    <button class=\"btn btn-primary\" type=\"button\" ng-click=\"$ctrl.ok()\">OK</button>\n    <button class=\"btn btn-danger\" type=\"button\" ng-click=\"$ctrl.cancel()\">Cancel</button>\n</div>\n";
            return ImageModalTemplate;
        }());
        Bootstrap.ImageModalTemplate = ImageModalTemplate;
        var ImageModalController = (function () {
            function ImageModalController($scope, $uibModalInstance, $log, $translate, size, format, quality) {
                this.$scope = $scope;
                this.$uibModalInstance = $uibModalInstance;
                this.size = size;
                this.format = format;
                this.quality = quality;
                this.image = "";
                this.croppedImage = "";
            }
            ImageModalController.prototype.ok = function () {
                this.$uibModalInstance.close(this.croppedImage);
            };
            ImageModalController.prototype.cancel = function () {
                this.$uibModalInstance.dismiss("cancel");
            };
            ImageModalController.$inject = ["$scope", "$uibModalInstance", "$log", "$translate", "size", "format", "quality"];
            return ImageModalController;
        }());
        Bootstrap.ImageModalController = ImageModalController;
    })(Bootstrap = Allors.Bootstrap || (Allors.Bootstrap = {}));
})(Allors || (Allors = {}));
//# sourceMappingURL=ImageModal.js.map