module Allors.Data {
    export class LoadObjectData {
        id: string;
        version: string;
        type: string;
    }

    export class LoadData {
        objects: LoadObjectData[];
    }
}