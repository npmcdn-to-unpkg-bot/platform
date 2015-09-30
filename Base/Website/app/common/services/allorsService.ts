module App.Common.Services
{
    export class AllorsService {
        database : Allors.IDatabase;

        static $inject = ["$http", "$q","$rootScope"];
        constructor(private $http: ng.IHttpService, private $q: ng.IQService) {
            this.database = new Allors.Database(Allors.Meta.population);
        }

        public load(response: Allors.Data.Response): ng.IPromise<Allors.Context> {

            var defer = this.$q.defer();

            var requireLoadIds = this.database.check(response);
            if (requireLoadIds.objects.length > 0) {

                this.$http.post('/Angular/Load', requireLoadIds)
                    .then(
                        v => {
                            var loadResponse = <Allors.Data.LoadResponse>v.data;
                            this.database.load(loadResponse);

                            defer.resolve(this.createContext(response));
                    })
                    .catch(v => {
                        throw v;
                    });

            } else {
                defer.resolve(this.createContext(response));
            }

            return defer.promise;
        }

        private createContext(response: Allors.Data.Response): Allors.Context{
            var workspace = new Allors.Workspace(this.database);

            var namedObjects: { [name: string]: Allors.WorkspaceObject; } = {}
            _.map(response.namedObjects, (v,k) => {
                namedObjects[k] = workspace.get(v);
            });

            var namedCollections: { [name: string]: Allors.WorkspaceObject[]; } = {}
            _.map(response.namedCollections, (v, k) => {
                namedCollections[k] = _.map(v, (obj) => { return workspace.get(obj) });
            });

            var namedValues: { [name: string]: any; } = {}
            _.map(response.namedValues, (v, k) => {
                namedObjects[k] = v;
            });

            var context = new Allors.Context(workspace, namedObjects, namedCollections, namedValues);
            return context;
        }
    }

    angular
        .module("app")
        .service("allorsService",
        AllorsService);
}