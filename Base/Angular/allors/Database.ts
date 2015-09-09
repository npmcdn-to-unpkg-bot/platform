module Allors {
    export class Database {
        private objectTypeByName: { [id: string]: ObjectType; };
        private databaseObjectById: { [id: string]: DatabaseObject; } = {};

        constructor(objectTypeByName: { [id: string]: ObjectType; }) {
            this.objectTypeByName = objectTypeByName;
        }

        update(updates: any[]): void {
            for (var i = 0; i < updates.length; i++) {
                var update = updates[i];
                var objectType = this.objectTypeByName[update.objectType];

                var objects : any[] = update.objects;
                for (var j = 0; j < objects.length; j++) {
                    var object = objects[j];
                    object.database = this;
                    object.objectType = objectType;

                    this.databaseObjectById[object.id] = object;
                }
            }
        }
        
        get(id: string): DatabaseObject {
            return this.databaseObjectById[id];
        }
}
}