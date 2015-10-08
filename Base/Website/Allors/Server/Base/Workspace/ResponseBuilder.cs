using System.Collections.Generic;
using System.Linq;
using Allors.Meta;

namespace Allors.Web
{
    public class ResponseBuilder
    {
        private readonly HashSet<IObject> objects = new HashSet<IObject>();
        private readonly Dictionary<string, IObject> objectByName = new Dictionary<string, IObject>();
        private readonly Dictionary<string, List<IObject>> collectionsByName = new Dictionary<string, List<IObject>>();
        private readonly Dictionary<string, object> valueByName = new Dictionary<string, object>();

        public Response Build()
        {
            return new Response
            {
                Objects = this.objects.Select(x => new[] {x.Id.ToString(), x.Strategy.ObjectVersion.ToString()}).ToArray(),
                NamedObjects = this.objectByName.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Id.ToString()),
                NamedCollections = this.collectionsByName.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Select(obj => obj.Id.ToString()).ToArray()),
                NamedValues = valueByName,
            };
        }

        public void AddObject(string name, IObject namedObject, Tree tree = null)
        {
            if (namedObject != null)
            {
                // Prefetch
                if (tree != null)
                {
                    var session = namedObject.Strategy.Session;
                    var prefetcher = tree.BuildPrefetechPolicy();
                    session.Prefetch(prefetcher, namedObject);
                }

                objects.Add(namedObject);
                objectByName.Add(name, namedObject);
                tree?.Resolve(namedObject, this.objects);
            }

        }

        public void AddCollection(string name, IEnumerable<IObject> namedObjects, Tree tree = null)
        {
            if (namedObjects != null)
            {
                List<IObject> namedCollection;
                if (!collectionsByName.TryGetValue(name, out namedCollection))
                {
                    namedCollection = new List<IObject>();
                    this.collectionsByName.Add(name, namedCollection);
                }

                var namedObjectList = (namedObjects as IList<IObject>) ?? namedObjects.ToArray();

                // Prefetch
                if (tree != null && namedObjectList.Count > 0)
                {
                    var session = namedObjectList[0].Strategy.Session;
                    var prefetcher = tree.BuildPrefetechPolicy();
                    session.Prefetch(prefetcher, namedObjectList.ToArray());
                }

                namedCollection.AddRange(namedObjectList);
                foreach (var namedObject in namedObjectList)
                {
                    objects.Add(namedObject);
                    tree?.Resolve(namedObject, this.objects);
                }
            }
        }

        public void AddValue(string name, object value)
        {
            if (value != null)
            {
                this.valueByName.Add(name, value);
            }
        }
    }
}