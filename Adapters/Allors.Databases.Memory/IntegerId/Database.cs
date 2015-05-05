// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Database.cs" company="Allors bvba">
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

namespace Allors.Databases.Memory.IntegerId
{
    public sealed class Database : Memory.Database
    {
        private Session session;

        public Database(Configuration configuration)
            : base(configuration)
        {
        }

        protected override Memory.Session Session
        {
            get { return this.session ?? (this.session = new Session(this)); }
        }

        public override void Init()
        {
            this.Session.Rollback();
            this.Session.Init();
            this.session = null;
            this.Properties = null;
        }        
    }
}