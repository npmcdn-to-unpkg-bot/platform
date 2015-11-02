module App {

    interface IToManyScope extends ng.IScope {
        lookup: (criteria:string) => ng.IPromise<any>;

        select: (item: any) => void;
        startEdit: () => void;
        endEdit: () => void;
        remove: (item: any) => void;

        edit: boolean;
        
        obj: any;
        relation: string;
        display: string;
    }

    //one.$inject = [''];
    function toMany(): ng.IDirective {

        function link(scope: IToManyScope, element: ng.IAugmentedJQuery): void {
            scope.lookup = criteria => {
                return scope["parentLookup"]({ criteria: criteria });
            }

            scope.select = (item) => {
                var relation = scope.relation;
                scope.obj.add(relation, item);
                scope.endEdit();
            }

            scope.startEdit = () => {
                scope.edit = true;
            }

            scope.endEdit = () => {
                scope.edit = false;
            }

            scope.remove = (item) => {
                var relation = scope.relation;
                scope.obj.remove(relation, item);
                scope.endEdit();
            }
        }

        return <ng.IDirective>{
            restrict: "E",
            templateUrl: "app/common/directives/toMany/toMany.html",
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
        .directive('toMany', toMany);
}
