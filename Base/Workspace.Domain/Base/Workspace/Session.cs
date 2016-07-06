namespace Allors.Workspace
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Allors.Workspace.Data;
    using Meta;

    public interface ISession
    {
        bool hasChanges { get; }

        ISessionObject get(long id);

        ISessionObject create(Class @class);

        Data.PushRequest pushRequest();

        void pushResponse(Data.PushResponse saveResponse);

        void reset();
    }

    public class Session : ISession
    {
        private static long idCounter = 0;

        private Workspace workspace;
        private Dictionary<long, SessionObject> sessionObjectById = new Dictionary<long, SessionObject>();
        private Dictionary<long, SessionObject> newSessionObjectById = new Dictionary<long, SessionObject>();

        public Session(Workspace workspace)
        {
            this.workspace = workspace;
        }

        public bool hasChanges
        {
            get
            {
                return this.newSessionObjectById.Count > 0 || this.sessionObjectById.Values.Any(v => v.hasChanges);
            }
        }

        public ISessionObject get(long id)
        {
            SessionObject sessionObject;
            if (!this.sessionObjectById.TryGetValue(id, out sessionObject))
            {
                var workspaceObject = this.workspace.get(id);

                sessionObject = this.workspace.ObjectFactory.Create(this, workspaceObject.objectType);

                sessionObject.workspaceObject = workspaceObject;
                sessionObject.objectType = workspaceObject.objectType;

                this.sessionObjectById[workspaceObject.id] = sessionObject;
            }

            return sessionObject;
        }

        public ISessionObject create(Class @class)
        {
            var newSessionObject = this.workspace.ObjectFactory.Create(this, @class);

            var newId = --Session.idCounter;
            newSessionObject.newId = newId;
            newSessionObject.objectType = @class;

            this.newSessionObjectById[newId] = newSessionObject;

            return newSessionObject;
        }

        public void reset()
        {
            foreach (var newSessionObject in this.newSessionObjectById.Values)
            {
                newSessionObject.reset();
            }

            foreach (var sessionObject in this.sessionObjectById.Values)
            {
                sessionObject.reset();
            }
        }

        public PushRequest pushRequest()
        {
            var data = new PushRequest
                           {
                               newObjects = new List<PushRequestNewObject>(),
                               objects = new List<PushRequestObject>()
                           };

            foreach (var newSessionObject in this.newSessionObjectById.Values)
            {
                var objectData = newSessionObject.saveNew();
                if (objectData != null)
                {
                    data.newObjects.Add(objectData);
                }
            }

            foreach (var sessionObject in this.sessionObjectById.Values)
            {
                var objectData = sessionObject.save();
                if (objectData != null)
                {
                    data.objects.Add(objectData);
                }
            }

            return data;
        }
        
        public void pushResponse(PushResponse pushResponse)
        {
            if (pushResponse.newObjects != null && pushResponse.newObjects.Length > 0)
            {
                foreach (var pushResponseNewObject in pushResponse.newObjects)
                {
                    var newId = long.Parse(pushResponseNewObject.ni);
                    var id = long.Parse(pushResponseNewObject.i);

                    var newSessionObject = this.newSessionObjectById[newId];

                    var loadResponse = new SyncResponse
                                           {
                                               userSecurityHash = "#",
                                               // This should trigger a load on next check
                                               objects =
                                                   new[]
                                                       {
                                                           new SyncResponseObject
                                                               {
                                                                   i = id.ToString(),
                                                                   v = "",
                                                                   t =
                                                                       newSessionObject
                                                                       .objectType.Name,
                                                                   roles = new object[0][],
                                                                   methods = new string[0][]
                                                               }
                                                       }
                                           };

                    this.newSessionObjectById.Remove(newId);
                    newSessionObject.newId = null;

                    this.workspace.sync(loadResponse);
                    var workspaceObject = this.workspace.get(id);
                    newSessionObject.workspaceObject = workspaceObject;

                    this.sessionObjectById[id] = newSessionObject;
                }
            }

            if (this.newSessionObjectById != null && this.newSessionObjectById.Count != 0)
            {
                throw new Exception("Not all new objects received ids");
            }
        }
    }
}