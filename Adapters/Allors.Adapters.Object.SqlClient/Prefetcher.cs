//------------------------------------------------------------------------------------------------- 
// <copyright file="Prefetcher.cs" company="Allors bvba">
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

using Allors;

namespace Allors.Adapters.Object.SqlClient
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    using Allors.Meta;

    internal class Prefetcher
    {
        private readonly DatabaseSession session;
        private readonly PrefetchPolicy prefetchPolicy;
        private readonly List<Reference> references;

        public Prefetcher(DatabaseSession session, PrefetchPolicy prefetchPolicy, List<Reference> references)
        {
            this.session = session;
            this.references = references;
            this.prefetchPolicy = prefetchPolicy;
        }

        private Prefetcher(DatabaseSession session, PrefetchPolicy prefetchPolicy, IEnumerable<ObjectId> objectIds)
        {
            this.session = session;
            this.prefetchPolicy = prefetchPolicy;
            this.references = session.GetReferences(objectIds);
        }

        public void Execute()
        {
            if (this.references.Any(reference => reference.IsUnknownCacheId || !reference.ExistsKnown))
            {
                this.session.GetCacheIdsAndExists();
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
                                List<Reference> classBasedReferences;
                                if (!referencesByClass.TryGetValue(reference.Class, out classBasedReferences))
                                {
                                    classBasedReferences = new List<Reference>();
                                    referencesByClass.Add(reference.Class, classBasedReferences);
                                }

                                classBasedReferences.Add(reference);
                            }

                            foreach (var dictionaryEntry in referencesByClass)
                            {
                                var @class = dictionaryEntry.Key;
                                var classBasedReferences = dictionaryEntry.Value;

                                this.session.PrefetchUnitRoles(@class, classBasedReferences, roleType);
                            }
                        }
                    }
                    else
                    {
                        var nestedPrefetchPolicy = prefetchRule.PrefetchPolicy;
                        var existNestedPrefetchPolicy = nestedPrefetchPolicy != null;
                        var nestedObjectIds = existNestedPrefetchPolicy ? new List<ObjectId>() : null;

                        var relationType = roleType.RelationType;
                        if (roleType.IsOne)
                        {
                            if (relationType.ExistExclusiveClasses)
                            {
                                this.session.PrefetchCompositeRoleObjectTable(this.references, roleType, nestedObjectIds);
                            }
                            else
                            {
                                this.session.PrefetchCompositeRoleRelationTable(this.references, roleType, nestedObjectIds);
                            }
                        }
                        else
                        {
                            var associationType = relationType.AssociationType;
                            if (!(associationType.IsMany && roleType.IsMany) && relationType.ExistExclusiveClasses)
                            {
                                this.session.PrefetchCompositesRoleObjectTable(this.references, roleType, nestedObjectIds);
                            }
                            else
                            {
                                this.session.PrefetchCompositesRoleRelationTable(this.references, roleType, nestedObjectIds);
                            }
                        }

                        if (existNestedPrefetchPolicy)
                        {
                            new Prefetcher(this.session, nestedPrefetchPolicy, nestedObjectIds).Execute();
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
                    var nestedObjectIds = existNestedPrefetchPolicy ? new List<ObjectId>() : null;

                    if (!(associationType.IsMany && roleType.IsMany) && relationType.ExistExclusiveClasses)
                    {
                        if (associationType.IsOne)
                        {
                            this.session.PrefetchCompositeAssociationObjectTable(this.references, associationType, nestedObjectIds);
                        }
                        else
                        {
                            this.session.PrefetchCompositesAssociationObjectTable(this.references, associationType, nestedObjectIds);
                        }
                    }
                    else
                    {
                        if (associationType.IsOne)
                        {
                            this.session.PrefetchCompositeAssociationRelationTable(this.references, associationType, nestedObjectIds);
                        }
                        else
                        {
                            this.session.PrefetchCompositesAssociationRelationTable(this.references, associationType, nestedObjectIds);
                        }
                    }

                    if (existNestedPrefetchPolicy)
                    {
                        new Prefetcher(this.session, nestedPrefetchPolicy, nestedObjectIds).Execute();
                    }
                }
            }
        }
    }
}