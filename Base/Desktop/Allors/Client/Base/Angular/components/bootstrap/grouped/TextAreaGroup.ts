namespace Allors.Bootstrap {
    export class TextareaGroupTemplate {
        static name = "allors/bootstrap/textarea-group";

        static createDefaultView() {
            return `
<b-group field="$ctrl">
    <b-label field="$ctrl"/>
    <b-input-group field="$ctrl">
` + TextareaTemplate.createDefaultView() + `
    </b-input-group>
</b-group>
`;
        }

        static register(templateCache: angular.ITemplateCacheService, view = TextareaGroupTemplate.createDefaultView()) {
            templateCache.put(TextareaGroupTemplate.name, view);
        }
    }

    angular
        .module("allors")
        .component("bTextareaGroup", {
            controller: TextareaController,
            templateUrl: TextareaGroupTemplate.name,
            require: FormController.require,
            bindings: TextareaController.bindings
        } as any);
}
