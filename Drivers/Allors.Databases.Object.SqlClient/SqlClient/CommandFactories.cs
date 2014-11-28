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

    using AddCompositeRoleFactory = Allors.Databases.Object.SqlClient.Commands.Procedure.AddCompositeRoleFactory;

    internal sealed class CommandFactories 
    {
        private readonly Database database;

        // Session
        private GetIObjectTypeFactory getIObjectTypeFactory;
        private InstantiateObjectsFactory instantiateObjectsFactory;
        private AddCompositeRoleFactory addCompositeRoleFactory;
        private RemoveCompositeRoleFactory removeCompositeRoleFactory;
        private CreateObjectFactory createObjectFactory;
        private CreateObjectsFactory createObjectsFactory;
        private InsertObjectFactory insertObjectFactory;
        private InstantiateObjectFactory instantiateObjectFactory;
        private DeleteObjectFactory deleteObjectFactory;
        private GetCompositeAssociationFactory getCompositeAssociationFactory;
        private GetCompositeAssociationsFactory getCompositeAssociationsFactory;
        private GetCompositeRoleFactory getCompositeRoleFactory;
        private GetCompositeRolesFactory getCompositeRolesFactory;
        private GetUnitRolesFactory getUnitRolesFactory;
        private ClearCompositeAndCompositesRoleFactory clearCompositeAndCompositesRoleFactory;
        private SetCompositeRoleFactory setCompositeRoleFactory;
        private SetUnitRoleFactory setUnitRoleFactory;
        private SetUnitRolesFactory setUnitRolesFactory;
        private GetCacheIdsFactory getCacheIdsFactory;
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

        internal CreateObjectFactory CreateObjectFactory
        {
            get
            {
                return this.createObjectFactory ?? (this.createObjectFactory = new CreateObjectFactory(this.SqlClientDatabase));
            }
        }

        internal CreateObjectsFactory CreateObjectsFactory
        {
            get
            {
                return this.createObjectsFactory ?? (this.createObjectsFactory = new CreateObjectsFactory(this.SqlClientDatabase));
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

        internal AddCompositeRoleFactory AddCompositeRoleFactory
        {
            get
            {
                return this.addCompositeRoleFactory ?? (this.addCompositeRoleFactory = new AddCompositeRoleFactory(this.SqlClientDatabase));
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
        
        internal GetCompositeAssociationFactory GetCompositeAssociationFactory
        {
            get
            {
                return this.getCompositeAssociationFactory ?? (this.getCompositeAssociationFactory = new GetCompositeAssociationFactory(this.database));
            }
        }

        internal GetCompositeAssociationsFactory GetCompositeAssociationsFactory
        {
            get
            {
                return this.getCompositeAssociationsFactory ?? (this.getCompositeAssociationsFactory = new GetCompositeAssociationsFactory(this.database));
            }
        }

        internal GetCompositeRoleFactory GetCompositeRoleFactory
        {
            get
            {
                return this.getCompositeRoleFactory ?? (this.getCompositeRoleFactory = new GetCompositeRoleFactory(this.database));
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

        internal ClearCompositeAndCompositesRoleFactory ClearCompositeAndCompositesRoleFactory
        {
            get
            {
                return this.clearCompositeAndCompositesRoleFactory ?? (this.clearCompositeAndCompositesRoleFactory = new ClearCompositeAndCompositesRoleFactory(this.database));
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

        internal GetCacheIdsFactory GetCacheIdsFactory
        {
            get
            {
                return this.getCacheIdsFactory ?? (this.getCacheIdsFactory = new GetCacheIdsFactory(this.database));
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