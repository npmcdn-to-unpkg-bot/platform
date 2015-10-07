namespace Allors.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Allors.Adapters;
    using Allors.Domain;
    using Allors.Meta;
    using Allors.Web.Workspace;

    public class SaveResponseBuilder
    {
        private readonly ISession session;
        private readonly SaveRequest saveRequest;
        private readonly string @group;
        private readonly User user;

        public SaveResponseBuilder(ISession session, User user, SaveRequest saveRequest, string @group)
        {
            this.session = session;
            this.user = user;
            this.saveRequest = saveRequest;
            this.group = group;
        }

        public SaveResponse Build()
        {
            var saveResponse = new SaveResponse();

            if (this.saveRequest.Objects != null && this.saveRequest.Objects.Length > 0)
            {
                // bulk load all objects
                var objectIds = this.saveRequest.Objects.Select(v => v.I).ToArray();
                this.session.Instantiate(objectIds);

                foreach (var saveRequestObject in this.saveRequest.Objects)
                {
                    var obj = this.session.Instantiate(saveRequestObject.I);
                    var composite = (Composite)obj.Strategy.Class;
                    var roleTypes = composite.RoleTypesByGroup[@group];

                    var acl = new AccessControlList(obj, this.user);

                    if (!saveRequestObject.V.Equals(obj.Strategy.ObjectVersion.ToString()))
                    {
                        saveResponse.AddVersionError(obj);
                    }
                    else
                    {
                        foreach (var saveRequestRole in saveRequestObject.Roles)
                        {
                            var roleTypeName = saveRequestRole.T;
                            var roleType = roleTypes.FirstOrDefault(v => v.SingularPropertyName.Equals(roleTypeName));

                            if (roleType != null)
                            {
                                if (acl.CanWrite(roleType))
                                {
                                    if (roleType.ObjectType.IsUnit)
                                    {
                                        var unitType = (IUnit)roleType.ObjectType;
                                        var role = saveRequestRole.S;
                                        if (role is string)
                                        {
                                            role = Serialization.ReadString((string)role, unitType.UnitTag);
                                        }

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
                                                if (role == null)
                                                {
                                                    saveResponse.AddMissingError(roleId);
                                                }
                                                else
                                                {
                                                    obj.Strategy.SetCompositeRole(roleType, role);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // Set
                                            if (saveRequestRole.S != null)
                                            {
                                                var roleIds = (string[])saveRequestRole.S;
                                                if (roleIds.Length == 0)
                                                {
                                                    obj.Strategy.RemoveCompositeRole(roleType);
                                                }
                                                else
                                                {
                                                    var roles = this.session.Instantiate(roleIds);
                                                    if (roles.Length != roleIds.Length)
                                                    {
                                                        AddMissingRoles(roles, roleIds, saveResponse);
                                                    }
                                                    else
                                                    {
                                                        obj.Strategy.SetCompositeRoles(roleType, roles);
                                                    }
                                                }
                                            }

                                            // Add
                                            if (saveRequestRole.A != null)
                                            {
                                                var roleIds = saveRequestRole.A;
                                                if (roleIds.Length != 0)
                                                {
                                                    var roles = this.session.Instantiate(roleIds);
                                                    if (roles.Length != roleIds.Length)
                                                    {
                                                        AddMissingRoles(roles, roleIds, saveResponse);
                                                    }
                                                    else
                                                    {
                                                        foreach (var role in roles)
                                                        {
                                                            obj.Strategy.AddCompositeRole(roleType, role);
                                                        }
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
                                                    if (roles.Length != roleIds.Length)
                                                    {
                                                        AddMissingRoles(roles, roleIds, saveResponse);
                                                    }
                                                    else
                                                    {
                                                        foreach (var role in roles)
                                                        {
                                                            obj.Strategy.RemoveCompositeRole(roleType, role);

                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    saveResponse.AddAccessError(obj);
                                }
                            }
                        }
                    }
                }

                var derivationLog = this.session.Derive();

                if (derivationLog.HasErrors)
                {
                    saveResponse.AddDerivationErrors(derivationLog);
                }

                if (!saveResponse.HasErrors)
                {
                    this.session.Commit();
                }
            }

            return saveResponse;
        }

        private static void AddMissingRoles(IObject[] actualRoles, string[] requestedRoleIds, SaveResponse saveResponse)
        {
            var actualRoleIds = actualRoles.Select(x => x.Id.ToString());
            var missingRoleIds = requestedRoleIds.Except(actualRoleIds);
            foreach (var missingRoleId in missingRoleIds)
            {
                saveResponse.AddMissingError(missingRoleId);
            }
        }
    }
}