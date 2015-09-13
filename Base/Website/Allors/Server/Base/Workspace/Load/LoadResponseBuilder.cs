using System.Collections.Generic;
using System.Linq;
using Allors.Web.Workspace;

namespace Allors.Web
{
    public class LoadResponseBuilder
    {
        private readonly ISession session;
        private readonly LoadRequest loadRequest;

        public LoadResponseBuilder(ISession session, LoadRequest loadRequest)
        {
            this.session = session;
            this.loadRequest = loadRequest;
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
            var roles = new List<object[]>();
            foreach (var roleType in obj.Strategy.Class.RoleTypes)
            {
                if (roleType.ObjectType.IsUnit)
                {
                    var propertyName = roleType.SingularPropertyName;
                    var role = obj.Strategy.GetUnitRole(roleType);
                    if (role != null)
                    {
                        roles.Add(new[] { propertyName, role });
                    }

                }
                else
                {
                    if (roleType.IsOne)
                    {
                        var propertyName = roleType.SingularPropertyName;
                        var role = obj.Strategy.GetCompositeRole(roleType);
                        if (role != null)
                        {
                            roles.Add(new object[] { propertyName, role.Id.ToString() });
                        }
                    }
                    else
                    {
                        var propertyName = roleType.SingularPropertyName;
                        var role = obj.Strategy.GetCompositeRoles(roleType);
                        if (role.Count != 0)
                        {
                            var ids = role.Cast<IObject>().Select(roleObject => roleObject.Id.ToString()).ToList();
                            roles.Add(new object[] { propertyName, ids });
                        }
                    }

                }

            }
            return roles.ToArray();
        }
    }
}