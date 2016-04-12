namespace Allors.Bootstrap {
    export class StaticGroupTemplate {
        static name = "allors/bootstrap/static-group";

        private static createDefaultView() {
            return `
<b-group field="$ctrl">
    <b-label field="$ctrl"/>
    <b-input-group field="$ctrl">
` + StaticTemplate.createDefaultView() + `
    </b-input-group>
</b-group>
`;
        }

        static register(templateCache: angular.ITemplateCacheService, view = StaticGroupTemplate.createDefaultView()) {
            templateCache.put(StaticGroupTemplate.name, view);
        }
    }

    angular
        .module("allors")
        .component("bStaticGroup", {
            controller: StaticController,
            templateUrl: ["$element", "$attrs", () => StaticGroupTemplate.name],
            require: FormController.require,
            bindings: StaticController.bindings
        } as any);
}
