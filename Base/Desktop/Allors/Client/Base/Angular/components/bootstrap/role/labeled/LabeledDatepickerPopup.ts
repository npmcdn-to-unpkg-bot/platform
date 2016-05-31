namespace Allors.Bootstrap.Role {

    export class LabeledDatepickerPopupTemplate {
        static name = "allors/bootstrap/role/labeled-datepicker-popup";

        static createDefaultView() {
            return `
<b-role-labeled field="$ctrl">
    <b-role-label field="$ctrl"/>
    <b-role-labeled-input field="$ctrl">
` + DatepickerPopupTemplate.createDefaultView() + `
    </b-input-group>
</b-role-labeled>
`;
        }

        static register(templateCache: angular.ITemplateCacheService, view = LabeledDatepickerPopupTemplate.createDefaultView()) {
            templateCache.put(LabeledDatepickerPopupTemplate.name, view);
        }
    }

    angular
        .module("allors")
        .component("bRoleLabeledDatepickerPopup", {
            controller: DatepickerPopupController,
            templateUrl: LabeledDatepickerPopupTemplate.name,
            require: FormController.require,
            bindings: DatepickerPopupController.bindings

        } as any);
}
