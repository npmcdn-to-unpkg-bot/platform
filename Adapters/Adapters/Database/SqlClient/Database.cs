// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Database.cs" company="Allors bvba">
//   Copyright 2002-2013 Allors bvba.
// Dual Licensed under
//   a) the Lesser General Public Licence v3 (LGPL)
//   b) the Allors License
// The LGPL License is included in the file lgpl.txt.
// The Allors License is an addendum to your contract.
// Allors Platform is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// For more information visit http://www.allors.com/legal
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Adapters.Database.SqlClient
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Xml;

    using Allors.Meta;

    public class Database : IDatabase
    {
        private readonly Guid id;
        private readonly ObjectIds objectIds;
        private readonly IObjectFactory objectFactory;
        private readonly IWorkspaceFactory workspaceFactory;
        private readonly string connectionString;
        private readonly int commandTimeout;
        private readonly IsolationLevel isolationLevel;
        private readonly Schema schema;
        private readonly RoleChecks roleChecks;

        private Dictionary<string, object> properties;

        public Database(Configuration configuration)
        {
            this.id = configuration.Id;

            this.objectIds = configuration.ObjectIds;
            this.objectFactory = configuration.ObjectFactory;
            this.workspaceFactory = configuration.WorkspaceFactory;
            this.connectionString = configuration.ConnectionString;
            this.commandTimeout = configuration.CommandTimeout;
            this.isolationLevel = configuration.IsolationLevel;
            this.roleChecks = new RoleChecks();

            this.schema = new Schema(this.MetaPopulation, this.ConnectionString, this.ObjectIds);
        }

        public event SessionCreatedEventHandler SessionCreated;

        public event ObjectNotLoadedEventHandler ObjectNotLoaded;

        public event RelationNotLoadedEventHandler RelationNotLoaded;
        
        public Guid Id 
        {
            get
            {
                return this.id;
            }
        }

        public IObjectFactory ObjectFactory 
        {
            get
            {
                return this.objectFactory;
            }
        }

        public bool IsDatabase
        {
            get
            {
                return true;
            }
        }

        public bool IsWorkspace 
        {
            get
            {
                return false;
            }
        }

        public bool IsShared 
        {
            get
            {
                return true;
            }
        }

        public IMetaPopulation MetaPopulation 
        {
            get
            {
                return this.ObjectFactory.MetaPopulation;
            }
        }

        public string ConnectionString
        {
            get
            {
                return this.connectionString;
            }
        }

        public int CommandTimeout
        {
            get
            {
                return this.commandTimeout;
            }
        }

        public IsolationLevel IsolationLevel
        {
            get
            {
                return this.isolationLevel;
            }
        }

        public ObjectIds ObjectIds
        {
            get
            {
                return this.objectIds;
            }
        }

        public Schema Schema
        {
            get
            {
                return this.schema;
            }
        }

        internal RoleChecks RoleChecks
        {
            get
            {
                return this.roleChecks;
            }
        }

        public object this[string name]
        {
            get
            {
                if (this.properties == null)
                {
                    return null;
                }

                object value;
                this.properties.TryGetValue(name, out value);
                return value;
            }

            set
            {
                if (this.properties == null)
                {
                    this.properties = new Dictionary<string, object>();
                }

                if (value == null)
                {
                    this.properties.Remove(name);
                }
                else
                {
                    this.properties[name] = value;
                }
            }
        }

        public void Init()
        {
            this.Schema.Init();
        }
        
        public IWorkspace CreateWorkspace()
        {
            if (this.workspaceFactory == null)
            {
                throw new Exception("No workspacefactory defined");
            }

            return this.workspaceFactory.CreateWorkspace(this);
        }

        IDatabaseSession IDatabase.CreateSession()
        {
            return this.CreateSession();
        }

        ISession IPopulation.CreateSession()
        {
            return this.CreateSession();
        }

        public DatabaseSession CreateSession()
        {
            return new DatabaseSession(this);
        }

        public void Load(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        public void Save(XmlWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}