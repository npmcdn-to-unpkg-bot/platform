namespace Allors.Bootstrap {

    export class SelectOneTemplate {
        static name = "allors/bootstrap/select-one";

        static bootstrap = 
`
<b-group f="$ctrl">
    <b-label f="$ctrl"/> 
    <div ng-if="!$ctrl.edit">
        <input type="text" autocomplete="off" class="form-control"
               ng-model="$ctrl.display"
               ng-disabled="!$ctrl.canWrite" 
               ng-focus="$ctrl.startEdit()" >
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
        <button ng-show="$ctrl.noResults" class="btn btn-defualt" ng-click="$ctrl.stopEdit()">
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

        static $inject = ["$log"];
        constructor($log: angular.ILogService) {
            super($log);
        }
      
        startEdit() {
            this.edit = true;
        }

        stopEdit() {
            this.edit = false;
        }
        
        lookup = criteria => {
            return (this as any).l({ criteria: criteria });
        }

        select(item: any) {
            this.role = item;
            this.stopEdit();
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
                l: "&l"
            }
        } as any);
}
