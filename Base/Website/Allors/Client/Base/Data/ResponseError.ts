module Allors.Data {
    export interface ResponseError {
        hasErrors: boolean;

        versionErrors : string[];

        accessErrors : string[];

        missingErrors : string[];

        derivationErrors : ResponseDerivationError[];
    }
}