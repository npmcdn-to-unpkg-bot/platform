module Allors.Data {
    export class SaveRoleRequest {
        t: string;
        s: any;
        a: string[];
        r: string[];
    }

    export class SaveObjectRequest {
        i: string;
        v: string;
        roles: SaveRoleRequest[];
    }

    export class SaveRequest {
        objects: SaveObjectRequest[];
    }
}