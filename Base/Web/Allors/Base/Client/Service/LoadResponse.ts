module Allors.Data {
    export class LoadObjectResponse {
        i: string;
        v: string;
        t: string;
    }

    export class LoadResponse {
        objects: LoadObjectResponse[];
    }
}