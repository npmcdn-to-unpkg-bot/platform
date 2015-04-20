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

namespace Allors.Databases.Object.SqlClient
{
    using System.Collections.Generic;

    using Allors.Meta;

    internal class Prefetcher
    {
        private readonly DatabaseSession session;

        private readonly bool unitRoleTypes;
        private readonly List<IRoleType> compositeRoleTypes;
        private readonly List<IAssociationType> compositeAssociationTypes; 
        
        private readonly Dictionary<IClass, List<Reference>> referencesByClass;

        public Prefetcher(DatabaseSession session, List<Reference> references, IPropertyType[] propertyTypes)
        {
            this.session = session;

            foreach (var propertyType in propertyTypes)
            {
                var roleType = propertyType as IRoleType;
                if (roleType != null)
                {
                    var objectType = roleType.ObjectType;
                    if (objectType.IsUnit)
                    {
                        this.unitRoleTypes = true;
                    }
                    else
                    {
                        if (this.compositeRoleTypes == null)
                        {
                            this.compositeRoleTypes = new List<IRoleType>();
                        }

                        this.compositeRoleTypes.Add(roleType);
                    }
                }
                else
                {
                    var associationType = (IAssociationType)propertyType;
                    if (this.compositeAssociationTypes == null)
                    {
                        this.compositeAssociationTypes = new List<IAssociationType>();
                    }

                    this.compositeAssociationTypes.Add(associationType);
                }
            }

            this.referencesByClass = new Dictionary<IClass, List<Reference>>();
            foreach (var reference in references)
            {
                List<Reference> refs;
                if (!this.referencesByClass.TryGetValue(reference.Class, out refs))
                {
                    refs = new List<Reference>();
                    this.referencesByClass.Add(reference.Class, refs);
                }

                refs.Add(reference);
            }
        }

        public void Execute()
        {
            foreach (var dictionaryEntry in this.referencesByClass)
            {
                var @class = dictionaryEntry.Key;
                var references = dictionaryEntry.Value;

                if (this.unitRoleTypes)
                {
                    this.session.PrefetchUnitRoles(@class, references);
                }

                if (this.compositeRoleTypes != null)
                {
                    foreach (var roleType in this.compositeRoleTypes)
                    {
                        this.session.PrefetchCompositeRoles(@class, references, roleType);
                    }
                }

                if (this.compositeAssociationTypes != null)
                {
                    foreach (var associationType in this.compositeAssociationTypes)
                    {
                        this.session.PrefetchCompositeAssociations(@class, references, associationType);
                    }
                }

            }
        }
    }
}