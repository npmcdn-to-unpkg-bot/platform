namespace Allors.Data {
    export interface SaveResponse extends ResponseError {
        hasErrors: boolean;
        errorMessage?: string;
        versionErrors?: string[];
        accessErrors?: string[];
        missingErrors?: string[];
        derivationErrors?: Allors.Data.ResponseDerivationError[];

        newObjects?: Allors.Data.SaveResponseNewObject[];
    }
}