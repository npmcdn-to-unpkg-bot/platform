module App.Person {
    interface IPersonModel {
        root: Allors.Domain.Person;
    }

    interface IPersonState extends ng.ui.IState {
    }

    class PersonController implements IPersonModel {

        public root: Allors.Domain.Person;
        private context: Allors.Context;

        static $inject = ["$rootScope", "$scope", "$http", "allorsService"];
        constructor(private $rootScope: ng.IRootScopeService, private $scope: ng.IScope, private $http: ng.IHttpService, private allorsService: App.Common.Services.AllorsService) {
            this.refresh();
            this.$scope.$on("refresh", () => { this.refresh() });
        }

        public save(): void {
            var save = this.context.workspace.save();
            this.$http.post('/Angular/Save', { objects: save.objects}).then(response => {
                this.$rootScope.$broadcast("refresh");
            });
        }

        private refresh(): void {
            this.$http.post('/Angular/Person', {}).then(response => {
                this.allorsService.load(<Allors.Data.Response>response.data)
                    .then(context => {
                        this.context = context;
                        this.root = <Allors.Domain.Person>context.objectByName["root"];
                    });
            });
        }
    }
    angular
        .module("app")
        .controller("personController",
            PersonController);

}
