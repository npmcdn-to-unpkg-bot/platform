// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompositePredicate.cs" company="Allors bvba">
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

namespace Allors.Adapters.Database.Sql
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Meta;

    public abstract class CompositePredicate : Predicate, ICompositePredicate
    {
        private readonly ExtentFiltered extent;
        private readonly List<Predicate> filters;

        protected CompositePredicate(ExtentFiltered extent)
        {
            this.extent = extent;
            this.filters = new List<Predicate>(4);

            if (extent.Strategy != null)
            {
                var allorsObject = extent.Strategy.GetObject();
                if (extent.AssociationType != null)
                {
                    var role = extent.AssociationType.RoleType;
                    if (role.IsMany)
                    {
                        this.AddContains(role, allorsObject);
                    }
                    else
                    {
                        this.AddEquals(role, allorsObject);
                    }
                }
                else
                {
                    var association = extent.RoleType.AssociationType;
                    if (association.IsMany)
                    {
                        this.AddContains(association, allorsObject);
                    }
                    else
                    {
                        this.AddEquals(association, allorsObject);
                    }
                }
            }
        }

        public override bool Include
        {
            get
            {
                foreach (var filter in this.Filters)
                {
                    if (filter.Include)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        protected ExtentFiltered Extent
        {
            get
            {
                return this.extent;
            }
        }

        protected List<Predicate> Filters
        {
            get
            {
                return this.filters;
            }
        }

        public static Class[] GetConcreteSubClasses(Composite type)
        {
            var @interface = type as Interface;
            if (@interface != null)
            {
                return @interface.Subclasses.ToArray();
            }

            return new[] { (Class)type };
        }

        public ICompositePredicate AddAnd()
        {
            this.Extent.FlushCache();
            var allFilter = new AndPredicate(this.Extent);
            this.Filters.Add(allFilter);
            return allFilter;
        }

        public ICompositePredicate AddBetween(RoleType role, object firstValue, object secondValue)
        {
            this.Extent.FlushCache();
            var betweenRoleA = firstValue as RoleType;
            var betweenRoleB = secondValue as RoleType;
            var betweenAssociationA = firstValue as AssociationType;
            var betweenAssociationB = secondValue as AssociationType;
            if (betweenRoleA != null && betweenRoleB != null)
            {
                this.Filters.Add(new RoleBetweenRole(this.Extent, role, betweenRoleA, betweenRoleB));
            }
            else if (betweenAssociationA != null && betweenAssociationB != null)
            {
                throw new NotImplementedException();
            }
            else
            {
                this.Filters.Add(new RoleBetweenValue(this.Extent, role, firstValue, secondValue));
            }

            return this;
        }

        public ICompositePredicate AddContainedIn(RoleType role, Allors.Extent containingExtent)
        {
            this.Extent.FlushCache();
            this.Filters.Add(new RoleContainedInExtent(this.Extent, role, containingExtent));
            return this;
        }

        public ICompositePredicate AddContainedIn(RoleType role, IEnumerable<IObject> containingEnumerable)
        {
            this.Extent.FlushCache();
            this.Filters.Add(new RoleContainedInEnumerable(this.Extent, role, containingEnumerable));
            return this;
        }

        public ICompositePredicate AddContainedIn(AssociationType association, Allors.Extent containingExtent)
        {
            this.Extent.FlushCache();
            this.Filters.Add(new AssociationContainedInExtent(this.Extent, association, containingExtent));
            return this;
        }

        public ICompositePredicate AddContainedIn(AssociationType association, IEnumerable<IObject> containingEnumerable)
        {
            this.Extent.FlushCache();
            this.Filters.Add(new AssociationContainedInEnumerable(this.Extent, association, containingEnumerable));
            return this;
        }

        public ICompositePredicate AddContains(RoleType role, IObject containedObject)
        {
            this.Extent.FlushCache();
            this.Filters.Add(new RoleContains(this.Extent, role, containedObject));
            return this;
        }

        public ICompositePredicate AddContains(AssociationType association, IObject containedObject)
        {
            this.Extent.FlushCache();
            this.Filters.Add(new AssociationContains(this.Extent, association, containedObject));
            return this;
        }

        public ICompositePredicate AddEquals(IObject allorsObject)
        {
            this.Extent.FlushCache();
            this.Filters.Add(new Equals(allorsObject));
            return this;
        }

        public ICompositePredicate AddEquals(RoleType role, object obj)
        {
            this.Extent.FlushCache();
            var equalsRole = obj as RoleType;
            var equalsAssociation = obj as AssociationType;
            if (equalsRole != null)
            {
                this.Filters.Add(new RoleEqualsRole(this.Extent, role, equalsRole));
            }
            else if (equalsAssociation != null)
            {
                throw new NotImplementedException();
            }
            else
            {
                this.Filters.Add(new RoleEqualsValue(this.Extent, role, obj));
            }

            return this;
        }

        public ICompositePredicate AddEquals(AssociationType association, IObject allorsObject)
        {
            this.Extent.FlushCache();
            this.Filters.Add(new AssociationEquals(this.Extent, association, allorsObject));
            return this;
        }

        public ICompositePredicate AddExists(RoleType role)
        {
            this.Extent.FlushCache();
            this.Filters.Add(new RoleExists(this.Extent, role));
            return this;
        }

        public ICompositePredicate AddExists(AssociationType association)
        {
            this.Extent.FlushCache();
            this.Filters.Add(new AssociationExists(this.Extent, association));
            return this;
        }

        public ICompositePredicate AddGreaterThan(RoleType role, object value)
        {
            this.Extent.FlushCache();
            var greaterThanRole = value as RoleType;
            var greaterThanAssociation = value as AssociationType;
            if (greaterThanRole != null)
            {
                this.Filters.Add(new RoleGreaterThanRole(this.Extent, role, greaterThanRole));
            }
            else if (greaterThanAssociation != null)
            {
                throw new NotImplementedException();
            }
            else
            {
                this.Filters.Add(new RoleGreaterThanValue(this.Extent, role, value));
            }

            return this;
        }

        public ICompositePredicate AddInstanceof(Composite type)
        {
            this.Extent.FlushCache();
            this.Filters.Add(new InstanceOf(type, GetConcreteSubClasses(type)));
            return this;
        }

        public ICompositePredicate AddInstanceof(RoleType role, Composite type)
        {
            this.Extent.FlushCache();
            this.Filters.Add(new RoleInstanceof(this.Extent, role, type, GetConcreteSubClasses(type)));
            return this;
        }

        public ICompositePredicate AddInstanceof(AssociationType association, Composite type)
        {
            this.Extent.FlushCache();
            this.Filters.Add(new AssociationInstanceOf(this.Extent, association, type, GetConcreteSubClasses(type)));
            return this;
        }

        public ICompositePredicate AddLessThan(RoleType role, object value)
        {
            this.Extent.FlushCache();
            var lessThanRole = value as RoleType;
            var lessThanAssociation = value as AssociationType;
            if (lessThanRole != null)
            {
                this.Filters.Add(new RoleLessThanRole(this.Extent, role, lessThanRole));
            }
            else if (lessThanAssociation != null)
            {
                throw new NotImplementedException();
            }
            else
            {
                this.Filters.Add(new RoleLessThanValue(this.Extent, role, value));
            }

            return this;
        }

        public ICompositePredicate AddLike(RoleType role, string value)
        {
            this.Extent.FlushCache();
            this.Filters.Add(new RoleLike(this.Extent, role, value));
            return this;
        }

        public ICompositePredicate AddNot()
        {
            this.Extent.FlushCache();
            var noneFilter = new Not(this.Extent);
            this.Filters.Add(noneFilter);
            return noneFilter;
        }

        public ICompositePredicate AddOr()
        {
            this.Extent.FlushCache();
            var anyFilter = new Or(this.Extent);
            this.Filters.Add(anyFilter);
            return anyFilter;
        }

        public override void Setup(ExtentStatement statement)
        {
            foreach (var filter in this.Filters)
            {
                filter.Setup(statement);
            }
        }
    }
}