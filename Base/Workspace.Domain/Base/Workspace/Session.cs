namespace Allors.Workspace {
    using System;
    using System.Collections.Generic;
using System.Linq;
using System.Reflection;

    using Allors.Data;
    using Allors.Meta;

    public interface ISession {
        bool hasChanges { get; }

        ISessionObject get(string id);

        ISessionObject create(IClass @class);

        Data.PushRequest pushRequest();

        void pushResponse(Data.PushResponse saveResponse);

        void reset();
    }

    public class Session : ISession {
        private static long idCounter = 0;

        private IWorkspace workspace;
        private Dictionary<string, ISessionObject> sessionObjectById = new Dictionary<string, ISessionObject>();
        private Dictionary<string, INewSessionObject> newSessionObjectById = new Dictionary<string, INewSessionObject>();

        public Session(IWorkspace workspace) {
            this.workspace = workspace;
        }

        public bool hasChanges {
            get
            {
                return this.newSessionObjectById.Count > 0 || this.sessionObjectById.Values.Any(v => v.hasChanges);
            }
        }

         public ISessionObject get(string id) {
            if (id == null) {
                return null;
            }

            ISessionObject sessionObject;
             if (!this.sessionObjectById.TryGetValue(id, out sessionObject))
             {
                var workspaceObject = this.workspace.get(id);
                
                var type = workspaceObject.objectType.ClrType;
                sessionObject = (ISessionObject)Activator.CreateInstance(type, new object[] { workspaceObject });
                this.sessionObjectById[sessionObject.id] = sessionObject;
            }

            return sessionObject;
        }

        public ISessionObject create(IClass @class) {

            var type = @class.ClrType;
            var newSessionObject = (INewSessionObject)Activator.CreateInstance(type, new object[] { this, --Session.idCounter });
            this.newSessionObjectById[newSessionObject.newId] = newSessionObject;

            return newSessionObject;
        }

        public void reset() {
            foreach (var newSessionObject in this.newSessionObjectById.Values)
            {
                newSessionObject.reset();
            }

            foreach (var sessionObject in this.sessionObjectById.Values)
            {
                sessionObject.reset();
            }
        }

        public PushRequest pushRequest() {
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
        
        public void pushResponse(Data.PushResponse pushResponse)
        {
            if (pushResponse.newObjects != null && pushResponse.newObjects.Length > 0)
            {
                foreach (var pushResponseNewObject in pushResponse.newObjects)
                {
                    var newId = pushResponseNewObject.ni;
                    var id = pushResponseNewObject.i;

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
                                                                   i = id,
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