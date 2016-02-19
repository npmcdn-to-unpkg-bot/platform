namespace Allors
{
	export class Commands 
    {
        constructor(public context: Context, public events: Events, public commandNotifier: ICommandNotifier) {
        }

        save(): ng.IPromise<any> {
            return this.context.save()
                .then((response) => this.commandNotifier.save(response as Data.SaveResponse))
                .finally(() => this.events.broadcastRefresh())
                .catch((e) => { throw new Error(String(e)) });
        }

        invoke(method: Method): ng.IPromise<any> {
            return this.context.invoke(method)
                .then((response) => this.commandNotifier.invoke(response as Data.InvokeResponse))
                .finally(() => this.events.broadcastRefresh())
                .catch((e) => {
                    throw new Error(String(e));
                });
        }

        invokeWithSave(method: Method): ng.IPromise<any> {
            return this.context.save()
                .then((response) => this.commandNotifier.save(response as Data.SaveResponse))
                .then(() => this.context.invoke(method))
                .then((response) => this.commandNotifier.invoke(response as Data.InvokeResponse))
                .finally(() => this.events.broadcastRefresh())
                .catch((e) => { throw new Error(String(e)) });
        }

        invokeCustom(service: string, args?: any): ng.IPromise<any> {
            return this.context.invokeCustom(service, args)
                .then((response) => this.commandNotifier.invoke(response as Data.InvokeResponse))
                .finally(() => this.events.broadcastRefresh())
                .catch((e) => {
                    throw new Error(String(e));
                });
        }

        invokeCustomWithSave(service: string, args?: any): ng.IPromise<any> {
            return this.context.save()
                .then((response) => this.commandNotifier.save(response as Data.SaveResponse))
                .then(() => this.context.invokeCustom(service, args))
                .then((response) => this.commandNotifier.invoke(response as Data.InvokeResponse))
                .finally(() => this.events.broadcastRefresh())
                .catch((e) => { throw new Error(String(e)) });
        }

        query(query: string, args: any): ng.IPromise<any> {
            return this.context.query(query, args);
        }

        queryResults(query: string, args: any): ng.IPromise<any> {
            return this.context.query(query, args)
                .then(result => {
                    var results = result.collections["results"];
                    return results;
                });
        }
    }
}
