namespace Allors.Bootstrap.Role {
    export class LabeledEnumTemplate {
        static name = "allors/bootstrap/role/labeled-enum";

        static createDefaultView() {
            return `
<b-role-labeled field="$ctrl">
    <b-role-label field="$ctrl"/>
    <b-role-labeled-input field="$ctrl">
` + EnumTemplate.createDefaultView() + `
    </b-role-labeled-input>
</b-role-labeled>
`;
        };

        static register(templateCache: angular.ITemplateCacheService, view = LabeledEnumTemplate.createDefaultView()) {
            templateCache.put(LabeledEnumTemplate.name, view);
        }
    }
 
    angular
        .module("allors")
        .component("bRoleLabeledEnum", {
            controller: EnumController,
            templateUrl: LabeledEnumTemplate.name,
            require: FormController.require,
            bindings: EnumController.bindings
        } as any);
}
