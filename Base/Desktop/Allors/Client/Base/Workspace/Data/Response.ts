module Allors.Data {
    export interface Response {
        objects?: string[][];

        namedObjects?: { [id: string]: string; };
        namedCollections?: { [id: string]: string[]; };
        namedValues?: { [id: string]: any; };
    }
}