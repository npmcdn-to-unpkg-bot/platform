// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtentAssociation.cs" company="Allors bvba">
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

namespace Allors.Adapters.Workspace.Memory
{
    using System.Collections.Generic;
    using Allors.Meta;

    public sealed class ExtentAssociation : Extent
    {
        private static readonly HashSet<Strategy> EmptySet = new HashSet<Strategy>();

        private readonly Strategy roleStrategy;
        private readonly IAssociationType associationType;

        public ExtentAssociation(Strategy roleStrategy, IAssociationType associationType)
        {
            this.roleStrategy = roleStrategy;
            this.associationType = associationType;
        }

        public override IComposite ObjectType
        {
            get { return this.associationType.ObjectType; }
        }

        internal override WorkspaceSession Session
        {
            get { return this.roleStrategy.WorkspaceSession; }
        }

        protected override HashSet<Strategy> GetExtentStrategies()
        {
            HashSet<Strategy> strategies = this.roleStrategy.WorkspaceSession.GetAssociations(this.roleStrategy, this.associationType) ??
                                           EmptySet;

            return strategies;
        }
    }
}