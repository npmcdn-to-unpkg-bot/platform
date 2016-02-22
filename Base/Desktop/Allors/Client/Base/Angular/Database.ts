// TODO: rename to push pull and sync
namespace Allors {
    export class Database {
        constructor(private $http: ng.IHttpService, public $q: ng.IQService, public prefix: string) {
        }

        pull(name: string, params?: any): ng.IPromise<Data.PullResponse> {
            return this.$q((resolve, reject) => {

                const serviceName = this.prefix + name;
                this.$http.post(serviceName, params)
                    .then((callbackArg: ng.IHttpPromiseCallbackArg<Data.PullResponse>) => {
                        var response = callbackArg.data;
                        resolve(response);
                    })
                    .catch(e => {
                        reject(e);
                    });

            });
        }

        sync(syncRequest: Data.SyncRequest): ng.IPromise<Data.SyncResponse> {
            return this.$q((resolve, reject) => {

                this.$http.post(`${this.prefix}Sync`, syncRequest)
                    .then((callbackArg: ng.IHttpPromiseCallbackArg<Data.SyncResponse>) => {
                        var loadResponse = callbackArg.data;
                        resolve(loadResponse);
                    })
                    .catch(e => {
                        reject(e);
                    });

            });
        }

        push(pushRequest: Data.PushRequest): ng.IPromise<Data.PushResponse> {
            return this.$q((resolve, reject) => {

                this.$http.post(`${this.prefix}Push`, pushRequest)
                    .then((callbackArg: ng.IHttpPromiseCallbackArg<Data.PushResponse>) => {
                        var saveResponse = callbackArg.data;
                        resolve(saveResponse);
                })
                    .catch(e => {
                        reject(e);
                    });

            });
        }

        invoke(method: Method): ng.IPromise<Data.InvokeResponse>;
        invoke(service: string, args?: any): ng.IPromise<Data.InvokeResponse>;
        invoke(methodOrService: Method | string, args?: any): ng.IPromise<Data.InvokeResponse> {
            return this.$q((resolve, reject) => {

                if (methodOrService instanceof Method) {
                    const method = methodOrService;
                    const invokeRequest: Data.InvokeRequest = { i: method.object.id, v: method.object.version, m: method.name };
                    this.$http.post(`${this.prefix}Invoke`, invokeRequest)
                        .then((callbackArg: ng.IHttpPromiseCallbackArg<Data.InvokeResponse>) => {
                            var invokeResponse = callbackArg.data;
                            resolve(invokeResponse);
                        })
                        .catch(e => {
                            reject(e);
                        });
                }
                else {
                    const service = methodOrService;
                    this.$http.post(`${this.prefix}${service}`, args)
                        .then((callbackArg: ng.IHttpPromiseCallbackArg<Data.InvokeResponse>) => {
                            var invokeResponse = callbackArg.data;
                            resolve(invokeResponse);
                        })
                        .catch(e => {
                            reject(e);
                        });
                }

            });
        }
    }
}