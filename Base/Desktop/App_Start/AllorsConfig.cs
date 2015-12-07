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
                var accessControlPrefetch = new PrefetchPolicyBuilder()
                    .WithRule(AccessControl.Meta.EffectiveUsers)
                    .WithRule(AccessControl.Meta.EffectivePermissions)
                    .Build();

                var securityTokenPrefetch = new PrefetchPolicyBuilder()
                    .WithRule(SecurityToken.Meta.AccessControls, accessControlPrefetch)
                    .Build();

                using (var session = Config.Default.CreateSession())
                {
                    var securityTokens = new SecurityTokens(session).Extent();
                    session.Prefetch(securityTokenPrefetch, securityTokens);
                }
            }
        }
    }
}
