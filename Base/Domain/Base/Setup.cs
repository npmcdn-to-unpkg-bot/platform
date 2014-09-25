// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Setup.cs" company="Allors bvba">
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
    using System.Collections.Generic;
    using System.Reflection;

    using Allors.Domain;
    using Allors.Meta;

    public partial class Setup
    {
        private readonly IDatabaseSession session;
        private readonly Dictionary<ObjectType, IObjects> objectsByObjectType;
        private readonly ObjectsGraph objectsGraph;

        public Setup(IDatabaseSession session)
        {
            this.session = session;

            this.objectsByObjectType = new Dictionary<ObjectType, IObjects>();
            foreach (var objectType in session.Database.MetaPopulation.Composites)
            {
                this.objectsByObjectType[objectType] = objectType.GetObjects(session);
            }

            this.objectsGraph = new ObjectsGraph();
        }
        
        public void Apply()
        {
            this.OnPrePrepare();

            foreach (var objects in this.objectsByObjectType.Values)
            {
                objects.Prepare(this);
            }

            this.OnPostPrepare();

            this.OnPreSetup();

            this.objectsGraph.Invoke(objects => objects.Setup(this));

            this.OnPostSetup();

            this.session.Derive(true);

            new Security(this.session).Apply();
        }

        public void Add(IObjects objects)
        {
            this.objectsGraph.Add(objects);
        }
        
        public void AddDependency(ObjectType dependent, ObjectType dependee)
        {
            this.objectsGraph.AddDependency(this.objectsByObjectType[dependent], this.objectsByObjectType[dependee]);
        }
        
        private static byte[] GetEmbeddedResource(string resourceName)
        {
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
            if (stream != null)
            {
                var content = new byte[stream.Length];
                stream.Read(content, 0, content.Length);
                return content;
            }

            return null;
        }

        private void BaseOnPrePrepare()
        {
            var singleton = new SingletonBuilder(this.session).Build();

            singleton.DefaultSecurityToken = new SecurityTokenBuilder(this.session).Build();
            singleton.AdministratorSecurityToken = new SecurityTokenBuilder(this.session).Build();
        }

        private void BaseOnPostSetup()
        {
            var guest = new PersonBuilder(this.session).WithUserName("guest").WithLastName("Guest").Build();
            new UserGroups(this.session).Guests.AddMember(guest);
            Singleton.Instance(this.session).Guest = guest;

            var administrator = new PersonBuilder(this.session).WithUserName("administrator").WithLastName("Administrator").Build();
            new UserGroups(this.session).Administrators.AddMember(administrator);
        }

        private void BaseOnPostPrepare()
        {
        }

        private void BaseOnPreSetup()
        {
        }
    }
}