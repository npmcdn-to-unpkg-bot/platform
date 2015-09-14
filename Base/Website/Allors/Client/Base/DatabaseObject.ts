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
                var name = role[0];
                var access = role[1];
                var canRead = access.indexOf('r') !== -1;
                var canWrite = access.indexOf('w') !== -1;

                this.roles["CanRead" + name] = canRead;
                this.roles["CanWrite" + name] = canWrite;

                if (canRead) {
                    var value = role[2];
                    this.roles[name] = value;
                }

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