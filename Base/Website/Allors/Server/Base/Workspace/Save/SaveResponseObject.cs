using Newtonsoft.Json;

namespace Allors.Web.Workspace
{
    public class SaveResponseObject
    {
        [JsonProperty(PropertyName = "i")]
        public string Id { get; set; }

        public SaveResponseRole[] Roles { get; set; }
    }
}