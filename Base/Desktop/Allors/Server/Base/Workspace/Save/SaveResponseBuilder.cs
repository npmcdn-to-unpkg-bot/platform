using Antlr.Runtime;

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

            Dictionary<string, IObject> objectByNewId = null;

            if (this.saveRequest.NewObjects != null && this.saveRequest.NewObjects.Length > 0)
            {
                objectByNewId = this.saveRequest.NewObjects.ToDictionary(x => x.NI, x =>
                {
                    var cls = session.Database.MetaPopulation.FindClassByName(x.T);
                    return (IObject)Allors.ObjectBuilder.Build(session,cls);
                });
            }

            if (this.saveRequest.Objects != null && this.saveRequest.Objects.Length > 0)
            {
                // bulk load all objects
                var objectIds = this.saveRequest.Objects.Select(v => v.I).ToArray();
                this.session.Instantiate(objectIds);

                foreach (var saveRequestObject in this.saveRequest.Objects)
                {
                    var obj = this.session.Instantiate(saveRequestObject.I);

                    if (!saveRequestObject.V.Equals(obj.Strategy.ObjectVersion.ToString()))
                    {
                        saveResponse.AddVersionError(obj);
                    }
                    else
                    {
                        var saveRequestRoles = saveRequestObject.Roles;
                        SaveRequestRoles(saveRequestRoles, obj, saveResponse, objectByNewId);
                    }
                }
            }

            if (objectByNewId != null)
            {
                foreach (var saveRequestNewObject in this.saveRequest.NewObjects)
                {
                    var obj = objectByNewId[saveRequestNewObject.NI];
                    var saveRequestRoles = saveRequestNewObject.Roles;
                    if (saveRequestRoles != null)
                    {
                        SaveRequestRoles(saveRequestRoles, obj, saveResponse, objectByNewId);
                    }
                }
            }
            
            var validation = this.session.Derive();

            if (validation.HasErrors)
            {
                saveResponse.AddDerivationErrors(validation);
            }

            if (!saveResponse.HasErrors)
            {
                if (objectByNewId != null)
                {
                    saveResponse.NewObjects = objectByNewId.Select(dictionaryEntry => new SaveResponseNewObject
                    {
                        I = dictionaryEntry.Value.Id.ToString(),
                        NI = dictionaryEntry.Key
                    }).ToArray();
                }

                this.session.Commit();
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

        private void SaveRequestRoles(IList<SaveRequestRole> saveRequestRoles, IObject obj, SaveResponse saveResponse, Dictionary<string, IObject> objectByNewId)
        {
            foreach (var saveRequestRole in saveRequestRoles)
            {
                var composite = (Composite)obj.Strategy.Class;
                var roleTypes = composite.RoleTypesByGroup[@group];
                var acl = new AccessControlList(obj, this.user);

                var roleTypeName = saveRequestRole.T;
                var roleType = roleTypes.FirstOrDefault(v => v.PropertyName.Equals(roleTypeName));

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

                            obj.Strategy.SetUnitRole(roleType.RelationType, role);
                        }
                        else
                        {
                            if (roleType.IsOne)
                            {
                                var roleId = (string)saveRequestRole.S;
                                if (string.IsNullOrEmpty(roleId))
                                {
                                    obj.Strategy.RemoveCompositeRole(roleType.RelationType);
                                }
                                else
                                {
                                    var role = GetRole(roleId, objectByNewId);
                                    if (role == null)
                                    {
                                        saveResponse.AddMissingError(roleId);
                                    }
                                    else
                                    {
                                        obj.Strategy.SetCompositeRole(roleType.RelationType, role);
                                    }
                                }
                            }
                            else
                            {
                                // Add
                                if (saveRequestRole.A != null)
                                {
                                    var roleIds = saveRequestRole.A;
                                    if (roleIds.Length != 0)
                                    {
                                        var roles = GetRoles(roleIds, objectByNewId);
                                        if (roles.Length != roleIds.Length)
                                        {
                                            AddMissingRoles(roles, roleIds, saveResponse);
                                        }
                                        else
                                        {
                                            foreach (var role in roles)
                                            {
                                                obj.Strategy.AddCompositeRole(roleType.RelationType, role);
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
                                        var roles = GetRoles(roleIds, objectByNewId);
                                        if (roles.Length != roleIds.Length)
                                        {
                                            AddMissingRoles(roles, roleIds, saveResponse);
                                        }
                                        else
                                        {
                                            foreach (var role in roles)
                                            {
                                                obj.Strategy.RemoveCompositeRole(roleType.RelationType, role);
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

        private IObject GetRole(string roleId, Dictionary<string, IObject> objectByNewId)
        {
            IObject role;
            if (objectByNewId == null || !objectByNewId.TryGetValue(roleId, out role))
            {
                role = this.session.Instantiate(roleId);
            }
            return role;
        }

        private IObject[] GetRoles(string[] roleIds, Dictionary<string, IObject> objectByNewId)
        {
            if (objectByNewId == null)
            {
                return this.session.Instantiate(roleIds);
            }

            var roles = new List<IObject>();
            List<string> existingRoleIds = null;
            foreach (var roleId in roleIds)
            {
                IObject role;
                if (objectByNewId.TryGetValue(roleId, out role))
                {
                    roles.Add(role);
                }
                else
                {
                    if (existingRoleIds == null)
                    {
                        existingRoleIds = new List<string>();
                    }

                    existingRoleIds.Add(roleId);
                }
            }

            if (existingRoleIds != null)
            {
                var existingRoles = this.session.Instantiate(existingRoleIds.ToArray());
                roles.AddRange(existingRoles);
            }

            return roles.ToArray();
        }
    }
}