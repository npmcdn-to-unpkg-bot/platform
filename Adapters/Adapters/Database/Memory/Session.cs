// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Session.cs" company="Allors bvba">
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
    using System.Xml;

    using Allors.Meta;

    public abstract class Session : IDatabaseSession
    {
        private static readonly HashSet<Strategy> EmptyStrategies = new HashSet<Strategy>();

        private readonly Dictionary<IObjectType, IObjectType[]> concreteClassesByObjectType;

        private readonly Database database;

        private ChangeSet changeSet;

        private bool busyCommittingOrRollingBack;

        private Dictionary<ObjectId, Strategy> strategyByObjectId;
        private Dictionary<IObjectType, HashSet<Strategy>> strategiesByObjectType;

        private Dictionary<string, object> properties;

        protected Session(Database database)
        {
            this.database = database;
            this.busyCommittingOrRollingBack = false;

            this.concreteClassesByObjectType = new Dictionary<IObjectType, IObjectType[]>();

            this.changeSet = new ChangeSet();
        }

        public event SessionCommittedEventHandler Committed;

        public event SessionCommittingEventHandler Committing;

        public event SessionRolledBackEventHandler RolledBack;

        public event SessionRollingBackEventHandler RollingBack;

        public abstract IWorkspaceSession WorkspaceSession { get; }

        public IDatabaseSession DatabaseSession
        {
            get
            {
                return this;
            }
        }

        public IPopulation Population
        {
            get { return this.database; }
        }

        public IDatabase Database
        {
            get { return this.database; }
        }

        public bool IsProfilingEnabled
        {
            get
            {
                return false;
            }
        }

        internal ChangeSet MemoryChangeSet
        {
            get
            {
                return this.changeSet;
            }
        }

        internal Database MemoryDatabase
        {
            get { return this.database; }
        }

        internal abstract ObjectIds ObjectIds { get; }

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

        public void Commit()
        {
            if (!this.busyCommittingOrRollingBack)
            {
                try
                {
                    this.busyCommittingOrRollingBack = true;
                    if (this.Committing != null)
                    {
                        // Errors thrown in Committing event handlers 
                        // should have no effect on the current state of the DatabaseSession.
                        this.Committing(this, new SessionCommittingEventArgs(this));
                    }

                    IList<Strategy> strategiesToDelete = null;
                    foreach (var dictionaryEntry in this.strategyByObjectId)
                    {
                        var strategy = dictionaryEntry.Value;

                        strategy.Commit();

                        if (strategy.IsDeleted)
                        {
                            if (strategiesToDelete == null)
                            {
                                strategiesToDelete = new List<Strategy>();
                            }

                            strategiesToDelete.Add(strategy);
                        }
                    }

                    if (strategiesToDelete != null)
                    {
                        foreach (var strategy in strategiesToDelete)
                        {
                            this.strategyByObjectId.Remove(strategy.ObjectId);

                            HashSet<Strategy> strategies;
                            if (this.strategiesByObjectType.TryGetValue(strategy.ObjectType, out strategies))
                            {
                                strategies.Remove(strategy);
                            }
                        }
                    }

                    this.changeSet = new ChangeSet();

                    if (this.Committed != null)
                    {
                        this.Committed(this, new SessionCommittedEventArgs(this));
                    }
                }
                finally
                {
                    this.busyCommittingOrRollingBack = false;
                }
            }
        }

        public void Rollback()
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
                        if (strategy.IsDeleted)
                        {
                            this.strategyByObjectId.Remove(strategy.ObjectId);

                            HashSet<Strategy> strategies;
                            if (this.strategiesByObjectType.TryGetValue(strategy.ObjectType, out strategies))
                            {
                                strategies.Remove(strategy);
                            }
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
                    this.busyCommittingOrRollingBack = false;
                }
            }
        }

        public void Dispose()
        {
            this.Rollback();
        }

        public T Create<T>() where T : IObject
        {
            var objectType = this.database.ObjectFactory.GetObjectTypeForType(typeof(T));

            var @class = objectType as IClass;
            if (@class == null)
            {
                throw new Exception("IObjectType should be a class");
            }
            
            return (T)this.Create(@class);
        }

        public IObject[] Create(IClass objectType, int count)
        {
            var arrayType = this.database.ObjectFactory.GetTypeForObjectType(objectType);
            var allorsObjects = (IObject[])Array.CreateInstance(arrayType, count);
            for (var i = 0; i < count; i++)
            {
                allorsObjects[i] = this.Create(objectType);
            }

            return allorsObjects;
        }

        public IObject Insert(IClass objectType, string objectId)
        {
            var id = this.ObjectIds.Parse(objectId);
            return this.Insert(objectType, id);
        }

        public IObject Insert(IClass objectType, ObjectId objectId)
        {
            var strategy = this.InsertStrategy(objectType, objectId);
            return strategy.GetObject();
        }

        public IObject Instantiate(string objectIdString)
        {
            var id = this.ObjectIds.Parse(objectIdString);
            return this.Instantiate(id);
        }

        public IObject Instantiate(IObject obj)
        {
            return this.Instantiate(obj.Strategy.ObjectId);
        }

        public IObject Instantiate(ObjectId objectId)
        {
            var strategy = this.InstantiateMemoryStrategy(objectId);
            if (strategy != null)
            {
                return strategy.GetObject();
            }

            return null;
        }

        public IStrategy InstantiateStrategy(ObjectId objectId)
        {
            return this.InstantiateMemoryStrategy(objectId);
        }

        public IObject[] Instantiate(string[] objectIdStrings)
        {
            var objectIds = new ObjectId[objectIdStrings.Length];
            for (var i = 0; i < objectIdStrings.Length; i++)
            {
                objectIds[i] = this.ObjectIds.Parse(objectIdStrings[i]);
            }

            return this.Instantiate(objectIds);
        }

        public IObject[] Instantiate(IObject[] objects)
        {
            var objectIds = new ObjectId[objects.Length];
            for (var i = 0; i < objects.Length; i++)
            {
                objectIds[i] = objects[i].Id;
            }

            return this.Instantiate(objectIds);
        }

        public IObject[] Instantiate(ObjectId[] objectIds)
        {
            var allorsObjects = new ArrayList(objectIds.Length);

            foreach (var objectId in objectIds)
            {
                var strategy = this.InstantiateMemoryStrategy(objectId);
                if (strategy != null)
                {
                    allorsObjects.Add(strategy.GetObject());
                }
            }

            return (IObject[])allorsObjects.ToArray(typeof(IObject));
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

        public Extent<T> Extent<T>() where T : IObject
        {
            var compositeType = this.database.ObjectFactory.GetObjectTypeForType(typeof(T)) as IComposite;

            if (compositeType == null)
            {
                throw new Exception("type should be a CompositeType");
            }

            return this.Extent(compositeType);
        }

        public virtual Allors.Extent Extent(IComposite objectType)
        {
            return new ExtentFiltered(this, objectType);
        }

        public virtual Allors.Extent Union(Allors.Extent firstOperand, Allors.Extent secondOperand)
        {
            var firstExtent = firstOperand as Extent ?? ((ExtentSwitch)firstOperand).Extent;
            var secondExtent = secondOperand as Extent ?? ((ExtentSwitch)secondOperand).Extent;

            return new ExtentOperation(this, firstExtent, secondExtent, ExtentOperationType.Union);
        }

        public virtual Allors.Extent Intersect(Allors.Extent firstOperand, Allors.Extent secondOperand)
        {
            return new ExtentOperation(
                this,
                (Extent)firstOperand,
                (Extent)secondOperand,
                ExtentOperationType.Intersect);
        }

        public virtual Allors.Extent Except(Allors.Extent firstOperand, Allors.Extent secondOperand)
        {
            return new ExtentOperation(
                this,
                (Extent)firstOperand,
                (Extent)secondOperand,
                ExtentOperationType.Except);
        }

        public virtual IObject Create(IClass objectType)
        {
            var strategy = new Strategy(this, objectType, this.ObjectIds.Next());
            this.AddStrategy(strategy);

            this.changeSet.OnCreated(strategy);

            return strategy.GetObject();
        }

        internal void Init()
        {
            this.Reset();
        }

        internal Type GetTypeForObjectType(IObjectType objectType)
        {
            return this.database.ObjectFactory.GetTypeForObjectType(objectType);
        }

        internal virtual Strategy InsertStrategy(IClass objectType, ObjectId objectId)
        {
            var strategy = this.GetStrategy(objectId);
            if (strategy != null)
            {
                throw new Exception("Duplicate id error");
            }

            this.ObjectIds.AdjustCurrentId(objectId);
            strategy = new Strategy(this, objectType, objectId);
            this.AddStrategy(strategy);

            this.changeSet.OnCreated(strategy);

            return strategy;
        }

        internal virtual void Reset()
        {
            // Strategies
            this.strategyByObjectId = new Dictionary<ObjectId, Strategy>();
            this.strategiesByObjectType = new Dictionary<IObjectType, HashSet<Strategy>>();

            this.properties = null;
        }

        internal virtual Strategy InstantiateMemoryStrategy(ObjectId objectId)
        {
            return this.GetStrategy(objectId);
        }

        internal Strategy GetStrategy(IObject obj)
        {
            if (obj == null)
            {
                return null;
            }

            return this.GetStrategy(obj.Id);
        }

        internal Strategy GetStrategy(ObjectId objectId)
        {
            Strategy strategy;
            if (!this.strategyByObjectId.TryGetValue(objectId, out strategy))
            {
                return null;
            }

            return strategy.IsDeleted ? null : strategy;
        }

        internal void AddStrategy(Strategy strategy)
        {
            this.strategyByObjectId.Add(strategy.ObjectId, strategy);

            HashSet<Strategy> strategies;
            if (!this.strategiesByObjectType.TryGetValue(strategy.ObjectType, out strategies))
            {
                strategies = new HashSet<Strategy>();
                this.strategiesByObjectType.Add(strategy.ObjectType, strategies);
            }

            strategies.Add(strategy);
        }

        internal virtual HashSet<Strategy> GetStrategiesForExtentIncludingDeleted(IObjectType type)
        {
            IObjectType[] concreteClasses;
            if (!this.concreteClassesByObjectType.TryGetValue(type, out concreteClasses))
            {
                var sortedClassAndSubclassList = new List<IObjectType>();

                if (type is IClass)
                {
                    sortedClassAndSubclassList.Add(type);
                }

                if (type is IInterface)
                {
                    foreach (var subClass in ((IInterface)type).Subclasses)
                    {
                        sortedClassAndSubclassList.Add(subClass);
                    }
                }

                concreteClasses = sortedClassAndSubclassList.ToArray();

                this.concreteClassesByObjectType[type] = concreteClasses;
            }

            switch (concreteClasses.Length)
            {
                case 0:
                    return EmptyStrategies;

                case 1:
                    {
                        var objectType = concreteClasses[0];
                        HashSet<Strategy> strategies;
                        if (this.strategiesByObjectType.TryGetValue(objectType, out strategies))
                        {
                            return strategies;
                        }

                        return EmptyStrategies;
                    }

                default:
                    {
                        var strategies = new HashSet<Strategy>();

                        foreach (var objectType in concreteClasses)
                        {
                            HashSet<Strategy> objectTypeStrategies;
                            if (this.strategiesByObjectType.TryGetValue(objectType, out objectTypeStrategies))
                            {
                                strategies.UnionWith(objectTypeStrategies);
                            }
                        }

                        return strategies;
                    }
            }
        }

        internal void Save(XmlWriter writer)
        {
            var sortedNonDeletedStrategiesByObjectType = new Dictionary<IObjectType, List<Strategy>>();
            foreach (var dictionaryEntry in this.strategyByObjectId)
            {
                var strategy = dictionaryEntry.Value;
                if (!strategy.IsDeleted)
                {
                    var objectType = strategy.ObjectType;

                    List<Strategy> sortedNonDeletedStrategies;
                    if (!sortedNonDeletedStrategiesByObjectType.TryGetValue(objectType, out sortedNonDeletedStrategies))
                    {
                        sortedNonDeletedStrategies = new List<Strategy>();
                        sortedNonDeletedStrategiesByObjectType[objectType] = sortedNonDeletedStrategies;
                    }

                    sortedNonDeletedStrategies.Add(strategy);
                }
            }

            foreach (var dictionaryEntry in sortedNonDeletedStrategiesByObjectType)
            {
                var sortedNonDeletedStrategies = dictionaryEntry.Value;
                sortedNonDeletedStrategies.Sort(new Strategy.ObjectIdComparer());
            }

            var save = new Save(this, writer, sortedNonDeletedStrategiesByObjectType);
            save.Execute();
        }
    }
}