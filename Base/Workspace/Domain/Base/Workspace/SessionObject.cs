namespace Allors.Workspace {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Allors.Meta;

    public interface ISessionObject {
        long? id { get; }

        long? version { get; }

        Class objectType { get; }

        ISession session { get; }

        IWorkspaceObject workspaceObject { get; set; }

        bool hasChanges { get; }

        bool canRead(RoleType roleType);

        bool canWrite(RoleType roleType);

        bool exist(RoleType roleType);

        object get(RoleType roleType);

        void set(RoleType roleType, object value);

        void add(RoleType roleType, ISessionObject value);

        void remove(RoleType roleType, ISessionObject value);

        Data.PushRequestObject save();

        Data.PushRequestNewObject saveNew();

        void reset();
    }

    public interface INewSessionObject : ISessionObject {
        long? newId { get; set; }
    }

    public class SessionObject : INewSessionObject
    {
        protected SessionObject(Session session)
        {
            this.session = session;
        }

        public ISession session { get; }

        public IWorkspaceObject workspaceObject { get; set; }
        public Class objectType { get; set; }

        public long? newId { get; set; }

        private Dictionary<RoleType, object> changedRoleByRoleType;
        private Dictionary<RoleType, object> roleByRoleType = new Dictionary<RoleType, object>();

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
 
        public long? id => this.workspaceObject?.id;

        public long? version => this.workspaceObject?.version;

        public bool canRead(RoleType roleType) {
            if (this.newId != null) {
                return true;
            }

            return this.workspaceObject.canRead(roleType.PropertyName);
        }

        public bool canWrite(RoleType roleType) {
            if (this.newId != null) {
                return true;
            }

            return this.workspaceObject.canWrite(roleType.PropertyName);
        }

        public bool exist(RoleType roleType)
        {
            var value = this.get(roleType);
            if (roleType.ObjectType.IsComposite && roleType.IsMany)
            {
                return !((IEnumerable<SessionObject>)value).Any();
            }

            return value != null;
        }

        public object get(RoleType roleType)
        {
            object value;
            if (!this.roleByRoleType.TryGetValue(roleType, out value))
            {
                if (this.newId == null)
                {
                    if (roleType.ObjectType.IsUnit)
                    {
                        this.workspaceObject.roles.TryGetValue(roleType.PropertyName, out value);
                    }
                    else
                    {
                        try
                        {
                            if (roleType.IsOne)
                            {
                                object role;
                                this.workspaceObject.roles.TryGetValue(roleType.PropertyName, out role);
                                value = role != null ? this.session.get(long.Parse((string)role)) : null;
                            }
                            else
                            {
                                object roles;
                                this.workspaceObject.roles.TryGetValue(roleType.PropertyName, out roles);
                                var array = (roles as string[])?.Select(role => this.session.get(long.Parse(role))).ToArray() ?? new ISessionObject[0];
                                value = new ArrayList(array).ToArray(roleType.ObjectType.ClrType);
                            }
                        }
                        catch (Exception e)
                        {
                            var x = "N/A";
                            try
                            {
                                x = this.ToString();
                            }
                            catch (Exception e2) { };

                            throw new Exception($"Could not get role {roleType.PropertyName} from [objectType: ${this.objectType.Name}, id: ${this.id}, value: '${x}']");
                        }
                    }
                }
                else
                {
                    if (roleType.ObjectType.IsComposite && roleType.IsMany)
                    {
                        value = new ArrayList(new ISessionObject[0]).ToArray(roleType.ObjectType.ClrType);
                    }
                }

                this.roleByRoleType[roleType] = value;
            }

            return value;
        }

        public void set(RoleType roleType, object value) {
            if (this.changedRoleByRoleType == null) {
                this.changedRoleByRoleType = new Dictionary<RoleType, object>();
            }

            if (value == null) {
                if (roleType.ObjectType.IsComposite && roleType.IsMany) {
                    value = new ISessionObject[0];
                }
            }

            if (roleType.ObjectType.IsComposite && roleType.IsMany)
            {
                value = new ArrayList((Array)value).ToArray(roleType.ObjectType.ClrType);
            }
            
            this.roleByRoleType[roleType] = value;
            this.changedRoleByRoleType[roleType] = value;
        }

        public void add(RoleType roleType, ISessionObject value) {
            var roles = (ISessionObject[])this.get(roleType);
            if (!roles.Contains(value)) {
                roles = new List<ISessionObject>(roles) { value }.ToArray();
            }

            this.set(roleType, roles);
        }

        public void remove(RoleType roleType, ISessionObject value) {
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
                    i = this.id.ToString(),
                    v = this.version.ToString(),
                    roles = this.saveRoles()
                };

                return data;
            }

            return null;
        }

        public Data.PushRequestNewObject saveNew() {
            var data = new Data.PushRequestNewObject
            {
                ni = this.newId.ToString(),
                t = this.objectType.Name
            };

            if (this.changedRoleByRoleType != null) {
                data.roles = this.saveRoles();
            }

            return data;
        }
        
        public void reset() {
            if (this.workspaceObject != null) {
                this.workspaceObject = this.workspaceObject.workspace.get(this.id.Value);
            }

            this.changedRoleByRoleType = null;

            this.roleByRoleType = new Dictionary<RoleType, object>();
        }

        private Data.PushRequestRole[] saveRoles()
        {
            var saveRoles = new List<Data.PushRequestRole>();

            foreach (var keyValuePair in this.changedRoleByRoleType)
            {
                var roleType = keyValuePair.Key;
                var role = keyValuePair.Value;

                var saveRole = new Data.PushRequestRole { t = roleType.PropertyName };

                if (roleType.ObjectType.IsUnit)
                {
                    saveRole.s = role;
                }
                else
                {
                    if (roleType.IsOne)
                    {
                        var sessionRole = (SessionObject)role;
                        saveRole.s = sessionRole?.id?.ToString() ?? sessionRole?.newId?.ToString();
                    }
                    else
                    {
                        var sessionRoles = (SessionObject[])role;
                        var roleIds = sessionRoles.Select(item => (item.id ?? item.newId).ToString()).ToArray();
                        if (this.newId != null)
                        {
                            saveRole.a = roleIds;
                        }
                        else
                        {
                            object originalRoleIdsObject;
                            if (!this.workspaceObject.roles.TryGetValue(roleType.PropertyName, out originalRoleIdsObject))
                            {
                                saveRole.a = roleIds;
                            }
                            else
                            {
                                var originalRoleIds = (string[])originalRoleIdsObject;
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