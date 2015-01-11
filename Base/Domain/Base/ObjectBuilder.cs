// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectBuilder.cs" company="Allors bvba">
//   Copyright 2002-2013 Allors bvba.
//
// Dual Licensed under
//   a) the General Public Licence v3 (GPL)
//   b) the Allors License
//
// The GPL License is included in the file gpl.txt.
// The Allors License is an addendum to your contract.
//
// Allors Applications is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// For more information visit http://www.allors.com/legal
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors
{
    using System;
    using System.Collections.Generic;

    using Allors.Meta;

    public abstract class ObjectBuilder<T> : ObjectBuilder where T : Domain.Object
    {
        private readonly ISession session;

        private bool built;
        private Exception exception;

        protected ObjectBuilder(ISession session)
        {
            this.session = session;
        }

        ~ObjectBuilder()
        {
            if (this.exception == null && !this.built)
            {
                throw new Exception(this + " was not built.");
            }
        }

        protected ISession Session
        {
            get { return this.session; }
        }

        protected IDatabaseSession DatabaseSession
        {
            get
            {
                return this.session as IDatabaseSession ?? ((IWorkspaceSession)this.session).DatabaseSession;
            }
        }

        public override void Dispose()
        {
            this.Build();
        }

        public override string ToString()
        {
            return "Builder for " + typeof(T).Name;
        }

        public override IObject DefaultBuild()
        {
            return this.Build();
        }

        public virtual T Build()
        {
            GC.SuppressFinalize(this);

            try
            {
                this.OnPreBuild();
                var instance = this.session.Create<T>();
                instance.OnBuild(this);

                this.OnPostBuild(instance);

                this.built = true;

                return instance;
            }
            catch (Exception e)
            {
                this.exception = e;
                throw;
            }
        }

        protected virtual void OnPreBuild()
        {
        }

        protected virtual void OnPostBuild(T instance)
        {
            instance.OnPostBuild().WithBuilder(this).Execute();
        }
    }

    public abstract class ObjectBuilder : IObjectBuilder
    {
        public static readonly Dictionary<IObjectType, Type> BuilderTypeByObjectTypeId = new Dictionary<IObjectType, Type>();

        public static object Build(ISession session, IObjectType objectType)
        {
            if (objectType.IsUnit)
            {
                var unit = (Unit)objectType;
                var unitTypeTag = unit.UnitTag;
                switch (unitTypeTag)
                {
                    case UnitTags.AllorsString:
                        return string.Empty;

                    case UnitTags.AllorsInteger:
                        return 0;

                    case UnitTags.AllorsDecimal:
                        return 0m;

                    case UnitTags.AllorsFloat:
                        return 0d;

                    case UnitTags.AllorsBoolean:
                        return false;

                    case UnitTags.AllorsBinary:
                        return new byte[0];

                    case UnitTags.AllorsUnique:
                        return Guid.NewGuid();

                    default:
                        throw new ArgumentException("Unknown Unit ObjectType: " + unitTypeTag);
                }
            }

            Type builderType;
            if (!BuilderTypeByObjectTypeId.TryGetValue(objectType, out builderType))
            {
                var builderTypeName = "Allors.Domain." + objectType.Name + "Builder";
                builderType = Type.GetType(builderTypeName, false);
                if (builderType != null)
                {
                    BuilderTypeByObjectTypeId[objectType] = builderType;
                }
            }

            if (builderType != null)
            {
                object[] parameters = { session };
                var builder = (IObjectBuilder)Activator.CreateInstance(builderType, parameters);
                return builder.DefaultBuild();
            }

            return session.Create((Class)objectType);
        }

        public abstract void Dispose();

        public abstract IObject DefaultBuild();
    }
}