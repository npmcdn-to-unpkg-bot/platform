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
        private readonly IMetaPopulation metaPopulation;

        /// <summary>
        ///  The assembly.
        /// </summary>
        private readonly Assembly assembly;

        /// <summary>
        /// The namespace.
        /// </summary>
        private readonly string ns;

        /// <summary>
        /// <see cref="Type"/> by <see cref="IObjectType"/> cache.
        /// </summary>
        private readonly Dictionary<IObjectType, Type> typeByObjectType;

        /// <summary>
        /// <see cref="Type"/> by <see cref="IObjectType"/> id cache.
        /// </summary>
        private readonly Dictionary<Type, IObjectType> objectTypeByType;

        /// <summary>
        /// <see cref="IObjectType"/> by <see cref="IObjectType"/> id cache.
        /// </summary>
        private readonly Dictionary<Guid, IObjectType> objectTypeByObjectTypeId;

        /// <summary>
        /// <see cref="ConstructorInfo"/> by <see cref="IObjectType"/> cache.
        /// </summary>
        private readonly Dictionary<IObjectType, ConstructorInfo> contructorInfoByObjectType;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectFactory"/> class.
        /// </summary>
        /// <param name="environment">
        /// The domain.
        /// </param>
        /// <param name="assembly">
        /// The assembly.
        /// </param>
        /// <param name="namespace">
        /// The namespace
        /// </param>
        public ObjectFactory(IMetaPopulation metaPopulation, Assembly assembly, string @namespace)
        {
            this.metaPopulation = metaPopulation;
            this.assembly = assembly;
            this.ns = @namespace;

            var validationLog = metaPopulation.Validate();
            if (validationLog.ContainsErrors)
            {
                throw new Exception(validationLog.ToString());
            }

            metaPopulation.Bind(assembly);
            
            this.typeByObjectType = new Dictionary<IObjectType, Type>();
            this.objectTypeByType = new Dictionary<Type, IObjectType>();
            this.objectTypeByObjectTypeId = new Dictionary<Guid, IObjectType>();
            this.contructorInfoByObjectType = new Dictionary<IObjectType, ConstructorInfo>();

            var types = assembly.GetTypes().Where(type => 
                type.Namespace != null && 
                type.Namespace.Equals(@namespace) && 
                type.GetInterfaces().Contains(typeof(IObject)));

            var typeByName = types.ToDictionary(type => type.Name, type => type);

            foreach (var objectType in metaPopulation.Composites)
            {
                var type = typeByName[objectType.Name];

                this.typeByObjectType[objectType] = type;
                this.objectTypeByType[type] = objectType;
                this.objectTypeByObjectTypeId[objectType.Id] = objectType;

                if (objectType is IClass)
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
        public IMetaPopulation MetaPopulation 
        {
            get
            {
                return this.metaPopulation;
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
        /// Gets the .Net <see cref="Type"/> given the Allors <see cref="IObjectType"/>.
        /// </summary>
        /// <param name="type">The .Net <see cref="Type"/>.</param>
        /// <returns>The Allors <see cref="IObjectType"/>.</returns>
        public IObjectType GetObjectTypeForType(Type type)
        {
            IObjectType objectType;
            return !this.objectTypeByType.TryGetValue(type, out objectType) ? null : objectType;
        }

        /// <summary>
        /// Gets the .Net <see cref="Type"/> given the Allors <see cref="IObjectType"/>.
        /// </summary>
        /// <param name="objectType">The Allors <see cref="IObjectType"/>.</param>
        /// <returns>The .Net <see cref="Type"/>.</returns>
        public Type GetTypeForObjectType(IObjectType objectType)
        {
            return this.typeByObjectType[objectType];
        }

        /// <summary>
        /// Gets the .Net <see cref="Type"/> given the Allors <see cref="IObjectType"/>.
        /// </summary>
        /// <param name="objectTypeId">The Allors <see cref="IObjectType"/> id.</param>
        /// <returns>The .Net <see cref="Type"/>.</returns>
        public IObjectType GetObjectTypeForType(Guid objectTypeId)
        {
            return this.objectTypeByObjectTypeId[objectTypeId];
        }
    }
}