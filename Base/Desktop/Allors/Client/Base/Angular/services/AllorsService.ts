namespace Allors {
    export interface IAllors {
        
    }

    class A  {
        constructor(public context: Context, public events: Events, public commands: Commands) {
        }

        // Context
        get objects(): { [name: string]: SessionObject; } {
            return this.context.objects;
        }

        get collections(): { [name: string]: SessionObject[]; } {
            return this.context.collections;
        }

        get values(): { [name: string]: any; } {
            return this.context.values;
        }

        get hasChanges(): boolean {
            return this.context.session.hasChanges;
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

        results(query: string, args: any): ng.IPromise<any> {
            return this.commands.results(query, args);
        }
    }

    export class AllorsServices {
        static $inject = ["databaseService", "workspaceService", "eventService"];
        constructor(
            public databaseService: DatabaseService,
            public workspaceService: WorkspaceService,
            public eventService: EventService) {
        }

        create(name, $scope, commandNotifier) {
            const context = new Context("Settings", this.databaseService, this.workspaceService);
            const events = new Events(context, this.eventService, $scope);
            const commands = new Commands(context, events, commandNotifier);
            return new A(context, events, commands);
        }
    }

    angular.module("allors")
        .service("allorsService",
        AllorsServices);
}