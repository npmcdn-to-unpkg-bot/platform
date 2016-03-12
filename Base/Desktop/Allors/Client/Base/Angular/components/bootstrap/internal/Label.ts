namespace Allors.Bootstrap {
    export class LabelTemplate {
        static name = "allors/bootstrap/label";

        static bootstrap = 
`
<label ng-class="$ctrl.form.horizontal ? 'col-sm-3' : '' ">{{$ctrl.field.label}}
    <span ng-if="$ctrl.field.help" class="fa fa-question-circle"
          uib-tooltip="{{$ctrl.field.help}}"
          tooltip-placement="right">
    </span>
</label>
`;

        static register(templateCache: angular.ITemplateCacheService) {
            templateCache.put(LabelTemplate.name, LabelTemplate.bootstrap);
        }
    }

    class LabelComponent {
        field: Bootstrap.Field;

        static $inject = ["$log"];
        constructor($log: angular.ILogService) {
        }
    }

    angular
        .module("allors")
        .component("bLabel", {
            controller: LabelComponent,
            templateUrl: LabelTemplate.name,
            require: {
                form: "^bForm"
            },
            bindings: {
                field: "<"
            }
        } as any);
}
