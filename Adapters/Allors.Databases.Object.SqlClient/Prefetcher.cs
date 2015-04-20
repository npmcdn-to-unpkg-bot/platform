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

        private readonly List<Reference> references;

        private readonly bool unitRoleTypes;
        private readonly List<IRoleType> compositeRoleTypes;
        private readonly List<IRoleType> compositesRoleTypes;
        private readonly List<IAssociationType> compositeAssociationTypes;
        private readonly List<IAssociationType> compositesAssociationTypes; 
        
        public Prefetcher(DatabaseSession session, List<Reference> references, IPropertyType[] propertyTypes)
        {
            this.session = session;
            this.references = references;

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
                        if (roleType.IsOne)
                        {
                            if (this.compositeRoleTypes == null)
                            {
                                this.compositeRoleTypes = new List<IRoleType>();
                            }

                            this.compositeRoleTypes.Add(roleType);
                        }
                        else
                        {
                            if (this.compositesRoleTypes == null)
                            {
                                this.compositesRoleTypes = new List<IRoleType>();
                            }

                            this.compositesRoleTypes.Add(roleType);
                        }
                    }
                }
                else
                {
                    var associationType = (IAssociationType)propertyType;

                    if (associationType.IsOne)
                    {
                        if (this.compositeAssociationTypes == null)
                        {
                            this.compositeAssociationTypes = new List<IAssociationType>();
                        }

                        this.compositeAssociationTypes.Add(associationType);
                    }
                    else
                    {
                        if (this.compositesAssociationTypes == null)
                        {
                            this.compositesAssociationTypes = new List<IAssociationType>();
                        }

                        this.compositesAssociationTypes.Add(associationType);
                    }
                }
            }
        }

        public void Execute()
        {
            if (this.unitRoleTypes)
            {
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

                    this.session.PrefetchUnitRoles(@class, classBasedReferences);
                }
            }

            if (this.compositeRoleTypes != null)
            {
                foreach (var roleType in this.compositeRoleTypes)
                {
                    this.session.PrefetchCompositeRole(this.references, roleType);
                }
            }
        }
    }
}