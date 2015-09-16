using System.Collections.Generic;

namespace Allors.Web.Workspace
{
    public class SaveResponse
    {
        public bool HasErrors { get; set; }

        public Dictionary<string, Dictionary<string, string[]>> Errors { get; set; }
    }
}