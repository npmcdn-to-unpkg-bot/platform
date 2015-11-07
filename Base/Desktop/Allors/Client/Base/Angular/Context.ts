module Allors {
    export class Context {
        objects: { [name: string]: Allors.WorkspaceObject; } = {};
        collections: { [name: string]: Allors.WorkspaceObject[]; } = {};
        values: { [name: string]: any; } = {};

        constructor(
            public service: Allors.Service,
            public workspace: Allors.IWorkspace,
            public name: string,
            private $q: ng.IQService) {
        }

        refresh(params?: any): ng.IPromise<any> {
            return this.service.query(this.name, params)
                .then(response => {
                    return this.service.load(this, response)
                        .then(result => {
                            this.workspace.sync();

                            this.objects = result.objects;
                            this.collections = result.collections;
                            this.values = result.values;
                        });
                });
        }

        query(service: string, params: any): ng.IPromise<Result> {
            return this.service.query(service, params)
                .then(response => {
                    return this.service.load(this, response);
                });
        }

        queryResults(service: string, params: any): ng.IPromise<any> {
            return this.query(service, params).then(response => {
                return response.collections["results"];
            });
        }

        save(): ng.IPromise<Allors.Data.SaveResponse> {
            return this.service.save(this);
        }

        invokeWithSave(method: Allors.Method, refresh: () => angular.IPromise<any>): ng.IPromise<Allors.Data.ResponseError> {
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

        invoke(method: Allors.Method, refresh?: () => angular.IPromise<any>): ng.IPromise<Allors.Data.ResponseError> {
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