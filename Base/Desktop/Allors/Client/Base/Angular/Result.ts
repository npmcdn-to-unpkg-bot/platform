namespace Allors {
    export class Result {
        objects: { [name: string]: Allors.SessionObject; } = {};
        collections: { [name: string]: Allors.SessionObject[]; } = {};
        values: { [name: string]: any; } = {};
    }
}