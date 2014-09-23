// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Persons.cs" company="Allors bvba">
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
    
    using Allors.Domain;

    public partial class Persons
    {
        public static void AppsDeriveCommissions(ISession session)
        {
            foreach (Person person in session.Extent<Person>())
            {
                if (person.ExistSalesRepRevenuesWhereSalesRep)
                {
                    person.DeriveCommission();
                }
            }
        }

        protected override void AppsSecure(Security config)
        {
            base.AppsSecure(config);

            config.GrantCustomer(this.ObjectType, Meta.BirthDate, Operation.Read, Operation.Write);
            config.GrantCustomer(this.ObjectType, Meta.Citizenship, Operation.Read, Operation.Write);
            config.GrantCustomer(this.ObjectType, Meta.FirstName, Operation.Read, Operation.Write);
            config.GrantCustomer(this.ObjectType, Meta.Gender, Operation.Read, Operation.Write);
            config.GrantCustomer(this.ObjectType, Meta.LastName, Operation.Read, Operation.Write);
            config.GrantCustomer(this.ObjectType, Meta.MaritalStatus, Operation.Read, Operation.Write);
            config.GrantCustomer(this.ObjectType, Meta.MiddleName, Operation.Read, Operation.Write);
            config.GrantCustomer(this.ObjectType, Meta.MothersMaidenName, Operation.Read, Operation.Write);
            config.GrantCustomer(this.ObjectType, Meta.PartyContactMechanism, Operation.Read, Operation.Write);
            config.GrantCustomer(this.ObjectType, Meta.Passport, Operation.Read, Operation.Write);
            config.GrantCustomer(this.ObjectType, Meta.Picture, Operation.Read, Operation.Write);
            config.GrantCustomer(this.ObjectType, Meta.PreferredCurrency, Operation.Read, Operation.Write);
            config.GrantCustomer(this.ObjectType, Meta.Locale, Operation.Read, Operation.Write);
            config.GrantCustomer(this.ObjectType, Meta.Title, Operation.Read, Operation.Write);
            config.GrantCustomer(this.ObjectType, Meta.Salutation, Operation.Read, Operation.Write);
            config.GrantCustomer(this.ObjectType, Meta.SocialSecurityNumber, Operation.Read, Operation.Write);
            config.GrantCustomer(this.ObjectType, Meta.BankAccount, Operation.Read, Operation.Write);

            config.GrantSales(this.ObjectType, Meta.BirthDate, Operation.Read, Operation.Write);
            config.GrantSales(this.ObjectType, Meta.Citizenship, Operation.Read, Operation.Write);
            config.GrantSales(this.ObjectType, Meta.FirstName, Operation.Read, Operation.Write);
            config.GrantSales(this.ObjectType, Meta.Gender, Operation.Read, Operation.Write);
            config.GrantSales(this.ObjectType, Meta.LastName, Operation.Read, Operation.Write);
            config.GrantSales(this.ObjectType, Meta.MaritalStatus, Operation.Read, Operation.Write);
            config.GrantSales(this.ObjectType, Meta.MiddleName, Operation.Read, Operation.Write);
            config.GrantSales(this.ObjectType, Meta.MothersMaidenName, Operation.Read, Operation.Write);
            config.GrantSales(this.ObjectType, Meta.PartyContactMechanism, Operation.Read, Operation.Write);
            config.GrantSales(this.ObjectType, Meta.Passport, Operation.Read, Operation.Write);
            config.GrantSales(this.ObjectType, Meta.Picture, Operation.Read, Operation.Write);
            config.GrantSales(this.ObjectType, Meta.PreferredCurrency, Operation.Read, Operation.Write);
            config.GrantSales(this.ObjectType, Meta.Locale, Operation.Read, Operation.Write);
            config.GrantSales(this.ObjectType, Meta.Title, Operation.Read, Operation.Write);
            config.GrantSales(this.ObjectType, Meta.Salutation, Operation.Read, Operation.Write);
            config.GrantSales(this.ObjectType, Meta.SocialSecurityNumber, Operation.Read, Operation.Write);
            config.GrantSales(this.ObjectType, Meta.BankAccount, Operation.Read, Operation.Write);

            config.GrantSupplier(this.ObjectType, Meta.BirthDate, Operation.Read, Operation.Write);
            config.GrantSupplier(this.ObjectType, Meta.Citizenship, Operation.Read, Operation.Write);
            config.GrantSupplier(this.ObjectType, Meta.FirstName, Operation.Read, Operation.Write);
            config.GrantSupplier(this.ObjectType, Meta.Gender, Operation.Read, Operation.Write);
            config.GrantSupplier(this.ObjectType, Meta.LastName, Operation.Read, Operation.Write);
            config.GrantSupplier(this.ObjectType, Meta.MaritalStatus, Operation.Read, Operation.Write);
            config.GrantSupplier(this.ObjectType, Meta.MiddleName, Operation.Read, Operation.Write);
            config.GrantSupplier(this.ObjectType, Meta.MothersMaidenName, Operation.Read, Operation.Write);
            config.GrantSupplier(this.ObjectType, Meta.PartyContactMechanism, Operation.Read, Operation.Write);
            config.GrantSupplier(this.ObjectType, Meta.Passport, Operation.Read, Operation.Write);
            config.GrantSupplier(this.ObjectType, Meta.Picture, Operation.Read, Operation.Write);
            config.GrantSupplier(this.ObjectType, Meta.PreferredCurrency, Operation.Read, Operation.Write);
            config.GrantSupplier(this.ObjectType, Meta.Locale, Operation.Read, Operation.Write);
            config.GrantSupplier(this.ObjectType, Meta.Title, Operation.Read, Operation.Write);
            config.GrantSupplier(this.ObjectType, Meta.Salutation, Operation.Read, Operation.Write);
            config.GrantSupplier(this.ObjectType, Meta.SocialSecurityNumber, Operation.Read, Operation.Write);
            config.GrantSupplier(this.ObjectType, Meta.BankAccount, Operation.Read, Operation.Write);

            config.GrantPartner(this.ObjectType, Meta.BirthDate, Operation.Read, Operation.Write);
            config.GrantPartner(this.ObjectType, Meta.Citizenship, Operation.Read, Operation.Write);
            config.GrantPartner(this.ObjectType, Meta.FirstName, Operation.Read, Operation.Write);
            config.GrantPartner(this.ObjectType, Meta.Gender, Operation.Read, Operation.Write);
            config.GrantPartner(this.ObjectType, Meta.LastName, Operation.Read, Operation.Write);
            config.GrantPartner(this.ObjectType, Meta.MaritalStatus, Operation.Read, Operation.Write);
            config.GrantPartner(this.ObjectType, Meta.MiddleName, Operation.Read, Operation.Write);
            config.GrantPartner(this.ObjectType, Meta.MothersMaidenName, Operation.Read, Operation.Write);
            config.GrantPartner(this.ObjectType, Meta.PartyContactMechanism, Operation.Read, Operation.Write);
            config.GrantPartner(this.ObjectType, Meta.Passport, Operation.Read, Operation.Write);
            config.GrantPartner(this.ObjectType, Meta.Picture, Operation.Read, Operation.Write);
            config.GrantPartner(this.ObjectType, Meta.PreferredCurrency, Operation.Read, Operation.Write);
            config.GrantPartner(this.ObjectType, Meta.Locale, Operation.Read, Operation.Write);
            config.GrantPartner(this.ObjectType, Meta.Title, Operation.Read, Operation.Write);
            config.GrantPartner(this.ObjectType, Meta.Salutation, Operation.Read, Operation.Write);
            config.GrantPartner(this.ObjectType, Meta.SocialSecurityNumber, Operation.Read, Operation.Write);
            config.GrantPartner(this.ObjectType, Meta.BankAccount, Operation.Read, Operation.Write);
        }
    }
}