// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtentStatementChild.cs" company="Allors bvba">
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

namespace Allors.Adapters.Database.Sql
{
    using Allors.Meta;

    public class ExtentStatementChild : ExtentStatement
    {
        private readonly MetaAssociation associationType;
        private readonly MetaRole roleType;
        private readonly ExtentStatementRoot root;

        public ExtentStatementChild(ExtentStatementRoot root, SqlExtent extent, MetaRole roleType)
            : base(extent)
        {
            this.root = root;
            this.roleType = roleType;
        }

        public ExtentStatementChild(ExtentStatementRoot root, SqlExtent extent, MetaAssociation associationType)
            : base(extent)
        {
            this.root = root;
            this.associationType = associationType;
        }

        public MetaAssociation AssociationType
        {
            get { return this.associationType; }
        }

        public override bool IsRoot
        {
            get { return false; }
        }

        public MetaRole RoleType
        {
            get { return this.roleType; }
        }

        public override string ToString()
        {
            return this.root.ToString();
        }

        public override string AddParameter(object obj)
        {
            return this.root.AddParameter(obj);
        }

        public override void Append(string part)
        {
            this.root.Append(part);
        }

        public override string CreateAlias()
        {
            return this.root.CreateAlias();
        }

        public override ExtentStatement CreateChild(SqlExtent extent, MetaAssociation association)
        {
            return new ExtentStatementChild(this.root, extent, association);
        }

        public override ExtentStatement CreateChild(SqlExtent extent, MetaRole role)
        {
            return new ExtentStatementChild(this.root, extent, role);
        }
    }
}