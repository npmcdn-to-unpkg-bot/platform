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

    angular
        .module("allors")
        .component("bImageGroup", {
            controller: ImageController,
            templateUrl: ImageGroupTemplate.name,
            require: FormController.require,
            bindings: ImageController.bindings
        } as any);
}
