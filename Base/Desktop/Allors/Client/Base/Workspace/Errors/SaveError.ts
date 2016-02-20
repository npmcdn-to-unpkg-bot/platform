namespace Allors {
    export class SaveError extends ResponseError {

        constructor(public saveResponse: Data.SaveResponse, message?: string) {
            super(saveResponse, message);
        }
        
    }
}