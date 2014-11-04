//------------------------------------------------------------------------------------------------- 
// <copyright file="Repository.cs" company="Allors bvba">
// Copyright 2002-2013 Allors bvba.
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
//-------------------------------------------------------------------------------------------------

namespace Allors.Meta
{
    using System;

    public static partial class Repository
    {
        public static Domain Test(MetaPopulation env)
        {
            // Imports
            var core = (Domain)env.Find(new Guid("CA802192-8186-4C2A-8315-A8DEFAA74A12"));

            var allorsString = (Unit)env.Find(UnitIds.StringId);
            var allorsInteger = (Unit)env.Find(UnitIds.IntegerId);
            var allorsDecimal = (Unit)env.Find(UnitIds.DecimalId);
            var allorsDouble = (Unit)env.Find(UnitIds.FloatId);
            var allorsBoolean = (Unit)env.Find(UnitIds.BooleanId);
            var allorsUnique = (Unit)env.Find(UnitIds.Unique);
            var allorsBinary = (Unit)env.Find(UnitIds.BinaryId);

            // Domain
            var domain = new Domain(env, new Guid("FEEA74C4-D5B6-44DA-BD0E-D4864CC88B88")) { Name = "Test" };
            domain.AddDirectSuperdomain(core);

            // Objects
            var c1 = new ClassBuilder(domain, new Guid("7041c691-d896-4628-8f50-1c24f5d03414")).WithSingularName("C1").WithPluralName("C1s").Build();
            var c2 = new ClassBuilder(domain, new Guid("72c07e8a-03f5-4da8-ab37-236333d4f74e")).WithSingularName("C2").WithPluralName("C2s").Build();
            var i1 = new InterfaceBuilder(domain, new Guid("fefcf1b6-ac8f-47b0-bed5-939207a2833e")).WithSingularName("I1").WithPluralName("I1s").Build();
            var i2 = new InterfaceBuilder(domain, new Guid("19bb2bc3-d53a-4d15-86d0-b250fdbcb0a0")).WithSingularName("I2").WithPluralName("I2s").Build();
            var i12 = new InterfaceBuilder(domain, new Guid("97755724-b934-4cc5-beb4-3d49a7a4b27e")).WithSingularName("I12").WithPluralName("I12s").Build();

            // C1
            new InheritanceBuilder(domain, new Guid("6B3E698A-5F31-44BC-8864-92D1EC7EC898")).WithSubtype(c1).WithSupertype(i1).Build();

            // C2
            new InheritanceBuilder(domain, new Guid("328FF789-D96D-43F1-A7BA-8488A41D7B9B")).WithSubtype(c2).WithSupertype(i1).Build();

            // I1
            new InheritanceBuilder(domain, new Guid("DEBE8459-3C55-4905-9ADF-396F519943D0")).WithSubtype(i1).WithSupertype(i12).Build();

            // I2
            new InheritanceBuilder(domain, new Guid("10d59b2a-9b59-43e3-a63d-2a5dda8f2921")).WithSubtype(i2).WithSupertype(i12).Build();

            // RelationTypes
            // I12
            new RelationTypeBuilder(domain, new Guid("35040d7c-ab7f-4a99-9d09-e01e24ca3cb9"), new Guid("3aa841fd-a95d-4ddc-b994-5e432fd9f2ef"), new Guid("c39a79f1-3b54-45bb-ad24-3cec889691fc")).WithObjectTypes(i12, allorsBoolean).Build();
            new RelationTypeBuilder(domain, new Guid("4f095abd-8803-4610-87f0-2847ddd5e9f4"), new Guid("e1a86fa0-c857-4be0-8abc-704339bbdc82"), new Guid("c7cb9a8b-7df5-4677-902f-b6f4b9aec802")).WithObjectTypes(i12, allorsDecimal).Build();
            new RelationTypeBuilder(domain, new Guid("9f91841c-f63f-4ffa-bee6-62e100f3cd15"), new Guid("8841f638-0522-46b6-a6cf-797548264f0d"), new Guid("15ba5c39-5269-4f61-b595-7b8b6fcefe9a")).WithObjectTypes(i12, allorsString).Build();
            new RelationTypeBuilder(domain, new Guid("d30dd036-6d28-48df-873b-3a76da8c029e"), new Guid("ee50ff17-39d8-44f7-8d14-e63f4c822ed4"), new Guid("25cb17ec-01e2-4658-a06b-2a620f152923")).WithObjectTypes(i12, allorsInteger).Build();
            new RelationTypeBuilder(domain, new Guid("fbad33e7-ede1-41fc-97e9-ddf33a0f6459"), new Guid("a9f79b82-bb7c-4cdc-ac16-333a1b994387"), new Guid("81acc49f-16c9-4677-80f4-c3e768a7b9e3")).WithObjectTypes(i12, allorsDouble).Build();
            
            return domain;
        }
    }
}