namespace Allors.Web
{
    using System.Collections.Generic;
    using System.Linq;
    using Allors.Domain;
    using Allors.Meta;
    using Allors.Web.Workspace;

    public class SaveResponseBuilder
    {
        private static readonly object[][] EmptyRoles = {};

        private readonly ISession session;
        private readonly SaveRequest saveRequest;
        private string @group;
        private User user;

        public SaveResponseBuilder(ISession session, User user, SaveRequest saveRequest, string @group)
        {
            this.session = session;
            this.user = user;
            this.saveRequest = saveRequest;
            this.group = group;
        }

        public SaveResponse Build()
        {
            if (this.saveRequest.Objects == null || this.saveRequest.Objects.Length == 0)
            {
                return new SaveResponse { HasErrors = false };
            }

            // bulk load all objects
            var objectIds = saveRequest.Objects.Select(v => v.I).ToArray();
            this.session.Instantiate(objectIds);

            var accessErrorRoleTypesByObject = new Dictionary<IObject, IList<RoleType>>();

            foreach (var saveRequestObject in saveRequest.Objects)
            {
                var obj = this.session.Instantiate(saveRequestObject.I);
                var composite = (Composite)obj.Strategy.Class;
                var roleTypes = composite.RoleTypesByGroup[@group];

                var acl = new AccessControlList(obj, this.user);

                foreach (var saveRequestRole in saveRequestObject.Roles)
                {
                    var roleTypeName = saveRequestRole.T;
                    var roleType = roleTypes.FirstOrDefault(v => v.PropertyName.Equals(roleTypeName));

                    if (roleType != null)
                    {
                        if (acl.CanWrite(roleType))
                        {
                            if (roleType.ObjectType.IsUnit)
                            {
                                var role = saveRequestRole.S;
                                obj.Strategy.SetUnitRole(roleType, role);
                            }
                            else
                            {
                                if (roleType.IsOne)
                                {
                                    var roleId = (string)saveRequestRole.S;
                                    if (string.IsNullOrEmpty(roleId))
                                    {
                                        obj.Strategy.RemoveCompositeRole(roleType);
                                    }
                                    else
                                    {
                                        var role = this.session.Instantiate(roleId);
                                        // TODO: log error if role is null
                                        obj.Strategy.SetCompositeRole(roleType, role);
                                    }

                                }
                                else
                                {
                                    // Set
                                    if (saveRequestRole.S!=null)
                                    {
                                        var roleIds = (string[])saveRequestRole.S;
                                        if (roleIds.Length == 0)
                                        {
                                            obj.Strategy.RemoveCompositeRole(roleType);
                                        }
                                        else
                                        {
                                            var roles = this.session.Instantiate(roleIds);
                                            // TODO: log error if roles are missing
                                            obj.Strategy.SetCompositeRoles(roleType, roles);
                                        }
                                    }

                                    // Add
                                    if (saveRequestRole.A != null)
                                    {
                                        var roleIds = saveRequestRole.A;
                                        if (roleIds.Length != 0)
                                        {
                                            var roles = this.session.Instantiate(roleIds);
                                            // TODO: log error if roles are missing
                                            foreach (var role in roles)
                                            {
                                                obj.Strategy.AddCompositeRole(roleType, role);

                                            }

                                        }
                                    }

                                    // Remove
                                    if (saveRequestRole.R != null)
                                    {
                                        var roleIds = saveRequestRole.R;
                                        if (roleIds.Length != 0)
                                        {
                                            var roles = this.session.Instantiate(roleIds);
                                            // TODO: log error if roles are missing
                                            foreach (var role in roles)
                                            {
                                                obj.Strategy.RemoveCompositeRole(roleType, role);

                                            }

                                        }
                                    }
                                }
                            }

                        }
                        else
                        {
                            IList<RoleType> accessErrorRoleTypes;
                            if (!accessErrorRoleTypesByObject.TryGetValue(obj, out accessErrorRoleTypes))
                            {
                                accessErrorRoleTypes = new List<RoleType>();
                                accessErrorRoleTypesByObject.Add(obj, accessErrorRoleTypes);
                            }

                            accessErrorRoleTypes.Add(roleType);
                        }

                    }
                }
            }

            var derivationLog = this.session.Derive();

            var saveResponse = new SaveResponse
            {
                Errors = GetObjects(derivationLog, accessErrorRoleTypesByObject)
            };

            saveResponse.HasErrors = saveResponse.Errors.Count > 0;

            if (!saveResponse.HasErrors)
            {
                this.session.Commit();
            }
            
            return saveResponse;
        }

        private Dictionary<string, Dictionary<string, string[]>> GetObjects(DerivationLog derivationLog, Dictionary<IObject, IList<RoleType>> accessErrorRoleTypesByObject)
        {
            var messagesByRoleTypeByObject = new Dictionary<IObject, Dictionary<RoleType, List<string>>>();

            foreach (var derivationError in derivationLog.Errors)
            {
                var obj = derivationError.Relations[0].Association;

                Dictionary<RoleType, List<string>> messagesByRoleType;
                if (!messagesByRoleTypeByObject.TryGetValue(obj, out messagesByRoleType))
                {
                    messagesByRoleType = new Dictionary<RoleType, List<string>>();
                    messagesByRoleTypeByObject.Add(obj, messagesByRoleType);
                }

                foreach (var roleType in derivationError.RoleTypes)
                {
                    List<string> messages;
                    if (!messagesByRoleType.TryGetValue(roleType, out messages))
                    {
                        messages = new List<string>();
                        messagesByRoleType.Add(roleType, messages);
                    }

                    messages.Add(derivationError.ToString());
                }
            }

            foreach (var kv in accessErrorRoleTypesByObject)
            {
                var obj = kv.Key;
                var accessErrorRoleTypes = kv.Value;

                Dictionary<RoleType, List<string>> messagesByRoleType;
                if (!messagesByRoleTypeByObject.TryGetValue(obj, out messagesByRoleType))
                {
                    messagesByRoleType = new Dictionary<RoleType, List<string>>();
                    messagesByRoleTypeByObject.Add(obj, messagesByRoleType);
                }

                foreach (var roleType in accessErrorRoleTypes)
                {
                    List<string> messages;
                    if (!messagesByRoleType.TryGetValue(roleType, out messages))
                    {
                        messages = new List<string>();
                        messagesByRoleType.Add(roleType, messages);
                    }

                    messages.Add("Access error");
                }
            }
            
            return messagesByRoleTypeByObject.ToDictionary(kvp => kvp.Key.Id.ToString(), kvp => kvp.Value.ToDictionary(kvp2 => kvp2.Key.PropertyName, kvp2=>kvp2.Value.ToArray()));
        }
    }
}