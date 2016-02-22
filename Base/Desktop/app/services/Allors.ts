namespace App {
    export class AllorsService {
        database: Allors.Database;
        workspace: Allors.Workspace;

        static $inject = ["$http", "$q", "$rootScope", "toastr"];
        constructor(public $http: ng.IHttpService, public $q: ng.IQService, public $rootScope: ng.IRootScopeService, public toastr: angular.toastr.IToastrService) {
            const prefix = "/Angular/";
            this.database = new Allors.Database(this.$http, this.$q, prefix);
            this.workspace = new Allors.Workspace(Allors.Data.metaPopulation);
        }
    }
    angular.module("app")
        .service("allorsService",
        AllorsService);
}