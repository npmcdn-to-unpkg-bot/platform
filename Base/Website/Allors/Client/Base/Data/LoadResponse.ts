module Allors.Data {
    export class LoadResponseObject {
        i: string;
        v: string;
        t: string;

        roles: string[][];
    }

    export class LoadResponse {
        objects: LoadResponseObject[];
    }
}