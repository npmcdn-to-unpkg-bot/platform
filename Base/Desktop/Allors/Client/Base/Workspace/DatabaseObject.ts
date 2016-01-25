module Allors {
    export interface IDatabaseObject {
        id: string;
        version: string;
        userSecurityHash: string;
        objectType: ObjectType;

        database: IDatabase;
        roles: any;
        methods: any;
    }

    export class DatabaseObject implements IDatabaseObject {
        database: IDatabase;
        roles: any;
        methods: any;

        private i: string;
        private v: string;
        private u: string;
        private t: string;

        constructor(database: IDatabase, loadResponse: Data.LoadResponse, loadObject: Data.LoadResponseObject) {
            this.database = database;
            this.i = loadObject.i;
            this.v = loadObject.v;
            this.u = loadResponse.userSecurityHash;
            this.t = loadObject.t;

            this.roles = {};
            this.methods = {};

            var objectType = this.database.objectTypeByName[this.t];

            _.forEach(loadObject.roles, role => {
                var name = role[0];
                var access = role[1];
                var canRead = access.indexOf('r') !== -1;
                var canWrite = access.indexOf('w') !== -1;

                this.roles["CanRead" + name] = canRead;
                this.roles["CanWrite" + name] = canWrite;
                
                if (canRead) {
                    var roleType = objectType.roleTypeByName[name];
                    var value = role[2];
                    if (value && roleType.isUnit && roleType.objectType === "DateTime") {
                        value = new Date(<string>value);
                    }
                    this.roles[name] = value;
                }

            });

            _.forEach(loadObject.methods, method => {
                var name = method[0];
                var access = method[1];
                var canExecute = access.indexOf('x') !== -1;

                this.methods["CanExecute" + name] = canExecute;
            });
        }

        get id(): string {
            return this.i;
        }

        get version(): string {
            return this.v;
        }

        get userSecurityHash(): string {
            return this.u;
        }

        get objectType(): ObjectType {
            return this.database.objectTypeByName[this.t];
        }
    }
}