// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtentRole.cs" company="Allors bvba">
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

    public class ExtentRole : Extent
    {
        private static readonly HashSet<Strategy> EmptySet = new HashSet<Strategy>();

        private readonly Strategy associationStrategy;
        private readonly IRoleType roleType;

        public ExtentRole(Strategy associationStrategy, IRoleType roleType)
        {
            this.associationStrategy = associationStrategy;
            this.roleType = roleType;
        }

        public override IComposite ObjectType
        {
            get { return (IComposite)this.roleType.ObjectType; }
        }
        
        internal override WorkspaceSession Session
        {
            get { return this.associationStrategy.WorkspaceSession; }
        }

        protected override HashSet<Strategy> GetExtentStrategies()
        {
            HashSet<Strategy> strategies = this.associationStrategy.WorkspaceSession.GetCompositeRoles(this.associationStrategy, this.roleType) ??
                               EmptySet;

            return strategies;
        }
    }
}