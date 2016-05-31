namespace Allors.Bootstrap.Role {
    export class LabeledStaticTemplate {
        static name = "allors/bootstrap/static-group";

        private static createDefaultView() {
            return `
<b-role-labeled field="$ctrl">
    <b-role-label field="$ctrl"/>
    <b-role-labeled-input field="$ctrl">
` + StaticTemplate.createDefaultView() + `
    </b-role-labeled-input>
</b-role-labeled>
`;
        }

        static register(templateCache: angular.ITemplateCacheService, view = LabeledStaticTemplate.createDefaultView()) {
            templateCache.put(LabeledStaticTemplate.name, view);
        }
    }

    angular
        .module("allors")
        .component("bRoleLabeledStatic", {
            controller: StaticController,
            templateUrl: LabeledStaticTemplate.name,
            require: FormController.require,
            bindings: StaticController.bindings
        } as any);
}
