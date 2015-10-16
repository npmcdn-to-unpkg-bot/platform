module Allors.Data {
    export class SaveResponse implements ResponseError {
        hasErrors: boolean;
        errorMessage: string;
        versionErrors: string[];
        accessErrors: string[];
        missingErrors: string[];
        derivationErrors: Allors.Data.ResponseDerivationError[];
    }
}