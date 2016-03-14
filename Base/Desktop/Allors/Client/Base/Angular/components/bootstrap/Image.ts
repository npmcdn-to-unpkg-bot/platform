namespace Allors.Bootstrap {
    export class ImageTemplate {
        static name = "allors/bootstrap/image";

        private static view = 
`
<b-group field="$ctrl">
    <b-label field="$ctrl"/>
    <b-input-group field="$ctrl">
        
        <div ng-if="!$ctrl.role">
            <button type="button" class="btn btn-default" ng-click="$ctrl.add()">Add new image</button>
        </div>
        
        <div ng-if="$ctrl.role.InDataUri">
            <a ng-click="$ctrl.add()">
                <img ng-src="{{$ctrl.role.InDataUri}}" class="img-responsive img-thumbnail"/>
            </a>
        </div>

        <div ng-if="!$ctrl.role.InDataUri && $ctrl.role">
            <a ng-click="$ctrl.add()">
                <img ng-src="/media/display/{{$ctrl.role.UniqueId}}?revision={{$ctrl.role.Revision}}" class="img-responsive"/>
            </a>
        </div>

    </b-input-group>
</b-group>
`;

        static register(templateCache: angular.ITemplateCacheService) {
            templateCache.put(ImageTemplate.name, ImageTemplate.view);
        }
    }

    class ImageController extends Bootstrap.Field {

        size: number;
        format: string;
        quality: number;

        static $inject = ["$scope", "$uibModal", "$log", "$translate"];
        constructor(private $scope: ng.IScope, private $uibModal: angular.ui.bootstrap.IModalService, $log: angular.ILogService, $translate: angular.translate.ITranslateService) {
            super($log, $translate);
        }

        add() {
            const modalInstance = this.$uibModal.open({
                templateUrl: ImageModalTemplate.name,
                controller: ImageModalController,
                controllerAs: "$ctrl",
                resolve: {
                    size: this.size,
                    format: this.format,
                    quality: this.quality
                }
            });

            modalInstance.result.then(selectedItem => {
                if (!this.role) {
                    this.role = this.object.session.create("Media");
                }

                var media = this.role as Domain.Media;
                media.InDataUri = selectedItem;
            });
        }
    }

    angular
        .module("allors")
        .component("bImage", {
            controller: ImageController,
            templateUrl: ImageTemplate.name,
            require: {
                form: "^bForm"
            },
            bindings: {
                object: "<",
                relation: "@",
                size: "<",
                format: "<",
                quality: "<"
            }
        } as any);
}
