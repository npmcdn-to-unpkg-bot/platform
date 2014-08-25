//------------------------------------------------------------------------------------------------- 
// <copyright file="ObjectFactory.cs" company="Allors bvba">
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
// <summary>Defines the ObjectBase type.</summary>
//-------------------------------------------------------------------------------------------------
namespace Allors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Allors.Meta;

    /// <summary>
    /// A base implementation for a static <see cref="IObjectFactory"/>.
    /// </summary>
    public class ObjectFactory : IObjectFactory
    {
        /// <summary>
        /// The domain.
        /// </summary>
        private readonly MetaDomain domain;

        /// <summary>
        ///  The assembly.
        /// </summary>
        private readonly Assembly assembly;

        /// <summary>
        /// The namespace.
        /// </summary>
        private readonly string ns;

        /// <summary>
        /// <see cref="Type"/> by <see cref="MetaObject"/> cache.
        /// </summary>
        private readonly Dictionary<MetaObject, Type> typeByObjectType;

        /// <summary>
        /// <see cref="Type"/> by <see cref="MetaObject"/> id cache.
        /// </summary>
        private readonly Dictionary<Type, MetaObject> objectTypeByType;

        /// <summary>
        /// <see cref="MetaObject"/> by <see cref="MetaObject"/> id cache.
        /// </summary>
        private readonly Dictionary<Guid, MetaObject> objectTypeByObjectTypeId;

        /// <summary>
        /// <see cref="ConstructorInfo"/> by <see cref="MetaObject"/> cache.
        /// </summary>
        private readonly Dictionary<MetaObject, ConstructorInfo> contructorInfoByObjectType;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectFactory"/> class.
        /// </summary>
        /// <param name="domain">
        /// The domain.
        /// </param>
        /// <param name="assembly">
        /// The assembly.
        /// </param>
        /// <param name="namespace">
        /// The namespace
        /// </param>
        public ObjectFactory(MetaDomain domain, Assembly assembly, string @namespace)
        {
            var validationLog = domain.Validate();
            if (validationLog.ContainsErrors)
            {
                throw new Exception(validationLog.ToString());
            }

            domain.Derive();

            this.domain = domain;
            this.assembly = assembly;
            this.ns = @namespace;

            this.typeByObjectType = new Dictionary<MetaObject, Type>();
            this.objectTypeByType = new Dictionary<Type, MetaObject>();
            this.objectTypeByObjectTypeId = new Dictionary<Guid, MetaObject>();
            this.contructorInfoByObjectType = new Dictionary<MetaObject, ConstructorInfo>();

            var types = assembly.GetTypes().Where(type => 
                type.Namespace != null && 
                type.Namespace.Equals(@namespace) && 
                type.GetInterfaces().Contains(typeof(IObject)));

            var typeByName = types.ToDictionary(type => type.Name, type => type);

            foreach (var objectType in domain.CompositeObjectTypes)
            {
                var type = typeByName[objectType.Name];

                this.typeByObjectType[objectType] = type;
                this.objectTypeByType[type] = objectType;
                this.objectTypeByObjectTypeId[objectType.Id] = objectType;

                if (objectType.IsConcrete)
                {
                    var parameterTypes = new[] { typeof(IStrategy) };
                    var constructor = type.GetConstructor(parameterTypes);
                    if (constructor == null)
                    {
                        throw new ArgumentException(objectType.Name + " has no Allors constructor.");
                    }

                    this.contructorInfoByObjectType[objectType] = constructor;
                }
            }
        }

        /// <summary>
        /// Gets the namespace.
        /// </summary>
        public string Namespace 
        {
            get
            {
                return this.ns;
            }
        }

        /// <summary>
        /// Gets the assembly.
        /// </summary>
        public Assembly Assembly
        {
            get
            {
                return this.assembly;
            }
        }

        /// <summary>
        /// Gets the domain.
        /// </summary>
        public MetaDomain Domain 
        {
            get
            {
                return this.domain;
            }
        }

        /// <summary>
        /// Creates a new <see cref="IObject"/> given the <see cref="IStrategy"/>.
        /// </summary>
        /// <param name="strategy">The <see cref="IStrategy"/> for the new <see cref="IObject"/>.</param>
        /// <returns>The new <see cref="IObject"/>.</returns>
        public IObject Create(IStrategy strategy)
        {
            var objectType = strategy.ObjectType;
            var constructor = this.contructorInfoByObjectType[objectType];
            object[] parameters = { strategy };
            
            return (IObject)constructor.Invoke(parameters);
        }

        /// <summary>
        /// Gets the .Net <see cref="Type"/> given the Allors <see cref="MetaObject"/>.
        /// </summary>
        /// <param name="type">The .Net <see cref="Type"/>.</param>
        /// <returns>The Allors <see cref="MetaObject"/>.</returns>
        public MetaObject GetObjectTypeForType(Type type)
        {
            MetaObject objectType;
            return !this.objectTypeByType.TryGetValue(type, out objectType) ? null : objectType;
        }

        /// <summary>
        /// Gets the .Net <see cref="Type"/> given the Allors <see cref="MetaObject"/>.
        /// </summary>
        /// <param name="objectType">The Allors <see cref="MetaObject"/>.</param>
        /// <returns>The .Net <see cref="Type"/>.</returns>
        public Type GetTypeForObjectType(MetaObject objectType)
        {
            return this.typeByObjectType[objectType];
        }

        /// <summary>
        /// Gets the .Net <see cref="Type"/> given the Allors <see cref="MetaObject"/>.
        /// </summary>
        /// <param name="objectTypeId">The Allors <see cref="MetaObject"/> id.</param>
        /// <returns>The .Net <see cref="Type"/>.</returns>
        public MetaObject GetObjectTypeForType(Guid objectTypeId)
        {
            return this.objectTypeByObjectTypeId[objectTypeId];
        }
    }
}