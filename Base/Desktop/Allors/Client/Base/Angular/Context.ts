namespace Allors {
    export class Context {
        $q: ng.IQService;

        workspace: IWorkspace;
        session: ISession;

        objects: { [name: string]: SessionObject; } = {};
        collections: { [name: string]: SessionObject[]; } = {};
        values: { [name: string]: any; } = {};

        constructor(
            public name: string,
            private databaseService: DatabaseService,
            private workspaceService: WorkspaceService) {

            this.$q = this.databaseService.$q;

            this.workspace = workspaceService.workspace;
            this.session = new Session(this.workspace);
        }

        refresh(params?: any): ng.IPromise<any> {
            const defer: ng.IDeferred<any> = this.$q.defer();

            this.databaseService.sync(this.name, params)
                .then((response: Data.Response) => {
                    const requireLoadIds = this.workspace.diff(response);

                    if (requireLoadIds.objects.length > 0) {
                        this.databaseService.load(requireLoadIds)
                            .then(u => {
                                var loadResponse = u as Data.LoadResponse;

                                this.workspace.load(loadResponse);
                                this.update(response);
                                this.session.reset();
                                defer.resolve();
                            })
                            .catch(e => {
                                throw Error(String(e));
                            });
                    } else {
                        this.update(response);
                        this.session.reset();
                        defer.resolve();
                    }
                })
                .catch(e => {
                    throw Error(String(e));
                });

            return defer.promise;
        }

        query(service: string, params: any): ng.IPromise<Result> {
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
                            })
                            .catch(e => {
                                throw Error(String(e));
                            });
                    } else {
                        const result = new Result(this.session, response);
                        defer.resolve(result);
                    }
                })
                .catch(e => {
                    throw Error(String(e));
                });

            return defer.promise;
        }

        save(): ng.IPromise<Data.SaveResponse> {
            var defer = this.$q.defer();

            const saveRequest = this.session.save();
            this.databaseService.save(saveRequest)
                .then((saveResponse: Data.SaveResponse) => {
                    if (saveResponse.hasErrors) {
                        defer.reject(saveResponse);
                    }
                    else
                    {
                        this.session.onSaved(saveResponse as Data.SaveResponse);
                        defer.resolve(saveResponse);
                    }
                })
                .catch(e => {
                    throw Error(String(e));
                });

            return defer.promise;
        }

        invoke(method: Method, refresh?: () => angular.IPromise<any>): ng.IPromise<Data.ResponseError> {
            var defer = this.$q.defer();

            this.databaseService.invoke(method)
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
    }
}