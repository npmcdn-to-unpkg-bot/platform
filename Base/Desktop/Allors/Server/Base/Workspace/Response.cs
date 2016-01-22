namespace Allors.Web
{
    using System.Collections.Generic;

    public class Response
    {
        public string UserSecurityHash { get; set; }

        public string[][] Objects { get; set; }

        public Dictionary<string, string> NamedObjects { get; set; }

        public Dictionary<string, string[]> NamedCollections { get; set; }

        public Dictionary<string, object> NamedValues { get; set; }
    }
}