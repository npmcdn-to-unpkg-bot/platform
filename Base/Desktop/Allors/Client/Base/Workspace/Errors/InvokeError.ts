namespace Allors {
    export class InvokeError extends ResponseError {

        constructor(public invokeResponse: Data.SaveResponse, message?: string) {
            super(invokeResponse, message);
        }
        
    }
}