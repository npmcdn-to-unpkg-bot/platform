namespace Allors.Bootstrap {
    export class TextTemplates {

        static bootstrap = 
`<div class="form-group">
<label>{{$ctrl.label }}</label> <input placeholder="{{$ctrl.placeholder}}" class="form-control" ng-model="$ctrl.role">
</div>`;

        static register(templateCache: ng.ITemplateCacheService) {
            templateCache.put("allors/bootstrap/text", TextTemplates.bootstrap);
        }
    }

    class TextController extends FieldController {
        static $inject = ["$log"];
        constructor($log: angular.ILogService) {
            super($log);
        }
    }

    angular
        .module("allors")
        .component("bText", {
            controller: TextController,
            templateUrl: ["$element", "$attrs", ($element: ng.IAugmentedJQuery, $attrs: ng.IAttributes) => "allors/bootstrap/text"],
            require: {
                form: "^bForm"
            },
            bindings: {
                object: "<o",
                roleTypeName: "@r"
            }
        } as any);
}
