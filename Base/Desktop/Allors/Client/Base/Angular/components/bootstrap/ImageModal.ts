﻿namespace Allors.Bootstrap {
    export class ImageModalTemplate {
        static name = "allors/bootstrap/image/modal";

        private static view =
`
{{$ctrl.format}}
<div class="modal-header">
    <h3 class="modal-title">Image</h3>
</div>

<div class="modal-body">
    
    <div class="row" style="height:20vw;">
        <div class="col-sm-6" style="height:100%;">
            <img-crop   image="$ctrl.image" 
                        area-min-size="$ctrl.size"
                        result-image="$ctrl.croppedImage" 
                        result-image-size="$ctrl.size"
                        result-image-format="$ctrl.format"
                        result-image-quality="$ctrl.quality">
            </img-crop>
        </div>

        <div class="col-sm-6 center-block" style="height:100%;">
            <span style="display: inline-block; height: 100%; vertical-align: middle;"></span>
            <img ng-src="{{$ctrl.croppedImage}}" class="img-responsive img-thumbnail" style="vertical-align: middle;"/>
        </div>
    </div>

</div>

<div class="modal-footer">
    <div class="pull-left">
        <label class="btn btn-default" for="file-selector">
            <input id="file-selector" type="file" style="display:none;" model-data-uri="$ctrl.image">
            Select file
        </label>
    </div>

    <button class="btn btn-primary" type="button" ng-click="$ctrl.ok()">OK</button>
    <button class="btn btn-danger" type="button" ng-click="$ctrl.cancel()">Cancel</button>
</div>
`;
        
        static register(templateCache: angular.ITemplateCacheService) {
            templateCache.put(ImageModalTemplate.name, ImageModalTemplate.view);
        }
    }

    export class ImageModalController {

        image = "";
        croppedImage = "";

        static $inject = ["$scope", "$uibModalInstance", "$log", "$translate", "size", "format", "quality"];
        constructor(private $scope: ng.IScope, private $uibModalInstance: angular.ui.bootstrap.IModalServiceInstance, $log: angular.ILogService, $translate: angular.translate.ITranslateService, private size: number, private format: string, private quality: number) {
        }

        ok() {
            this.$uibModalInstance.close(this.croppedImage);
        }

        cancel() {
            this.$uibModalInstance.dismiss("cancel");
        }
    }
}