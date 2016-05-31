namespace Allors.Bootstrap.Role {
    export class LabeledRadioButtonTemplate {
        static name = "allors/bootstrap/role/radio-button-group";

        static createDefaultView() {
            return `
<b-role-labeled field="$ctrl">
    <b-role-label field="$ctrl"/>
    <b-role-labeled-input field="$ctrl">
` + RadioButtonTemplate.createDefaultView() + `
    </b-role-labeled-input>
</b-role-labeled>
`;
        }

        static register(templateCache: angular.ITemplateCacheService, view = LabeledRadioButtonTemplate.createDefaultView()) {
            templateCache.put(LabeledRadioButtonTemplate.name, view);
        }
    }

    angular
        .module("allors")
        .component("bRoleLabeledRadioButton", {
            controller: RadioButtonController,
            templateUrl: ["$element", "$attrs", () => LabeledRadioButtonTemplate.name],
            require: FormController.require,
            bindings: RadioButtonController.bindings
        } as any);
}
