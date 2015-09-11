module Allors.Data {
    export class LoadResponseObject {
        i: string;
        v: string;
        t: string;
    }

    export class LoadResponse {
        objects: LoadResponseObject[];
    }
}