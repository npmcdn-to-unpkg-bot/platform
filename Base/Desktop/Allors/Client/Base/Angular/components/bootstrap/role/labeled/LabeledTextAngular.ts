namespace Allors.Bootstrap.Role {
    export class LabeledTextAngularTemplate {
        static name = "allors/bootstrap/role/labeled-text-angular";

        private static createDefaultView() {
            return `
<b-role-labeled field="$ctrl">
    <b-role-label field="$ctrl"/>
    <b-role-labeled-input field="$ctrl">
` + TextAngularTemplate.createDefaultView() + `
    </b-role-labeled-input>
</b-role-labeled>
`;
        }

        static register(templateCache: angular.ITemplateCacheService, view = LabeledTextAngularTemplate.createDefaultView()) {
            templateCache.put(LabeledTextAngularTemplate.name, view);
        }
    }

    angular
        .module("allors")
        .component("bRoleLabeledTextAngular", {
            controller: TextAngularController,
            templateUrl: LabeledTextAngularTemplate.name,
            require: FormController.require,
            bindings: TextAngularController.bindings
        } as any);
}
