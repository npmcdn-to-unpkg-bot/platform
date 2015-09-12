using Newtonsoft.Json;

namespace Allors.Web.Workspace
{
    public class SaveRequestRole
    {
        [JsonProperty(PropertyName = "t")]
        public string RoleTypeName { get; set; }

        [JsonProperty(PropertyName = "s")]
        public string Set { get; set; }

        [JsonProperty(PropertyName = "a")]
        public string[] Add { get; set; }

        [JsonProperty(PropertyName = "r")]
        public string[] Remove { get; set; }
    }
}