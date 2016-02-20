namespace Allors {
    export class Commands 
    {
        constructor(public context: Context, public events: Events) {
        }

        save(): ng.IPromise<any> {
            return this.context
                .save()
                .finally(() => this.events.broadcastRefresh())
                .catch((e) => this.handleError(e));
        }

        invoke(method: Method): ng.IPromise<Data.InvokeResponse>;
        invoke(service: string, args?: any): ng.IPromise<any>;
        invoke(methodOrService: Method | string, args?: any): ng.IPromise<any> {
            
            if (methodOrService instanceof Method) {
                return this.context
                    .invoke(methodOrService)
                    .finally(() => this.events.broadcastRefresh())
                    .catch((e) => this.handleError(e));
            } else {
                return this.context
                    .invoke(methodOrService as string, args)
                    .finally(() => this.events.broadcastRefresh())
                    .catch((e) => this.handleError(e));
            }
        }

        saveAndInvoke(method: Method): ng.IPromise<Data.InvokeResponse>;
        saveAndInvoke(service: string, args?: any): ng.IPromise<any>;
        saveAndInvoke(methodOrService: Method | string, args?: any): ng.IPromise<any> {

            if (methodOrService instanceof Method) {
                return this.context
                    .save()
                    .then(() => this.context.invoke(methodOrService))
                    .finally(() => this.events.broadcastRefresh())
                    .catch((e) => this.handleError(e));
            } else {
                return this.context
                    .save()
                    .then(() => this.context.invoke(methodOrService as string, args))
                    .finally(() => this.events.broadcastRefresh())
                    .catch((e) => this.handleError(e));
            }
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

        private handleError(e: any): any {
            if (e.hasErrors) {
                return e;
            } else {
                throw e;
            }
        }
    }
}
