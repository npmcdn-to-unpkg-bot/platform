//------------------------------------------------------------------------------------------------- 
// <copyright file="DatabaseSession.cs" company="Allors bvba">
// Copyright 2002-2013 Allors bvba.
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
// <summary>Defines the DatabaseSession type.</summary>
//-------------------------------------------------------------------------------------------------
using Allors;

namespace Allors.Databases.Memory.IntegerId
{
    using System;

    internal sealed class Session : Memory.Session
    {
        private ObjectIds objectIds;

        public Session(Memory.Database database)
            : base(database)
        {
            this.Reset();
        }

        public override IWorkspaceSession WorkspaceSession
        {
            get { throw new NotSupportedException(); }
        }

        internal override Memory.ObjectIds ObjectIds
        {
            get { return this.objectIds; }
        }

        internal override void Reset()
        {
            base.Reset();
            this.objectIds = new ObjectIds();
        }
    }
}