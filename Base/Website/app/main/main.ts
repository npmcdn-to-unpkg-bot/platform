module App.Main {
    interface IMainModel {
        root: Allors.Domain.Person;
    }
    
    class MainController {
        public root: Allors.Domain.Person;
        private context: Allors.Context;

        static $inject = ["$rootScope", "$scope", "$http", "allorsService"];
        constructor(private $rootScope: ng.IRootScopeService, private $scope: ng.IScope, private $http: ng.IHttpService, private allorsService: Allors.Service) {
            this.refresh();
            this.$scope.$on("refresh", () => { this.refresh() });
        }
        
        private refresh(): void{
            //this.$http.post('/Angular/Main', {}).then(response => {
            //    this.allorsService.load(<Allors.Data.Response>response.data)
            //        .then(context => {
            //            this.context = context;
            //            this.root = <Allors.Domain.Person>context.objects["root"];
            //        });
            //});
        }

    }
    angular
        .module("app")
        .controller("mainController",
            MainController);

}
