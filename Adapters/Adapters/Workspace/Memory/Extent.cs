// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extent.cs" company="Allors bvba">
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

    using Allors.Meta;

    public abstract class Extent : Allors.Extent
    {
        private static readonly List<Strategy> EmptyList = new List<Strategy>();
        private IObject[] defaultObjectArray;

        private And filter;
        private ExtentSort sorter;
        private List<Strategy> strategies;

        public override ICompositePredicate Filter
        {
            get { return this.filter ?? (this.filter = new And(this)); }
        }

        public override int Count
        {
            get
            {
                return this.Strategies.Count;
            }
        }

        public override IObject First
        {
            get
            {
                if (this.Count > 0)
                {
                    return this.Strategies[0].GetObject();
                }

                return null;
            }
        }

        internal abstract WorkspaceSession Session { get; }

        protected virtual List<Strategy> Strategies
        {
            get
            {
                if (this.strategies == null)
                {
                    var relationStrategies = this.GetExtentStrategies();

                    if (relationStrategies != null)
                    {
                        this.strategies = new List<Strategy>();
                        foreach (var strategy in relationStrategies)
                        {
                            if (this.filter == null || !this.filter.Include || this.filter.Evaluate(strategy) == ThreeValuedLogic.True)
                            {
                                this.strategies.Add(strategy);
                            }
                        }

                        if (this.sorter != null)
                        {
                            this.strategies.Sort(this.sorter);
                        }
                    }
                    else
                    {
                        this.strategies = EmptyList;
                    }
                }

                return this.strategies;
            }
        }

        public override bool Contains(object value)
        {
            var valueStrategy = this.Session.GetStrategy(((IObject)value).Id);
            return this.Strategies.Contains(valueStrategy);
        }

        public override void CopyTo(Array array, int index)
        {
            for (var i = index; i < this.Strategies.Count; i++)
            {
                array.SetValue(this.Strategies[i].GetObject(), i);
            }
        }

        public override IEnumerator GetEnumerator()
        {
            return this.Strategies.Select(strategy => strategy.GetObject()).GetEnumerator();
        }

        public override int IndexOf(object value)
        {
            var strategy = this.Session.GetStrategy(((IObject)value).Id);
            return this.Strategies.IndexOf(strategy);
        }

        public override IObject[] ToArray()
        {
            var clrType = this.Session.MemoryWorkspace.Database.ObjectFactory.GetTypeForObjectType(this.ObjectType);

            if (this.Strategies.Count > 0)
            {
                var objects = (IObject[])Array.CreateInstance(clrType, this.Strategies.Count);
                this.CopyTo(objects, 0);
                return objects;
            }

            return this.defaultObjectArray ?? (this.defaultObjectArray = (IObject[])Array.CreateInstance(clrType, 0));
        }

        public override IObject[] ToArray(Type type)
        {
            if (this.Strategies.Count > 0)
            {
                var objects = (IObject[])Array.CreateInstance(type, this.Strategies.Count);
                for (var i = 0; i < this.Strategies.Count; i++)
                {
                    objects[i] = this.Strategies[i].GetObject();
                }

                return objects;
            }

            return (IObject[])Array.CreateInstance(type, 0);
        }

        public override Allors.Extent AddSort(IRoleType roleType)
        {
            return this.AddSort(roleType, SortDirection.Ascending);
        }

        public override Allors.Extent AddSort(IRoleType roleType, SortDirection direction)
        {
            if (this.sorter == null)
            {
                this.sorter = new ExtentSort(roleType, direction);
            }
            else
            {
                this.sorter.AddSort(roleType, direction);
            }

            this.Invalidate();
            return this;
        }

        internal virtual void Invalidate()
        {
            this.strategies = null;
        }

        internal virtual void CheckForAssociationType(IAssociationType association)
        {
            // TODO: Optimize
            if (!this.ObjectType.AssociationTypes.Contains(association))
            {
                throw new ArgumentException("Extent does not have association " + association);
            }
        }

        internal virtual void CheckForRoleType(IRoleType role)
        {
            // TODO: Optimize
            if (!this.ObjectType.RoleTypes.Contains(role))
            {
                throw new ArgumentException("Extent does not have role " + role.SingularName);
            }
        }

        protected override IObject GetItem(int index)
        {
            return this.Strategies[index].GetObject();
        }

        protected abstract HashSet<Strategy> GetExtentStrategies();
    }
}