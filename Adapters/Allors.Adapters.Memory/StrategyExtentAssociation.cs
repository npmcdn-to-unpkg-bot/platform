// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StrategyExtentAssociation.cs" company="Allors bvba">
//   Copyright 2002-2016 Allors bvba.
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

using Allors;

namespace Allors.Adapters.Memory
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Allors.Meta;

    public sealed class StrategyExtentAssociation : StrategyExtent
    {
        private static readonly List<Strategy> EmptyList = new List<Strategy>();
        private readonly Strategy roleStrategy;
        private readonly IAssociationType associationType;

        private IObject[] defaultObjectArray;

        private List<Strategy> associations;

        public StrategyExtentAssociation(Strategy roleStrategy, IAssociationType associationType)
        {
            this.roleStrategy = roleStrategy;
            this.associationType = associationType;
        }

        public override int Count
        {
            get { return this.GetStrategies().Count; }
        }

        public override ICompositePredicate Filter
        {
            get { throw new NotSupportedException(); }
        }

        public override IObject First
        {
            get
            {
                return this.GetStrategies().Select(strategy => strategy.GetObject()).FirstOrDefault();
            }
        }

        public override IComposite ObjectType
        {
            get { return this.associationType.ObjectType; }
        }
        
        internal override Session Session
        {
            get { return this.roleStrategy.MemorySession; }
        }

        public override Allors.Extent AddSort(IRoleType roleType)
        {
            throw new NotSupportedException();
        }

        public override Allors.Extent AddSort(IRoleType roleType, SortDirection direction)
        {
            throw new NotSupportedException();
        }

        public override bool Contains(object value)
        {
            var strategies = this.GetStrategies();
            var valueStrategy = this.roleStrategy.MemorySession.InstantiateMemoryStrategy(((IObject)value).Id);
            return strategies.Contains(valueStrategy);
        }

        public override void CopyTo(Array array, int index)
        {
            this.FillObjects();
            for (var i = index; i < this.associations.Count; i++)
            {
                array.SetValue(this.associations[i].GetObject(), i);
            }
        }

        public override IEnumerator GetEnumerator()
        {
            this.FillObjects();
            return this.associations.Select(strategy => strategy.GetObject()).GetEnumerator();
        }

        public override int IndexOf(object value)
        {
            this.FillObjects();
            var strategy = this.Session.InstantiateMemoryStrategy(((IObject)value).Id);
            return this.associations.IndexOf(strategy);
        }

        public override IObject[] ToArray()
        {
            this.FillObjects();
            var clrType = this.Session.GetTypeForObjectType(this.ObjectType);

            if (this.associations.Count > 0)
            {
                var objects = (IObject[])Array.CreateInstance(clrType, this.associations.Count);
                this.CopyTo(objects, 0);
                return objects;
            }

            return this.defaultObjectArray ?? (this.defaultObjectArray = (IObject[])Array.CreateInstance(clrType, 0));
        }

        public override IObject[] ToArray(Type type)
        {
            this.FillObjects();
            if (this.associations.Count > 0)
            {
                var objects = (IObject[])Array.CreateInstance(type, this.associations.Count);
                for (var i = 0; i < this.associations.Count; i++)
                {
                    objects[i] = this.associations[i].GetObject();
                }

                return objects;
            }

            return (IObject[])Array.CreateInstance(type, 0);
        }

        internal override void UpgradeTo(ExtentFiltered extent)
        {
            if (this.associationType.RoleType.IsMany)
            {
                extent.Filter.AddContains(this.associationType.RoleType, this.roleStrategy.GetObject());
            }
            else
            {
                extent.Filter.AddEquals(this.associationType.RoleType, this.roleStrategy.GetObject());
            }
        }

        protected override IObject GetItem(int index)
        {
            this.FillObjects();
            return this.associations[index].GetObject();
        }

        private void FillObjects()
        {
            if (this.associations == null)
            {
                var strategies = this.GetStrategies();

                if (strategies != null)
                {
                    this.associations = new List<Strategy>();
                    foreach (var strategy in strategies)
                    {
                        this.associations.Add(strategy);
                    }
                }
                else
                {
                    this.associations = EmptyList;
                }
            }
        }

        private List<Strategy> GetStrategies()
        {
            return this.roleStrategy.GetStrategies(this.associationType);
        }
    }
}