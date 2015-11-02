module Allors.Data {
    export interface InvokeResponse extends ResponseError {
        hasErrors: boolean;
        errorMessage?: string;
        versionErrors?: string[];
        accessErrors?: string[];
        missingErrors?: string[];
        derivationErrors?: Allors.Data.ResponseDerivationError[];
    }
}