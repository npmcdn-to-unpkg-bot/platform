using System.Web.Configuration;

namespace Desktop
{
    using Allors;
    using Allors.Domain;
    using Allors.Meta;

    public class AllorsConfig
    {
        public static bool IsProduction
        {
            get
            {
                var production = WebConfigurationManager.AppSettings["production"];
                return string.IsNullOrWhiteSpace(production) || bool.Parse(production);
            }
        }

        public static void Register()
        {
            // Initialize meta population
            var metaPopulation = MetaPopulation.Instance;

            if (IsProduction)
            {
                // Warm up caches
                var permissionPrefetcher = new PrefetchPolicyBuilder()
                    .WithRule(Permission.Meta.ConcreteClassPointer)
                    .Build();

                var rolePrefetch = new PrefetchPolicyBuilder()
                    .WithRule(Role.Meta.Permissions, permissionPrefetcher)
                    .WithRule(Role.Meta.Name)
                    .Build();

                var userGroupsPrefetch = new PrefetchPolicyBuilder()
                    .WithRule(UserGroup.Meta.Members)
                    .WithRule(UserGroup.Meta.Name)
                    .Build();

                var accessControlPrefetch = new PrefetchPolicyBuilder()
                    .WithRule(AccessControl.Meta.Objects)
                    .WithRule(AccessControl.Meta.Role, rolePrefetch)
                    .WithRule(AccessControl.Meta.SubjectGroups, userGroupsPrefetch)
                    .WithRule(AccessControl.Meta.Subjects)
                    .Build();

                using (var session = Config.Default.CreateSession())
                {
                    var securityCache = new SecurityCache(session);

                    var accessControls = new AccessControls(session).Extent().ToArray();
                    session.Prefetch(accessControlPrefetch, accessControls);
                }
            }
        }
    }
}
