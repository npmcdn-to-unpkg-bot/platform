namespace Allors.Web
{
    using System.Collections.Generic;
    using System.Linq;
    using Allors.Domain;
    using Allors.Meta;
    using Allors.Web.Workspace;

    public class LoadResponseBuilder
    {
        private static readonly object[][] EmptyRoles = {};

        private readonly ISession session;
        private readonly LoadRequest loadRequest;
        private string @group;
        private User user;

        public LoadResponseBuilder(ISession session, User user, LoadRequest loadRequest, string @group)
        {
            this.session = session;
            this.user = user;
            this.loadRequest = loadRequest;
            this.group = group;
        }

        public LoadResponse Build()
        {
            var objects = this.session.Instantiate(loadRequest.Objects);

            return new LoadResponse
            {
                Objects = objects.Select(x => new LoadResponseObject
                {
                    Id = x.Id.ToString(),
                    Version = x.Strategy.ObjectVersion.ToString(),
                    ObjectType = x.Strategy.Class.Name,
                    Roles = GetRoles(x),
                }).ToArray() 
            };
        }

        private object[][] GetRoles(IObject obj)
        {
            var composite = (Composite)obj.Strategy.Class;
            var roleTypes = composite.RoleTypesByGroup[@group];

            if (roleTypes.Count > 0)
            {
                AccessControlList acl = null;
                if (obj is AccessControlledObject)
                {
                    acl = new AccessControlList(obj, this.user);
                }

                var roles = new List<object[]>();
                foreach (var roleType in roleTypes)
                {
                    var propertyName = roleType.SingularPropertyName;

                    var canRead = acl == null || acl.CanRead(roleType);
                    var canWrite = acl != null && acl.CanWrite(roleType);
                    var access = ((canRead ? "r" : string.Empty) + (canWrite ? "w" : string.Empty));

                    if (canRead)
                    {
                        if (roleType.ObjectType.IsUnit)
                        {
                            var role = obj.Strategy.GetUnitRole(roleType);
                            if (role != null)
                            {
                                roles.Add(new[] { propertyName, access, role });
                            }

                        }
                        else
                        {
                            if (roleType.IsOne)
                            {
                                var role = obj.Strategy.GetCompositeRole(roleType);
                                if (role != null)
                                {
                                    roles.Add(new object[] { propertyName, access, role.Id.ToString() });
                                }
                            }
                            else
                            {
                                var role = obj.Strategy.GetCompositeRoles(roleType);
                                if (role.Count != 0)
                                {
                                    var ids = role.Cast<IObject>().Select(roleObject => roleObject.Id.ToString()).ToList();
                                    roles.Add(new object[] { propertyName, access, ids });
                                }
                            }

                        }
                    }
                    else
                    {
                        roles.Add(new object[] { propertyName, access });
                    }

                }

                return roles.ToArray();
            }

            return EmptyRoles;
        }
    }
}