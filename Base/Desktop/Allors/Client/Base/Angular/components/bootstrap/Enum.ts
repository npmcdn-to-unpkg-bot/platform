namespace Allors.Bootstrap {

    export class EnumTemplate {
        static name = "allors/bootstrap/enum";

        static view = 
`
<select class="form-control" 
            ng-model="$ctrl.role" 
            ng-disabled="!$ctrl.canWrite" 
            ng-required="$ctrl.roleType.isRequired"
            ng-options="enum.value as enum.name for enum in $ctrl.enums">
    <option ng-if="!$ctrl.roleType.isRequired" value=""></option>     
</select>
`;

        static register(templateCache: angular.ITemplateCacheService) {
            templateCache.put(EnumTemplate.name, EnumTemplate.view);
        }
    }

    class Enum {
        constructor(public value: number, public name: string) {}
    }

    class EnumController extends Bootstrap.Field {

        fullTypeName: string;

        enums: Enum[];

        static $inject = ["$log", "$translate"];
        constructor($log: angular.ILogService, $translate: angular.translate.ITranslateService) {
            super($log, $translate);
            
            const type = eval(this.fullTypeName);
            if (type) {
                const lastIndex = this.fullTypeName.lastIndexOf(".");
                const typeName = this.fullTypeName.substr(lastIndex + 1);

                this.enums = [];
                for (let k in type) {
                    if (type.hasOwnProperty(k)) {
                        const value = type[k];
                        if (typeof value === "number") {

                            const name = type[value];
                            const humanizedName = Filters.Humanize.filter(name);

                            const enumeration = new Enum(value, humanizedName);
                            this.enums.push(enumeration);

                            ((enumeration, key1, key2) => {
                                this.translate(key1, key2, (translatedName) => {
                                    if (translatedName) {
                                        enumeration.name = translatedName;
                                    }
                                });
                            })(enumeration, `enum_${typeName}_${value}`, `enum_${typeName}_${name}`);

                        }
                    }
                }
            } 
        }
    }

    angular
        .module("allors")
        .component("bEnum", {
            controller: EnumController,
            templateUrl: EnumTemplate.name,
            require: {
                form: "^bForm"
            },
            bindings: {
                object: "<",
                relation: "@",
                fullTypeName: "@enum"
            }
        } as any);
}
