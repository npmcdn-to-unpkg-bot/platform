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

namespace Allors.R1.Adapters.Database.SqlClient
{
    using Allors.R1.Adapters.Database.Sql.Commands;
    using Allors.R1.Adapters.Database.SqlClient.Commands.Procedure;
    using Allors.R1.Adapters.Database.SqlClient.Commands.Text;

    using AddCompositeRoleFactory = Allors.R1.Adapters.Database.SqlClient.Commands.Procedure.AddCompositeRoleFactory;

    public sealed class CommandFactories : Sql.CommandFactories
    {
        private readonly Database database;

        // Session
        private IGetObjectTypeFactory getObjectTypeFactory;
        private IInstantiateObjectsFactory instantiateObjectsFactory;
        private IAddCompositeRoleFactory addCompositeRoleFactory;
        private IRemoveCompositeRoleFactory removeCompositeRoleFactory;
        private ICreateObjectFactory createObjectFactory;
        private ICreateObjectsFactory createObjectsFactory;
        private IInsertObjectFactory insertObjectFactory;
        private IInstantiateObjectFactory instantiateObjectFactory;
        private IDeleteObjectFactory deleteObjectFactory;
        private IGetCompositeAssociationFactory getCompositeAssociationFactory;
        private IGetCompositeAssociationsFactory getCompositeAssociationsFactory;
        private IGetCompositeRoleFactory getCompositeRoleFactory;
        private IGetCompositeRolesFactory getCompositeRolesFactory;
        private IGetUnitRolesFactory getUnitRolesFactory;
        private IClearCompositeAndCompositesRoleFactory clearCompositeAndCompositesRoleFactory;
        private ISetCompositeRoleFactory setCompositeRoleFactory;
        private ISetUnitRoleFactory setUnitRoleFactory;
        private ISetUnitRolesFactory setUnitRolesFactory;
        private IGetCacheIdsFactory getCacheIdsFactory;
        private IUpdateCacheIdsFactory updateCacheIdsFactory;
        
        public CommandFactories(Database database)
        {
            this.database = database;
        }

        public override IGetObjectTypeFactory GetObjectTypeFactory
        {
            get
            {
                return this.getObjectTypeFactory ?? (this.getObjectTypeFactory = new GetObjectTypeFactory(this.database));
            }
        }

        public override ICreateObjectFactory CreateObjectFactory
        {
            get
            {
                return this.createObjectFactory ?? (this.createObjectFactory = new CreateObjectFactory(this.SqlClientDatabase));
            }
        }

        public override ICreateObjectsFactory CreateObjectsFactory
        {
            get
            {
                return this.createObjectsFactory ?? (this.createObjectsFactory = new CreateObjectsFactory(this.SqlClientDatabase));
            }
        }

        public override IInsertObjectFactory InsertObjectFactory
        {
            get
            {
                return this.insertObjectFactory ?? (this.insertObjectFactory = new InsertObjectFactory(this.SqlClientDatabase));
            }
        }

        public override IInstantiateObjectsFactory InstantiateObjectsFactory
        {
            get
            {
                return this.instantiateObjectsFactory ?? (this.instantiateObjectsFactory = new InstantiateObjectsFactory(this.SqlClientDatabase));
            }
        }

        public override IAddCompositeRoleFactory AddCompositeRoleFactory
        {
            get
            {
                return this.addCompositeRoleFactory ?? (this.addCompositeRoleFactory = new AddCompositeRoleFactory(this.SqlClientDatabase));
            }
        }

        public override IRemoveCompositeRoleFactory RemoveCompositeRoleFactory
        {
            get
            {
                return this.removeCompositeRoleFactory ?? (this.removeCompositeRoleFactory = new RemoveCompositeRoleFactory(this.SqlClientDatabase));
            }
        }

        public override IInstantiateObjectFactory InstantiateObjectFactory
        {
            get
            {
                return this.instantiateObjectFactory ?? (this.instantiateObjectFactory = new InstantiateObjectFactory(this.database));
            }
        }

        public override IDeleteObjectFactory DeleteObjectFactory
        {
            get
            {
                return this.deleteObjectFactory ?? (this.deleteObjectFactory = new DeleteObjectFactory(this.database));
            }
        }
        
        public override IGetCompositeAssociationFactory GetCompositeAssociationFactory
        {
            get
            {
                return this.getCompositeAssociationFactory ?? (this.getCompositeAssociationFactory = new GetCompositeAssociationFactory(this.database));
            }
        }

        public override IGetCompositeAssociationsFactory GetCompositeAssociationsFactory
        {
            get
            {
                return this.getCompositeAssociationsFactory ?? (this.getCompositeAssociationsFactory = new GetCompositeAssociationsFactory(this.database));
            }
        }

        public override IGetCompositeRoleFactory GetCompositeRoleFactory
        {
            get
            {
                return this.getCompositeRoleFactory ?? (this.getCompositeRoleFactory = new GetCompositeRoleFactory(this.database));
            }
        }

        public override IGetCompositeRolesFactory GetCompositeRolesFactory
        {
            get
            {
                return this.getCompositeRolesFactory ?? (this.getCompositeRolesFactory = new GetCompositeRolesFactory(this.database));
            }
        }

        public override IGetUnitRolesFactory GetUnitRolesFactory
        {
            get
            {
                return this.getUnitRolesFactory ?? (this.getUnitRolesFactory = new GetUnitRolesFactory(this.database));
            }
        }

        public override IClearCompositeAndCompositesRoleFactory ClearCompositeAndCompositesRoleFactory
        {
            get
            {
                return this.clearCompositeAndCompositesRoleFactory ?? (this.clearCompositeAndCompositesRoleFactory = new ClearCompositeAndCompositesRoleFactory(this.database));
            }
        }

        public override ISetCompositeRoleFactory SetCompositeRoleFactory
        {
            get
            {
                return this.setCompositeRoleFactory ?? (this.setCompositeRoleFactory = new SetCompositeRoleFactory(this.database));
            }
        }

        public override ISetUnitRoleFactory SetUnitRoleFactory
        {
            get
            {
                return this.setUnitRoleFactory ?? (this.setUnitRoleFactory = new SetUnitRoleFactory(this.database));
            }
        }

        public override ISetUnitRolesFactory SetUnitRolesFactory
        {
            get
            {
                return this.setUnitRolesFactory ?? (this.setUnitRolesFactory = new SetUnitRolesFactory(this.database));
            }
        }

        public override IGetCacheIdsFactory GetCacheIdsFactory
        {
            get
            {
                return this.getCacheIdsFactory ?? (this.getCacheIdsFactory = new GetCacheIdsFactory(this.database));
            }
        }

        public override IUpdateCacheIdsFactory UpdateCacheIdsFactory
        {
            get
            {
                return this.updateCacheIdsFactory ?? (this.updateCacheIdsFactory = new UpdateCacheIdsFactory(this.SqlClientDatabase));
            }
        }

        protected override Sql.Database Database
        {
            get { return this.SqlClientDatabase; }
        }

        private Database SqlClientDatabase
        {
            get { return this.database; }
        }
    }
}