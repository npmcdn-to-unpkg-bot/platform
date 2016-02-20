namespace Allors {
    export interface IAllors {
        // Context
        objects: { [name: string]: ISessionObject; };
        collections: { [name: string]: ISessionObject[]; };
        values: { [name: string]: any; };
        hasChanges: boolean ;

        create(objectTypeName: string): ISessionObject;
 
        // Events
        onRefresh(handler: () => void);

        // Commands
        refresh(params ?: any): ng.IPromise<any>;
        save(): ng.IPromise<any>;
        invoke(method: Method): ng.IPromise<any>;
        invokeWithSave(method: Method): ng.IPromise<any>;
        invokeCustom(service: string, args ?: any): ng.IPromise<any>;
        invokeCustomWithSave(service: string, args ?: any): ng.IPromise<any>;
        query(query: string, args: any): ng.IPromise<any>;
        queryResults(query: string, args: any): ng.IPromise<any>;
    }

    class A implements IAllors {
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
            return this.commands.invokeWithSave(method);
        }

        invokeCustom(service: string, args?: any): ng.IPromise<any> {
            return this.commands.invokeCustom(service, args);
        }

        invokeCustomWithSave(service: string, args?: any): ng.IPromise<any> {
            return this.commands.invokeCustomWithSave(service, args);
        }

        query(query: string, args: any): ng.IPromise<any> {
            return this.commands.query(query, args);
        }

        queryResults(query: string, args: any): ng.IPromise<any> {
            return this.commands.queryResults(query, args);
        }
    }

    export class AllorsService {
        static $inject = ["databaseService", "workspaceService", "eventService"];
        constructor(
            public databaseService: DatabaseService,
            public workspaceService: WorkspaceService,
            public eventService: EventService) {
        }

        create(name, $scope, commandNotifier) {
            const context = new Context(name, this.databaseService, this.workspaceService);
            const events = new Events(context, this.eventService, $scope);
            const commands = new Commands(context, events, commandNotifier);
            return new A(context, events, commands);
        }
    }

    angular.module("allors")
        .service("allorsService",
        AllorsService);
}