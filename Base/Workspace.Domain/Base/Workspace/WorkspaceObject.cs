﻿namespace Allors.Workspace {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Meta;

    public interface IWorkspaceObject {
        string id { get;  }

        string version { get;  }

        string userSecurityHash { get;  }

        ObjectType objectType { get;  }

        IWorkspace workspace { get;  }

        Dictionary<string, object> roles { get;  }

        Dictionary<string, object> methods { get;  }

        bool canRead(string roleTypeName);

        bool canWrite(string roleTypeName);
    }

    public class WorkspaceObject : IWorkspaceObject {
        public IWorkspace workspace { get; set; }
        public Dictionary<string, object> roles { get; set; }
        public Dictionary<string, object> methods { get; set; }

        private string i;
        private string v;
        private string u;
        private string t;

        public WorkspaceObject(IWorkspace workspace, Data.SyncResponse loadResponse, Data.SyncResponseObject loadObject) {
            this.workspace = workspace;
            this.i = loadObject.i;
            this.v = loadObject.v;
            this.u = loadResponse.userSecurityHash;
            this.t = loadObject.t;

            this.roles = new Dictionary<string, object>();
            this.methods =new Dictionary<string, object>();

            this.objectType = this.workspace.Population.FindByName(this.t);

            foreach (var role in loadObject.roles)
            {
                var name = (string)role[0];
                var access = (string)role[1];
                var canRead = access.Contains("r");
                var canWrite = access.Contains("w");

                this.roles["CanRead{name}"] = canRead;
                this.roles["CanWrite{name}"] = canWrite;

                if (canRead)
                {
                    var roleType = ((Composite)this.objectType).RoleTypes.First(v=>v.Name.Equals(name));
                    var value = role[2];
                    if (value != null && roleType.ObjectType.IsUnit && roleType.ObjectType.Name.Equals("DateTime"))
                    {
                        // TODO: check datetime
                        value = DateTime.Parse((string)value);
                    }
                    this.roles[name] = value;
                }
            }


            foreach (var method in loadObject.methods)
            {
                var name = method[0];
                var access = method[1];
                var canExecute = access.Contains("x");

                this.methods[$"CanExecute{name}"] = canExecute;
            }
        }

        public string id => this.i;

        public string version => this.v;

        public string userSecurityHash => this.u;

        public ObjectType objectType { get; }

        public bool canRead(string roleTypeName) {
            return (bool)this.roles[$"CanRead{roleTypeName}"];
        }

        public bool canWrite(string roleTypeName) {
            return (bool)this.roles[$"CanWrite{roleTypeName}"];
        }
    }
}