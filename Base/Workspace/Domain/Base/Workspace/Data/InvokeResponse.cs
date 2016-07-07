namespace Allors.Workspace.Data {
    public interface InvokeResponse : ErrorResponse {
        bool hasErrors { get; set; }
        string errorMessage { get; set; }
        string versionErrors { get; set; }
        string accessErrors { get; set; }
        string missingErrors { get; set; }
        PullResponseDerivationError[] derivationErrors { get; set; }
    }
}