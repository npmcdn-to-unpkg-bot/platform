namespace Allors.Bootstrap {
    export class RadioButtonGroupTemplate {
        static name = "allors/bootstrap/radio-button-group";

        static createDefaultView() {
            return `
<b-group field="$ctrl">
    <b-label field="$ctrl"/>
    <b-input-group field="$ctrl">
` + RadioButtonTemplate.createDefaultView() + `
    </b-input-group>
</b-group>
`;
        }

        static register(templateCache: angular.ITemplateCacheService, view = RadioButtonGroupTemplate.createDefaultView()) {
            templateCache.put(RadioButtonGroupTemplate.name, view);
        }
    }

    angular
        .module("allors")
        .component("bRadioButtonGroup", {
            controller: RadioButtonController,
            templateUrl: ["$element", "$attrs", () => RadioButtonGroupTemplate.name],
            require: FormController.require,
            bindings: RadioButtonController.bindings
        } as any);
}
