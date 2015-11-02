using System.Collections.Generic;

namespace Allors.Web
{
    public class Response
    {
        public string[][] Objects { get; set; }

        public Dictionary<string, string> NamedObjects { get; set; }

        public Dictionary<string, string[]> NamedCollections { get; set; }

        public Dictionary<string, object> NamedValues { get; set; }
    }
}