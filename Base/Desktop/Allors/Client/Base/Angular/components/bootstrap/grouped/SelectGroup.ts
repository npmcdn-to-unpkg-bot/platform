namespace Allors.Bootstrap {

    export class SelectGroupTemplate {
        static name = "allors/bootstrap/select-group";

        static createDefaultView() {
            return `
<b-group field="$ctrl">
    <b-label field="$ctrl"/> 
    <b-input-group field="$ctrl">
` + SelectTemplate.createDefaultView() + `
    </b-input-group>
</b-group>
`;
        }

        static register(templateCache: angular.ITemplateCacheService, view = SelectGroupTemplate.createDefaultView()) {
            templateCache.put(SelectGroupTemplate.name, view);
        }
    }
    
    angular
        .module("allors")
        .component("bSelectGroup", {
            controller: SelectController,
            templateUrl: SelectGroupTemplate.name,
            require: FormController.require,
            bindings: SelectController.bindings
        } as any);
}
