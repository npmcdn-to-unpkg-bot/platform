module Allors.Data {
    export class Response {
        objects: string[][];

        namedObjects: { [id: string]: string; };
        namedCollections: { [id: string]: string[]; };
        namedValues: { [id: string]: any; };
    }
}