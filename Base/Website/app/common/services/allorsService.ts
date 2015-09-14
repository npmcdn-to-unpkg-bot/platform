module App.Common.Services
{
    export class AllorsService {
        database : Allors.Database;

        static $inject = ["$http", "$q","$rootScope"];
        constructor(private $http: ng.IHttpService, private $q: ng.IQService) {
            this.database = new Allors.Database(Allors.Meta.population);
        }

        public load(response: Allors.Data.Response): ng.IPromise<Allors.Context> {

            var defer = this.$q.defer();

            var requireLoadIds = this.database.check(response);
            if (requireLoadIds.objects.length > 0) {

                this.$http.post('/Angular/Load', requireLoadIds).then(
                    x => {
                        var loadResponse = <Allors.Data.LoadResponse>x.data;
                        this.database.load(loadResponse);

                        defer.resolve(this.createContext(response));

                    });

            } else {
                defer.resolve(this.createContext(response));
            }

            return defer.promise;
        }

        private createContext(response: Allors.Data.Response): Allors.Context{
            var workspace = new Allors.Workspace(this.database);

            var objectByName: { [name: string]: Allors.WorkspaceObject; } = {}
            _.map(response.namedObjects, v => {
                objectByName[v[0]] = workspace.get(v[1]);
            });

            return new Allors.Context(workspace, objectByName);
        }
    }

    angular
        .module("app")
        .service("allorsService",
        AllorsService);
}