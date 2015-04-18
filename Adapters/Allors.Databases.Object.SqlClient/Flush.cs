// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Flush.cs" company="Allors bvba">
//   Copyright 2002-2013 Allors bvba.
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
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Databases.Object.SqlClient
{
    using System.Collections.Generic;
    using Allors.Meta;

    internal class Flush
    {
        private const int BatchSize = 1000;
        private readonly DatabaseSession session;

        private Dictionary<IClass, Dictionary<IRoleType, List<UnitRelation>>> setUnitRoleRelationsByIRoleTypeByExclusiveRootClass;
        private Dictionary<IRoleType, List<CompositeRelation>> setCompositeRoleRelationsByIRoleType;
        private Dictionary<IRoleType, List<CompositeRelation>> addCompositeRoleRelationsByIRoleType;
        private Dictionary<IRoleType, List<CompositeRelation>> removeCompositeRoleRelationsByIRoleType;
        private Dictionary<IRoleType, IList<ObjectId>> clearCompositeRoleRelationsByIRoleType;

        internal Flush(DatabaseSession session, Dictionary<Reference, Roles> unsyncedRolesByReference)
        {
            this.session = session;

            foreach (var dictionaryEntry in unsyncedRolesByReference)
            {
                var roles = dictionaryEntry.Value;
                roles.Flush(this);
            }
        }

        internal void Execute()
        {
            if (this.setUnitRoleRelationsByIRoleTypeByExclusiveRootClass != null)
            {
                foreach (var firstDictionaryEntry in this.setUnitRoleRelationsByIRoleTypeByExclusiveRootClass)
                {
                    var exclusiveRootClass = firstDictionaryEntry.Key;
                    var setUnitRoleRelationsByIRoleType = firstDictionaryEntry.Value;
                    foreach (var secondDictionaryEntry in setUnitRoleRelationsByIRoleType)
                    {
                        var roleType = secondDictionaryEntry.Key;
                        var relations = secondDictionaryEntry.Value;
                        if (relations.Count > 0)
                        {
                            this.session.SetUnitRole(relations, exclusiveRootClass, roleType);
                        }
                    }
                }
            }

            this.setUnitRoleRelationsByIRoleTypeByExclusiveRootClass = null;

            if (this.setCompositeRoleRelationsByIRoleType != null)
            {
                foreach (var dictionaryEntry in this.setCompositeRoleRelationsByIRoleType)
                {
                    var roleType = dictionaryEntry.Key;
                    var relations = dictionaryEntry.Value;
                    if (relations.Count > 0)
                    {
                        this.session.SetCompositeRole(relations, roleType);
                    }
                }
            }

            this.setCompositeRoleRelationsByIRoleType = null;

            if (this.addCompositeRoleRelationsByIRoleType != null)
            {
                foreach (var dictionaryEntry in this.addCompositeRoleRelationsByIRoleType)
                {
                    var roleType = dictionaryEntry.Key;
                    var relations = dictionaryEntry.Value;
                    if (relations.Count > 0)
                    {
                        this.session.AddCompositeRole(relations, roleType);
                    }
                }
            }

            this.addCompositeRoleRelationsByIRoleType = null;

            if (this.removeCompositeRoleRelationsByIRoleType != null)
            {
                foreach (var dictionaryEntry in this.removeCompositeRoleRelationsByIRoleType)
                {
                    var roleType = dictionaryEntry.Key;
                    var relations = dictionaryEntry.Value;
                    if (relations.Count > 0)
                    {
                        this.session.RemoveCompositeRole(relations, roleType);
                    }
                }
            }

            this.removeCompositeRoleRelationsByIRoleType = null;

            if (this.clearCompositeRoleRelationsByIRoleType != null)
            {
                foreach (var dictionaryEntry in this.clearCompositeRoleRelationsByIRoleType)
                {
                    var roleType = dictionaryEntry.Key;
                    var relations = dictionaryEntry.Value;
                    if (relations.Count > 0)
                    {
                        this.session.ClearCompositeAndCompositesRole(relations, roleType);
                    }
                }
            }

            this.clearCompositeRoleRelationsByIRoleType = null;
        }

        internal void SetUnitRoles(Roles roles, List<IRoleType> unitRoles)
        {
            roles.Reference.Session.SetUnitRoles(roles, unitRoles);
        }

        internal void SetUnitRole(Reference association, IRoleType roleType, object role)
        {
            if (this.setUnitRoleRelationsByIRoleTypeByExclusiveRootClass == null)
            {
                this.setUnitRoleRelationsByIRoleTypeByExclusiveRootClass = new Dictionary<IClass, Dictionary<IRoleType, List<UnitRelation>>>();
            }

            var exclusiveRootClass = association.ObjectType.ExclusiveClass;

            Dictionary<IRoleType, List<UnitRelation>> setUnitRoleRelationsByIRoleType;
            if (!this.setUnitRoleRelationsByIRoleTypeByExclusiveRootClass.TryGetValue(exclusiveRootClass, out setUnitRoleRelationsByIRoleType))
            {
                setUnitRoleRelationsByIRoleType = new Dictionary<IRoleType, List<UnitRelation>>();
                this.setUnitRoleRelationsByIRoleTypeByExclusiveRootClass[exclusiveRootClass] = setUnitRoleRelationsByIRoleType;
            }

            List<UnitRelation> relations;
            if (!setUnitRoleRelationsByIRoleType.TryGetValue(roleType, out relations))
            {
                relations = new List<UnitRelation>();
                setUnitRoleRelationsByIRoleType[roleType] = relations;
            }

            var unitRelation = new UnitRelation(association.ObjectId, role);
            relations.Add(unitRelation);

            if (relations.Count > BatchSize)
            {
                this.session.SetUnitRole(relations, exclusiveRootClass, roleType);
                relations.Clear();
            }
        }

        internal void SetCompositeRole(Reference association, IRoleType roleType, ObjectId role)
        {
            if (this.setCompositeRoleRelationsByIRoleType == null)
            {
                this.setCompositeRoleRelationsByIRoleType = new Dictionary<IRoleType, List<CompositeRelation>>();
            }

            List<CompositeRelation> relations;
            if (!this.setCompositeRoleRelationsByIRoleType.TryGetValue(roleType, out relations))
            {
                relations = new List<CompositeRelation>();
                this.setCompositeRoleRelationsByIRoleType[roleType] = relations;
            }

            relations.Add(new CompositeRelation(association.ObjectId, role));

            if (relations.Count > BatchSize)
            {
                this.session.SetCompositeRole(relations, roleType);
                relations.Clear();
            }
        }

        internal void AddCompositeRole(Reference association, IRoleType roleType, HashSet<ObjectId> added)
        {
            if (this.addCompositeRoleRelationsByIRoleType == null)
            {
                this.addCompositeRoleRelationsByIRoleType = new Dictionary<IRoleType, List<CompositeRelation>>();
            }

            List<CompositeRelation> relations;
            if (!this.addCompositeRoleRelationsByIRoleType.TryGetValue(roleType, out relations))
            {
                relations = new List<CompositeRelation>();
                this.addCompositeRoleRelationsByIRoleType[roleType] = relations;
            }

            foreach (var roleObjectId in added)
            {
                relations.Add(new CompositeRelation(association.ObjectId, roleObjectId)); 
            }

            if (relations.Count > BatchSize)
            {
                this.session.AddCompositeRole(relations, roleType);
                relations.Clear();
            }
        }

        internal void RemoveCompositeRole(Reference association, IRoleType roleType, HashSet<ObjectId> removed)
        {
            if (this.removeCompositeRoleRelationsByIRoleType == null)
            {
                this.removeCompositeRoleRelationsByIRoleType = new Dictionary<IRoleType, List<CompositeRelation>>();
            }

            List<CompositeRelation> relations;
            if (!this.removeCompositeRoleRelationsByIRoleType.TryGetValue(roleType, out relations))
            {
                relations = new List<CompositeRelation>();
                this.removeCompositeRoleRelationsByIRoleType[roleType] = relations;
            }

            foreach (var roleObjectId in removed)
            {
                relations.Add(new CompositeRelation(association.ObjectId, roleObjectId));
            }

            if (relations.Count > BatchSize)
            {
                this.session.RemoveCompositeRole(relations, roleType);
                relations.Clear();
            }
        }

        internal void ClearCompositeAndCompositesRole(Reference association, IRoleType roleType)
        {
            if (this.clearCompositeRoleRelationsByIRoleType == null)
            {
                this.clearCompositeRoleRelationsByIRoleType = new Dictionary<IRoleType, IList<ObjectId>>();
            }

            IList<ObjectId> relations;
            if (!this.clearCompositeRoleRelationsByIRoleType.TryGetValue(roleType, out relations))
            {
                relations = new List<ObjectId>();
                this.clearCompositeRoleRelationsByIRoleType[roleType] = relations;
            }

            relations.Add(association.ObjectId);

            if (relations.Count > BatchSize)
            {
                this.session.ClearCompositeAndCompositesRole(relations, roleType);
                relations.Clear();
            }
        }
    }
}