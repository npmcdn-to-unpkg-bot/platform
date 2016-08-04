﻿namespace Allors.Bootstrap {

    export class RadioTemplate {
        static name = "allors/bootstrap/radio";

        static createDefaultView() {
            return `
<label>
    <input type="radio" 
        ng-model="$ctrl.role" 
        ng-disabled="!$ctrl.canWrite"
        ng-required="$ctrl.roleType.isRequired"
        ng-value="true">
    {{$ctrl.falseLabel}}
</label>

<br/>

<label>
<input type="radio" 
        ng-model="$ctrl.role" 
        ng-disabled="!$ctrl.canWrite"
        ng-required="$ctrl.roleType.isRequired"
        ng-value="false">
{{$ctrl.trueLabel}}
</label>

<br/>
`;
        }

        static register(templateCache: angular.ITemplateCacheService, view = RadioTemplate.createDefaultView()) {
            templateCache.put(RadioTemplate.name, view);
        }
    }

    export class RadioController extends Field {
        static bindings = {
            object: "<",
            relation: "@",
            trueLabel: "@true",
            falseLabel: "@false"
        } as { [binding: string]: string }

        static $inject = ["$log", "$translate"];
        constructor($log: angular.ILogService, $translate: angular.translate.ITranslateService) {
            super($log, $translate);
        }

        $onInit() {
        }
    }

    angular
        .module("allors")
        .component("bRadio", {
            controller: RadioController,
            templateUrl: RadioTemplate.name,
            require: FormController.require,
            bindings: RadioController.bindings
        });
}
