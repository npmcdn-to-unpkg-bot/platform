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

namespace Allors.Adapters.Database.SqlClient
{
    using System.Collections.Generic;

    using Allors.Adapters.Database.Sql;
    using Allors.Meta;

    public class Flush : IFlush
    {
        private const int BatchSize = 1000;
        private readonly DatabaseSession session;

        private Dictionary<ObjectType, Dictionary<RoleType, List<UnitRelation>>> setUnitRoleRelationsByRoleTypeByExclusiveLeafClass;
        private Dictionary<RoleType, List<CompositeRelation>> setCompositeRoleRelationsByRoleType;
        private Dictionary<RoleType, List<CompositeRelation>> addCompositeRoleRelationsByRoleType;
        private Dictionary<RoleType, List<CompositeRelation>> removeCompositeRoleRelationsByRoleType;
        private Dictionary<RoleType, IList<ObjectId>> clearCompositeRoleRelationsByRoleType;

        public Flush(DatabaseSession session, Dictionary<Reference, Roles> unsyncedRolesByReference)
        {
            this.session = session;

            foreach (var dictionaryEntry in unsyncedRolesByReference)
            {
                var roles = dictionaryEntry.Value;
                roles.Flush(this);
            }
        }

        public void Execute()
        {
            if (this.setUnitRoleRelationsByRoleTypeByExclusiveLeafClass != null)
            {
                foreach (var firstDictionaryEntry in this.setUnitRoleRelationsByRoleTypeByExclusiveLeafClass)
                {
                    var exclusiveLeafClass = firstDictionaryEntry.Key;
                    var setUnitRoleRelationsByRoleType = firstDictionaryEntry.Value;
                    foreach (var secondDictionaryEntry in setUnitRoleRelationsByRoleType)
                    {
                        var roleType = secondDictionaryEntry.Key;
                        var relations = secondDictionaryEntry.Value;
                        if (relations.Count > 0)
                        {
                            this.session.SessionCommands.SetUnitRoleCommand.Execute(relations, exclusiveLeafClass, roleType);
                        }
                    }
                }
            }

            this.setUnitRoleRelationsByRoleTypeByExclusiveLeafClass = null;

            if (this.setCompositeRoleRelationsByRoleType != null)
            {
                foreach (var dictionaryEntry in this.setCompositeRoleRelationsByRoleType)
                {
                    var roleType = dictionaryEntry.Key;
                    var relations = dictionaryEntry.Value;
                    if (relations.Count > 0)
                    {
                        this.session.SessionCommands.SetCompositeRoleCommand.Execute(relations, roleType);
                    }
                }
            }

            this.setCompositeRoleRelationsByRoleType = null;

            if (this.addCompositeRoleRelationsByRoleType != null)
            {
                foreach (var dictionaryEntry in this.addCompositeRoleRelationsByRoleType)
                {
                    var roleType = dictionaryEntry.Key;
                    var relations = dictionaryEntry.Value;
                    if (relations.Count > 0)
                    {
                        this.session.SessionCommands.AddCompositeRoleCommand.Execute(relations, roleType);
                    }
                }
            }

            this.addCompositeRoleRelationsByRoleType = null;

            if (this.removeCompositeRoleRelationsByRoleType != null)
            {
                foreach (var dictionaryEntry in this.removeCompositeRoleRelationsByRoleType)
                {
                    var roleType = dictionaryEntry.Key;
                    var relations = dictionaryEntry.Value;
                    if (relations.Count > 0)
                    {
                        this.session.SessionCommands.RemoveCompositeRoleCommand.Execute(relations, roleType);
                    }
                }
            }

            this.removeCompositeRoleRelationsByRoleType = null;

            if (this.clearCompositeRoleRelationsByRoleType != null)
            {
                foreach (var dictionaryEntry in this.clearCompositeRoleRelationsByRoleType)
                {
                    var roleType = dictionaryEntry.Key;
                    var relations = dictionaryEntry.Value;
                    if (relations.Count > 0)
                    {
                        this.session.SessionCommands.ClearCompositeAndCompositesRoleCommand.Execute(relations, roleType);
                    }
                }
            }

            this.clearCompositeRoleRelationsByRoleType = null;
        }

        public void SetUnitRoles(Roles roles, List<RoleType> unitRoles)
        {
            roles.Reference.Session.SessionCommands.SetUnitRolesCommand.Execute(roles, unitRoles);
        }

        public void SetUnitRole(Reference association, RoleType roleType, object role)
        {
            if (this.setUnitRoleRelationsByRoleTypeByExclusiveLeafClass == null)
            {
                this.setUnitRoleRelationsByRoleTypeByExclusiveLeafClass = new Dictionary<ObjectType, Dictionary<RoleType, List<UnitRelation>>>();
            }

            var exclusiveLeafClass = association.ObjectType.ExclusiveLeafClass;

            Dictionary<RoleType, List<UnitRelation>> setUnitRoleRelationsByRoleType;
            if (!this.setUnitRoleRelationsByRoleTypeByExclusiveLeafClass.TryGetValue(exclusiveLeafClass, out setUnitRoleRelationsByRoleType))
            {
                setUnitRoleRelationsByRoleType = new Dictionary<RoleType, List<UnitRelation>>();
                this.setUnitRoleRelationsByRoleTypeByExclusiveLeafClass[exclusiveLeafClass] = setUnitRoleRelationsByRoleType;
            }

            List<UnitRelation> relations;
            if (!setUnitRoleRelationsByRoleType.TryGetValue(roleType, out relations))
            {
                relations = new List<UnitRelation>();
                setUnitRoleRelationsByRoleType[roleType] = relations;
            }

            var unitRelation = new UnitRelation(association.ObjectId, role);
            relations.Add(unitRelation);

            if (relations.Count > BatchSize)
            {
                this.session.SessionCommands.SetUnitRoleCommand.Execute(relations, exclusiveLeafClass, roleType);
                relations.Clear();
            }
        }

        public void SetCompositeRole(Reference association, RoleType roleType, ObjectId role)
        {
            if (this.setCompositeRoleRelationsByRoleType == null)
            {
                this.setCompositeRoleRelationsByRoleType = new Dictionary<RoleType, List<CompositeRelation>>();
            }

            List<CompositeRelation> relations;
            if (!this.setCompositeRoleRelationsByRoleType.TryGetValue(roleType, out relations))
            {
                relations = new List<CompositeRelation>();
                this.setCompositeRoleRelationsByRoleType[roleType] = relations;
            }

            relations.Add(new CompositeRelation(association.ObjectId, role));

            if (relations.Count > BatchSize)
            {
                this.session.SessionCommands.SetCompositeRoleCommand.Execute(relations, roleType);
                relations.Clear();
            }
        }

        public void AddCompositeRole(Reference association, RoleType roleType, HashSet<ObjectId> added)
        {
            if (this.addCompositeRoleRelationsByRoleType == null)
            {
                this.addCompositeRoleRelationsByRoleType = new Dictionary<RoleType, List<CompositeRelation>>();
            }

            List<CompositeRelation> relations;
            if (!this.addCompositeRoleRelationsByRoleType.TryGetValue(roleType, out relations))
            {
                relations = new List<CompositeRelation>();
                this.addCompositeRoleRelationsByRoleType[roleType] = relations;
            }

            foreach (var roleObjectId in added)
            {
                relations.Add(new CompositeRelation(association.ObjectId, roleObjectId)); 
            }

            if (relations.Count > BatchSize)
            {
                this.session.SessionCommands.AddCompositeRoleCommand.Execute(relations, roleType);
                relations.Clear();
            }
        }

        public void RemoveCompositeRole(Reference association, RoleType roleType, HashSet<ObjectId> removed)
        {
            if (this.removeCompositeRoleRelationsByRoleType == null)
            {
                this.removeCompositeRoleRelationsByRoleType = new Dictionary<RoleType, List<CompositeRelation>>();
            }

            List<CompositeRelation> relations;
            if (!this.removeCompositeRoleRelationsByRoleType.TryGetValue(roleType, out relations))
            {
                relations = new List<CompositeRelation>();
                this.removeCompositeRoleRelationsByRoleType[roleType] = relations;
            }

            foreach (var roleObjectId in removed)
            {
                relations.Add(new CompositeRelation(association.ObjectId, roleObjectId));
            }

            if (relations.Count > BatchSize)
            {
                this.session.SessionCommands.RemoveCompositeRoleCommand.Execute(relations, roleType);
                relations.Clear();
            }
        }

        public void ClearCompositeAndCompositesRole(Reference association, RoleType roleType)
        {
            if (this.clearCompositeRoleRelationsByRoleType == null)
            {
                this.clearCompositeRoleRelationsByRoleType = new Dictionary<RoleType, IList<ObjectId>>();
            }

            IList<ObjectId> relations;
            if (!this.clearCompositeRoleRelationsByRoleType.TryGetValue(roleType, out relations))
            {
                relations = new List<ObjectId>();
                this.clearCompositeRoleRelationsByRoleType[roleType] = relations;
            }

            relations.Add(association.ObjectId);

            if (relations.Count > BatchSize)
            {
                this.session.SessionCommands.ClearCompositeAndCompositesRoleCommand.Execute(relations, roleType);
                relations.Clear();
            }
        }
    }
}