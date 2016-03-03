module App {

    interface IToOneScope extends ng.IScope {
        lookup: (criteria:string) => ng.IPromise<any>;

        select: (item: any) => void;
        startEdit: () => void;
        endEdit: () => void;

        edit: boolean;
        
        obj: any;
        relation: string;
        display: string;
    }

    //one.$inject = [''];
    function toOne(): ng.IDirective {

        function link(scope: IToOneScope, element: ng.IAugmentedJQuery): void {
            scope.lookup = criteria => {
                return scope["parentLookup"]({ criteria: criteria });
            }

            scope.select = (item) => {
                var relation = scope.relation;
                scope.obj[relation] = item;
                scope.endEdit();
            }

            scope.startEdit = () => {
                scope.edit = true;
            }

            scope.endEdit = () => {
                scope.edit = false;
            }
        }

        return <ng.IDirective>{
            restrict: "E",
            templateUrl: "app/common/directives/toOne/toOne.html",
            link: link,
            scope: {
                obj: "=",
                relation: "@",
                display: "@",
                parentLookup: "&lookup"
            }
        };
    }

    angular
        .module('app')
        .directive('toOne', toOne);
}
