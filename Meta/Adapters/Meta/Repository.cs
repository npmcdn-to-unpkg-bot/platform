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

    using Allors.Meta.Builders;

    public static partial class Repository
    {
        public static Domain Adapters(MetaPopulation env)
        {
            // Imports
            var core = (Domain)env.Find(new Guid("CA802192-8186-4C2A-8315-A8DEFAA74A12"));

            var AllorsString = (Unit)env.Find(UnitIds.StringId);
            var AllorsInteger = (Unit)env.Find(UnitIds.IntegerId);
            var AllorsLong = (Unit)env.Find(UnitIds.LongId);
            var AllorsDecimal = (Unit)env.Find(UnitIds.DecimalId);
            var AllorsDouble = (Unit)env.Find(UnitIds.DoubleId);
            var AllorsBoolean = (Unit)env.Find(UnitIds.BooleanId);
            var AllorsDateTime = (Unit)env.Find(UnitIds.DatetimeId);
            var AllorsUnique = (Unit)env.Find(UnitIds.Unique);
            var AllorsBinary = (Unit)env.Find(UnitIds.BinaryId);

            // Objects
            var domain = new Domain(env, new Guid("FEEA74C4-D5B6-44DA-BD0E-D4864CC88B88")) { Name = "Adapters" };
            domain.AddDirectSuperdomain(core);

            var C1 = new ClassBuilder(domain, new Guid("7041c691-d896-4628-8f50-1c24f5d03414")).WithSingularName("C1").WithPluralName("C1s").Build();
            var C2 = new ClassBuilder(domain, new Guid("72c07e8a-03f5-4da8-ab37-236333d4f74e")).WithSingularName("C2").WithPluralName("C2s").Build();
            var C3 = new ClassBuilder(domain, new Guid("2a9b5a77-6065-4f2a-bbc3-655426f0f97b")).WithSingularName("C3").WithPluralName("C3s").Build();
            var C4 = new ClassBuilder(domain, new Guid("20049a79-20c7-478b-a5ba-c54b1e615168")).WithSingularName("C4").WithPluralName("C4s").Build();
            var I1 = new InterfaceBuilder(domain, new Guid("fefcf1b6-ac8f-47b0-bed5-939207a2833e")).WithSingularName("I1").WithPluralName("I1s").Build();
            var I2 = new InterfaceBuilder(domain, new Guid("19bb2bc3-d53a-4d15-86d0-b250fdbcb0a0")).WithSingularName("I2").WithPluralName("I2s").Build();
            var I3 = new InterfaceBuilder(domain, new Guid("2d86277f-3993-4831-a7de-3640166d3d50")).WithSingularName("I3").WithPluralName("I3s").Build();
            var I4 = new InterfaceBuilder(domain, new Guid("7a49be0e-cb91-4e1e-b113-ac67ec969935")).WithSingularName("I4").WithPluralName("I4s").Build();
            var I12 = new InterfaceBuilder(domain, new Guid("97755724-b934-4cc5-beb4-3d49a7a4b27e")).WithSingularName("I12").WithPluralName("I12s").Build();
            var I23 = new InterfaceBuilder(domain, new Guid("29cb9717-2452-4da0-9a29-8bd5d815307a")).WithSingularName("I23").WithPluralName("I23s").Build();
            var I34 = new InterfaceBuilder(domain, new Guid("ebc22540-54c8-4601-a43d-2ed6da9f3e79")).WithSingularName("I34").WithPluralName("I34s").Build();
            var S1 = new InterfaceBuilder(domain, new Guid("15c3bb71-075d-48ad-8a00-250c2f627092")).WithSingularName("S1").WithPluralName("S1s").Build();
            var S2 = new InterfaceBuilder(domain, new Guid("feeb7027-7c6c-4cb5-8718-93e6e8a4afd8")).WithSingularName("S2").WithPluralName("S2s").Build();
            var S3 = new InterfaceBuilder(domain, new Guid("5b24107d-f5e8-499b-94f7-2bf712493546")).WithSingularName("S3").WithPluralName("S3s").Build();
            var S4 = new InterfaceBuilder(domain, new Guid("5b348bcb-823d-4cbe-b3ac-a18b1cd96581")).WithSingularName("S4").WithPluralName("S4s").Build();
            var S12 = new InterfaceBuilder(domain, new Guid("c5747a64-f468-4d0d-80f3-6463bd32b0ca")).WithSingularName("S12").WithPluralName("S12s").Build();
            var S1234 = new InterfaceBuilder(domain, new Guid("c3c0ecf3-9f8d-4701-854f-8ddea1bd69fd")).WithSingularName("S1234").WithPluralName("S1234s").Build();

            var InterfaceWithoutConcreteClass = new InterfaceBuilder(domain, new Guid("2f4bc713-47c9-4e07-9f2b-1d22a0cb4fad")).WithSingularName("InterfaceWithoutConcreteClass").WithPluralName("InterfacesWithoutConcreteClass").Build();
            var ClassWithoutUnitRoles = new ClassBuilder(domain, new Guid("071d291d-fcc6-4511-8aa2-2d30fdeede8f")).WithSingularName("ClassWithoutUnitRoles").WithPluralName("ClassesWithoutUnitRoles").Build();
            var ClassWithoutRoles = new ClassBuilder(domain, new Guid("e1008840-6d7c-4d44-b2ad-1545d23f90d8")).WithSingularName("ClassWithoutRoles").WithPluralName("ClassesWithoutRoles").Build();
            var SingleUnit = new ClassBuilder(domain, new Guid("c3e82ab0-f586-4913-acb0-838ffd6701f8")).WithSingularName("SingleUnit").WithPluralName("SingleUnits").Build();

            var LT32 = new ClassBuilder(domain, new Guid("67c8d19f-1947-487c-8884-dbd76033aab0")).WithSingularName("LT32").WithPluralName("LT32s").Build();
            var LT32UnitGT32Composite = new ClassBuilder(domain, new Guid("15ea889f-21d6-4682-aca2-c2987f592f0e")).WithSingularName("LT32UnitGT32Composite").WithPluralName("LT32UnitGT32Composites").Build();
            var ILT32Composite = new InterfaceBuilder(domain, new Guid("4f53e1e7-e88a-4161-969c-1fed0b3a24a2")).WithSingularName("ILT32Composite").WithPluralName("ILT32Composites").Build();
            var ILT32Unit = new InterfaceBuilder(domain, new Guid("228fa79f-afa7-418c-968e-8c0d38fb3ad2")).WithSingularName("ILT32Unit").WithPluralName("ILT32Units").Build();
            var GT32 = new ClassBuilder(domain, new Guid("4f6301b3-6f0a-40c2-8267-4f8631bae706")).WithSingularName("GT32").WithPluralName("GT32s").Build();
            var GT32UnitLT32Composite = new ClassBuilder(domain, new Guid("7683eb7f-cbac-4947-ac29-4ef15ae47597")).WithSingularName("GT32UnitLT32Composite").WithPluralName("GT32UnitLT32Composites").Build();
            var IGT32Unit = new InterfaceBuilder(domain, new Guid("584681af-90f0-45b1-a80e-6a73c3592600")).WithSingularName("IGT32Unit").WithPluralName("IGT32Units").Build();
            var IGT32Composite = new InterfaceBuilder(domain, new Guid("ee84609f-e165-4037-b8ce-f7c8b826e603")).WithSingularName("IGT32Composite").WithPluralName("IGT32Composites").Build();

            var Sandbox = new ClassBuilder(domain, new Guid("73970b0f-1ff4-4d39-aad8-fdbfbaae472f")).WithSingularName("Sandbox").WithPluralName("Sandboxes").Build();
            var ISandbox = new InterfaceBuilder(domain, new Guid("7ba2ab26-491b-49eb-944c-26f6bb66e50f")).WithSingularName("ISandbox").WithPluralName("ISandboxes").Build();

            var Named = new InterfaceBuilder(domain, new Guid("fcaa52e3-4a90-4981-b45d-d158e2589506")).WithSingularName("Named").WithPluralName("Nameds").Build();
            var User = new ClassBuilder(domain, new Guid("0d6bc154-112b-4a58-aa96-3b2a96f82523")).WithSingularName("User").WithPluralName("Users").Build();
            var Person = new ClassBuilder(domain, new Guid("6a082a25-a8f2-4acd-a1a3-ba4461b729f1")).WithSingularName("Person").WithPluralName("Persons").Build();
            var Company = new ClassBuilder(domain, new Guid("b1b6361e-5ee5-434c-9c92-46c6166195c4")).WithSingularName("Company").WithPluralName("Companies").Build();

            // Inheritances
            // S1
            new InheritanceBuilder(domain, new Guid("c54b5dd3-1228-4ad5-9dda-8827f2d93df0")).WithSubtype(S1).WithSupertype(S1234).Build();

            // I2
            new InheritanceBuilder(domain, new Guid("10d59b2a-9b59-43e3-a63d-2a5dda8f2921")).WithSubtype(I2).WithSupertype(S1234).Build();
            new InheritanceBuilder(domain, new Guid("c53f1f14-2a22-4b6d-8e3c-3c50c07e5299")).WithSubtype(I2).WithSupertype(S2).Build();

            // C4
            new InheritanceBuilder(domain, new Guid("51509eb9-02f4-494b-ab30-04acd4286b42")).WithSubtype(C4).WithSupertype(I4).Build();
            new InheritanceBuilder(domain, new Guid("782b5d64-91f9-4e27-ad92-6a1f0a8c7e07")).WithSubtype(C4).WithSupertype(I34).Build();

            // C3
            new InheritanceBuilder(domain, new Guid("44af67b9-26ae-43c1-ba80-3e8298e4bed8")).WithSubtype(C3).WithSupertype(I3).Build();
            new InheritanceBuilder(domain, new Guid("71862815-beba-495c-9d30-47b5d248da34")).WithSubtype(C3).WithSupertype(I23).Build();
            new InheritanceBuilder(domain, new Guid("f90bfd4b-d610-46ab-a02d-5f4f2590bbcb")).WithSubtype(C3).WithSupertype(I34).Build();

            // I3
            new InheritanceBuilder(domain, new Guid("614c5332-4836-44fe-80c0-3196fcdbad51")).WithSubtype(I3).WithSupertype(S1234).Build();
            new InheritanceBuilder(domain, new Guid("c78ab091-6fd4-4ca1-a245-525f84bff02b")).WithSubtype(I3).WithSupertype(S3).Build();

            // Person
            new InheritanceBuilder(domain, new Guid("3fea8f15-6a9e-425b-9064-3efd9b7b809a")).WithSubtype(Person).WithSupertype(Named).Build();

            // C1
            new InheritanceBuilder(domain, new Guid("7ba9043d-2307-4d81-b56d-b1ddbd3070e4")).WithSubtype(C1).WithSupertype(I1).Build();
            new InheritanceBuilder(domain, new Guid("941aa988-3890-420d-bd6a-515f2bc3a7f8")).WithSubtype(C1).WithSupertype(I12).Build();

            // C2
            new InheritanceBuilder(domain, new Guid("1fa71df5-71a2-49f9-98eb-813f3992c4d5")).WithSubtype(C2).WithSupertype(I2).Build();
            new InheritanceBuilder(domain, new Guid("8007d524-40f1-4a2d-9299-6fd8d53692c1")).WithSubtype(C2).WithSupertype(I23).Build();
            new InheritanceBuilder(domain, new Guid("ad06478b-1756-46a4-a0a2-14f0f237eb4f")).WithSubtype(C2).WithSupertype(I12).Build();

            // I4
            new InheritanceBuilder(domain, new Guid("2dea90ce-fa75-45db-bba2-4713fafd061c")).WithSubtype(I4).WithSupertype(S4).Build();
            new InheritanceBuilder(domain, new Guid("ecd3bb16-7221-440b-8ab5-01880fdd4dee")).WithSubtype(I4).WithSupertype(S1234).Build();

            // I12
            new InheritanceBuilder(domain, new Guid("cef6e9fb-c5a9-473a-a25e-23bc60115012")).WithSubtype(I12).WithSupertype(S12).Build();

            // Company
            new InheritanceBuilder(domain, new Guid("a7b5f53e-1e73-48ff-a313-16c9ebc0609d")).WithSubtype(Company).WithSupertype(Named).Build();

            // I1
            new InheritanceBuilder(domain, new Guid("280e235b-f17a-4505-917e-e51647ca6928")).WithSubtype(I1).WithSupertype(S1).Build();
            new InheritanceBuilder(domain, new Guid("a1c0ac5b-cffa-4a5d-8812-9383a3e1cadc")).WithSubtype(I1).WithSupertype(S1234).Build();



            // RelationTypes

            // User
            new RelationTypeBuilder(domain, new Guid("1ffa3cb7-41f0-406a-a3a5-2f3a4c5ad59c")).WithObjectTypes(User, User).WithSingularName("Select").WithPluralName("Selects").WithCardinality(Cardinalities.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("bc6b71a8-2a66-4b57-9c86-ecf521b973ba")).WithObjectTypes(User, AllorsString).WithSingularName("From").WithPluralName("Froms").WithSize(256).Build();

            // S1
            new RelationTypeBuilder(domain, new Guid("294e7ce3-1b0b-490a-a5e8-6149885d4943")).WithObjectTypes(S1, AllorsDecimal).WithScale(2).WithPrecision(19).Build();
            new RelationTypeBuilder(domain, new Guid("4cd28d56-ffd6-461c-b9ed-ca0e4bae51df")).WithObjectTypes(S1, AllorsInteger).Build();
            new RelationTypeBuilder(domain, new Guid("55ab6cfa-651b-48ec-bc33-ad3a381d2260")).WithObjectTypes(S1, AllorsBinary).WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("645c20ac-5b4f-40db-8d11-d2b07123dabe")).WithObjectTypes(S1, AllorsUnique).Build();
            new RelationTypeBuilder(domain, new Guid("678b14c4-b5ae-48e3-ac06-2459cab66c34")).WithObjectTypes(S1, AllorsString).WithSingularName("StringLarge").WithPluralName("StringsLarge").WithSize(100000).Build();
            new RelationTypeBuilder(domain, new Guid("6a166388-5bca-4cd9-bfee-0da27cbc3073")).WithObjectTypes(S1, S2).WithSingularName("S2many2one").WithPluralName("S2many2ones").WithCardinality(Cardinalities.ManyToOne).Build();
            new RelationTypeBuilder(domain, new Guid("6ee98698-15dc-4998-88c3-d2a4d1c19e8c")).WithObjectTypes(S1, S2).WithSingularName("S2one2many").WithPluralName("S2one2manies").WithCardinality(Cardinalities.OneToMany).Build();
            new RelationTypeBuilder(domain, new Guid("701ca57d-241f-470c-b690-9045c0f76c8f")).WithObjectTypes(S1, AllorsDouble).Build();
            new RelationTypeBuilder(domain, new Guid("70815e0c-11d4-41ac-b0b2-105f8ede6d27")).WithObjectTypes(S1, AllorsString).WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("77afee4a-08b7-4231-aa73-575145efd1e3")).WithObjectTypes(S1, C1).WithSingularName("C1many2one").WithPluralName("C1many2ones").WithCardinality(Cardinalities.ManyToOne).Build();
            new RelationTypeBuilder(domain, new Guid("8f5485ba-5a82-4d01-809e-52b467f958d8")).WithObjectTypes(S1, C1).WithSingularName("C1one2one").WithPluralName("C1one2ones").Build();
            new RelationTypeBuilder(domain, new Guid("9fbcf7ce-3b59-458d-ab5e-9c48dd3842b3")).WithObjectTypes(S1, AllorsBoolean).Build();
            new RelationTypeBuilder(domain, new Guid("c0cfe3ee-d184-40bd-8354-b0b0bd4e641c")).WithObjectTypes(S1, C1).WithSingularName("C1many2many").WithPluralName("C1many2manies").WithCardinality(Cardinalities.ManyToMany).Build();
            new RelationTypeBuilder(domain, new Guid("c6f49460-a259-44de-b674-4d0585fe00cd")).WithObjectTypes(S1, S2).WithSingularName("S2many2many").WithPluralName("S2many2manies").WithCardinality(Cardinalities.ManyToMany).Build();
            new RelationTypeBuilder(domain, new Guid("dc22175f-185d-4cd3-b492-74b0a9389c91")).WithObjectTypes(S1, S2).WithSingularName("S2one2one").WithPluralName("S2one2ones").Build();
            new RelationTypeBuilder(domain, new Guid("de02a563-2c77-4936-ab07-d322b8dcaec9")).WithObjectTypes(S1, AllorsLong).Build();
            new RelationTypeBuilder(domain, new Guid("e263ac2b-822d-4aa4-8a8c-67db3f2b4bb0")).WithObjectTypes(S1, AllorsDateTime).Build();
            new RelationTypeBuilder(domain, new Guid("ef918b82-87f4-4591-bf19-2fd5a1019ece")).WithObjectTypes(S1, C1).WithSingularName("C1one2many").WithPluralName("C1one2manies").WithCardinality(Cardinalities.OneToMany).Build();

            // I2
            new RelationTypeBuilder(domain, new Guid("35040d7c-ab7f-4a99-9d09-e01e24ca3cb9")).WithObjectTypes(I2, AllorsBoolean).Build();
            new RelationTypeBuilder(domain, new Guid("4f095abd-8803-4610-87f0-2847ddd5e9f4")).WithObjectTypes(I2, AllorsDecimal).WithScale(2).WithPrecision(19).Build();
            new RelationTypeBuilder(domain, new Guid("81d9eb2f-55a7-4d1c-853d-4369eb691ba5")).WithObjectTypes(I2, AllorsDateTime).Build();
            new RelationTypeBuilder(domain, new Guid("9f91841c-f63f-4ffa-bee6-62e100f3cd15")).WithObjectTypes(I2, AllorsString).WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("d0c49b92-a108-48b5-bc95-72d2e6109ad2")).WithObjectTypes(I2, AllorsLong).Build();
            new RelationTypeBuilder(domain, new Guid("d30dd036-6d28-48df-873b-3a76da8c029e")).WithObjectTypes(I2, AllorsInteger).Build();
            new RelationTypeBuilder(domain, new Guid("fbad33e7-ede1-41fc-97e9-ddf33a0f6459")).WithObjectTypes(I2, AllorsDouble).Build();

            // C4
            new RelationTypeBuilder(domain, new Guid("9f24fc51-8568-4ffc-b47a-c5c317d00954")).WithObjectTypes(C4, AllorsString).WithSize(256).Build();

            // ILT32Unit
            new RelationTypeBuilder(domain, new Guid("6822f677-7249-4c28-9b9c-18b21ba6f597")).WithObjectTypes(ILT32Unit, AllorsString).WithSingularName("AllorsString1").WithPluralName("AllorsStrings1").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("b2734796-7140-4830-a0de-88df7d27b6a8")).WithObjectTypes(ILT32Unit, AllorsString).WithSingularName("AllorsString3").WithPluralName("AllorsStrings3").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("ced16c48-6301-4652-8dcb-ed8a80ea7ce4")).WithObjectTypes(ILT32Unit, AllorsString).WithSingularName("AllorsString2").WithPluralName("AllorsStrings2").WithSize(256).Build();

            // I23
            new RelationTypeBuilder(domain, new Guid("0407c93e-f2ea-49e4-8779-44b42c554e60")).WithObjectTypes(I23, AllorsString).WithSize(256).Build();

            // C3
            new RelationTypeBuilder(domain, new Guid("02a07b71-a40d-4600-ae12-370be7e973f5")).WithObjectTypes(C3, AllorsString).WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("0e06c403-2a29-4f40-b7b6-3e4fed28aeba")).WithObjectTypes(C3, C2).WithSingularName("C2many2many").WithPluralName("C2many2manies").WithCardinality(Cardinalities.ManyToMany).Build();
            new RelationTypeBuilder(domain, new Guid("29e76785-f3eb-48b9-a9bf-c44e64762631")).WithObjectTypes(C3, I4).WithSingularName("I4one2one").WithPluralName("I4one2ones").Build();
            new RelationTypeBuilder(domain, new Guid("39313684-8ea1-4f15-aada-2a16feb148ea")).WithObjectTypes(C3, C4).WithSingularName("C4many2one").WithPluralName("C4many2ones").WithCardinality(Cardinalities.ManyToOne).Build();
            new RelationTypeBuilder(domain, new Guid("5e6c2802-3dc5-405a-a2f7-03c9361d4562")).WithObjectTypes(C3, C4).WithSingularName("C4many2many").WithPluralName("C4many2manies").WithCardinality(Cardinalities.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("8f2225b7-8c15-414a-a9be-50c757f80b3e")).WithObjectTypes(C3, I4).WithSingularName("I4many2many").WithPluralName("I4many2manies").WithCardinality(Cardinalities.ManyToMany).Build();
            new RelationTypeBuilder(domain, new Guid("92505f70-3611-4ed6-bd27-71030299e176")).WithObjectTypes(C3, C2).WithSingularName("C2one2many").WithPluralName("C2one2manies").WithCardinality(Cardinalities.OneToMany).Build();
            new RelationTypeBuilder(domain, new Guid("958bc7c6-d609-4407-ba92-50726c9af5d5")).WithObjectTypes(C3, C2).WithSingularName("C2many2one").WithPluralName("C2many2ones").WithCardinality(Cardinalities.ManyToOne).Build();
            new RelationTypeBuilder(domain, new Guid("b7745909-a63a-448a-b4bd-6caf614c4b12")).WithObjectTypes(C3, I4).WithSingularName("I4many2one").WithPluralName("I4many2ones").WithCardinality(Cardinalities.ManyToOne).Build();
            new RelationTypeBuilder(domain, new Guid("d1601926-ae62-4592-b15b-6511e0d98355")).WithObjectTypes(C3, C4).WithSingularName("C4one2many").WithPluralName("C4one2manies").WithCardinality(Cardinalities.OneToMany).Build();
            new RelationTypeBuilder(domain, new Guid("d81da318-f954-42b4-b605-e011a92726ba")).WithObjectTypes(C3, C2).WithSingularName("C2one2one").WithPluralName("C2one2ones").Build();
            new RelationTypeBuilder(domain, new Guid("da44bf79-b72e-4565-bd33-0eb278a6f4ec")).WithObjectTypes(C3, C4).WithSingularName("C4one2one").WithPluralName("C4one2ones").Build();
            new RelationTypeBuilder(domain, new Guid("dd006700-a00c-4c67-819e-1d63df26a5b6")).WithObjectTypes(C3, AllorsString).WithSingularName("StringEquals").WithPluralName("StringsEquals").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("ed3267fb-fbc4-4e38-87f5-8e2ee91b1bac")).WithObjectTypes(C3, I4).WithSingularName("I4one2many").WithPluralName("I4one2manies").WithCardinality(Cardinalities.OneToMany).Build();

            // I3
            new RelationTypeBuilder(domain, new Guid("00b706bb-681e-44ce-bbf3-c3b01bb11269")).WithObjectTypes(I3, C4).WithSingularName("C4many2many").WithPluralName("C4many2manies").WithCardinality(Cardinalities.ManyToMany).Build();
            new RelationTypeBuilder(domain, new Guid("25a3bcbf-cd9a-4735-879d-c5415b19cf88")).WithObjectTypes(I3, AllorsString).WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("2b273c39-cc85-4585-806f-d991f43dda29")).WithObjectTypes(I3, I4).WithSingularName("I4one2many").WithPluralName("I4one2manies").WithCardinality(Cardinalities.OneToMany).Build();
            new RelationTypeBuilder(domain, new Guid("3a55d57f-768f-4c11-9c18-baa5f3eeda8c")).WithObjectTypes(I3, C4).WithSingularName("C4one2many").WithPluralName("C4one2manies").WithCardinality(Cardinalities.OneToMany).Build();
            new RelationTypeBuilder(domain, new Guid("3f553db3-b490-4de5-b388-5d096d83de0d")).WithObjectTypes(I3, I4).WithSingularName("I4many2many").WithPluralName("I4many2manies").WithCardinality(Cardinalities.ManyToMany).Build();
            new RelationTypeBuilder(domain, new Guid("57f8f305-e1a9-452b-bcc1-febf7ccc346a")).WithObjectTypes(I3, I4).WithSingularName("I4many2one").WithPluralName("I4many2one").WithCardinality(Cardinalities.ManyToOne).Build();
            new RelationTypeBuilder(domain, new Guid("cc48853e-46f3-4292-be9b-8a4937cea308")).WithObjectTypes(I3, C4).WithSingularName("C4one2one").WithPluralName("C4one2ones").Build();
            new RelationTypeBuilder(domain, new Guid("d36e7cf1-08d1-4333-b539-e50503c10934")).WithObjectTypes(I3, I4).WithSingularName("I4one2one").WithPluralName("I4one2one").Build();
            new RelationTypeBuilder(domain, new Guid("d5ff5333-6bbc-4bb5-8208-44e1d4b53aee")).WithObjectTypes(I3, C4).WithSingularName("C4many2one").WithPluralName("C4many2ones").WithCardinality(Cardinalities.ManyToOne).Build();
            new RelationTypeBuilder(domain, new Guid("e0cf6092-d865-4386-823b-a2906a3eab1a")).WithObjectTypes(I3, AllorsString).WithSingularName("StringEquals").WithPluralName("StringsEquals").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("fb90c539-a392-4618-bb0b-9809a3a673aa")).WithObjectTypes(I3, C1).WithSingularName("C1one2one").WithPluralName("C1one2ones").Build();

            // InterfaceWithoutConcreteClass
            new RelationTypeBuilder(domain, new Guid("b490715d-e318-471b-bd37-1c1e12c0314e")).WithObjectTypes(InterfaceWithoutConcreteClass, AllorsBoolean).Build();

            // ILT32Composite
            new RelationTypeBuilder(domain, new Guid("be3fc71d-66d8-411f-ab5f-4ed91e437852")).WithObjectTypes(ILT32Composite, ILT32Composite).WithSingularName("Self3").WithPluralName("Selfs3").WithCardinality(Cardinalities.ManyToOne).Build();
            new RelationTypeBuilder(domain, new Guid("c03a8b50-7fd1-4304-9d45-2c699fcbee80")).WithObjectTypes(ILT32Composite, ILT32Composite).WithSingularName("Self2").WithPluralName("Selfs2").WithCardinality(Cardinalities.ManyToOne).Build();
            new RelationTypeBuilder(domain, new Guid("d0eeeb45-97a6-465e-9a05-7e0fa970a969")).WithObjectTypes(ILT32Composite, ILT32Composite).WithSingularName("Self1").WithPluralName("Selfs1").WithCardinality(Cardinalities.ManyToOne).Build();

            // IGT32Unit
            new RelationTypeBuilder(domain, new Guid("113ea45f-0e8a-423d-b650-30ab4ac85ebd")).WithObjectTypes(IGT32Unit, AllorsString).WithSingularName("AllorsString2").WithPluralName("AllorsStrings2").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("163739dd-60aa-48b3-8566-43accb24cf0f")).WithObjectTypes(IGT32Unit, AllorsString).WithSingularName("AllorsString5").WithPluralName("AllorsStrings5").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("18bf90a6-2954-4e4f-bfa9-78ede63314bf")).WithObjectTypes(IGT32Unit, AllorsString).WithSingularName("AllorsString19").WithPluralName("AllorsStrings19").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("209d428f-87b5-49d9-b3b6-9ef357889f2a")).WithObjectTypes(IGT32Unit, AllorsString).WithSingularName("AllorsString18").WithPluralName("AllorsStrings18").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("2279e1c7-1f8d-4daf-b686-aee9c143ce5d")).WithObjectTypes(IGT32Unit, AllorsString).WithSingularName("AllorsString21").WithPluralName("AllorsStrings21").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("26a72acf-af4e-48b5-af95-b3fa78bfbcf8")).WithObjectTypes(IGT32Unit, AllorsString).WithSingularName("AllorsString31").WithPluralName("AllorsStrings31").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("36daace4-f9d1-453d-9caf-90173b13017b")).WithObjectTypes(IGT32Unit, AllorsString).WithSingularName("AllorsString15").WithPluralName("AllorsStrings15").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("4c0539d2-2ef3-4572-8098-3e161c338316")).WithObjectTypes(IGT32Unit, AllorsString).WithSingularName("AllorsString6").WithPluralName("AllorsStrings6").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("505b67b2-6e0b-45cc-9474-5782ab40f0a7")).WithObjectTypes(IGT32Unit, AllorsString).WithSingularName("AllorsString27").WithPluralName("AllorsStrings27").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("60be7e02-6c19-4f55-a67d-041c0c29c7b1")).WithObjectTypes(IGT32Unit, AllorsString).WithSingularName("AllorsString11").WithPluralName("AllorsStrings11").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("63e19c51-8721-4a53-a129-fff09429498e")).WithObjectTypes(IGT32Unit, AllorsString).WithSingularName("AllorsString33").WithPluralName("AllorsStrings33").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("6facb71c-1399-41c3-94cd-e51b2ace2d49")).WithObjectTypes(IGT32Unit, AllorsString).WithSingularName("AllorsString32").WithPluralName("AllorsStrings32").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("7890180e-3ea8-490d-a360-16f04ef567dd")).WithObjectTypes(IGT32Unit, AllorsString).WithSingularName("AllorsString25").WithPluralName("AllorsStrings25").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("7a653b33-2ea5-483f-903d-6f13891e6c44")).WithObjectTypes(IGT32Unit, AllorsString).WithSingularName("AllorsString8").WithPluralName("AllorsStrings8").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("81d16484-71fd-445b-a681-0363a6d95325")).WithObjectTypes(IGT32Unit, AllorsString).WithSingularName("AllorsString28").WithPluralName("AllorsStrings28").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("84670520-d8c9-407f-82e3-6eb53f1fb290")).WithObjectTypes(IGT32Unit, AllorsString).WithSingularName("AllorsString29").WithPluralName("AllorsStrings29").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("88324671-7170-4798-8cc0-d2b25212f7a1")).WithObjectTypes(IGT32Unit, AllorsString).WithSingularName("AllorsString20").WithPluralName("AllorsStrings20").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("8d97b1d0-304a-4e8a-b62f-f425e9327ad8")).WithObjectTypes(IGT32Unit, AllorsString).WithSingularName("AllorsString16").WithPluralName("AllorsStrings16").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("8f538538-785f-4cdc-9106-2137644f36ae")).WithObjectTypes(IGT32Unit, AllorsString).WithSingularName("AllorsString30").WithPluralName("AllorsStrings30").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("96f9bb98-8658-4903-9b97-7dbb50ac258d")).WithObjectTypes(IGT32Unit, AllorsString).WithSingularName("AllorsString12").WithPluralName("AllorsStrings12").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("a0ce37ac-ec40-4215-9ff6-7b39121080af")).WithObjectTypes(IGT32Unit, AllorsString).WithSingularName("AllorsString26").WithPluralName("AllorsStrings26").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("a5ed3f77-5f87-4994-8f25-a35fad3f71fe")).WithObjectTypes(IGT32Unit, AllorsString).WithSingularName("AllorsString17").WithPluralName("AllorsStrings17").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("a6c3242f-aab8-481e-803e-67d7d45f15d3")).WithObjectTypes(IGT32Unit, AllorsString).WithSingularName("AllorsString4").WithPluralName("AllorsStrings4").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("a91487f7-8b1a-454c-9adb-e14c3ac49271")).WithObjectTypes(IGT32Unit, AllorsString).WithSingularName("AllorsString10").WithPluralName("AllorsStrings10").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("abd8508a-e03a-4bee-ac5f-738551400205")).WithObjectTypes(IGT32Unit, AllorsString).WithSingularName("AllorsString23").WithPluralName("AllorsStrings23").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("b43ff179-22f1-47cb-a304-24e4ec977cf9")).WithObjectTypes(IGT32Unit, AllorsString).WithSingularName("AllorsString24").WithPluralName("AllorsStrings24").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("b9309d7a-9946-4462-93a8-51f78efe0696")).WithObjectTypes(IGT32Unit, AllorsString).WithSingularName("AllorsString1").WithPluralName("AllorsStrings1").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("ca170e8c-5aef-452e-8a3e-1228054d9a85")).WithObjectTypes(IGT32Unit, AllorsString).WithSingularName("AllorsString14").WithPluralName("AllorsStrings14").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("cdb2dbc9-e481-4d7b-8746-e931c7c75da5")).WithObjectTypes(IGT32Unit, AllorsString).WithSingularName("AllorsString13").WithPluralName("AllorsStrings13").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("ce493f43-d598-43fd-970f-042debdc0d67")).WithObjectTypes(IGT32Unit, AllorsString).WithSingularName("AllorsString22").WithPluralName("AllorsStrings22").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("db9ce637-26ba-4551-abc2-4199d91e7db5")).WithObjectTypes(IGT32Unit, AllorsString).WithSingularName("AllorsString3").WithPluralName("AllorsStrings3").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("e27c59c0-a8ed-46c2-8fd6-707bb45b8af5")).WithObjectTypes(IGT32Unit, AllorsString).WithSingularName("AllorsString7").WithPluralName("AllorsStrings7").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("e518ffe9-7a15-469d-9062-fb0f3e25fde3")).WithObjectTypes(IGT32Unit, AllorsString).WithSingularName("AllorsString9").WithPluralName("AllorsStrings9").WithSize(256).Build();

            // Person
            new RelationTypeBuilder(domain, new Guid("25ff791d-9547-41ba-ac34-f2fe501ef217")).WithObjectTypes(Person, Person).WithSingularName("NextPerson").WithPluralName("NextPersons").Build();
            new RelationTypeBuilder(domain, new Guid("6cc83cb8-cb94-4716-bb7d-e25201f06b20")).WithObjectTypes(Person, Company).WithCardinality(Cardinalities.ManyToOne).WithIsIndexed(true).Build();

            // C1
            new RelationTypeBuilder(domain, new Guid("024db9e0-b51f-4d8b-a2d0-0a041dcebd62")).WithObjectTypes(C1, AllorsDecimal).WithSingularName("DecimalBetweenA").WithPluralName("DecimalsBetweenA").WithScale(2).WithPrecision(19).Build();
            new RelationTypeBuilder(domain, new Guid("03245a2b-be79-4cd5-a3fb-40ca784c108d")).WithObjectTypes(C1, AllorsLong).WithSingularName("LongLessThan").WithPluralName("LongsLessThan").Build();
            new RelationTypeBuilder(domain, new Guid("03fc18eb-46be-411a-9b1e-4a1953843d92")).WithObjectTypes(C1, I2).WithSingularName("I2one2one").WithPluralName("I2one2ones").Build();
            new RelationTypeBuilder(domain, new Guid("0aefa669-9c8a-4fbf-98a4-230d93df8341")).WithObjectTypes(C1, AllorsDecimal).WithSingularName("DecimalBetweenB").WithPluralName("DecimalsBetweenB").WithScale(2).WithPrecision(19).Build();
            new RelationTypeBuilder(domain, new Guid("0e57dd07-bb58-4620-a898-3060af007f60")).WithObjectTypes(C1, AllorsString).WithSingularName("Argument").WithPluralName("Arguments").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("10df748e-3b9c-48f4-82dc-85498f199567")).WithObjectTypes(C1, S1).WithSingularName("S1one2many").WithPluralName("S1one2manies").WithCardinality(Cardinalities.OneToMany).Build();
            new RelationTypeBuilder(domain, new Guid("13761939-4842-45ba-af73-2a5976e2d6e3")).WithObjectTypes(C1, I12).WithSingularName("I12one2one").WithPluralName("I12one2ones").Build();
            new RelationTypeBuilder(domain, new Guid("20713860-8abd-4d71-8ccc-2b4d1b88bce3")).WithObjectTypes(C1, AllorsString).WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("2cd8b843-f1f5-413d-9d6d-0d2b9b3c5cf6")).WithObjectTypes(C1, C1).WithSingularName("C1many2one").WithPluralName("C1many2ones").WithCardinality(Cardinalities.ManyToOne).Build();
            new RelationTypeBuilder(domain, new Guid("2cee32ad-4e62-4112-9775-f84b0298e93a")).WithObjectTypes(C1, S2).WithSingularName("S2many2one").WithPluralName("S2many2ones").WithCardinality(Cardinalities.ManyToOne).Build();
            new RelationTypeBuilder(domain, new Guid("2fa10f1e-d7f6-4f75-92a8-15d7b02b8c19")).WithObjectTypes(C1, AllorsDouble).WithSingularName("DoubleBetweenA").WithPluralName("DoublesBetweenA").Build();
            new RelationTypeBuilder(domain, new Guid("2fc66f19-7fd4-4dc1-95ef-7931864ad083")).WithObjectTypes(C1, C1).WithSingularName("Many2One").WithPluralName("Many2Ones").WithCardinality(Cardinalities.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("2ff1c9ba-0017-466e-9f11-776086e6d0b0")).WithObjectTypes(C1, C1).WithSingularName("C1many2many").WithPluralName("C1many2manies").WithCardinality(Cardinalities.ManyToMany).Build();
            new RelationTypeBuilder(domain, new Guid("3673e4f6-8b40-44e7-be25-d73907b5806a")).WithObjectTypes(C1, S1).WithSingularName("S1many2many").WithPluralName("S1many2manies").WithCardinality(Cardinalities.ManyToMany).Build();
            new RelationTypeBuilder(domain, new Guid("392e8c95-bbfc-4d24-b751-36c17a7b0ee6")).WithObjectTypes(C1, AllorsDouble).WithSingularName("DoubleBetweenB").WithPluralName("DoublesBetweenB").Build();
            new RelationTypeBuilder(domain, new Guid("3fea182f-07b0-4c36-8170-960b484801f6")).WithObjectTypes(C1, I1).WithSingularName("I1one2one").WithPluralName("I1one2one").Build();
            new RelationTypeBuilder(domain, new Guid("49970761-ebe1-4623-a822-5ee1d1f3fafc")).WithObjectTypes(C1, AllorsInteger).WithSingularName("IntegerLessThan").WithPluralName("IntegersLessThan").Build();
            new RelationTypeBuilder(domain, new Guid("4b970db5-d0ec-4765-9f9b-6e9aafc9dbcc")).WithObjectTypes(C1, AllorsString).WithSingularName("StringLarge").WithPluralName("StringsLarge").WithSize(100000).Build();
            new RelationTypeBuilder(domain, new Guid("4c0362ad-4d0e-4e57-a057-1852ddd8eed8")).WithObjectTypes(C1, I2).WithSingularName("I2one2many").WithPluralName("I2one2manies").WithCardinality(Cardinalities.OneToMany).Build();
            new RelationTypeBuilder(domain, new Guid("4c776502-77d7-45d9-b101-62dee27c0c2e")).WithObjectTypes(C1, C1).WithSingularName("C1one2one").WithPluralName("C1one2ones").Build();
            new RelationTypeBuilder(domain, new Guid("4c95279f-fb68-49d1-b9c2-27c612c4c28e")).WithObjectTypes(C1, AllorsDouble).WithSingularName("DoubleGreaterThan").WithPluralName("DoublesGreaterThan").Build();
            new RelationTypeBuilder(domain, new Guid("4dab4e16-b8a2-46c1-949d-62aead9a9c9f")).WithObjectTypes(C1, I2).WithSingularName("I2many2one").WithPluralName("I2many2one").WithCardinality(Cardinalities.ManyToOne).Build();
            new RelationTypeBuilder(domain, new Guid("599420c6-0757-49f6-8ae7-4cb0714ca791")).WithObjectTypes(C1, I12).WithSingularName("I12many2one").WithPluralName("I12many2ones").WithCardinality(Cardinalities.ManyToOne).Build();
            new RelationTypeBuilder(domain, new Guid("6459deba-24e6-4867-a555-75f672f33893")).WithObjectTypes(C1, AllorsDateTime).WithSingularName("DateTimeLessThan").WithPluralName("DateTimesLessThan").Build();
            new RelationTypeBuilder(domain, new Guid("6aadb05d-6b80-47c5-b625-18b86e762c94")).WithObjectTypes(C1, AllorsDateTime).WithSingularName("DateTimeBetweenA").WithPluralName("DateTimesBetweenA").Build();
            new RelationTypeBuilder(domain, new Guid("71abe169-dea4-4834-8d37-34cbcffa6cee")).WithObjectTypes(C1, C2).WithSingularName("C2many2many").WithPluralName("C2many2manies").WithCardinality(Cardinalities.ManyToMany).Build();
            new RelationTypeBuilder(domain, new Guid("724f101c-db45-44f3-b9ca-c8f3b0c28d29")).WithObjectTypes(C1, S1).WithSingularName("S1many2one").WithPluralName("S1many2ones").WithCardinality(Cardinalities.ManyToOne).Build();
            new RelationTypeBuilder(domain, new Guid("79fbfbc3-50e3-4e45-a5bf-8a253bb6f0c6")).WithObjectTypes(C1, I1).WithSingularName("I1many2many").WithPluralName("I1many2manies").WithCardinality(Cardinalities.ManyToMany).Build();
            new RelationTypeBuilder(domain, new Guid("7b058b52-dc6b-4f8c-af72-28c9b0c0fde4")).WithObjectTypes(C1, AllorsDouble).WithSingularName("DoubleLessThan").WithPluralName("DoublesLessThan").Build();
            new RelationTypeBuilder(domain, new Guid("7fce490e-78af-46a9-a87d-de233073ab3c")).WithObjectTypes(C1, I1).WithSingularName("I1many2one").WithPluralName("I1many2ones").WithCardinality(Cardinalities.ManyToOne).Build();
            new RelationTypeBuilder(domain, new Guid("8592bb21-c3cc-4f70-bf2b-442269a01104")).WithObjectTypes(C1, AllorsLong).WithSingularName("LongGreaterThan").WithPluralName("LongsGreaterThan").Build();
            new RelationTypeBuilder(domain, new Guid("8679b3aa-cdad-4ee1-b4fb-edcefd660edb")).WithObjectTypes(C1, AllorsDecimal).WithSingularName("DecimalGreaterThan").WithPluralName("DecimalsGreaterThan").WithScale(2).WithPrecision(19).Build();
            new RelationTypeBuilder(domain, new Guid("87eb0d19-73a7-4aae-aeed-66dc9163233c")).WithObjectTypes(C1, AllorsDecimal).WithScale(2).WithPrecision(10).Build();
            new RelationTypeBuilder(domain, new Guid("91e8b23b-48fb-4d20-8a71-89c5630f1c78")).WithObjectTypes(C1, AllorsLong).Build();
            new RelationTypeBuilder(domain, new Guid("92cbd254-9763-41e1-9c73-4a378aab4b8e")).WithObjectTypes(C1, S2).WithSingularName("S2one2one").WithPluralName("S2one2ones").Build();
            new RelationTypeBuilder(domain, new Guid("934421bd-6cac-4e99-9457-43117a9f3c52")).WithObjectTypes(C1, AllorsDateTime).WithSingularName("DateTimeBetweenB").WithPluralName("DateTimesBetweenB").Build();
            new RelationTypeBuilder(domain, new Guid("97f31053-0e7b-42a0-90c2-ce6f09c56e86")).WithObjectTypes(C1, AllorsBinary).WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("9d8c9863-dd8d-4c85-a5e6-58042ff3619d")).WithObjectTypes(C1, AllorsDateTime).WithSingularName("DateTimeGreaterThan").WithPluralName("DateTimesGreaterThan").Build();
            new RelationTypeBuilder(domain, new Guid("9df07ff8-7a29-4d41-a08e-d46efdd15e32")).WithObjectTypes(C1, S1).WithSingularName("S1one2one").WithPluralName("S1one2ones").Build();
            new RelationTypeBuilder(domain, new Guid("ab6d11cc-ec86-4828-8875-2e9a779ba627")).WithObjectTypes(C1, C1).WithSingularName("C1one2many").WithPluralName("C1one2manies").WithCardinality(Cardinalities.OneToMany).Build();
            new RelationTypeBuilder(domain, new Guid("ac0cfbe2-a2ff-4781-83aa-5d4e459d939f")).WithObjectTypes(C1, I1).WithSingularName("I1one2many").WithPluralName("I1one2manies").WithCardinality(Cardinalities.OneToMany).Build();
            new RelationTypeBuilder(domain, new Guid("ac2096a9-b58b-41d3-a1d3-920f0b41cb2f")).WithObjectTypes(C1, C2).WithSingularName("C2many2one").WithPluralName("C2many2ones").WithCardinality(Cardinalities.ManyToOne).Build();
            new RelationTypeBuilder(domain, new Guid("ad1b1fb1-b30c-431f-b975-5505f6311a18")).WithObjectTypes(C1, I12).WithSingularName("I12one2many").WithPluralName("I12one2manies").WithCardinality(Cardinalities.OneToMany).Build();
            new RelationTypeBuilder(domain, new Guid("b2071550-cc1b-4543-b98f-006e7564a74b")).WithObjectTypes(C1, S2).WithSingularName("S2many2many").WithPluralName("S2many2manies").WithCardinality(Cardinalities.ManyToOne).Build();
            new RelationTypeBuilder(domain, new Guid("b4e3d3d1-65b2-4803-954f-1e09f39e5594")).WithObjectTypes(C1, C2).WithSingularName("C2one2one").WithPluralName("C2one2ones").Build();
            new RelationTypeBuilder(domain, new Guid("b4ee673f-bba0-4e24-9cda-3cf993c79a0a")).WithObjectTypes(C1, AllorsBoolean).Build();
            new RelationTypeBuilder(domain, new Guid("c58903fb-443b-4de9-b010-15f3f09ff5df")).WithObjectTypes(C1, I12).WithSingularName("I12many2many").WithPluralName("I12many2manies").WithCardinality(Cardinalities.ManyToMany).Build();
            new RelationTypeBuilder(domain, new Guid("c92fbc53-ae5e-450e-9681-ca17833e6e2f")).WithObjectTypes(C1, I2).WithSingularName("I2many2many").WithPluralName("I2many2manies").WithCardinality(Cardinalities.ManyToMany).Build();
            new RelationTypeBuilder(domain, new Guid("cef13620-b7d7-4bfe-8d3b-c0f826da5989")).WithObjectTypes(C1, AllorsUnique).Build();
            new RelationTypeBuilder(domain, new Guid("d3f73a6d-8f95-44c6-bbc8-ddc468b803f7")).WithObjectTypes(C1, C3).WithSingularName("C3one2one").WithPluralName("C3one2ones").Build();
            new RelationTypeBuilder(domain, new Guid("da4d6a24-6b0f-4841-b355-80ee1ba10c59")).WithObjectTypes(C1, C3).WithSingularName("C3many2many").WithPluralName("C3many2manies").WithCardinality(Cardinalities.ManyToMany).Build();
            new RelationTypeBuilder(domain, new Guid("dc55a574-5546-4a68-b886-706c39bc4e80")).WithObjectTypes(C1, AllorsString).WithSingularName("StringEquals").WithPluralName("StringsEquals").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("e109ccb9-6469-4dd4-9b04-2a6fb5d6d6a8")).WithObjectTypes(C1, AllorsLong).WithSingularName("LongBetweenA").WithPluralName("LongsBetweenA").Build();
            new RelationTypeBuilder(domain, new Guid("e2153298-73b0-4f5f-bba0-00c832b044b3")).WithObjectTypes(C1, AllorsInteger).WithSingularName("IntegerGreaterThan").WithPluralName("IntegersGreaterThan").Build();
            new RelationTypeBuilder(domain, new Guid("e3af3413-4631-4052-ac57-955651a319fc")).WithObjectTypes(C1, C3).WithSingularName("C3may2one").WithPluralName("C3may2ones").WithCardinality(Cardinalities.ManyToOne).Build();
            new RelationTypeBuilder(domain, new Guid("e3dedb1d-6738-46f7-8a25-77213c90a8f9")).WithObjectTypes(C1, AllorsInteger).WithSingularName("IntegerBetweenB").WithPluralName("IntegersBetweenB").Build();
            new RelationTypeBuilder(domain, new Guid("ef75cc4e-8787-4f1c-ae5c-73577d721467")).WithObjectTypes(C1, AllorsDateTime).Build();
            new RelationTypeBuilder(domain, new Guid("ef909fec-7a03-4a3c-a3f4-6097a51ff1f0")).WithObjectTypes(C1, AllorsInteger).WithSingularName("IntegerBetweenA").WithPluralName("IntegersBetweenA").Build();
            new RelationTypeBuilder(domain, new Guid("f268783d-42ed-41c1-b0b0-b8a60e30a601")).WithObjectTypes(C1, AllorsDouble).Build();
            new RelationTypeBuilder(domain, new Guid("f31a140e-6e95-4c7b-bdd7-d58137061b85")).WithObjectTypes(C1, AllorsLong).WithSingularName("LongBetweenB").WithPluralName("LongsBetweenB").Build();
            new RelationTypeBuilder(domain, new Guid("f39739d2-e8fc-406e-be6a-c92acee07686")).WithObjectTypes(C1, C2).WithSingularName("C2one2many").WithPluralName("C2one2manies").WithCardinality(Cardinalities.OneToMany).Build();
            new RelationTypeBuilder(domain, new Guid("f47b9392-1391-416e-9a49-23ab0627133e")).WithObjectTypes(C1, S2).WithSingularName("S2one2many").WithPluralName("S2one2manies").WithCardinality(Cardinalities.OneToMany).Build();
            new RelationTypeBuilder(domain, new Guid("f4920d94-8cd0-45b6-be00-f18d377368fd")).WithObjectTypes(C1, AllorsInteger).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("fc56ca04-9737-4b51-939e-4854e5507953")).WithObjectTypes(C1, AllorsDecimal).WithSingularName("DecimalLessThan").WithPluralName("DecimalsLessThan").WithScale(2).WithPrecision(19).Build();
            new RelationTypeBuilder(domain, new Guid("fee2d1a8-bb65-4bfe-b25f-407c629dec18")).WithObjectTypes(C1, C3).WithSingularName("C3one2many").WithPluralName("C3one2manies").WithCardinality(Cardinalities.OneToMany).Build();

            // C2
            new RelationTypeBuilder(domain, new Guid("07eaa992-322a-40e9-bf2c-aa33b69f54cd")).WithObjectTypes(C2, AllorsDecimal).WithScale(2).WithPrecision(19).Build();
            new RelationTypeBuilder(domain, new Guid("0947eb06-5511-475f-8d68-06cfb812678e")).WithObjectTypes(C2, C1).WithSingularName("C1many2many").WithPluralName("C1many2manies").WithCardinality(Cardinalities.ManyToMany).Build();
            new RelationTypeBuilder(domain, new Guid("0ecc2d3b-f813-44db-b349-3e67d7e0b2d7")).WithObjectTypes(C2, C2).WithSingularName("C2many2one").WithPluralName("C2many2ones").WithCardinality(Cardinalities.ManyToOne).Build();
            new RelationTypeBuilder(domain, new Guid("262ad367-a52c-4d8b-94e2-b477bb098423")).WithObjectTypes(C2, AllorsDouble).Build();
            new RelationTypeBuilder(domain, new Guid("41cd7805-0f93-460f-8572-9c479f3db206")).WithObjectTypes(C2, AllorsLong).Build();
            new RelationTypeBuilder(domain, new Guid("42f9f4b6-3b35-4168-93cb-35171dbf83f4")).WithObjectTypes(C2, AllorsInteger).Build();
            new RelationTypeBuilder(domain, new Guid("49d04b6f-6393-49f6-bb6b-2dd634d6b9ee")).WithObjectTypes(C2, C2).WithSingularName("C2many2many").WithPluralName("C2many2manies").WithCardinality(Cardinalities.ManyToMany).Build();
            new RelationTypeBuilder(domain, new Guid("61daaaae-dd22-405e-aa98-6321d2f8af04")).WithObjectTypes(C2, AllorsBoolean).Build();
            new RelationTypeBuilder(domain, new Guid("7ee9d97c-8ae3-438c-adfd-6a35b3ff645b")).WithObjectTypes(C2, C1).WithSingularName("C1many2one").WithPluralName("C1many2ones").WithCardinality(Cardinalities.ManyToOne).Build();
            new RelationTypeBuilder(domain, new Guid("9540e8d3-9fe3-4aea-9918-fc31210f2622")).WithObjectTypes(C2, C1).WithSingularName("C1one2one").WithPluralName("C1one2ones").Build();
            new RelationTypeBuilder(domain, new Guid("9c7cde3f-9b61-4c79-a5d7-afe1067262ce")).WithObjectTypes(C2, AllorsString).WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("9e9d1c6a-f647-4922-b5f4-874b8b6c1907")).WithObjectTypes(C2, C2).WithSingularName("C2one2one").WithPluralName("C2one2ones").Build();
            new RelationTypeBuilder(domain, new Guid("a95948a7-3f12-4b85-8823-82dea87740c0")).WithObjectTypes(C2, C2).WithSingularName("C2one2many").WithPluralName("C2one2manies").WithCardinality(Cardinalities.OneToMany).Build();
            new RelationTypeBuilder(domain, new Guid("ce23482d-3a22-4202-98e7-5934fd9abd2d")).WithObjectTypes(C2, AllorsDateTime).Build();
            new RelationTypeBuilder(domain, new Guid("d82be8f5-673a-466b-8abb-077be0bc6eb5")).WithObjectTypes(C2, C1).WithSingularName("C1one2many").WithPluralName("C1one2manies").WithCardinality(Cardinalities.OneToMany).Build();
            new RelationTypeBuilder(domain, new Guid("d92643c0-854c-40f8-92c8-93a0245e33c2")).WithObjectTypes(C2, C3).WithSingularName("C3Many2Many").WithPluralName("C3Many2Manies").WithCardinality(Cardinalities.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("f3482f88-4408-4e2e-b179-7f757bf0eb3d")).WithObjectTypes(C2, C3).WithSingularName("C3Many2One").WithPluralName("C3Many2Ones").WithCardinality(Cardinalities.ManyToOne).WithIsIndexed(true).Build();

            // Sandbox
            new RelationTypeBuilder(domain, new Guid("0e0ee030-8fb5-42fb-82b5-5daade2aca9d")).WithObjectTypes(Sandbox, Sandbox).WithSingularName("InvisibleMany").WithPluralName("InvisibleManies").WithCardinality(Cardinalities.ManyToMany).Build();
            new RelationTypeBuilder(domain, new Guid("122b0376-8d1a-4d46-b8a0-9f4ea94c9e96")).WithObjectTypes(Sandbox, Sandbox).WithSingularName("InvisibleOne").WithPluralName("InvisibleOnes").Build();
            new RelationTypeBuilder(domain, new Guid("5eec5096-d8ba-424e-988f-b50828fc7b51")).WithObjectTypes(Sandbox, AllorsString).WithSingularName("InvisibleValue").WithPluralName("InvisibleValues").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("856a0161-2a46-428a-bae5-95d6a86a89e8")).WithObjectTypes(Sandbox, AllorsString).WithSingularName("Test").WithPluralName("Tests").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("c82d1693-7b88-4fab-8389-a43185c832ed")).WithObjectTypes(Sandbox, AllorsString).WithSize(256).Build();

            // ISandbox
            new RelationTypeBuilder(domain, new Guid("38361bff-62b3-4607-8291-cfdaeedbd36d")).WithObjectTypes(ISandbox, AllorsString).WithSingularName("InvisibleValue").WithPluralName("InvisibleValues").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("796ab057-88a0-4d71-bc4a-2673a209161b")).WithObjectTypes(ISandbox, ISandbox).WithSingularName("InvisibleMany").WithPluralName("InvisibleManies").WithCardinality(Cardinalities.ManyToMany).Build();
            new RelationTypeBuilder(domain, new Guid("dba5deb2-880d-47f4-adae-0b3125ff1379")).WithObjectTypes(ISandbox, ISandbox).WithSingularName("InvisibleOne").WithPluralName("InvisibleOnes").Build();

            // I12
            new RelationTypeBuilder(domain, new Guid("1a0eb6ea-d877-42c9-a35a-889fb347f883")).WithObjectTypes(I12, AllorsBoolean).Build();
            new RelationTypeBuilder(domain, new Guid("249ff221-9261-4219-b0a8-0dc2a8dac8db")).WithObjectTypes(I12, AllorsInteger).Build();
            new RelationTypeBuilder(domain, new Guid("2c05b90e-a036-450a-8b4e-ee70c8146fed")).WithObjectTypes(I12, I34).WithSingularName("I34one2many").WithPluralName("I34one2manies").WithCardinality(Cardinalities.OneToMany).Build();
            new RelationTypeBuilder(domain, new Guid("3327e14d-5601-4806-b6c5-b740a2c3aa38")).WithObjectTypes(I12, C3).WithSingularName("C3many2one").WithPluralName("C3many2ones").WithCardinality(Cardinalities.ManyToOne).Build();
            new RelationTypeBuilder(domain, new Guid("3589d5bc-3338-449a-bd14-34a19d92251e")).WithObjectTypes(I12, C2).WithSingularName("C2many2one").WithPluralName("C2many2ones").WithCardinality(Cardinalities.ManyToOne).Build();
            new RelationTypeBuilder(domain, new Guid("4c7dd6a2-db16-4477-9b21-34dcb8f50738")).WithObjectTypes(I12, AllorsDouble).Build();
            new RelationTypeBuilder(domain, new Guid("61fc731f-d769-4eb9-bf87-983e73e403e4")).WithObjectTypes(I12, I34).WithSingularName("I34many2one").WithPluralName("I34many2ones").WithCardinality(Cardinalities.ManyToOne).Build();
            new RelationTypeBuilder(domain, new Guid("716d13fc-f608-41a8-ac9e-824890c585b5")).WithObjectTypes(I12, I34).WithSingularName("I34many2many").WithPluralName("I34many2manies").WithCardinality(Cardinalities.ManyToMany).Build();
            new RelationTypeBuilder(domain, new Guid("74a22498-ec2c-441b-a42c-0c248ace685d")).WithObjectTypes(I12, C3).WithSingularName("C3one2one").WithPluralName("C3one2ones").Build();
            new RelationTypeBuilder(domain, new Guid("7f373030-657a-4c6b-a086-ac4de33e4648")).WithObjectTypes(I12, C2).WithSingularName("C2many2many").WithPluralName("C2many2manies").WithCardinality(Cardinalities.ManyToMany).Build();
            new RelationTypeBuilder(domain, new Guid("94b55cec-cf78-420b-b4c9-1bca8a8af9dc")).WithObjectTypes(I12, AllorsLong).Build();
            new RelationTypeBuilder(domain, new Guid("9fbca845-1f98-4ac8-8117-fa66bbe287eb")).WithObjectTypes(I12, AllorsDecimal).WithScale(2).WithPrecision(19).Build();
            new RelationTypeBuilder(domain, new Guid("afabb84c-f1b3-423b-9028-2ec5bb58e994")).WithObjectTypes(I12, C2).WithSingularName("C2one2one").WithPluralName("C2one2ones").Build();
            new RelationTypeBuilder(domain, new Guid("b0fc73fb-fa74-4e8c-b9e1-17c01698f342")).WithObjectTypes(I12, C3).WithSingularName("C3one2many").WithPluralName("C3one2manies").WithCardinality(Cardinalities.OneToMany).Build();
            new RelationTypeBuilder(domain, new Guid("b889bc75-3d93-4577-a4d7-752393284220")).WithObjectTypes(I12, C3).WithSingularName("C3many2many").WithPluralName("C3many2manies").WithCardinality(Cardinalities.ManyToMany).Build();
            new RelationTypeBuilder(domain, new Guid("c2d1f044-b996-4b16-8fe3-1786f86973b1")).WithObjectTypes(I12, AllorsString).WithSingularName("PrefetchTest").WithPluralName("PrefetchesTest").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("c3a2e1da-307c-4fad-ab34-6e9d07eea74f")).WithObjectTypes(I12, AllorsDateTime).Build();
            new RelationTypeBuilder(domain, new Guid("e227ff6c-a4df-49cf-a02f-04e94af6eb4b")).WithObjectTypes(I12, AllorsString).WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("f31ace17-76b1-46db-9fc0-099b94fbada5")).WithObjectTypes(I12, I34).WithSingularName("I34one2one").WithPluralName("I34one2ones").Build();
            new RelationTypeBuilder(domain, new Guid("f37b107e-74e5-401f-a7e8-8ac54ceb6c73")).WithObjectTypes(I12, C2).WithSingularName("C2one2many").WithPluralName("C2one2manies").WithCardinality(Cardinalities.OneToMany).Build();

            // Company
            new RelationTypeBuilder(domain, new Guid("08ab248d-bdb1-49c5-a2da-d6485f49239f")).WithObjectTypes(Company, Person).WithSingularName("Manager").WithPluralName("Managers").WithCardinality(Cardinalities.ManyToOne).Build();
            new RelationTypeBuilder(domain, new Guid("1a4087de-f116-4f79-9441-31faee8054f3")).WithObjectTypes(Company, Person).WithSingularName("Employee").WithPluralName("Employees").WithCardinality(Cardinalities.OneToMany).Build();
            new RelationTypeBuilder(domain, new Guid("28021756-f15f-4671-aa01-a40d3707d61a")).WithObjectTypes(Company, Person).WithSingularName("FirstPerson").WithPluralName("FirstPersons").Build();
            new RelationTypeBuilder(domain, new Guid("2f9fc05e-c904-4056-83f0-a7081762594a")).WithObjectTypes(Company, Named).WithSingularName("NamedOneSort2").WithPluralName("NamedsOneSort2").WithCardinality(Cardinalities.OneToMany).Build();
            new RelationTypeBuilder(domain, new Guid("44abca14-9fb2-42a7-b8ab-a1ca87d87b2e")).WithObjectTypes(Company, Person).WithSingularName("Owner").WithPluralName("Owners").WithCardinality(Cardinalities.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("509c5341-3d87-4da4-a807-5567d897169b")).WithObjectTypes(Company, Person).WithSingularName("IndexedMany2ManyPerson").WithPluralName("IndexedMany2ManyPersons").WithCardinality(Cardinalities.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("62b4ddac-efd7-4fc9-bbed-91c831a62f01")).WithObjectTypes(Company, Person).WithSingularName("PersonOneSort1").WithPluralName("PersonsOneSort1").WithCardinality(Cardinalities.OneToMany).Build();
            new RelationTypeBuilder(domain, new Guid("64c1be0a-0636-4da0-8404-2a93ab600cd9")).WithObjectTypes(Company, Person).WithSingularName("PersonManySort1").WithPluralName("PersonsManySort1").WithCardinality(Cardinalities.ManyToMany).Build();
            new RelationTypeBuilder(domain, new Guid("996d27ff-3615-4a51-9214-944fac566a11")).WithObjectTypes(Company, Named).WithSingularName("NamedManySort1").WithPluralName("NamedsManySort1").WithCardinality(Cardinalities.ManyToMany).Build();
            new RelationTypeBuilder(domain, new Guid("a9f60154-6bd1-4c76-94eb-edfd5beb6749")).WithObjectTypes(Company, Person).WithSingularName("PersonManySort2").WithPluralName("PersonsManySort2").WithCardinality(Cardinalities.ManyToMany).Build();
            new RelationTypeBuilder(domain, new Guid("bdf71d38-8082-4a99-9636-4f4ec26fd45c")).WithObjectTypes(Company, Person).WithSingularName("PersonOneSort2").WithPluralName("PersonsOneSort2").WithCardinality(Cardinalities.OneToMany).Build();
            new RelationTypeBuilder(domain, new Guid("c1f68661-4999-4851-9224-1878258b6a58")).WithObjectTypes(Company, Named).WithSingularName("NamedManySort2").WithPluralName("NamedsManySort2").WithCardinality(Cardinalities.ManyToOne).Build();
            new RelationTypeBuilder(domain, new Guid("c53bdaea-c0a5-4179-bfbb-e12de45e2ae0")).WithObjectTypes(Company, Person).WithSingularName("Many2ManyPerson").WithPluralName("Many2ManyPersons").WithCardinality(Cardinalities.ManyToMany).Build();
            new RelationTypeBuilder(domain, new Guid("cde0a8e7-1a14-4f1a-a0ca-a305f0548df8")).WithObjectTypes(Company, Company).WithSingularName("Child").WithPluralName("Children").WithCardinality(Cardinalities.OneToMany).Build();
            new RelationTypeBuilder(domain, new Guid("cdf04399-aa37-4ea2-9ac8-bf6d19884933")).WithObjectTypes(Company, Named).WithSingularName("NamedOneSort1").WithPluralName("NamedsOneSort1").WithCardinality(Cardinalities.OneToMany).Build();

            // S1234
            new RelationTypeBuilder(domain, new Guid("012a43d3-e1e0-4693-a771-1526c29b7ac4")).WithObjectTypes(S1234, AllorsDouble).Build();
            new RelationTypeBuilder(domain, new Guid("2ac36edd-d718-4252-b7cf-74849e1fca6e")).WithObjectTypes(S1234, AllorsDecimal).WithScale(2).WithPrecision(19).Build();
            new RelationTypeBuilder(domain, new Guid("46263379-afd4-4472-bb05-057fb88163ab")).WithObjectTypes(S1234, AllorsInteger).Build();
            new RelationTypeBuilder(domain, new Guid("4b846355-000b-4651-bff2-51f1275c1461")).WithObjectTypes(S1234, S1234).WithSingularName("S1234many2one").WithPluralName("S1234many2one").WithCardinality(Cardinalities.ManyToOne).Build();
            new RelationTypeBuilder(domain, new Guid("58a56dee-c613-4d76-ab99-5608e7709cd8")).WithObjectTypes(S1234, C2).WithSingularName("C2one2one").WithPluralName("C2one2ones").Build();
            new RelationTypeBuilder(domain, new Guid("73302b50-8526-40ae-a202-5b17e1093629")).WithObjectTypes(S1234, C2).WithSingularName("C2many2many").WithPluralName("C2many2manies").WithCardinality(Cardinalities.ManyToMany).Build();
            new RelationTypeBuilder(domain, new Guid("8fb24e1c-9e04-4b3d-8a97-153d3c0ea7ec")).WithObjectTypes(S1234, S1234).WithSingularName("S1234one2many").WithPluralName("S1234one2manies").WithCardinality(Cardinalities.OneToMany).Build();
            new RelationTypeBuilder(domain, new Guid("94a49847-273f-4e9b-b07b-d615d994757a")).WithObjectTypes(S1234, C2).WithSingularName("C2one2many").WithPluralName("C2one2manies").WithCardinality(Cardinalities.OneToMany).Build();
            new RelationTypeBuilder(domain, new Guid("a2e7c6f6-ca0d-4fb3-9431-8dd1be7ebdb7")).WithObjectTypes(S1234, S1234).WithSingularName("S1234many2many").WithPluralName("S1234many2manies").WithCardinality(Cardinalities.ManyToMany).Build();
            new RelationTypeBuilder(domain, new Guid("b0814062-4881-43ec-935e-c6b61ef0bcf6")).WithObjectTypes(S1234, AllorsLong).Build();
            new RelationTypeBuilder(domain, new Guid("b299db28-1107-4120-946c-fbdad2271c5c")).WithObjectTypes(S1234, AllorsString).WithSingularName("ClassName").WithPluralName("ClassNames").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("c13e8484-75a3-40be-afd5-44a31aca3771")).WithObjectTypes(S1234, AllorsDateTime).Build();
            new RelationTypeBuilder(domain, new Guid("c2fac2fc-14c6-4aa3-89ff-afba1316d06d")).WithObjectTypes(S1234, S1234).WithSingularName("S1234one2one").WithPluralName("S1234one2one").Build();
            new RelationTypeBuilder(domain, new Guid("df9eb36a-366f-4a5a-a750-f2f23f681c74")).WithObjectTypes(S1234, C2).WithSingularName("C2many2one").WithPluralName("C2many2ones").WithCardinality(Cardinalities.ManyToOne).Build();
            new RelationTypeBuilder(domain, new Guid("e6164217-2f54-4134-8c53-4a45caa9dd11")).WithObjectTypes(S1234, AllorsString).WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("ef45cd72-2e16-47df-b949-c803a554b307")).WithObjectTypes(S1234, AllorsBoolean).Build();

            // SingleUnit
            new RelationTypeBuilder(domain, new Guid("acf7d284-2480-4a09-a13b-ba4ba96e0892")).WithObjectTypes(SingleUnit, AllorsInteger).Build();

            // S12
            new RelationTypeBuilder(domain, new Guid("06fabe71-737a-4cff-ac10-2d15dafce503")).WithObjectTypes(S12, AllorsString).WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("2eb9e232-4ed4-4997-a21a-f11bb0fe3b0e")).WithObjectTypes(S12, AllorsDateTime).Build();
            new RelationTypeBuilder(domain, new Guid("39f50108-df59-455d-8371-fc07f3dbb7ef")).WithObjectTypes(S12, C2).WithSingularName("C2many2many").WithPluralName("C2many2manies").WithCardinality(Cardinalities.ManyToMany).Build();
            new RelationTypeBuilder(domain, new Guid("61e8c425-407e-408b-9f2e-c95548833004")).WithObjectTypes(S12, C2).WithSingularName("C2many2one").WithPluralName("C2many2ones").WithCardinality(Cardinalities.ManyToOne).Build();
            new RelationTypeBuilder(domain, new Guid("830117d4-fbe1-4944-bacf-54331e8451d7")).WithObjectTypes(S12, C2).WithSingularName("C2one2one").WithPluralName("C2one2ones").Build();
            new RelationTypeBuilder(domain, new Guid("a3aac482-aad0-4b59-9361-51b23867e5a2")).WithObjectTypes(S12, C2).WithSingularName("C2one2many").WithPluralName("C2one2manies").WithCardinality(Cardinalities.OneToMany).Build();
            new RelationTypeBuilder(domain, new Guid("a97eca8e-807b-4a06-9587-6240f6150203")).WithObjectTypes(S12, AllorsBoolean).Build();
            new RelationTypeBuilder(domain, new Guid("acc4ae39-2d5c-4485-be22-87b27e84b627")).WithObjectTypes(S12, AllorsDouble).Build();
            new RelationTypeBuilder(domain, new Guid("d07313ca-fd8d-4c74-928e-41274aa28de9")).WithObjectTypes(S12, AllorsInteger).Build();
            new RelationTypeBuilder(domain, new Guid("f7ace363-89bd-4ea5-a865-4a6e3de2d723")).WithObjectTypes(S12, AllorsDecimal).WithScale(2).WithPrecision(19).Build();

            // ClassWithoutRoles
            // I34
            new RelationTypeBuilder(domain, new Guid("37e8d764-bfeb-40d8-b7e9-d94e455dcc11")).WithObjectTypes(I34, AllorsDecimal).WithScale(2).WithPrecision(19).Build();
            new RelationTypeBuilder(domain, new Guid("4a6db64f-aeeb-4657-a24c-7997129f3efa")).WithObjectTypes(I34, AllorsBoolean).Build();
            new RelationTypeBuilder(domain, new Guid("9b774204-37f3-4663-9162-dc801ea200f6")).WithObjectTypes(I34, AllorsDouble).Build();
            new RelationTypeBuilder(domain, new Guid("cd30dada-24c5-4b94-8f58-ab1018f087ea")).WithObjectTypes(I34, AllorsInteger).Build();
            new RelationTypeBuilder(domain, new Guid("d8125c69-1921-4e16-84bc-d3d174be7b83")).WithObjectTypes(I34, AllorsString).WithSize(256).Build();

            // IGT32Composite
            new RelationTypeBuilder(domain, new Guid("010bc5d7-9e1e-4ca7-a146-33b73252c4c8")).WithObjectTypes(IGT32Composite, IGT32Composite).WithSingularName("Self13").WithPluralName("Selfs13").Build();
            new RelationTypeBuilder(domain, new Guid("02894576-278f-4cbe-9c19-346187f9006f")).WithObjectTypes(IGT32Composite, IGT32Composite).WithSingularName("Self31").WithPluralName("Selfs31").Build();
            new RelationTypeBuilder(domain, new Guid("03f0e0ab-d24d-4eae-9b05-0ce153055530")).WithObjectTypes(IGT32Composite, IGT32Composite).WithSingularName("Self14").WithPluralName("Selfs14").Build();
            new RelationTypeBuilder(domain, new Guid("11eb24d1-0c4d-4060-8373-e2f53da416d4")).WithObjectTypes(IGT32Composite, IGT32Composite).WithSingularName("Self21").WithPluralName("Selfs21").Build();
            new RelationTypeBuilder(domain, new Guid("1d4d3282-f7bc-4619-ae32-d987b4bd87b7")).WithObjectTypes(IGT32Composite, IGT32Composite).WithSingularName("Self2").WithPluralName("Selfs2").Build();
            new RelationTypeBuilder(domain, new Guid("3a691474-812c-4631-9909-0864297c9e86")).WithObjectTypes(IGT32Composite, IGT32Composite).WithSingularName("Self23").WithPluralName("Selfs23").Build();
            new RelationTypeBuilder(domain, new Guid("3b523d8e-2163-401a-9ccf-7d85777e216f")).WithObjectTypes(IGT32Composite, IGT32Composite).WithSingularName("Self22").WithPluralName("Selfs22").Build();
            new RelationTypeBuilder(domain, new Guid("4f4eaf7d-cc6c-4279-b371-d569fc07f148")).WithObjectTypes(IGT32Composite, IGT32Composite).WithSingularName("Self18").WithPluralName("Selfs18").Build();
            new RelationTypeBuilder(domain, new Guid("6e2f60b4-ee37-4c66-9425-aee146f51bc8")).WithObjectTypes(IGT32Composite, IGT32Composite).WithSingularName("Self17").WithPluralName("Selfs17").Build();
            new RelationTypeBuilder(domain, new Guid("6f1e2848-b27f-4ccc-a35e-467d77577a29")).WithObjectTypes(IGT32Composite, IGT32Composite).WithSingularName("Self3").WithPluralName("Selfs3").Build();
            new RelationTypeBuilder(domain, new Guid("77fccc90-38f2-48f6-b834-58f7f972823b")).WithObjectTypes(IGT32Composite, IGT32Composite).WithSingularName("Self29").WithPluralName("Selfs29").Build();
            new RelationTypeBuilder(domain, new Guid("7d18345c-7754-4ad7-96fa-e83460fa6235")).WithObjectTypes(IGT32Composite, IGT32Composite).WithSingularName("Self26").WithPluralName("Selfs26").Build();
            new RelationTypeBuilder(domain, new Guid("8ca8e840-1bf7-4131-b5a3-0abb66ba4e36")).WithObjectTypes(IGT32Composite, IGT32Composite).WithSingularName("Self4").WithPluralName("Selfs4").Build();
            new RelationTypeBuilder(domain, new Guid("8e898953-b166-4573-a56c-3be50b9c651d")).WithObjectTypes(IGT32Composite, IGT32Composite).WithSingularName("Self8").WithPluralName("Selfs8").Build();
            new RelationTypeBuilder(domain, new Guid("90bb79e0-d32b-49e9-8c05-b02505a31858")).WithObjectTypes(IGT32Composite, IGT32Composite).WithSingularName("Self24").WithPluralName("Selfs24").Build();
            new RelationTypeBuilder(domain, new Guid("90fe5360-126b-4b2d-a7ba-b29c026883a4")).WithObjectTypes(IGT32Composite, IGT32Composite).WithSingularName("Self12").WithPluralName("Selfs12").Build();
            new RelationTypeBuilder(domain, new Guid("991b59d1-9225-4534-a86e-8668068c9d45")).WithObjectTypes(IGT32Composite, IGT32Composite).WithSingularName("Self7").WithPluralName("Selfs7").Build();
            new RelationTypeBuilder(domain, new Guid("a11bfd43-47a9-4f0f-a20a-ec60939a4de1")).WithObjectTypes(IGT32Composite, IGT32Composite).WithSingularName("Self6").WithPluralName("Selfs6").Build();
            new RelationTypeBuilder(domain, new Guid("ae8fbd21-64dd-4667-b0d9-f6398e14364f")).WithObjectTypes(IGT32Composite, IGT32Composite).WithSingularName("Self25").WithPluralName("Selfs25").Build();
            new RelationTypeBuilder(domain, new Guid("b6e0754a-b271-4853-afa0-fddb96444249")).WithObjectTypes(IGT32Composite, IGT32Composite).WithSingularName("Self20").WithPluralName("Selfs20").Build();
            new RelationTypeBuilder(domain, new Guid("b9d79c6c-46cb-4bd8-80a7-8bcae27a3d3c")).WithObjectTypes(IGT32Composite, IGT32Composite).WithSingularName("Self9").WithPluralName("Selfs9").Build();
            new RelationTypeBuilder(domain, new Guid("c643a160-556b-44bb-b3e4-232d291ff1e2")).WithObjectTypes(IGT32Composite, IGT32Composite).WithSingularName("Self11").WithPluralName("Selfs11").Build();
            new RelationTypeBuilder(domain, new Guid("c662f343-3859-4d04-8d4b-011087c72885")).WithObjectTypes(IGT32Composite, IGT32Composite).WithSingularName("Self32").WithPluralName("Selfs32").Build();
            new RelationTypeBuilder(domain, new Guid("c6932f0a-e1de-4d93-ab94-80a5eb0a315c")).WithObjectTypes(IGT32Composite, IGT32Composite).WithSingularName("Self28").WithPluralName("Selfs28").Build();
            new RelationTypeBuilder(domain, new Guid("c9f2803b-890d-4370-831b-83c65805b160")).WithObjectTypes(IGT32Composite, IGT32Composite).WithSingularName("Self33").WithPluralName("Selfs33").Build();
            new RelationTypeBuilder(domain, new Guid("cb03691e-8483-4af4-9fc0-83d9ab358e12")).WithObjectTypes(IGT32Composite, IGT32Composite).WithSingularName("Self27").WithPluralName("Selfs27").Build();
            new RelationTypeBuilder(domain, new Guid("d2b6c061-927e-4db5-b419-ec7375d8845a")).WithObjectTypes(IGT32Composite, IGT32Composite).WithSingularName("Self30").WithPluralName("Selfs30").Build();
            new RelationTypeBuilder(domain, new Guid("e50d68f0-ab9d-4a0e-8976-324037145aec")).WithObjectTypes(IGT32Composite, IGT32Composite).WithSingularName("Self1").WithPluralName("Selfs1").Build();
            new RelationTypeBuilder(domain, new Guid("ec22d147-fed5-40a7-9c85-4fccc0717127")).WithObjectTypes(IGT32Composite, IGT32Composite).WithSingularName("Self15").WithPluralName("Selfs15").Build();
            new RelationTypeBuilder(domain, new Guid("f16b7de2-aed2-49c9-b1dc-618e919136a6")).WithObjectTypes(IGT32Composite, IGT32Composite).WithSingularName("Self5").WithPluralName("Selfs5").Build();
            new RelationTypeBuilder(domain, new Guid("fdcad358-8532-471a-a47e-1ad45a34a962")).WithObjectTypes(IGT32Composite, IGT32Composite).WithSingularName("Self19").WithPluralName("Selfs19").Build();
            new RelationTypeBuilder(domain, new Guid("fee41b72-ace5-4cc4-bde5-e1df40b388e4")).WithObjectTypes(IGT32Composite, IGT32Composite).WithSingularName("Self10").WithPluralName("Selfs10").Build();
            new RelationTypeBuilder(domain, new Guid("ffbe4164-497e-4b02-acc7-fefec48dc36e")).WithObjectTypes(IGT32Composite, IGT32Composite).WithSingularName("Self16").WithPluralName("Selfs16").Build();

            // Named
            new RelationTypeBuilder(domain, new Guid("ce43ca5e-4dfb-4fe1-98ea-17d8382e9531")).WithObjectTypes(Named, AllorsString).WithSingularName("Name").WithPluralName("Names").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("fdad723a-f062-492a-989c-8d8727c52679")).WithObjectTypes(Named, AllorsInteger).WithSingularName("Index").WithPluralName("Indeces").Build();

            // S2
            new RelationTypeBuilder(domain, new Guid("1c758737-140a-49f0-badc-29658b4bc55f")).WithObjectTypes(S2, AllorsString).WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("1f5a6afe-f458-43db-bea0-8c90074b5abf")).WithObjectTypes(S2, AllorsInteger).Build();
            new RelationTypeBuilder(domain, new Guid("74dd2b7b-e647-4967-9838-46c701baf3a7")).WithObjectTypes(S2, AllorsDouble).Build();
            new RelationTypeBuilder(domain, new Guid("9a191c76-bd05-498f-91da-33184c72fe90")).WithObjectTypes(S2, AllorsBoolean).Build();
            new RelationTypeBuilder(domain, new Guid("9d70a5f5-ed72-4ba3-98ac-e50752f8fb79")).WithObjectTypes(S2, AllorsDecimal).WithScale(2).WithPrecision(19).Build();
            new RelationTypeBuilder(domain, new Guid("a305d91a-5fe1-467d-9f24-6cce5dd30b1d")).WithObjectTypes(S2, AllorsDateTime).Build();
            new RelationTypeBuilder(domain, new Guid("bbb8d0fa-fe1e-49a6-a18d-0c790e52bb0c")).WithObjectTypes(S2, AllorsLong).Build();

            // I1
            new RelationTypeBuilder(domain, new Guid("00a70a04-4fc8-4585-83ce-0f7f0e0db7ab")).WithObjectTypes(I1, I34).WithSingularName("I34one2many").WithPluralName("I34one2manies").WithCardinality(Cardinalities.OneToMany).Build();
            new RelationTypeBuilder(domain, new Guid("036e3008-07f8-4a15-bca2-eb21837778a0")).WithObjectTypes(I1, I2).WithSingularName("I2one2many").WithPluralName("I2one2manies").WithCardinality(Cardinalities.OneToMany).Build();
            new RelationTypeBuilder(domain, new Guid("0b0f8c40-266c-424a-8276-0e8e2673d1a7")).WithObjectTypes(I1, I2).WithSingularName("I2many2one").WithPluralName("I2many2ones").WithCardinality(Cardinalities.ManyToOne).Build();
            new RelationTypeBuilder(domain, new Guid("0d63e4c7-28de-4d47-8f23-7ee1d3606751")).WithObjectTypes(I1, C2).WithSingularName("C2many2one").WithPluralName("C2many2ones").WithCardinality(Cardinalities.ManyToOne).Build();
            new RelationTypeBuilder(domain, new Guid("14a93943-13f6-481d-98c7-19fb55625af9")).WithObjectTypes(I1, C2).WithSingularName("C2one2one").WithPluralName("C2one2ones").Build();
            new RelationTypeBuilder(domain, new Guid("19e09e31-31ac-44cc-ad1e-a015f4747aeb")).WithObjectTypes(I1, AllorsDecimal).WithSingularName("DecimalBetweenA").WithPluralName("DecimalsBetweenA").WithScale(2).WithPrecision(19).Build();
            new RelationTypeBuilder(domain, new Guid("1d41941b-3b1d-48d7-bc6f-e8811cbd96e4")).WithObjectTypes(I1, S1).WithSingularName("S1one2one").WithPluralName("S1one2one").Build();
            new RelationTypeBuilder(domain, new Guid("28b92468-27e5-4471-b3a5-37b8ec4f794e")).WithObjectTypes(I1, I12).WithSingularName("I12many2one").WithPluralName("I12many2ones").WithCardinality(Cardinalities.ManyToOne).Build();
            new RelationTypeBuilder(domain, new Guid("28ceffc2-c776-4a0a-9825-a6d1bcb265dc")).WithObjectTypes(I1, AllorsString).WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("29244f33-6d79-44aa-9ed2-8cc01b5070b7")).WithObjectTypes(I1, AllorsDateTime).WithSingularName("DateTimeLessThan").WithPluralName("DateTimesLessThan").Build();
            new RelationTypeBuilder(domain, new Guid("2cd562b6-7f54-49af-b853-2244f10ec60e")).WithObjectTypes(I1, C2).WithSingularName("C2one2many").WithPluralName("C2one2manies").WithCardinality(Cardinalities.OneToMany).Build();
            new RelationTypeBuilder(domain, new Guid("2e98ec7e-486f-4b96-ac15-5149fe6c4e0e")).WithObjectTypes(I1, AllorsString).WithSingularName("StringLarge").WithPluralName("StringsLarge").WithSize(100000).Build();
            new RelationTypeBuilder(domain, new Guid("2f739fa2-c169-4721-8d2d-79f27a6e57c6")).WithObjectTypes(I1, AllorsDouble).WithSingularName("DoubleLessThan").WithPluralName("DoublesLessThan").Build();
            new RelationTypeBuilder(domain, new Guid("32fc21cc-4be7-4a0e-ac71-df135be95e68")).WithObjectTypes(I1, AllorsDateTime).Build();
            new RelationTypeBuilder(domain, new Guid("33f13167-3a14-4b06-a1d8-87076918b285")).WithObjectTypes(I1, C1).WithSingularName("C1many2one").WithPluralName("C1many2ones").WithCardinality(Cardinalities.ManyToOne).Build();
            new RelationTypeBuilder(domain, new Guid("350e73e2-5a0c-4b07-ae27-a16dbb19080d")).WithObjectTypes(I1, AllorsLong).WithSingularName("LongBetweenB").WithPluralName("LongsBetweenB").Build();
            new RelationTypeBuilder(domain, new Guid("381c61c1-312d-47ea-8314-8ac051378a81")).WithObjectTypes(I1, I12).WithSingularName("I12one2one").WithPluralName("I12one2ones").Build();
            new RelationTypeBuilder(domain, new Guid("39f1c13c-7d77-429f-ac9b-1491e949aa3a")).WithObjectTypes(I1, AllorsDecimal).WithSingularName("DecimalGreaterThan").WithPluralName("DecimalsGreaterThan").WithScale(2).WithPrecision(19).Build();
            new RelationTypeBuilder(domain, new Guid("4401d0b8-2450-45a8-92d2-ff3961e129b2")).WithObjectTypes(I1, C1).WithSingularName("C1one2one").WithPluralName("C1one2ones").Build();
            new RelationTypeBuilder(domain, new Guid("47c5ef75-a428-4444-afdb-1d5ef89a7a71")).WithObjectTypes(I1, AllorsLong).WithSingularName("LongLessThan").WithPluralName("LongsLessThan").Build();
            new RelationTypeBuilder(domain, new Guid("4a30d40e-ade3-4304-b17b-185abc8b7fde")).WithObjectTypes(I1, I2).WithSingularName("I2many2many").WithPluralName("I2many2manies").WithCardinality(Cardinalities.ManyToMany).Build();
            new RelationTypeBuilder(domain, new Guid("518da995-1f6b-4632-94f1-11cea5e72717")).WithObjectTypes(I1, AllorsInteger).WithSingularName("IntegerBetweenA").WithPluralName("IntegersBetweenA").Build();
            new RelationTypeBuilder(domain, new Guid("528ece9c-81f2-4ea4-8d42-50d9a3fe1eea")).WithObjectTypes(I1, I34).WithSingularName("I34many2one").WithPluralName("I34many2ones").WithCardinality(Cardinalities.ManyToOne).Build();
            new RelationTypeBuilder(domain, new Guid("58d75a73-61d3-4ad7-bd1a-b3e673d8ee31")).WithObjectTypes(I1, AllorsDouble).WithSingularName("DoubleBetweenA").WithPluralName("DoublesBetweenA").Build();
            new RelationTypeBuilder(domain, new Guid("5901c4d4-420f-47a3-87e3-ac04b4601efc")).WithObjectTypes(I1, AllorsInteger).WithSingularName("IntegerLessThan").WithPluralName("IntegersLessThan").Build();
            new RelationTypeBuilder(domain, new Guid("5cb44331-fd8c-4f73-8994-161f702849b6")).WithObjectTypes(I1, AllorsInteger).Build();
            new RelationTypeBuilder(domain, new Guid("68549750-b8f9-4a29-a078-803e7348e142")).WithObjectTypes(I1, S2).WithSingularName("S2one2one").WithPluralName("S2one2ones").Build();
            new RelationTypeBuilder(domain, new Guid("6c3d04be-6f95-44b8-863a-245e150e3110")).WithObjectTypes(I1, AllorsBoolean).Build();
            new RelationTypeBuilder(domain, new Guid("6e7c286c-42e0-45d7-8ad8-ac0ed91dbbb5")).WithObjectTypes(I1, I1).WithSingularName("I1many2one").WithPluralName("I1many2ones").WithCardinality(Cardinalities.ManyToOne).Build();
            new RelationTypeBuilder(domain, new Guid("7014e84c-62c4-48ba-b4ec-ab52a897f443")).WithObjectTypes(I1, C1).WithSingularName("C1many2many").WithPluralName("C1many2manies").WithCardinality(Cardinalities.ManyToMany).Build();
            new RelationTypeBuilder(domain, new Guid("70312f37-52e9-4cf6-9dd6-b357628ea3ed")).WithObjectTypes(I1, I2).WithSingularName("I2one2one").WithPluralName("I2one2ones").Build();
            new RelationTypeBuilder(domain, new Guid("75fb2012-0b49-4442-a9b4-8239cffb1d37")).WithObjectTypes(I1, AllorsLong).Build();
            new RelationTypeBuilder(domain, new Guid("818b4013-5ef1-4455-9f0d-9a39fa3425bb")).WithObjectTypes(I1, AllorsDecimal).WithScale(2).WithPrecision(10).Build();
            new RelationTypeBuilder(domain, new Guid("82a81e9e-7a13-43d3-bb8f-227edfe26a1f")).WithObjectTypes(I1, S1).WithSingularName("S1many2many").WithPluralName("S1many2manies").WithCardinality(Cardinalities.ManyToMany).Build();
            new RelationTypeBuilder(domain, new Guid("9095f55b-de23-49d7-a28e-918c22c5cfd2")).WithObjectTypes(I1, AllorsDateTime).WithSingularName("DateTimeGreaterThan").WithPluralName("DateTimesGreaterThan").Build();
            new RelationTypeBuilder(domain, new Guid("912eeb1b-c5d6-4ea3-9e66-6d92cc455ef6")).WithObjectTypes(I1, I34).WithSingularName("I34many2many").WithPluralName("I34many2manies").WithCardinality(Cardinalities.ManyToMany).Build();
            new RelationTypeBuilder(domain, new Guid("9291fb85-9d1f-4c5d-96ec-797be51557ce")).WithObjectTypes(I1, I34).WithSingularName("I34one2one").WithPluralName("I34one2ones").Build();
            new RelationTypeBuilder(domain, new Guid("95fff847-922f-4d6f-9e98-37013bdf6b06")).WithObjectTypes(I1, I1).WithSingularName("I1one2many").WithPluralName("I1one2manies").WithCardinality(Cardinalities.OneToMany).Build();
            new RelationTypeBuilder(domain, new Guid("9735d027-4249-4540-9658-f3ec06d3b868")).WithObjectTypes(I1, I1).WithSingularName("I1many2many").WithPluralName("I1many2manies").WithCardinality(Cardinalities.ManyToMany).Build();
            new RelationTypeBuilder(domain, new Guid("973d6e4f-57ff-454a-9621-bd5dccb65525")).WithObjectTypes(I1, S2).WithSingularName("S2many2many").WithPluralName("S2many2manies").WithCardinality(Cardinalities.ManyToMany).Build();
            new RelationTypeBuilder(domain, new Guid("9b05ecb0-c3d5-4b11-98dc-653aef9f65cc")).WithObjectTypes(I1, I12).WithSingularName("I12many2many").WithPluralName("I12many2manies").WithCardinality(Cardinalities.ManyToMany).Build();
            new RelationTypeBuilder(domain, new Guid("9f70c4eb-2e36-4ae1-8ed2-b3fab908e392")).WithObjectTypes(I1, AllorsString).WithSingularName("StringEquals").WithPluralName("StringsEquals").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("a4001f82-570e-441a-8d74-bac9241f12f2")).WithObjectTypes(I1, AllorsLong).WithSingularName("LongGreaterThan").WithPluralName("LongsAllors").Build();
            new RelationTypeBuilder(domain, new Guid("a458ad6e-0f4a-473b-a233-04b8e7fadf62")).WithObjectTypes(I1, I12).WithSingularName("I12one2many").WithPluralName("I12one2manies").WithCardinality(Cardinalities.OneToMany).Build();
            new RelationTypeBuilder(domain, new Guid("a77bcd80-82df-4b76-a1bc-8e78106d7d53")).WithObjectTypes(I1, S2).WithSingularName("S2one2many").WithPluralName("S2one2manies").WithCardinality(Cardinalities.OneToMany).Build();
            new RelationTypeBuilder(domain, new Guid("b4f171d3-1463-41bc-8230-e53e5a717b89")).WithObjectTypes(I1, C2).WithSingularName("C2many2many").WithPluralName("C2many2manies").WithCardinality(Cardinalities.ManyToMany).Build();
            new RelationTypeBuilder(domain, new Guid("b9c67658-4abc-41f3-9434-c8512a482179")).WithObjectTypes(I1, AllorsBinary).WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("c04d1e56-2686-495b-a02d-cda84f7cd2ff")).WithObjectTypes(I1, AllorsDecimal).WithSingularName("DecimalBetweenB").WithPluralName("DecimalsBetweenB").WithScale(2).WithPrecision(19).Build();
            new RelationTypeBuilder(domain, new Guid("c1021c10-9065-46f0-9cbf-30fa1e9e06ae")).WithObjectTypes(I1, AllorsLong).WithSingularName("LongBetweenA").WithPluralName("LongsBetweenA").Build();
            new RelationTypeBuilder(domain, new Guid("c3496e43-335b-43b8-9fed-44439c9ae0d1")).WithObjectTypes(I1, AllorsDouble).WithSingularName("DoubleGreaterThan").WithPluralName("DoublesGreaterThan").Build();
            new RelationTypeBuilder(domain, new Guid("c892a286-fe92-4b8b-98ba-c5e02fb96279")).WithObjectTypes(I1, AllorsInteger).WithSingularName("IntegerBetweenB").WithPluralName("IntegersBetweenB").Build();
            new RelationTypeBuilder(domain, new Guid("c95ac96b-4385-4e31-8719-f120c76ab67a")).WithObjectTypes(I1, AllorsDateTime).WithSingularName("DateTimeBetweenA").WithPluralName("DateTimesBetweenA").Build();
            new RelationTypeBuilder(domain, new Guid("cdb758bf-ecaf-4d99-88fb-58df9258c13c")).WithObjectTypes(I1, AllorsDouble).Build();
            new RelationTypeBuilder(domain, new Guid("d24b5b74-6ea2-4788-857c-90e0ba1433a5")).WithObjectTypes(I1, S1).WithSingularName("S1one2many").WithPluralName("S1one2manies").WithCardinality(Cardinalities.OneToMany).Build();
            new RelationTypeBuilder(domain, new Guid("ddbfe021-3310-4d8e-a4ef-438306aaf191")).WithObjectTypes(I1, I1).WithSingularName("I1one2one").WithPluralName("I1one2ones").Build();
            new RelationTypeBuilder(domain, new Guid("e8f1c37a-6bae-4ff5-b385-39bff287bf78")).WithObjectTypes(I1, AllorsInteger).WithSingularName("IntegerGreaterThan").WithPluralName("IntegersGreaterThan").Build();
            new RelationTypeBuilder(domain, new Guid("ee44a1bb-a5c7-4b05-a06b-8ff9ca9d4f98")).WithObjectTypes(I1, S1).WithSingularName("S1many2one").WithPluralName("S1many2one").WithCardinality(Cardinalities.ManyToOne).Build();
            new RelationTypeBuilder(domain, new Guid("eec19d8e-727c-437a-95db-b301cd1cd65a")).WithObjectTypes(I1, AllorsDouble).WithSingularName("DoubleBetweenB").WithPluralName("DoublesBetweenB").Build();
            new RelationTypeBuilder(domain, new Guid("f1a1ef6a-8275-4b57-8cd0-8e79ee5a517d")).WithObjectTypes(I1, AllorsDecimal).WithSingularName("DecimalLessThan").WithPluralName("DecimalsLessThan").WithScale(2).WithPrecision(19).Build();
            new RelationTypeBuilder(domain, new Guid("f5a6b7d9-9f49-44a8-b303-1a2969195bd1")).WithObjectTypes(I1, AllorsDateTime).WithSingularName("DateTimeBetweenB").WithPluralName("DateTimesBetweenB").Build();
            new RelationTypeBuilder(domain, new Guid("f9d7411e-7993-4e43-a7e2-726f1e44e29c")).WithObjectTypes(I1, AllorsUnique).Build();
            new RelationTypeBuilder(domain, new Guid("fbc1fd9f-853a-4b7d-b618-447b765b3bcb")).WithObjectTypes(I1, C1).WithSingularName("C1one2many").WithPluralName("C1one2manies").WithCardinality(Cardinalities.OneToMany).Build();
            new RelationTypeBuilder(domain, new Guid("fe51c02e-ed28-4628-9da1-7bc2131c8992")).WithObjectTypes(I1, S2).WithSingularName("S2many2one").WithPluralName("S2many2ones").WithCardinality(Cardinalities.ManyToOne).Build();
            
            return domain;
        }
    }
}