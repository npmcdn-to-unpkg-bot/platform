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
        public static Domain BaseTest(MetaPopulation env)
        {
            // Imports
            // Core
            var allorsString = (Unit)env.Find(UnitIds.StringId);
            var allorsInteger = (Unit)env.Find(UnitIds.IntegerId);
            var allorsLong = (Unit)env.Find(UnitIds.LongId);
            var allorsDecimal = (Unit)env.Find(UnitIds.DecimalId);
            var allorsDouble = (Unit)env.Find(UnitIds.DoubleId);
            var allorsBoolean = (Unit)env.Find(UnitIds.BooleanId);
            var allorsDateTime = (Unit)env.Find(UnitIds.DatetimeId);
            var allorsUnique = (Unit)env.Find(UnitIds.Unique);
            var allorsBinary = (Unit)env.Find(UnitIds.BinaryId);

            // Base
            var @base = (Domain)env.Find(new Guid("CA802192-8186-4C2A-8315-A8DEFAA74A12"));

            var stringTemplate = (Class)env.Find(new Guid("0c50c02a-cc9c-4617-8530-15a24d4ac969"));
            var printable = (Interface)env.Find(new Guid("61207a42-3199-4249-baa4-9dd11dc0f5b1"));
            var searchResult = (Interface)env.Find(new Guid("a0ac7040-6984-4267-a200-919875e08909"));
            var enumeration = (Interface)env.Find(new Guid("b7bcc22f-03f0-46fd-b738-4e035921d445"));
            var country = (Class)env.Find(new Guid("c22bf60e-6428-4d10-8194-94f7be396f28"));
            var person = (Class)env.Find(new Guid("c799ca62-a554-467d-9aa2-1663293bb37f"));
            var media = (Class)env.Find(new Guid("da5b86a3-4f33-4c0d-965d-f4fbc1179374"));
            var searchable = (Interface)env.Find(new Guid("ff34f3f1-6a17-404f-a9e5-5cffcdaa3d31"));
            var uniquelyIdentifiable = (Interface)env.Find(new Guid("122ccfe1-f902-44c1-9d6c-6f6a0afa9469"));
            var singleton = (Class)env.Find(new Guid("313b97a5-328c-4600-9dd2-b5bc146fb13b"));
            var accessControlledObject = (Interface)env.Find(new Guid("eb0ff756-3e3d-4cf9-8935-8802a73d2df2"));
            var userInterfaceable = (Interface)env.Find(new Guid("eea17b39-8912-40b3-8403-293bd5a3316d"));

            // BaseTest
            var domain = new Domain(env, new Guid("47636693-E55F-4ED3-93B6-3D75F11D5D4B")) { Name = "BaseTest" };
            domain.AddDirectSuperdomain(@base);

            // ObjectTypes
            var c1 = new ClassBuilder(domain, new Guid("7041c691-d896-4628-8f50-1c24f5d03414")).WithSingularName("C1").WithPluralName("C1s").Build();
            var c2 = new ClassBuilder(domain, new Guid("72c07e8a-03f5-4da8-ab37-236333d4f74e")).WithSingularName("C2").WithPluralName("C2s").Build();
            var i1 = new InterfaceBuilder(domain, new Guid("fefcf1b6-ac8f-47b0-bed5-939207a2833e")).WithSingularName("I1").WithPluralName("I1s").Build();
            var i2 = new InterfaceBuilder(domain, new Guid("19bb2bc3-d53a-4d15-86d0-b250fdbcb0a0")).WithSingularName("I2").WithPluralName("I2s").Build();
            var i12 = new InterfaceBuilder(domain, new Guid("b45ec13c-704f-413d-a662-bdc59a17bfe3")).WithSingularName("I12").WithPluralName("I12s").Build();

            var derivationLogC1 = new ClassBuilder(domain, new Guid("2361c456-b624-493a-8377-2dd1e697e17a")).WithSingularName("DerivationLogC1").WithPluralName("DerivationLogC1s").Build();
            var derivationLogC2 = new ClassBuilder(domain, new Guid("c7563dd3-77b2-43ff-92f9-a4f98db36acf")).WithSingularName("DerivationLogC2").WithPluralName("DerivationLogC2s").Build();
            var derivationLogI12 = new InterfaceBuilder(domain, new Guid("d61872ee-3778-47e8-8931-003f3f48cbc5")).WithSingularName("DerivationLogI12").WithPluralName("DerivationLogI12s").Build();
            
            var dependent = new ClassBuilder(domain, new Guid("0cb8d2a7-4566-432f-9882-893b05a77f44")).WithSingularName("Dependent").WithPluralName("Dependents").Build();
            var four = new ClassBuilder(domain, new Guid("1248e212-ca71-44aa-9e87-6e83dae9d4fd")).WithSingularName("Four").WithPluralName("Fours").Build();
            var address = new InterfaceBuilder(domain, new Guid("130aa2ff-4f14-4ad7-8a27-f80e8aebfa00")).WithSingularName("Address").WithPluralName("Addresses").Build();
            var first = new ClassBuilder(domain, new Guid("1937b42e-954b-4ef9-bc63-5b8ae7903e9d")).WithSingularName("First").WithPluralName("Firsts").Build();
            var homeAddress = new ClassBuilder(domain, new Guid("2561e93c-5b85-44fb-a924-a1c0d1f78846")).WithSingularName("HomeAddress").WithPluralName("HomeAddresses").Build();
            var place = new ClassBuilder(domain, new Guid("268f63d2-17da-4f29-b0d0-76db611598c6")).WithSingularName("Place").WithPluralName("Places").Build();
            var gender = new ClassBuilder(domain, new Guid("270f0dc8-1bc2-4a42-9617-45e93d5403c8")).WithSingularName("Gender").WithPluralName("Genders").Build();
            var dependee = new ClassBuilder(domain, new Guid("2cc9bde1-80da-4159-bb20-219074266101")).WithSingularName("Dependee").WithPluralName("Dependees").Build();
            var third = new ClassBuilder(domain, new Guid("39116edf-34cf-45a6-ac09-2e4f98f28e14")).WithSingularName("Third").WithPluralName("Thirds").Build();
            var organisation = new ClassBuilder(domain, new Guid("3a5dcec7-308f-48c7-afee-35d38415aa0b")).WithSingularName("Organisation").WithPluralName("Organisations").Build();
            var subdependee = new ClassBuilder(domain, new Guid("46a437d1-455b-4ddd-b83c-068938c352bd")).WithSingularName("Subdependee").WithPluralName("Subdependees").Build();
            var unit = new ClassBuilder(domain, new Guid("4e501cd6-807c-4f10-b60b-acd1d80042cd")).WithSingularName("Unit").WithPluralName("Units").Build();
            var shared = new InterfaceBuilder(domain, new Guid("5c3876c3-c3be-46aa-a598-a68b964d329e")).WithSingularName("Shared").WithPluralName("Shareds").Build();
            var one = new ClassBuilder(domain, new Guid("5d9b9cad-3720-47c3-9693-289698bf3dd0")).WithSingularName("One").WithPluralName("Ones").Build();
            var @from = new ClassBuilder(domain, new Guid("6217b428-4ad0-4f7f-ad4b-e334cf0b3ab1")).WithSingularName("From").WithPluralName("Froms").Build();
            var statefulCompany = new ClassBuilder(domain, new Guid("62859bfb-7949-4f7f-a428-658447576d0a")).WithSingularName("StatefulCompany").WithPluralName("StatefulCompanies").Build();
            var to = new ClassBuilder(domain, new Guid("7eb25112-4b81-4e8d-9f75-90950c40c65f")).WithSingularName("To").WithPluralName("Tos").Build();
            var mailboxAddress = new ClassBuilder(domain, new Guid("7ee3b00b-4e63-4774-b744-3add2c6035ab")).WithSingularName("MailboxAddress").WithPluralName("MailboxAddresses").Build();
            var extender = new ClassBuilder(domain, new Guid("830cdcb1-31f1-4481-8399-00c034661450")).WithSingularName("Extender").WithPluralName("Extenders").Build();
            var two = new ClassBuilder(domain, new Guid("9ec7e136-815c-4726-9991-e95a3ec9e092")).WithSingularName("Two").WithPluralName("Twos").Build();
            var searchTest = new ClassBuilder(domain, new Guid("a881caf5-e4e6-49d3-87ee-512c0afed279")).WithSingularName("SearchTest").WithPluralName("SearchTests").Build();
            var badUi = new ClassBuilder(domain, new Guid("bb1b0a2e-66d1-4e09-860f-52dc7145029e")).WithSingularName("BadUI").WithPluralName("BadUIs").Build();
            var three = new ClassBuilder(domain, new Guid("bdaed62e-6369-46c0-a379-a1eef81b1c3d")).WithSingularName("Three").WithPluralName("Threes").Build();
            var second = new ClassBuilder(domain, new Guid("c1f169a1-553b-4a24-aba7-01e0b7102fe5")).WithSingularName("Second").WithPluralName("Seconds").Build();
            var classWithoutRoles = new ClassBuilder(domain, new Guid("e1008840-6d7c-4d44-b2ad-1545d23f90d8")).WithSingularName("ClassWithoutRoles").WithPluralName("ClassesWithoutRoles").Build();
            
            // Inheritances
            // C1
            new InheritanceBuilder(domain, new Guid("2d0db6cd-5837-4bbd-ad9e-9203a6cc7c61")).WithSubtype(c1).WithSupertype(i1).Build();

            // C2
            new InheritanceBuilder(domain, new Guid("07e545fb-5678-43da-a59e-65f48a9e88ed")).WithSubtype(c2).WithSupertype(i2).Build();

            // I1
            new InheritanceBuilder(domain, new Guid("4A33C229-5E53-4BAC-AFD2-B317A1400978")).WithSubtype(i1).WithSupertype(i12).Build();

            // I2
            new InheritanceBuilder(domain, new Guid("bdd512e8-b9dc-4a79-817e-58657b1d62e4")).WithSubtype(i2).WithSupertype(i12).Build();

            // I12
            new InheritanceBuilder(domain, new Guid("F56F2502-0394-4103-9C70-D66D56408EDD")).WithSubtype(i12).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("a71cee9c-3f73-4be5-941c-3f86d9fb0e07")).WithSubtype(i12).WithSupertype(accessControlledObject).Build();
            new InheritanceBuilder(domain, new Guid("da105c1e-e19b-438a-bfc0-b845fb81864c")).WithSubtype(i12).WithSupertype(searchable).Build();

            // Four
            new InheritanceBuilder(domain, new Guid("1a077ff9-b309-4982-8d79-2b176394eee4")).WithSubtype(four).WithSupertype(shared).Build();
            new InheritanceBuilder(domain, new Guid("eb8b60d0-fb51-43f8-8b4d-5086d23540c7")).WithSubtype(four).WithSupertype(userInterfaceable).Build();

            // Address
            new InheritanceBuilder(domain, new Guid("f4ffbc32-e608-463f-80e3-8c727796bcd1")).WithSubtype(address).WithSupertype(userInterfaceable).Build();

            // DerivationLogC1
            new InheritanceBuilder(domain, new Guid("e1abeb9f-d257-409c-8c1a-b79e3193f050")).WithSubtype(derivationLogC1).WithSupertype(derivationLogI12).Build();

            // HomeAddress
            new InheritanceBuilder(domain, new Guid("494ef8df-18ca-4399-811d-9c78cb3ae1f2")).WithSubtype(homeAddress).WithSupertype(searchable).Build();
            new InheritanceBuilder(domain, new Guid("ab97d574-18bc-45cd-881d-87e2b024ceef")).WithSubtype(homeAddress).WithSupertype(address).Build();

            // Place
            new InheritanceBuilder(domain, new Guid("988c1ffa-44bc-4171-84a0-b621328f71ad")).WithSubtype(place).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("d5c3e9b1-dd7b-4952-ac42-d200deb0412a")).WithSubtype(place).WithSupertype(searchable).Build();

            // Gender
            new InheritanceBuilder(domain, new Guid("2c5e6389-9a31-4ac8-aeeb-9e9a1b8f98a1")).WithSubtype(gender).WithSupertype(enumeration).Build();
            
            // Organisation
            new InheritanceBuilder(domain, new Guid("1206d70d-b5d5-42e8-b2e6-3a155044aa29")).WithSubtype(organisation).WithSupertype(searchable).Build();
            new InheritanceBuilder(domain, new Guid("2324bc9f-79a1-44f7-9041-00ed74e789e3")).WithSubtype(organisation).WithSupertype(uniquelyIdentifiable).Build();
            new InheritanceBuilder(domain, new Guid("390c3e64-c0f6-469b-81aa-d45254d15be8")).WithSubtype(organisation).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("eac51e90-62c9-4929-b46d-e20c4999f734")).WithSubtype(organisation).WithSupertype(searchResult).Build();

            // Unit
            new InheritanceBuilder(domain, new Guid("3f713f76-d79f-477d-adff-a6b438df4c5e")).WithSubtype(unit).WithSupertype(accessControlledObject).Build();
            new InheritanceBuilder(domain, new Guid("89cafe0f-0ea2-40c6-b709-c366a5a79e07")).WithSubtype(unit).WithSupertype(userInterfaceable).Build();

            // Shared
            new InheritanceBuilder(domain, new Guid("4f60cdcf-c611-482d-877f-5c76cf723a78")).WithSubtype(shared).WithSupertype(userInterfaceable).Build();

            // One
            new InheritanceBuilder(domain, new Guid("ae3ba09f-3c0f-4dc8-8147-1fed71aa96be")).WithSubtype(one).WithSupertype(shared).Build();
            new InheritanceBuilder(domain, new Guid("e8e3dacf-f084-445d-9652-470212739a14")).WithSubtype(one).WithSupertype(userInterfaceable).Build();

            // From
            new InheritanceBuilder(domain, new Guid("27d445e8-5dad-499b-8904-cb2383582f0e")).WithSubtype(@from).WithSupertype(userInterfaceable).Build();

            // To
            new InheritanceBuilder(domain, new Guid("ffc0a33a-9859-4c02-ba6e-d5fa41c3dcab")).WithSubtype(to).WithSupertype(userInterfaceable).Build();

            // MailboxAddress
            new InheritanceBuilder(domain, new Guid("543f3140-e739-4173-aa1e-a06e1282e629")).WithSubtype(mailboxAddress).WithSupertype(searchable).Build();
            new InheritanceBuilder(domain, new Guid("c849f4f5-f4ec-45d0-a384-d94a997c854d")).WithSubtype(mailboxAddress).WithSupertype(address).Build();

            // Extender
            // Two
            new InheritanceBuilder(domain, new Guid("7d57d7cb-25ec-4252-8e4f-732fa644ac2e")).WithSubtype(two).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("a28aef2a-8dff-4427-b132-2161894e1886")).WithSubtype(two).WithSupertype(shared).Build();

            // SearchTest
            new InheritanceBuilder(domain, new Guid("1367b4d9-1847-43a1-ad3d-e4db7fa0e276")).WithSubtype(searchTest).WithSupertype(searchable).Build();
            new InheritanceBuilder(domain, new Guid("8217c5cb-661d-42d8-b133-a9926fffc245")).WithSubtype(searchTest).WithSupertype(userInterfaceable).Build();

            // BadUI
            new InheritanceBuilder(domain, new Guid("645bf04f-a018-4ee7-a624-45a10b3ec771")).WithSubtype(badUi).WithSupertype(userInterfaceable).Build();

            // Three
            new InheritanceBuilder(domain, new Guid("0e2ff702-d2fe-4298-b97d-ab9d7bec94b3")).WithSubtype(three).WithSupertype(shared).Build();
            new InheritanceBuilder(domain, new Guid("d93cdcce-1d39-49d9-a577-928f5eb567f3")).WithSubtype(three).WithSupertype(userInterfaceable).Build();

            // Second
            // DerivationLogC2
            new InheritanceBuilder(domain, new Guid("f3e0444d-8b32-4396-80c4-b07905563c17")).WithSubtype(derivationLogC2).WithSupertype(derivationLogI12).Build();

            // DerivationLogI12
            // ClassWithoutRoles
            new InheritanceBuilder(domain, new Guid("05271ee7-baee-4272-a7c2-ab03de1da03d")).WithSubtype(classWithoutRoles).WithSupertype(userInterfaceable).Build();

            // Person
            new InheritanceBuilder(domain, new Guid("19848b71-0042-40c2-8e88-d44db074bf5a")).WithSubtype(person).WithSupertype(printable).Build();
            
            // RelationTypes
            // Dependent
            new RelationTypeBuilder(domain, new Guid("8859af04-ba38-42ce-8ac9-f428c3f92f31")).WithObjectTypes(dependent, dependee).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("9884955e-74ed-4f9d-9362-8e0274c53bf9")).WithObjectTypes(dependent, allorsInteger).WithSingularName("Counter").WithPluralName("Counters").Build();
            new RelationTypeBuilder(domain, new Guid("e971733a-c381-4b5e-8e62-6bbd6d285bd7")).WithObjectTypes(dependent, allorsInteger).WithSingularName("Subcounter").WithPluralName("Subcounters").Build();

            // Address
            new RelationTypeBuilder(domain, new Guid("36e7d935-a9c7-484d-8551-9bdc5bdeab68")).WithObjectTypes(address, place).WithSingularName("Place").WithPluralName("Places").WithCardinality(Cardinalities.ManyToOne).WithIsIndexed(true).Build();

            // First
            new RelationTypeBuilder(domain, new Guid("24886999-11f0-408f-b094-14b36ac4129b")).WithObjectTypes(first, second).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("b0274351-3403-4384-afb6-2cb49cd03893")).WithObjectTypes(first, allorsBoolean).WithSingularName("CreateCycle").WithPluralName("CreateCycles").Build();
            new RelationTypeBuilder(domain, new Guid("f2b61dd5-d30c-445a-ae7a-af1c0cc8e278")).WithObjectTypes(first, allorsBoolean).WithSingularName("IsDerived").WithPluralName("IsDeriveds").Build();

            // I2
            new RelationTypeBuilder(domain, new Guid("01d9ff41-d503-421e-93a6-5563e1787543")).WithObjectTypes(i2, i2).WithSingularName("I2Many2One").WithPluralName("I2Many2Ones").WithCardinality(Cardinalities.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("1f763206-c575-4e34-9e6b-997d434d3f42")).WithObjectTypes(i2, c1).WithSingularName("C1Many2One").WithPluralName("C1Many2Ones").WithCardinality(Cardinalities.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("23e9c15f-097f-4452-9bac-d7cf2a65134a")).WithObjectTypes(i2, i12).WithSingularName("I12Many2One").WithPluralName("I12Many2Ones").WithCardinality(Cardinalities.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("35040d7c-ab7f-4a99-9d09-e01e24ca3cb9")).WithObjectTypes(i2, allorsBoolean).Build();
            new RelationTypeBuilder(domain, new Guid("40b8edb3-e8c4-46c0-855b-4b18e0e8d7f3")).WithObjectTypes(i2, c1).WithSingularName("C1One2Many").WithPluralName("C1One2Manies").WithCardinality(Cardinalities.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("49736daf-d0bd-4216-97fa-958cfa21a4f0")).WithObjectTypes(i2, c1).WithSingularName("C1One2One").WithPluralName("C1One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("4f095abd-8803-4610-87f0-2847ddd5e9f4")).WithObjectTypes(i2, allorsDecimal).WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("5ebbc734-23dd-494f-af2d-8e75caaa3e26")).WithObjectTypes(i2, i2).WithSingularName("I2Many2any").WithPluralName("I2Many2Manies").WithCardinality(Cardinalities.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("62a8a93d-3744-49de-9f9a-9997b6ef4da6")).WithObjectTypes(i2, allorsBinary).WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("663559c4-ef64-4e78-89b4-bfa00691c627")).WithObjectTypes(i2, allorsUnique).Build();
            new RelationTypeBuilder(domain, new Guid("6bb406bc-627b-444c-9c16-df9878e05e9c")).WithObjectTypes(i2, i1).WithSingularName("I1Many2One").WithPluralName("I1Many2Ones").WithCardinality(Cardinalities.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("83dc0581-e04a-4f51-a44e-4fef63d44356")).WithObjectTypes(i2, i12).WithSingularName("I12One2Many").WithPluralName("I12One2Manies").WithCardinality(Cardinalities.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("87499e99-ed77-44c1-89d6-b4f570b6f217")).WithObjectTypes(i2, i12).WithSingularName("I12One2One").WithPluralName("I12One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("92fdb313-0b90-48f6-b054-a4ab38f880ba")).WithObjectTypes(i2, c2).WithSingularName("C2Many2Many").WithPluralName("C2Many2Manies").WithCardinality(Cardinalities.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("9bed0518-1946-4e23-9d4b-e4cda439984c")).WithObjectTypes(i2, i1).WithSingularName("I1Many2Many").WithPluralName("I1Many2Manies").WithCardinality(Cardinalities.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("9f361b97-0b04-496d-ac60-718760c2a4e2")).WithObjectTypes(i2, c2).WithSingularName("C2Many2One").WithPluralName("C2Many2Ones").WithCardinality(Cardinalities.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("9f91841c-f63f-4ffa-bee6-62e100f3cd15")).WithObjectTypes(i2, allorsString).WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("b39fdd23-d7dd-473f-9705-df2f29be5ffe")).WithObjectTypes(i2, c2).WithSingularName("C2One2Many").WithPluralName("C2One2Manies").WithCardinality(Cardinalities.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("b640bf16-0dc0-4203-aa76-f456371239ae")).WithObjectTypes(i2, i1).WithSingularName("I1One2One").WithPluralName("I1One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("b6d44d67-3a51-482c-88d1-a4917ca2a065")).WithObjectTypes(i2, allorsDateTime).Build();
            new RelationTypeBuilder(domain, new Guid("bbb01166-2671-4ca1-8b1e-12e6ae8aeb03")).WithObjectTypes(i2, i1).WithSingularName("I1One2Many").WithPluralName("I1One2Manies").WithCardinality(Cardinalities.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("cb9f21e0-a841-45de-8ba4-991b4ceca616")).WithObjectTypes(i2, i12).WithSingularName("I12Many2Many").WithPluralName("I12Many2Manies").WithCardinality(Cardinalities.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("cc4c704c-ab7e-45d4-baa9-b67cfff9448e")).WithObjectTypes(i2, i2).WithSingularName("I2One2One").WithPluralName("I2One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("d0c49b92-a108-48b5-bc95-72d2e6109ad2")).WithObjectTypes(i2, allorsLong).Build();
            new RelationTypeBuilder(domain, new Guid("d30dd036-6d28-48df-873b-3a76da8c029e")).WithObjectTypes(i2, allorsInteger).Build();
            new RelationTypeBuilder(domain, new Guid("deb9cbd3-386f-4599-802c-be50945b9f1d")).WithObjectTypes(i2, i2).WithSingularName("I2One2Many").WithPluralName("I2One2Manies").WithCardinality(Cardinalities.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("f364c9fe-ad36-4305-80fd-4921451c70a5")).WithObjectTypes(i2, c1).WithSingularName("C1Many2Many").WithPluralName("C1Many2Manies").WithCardinality(Cardinalities.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("f85c2d97-10b9-478d-9b82-2700d95d5cb1")).WithObjectTypes(i2, c2).WithSingularName("C2One2One").WithPluralName("C2One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("fbad33e7-ede1-41fc-97e9-ddf33a0f6459")).WithObjectTypes(i2, allorsDouble).Build();

            // HomeAddress
            new RelationTypeBuilder(domain, new Guid("6f0f42c4-9b47-47c2-a632-da8e08116be4")).WithObjectTypes(homeAddress, allorsString).WithSingularName("Street").WithPluralName("Street").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("b181d077-e897-4add-9456-67b9760d32e8")).WithObjectTypes(homeAddress, allorsString).WithSingularName("HouseNumber").WithPluralName("HouseNumbers").WithSize(256).Build();

            // Place
            new RelationTypeBuilder(domain, new Guid("1bf1cc1e-75bf-4a3f-87bd-a2fae2697855")).WithObjectTypes(place, country).WithCardinality(Cardinalities.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("d029f486-4bb8-43a1-8356-98b9bee10de4")).WithObjectTypes(place, allorsString).WithSingularName("City").WithPluralName("Cities").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("d80d7c6a-138a-43dd-9748-8ffb89b1dabb")).WithObjectTypes(place, allorsString).WithSingularName("PostalCode").WithPluralName("PostalCodes").WithSize(256).Build();

            // Dependee
            new RelationTypeBuilder(domain, new Guid("1b8e0350-c446-48dc-85c0-71130cc1490e")).WithObjectTypes(dependee, subdependee).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("c1e86449-e5a8-4911-97c7-b03de9142f98")).WithObjectTypes(dependee, allorsInteger).WithSingularName("Subcounter").WithPluralName("Subcounters").Build();
            new RelationTypeBuilder(domain, new Guid("d58d1f28-3abd-4294-abde-885bdd16f466")).WithObjectTypes(dependee, allorsInteger).WithSingularName("Counter").WithPluralName("Counters").Build();
            new RelationTypeBuilder(domain, new Guid("e73b8fc5-0148-486a-9379-cfb051b303d2")).WithObjectTypes(dependee, allorsBoolean).WithSingularName("DeleteDependent").WithPluralName("DeleteDependents").Build();
            
            // Third
            new RelationTypeBuilder(domain, new Guid("6ab5a7af-a0f0-4940-9be3-6f6430a9e728")).WithObjectTypes(third, allorsBoolean).WithSingularName("IsDerived").WithPluralName("IsDeriveds").Build();

            // Organisation
            new RelationTypeBuilder(domain, new Guid("15f33fa4-c878-45a0-b40c-c5214bce350b")).WithObjectTypes(organisation, person).WithSingularName("Shareholder").WithPluralName("Shareholders").WithCardinality(Cardinalities.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("2cc74901-cda5-4185-bcd8-d51c745a8437")).WithObjectTypes(organisation, allorsString).WithSingularName("Name").WithPluralName("Names").WithIsIndexed(true).WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("2cfea5d4-e893-4264-a966-a68716839acd")).WithObjectTypes(organisation, allorsString).WithSingularName("Description").WithPluralName("Descriptions").WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("49b96f79-c33d-4847-8c64-d50a6adb4985")).WithObjectTypes(organisation, person).WithSingularName("Employee").WithPluralName("Employees").WithCardinality(Cardinalities.OneToMany).Build();
            new RelationTypeBuilder(domain, new Guid("5fa25b53-e2a7-44c8-b6ff-f9575abb911d")).WithObjectTypes(organisation, allorsBoolean).WithSingularName("Incorporated").WithPluralName("Incorporateds").Build();
            new RelationTypeBuilder(domain, new Guid("68c61cea-4e6e-4ed5-819b-7ec794a10870")).WithObjectTypes(organisation, allorsBoolean).WithSingularName("IsSupplier").WithPluralName("AreSupplier").Build();
            new RelationTypeBuilder(domain, new Guid("7046c2b4-d458-4343-8446-d23d9c837c84")).WithObjectTypes(organisation, allorsDateTime).WithSingularName("IncorporationDate").WithPluralName("IncorporationDates").Build();
            new RelationTypeBuilder(domain, new Guid("73f23588-1444-416d-b43c-b3384ca87bfc")).WithObjectTypes(organisation, address).WithSingularName("Address").WithPluralName("Addresses").WithCardinality(Cardinalities.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("845ff004-516f-4ad5-9870-3d0e966a9f7d")).WithObjectTypes(organisation, person).WithSingularName("Owner").WithPluralName("Owners").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("b201d2a0-2335-47a1-aa8d-8416e89a9fec")).WithObjectTypes(organisation, media).WithSingularName("Logo").WithPluralName("Logos").WithCardinality(Cardinalities.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("bac702b8-7874-45c3-a410-102e1caea4a7")).WithObjectTypes(organisation, allorsString).WithSingularName("Size").WithPluralName("Sizes").WithSize(256).Build();

            // Subdependee
            new RelationTypeBuilder(domain, new Guid("194930f9-9c3f-458d-93ec-3d7bea4cd538")).WithObjectTypes(subdependee, allorsInteger).WithSingularName("Subcounter").WithPluralName("Subcounters").Build();

            // Unit
            new RelationTypeBuilder(domain, new Guid("09bfade2-1739-4a7b-80ec-982fe43428ce")).WithObjectTypes(unit, allorsLong).Build();
            new RelationTypeBuilder(domain, new Guid("24771d5b-f920-4820-aff7-ea6391b4a45c")).WithObjectTypes(unit, allorsBinary).WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("4d6a80f5-0fa7-4867-91f8-37aa92b6707b")).WithObjectTypes(unit, allorsDateTime).Build();
            new RelationTypeBuilder(domain, new Guid("5a788ebe-65e9-4d5e-853a-91bb4addabb5")).WithObjectTypes(unit, allorsBoolean).Build();
            new RelationTypeBuilder(domain, new Guid("74a35820-ef8c-4373-9447-6215ee8279c0")).WithObjectTypes(unit, allorsDouble).Build();
            new RelationTypeBuilder(domain, new Guid("b817ba76-876e-44ea-8e5a-51d552d4045e")).WithObjectTypes(unit, allorsInteger).Build();
            new RelationTypeBuilder(domain, new Guid("c724c733-972a-411c-aecb-e865c2628a90")).WithObjectTypes(unit, allorsString).WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("ed58ae4c-24e0-4dd1-8b1c-0909df1e0fcd")).WithObjectTypes(unit, allorsUnique).Build();
            new RelationTypeBuilder(domain, new Guid("f746da51-ea2d-4e22-9ecb-46d4dbc1b084")).WithObjectTypes(unit, allorsDecimal).WithPrecision(19).WithScale(2).Build();

            // Shared
            // One
            new RelationTypeBuilder(domain, new Guid("448878af-c992-4256-baa7-239335a26bc6")).WithObjectTypes(one, two).WithCardinality(Cardinalities.ManyToOne).WithIsIndexed(true).Build();

            // From
            new RelationTypeBuilder(domain, new Guid("d9a9896d-e175-410a-9916-9261d83aa229")).WithObjectTypes(@from, to).WithSingularName("To").WithPluralName("Tos").WithCardinality(Cardinalities.OneToMany).WithIsIndexed(true).Build();

            // StatefulCompany
            new RelationTypeBuilder(domain, new Guid("6c848eeb-7b42-45ea-81ac-fa983e1e0fa9")).WithObjectTypes(statefulCompany, person).WithSingularName("Employee").WithPluralName("Employees").WithCardinality(Cardinalities.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("6e429d87-ea80-465e-9aa6-0f7d546b6bb3")).WithObjectTypes(statefulCompany, allorsString).WithSingularName("Name").WithPluralName("Names").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("9940e8ed-189e-42c6-b0d1-7c01920b9fac")).WithObjectTypes(statefulCompany, person).WithSingularName("Manager").WithPluralName("Managers").WithCardinality(Cardinalities.ManyToOne).WithIsIndexed(true).Build();

            // C1
            new RelationTypeBuilder(domain, new Guid("0e7f529b-bc91-4a40-a7e7-a17341c6bf5b")).WithObjectTypes(c1, i1).WithSingularName("I1One2One").WithPluralName("I1One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("20713860-8abd-4d71-8ccc-2b4d1b88bce3")).WithObjectTypes(c1, allorsString).WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("5490dc63-a8f6-4a86-91ef-fef97a86f119")).WithObjectTypes(c1, c2).WithSingularName("C2Many2One").WithPluralName("C2Many2Ones").WithCardinality(Cardinalities.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("6def7988-4bcf-4964-9de6-c6ede41d5e5a")).WithObjectTypes(c1, i2).WithSingularName("I2One2One").WithPluralName("I2One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("79c00218-bb4f-40e9-af7d-61af444a4a54")).WithObjectTypes(c1, c1).WithSingularName("C1One2One").WithPluralName("C1One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("7bb216f2-8e9c-4dcd-890b-579130ab0a8b")).WithObjectTypes(c1, i1).WithSingularName("I1Many2One").WithPluralName("I1Many2Ones").WithCardinality(Cardinalities.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("815878f6-16f2-42f2-9b24-f394ddf789c2")).WithObjectTypes(c1, i1).WithSingularName("I1Many2Many").WithPluralName("I1Many2Manies").WithCardinality(Cardinalities.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("82f5fb26-c260-41bc-a784-a2d5e35243bd")).WithObjectTypes(c1, i2).WithSingularName("I2One2Many").WithPluralName("I2One2Manies").WithCardinality(Cardinalities.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("87eb0d19-73a7-4aae-aeed-66dc9163233c")).WithObjectTypes(c1, allorsDecimal).WithPrecision(10).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("8c198447-e943-4f5a-b749-9534b181c664")).WithObjectTypes(c1, c1).WithSingularName("C1Many2Many").WithPluralName("C1Many2Manies").WithCardinality(Cardinalities.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("91e8b23b-48fb-4d20-8a71-89c5630f1c78")).WithObjectTypes(c1, allorsLong).Build();
            new RelationTypeBuilder(domain, new Guid("94a2b37d-9431-4496-b992-630cda5b9851")).WithObjectTypes(c1, i12).WithSingularName("I12Many2Many").WithPluralName("I12Many2Manies").WithCardinality(Cardinalities.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("97f31053-0e7b-42a0-90c2-ce6f09c56e86")).WithObjectTypes(c1, allorsBinary).WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("98c5f58b-1777-4d9a-8828-37dbf7051510")).WithObjectTypes(c1, i12).WithSingularName("I12One2Many").WithPluralName("I12One2Manies").WithCardinality(Cardinalities.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("9f6538c2-e6dd-4c27-80ed-2748f645cb95")).WithObjectTypes(c1, c2).WithSingularName("C2One2Many").WithPluralName("C2One2Manies").WithCardinality(Cardinalities.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("a0ac5a65-2cbd-4c51-9417-b10150bc5699")).WithObjectTypes(c1, c1).WithSingularName("C1One2Many").WithPluralName("C1One2Manies").WithCardinality(Cardinalities.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("a64abd21-dadf-483d-9499-d19aa8e33791")).WithObjectTypes(c1, allorsString).WithSingularName("AllorsStringMax").WithPluralName("AllorsStringsMax").WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("a8e18ea7-cbf2-4ea7-ae14-9f4bcfdb55de")).WithObjectTypes(c1, c1).WithSingularName("C1Many2One").WithPluralName("C1Many2Ones").WithCardinality(Cardinalities.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("b4ee673f-bba0-4e24-9cda-3cf993c79a0a")).WithObjectTypes(c1, allorsBoolean).Build();
            new RelationTypeBuilder(domain, new Guid("b9f2c4c7-6979-40cf-82a2-fa99a5d9e9a4")).WithObjectTypes(c1, i12).WithSingularName("I12One2One").WithPluralName("I12One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("bcf4df45-6616-4cdf-8ada-f944f9c7ff1a")).WithObjectTypes(c1, i12).WithSingularName("I12Many2One").WithPluralName("I12Many2Ones").WithCardinality(Cardinalities.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("cda97972-84c8-48e3-99d8-fd7c99c5dbc9")).WithObjectTypes(c1, i2).WithSingularName("I2Many2Many").WithPluralName("I2Many2Manies").WithCardinality(Cardinalities.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("cef13620-b7d7-4bfe-8d3b-c0f826da5989")).WithObjectTypes(c1, allorsUnique).Build();
            new RelationTypeBuilder(domain, new Guid("d0341bed-2732-4bcb-b1bb-9f9589de5d03")).WithObjectTypes(c1, i2).WithSingularName("I2Many2One").WithPluralName("I2Many2Ones").WithCardinality(Cardinalities.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("e0656d9a-75a6-4e59-aaa1-3ff03d440059")).WithObjectTypes(c1, i1).WithSingularName("I1One2Many").WithPluralName("I1One2Manies").WithCardinality(Cardinalities.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("e97fc754-c736-4359-9662-19dce9429f89")).WithObjectTypes(c1, c2).WithSingularName("C2One2One").WithPluralName("C2One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("ef75cc4e-8787-4f1c-ae5c-73577d721467")).WithObjectTypes(c1, allorsDateTime).Build();
            new RelationTypeBuilder(domain, new Guid("f268783d-42ed-41c1-b0b0-b8a60e30a601")).WithObjectTypes(c1, allorsDouble).Build();
            new RelationTypeBuilder(domain, new Guid("f29d4a52-9ba5-40f6-ba99-050cbd03e554")).WithObjectTypes(c1, c2).WithSingularName("C2Many2Many").WithPluralName("C2Many2Manies").WithCardinality(Cardinalities.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("f4920d94-8cd0-45b6-be00-f18d377368fd")).WithObjectTypes(c1, allorsInteger).WithIsIndexed(true).Build();

            // C2
            new RelationTypeBuilder(domain, new Guid("07eaa992-322a-40e9-bf2c-aa33b69f54cd")).WithObjectTypes(c2, allorsDecimal).WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("0c8209e3-b2fc-4c7a-acd2-6b5b8ac89bf4")).WithObjectTypes(c2, c1).WithSingularName("C1One2One").WithPluralName("C1One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("12896fc2-c9e9-4a89-b875-0aeb92e298e5")).WithObjectTypes(c2, c2).WithSingularName("C2Many2One").WithPluralName("C2Many2Ones").WithCardinality(Cardinalities.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("1444d919-6ca1-4642-8d18-9d955c817581")).WithObjectTypes(c2, allorsUnique).Build();
            new RelationTypeBuilder(domain, new Guid("165cc83e-935d-4d0d-aec7-5da155300086")).WithObjectTypes(c2, i12).WithSingularName("I12Many2One").WithPluralName("I12Many2Ones").WithCardinality(Cardinalities.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("1d0c57c9-a3d1-4134-bc7d-7bb587d8250f")).WithObjectTypes(c2, i12).WithSingularName("I12One2One").WithPluralName("I12One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("1d98eda7-6dba-43f1-a5ce-44f7ed104cf9")).WithObjectTypes(c2, i1).WithSingularName("I1Many2Many").WithPluralName("I1Many2Manies").WithCardinality(Cardinalities.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("262ad367-a52c-4d8b-94e2-b477bb098423")).WithObjectTypes(c2, allorsDouble).Build();
            new RelationTypeBuilder(domain, new Guid("2ac55066-c748-4f90-9d0f-1090fe02cc76")).WithObjectTypes(c2, i1).WithSingularName("I1One2Many").WithPluralName("I1One2Manies").WithCardinality(Cardinalities.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("38063edc-271a-410d-b857-807a9100c7b5")).WithObjectTypes(c2, i2).WithSingularName("I2One2One").WithPluralName("I2One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("41cd7805-0f93-460f-8572-9c479f3db206")).WithObjectTypes(c2, allorsLong).Build();
            new RelationTypeBuilder(domain, new Guid("42f9f4b6-3b35-4168-93cb-35171dbf83f4")).WithObjectTypes(c2, allorsInteger).Build();
            new RelationTypeBuilder(domain, new Guid("4a963639-72c3-4e9f-9058-bcfc8fe0bc9e")).WithObjectTypes(c2, i2).WithSingularName("I2Many2Many").WithPluralName("I2Many2Manies").WithCardinality(Cardinalities.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("50300577-b5fb-4c16-9ac5-41151543f958")).WithObjectTypes(c2, i12).WithSingularName("I12Many2Many").WithPluralName("I12Many2Manies").WithCardinality(Cardinalities.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("60680366-4790-4443-a941-b30cd4bd3848")).WithObjectTypes(c2, c2).WithSingularName("C2One2Many").WithPluralName("C2One2Manies").WithCardinality(Cardinalities.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("61daaaae-dd22-405e-aa98-6321d2f8af04")).WithObjectTypes(c2, allorsBoolean).Build();
            new RelationTypeBuilder(domain, new Guid("65a246a7-cd78-45eb-90db-39f542e7c6cf")).WithObjectTypes(c2, i1).WithSingularName("I1Many2One").WithPluralName("I1Many2Ones").WithCardinality(Cardinalities.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("67780894-fa62-48ba-8f47-7f54106090cd")).WithObjectTypes(c2, i1).WithSingularName("I1One2One").WithPluralName("I1One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("70600f67-7b18-4b5c-b11e-2ed180c5b2d6")).WithObjectTypes(c2, c1).WithSingularName("C1Many2Many").WithPluralName("C1Many2Manies").WithCardinality(Cardinalities.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("770eb33c-c8ef-4629-a3a0-20decd92ff62")).WithObjectTypes(c2, i12).WithSingularName("I12One2Many").WithPluralName("I12One2Manies").WithCardinality(Cardinalities.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("7a9129c9-7b6d-4bdd-a630-cfd1392549b7")).WithObjectTypes(c2, i2).WithSingularName("I2One2Many").WithPluralName("I2One2Manies").WithCardinality(Cardinalities.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("86ad371b-0afd-420b-a855-38ebb3f39f38")).WithObjectTypes(c2, c2).WithSingularName("C2One2One").WithPluralName("C2One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("9c7cde3f-9b61-4c79-a5d7-afe1067262ce")).WithObjectTypes(c2, allorsString).WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("a5315151-aa0f-42a3-9d5b-2c7f2cb92560")).WithObjectTypes(c2, c1).WithSingularName("C1Many2One").WithPluralName("C1Many2Ones").WithCardinality(Cardinalities.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("bc6c7fe0-6501-428c-a929-da87a9f4b885")).WithObjectTypes(c2, c2).WithSingularName("C2Many2Many").WithPluralName("C2Many2Manies").WithCardinality(Cardinalities.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("ce23482d-3a22-4202-98e7-5934fd9abd2d")).WithObjectTypes(c2, allorsDateTime).Build();
            new RelationTypeBuilder(domain, new Guid("e08d75a9-9b67-4d20-a476-757f8fb17376")).WithObjectTypes(c2, i2).WithSingularName("I2Many2One").WithPluralName("I2Many2Ones").WithCardinality(Cardinalities.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("f748949e-de5a-4f2e-85e2-e15516d9bf24")).WithObjectTypes(c2, c1).WithSingularName("C1One2Many").WithPluralName("C1One2Manies").WithCardinality(Cardinalities.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("fa8ad982-9953-47dd-9905-81cc4572300e")).WithObjectTypes(c2, allorsBinary).WithSize(-1).Build();

            // To
            new RelationTypeBuilder(domain, new Guid("4be564ac-77bc-48b8-b945-7d39f2ea9903")).WithObjectTypes(to, allorsString).WithSingularName("Name").WithPluralName("Names").WithSize(256).Build();

            // MailboxAddress
            new RelationTypeBuilder(domain, new Guid("03c9970e-d9d6-427d-83d0-00e0888f5588")).WithObjectTypes(mailboxAddress, allorsString).WithSingularName("PoBox").WithPluralName("PoBoxes").WithSize(256).Build();

            // Extender
            new RelationTypeBuilder(domain, new Guid("525bbc9e-d488-419f-ac02-0ab6ac409bac")).WithObjectTypes(extender, allorsString).WithSize(256).Build();

            // Two
            new RelationTypeBuilder(domain, new Guid("8930c13c-ad5a-4b0e-b3bf-d7cdf6f5b867")).WithObjectTypes(two, shared).WithCardinality(Cardinalities.ManyToOne).WithIsIndexed(true).Build();

            // SearchTest
            new RelationTypeBuilder(domain, new Guid("89c930ae-82ce-4f05-9c1f-a9bf9d8e8bb4")).WithObjectTypes(searchTest, allorsString).WithSingularName("Text").WithPluralName("Texts").WithSize(256).Build();

            // I12
            new RelationTypeBuilder(domain, new Guid("042d1311-1c06-4d7c-b68e-eb734f9c7327")).WithObjectTypes(i12, allorsBinary).WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("107c212d-cc1c-41b2-9c1d-b40c0102072c")).WithObjectTypes(i12, c2).WithSingularName("C2One2One").WithPluralName("C2One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("1611cb5d-4676-4e85-bfc5-5572e8ff1138")).WithObjectTypes(i12, allorsDouble).Build();
            new RelationTypeBuilder(domain, new Guid("167b53c0-644c-467e-9f7c-fcb9415d02c6")).WithObjectTypes(i12, i1).WithSingularName("I1Many2One").WithPluralName("I1Many2Ones").WithCardinality(Cardinalities.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("199a84c4-c7cb-4f23-8b6c-078b14525e18")).WithObjectTypes(i12, allorsString).WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("1bf2abe0-9273-4fb9-b491-020320f1f8db")).WithObjectTypes(i12, i12).WithSingularName("I12Many2Many").WithPluralName("I12Many2Manies").WithCardinality(Cardinalities.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("41a74fec-cfbc-43ca-a6e7-890f0dd1eddb")).WithObjectTypes(i12, allorsDecimal).WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("4a2b2f43-037d-4149-8a1e-401e5df963ba")).WithObjectTypes(i12, i2).WithSingularName("I2Many2Many").WithPluralName("I2Many2Manies").WithCardinality(Cardinalities.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("51ebb024-c847-4165-b216-b3b6e8883961")).WithObjectTypes(i12, c2).WithSingularName("C2Many2Many").WithPluralName("C2Many2Manies").WithCardinality(Cardinalities.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("59ae05e3-573c-4ea4-9181-2c545236ed1e")).WithObjectTypes(i12, i1).WithSingularName("I1Many2Many").WithPluralName("I1Many2Manies").WithCardinality(Cardinalities.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("5e473f63-b1d7-4530-b64f-26435fb5063c")).WithObjectTypes(i12, i12).WithSingularName("I12One2Many").WithPluralName("I12One2Manies").WithCardinality(Cardinalities.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("6daafb16-1bc3-4f15-8e25-1a982c5bb3c5")).WithObjectTypes(i12, allorsString).WithSingularName("Name").WithPluralName("Names").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("7827af95-147f-4803-865a-b418d567da68")).WithObjectTypes(i12, c1).WithSingularName("C1Many2Many").WithPluralName("C1Many2Manies").WithCardinality(Cardinalities.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("7f6fdb73-3e19-40e7-8feb-6ddbdf2e745a")).WithObjectTypes(i12, i2).WithSingularName("I2Many2One").WithPluralName("I2Many2Ones").WithCardinality(Cardinalities.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("93a59d0a-278d-435b-967e-551523f0cb85")).WithObjectTypes(i12, allorsUnique).Build();
            new RelationTypeBuilder(domain, new Guid("95551e3a-bad2-4136-923f-c8e5f0f2aec7")).WithObjectTypes(i12, allorsInteger).Build();
            new RelationTypeBuilder(domain, new Guid("95c77a0f-7f4c-4142-a93f-f688cfd554af")).WithObjectTypes(i12, i1).WithSingularName("I1One2Many").WithPluralName("I1One2Manies").WithCardinality(Cardinalities.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("9aefdda0-e547-4c9b-bf28-431669f8ea2e")).WithObjectTypes(i12, c1).WithSingularName("C1One2One").WithPluralName("C1One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("9bcbb810-fe3e-4829-8b1c-40219d16b60b")).WithObjectTypes(i12, allorsLong).Build();
            new RelationTypeBuilder(domain, new Guid("a89b4c06-bba5-4b05-bd6f-c32bc195c32f")).WithObjectTypes(i12, i12).WithSingularName("I12One2One").WithPluralName("I12One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("ac920d1d-290b-484b-9283-3829337182bc")).WithObjectTypes(i12, i2).WithSingularName("I2One2One").WithPluralName("I2One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("b2e3ddda-0cc3-4cfd-a114-9040882ec58a")).WithObjectTypes(i12, i12).WithSingularName("Dependency").WithPluralName("Dependencies").WithCardinality(Cardinalities.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("b2f568a1-51ba-4b6b-a1f1-b82bdec382b5")).WithObjectTypes(i12, i2).WithSingularName("I2One2Many").WithPluralName("I2One2Manies").WithCardinality(Cardinalities.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("c018face-b292-455c-a2c0-8f71377fb6cb")).WithObjectTypes(i12, c2).WithSingularName("C2Many2One").WithPluralName("C2Many2Ones").WithCardinality(Cardinalities.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("c6ecc142-0fbd-48b7-98ae-994fa9b5b814")).WithObjectTypes(i12, i12).WithSingularName("I12Many2One").WithPluralName("I12Many2Ones").WithCardinality(Cardinalities.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("ccdd1ae2-263e-4221-9841-4cff1907ee8d")).WithObjectTypes(i12, allorsBoolean).Build();
            new RelationTypeBuilder(domain, new Guid("ce0f7d58-b415-43f3-989b-9d8b34754e4b")).WithObjectTypes(i12, i1).WithSingularName("I1One2One").WithPluralName("I1One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("f302dd07-1abc-409e-aa71-ec9f7ac439aa")).WithObjectTypes(i12, c1).WithSingularName("C1One2Many").WithPluralName("C1One2Manies").WithCardinality(Cardinalities.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("f6436bc9-e307-4001-8f1f-5b37553ab3c6")).WithObjectTypes(i12, c1).WithSingularName("C1Many2One").WithPluralName("C1Many2Ones").WithCardinality(Cardinalities.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("fa6656dc-3a7a-4701-bc6b-3cd06aaa4483")).WithObjectTypes(i12, allorsDateTime).Build();

            // BadUI
            new RelationTypeBuilder(domain, new Guid("8a999086-ca90-40a1-90ae-475d231bb1eb")).WithObjectTypes(badUi, person).WithSingularName("PersonMany").WithPluralName("PersonsMany").WithCardinality(Cardinalities.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("9ebbb9d1-2ca7-4a7f-9f18-f25c05fd28c1")).WithObjectTypes(badUi, organisation).WithSingularName("CompanyOne").WithPluralName("CompanyOnes").WithCardinality(Cardinalities.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("a4db0d75-3dff-45ac-9c1d-623bca046b4a")).WithObjectTypes(badUi, person).WithSingularName("PersonOne").WithPluralName("PersonOnes").WithCardinality(Cardinalities.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("a8621048-48b5-43c4-b10b-17225958d177")).WithObjectTypes(badUi, organisation).WithSingularName("CompanyMany").WithPluralName("CompanyManies").WithCardinality(Cardinalities.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("c93a102e-ecdb-4189-a0fc-eeea8b4b85d4")).WithObjectTypes(badUi, allorsString).WithSize(256).Build();

            // Three
            new RelationTypeBuilder(domain, new Guid("1697f09c-0d3d-4e5e-9f3f-9d3ae0718fd3")).WithObjectTypes(three, four).WithCardinality(Cardinalities.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("4ace9948-4a22-465c-aa40-61c8fd65784d")).WithObjectTypes(three, allorsString).WithSize(-1).Build();

            // Second
            new RelationTypeBuilder(domain, new Guid("4f0eba0d-09b4-4bbc-8e42-15de94921ab5")).WithObjectTypes(second, third).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("8a7b7af9-f421-4e96-a1a7-04d4c4bdd1d7")).WithObjectTypes(second, allorsBoolean).WithSingularName("IsDerived").WithPluralName("IsDeriveds").Build();

            // DerivationLogC2
            // DerivationLogI12
            new RelationTypeBuilder(domain, new Guid("0b89b096-a69a-495c-acfe-b24a9b27e320")).WithObjectTypes(derivationLogI12, allorsUnique).WithSingularName("UniqueId").WithPluralName("UniqueIds").Build();

            // ClassWithoutRoles
            // I1
            new RelationTypeBuilder(domain, new Guid("06b72534-49a8-4f6d-a991-bc4aaf6f939f")).WithObjectTypes(i1, i1).WithSingularName("I1Many2One").WithPluralName("I1Many2Ones").WithCardinality(Cardinalities.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("0a2895ec-0102-493d-9b94-e12e94b4a403")).WithObjectTypes(i1, i12).WithSingularName("I12Many2Many").WithPluralName("I12Many2Manies").WithCardinality(Cardinalities.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("0acbea28-f8aa-477c-b296-b8976d9b10a5")).WithObjectTypes(i1, i2).WithSingularName("I2Many2Many").WithPluralName("I2Many2Manies").WithCardinality(Cardinalities.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("194580f4-e0e3-4b52-b9ba-6020171be4e9")).WithObjectTypes(i1, i2).WithSingularName("I2Many2One").WithPluralName("I2Many2Ones").WithCardinality(Cardinalities.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("28ceffc2-c776-4a0a-9825-a6d1bcb265dc")).WithObjectTypes(i1, allorsString).WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("2e85d74a-8d13-4bc0-ae4f-42b305e79373")).WithObjectTypes(i1, i12).WithSingularName("I12Many2One").WithPluralName("I12Many2Ones").WithCardinality(Cardinalities.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("39e28141-fd6b-4f49-8884-d5400f6c57ff")).WithObjectTypes(i1, i2).WithSingularName("I2One2Many").WithPluralName("I2One2Manies").WithCardinality(Cardinalities.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("4506a14b-22f1-41fe-972b-40fab7c6dd31")).WithObjectTypes(i1, c2).WithSingularName("C2One2Many").WithPluralName("C2One2Manies").WithCardinality(Cardinalities.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("47f775ef-2fe1-475a-81d8-2267dfd01085")).WithObjectTypes(i1, allorsDateTime).Build();
            new RelationTypeBuilder(domain, new Guid("593914b1-af95-4992-9703-2b60f4ea0926")).WithObjectTypes(i1, c1).WithSingularName("C1One2One").WithPluralName("C1One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("5cb44331-fd8c-4f73-8994-161f702849b6")).WithObjectTypes(i1, allorsInteger).Build();
            new RelationTypeBuilder(domain, new Guid("6199e5b4-133d-4d0e-9941-207316164ec8")).WithObjectTypes(i1, c2).WithSingularName("C2Many2Many").WithPluralName("C2Many2Manies").WithCardinality(Cardinalities.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("670c753e-8ea0-40b1-bfc9-7388074191d3")).WithObjectTypes(i1, i1).WithSingularName("I1One2Many").WithPluralName("I1One2Manies").WithCardinality(Cardinalities.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("6bb3ba6d-ffc7-4700-9723-c323b9b2d233")).WithObjectTypes(i1, i1).WithSingularName("I1Many2Many").WithPluralName("I1Many2Manies").WithCardinality(Cardinalities.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("6c3d04be-6f95-44b8-863a-245e150e3110")).WithObjectTypes(i1, allorsBoolean).Build();
            new RelationTypeBuilder(domain, new Guid("75fb2012-0b49-4442-a9b4-8239cffb1d37")).WithObjectTypes(i1, allorsLong).Build();
            new RelationTypeBuilder(domain, new Guid("818b4013-5ef1-4455-9f0d-9a39fa3425bb")).WithObjectTypes(i1, allorsDecimal).WithPrecision(10).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("a51d9d21-40ec-44b9-853d-8c18f54d659d")).WithObjectTypes(i1, i12).WithSingularName("I12One2One").WithPluralName("I12One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("a5761a0e-5c10-407a-bd68-0c4f69d78968")).WithObjectTypes(i1, i2).WithSingularName("I2One2One").WithPluralName("I2One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("b6e0fce0-14fc-46e3-995d-1b6e3699ed96")).WithObjectTypes(i1, c2).WithSingularName("C2One2One").WithPluralName("C2One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("b89092f1-8775-4b6a-99ef-f8626bc770bd")).WithObjectTypes(i1, c1).WithSingularName("C1One2Many").WithPluralName("C1One2Manies").WithCardinality(Cardinalities.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("b9c67658-4abc-41f3-9434-c8512a482179")).WithObjectTypes(i1, allorsBinary).WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("bcc9eee6-fa07-4d37-be84-b691bfce24be")).WithObjectTypes(i1, c1).WithSingularName("C1Many2Many").WithPluralName("C1Many2Manies").WithCardinality(Cardinalities.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("cdb758bf-ecaf-4d99-88fb-58df9258c13c")).WithObjectTypes(i1, allorsDouble).Build();
            new RelationTypeBuilder(domain, new Guid("e1b13216-7210-4c24-a668-83b40162a21b")).WithObjectTypes(i1, i1).WithSingularName("I1One2One").WithPluralName("I1One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("e3126228-342a-4415-a2e8-d52eceaeaf89")).WithObjectTypes(i1, c1).WithSingularName("C1Many2One").WithPluralName("C1Many2Ones").WithCardinality(Cardinalities.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("e386cca6-e738-4c37-8bfc-b23057d7a0be")).WithObjectTypes(i1, i12).WithSingularName("I12One2Many").WithPluralName("I12One2Manies").WithCardinality(Cardinalities.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("ef1a0a5e-1794-4478-9d0a-517182355206")).WithObjectTypes(i1, c2).WithSingularName("C2Many2One").WithPluralName("C2Many2Ones").WithCardinality(Cardinalities.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("f9d7411e-7993-4e43-a7e2-726f1e44e29c")).WithObjectTypes(i1, allorsUnique).Build();

            // Person
            new RelationTypeBuilder(domain, new Guid("0375a3d3-1a1b-4cbb-b735-1fe508bcc672")).WithObjectTypes(person, address).WithSingularName("MainAddress").WithPluralName("MainAddresses").WithCardinality(Cardinalities.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("15de4e58-c5ef-4ebb-9bf6-5ab06a02c5a4")).WithObjectTypes(person, allorsString).WithSingularName("TinyMCEText").WithPluralName("TinyMCETexts").WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("1b057406-3343-426b-ab5b-ceb93ba02446")).WithObjectTypes(person, allorsString).WithSingularName("Text").WithPluralName("Texts").WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("2a25125f-3545-4209-afc6-523eb0d8851e")).WithObjectTypes(person, allorsInteger).WithSingularName("Age").WithPluralName("Ages").Build();
            new RelationTypeBuilder(domain, new Guid("54f11f06-8d3f-4d58-bcdc-d40e6820fdad")).WithObjectTypes(person, allorsBoolean).WithSingularName("IsStudent").WithPluralName("AreStudent").Build();
            new RelationTypeBuilder(domain, new Guid("6340de2a-c3b1-4893-a7f3-cb924b82fa0e")).WithObjectTypes(person, mailboxAddress).WithSingularName("MailboxAddress").WithPluralName("MailboxAddresses").WithCardinality(Cardinalities.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("654f6c84-62f2-4c0a-9d68-532ed3f39447")).WithObjectTypes(person, gender).WithSingularName("Gender").WithPluralName("Genders").WithCardinality(Cardinalities.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("688ebeb9-8a53-4e8d-b284-3faa0a01ef7c")).WithObjectTypes(person, allorsString).WithSingularName("FullName").WithPluralName("FullNames").WithIsDerived(true).WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("6b626ba5-0c45-48c7-8b6b-5ea85e002d90")).WithObjectTypes(person, allorsInteger).WithSingularName("ShirtSize").WithPluralName("ShirtSizes").Build();
            new RelationTypeBuilder(domain, new Guid("6cc34453-ac7a-4004-8380-033f92324e99")).WithObjectTypes(person, allorsString).WithSingularName("CKEditorText").WithPluralName("CKEditorTexts").WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("a8a3b4b8-c4f2-4054-ab2a-2eac6fd058e4")).WithObjectTypes(person, allorsBoolean).WithSingularName("IsMarried").WithPluralName("AreMarried").Build();
            new RelationTypeBuilder(domain, new Guid("adf83a86-878d-4148-a9fc-152f56697136")).WithObjectTypes(person, allorsDateTime).WithSingularName("BirthDate").WithPluralName("BirthDates").Build();
            new RelationTypeBuilder(domain, new Guid("afc32e62-c310-421b-8c1d-6f2b0bb88b54")).WithObjectTypes(person, allorsDecimal).WithSingularName("Weight").WithPluralName("Weights").WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("b3ddd2df-8a5a-4747-bd4f-1f1eb37386b3")).WithObjectTypes(person, media).WithSingularName("Photo").WithPluralName("Photos").WithCardinality(Cardinalities.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("e9e7c874-4d94-42ff-a4c9-414d05ff9533")).WithObjectTypes(person, address).WithSingularName("Address").WithPluralName("Addresses").WithCardinality(Cardinalities.ManyToMany).WithIsIndexed(true).Build();

            // Singleton
            new RelationTypeBuilder(domain, new Guid("9ce2ef9b-2376-474d-9aa2-d23fbe1ed236")).WithObjectTypes(singleton, stringTemplate).WithSingularName("PersonTemplate").WithPluralName("PersonTemplates").WithCardinality(Cardinalities.ManyToOne).WithIsIndexed(true).Build();

            return domain;
        }
    }
}