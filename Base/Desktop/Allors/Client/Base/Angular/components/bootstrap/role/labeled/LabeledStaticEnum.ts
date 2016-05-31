namespace Allors.Bootstrap.Role {
    export class StaticEnumGroupTemplate {
        static name = "allors/bootstrap/role/labeled-static-enum";

        private static createDefaultView() {
            return `
<b-role-labeled field="$ctrl">
    <b-role-label field="$ctrl"/>
    <b-role-labeled-input field="$ctrl">
` + StaticEnumTemplate.createDefaultView() + `
    </b-role-labeled-input>
</b-group>
`;
        }

        static register(templateCache: angular.ITemplateCacheService, view = StaticEnumGroupTemplate.createDefaultView()) {
            templateCache.put(StaticEnumGroupTemplate.name, view);
        }
    }

    angular
        .module("allors")
        .component("bRoleLabeledStaticEnum", {
            controller: StaticEnumController,
            templateUrl: StaticEnumGroupTemplate.name,
            require: FormController.require,
            bindings: StaticEnumController.bindings
        } as any);
}
