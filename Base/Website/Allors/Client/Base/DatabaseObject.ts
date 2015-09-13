module Allors {
    export class DatabaseObject {
        database: Database;
        private i: string;
        private v: string;
        private t: string;
        public roles: any;

        constructor(database: Database, loadObject: Data.LoadResponseObject) {
            this.database = database;
            this.i = loadObject.i;
            this.v = loadObject.v;
            this.t = loadObject.t;

            this.roles = {};

            _.forEach(loadObject.roles, role => {
                this.roles[role[0]] = role[1];
            });
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