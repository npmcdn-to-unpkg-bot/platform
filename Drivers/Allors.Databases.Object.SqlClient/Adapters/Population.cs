// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Population.cs" company="Allors bvba">
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

namespace Allors.R1.Adapters
{
    using System;
    using System.Collections.Generic;
    using System.Xml;

    using Meta;

    public abstract class Population : IPopulation
    {
        private readonly IObjectFactory objectFactory;

        private readonly Dictionary<ObjectType, object> concreteClassesByObjectType;

        protected Dictionary<string, object> Properties;

        protected Population(Configuration configuration)
        {
            this.objectFactory = configuration.ObjectFactory;
            this.concreteClassesByObjectType = new Dictionary<ObjectType, object>();
        }
        
        public abstract event SessionCreatedEventHandler SessionCreated;

        public IObjectFactory ObjectFactory
        {
            get
            {
                return this.objectFactory;
            }
        }

        public abstract Guid Id { get; }

        public abstract bool IsDatabase { get; }

        public abstract bool IsWorkspace { get; }

        public Domain Domain 
        {
            get
            {
                return this.objectFactory.Domain;
            }
        }

        public object this[string name]
        {
            get
            {
                if (this.Properties == null)
                {
                    return null;
                }

                object value;
                this.Properties.TryGetValue(name, out value);
                return value;
            }

            set
            {
                if (this.Properties == null)
                {
                    this.Properties = new Dictionary<string, object>();
                }

                if (value == null)
                {
                    this.Properties.Remove(name);
                }
                else
                {
                    this.Properties[name] = value;
                }
            }
        }
        
        public abstract ISession CreateSession();
       
        public abstract void Load(XmlReader reader);

        public abstract void Save(XmlWriter writer);

        /// <summary>
        /// Assert that the unit is compatible with the ObjectType of the RoleType.
        /// </summary>
        /// <param name="unit">
        /// The unit .
        /// </param>
        /// <param name="roleType">
        /// The role type.
        /// </param>
        /// <returns>
        /// The normalize.
        /// </returns>
        public object Internalize(object unit, RoleType roleType)
        {
            var objectType = roleType.ObjectType;
            var unitTypeTag = (UnitTypeTags)objectType.UnitTag;

            var normalizedUnit = unit;

            switch (unitTypeTag)
            {
                case UnitTypeTags.AllorsString:
                    if (!(unit is string))
                    {
                        throw new ArgumentException("RoleType is not a String.");
                    }

                    var stringUnit = (string)unit;
                    var size = roleType.Size;
                    if (size > -1 && stringUnit.Length > size)
                    {
                        throw new ArgumentException("Size of relationType " + roleType + " is too big (" + stringUnit.Length + ">" + size + ").");
                    }

                    break;
                case UnitTypeTags.AllorsInteger:
                    if (!(unit is int))
                    {
                        throw new ArgumentException("RoleType is not an Integer.");
                    }

                    break;
                case UnitTypeTags.AllorsLong:
                    if (unit is int)
                    {
                        normalizedUnit = Convert.ToInt64(unit);
                    }
                    else if (!(unit is long))
                    {
                        throw new ArgumentException("RoleType is not a Long.");
                    }

                    break;
                case UnitTypeTags.AllorsDecimal:
                    if (unit is int || unit is long || unit is float || unit is double)
                    {
                        normalizedUnit = Convert.ToDecimal(unit);
                    }
                    else if (!(unit is decimal))
                    {
                        throw new ArgumentException("RoleType is not a Decimal.");
                    }

                    break;
                case UnitTypeTags.AllorsDouble:
                    if (unit is int || unit is long || unit is float)
                    {
                        normalizedUnit = Convert.ToDouble(unit);
                    }
                    else if (!(unit is double))
                    {
                        throw new ArgumentException("RoleType is not a Double.");
                    }

                    break;
                case UnitTypeTags.AllorsBoolean:
                    if (!(unit is bool))
                    {
                        throw new ArgumentException("RoleType is not a Boolean.");
                    }

                    break;
                case UnitTypeTags.AllorsDateTime:
                    if (!(unit is DateTime))
                    {
                        throw new ArgumentException("RoleType is not a DateTime.");
                    }

                    var dateTime = (DateTime)unit;

                    if (dateTime == DateTime.MaxValue || dateTime == DateTime.MinValue)
                    {
                        normalizedUnit = unit;
                    }
                    else
                    {
                        normalizedUnit = dateTime.ToUniversalTime();
                    }

                    break;
                case UnitTypeTags.AllorsUnique:
                    if (!(unit is Guid))
                    {
                        throw new ArgumentException("RoleType is not a Boolean.");
                    }

                    break;
                case UnitTypeTags.AllorsBinary:
                    if (!(unit is byte[]))
                    {
                        throw new ArgumentException("RoleType is not a Boolean.");
                    }

                    break;
                default:
                    throw new ArgumentException("Unknown Unit ObjectType: " + unitTypeTag);
            }

            return normalizedUnit;
        }

        public bool ContainsConcreteClass(ObjectType objectType, ObjectType concreteClass)
        {
            object concreteClassOrClasses;
            if (!this.concreteClassesByObjectType.TryGetValue(objectType, out concreteClassOrClasses))
            {
                if (objectType.ConcreteClasses.Length == 1)
                {
                    concreteClassOrClasses = objectType.ConcreteClasses[0];
                    this.concreteClassesByObjectType[objectType] = concreteClassOrClasses;
                }
                else
                {
                    concreteClassOrClasses = new HashSet<ObjectType>(objectType.ConcreteClasses);
                    this.concreteClassesByObjectType[objectType] = concreteClassOrClasses;
                }
            }

            if (concreteClassOrClasses is ObjectType)
            {
                return concreteClass.Equals(concreteClassOrClasses);
            }

            var concreteClasses = (HashSet<ObjectType>)concreteClassOrClasses;
            return concreteClasses.Contains(concreteClass);
        }

        public void UnitRoleChecks(IStrategy strategy, RoleType relationType)
        {
            if (!this.ContainsConcreteClass(relationType.AssociationType.ObjectType, strategy.ObjectType))
            {
                throw new ArgumentException(strategy.ObjectType + " is not a valid association object type for " + relationType + ".");
            }

            if (!relationType.ObjectType.IsUnit)
            {
                throw new ArgumentException(relationType.ObjectType + " on relationType " + relationType + " is not a unit type.");
            }
        }

        public void CompositeRoleChecks(IStrategy strategy, RoleType roleType)
        {
            this.CompositeSharedChecks(strategy, roleType, null);
        }

        public void CompositeRoleChecks(IStrategy strategy, RoleType roleType, IObject role)
        {
            this.CompositeSharedChecks(strategy, roleType, role);
            if (!roleType.IsOne)
            {
                throw new ArgumentException("RelationType " + roleType + " has multiplicity many.");
            }
        }

        public void CompositeRolesChecks(IStrategy strategy, RoleType roleType, IObject role)
        {
            this.CompositeSharedChecks(strategy, roleType, role);
            if (!roleType.IsMany)
            {
                throw new ArgumentException("RelationType " + roleType + " has multiplicity one.");
            }
        }

        private void CompositeSharedChecks(IStrategy strategy, RoleType roleType, IObject role)
        {
            if (!this.ContainsConcreteClass(roleType.AssociationType.ObjectType, strategy.ObjectType))
            {
                throw new ArgumentException(strategy.ObjectType + " has no relationType with role " + roleType + ".");
            }

            if (role != null)
            {
                if (!strategy.Session.Equals(role.Strategy.Session))
                {
                    throw new ArgumentException(role + " is from different session");
                }

                if (role.Strategy.IsDeleted)
                {
                    throw new ArgumentException(roleType + " on object " + strategy + " is removed.");
                }

                if (!roleType.ObjectType.ContainsConcreteClass(role.Strategy.ObjectType))
                {
                    throw new ArgumentException(role.Strategy.ObjectType + " is not compatible with type " + roleType.ObjectType + " of role " + roleType + ".");
                }
            }
        }
   }
}