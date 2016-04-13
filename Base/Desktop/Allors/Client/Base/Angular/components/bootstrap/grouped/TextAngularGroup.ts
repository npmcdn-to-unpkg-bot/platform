namespace Allors.Bootstrap {
    export class TextAngularGroupTemplate {
        static name = "allors/bootstrap/text-angular-group";

        private static createDefaultView() {
            return `
<b-group field="$ctrl">
    <b-label field="$ctrl"/>
    <b-input-group field="$ctrl">
` + TextAngularTemplate.createDefaultView() + `
    </b-input-group>
</b-group>
`;
        }

        static register(templateCache: angular.ITemplateCacheService, view = TextAngularGroupTemplate.createDefaultView()) {
            templateCache.put(TextAngularGroupTemplate.name, view);
        }
    }

    angular
        .module("allors")
        .component("bTextAngularGroup", {
            controller: TextAngularController,
            templateUrl: TextAngularGroupTemplate.name,
            require: FormController.require,
            bindings: TextAngularController.bindings
        } as any);
}
