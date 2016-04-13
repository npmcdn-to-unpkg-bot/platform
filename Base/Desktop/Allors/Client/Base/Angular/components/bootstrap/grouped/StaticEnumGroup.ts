namespace Allors.Bootstrap {
    export class StaticEnumGroupTemplate {
        static name = "allors/bootstrap/static-enum-group";

        private static createDefaultView() {
            return `
<b-group field="$ctrl">
    <b-label field="$ctrl"/>
    <b-input-group field="$ctrl">
` + StaticEnumTemplate.createDefaultView() + `
    </b-input-group>
</b-group>
`;
        }

        static register(templateCache: angular.ITemplateCacheService, view = StaticEnumGroupTemplate.createDefaultView()) {
            templateCache.put(StaticEnumGroupTemplate.name, view);
        }
    }

    angular
        .module("allors")
        .component("bStaticEnumGroup", {
            controller: StaticEnumController,
            templateUrl: StaticEnumGroupTemplate.name,
            require: FormController.require,
            bindings: StaticEnumController.bindings
        } as any);
}
