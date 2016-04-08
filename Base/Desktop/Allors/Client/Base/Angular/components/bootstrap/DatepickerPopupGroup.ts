namespace Allors.Bootstrap {

    export class DatepickerPopupGroupTemplate {
        static name = "allors/bootstrap/datepicker-popup-group";

        static view = 
`
<b-group field="$ctrl">
    <b-label field="$ctrl"/>
    <b-input-group field="$ctrl">

    <p class="input-group">
        <input type="date" placeholder="{{$ctrl.placeholder}}" class="form-control" 
                    uib-datepicker-popup
                    datepicker-options="$ctrl.options" 
                    is-open="$ctrl.opened" 
                    ng-model="$ctrl.role"
                    ng-model-options="$ctrl.modelOptions"
                    ng-disabled="!$ctrl.canWrite"
                    ng-required="$ctrl.roleType.isRequired">
         <span class="input-group-btn">
            <button type="button" class="btn btn-default" ng-click="$ctrl.opened = !$ctrl.opened"><i class="glyphicon glyphicon-calendar"></i></button>
        </span>
    </p>

    </b-input-group>
</b-group>
`;

        static register(templateCache: angular.ITemplateCacheService) {
            templateCache.put(DatepickerPopupGroupTemplate.name, DatepickerPopupGroupTemplate.view);
        }
    }

    class DatepickerPopupGroupController extends Bootstrap.Field {
        timezone = "UTC";
        format = "yyyy-MM-dd";
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
        .component("bDatepickerPopupGroup", {
            controller: DatepickerPopupGroupController,
            templateUrl: DatepickerPopupGroupTemplate.name,
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
