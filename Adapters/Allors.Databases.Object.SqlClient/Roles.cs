// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Roles.cs" company="Allors bvba">
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
    using System;
    using System.Collections.Generic;

    using Allors.Databases.Object.SqlClient.Caching;
    using Allors.Meta;
    using Allors.Populations;

    internal class Roles
    {
        internal readonly Reference Reference;

        private ICachedObject cachedObject;
        
        private Dictionary<IRoleType, object> originalRoleByRoleType;
        
        private Dictionary<IRoleType, object> modifiedRoleByRoleType;
        private Dictionary<IRoleType, CompositeRoles> modifiedRolesByRoleType;

        private HashSet<IRoleType> requireFlushRoles;

        internal Roles(Reference reference)
        {
            this.Reference = reference;
        }

        internal ICachedObject CachedObject
        {
            get
            {
                if (this.cachedObject == null && !this.Reference.IsNew)
                {
                    var cache = this.Reference.Session.Database.Cache;
                    this.cachedObject = cache.GetOrCreateCachedObject(this.Reference.Class, this.Reference.ObjectId, this.Reference.CacheId);
                }

                return this.cachedObject;
            }
        }

        internal Dictionary<IRoleType, object> ModifiedRoleByRoleType
        {
            get
            {
                return this.modifiedRoleByRoleType ?? (this.modifiedRoleByRoleType = new Dictionary<IRoleType, object>());
            }
        }

        private Dictionary<IRoleType, object> OriginalRoleByRoleType
        {
            get
            {
                return this.originalRoleByRoleType ?? (this.originalRoleByRoleType = new Dictionary<IRoleType, object>());
            }
        }

        private HashSet<IRoleType> RequireFlushRoles
        {
            get
            {
                return this.requireFlushRoles ?? (this.requireFlushRoles = new HashSet<IRoleType>());
            }
        }

        private Dictionary<IRoleType, CompositeRoles> ModifiedRolesByRoleType
        {
            get
            {
                return this.modifiedRolesByRoleType ?? (this.modifiedRolesByRoleType = new Dictionary<IRoleType, CompositeRoles>());
            }
        }

        private ChangeSet ChangeSet
        {
            get
            {
                return this.Reference.Session.ChangeSet;
            }
        }

        internal object GetUnitRole(IRoleType roleType)
        {
            object role = null;
            if (this.modifiedRoleByRoleType == null || !this.modifiedRoleByRoleType.TryGetValue(roleType, out role))
            {
                if (this.CachedObject == null || !this.CachedObject.TryGetValue(roleType, out role))
                {
                    if (!this.Reference.IsNew)
                    {
                        this.Reference.Session.GetUnitRoles(this);
                        this.cachedObject.TryGetValue(roleType, out role);
                    }
                }
            }

            return role;
        }

        internal void SetUnitRole(IRoleType roleType, object role)
        {
            this.ChangeSet.OnChangingUnitRole(this, roleType);

            this.SetOriginal(roleType, role);

            this.ModifiedRoleByRoleType[roleType] = role;
            this.RequireFlushRoles.Add(roleType);

            this.Reference.Session.RequireFlush(this.Reference, this);
        }

        internal virtual ObjectId GetCompositeRole(IRoleType roleType)
        {
            object role = null;
            if (this.modifiedRoleByRoleType == null || !this.modifiedRoleByRoleType.TryGetValue(roleType, out role))
            {
                if (this.CachedObject == null || !this.CachedObject.TryGetValue(roleType, out role))
                {
                    if (!this.Reference.IsNew)
                    {
                        this.Reference.Session.GetCompositeRole(this, roleType);
                        this.cachedObject.TryGetValue(roleType, out role);
                    }
                }
            }

            return (ObjectId)role;
        }

        internal void SetCompositeRole(IRoleType roleType, Strategy newRoleStrategy)
        {
            var previousRole = this.GetCompositeRole(roleType);
            var newRole = newRoleStrategy == null ? null : newRoleStrategy.Reference.ObjectId;

            this.ChangeSet.OnChangingCompositeRole(this, roleType, previousRole, newRole);

            if (newRole != null && !newRole.Equals(previousRole))
            {
                if (roleType.AssociationType.IsOne)
                {
                    if (previousRole != null)
                    {
                        var previousRoleStrategy = this.Reference.Session.GetOrCreateReferenceForExistingObject(previousRole).Strategy;
                        var previousAssociation = previousRoleStrategy.GetCompositeAssociation(roleType.AssociationType);
                        if (previousAssociation != null)
                        {
                            previousAssociation.Strategy.RemoveCompositeRole(roleType);
                        }
                    }

                    var newRoleAssociation = newRoleStrategy.GetCompositeAssociation(roleType.AssociationType);
                    if (newRoleAssociation != null && !newRoleAssociation.Id.Equals(this.Reference.ObjectId))
                    {
                        newRoleAssociation.Strategy.RemoveCompositeRole(roleType);
                    }

                    this.Reference.Session.SetAssociation(this.Reference, newRoleStrategy, roleType.AssociationType);
                }
                else
                {
                    if (previousRole != null)
                    {
                        var previousRoleReference = this.Reference.Session.GetOrCreateReferenceForExistingObject(previousRole);
                        this.Reference.Session.RemoveAssociation(this.Reference, previousRoleReference, roleType.AssociationType);
                    }
                }
            }

            if (previousRole != null && !previousRole.Equals(newRole))
            {
                this.Reference.Session.TriggerFlush(previousRole, roleType.AssociationType);
            }

            if ((newRole == null && previousRole != null) ||
                (newRole != null && !newRole.Equals(previousRole)))
            {
                this.SetOriginal(roleType, newRole);
                this.ModifiedRoleByRoleType[roleType] = newRole;
                this.AddRequiresFlushRoleType(roleType);
                this.Reference.Session.RequireFlush(this.Reference, this);

                if (newRole != null)
                {
                    if (roleType.AssociationType.IsMany)
                    {
                        this.Reference.Session.AddAssociation(this.Reference, newRoleStrategy.Reference, roleType.AssociationType);
                        this.Reference.Session.TriggerFlush(newRole, roleType.AssociationType);
                    }
                }
            }
        }

        internal void RemoveCompositeRole(IRoleType roleType)
        {
            var currentRole = this.GetCompositeRole(roleType);
            if (currentRole != null)
            {
                var currentRoleStrategy = this.Reference.Session.GetOrCreateReferenceForExistingObject(currentRole).Strategy;

                this.ChangeSet.OnChangingCompositeRole(this, roleType, currentRoleStrategy == null ? null : currentRoleStrategy.ObjectId, null);

                if (roleType.AssociationType.IsOne)
                {
                    this.Reference.Session.SetAssociation(null, currentRoleStrategy, roleType.AssociationType);
                }
                else
                {
                    this.Reference.Session.RemoveAssociation(this.Reference, currentRoleStrategy.Reference, roleType.AssociationType);
                    this.Reference.Session.TriggerFlush(currentRole, roleType.AssociationType);
                }

                this.SetOriginal(roleType, null);
                this.ModifiedRoleByRoleType[roleType] = null;
                this.AddRequiresFlushRoleType(roleType);
                this.Reference.Session.RequireFlush(this.Reference, this);
            }
        }

        internal virtual IEnumerable<ObjectId> GetCompositeRoles(IRoleType roleType)
        {
            CompositeRoles compositeRoles;
            if (this.ModifiedRolesByRoleType != null && this.ModifiedRolesByRoleType.TryGetValue(roleType, out compositeRoles))
            {
                return compositeRoles.ObjectIds;
            }

            return this.GetNonModifiedCompositeRoles(roleType);
        }

        internal void AddCompositeRole(IRoleType roleType, Strategy role)
        {
            CompositeRoles compositeRoles;
            if (this.ModifiedRolesByRoleType == null || !this.ModifiedRolesByRoleType.TryGetValue(roleType, out compositeRoles))
            {
                compositeRoles = new CompositeRoles(this.GetCompositeRoles(roleType));
                this.ModifiedRolesByRoleType[roleType] = compositeRoles;
            }

            this.ChangeSet.OnChangingCompositesRole(this, roleType, role);

            if (!compositeRoles.Contains(role.ObjectId))
            {
                compositeRoles.Add(role.ObjectId);

                if (roleType.AssociationType.IsOne)
                {
                    var previousAssociationObject = role.GetCompositeAssociation(roleType.AssociationType);
                    var previousAssociation = previousAssociationObject != null ? (Strategy)previousAssociationObject.Strategy : null;
                    if (previousAssociation != null && !previousAssociation.ObjectId.Equals(this.Reference.ObjectId))
                    {
                        previousAssociation.RemoveCompositeRole(roleType, role.GetObject());
                    }

                    this.Reference.Session.SetAssociation(this.Reference, role, roleType.AssociationType);
                }
                else
                {
                    this.Reference.Session.AddAssociation(this.Reference, role.Reference, roleType.AssociationType);
                    this.Reference.Session.TriggerFlush(role.ObjectId, roleType.AssociationType);
                }

                this.AddRequiresFlushRoleType(roleType);
                this.Reference.Session.RequireFlush(this.Reference, this);
            }
        }

        internal void RemoveCompositeRole(IRoleType roleType, Strategy role)
        {
            CompositeRoles compositeRoles;
            if (this.ModifiedRolesByRoleType == null || !this.ModifiedRolesByRoleType.TryGetValue(roleType, out compositeRoles))
            {
                compositeRoles = new CompositeRoles(this.GetCompositeRoles(roleType));
                this.ModifiedRolesByRoleType[roleType] = compositeRoles;
            }

            if (compositeRoles.Contains(role.ObjectId))
            {
                this.ChangeSet.OnChangingCompositesRole(this, roleType, role);

                compositeRoles.Remove(role.ObjectId);

                if (roleType.AssociationType.IsOne)
                {
                    this.Reference.Session.SetAssociation(null, role, roleType.AssociationType);
                }
                else
                {
                    this.Reference.Session.RemoveAssociation(this.Reference, role.Reference, roleType.AssociationType);
                    this.Reference.Session.TriggerFlush(role.ObjectId, roleType.AssociationType);
                }

                this.AddRequiresFlushRoleType(roleType);
                this.Reference.Session.RequireFlush(this.Reference, this);
            }
        }

        internal void Flush(Flush flush)
        {
            IRoleType unitRole = null;
            List<IRoleType> unitRoles = null;
            foreach (var flushRole in this.RequireFlushRoles)
            {
                if (flushRole.ObjectType.IsUnit)
                {
                    if (unitRole == null)
                    {
                        unitRole = flushRole;
                    }
                    else
                    {
                        if (unitRoles == null)
                        {
                            unitRoles = new List<IRoleType> { unitRole };
                        }

                        unitRoles.Add(flushRole);
                    }
                }
                else
                {
                    if (flushRole.IsOne)
                    {
                        var role = this.GetCompositeRole(flushRole);
                        if (role != null)
                        {
                            flush.SetCompositeRole(this.Reference, flushRole, role);
                        }
                        else
                        {
                            flush.ClearCompositeAndCompositesRole(this.Reference, flushRole);
                        }
                    }
                    else
                    {
                        var roles = this.ModifiedRolesByRoleType[flushRole];
                        roles.Flush(flush, this, flushRole);                        
                    }
                }
            }

            if (unitRoles != null)
            {
                unitRoles.Sort(MetaObjectComparer.ById);
                this.Reference.Session.SetUnitRoles(this, unitRoles);
            }
            else if (unitRole != null)
            {
                flush.SetUnitRole(this.Reference, unitRole, this.GetUnitRole(unitRole));
            }

            this.requireFlushRoles = null;
        }

        internal int ExtentCount(IRoleType roleType)
        {
            CompositeRoles compositeRoles;
            if (this.ModifiedRolesByRoleType != null && this.ModifiedRolesByRoleType.TryGetValue(roleType, out compositeRoles))
            {
                return compositeRoles.Count;
            }

            return this.GetNonModifiedCompositeRoles(roleType).Length;
        }

        internal IObject ExtentFirst(DatabaseSession session, IRoleType roleType)
        {
            CompositeRoles compositeRoles;
            if (this.ModifiedRolesByRoleType != null && this.ModifiedRolesByRoleType.TryGetValue(roleType, out compositeRoles))
            {
                var objectId = compositeRoles.First;
                return objectId == null ? null : session.GetOrCreateReferenceForExistingObject(objectId).Strategy.GetObject();
            }

            var nonModifiedCompositeRoles = this.GetNonModifiedCompositeRoles(roleType);
            if (nonModifiedCompositeRoles.Length > 0)
            {
                return session.GetOrCreateReferenceForExistingObject(nonModifiedCompositeRoles[0]).Strategy.GetObject();
            }

            return null;
        }

        internal void ExtentCopyTo(DatabaseSession session, IRoleType roleType, Array array, int index)
        {
            CompositeRoles compositeRoles;
            if (this.ModifiedRolesByRoleType != null && this.ModifiedRolesByRoleType.TryGetValue(roleType, out compositeRoles))
            {
                var i = 0;
                foreach (var objectId in compositeRoles.ObjectIds)
                {
                    array.SetValue(session.GetOrCreateReferenceForExistingObject(objectId).Strategy.GetObject(), index + i);
                    ++i;
                }

                return;
            }

            var nonModifiedCompositeRoles = this.GetNonModifiedCompositeRoles(roleType);
            for (var i = 0; i < nonModifiedCompositeRoles.Length; i++)
            {
                var objectId = nonModifiedCompositeRoles[i];
                array.SetValue(session.GetOrCreateReferenceForExistingObject(objectId).Strategy.GetObject(), index + i);
            }
        }

        internal bool ExtentContains(IRoleType roleType, ObjectId objectId)
        {
            CompositeRoles compositeRoles;
            if (this.ModifiedRolesByRoleType != null && this.ModifiedRolesByRoleType.TryGetValue(roleType, out compositeRoles))
            {
                return compositeRoles.Contains(objectId);
            }

            return Array.IndexOf(this.GetNonModifiedCompositeRoles(roleType), objectId) >= 0;
        }

        private void AddRequiresFlushRoleType(IRoleType roleType)
        {
            if (this.requireFlushRoles == null)
            {
                this.requireFlushRoles = new HashSet<IRoleType>();
            }

            this.requireFlushRoles.Add(roleType);
        }

        private ObjectId[] GetNonModifiedCompositeRoles(IRoleType roleType)
        {
            if (!this.Reference.IsNew)
            {
                object roleOut;
                if (this.CachedObject != null && this.CachedObject.TryGetValue(roleType, out roleOut))
                {
                    return (ObjectId[])roleOut;
                }

                this.Reference.Session.GetCompositesRole(this, roleType);
                this.cachedObject.TryGetValue(roleType, out roleOut);
                var role = (ObjectId[])roleOut;
                return role;
            }

            return ObjectId.EmptyObjectIds;
        }

        private void SetOriginal(IRoleType roleType, object role)
        {
            if (!this.OriginalRoleByRoleType.ContainsKey(roleType))
            {
                this.OriginalRoleByRoleType[roleType] = role;
            }
        }

        private class CompositeRoles
        {
            private readonly HashSet<ObjectId> baseline;
            private HashSet<ObjectId> original;
            private HashSet<ObjectId> added;
            private HashSet<ObjectId> removed;

            internal CompositeRoles(IEnumerable<ObjectId> compositeRoles)
            {
                this.baseline = new HashSet<ObjectId>(compositeRoles);
            }

            internal HashSet<ObjectId> ObjectIds
            {
                get
                {
                    if ((this.removed == null || this.removed.Count == 0) && (this.added == null || this.added.Count == 0))
                    {
                        return this.baseline;
                    }

                    var merged = new HashSet<ObjectId>(this.baseline);
                    if (this.removed != null && this.removed.Count > 0)
                    {
                        merged.ExceptWith(this.removed);
                    }

                    if (this.added != null && this.added.Count > 0)
                    {
                        merged.UnionWith(this.added);
                    }

                    return merged;
                }
            }

            internal int Count
            {
                get
                {
                    var addedCount = this.added == null ? 0 : this.added.Count;
                    var removedCount = this.removed == null ? 0 : this.removed.Count;

                    return this.baseline.Count + addedCount - removedCount;
                }
            }

            internal ObjectId First
            {
                get
                {
                    if (this.removed == null || this.removed.Count == 0)
                    {
                        if (this.baseline.Count > 0)
                        {
                            return this.GetFirst(this.baseline);
                        }

                        if (this.added != null && this.added.Count > 0)
                        {
                            return this.GetFirst(this.added);
                        }

                        return null;
                    }

                    var roles = this.ObjectIds;
                    if (roles.Count > 0)
                    {
                        return this.GetFirst(roles);
                    }

                    return null;
                }
            }

            internal bool Contains(ObjectId objectId)
            {
                if (this.removed != null && this.removed.Contains(objectId))
                {
                    return false;
                }

                return this.baseline.Contains(objectId) || (this.added != null && this.added.Contains(objectId));
            }

            internal void Add(ObjectId objectId)
            {
                if (this.original == null)
                {
                    this.original = new HashSet<ObjectId>(this.baseline);
                }

                if (this.removed != null && this.removed.Contains(objectId))
                {
                    this.removed.Remove(objectId);
                    return;
                }

                if (!this.baseline.Contains(objectId))
                {
                    if (this.added == null)
                    {
                        this.added = new HashSet<ObjectId>();
                    }

                    this.added.Add(objectId);
                }
            }

            internal void Remove(ObjectId objectId)
            {
                if (this.original == null)
                {
                    this.original = new HashSet<ObjectId>(this.baseline);
                }

                if (this.added != null && this.added.Contains(objectId))
                {
                    this.added.Remove(objectId);
                    return;
                }

                if (this.baseline.Contains(objectId))
                {
                    if (this.removed == null)
                    {
                        this.removed = new HashSet<ObjectId>();
                    }

                    this.removed.Add(objectId);
                }
            }

            internal void Flush(Flush flush, Roles roles, IRoleType roleType)
            {
                if (this.Count == 0)
                {
                    flush.ClearCompositeAndCompositesRole(roles.Reference, roleType);
                }
                else
                {
                    if (this.added != null && this.added.Count > 0)
                    {
                        flush.AddCompositeRole(roles.Reference, roleType, this.added);
                    }

                    if (this.removed != null && this.removed.Count > 0)
                    {
                        flush.RemoveCompositeRole(roles.Reference, roleType, this.removed);
                    }
                }

                if (this.added != null)
                {
                    this.baseline.UnionWith(this.added);
                }

                if (this.removed != null)
                {
                    this.baseline.ExceptWith(this.removed);
                }

                this.added = null;
                this.removed = null;
            }

            private ObjectId GetFirst(HashSet<ObjectId> hashSet)
            {
                var enumerator = hashSet.GetEnumerator();
                enumerator.MoveNext();
                return enumerator.Current;
            }
        }
    }
}