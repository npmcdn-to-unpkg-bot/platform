// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Singletons.cs" company="Allors bvba">
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
    using System;

    public partial class Singletons
    {
        private static readonly Guid PersonTemplateId = new Guid("7432004B-AEBC-4E6C-958E-5E83B4E027C9");

        protected override void CustomSetup(Allors.Setup setup)
        {
            base.CustomSetup(setup);

            this.Instance.PersonTemplate = new StringTemplateBuilder(this.Session).WithUniqueId(PersonTemplateId)
                                                   .WithName("Person Derivation")
                                                   .WithLocale(new Locales(this.Session).DutchBelgium)
                                                   .WithBody(@"main(this) ::= <<
Hello $this.UserName$!
>>")
                                                   .Build();
        }

        protected override void CustomSecure(Security config)
        {
            var defaultSecurityToken = this.Instance.DefaultSecurityToken;
            
            if (!this.Instance.ExistSalesAccessControl)
            {
                this.Instance.SalesAccessControl = new AccessControlBuilder(this.Session)
                .WithRole(new Roles(this.Session).Sales)
                .WithSubjectGroup(new UserGroups(this.Session).Sales)
                .Build();

                defaultSecurityToken.AddAccessControl(this.Instance.SalesAccessControl);
            }

            if (!this.Instance.ExistOperationsAccessControl)
            {
                this.Instance.OperationsAccessControl = new AccessControlBuilder(this.Session)
                .WithRole(new Roles(this.Session).Operations)
                .WithSubjectGroup(new UserGroups(this.Session).Operations)
                .Build();

                defaultSecurityToken.AddAccessControl(this.Instance.OperationsAccessControl);
            }

            if (!this.Instance.ExistProcurementAccessControl)
            {
                this.Instance.ProcurementAccessControl = new AccessControlBuilder(this.Session)
                .WithRole(new Roles(this.Session).Procurement)
                .WithSubjectGroup(new UserGroups(this.Session).Procurement)
                .Build();

                defaultSecurityToken.AddAccessControl(this.Instance.ProcurementAccessControl);
            }
        }
    }
}
