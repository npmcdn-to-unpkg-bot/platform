namespace Allors.Bootstrap.Role {

    export class LabeledSelectTemplate {
        static name = "allors/bootstrap/role/labeled-select";

        static createDefaultView() {
            return `
<b-role-labeled field="$ctrl">
    <b-role-label field="$ctrl"/> 
    <b-role-labeled-input field="$ctrl">
` + SelectTemplate.createDefaultView() + `
    </b-role-labeled-input>
</b-role-labeled>
`;
        }

        static register(templateCache: angular.ITemplateCacheService, view = LabeledSelectTemplate.createDefaultView()) {
            templateCache.put(LabeledSelectTemplate.name, view);
        }
    }
    
    angular
        .module("allors")
        .component("bRoleLabeledSelect", {
            controller: SelectController,
            templateUrl: LabeledSelectTemplate.name,
            require: FormController.require,
            bindings: SelectController.bindings
        } as any);
}
