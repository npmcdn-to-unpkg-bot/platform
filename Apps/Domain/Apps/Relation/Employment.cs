// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Employment.cs" company="Allors bvba">
//   Copyright 2002-2012 Allors bvba.
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

    using Allors.Domain;

    using Resources;

    public partial class Employment
    {
        protected override void AppsOnPostBuild(IObjectBuilder builder)
        {
            base.AppsOnPostBuild(builder);

            if (!this.ExistFromDate)
            {
                this.FromDate = DateTime.Now;
            }
        }

        protected override void AppsDerive(IDerivation derivation)
        {
            base.AppsDerive(derivation);

            derivation.Log.AssertExists(this, Employments.Meta.Employer);
            derivation.Log.AssertExists(this, Employments.Meta.Employee);
            derivation.Log.AssertExists(this, Employments.Meta.FromDate);

            if (this.ExistEmployee && this.ExistEmployer)
            {
                var employments = this.Employee.EmploymentsWhereEmployee;
                employments.Filter.AddNot().AddExists(Employments.Meta.ThroughDate);

                if (employments.Count > 1)
                {
                    derivation.Log.AddError(this, Employments.Meta.FromDate, ErrorMessages.ActiveDeploymentRegistered, this.Employer.DeriveDisplayName());
                }
            }

            this.DisplayName = string.Format(
                "{0} employee at {1}",
                this.ExistEmployee ? this.Employee.DeriveDisplayName() : null,
                this.ExistEmployer ? this.Employer.DeriveDisplayName() : null);

            if (this.ExistEmployee)
            {
                this.Employee.Derive(derivation);
            }

            if (this.ExistEmployer)
            {
                this.Employer.Derive(derivation);
            }

            if (this.ExistEmployee && this.Employee.ExistSalesRepRelationshipsWhereSalesRepresentative)
            {
                foreach (SalesRepRelationship salesRepRelationship in this.Employee.SalesRepRelationshipsWhereSalesRepresentative)
                {
                    salesRepRelationship.Derive(derivation);
                }
            }
        }
    }
}