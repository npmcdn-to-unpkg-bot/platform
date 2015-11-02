module App.Layout {
    class LayoutController {

        public user: Allors.Domain.Person;

        private context: Allors.Context;

        get showTest(): boolean {
            if (this.user && this.user.UserName.toLowerCase() === "administrator") {
                return true;
            }

            return false;
        }

        static $inject = ["$rootScope", "$scope", "$http", "$log", "allorsService"];
        constructor(private $rootScope: ng.IRootScopeService, private $scope: ng.IScope, private $http: ng.IHttpService, private $log: ng.ILogService, private service: Allors.Service) {
            this.context = service.createContext("Main");

            this.refresh(true)
                .then(() => {
                    this.$scope.$on("refresh", (event, args) => {
                        this.refresh(args !== this.context.name);
                    });
                });
        }
        
        private refresh(init: boolean): ng.IPromise<any> {
            return this.context.refresh()
                .then(() => {
                    this.user = <Allors.Domain.Person>this.context.objects["user"];
                });
        }

    }
    angular
        .module("app")
        .controller("layoutController",
        LayoutController);

}
