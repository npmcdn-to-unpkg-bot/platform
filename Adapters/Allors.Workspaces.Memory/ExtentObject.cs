// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtentObject.cs" company="Allors bvba">
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

namespace Allors.Workspaces.Memory
{
    using System.Collections.Generic;
    using Allors.Meta;

    public class ExtentObject : Extent
    {
        private readonly WorkspaceSession session;
        private readonly IComposite objectType;

        public ExtentObject(WorkspaceSession session, IComposite objectType)
        {
            this.session = session;
            this.objectType = objectType;
        }

        public override IComposite ObjectType
        {
            get { return this.objectType; }
        }

        internal override WorkspaceSession Session
        {
            get { return this.session; }
        }

        protected override HashSet<Strategy> GetExtentStrategies()
        {
            return this.session.GetStrategiesForExtent(this.objectType);
        }
    }
}