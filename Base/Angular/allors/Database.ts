module Allors {
    export class Database {
        private databaseObjectById: { [id: string]: DatabaseObject; } = {};

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