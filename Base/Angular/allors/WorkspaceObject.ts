module Allors {
    export class WorkspaceObject {
        databaseObject: DatabaseObject;

        constructor(databaseObject: DatabaseObject) {
            this.databaseObject = databaseObject;
        }

        get(roleType): any {
        }

        set(roleType, newValue) {
        }
    }
}