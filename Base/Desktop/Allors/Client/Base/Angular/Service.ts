namespace Allors {
    export class Service {
        workspace: Workspace;

        static $inject = ["$http", "$q", "$rootScope"];
        constructor(private $http: ng.IHttpService, private $q: ng.IQService) {
            this.workspace = new Workspace(Data.metaPopulation);
        }

        createContext(name: string): Context {
            const session = new Session(this.workspace);
            return new Context(this, session, name, this.$q);
        }

        sync(name: string, params: any): ng.IPromise<Allors.Data.Response> {
            var defer = this.$q.defer();

            const serviceName = '/Angular/' + name;
            this.$http.post(serviceName, params)
                .then((response: ng.IHttpPromiseCallbackArg<Allors.Data.Response>) => {
                    var saveResponse = response.data;
                    defer.resolve(saveResponse);
                })
                .catch(e => {
                    throw e;
                });

            return defer.promise;
        }

        load(context: Context, response: Data.Response): ng.IPromise<Result> {

            var defer: ng.IDeferred<Result> = this.$q.defer();

            const requireLoadIds = this.workspace.diff(response);
            if (requireLoadIds.objects.length > 0) {

                this.$http.post('/Angular/Load', requireLoadIds)
                    .then(r => {
                        var loadResponse = <Data.LoadResponse>r.data;
                        this.workspace.load(loadResponse);
                        var result = this.buildResult(context, response);
                        defer.resolve(result);
                    })
                    .catch(e => {
                        throw e;
                    });

            } else {
                const result = this.buildResult(context, response);
                defer.resolve(result);
            }

            return defer.promise;
        }

        save(context: Context): ng.IPromise<Allors.Data.SaveResponse> {

            var defer = this.$q.defer();

            const saveRequest = context.session.save();
            this.$http.post('/Angular/Save', saveRequest)
                .then((response: ng.IHttpPromiseCallbackArg<Data.SaveResponse>) => {
                    var saveResponse = response.data;
                    if (saveResponse.hasErrors) {
                        defer.reject(saveResponse);
                    } else {
                        context.session.onSaved(<Data.SaveResponse>saveResponse);
                        defer.resolve(saveResponse);
                    }
                })
                .catch(e => { throw e; });

            return defer.promise;
        }

        invoke(method: Method): ng.IPromise<Data.SaveResponse> {
            var defer = this.$q.defer();
            const invokeRequest: Data.InvokeRequest = { i: method.object.id, v: method.object.version, m: method.name };
            this.$http.post('/Angular/Invoke', invokeRequest)
                .then((response: ng.IHttpPromiseCallbackArg<Data.SaveResponse>) => {
                    var invokeResponse = response.data;
                    if (invokeResponse.hasErrors) {
                        defer.reject(invokeResponse);
                    } else {
                        defer.resolve(invokeResponse);
                    }
                })
                .catch(e => { throw e; });

            return defer.promise;
        }

        invokeCustom(service: string, args: any): ng.IPromise<Data.SaveResponse> {
            var defer = this.$q.defer();
            this.$http.post(`/Angular/${service}`, args)
                .then((response: ng.IHttpPromiseCallbackArg<Data.ResponseError>) => {
                    var responseError = response.data;
                    if (responseError.hasErrors) {
                        defer.reject(responseError);
                    } else {
                        defer.resolve(responseError);
                    }
                })
                .catch(e => { throw e; });

            return defer.promise;
        }

        private buildResult(context: Context, response: Data.Response): Result {
            var session = context.session;

            var result = new Result();

            _.map(response.namedObjects, (v, k) => {
                result.objects[k] = session.get(v);
            });

            _.map(response.namedCollections, (v, k) => {
                result.collections[k] = _.map(v, (obj) => { return session.get(obj) });
            });

            _.map(response.namedValues, (v, k) => {
                result.values[k] = v;
            });

            return result;
        }

    }

    angular.module("allors", [])
        .service("allorsService",
        Service);
}