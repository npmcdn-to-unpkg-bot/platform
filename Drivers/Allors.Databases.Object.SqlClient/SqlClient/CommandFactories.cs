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

    public sealed class CommandFactories 
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
        
        public CommandFactories(Database database)
        {
            this.database = database;
        }

        public GetIObjectTypeFactory GetIObjectTypeFactory
        {
            get
            {
                return this.getIObjectTypeFactory ?? (this.getIObjectTypeFactory = new GetIObjectTypeFactory(this.database));
            }
        }

        public CreateObjectFactory CreateObjectFactory
        {
            get
            {
                return this.createObjectFactory ?? (this.createObjectFactory = new CreateObjectFactory(this.SqlClientDatabase));
            }
        }

        public CreateObjectsFactory CreateObjectsFactory
        {
            get
            {
                return this.createObjectsFactory ?? (this.createObjectsFactory = new CreateObjectsFactory(this.SqlClientDatabase));
            }
        }

        public InsertObjectFactory InsertObjectFactory
        {
            get
            {
                return this.insertObjectFactory ?? (this.insertObjectFactory = new InsertObjectFactory(this.SqlClientDatabase));
            }
        }

        public InstantiateObjectsFactory InstantiateObjectsFactory
        {
            get
            {
                return this.instantiateObjectsFactory ?? (this.instantiateObjectsFactory = new InstantiateObjectsFactory(this.SqlClientDatabase));
            }
        }

        public AddCompositeRoleFactory AddCompositeRoleFactory
        {
            get
            {
                return this.addCompositeRoleFactory ?? (this.addCompositeRoleFactory = new AddCompositeRoleFactory(this.SqlClientDatabase));
            }
        }

        public RemoveCompositeRoleFactory RemoveCompositeRoleFactory
        {
            get
            {
                return this.removeCompositeRoleFactory ?? (this.removeCompositeRoleFactory = new RemoveCompositeRoleFactory(this.SqlClientDatabase));
            }
        }

        public InstantiateObjectFactory InstantiateObjectFactory
        {
            get
            {
                return this.instantiateObjectFactory ?? (this.instantiateObjectFactory = new InstantiateObjectFactory(this.database));
            }
        }

        public DeleteObjectFactory DeleteObjectFactory
        {
            get
            {
                return this.deleteObjectFactory ?? (this.deleteObjectFactory = new DeleteObjectFactory(this.database));
            }
        }
        
        public GetCompositeAssociationFactory GetCompositeAssociationFactory
        {
            get
            {
                return this.getCompositeAssociationFactory ?? (this.getCompositeAssociationFactory = new GetCompositeAssociationFactory(this.database));
            }
        }

        public GetCompositeAssociationsFactory GetCompositeAssociationsFactory
        {
            get
            {
                return this.getCompositeAssociationsFactory ?? (this.getCompositeAssociationsFactory = new GetCompositeAssociationsFactory(this.database));
            }
        }

        public GetCompositeRoleFactory GetCompositeRoleFactory
        {
            get
            {
                return this.getCompositeRoleFactory ?? (this.getCompositeRoleFactory = new GetCompositeRoleFactory(this.database));
            }
        }

        public GetCompositeRolesFactory GetCompositeRolesFactory
        {
            get
            {
                return this.getCompositeRolesFactory ?? (this.getCompositeRolesFactory = new GetCompositeRolesFactory(this.database));
            }
        }

        public GetUnitRolesFactory GetUnitRolesFactory
        {
            get
            {
                return this.getUnitRolesFactory ?? (this.getUnitRolesFactory = new GetUnitRolesFactory(this.database));
            }
        }

        public ClearCompositeAndCompositesRoleFactory ClearCompositeAndCompositesRoleFactory
        {
            get
            {
                return this.clearCompositeAndCompositesRoleFactory ?? (this.clearCompositeAndCompositesRoleFactory = new ClearCompositeAndCompositesRoleFactory(this.database));
            }
        }

        public SetCompositeRoleFactory SetCompositeRoleFactory
        {
            get
            {
                return this.setCompositeRoleFactory ?? (this.setCompositeRoleFactory = new SetCompositeRoleFactory(this.database));
            }
        }

        public SetUnitRoleFactory SetUnitRoleFactory
        {
            get
            {
                return this.setUnitRoleFactory ?? (this.setUnitRoleFactory = new SetUnitRoleFactory(this.database));
            }
        }

        public SetUnitRolesFactory SetUnitRolesFactory
        {
            get
            {
                return this.setUnitRolesFactory ?? (this.setUnitRolesFactory = new SetUnitRolesFactory(this.database));
            }
        }

        public GetCacheIdsFactory GetCacheIdsFactory
        {
            get
            {
                return this.getCacheIdsFactory ?? (this.getCacheIdsFactory = new GetCacheIdsFactory(this.database));
            }
        }

        public UpdateCacheIdsFactory UpdateCacheIdsFactory
        {
            get
            {
                return this.updateCacheIdsFactory ?? (this.updateCacheIdsFactory = new UpdateCacheIdsFactory(this.SqlClientDatabase));
            }
        }

        protected Database Database
        {
            get { return this.SqlClientDatabase; }
        }

        private Database SqlClientDatabase
        {
            get { return this.database; }
        }
    }
}