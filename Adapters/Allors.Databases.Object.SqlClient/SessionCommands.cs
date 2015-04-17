// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SessionCommands.cs" company="Allors bvba">
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

    internal sealed class SessionCommands
    {
        private readonly DatabaseSession session;
        private readonly CommandFactories commandFactories;

        private InstantiateObjectsFactory.InstantiateObjects instantiateObjects;
        private SetUnitRolesFactory.SetUnitRoles setUnitRoles;

        internal SessionCommands(DatabaseSession session)
        {
            this.session = session;
            this.commandFactories = this.session.SqlClientDatabase.SqlClientCommandFactories;
        }

        internal InstantiateObjectsFactory.InstantiateObjects InstantiateObjectsCommand
        {
            get
            {
                return this.instantiateObjects ?? (this.instantiateObjects = this.commandFactories.InstantiateObjectsFactory.Create(this.session));
            }
        }

       internal SetUnitRolesFactory.SetUnitRoles SetUnitRolesCommand
        {
            get
            {
                return this.setUnitRoles ?? (this.setUnitRoles = this.commandFactories.SetUnitRolesFactory.Create(this.session));
            }
        }
    }
}