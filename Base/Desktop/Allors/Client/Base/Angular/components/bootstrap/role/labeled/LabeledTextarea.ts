namespace Allors.Bootstrap.Role {
    export class LabeledTextareaTemplate {
        static name = "allors/bootstrap/role/labeled-textarea";

        static createDefaultView() {
            return `
<b-role-labeled field="$ctrl">
    <b-role-label field="$ctrl"/>
    <b-role-labeled-input field="$ctrl">
` + TextareaTemplate.createDefaultView() + `
    </b-role-labeled-input>
</b-role-labeled>
`;
        }

        static register(templateCache: angular.ITemplateCacheService, view = LabeledTextareaTemplate.createDefaultView()) {
            templateCache.put(LabeledTextareaTemplate.name, view);
        }
    }

    angular
        .module("allors")
        .component("bRoleLabeledTextarea", {
            controller: TextareaController,
            templateUrl: LabeledTextareaTemplate.name,
            require: FormController.require,
            bindings: TextareaController.bindings
        } as any);
}
