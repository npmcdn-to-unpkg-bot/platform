namespace Allors {
    export class Context {
        $q: ng.IQService;

        workspace: IWorkspace;
        session: ISession;

        objects: { [name: string]: ISessionObject; } = {};
        collections: { [name: string]: ISessionObject[]; } = {};
        values: { [name: string]: any; } = {};

        constructor(
            public name: string,
            public databaseService: DatabaseService,
            workspaceService: WorkspaceService) {

            this.$q = this.databaseService.$q;

            this.workspace = workspaceService.workspace;
            this.session = new Session(this.workspace);
        }

        refresh(params?: any): ng.IPromise<any> {
            return this.$q((resolve, reject) => {

                return this.databaseService.sync(this.name, params)
                    .then((response: Data.Response) => {
                        try {
                            const requireLoadIds = this.workspace.diff(response);

                            if (requireLoadIds.objects.length > 0) {
                                this.databaseService.load(requireLoadIds)
                                    .then((loadResponse: Data.LoadResponse) => {
                                        this.workspace.load(loadResponse);
                                        this.update(response);
                                        this.session.reset();
                                        resolve();
                                    })
                                    .catch(e2 => reject(e2));
                            } else {
                                this.update(response);
                                this.session.reset();
                                resolve();
                            }
                        } catch (e) {
                            reject(e);
                        }
                    })
                    .catch(e => reject(e));

            });
        }

        query(service: string, params: any): ng.IPromise<Result> {
            return this.$q((resolve, reject) => {

                this.databaseService.sync(service, params)
                    .then(v => {
                        try {
                            const response = v as Data.Response;
                            const requireLoadIds = this.workspace.diff(response);

                            if (requireLoadIds.objects.length > 0) {
                                this.databaseService.load(requireLoadIds)
                                    .then(u => {
                                        var loadResponse = u as Data.LoadResponse;
                                        this.workspace.load(loadResponse);
                                        const result = new Result(this.session, response);
                                        resolve(result);
                                    })
                                    .catch((e2) => reject(e2));
                            } else {
                                const result = new Result(this.session, response);
                                resolve(result);
                            }
                        } catch (e) {
                            reject(e);
                        }
                    })
                    .catch((e) => reject(e));

            });
        }

        save(): ng.IPromise<Data.SaveResponse> {
            return this.$q((resolve, reject) => {

                try {
                    const saveRequest = this.session.saveRequest();
                    this.databaseService.save(saveRequest)
                        .then((saveResponse: Data.SaveResponse) => {
                            resolve(saveResponse);
                        })
                        .catch((e2) => reject(e2));
                } catch (e) {
                    reject(e);
                }

            });
        }

        invoke(method: Method): ng.IPromise<Data.InvokeResponse> {
            return this.databaseService.invoke(method);
        }

        invokeCustom(service: string, args?: any): ng.IPromise<Data.InvokeResponse> {
            return this.databaseService.invokeCustom(service, args);
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