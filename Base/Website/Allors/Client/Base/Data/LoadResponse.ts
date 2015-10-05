module Allors.Data {
    export class LoadResponseObject {
        i: string;
        v: string;
        t: string;

        roles: string[][];
        methods: string[][];
    }

    export class LoadResponse {
        objects: LoadResponseObject[];
    }
}