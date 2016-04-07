﻿namespace Allors.Bootstrap {

    export class RadioButtonTemplate {
        static name = "allors/bootstrap/radio-button";

        static view = 
`
<div class="btn-group">
    <label class="btn btn-info" 
           uib-btn-radio="true" 
           ng-model="$ctrl.role" 
           ng-disabled="!$ctrl.canWrite"
           ng-required="$ctrl.roleType.isRequired">{{$ctrl.falseLabel}}</label>
    <label class="btn btn-info" 
           uib-btn-radio="false"
           ng-model="$ctrl.role"
           ng-disabled="!$ctrl.canWrite"
           ng-required="$ctrl.roleType.isRequired">{{$ctrl.trueLabel}}</label>
</div>
`;
        static register(templateCache: angular.ITemplateCacheService) {
            templateCache.put(RadioButtonTemplate.name, RadioButtonTemplate.view);
        }
    }

    class RadioButtonController extends Bootstrap.Field {
        static $inject = ["$log", "$translate"];
        constructor($log: angular.ILogService, $translate: angular.translate.ITranslateService) {
            super($log, $translate);
        }
    }

    angular
        .module("allors")
        .component("bRadioButton", {
            controller: RadioButtonController,
            templateUrl: RadioButtonTemplate.name,
            require: {
                form: "^bForm"
            },
            bindings: {
                object: "<",
                relation: "@",
                trueLabel: "@true",
                falseLabel: "@false"
            }
        } as any);
}