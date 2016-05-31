namespace Allors.Bootstrap.Role {
    export class LabeledImageTemplate {
        static name = "allors/bootstrap/role/labeled-image";

        static createDefaultView() {
            return `
<b-role-labeled field="$ctrl">
    <b-role-label field="$ctrl"/>
    <b-role-labeled-input field="$ctrl">
 ` + ImageTemplate.createDefaultView() + `
    </b-role-labeled-input>
</b-role-labeled>
`;
        }

        static register(templateCache: angular.ITemplateCacheService, view = LabeledImageTemplate.createDefaultView()) {
            templateCache.put(LabeledImageTemplate.name, view);
        }
    }

    angular
        .module("allors")
        .component("bRoleLabeledImage", {
            controller: ImageController,
            templateUrl: LabeledImageTemplate.name,
            require: FormController.require,
            bindings: ImageController.bindings
        } as any);
}
