namespace Allors {
    export class DatabaseService {
        prefix = "/Angular/";

        static $inject = ["$http", "$q"];
        constructor(private $http: ng.IHttpService, public $q: ng.IQService) {
        }

        sync(name: string, params?: any): ng.IPromise<Data.Response> {
            return this.$q((resolve, reject) => {

                const serviceName = this.prefix + name;
                this.$http.post(serviceName, params)
                    .then((callbackArg: ng.IHttpPromiseCallbackArg<Data.Response>) => {
                        var response = callbackArg.data;
                        resolve(response);
                    })
                    .catch(e => {
                        reject(e);
                    });

            });
        }

        load(loadRequest: Data.LoadRequest): ng.IPromise<Data.LoadResponse> {
            return this.$q((resolve, reject) => {

                this.$http.post(`${this.prefix}Load`, loadRequest)
                    .then((callbackArg: ng.IHttpPromiseCallbackArg<Data.LoadResponse>) => {
                        var loadResponse = callbackArg.data;
                        resolve(loadResponse);
                    })
                    .catch(e => {
                        reject(e);
                    });

            });
        }

        save(saveRequest: Data.SaveRequest): ng.IPromise<Data.SaveResponse> {
            return this.$q((resolve, reject) => {

                this.$http.post(`${this.prefix}Save`, saveRequest)
                    .then((callbackArg: ng.IHttpPromiseCallbackArg<Data.SaveResponse>) => {
                        var saveResponse = callbackArg.data;
                        if (saveResponse.hasErrors) {
                            reject(new SaveError(saveResponse));
                        } else {
                            resolve(saveResponse);
                        }
                    })
                    .catch(e => {
                        reject(e);
                    });

            });
        }

        invoke(method: Method): ng.IPromise<Data.InvokeResponse> {
            return this.$q((resolve, reject) => {

                const invokeRequest: Data.InvokeRequest = { i: method.object.id, v: method.object.version, m: method.name };
                this.$http.post(`${this.prefix}Invoke`, invokeRequest)
                    .then((callbackArg: ng.IHttpPromiseCallbackArg<Data.InvokeResponse>) => {
                        var invokeResponse = callbackArg.data;
                        if (invokeResponse.hasErrors) {
                            reject(new InvokeError(invokeResponse));
                        } else {
                            resolve(invokeResponse);
                        }
                    })
                    .catch(e => {
                        reject(e);
                    });

            });
        }

        invokeCustom(service: string, args?: any): ng.IPromise<Data.InvokeResponse> {
            return this.$q((resolve, reject) => {

            this.$http.post(`${this.prefix}${service}`, args)
                .then((callbackArg: ng.IHttpPromiseCallbackArg<Data.InvokeResponse>) => {
                    var invokeResponse = callbackArg.data;
                    if (invokeResponse.hasErrors) {
                        reject(new InvokeError(invokeResponse));
                    } else {
                        resolve(invokeResponse);
                    }
                })
                .catch(e => {
                    reject(e);
                });

            });
        }
    }

    angular.module("allors")
        .service("databaseService",
        DatabaseService);
}