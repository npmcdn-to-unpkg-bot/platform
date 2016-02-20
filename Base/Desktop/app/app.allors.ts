namespace App {
    export class AllorsService {
        database: Allors.Database;
        workspace: Allors.Workspace;

        static $inject = ["$http", "$q", "$rootScope"];
        constructor(public $http: ng.IHttpService, public $q: ng.IQService, public $rootScope: ng.IRootScopeService) {
            const prefix = "/Angular/";
            this.database = new Allors.Database(this.$http, this.$q, prefix);
            this.workspace = new Allors.Workspace(Allors.Data.metaPopulation);
        }

        create(name, $scope, commandNotifier): Allors.IAllors {
            const context = new Allors.Context(name, this.database, this.workspace);
            const events = new Allors.Events(context, this.$rootScope, $scope);
            const commands = new Allors.Commands(context, events);
            return new Allors.IAllors(context, events, commands);
        }
    }
    angular.module("app")
        .service("allorsService",
        AllorsService);
}