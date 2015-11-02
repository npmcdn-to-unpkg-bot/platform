module Allors.Data {
    export interface LoadResponseObject {
        i: string;
        v: string;
        t: string;

        roles?: any[][];
        methods?: string[][];
    }

    export interface LoadResponse {
        objects: LoadResponseObject[];
    }
}