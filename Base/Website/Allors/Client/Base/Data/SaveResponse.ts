module Allors.Data {
    export class SaveResponse {
        hasErrors : boolean;
        errors: { [id: string]: { [roleType: string]: string[]; }; };
    }
}