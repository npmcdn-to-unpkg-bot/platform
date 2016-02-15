namespace Allors {
    export class Events {
        private static refreshEventName = "refresh";

        rootScope: angular.IRootScopeService;

        constructor(public context: Context, public eventService: EventService, public scope: ng.IScope) {
            this.rootScope = eventService.$rootScope;
        }

        on(eventName: string, handler: () => void) {
            this.scope.$on(eventName, handler);
        }

        onRefresh(handler: () => void) {
            this.on(Events.refreshEventName, handler);
        }

        broadcast(eventName: string) {
            this.rootScope.$broadcast(eventName, this.context.name);
        }

        broadcastRefresh() {
            this.broadcast(Events.refreshEventName);
        }
    }
}