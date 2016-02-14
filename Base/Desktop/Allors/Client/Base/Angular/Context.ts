namespace Allors {
    export class Context {
        $q: ng.IQService;

        busy: boolean;

        workspace: IWorkspace;
        session: ISession;

        objects: { [name: string]: SessionObject; } = {};
        collections: { [name: string]: SessionObject[]; } = {};
        values: { [name: string]: any; } = {};

        constructor(
            public name: string,
            public databaseService: DatabaseService,
            workspaceService: WorkspaceService) {

            this.$q = this.databaseService.$q;

            this.workspace = workspaceService.workspace;
            this.session = new Session(this.workspace);

            this.busy = false;
        }

        refresh(params?: any): ng.IPromise<any> {
            this.assertNotBusy();

            const defer: ng.IDeferred<any> = this.$q.defer();

            this.databaseService.sync(this.name, params)
                .then((response: Data.Response) => {
                    const requireLoadIds = this.workspace.diff(response);

                    if (requireLoadIds.objects.length > 0) {
                        this.databaseService.load(requireLoadIds)
                            .then((loadResponse: Data.LoadResponse) => {

                                this.workspace.load(loadResponse);
                                this.update(response);
                                this.session.reset();
                                defer.resolve();
                            });
                    } else {
                        this.update(response);
                        this.session.reset();
                        defer.resolve();
                    }
                })
                .catch(e => {
                    throw new Error(String(e));
                });

            return defer.promise;
        }

        query(service: string, params: any): ng.IPromise<Result> {
            this.assertNotBusy();
            
            const defer: ng.IDeferred<any> = this.$q.defer();

            this.databaseService.sync(this.name, params)
                .then(v => {
                    const response = v as Data.Response;
                    const requireLoadIds = this.workspace.diff(response);

                    if (requireLoadIds.objects.length > 0) {
                        this.databaseService.load(requireLoadIds)
                            .then(u => {
                                var loadResponse = u as Data.LoadResponse;

                                this.workspace.load(loadResponse);

                                const result = new Result(this.session, response);
                                defer.resolve(result);
                            });
                    } else {
                        const result = new Result(this.session, response);
                        defer.resolve(result);
                    }
                })
                .catch(e => {
                    throw new Error(String(e));
                });

            return defer.promise;
        }

        save(): ng.IPromise<Data.SaveResponse> {
            this.assertNotBusy();

            const defer = this.$q.defer();

            const saveRequest = this.session.saveRequest();
            this.databaseService.save(saveRequest)
                .then((saveResponse: Data.SaveResponse) => {
                    if (!saveResponse.hasErrors) {
                        this.session.saveResponse(saveResponse as Data.SaveResponse);
                    }

                    defer.resolve(saveResponse);
               })
                .catch(e => {
                    throw new Error(String(e));
                });

            return defer.promise;
        }

        invoke(method: Method): ng.IPromise<Data.ResponseError> {
            this.assertNotBusy();

            const defer = this.$q.defer();

            this.databaseService.invoke(method)
                .then((invokeResponse: Data.InvokeResponse) => {
                    defer.resolve(invokeResponse);
                })
                .catch(e => {
                    throw new Error(String(e));
                });

            return defer.promise;
        }

        invokeCustom(service: string, args?: any): ng.IPromise<Data.ResponseError> {
            this.assertNotBusy();

            const defer = this.$q.defer();

            this.databaseService.invokeCustom(service, args)
                .then((invokeResponse: Data.InvokeResponse) => {
                    defer.resolve(invokeResponse);
                })
                .catch(e => {
                    throw new Error(String(e));
                });

            return defer.promise;
        }

        finalize(promise: ng.IPromise<any>): ng.IPromise<any> {
            this.busy = true;

            const defer = this.$q.defer();

            promise
                .then((success) => {
                    this.busy = false;
                    defer.resolve(success);
                }, (error) => {
                    this.busy = false;
                    defer.reject(error);
                });

            return defer.promise;
        }

        private update(response: Data.Response): void {

            _.map(response.namedObjects, (v, k) => {
                this.objects[k] = this.session.get(v);
            });

            _.map(response.namedCollections, (v, k) => {
                this.collections[k] = _.map(v, (obj) => { return this.session.get(obj) });
            });

            _.map(response.namedValues, (v, k) => {
                this.values[k] = v;
            });
        }

        private assertNotBusy() {
            if (this.busy) {
                throw new Error("Context is busy");
            }
        }
    }
}