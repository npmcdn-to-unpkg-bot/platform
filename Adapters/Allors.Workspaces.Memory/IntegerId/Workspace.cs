// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Workspace.cs" company="Allors bvba">
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

namespace Allors.Workspaces.Memory.IntegerId
{
    public class Workspace : Memory.Workspace
    {
        private ObjectIds objectIds;

        public Workspace(Configuration configuration)
            : base(configuration)
        {
            this.objectIds = new ObjectIds();
        }

        internal override Memory.ObjectIds ObjectIds
        {
            get { return this.objectIds; }
        }

        internal override bool IsWorkspaceNew(ObjectId objectId)
        {
            var idInteger = (ObjectIdInteger)objectId;
            return idInteger.ValueInteger < 0;
        }

        protected override void ResetObjectIds()
        {
            this.objectIds = new ObjectIds();
        }
    }
}