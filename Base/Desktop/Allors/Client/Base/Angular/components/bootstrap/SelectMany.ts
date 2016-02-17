namespace Allors.Bootstrap {

    export class SelectManyTemplate {
        static name = "allors/bootstrap/select-many";

        static bootstrap = 
`
<b-group f="$ctrl">
    <b-label f="$ctrl"/> 
    <div class="form-control clear-fix" ng-focus="$ctrl.startEdit()" ng-disabled="!$ctrl.canWrite">
        <button class="btn btn-xs" ng-repeat="x in $ctrl.role" ng-click="$ctrl.remove(x)">
            {{x[$ctrl.displayName]}} <span class="fa fa-close">
        </button>
        <input ng-if="$ctrl.edit" ng-model="typed" focus autocomplete="off" style="border: none"
               uib-typeahead="x as x[$ctrl.displayName] for x in $ctrl.lookup($viewValue)"
               typeahead-editable="false"
               typeahead-no-results="$ctrl.noResults"
               typeahead-select-on-exact="true"
               typeahead-select-on-blur="true"
               typeahead-on-select="$ctrl.select($item)"
               ng-blur="$ctrl.stopEdit()"/>
    </div>
    <div>
        <button ng-show="$ctrl.noResults" class="btn btn-defualt" ng-click="$ctrl.stopEdit()">
            <i class="fa fa-exclamation-circle"></i> No Results
        </button>
    </div>
</b-group>
`;

        static register(templateCache: ng.ITemplateCacheService) {
            templateCache.put(SelectManyTemplate.name, SelectManyTemplate.bootstrap);
        }
    }

    class UiSelectManyController extends Allors.Bootstrap.Field {
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
            this.object.add(this.roleTypeName, item);
            this.stopEdit();
        }

        remove = (item) => {
            this.object.remove(this.roleTypeName, item);
            this.stopEdit();
        }
    }

    angular
        .module("allors")
        .component("bSelectMany", {
            controller: UiSelectManyController,
            templateUrl: ["$element", "$attrs", ($element: ng.IAugmentedJQuery, $attrs: ng.IAttributes) => SelectManyTemplate.name],
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
