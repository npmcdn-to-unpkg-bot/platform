namespace Allors.Bootstrap {

    export class DatepickerPopupGroupTemplate {
        static name = "allors/bootstrap/datepicker-popup-group";

        static createDefaultView() {
            return `
<b-group field="$ctrl">
    <b-label field="$ctrl"/>
    <b-input-group field="$ctrl">
` + DatepickerPopupTemplate.createDefaultView() + `
    </b-input-group>
</b-group>
`;
        }

        static register(templateCache: angular.ITemplateCacheService, view = DatepickerPopupGroupTemplate.createDefaultView()) {
            templateCache.put(DatepickerPopupGroupTemplate.name, view);
        }
    }

    angular
        .module("allors")
        .component("bDatepickerPopupGroup", {
            controller: DatepickerPopupController,
            templateUrl: DatepickerPopupGroupTemplate.name,
            require: FormController.require,
            bindings: FormController.bindings

        } as any);
}
