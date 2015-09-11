module Allors {
    export class DatabaseObject {
        database: Database;
        private i: string;
        private v: string;
        private t: string;

        constructor(database: Database, loadObject: Data.LoadObjectResponse) {
            _.assign(this, loadObject);
            this.database = database;
        }

        get id(): string {
            return this.i;
        }

        get version(): string {
            return this.v;
        }

        get objectType(): ObjectType {
            return this.database.objectTypeByName[this.t];
        }
    }
}