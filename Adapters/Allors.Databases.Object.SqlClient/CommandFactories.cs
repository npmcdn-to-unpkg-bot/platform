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
    using Allors.Databases.Object.SqlClient.Commands.Text;

    internal sealed class CommandFactories 
    {
        private readonly Database database;

        // Session
        private GetIObjectTypeFactory getIObjectTypeFactory;
        private InstantiateObjectsFactory instantiateObjectsFactory;
        private InsertObjectFactory insertObjectFactory;
        private InstantiateObjectFactory instantiateObjectFactory;
        private SetUnitRolesFactory setUnitRolesFactory;
        
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

        internal InstantiateObjectFactory InstantiateObjectFactory
        {
            get
            {
                return this.instantiateObjectFactory ?? (this.instantiateObjectFactory = new InstantiateObjectFactory(this.database));
            }
        }

        internal SetUnitRolesFactory SetUnitRolesFactory
        {
            get
            {
                return this.setUnitRolesFactory ?? (this.setUnitRolesFactory = new SetUnitRolesFactory(this.database));
            }
        }

        private Database SqlClientDatabase
        {
            get { return this.database; }
        }
    }
}