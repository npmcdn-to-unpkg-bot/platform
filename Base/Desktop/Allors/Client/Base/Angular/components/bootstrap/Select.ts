namespace Allors.Bootstrap {

    export class SelectTemplate {
        static name = "allors/bootstrap/select";

        static view = 
`
<b-group field="$ctrl">
    <b-label field="$ctrl"/> 
    <b-input-group field="$ctrl">
        <ui-select ng-if="$ctrl.roleType.isOne && $ctrl.options !== undefined" ng-model="$ctrl.role" ng-disabled="!$ctrl.canWrite" ng-required="$ctrl.roleType.isRequired">
            <ui-select-match placeholder="Select a value">{{$ctrl.displayValue}}</ui-select-match>
            <ui-select-choices repeat="item in $ctrl.options | filter: $ctrl.filterFunction($select.search)">
                <div ng-bind-html="item[$ctrl.display] | highlight: $select.search"></div>
            </ui-select-choices>
        </ui-select>

        <ui-select ng-if="$ctrl.roleType.isOne && $ctrl.options === undefined" ng-model="$ctrl.role" ng-disabled="!$ctrl.canWrite" ng-required="$ctrl.roleType.isRequired">
            <ui-select-match placeholder="Select a value">{{$ctrl.displayValue}}</ui-select-match>
            <ui-select-choices repeat="item in $ctrl.asyncOptions" refresh="$ctrl.refresh($select.search)" refresh-delay="0">
                <div ng-bind-html="item[$ctrl.display] | highlight: $select.search"></div>
            </ui-select-choices>
        </ui-select>

        <ui-select ng-if="$ctrl.roleType.isMany && $ctrl.options !== undefined" multiple ng-model="$ctrl.role" ng-disabled="!$ctrl.canWrite" ng-required="$ctrl.roleType.isRequired">
            <ui-select-match placeholder="Select values">{{$item[$ctrl.display]}}</ui-select-match>
            <ui-select-choices repeat="item in $ctrl.options | filter: $ctrl.filterFunction($select.search)">
                <div ng-bind-html="item[$ctrl.display] | highlight: $select.search"></div>
            </ui-select-choices>
        </ui-select>

        <ui-select ng-if="$ctrl.roleType.isMany && $ctrl.options === undefined" multiple ng-model="$ctrl.role" ng-disabled="!$ctrl.canWrite" ng-required="$ctrl.roleType.isRequired">
            <ui-select-match placeholder="Select values">{{$item[$ctrl.display]}}</ui-select-match>
            <ui-select-choices repeat="item in $ctrl.asyncOptions" refresh="$ctrl.refresh($select.search)" refresh-delay="0">
                <div ng-bind-html="item[$ctrl.display] | highlight: $select.search"></div>
            </ui-select-choices>
        </ui-select>
    </b-input-group>

</b-group>
`;

        static register(templateCache: angular.ITemplateCacheService) {
            templateCache.put(SelectTemplate.name, SelectTemplate.view);
        }
    }

    class SelectComponent extends Bootstrap.Field {

        options: SessionObject[];
        asyncOptions: SessionObject[];

        static $inject = ["$log", "$translate"];
        constructor($log: angular.ILogService, $translate: angular.translate.ITranslateService) {
            super($log, $translate);
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
        .component("bSelect", {
            controller: SelectComponent,
            templateUrl: SelectTemplate.name,
            require: {
                form: "^bForm"
            },
            bindings: {
                object: "<",
                relation: "@",
                display: "@",
                options: "<",
                lookup: "&lookup"
            }
        } as any);
}
