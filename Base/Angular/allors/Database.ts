module Allors {
    export class Database {
        objectTypeByName: { [name: string]: ObjectType; } = {};
        private databaseObjectById: { [id: string]: DatabaseObject; } = {};

        constructor(data: Data.MetaData) {
            _.forEach(data.classes, objectTypeData => {
                var objectType = new ObjectType(objectTypeData);
                this.objectTypeByName[objectType.name] = objectType;
            });
        }

        load(data: Data.LoadData): void {
            _.forEach(data.objects, objectData => {
                var databaseObject = new DatabaseObject(this, objectData);
                this.databaseObjectById[databaseObject.id] = databaseObject;
            });
        }

        get(id: string): DatabaseObject {
            return this.databaseObjectById[id];
        }
}
}