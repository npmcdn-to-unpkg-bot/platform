namespace Allors {
    export class DatabaseService {
        prefix = "/Angular/";

        static $inject = ["$http", "$q"];
        constructor(private $http: ng.IHttpService, public $q: ng.IQService) {
        }

        sync(name: string, params?: any): ng.IPromise<Data.Response> {
            var defer = this.$q.defer();

            const serviceName =  this.prefix + name;
            this.$http.post(serviceName, params)
                .then((response: ng.IHttpPromiseCallbackArg<Data.Response>) => {
                    var saveResponse = response.data;
                    defer.resolve(saveResponse);
                })
                .catch(e => {
                    throw Error(String(e));
                });

            return defer.promise;
        }

        load(loadRequest: Data.LoadRequest): ng.IPromise<Data.LoadResponse> {

            const defer: ng.IDeferred<Data.LoadResponse> = this.$q.defer();

            this.$http.post(`${this.prefix}Load`, loadRequest)
                .then(r => {
                    var loadResponse = r.data as Data.LoadResponse;
                    defer.resolve(loadResponse);
                })
                .catch(e => {
                    throw Error(String(e));
                });

            return defer.promise;
        }

        save(saveRequest: Data.SaveRequest): ng.IPromise<Data.SaveResponse> {

            const defer: ng.IDeferred<Data.SaveResponse> = this.$q.defer();

            this.$http.post(`${this.prefix}Save`, saveRequest)
                .then((response: ng.IHttpPromiseCallbackArg<Data.SaveResponse>) => {
                    var saveResponse = response.data;
                    defer.resolve(saveResponse);
                })
                .catch(e => {
                    throw Error(String(e));
                });

            return defer.promise;
        }

        invoke(method: Method): ng.IPromise<Data.SaveResponse> {
            var defer = this.$q.defer();
            const invokeRequest: Data.InvokeRequest = { i: method.object.id, v: method.object.version, m: method.name };
            this.$http.post(`${this.prefix}Invoke`, invokeRequest)
                .then((response: ng.IHttpPromiseCallbackArg<Data.SaveResponse>) => {
                    var invokeResponse = response.data;
                    if (invokeResponse.hasErrors) {
                        defer.reject(invokeResponse);
                    } else {
                        defer.resolve(invokeResponse);
                    }
                })
                .catch(e => {
                    throw Error(String(e));
                });

            return defer.promise;
        }

        invokeCustom(service: string, args: any): ng.IPromise<Data.SaveResponse> {
            var defer = this.$q.defer();
            this.$http.post(`${this.prefix}${service}`, args)
                .then((response: ng.IHttpPromiseCallbackArg<Data.ResponseError>) => {
                    var responseError = response.data;
                    if (responseError.hasErrors) {
                        defer.reject(responseError);
                    } else {
                        defer.resolve(responseError);
                    }
                })
                .catch(e => {
                    throw Error(String(e));
                });

            return defer.promise;
        }
    }

    angular.module("allors")
        .service("databaseService",
        DatabaseService);
}