namespace Allors {
    export abstract class Control {
        context: Context;
        events: Events;

        constructor(name: string, database: Database, workspace: Workspace, $rootScope: ng.IRootScopeService, protected $scope: ng.IScope, protected $q: ng.IQService) {
            this.context = new Context(name, database, workspace);
            this.events = new Events(this.context, $rootScope, $scope);

            this.events.onRefresh(() => this.refresh());
        }

        // Context
        get objects(): { [name: string]: ISessionObject; } {
            return this.context.objects;
        }

        get collections(): { [name: string]: ISessionObject[]; } {
            return this.context.collections;
        }

        get values(): { [name: string]: any; } {
            return this.context.values;
        }

        get hasChanges(): boolean {
            return this.context.session.hasChanges;
        }

        create(objectTypeName: string) {
            return this.context.session.create(objectTypeName);
        }
        
        // Commands
        load(params?: any): ng.IPromise<any> {
            return this.context.load(params);
        }

        save(): ng.IPromise<any> {
            return this.$q((resolve, reject) => {

                this.context
                    .save()
                    .then((saveResponse: Data.SaveResponse) => {
                        if (saveResponse.hasErrors) {
                            reject(new SaveError(this.context, saveResponse));
                        } else {
                            resolve(saveResponse);
                        }
                    }, (e) => {
                        throw e;
                    })
                    .finally(() => this.events.broadcastRefresh());

            });
        }

        invoke(method: Method): ng.IPromise<Data.InvokeResponse>;
        invoke(service: string, args?: any): ng.IPromise<any>;
        invoke(methodOrService: Method | string, args?: any): ng.IPromise<any> {
            return this.$q((resolve, reject) => {

                if (methodOrService instanceof Method) {
                    return this.context
                        .invoke(methodOrService)
                        .then((invokeResponse: Data.InvokeResponse) => {
                            if (invokeResponse.hasErrors) {
                                reject(new InvokeError(this.context, invokeResponse));
                            } else {
                                resolve(invokeResponse);
                            }
                        }, (e) => {
                            throw e;
                        })
                        .finally(() => this.events.broadcastRefresh());
                } else {
                    return this.context
                        .invoke(methodOrService as string, args)
                        .then((invokeResponse: Data.InvokeResponse) => {
                            if (invokeResponse.hasErrors) {
                                reject(new InvokeError(this.context, invokeResponse));
                            } else {
                                resolve(invokeResponse);
                            }
                        }, (e) => {
                            throw e;
                        })
                        .finally(() => this.events.broadcastRefresh());
                }

            });
        }

        saveAndInvoke(method: Method): ng.IPromise<Data.InvokeResponse>;
        saveAndInvoke(service: string, args?: any): ng.IPromise<any>;
        saveAndInvoke(methodOrService: Method | string, args?: any): ng.IPromise<any> {
            return this.$q((resolve, reject) => {

                var save = this.context
                    .save()
                    .then((saveResponse: Data.SaveResponse) => {
                        if (saveResponse.hasErrors) {
                            reject(new SaveError(this.context, saveResponse));
                        } else {
                            resolve(saveResponse);
                        }
                    }, (e) => {
                        throw e;
                    });

                if (methodOrService instanceof Method) {
                    return save
                        .then(() => this.context.invoke(methodOrService)
                            .then((invokeResponse: Data.InvokeResponse) => {
                                if (invokeResponse.hasErrors) {
                                    reject(new InvokeError(this.context, invokeResponse));
                                } else {
                                    resolve(invokeResponse);
                                }
                            }, (e) => {
                                throw e;
                            })
                        )
                        .finally(() => this.events.broadcastRefresh());
                } else {
                    return save
                        .then(() => this.context.invoke(methodOrService as string, args)
                            .then((invokeResponse: Data.InvokeResponse) => {
                                if (invokeResponse.hasErrors) {
                                    reject(new InvokeError(this.context, invokeResponse));
                                } else {
                                    resolve(invokeResponse);
                                }
                            }, (e) => {
                                throw e;
                            })
                        )
                        .finally(() => this.events.broadcastRefresh());
                }

            });
        }

        query(query: string, args: any): ng.IPromise<any> {
            return this.context
                .query(query, args)
                .catch((e) => { throw e; });
        }

        queryResults(query: string, args: any): ng.IPromise<any> {
            return this.context
                .query(query, args)
                .then(result => {
                    var results = result.collections["results"];
                    return results;
                })
                .catch((e) => { throw e; });
        }

        protected abstract refresh(): ng.IPromise<any>;

        private handleError(e: any): any {
            if (e.hasErrors) {
                return e;
            } else {
                throw e;
            }
        }
    }
}