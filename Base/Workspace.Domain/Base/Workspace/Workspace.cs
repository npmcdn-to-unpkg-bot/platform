namespace Allors.Workspace
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Meta;

    public interface IWorkspace
    {
        ObjectFactory ObjectFactory { get; }

        Data.SyncRequest diff(Data.PullResponse data);

        void sync(Data.SyncResponse data);

        WorkspaceObject get(long id);
    }

    public class Workspace : IWorkspace
    {
        public ObjectFactory ObjectFactory { get; }

        public Dictionary<string, ObjectType> objectTypeByName = new Dictionary<string, ObjectType>();

        public string userSecurityHash;

        Dictionary<long, WorkspaceObject> workspaceObjectById = new Dictionary<long, WorkspaceObject>();
        
        public Workspace(ObjectFactory objectFactory)
        {
            this.ObjectFactory = objectFactory;
        }

        public Data.SyncRequest diff(Data.PullResponse response)  {

            if (response == null)
            {
            }

            var userSecurityHash = response.userSecurityHash;

            var requireLoadIds = new Data.SyncRequest
                                     {
                                         objects = response.objects.Where(v =>
                                                 {
                                                     var id = long.Parse(v[0]);
                                                     var version = long.Parse(v[1]);
                                                     WorkspaceObject workspaceObject;
                                                     this.workspaceObjectById.TryGetValue(id, out workspaceObject);
                                                     return workspaceObject == null || !workspaceObject.version.Equals(version) || !workspaceObject.userSecurityHash.Equals(userSecurityHash);
                                                 }).Select(v => v[0]).ToArray()
                                     };

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

        public WorkspaceObject get(long id) {
            var workspaceObject = this.workspaceObjectById[id];
            if (workspaceObject == null)
            {
                throw new Exception($"Object with id {id} is not present.");
            }

            return workspaceObject;
        }
    }
}