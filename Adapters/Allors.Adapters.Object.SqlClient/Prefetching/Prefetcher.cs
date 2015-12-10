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
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Linq;

    using Allors.Meta;
    
    internal class Prefetcher
    {
        private static readonly ObjectId[] EmptyObjectIds = { };

        public Session Session { get; }

        public Database Database => this.Session.Database;

        #region Fields
        private Dictionary<IClass, Command> prefetchUnitRolesByClass;
        private Dictionary<IRoleType, Command> prefetchCompositeRoleByRoleType;
        private Dictionary<IRoleType, Command> prefetchCompositesRoleByRoleType;
        private Dictionary<IAssociationType, Command> prefetchCompositeAssociationByAssociationType;
        #endregion

        public Prefetcher(Session session)
        {
            this.Session = session;
        }

        #region Properties
        private Dictionary<IClass, Command> PrefetchUnitRolesByClass
        {
            get
            {
                return this.prefetchUnitRolesByClass ?? (this.prefetchUnitRolesByClass = new Dictionary<IClass, Command>());
            }
        }

        private Dictionary<IRoleType, Command> PrefetchCompositeRoleByRoleType
        {
            get
            {
                return this.prefetchCompositeRoleByRoleType ?? (this.prefetchCompositeRoleByRoleType = new Dictionary<IRoleType, Command>());
            }
        }

        private Dictionary<IRoleType, Command> PrefetchCompositesRoleByRoleType
        {
            get
            {
                return this.prefetchCompositesRoleByRoleType ?? (this.prefetchCompositesRoleByRoleType = new Dictionary<IRoleType, Command>());
            }
        }

        private Dictionary<IAssociationType, Command> PrefetchCompositeAssociationByAssociationType
        {
            get
            {
                return this.prefetchCompositeAssociationByAssociationType ?? (this.prefetchCompositeAssociationByAssociationType = new Dictionary<IAssociationType, Command>());
            }
        }
        #endregion


        internal List<Reference> GetReferencesForPrefetching(IEnumerable<ObjectId> objectIds)
        {
            var references = new List<Reference>();

            List<ObjectId> referencesToInstantiate = null;
            foreach (var objectId in objectIds)
            {
                Reference reference;
                this.Session.referenceByObjectId.TryGetValue(objectId, out reference);
                if (reference != null && reference.ExistsKnown && !reference.IsUnknownCacheId)
                {
                    if (reference.Exists && !reference.IsNew)
                    {
                        references.Add(reference);
                    }
                }
                else
                {
                    if (referencesToInstantiate == null)
                    {
                        referencesToInstantiate = new List<ObjectId>();
                    }

                    referencesToInstantiate.Add(objectId);
                }
            }

            if (referencesToInstantiate != null)
            {
                // TODO: Remove dependency from Prefetcher to SessionCommands
                var existsUnknownReferences = this.Session.Commands.InstantiateObjects(referencesToInstantiate);
                references.AddRange(existsUnknownReferences);
            }

            return references;
        }

        internal void ResetCommands()
        {
            this.prefetchUnitRolesByClass = null;
            this.prefetchCompositeRoleByRoleType = null;
            this.prefetchCompositesRoleByRoleType = null;
            this.prefetchCompositeAssociationByAssociationType = null;
        }

        internal void PrefetchUnitRoles(IClass @class, List<Reference> associations, IRoleType anyRoleType)
        {
            var references = this.FilterForPrefetchRoles(associations, anyRoleType);
            if (references.Count == 0)
            {
                return;
            }

            Command command;
            if (!this.PrefetchUnitRolesByClass.TryGetValue(@class, out command))
            {
                var sql = this.Database.Mapping.ProcedureNameForPrefetchUnitRolesByClass[@class];
                command = this.Session.Connection.CreateCommand();
                command.CommandText = sql;
                command.CommandType = CommandType.StoredProcedure;
                command.AddObjectTableParameter(references);
                this.prefetchUnitRolesByClass[@class] = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForTableType].Value = this.Database.CreateObjectTable(references);
            }

            using (DbDataReader reader = command.ExecuteReader())
            {
                var sortedUnitRoles = this.Database.GetSortedUnitRolesByObjectType(@class);
                var cache = this.Database.Cache;

                while (reader.Read())
                {
                    var objectId = this.Database.ObjectIds.Parse(reader[0].ToString());
                    var reference = this.Session.referenceByObjectId[objectId];

                    Roles modifiedRoles = null;
                    if (this.Session.modifiedRolesByReference != null)
                    {
                        this.Session.modifiedRolesByReference.TryGetValue(reference, out modifiedRoles);
                    }

                    var cachedObject = cache.GetOrCreateCachedObject(@class, objectId, reference.CacheId);

                    for (var i = 0; i < sortedUnitRoles.Length; i++)
                    {
                        var roleType = sortedUnitRoles[i];

                        var index = i + 1;
                        object unit = null;
                        if (!reader.IsDBNull(index))
                        {
                            var unitTypeTag = ((IUnit)roleType.ObjectType).UnitTag;
                            switch (unitTypeTag)
                            {
                                case UnitTags.AllorsString:
                                    unit = reader.GetString(index);
                                    break;
                                case UnitTags.AllorsInteger:
                                    unit = reader.GetInt32(index);
                                    break;
                                case UnitTags.AllorsFloat:
                                    unit = reader.GetDouble(index);
                                    break;
                                case UnitTags.AllorsDecimal:
                                    unit = reader.GetDecimal(index);
                                    break;
                                case UnitTags.AllorsDateTime:
                                    var dateTime = reader.GetDateTime(index);
                                    if (dateTime == DateTime.MaxValue || dateTime == DateTime.MinValue)
                                    {
                                        unit = dateTime;
                                    }
                                    else
                                    {
                                        unit = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond, DateTimeKind.Utc);
                                    }

                                    break;
                                case UnitTags.AllorsBoolean:
                                    unit = reader.GetBoolean(index);
                                    break;
                                case UnitTags.AllorsUnique:
                                    unit = reader.GetGuid(index);
                                    break;
                                case UnitTags.AllorsBinary:
                                    var byteArray = (byte[])reader.GetValue(index);
                                    unit = byteArray;
                                    break;
                                default:
                                    throw new ArgumentException("Unknown Unit ObjectType: " + roleType.ObjectType.Name);
                            }
                        }

                        if (modifiedRoles == null || !modifiedRoles.ModifiedRoleByRoleType.ContainsKey(roleType))
                        {
                            cachedObject.SetValue(roleType, unit);
                        }
                    }
                }
            }
        }

        internal void PrefetchCompositeRoleObjectTable(List<Reference> associations, IRoleType roleType, List<ObjectId> nestedObjectIds)
        {
            var references = nestedObjectIds == null ? this.FilterForPrefetchRoles(associations, roleType) : this.FilterForPrefetchCompositeRoles(associations, roleType, nestedObjectIds);
            if (references.Count == 0)
            {
                return;
            }

            Command command;
            if (!this.PrefetchCompositeRoleByRoleType.TryGetValue(roleType, out command))
            {
                command = this.Session.Connection.CreateCommand();
                command.CommandText = this.Database.Mapping.ProcedureNameForPrefetchRoleByRelationType[roleType.RelationType];
                command.CommandType = CommandType.StoredProcedure;
                command.AddObjectTableParameter(references);
                this.prefetchCompositeRoleByRoleType[roleType] = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForTableType].Value = this.Database.CreateObjectTable(references);
            }

            using (DbDataReader reader = command.ExecuteReader())
            {
                var cache = this.Database.Cache;

                while (reader.Read())
                {
                    var associationId = this.Database.ObjectIds.Parse(reader[0].ToString());
                    var associationReference = this.Session.referenceByObjectId[associationId];

                    var cachedObject = cache.GetOrCreateCachedObject(associationReference.Class, associationId, associationReference.CacheId);

                    var roleIdValue = reader[1];

                    if (roleIdValue == null || roleIdValue == DBNull.Value)
                    {
                        cachedObject.SetValue(roleType, null);
                    }
                    else
                    {
                        var roleId = this.Database.ObjectIds.Parse(roleIdValue.ToString());
                        cachedObject.SetValue(roleType, roleId);

                        if (nestedObjectIds != null)
                        {
                            nestedObjectIds.Add(roleId);
                        }
                    }
                }
            }
        }

        internal void PrefetchCompositeRoleRelationTable(List<Reference> associations, IRoleType roleType, List<ObjectId> nestedObjectIds)
        {
            var references = nestedObjectIds == null ? this.FilterForPrefetchRoles(associations, roleType) : this.FilterForPrefetchCompositeRoles(associations, roleType, nestedObjectIds);
            if (references.Count == 0)
            {
                return;
            }

            Command command;
            if (!this.PrefetchCompositeRoleByRoleType.TryGetValue(roleType, out command))
            {
                var sql = this.Database.Mapping.ProcedureNameForPrefetchRoleByRelationType[roleType.RelationType];
                command = this.Session.Connection.CreateCommand();
                command.CommandText = sql;
                command.CommandType = CommandType.StoredProcedure;
                command.AddObjectTableParameter(references);
                this.prefetchCompositeRoleByRoleType[roleType] = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForTableType].Value = this.Database.CreateObjectTable(references);
            }

            var roleByAssociation = new Dictionary<Reference, ObjectId>();
            using (DbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var associationId = this.Database.ObjectIds.Parse(reader[0].ToString());
                    var associationReference = this.Session.referenceByObjectId[associationId];
                    var roleId = this.Database.ObjectIds.Parse(reader[1].ToString());
                    roleByAssociation.Add(associationReference, roleId);
                }
            }

            var cache = this.Database.Cache;
            foreach (var reference in references)
            {
                var cachedObject = cache.GetOrCreateCachedObject(reference.Class, reference.ObjectId, reference.CacheId);

                ObjectId roleId;
                if (roleByAssociation.TryGetValue(reference, out roleId))
                {
                    cachedObject.SetValue(roleType, roleId);

                    if (nestedObjectIds != null)
                    {
                        nestedObjectIds.Add(roleId);
                    }
                }
                else
                {
                    cachedObject.SetValue(roleType, null);
                }
            }
        }

        internal void PrefetchCompositesRoleObjectTable(List<Reference> associations, IRoleType roleType, List<ObjectId> nestedObjectIds)
        {
            var references = nestedObjectIds == null ? this.FilterForPrefetchRoles(associations, roleType) : this.FilterForPrefetchCompositesRoles(associations, roleType, nestedObjectIds);
            if (references.Count == 0)
            {
                return;
            }

            Command command;
            if (!this.PrefetchCompositesRoleByRoleType.TryGetValue(roleType, out command))
            {
                var sql = this.Database.Mapping.ProcedureNameForPrefetchRoleByRelationType[roleType.RelationType];
                command = this.Session.Connection.CreateCommand();
                command.CommandText = sql;
                command.CommandType = CommandType.StoredProcedure;
                command.AddObjectTableParameter(references);
                this.prefetchCompositesRoleByRoleType[roleType] = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForTableType].Value = this.Database.CreateObjectTable(references);
            }

            var rolesByAssociation = new Dictionary<Reference, List<ObjectId>>();
            using (DbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var associationId = this.Database.ObjectIds.Parse(reader[0].ToString());
                    var associationReference = this.Session.referenceByObjectId[associationId];

                    var roleIdValue = reader[1];
                    if (roleIdValue == null || roleIdValue == DBNull.Value)
                    {
                        rolesByAssociation[associationReference] = null;
                    }
                    else
                    {
                        var objectId = this.Database.ObjectIds.Parse(roleIdValue.ToString());
                        List<ObjectId> roles;
                        if (!rolesByAssociation.TryGetValue(associationReference, out roles))
                        {
                            roles = new List<ObjectId>();
                            rolesByAssociation[associationReference] = roles;
                        }

                        roles.Add(objectId);
                    }
                }
            }

            var cache = this.Database.Cache;
            foreach (var dictionaryEntry in rolesByAssociation)
            {
                var association = dictionaryEntry.Key;
                var roles = dictionaryEntry.Value;

                var cachedObject = cache.GetOrCreateCachedObject(association.Class, association.ObjectId, association.CacheId);
                cachedObject.SetValue(roleType, roles == null ? EmptyObjectIds : roles.ToArray());

                if (nestedObjectIds != null)
                {
                    nestedObjectIds.AddRange(roles);
                }
            }
        }

        internal void PrefetchCompositesRoleRelationTable(List<Reference> associations, IRoleType roleType, List<ObjectId> nestedObjectIds)
        {
            var references = nestedObjectIds == null ? this.FilterForPrefetchRoles(associations, roleType) : this.FilterForPrefetchCompositesRoles(associations, roleType, nestedObjectIds);
            if (references.Count == 0)
            {
                return;
            }

            Command command;
            if (!this.PrefetchCompositesRoleByRoleType.TryGetValue(roleType, out command))
            {
                var sql = this.Database.Mapping.ProcedureNameForPrefetchRoleByRelationType[roleType.RelationType];
                command = this.Session.Connection.CreateCommand();
                command.CommandText = sql;
                command.CommandType = CommandType.StoredProcedure;
                command.AddObjectTableParameter(references);
                this.prefetchCompositesRoleByRoleType[roleType] = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForTableType].Value = this.Database.CreateObjectTable(references);
            }

            var rolesByAssociation = new Dictionary<Reference, List<ObjectId>>();
            using (DbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var associationId = this.Database.ObjectIds.Parse(reader[0].ToString());
                    var associationReference = this.Session.referenceByObjectId[associationId];

                    var role = this.Database.ObjectIds.Parse(reader[1].ToString());
                    List<ObjectId> roles;
                    if (!rolesByAssociation.TryGetValue(associationReference, out roles))
                    {
                        roles = new List<ObjectId>();
                        rolesByAssociation[associationReference] = roles;
                    }

                    roles.Add(role);
                }
            }

            var cache = this.Database.Cache;
            foreach (var reference in references)
            {
                Roles modifiedRoles = null;
                if (this.Session.modifiedRolesByReference != null)
                {
                    this.Session.modifiedRolesByReference.TryGetValue(reference, out modifiedRoles);
                }

                if (modifiedRoles == null || !modifiedRoles.ModifiedRoleByRoleType.ContainsKey(roleType))
                {
                    var cachedObject = cache.GetOrCreateCachedObject(reference.Class, reference.ObjectId, reference.CacheId);

                    List<ObjectId> roles;
                    if (rolesByAssociation.TryGetValue(reference, out roles))
                    {
                        cachedObject.SetValue(roleType, roles.ToArray());

                        if (nestedObjectIds != null)
                        {
                            nestedObjectIds.AddRange(roles);
                        }
                    }
                    else
                    {
                        cachedObject.SetValue(roleType, EmptyObjectIds);
                    }
                }
            }
        }

        internal void PrefetchCompositeAssociationObjectTable(List<Reference> roles, IAssociationType associationType, List<ObjectId> nestedObjectIds)
        {
            var references = nestedObjectIds == null ? this.FilterForPrefetchAssociations(roles, associationType) : this.FilterForPrefetchCompositeAssociations(roles, associationType, nestedObjectIds);
            if (references.Count == 0)
            {
                return;
            }

            Command command;
            if (!this.PrefetchCompositeAssociationByAssociationType.TryGetValue(associationType, out command))
            {
                var roleType = associationType.RoleType;
                var sql = this.Database.Mapping.ProcedureNameForPrefetchAssociationByRelationType[roleType.RelationType];
                command = this.Session.Connection.CreateCommand();
                command.CommandText = sql;
                command.CommandType = CommandType.StoredProcedure;
                command.AddObjectTableParameter(references);
                this.prefetchCompositeAssociationByAssociationType[associationType] = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForTableType].Value = this.Database.CreateObjectTable(references);
            }

            using (DbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var roleId = this.Database.ObjectIds.Parse(reader[1].ToString());
                    var role = this.Session.referenceByObjectId[roleId];

                    var associationByRole = this.Session.GetAssociationByRole(associationType);
                    if (!associationByRole.ContainsKey(role))
                    {
                        var associationIdValue = reader[0];
                        Reference association = null;
                        if (associationIdValue != null && associationIdValue != DBNull.Value)
                        {
                            var associationId = this.Database.ObjectIds.Parse(associationIdValue.ToString());
                            if (associationType.ObjectType.ExistExclusiveClass)
                            {
                                association = this.Session.GetOrCreateReferenceForExistingObject(associationType.ObjectType.ExclusiveClass, associationId);
                            }
                            else
                            {
                                association = this.Session.GetOrCreateReferenceForExistingObject(associationId);
                            }

                            if (nestedObjectIds != null)
                            {
                                nestedObjectIds.Add(association.ObjectId);
                            }
                        }

                        associationByRole[role] = association;

                        this.Session.FlushConditionally(roleId, associationType);
                    }
                }
            }
        }

        internal void PrefetchCompositeAssociationRelationTable(List<Reference> roles, IAssociationType associationType, List<ObjectId> nestedObjectIds)
        {
            var references = nestedObjectIds == null ? this.FilterForPrefetchAssociations(roles, associationType) : this.FilterForPrefetchCompositesAssociations(roles, associationType, nestedObjectIds);
            if (references.Count == 0)
            {
                return;
            }

            Command command;
            if (!this.PrefetchCompositeAssociationByAssociationType.TryGetValue(associationType, out command))
            {
                var roleType = associationType.RoleType;
                var sql = this.Database.Mapping.ProcedureNameForPrefetchAssociationByRelationType[roleType.RelationType];
                command = this.Session.Connection.CreateCommand();
                command.CommandText = sql;
                command.CommandType = CommandType.StoredProcedure;
                command.AddObjectTableParameter(roles);
                this.prefetchCompositeAssociationByAssociationType[associationType] = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForTableType].Value = this.Database.CreateObjectTable(roles);
            }

            var prefetchedAssociationByRole = new Dictionary<Reference, ObjectId>();
            using (DbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var roleId = this.Database.ObjectIds.Parse(reader[1].ToString());
                    var roleReference = this.Session.referenceByObjectId[roleId];
                    var associationId = this.Database.ObjectIds.Parse(reader[0].ToString());
                    prefetchedAssociationByRole.Add(roleReference, associationId);
                }
            }

            var associationByRole = this.Session.GetAssociationByRole(associationType);
            foreach (var role in roles)
            {
                if (!associationByRole.ContainsKey(role))
                {
                    Reference association = null;

                    ObjectId associationId;
                    if (prefetchedAssociationByRole.TryGetValue(role, out associationId))
                    {
                        if (associationType.ObjectType.ExistExclusiveClass)
                        {
                            association = this.Session.GetOrCreateReferenceForExistingObject(associationType.ObjectType.ExclusiveClass, associationId);
                        }
                        else
                        {
                            association = this.Session.GetOrCreateReferenceForExistingObject(associationId);
                        }

                        nestedObjectIds?.Add(associationId);
                    }

                    associationByRole[role] = association;

                    this.Session.FlushConditionally(role.ObjectId, associationType);
                }
            }
        }

        internal void PrefetchCompositesAssociationObjectTable(List<Reference> roles, IAssociationType associationType, List<ObjectId> nestedObjectIds)
        {
            var references = nestedObjectIds == null ? this.FilterForPrefetchAssociations(roles, associationType) : this.FilterForPrefetchCompositeAssociations(roles, associationType, nestedObjectIds);
            if (references.Count == 0)
            {
                return;
            }

            Command command;
            if (!this.PrefetchCompositeAssociationByAssociationType.TryGetValue(associationType, out command))
            {
                var roleType = associationType.RoleType;
                var sql = this.Database.Mapping.ProcedureNameForPrefetchAssociationByRelationType[roleType.RelationType];
                command = this.Session.Connection.CreateCommand();
                command.CommandText = sql;
                command.CommandType = CommandType.StoredProcedure;
                command.AddObjectTableParameter(roles);
                this.prefetchCompositeAssociationByAssociationType[associationType] = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForTableType].Value = this.Database.CreateObjectTable(roles);
            }

            var prefetchedAssociationByRole = new Dictionary<Reference, List<ObjectId>>();
            using (DbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var roleId = this.Database.ObjectIds.Parse(reader[1].ToString());
                    var roleReference = this.Session.referenceByObjectId[roleId];

                    var associationIdValue = reader[0];
                    if (associationIdValue != null && associationIdValue != DBNull.Value)
                    {
                        List<ObjectId> associations;
                        if (!prefetchedAssociationByRole.TryGetValue(roleReference, out associations))
                        {
                            associations = new List<ObjectId>();
                            prefetchedAssociationByRole.Add(roleReference, associations);
                        }

                        var associationId = this.Database.ObjectIds.Parse(associationIdValue.ToString());
                        associations.Add(associationId);

                        if (associationType.ObjectType.ExistExclusiveClass)
                        {
                            this.Session.GetOrCreateReferenceForExistingObject(associationType.ObjectType.ExclusiveClass, associationId);
                        }
                        else
                        {
                            this.Session.GetOrCreateReferenceForExistingObject(associationId);
                        }
                    }
                }
            }

            var associationsByRole = this.Session.GetAssociationsByRole(associationType);
            foreach (var role in roles)
            {
                if (!associationsByRole.ContainsKey(role))
                {
                    List<ObjectId> associations;
                    if (!prefetchedAssociationByRole.TryGetValue(role, out associations))
                    {
                        associationsByRole[role] = EmptyObjectIds;
                    }
                    else
                    {
                        associationsByRole[role] = associations.ToArray();

                        if (nestedObjectIds != null)
                        {
                            nestedObjectIds.AddRange(associations);
                        }
                    }

                    this.Session.FlushConditionally(role.ObjectId, associationType);
                }
            }
        }

        internal void PrefetchCompositesAssociationRelationTable(List<Reference> roles, IAssociationType associationType, List<ObjectId> nestedObjectIds)
        {
            var references = nestedObjectIds == null ? this.FilterForPrefetchAssociations(roles, associationType) : this.FilterForPrefetchCompositeAssociations(roles, associationType, nestedObjectIds);
            if (references.Count == 0)
            {
                return;
            }

            Command command;
            if (!this.PrefetchCompositeAssociationByAssociationType.TryGetValue(associationType, out command))
            {
                var roleType = associationType.RoleType;
                var sql = this.Database.Mapping.ProcedureNameForPrefetchAssociationByRelationType[roleType.RelationType];
                command = this.Session.Connection.CreateCommand();
                command.CommandText = sql;
                command.CommandType = CommandType.StoredProcedure;
                command.AddObjectTableParameter(roles);
                this.prefetchCompositeAssociationByAssociationType[associationType] = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForTableType].Value = this.Database.CreateObjectTable(roles);
            }

            var prefetchedAssociations = new HashSet<ObjectId>();

            var prefetchedAssociationByRole = new Dictionary<Reference, List<ObjectId>>();
            using (DbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var roleId = this.Database.ObjectIds.Parse(reader[1].ToString());
                    var roleReference = this.Session.referenceByObjectId[roleId];

                    List<ObjectId> associations;
                    if (!prefetchedAssociationByRole.TryGetValue(roleReference, out associations))
                    {
                        associations = new List<ObjectId>();
                        prefetchedAssociationByRole.Add(roleReference, associations);
                    }

                    var associationId = this.Database.ObjectIds.Parse(reader[0].ToString());
                    associations.Add(associationId);
                    prefetchedAssociations.Add(associationId);
                }
            }

            foreach (var associationId in prefetchedAssociations)
            {
                if (associationType.ObjectType.ExistExclusiveClass)
                {
                    this.Session.GetOrCreateReferenceForExistingObject(associationType.ObjectType.ExclusiveClass, associationId);
                }
                else
                {
                    this.Session.GetOrCreateReferenceForExistingObject(associationId);
                }
            }

            var associationsByRole = this.Session.GetAssociationsByRole(associationType);
            foreach (var role in roles)
            {
                if (!associationsByRole.ContainsKey(role))
                {
                    List<ObjectId> associations;
                    if (!prefetchedAssociationByRole.TryGetValue(role, out associations))
                    {
                        associationsByRole[role] = EmptyObjectIds;
                    }
                    else
                    {
                        associationsByRole[role] = associations.ToArray();

                        if (nestedObjectIds != null)
                        {
                            nestedObjectIds.AddRange(associations);
                        }
                    }

                    this.Session.FlushConditionally(role.ObjectId, associationType);
                }
            }
        }

        private List<Reference> FilterForPrefetchRoles(List<Reference> associations, IRoleType roleType)
        {
            var references = new List<Reference>();

            var cache = this.Database.Cache;

            foreach (var association in associations)
            {
                object role;

                Roles roles;
                if (this.Session.modifiedRolesByReference != null && this.Session.modifiedRolesByReference.TryGetValue(association, out roles))
                {
                    if (roles.TryGetUnitRole(roleType, out role))
                    {
                        continue;
                    }
                }
                else
                {
                    if (!association.IsUnknownCacheId)
                    {
                        var cacheObject = cache.GetOrCreateCachedObject(association.Class, association.ObjectId, association.CacheId);
                        if (cacheObject.TryGetValue(roleType, out role))
                        {
                            continue;
                        }
                    }
                }

                references.Add(association);
            }

            return references;
        }

        private List<Reference> FilterForPrefetchCompositeRoles(List<Reference> associations, IRoleType roleType, List<ObjectId> nestedObjects)
        {
            var references = new List<Reference>();

            var cache = this.Database.Cache;

            foreach (var association in associations)
            {
                Roles roles;
                if (this.Session.modifiedRolesByReference != null && this.Session.modifiedRolesByReference.TryGetValue(association, out roles))
                {
                    ObjectId role;
                    if (roles.TryGetCompositeRole(roleType, out role))
                    {
                        if (role != null)
                        {
                            nestedObjects.Add(role);
                        }

                        continue;
                    }
                }
                else
                {
                    if (!association.IsUnknownCacheId)
                    {
                        var cacheObject = cache.GetOrCreateCachedObject(association.Class, association.ObjectId, association.CacheId);

                        object role;
                        if (cacheObject.TryGetValue(roleType, out role))
                        {
                            if (role != null)
                            {
                                nestedObjects.Add((ObjectId)role);
                            }

                            continue;
                        }
                    }
                }

                references.Add(association);
            }

            return references;
        }

        private List<Reference> FilterForPrefetchCompositesRoles(List<Reference> associations, IRoleType roleType, List<ObjectId> nestedObjects)
        {
            var references = new List<Reference>();

            var cache = this.Database.Cache;

            foreach (var association in associations)
            {
                Roles roles;
                if (this.Session.modifiedRolesByReference != null && this.Session.modifiedRolesByReference.TryGetValue(association, out roles))
                {
                    IEnumerable<ObjectId> role;
                    if (roles.TryGetCompositesRole(roleType, out role))
                    {
                        nestedObjects.AddRange(role);
                        continue;
                    }
                }
                else
                {
                    if (!association.IsUnknownCacheId)
                    {
                        var cacheObject = cache.GetOrCreateCachedObject(association.Class, association.ObjectId, association.CacheId);

                        object role;
                        if (cacheObject.TryGetValue(roleType, out role))
                        {
                            nestedObjects.AddRange((ObjectId[])role);
                            continue;
                        }
                    }
                }

                references.Add(association);
            }

            return references;
        }

        private List<Reference> FilterForPrefetchAssociations(List<Reference> roles, IAssociationType associationType)
        {
            Dictionary<Reference, Reference> associationByRole;
            if (!this.Session.associationByRoleByAssociationType.TryGetValue(associationType, out associationByRole))
            {
                return roles;
            }

            return roles.Where(role => !associationByRole.ContainsKey(role)).ToList();
        }

        private List<Reference> FilterForPrefetchCompositeAssociations(List<Reference> roles, IAssociationType associationType, List<ObjectId> nestedObjectIds)
        {
            Dictionary<Reference, Reference> associationByRole;
            if (!this.Session.associationByRoleByAssociationType.TryGetValue(associationType, out associationByRole))
            {
                return roles;
            }

            var references = new List<Reference>();
            foreach (var role in roles)
            {
                Reference association;
                if (associationByRole.TryGetValue(role, out association))
                {
                    nestedObjectIds.Add(association.ObjectId);
                    continue;
                }

                references.Add(role);
            }

            return references;
        }

        private List<Reference> FilterForPrefetchCompositesAssociations(List<Reference> roles, IAssociationType associationType, List<ObjectId> nestedObjectIds)
        {
            Dictionary<Reference, ObjectId[]> associationByRole;
            if (!this.Session.associationsByRoleByAssociationType.TryGetValue(associationType, out associationByRole))
            {
                return roles;
            }

            var references = new List<Reference>();
            foreach (var role in roles)
            {
                ObjectId[] association;
                if (associationByRole.TryGetValue(role, out association))
                {
                    nestedObjectIds.AddRange(association);
                    continue;
                }

                references.Add(role);
            }

            return references;
        }
    }
}