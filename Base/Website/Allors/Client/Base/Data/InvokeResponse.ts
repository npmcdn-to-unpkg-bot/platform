module Allors.Data {
    export class InvokeResponse implements ResponseError {
        hasErrors: boolean;
        versionErrors: string[];
        accessErrors: string[];
        missingErrors: string[];
        derivationErrors: Allors.Data.ResponseDerivationError[];
    }
}