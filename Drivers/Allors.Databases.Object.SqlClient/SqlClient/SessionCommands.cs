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
    using Allors.Databases.Object.SqlClient.Commands.Procedure;
    using Allors.Databases.Object.SqlClient.Commands.Text;

    internal sealed class SessionCommands
    {
        private readonly DatabaseSession session;
        private readonly CommandFactories commandFactories;

        private GetIObjectTypeFactory.GetIObjectType getIObjectType;
        private CreateObjectFactory.CreateObject createObjectCommand;
        private CreateObjectsFactory.CreateObjects createObjects;
        private InsertObjectFactory.InsertObject insertObject;
        private DeleteObjectFactory.DeleteObject deleteObject;
        private InstantiateObjectFactory.InstantiateObject instantiateObject;
        private InstantiateObjectsFactory.InstantiateObjects instantiateObjects;
        private GetCompositeRoleFactory.GetCompositeRole getCompositeRole;
        private SetCompositeRoleFactory.SetCompositeRole setCompositeRole;
        private ClearCompositeAndCompositesRoleFactory.ClearCompositeAndCompositesRole clearCompositeAndCompoisitesRole;
        private GetCompositeAssociationFactory.GetCompositeAssociation getCompositeAssociation;
        private GetCompositeRolesFactory.GetCompositeRoles getCompositeRoles;
        private AddCompositeRoleFactory.AddCompositeRole addCompositeRole;
        private RemoveCompositeRoleFactory.RemoveCompositeRole removeCompositeRole;
        private GetCompositeAssociationsFactory.GetCompositeAssociations getCompositeAssociations;
        private UpdateCacheIdsFactory.UpdateCacheIds updateCacheIds;
        private GetUnitRolesFactory.GetUnitRoles getUnitRoles;
        private SetUnitRoleFactory.SetUnitRole setUnitRole;
        private SetUnitRolesFactory.SetUnitRoles setUnitRoles;
        private GetCacheIdsFactory.GetCacheIds getCacheIds;

        internal SessionCommands(DatabaseSession session)
        {
            this.session = session;
            this.commandFactories = this.session.SqlClientDatabase.SqlClientCommandFactories;
        }

        internal GetIObjectTypeFactory.GetIObjectType GetObjectType
        {
            get
            {
                return this.getIObjectType ?? (this.getIObjectType = this.commandFactories.GetIObjectTypeFactory.Create(this.session));
            }
        }

        internal CreateObjectFactory.CreateObject CreateObjectCommand
        {
            get
            {
                return this.createObjectCommand ?? (this.createObjectCommand = this.commandFactories.CreateObjectFactory.Create(this.session));
            }
        }

        internal CreateObjectsFactory.CreateObjects CreateObjectsCommand
        {
            get
            {
                return this.createObjects ?? (this.createObjects = this.commandFactories.CreateObjectsFactory.Create(this.session));
            }
        }

        internal InsertObjectFactory.InsertObject InsertObjectCommand
        {
            get
            {
                return this.insertObject ?? (this.insertObject = this.commandFactories.InsertObjectFactory.Create(this.session));
            }
        }

        internal DeleteObjectFactory.DeleteObject DeleteObjectCommand
        {
            get
            {
                return this.deleteObject ?? (this.deleteObject = this.commandFactories.DeleteObjectFactory.Create(this.session));
            }
        }

        internal InstantiateObjectFactory.InstantiateObject InstantiateObjectCommand
        {
            get
            {
                return this.instantiateObject ?? (this.instantiateObject = this.commandFactories.InstantiateObjectFactory.Create(this.session));
            }
        }

        internal InstantiateObjectsFactory.InstantiateObjects InstantiateObjectsCommand
        {
            get
            {
                return this.instantiateObjects ?? (this.instantiateObjects = this.commandFactories.InstantiateObjectsFactory.Create(this.session));
            }
        }

        internal GetUnitRolesFactory.GetUnitRoles GetUnitRolesCommand
        {
            get
            {
                return this.getUnitRoles ?? (this.getUnitRoles = this.commandFactories.GetUnitRolesFactory.Create(this.session));
            }
        }

        internal SetUnitRoleFactory.SetUnitRole SetUnitRoleCommand
        {
            get
            {
                return this.setUnitRole ?? (this.setUnitRole = this.commandFactories.SetUnitRoleFactory.Create(this.session));
            }
        }

        internal SetUnitRolesFactory.SetUnitRoles SetUnitRolesCommand
        {
            get
            {
                return this.setUnitRoles ?? (this.setUnitRoles = this.commandFactories.SetUnitRolesFactory.Create(this.session));
            }
        }

        internal GetCompositeRoleFactory.GetCompositeRole GetCompositeRoleCommand
        {
            get
            {
                return this.getCompositeRole ?? (this.getCompositeRole = this.commandFactories.GetCompositeRoleFactory.Create(this.session));
            }
        }

        internal SetCompositeRoleFactory.SetCompositeRole SetCompositeRoleCommand
        {
            get
            {
                return this.setCompositeRole ?? (this.setCompositeRole = this.commandFactories.SetCompositeRoleFactory.Create(this.session));
            }
        }

        internal ClearCompositeAndCompositesRoleFactory.ClearCompositeAndCompositesRole ClearCompositeAndCompositesRoleCommand
        {
            get
            {
                return this.clearCompositeAndCompoisitesRole ?? (this.clearCompositeAndCompoisitesRole = this.commandFactories.ClearCompositeAndCompositesRoleFactory.Create(this.session));
            }
        }

        internal GetCompositeAssociationFactory.GetCompositeAssociation GetCompositeAssociationCommand
        {
            get
            {
                return this.getCompositeAssociation ?? (this.getCompositeAssociation = this.commandFactories.GetCompositeAssociationFactory.Create(this.session));
            }
        }

        internal GetCompositeRolesFactory.GetCompositeRoles GetCompositeRolesCommand
        {
            get
            {
                return this.getCompositeRoles ?? (this.getCompositeRoles = this.commandFactories.GetCompositeRolesFactory.Create(this.session));
            }
        }

        internal AddCompositeRoleFactory.AddCompositeRole AddCompositeRoleCommand
        {
            get
            {
                return this.addCompositeRole ?? (this.addCompositeRole = this.commandFactories.AddCompositeRoleFactory.Create(this.session));
            }
        }

        internal RemoveCompositeRoleFactory.RemoveCompositeRole RemoveCompositeRoleCommand
        {
            get
            {
                return this.removeCompositeRole ?? (this.removeCompositeRole = this.commandFactories.RemoveCompositeRoleFactory.Create(this.session));
            }
        }

        internal GetCompositeAssociationsFactory.GetCompositeAssociations GetCompositeAssociationsCommand
        {
            get
            {
                return this.getCompositeAssociations ?? (this.getCompositeAssociations = this.commandFactories.GetCompositeAssociationsFactory.Create(this.session));
            }
        }

        internal UpdateCacheIdsFactory.UpdateCacheIds UpdateCacheIdsCommand
        {
            get
            {
                return this.updateCacheIds ?? (this.updateCacheIds = this.commandFactories.UpdateCacheIdsFactory.Create(this.session));
            }
        }

        internal GetCacheIdsFactory.GetCacheIds GetCacheIdsCommand
        {
            get
            {
                return this.getCacheIds ?? (this.getCacheIds = this.commandFactories.GetCacheIdsFactory.Create(this.session));
            }
        }
    }
}