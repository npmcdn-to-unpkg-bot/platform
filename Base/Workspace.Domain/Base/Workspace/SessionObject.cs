namespace Allors.Workspace {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Allors.Meta;

    public interface ISessionObject {
        string id { get; }

        string version { get; }

        Meta.ObjectType objectType { get; }

        ISession session { get; }

        IWorkspaceObject workspaceObject { get; set; }

        bool hasChanges { get; }

        bool canRead(IRoleType roleType);

        bool canWrite(IRoleType roleType);

        bool exist(IRoleType roleType);

        object get(IRoleType roleType);

        void set(IRoleType roleType, object value);

        void add(IRoleType roleType, ISessionObject value);

        void remove(IRoleType roleType, ISessionObject value);

        Data.PushRequestObject save();

        Data.PushRequestNewObject saveNew();

        void reset();
    }

    public interface INewSessionObject : ISessionObject {
        string newId { get; set; }
    }

    public class SessionObject : INewSessionObject {
        public ISession session { get; set; }

        public IWorkspaceObject workspaceObject { get; set; }
        public ObjectType objectType { get; set; }

        public string newId { get; set; }

        private Dictionary<IRoleType, object> changedRoleByRoleType;
        private Dictionary<IRoleType, object> roleByRoleType = new Dictionary<IRoleType, object>();

        public bool hasChanges {
            get
            {
                if (this.newId != null)
                {
                    return true;
                }

                return this.changedRoleByRoleType != null;
            }
        }
 
        public string id => this.workspaceObject?.id;

        public string version => this.workspaceObject?.version;

        public bool canRead(IRoleType roleType) {
            if (this.newId != null) {
                return true;
            }

            return this.workspaceObject.canRead(roleType.SingularName);
        }

        public bool canWrite(IRoleType roleType) {
            if (this.newId != null) {
                return true;
            }

            return this.workspaceObject.canWrite(roleType.SingularName);
        }

        public bool exist(IRoleType roleType)
        {
            var value = this.get(roleType);
            if (roleType.ObjectType.IsComposite && roleType.IsMany)
            {
                return !((IEnumerable<SessionObject>)value).Any();
            }

            return value != null;
        }

        public object get(IRoleType roleType)
        {
            object value;
            if (this.newId == null) {
                if (roleType.ObjectType.IsUnit) {
                    value = this.workspaceObject.roles[roleType.SingularName];
                } else {
                    try {
                        if (roleType.IsOne) {
                            string role = (string)this.workspaceObject.roles[roleType.SingularName];
                            value = role != null ? this.session.get(role) : null;
                        } else {
                            string[] roles = (string[])this.workspaceObject.roles[roleType.SingularName];
                            value = roles?.Select(role => this.session.get(role)) ?? new ISessionObject[0];
                        }
                    }
                    catch (Exception e) {
                        var x = "N/A";
                        try {
                            x = this.ToString();
                        }
                        catch (Exception e2) { };

                        throw new Exception($"Could not get role {roleType.SingularName} from [objectType: ${this.objectType.Name}, id: ${this.id}, value: '${x}']");
                    }
                }
            } else {
                if (roleType.ObjectType.IsComposite && roleType.IsMany) {
                    value = new ISessionObject[0];
                } else {
                    value = null;
                }                    
            }

            this.roleByRoleType[roleType] = value;

            return value;
        }

        public void set(IRoleType roleType, object value) {
            if (this.changedRoleByRoleType == null) {
                this.changedRoleByRoleType = new Dictionary<IRoleType, object>();
            }

            if (value == null) {
                if (roleType.ObjectType.IsComposite && roleType.IsMany) {
                    value = new ISessionObject[0];
                }
            }

            this.roleByRoleType[roleType] = value;
            this.changedRoleByRoleType[roleType] = value;
        }

        public void add(IRoleType roleType, ISessionObject value) {
            var roles = (ISessionObject[])this.get(roleType);
            if (!roles.Contains(value)) {
                roles = new List<ISessionObject>(roles) { value }.ToArray();
            }

            this.set(roleType, roles);
        }

        public void remove(IRoleType roleType, ISessionObject value) {
            var roles = (ISessionObject[])this.get(roleType);
            if (!roles.Contains(value))
            {
                var newRoles = new List<ISessionObject>(roles);
                newRoles.Remove(value);
                roles = newRoles.ToArray();
            }

            this.set(roleType, roles);
        }

        public Data.PushRequestObject save() {
            if (this.changedRoleByRoleType != null) {
                var data = new Data.PushRequestObject
                {
                    i = this.id,
                    v = this.version,
                    roles = this.saveRoles()
                };

                return data;
            }

            return null;
        }

        public Data.PushRequestNewObject saveNew() {
            var data = new Data.PushRequestNewObject
            {
                ni = this.newId,
                t = this.objectType.Name
            };

            if (this.changedRoleByRoleType != null) {
                data.roles = this.saveRoles();
            }

            return data;
        }
        
        public void reset() {
            if (this.workspaceObject != null) {
                this.workspaceObject = this.workspaceObject.workspace.get(this.id);
            }

            if (this.changedRoleByRoleType != null) {
                this.changedRoleByRoleType = null;
            }

            this.roleByRoleType = new Dictionary<IRoleType, object>();
        }

        private Data.PushRequestRole[] saveRoles() {
            var saveRoles = new List<Data.PushRequestRole>();

            foreach (var keyValuePair in this.changedRoleByRoleType)
            {
                var roleType = keyValuePair.Key;
                var role = keyValuePair.Value;

                var saveRole = new Data.PushRequestRole();
                saveRole.t = roleType.SingularName;

                if (roleType.ObjectType.IsUnit)
                {
                    saveRole.s = role;
                }
                else
                {
                    if (roleType.IsOne)
                    {
                        var sessionRole = (INewSessionObject)role;
                        saveRole.s = sessionRole?.id ?? sessionRole?.newId;
                    }
                    else
                    {
                        var sessionRoles = (INewSessionObject[])role;
                        var roleIds = sessionRoles.Select(item => item.id ?? item.newId).ToArray();
                        if (this.newId != null)
                        {
                            saveRole.a = roleIds;
                        }
                        else
                        {
                            var originalRoleIds = (string[])this.workspaceObject.roles[roleType.SingularName];
                            if (originalRoleIds == null)
                            {
                                saveRole.a = roleIds;
                            }
                            else
                            {
                                saveRole.a = roleIds.Except(originalRoleIds).ToArray();
                                saveRole.r = originalRoleIds.Except(roleIds).ToArray();
                            }
                        }
                    }
                }

                saveRoles.Add(saveRole);
            }

            return saveRoles.ToArray();
        }
    }
}