module Allors {
    export class ObjectDiff {
        id: string;
        version: string;

        roleDiffByRoleTypeName: { [id: string]: RoleDiff; } = {};

        constructor(id: string, version: string) {
            this.id = id;
            this.version = version;
        }
    }
}