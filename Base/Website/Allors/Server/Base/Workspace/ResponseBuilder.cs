using System.Collections.Generic;
using System.Linq;
using System.Resources;
using Allors.Domain;
using Allors.Meta;

namespace Allors.Web
{
    public class ResponseBuilder
    {
        private readonly HashSet<IObject> objects = new HashSet<IObject>();
        private readonly Dictionary<string, IObject> namedObjects = new Dictionary<string, IObject>();
         
        public Response Build()
        {
            return new Response
            {
                Objects = this.objects.Select(x => new[] {x.Id.ToString(), x.Strategy.ObjectVersion.ToString()}).ToArray(),
                NamedObjects = this.namedObjects.Select(kvp => new[] { kvp.Key, kvp.Value.Id.ToString() }).ToArray()
            };
        }

        public void Add(string name, IObject namedObject, Tree tree = null)
        {
            namedObjects.Add(name, namedObject);
            tree?.Resolve(namedObject, objects);
        }
    }
}