namespace Allors.Workspace.Client
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Data;

    public class Context
    {
        private readonly string name;
        private readonly Database database;
        private readonly Workspace workspace;

        public Context(string name, Database database, Workspace workspace)
        {
            this.name = name;
            this.database = database;
            this.workspace = workspace;

            this.Session = new Session(this.workspace);
        }

        public Session Session { get; }

        public Dictionary<string, SessionObject> Objects { get; private set; }

        public Dictionary<string, SessionObject[]> Collections { get; private set; }

        public Dictionary<string, object> Values { get; private set; }

        public async Task Load(object args)
        {
            var response = await this.database.Pull(this.name, args);
            var requireLoadIds = this.workspace.diff(response);
            if (requireLoadIds.objects.Length > 0)
            {
                var loadResponse = await this.database.Sync(requireLoadIds);
                this.workspace.sync(loadResponse);
            }

            this.Update(response);
            this.Session.Reset();
        }

        public async Task<Result> Query(string service, object args)
        {
            var pullResponse = await this.database.Pull(service, args);
            var requireLoadIds = this.workspace.diff(pullResponse);
            if (requireLoadIds.objects.Length > 0)
            {
                var loadResponse = await this.database.Sync(requireLoadIds);
                this.workspace.sync(loadResponse);
            }

            var result = new Result(this.Session, pullResponse);
            return result;
        }

        public async Task<PushResponse> Save()
        {
            var saveRequest = this.Session.PushRequest();
            var pushResponse = await this.database.Push(saveRequest);
            this.Session.PushResponse(pushResponse);
            return pushResponse;
        }

        public Task<InvokeResponse> Invoke(Method method)
        {
            return this.database.Invoke(method);
        }

        public Task<InvokeResponse> Invoke(string service, object args)
        {
            return this.database.Invoke(service, args);
        }
        
        private void Update(PullResponse response)
        {
            this.Objects = response.namedObjects.ToDictionary(
                pair => pair.Key,
                pair => this.Session.Get(long.Parse(pair.Value)));
            this.Collections = response.namedCollections.ToDictionary(
                pair => pair.Key,
                pair => pair.Value.Select(v => this.Session.Get(long.Parse(v))).ToArray());
            this.Values = response.namedValues.ToDictionary(
                pair => pair.Key,
                pair => pair.Value);
        }
    }
}