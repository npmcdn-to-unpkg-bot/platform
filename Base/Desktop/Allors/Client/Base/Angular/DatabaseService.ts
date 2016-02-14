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
                .then((callbackArg: ng.IHttpPromiseCallbackArg<Data.Response>) => {
                    var response = callbackArg.data;
                    defer.resolve(response);
                })
                .catch(e => {
                    throw new Error(String(e));
                });

            return defer.promise;
        }

        load(loadRequest: Data.LoadRequest): ng.IPromise<Data.LoadResponse> {

            const defer: ng.IDeferred<Data.LoadResponse> = this.$q.defer();

            this.$http.post(`${this.prefix}Load`, loadRequest)
                .then((callbackArg: ng.IHttpPromiseCallbackArg<Data.LoadResponse>) => {
                    var loadResponse = callbackArg.data;
                    defer.resolve(loadResponse);
                })
                .catch(e => {
                    throw new Error(String(e));
                });

            return defer.promise;
        }

        save(saveRequest: Data.SaveRequest): ng.IPromise<Data.SaveResponse> {

            const defer: ng.IDeferred<Data.SaveResponse> = this.$q.defer();

            this.$http.post(`${this.prefix}Save`, saveRequest)
                .then((callbackArg: ng.IHttpPromiseCallbackArg<Data.SaveResponse>) => {
                    var saveRespons = callbackArg.data;
                    defer.resolve(saveRespons);
                })
                .catch(e => {
                    throw new Error(String(e));
                });

            return defer.promise;
        }

        invoke(method: Method): ng.IPromise<Data.InvokeResponse> {
            var defer = this.$q.defer();
            const invokeRequest: Data.InvokeRequest = { i: method.object.id, v: method.object.version, m: method.name };
            this.$http.post(`${this.prefix}Invoke`, invokeRequest)
                .then((callbackArg: ng.IHttpPromiseCallbackArg<Data.InvokeResponse>) => {
                    var invokeResponse = callbackArg.data;
                    defer.resolve(invokeResponse);
                })
                .catch(e => {
                    throw new Error(String(e));
                });

            return defer.promise;
        }

        invokeCustom(service: string, args?: any): ng.IPromise<Data.InvokeResponse> {
            var defer = this.$q.defer();
            this.$http.post(`${this.prefix}${service}`, args)
                .then((callbackArg: ng.IHttpPromiseCallbackArg<Data.InvokeResponse>) => {
                    var invokeResponse = callbackArg.data;
                    defer.resolve(invokeResponse);
                })
                .catch(e => {
                    throw new Error(String(e));
                });

            return defer.promise;
        }
    }

    angular.module("allors")
        .service("databaseService",
        DatabaseService);
}