namespace Allors.Bootstrap.Role {
    export class LabeledTextTemplate {
        static name = "allors/bootstrap/role/labeled-text";

        static createDefaultView() {
            return `
<b-role-labeled field="$ctrl">
    <b-role-label field="$ctrl"/>
    <b-role-labeled-input field="$ctrl">
` + TextTemplate.createDefaultView() + `
    </b-role-labeled-input>
</b-role-labeled>
`;
        };

        static register(templateCache: angular.ITemplateCacheService, view = LabeledTextTemplate.createDefaultView()) {
            templateCache.put(LabeledTextTemplate.name, view);
        }
    }

    angular
        .module("allors")
        .component("bRoleLabeledText", {
            controller: TextController,
            templateUrl: LabeledTextTemplate.name,
            require: FormController.require,
            bindings: TextController.bindings
        } as any);
}
