namespace Allors.Workspace.Client
{
    using System.Collections.Generic;
    using System.Linq;

    using Data;

    public class Result {

        public Dictionary<string, ISessionObject> objects;
        public Dictionary<string, ISessionObject[]> collections;
        public Dictionary<string, object> values;

        public Result(ISession session, PullResponse response)
        {
            this.objects = response.namedObjects.ToDictionary(
                pair => pair.Key,
                pair => session.get(long.Parse(pair.Value)));
            this.collections = response.namedCollections.ToDictionary(
                pair => pair.Key,
                pair => pair.Value.Select(v => session.get(long.Parse(v))).ToArray());
            this.values = response.namedValues.ToDictionary(
                pair => pair.Key, 
                pair => pair.Value);
        }
    }
}