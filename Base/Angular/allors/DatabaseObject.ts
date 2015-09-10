module Allors {
    export class DatabaseObject {
        database: Database;

        id: string;
        version: string;
        type: string;

        constructor(database: Database, loadObject: Data.LoadObjectData) {
            _.assign(this, loadObject);
            this.database = database;
        }

    }
}