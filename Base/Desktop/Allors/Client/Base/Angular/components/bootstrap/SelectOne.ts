namespace Allors.Bootstrap {

    export class SelectOneTemplate {
        static name = "allors/bootstrap/select-one";

        static bootstrap = 
`
<b-group f="$ctrl">
    <b-label f="$ctrl"/> 
    <div ng-if="!$ctrl.edit" class="input-group">
        <input type="text" autocomplete="off" class="form-control"
               ng-model="$ctrl.display"
               ng-disabled="!$ctrl.canWrite" 
               ng-focus="$ctrl.startEdit()" >
        <span class="input-group-btn">
            <button ng-click="$ctrl.remove()" class="btn btn-default" type="button"><span class="fa fa-close"></span></button>
        </span>
    </div>

    <div ng-if="$ctrl.edit">
        <input ng-model="$ctrl.typed" class="form-control" focus autocomplete="off"
               uib-typeahead="x as x[$ctrl.displayName] for x in $ctrl.lookup($viewValue)"
               typeahead-editable="false"
               typeahead-no-results="$ctrl.noResults"
               typeahead-select-on-exact="true"
               typeahead-select-on-blur="true"
               typeahead-on-select="$ctrl.select($item)"
               ng-blur="$ctrl.stopEdit()" />
        <button ng-if="$ctrl.noResults" class="btn btn-defualt" ng-click="$ctrl.stopEdit()">
            <i class="fa fa-exclamation-circle"></i> No Results
        </button>
    </div>
</b-group>
`;

        static register(templateCache: ng.ITemplateCacheService) {
            templateCache.put(SelectOneTemplate.name, SelectOneTemplate.bootstrap);
        }
    }

    class UiSelectOneController extends Bootstrap.Field {
        edit: boolean;
        noResulst: boolean;
        typed: string;

        static $inject = ["$log", "$translate"];
        constructor($log: angular.ILogService, $translate: angular.translate.ITranslateService) {
            super($log, $translate);
        }
      
        startEdit() {
            this.edit = true;
        }

        stopEdit() {
            this.edit = false;
        }
        
        lookup(criteria) {
            return this.boundLookup({ criteria: criteria });
        }

        select(item: any) {
            this.role = item;
            this.stopEdit();
        }

        remove() {
            this.role = null;
        }
    }

    angular
        .module("allors")
        .component("bSelectOne", {
            controller: UiSelectOneController,
            templateUrl: ["$element", "$attrs", ($element: ng.IAugmentedJQuery, $attrs: ng.IAttributes) => SelectOneTemplate.name],
            require: {
                form: "^bForm"
            },
            bindings: {
                object: "<o",
                roleTypeName: "@r",
                displayName: "@d",
                boundLookup: "&l"
            }
        } as any);
}
