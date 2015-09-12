using Newtonsoft.Json;

namespace Allors.Web.Workspace
{
    public class SaveRequestObject
    {
        [JsonProperty(PropertyName = "i")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "v")]
        public string Version { get; set; }

        public SaveRequestRole Roles { get; set; }
    }
}