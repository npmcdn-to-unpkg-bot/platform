module Allors.Data {
    export class SaveRequestRole {
        t: string;
        s: any;
        a: string[];
        r: string[];
    }

    export class SaveRequestObject {
        i: string;
        v: string;
        roles: SaveRequestRole[];
    }

    export class SaveRequest {
        objects: SaveRequestObject[];
    }
}