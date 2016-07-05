namespace Allors.Data {
    using System.Collections.Generic;
    public interface PullResponse {
        string userSecurityHash { get; set; }

        string[][] objects { get; set; }

        Dictionary<string, string> namedObjects { get; set; }
        Dictionary<string, string[]> namedCollections { get; set; }
        Dictionary<string,object> namedValues { get; set; }
    }
}