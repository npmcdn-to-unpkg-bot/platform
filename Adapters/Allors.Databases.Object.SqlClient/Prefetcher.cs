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
        private readonly List<IRoleType> compositeRoleTypesObjectTable;
        private readonly List<IRoleType> compositeRoleTypesRelationTable;
        private readonly List<IRoleType> compositesRoleTypesObjectTable;
        private readonly List<IRoleType> compositesRoleTypesRelationTable;
        private readonly List<IAssociationType> compositeAssociationTypesObjectTable;
        private readonly List<IAssociationType> compositeAssociationTypesRelationTable;
        private readonly List<IAssociationType> compositesAssociationTypesObjectTable;
        private readonly List<IAssociationType> compositesAssociationTypesRelationTable;
        
        public Prefetcher(DatabaseSession session, List<Reference> references, IPropertyType[] propertyTypes)
        {
            this.session = session;
            this.references = references;

            foreach (var propertyType in propertyTypes)
            {
                if (propertyType is IRoleType)
                {
                    var roleType = (IRoleType)propertyType;
                    var objectType = roleType.ObjectType;
                    if (objectType.IsUnit)
                    {
                        this.unitRoleTypes = true;
                    }
                    else
                    {
                        var relationType = roleType.RelationType;
                        if (roleType.IsOne)
                        {
                            if (relationType.ExistExclusiveClasses)
                            {
                                if (this.compositeRoleTypesObjectTable == null)
                                {
                                    this.compositeRoleTypesObjectTable = new List<IRoleType>();
                                }

                                this.compositeRoleTypesObjectTable.Add(roleType);
                            }
                            else
                            {
                                if (this.compositeRoleTypesRelationTable == null)
                                {
                                    this.compositeRoleTypesRelationTable = new List<IRoleType>();
                                }

                                this.compositeRoleTypesRelationTable.Add(roleType);
                            }
                        }
                        else
                        {
                            var associationType = relationType.AssociationType;
                            if (!(associationType.IsMany && roleType.IsMany) && relationType.ExistExclusiveClasses)
                            {
                                if (this.compositesRoleTypesObjectTable == null)
                                {
                                    this.compositesRoleTypesObjectTable = new List<IRoleType>();
                                }

                                this.compositesRoleTypesObjectTable.Add(roleType);
                            }
                            else
                            {
                                if (this.compositesRoleTypesRelationTable == null)
                                {
                                    this.compositesRoleTypesRelationTable = new List<IRoleType>();
                                }

                                this.compositesRoleTypesRelationTable.Add(roleType);
                            }
                        }
                    }
                }
                else
                {
                    var associationType = (IAssociationType)propertyType;
                    var relationType = associationType.RelationType;
                    var roleType = relationType.RoleType;

                    if (!(associationType.IsMany && roleType.IsMany) && relationType.ExistExclusiveClasses)
                    {
                        if (associationType.IsOne)
                        {
                            if (this.compositeAssociationTypesObjectTable == null)
                            {
                                this.compositeAssociationTypesObjectTable = new List<IAssociationType>();
                            }

                            this.compositeAssociationTypesObjectTable.Add(associationType);
                        }
                        else
                        {
                            if (this.compositesAssociationTypesObjectTable == null)
                            {
                                this.compositesAssociationTypesObjectTable = new List<IAssociationType>();
                            }

                            this.compositesAssociationTypesObjectTable.Add(associationType);
                        }
                    }
                    else
                    {
                        if (associationType.IsOne)
                        {
                            if (this.compositeAssociationTypesRelationTable == null)
                            {
                                this.compositeAssociationTypesRelationTable = new List<IAssociationType>();
                            }

                            this.compositeAssociationTypesRelationTable.Add(associationType);
                        }
                        else
                        {
                            if (this.compositesAssociationTypesRelationTable == null)
                            {
                                this.compositesAssociationTypesRelationTable = new List<IAssociationType>();
                            }

                            this.compositesAssociationTypesRelationTable.Add(associationType);
                        }
                    }
                }
            }
        }

        public void Execute()
        {
            this.session.Flush();

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

            if (this.compositeRoleTypesObjectTable != null)
            {
                foreach (var roleType in this.compositeRoleTypesObjectTable)
                {
                    this.session.PrefetchCompositeRoleObjectTable(this.references, roleType);
                }
            }

            if (this.compositeRoleTypesRelationTable != null)
            {
                foreach (var roleType in this.compositeRoleTypesRelationTable)
                {
                    this.session.PrefetchCompositeRoleRelationTable(this.references, roleType);
                }
            }

            if (this.compositesRoleTypesObjectTable != null)
            {
                foreach (var roleType in this.compositesRoleTypesObjectTable)
                {
                    this.session.PrefetchCompositesRoleObjectTable(this.references, roleType);
                }
            }

            if (this.compositesRoleTypesRelationTable != null)
            {
                foreach (var roleType in this.compositesRoleTypesRelationTable)
                {
                    this.session.PrefetchCompositesRoleRelationTable(this.references, roleType);
                }
            }

            if (this.compositeAssociationTypesObjectTable != null)
            {
                foreach (var associationType in this.compositeAssociationTypesObjectTable)
                {
                    this.session.PrefetchCompositeAssociationObjectTable(this.references, associationType);
                }
            }

            if (this.compositesAssociationTypesObjectTable != null)
            {
                foreach (var associationType in this.compositesAssociationTypesObjectTable)
                {
                    this.session.PrefetchCompositesAssociationObjectTable(this.references, associationType);
                }
            }

            if (this.compositeAssociationTypesRelationTable != null)
            {
                foreach (var associationType in this.compositeAssociationTypesRelationTable)
                {
                    this.session.PrefetchCompositeAssociationRelationTable(this.references, associationType);
                }
            }

            if (this.compositesAssociationTypesRelationTable != null)
            {
                foreach (var associationType in this.compositesAssociationTypesRelationTable)
                {
                    this.session.PrefetchCompositesAssociationRelationTable(this.references, associationType);
                }
            }
        }
    }
}