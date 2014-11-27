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

namespace Allors.Adapters
{
    using System;
    using System.Collections.Generic;
    using System.Xml;

    using Meta;

    public abstract class Population : IPopulation
    {
        private readonly IObjectFactory objectFactory;

        private readonly Dictionary<IObjectType, object> concreteClassesByIObjectType;

        protected Dictionary<string, object> Properties;

        protected Population(Configuration configuration)
        {
            this.objectFactory = configuration.ObjectFactory;
            this.concreteClassesByIObjectType = new Dictionary<IObjectType, object>();
        }
        
        public abstract event SessionCreatedEventHandler SessionCreated;

        public IObjectFactory ObjectFactory
        {
            get
            {
                return this.objectFactory;
            }
        }

        public abstract string Id { get; }

        public abstract bool IsDatabase { get; }

        public abstract bool IsWorkspace { get; }

        public IMetaPopulation MetaPopulation 
        {
            get
            {
                return this.objectFactory.MetaPopulation;
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
        /// Assert that the unit is compatible with the IObjectType of the IRoleType.
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
        public object Internalize(object unit, IRoleType roleType)
        {
            var objectType = (IUnit)roleType.ObjectType;
            var unitTypeTag = objectType.UnitTag;

            var normalizedUnit = unit;

            switch (unitTypeTag)
            {
                case UnitTags.AllorsString:
                    if (!(unit is string))
                    {
                        throw new ArgumentException("IRoleType is not a String.");
                    }

                    var stringUnit = (string)unit;
                    var size = roleType.Size;
                    if (size > -1 && stringUnit.Length > size)
                    {
                        throw new ArgumentException("Size of relationType " + roleType + " is too big (" + stringUnit.Length + ">" + size + ").");
                    }

                    break;
                case UnitTags.AllorsInteger:
                    if (!(unit is int))
                    {
                        throw new ArgumentException("IRoleType is not an Integer.");
                    }

                    break;
                case UnitTags.AllorsFloat:
                    if (unit is int || unit is long || unit is float)
                    {
                        normalizedUnit = Convert.ToDouble(unit);
                    }
                    else if (!(unit is double))
                    {
                        throw new ArgumentException("RoleType is not a Double.");
                    }

                    break;
                case UnitTags.AllorsDecimal:
                    if (unit is int || unit is long || unit is float || unit is double)
                    {
                        normalizedUnit = Convert.ToDecimal(unit);
                    }
                    else if (!(unit is decimal))
                    {
                        throw new ArgumentException("IRoleType is not a Decimal.");
                    }

                    break;
                case UnitTags.AllorsBoolean:
                    if (!(unit is bool))
                    {
                        throw new ArgumentException("IRoleType is not a Boolean.");
                    }

                    break;
                case UnitTags.AllorsUnique:
                    if (!(unit is Guid))
                    {
                        throw new ArgumentException("IRoleType is not a Boolean.");
                    }

                    break;
                case UnitTags.AllorsBinary:
                    if (!(unit is byte[]))
                    {
                        throw new ArgumentException("IRoleType is not a Boolean.");
                    }

                    break;
                default:
                    throw new ArgumentException("Unknown Unit ObjectType: " + unitTypeTag);
            }

            return normalizedUnit;
        }

        public bool ContainsConcreteClass(IObjectType objectType, IObjectType concreteClass)
        {
            object concreteClassOrClasses;
            if (!this.concreteClassesByIObjectType.TryGetValue(objectType, out concreteClassOrClasses))
            {
                if (objectType.IsClass)
                {
                    concreteClassOrClasses = objectType;
                    this.concreteClassesByIObjectType[objectType] = concreteClassOrClasses;
                }
                else
                {
                    concreteClassOrClasses = new HashSet<IObjectType>(((IInterface)objectType).Subclasses);
                    this.concreteClassesByIObjectType[objectType] = concreteClassOrClasses;
                }
            }

            if (concreteClassOrClasses is IObjectType)
            {
                return concreteClass.Equals(concreteClassOrClasses);
            }

            var concreteClasses = (HashSet<IObjectType>)concreteClassOrClasses;
            return concreteClasses.Contains(concreteClass);
        }

        public void UnitRoleChecks(IStrategy strategy, IRoleType relationType)
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

        public void CompositeRoleChecks(IStrategy strategy, IRoleType roleType)
        {
            this.CompositeSharedChecks(strategy, roleType, null);
        }

        public void CompositeRoleChecks(IStrategy strategy, IRoleType roleType, IObject role)
        {
            this.CompositeSharedChecks(strategy, roleType, role);
            if (!roleType.IsOne)
            {
                throw new ArgumentException("IRelationType " + roleType + " has multiplicity many.");
            }
        }

        public void CompositeRolesChecks(IStrategy strategy, IRoleType roleType, IObject role)
        {
            this.CompositeSharedChecks(strategy, roleType, role);
            if (!roleType.IsMany)
            {
                throw new ArgumentException("IRelationType " + roleType + " has multiplicity one.");
            }
        }

        private void CompositeSharedChecks(IStrategy strategy, IRoleType roleType, IObject role)
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

                if (!ContainsConcreteClass(roleType.ObjectType, role.Strategy.ObjectType))
                {
                    throw new ArgumentException(role.Strategy.ObjectType + " is not compatible with type " + roleType.ObjectType + " of role " + roleType + ".");
                }
            }
        }
   }
}