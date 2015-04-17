// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandFactories.cs" company="Allors bvba">
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

namespace Allors.Databases.Object.SqlClient
{
    using Allors.Databases.Object.SqlClient.Commands.Procedure;
    using Allors.Databases.Object.SqlClient.Commands.Text;

    internal sealed class CommandFactories 
    {
        private readonly Database database;

        // Session
        private GetIObjectTypeFactory getIObjectTypeFactory;
        private InstantiateObjectsFactory instantiateObjectsFactory;
        private RemoveCompositeRoleFactory removeCompositeRoleFactory;
        private InsertObjectFactory insertObjectFactory;
        private InstantiateObjectFactory instantiateObjectFactory;
        private DeleteObjectFactory deleteObjectFactory;
        private GetCompositeRolesFactory getCompositeRolesFactory;
        private GetUnitRolesFactory getUnitRolesFactory;
        private SetCompositeRoleFactory setCompositeRoleFactory;
        private SetUnitRoleFactory setUnitRoleFactory;
        private SetUnitRolesFactory setUnitRolesFactory;
        private UpdateCacheIdsFactory updateCacheIdsFactory;
        
        internal CommandFactories(Database database)
        {
            this.database = database;
        }

        internal GetIObjectTypeFactory GetIObjectTypeFactory
        {
            get
            {
                return this.getIObjectTypeFactory ?? (this.getIObjectTypeFactory = new GetIObjectTypeFactory(this.database));
            }
        }

        internal InsertObjectFactory InsertObjectFactory
        {
            get
            {
                return this.insertObjectFactory ?? (this.insertObjectFactory = new InsertObjectFactory(this.SqlClientDatabase));
            }
        }

        internal InstantiateObjectsFactory InstantiateObjectsFactory
        {
            get
            {
                return this.instantiateObjectsFactory ?? (this.instantiateObjectsFactory = new InstantiateObjectsFactory(this.SqlClientDatabase));
            }
        }

        internal RemoveCompositeRoleFactory RemoveCompositeRoleFactory
        {
            get
            {
                return this.removeCompositeRoleFactory ?? (this.removeCompositeRoleFactory = new RemoveCompositeRoleFactory(this.SqlClientDatabase));
            }
        }

        internal InstantiateObjectFactory InstantiateObjectFactory
        {
            get
            {
                return this.instantiateObjectFactory ?? (this.instantiateObjectFactory = new InstantiateObjectFactory(this.database));
            }
        }

        internal DeleteObjectFactory DeleteObjectFactory
        {
            get
            {
                return this.deleteObjectFactory ?? (this.deleteObjectFactory = new DeleteObjectFactory(this.database));
            }
        }
        
        internal GetCompositeRolesFactory GetCompositeRolesFactory
        {
            get
            {
                return this.getCompositeRolesFactory ?? (this.getCompositeRolesFactory = new GetCompositeRolesFactory(this.database));
            }
        }

        internal GetUnitRolesFactory GetUnitRolesFactory
        {
            get
            {
                return this.getUnitRolesFactory ?? (this.getUnitRolesFactory = new GetUnitRolesFactory(this.database));
            }
        }

        internal SetCompositeRoleFactory SetCompositeRoleFactory
        {
            get
            {
                return this.setCompositeRoleFactory ?? (this.setCompositeRoleFactory = new SetCompositeRoleFactory(this.database));
            }
        }

        internal SetUnitRoleFactory SetUnitRoleFactory
        {
            get
            {
                return this.setUnitRoleFactory ?? (this.setUnitRoleFactory = new SetUnitRoleFactory(this.database));
            }
        }

        internal SetUnitRolesFactory SetUnitRolesFactory
        {
            get
            {
                return this.setUnitRolesFactory ?? (this.setUnitRolesFactory = new SetUnitRolesFactory(this.database));
            }
        }

        internal UpdateCacheIdsFactory UpdateCacheIdsFactory
        {
            get
            {
                return this.updateCacheIdsFactory ?? (this.updateCacheIdsFactory = new UpdateCacheIdsFactory(this.SqlClientDatabase));
            }
        }

        private Database SqlClientDatabase
        {
            get { return this.database; }
        }
    }
}