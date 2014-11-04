// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Workspace.cs" company="Allors bvba">
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

namespace Allors.Adapters.Workspace.Memory
{
    using System;
    using System.Xml;

    using Allors.Adapters;
    using Allors.Populations;

    public abstract class Workspace : Population, IWorkspace
    {
        private readonly IDatabase database;

        private WorkspaceSession workspaceSession;

        protected Workspace(Configuration configuration)
            : base(configuration)
        {
            this.database = configuration.Database;
        }

        public override bool IsDatabase
        {
            get
            {
                return false;
            }
        }

        public override bool IsWorkspace
        {
            get
            {
                return true;
            }
        }

        public override Guid Id
        {
            get { return this.Database.Id; }
        }

        public IDatabase Database
        {
            get
            {
                return this.database;
            }
        }

        public bool AreSessionsShared
        {
            get { return true; }
        }

        public bool IsPopulationShared
        {
            get { return false; }
        }

        internal abstract ObjectIds ObjectIds { get; }

        internal virtual WorkspaceSession WorkspaceSession
        {
            get
            {
                return this.workspaceSession ?? (this.workspaceSession = this.CreateNewWorkspaceSession());
            }
        }

        public override ISession CreateSession()
        {
            return this.CreateWorkspaceSession();
        }

        IWorkspaceSession IWorkspace.CreateSession()
        {
            return this.CreateWorkspaceSession();
        }

        public override void Load(XmlReader reader)
        {
            if (this.workspaceSession != null)
            {
                this.workspaceSession.Rollback();
                this.workspaceSession.Clear();
            }

            this.workspaceSession = null;

            while (reader.Read()) 
            {
                // only process elements, ignore others
                if (reader.NodeType.Equals(XmlNodeType.Element))
                {
                    if (reader.Name.Equals(Serialization.Population))
                    {
                        Serialization.CheckVersion(reader);

                        if (!reader.IsEmptyElement)
                        {
                            this.WorkspaceSession.LoadPopulation(reader);
                        }

                        break;
                    }
                }
            }

            this.WorkspaceSession.Commit();
        }

        public override void Save(XmlWriter writer)
        {
            var writeDocument = false;
            if (writer.WriteState == WriteState.Start)
            {
                writer.WriteStartDocument();
                writer.WriteStartElement(Serialization.Allors);
                writeDocument = true;
            }

            this.WorkspaceSession.Save(writer);

            if (writeDocument)
            {
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        internal abstract bool IsWorkspaceNew(ObjectId objectId);

        protected abstract void ResetObjectIds();

        protected virtual WorkspaceSession CreateNewWorkspaceSession()
        {
            return new WorkspaceSession(this);
        }

        protected virtual IWorkspaceSession CreateWorkspaceSession()
        {
            return this.WorkspaceSession;
        }
    }
}