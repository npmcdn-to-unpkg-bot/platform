namespace Allors.Bootstrap {

    export class DatepickerPopupTemplate {
        static name = "allors/bootstrap/datepicker-popup";

        static view = 
`
<input type="date" placeholder="{{$ctrl.placeholder}}" class="form-control" 
            uib-datepicker-popup 
            datepicker-options="$ctrl.options" 
            is-open="$ctrl.opened" 
            ng-model="$ctrl.role"
            ng-model-options="$ctrl.modelOptions"
            ng-disabled="!$ctrl.canWrite"
            ng-required="$ctrl.roleType.isRequired">
`;

        static register(templateCache: angular.ITemplateCacheService) {
            templateCache.put(DatepickerPopupTemplate.name, DatepickerPopupTemplate.view);
        }
    }

    class DatepickerPopupController extends Bootstrap.Field {
        timezone = "UTC";
        format = "yyyy-mm-dd";
        options: any = {};
        modelOptions: any = {};
        opened: boolean;

        static $inject = ["$log", "$translate"];
        constructor($log: angular.ILogService, $translate: angular.translate.ITranslateService) {
            super($log, $translate);
        }

        $onInit() {
            this.modelOptions.timezone = this.timezone;
        }
    }

    angular
        .module("allors")
        .component("bDatepickerPopup", {
            controller: DatepickerPopupController,
            templateUrl: DatepickerPopupTemplate.name,
            require: {
                form: "^bForm"
            },
            bindings: {
                object: "<",
                relation: "@",
                timezone: "@",
                format: "@",
                options: "<"
            }
        } as any);
}
