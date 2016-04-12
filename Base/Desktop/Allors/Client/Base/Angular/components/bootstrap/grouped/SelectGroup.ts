namespace Allors.Bootstrap {

    export class SelectGroupTemplate {
        static name = "allors/bootstrap/select-group";

        static createDefaultView() {
            return `
<b-group field="$ctrl">
    <b-label field="$ctrl"/> 
    <b-input-group field="$ctrl">
` + SelectTemplate.createDefaultView() + `
    </b-input-group>
</b-group>
`;
        }

        static register(templateCache: angular.ITemplateCacheService, view = SelectGroupTemplate.createDefaultView()) {
            templateCache.put(SelectGroupTemplate.name, view);
        }
    }

    class SelectGroupController extends Bootstrap.Field {

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
        .component("bSelectGroup", {
            controller: SelectGroupController,
            templateUrl: SelectGroupTemplate.name,
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
