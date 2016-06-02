﻿namespace Allors.Bootstrap {

    export class SelectOneTemplate {
        static name = "allors/bootstrap/select-one";

        static createDefaultView() {
            return `
<ui-select ng-if="$ctrl.options !== undefined" ng-model="$ctrl.model" append-to-body="$ctrl.appendToBody" >
    <ui-select-match placeholder="{{$ctrl.placeholder}}" allow-clear="{{$ctrl.allowClear}}">{{$ctrl.displayValue}}</ui-select-match>
    <ui-select-choices repeat="item in $ctrl.options | filter: $ctrl.filterFunction($select.search) | orderBy: $ctrl.orderBy()">
        <div ng-bind-html="item[$ctrl.display] | highlight: $select.search"></div>
    </ui-select-choices>
</ui-select>

<ui-select ng-if="$ctrl.options === undefined" ng-model="$ctrl.model" append-to-body="$ctrl.appendToBody" >
    <ui-select-match placeholder="{{$ctrl.placeholder}}" allow-clear="{{$ctrl.allowClear}}">{{$ctrl.displayValue}}</ui-select-match>
    <ui-select-choices repeat="item in $ctrl.asyncOptions | orderBy: $ctrl.orderBy()" refresh="$ctrl.refresh($select.search)" refresh-delay="$ctrl.refreshDelay">
        <div ng-bind-html="item[$ctrl.display] | highlight: $select.search"></div>
    </ui-select-choices>
</ui-select>
`;
        }

        static register(templateCache: angular.ITemplateCacheService, view = SelectOneTemplate.createDefaultView()) {
            templateCache.put(SelectOneTemplate.name, view);
        }
    }

    export class SelectOneController {
        static bindings = {
            model: "=",
            display: "@",
            options: "<",
            refreshDelay: "<",
            lookup: "&lookup",
            order: "<",
            allowClear: "<",
            appendToBody: "<",
            placeholder: "@"
        }

        appendToBody = false;
        allowClear = true;
        placeholder = "Select a value";

        model: SessionObject;
        display: string;
        options: SessionObject[];
        asyncOptions: SessionObject[];
        order: any;

        lookup: (any) => angular.IPromise<any>;

        static $inject = ["$log", "$translate"];
        constructor($log: angular.ILogService) {
        }

        get displayValue(): any {
            return this.model && this.model[this.display];
        }

        filterFunction(criteria: string): (object) => boolean {

            return (object) => {
                const value = object[this.display] as string;
                if (value) {
                    const lowerCaseValue = value.toLowerCase();
                    const lowerCaseCriteria = criteria.toLowerCase();

                    return lowerCaseValue.indexOf(lowerCaseCriteria) >= 0;
                }

                return false;
            }
        }

        orderBy() {
            if (this.order) {
                return this.order;
            } else {
                return this.display;
            }
        }

        refresh(criteria) {
            this
                .lookup({ criteria: criteria })
                .then((results) => {
                    this.asyncOptions = results;
                });
        }
    }

    angular
        .module("allors")
        .component("bSelectOne", {
            controller: SelectOneController,
            templateUrl: SelectOneTemplate.name,
            require: FormController.require,
            bindings: SelectOneController.bindings
        } as any);
}
