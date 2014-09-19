// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DerivationRelation.cs" company="Allors bvba">
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

namespace Allors.Domain
{
    using System.Text;

    using Allors;
    using Allors.Meta;

    public class DerivationRelation
    {
        private readonly IObject association;
        private readonly RoleType roleType;

        public DerivationRelation(IObject association, RoleType roleType)
        {
            this.association = association;
            this.roleType = roleType;
        }

        public IObject Association
        {
            get { return this.association; }
        }

        public RoleType RoleType
        {
            get { return this.roleType; }
        }

        public object Role
        {
            get { return this.association.Strategy.GetRole(this.roleType); }
        }

        public static DerivationRelation[] Create(IObject association, params RoleType[] roleTypes)
        {
            var derivationRoles = new DerivationRelation[roleTypes.Length];
            for (var i = 0; i < derivationRoles.Length; i++)
            {
                derivationRoles[i] = new DerivationRelation(association, roleTypes[i]);
            }

            return derivationRoles;
        }

        public static string ToString(DerivationRelation[] relations)
        {
            var stringBuilder = new StringBuilder();
            var first = true;
            foreach (var relation in relations)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    stringBuilder.Append(", ");
                }

                stringBuilder.Append(relation);
            }

            return stringBuilder.ToString();
        }

        public override string ToString()
        {
            string associationName = null;
            if (!this.Association.Strategy.IsDeleted && this.Association is UserInterfaceable)
            {
                associationName = ((UserInterfaceable)this.Association).DisplayName;
            }

            if (string.IsNullOrEmpty(associationName))
            {
                associationName = this.Association.Strategy.ObjectType.Name;
            }

            if (this.RoleType != null)
            {
                var roleName = this.RoleType.Name;
                return associationName + "." + roleName;
            }

            return associationName;
        }
    }
}