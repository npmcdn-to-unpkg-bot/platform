namespace Allors {
    export abstract class ResponseError extends Error {

        constructor(public responseError: Data.ResponseError, message?: string) {
            super(message);
        }
    }
}