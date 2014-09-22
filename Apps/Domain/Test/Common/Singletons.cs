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
    using Allors.Domain;

    using global::System;

    public partial class Singletons
    {
        private static readonly Guid PersonTemplateId = new Guid("7432004B-AEBC-4E6C-958E-5E83B4E027C9");

        protected override void TestSetup(Allors.Setup setup)
        {
            base.TestSetup(setup);

            this.Instance.PersonTemplate = new StringTemplateBuilder(this.Session).WithUniqueId(PersonTemplateId)
                                                   .WithName("Person Derivation")
                                                   .WithBody(@"main(this) ::= <<
Hello $this.UserName$!
>>")
                                                   .Build();
        }
    }
}
