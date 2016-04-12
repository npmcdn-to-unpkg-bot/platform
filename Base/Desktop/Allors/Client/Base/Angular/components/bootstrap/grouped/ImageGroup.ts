namespace Allors.Bootstrap {
    export class ImageGroupTemplate {
        static name = "allors/bootstrap/image-group";

        static createDefaultView() {
            return `
<b-group field="$ctrl">
    <b-label field="$ctrl"/>
    <b-input-group field="$ctrl">
 ` + ImageTemplate.createDefaultView() + `
    </b-input-group>
</b-group>
`;
        }

        static register(templateCache: angular.ITemplateCacheService, view = ImageGroupTemplate.createDefaultView()) {
            templateCache.put(ImageGroupTemplate.name, view);
        }
    }

    class ImageGroupController extends Bootstrap.Field {

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
        .component("bImageGroup", {
            controller: ImageGroupController,
            templateUrl: ImageGroupTemplate.name,
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
