// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Conflict.cs" company="Allors bvba">
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
    using Allors.Meta;

    public class Conflict : IConflict
    {
        private readonly IRoleType roleType;
        private readonly Strategy strategy;

        internal Conflict(Strategy strategy, IRoleType roleType)
        {
            this.strategy = strategy;
            this.roleType = roleType;
        }

        internal Conflict(Strategy strategy)
        {
            this.strategy = strategy;
        }

        public IObject Object
        {
            get { return this.Strategy.GetObject(); }
        }

        public ObjectId ObjectId
        {
            get { return this.Strategy.ObjectId; }
        }

        public IRoleType RoleType
        {
            get { return this.roleType; }
        }

        internal Strategy Strategy
        {
            get { return this.strategy; }
        }
    }
}