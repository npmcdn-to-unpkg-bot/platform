namespace Allors {
    export class Context {
        objects: { [name: string]: SessionObject; } = {};
        collections: { [name: string]: SessionObject[]; } = {};
        values: { [name: string]: any; } = {};

        constructor(
            public service: Service,
            public session: ISession,
            public name: string,
            private $q: ng.IQService) {
        }

        refresh(params?: any): ng.IPromise<any> {
            return this.service.sync(this.name, params)
                .then(response => {
                    return this.service.load(this, <Data.Response>response)
                        .then(result => {
                            this.session.reset();

                            this.objects = result.objects;
                            this.collections = result.collections;
                            this.values = result.values;
                        });
                });
        }

        query(service: string, params: any): ng.IPromise<Result> {
            return this.service.sync(service, params)
                .then(response => {
                    return this.service.load(this, <Data.Response>response);
                });
        }

        queryResults(service: string, params: any): ng.IPromise<any> {
            return this.query(service, params).then(response => {
                return response.collections["results"];
            });
        }

        save(): ng.IPromise<Data.SaveResponse> {
            return this.service.save(this);
        }

        invokeWithSave(method: Method, refresh: () => angular.IPromise<any>): ng.IPromise<Data.ResponseError> {
            var defer = this.$q.defer();

            this.save()
                .then(() => {
                    refresh()
                        .then(() => {
                            this.invoke(method, refresh)
                                .then(() => defer.resolve())
                                .catch(responseError => {
                                    defer.reject(responseError);
                                });
                        });
                })
                .catch(responseError => {
                    defer.reject(responseError);
                });

            return defer.promise;
        }

        invoke(method: Method, refresh?: () => angular.IPromise<any>): ng.IPromise<Data.ResponseError> {
            var defer = this.$q.defer();

            this.service.invoke(method)
                .then(() => {
                    if (refresh) {
                        refresh()
                            .then(() => {
                                defer.resolve();
                            });
                    } else {
                        defer.resolve();
                    }
                })
                .catch(responseError => {
                    defer.reject(responseError);
                });

            return defer.promise;
        }
    }
}