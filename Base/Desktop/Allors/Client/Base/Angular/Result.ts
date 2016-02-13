namespace Allors {
    export class Result {
        objects: { [name: string]: SessionObject; } = {};
        collections: { [name: string]: SessionObject[]; } = {};
        values: { [name: string]: any; } = {};
    }
}