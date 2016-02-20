namespace Allors {
    export class IAllors {
        constructor(public context: Context, public events: Events, public commands: Commands) {
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

        get(id: string): ISessionObject {
            return this.context.session.get(id);
        }

        create(objectTypeName: string): ISessionObject {
            return this.context.session.create(objectTypeName);
        }

        // Events
        onRefresh(handler: () => void) {
            this.events.onRefresh(handler);
        }

        // Commands
        refresh(params?: any): ng.IPromise<any> {
            return this.context.refresh(params);
        }

        save(): ng.IPromise<any> {
            return this.commands.save();
        }

        invoke(method: Method): ng.IPromise<any> {
            return this.commands.invoke(method);
        }

        invokeWithSave(method: Method): ng.IPromise<any> {
            return this.commands.saveAndInvoke(method);
        }

        query(query: string, args: any): ng.IPromise<any> {
            return this.commands.query(query, args);
        }

        queryResults(query: string, args: any): ng.IPromise<any> {
            return this.commands.queryResults(query, args);
        }
    }
}