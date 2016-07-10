namespace Allors.Workspace.Client
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Data;

    public class Context
    {
        public Dictionary<string, ISessionObject> objects;
        public Dictionary<string, ISessionObject[]> collections;
        public Dictionary<string, object> values;

        public ISession session;

        private readonly string name;
        private readonly Database database;
        private readonly Workspace workspace;

        public Context(string name, Database database, Workspace workspace)
        {
            this.name = name;
            this.database = database;
            this.workspace = workspace;

            this.session = new Session(this.workspace);
        }

        public async Task load(Dictionary<string,string> p)
        {
            var response = await this.database.pull(this.name, p);
            var requireLoadIds = this.workspace.diff(response);
            if (requireLoadIds.objects.Length > 0)
            {
                var loadResponse = await this.database.sync(requireLoadIds);
                this.workspace.sync(loadResponse);
            }

            this.update(response);
            this.session.reset();
        }

        public async Task<Result> query(string service, Dictionary<string, string> p) {

            var pullResponse = await this.database.pull(service, p);
            var requireLoadIds = this.workspace.diff(pullResponse);
            if (requireLoadIds.objects.Length > 0)
            {
                var loadResponse = await this.database.sync(requireLoadIds);
                this.workspace.sync(loadResponse);
            }

            var result = new Result(this.session, pullResponse);
            return result;
        }

        public async Task<PushResponse> save(){
            var saveRequest = this.session.pushRequest();
            var pushResponse = await this.database.push(saveRequest);
            this.session.pushResponse(pushResponse);
            return pushResponse;
        }

        public Task<InvokeResponse> invoke(Method method)
        {
            return this.database.invoke(method);
        }

        public Task<InvokeResponse> invoke(string service, Dictionary<string, string> values)
        {
            return this.database.invoke(service, values);
        }
        
        private void update(PullResponse response) {

            this.objects = response.namedObjects.ToDictionary(
                pair => pair.Key,
                pair => this.session.get(long.Parse(pair.Value)));
            this.collections = response.namedCollections.ToDictionary(
                pair => pair.Key,
                pair => pair.Value.Select(v => this.session.get(long.Parse(v))).ToArray());
            this.values = response.namedValues.ToDictionary(
                pair => pair.Key,
                pair => pair.Value);
        }
    }
}