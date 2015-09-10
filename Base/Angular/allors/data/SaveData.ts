module Allors.Data {
    export class SaveRoleData {
        set: any;
        add: string[];
        remove: string[];
    }

    export class SaveObjectData {
        id: string;
        version: string;
        roles: SaveRoleData[];
    }

    export class SaveData {
        objects: SaveObjectData[];
    }
}