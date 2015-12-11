//------------------------------------------------------------------------------------------------- 
// <copyright file="Prefetch.cs" company="Allors bvba">
// Copyright 2002-2013 Allors bvba.
// 
// Dual Licensed under
//   a) the Lesser General Public Licence v3 (LGPL)
//   b) the Allors License
// 
// The LGPL License is included in the file lgpl.txt.
// The Allors License is an addendum to your contract.
// 
// Allors Platform is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// For more information visit http://www.allors.com/legal
// </copyright>
// <summary>Defines the Session type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors.Adapters.Object.SqlClient
{
    using System.Collections.Generic;
    using System.Linq;

    using Allors;
    using Allors.Meta;

    internal class Prefetch
    {
        private readonly Prefetcher prefetcher;
        private readonly PrefetchPolicy prefetchPolicy;
        private readonly HashSet<Reference> references;

        private readonly bool isRoot;
        private readonly HashSet<long> leafs;

        public Prefetch(Prefetcher prefetcher, PrefetchPolicy prefetchPolicy, HashSet<Reference> references)
        {
            this.prefetcher = prefetcher;
            this.references = references;
            this.prefetchPolicy = prefetchPolicy;

            this.isRoot = true;
            this.leafs = new HashSet<long>();
        }

        private Prefetch(Prefetcher prefetcher, PrefetchPolicy prefetchPolicy, IEnumerable<long> objectIds, HashSet<long> leafs)
        {
            this.prefetcher = prefetcher;
            this.prefetchPolicy = prefetchPolicy;
            this.references = prefetcher.GetReferencesForPrefetching(objectIds);

            this.isRoot = false;
            this.leafs = leafs;
        }

        public void Execute()
        {
            if (this.references.Any(reference => reference.IsUnknownVersion || !reference.ExistsKnown))
            {
                this.prefetcher.Session.GetVersionAndExists();
            }

            var unitRoles = false;
            foreach (var prefetchRule in this.prefetchPolicy)
            {
                var propertyType = prefetchRule.PropertyType;
                if (propertyType is IRoleType)
                {
                    var roleType = (IRoleType)propertyType;
                    var objectType = roleType.ObjectType;
                    if (objectType.IsUnit)
                    {
                        if (!unitRoles)
                        {
                            unitRoles = true;

                            var referencesByClass = new Dictionary<IClass, List<Reference>>();
                            foreach (var reference in this.references)
                            {
                                List<Reference> classedReferences;
                                if (!referencesByClass.TryGetValue(reference.Class, out classedReferences))
                                {
                                    classedReferences = new List<Reference>();
                                    referencesByClass.Add(reference.Class, classedReferences);
                                }

                                classedReferences.Add(reference);
                            }

                            foreach (var dictionaryEntry in referencesByClass)
                            {
                                var @class = dictionaryEntry.Key;
                                var classedReferences = dictionaryEntry.Value;

                                var referencesWithoutCachedRole = new HashSet<Reference>();
                                foreach (var reference in classedReferences)
                                {
                                    var roles = this.prefetcher.Session.State.GetOrCreateRoles(reference);
                                    object role;
                                    if (!roles.TryGetUnitRole(roleType, out role))
                                    {
                                        referencesWithoutCachedRole.Add(reference);
                                    }
                                }

                                this.prefetcher.PrefetchUnitRoles(@class, referencesWithoutCachedRole, roleType);
                            }
                        }
                    }
                    else
                    {
                        var nestedPrefetchPolicy = prefetchRule.PrefetchPolicy;
                        var existNestedPrefetchPolicy = nestedPrefetchPolicy != null;
                        var nestedObjectIds = existNestedPrefetchPolicy ? new HashSet<long>() : null;

                        var relationType = roleType.RelationType;
                        if (roleType.IsOne)
                        {
                            if (relationType.ExistExclusiveClasses)
                            {
                                this.prefetcher.PrefetchCompositeRoleObjectTable(this.references, roleType, nestedObjectIds, leafs);
                            }
                            else
                            {
                                this.prefetcher.PrefetchCompositeRoleRelationTable(this.references, roleType, nestedObjectIds, leafs);
                            }
                        }
                        else
                        {
                            var associationType = relationType.AssociationType;
                            if (!(associationType.IsMany && roleType.IsMany) && relationType.ExistExclusiveClasses)
                            {
                                this.prefetcher.PrefetchCompositesRoleObjectTable(this.references, roleType, nestedObjectIds, leafs);
                            }
                            else
                            {
                                this.prefetcher.PrefetchCompositesRoleRelationTable(this.references, roleType, nestedObjectIds, leafs);
                            }
                        }

                        if (existNestedPrefetchPolicy)
                        {
                            new Prefetch(this.prefetcher, nestedPrefetchPolicy, nestedObjectIds, this.leafs).Execute();
                        }
                    }
                }
                else
                {
                    var associationType = (IAssociationType)propertyType;
                    var relationType = associationType.RelationType;
                    var roleType = relationType.RoleType;

                    var nestedPrefetchPolicy = prefetchRule.PrefetchPolicy;
                    var existNestedPrefetchPolicy = nestedPrefetchPolicy != null;
                    var nestedObjectIds = existNestedPrefetchPolicy ? new HashSet<long>() : null;

                    if (!(associationType.IsMany && roleType.IsMany) && relationType.ExistExclusiveClasses)
                    {
                        if (associationType.IsOne)
                        {
                            this.prefetcher.PrefetchCompositeAssociationObjectTable(this.references, associationType, nestedObjectIds, this.leafs);
                        }
                        else
                        {
                            this.prefetcher.PrefetchCompositesAssociationObjectTable(this.references, associationType, nestedObjectIds, this.leafs);
                        }
                    }
                    else
                    {
                        if (associationType.IsOne)
                        {
                            this.prefetcher.PrefetchCompositeAssociationRelationTable(this.references, associationType, nestedObjectIds, this.leafs);
                        }
                        else
                        {
                            this.prefetcher.PrefetchCompositesAssociationRelationTable(this.references, associationType, nestedObjectIds, this.leafs);
                        }
                    }

                    if (existNestedPrefetchPolicy)
                    {
                        new Prefetch(this.prefetcher, nestedPrefetchPolicy, nestedObjectIds, this.leafs).Execute();
                    }
                }
            }

            if (this.isRoot && this.leafs.Count > 0)
            {
                this.prefetcher.GetReferencesForPrefetching(this.leafs);
            }
        }
    }
}