namespace Allors.Bootstrap {
    export class EnumGroupTemplate {
        static name = "allors/bootstrap/enum-group";

        static createDefaultView() {
            return `
<b-group field="$ctrl">
    <b-label field="$ctrl"/>
    <b-input-group field="$ctrl">
` + EnumTemplate.createDefaultView() + `
    </b-input-group>
</b-group>
`;
        };

        static register(templateCache: angular.ITemplateCacheService, view = EnumGroupTemplate.createDefaultView()) {
            templateCache.put(EnumGroupTemplate.name, view);
        }
    }
 
    angular
        .module("allors")
        .component("bEnumGroup", {
            controller: EnumController,
            templateUrl: ["$element", "$attrs", () => EnumGroupTemplate.name],
            require: FormController.require,
            bindings: EnumController.bindings
        } as any);
}
