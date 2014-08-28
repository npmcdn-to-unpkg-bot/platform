// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorkspaceSession.cs" company="Allors bvba">
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

namespace Allors.Adapters.Workspace.Memory
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;

    using Allors.Adapters;
    using Allors.Meta;

    public class WorkspaceSession : IWorkspaceSession
    {
        private readonly Dictionary<ObjectType, ObjectType[]> concreteClassesByObjectType;
        private ChangeSet changeSet;
        
        private IDatabaseSession databaseSession;
        private bool synced;

        private List<Strategy> syncedDeletedStrategies;
        private List<Strategy> rollbackSyncedDeletedStrategies;

        private Dictionary<RoleType, Dictionary<Strategy, object>> originalUnitRoleByAssociationByRoleType;
        private Dictionary<RoleType, Dictionary<Strategy, object>> orignalRollbackUnitRoleByAssociationByRoleType;
        private Dictionary<RoleType, Dictionary<Strategy, object>> diffUnitRoleByAssociationByRoleType;
        private Dictionary<RoleType, Dictionary<Strategy, object>> diffRollbackUnitRoleByAssociationByRoleType;

        private Dictionary<RoleType, Dictionary<Strategy, Strategy>> originalCompositeRoleByAssociationByRoleType;
        private Dictionary<RoleType, Dictionary<Strategy, Strategy>> originalRollbackCompositeRoleByAssociationByRoleType;
        private Dictionary<RoleType, Dictionary<Strategy, Strategy>> diffCompositeRoleByAssociationByRoleType;
        private Dictionary<RoleType, Dictionary<Strategy, Strategy>> diffRollbackCompositeRoleByAssociationByRoleType;

        private Dictionary<RoleType, Dictionary<Strategy, HashSet<Strategy>>> originalCompositeRolesByAssociationByRoleType;
        private Dictionary<RoleType, Dictionary<Strategy, HashSet<Strategy>>> originalrollbackCompositeRolesByAssociationByRoleType;
        private Dictionary<RoleType, Dictionary<Strategy, HashSet<Strategy>>> diffCompositeRolesByAssociationByRoleType;
        private Dictionary<RoleType, Dictionary<Strategy, HashSet<Strategy>>> diffRollbackCompositeRolesByAssociationByRoleType;

        private Dictionary<ObjectId, ObjectId> preSyncIdsByPostSyncId;

        private Dictionary<ObjectId, Strategy> strategyByObjectId;

        private bool busyCommittingOrRollingBack;

        private Dictionary<string, object> properties;

        internal WorkspaceSession(Workspace workspace)
        {
            this.MemoryWorkspace = workspace;
            this.changeSet = new ChangeSet();

            this.syncedDeletedStrategies = new List<Strategy>();
            this.rollbackSyncedDeletedStrategies = new List<Strategy>();

            this.diffUnitRoleByAssociationByRoleType = new Dictionary<RoleType, Dictionary<Strategy, object>>();
            this.diffRollbackUnitRoleByAssociationByRoleType = new Dictionary<RoleType, Dictionary<Strategy, object>>();
            this.originalUnitRoleByAssociationByRoleType = new Dictionary<RoleType, Dictionary<Strategy, object>>();
            this.diffRollbackUnitRoleByAssociationByRoleType = new Dictionary<RoleType, Dictionary<Strategy, object>>();

            this.diffCompositeRoleByAssociationByRoleType = new Dictionary<RoleType, Dictionary<Strategy, Strategy>>();
            this.diffRollbackCompositeRoleByAssociationByRoleType = new Dictionary<RoleType, Dictionary<Strategy, Strategy>>();
            this.diffRollbackCompositeRoleByAssociationByRoleType = new Dictionary<RoleType, Dictionary<Strategy, Strategy>>();
            this.originalCompositeRoleByAssociationByRoleType = new Dictionary<RoleType, Dictionary<Strategy, Strategy>>();

            this.diffCompositeRolesByAssociationByRoleType = new Dictionary<RoleType, Dictionary<Strategy, HashSet<Strategy>>>();
            this.diffRollbackCompositeRolesByAssociationByRoleType = new Dictionary<RoleType, Dictionary<Strategy, HashSet<Strategy>>>();
            this.diffRollbackCompositeRolesByAssociationByRoleType = new Dictionary<RoleType, Dictionary<Strategy, HashSet<Strategy>>>();
            this.originalCompositeRolesByAssociationByRoleType = new Dictionary<RoleType, Dictionary<Strategy, HashSet<Strategy>>>();

            this.strategyByObjectId = new Dictionary<ObjectId, Strategy>();
            this.preSyncIdsByPostSyncId = new Dictionary<ObjectId, ObjectId>();

            this.concreteClassesByObjectType = new Dictionary<ObjectType, ObjectType[]>();

            this.busyCommittingOrRollingBack = false;
            this.synced = false;
        }

        public event SessionCommittingEventHandler Committing;

        public event SessionCommittedEventHandler Committed;

        public event SessionRollingBackEventHandler RollingBack;

        public event SessionRolledBackEventHandler RolledBack;

        public IWorkspace Workspace
        {
            get { return this.MemoryWorkspace; }
        }

        public virtual IDatabaseSession DatabaseSession
        {
            get
            {
                if (this.databaseSession == null && this.Workspace.Database != null)
                {
                    this.databaseSession = this.Workspace.Database.CreateSession();
                }

                return this.databaseSession;
            }

            set
            {
                this.databaseSession = value;
            }
        }

        public IPopulation Population
        {
            get { return this.MemoryWorkspace; }
        }

        public virtual IConflict[] Conflicts
        {
            get
            {
                var conflicts = new List<IConflict>();

                foreach (var strategyByObjectIdEntry in this.strategyByObjectId)
                {
                    var workspaceStrategy = strategyByObjectIdEntry.Value;
                    if (!workspaceStrategy.IsDeleted && !workspaceStrategy.IsNewInWorkspace)
                    {
                        if (this.DatabaseSession.InstantiateStrategy(workspaceStrategy.ObjectId) == null)
                        {
                            conflicts.Add(new Conflict(workspaceStrategy));
                        }
                    }
                }

                foreach (var originalUnitRoleByAssociationByRoleTypeEntry in this.originalUnitRoleByAssociationByRoleType)
                {
                    var roleType = originalUnitRoleByAssociationByRoleTypeEntry.Key;
                    var originalUnitRoleByAssociation = originalUnitRoleByAssociationByRoleTypeEntry.Value;

                    foreach (var originalRoleByAssociationEntry in originalUnitRoleByAssociation)
                    {
                        var association = originalRoleByAssociationEntry.Key;
                        if (!association.IsNewInWorkspace)
                        {
                            var originalRole = originalRoleByAssociationEntry.Value;

                            var databaseAssociation = this.DatabaseSession.InstantiateStrategy(association.ObjectId);
                            if (databaseAssociation == null)
                            {
                                conflicts.Add(new Conflict(association));
                                continue;
                            }
                            var databaseRole = databaseAssociation.GetUnitRole(roleType);

                            if (!AreUnitsEqual(roleType, originalRole, databaseRole))
                            {
                                conflicts.Add(new Conflict(association, roleType));
                            }
                        }
                    }
                }

                foreach (var originalCompositeRoleByAssociationByRoleTypeEntry in this.originalCompositeRoleByAssociationByRoleType)
                {
                    var roleType = originalCompositeRoleByAssociationByRoleTypeEntry.Key;
                    var originalRoleByAssociation = originalCompositeRoleByAssociationByRoleTypeEntry.Value;

                    foreach (var originalRoleByAssociationEntry in originalRoleByAssociation)
                    {
                        var association = originalRoleByAssociationEntry.Key;
                        if (!association.IsNewInWorkspace)
                        {
                            var originalRole = originalRoleByAssociationEntry.Value;

                            var databaseAssociation = this.DatabaseSession.InstantiateStrategy(association.ObjectId);
                            if (databaseAssociation == null)
                            {
                                conflicts.Add(new Conflict(association));
                                continue;
                            }

                            var databaseRole = databaseAssociation.GetCompositeRole(roleType);

                            if (originalRole == null)
                            {
                                if (databaseRole != null)
                                {
                                    conflicts.Add(new Conflict(association, roleType));
                                }
                            }
                            else
                            {
                                if (databaseRole == null)
                                {
                                    conflicts.Add(new Conflict(association, roleType));
                                }
                                else
                                {
                                    if (!Equals(originalRole.ObjectId, databaseRole.Id))
                                    {
                                        conflicts.Add(new Conflict(association, roleType));
                                    }
                                }
                            }
                        }
                    }
                }

                foreach (var originalCompositeRolesByAssociationByRoleTypeEntry in this.originalCompositeRolesByAssociationByRoleType)
                {
                    var roleType = originalCompositeRolesByAssociationByRoleTypeEntry.Key;
                    var originalRolesByAssociation = originalCompositeRolesByAssociationByRoleTypeEntry.Value;

                    foreach (var originalRolesByAssociationEntry in originalRolesByAssociation)
                    {
                        var association = originalRolesByAssociationEntry.Key;
                        if (!association.IsNewInWorkspace)
                        {
                            var originalRoles = originalRolesByAssociationEntry.Value;

                            var databaseAssociation = this.DatabaseSession.InstantiateStrategy(association.ObjectId);
                            if (databaseAssociation == null)
                            {
                                conflicts.Add(new Conflict(association));
                                continue;
                            }

                            var databaseRole = databaseAssociation.GetCompositeRoles(roleType);

                            if (originalRoles == null)
                            {
                                if (databaseRole != null && databaseRole.Count > 0)
                                {
                                    conflicts.Add(new Conflict(association, roleType));
                                }
                            }
                            else
                            {
                                if (databaseRole == null || databaseRole.Count == 0)
                                {
                                    conflicts.Add(new Conflict(association, roleType));
                                }
                                else
                                {
                                    if (!Equals(originalRoles.Count, databaseRole.Count))
                                    {
                                        conflicts.Add(new Conflict(association, roleType));
                                    }
                                    else
                                    {
                                        foreach (var originalRoleStrategy in originalRoles)
                                        {
                                            var databaseRoleObject = this.DatabaseSession.Instantiate(originalRoleStrategy.ObjectId);
                                            if (databaseRoleObject == null || !databaseRole.Contains(databaseRoleObject))
                                            {
                                                conflicts.Add(new Conflict(association, roleType));
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                return conflicts.ToArray();
            }
        }

        internal Workspace MemoryWorkspace { get; private set; }

        internal ChangeSet WorkspaceChangeSet
        {
            get
            {
                return this.changeSet;
            }
        }

        public object this[string name]
        {
            get
            {
                if (this.properties == null)
                {
                    return null;
                }

                object value;
                this.properties.TryGetValue(name, out value);
                return value;
            }

            set
            {
                if (this.properties == null)
                {
                    this.properties = new Dictionary<string, object>();
                }

                if (value == null)
                {
                    this.properties.Remove(name);
                }
                else
                {
                    this.properties[name] = value;
                }
            }
        }

        public virtual void Resolve(IConflict conflict)
        {
            var strategy = this.GetStrategy(conflict.ObjectId);

            if (conflict.RoleType != null)
            {
                var roleType = conflict.RoleType;

                if (roleType.ObjectType is Unit)
                {
                    Dictionary<Strategy, object> originalUnitRoleByAssociation;
                    if (this.originalUnitRoleByAssociationByRoleType.TryGetValue(roleType, out originalUnitRoleByAssociation))
                    {
                        originalUnitRoleByAssociation.Remove(strategy);
                    }

                    Dictionary<Strategy, object> unitRoleByAssociation;
                    if (this.diffUnitRoleByAssociationByRoleType.TryGetValue(roleType, out unitRoleByAssociation))
                    {
                        unitRoleByAssociation.Remove(strategy);
                    }
                }
                else
                {
                    if (roleType.IsMany)
                    {
                        Dictionary<Strategy, HashSet<Strategy>> originalCompositeRolesByAssociation;
                        if (this.originalCompositeRolesByAssociationByRoleType.TryGetValue(roleType, out originalCompositeRolesByAssociation))
                        {
                            originalCompositeRolesByAssociation.Remove(strategy);
                        }

                        Dictionary<Strategy, HashSet<Strategy>> compositeRolesByAssociation;
                        if (this.diffCompositeRolesByAssociationByRoleType.TryGetValue(roleType, out compositeRolesByAssociation))
                        {
                            compositeRolesByAssociation.Remove(strategy);
                        }
                    }
                    else
                    {
                        Dictionary<Strategy, Strategy> originalCompositeRoleByAssociation;
                        if (this.originalCompositeRoleByAssociationByRoleType.TryGetValue(roleType, out originalCompositeRoleByAssociation))
                        {
                            originalCompositeRoleByAssociation.Remove(strategy);
                        }

                        Dictionary<Strategy, Strategy> compositeRoleByAssociation;
                        if (this.diffCompositeRoleByAssociationByRoleType.TryGetValue(roleType, out compositeRoleByAssociation))
                        {
                            compositeRoleByAssociation.Remove(strategy);
                        }
                    }
                }
            }
            else
            {
                var databaseObject = this.DatabaseSession.Instantiate(conflict.ObjectId);
                if (databaseObject == null)
                {
                    strategy.Delete();

                    foreach (var roleType in strategy.ObjectType.RoleTypes)
                    {
                        if (roleType.ObjectType is Unit)
                        {
                            Dictionary<Strategy, object> originalUnitRoleByStrategy;
                            if (this.originalUnitRoleByAssociationByRoleType.TryGetValue(roleType, out originalUnitRoleByStrategy))
                            {
                                originalUnitRoleByStrategy.Remove(strategy);
                            }
                        }
                        else
                        {
                            if (roleType.IsMany)
                            {
                                Dictionary<Strategy, HashSet<Strategy>> originalCompositesRoleByStrategy;
                                if (this.originalCompositeRolesByAssociationByRoleType.TryGetValue(roleType, out originalCompositesRoleByStrategy))
                                {
                                    originalCompositesRoleByStrategy.Remove(strategy);
                                }
                            }
                            else
                            {
                                Dictionary<Strategy, Strategy> originalCompositeRoleByStrategy;
                                if (this.originalCompositeRoleByAssociationByRoleType.TryGetValue(roleType, out originalCompositeRoleByStrategy))
                                {
                                    originalCompositeRoleByStrategy.Remove(strategy);
                                }
                            }
                        }
                    }

                    foreach (var associationType in strategy.ObjectType.AssociationTypes)
                    {
                        var roleType = associationType.RoleType;
                        if (roleType.IsMany)
                        {
                            var associationsWithRole = new List<Strategy>();

                            Dictionary<Strategy, HashSet<Strategy>> compositeRolesByAssociation;
                            if (this.originalCompositeRolesByAssociationByRoleType.TryGetValue(roleType, out compositeRolesByAssociation))
                            {
                                foreach (var compositeRolesByStrategyEntry in compositeRolesByAssociation)
                                {
                                    var roles = compositeRolesByStrategyEntry.Value;
                                    if (roles != null && roles.Contains(strategy))
                                    {
                                        associationsWithRole.Add(compositeRolesByStrategyEntry.Key);
                                    }
                                }

                                foreach (var associationWithRole in associationsWithRole)
                                {
                                    var roles = compositeRolesByAssociation[associationWithRole];
                                    roles.Remove(strategy);
                                    if (roles.Count == 0)
                                    {
                                        compositeRolesByAssociation[associationWithRole] = null;
                                    }
                                }
                            }
                        }
                        else
                        {
                            var associationsWithRole = new List<Strategy>();

                            Dictionary<Strategy, Strategy> compositeRoleByStrategy;
                            if (this.originalCompositeRoleByAssociationByRoleType.TryGetValue(roleType, out compositeRoleByStrategy))
                            {
                                foreach (var compositeRoleByStrategyEntry in compositeRoleByStrategy)
                                {
                                    var role = compositeRoleByStrategyEntry.Value;
                                    if (strategy.Equals(role))
                                    {
                                        associationsWithRole.Add(compositeRoleByStrategyEntry.Key);
                                    }
                                }

                                foreach (var associationWithRole in associationsWithRole)
                                {
                                    compositeRoleByStrategy[associationWithRole] = null;
                                }
                            }
                        }
                    }
                }
            }
        }

        public virtual void Resolve(IConflict[] conflicts)
        {
            foreach (var conflict in conflicts)
            {
                this.Resolve(conflict);
            }
        }

        public virtual IObject[] LocalExtent()
        {
            var objects = new List<IObject>();
            foreach (var dictionaryEntry in this.strategyByObjectId)
            {
                var strategy = dictionaryEntry.Value;
                if (!strategy.IsDeleted)
                {
                    objects.Add(strategy.GetObject());
                }
            }

            return objects.ToArray();
        }

        public virtual Allors.Extent LocalExtent(Composite objectType)
        {
            return new ExtentObject(this, objectType);
        }

        public virtual void Sync()
        {
            this.synced = true;

            if (this.Conflicts.Length > 0)
            {
                throw new Exception("Sync has unhandled conflicts");
            }

            // Strategies
            var newWorkspaceStrategies = new ArrayList();
            var deletedWorkspaceStrategies = new ArrayList();

            foreach (var strategyByObjectIdEntry in this.strategyByObjectId)
            {
                var strategy = strategyByObjectIdEntry.Value;
                if (!strategy.IsDeleted && strategy.IsNewInWorkspace)
                {
                    newWorkspaceStrategies.Add(strategy);
                }

                if (strategy.IsDeleted && !strategy.IsNewInWorkspace && !this.syncedDeletedStrategies.Contains(strategy))
                {
                    deletedWorkspaceStrategies.Add(strategy);
                }
            }

            foreach (Strategy newWorkspaceStrategy in newWorkspaceStrategies)
            {
                var databaseObject = this.DatabaseSession.Create(newWorkspaceStrategy.ObjectType);

                var oldId = newWorkspaceStrategy.ObjectId;
                var newId = databaseObject.Strategy.ObjectId;

                this.strategyByObjectId[newId] = newWorkspaceStrategy;
                this.strategyByObjectId.Remove(oldId);

                newWorkspaceStrategy.ObjectId = newId;

                this.preSyncIdsByPostSyncId[newId] = oldId;
            }

            foreach (Strategy deletedWorkspaceStrategy in deletedWorkspaceStrategies)
            {
                var databaseObject = this.DatabaseSession.Instantiate(deletedWorkspaceStrategy.ObjectId);
                if (databaseObject != null)
                {
                    databaseObject.Strategy.Delete();
                }

                this.syncedDeletedStrategies.Add(deletedWorkspaceStrategy);
            }

            // Relations
            foreach (var originalUnitRoleByAssociationByRoleTypeEntry in this.originalUnitRoleByAssociationByRoleType)
            {
                var roleType = originalUnitRoleByAssociationByRoleTypeEntry.Key;
                var originalUnitRoleByAssociation = originalUnitRoleByAssociationByRoleTypeEntry.Value;

                Dictionary<Strategy, object> diffUnitRoleByAssociation;
                if (this.diffUnitRoleByAssociationByRoleType.TryGetValue(roleType, out diffUnitRoleByAssociation))
                {
                    foreach (var originalRoleByAssociationEntry in originalUnitRoleByAssociation)
                    {
                        var association = originalRoleByAssociationEntry.Key;

                        if (!association.IsDeleted)
                        {
                            object role;
                            if (diffUnitRoleByAssociation.TryGetValue(association, out role))
                            {
                                var databaseAssociation = this.DatabaseSession.InstantiateStrategy(association.ObjectId);
                                databaseAssociation.SetUnitRole(roleType, role);
                            }
                        }
                    }
                }
            }

            foreach (var originalCompositeRoleByAssociationByRoleTypeEntry in this.originalCompositeRoleByAssociationByRoleType)
            {
                var roleType = originalCompositeRoleByAssociationByRoleTypeEntry.Key;
                var originalRoleByAssociation = originalCompositeRoleByAssociationByRoleTypeEntry.Value;

                Dictionary<Strategy, Strategy> diffCompositeRoleByAssociation;
                if (this.diffCompositeRoleByAssociationByRoleType.TryGetValue(
                    roleType, out diffCompositeRoleByAssociation))
                {
                    foreach (var originalRoleByAssociationEntry in originalRoleByAssociation)
                    {
                        var association = originalRoleByAssociationEntry.Key;

                        if (!association.IsDeleted)
                        {
                            Strategy role;
                            if (diffCompositeRoleByAssociation.TryGetValue(association, out role))
                            {
                                var databaseAssociation = this.DatabaseSession.InstantiateStrategy(association.ObjectId);

                                IObject databaseRoleObject = null;
                                if (role != null)
                                {
                                    databaseRoleObject = this.DatabaseSession.Instantiate(role.ObjectId);
                                }

                                databaseAssociation.SetCompositeRole(roleType, databaseRoleObject);
                            }
                        }
                    }
                }
            }

            foreach (var originalCompositeRolesByAssociationByRoleTypeEntry in this.originalCompositeRolesByAssociationByRoleType)
            {
                var roleType = originalCompositeRolesByAssociationByRoleTypeEntry.Key;
                var originalRolesByAssociation = originalCompositeRolesByAssociationByRoleTypeEntry.Value;

                Dictionary<Strategy, HashSet<Strategy>> diffCompositeRolesByAssociation;
                if (this.diffCompositeRolesByAssociationByRoleType.TryGetValue(roleType, out diffCompositeRolesByAssociation))
                {
                    foreach (var originalRolesByAssociationEntry in originalRolesByAssociation)
                    {
                        var association = originalRolesByAssociationEntry.Key;

                        if (!association.IsDeleted)
                        {
                            HashSet<Strategy> roles;
                            if (diffCompositeRolesByAssociation.TryGetValue(association, out roles))
                            {
                                var databaseAssociation = this.DatabaseSession.InstantiateStrategy(association.ObjectId);

                                IObject[] databaseRoleObjects = null;
                                if (roles != null)
                                {
                                    var objectIds = new List<ObjectId>();
                                    foreach (var role in roles)
                                    {
                                        objectIds.Add(role.ObjectId);
                                    }

                                    databaseRoleObjects = this.DatabaseSession.Instantiate(objectIds.ToArray());
                                }

                                databaseAssociation.SetCompositeRoles(roleType, databaseRoleObjects);
                            }
                        }
                    }
                }
            }

            this.originalUnitRoleByAssociationByRoleType = new Dictionary<RoleType, Dictionary<Strategy, object>>();
            this.diffUnitRoleByAssociationByRoleType = new Dictionary<RoleType, Dictionary<Strategy, object>>();

            this.originalCompositeRoleByAssociationByRoleType = new Dictionary<RoleType, Dictionary<Strategy, Strategy>>();
            this.diffCompositeRoleByAssociationByRoleType = new Dictionary<RoleType, Dictionary<Strategy, Strategy>>();

            this.originalCompositeRolesByAssociationByRoleType = new Dictionary<RoleType, Dictionary<Strategy, HashSet<Strategy>>>();
            this.diffCompositeRolesByAssociationByRoleType = new Dictionary<RoleType, Dictionary<Strategy, HashSet<Strategy>>>();

            this.changeSet = new ChangeSet();
        }

        public virtual void Commit()
        {
            if (!this.busyCommittingOrRollingBack)
            {
                try
                {
                    this.busyCommittingOrRollingBack = true;
                    if (this.Committing != null)
                    {
                        // Errors thrown in Committing event handlers 
                        // should cause the commit to fail. The current
                        // session is not affected.
                        this.Committing(this, new SessionCommittingEventArgs(this));
                    }

                    foreach (var strategy in new List<Strategy>(this.strategyByObjectId.Values))
                    {
                        strategy.Commit();
                    }

                    this.preSyncIdsByPostSyncId = new Dictionary<ObjectId, ObjectId>();

                    this.rollbackSyncedDeletedStrategies = new List<Strategy>(this.syncedDeletedStrategies);

                    // Copy current state to rollback state
                    // Unit
                    this.orignalRollbackUnitRoleByAssociationByRoleType = new Dictionary<RoleType, Dictionary<Strategy, object>>();
                    foreach (var dictionaryEntry in this.originalUnitRoleByAssociationByRoleType)
                    {
                        var roleType = dictionaryEntry.Key;
                        var originalUnitRoleByStrategy = dictionaryEntry.Value;
                        this.orignalRollbackUnitRoleByAssociationByRoleType[roleType] = new Dictionary<Strategy, object>(originalUnitRoleByStrategy);
                    }

                    this.diffRollbackUnitRoleByAssociationByRoleType = new Dictionary<RoleType, Dictionary<Strategy, object>>();
                    foreach (var dictionaryEntry in this.diffUnitRoleByAssociationByRoleType)
                    {
                        var roleType = dictionaryEntry.Key;
                        var unitRoleByStrategy = dictionaryEntry.Value;
                        this.diffRollbackUnitRoleByAssociationByRoleType[roleType] = new Dictionary<Strategy, object>(unitRoleByStrategy);
                    }

                    // Composite
                    this.originalRollbackCompositeRoleByAssociationByRoleType = new Dictionary<RoleType, Dictionary<Strategy, Strategy>>();
                    foreach (var dictionaryEntry in this.originalCompositeRoleByAssociationByRoleType)
                    {
                        var roleType = dictionaryEntry.Key;
                        var orignalCompositeRoleByStrategy = dictionaryEntry.Value;
                        this.originalRollbackCompositeRoleByAssociationByRoleType[roleType] = new Dictionary<Strategy, Strategy>(orignalCompositeRoleByStrategy);
                    }

                    this.diffRollbackCompositeRoleByAssociationByRoleType = new Dictionary<RoleType, Dictionary<Strategy, Strategy>>();
                    foreach (var dictionaryEntry in this.diffCompositeRoleByAssociationByRoleType)
                    {
                        var roleType = dictionaryEntry.Key;
                        var compositeRoleByStrategy = dictionaryEntry.Value;
                        this.diffRollbackCompositeRoleByAssociationByRoleType[roleType] = new Dictionary<Strategy, Strategy>(compositeRoleByStrategy);
                    }

                    // Composites
                    this.originalrollbackCompositeRolesByAssociationByRoleType = new Dictionary<RoleType, Dictionary<Strategy, HashSet<Strategy>>>();
                    foreach (var dictionaryEntry in this.originalCompositeRolesByAssociationByRoleType)
                    {
                        var roleType = dictionaryEntry.Key;
                        var rollbackOriginalCompositesRoleByAssociation = new Dictionary<Strategy, HashSet<Strategy>>(dictionaryEntry.Value);

                        foreach (var rollbackOriginalCompositesRoleByAssociationEntry in dictionaryEntry.Value)
                        {
                            var association = rollbackOriginalCompositesRoleByAssociationEntry.Key;
                            var roles = rollbackOriginalCompositesRoleByAssociationEntry.Value;
                            if (roles != null)
                            {
                                rollbackOriginalCompositesRoleByAssociation[association] = new HashSet<Strategy>(roles);
                            }
                            else
                            {
                                rollbackOriginalCompositesRoleByAssociation[association] = null;
                            }
                        }

                        this.originalrollbackCompositeRolesByAssociationByRoleType[roleType] = rollbackOriginalCompositesRoleByAssociation;
                    }

                    this.diffRollbackCompositeRolesByAssociationByRoleType = new Dictionary<RoleType, Dictionary<Strategy, HashSet<Strategy>>>();
                    foreach (var dictionaryEntry in this.diffCompositeRolesByAssociationByRoleType)
                    {
                        var roleType = dictionaryEntry.Key;
                        var rollbackCompositesRoleByAssociation = new Dictionary<Strategy, HashSet<Strategy>>(dictionaryEntry.Value);

                        foreach (var rollbackCompositesRoleByAssociationEntry in dictionaryEntry.Value)
                        {
                            var association = rollbackCompositesRoleByAssociationEntry.Key;
                            var roles = rollbackCompositesRoleByAssociationEntry.Value;
                            if (roles != null)
                            {
                                rollbackCompositesRoleByAssociation[association] = new HashSet<Strategy>(roles);
                            }
                            else
                            {
                                rollbackCompositesRoleByAssociation[association] = null;
                            }
                        }

                        this.diffRollbackCompositeRolesByAssociationByRoleType[roleType] = rollbackCompositesRoleByAssociation;
                    }

                    this.changeSet = new ChangeSet();

                    if (this.Committed != null)
                    {
                        this.Committed(this, new SessionCommittedEventArgs(this));
                    }
                }
                finally
                {
                    try
                    {
                        if (this.synced)
                        {
                            this.DatabaseSession.Commit();
                        }
                        else
                        {
                            this.DatabaseSession.Rollback();
                        }
                    }
                    finally
                    {
                        this.synced = false;
                        this.busyCommittingOrRollingBack = false;
                    }
                }
            }
        }

        public virtual void Rollback()
        {
            if (!this.busyCommittingOrRollingBack)
            {
                try
                {
                    this.busyCommittingOrRollingBack = true;
                    if (this.RollingBack != null)
                    {
                        this.RollingBack(this, new SessionRollingBackEventArgs(this));
                    }

                    foreach (var strategy in new List<Strategy>(this.strategyByObjectId.Values))
                    {
                        strategy.Rollback();
                    }

                    this.syncedDeletedStrategies = new List<Strategy>(this.rollbackSyncedDeletedStrategies);

                    foreach (var rollbackPreSyncIdByPostSyncId in this.preSyncIdsByPostSyncId)
                    {
                        var postSyncId = rollbackPreSyncIdByPostSyncId.Key;
                        var preSyncId = rollbackPreSyncIdByPostSyncId.Value;

                        var strategy = this.strategyByObjectId[postSyncId];
                        this.strategyByObjectId.Remove(postSyncId);

                        strategy.ObjectId = preSyncId;
                        this.strategyByObjectId[preSyncId] = strategy;
                    }

                    this.preSyncIdsByPostSyncId = new Dictionary<ObjectId, ObjectId>();

                    // Copy rollback state to current state
                    // Unit
                    this.originalUnitRoleByAssociationByRoleType = new Dictionary<RoleType, Dictionary<Strategy, object>>();
                    if (this.orignalRollbackUnitRoleByAssociationByRoleType != null)
                    {
                        foreach (var dictionaryEntry in this.orignalRollbackUnitRoleByAssociationByRoleType)
                        {
                            var roleType = dictionaryEntry.Key;
                            var rollbackOriginalUnitRoleByStrategy = dictionaryEntry.Value;
                            this.originalUnitRoleByAssociationByRoleType[roleType] = new Dictionary<Strategy, object>(rollbackOriginalUnitRoleByStrategy);
                        }
                    }

                    this.diffUnitRoleByAssociationByRoleType = new Dictionary<RoleType, Dictionary<Strategy, object>>();
                    if (this.diffRollbackUnitRoleByAssociationByRoleType != null)
                    {
                        foreach (var dictionaryEntry in this.diffRollbackUnitRoleByAssociationByRoleType)
                        {
                            var roleType = dictionaryEntry.Key;
                            var rolebackUnitRoleByStrategy = dictionaryEntry.Value;
                            this.diffUnitRoleByAssociationByRoleType[roleType] = new Dictionary<Strategy, object>(rolebackUnitRoleByStrategy);
                        }
                    }
 
                    // Composite
                    this.originalCompositeRoleByAssociationByRoleType = new Dictionary<RoleType, Dictionary<Strategy, Strategy>>();
                    if (this.originalRollbackCompositeRoleByAssociationByRoleType != null)
                    {
                        foreach (var dictionaryEntry in this.originalRollbackCompositeRoleByAssociationByRoleType)
                        {
                            var roleType = dictionaryEntry.Key;
                            var rollbackOriginalCompositeRoleByStrategy = dictionaryEntry.Value;
                            this.originalCompositeRoleByAssociationByRoleType[roleType] = new Dictionary<Strategy, Strategy>(rollbackOriginalCompositeRoleByStrategy);
                        }
                    }

                    this.diffCompositeRoleByAssociationByRoleType = new Dictionary<RoleType, Dictionary<Strategy, Strategy>>();
                    if (this.diffRollbackCompositeRoleByAssociationByRoleType != null)
                    {
                        foreach (var dictionaryEntry in this.diffRollbackCompositeRoleByAssociationByRoleType)
                        {
                            var roleType = dictionaryEntry.Key;
                            var rollbackCompositeRoleByStrategy = dictionaryEntry.Value;
                            this.diffCompositeRoleByAssociationByRoleType[roleType] = new Dictionary<Strategy, Strategy>(rollbackCompositeRoleByStrategy);
                        }
                    }
 
                    // Composites
                    this.originalCompositeRolesByAssociationByRoleType = new Dictionary<RoleType, Dictionary<Strategy, HashSet<Strategy>>>();
                    if (this.originalrollbackCompositeRolesByAssociationByRoleType != null)
                    {
                        foreach (var dictionaryEntry in this.originalrollbackCompositeRolesByAssociationByRoleType)
                        {
                            var roleType = dictionaryEntry.Key;
                            var originalCompositesRoleByAssociation = new Dictionary<Strategy, HashSet<Strategy>>(dictionaryEntry.Value);

                            foreach (var rollbackOriginalCompositesRoleByAssociationEntry in dictionaryEntry.Value)
                            {
                                var association = rollbackOriginalCompositesRoleByAssociationEntry.Key;
                                var roles = rollbackOriginalCompositesRoleByAssociationEntry.Value;
                                if (roles != null)
                                {
                                    originalCompositesRoleByAssociation[association] = new HashSet<Strategy>(roles);
                                }
                                else
                                {
                                    originalCompositesRoleByAssociation[association] = null;
                                }
                            }

                            this.originalCompositeRolesByAssociationByRoleType[roleType] = originalCompositesRoleByAssociation;
                        }
                    }

                    this.diffCompositeRolesByAssociationByRoleType = new Dictionary<RoleType, Dictionary<Strategy, HashSet<Strategy>>>();
                    if (this.diffRollbackCompositeRolesByAssociationByRoleType != null)
                    {
                        foreach (var dictionaryEntry in this.diffRollbackCompositeRolesByAssociationByRoleType)
                        {
                            var roleType = dictionaryEntry.Key;
                            var compositesRoleByAssociation = new Dictionary<Strategy, HashSet<Strategy>>(dictionaryEntry.Value);

                            foreach (var rollbackCompositesRoleByAssociationEntry in dictionaryEntry.Value)
                            {
                                var association = rollbackCompositesRoleByAssociationEntry.Key;
                                var roles = rollbackCompositesRoleByAssociationEntry.Value;
                                if (roles != null)
                                {
                                    compositesRoleByAssociation[association] = new HashSet<Strategy>(roles);
                                }
                                else
                                {
                                    compositesRoleByAssociation[association] = null;
                                }
                            }

                            this.diffCompositeRolesByAssociationByRoleType[roleType] = compositesRoleByAssociation;
                        }
                    }

                    this.changeSet = new ChangeSet();

                    if (this.RolledBack != null)
                    {
                        this.RolledBack(this, new SessionRolledBackEventArgs(this));
                    }
                }
                finally
                {
                    try
                    {
                        if (this.databaseSession != null)
                        {
                            this.databaseSession.Rollback();
                        }
                    }
                    finally
                    {
                        this.synced = false;
                        this.busyCommittingOrRollingBack = false;
                    }
                }
            }
        }

        public virtual void Dispose()
        {
            this.Rollback();
        }

        public virtual T Create<T>() where T : IObject
        {
            var objectType = this.MemoryWorkspace.Database.ObjectFactory.GetObjectTypeForType(typeof(T));
            var @class = objectType as Class;
            if (@class == null)
            {
                throw new Exception("ObjectType should be a class");
            }

            return (T)this.Create(@class);
        }

        public virtual IObject[] Create(Class objectType, int count)
        {
            var arrayType = this.MemoryWorkspace.Database.ObjectFactory.GetTypeForObjectType(objectType);
            var allorsObjects = (IObject[])Array.CreateInstance(arrayType, count);
            for (var i = 0; i < count; i++)
            {
                allorsObjects[i] = this.Create(objectType);
            }

            return allorsObjects;
        }

        public virtual IObject Instantiate(string objectIdString)
        {
            var id = this.MemoryWorkspace.ObjectIds.Parse(objectIdString);
            return this.Instantiate(id);
        }

        public virtual IObject Instantiate(IObject obj)
        {
            return this.Instantiate(obj.Strategy.ObjectId);
        }

        public virtual IObject Instantiate(ObjectId objectId)
        {
            var strategy = this.GetStrategy(objectId);
            if (strategy != null)
            {
                return strategy.GetObject();
            }

            return null;
        }

        public virtual IObject[] Instantiate(string[] objectIdStrings)
        {
            var objectIds = new ObjectId[objectIdStrings.Length];
            for (var i = 0; i < objectIdStrings.Length; i++)
            {
                objectIds[i] = this.MemoryWorkspace.ObjectIds.Parse(objectIdStrings[i]);
            }

            return this.Instantiate(objectIds);
        }

        public virtual IObject[] Instantiate(IObject[] objects)
        {
            var objectIds = new ObjectId[objects.Length];
            for (var i = 0; i < objects.Length; i++)
            {
                objectIds[i] = objects[i].Id;
            }

            return this.Instantiate(objectIds);
        }

        public virtual IObject[] Instantiate(ObjectId[] objectIds)
        {
            var allorsObjects = new List<IObject>(objectIds.Length);

            foreach (var objectId in objectIds)
            {
                var strategy = this.GetStrategy(objectId);
                if (strategy != null)
                {
                    allorsObjects.Add(strategy.GetObject());
                }
            }

            return allorsObjects.ToArray();
        }

        public IChangeSet Checkpoint()
        {
            try
            {
                return this.changeSet;
            }
            finally
            {
                this.changeSet = new ChangeSet();
            }
        }

        public virtual Extent<T> Extent<T>() where T : IObject
        {
            throw new NotSupportedException();
        }

        public virtual Allors.Extent Extent(Composite objectType)
        {
            throw new NotSupportedException();
        }

        public virtual Allors.Extent Union(Allors.Extent firstOperand, Allors.Extent secondOperand)
        {
            throw new NotSupportedException();
        }

        public virtual Allors.Extent Intersect(Allors.Extent firstOperand, Allors.Extent secondOperand)
        {
            throw new NotSupportedException();
        }

        public virtual Allors.Extent Except(Allors.Extent firstOperand, Allors.Extent secondOperand)
        {
            throw new NotSupportedException();
        }

        public virtual IObject Create(Class objectType)
        {
            var strategy = this.CreateNewStrategy(objectType);
            this.strategyByObjectId.Add(strategy.ObjectId, strategy);

            this.changeSet.OnCreated(strategy);

            return strategy.GetObject();
        }

        internal virtual void LoadPopulation(XmlReader reader)
        {
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    // eat everything but elements
                    case XmlNodeType.Element:
                        if (reader.Name.Equals(Serialization.Objects))
                        {
                            if (!reader.IsEmptyElement)
                            {
                                this.LoadObjects(reader);
                            }
                        }
                        else if (reader.Name.Equals(Serialization.Relations))
                        {
                            if (!reader.IsEmptyElement)
                            {
                                this.LoadRelations(reader);
                            }
                        }

                        break;
                    case XmlNodeType.EndElement:
                        return;
                }
            }
        }

        internal virtual void LoadObjects(XmlReader reader)
        {
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.Name.Equals(Serialization.Database))
                        {
                            if (!reader.IsEmptyElement)
                            {
                                this.LoadDatabaseObjects(reader);
                            }
                        }
                        else if (reader.Name.Equals(Serialization.Workspace))
                        {
                            if (!reader.IsEmptyElement)
                            {
                                this.LoadWorkspaceObjects(reader);
                            }
                        }

                        break;
                    case XmlNodeType.EndElement:
                        return;
                }
            }
        }

        internal virtual void LoadRelations(XmlReader reader)
        {
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.Name.Equals(Serialization.Database))
                        {
                            if (!reader.IsEmptyElement)
                            {
                                this.LoadDatabaseRelations(reader);
                            }
                        }
                        else if (reader.Name.Equals(Serialization.Workspace))
                        {
                            if (!reader.IsEmptyElement)
                            {
                                this.LoadWorkspaceRelations(reader);
                            }
                        }

                        break;
                    case XmlNodeType.EndElement:
                        return;
                }
            }
        }

        internal virtual void Save(XmlWriter writer)
        {
            writer.WriteStartElement(Serialization.Population);
            writer.WriteAttributeString(Serialization.Version, Serialization.VersionCurrent);

            this.SaveObjects(writer);
            
            writer.WriteStartElement(Serialization.Relations);

            this.SaveDatabaseRelations(writer);
            this.SaveWorkRelations(writer);

            writer.WriteEndElement();

            writer.WriteEndElement();
        }

        internal virtual Strategy GetStrategy(IObject obj)
        {
            if (obj == null)
            {
                return null;
            }

            return this.GetStrategy(obj.Id);
        }

        internal virtual Strategy GetStrategy(ObjectId objectId)
        {
            Strategy strategy;
            if (!this.strategyByObjectId.TryGetValue(objectId, out strategy))
            {
                var databaseStrategy = this.DatabaseSession.InstantiateStrategy(objectId);
                if (databaseStrategy != null)
                {
                    strategy = this.GetStrategy(databaseStrategy);
                    this.strategyByObjectId.Add(strategy.ObjectId, strategy);
                }
                else
                {
                    return null;
                }
            }

            return strategy.IsDeleted ? null : strategy;
        }

        internal virtual HashSet<Strategy> LoadRoleStrategies(XmlReader reader)
        {
            if (!reader.IsEmptyElement)
            {
                var value = reader.ReadString();
                var roleIds = value.Split(Serialization.ObjectsSplitterCharArray);
                var strategies = new HashSet<Strategy>();
                foreach (var roleId in roleIds)
                {
                    var objectId = this.MemoryWorkspace.ObjectIds.Parse(roleId);
                    Strategy strategy;
                    if (!this.strategyByObjectId.TryGetValue(objectId, out strategy))
                    {
                        throw new Exception("Strategy not found");
                    }

                    strategies.Add(strategy);
                }

                return strategies;
            }

            return null;
        }

        internal virtual Strategy LoadStrategy(XmlReader reader)
        {
            if (!reader.IsEmptyElement)
            {
                var value = reader.ReadString();
                var objectId = this.MemoryWorkspace.ObjectIds.Parse(value);
                Strategy strategy;
                if (!this.strategyByObjectId.TryGetValue(objectId, out strategy))
                {
                    throw new Exception("Strategy not found");
                }

                return strategy;
            }

            return null;
        }

        internal virtual void Delete(Strategy association)
        {
            foreach (var roleType in association.ObjectType.RoleTypes)
            {
                if (roleType.ObjectType is Unit)
                {
                    Dictionary<Strategy, object> diffUnitRoleByStrategy;
                    if (this.diffUnitRoleByAssociationByRoleType.TryGetValue(roleType, out diffUnitRoleByStrategy))
                    {
                        diffUnitRoleByStrategy.Remove(association);
                    }
                }
                else
                {
                    if (roleType.IsMany)
                    {
                        Dictionary<Strategy, HashSet<Strategy>> diffCompositesRoleByStrategy;
                        if (this.diffCompositeRolesByAssociationByRoleType.TryGetValue(roleType, out diffCompositesRoleByStrategy))
                        {
                            diffCompositesRoleByStrategy.Remove(association);
                        }
                    }
                    else
                    {
                        Dictionary<Strategy, Strategy> diffCompositeRoleByStrategy;
                        if (this.diffCompositeRoleByAssociationByRoleType.TryGetValue(roleType, out diffCompositeRoleByStrategy))
                        {
                            diffCompositeRoleByStrategy.Remove(association);
                        }
                    }
                }
            }

            foreach (var associationType in association.ObjectType.AssociationTypes)
            {
                var roleType = associationType.RoleType;
                if (roleType.IsMany)
                {
                    var associationsWithRole = new List<Strategy>();

                    Dictionary<Strategy, HashSet<Strategy>> compositeRolesByAssociation;
                    if (this.diffCompositeRolesByAssociationByRoleType.TryGetValue(roleType, out compositeRolesByAssociation))
                    {
                        foreach (var compositeRolesByStrategyEntry in compositeRolesByAssociation)
                        {
                            var roles = compositeRolesByStrategyEntry.Value;
                            if (roles != null &&
                                roles.Contains(association))
                            {
                                associationsWithRole.Add(compositeRolesByStrategyEntry.Key);
                            }
                        }

                        foreach (var associationWithRole in associationsWithRole)
                        {
                            var roles = compositeRolesByAssociation[associationWithRole];
                            roles.Remove(association);
                            if (roles.Count == 0)
                            {
                                compositeRolesByAssociation[associationWithRole] = null;
                            }
                        }
                    }
                }
                else
                {
                    var associationsWithRole = new List<Strategy>();

                    Dictionary<Strategy, Strategy> compositeRoleByStrategy;
                    if (this.diffCompositeRoleByAssociationByRoleType.TryGetValue(roleType, out compositeRoleByStrategy))
                    {
                        foreach (var compositeRoleByStrategyEntry in compositeRoleByStrategy)
                        {
                            var role = compositeRoleByStrategyEntry.Value;
                            if (association.Equals(role))
                            {
                                associationsWithRole.Add(compositeRoleByStrategyEntry.Key);
                            }
                        }

                        foreach (var associationWithRole in associationsWithRole)
                        {
                            compositeRoleByStrategy[associationWithRole] = null;
                        }
                    }
                }
            }
        }

        internal virtual object GetUnitRole(Strategy association, RoleType roleType)
        {
            var originalUnitRole = this.GetOriginalUnitRole(roleType, association);

            object unitRole;
            Dictionary<Strategy, object> unitRoleByAssociation;
            if (this.diffUnitRoleByAssociationByRoleType.TryGetValue(roleType, out unitRoleByAssociation) &&
                unitRoleByAssociation.TryGetValue(association, out unitRole))
            {
                return unitRole;
            }

            return originalUnitRole;
        }

        internal virtual void SetUnitRole(Strategy association, RoleType roleType, object role)
        {
            var originalUnitRole = this.GetOriginalUnitRole(roleType, association);

            // Set new unit
            if (AreUnitsEqual(roleType, originalUnitRole, role))
            {
                Dictionary<Strategy, object> unitRoleByAssociation;
                if (this.diffUnitRoleByAssociationByRoleType.TryGetValue(roleType, out unitRoleByAssociation))
                {
                    unitRoleByAssociation.Remove(association);
                }
            }
            else
            {
                Dictionary<Strategy, object> unitRoleByAssociation;
                if (!this.diffUnitRoleByAssociationByRoleType.TryGetValue(roleType, out unitRoleByAssociation))
                {
                    unitRoleByAssociation = new Dictionary<Strategy, object>();
                    this.diffUnitRoleByAssociationByRoleType[roleType] = unitRoleByAssociation;
                }

                unitRoleByAssociation[association] = role;
            }
        }

        internal virtual Strategy GetCompositeRole(Strategy association, RoleType roleType)
        {
            var originalCompositeRole = this.GetOriginalCompositeRole(association, roleType);
            
            Strategy compositeRole;
            Dictionary<Strategy, Strategy> compositeRoleByStrategy;
            if (this.diffCompositeRoleByAssociationByRoleType.TryGetValue(roleType, out compositeRoleByStrategy) &&
                compositeRoleByStrategy.TryGetValue(association, out compositeRole))
            {
                return compositeRole;
            }

            return originalCompositeRole;
        }

        internal virtual void SetCompositeRole(Strategy association, RoleType roleType, Strategy role, Strategy previousRole)
        {
            // 1 - 1
            if (roleType.AssociationType.IsOne)
            {
                // previous association
                var previousAssociation = this.GetAssociation(role, roleType.AssociationType);

                if (previousAssociation != null && 
                    !association.Equals(previousAssociation))
                {
                    this.GetOriginalCompositeRole(previousAssociation, roleType);

                    Dictionary<Strategy, Strategy> compositeRoleByPreviousAssociation;
                    if (this.diffCompositeRoleByAssociationByRoleType.TryGetValue(roleType, out compositeRoleByPreviousAssociation))
                    {
                        compositeRoleByPreviousAssociation.Remove(previousAssociation);
                    }
                }
            }

            // Set new composite role
            var originalCompositeRole = this.GetOriginalCompositeRole(association, roleType);
            var compositeRoleByAssociation = this.GetCompositeRoleByAssociation(roleType);

            if (role.Equals(originalCompositeRole))
            {
                compositeRoleByAssociation.Remove(association);
            }
            else
            {
                compositeRoleByAssociation[association] = role;
            }
        }

        internal virtual void RemoveCompositeRole(Strategy association, RoleType roleType, Strategy previousRoleStrategy)
        {
            var originalCompositeRole = this.GetOriginalCompositeRole(association, roleType);

            var compositeRoleByAssociation = this.GetCompositeRoleByAssociation(roleType);
            if (originalCompositeRole == null)
            {
                compositeRoleByAssociation.Remove(association);
            }
            else
            {
                compositeRoleByAssociation[association] = null;
            }
        }

        internal virtual HashSet<Strategy> GetCompositeRoles(Strategy association, RoleType roleType)
        {
            var originalCompositeRoles = this.GetOriginalCompositeRoles(association, roleType);

            HashSet<Strategy> compositeRoles;
            Dictionary<Strategy, HashSet<Strategy>> compositesRoleByStrategy;
            if (this.diffCompositeRolesByAssociationByRoleType.TryGetValue(roleType, out compositesRoleByStrategy) &&
                compositesRoleByStrategy.TryGetValue(association, out compositeRoles))
            {
                return compositeRoles;
            }

            return originalCompositeRoles;
        }

        internal virtual void AddCompositeRole(Strategy association, RoleType roleType, Strategy role)
        {
            var originalCompositeRoles = this.GetOriginalCompositeRoles(association, roleType);
            var compositeRolesByAssociation = this.GetCompositeRolesByAssociation(roleType);

            HashSet<Strategy> compositeRoles;
            if (!compositeRolesByAssociation.TryGetValue(association, out compositeRoles))
            {
                compositeRoles = (originalCompositeRoles == null) ? new HashSet<Strategy>() : new HashSet<Strategy>(originalCompositeRoles);
            }

            if (compositeRoles == null)
            {
                compositeRoles = new HashSet<Strategy>();
            }

            // 1 - *
            if (roleType.AssociationType.IsOne)
            {
                // previous association
                var previousAssociation = this.GetAssociation(role, roleType.AssociationType);
                if (previousAssociation != null)
                {
                    var previousCompositeRoles = this.GetCompositeRoles(previousAssociation, roleType);
                    this.RemoveCompositeRole(previousAssociation, roleType, role, previousCompositeRoles);
                }
            }

            // Set new composite role
            compositeRoles.Add(role);

            if (AreCompositesEqual(compositeRoles, originalCompositeRoles))
            {
                compositeRolesByAssociation.Remove(association);
            }
            else
            {
                compositeRolesByAssociation[association] = compositeRoles;
            }
        }

        internal virtual void RemoveCompositeRole(Strategy association, RoleType roleType, Strategy role, HashSet<Strategy> previousRoles)
        {
            var compositeRolesByAssociation = this.GetCompositeRolesByAssociation(roleType);
            var originalCompositeRoles = this.GetOriginalCompositeRoles(association, roleType);

            HashSet<Strategy> compositeRoles;
            if (!compositeRolesByAssociation.TryGetValue(association, out compositeRoles))
            {
                compositeRoles = (originalCompositeRoles == null) ? new HashSet<Strategy>() : new HashSet<Strategy>(originalCompositeRoles);
            }

            if (compositeRoles != null)
            {
                compositeRoles.Remove(role);
            }

            if (AreCompositesEqual(compositeRoles, originalCompositeRoles))
            {
                compositeRolesByAssociation.Remove(association);
            }
            else
            {
                compositeRolesByAssociation[association] = compositeRoles;
            }
        }

        internal virtual void RemoveCompositeRoles(Strategy association, RoleType roleType, HashSet<Strategy> previousRoleStrategy)
        {
            var originalCompositeRoles = this.GetOriginalCompositeRoles(association, roleType);
            var compositeRolesByAssociation = this.GetCompositeRolesByAssociation(roleType);

            if (originalCompositeRoles == null)
            {
                compositeRolesByAssociation.Remove(association);
            }
            else
            {
                compositeRolesByAssociation[association] = null;
            }
        }

        internal virtual Strategy GetAssociation(Strategy role, AssociationType associationType)
        {
            var roleType = associationType.RoleType;

            if (roleType.IsMany)
            {
                // 1 - *
                Dictionary<Strategy, HashSet<Strategy>> diffCompositeRolesByAssociation;
                if (this.diffCompositeRolesByAssociationByRoleType.TryGetValue(roleType, out diffCompositeRolesByAssociation))
                {
                    foreach (var entry in diffCompositeRolesByAssociation)
                    {
                        var association = entry.Key;
                        var diffCompositeRoles = entry.Value;

                        if (diffCompositeRoles != null && diffCompositeRoles.Contains(role))
                        {
                            // Workspace match found
                            return association;
                        }
                    }
                }

                // No Workspace match found
                var originalAssociation = role.GetOriginalCompositeAssociation(associationType);

                // Check if association hasn't been reassigned
                HashSet<Strategy> diffCompositeRole;
                if (diffCompositeRolesByAssociation != null && originalAssociation != null && 
                    diffCompositeRolesByAssociation.TryGetValue(originalAssociation, out diffCompositeRole))
                {
                    if (diffCompositeRole == null || !diffCompositeRole.Contains(role))
                    {
                        return null;
                    }
                }

                return originalAssociation;
            }
            else
            {
                // 1 - 1
                Dictionary<Strategy, Strategy> diffCompositeRoleByAssociation;
                if (this.diffCompositeRoleByAssociationByRoleType.TryGetValue(roleType, out diffCompositeRoleByAssociation))
                {
                    foreach (var diffCompositeRoleByAssociationEntry in diffCompositeRoleByAssociation)
                    {
                        if (role.Equals(diffCompositeRoleByAssociationEntry.Value))
                        {
                            // Workspace match found
                            return diffCompositeRoleByAssociationEntry.Key;
                        }
                    }
                }

                // No exact match found
                var association = role.GetOriginalCompositeAssociation(associationType);

                // Check if association hasn't been reassigned
                Strategy diffCompositeRole;
                if (association != null && diffCompositeRoleByAssociation != null &&
                    diffCompositeRoleByAssociation.TryGetValue(association, out diffCompositeRole))
                {
                    if (!role.Equals(diffCompositeRole))
                    {
                        return null;
                    }
                }

                return association;
            }
        }

        internal virtual HashSet<Strategy> GetAssociations(Strategy role, AssociationType associationType)
        {
            var associations = new HashSet<Strategy>();
            var roleType = associationType.RoleType;

            if (roleType.IsMany)
            {
                // * - *
                Dictionary<Strategy, HashSet<Strategy>> diffCompositeRolesByAssociation;
                if (this.diffCompositeRolesByAssociationByRoleType.TryGetValue(roleType, out diffCompositeRolesByAssociation))
                {
                    foreach (var diffCompositeRolesByAssociationEntry in diffCompositeRolesByAssociation)
                    {
                        if (diffCompositeRolesByAssociationEntry.Value != null &&
                            diffCompositeRolesByAssociationEntry.Value.Contains(role))
                        {
                            // match found
                            associations.Add(diffCompositeRolesByAssociationEntry.Key);
                        }
                    }
                }

                var originalAssociations = role.GetOriginalCompositesAssociation(associationType);
                foreach (var originalAssociation in originalAssociations)
                {
                    // Check if association hasn't been reassigned
                    HashSet<Strategy> diffCompositeRoles;
                    if (diffCompositeRolesByAssociation != null &&
                        diffCompositeRolesByAssociation.TryGetValue(originalAssociation, out diffCompositeRoles))
                    {
                        if (diffCompositeRoles != null && diffCompositeRoles.Contains(role))
                        {
                            associations.Add(originalAssociation);
                        }
                    }
                    else
                    {
                        associations.Add(originalAssociation);
                    }
                }
            }
            else
            {
                // * - 1
                Dictionary<Strategy, Strategy> diffCompositeRoleByAssociation;
                if (this.diffCompositeRoleByAssociationByRoleType.TryGetValue(roleType, out diffCompositeRoleByAssociation))
                {
                    foreach (var diffCompositeRoleByAssociationEntry in diffCompositeRoleByAssociation)
                    {
                        if (diffCompositeRoleByAssociationEntry.Value == role)
                        {
                            // match found
                            associations.Add(diffCompositeRoleByAssociationEntry.Key);
                        }
                    }
                }

                var originalAssociations = role.GetOriginalCompositesAssociation(associationType);
                foreach (var originalAssociation in originalAssociations)
                {
                    // Check if association hasn't been reassigned
                    Strategy diffCompositeRole;
                    if (diffCompositeRoleByAssociation != null &&
                        diffCompositeRoleByAssociation.TryGetValue(originalAssociation, out diffCompositeRole))
                    {
                        if (role.Equals(diffCompositeRole))
                        {
                            associations.Add(originalAssociation);
                        }
                    }
                    else
                    {
                        associations.Add(originalAssociation);
                    }
                }
            }

            return associations;
        }

        internal virtual void Clear()
        {
            this.databaseSession = null;

            this.syncedDeletedStrategies = null;
            this.rollbackSyncedDeletedStrategies = null;

            this.originalUnitRoleByAssociationByRoleType = null;
            this.orignalRollbackUnitRoleByAssociationByRoleType = null;
            this.diffUnitRoleByAssociationByRoleType = null;
            this.diffRollbackUnitRoleByAssociationByRoleType = null;

            this.originalCompositeRoleByAssociationByRoleType = null;
            this.originalRollbackCompositeRoleByAssociationByRoleType = null;
            this.diffCompositeRoleByAssociationByRoleType = null;
            this.diffRollbackCompositeRoleByAssociationByRoleType = null;

            this.originalCompositeRolesByAssociationByRoleType = null;
            this.originalrollbackCompositeRolesByAssociationByRoleType = null;
            this.diffCompositeRolesByAssociationByRoleType = null;
            this.diffRollbackCompositeRolesByAssociationByRoleType = null;

            this.preSyncIdsByPostSyncId = null;

            this.strategyByObjectId = null;

            this.busyCommittingOrRollingBack = false;

            this.synced = false;
        }

        internal virtual HashSet<Strategy> GetStrategiesForExtent(ObjectType objectType)
        {
            ObjectType[] concreteClasses;
            if (!this.concreteClassesByObjectType.TryGetValue(objectType, out concreteClasses))
            {
                var sortedClassAndSubclassList = new List<ObjectType>();

                if (objectType is Class)
                {
                    sortedClassAndSubclassList.Add(objectType);
                }

                if (objectType is Interface)
                {
                    foreach (var subClass in ((Interface)objectType).Subclasses)
                    {
                        if (subClass is Class)
                        {
                            sortedClassAndSubclassList.Add(subClass);
                        }
                    }
                }

                concreteClasses = sortedClassAndSubclassList.ToArray();

                this.concreteClassesByObjectType[objectType] = concreteClasses;
            }

            var strategies = new HashSet<Strategy>();
            foreach (var strategyByObjectIdEntry in this.strategyByObjectId)
            {
                var strategy = strategyByObjectIdEntry.Value;
                if (!strategy.IsDeleted && concreteClasses.Contains(strategy.ObjectType))
                {
                    strategies.Add(strategy);
                }
            }

            return strategies;
        }

        internal Strategy GetLocalCompositeRole(Strategy association, RoleType roleType)
        {
            Strategy compositeRole;
            Dictionary<Strategy, Strategy> compositeRoleByStrategy;
            if (this.diffCompositeRoleByAssociationByRoleType.TryGetValue(roleType, out compositeRoleByStrategy) &&
                compositeRoleByStrategy.TryGetValue(association, out compositeRole))
            {
                return compositeRole;
            }

            Dictionary<Strategy, Strategy> originalCompositeRoleByAssociation;
            if (this.originalCompositeRoleByAssociationByRoleType.TryGetValue(roleType, out originalCompositeRoleByAssociation))
            {
                Strategy originalCompositeRole;
                if (originalCompositeRoleByAssociation.TryGetValue(association, out originalCompositeRole))
                {
                    return originalCompositeRole;
                }
            }

            return null;
        }

        internal HashSet<Strategy> GetLocalCompositeRoles(Strategy association, RoleType roleType)
        {
            HashSet<Strategy> compositesRole;
            Dictionary<Strategy, HashSet<Strategy>> compositesRoleByStrategy;
            if (this.diffCompositeRolesByAssociationByRoleType.TryGetValue(roleType, out compositesRoleByStrategy) &&
                compositesRoleByStrategy.TryGetValue(association, out compositesRole))
            {
                return compositesRole;
            }

            Dictionary<Strategy, HashSet<Strategy>> originalCompositeRolesByAssociation;
            if (this.originalCompositeRolesByAssociationByRoleType.TryGetValue(roleType, out originalCompositeRolesByAssociation))
            {
                HashSet<Strategy> originalComposites;
                if (!originalCompositeRolesByAssociation.TryGetValue(association, out originalComposites))
                {
                    return originalComposites;
                }
            }

            return null;
        }

        protected internal virtual Strategy GetOriginalCompositeRole(Strategy association, RoleType roleType)
        {
            Dictionary<Strategy, Strategy> originalCompositeRoleByAssociation;
            if (!this.originalCompositeRoleByAssociationByRoleType.TryGetValue(roleType, out originalCompositeRoleByAssociation))
            {
                originalCompositeRoleByAssociation = new Dictionary<Strategy, Strategy>();
                this.originalCompositeRoleByAssociationByRoleType[roleType] = originalCompositeRoleByAssociation;
            }

            Strategy originalComposite;
            if (!originalCompositeRoleByAssociation.TryGetValue(association, out originalComposite))
            {
                originalComposite = association.GetOriginalCompositeRole(roleType);
                originalCompositeRoleByAssociation[association] = originalComposite;
            }

            return originalComposite;
        }

        protected internal virtual HashSet<Strategy> GetOriginalCompositeRoles(Strategy association, RoleType roleType)
        {
            Dictionary<Strategy, HashSet<Strategy>> originalCompositeRolesByAssociation;
            if (!this.originalCompositeRolesByAssociationByRoleType.TryGetValue(roleType, out originalCompositeRolesByAssociation))
            {
                originalCompositeRolesByAssociation = new Dictionary<Strategy, HashSet<Strategy>>();
                this.originalCompositeRolesByAssociationByRoleType[roleType] = originalCompositeRolesByAssociation;
            }

            HashSet<Strategy> originalComposites;
            if (!originalCompositeRolesByAssociation.TryGetValue(association, out originalComposites))
            {
                originalComposites = association.GetOriginalCompositeRoles(roleType);
                originalCompositeRolesByAssociation[association] = originalComposites;
            }

            return originalComposites;
        }

        protected virtual void SaveObjects(XmlWriter writer)
        {
            writer.WriteStartElement(Serialization.Objects);

            var databaseStrategiesByObjectType = new Dictionary<ObjectType, List<Strategy>>();
            var newStrategiesByObjectType = new Dictionary<ObjectType, List<Strategy>>();
            var deletedStrategiesByObjectType = new Dictionary<ObjectType, List<Strategy>>();

            foreach (var strategiesByObjectIdEntry in this.strategyByObjectId)
            {
                var strategy = strategiesByObjectIdEntry.Value;

                if (strategy.IsDeleted)
                {
                    if (!strategy.IsNewInWorkspace)
                    {
                        List<Strategy> deletedStrategies;
                        if (!deletedStrategiesByObjectType.TryGetValue(strategy.ObjectType, out deletedStrategies))
                        {
                            deletedStrategies = new List<Strategy>();
                            deletedStrategiesByObjectType.Add(strategy.ObjectType, deletedStrategies);
                        }

                        deletedStrategies.Add(strategy);
                    }
                }
                else
                {
                    if (strategy.IsNewInWorkspace)
                    {
                        List<Strategy> newStrategies;
                        if (!newStrategiesByObjectType.TryGetValue(strategy.ObjectType, out newStrategies))
                        {
                            newStrategies = new List<Strategy>();
                            newStrategiesByObjectType.Add(strategy.ObjectType, newStrategies);
                        }

                        newStrategies.Add(strategy);
                    }
                    else
                    {
                        List<Strategy> databaseStrategies;
                        if (!databaseStrategiesByObjectType.TryGetValue(strategy.ObjectType, out databaseStrategies))
                        {
                            databaseStrategies = new List<Strategy>();
                            databaseStrategiesByObjectType.Add(strategy.ObjectType, databaseStrategies);
                        }

                        databaseStrategies.Add(strategy);
                    }
                }
            }

            SaveDatabaseStrategies(writer, databaseStrategiesByObjectType);
            SaveWorkStrategies(writer, newStrategiesByObjectType, deletedStrategiesByObjectType);

            writer.WriteEndElement();
        }

        protected virtual void SaveDatabaseRelations(XmlWriter writer)
        {
            writer.WriteStartElement(Serialization.Database);

            foreach (var originalUnitRoleByAssociationByRoleTypeEntry in this.originalUnitRoleByAssociationByRoleType)
            {
                var roleType = originalUnitRoleByAssociationByRoleTypeEntry.Key;
                var originalUnitRoleByAssociation = originalUnitRoleByAssociationByRoleTypeEntry.Value;

                writer.WriteStartElement(Serialization.RelationTypeUnit);
                writer.WriteAttributeString(Serialization.Id, roleType.RelationType.IdAsString);

                foreach (var originalUnitRoleByAssociationEntry in originalUnitRoleByAssociation)
                {
                    var association = originalUnitRoleByAssociationEntry.Key;
                    if (!association.IsNewInWorkspace)
                    {
                        var role = originalUnitRoleByAssociationEntry.Value;

                        if (role != null)
                        {
                            writer.WriteStartElement(Serialization.Relation);
                            writer.WriteAttributeString(Serialization.Association, association.ObjectId.ToString());

                            var unitType = (Unit)roleType.ObjectType;
                            var unitTypeTag = (UnitTags)unitType.UnitTag;
                            var roleString = Serialization.WriteString(unitTypeTag, role);
                            writer.WriteString(roleString);

                            writer.WriteEndElement();
                        }
                        else
                        {
                            SaveEmptyRelation(writer, association);
                        }
                    }
                }

                writer.WriteEndElement();
            }

            foreach (var originalCompositeRoleByAssociationByRoleTypeEntry in this.originalCompositeRoleByAssociationByRoleType)
            {
                var roleType = originalCompositeRoleByAssociationByRoleTypeEntry.Key;
                var originalCompositeRoleByAssociation = originalCompositeRoleByAssociationByRoleTypeEntry.Value;

                writer.WriteStartElement(Serialization.RelationTypeComposite);
                writer.WriteAttributeString(Serialization.Id, roleType.RelationType.IdAsString);

                foreach (var originalCompositeRoleByAssociationEntry in originalCompositeRoleByAssociation)
                {
                    var association = originalCompositeRoleByAssociationEntry.Key;
                    if (!association.IsNewInWorkspace)
                    {
                        var role = originalCompositeRoleByAssociationEntry.Value;

                        if (role != null)
                        {
                            writer.WriteStartElement(Serialization.Relation);
                            writer.WriteAttributeString(Serialization.Association, association.ObjectId.ToString());

                            writer.WriteString(role.ObjectId.ToString());

                            writer.WriteEndElement();
                        }
                        else
                        {
                            SaveEmptyRelation(writer, association);
                        }
                    }
                }

                writer.WriteEndElement();
            }

            foreach (var originalCompositeRolesByAssociationByRoleTypeEntry in this.originalCompositeRolesByAssociationByRoleType)
            {
                var roleType = originalCompositeRolesByAssociationByRoleTypeEntry.Key;
                var originalCompositeRoleByAssociation = originalCompositeRolesByAssociationByRoleTypeEntry.Value;

                writer.WriteStartElement(Serialization.RelationTypeComposite);
                writer.WriteAttributeString(Serialization.Id, roleType.RelationType.IdAsString);

                foreach (var originalCompositeRolesByAssociationEntry in originalCompositeRoleByAssociation)
                {
                    var association = originalCompositeRolesByAssociationEntry.Key;
                    if (!association.IsNewInWorkspace)
                    {
                        var role = originalCompositeRolesByAssociationEntry.Value;

                        if (role != null)
                        {
                            writer.WriteStartElement(Serialization.Relation);
                            writer.WriteAttributeString(Serialization.Association, association.ObjectId.ToString());

                            var enumerator = role.GetEnumerator();
                            enumerator.MoveNext();
                            while (enumerator.Current != null)
                            {
                                writer.WriteString(enumerator.Current.ObjectId.ToString());
                                if (enumerator.MoveNext())
                                {
                                    writer.WriteString(Serialization.ObjectsSplitter);
                                }
                            }

                            writer.WriteEndElement();
                        }
                        else
                        {
                            SaveEmptyRelation(writer, association);
                        }
                    }
                }

                writer.WriteEndElement();
            }

            writer.WriteEndElement();
        }

        protected virtual void SaveWorkRelations(XmlWriter writer)
        {
            writer.WriteStartElement(Serialization.Workspace);

            foreach (var unitRoleByAssociationByRoleTypeEntry in this.diffUnitRoleByAssociationByRoleType)
            {
                var roleType = unitRoleByAssociationByRoleTypeEntry.Key;
                var unitRoleByAssociation = unitRoleByAssociationByRoleTypeEntry.Value;

                writer.WriteStartElement(Serialization.RelationTypeUnit);
                writer.WriteAttributeString(Serialization.Id, roleType.RelationType.IdAsString);

                foreach (var unitRoleByAssociationEntry in unitRoleByAssociation)
                {
                    var association = unitRoleByAssociationEntry.Key;
                    var role = unitRoleByAssociationEntry.Value;

                    if (role != null)
                    {
                        writer.WriteStartElement(Serialization.Relation);
                        writer.WriteAttributeString(Serialization.Association, association.ObjectId.ToString());

                        var unitType = (Unit)roleType.ObjectType;
                        var unitTypeTag = (UnitTags)unitType.UnitTag;
                        var roleString = Serialization.WriteString(unitTypeTag, role);
                        writer.WriteString(roleString);

                        writer.WriteEndElement();
                    }
                    else
                    {
                        SaveEmptyRelation(writer, association);
                    }
                }

                writer.WriteEndElement();
            }

            foreach (var compositeRoleByAssociationByRoleTypeEntry in this.diffCompositeRoleByAssociationByRoleType)
            {
                var roleType = compositeRoleByAssociationByRoleTypeEntry.Key;
                var compositeRoleByAssociation = compositeRoleByAssociationByRoleTypeEntry.Value;

                writer.WriteStartElement(Serialization.RelationTypeComposite);
                writer.WriteAttributeString(Serialization.Id, roleType.RelationType.IdAsString);

                foreach (var compositeRoleByAssociationEntry in compositeRoleByAssociation)
                {
                    var association = compositeRoleByAssociationEntry.Key;
                    var role = compositeRoleByAssociationEntry.Value;

                    if (role != null)
                    {
                        writer.WriteStartElement(Serialization.Relation);
                        writer.WriteAttributeString(Serialization.Association, association.ObjectId.ToString());

                        writer.WriteString(role.ObjectId.ToString());

                        writer.WriteEndElement();
                    }
                    else
                    {
                        SaveEmptyRelation(writer, association);
                    }
                }

                writer.WriteEndElement();
            }

            foreach (var compositeRolesByAssociationByRoleTypeEntry in this.diffCompositeRolesByAssociationByRoleType)
            {
                var roleType = compositeRolesByAssociationByRoleTypeEntry.Key;
                var compositeRoleByAssociation = compositeRolesByAssociationByRoleTypeEntry.Value;

                writer.WriteStartElement(Serialization.RelationTypeComposite);
                writer.WriteAttributeString(Serialization.Id, roleType.RelationType.IdAsString);

                foreach (var compositeRolesByAssociationEntry in compositeRoleByAssociation)
                {
                    var association = compositeRolesByAssociationEntry.Key;
                    var role = compositeRolesByAssociationEntry.Value;

                    if (role != null)
                    {
                        writer.WriteStartElement(Serialization.Relation);
                        writer.WriteAttributeString(Serialization.Association, association.ObjectId.ToString());

                        var enumerator = role.GetEnumerator();
                        enumerator.MoveNext();
                        while (enumerator.Current != null)
                        {
                            writer.WriteString(enumerator.Current.ObjectId.ToString());
                            if (enumerator.MoveNext())
                            {
                                writer.WriteString(Serialization.ObjectsSplitter);
                            }
                        }

                        writer.WriteEndElement();
                    }
                    else
                    {
                        SaveEmptyRelation(writer, association);
                    }
                }

                writer.WriteEndElement();
            }

            writer.WriteEndElement();
        }

        protected virtual void LoadDatabaseObjects(XmlReader reader)
        {
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    // eat everything but elements
                    case XmlNodeType.Element:
                        if (reader.Name.Equals(Serialization.ObjectType))
                        {
                            if (!reader.IsEmptyElement)
                            {
                                var objectTypeIdString = reader.GetAttribute(Serialization.Id);
                                if (string.IsNullOrEmpty(objectTypeIdString))
                                {
                                    throw new Exception("Object type has no id");
                                }

                                var objectTypeId = new Guid(objectTypeIdString);
                                var objectType = this.MemoryWorkspace.Database.ObjectFactory.GetObjectTypeForType(objectTypeId);

                                var objectIdsString = reader.ReadString();
                                var objectIds = objectIdsString.Split(Serialization.ObjectsSplitterCharArray);

                                foreach (var objectIdString in objectIds)
                                {
                                    var objectId = this.MemoryWorkspace.ObjectIds.Parse(objectIdString);

                                    var @class = objectType as Class;
                                    if (@class == null)
                                    {
                                        throw new Exception("Could not load object with id " + objectId);
                                    }

                                    Strategy strategy;
                                    if (this.strategyByObjectId.TryGetValue(objectId, out strategy))
                                    {
                                        throw new Exception("Duplicate id");
                                    }

                                    this.MemoryWorkspace.ObjectIds.AdjustCurrentId(objectId);
                                    strategy = this.LoadStrategy(@class, objectId);
                                    this.strategyByObjectId.Add(strategy.ObjectId, strategy);
                                }
                            }
                        }

                        break;
                    case XmlNodeType.EndElement:
                        return;
                }
            }
        }

        protected virtual void LoadDatabaseRelations(XmlReader reader)
        {
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    // eat everything but elements
                    case XmlNodeType.Element:
                        if (reader.Name.Equals(Serialization.RelationTypeUnit))
                        {
                            if (!reader.IsEmptyElement)
                            {
                                this.LoadDatabaseRelations(reader, true);
                            }
                        }
                        else if (reader.Name.Equals(Serialization.RelationTypeComposite))
                        {
                            if (!reader.IsEmptyElement)
                            {
                                this.LoadDatabaseRelations(reader, false);
                            }
                        }

                        break;
                    case XmlNodeType.EndElement:
                        return;
                }
            }
        }

        protected virtual void LoadDatabaseRelations(XmlReader reader, bool isUnit)
        {
            var relationTypeIdString = reader.GetAttribute(Serialization.Id);
            if (string.IsNullOrEmpty(relationTypeIdString))
            {
                throw new Exception("Role type id is missing");
            }

            var relationTypeId = new Guid(relationTypeIdString);
            var roleType = ((RelationType)this.Workspace.Database.Domain.Find(relationTypeId)).RoleType;

            if (!reader.IsEmptyElement)
            {
                if (roleType == null ||
                    roleType.ObjectType is Unit != isUnit)
                {
                    throw new Exception("RoleType changed during Save/Load");
                }

                if (roleType.ObjectType is Unit)
                {
                    this.LoadDatabaseUnitRelations(reader, roleType);
                }
                else
                {
                    this.LoadDatabaseCompositeRelations(reader, roleType);
                }
            }
        }

        protected virtual void LoadDatabaseUnitRelations(XmlReader reader, RoleType roleType)
        {
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    // eat everything but elements
                    case XmlNodeType.Element:
                        var relationKind = reader.Name;
                        if (relationKind.Equals(Serialization.Relation) ||
                            relationKind.Equals(Serialization.NoRelation))
                        {
                            var associationString = reader.GetAttribute(Serialization.Association);
                            if (string.IsNullOrEmpty(associationString))
                            {
                                throw new Exception("Association is missing");
                            }

                            var a = this.MemoryWorkspace.ObjectIds.Parse(associationString);

                            Strategy association;
                            if (!this.strategyByObjectId.TryGetValue(a, out association))
                            {
                                throw new Exception("Could not instantiate association with id " + a);
                            }

                            try
                            {
                                object unit = null;
                                if (relationKind.Equals(Serialization.Relation))
                                {
                                    unit = association.LoadUnitRole(reader, roleType);
                                }

                                Dictionary<Strategy, object> originalUnitRoleByAssociation;
                                if (!this.originalUnitRoleByAssociationByRoleType.TryGetValue(roleType, out originalUnitRoleByAssociation))
                                {
                                    originalUnitRoleByAssociation = new Dictionary<Strategy, object>();
                                    this.originalUnitRoleByAssociationByRoleType[roleType] = originalUnitRoleByAssociation;
                                }

                                originalUnitRoleByAssociation[association] = unit;
                            }
                            catch
                            {
                                throw new Exception("Could not load unit relation");
                            }
                        }

                        break;
                    case XmlNodeType.EndElement:
                        return;
                }
            }
        }

        protected virtual void LoadDatabaseCompositeRelations(XmlReader reader, RoleType roleType)
        {
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.Name.Equals(Serialization.Relation) ||
                            reader.Name.Equals(Serialization.NoRelation))
                        {
                            var associationString = reader.GetAttribute(Serialization.Association);
                            var a = this.MemoryWorkspace.ObjectIds.Parse(associationString);

                            Strategy association;
                            if (!this.strategyByObjectId.TryGetValue(a, out association))
                            {
                                throw new Exception("Could not instantiate association with id " + a);
                            }

                            if (roleType.IsMany)
                            {
                                HashSet<Strategy> roles = null;
                                if (reader.Name.Equals(Serialization.Relation))
                                {
                                    roles = association.LoadDatabaseCompositesRole(reader, roleType);
                                }

                                Dictionary<Strategy, HashSet<Strategy>> originalCompositesRoleByAssociation;
                                if (!this.originalCompositeRolesByAssociationByRoleType.TryGetValue(roleType, out originalCompositesRoleByAssociation))
                                {
                                    originalCompositesRoleByAssociation = new Dictionary<Strategy, HashSet<Strategy>>();
                                    this.originalCompositeRolesByAssociationByRoleType[roleType] = originalCompositesRoleByAssociation;
                                }

                                originalCompositesRoleByAssociation[association] = roles;
                            }
                            else
                            {
                                Strategy role = null;
                                if (reader.Name.Equals(Serialization.Relation))
                                {
                                    role = association.LoadDatabaseCompositeRole(reader, roleType);
                                }

                                Dictionary<Strategy, Strategy> originalCompositeRoleByAssociation;
                                if (!this.originalCompositeRoleByAssociationByRoleType.TryGetValue(roleType, out originalCompositeRoleByAssociation))
                                {
                                    originalCompositeRoleByAssociation = new Dictionary<Strategy, Strategy>();
                                    this.originalCompositeRoleByAssociationByRoleType[roleType] = originalCompositeRoleByAssociation;
                                }

                                originalCompositeRoleByAssociation[association] = role;
                            }
                        }

                        break;
                    case XmlNodeType.EndElement:
                        return;
                }
            }
        }

        protected virtual void LoadWorkspaceObjects(XmlReader reader)
        {
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    // eat everything but elements
                    case XmlNodeType.Element:
                        if (reader.Name.Equals(Serialization.New))
                        {
                            if (!reader.IsEmptyElement)
                            {
                                this.LoadNewWorkspaceObjects(reader);
                            }
                        }
                        else if (reader.Name.Equals(Serialization.Deleted))
                        {
                            if (!reader.IsEmptyElement)
                            {
                                this.LoadDeletedWorkspaceObjects(reader);
                            }
                        }

                        break;
                    case XmlNodeType.EndElement:
                        return;
                }
            }
        }

        protected virtual void LoadNewWorkspaceObjects(XmlReader reader)
        {
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    // eat everything but elements
                    case XmlNodeType.Element:
                        if (reader.Name.Equals(Serialization.ObjectType))
                        {
                            if (!reader.IsEmptyElement)
                            {
                                var objectTypeIdString = reader.GetAttribute(Serialization.Id);
                                if (string.IsNullOrEmpty(objectTypeIdString))
                                {
                                    throw new Exception("Object type has no id");
                                }

                                var objectTypeId = new Guid(objectTypeIdString);
                                var objectType = this.MemoryWorkspace.Database.ObjectFactory.GetObjectTypeForType(objectTypeId);

                                var objectIdsString = reader.ReadString();
                                var objectIds = objectIdsString.Split(Serialization.ObjectsSplitterCharArray);

                                foreach (var objectIdString in objectIds)
                                {
                                    var objectId = this.MemoryWorkspace.ObjectIds.Parse(objectIdString);

                                    var @class = objectType as Class;
                                    if (@class == null)
                                    {
                                        throw new Exception("Could not load object with id " + objectId);
                                    }

                                    Strategy strategy;
                                    if (this.strategyByObjectId.TryGetValue(objectId, out strategy))
                                    {
                                        throw new Exception("Duplicate id");
                                    }

                                    this.MemoryWorkspace.ObjectIds.AdjustCurrentId(objectId);
                                    strategy = new Strategy(this, @class, objectId, false);
                                    this.strategyByObjectId.Add(strategy.ObjectId, strategy);
                                }
                            }
                        }

                        break;
                    case XmlNodeType.EndElement:
                        return;
                }
            }
        }

        protected virtual void LoadDeletedWorkspaceObjects(XmlReader reader)
        {
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    // eat everything but elements
                    case XmlNodeType.Element:
                        if (reader.Name.Equals(Serialization.ObjectType))
                        {
                            if (!reader.IsEmptyElement)
                            {
                                var objectTypeIdString = reader.GetAttribute(Serialization.Id);
                                if (string.IsNullOrEmpty(objectTypeIdString))
                                {
                                    throw new Exception("Object type has no id");
                                }

                                var objectTypeId = new Guid(objectTypeIdString);
                                var objectType = this.MemoryWorkspace.Database.ObjectFactory.GetObjectTypeForType(objectTypeId);

                                var objectIdsString = reader.ReadString();
                                var objectIds = objectIdsString.Split(Serialization.ObjectsSplitterCharArray);

                                foreach (var objectIdString in objectIds)
                                {
                                    var objectId = this.MemoryWorkspace.ObjectIds.Parse(objectIdString);

                                    var @class = objectType as Class;
                                    if (@class == null)
                                    {
                                        throw new Exception("Could not load object with id " + objectId);
                                    }

                                    Strategy strategy;
                                    if (this.strategyByObjectId.TryGetValue(objectId, out strategy))
                                    {
                                        throw new Exception("Duplicate id");
                                    }

                                    this.MemoryWorkspace.ObjectIds.AdjustCurrentId(objectId);
                                    strategy = new Strategy(this, @class, objectId, true);
                                    this.strategyByObjectId.Add(strategy.ObjectId, strategy);
                                }
                            }
                        }

                        break;
                    case XmlNodeType.EndElement:
                        return;
                }
            }
        }

        protected virtual void LoadWorkspaceRelations(XmlReader reader)
        {
            while (reader.Read())
            {
                // eat everything but elements
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.Name.Equals(Serialization.RelationTypeUnit))
                        {
                            if (!reader.IsEmptyElement)
                            {
                                this.LoadWorkspaceRelations(reader, true);
                            }
                        }
                        else if (reader.Name.Equals(Serialization.RelationTypeComposite))
                        {
                            if (!reader.IsEmptyElement)
                            {
                                this.LoadWorkspaceRelations(reader, false);
                            }
                        }

                        break;
                    case XmlNodeType.EndElement:
                        return;
                }
            }
        }

        protected virtual void LoadWorkspaceRelations(XmlReader reader, bool isUnit)
        {
            var relationTypeIdString = reader.GetAttribute(Serialization.Id);
            if (string.IsNullOrEmpty(relationTypeIdString))
            {
                throw new Exception("Role type id is missing");
            }

            var relationTypeId = new Guid(relationTypeIdString);
            var relationType = ((RelationType)this.Workspace.Database.Domain.Find(relationTypeId)).RoleType;

            if (!reader.IsEmptyElement)
            {
                if (relationType == null ||
                    relationType.ObjectType is Unit != isUnit)
                {
                    throw new Exception("RoleType changed during Save/Load");
                }

                if (relationType.ObjectType is Unit)
                {
                    this.LoadWorkspaceUnitRelations(reader, relationType);
                }
                else
                {
                    this.LoadWorkspaceCompositeRelations(reader, relationType);
                }
            }
        }

        protected virtual void LoadWorkspaceUnitRelations(XmlReader reader, RoleType roleType)
        {
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    // eat everything but elements
                    case XmlNodeType.Element:
                        if (reader.Name.Equals(Serialization.Relation) ||
                            reader.Name.Equals(Serialization.NoRelation))
                        {
                            var associationString = reader.GetAttribute(Serialization.Association);
                            if (string.IsNullOrEmpty(associationString))
                            {
                                throw new Exception("Association is missing");
                            }

                            var a = this.MemoryWorkspace.ObjectIds.Parse(associationString);

                            Strategy association;
                            if (!this.strategyByObjectId.TryGetValue(a, out association))
                            {
                                throw new Exception("Could not instantiate association with id " + a);
                            }

                            object unit = null;
                            if (reader.Name.Equals(Serialization.Relation))
                            {
                                unit = association.LoadUnitRole(reader, roleType);
                            }

                            Dictionary<Strategy, object> originalUnitRoleByAssociation;
                            if (!this.originalUnitRoleByAssociationByRoleType.TryGetValue(roleType, out originalUnitRoleByAssociation))
                            {
                                originalUnitRoleByAssociation = new Dictionary<Strategy, object>();
                                this.originalUnitRoleByAssociationByRoleType[roleType] = originalUnitRoleByAssociation;
                            }

                            object originalUnitRole;
                            if (!originalUnitRoleByAssociation.TryGetValue(association, out originalUnitRole))
                            {
                                // orignal unit role for IsNewInWorkspace objects
                                originalUnitRoleByAssociation[association] = null;
                            }

                            Dictionary<Strategy, object> diffUnitRoleByAssociation;
                            if (!this.diffUnitRoleByAssociationByRoleType.TryGetValue(roleType, out diffUnitRoleByAssociation))
                            {
                                diffUnitRoleByAssociation = new Dictionary<Strategy, object>();
                                this.diffUnitRoleByAssociationByRoleType[roleType] = diffUnitRoleByAssociation;
                            }

                            diffUnitRoleByAssociation[association] = unit;
                        }

                        break;
                    case XmlNodeType.EndElement:
                        return;
                }
            }
        }

        protected virtual void LoadWorkspaceCompositeRelations(XmlReader reader, RoleType roleType)
        {
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    // eat everything but elements
                    case XmlNodeType.Element:
                        if (reader.Name.Equals(Serialization.Relation) ||
                            reader.Name.Equals(Serialization.NoRelation))
                        {
                            var associationString = reader.GetAttribute(Serialization.Association);
                            var a = this.MemoryWorkspace.ObjectIds.Parse(associationString);

                            Strategy association;
                            if (!this.strategyByObjectId.TryGetValue(a, out association))
                            {
                                throw new Exception("Could not instantiate association with id " + a);
                            }

                            if (roleType.IsMany)
                            {
                                HashSet<Strategy> roles = null;
                                if (reader.Name.Equals(Serialization.Relation))
                                {
                                    roles = association.LoadDatabaseCompositesRole(reader, roleType);
                                }

                                Dictionary<Strategy, HashSet<Strategy>> originalCompositeRolesByAssociation;
                                if (!this.originalCompositeRolesByAssociationByRoleType.TryGetValue(roleType, out originalCompositeRolesByAssociation))
                                {
                                    originalCompositeRolesByAssociation = new Dictionary<Strategy, HashSet<Strategy>>();
                                    this.originalCompositeRolesByAssociationByRoleType[roleType] = originalCompositeRolesByAssociation;
                                }

                                HashSet<Strategy> originalCompositeRoles;
                                if (!originalCompositeRolesByAssociation.TryGetValue(association, out originalCompositeRoles))
                                {
                                    // orignal composite roles for IsNewInWorkspace objects
                                    originalCompositeRolesByAssociation[association] = null;
                                }

                                Dictionary<Strategy, HashSet<Strategy>> compositesRoleByAssociation;
                                if (!this.diffCompositeRolesByAssociationByRoleType.TryGetValue(roleType, out compositesRoleByAssociation))
                                {
                                    compositesRoleByAssociation = new Dictionary<Strategy, HashSet<Strategy>>();
                                    this.diffCompositeRolesByAssociationByRoleType[roleType] = compositesRoleByAssociation;
                                }

                                compositesRoleByAssociation[association] = roles;
                            }
                            else
                            {
                                Strategy role = null;
                                if (reader.Name.Equals(Serialization.Relation))
                                {
                                    role = association.LoadDatabaseCompositeRole(reader, roleType);
                                }

                                Dictionary<Strategy, Strategy> originalCompositeRoleByAssociation;
                                if (!this.originalCompositeRoleByAssociationByRoleType.TryGetValue(roleType, out originalCompositeRoleByAssociation))
                                {
                                    originalCompositeRoleByAssociation = new Dictionary<Strategy, Strategy>();
                                    this.originalCompositeRoleByAssociationByRoleType[roleType] = originalCompositeRoleByAssociation;
                                }

                                Strategy originalCompositeRole;
                                if (!originalCompositeRoleByAssociation.TryGetValue(association, out originalCompositeRole))
                                {
                                    // orignal composite role for IsNewInWorkspace objects
                                    originalCompositeRoleByAssociation[association] = null;
                                }

                                Dictionary<Strategy, Strategy> compositeRoleByAssociation;
                                if (!this.diffCompositeRoleByAssociationByRoleType.TryGetValue(roleType, out compositeRoleByAssociation))
                                {
                                    compositeRoleByAssociation = new Dictionary<Strategy, Strategy>();
                                    this.diffCompositeRoleByAssociationByRoleType[roleType] = compositeRoleByAssociation;
                                }

                                compositeRoleByAssociation[association] = role;
                            }
                        }

                        break;
                    case XmlNodeType.EndElement:
                        return;
                }
            }
        }

        protected virtual object GetOriginalUnitRole(RoleType roleType, Strategy association)
        {
            Dictionary<Strategy, object> originalUnitRoleByAssociation;
            if (!this.originalUnitRoleByAssociationByRoleType.TryGetValue(roleType, out originalUnitRoleByAssociation))
            {
                originalUnitRoleByAssociation = new Dictionary<Strategy, object>();
                this.originalUnitRoleByAssociationByRoleType[roleType] = originalUnitRoleByAssociation;
            }

            object originalUnit;
            if (!originalUnitRoleByAssociation.TryGetValue(association, out originalUnit))
            {
                originalUnit = association.GetOriginalUnitRole(roleType);
                originalUnitRoleByAssociation[association] = originalUnit;
            }

            return originalUnit;
        }

        protected virtual Dictionary<Strategy, Strategy> GetCompositeRoleByAssociation(RoleType roleType)
        {
            Dictionary<Strategy, Strategy> compositeRoleByAssociation;
            if (!this.diffCompositeRoleByAssociationByRoleType.TryGetValue(roleType, out compositeRoleByAssociation))
            {
                compositeRoleByAssociation = new Dictionary<Strategy, Strategy>();
                this.diffCompositeRoleByAssociationByRoleType[roleType] = compositeRoleByAssociation;
            }

            return compositeRoleByAssociation;
        }

        protected virtual Dictionary<Strategy, HashSet<Strategy>> GetCompositeRolesByAssociation(RoleType roleType)
        {
            Dictionary<Strategy, HashSet<Strategy>> compositeRolesByAssociation;
            if (!this.diffCompositeRolesByAssociationByRoleType.TryGetValue(roleType, out compositeRolesByAssociation))
            {
                compositeRolesByAssociation = new Dictionary<Strategy, HashSet<Strategy>>();
                this.diffCompositeRolesByAssociationByRoleType[roleType] = compositeRolesByAssociation;
            }

            return compositeRolesByAssociation;
        }

        protected virtual Strategy CreateNewStrategy(Class objectType)
        {
            return new Strategy(this, objectType, this.MemoryWorkspace.ObjectIds.Next());
        }

        protected virtual Strategy GetStrategy(IStrategy databaseStrategy)
        {
            return new Strategy(this, databaseStrategy);
        }

        protected virtual Strategy LoadStrategy(Class objectType, ObjectId objectId)
        {
            return new Strategy(this, objectType, objectId, false);
        }

        private static bool AreUnitsEqual(RoleType roleType, object role1, object role2)
        {
            if (role1 == null)
            {
                if (role2 != null)
                {
                    return false;
                }
            }
            else
            {
                if (role2 == null)
                {
                    return false;
                }

                var unitType = roleType.ObjectType as Unit;
                if (unitType != null && unitType.IsBinary)
                {
                    var binary1 = (byte[])role1;
                    var binary2 = (byte[])role2;

                    if (binary1.Length == binary2.Length)
                    {
                        for (var i = 0; i < binary1.Length; i++)
                        {
                            if (binary1[i] != binary2[i])
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (!Equals(role1, role2))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static bool AreCompositesEqual(HashSet<Strategy> role1, HashSet<Strategy> role2)
        {
            if (role1 == null || role1.Count == 0)
            {
                if (role2 != null && role2.Count > 0)
                {
                    return false;
                }
            }
            else
            {
                if (role2 == null || role2.Count() == 0)
                {
                    return false;
                }

                if (role1.Count == role2.Count)
                {
                    foreach (var role1Strategy in role1)
                    {
                        if (!role2.Contains(role1Strategy))
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        private static void SaveDatabaseStrategies(XmlWriter writer, Dictionary<ObjectType, List<Strategy>> databaseStrategiesByObjectType)
        {
            writer.WriteStartElement(Serialization.Database);

            foreach (var dictionaryEntry in databaseStrategiesByObjectType)
            {
                var objectType = dictionaryEntry.Key;
                var databaseStrategies = dictionaryEntry.Value;

                SaveStrategies(writer, objectType, databaseStrategies);
            }

            writer.WriteEndElement();
        }

        private static void SaveWorkStrategies(XmlWriter writer, Dictionary<ObjectType, List<Strategy>> newStrategiesByObjectType, Dictionary<ObjectType, List<Strategy>> deletedStrategiesByObjectType)
        {
            writer.WriteStartElement(Serialization.Workspace);

            writer.WriteStartElement(Serialization.New);

            foreach (var dictionaryEntry in newStrategiesByObjectType)
            {
                var objectType = dictionaryEntry.Key;
                var workStrategies = dictionaryEntry.Value;

                SaveStrategies(writer, objectType, workStrategies);
            }

            writer.WriteEndElement();

            writer.WriteStartElement(Serialization.Deleted);

            foreach (var dictionaryEntry in deletedStrategiesByObjectType)
            {
                var objectType = dictionaryEntry.Key;
                var deletedStrategies = dictionaryEntry.Value;

                SaveStrategies(writer, objectType, deletedStrategies);
            }

            writer.WriteEndElement();

            writer.WriteEndElement();
        }

        private static void SaveStrategies(XmlWriter writer, ObjectType objectType, List<Strategy> strategies)
        {
            writer.WriteStartElement(Serialization.ObjectType);
            writer.WriteAttributeString(Serialization.Id, objectType.IdAsString);

            for (var i = 0; i < strategies.Count; i++)
            {
                if (i > 0)
                {
                    writer.WriteString(Serialization.ObjectsSplitter);
                }

                var strategy = strategies[i];
                writer.WriteString(strategy.ObjectId.ToString());
            }

            writer.WriteEndElement();
        }

        private static void SaveEmptyRelation(XmlWriter writer, Strategy association)
        {
            writer.WriteStartElement(Serialization.NoRelation);
            writer.WriteAttributeString(Serialization.Association, association.ObjectId.ToString());

            writer.WriteEndElement();
        }
    }
}