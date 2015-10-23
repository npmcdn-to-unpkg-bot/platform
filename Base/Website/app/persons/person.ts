module App.Person {
    class PersonController{

        public root: Allors.Domain.Person;

        private context: Allors.Context;

        static $inject = ["$rootScope", "$scope", "$http", "allorsService"];
        constructor(private $rootScope: ng.IRootScopeService, private $scope: ng.IScope, private $http: ng.IHttpService, private allorsService: Allors.Service) {
            this.refresh();
            this.$scope.$on("refresh", () => { this.refresh() });
        }

        public method() {
            this.$http.post('/Angular/Invoke', <Allors.Data.InvokeRequest>{ i: this.root.id, v: this.root.version, m: "Method" })
                .then((response: ng.IHttpPromiseCallbackArg<Allors.Data.InvokeResponse>) => {
                    var executeResponse = response.data;
                })
                .catch(e => { throw e; });
        }

        public save(): void {
            var saveRequest = this.context.workspace.save();
            this.$http.post('/Angular/Save', { objects: saveRequest.objects })
                .then(saveResponse => {
                    this.$rootScope.$broadcast("refresh");
                })
                .catch(saveResponse => {
                    var errors = saveResponse;
                });
        }

        private refresh(): void {
            //this.$http.post('/Angular/Person', {}).then(response => {
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
        .controller("personController",
            PersonController);

}
