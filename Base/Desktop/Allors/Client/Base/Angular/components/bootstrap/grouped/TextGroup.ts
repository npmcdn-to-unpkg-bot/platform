namespace Allors.Bootstrap {
    export class TextGroupTemplate {
        static name = "allors/bootstrap/text-group";

        static createDefaultView() {
            return `
<b-group field="$ctrl">
    <b-label field="$ctrl"/>
    <b-input-group field="$ctrl">
` + TextTemplate.createDefaultView() + `
    </b-input-group>
</b-group>
`;
        };

        static register(templateCache: angular.ITemplateCacheService, view = TextGroupTemplate.createDefaultView()) {
            templateCache.put(TextGroupTemplate.name, view);
        }
    }

    angular
        .module("allors")
        .component("bTextGroup", {
            controller: TextController,
            templateUrl: TextGroupTemplate.name,
            require: FormController.require,
            bindings: TextController.bindings
        } as any);
}
