module Allors.Data {
    export class LoadResponseObject {
        i: string;
        v: string;
        t: string;

        roles: any[][];
        methods: string[][];
    }

    export class LoadResponse {
        objects: LoadResponseObject[];
    }
}