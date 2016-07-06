namespace Allors.Workspace {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Allors.Meta;

    public interface IWorkspace {
        Data.SyncRequest diff(Data.PullResponse data);
        void sync(Data.SyncResponse data);

        WorkspaceObject get(string id);

        MetaPopulation Population { get; }
    }

    public class Workspace : IWorkspace {
        public ObjectFactory Factory { get; }

        public MetaPopulation Population { get; }

        public Dictionary<string, ObjectType> objectTypeByName = new Dictionary<string, ObjectType>();

        public string userSecurityHash;

        Dictionary<string, WorkspaceObject> workspaceObjectById = new Dictionary<string, WorkspaceObject>();
        
        public Workspace(ObjectFactory objectFactory)
        {
            this.Factory = objectFactory;
        }

        public Data.SyncRequest diff(Data.PullResponse response)  {
            var userSecurityHash = response.userSecurityHash;

            var requireLoadIds = new Data.SyncRequest();
            requireLoadIds.objects = response.objects.Where(
                v =>
                    {
                        var id = v[0];
                        var version = v[1];
                        var workspaceObject = this.workspaceObjectById[id];
                        return workspaceObject == null || !workspaceObject.version.Equals(version) || !workspaceObject.userSecurityHash.Equals(userSecurityHash);
                    }).Select(v=>v[0]).ToArray();

            return requireLoadIds;
        }

        public void sync(Data.SyncResponse syncResponse)
        {
            foreach (var objectData in syncResponse.objects)
            {
                var workspaceObject = new WorkspaceObject(this, syncResponse, objectData);
                this.workspaceObjectById[workspaceObject.id] = workspaceObject;
            }
        }

        public WorkspaceObject get(string id) {
            var workspaceObject = this.workspaceObjectById[id];
            if (workspaceObject == null)
            {
                throw new Exception($"Object with id {id} is not present.");
            }

            return workspaceObject;
        }
    }
}