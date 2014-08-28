// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Strategy.cs" company="Allors bvba">
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

namespace Allors.Adapters.Database.Memory
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;

    using Allors.Meta;

    public sealed class Strategy : IStrategy
    {
        private readonly Session session;
        private readonly Class objectType;

        private readonly Dictionary<RoleType, object> unitRoleByRoleType;
        private readonly Dictionary<RoleType, Strategy> compositeRoleByRoleType;
        private readonly Dictionary<RoleType, HashSet<Strategy>> compositesRoleByRoleType;
        private readonly Dictionary<AssociationType, Strategy> compositeAssociationByAssociationType;
        private readonly Dictionary<AssociationType, HashSet<Strategy>> compositesAssociationByAssociationType;
        
        private Dictionary<RoleType, object> rollbackUnitRoleByRoleType;
        private Dictionary<RoleType, Strategy> rollbackCompositeRoleByRoleType;
        private Dictionary<RoleType, HashSet<Strategy>> rollbackCompositesRoleByRoleType;
        private Dictionary<AssociationType, Strategy> rollbackCompositeAssociationByAssociationType;
        private Dictionary<AssociationType, HashSet<Strategy>> rollbackCompositesAssociationByAssociationType;
        
        // TODO: move to a BitFlag
        private bool isDeleted;
        private bool isDeletedOnRollback;
        private bool isNew;

        private WeakReference allorizedObjectWeakReference;

        internal Strategy(Session session, Class objectType, ObjectId objectId)
        {
            this.session = session;
            this.objectType = objectType;
            this.ObjectId = objectId;

            this.isDeleted = false;
            this.isDeletedOnRollback = true;
            this.isNew = true;

            this.unitRoleByRoleType = new Dictionary<RoleType, object>();
            this.compositeRoleByRoleType = new Dictionary<RoleType, Strategy>();
            this.compositesRoleByRoleType = new Dictionary<RoleType, HashSet<Strategy>>();
            this.compositeAssociationByAssociationType = new Dictionary<AssociationType, Strategy>();
            this.compositesAssociationByAssociationType = new Dictionary<AssociationType, HashSet<Strategy>>();

            this.rollbackUnitRoleByRoleType = null;
            this.rollbackCompositeRoleByRoleType = null;
            this.rollbackCompositesRoleByRoleType = null;
            this.rollbackCompositeAssociationByAssociationType = null;
            this.rollbackCompositesAssociationByAssociationType = null;
        }

        public bool IsDeleted
        {
            get { return this.isDeleted; }
        }

        public bool IsNewInSession
        {
            get
            {
                return this.isNew;
            }
        }

        public bool IsNewInWorkspace
        {
            get
            {
                return false;
            }
        }

        public ObjectId ObjectId { get; internal set; }

        public Class ObjectType
        {
            get { return this.objectType; }
        }

        public ISession Session
        {
            get { return this.session; }
        }

        public IDatabaseSession DatabaseSession
        {
            get
            {
                return this.session;
            }
        }

        internal Session MemorySession
        {
            get { return this.session; }
        }

        private ChangeSet ChangeSet
        {
            get
            {
                return this.MemorySession.MemoryChangeSet;
            }
        }

        private Dictionary<RoleType, object> RollbackUnitRoleByRoleType 
        {
            get
            {
                return this.rollbackUnitRoleByRoleType
                       ?? (this.rollbackUnitRoleByRoleType = new Dictionary<RoleType, object>());
            }
        }

        private Dictionary<RoleType, Strategy> RollbackCompositeRoleByRoleType 
        {
            get
            {
                return this.rollbackCompositeRoleByRoleType
                       ?? (this.rollbackCompositeRoleByRoleType = new Dictionary<RoleType, Strategy>());
            }
        }

        private Dictionary<RoleType, HashSet<Strategy>> RollbackCompositesRoleByRoleType 
        {
            get
            {
                return this.rollbackCompositesRoleByRoleType
                       ?? (this.rollbackCompositesRoleByRoleType = new Dictionary<RoleType, HashSet<Strategy>>());
            }
        }

        private Dictionary<AssociationType, Strategy> RollbackCompositeAssociationByAssociationType 
        {
            get
            {
                return this.rollbackCompositeAssociationByAssociationType
                    ?? (this.rollbackCompositeAssociationByAssociationType = new Dictionary<AssociationType, Strategy>());
            }
        }

        private Dictionary<AssociationType, HashSet<Strategy>> RollbackCompositesAssociationByAssociationType 
        {
            get
            {
                return this.rollbackCompositesAssociationByAssociationType
                    ?? (this.rollbackCompositesAssociationByAssociationType = new Dictionary<AssociationType, HashSet<Strategy>>());
            }
        }
        
        public override string ToString()
        {
            return this.objectType.Name + " " + this.ObjectId;
        }

        public object GetRole(RoleType roleType)
        {
            if (roleType.ObjectType is Unit)
            {
                return this.GetUnitRole(roleType);
            }

            if (roleType.IsMany)
            {
                return this.GetCompositeRoles(roleType);
            }

            return this.GetCompositeRole(roleType);
        }

        public void SetRole(RoleType roleType, object value)
        {
            if (roleType.ObjectType is Unit)
            {
                this.SetUnitRole(roleType, value);
            }
            else
            {
                if (roleType.IsMany)
                {
                    var roles = value as Allors.Extent;
                    if (roles == null)
                    {
                        var roleList = new ArrayList((ICollection)value);
                        roles = (IObject[])roleList.ToArray(typeof(IObject));
                    }

                    this.SetCompositeRoles(roleType, roles);
                }
                else
                {
                    this.SetCompositeRole(roleType, (IObject)value);
                }
            }
        }

        public void RemoveRole(RoleType roleType)
        {
            if (roleType.ObjectType is Unit)
            {
                this.RemoveUnitRole(roleType);
            }
            else
            {
                if (roleType.IsMany)
                {
                    this.RemoveCompositeRoles(roleType);
                }
                else
                {
                    this.RemoveCompositeRole(roleType);
                }
            }
        }

        public bool ExistRole(RoleType roleType)
        {
            if (roleType.ObjectType is Unit)
            {
                return this.ExistUnitRole(roleType);
            }

            if (roleType.IsMany)
            {
                return this.ExistCompositeRoles(roleType);
            }

            return this.ExistCompositeRole(roleType);
        }

        public object GetUnitRole(RoleType roleType)
        {
            this.CheckRemoved();
            this.session.MemoryDatabase.UnitRoleChecks(this, roleType);

            var unitRole = this.GetInternalizedUnitRole(roleType);
            return unitRole;
        }
        
        public void SetUnitRole(RoleType roleType, object role)
        {
            this.CheckRemoved();
            this.session.MemoryDatabase.UnitRoleChecks(this, roleType);

            if (!this.RollbackUnitRoleByRoleType.ContainsKey(roleType))
            {
                this.RollbackUnitRoleByRoleType[roleType] = this.GetInternalizedUnitRole(roleType);
            }

            this.ChangeSet.OnChangingUnitRole(this, roleType);

            if (role == null)
            {
                this.unitRoleByRoleType.Remove(roleType);
            }
            else
            {
                role = this.session.MemoryDatabase.Internalize(role, roleType);
                this.unitRoleByRoleType[roleType] = role;
            }
        }

        public void RemoveUnitRole(RoleType roleType)
        {
            this.SetUnitRole(roleType, null);
        }

        public bool ExistUnitRole(RoleType roleType)
        {
            this.CheckRemoved();
            this.session.MemoryDatabase.UnitRoleChecks(this, roleType);
            return this.unitRoleByRoleType.ContainsKey(roleType);
        }

        public IObject GetCompositeRole(RoleType roleType)
        {
            this.CheckRemoved();

            Strategy strategy;
            this.compositeRoleByRoleType.TryGetValue(roleType, out strategy);

            if (strategy != null)
            {
                return strategy.GetObject();
            }

            return null;
        }

        public void SetCompositeRole(RoleType roleType, IObject newRole)
        {
            if (newRole == null)
            {
                this.RemoveCompositeRole(roleType);
            }
            else
            {
                if (roleType.AssociationType.IsOne)
                {
                    // 1-1
                    this.SetCompositeRoleOne2One(roleType, newRole);
                }
                else
                {
                    // *-1
                    this.SetCompositeRoleMany2One(roleType, newRole);
                }
            }
        }

        public void RemoveCompositeRole(RoleType roleType)
        {
            if (roleType.AssociationType.IsOne)
            {
                // 1-1
                this.RemoveCompositeRoleOne2One(roleType);
            }
            else
            {
                // *-1
                this.RemoveCompositeRoleMany2One(roleType);
            }
        }

        public bool ExistCompositeRole(RoleType roleType)
        {
            this.CheckRemoved();
            return this.compositeRoleByRoleType.ContainsKey(roleType);
        }

        public Allors.Extent GetCompositeRoles(RoleType roleType)
        {
            this.CheckRemoved();

            return new ExtentSwitch(this, roleType);
        }

        public void SetCompositeRoles(RoleType roleType, Allors.Extent roles)
        {
            if (roles == null || roles.Count == 0)
            {
                this.RemoveCompositeRoles(roleType);
            }
            else
            {
                this.CheckRemoved();

                var newStrategies = new HashSet<Strategy>();
                foreach (IObject role in roles)
                {
                    if (role != null)
                    {
                        this.session.MemoryDatabase.CompositeRolesChecks(this, roleType, role);

                        var roleStrategy = this.session.GetStrategy(role.Strategy.ObjectId);
                        newStrategies.Add(roleStrategy);
                    }
                }

                if (roleType.AssociationType.IsMany)
                {
                    this.SetCompositeRolesMany2Many(roleType, newStrategies);
                }
                else
                {
                    this.SetCompositesRoleOne2Many(roleType, newStrategies);
                }
            }
        }

        public void AddCompositeRole(RoleType roleType, IObject objectToAdd)
        {
            this.CheckRemoved();
            if (objectToAdd != null)
            {
                this.session.MemoryDatabase.CompositeRolesChecks(this, roleType, objectToAdd);

                var roleStrategy = this.session.GetStrategy(objectToAdd.Strategy.ObjectId);

                if (roleType.AssociationType.IsMany)
                {
                    this.AddCompositeRoleMany2Many(roleType, roleStrategy);
                }
                else
                {
                    this.AddCompositeRoleOne2Many(roleType, roleStrategy);
                }
            }
        }

        public void RemoveCompositeRole(RoleType roleType, IObject objectToRemove)
        {
            this.CheckRemoved();
            if (objectToRemove != null)
            {
                this.session.MemoryDatabase.CompositeRolesChecks(this, roleType, objectToRemove);

                var roleStrategy = this.session.GetStrategy(objectToRemove.Strategy.ObjectId);

                if (roleType.AssociationType.IsMany)
                {
                    this.RemoveCompositeRoleMany2Many(roleType, roleStrategy);
                }
                else
                {
                    this.RemoveCompositeRoleOne2Many(roleType, roleStrategy);
                }
            }
        }

        public void RemoveCompositeRoles(RoleType roleType)
        {
            this.CheckRemoved();
            if (roleType.AssociationType.IsMany)
            {
                this.RemoveCompositeRolesMany2Many(roleType);
            }
            else
            {
                this.RemoveCompositeRolesOne2Many(roleType);
            }
        }

        public bool ExistCompositeRoles(RoleType roleType)
        {
            this.CheckRemoved();
            HashSet<Strategy> roleStrategies;
            this.compositesRoleByRoleType.TryGetValue(roleType, out roleStrategies);
            return roleStrategies != null;
        }

        public object GetAssociation(AssociationType associationType)
        {
            if (associationType.IsMany)
            {
                return this.GetCompositeAssociations(associationType);
            }

            return this.GetCompositeAssociation(associationType);
        }

        public bool ExistAssociation(AssociationType associationType)
        {
            if (associationType.IsMany)
            {
                return this.ExistCompositeAssociations(associationType);
            }

            return this.ExistCompositeAssociation(associationType);
        }

        public IObject GetCompositeAssociation(AssociationType associationType)
        {
            this.CheckRemoved();
            Strategy strategy;
            this.compositeAssociationByAssociationType.TryGetValue(associationType, out strategy);
            if (strategy != null)
            {
                return strategy.GetObject();
            }

            return null;
        }

        public bool ExistCompositeAssociation(AssociationType associationType)
        {
            return this.GetCompositeAssociation(associationType) != null;
        }

        public Allors.Extent GetCompositeAssociations(AssociationType associationType)
        {
            this.CheckRemoved();

            return new ExtentSwitch(this, associationType);
        }

        public bool ExistCompositeAssociations(AssociationType associationType)
        {
            this.CheckRemoved();
            HashSet<Strategy> strategies;
            this.compositesAssociationByAssociationType.TryGetValue(associationType, out strategies);

            return strategies != null;
        }

        public void Delete()
        {
            this.CheckRemoved();

            // Roles
            foreach (var roleType in this.objectType.RoleTypes)
            {
                if (this.ExistRole(roleType))
                {
                    if (roleType.ObjectType is Unit)
                    {
                        this.RemoveUnitRole(roleType);
                    }
                    else
                    {
                        var associationType = roleType.AssociationType;
                        if (associationType.IsMany)
                        {
                            if (roleType.IsMany)
                            {
                                this.RemoveCompositeRolesMany2Many(roleType);
                            }
                            else
                            {
                                this.RemoveCompositeRoleMany2One(roleType);
                            }
                        }
                        else
                        {
                            if (roleType.IsMany)
                            {
                                this.RemoveCompositeRolesOne2Many(roleType);
                            }
                            else
                            {
                                this.RemoveCompositeRoleOne2One(roleType);
                            }
                        }
                    }
                }
            }

            // Associations
            foreach (var associationType in this.objectType.AssociationTypes)
            {
                var roleType = associationType.RoleType;

                if (this.ExistAssociation(associationType))
                {
                    if (associationType.IsMany)
                    {
                        HashSet<Strategy> associationStrategies;
                        this.compositesAssociationByAssociationType.TryGetValue(associationType, out associationStrategies);

                        // TODO: Optimize
                        if (associationStrategies != null)
                        {
                            foreach (var associationStrategy in new HashSet<Strategy>(associationStrategies))
                            {
                                if (roleType.IsMany)
                                {
                                    associationStrategy.RemoveCompositeRoleMany2Many(roleType, this);
                                }
                                else
                                {
                                    associationStrategy.RemoveCompositeRoleMany2One(roleType);
                                }
                            }
                        }
                    }
                    else
                    {
                        Strategy associationStrategy;
                        this.compositeAssociationByAssociationType.TryGetValue(associationType, out associationStrategy);

                        if (associationStrategy != null)
                        {
                            if (roleType.IsMany)
                            {
                                associationStrategy.RemoveCompositeRoleOne2Many(roleType, this);
                            }
                            else
                            {
                                associationStrategy.RemoveCompositeRoleOne2One(roleType);
                            }
                        }
                    }
                }
            }

            this.isDeleted = true;

            this.ChangeSet.OnDeleted(this);
        }

        public IObject GetObject()
        {
            IObject allorsObject;
            if (this.allorizedObjectWeakReference == null)
            {
                allorsObject = this.session.Database.ObjectFactory.Create(this);
                this.allorizedObjectWeakReference = new WeakReference(allorsObject);
            }
            else
            {
                allorsObject = (IObject)this.allorizedObjectWeakReference.Target;
                if (allorsObject == null)
                {
                    allorsObject = this.session.Database.ObjectFactory.Create(this);
                    this.allorizedObjectWeakReference.Target = allorsObject;
                }
            }

            return allorsObject;
        }

        internal void Commit()
        {
            this.isDeletedOnRollback = this.isDeleted;
            this.isNew = false;

            this.rollbackUnitRoleByRoleType = null;
            this.rollbackCompositeRoleByRoleType = null;
            this.rollbackCompositesRoleByRoleType = null;
            this.rollbackCompositeAssociationByAssociationType = null;
            this.rollbackCompositesAssociationByAssociationType = null;
        }

        internal void Rollback()
        {
            this.isDeleted = this.isDeletedOnRollback;
            this.isNew = false;

            foreach (var dictionaryItem in this.RollbackUnitRoleByRoleType)
            {
                var roleType = dictionaryItem.Key;
                var role = dictionaryItem.Value;

                if (role != null)
                {
                    this.unitRoleByRoleType[roleType] = role;
                }
                else
                {
                    this.unitRoleByRoleType.Remove(roleType);
                }
            }

            foreach (var dictionaryItem in this.RollbackCompositeRoleByRoleType)
            {
                var roleType = dictionaryItem.Key;
                var role = dictionaryItem.Value;

                if (role != null)
                {
                    this.compositeRoleByRoleType[roleType] = role;
                }
                else
                {
                    this.compositeRoleByRoleType.Remove(roleType);
                }
            }

            foreach (var dictionaryItem in this.RollbackCompositesRoleByRoleType)
            {
                var roleType = dictionaryItem.Key;
                var role = dictionaryItem.Value;

                if (role != null)
                {
                    this.compositesRoleByRoleType[roleType] = role;
                }
                else
                {
                    this.compositesRoleByRoleType.Remove(roleType);
                }
            }

            foreach (var dictionaryItem in this.RollbackCompositeAssociationByAssociationType)
            {
                var associationType = dictionaryItem.Key;
                var association = dictionaryItem.Value;

                if (association != null)
                {
                    this.compositeAssociationByAssociationType[associationType] = association;
                }
                else
                {
                    this.compositeAssociationByAssociationType.Remove(associationType);
                }
            }

            foreach (var dictionaryItem in this.RollbackCompositesAssociationByAssociationType)
            {
                var associationType = dictionaryItem.Key;
                var association = dictionaryItem.Value;

                if (association != null)
                {
                    this.compositesAssociationByAssociationType[associationType] = association;
                }
                else
                {
                    this.compositesAssociationByAssociationType.Remove(associationType);
                }
            }

            this.rollbackUnitRoleByRoleType = null;
            this.rollbackCompositeRoleByRoleType = null;
            this.rollbackCompositesRoleByRoleType = null;
            this.rollbackCompositeAssociationByAssociationType = null;
            this.rollbackCompositesAssociationByAssociationType = null;
        }

        internal object GetInternalizedUnitRole(RoleType roleType)
        {
            object unitRole;
            this.unitRoleByRoleType.TryGetValue(roleType, out unitRole);
            return unitRole;
        }

        internal List<Strategy> GetStrategies(AssociationType associationType)
        {
            HashSet<Strategy> strategies;
            this.compositesAssociationByAssociationType.TryGetValue(associationType, out strategies);
            if (strategies == null)
            {
                return new List<Strategy>();
            }

            return strategies.ToList();
        }

        internal List<Strategy> GetStrategies(RoleType roleType)
        {
            HashSet<Strategy> strategies;
            this.compositesRoleByRoleType.TryGetValue(roleType, out strategies);
            if (strategies == null)
            {
                return new List<Strategy>();
            }

            return strategies.ToList();
        }

        internal void SetCompositeRoleOne2One(RoleType roleType, IObject newRole)
        {
            this.CheckRemoved();
            this.session.MemoryDatabase.CompositeRoleChecks(this, roleType, newRole);

            var previousRole = this.GetCompositeRole(roleType);

            this.ChangeSet.OnChangingCompositeRole(this, roleType, previousRole, newRole);

            if (!newRole.Equals(previousRole))
            {
                var newRoleStrategy = this.session.GetStrategy(newRole);

                if (previousRole != null)
                {
                    // previous role
                    var previousRoleStrategy = this.session.GetStrategy(previousRole);
                    var associationType = roleType.AssociationType;
                    previousRoleStrategy.Backup(associationType);
                    previousRoleStrategy.compositeAssociationByAssociationType.Remove(associationType);
                }

                // previous association of newRole
                var newRolePreviousAssociation = newRoleStrategy.GetCompositeAssociation(roleType.AssociationType);
                if (newRolePreviousAssociation != null)
                {
                    var newRolePreviousAssociationStrategy = this.session.GetStrategy(newRolePreviousAssociation);
                    if (!this.Equals(newRolePreviousAssociationStrategy))
                    {
                        newRolePreviousAssociationStrategy.Backup(roleType);
                        newRolePreviousAssociationStrategy.compositeRoleByRoleType.Remove(roleType);
                    }
                }

                // Set new role
                this.Backup(roleType);
                this.compositeRoleByRoleType[roleType] = newRoleStrategy;

                // Set new role's association
                var associationType1 = roleType.AssociationType;
                newRoleStrategy.Backup(associationType1);
                newRoleStrategy.compositeAssociationByAssociationType[associationType1] = this;
            }
        }

        internal void SetCompositeRoleMany2One(RoleType roleType, IObject newRole)
        {
            this.CheckRemoved();
            this.session.MemoryDatabase.CompositeRoleChecks(this, roleType, newRole);

            var previousRole = this.GetCompositeRole(roleType);

            this.ChangeSet.OnChangingCompositeRole(this, roleType, previousRole, newRole);

            if (!newRole.Equals(previousRole))
            {
                var associationType = roleType.AssociationType;

                // Update association of previous role
                if (previousRole != null)
                {
                    var previousRoleStrategy = this.session.GetStrategy(previousRole);
                    HashSet<Strategy> previousRoleStrategies;
                    previousRoleStrategy.compositesAssociationByAssociationType.TryGetValue(associationType, out previousRoleStrategies);

                    previousRoleStrategy.Backup(associationType);
                    previousRoleStrategies.Remove(this);
                    if (previousRoleStrategies.Count == 0)
                    {
                        previousRoleStrategy.compositesAssociationByAssociationType.Remove(associationType);
                    }
                }

                var newRoleStrategy = this.session.GetStrategy(newRole);

                this.Backup(roleType);
                this.compositeRoleByRoleType[roleType] = newRoleStrategy;

                HashSet<Strategy> strategies;
                newRoleStrategy.compositesAssociationByAssociationType.TryGetValue(associationType, out strategies);

                newRoleStrategy.Backup(associationType);
                if (strategies == null)
                {
                    strategies = new HashSet<Strategy>();
                    newRoleStrategy.compositesAssociationByAssociationType[associationType] = strategies;
                }

                strategies.Add(this);
            }
        }

        internal void SetCompositesRoleOne2Many(RoleType roleType, HashSet<Strategy> newStrategies)
        {
            this.RemoveCompositeRolesOne2Many(roleType);

            // TODO: Optimize this
            foreach (var strategy in newStrategies)
            {
                this.AddCompositeRoleOne2Many(roleType, strategy);
            }
        }

        internal void SetCompositeRolesMany2Many(RoleType roleType, HashSet<Strategy> newStrategies)
        {
            this.RemoveCompositeRolesMany2Many(roleType);

            // TODO: Optimize this
            foreach (var strategy in newStrategies)
            {
                this.AddCompositeRoleMany2Many(roleType, strategy);
            }
        }

        internal void FillRoleForSave(Dictionary<RoleType, List<Strategy>> strategiesByRoleType)
        {
            if (this.IsDeleted)
            {
                return;
            }

            if (this.unitRoleByRoleType != null)
            {
                foreach (var dictionaryEntry in this.unitRoleByRoleType)
                {
                    var roleType = dictionaryEntry.Key;

                    List<Strategy> strategies;
                    if (!strategiesByRoleType.TryGetValue(roleType, out strategies))
                    {
                        strategies = new List<Strategy>();
                        strategiesByRoleType.Add(roleType, strategies);
                    }

                    strategies.Add(this);
                }
            }

            if (this.compositeRoleByRoleType != null)
            {
                foreach (var dictionaryEntry in this.compositeRoleByRoleType)
                {
                    var roleType = dictionaryEntry.Key;

                    List<Strategy> strategies;
                    if (!strategiesByRoleType.TryGetValue(roleType, out strategies))
                    {
                        strategies = new List<Strategy>();
                        strategiesByRoleType.Add(roleType, strategies);
                    }

                    strategies.Add(this);
                }
            }

            if (this.compositesRoleByRoleType != null)
            {
                foreach (var dictionaryEntry in this.compositesRoleByRoleType)
                {
                    var roleType = dictionaryEntry.Key;

                    List<Strategy> strategies;
                    if (!strategiesByRoleType.TryGetValue(roleType, out strategies))
                    {
                        strategies = new List<Strategy>();
                        strategiesByRoleType.Add(roleType, strategies);
                    }

                    strategies.Add(this);
                }
            }
        }

        internal void SaveUnit(XmlWriter writer, RoleType roleType)
        {
            var unitType = (Unit)roleType.ObjectType;
            var value = Serialization.WriteString((UnitTags)unitType.UnitTag, this.unitRoleByRoleType[roleType]);

            writer.WriteStartElement(Serialization.Relation);
            writer.WriteAttributeString(Serialization.Association, this.ObjectId.ToString());
            writer.WriteString(value);
            writer.WriteEndElement();
        }

        internal void SaveComposites(XmlWriter writer, RoleType roleType)
        {
            writer.WriteStartElement(Serialization.Relation);
            writer.WriteAttributeString(Serialization.Association, this.ObjectId.ToString());

            var roleStragies = this.compositesRoleByRoleType[roleType];
            var i = 0;
            foreach (var roleStrategy in roleStragies)
            {
                if (i > 0)
                {
                    writer.WriteString(Serialization.ObjectsSplitter);
                }

                writer.WriteString(roleStrategy.ObjectId.ToString());
                ++i;
            }

            writer.WriteEndElement();
        }

        internal void SaveComposite(XmlWriter writer, RoleType roleType)
        {
            writer.WriteStartElement(Serialization.Relation);
            writer.WriteAttributeString(Serialization.Association, this.ObjectId.ToString());

            var roleStragy = this.compositeRoleByRoleType[roleType];
            writer.WriteString(roleStragy.ObjectId.ToString());

            writer.WriteEndElement();
        }

        private void CheckRemoved()
        {
            if (this.isDeleted)
            {
                throw new Exception("Object of class " + this.objectType.Name + " with id " + this.ObjectId +
                                    " has been deleted");
            }
        }

        private void Backup(RoleType roleType)
        {
            if (roleType.IsMany)
            {
                if (!this.RollbackCompositesRoleByRoleType.ContainsKey(roleType))
                {
                    HashSet<Strategy> strategies;
                    this.compositesRoleByRoleType.TryGetValue(roleType, out strategies);

                    if (strategies == null)
                    {
                        this.RollbackCompositesRoleByRoleType[roleType] = null;
                    }
                    else
                    {
                        this.RollbackCompositesRoleByRoleType[roleType] = new HashSet<Strategy>(strategies);
                    }
                }
            }
            else
            {
                if (!this.RollbackCompositeRoleByRoleType.ContainsKey(roleType))
                {
                    Strategy strategy;
                    this.compositeRoleByRoleType.TryGetValue(roleType, out strategy);

                    if (strategy == null)
                    {
                        this.RollbackCompositeRoleByRoleType[roleType] = null;
                    }
                    else
                    {
                        this.RollbackCompositeRoleByRoleType[roleType] = strategy;
                    }
                }
            }
        }

        private void Backup(AssociationType associationType)
        {
            if (associationType.IsMany)
            {
                if (!this.RollbackCompositesAssociationByAssociationType.ContainsKey(associationType))
                {
                    HashSet<Strategy> strategies;
                    this.compositesAssociationByAssociationType.TryGetValue(associationType, out strategies);

                    if (strategies == null)
                    {
                        this.RollbackCompositesAssociationByAssociationType[associationType] = null;
                    }
                    else
                    {
                        this.RollbackCompositesAssociationByAssociationType[associationType] = new HashSet<Strategy>(strategies);
                    }
                }
            }
            else
            {
                if (!this.RollbackCompositeAssociationByAssociationType.ContainsKey(associationType))
                {
                    Strategy strategy;
                    this.compositeAssociationByAssociationType.TryGetValue(associationType, out strategy);

                    if (strategy == null)
                    {
                        this.RollbackCompositeAssociationByAssociationType[associationType] = null;
                    }
                    else
                    {
                        this.RollbackCompositeAssociationByAssociationType[associationType] = strategy;
                    }
                }
            }
        }

        private void RemoveCompositeRoleOne2One(RoleType roleType)
        {
            this.CheckRemoved();
            this.session.MemoryDatabase.CompositeRoleChecks(this, roleType);

            var previousRole = this.GetCompositeRole(roleType);

            if (previousRole != null)
            {
                this.ChangeSet.OnChangingCompositeRole(this, roleType, previousRole, null);
                
                var previousRoleStrategy = this.session.GetStrategy(previousRole);
                var associationType = roleType.AssociationType;
                previousRoleStrategy.Backup(associationType);
                previousRoleStrategy.compositeAssociationByAssociationType.Remove(associationType);
            }

            // remove role
            this.Backup(roleType);  // Test without this line, should give a red light ...
            this.compositeRoleByRoleType.Remove(roleType);
        }

        private void RemoveCompositeRoleMany2One(RoleType roleType)
        {
            this.CheckRemoved();
            this.session.MemoryDatabase.CompositeRoleChecks(this, roleType);

            var previousRole = this.GetCompositeRole(roleType);

            if (previousRole != null)
            {
                this.ChangeSet.OnChangingCompositeRole(this, roleType, previousRole, null);

                var previousRoleStrategy = this.session.GetStrategy(previousRole);
                var associationType = roleType.AssociationType;

                HashSet<Strategy> previousRoleStrategyAssociations;
                previousRoleStrategy.compositesAssociationByAssociationType.TryGetValue(associationType, out previousRoleStrategyAssociations);

                previousRoleStrategy.Backup(associationType);
                previousRoleStrategyAssociations.Remove(this);

                if (previousRoleStrategyAssociations.Count == 0)
                {
                    previousRoleStrategy.compositesAssociationByAssociationType.Remove(associationType);
                }
            }

            // remove role
            this.Backup(roleType);
            this.compositeRoleByRoleType.Remove(roleType);
        }

        private void AddCompositeRoleMany2Many(RoleType roleType, Strategy newRoleStrategy)
        {
            HashSet<Strategy> previousRoleStrategies;
            this.compositesRoleByRoleType.TryGetValue(roleType, out previousRoleStrategies);
            if (previousRoleStrategies != null && previousRoleStrategies.Contains(newRoleStrategy))
            {
                return;
            }

            this.ChangeSet.OnChangingCompositesRole(this, roleType, newRoleStrategy);

            // Add the new role
            this.Backup(roleType);
            HashSet<Strategy> roleStrategies;
            this.compositesRoleByRoleType.TryGetValue(roleType, out roleStrategies);
            if (roleStrategies == null)
            {
                roleStrategies = new HashSet<Strategy>();
                this.compositesRoleByRoleType[roleType] = roleStrategies;
            }

            roleStrategies.Add(newRoleStrategy);

            // Add the new association
            newRoleStrategy.Backup(roleType.AssociationType);
            HashSet<Strategy> newRoleStrategiesAssociationStrategies;
            newRoleStrategy.compositesAssociationByAssociationType.TryGetValue(roleType.AssociationType, out newRoleStrategiesAssociationStrategies);
            if (newRoleStrategiesAssociationStrategies == null)
            {
                newRoleStrategiesAssociationStrategies = new HashSet<Strategy>();
                newRoleStrategy.compositesAssociationByAssociationType[roleType.AssociationType] = newRoleStrategiesAssociationStrategies;
            }

            newRoleStrategiesAssociationStrategies.Add(this);
        }

        private void AddCompositeRoleOne2Many(RoleType roleType, Strategy newRoleStrategy)
        {
            HashSet<Strategy> previousRoleStrategies;
            this.compositesRoleByRoleType.TryGetValue(roleType, out previousRoleStrategies);
            if (previousRoleStrategies != null && previousRoleStrategies.Contains(newRoleStrategy))
            {
                return;
            }

            this.ChangeSet.OnChangingCompositesRole(this, roleType, newRoleStrategy);

            // 1-...
            Strategy newRolePreviousAssociationStrategy;
            newRoleStrategy.compositeAssociationByAssociationType.TryGetValue(roleType.AssociationType, out newRolePreviousAssociationStrategy);
            if (newRolePreviousAssociationStrategy != null)
            {
                // Remove obsolete role
                newRolePreviousAssociationStrategy.Backup(roleType);
                HashSet<Strategy> newRolePreviousAssociationStrategyRoleStrategies;
                newRolePreviousAssociationStrategy.compositesRoleByRoleType.TryGetValue(roleType, out newRolePreviousAssociationStrategyRoleStrategies);
                if (newRolePreviousAssociationStrategyRoleStrategies == null)
                {
                    newRolePreviousAssociationStrategyRoleStrategies = new HashSet<Strategy>();
                    newRolePreviousAssociationStrategy.compositesRoleByRoleType[roleType] = newRolePreviousAssociationStrategyRoleStrategies;
                }

                newRolePreviousAssociationStrategyRoleStrategies.Remove(newRoleStrategy);
                if (newRolePreviousAssociationStrategyRoleStrategies.Count == 0)
                {
                    newRolePreviousAssociationStrategy.compositesRoleByRoleType.Remove(roleType);
                }
            }

            // Add the new role
            this.Backup(roleType);
            HashSet<Strategy> roleStrategies;
            this.compositesRoleByRoleType.TryGetValue(roleType, out roleStrategies);
            if (roleStrategies == null)
            {
                roleStrategies = new HashSet<Strategy>();
                this.compositesRoleByRoleType[roleType] = roleStrategies;
            }

            roleStrategies.Add(newRoleStrategy);

            // Set new association
            newRoleStrategy.Backup(roleType.AssociationType);
            newRoleStrategy.compositeAssociationByAssociationType[roleType.AssociationType] = this;
        }

        private void RemoveCompositeRoleMany2Many(RoleType roleType, Strategy roleStrategy)
        {
            HashSet<Strategy> roleStrategies;
            this.compositesRoleByRoleType.TryGetValue(roleType, out roleStrategies);
            if (roleStrategies == null || !roleStrategies.Contains(roleStrategy))
            {
                return;
            }

            this.ChangeSet.OnChangingCompositesRole(this, roleType, roleStrategy);

            // Remove role
            roleStrategies.Remove(roleStrategy);
            if (roleStrategies.Count == 0)
            {
                this.compositesRoleByRoleType.Remove(roleType);
            }

            // Remove association
            roleStrategy.Backup(roleType.AssociationType);
            HashSet<Strategy> roleStrategiesAssociationStrategies;
            roleStrategy.compositesAssociationByAssociationType.TryGetValue(roleType.AssociationType, out roleStrategiesAssociationStrategies);
            roleStrategiesAssociationStrategies.Remove(this);

            if (roleStrategiesAssociationStrategies.Count == 0)
            {
                roleStrategy.compositesAssociationByAssociationType.Remove(roleType.AssociationType);
            }
        }

        private void RemoveCompositeRoleOne2Many(RoleType roleType, Strategy roleStrategy)
        {
            HashSet<Strategy> roleStrategies;
            this.compositesRoleByRoleType.TryGetValue(roleType, out roleStrategies);
            if (roleStrategies == null || !roleStrategies.Contains(roleStrategy))
            {
                return;
            }

            this.ChangeSet.OnChangingCompositesRole(this, roleType, roleStrategy);

            this.Backup(roleType);

            // Remove role
            roleStrategies.Remove(roleStrategy);
            if (roleStrategies.Count == 0)
            {
                this.compositesRoleByRoleType.Remove(roleType);
            }

            // Remove association
            roleStrategy.Backup(roleType.AssociationType);
            roleStrategy.compositeAssociationByAssociationType.Remove(roleType.AssociationType);
        }

        private void RemoveCompositeRolesMany2Many(RoleType roleType)
        {
            HashSet<Strategy> previousRoleStrategies;
            this.compositesRoleByRoleType.TryGetValue(roleType, out previousRoleStrategies);
            if (previousRoleStrategies != null)
            {
                this.ChangeSet.OnChangingCompositesRole(this, roleType, previousRoleStrategies);

                foreach (var strategy in new List<Strategy>(previousRoleStrategies))
                {
                    this.RemoveCompositeRoleMany2Many(roleType, strategy);
                }
            }
        }

        private void RemoveCompositeRolesOne2Many(RoleType roleType)
        {
            // TODO: Optimize
            HashSet<Strategy> previousRoleStrategies;
            this.compositesRoleByRoleType.TryGetValue(roleType, out previousRoleStrategies);
            if (previousRoleStrategies != null)
            {
                foreach (var strategy in new List<Strategy>(previousRoleStrategies))
                {
                    this.RemoveCompositeRoleOne2Many(roleType, strategy);
                }
            }
        }

        public class ObjectIdComparer : IComparer<Strategy>
        {
            public int Compare(Strategy x, Strategy y)
            {
                return x.ObjectId.CompareTo(y.ObjectId);
            }
        }
    }
}