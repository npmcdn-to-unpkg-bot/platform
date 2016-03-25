namespace Allors.Bootstrap {

    export class TinyMceTemplate {
        static name = "allors/bootstrap/tinymce";

        static view = 
`
<button class="btn btn-default pull-left" ng-if="$ctrl.canWrite && !$ctrl.editing" ng-click="$ctrl.editing = true"> 
    <span class="glyphicon glyphicon-pencil"></span>
</button>
<div class="well" ng-if="!$ctrl.editing" ng-bind-html="$ctrl.role"></div>
<div ng-if="$ctrl.editing">
    <textarea ui-tinymce="$ctrl.options" ng-model="$ctrl.role"></textarea>
</div>
`;

        static register(templateCache: angular.ITemplateCacheService) {
            templateCache.put(TinyMceTemplate.name, TinyMceTemplate.view);
        }
    }

    class TinyMce {
        constructor(public value: number, public name: string) {}
    }

    class TinyMceController extends Bootstrap.Field {
        options: any;
        editing = false;

        static $inject = ["$log", "$translate"];
        constructor($log: angular.ILogService, $translate: angular.translate.ITranslateService) {
            super($log, $translate);
        }
    }

    angular
        .module("allors")
        .component("bTinyMce", {
            controller: TinyMceController,
            templateUrl: TinyMceTemplate.name,
            require: {
                form: "^bForm"
            },
            bindings: {
                object: "<",
                relation: "@",
                options: "<"
            }
        } as any);
}
