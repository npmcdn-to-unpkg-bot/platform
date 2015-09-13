using Newtonsoft.Json;

namespace Allors.Web.Workspace
{
public class LoadResponseObject
    {
        [JsonProperty(PropertyName = "i")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "v")]
        public string Version { get; set; }

        [JsonProperty(PropertyName = "t")]
        public string ObjectType { get; set; }

        public object[][] Roles { get; set; }
    }
}