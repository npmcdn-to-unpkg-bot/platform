module Allors.Data {
    export class SaveResponse {
        errors: { [id: string]: { [roleType: string]: string[]; }; };
    }
}