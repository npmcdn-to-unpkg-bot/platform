namespace Allors.Data {
    export interface ResponseError {
        hasErrors: boolean;

        errorMessage?: string;

        versionErrors?: string[];

        accessErrors?: string[];

        missingErrors?: string[];

        derivationErrors?: ResponseDerivationError[];
    }
}