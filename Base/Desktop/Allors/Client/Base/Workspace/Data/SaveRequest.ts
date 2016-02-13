namespace Allors.Data {
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

    export class SaveRequestNewObject {
        ni: string;
        t: string;
        roles: SaveRequestRole[];
    }

    export class SaveRequest {
        newObjects: SaveRequestNewObject[];
        objects: SaveRequestObject[];
    }
}