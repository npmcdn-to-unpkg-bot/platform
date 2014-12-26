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
    using System.Linq;

    public static partial class Repository
    {
        public static void TestPostInit(MetaPopulation meta)
        {
            // Test
            var domain = new Domain(meta, new Guid("47636693-E55F-4ED3-93B6-3D75F11D5D4B")) { Name = "Test" };
            domain.AddDirectSuperdomain(Base);

            // ObjectTypes
            var c1 = new ClassBuilder(domain, new Guid("7041c691-d896-4628-8f50-1c24f5d03414")).WithSingularName("C1").WithPluralName("C1s").Build();
            var c2 = new ClassBuilder(domain, new Guid("72c07e8a-03f5-4da8-ab37-236333d4f74e")).WithSingularName("C2").WithPluralName("C2s").Build();
            var i1 = new InterfaceBuilder(domain, new Guid("fefcf1b6-ac8f-47b0-bed5-939207a2833e")).WithSingularName("I1").WithPluralName("I1s").Build();
            var i2 = new InterfaceBuilder(domain, new Guid("19bb2bc3-d53a-4d15-86d0-b250fdbcb0a0")).WithSingularName("I2").WithPluralName("I2s").Build();
            var i12 = new InterfaceBuilder(domain, new Guid("b45ec13c-704f-413d-a662-bdc59a17bfe3")).WithSingularName("I12").WithPluralName("I12s").Build();
            var s1 = new InterfaceBuilder(domain, new Guid("916A63C3-E825-4BE8-9156-A59A19B49B5E")).WithSingularName("S1").WithPluralName("S1s").Build();

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
            foreach (var composite in meta.Composites.Where( c => c.DefiningDomain.Equals(domain)))
            {
                if (!composite.DirectSupertypes.Contains(Derivable))
                {
                    new InheritanceBuilder(domain, Guid.NewGuid()).WithSubtype(composite).WithSupertype(Derivable).Build();
                }
            }

            // Dependent
            new InheritanceBuilder(domain, new Guid("6A2A0FEB-3E76-4BF1-921E-6A35FA255EF4")).WithSubtype(dependent).WithSupertype(Deletable).Build();

            // User
            new InheritanceBuilder(domain, new Guid("4EF23627-EE19-4BF0-AEDC-952039A64C52")).WithSubtype(User).WithSupertype(Deletable).Build();

            // C1
            new InheritanceBuilder(domain, new Guid("2d0db6cd-5837-4bbd-ad9e-9203a6cc7c61")).WithSubtype(c1).WithSupertype(i1).Build();

            // C2
            new InheritanceBuilder(domain, new Guid("07e545fb-5678-43da-a59e-65f48a9e88ed")).WithSubtype(c2).WithSupertype(i2).Build();

            // I1
            new InheritanceBuilder(domain, new Guid("4A33C229-5E53-4BAC-AFD2-B317A1400978")).WithSubtype(i1).WithSupertype(i12).Build();

            // I2
            new InheritanceBuilder(domain, new Guid("bdd512e8-b9dc-4a79-817e-58657b1d62e4")).WithSubtype(i2).WithSupertype(i12).Build();

            // I12
            new InheritanceBuilder(domain, new Guid("F56F2502-0394-4103-9C70-D66D56408EDD")).WithSubtype(i12).WithSupertype(UserInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("a71cee9c-3f73-4be5-941c-3f86d9fb0e07")).WithSubtype(i12).WithSupertype(AccessControlledObject).Build();
            new InheritanceBuilder(domain, new Guid("da105c1e-e19b-438a-bfc0-b845fb81864c")).WithSubtype(i12).WithSupertype(Searchable).Build();

            // S1
            new InheritanceBuilder(domain, new Guid("0C479724-316E-47A2-A081-958F910966B8")).WithSubtype(i1).WithSupertype(s1).Build();

            // Four
            new InheritanceBuilder(domain, new Guid("1a077ff9-b309-4982-8d79-2b176394eee4")).WithSubtype(four).WithSupertype(shared).Build();
            new InheritanceBuilder(domain, new Guid("eb8b60d0-fb51-43f8-8b4d-5086d23540c7")).WithSubtype(four).WithSupertype(UserInterfaceable).Build();

            // Address
            new InheritanceBuilder(domain, new Guid("f4ffbc32-e608-463f-80e3-8c727796bcd1")).WithSubtype(address).WithSupertype(UserInterfaceable).Build();

            // DerivationLogC1
            new InheritanceBuilder(domain, new Guid("e1abeb9f-d257-409c-8c1a-b79e3193f050")).WithSubtype(derivationLogC1).WithSupertype(derivationLogI12).Build();

            // HomeAddress
            new InheritanceBuilder(domain, new Guid("494ef8df-18ca-4399-811d-9c78cb3ae1f2")).WithSubtype(homeAddress).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("ab97d574-18bc-45cd-881d-87e2b024ceef")).WithSubtype(homeAddress).WithSupertype(address).Build();

            // Place
            new InheritanceBuilder(domain, new Guid("988c1ffa-44bc-4171-84a0-b621328f71ad")).WithSubtype(place).WithSupertype(UserInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("d5c3e9b1-dd7b-4952-ac42-d200deb0412a")).WithSubtype(place).WithSupertype(Searchable).Build();

            // Gender
            new InheritanceBuilder(domain, new Guid("2c5e6389-9a31-4ac8-aeeb-9e9a1b8f98a1")).WithSubtype(gender).WithSupertype(Enumeration).Build();
            
            // Organisation
            new InheritanceBuilder(domain, new Guid("1206d70d-b5d5-42e8-b2e6-3a155044aa29")).WithSubtype(organisation).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("2324bc9f-79a1-44f7-9041-00ed74e789e3")).WithSubtype(organisation).WithSupertype(UniquelyIdentifiable).Build();
            new InheritanceBuilder(domain, new Guid("390c3e64-c0f6-469b-81aa-d45254d15be8")).WithSubtype(organisation).WithSupertype(UserInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("eac51e90-62c9-4929-b46d-e20c4999f734")).WithSubtype(organisation).WithSupertype(SearchResult).Build();

            // Unit
            new InheritanceBuilder(domain, new Guid("3f713f76-d79f-477d-adff-a6b438df4c5e")).WithSubtype(unit).WithSupertype(AccessControlledObject).Build();
            new InheritanceBuilder(domain, new Guid("89cafe0f-0ea2-40c6-b709-c366a5a79e07")).WithSubtype(unit).WithSupertype(UserInterfaceable).Build();

            // Shared
            new InheritanceBuilder(domain, new Guid("4f60cdcf-c611-482d-877f-5c76cf723a78")).WithSubtype(shared).WithSupertype(UserInterfaceable).Build();

            // One
            new InheritanceBuilder(domain, new Guid("ae3ba09f-3c0f-4dc8-8147-1fed71aa96be")).WithSubtype(one).WithSupertype(shared).Build();
            new InheritanceBuilder(domain, new Guid("e8e3dacf-f084-445d-9652-470212739a14")).WithSubtype(one).WithSupertype(UserInterfaceable).Build();

            // From
            new InheritanceBuilder(domain, new Guid("27d445e8-5dad-499b-8904-cb2383582f0e")).WithSubtype(@from).WithSupertype(UserInterfaceable).Build();

            // To
            new InheritanceBuilder(domain, new Guid("ffc0a33a-9859-4c02-ba6e-d5fa41c3dcab")).WithSubtype(to).WithSupertype(UserInterfaceable).Build();

            // MailboxAddress
            new InheritanceBuilder(domain, new Guid("543f3140-e739-4173-aa1e-a06e1282e629")).WithSubtype(mailboxAddress).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("c849f4f5-f4ec-45d0-a384-d94a997c854d")).WithSubtype(mailboxAddress).WithSupertype(address).Build();

            // Two
            new InheritanceBuilder(domain, new Guid("7d57d7cb-25ec-4252-8e4f-732fa644ac2e")).WithSubtype(two).WithSupertype(UserInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("a28aef2a-8dff-4427-b132-2161894e1886")).WithSubtype(two).WithSupertype(shared).Build();

            // SearchTest
            new InheritanceBuilder(domain, new Guid("1367b4d9-1847-43a1-ad3d-e4db7fa0e276")).WithSubtype(searchTest).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("8217c5cb-661d-42d8-b133-a9926fffc245")).WithSubtype(searchTest).WithSupertype(UserInterfaceable).Build();

            // BadUI
            new InheritanceBuilder(domain, new Guid("645bf04f-a018-4ee7-a624-45a10b3ec771")).WithSubtype(badUi).WithSupertype(UserInterfaceable).Build();

            // Three
            new InheritanceBuilder(domain, new Guid("0e2ff702-d2fe-4298-b97d-ab9d7bec94b3")).WithSubtype(three).WithSupertype(shared).Build();
            new InheritanceBuilder(domain, new Guid("d93cdcce-1d39-49d9-a577-928f5eb567f3")).WithSubtype(three).WithSupertype(UserInterfaceable).Build();

            // DerivationLogC2
            new InheritanceBuilder(domain, new Guid("f3e0444d-8b32-4396-80c4-b07905563c17")).WithSubtype(derivationLogC2).WithSupertype(derivationLogI12).Build();

            // ClassWithoutRoles
            new InheritanceBuilder(domain, new Guid("05271ee7-baee-4272-a7c2-ab03de1da03d")).WithSubtype(classWithoutRoles).WithSupertype(UserInterfaceable).Build();

            // Person
            new InheritanceBuilder(domain, new Guid("19848b71-0042-40c2-8e88-d44db074bf5a")).WithSubtype(Person).WithSupertype(Printable).Build();

            // Dependent
            new RelationTypeBuilder(domain, new Guid("8859af04-ba38-42ce-8ac9-f428c3f92f31"), new Guid("cd3972e6-8ad4-4b01-9381-4d18718c7538"), new Guid("d6b1d6b6-539b-4b12-9363-18e7e9ab632c")).WithObjectTypes(dependent, dependee).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("9884955e-74ed-4f9d-9362-8e0274c53bf9"), new Guid("5b97e356-9bcd-4c4e-be7a-ef577eef5f14"), new Guid("d067129b-8440-4fc7-80d3-832ce569fe54")).WithObjectTypes(dependent, AllorsInteger).WithSingularName("Counter").WithPluralName("Counters").Build();
            new RelationTypeBuilder(domain, new Guid("e971733a-c381-4b5e-8e62-6bbd6d285bd7"), new Guid("6269351a-5e08-4b10-a895-ff2f669b259f"), new Guid("2b916cdb-93a6-42f1-b4e6-625b941c1874")).WithObjectTypes(dependent, AllorsInteger).WithSingularName("Subcounter").WithPluralName("Subcounters").Build();
            
            // Address
            new RelationTypeBuilder(domain, new Guid("36e7d935-a9c7-484d-8551-9bdc5bdeab68"), new Guid("113a8abd-e587-45a3-b118-92e60182c94b"), new Guid("4f7016f6-1b87-4ac4-8363-7f8210108928")).WithObjectTypes(address, place).WithSingularName("Place").WithPluralName("Places").WithMultiplicity(Multiplicity.ManyToOne).WithIsIndexed(true).Build();

            // First
            new RelationTypeBuilder(domain, new Guid("24886999-11f0-408f-b094-14b36ac4129b"), new Guid("e48ab2ee-c7a5-4d9a-b3ab-263f6aa4cdd1"), new Guid("cf5c725d-e567-44de-ab5b-b47bb0bf8647")).WithObjectTypes(first, second).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("b0274351-3403-4384-afb6-2cb49cd03893"), new Guid("ec145229-e33a-4807-a0dd-48778cc88ac7"), new Guid("12c46bf1-eed0-4e2a-b704-5d40032b4911")).WithObjectTypes(first, AllorsBoolean).WithSingularName("CreateCycle").WithPluralName("CreateCycles").Build();
            new RelationTypeBuilder(domain, new Guid("f2b61dd5-d30c-445a-ae7a-af1c0cc8e278"), new Guid("ae9f23b5-20a7-4ecc-b642-503d75c486f1"), new Guid("eb6b0565-1440-4b9b-aa23-51cfae3f93dd")).WithObjectTypes(first, AllorsBoolean).WithSingularName("IsDerived").WithPluralName("IsDeriveds").Build();

            // I2
            new RelationTypeBuilder(domain, new Guid("01d9ff41-d503-421e-93a6-5563e1787543"), new Guid("359ca62a-c74c-4936-a62d-9b8774174e8d"), new Guid("141b832f-7321-43b8-8033-dbad3f80edc3")).WithObjectTypes(i2, i2).WithSingularName("I2Many2One").WithPluralName("I2Many2Ones").WithMultiplicity(Multiplicity.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("1f763206-c575-4e34-9e6b-997d434d3f42"), new Guid("923f6373-cbf8-46b1-9b4b-185015ff59ac"), new Guid("9edd1eb9-2b9a-4375-a669-68c1859eace2")).WithObjectTypes(i2, c1).WithSingularName("C1Many2One").WithPluralName("C1Many2Ones").WithMultiplicity(Multiplicity.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("23e9c15f-097f-4452-9bac-d7cf2a65134a"), new Guid("278afe09-d0e7-4a41-a60b-b3a01fd14c93"), new Guid("e538ab5e-80f2-4a34-81e7-c9b92414dda1")).WithObjectTypes(i2, i12).WithSingularName("I12Many2One").WithPluralName("I12Many2Ones").WithMultiplicity(Multiplicity.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("35040d7c-ab7f-4a99-9d09-e01e24ca3cb9"), new Guid("d1f0ae79-1820-47a5-8869-496c3578a53d"), new Guid("0d2c6dbe-9bb2-414c-8f19-5381fe69ac64")).WithObjectTypes(i2, AllorsBoolean).Build();
            new RelationTypeBuilder(domain, new Guid("40b8edb3-e8c4-46c0-855b-4b18e0e8d7f3"), new Guid("078e1b17-f239-44b2-87d6-6350dd37ac1d"), new Guid("805d7871-bc51-4572-be01-e47ac8fef22a")).WithObjectTypes(i2, c1).WithSingularName("C1One2Many").WithPluralName("C1One2Manies").WithMultiplicity(Multiplicity.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("49736daf-d0bd-4216-97fa-958cfa21a4f0"), new Guid("02a80ccd-31c9-422c-8ad9-d96916dd7741"), new Guid("6ac5d426-9156-4467-8a04-85ccb6c964e2")).WithObjectTypes(i2, c1).WithSingularName("C1One2One").WithPluralName("C1One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("4f095abd-8803-4610-87f0-2847ddd5e9f4"), new Guid("5371c058-628e-4a1c-b654-ad0b7013eb17"), new Guid("ec80b71e-a933-4eb3-ab14-00b26c3bc805")).WithObjectTypes(i2, AllorsDecimal).WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("5ebbc734-23dd-494f-af2d-8e75caaa3e26"), new Guid("4d6c09d6-5644-47bb-a50a-464350053833"), new Guid("3aab87f3-2eab-4f81-9c1b-fd2e162a93b8")).WithObjectTypes(i2, i2).WithSingularName("I2Many2any").WithPluralName("I2Many2Manies").WithMultiplicity(Multiplicity.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("62a8a93d-3744-49de-9f9a-9997b6ef4da6"), new Guid("f9be65e7-6e36-42df-bb85-5198d0c12b74"), new Guid("e3ae23bc-5934-4c0d-a709-adb00110772d")).WithObjectTypes(i2, AllorsBinary).WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("663559c4-ef64-4e78-89b4-bfa00691c627"), new Guid("9513c57f-478a-423e-ba15-b9132bc28cd0"), new Guid("3f03fb6f-b0ba-4c78-b86a-9c4a1c574dd4")).WithObjectTypes(i2, AllorsUnique).Build();
            new RelationTypeBuilder(domain, new Guid("6bb406bc-627b-444c-9c16-df9878e05e9c"), new Guid("16647879-8af1-4f1c-8ef5-2cec85aa31f4"), new Guid("edee2f1c-3e94-45b5-80f4-160faa2074c4")).WithObjectTypes(i2, i1).WithSingularName("I1Many2One").WithPluralName("I1Many2Ones").WithMultiplicity(Multiplicity.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("83dc0581-e04a-4f51-a44e-4fef63d44356"), new Guid("b1c5cbb7-3d5f-48b8-b182-aa8a0cc3e72a"), new Guid("9598153e-9c1c-438a-a8a8-9822092a6a07")).WithObjectTypes(i2, i12).WithSingularName("I12One2Many").WithPluralName("I12One2Manies").WithMultiplicity(Multiplicity.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("87499e99-ed77-44c1-89d6-b4f570b6f217"), new Guid("e5201e06-3fbf-4b9c-aa65-1ee4ee9fabfb"), new Guid("e4c9f00e-7c3d-4b58-92f0-ccce24b55589")).WithObjectTypes(i2, i12).WithSingularName("I12One2One").WithPluralName("I12One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("92fdb313-0b90-48f6-b054-a4ab38f880ba"), new Guid("a45ffec8-5e4e-4b21-9d68-9b0050472ed2"), new Guid("17e159a2-f5a6-4828-9fef-796fcc9085e8")).WithObjectTypes(i2, c2).WithSingularName("C2Many2Many").WithPluralName("C2Many2Manies").WithMultiplicity(Multiplicity.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("9bed0518-1946-4e23-9d4b-e4cda439984c"), new Guid("7b4a8937-258c-4129-a282-89d5ab924d68"), new Guid("2e78a543-949f-4130-b659-80a9a60ad6ab")).WithObjectTypes(i2, i1).WithSingularName("I1Many2Many").WithPluralName("I1Many2Manies").WithMultiplicity(Multiplicity.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("9f361b97-0b04-496d-ac60-718760c2a4e2"), new Guid("c51f6fd4-c290-41b6-b594-19e9bcbbee6a"), new Guid("f60f8fa4-4e73-472d-b0b0-67f202c1e969")).WithObjectTypes(i2, c2).WithSingularName("C2Many2One").WithPluralName("C2Many2Ones").WithMultiplicity(Multiplicity.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("9f91841c-f63f-4ffa-bee6-62e100f3cd15"), new Guid("3164fd30-297e-4e2a-86d6-fad6754f1d59"), new Guid("7afb53c1-2fe3-44b6-b1d2-d5a9f6100076")).WithObjectTypes(i2, AllorsString).WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("b39fdd23-d7dd-473f-9705-df2f29be5ffe"), new Guid("8ddc9cbf-8e5c-4166-a2b0-6127c142da78"), new Guid("7cdd2b76-6c35-4e81-a1da-f5d0a300014b")).WithObjectTypes(i2, c2).WithSingularName("C2One2Many").WithPluralName("C2One2Manies").WithMultiplicity(Multiplicity.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("b640bf16-0dc0-4203-aa76-f456371239ae"), new Guid("257fa0c6-43ea-4fe9-8142-dbc172d1e138"), new Guid("26deb364-bd5e-4b5d-b28a-19689ab3c00d")).WithObjectTypes(i2, i1).WithSingularName("I1One2One").WithPluralName("I1One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("bbb01166-2671-4ca1-8b1e-12e6ae8aeb03"), new Guid("ee0766c7-0ef6-4ca0-b4a1-c399bc8df823"), new Guid("d8f011c4-3057-4384-9045-9c34b13db5c3")).WithObjectTypes(i2, i1).WithSingularName("I1One2Many").WithPluralName("I1One2Manies").WithMultiplicity(Multiplicity.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("cb9f21e0-a841-45de-8ba4-991b4ceca616"), new Guid("1127ff1b-1657-4e18-bdc9-bc90cd8a3c15"), new Guid("d838e921-ff63-4e4f-afd8-42dc29d23555")).WithObjectTypes(i2, i12).WithSingularName("I12Many2Many").WithPluralName("I12Many2Manies").WithMultiplicity(Multiplicity.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("cc4c704c-ab7e-45d4-baa9-b67cfff9448e"), new Guid("d15cb643-1ace-4dfe-b0af-e02e4273bbbb"), new Guid("12c2c263-7839-4734-9307-bcde6930a2b7")).WithObjectTypes(i2, i2).WithSingularName("I2One2One").WithPluralName("I2One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("d30dd036-6d28-48df-873b-3a76da8c029e"), new Guid("012e0afc-ebc7-4ae4-9fa0-49c72f3daebf"), new Guid("69c063b7-156f-4b7f-89eb-10c7eaf39ad5")).WithObjectTypes(i2, AllorsInteger).Build();
            new RelationTypeBuilder(domain, new Guid("deb9cbd3-386f-4599-802c-be50945b9f1d"), new Guid("3fcc8e73-5f3c-4ce0-8f45-daa813278d7e"), new Guid("c7d68f0d-24b1-40c9-9431-78763b776bee")).WithObjectTypes(i2, i2).WithSingularName("I2One2Many").WithPluralName("I2One2Manies").WithMultiplicity(Multiplicity.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("f364c9fe-ad36-4305-80fd-4921451c70a5"), new Guid("db6935b0-684c-48ce-97d0-6b7183a73adb"), new Guid("6ed084f6-8809-46d9-a3ec-4b086ddafb0a")).WithObjectTypes(i2, c1).WithSingularName("C1Many2Many").WithPluralName("C1Many2Manies").WithMultiplicity(Multiplicity.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("f85c2d97-10b9-478d-9b82-2700d95d5cb1"), new Guid("bfb08e5e-afc6-4f27-975f-5fb9af5bacc4"), new Guid("666c65ad-8bf7-40be-a51a-e69d3e0bfe01")).WithObjectTypes(i2, c2).WithSingularName("C2One2One").WithPluralName("C2One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("fbad33e7-ede1-41fc-97e9-ddf33a0f6459"), new Guid("c138d77b-e8bf-4945-962e-f74e338caad4"), new Guid("12ea1f33-0eed-4476-9cab-1fd62ed146a3")).WithObjectTypes(i2, AllorsFloat).Build();

            // HomeAddress
            new RelationTypeBuilder(domain, new Guid("6f0f42c4-9b47-47c2-a632-da8e08116be4"), new Guid("652a00b8-f708-4804-80b6-c1fe3211acf2"), new Guid("fc273b47-d98a-4afd-90ba-574fbdbfb395")).WithObjectTypes(homeAddress, AllorsString).WithSingularName("Street").WithPluralName("Street").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("b181d077-e897-4add-9456-67b9760d32e8"), new Guid("5eca1733-0f01-4141-b0d0-d7a2bfd90388"), new Guid("d29dbed0-a68a-4075-b893-55e16e6335fd")).WithObjectTypes(homeAddress, AllorsString).WithSingularName("HouseNumber").WithPluralName("HouseNumbers").WithSize(256).Build();

            // Place
            new RelationTypeBuilder(domain, new Guid("1bf1cc1e-75bf-4a3f-87bd-a2fae2697855"), new Guid("dce03fde-fbb1-45e7-b78d-9484fa6487ff"), new Guid("d88eaaa2-2622-48ef-960a-1b506d95f238")).WithObjectTypes(place, Country).WithMultiplicity(Multiplicity.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("d029f486-4bb8-43a1-8356-98b9bee10de4"), new Guid("1454029b-b016-41e1-b142-cea20c7b36d1"), new Guid("dccca416-913b-406a-9405-c5d037af2fd8")).WithObjectTypes(place, AllorsString).WithSingularName("City").WithPluralName("Cities").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("d80d7c6a-138a-43dd-9748-8ffb89b1dabb"), new Guid("944c752e-742c-426b-9ac9-c405080d4a8d"), new Guid("b54fcc51-e294-4732-82bf-a1117a4e2219")).WithObjectTypes(place, AllorsString).WithSingularName("PostalCode").WithPluralName("PostalCodes").WithSize(256).Build();

            // Dependee
            new RelationTypeBuilder(domain, new Guid("1b8e0350-c446-48dc-85c0-71130cc1490e"), new Guid("97c6a03f-f0c7-4c7d-b40f-1353e34431bd"), new Guid("89b8f5f6-5589-42ad-ac9e-1d984c02f7ea")).WithObjectTypes(dependee, subdependee).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("c1e86449-e5a8-4911-97c7-b03de9142f98"), new Guid("2786b8ca-2d71-44cc-8e1e-1896ac5e6c5c"), new Guid("af75f294-b20d-4304-8804-32ef9c0a324a")).WithObjectTypes(dependee, AllorsInteger).WithSingularName("Subcounter").WithPluralName("Subcounters").Build();
            new RelationTypeBuilder(domain, new Guid("d58d1f28-3abd-4294-abde-885bdd16f466"), new Guid("9a867244-8ea3-402b-9a9c-a78727dbee78"), new Guid("5f570211-688e-4050-bf54-997d22a529d5")).WithObjectTypes(dependee, AllorsInteger).WithSingularName("Counter").WithPluralName("Counters").Build();
            new RelationTypeBuilder(domain, new Guid("e73b8fc5-0148-486a-9379-cfb051b303d2"), new Guid("db615c1c-3d08-4faa-b19f-740bd7102fbd"), new Guid("bde110ae-8242-4d98-bdc3-feeed8fde742")).WithObjectTypes(dependee, AllorsBoolean).WithSingularName("DeleteDependent").WithPluralName("DeleteDependents").Build();

            // Third
            new RelationTypeBuilder(domain, new Guid("6ab5a7af-a0f0-4940-9be3-6f6430a9e728"), new Guid("a18d4c53-ba36-4936-8650-0d90182e5948"), new Guid("7866ac81-e84d-40c6-b9c0-5a038b1e838f")).WithObjectTypes(third, AllorsBoolean).WithSingularName("IsDerived").WithPluralName("IsDeriveds").Build();

            // Organisation
            new RelationTypeBuilder(domain, new Guid("15f33fa4-c878-45a0-b40c-c5214bce350b"), new Guid("4fdd9abb-f2e7-4f07-860e-27b4207224bd"), new Guid("45bef644-dfcf-417a-9356-3c1cfbcada1b")).WithObjectTypes(organisation, Person).WithSingularName("Shareholder").WithPluralName("Shareholders").WithMultiplicity(Multiplicity.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("2cc74901-cda5-4185-bcd8-d51c745a8437"), new Guid("896a4589-4caf-4cd2-8365-c4200b12f519"), new Guid("baa30557-79ff-406d-b374-9d32519b2de7")).WithObjectTypes(organisation, AllorsString).WithSingularName("Name").WithPluralName("Names").WithIsIndexed(true).WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("2cfea5d4-e893-4264-a966-a68716839acd"), new Guid("c3c93567-1d78-42ea-a8cf-77549cd1a235"), new Guid("d5965473-66cd-44b2-8048-a521c9cdadd0")).WithObjectTypes(organisation, AllorsString).WithSingularName("Description").WithPluralName("Descriptions").WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("49b96f79-c33d-4847-8c64-d50a6adb4985"), new Guid("b031ef1a-0102-4b19-b85d-aa9c404596c3"), new Guid("b95c7b34-a295-4600-82c8-826cc2186a00")).WithObjectTypes(organisation, Person).WithSingularName("Employee").WithPluralName("Employees").WithMultiplicity(Multiplicity.OneToMany).Build();
            new RelationTypeBuilder(domain, new Guid("5fa25b53-e2a7-44c8-b6ff-f9575abb911d"), new Guid("6a382c73-c6a2-4d8b-bc85-4623ede54298"), new Guid("1c3dec18-978c-470a-8857-5210b9267185")).WithObjectTypes(organisation, AllorsBoolean).WithSingularName("Incorporated").WithPluralName("Incorporateds").Build();
            new RelationTypeBuilder(domain, new Guid("68c61cea-4e6e-4ed5-819b-7ec794a10870"), new Guid("8494ad76-3422-4799-b5a6-caa077e53aca"), new Guid("90489246-8590-4578-8b8d-716a25abd27d")).WithObjectTypes(organisation, AllorsBoolean).WithSingularName("IsSupplier").WithPluralName("AreSupplier").Build();
            new RelationTypeBuilder(domain, new Guid("73f23588-1444-416d-b43c-b3384ca87bfc"), new Guid("d1a098bf-a3d8-4b71-948f-a77ae82f02db"), new Guid("a365f0ee-a94f-4435-a7b1-c92ac804a845")).WithObjectTypes(organisation, address).WithSingularName("Address").WithPluralName("Addresses").WithMultiplicity(Multiplicity.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("845ff004-516f-4ad5-9870-3d0e966a9f7d"), new Guid("3820f65f-0e79-4f30-a973-5d17dca6ad33"), new Guid("58d7df91-fbc5-4bcb-9398-a9957949402b")).WithObjectTypes(organisation, Person).WithSingularName("Owner").WithPluralName("Owners").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("b201d2a0-2335-47a1-aa8d-8416e89a9fec"), new Guid("e332003a-0287-4aab-9d95-257146ee4f1c"), new Guid("b1f5b479-e4d0-46de-8ad4-347076d9f180")).WithObjectTypes(organisation, Media).WithSingularName("Logo").WithPluralName("Logos").WithMultiplicity(Multiplicity.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("bac702b8-7874-45c3-a410-102e1caea4a7"), new Guid("8c2ce648-3942-4ead-9772-308c29bc905e"), new Guid("26a60588-3c90-4f4e-9bb6-8f45fe8f9606")).WithObjectTypes(organisation, AllorsString).WithSingularName("Size").WithPluralName("Sizes").WithSize(256).Build();

            // Subdependee
            new RelationTypeBuilder(domain, new Guid("194930f9-9c3f-458d-93ec-3d7bea4cd538"), new Guid("63ed21ba-b310-43fc-afed-a3eeea918204"), new Guid("6765f2b5-bf55-4713-a693-946fc0846b27")).WithObjectTypes(subdependee, AllorsInteger).WithSingularName("Subcounter").WithPluralName("Subcounters").Build();

            // Unit
            new RelationTypeBuilder(domain, new Guid("24771d5b-f920-4820-aff7-ea6391b4a45c"), new Guid("fe3aa333-e011-4a1e-85dc-ded48329cf00"), new Guid("4d4428fc-bac0-47af-ab5e-7c7b87880206")).WithObjectTypes(unit, AllorsBinary).WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("5a788ebe-65e9-4d5e-853a-91bb4addabb5"), new Guid("7620281d-3d8a-470a-9258-7a6d1b818b46"), new Guid("b5dd13eb-8923-4a66-94df-af5fadb42f1c")).WithObjectTypes(unit, AllorsBoolean).Build();
            new RelationTypeBuilder(domain, new Guid("74a35820-ef8c-4373-9447-6215ee8279c0"), new Guid("e5f7a565-372a-42ed-8da5-ffe6dd599f70"), new Guid("4a95fb0d-6849-499e-a140-6c942fb06f4d")).WithObjectTypes(unit, AllorsFloat).Build();
            new RelationTypeBuilder(domain, new Guid("b817ba76-876e-44ea-8e5a-51d552d4045e"), new Guid("80683240-71d5-4329-abd0-87c367b44fec"), new Guid("07070cb0-6e65-4a00-8754-50cf594ed9e1")).WithObjectTypes(unit, AllorsInteger).Build();
            new RelationTypeBuilder(domain, new Guid("c724c733-972a-411c-aecb-e865c2628a90"), new Guid("e4917fda-a605-4f6f-8f63-579ec688b629"), new Guid("f27c150a-ce8d-4ff3-9507-ccb0b91aa0c2")).WithObjectTypes(unit, AllorsString).WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("ed58ae4c-24e0-4dd1-8b1c-0909df1e0fcd"), new Guid("f117e164-ce37-4c12-a79e-38cda962adae"), new Guid("25dd4abf-c6da-4739-aed0-8528d1c00b8b")).WithObjectTypes(unit, AllorsUnique).Build();
            new RelationTypeBuilder(domain, new Guid("f746da51-ea2d-4e22-9ecb-46d4dbc1b084"), new Guid("3936ee9b-3bd6-44de-9340-4047749a6c2c"), new Guid("1408cd42-3125-48c7-86d7-4a5f71e75e25")).WithObjectTypes(unit, AllorsDecimal).WithPrecision(19).WithScale(2).Build();

            // Shared
            // One
            new RelationTypeBuilder(domain, new Guid("448878af-c992-4256-baa7-239335a26bc6"), new Guid("2c9236ed-892e-4005-9730-5a14f03f71e1"), new Guid("355b2e85-e597-4f88-9dca-45cbfbde527f")).WithObjectTypes(one, two).WithMultiplicity(Multiplicity.ManyToOne).WithIsIndexed(true).Build();

            // From
            new RelationTypeBuilder(domain, new Guid("d9a9896d-e175-410a-9916-9261d83aa229"), new Guid("a963f593-cad0-4fa9-96a3-3853f0f7d7c6"), new Guid("775a29b8-6e21-4545-9881-d52f6eb7db8b")).WithObjectTypes(@from, to).WithSingularName("To").WithPluralName("Tos").WithMultiplicity(Multiplicity.OneToMany).WithIsIndexed(true).Build();

            // StatefulCompany
            new RelationTypeBuilder(domain, new Guid("6c848eeb-7b42-45ea-81ac-fa983e1e0fa9"), new Guid("be566287-a26d-46fb-a4f2-1fc8bf1c1de4"), new Guid("2a482b25-a154-4306-87f3-b6cd7af3c80d")).WithObjectTypes(statefulCompany, Person).WithSingularName("Employee").WithPluralName("Employees").WithMultiplicity(Multiplicity.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("6e429d87-ea80-465e-9aa6-0f7d546b6bb3"), new Guid("de607129-6f68-4db6-a6ca-6ba53cae698d"), new Guid("94570d2c-2a5e-451f-905e-6ca61a469a31")).WithObjectTypes(statefulCompany, AllorsString).WithSingularName("Name").WithPluralName("Names").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("9940e8ed-189e-42c6-b0d1-7c01920b9fac"), new Guid("de4a92c8-4e08-4f37-9d6c-321dcce89e1c"), new Guid("3becaaa8-7b49-4616-8d79-a7bf04d9e666")).WithObjectTypes(statefulCompany, Person).WithSingularName("Manager").WithPluralName("Managers").WithMultiplicity(Multiplicity.ManyToOne).WithIsIndexed(true).Build();

            // C1
            new RelationTypeBuilder(domain, new Guid("0e7f529b-bc91-4a40-a7e7-a17341c6bf5b"), new Guid("1d1374c3-a28d-4904-b98a-3a14ceb2f7ea"), new Guid("da5ccb42-7878-45a9-9350-17f0f0a52fd4")).WithObjectTypes(c1, i1).WithSingularName("I1One2One").WithPluralName("I1One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("20713860-8abd-4d71-8ccc-2b4d1b88bce3"), new Guid("974aa133-255b-431f-a15d-b6a126d362b5"), new Guid("6dc98925-87a7-4959-8095-90eedef0e9a0")).WithObjectTypes(c1, AllorsString).WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("5490dc63-a8f6-4a86-91ef-fef97a86f119"), new Guid("3f307d57-1f39-4aba-822d-9881cef7223c"), new Guid("66a06e06-95e4-43ad-9b45-56687f8a2051")).WithObjectTypes(c1, c2).WithSingularName("C2Many2One").WithPluralName("C2Many2Ones").WithMultiplicity(Multiplicity.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("6def7988-4bcf-4964-9de6-c6ede41d5e5a"), new Guid("75e47fbe-6ce1-4cc1-a20f-51a861df9cc3"), new Guid("e7d1e28d-69ad-4d3a-b35a-2d0aaacb67db")).WithObjectTypes(c1, i2).WithSingularName("I2One2One").WithPluralName("I2One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("79c00218-bb4f-40e9-af7d-61af444a4a54"), new Guid("2276c942-dd96-41a6-b52f-cd3862c4692f"), new Guid("40ee2908-2556-4bdf-a82f-2ea33e181b91")).WithObjectTypes(c1, c1).WithSingularName("C1One2One").WithPluralName("C1One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("7bb216f2-8e9c-4dcd-890b-579130ab0a8b"), new Guid("531e89ab-a295-4f72-8496-cdd0d8605d37"), new Guid("8af8fbc6-2f59-4026-9093-5b335dfb8b7f")).WithObjectTypes(c1, i1).WithSingularName("I1Many2One").WithPluralName("I1Many2Ones").WithMultiplicity(Multiplicity.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("815878f6-16f2-42f2-9b24-f394ddf789c2"), new Guid("eca51eab-3815-410f-b4c5-f7e2a1318791"), new Guid("39f62f9e-52d3-47c5-8fd4-44e91d9b78be")).WithObjectTypes(c1, i1).WithSingularName("I1Many2Many").WithPluralName("I1Many2Manies").WithMultiplicity(Multiplicity.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("82f5fb26-c260-41bc-a784-a2d5e35243bd"), new Guid("f5329d84-1301-44ea-85b4-dc7d98554694"), new Guid("ca30ba2a-627f-43d1-b467-fe0d7cd015cc")).WithObjectTypes(c1, i2).WithSingularName("I2One2Many").WithPluralName("I2One2Manies").WithMultiplicity(Multiplicity.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("87eb0d19-73a7-4aae-aeed-66dc9163233c"), new Guid("96e8dfaf-3e1e-4c59-88f3-d47be6c96b74"), new Guid("43ccd07d-b9c4-465c-b2f9-083a36315e85")).WithObjectTypes(c1, AllorsDecimal).WithPrecision(10).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("8c198447-e943-4f5a-b749-9534b181c664"), new Guid("154222cb-0eb8-459d-839c-9c8857bd1c7e"), new Guid("c403f160-6486-4207-b32c-aa9ade27a28c")).WithObjectTypes(c1, c1).WithSingularName("C1Many2Many").WithPluralName("C1Many2Manies").WithMultiplicity(Multiplicity.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("94a2b37d-9431-4496-b992-630cda5b9851"), new Guid("a4a31323-7193-4709-828e-88b2c0f3f8aa"), new Guid("f225d708-c98f-44ff-9ed8-847cb1ddaacb")).WithObjectTypes(c1, i12).WithSingularName("I12Many2Many").WithPluralName("I12Many2Manies").WithMultiplicity(Multiplicity.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("97f31053-0e7b-42a0-90c2-ce6f09c56e86"), new Guid("70e42b8b-09e2-4cb1-a632-ff3785ee1c8d"), new Guid("e5cd692c-ab97-4cf8-9f8a-1de733526e74")).WithObjectTypes(c1, AllorsBinary).WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("98c5f58b-1777-4d9a-8828-37dbf7051510"), new Guid("3218ac29-2eac-4dc9-acad-2c708c3df994"), new Guid("51b3b28e-9017-4a1e-b5ba-06a9b14d88d6")).WithObjectTypes(c1, i12).WithSingularName("I12One2Many").WithPluralName("I12One2Manies").WithMultiplicity(Multiplicity.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("9f6538c2-e6dd-4c27-80ed-2748f645cb95"), new Guid("3ddac067-46f1-4302-bb1b-aa0e05dd55ae"), new Guid("c749e58c-0f1d-4946-b35d-878221aac72f")).WithObjectTypes(c1, c2).WithSingularName("C2One2Many").WithPluralName("C2One2Manies").WithMultiplicity(Multiplicity.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("a0ac5a65-2cbd-4c51-9417-b10150bc5699"), new Guid("d595765b-5e67-46f2-b19c-c58563dd1ae0"), new Guid("3d121ffa-0ff5-4627-9ec3-879c2830ff04")).WithObjectTypes(c1, c1).WithSingularName("C1One2Many").WithPluralName("C1One2Manies").WithMultiplicity(Multiplicity.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("a64abd21-dadf-483d-9499-d19aa8e33791"), new Guid("099e3d39-16b5-431a-853b-942a354c3a52"), new Guid("c186bb2f-8e19-468d-8a01-561384e5187d")).WithObjectTypes(c1, AllorsString).WithSingularName("AllorsStringMax").WithPluralName("AllorsStringsMax").WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("a8e18ea7-cbf2-4ea7-ae14-9f4bcfdb55de"), new Guid("8a546f48-fc09-48ae-997d-4a6de0cd458a"), new Guid("e6b21250-194b-4424-8b92-221c6d0e6228")).WithObjectTypes(c1, c1).WithSingularName("C1Many2One").WithPluralName("C1Many2Ones").WithMultiplicity(Multiplicity.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("b4ee673f-bba0-4e24-9cda-3cf993c79a0a"), new Guid("948aa9e6-5cb3-48dc-a3b7-3f8770269dae"), new Guid("ad456144-a19e-4c89-845b-9391dbc8f372")).WithObjectTypes(c1, AllorsBoolean).Build();
            new RelationTypeBuilder(domain, new Guid("b9f2c4c7-6979-40cf-82a2-fa99a5d9e9a4"), new Guid("911a9327-0237-4254-99a7-afff0d6a0369"), new Guid("50bf56c3-f05f-4172-86e1-aefead4a3a8c")).WithObjectTypes(c1, i12).WithSingularName("I12One2One").WithPluralName("I12One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("bcf4df45-6616-4cdf-8ada-f944f9c7ff1a"), new Guid("2128418c-6918-4be8-8a02-2bea142b7fc4"), new Guid("b5b4892d-e1d3-4a4b-a8a4-ac6ed0ff930e")).WithObjectTypes(c1, i12).WithSingularName("I12Many2One").WithPluralName("I12Many2Ones").WithMultiplicity(Multiplicity.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("cda97972-84c8-48e3-99d8-fd7c99c5dbc9"), new Guid("8ef5784c-6f76-431e-b59d-075813ad7863"), new Guid("ce5170b0-347a-49b7-9925-a7a5c5eb2c75")).WithObjectTypes(c1, i2).WithSingularName("I2Many2Many").WithPluralName("I2Many2Manies").WithMultiplicity(Multiplicity.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("cef13620-b7d7-4bfe-8d3b-c0f826da5989"), new Guid("6c18bd8f-9084-470b-9dfe-30263c98771b"), new Guid("2721249b-dadd-410d-b4e0-9d4a48e615d1")).WithObjectTypes(c1, AllorsUnique).Build();
            new RelationTypeBuilder(domain, new Guid("d0341bed-2732-4bcb-b1bb-9f9589de5d03"), new Guid("dacd7dfa-6650-438d-b564-49fbf89fea8d"), new Guid("2db69dd4-008b-4a17-aba5-6a050f35f7e3")).WithObjectTypes(c1, i2).WithSingularName("I2Many2One").WithPluralName("I2Many2Ones").WithMultiplicity(Multiplicity.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("e0656d9a-75a6-4e59-aaa1-3ff03d440059"), new Guid("637c5967-fb6c-45d4-81c4-de5559df785f"), new Guid("89e4802f-7c61-4deb-a243-f78e79578082")).WithObjectTypes(c1, i1).WithSingularName("I1One2Many").WithPluralName("I1One2Manies").WithMultiplicity(Multiplicity.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("e97fc754-c736-4359-9662-19dce9429f89"), new Guid("5bd37271-01c0-4cd3-94d5-0284700b3567"), new Guid("392f5a47-f181-475c-b5c9-f0b729c8413f")).WithObjectTypes(c1, c2).WithSingularName("C2One2One").WithPluralName("C2One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("f268783d-42ed-41c1-b0b0-b8a60e30a601"), new Guid("6ed0694c-a74f-44c3-835b-897f56343576"), new Guid("459d20d8-dadd-44e1-aa8a-396e6eab7538")).WithObjectTypes(c1, AllorsFloat).Build();
            new RelationTypeBuilder(domain, new Guid("f29d4a52-9ba5-40f6-ba99-050cbd03e554"), new Guid("122dc72f-cc92-440c-84e5-fe8340020c43"), new Guid("608db13d-1778-44a8-94f0-b86fc0f6992d")).WithObjectTypes(c1, c2).WithSingularName("C2Many2Many").WithPluralName("C2Many2Manies").WithMultiplicity(Multiplicity.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("f4920d94-8cd0-45b6-be00-f18d377368fd"), new Guid("c4202876-b670-4193-a459-3f0376e24c38"), new Guid("2687f5be-eebe-4ffb-a8b2-538134cb6f73")).WithObjectTypes(c1, AllorsInteger).WithIsIndexed(true).Build();

            // C2
            new RelationTypeBuilder(domain, new Guid("07eaa992-322a-40e9-bf2c-aa33b69f54cd"), new Guid("172c107a-e140-4462-9a62-5ef91e6ead2a"), new Guid("152c92f0-485e-4a28-b321-d6ed3b730fc0")).WithObjectTypes(c2, AllorsDecimal).WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("0c8209e3-b2fc-4c7a-acd2-6b5b8ac89bf4"), new Guid("56bb9554-819f-418a-9ce1-a91fa704b371"), new Guid("9292cb86-3e04-4cd4-b3fd-a5af7a5ce538")).WithObjectTypes(c2, c1).WithSingularName("C1One2One").WithPluralName("C1One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("12896fc2-c9e9-4a89-b875-0aeb92e298e5"), new Guid("781b282e-b86f-4747-9d5e-d0f7c08b0899"), new Guid("f41ddb05-4a96-40fa-859b-8b3d6dfcd86b")).WithObjectTypes(c2, c2).WithSingularName("C2Many2One").WithPluralName("C2Many2Ones").WithMultiplicity(Multiplicity.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("1444d919-6ca1-4642-8d18-9d955c817581"), new Guid("9263c1e7-0cda-4129-a16d-da5351adafcb"), new Guid("cc1f2cae-2a5d-4584-aa08-4b03fc2176d2")).WithObjectTypes(c2, AllorsUnique).Build();
            new RelationTypeBuilder(domain, new Guid("165cc83e-935d-4d0d-aec7-5da155300086"), new Guid("bc437b29-f883-41c1-b36f-20be37bc9b30"), new Guid("b2f83414-aa5c-44fd-a382-56119727785a")).WithObjectTypes(c2, i12).WithSingularName("I12Many2One").WithPluralName("I12Many2Ones").WithMultiplicity(Multiplicity.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("1d0c57c9-a3d1-4134-bc7d-7bb587d8250f"), new Guid("07c026ad-3515-4df0-bee7-ab61d5a9217d"), new Guid("c0562ba5-0402-44ea-acd0-9e78d20e7576")).WithObjectTypes(c2, i12).WithSingularName("I12One2One").WithPluralName("I12One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("1d98eda7-6dba-43f1-a5ce-44f7ed104cf9"), new Guid("cae17f3c-a605-4dce-b38d-01c23eea29df"), new Guid("d3e84546-02fc-40be-b550-dbd54cd8a139")).WithObjectTypes(c2, i1).WithSingularName("I1Many2Many").WithPluralName("I1Many2Manies").WithMultiplicity(Multiplicity.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("262ad367-a52c-4d8b-94e2-b477bb098423"), new Guid("31be0ad7-0886-406a-a69f-7e38b4526199"), new Guid("c52984df-80f8-4622-84e0-0e9f97cfaff3")).WithObjectTypes(c2, AllorsFloat).Build();
            new RelationTypeBuilder(domain, new Guid("2ac55066-c748-4f90-9d0f-1090fe02cc76"), new Guid("02a5ac2c-d0ac-482d-abee-117ed7eaa5ba"), new Guid("28f373c6-62b6-4f5c-b794-c10138043a63")).WithObjectTypes(c2, i1).WithSingularName("I1One2Many").WithPluralName("I1One2Manies").WithMultiplicity(Multiplicity.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("38063edc-271a-410d-b857-807a9100c7b5"), new Guid("6bedcc6b-af15-4f27-93e8-4404d23dfd99"), new Guid("642f5531-896d-482f-b746-4ecf08f27027")).WithObjectTypes(c2, i2).WithSingularName("I2One2One").WithPluralName("I2One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("42f9f4b6-3b35-4168-93cb-35171dbf83f4"), new Guid("622f9b4f-efc8-454f-9dd6-884bed5b5f4b"), new Guid("f5545dfc-e19a-456a-8469-46708ea5bb68")).WithObjectTypes(c2, AllorsInteger).Build();
            new RelationTypeBuilder(domain, new Guid("4a963639-72c3-4e9f-9058-bcfc8fe0bc9e"), new Guid("e8c9548b-3d75-4f2b-af4f-f953572c587c"), new Guid("a1a975a4-7d1e-4734-962e-2f717386783a")).WithObjectTypes(c2, i2).WithSingularName("I2Many2Many").WithPluralName("I2Many2Manies").WithMultiplicity(Multiplicity.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("50300577-b5fb-4c16-9ac5-41151543f958"), new Guid("1f16f92e-ba99-4553-bd1d-b95ba360468a"), new Guid("6210478c-86e3-4d8c-8e3c-3b29da3175ca")).WithObjectTypes(c2, i12).WithSingularName("I12Many2Many").WithPluralName("I12Many2Manies").WithMultiplicity(Multiplicity.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("60680366-4790-4443-a941-b30cd4bd3848"), new Guid("8fa68cfd-8e0c-40c6-881b-4ebe88487ae7"), new Guid("bfa632a3-f334-4c92-a1b1-21cfa726ab90")).WithObjectTypes(c2, c2).WithSingularName("C2One2Many").WithPluralName("C2One2Manies").WithMultiplicity(Multiplicity.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("61daaaae-dd22-405e-aa98-6321d2f8af04"), new Guid("a0291a20-3519-44e6-bb8d-b53682c02c52"), new Guid("bff48eef-9e8f-45b7-83ff-7b63dac407f1")).WithObjectTypes(c2, AllorsBoolean).Build();
            new RelationTypeBuilder(domain, new Guid("65a246a7-cd78-45eb-90db-39f542e7c6cf"), new Guid("eb4f1289-1c6c-4964-a9ba-50f991a96564"), new Guid("6ff71b5b-723d-424f-9e2f-fb37bb8118fe")).WithObjectTypes(c2, i1).WithSingularName("I1Many2One").WithPluralName("I1Many2Ones").WithMultiplicity(Multiplicity.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("67780894-fa62-48ba-8f47-7f54106090cd"), new Guid("38cd28ba-c584-4d06-b521-dcc8094c5ed3"), new Guid("128eb00f-03fc-432e-bec6-8fcfb265a3a9")).WithObjectTypes(c2, i1).WithSingularName("I1One2One").WithPluralName("I1One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("70600f67-7b18-4b5c-b11e-2ed180c5b2d6"), new Guid("a373cb01-731b-48be-a387-d057fdb70684"), new Guid("572738e4-956b-404d-97e8-4bb431ce7692")).WithObjectTypes(c2, c1).WithSingularName("C1Many2Many").WithPluralName("C1Many2Manies").WithMultiplicity(Multiplicity.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("770eb33c-c8ef-4629-a3a0-20decd92ff62"), new Guid("de757393-f81a-413c-897b-a47efd48cc79"), new Guid("8ac9a5cd-35a4-4ca3-a1af-ab3f489c7a52")).WithObjectTypes(c2, i12).WithSingularName("I12One2Many").WithPluralName("I12One2Manies").WithMultiplicity(Multiplicity.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("7a9129c9-7b6d-4bdd-a630-cfd1392549b7"), new Guid("87f7a34c-476f-4670-a670-30451c05842d"), new Guid("19f3caa1-c8d1-4257-b4ad-2f8ccb809524")).WithObjectTypes(c2, i2).WithSingularName("I2One2Many").WithPluralName("I2One2Manies").WithMultiplicity(Multiplicity.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("86ad371b-0afd-420b-a855-38ebb3f39f38"), new Guid("23f5e29b-c36b-416f-93da-9ef2d79fc0f1"), new Guid("cdf7b6ee-fa50-44a1-9433-d04d61ef3aeb")).WithObjectTypes(c2, c2).WithSingularName("C2One2One").WithPluralName("C2One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("9c7cde3f-9b61-4c79-a5d7-afe1067262ce"), new Guid("71d6109e-1c04-4598-88fa-f06308beb45b"), new Guid("8a96d544-e96f-45b5-aeee-d9381946ff31")).WithObjectTypes(c2, AllorsString).WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("a5315151-aa0f-42a3-9d5b-2c7f2cb92560"), new Guid("f2bf51b6-0375-4d77-8881-d4d313d682ef"), new Guid("54dce296-9454-440e-9cf3-1327ea439f0e")).WithObjectTypes(c2, c1).WithSingularName("C1Many2One").WithPluralName("C1Many2Ones").WithMultiplicity(Multiplicity.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("bc6c7fe0-6501-428c-a929-da87a9f4b885"), new Guid("794d2637-293c-49cc-a052-246a779825e9"), new Guid("73d243be-d8d0-42c7-b354-fd9786b4eaaa")).WithObjectTypes(c2, c2).WithSingularName("C2Many2Many").WithPluralName("C2Many2Manies").WithMultiplicity(Multiplicity.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("e08d75a9-9b67-4d20-a476-757f8fb17376"), new Guid("7d45be10-724e-46c4-8dac-4acdf7f515ad"), new Guid("888cd015-7323-45da-83fe-eb297e8ede51")).WithObjectTypes(c2, i2).WithSingularName("I2Many2One").WithPluralName("I2Many2Ones").WithMultiplicity(Multiplicity.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("f748949e-de5a-4f2e-85e2-e15516d9bf24"), new Guid("92c02837-9e6c-45ad-8772-b97a17afad8c"), new Guid("2c172bc6-a87b-4945-b02f-e00a38eb866d")).WithObjectTypes(c2, c1).WithSingularName("C1One2Many").WithPluralName("C1One2Manies").WithMultiplicity(Multiplicity.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("fa8ad982-9953-47dd-9905-81cc4572300e"), new Guid("604eec66-6a75-465b-93d8-349dcbccb2bd"), new Guid("e701ac90-488a-476f-9b13-ea361e8ff450")).WithObjectTypes(c2, AllorsBinary).WithSize(-1).Build();

            // To
            new RelationTypeBuilder(domain, new Guid("4be564ac-77bc-48b8-b945-7d39f2ea9903"), new Guid("7a6714c1-e58a-45ac-8ee5-ca5f22b6d528"), new Guid("53e0761a-a9f1-4516-a086-b766650ac28b")).WithObjectTypes(to, AllorsString).WithSingularName("Name").WithPluralName("Names").WithSize(256).Build();

            // MailboxAddress
            new RelationTypeBuilder(domain, new Guid("03c9970e-d9d6-427d-83d0-00e0888f5588"), new Guid("8d565792-4315-44eb-9930-55aa30e8f23a"), new Guid("10b46f89-7f3a-4571-8621-259a2a501dc7")).WithObjectTypes(mailboxAddress, AllorsString).WithSingularName("PoBox").WithPluralName("PoBoxes").WithSize(256).Build();

            // Extender
            new RelationTypeBuilder(domain, new Guid("525bbc9e-d488-419f-ac02-0ab6ac409bac"), new Guid("7dcdf3d7-25ad-4e8f-9634-63b771990681"), new Guid("bf9f7482-5277-40db-a6ac-5d4731cb5537")).WithObjectTypes(extender, AllorsString).WithSize(256).Build();

            // Two
            new RelationTypeBuilder(domain, new Guid("8930c13c-ad5a-4b0e-b3bf-d7cdf6f5b867"), new Guid("fd97db6d-d946-47ba-a2a0-88b732457b96"), new Guid("39eda296-4e8d-492b-b0c1-756ffcf9a493")).WithObjectTypes(two, shared).WithMultiplicity(Multiplicity.ManyToOne).WithIsIndexed(true).Build();

            // SearchTest
            new RelationTypeBuilder(domain, new Guid("89c930ae-82ce-4f05-9c1f-a9bf9d8e8bb4"), new Guid("aabeaa02-db59-4fb4-8c47-06ad2edb1d0b"), new Guid("1e3147ee-7425-4aa0-a18f-d5c8dc994a8c")).WithObjectTypes(searchTest, AllorsString).WithSingularName("Text").WithPluralName("Texts").WithSize(256).Build();

            // I12
            new RelationTypeBuilder(domain, new Guid("042d1311-1c06-4d7c-b68e-eb734f9c7327"), new Guid("0d3f0f95-aaa2-47bb-9e2b-654d2747b2b1"), new Guid("f7809a25-1b10-4eb0-9309-aeea6efcd7cb")).WithObjectTypes(i12, AllorsBinary).WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("107c212d-cc1c-41b2-9c1d-b40c0102072c"), new Guid("0a1b3b66-6bb2-4062-b3bb-991987dd5194"), new Guid("4c448b25-b56c-4486-b0c8-ac04a3249677")).WithObjectTypes(i12, c2).WithSingularName("C2One2One").WithPluralName("C2One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("1611cb5d-4676-4e85-bfc5-5572e8ff1138"), new Guid("4af20cc8-a610-4493-9420-7cd110cc6755"), new Guid("5f2eff86-71bf-480d-a6ad-1c93fc68b08d")).WithObjectTypes(i12, AllorsFloat).Build();
            new RelationTypeBuilder(domain, new Guid("167b53c0-644c-467e-9f7c-fcb9415d02c6"), new Guid("d039c8f9-217a-46cc-b428-7480d4991e1e"), new Guid("2e3dc9b9-3700-4090-bafa-2c60050d52d5")).WithObjectTypes(i12, i1).WithSingularName("I1Many2One").WithPluralName("I1Many2Ones").WithMultiplicity(Multiplicity.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("199a84c4-c7cb-4f23-8b6c-078b14525e18"), new Guid("65ed1ff6-eb81-410d-8817-62d61765408a"), new Guid("c778c7a7-9cf7-4a7e-8408-e4eb1ca94ce8")).WithObjectTypes(i12, AllorsString).WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("1bf2abe0-9273-4fb9-b491-020320f1f8db"), new Guid("732fc964-194e-4ece-bd39-bb3c47b83ff9"), new Guid("b311c57d-9565-48c1-80d8-1d3cf5a498ea")).WithObjectTypes(i12, i12).WithSingularName("I12Many2Many").WithPluralName("I12Many2Manies").WithMultiplicity(Multiplicity.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("41a74fec-cfbc-43ca-a6e7-890f0dd1eddb"), new Guid("7293e939-ad0b-4b62-935d-44a5309f2515"), new Guid("295a4e46-3133-4aff-a1dc-5101e584fb8a")).WithObjectTypes(i12, AllorsDecimal).WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("4a2b2f43-037d-4149-8a1e-401e5df963ba"), new Guid("cd90d290-95da-4137-aaf1-bcb59f10e9cb"), new Guid("f266759c-34c5-49a8-8d92-e2bbcb41c86a")).WithObjectTypes(i12, i2).WithSingularName("I2Many2Many").WithPluralName("I2Many2Manies").WithMultiplicity(Multiplicity.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("51ebb024-c847-4165-b216-b3b6e8883961"), new Guid("04bca123-7c45-43f4-a5ed-8691b0cbb0e3"), new Guid("f5928b47-5a57-4be8-a0a9-a729e8e467bb")).WithObjectTypes(i12, c2).WithSingularName("C2Many2Many").WithPluralName("C2Many2Manies").WithMultiplicity(Multiplicity.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("59ae05e3-573c-4ea4-9181-2c545236ed1e"), new Guid("064f5e1b-b5c8-45ee-baf1-094f6a723ede"), new Guid("397b339e-0277-4700-a5d1-d9d0ac23c362")).WithObjectTypes(i12, i1).WithSingularName("I1Many2Many").WithPluralName("I1Many2Manies").WithMultiplicity(Multiplicity.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("5e473f63-b1d7-4530-b64f-26435fb5063c"), new Guid("83e23750-52eb-4b3f-a675-bfe32570357b"), new Guid("d786aeb4-03bb-419a-90c9-e6ddaa940e93")).WithObjectTypes(i12, i12).WithSingularName("I12One2Many").WithPluralName("I12One2Manies").WithMultiplicity(Multiplicity.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("6daafb16-1bc3-4f15-8e25-1a982c5bb3c5"), new Guid("d39d3782-71a6-4b63-aaeb-0a6da0db153d"), new Guid("a89707e2-e3e1-4d24-9c56-180671e3409c")).WithObjectTypes(i12, AllorsString).WithSingularName("Name").WithPluralName("Names").WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("7827af95-147f-4803-865a-b418d567da68"), new Guid("7e707f89-6dd2-44a4-8f85-e00666af4d00"), new Guid("a4c1f678-a3ae-4707-81e9-b3f3411a5d93")).WithObjectTypes(i12, c1).WithSingularName("C1Many2Many").WithPluralName("C1Many2Manies").WithMultiplicity(Multiplicity.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("7f6fdb73-3e19-40e7-8feb-6ddbdf2e745a"), new Guid("644f55c6-8d39-4602-89bb-5797c9c8e1fd"), new Guid("2073096f-8918-4432-8fa2-42f4fd1a53a1")).WithObjectTypes(i12, i2).WithSingularName("I2Many2One").WithPluralName("I2Many2Ones").WithMultiplicity(Multiplicity.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("93a59d0a-278d-435b-967e-551523f0cb85"), new Guid("9c700ad0-e33e-4731-ac3a-4063c2da655b"), new Guid("839c7aaa-f044-4b93-97aa-00beeed8f3eb")).WithObjectTypes(i12, AllorsUnique).Build();
            new RelationTypeBuilder(domain, new Guid("95551e3a-bad2-4136-923f-c8e5f0f2aec7"), new Guid("f57afc9e-3832-4ae1-b3a0-659d7f00604c"), new Guid("cbd73ad2-a4cd-4b65-a3cd-55bb7c6f52bc")).WithObjectTypes(i12, AllorsInteger).Build();
            new RelationTypeBuilder(domain, new Guid("95c77a0f-7f4c-4142-a93f-f688cfd554af"), new Guid("870af1ab-075f-4e19-a283-6e6875d362bb"), new Guid("29f38fb4-8e6a-4f70-9ee9-f6819b9d759e")).WithObjectTypes(i12, i1).WithSingularName("I1One2Many").WithPluralName("I1One2Manies").WithMultiplicity(Multiplicity.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("9aefdda0-e547-4c9b-bf28-431669f8ea2e"), new Guid("f4399c8b-3394-4c2a-9ff0-16b2ece87fdf"), new Guid("ee9379c4-ef6a-4c6e-8190-bc71c36ac009")).WithObjectTypes(i12, c1).WithSingularName("C1One2One").WithPluralName("C1One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("a89b4c06-bba5-4b05-bd6f-c32bc195c32f"), new Guid("8dd3e2b6-805f-4c93-98d8-4864e6807760"), new Guid("e68fba09-6113-4b49-a6fa-a09e46a004f1")).WithObjectTypes(i12, i12).WithSingularName("I12One2One").WithPluralName("I12One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("ac920d1d-290b-484b-9283-3829337182bc"), new Guid("991e5b73-a9b0-40a4-8230-b3fb7cc46761"), new Guid("07702752-2c97-4b44-8c43-7c1f2a5e3d0d")).WithObjectTypes(i12, i2).WithSingularName("I2One2One").WithPluralName("I2One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("b2e3ddda-0cc3-4cfd-a114-9040882ec58a"), new Guid("014cf60e-595f-42d5-9146-e7d860396f4d"), new Guid("d5c22b99-6984-4042-98fd-93fe60dfe5d7")).WithObjectTypes(i12, i12).WithSingularName("Dependency").WithPluralName("Dependencies").WithMultiplicity(Multiplicity.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("b2f568a1-51ba-4b6b-a1f1-b82bdec382b5"), new Guid("6f37656a-21d0-4574-8eac-5342f7c6850d"), new Guid("09a2a7a1-4713-4c5c-828d-8be40f33d1ae")).WithObjectTypes(i12, i2).WithSingularName("I2One2Many").WithPluralName("I2One2Manies").WithMultiplicity(Multiplicity.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("c018face-b292-455c-a2c0-8f71377fb6cb"), new Guid("3239eb17-dc55-465f-854c-1d2d024bca94"), new Guid("2ff52878-3ade-4afe-9961-8f79336bb0a2")).WithObjectTypes(i12, c2).WithSingularName("C2Many2One").WithPluralName("C2Many2Ones").WithMultiplicity(Multiplicity.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("c6ecc142-0fbd-48b7-98ae-994fa9b5b814"), new Guid("c7469ffd-ffd7-4913-962c-8a7a0b4df3dd"), new Guid("1d091625-ec4a-486d-a9be-8f87fe300967")).WithObjectTypes(i12, i12).WithSingularName("I12Many2One").WithPluralName("I12Many2Ones").WithMultiplicity(Multiplicity.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("ccdd1ae2-263e-4221-9841-4cff1907ee8d"), new Guid("55be99e6-71fd-4483-b211-c3080e6ffa05"), new Guid("79723949-b9ad-40bf-baee-96d001942855")).WithObjectTypes(i12, AllorsBoolean).Build();
            new RelationTypeBuilder(domain, new Guid("ce0f7d58-b415-43f3-989b-9d8b34754e4b"), new Guid("33bd508e-d754-4533-9ecd-9c8ce8d10c88"), new Guid("72545574-d138-467c-8f21-0c5d15b1d793")).WithObjectTypes(i12, i1).WithSingularName("I1One2One").WithPluralName("I1One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("f302dd07-1abc-409e-aa71-ec9f7ac439aa"), new Guid("99b3bf26-3437-4b5b-a786-28c095975a48"), new Guid("ee291df6-6a3e-4d92-a779-879679e1b688")).WithObjectTypes(i12, c1).WithSingularName("C1One2Many").WithPluralName("C1One2Manies").WithMultiplicity(Multiplicity.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("f6436bc9-e307-4001-8f1f-5b37553ab3c6"), new Guid("63297178-60c1-4cbc-a68d-2842385ba266"), new Guid("6e5b98b0-1af3-4e99-8781-37ea97792a24")).WithObjectTypes(i12, c1).WithSingularName("C1Many2One").WithPluralName("C1Many2Ones").WithMultiplicity(Multiplicity.ManyToOne).WithIsIndexed(true).Build();

            // BadUI
            new RelationTypeBuilder(domain, new Guid("8a999086-ca90-40a1-90ae-475d231bb1eb"), new Guid("0ce20e7c-7be0-4c07-a179-e8d0e77f3de1"), new Guid("4ab20876-f8fc-4d39-87d7-8758f044587b")).WithObjectTypes(badUi, Person).WithSingularName("PersonMany").WithPluralName("PersonsMany").WithMultiplicity(Multiplicity.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("9ebbb9d1-2ca7-4a7f-9f18-f25c05fd28c1"), new Guid("37c64e26-a391-4c7b-98fb-53ccb5fbc795"), new Guid("4d2c7c20-b9c7-451b-b6b1-8552322ceddd")).WithObjectTypes(badUi, organisation).WithSingularName("CompanyOne").WithPluralName("CompanyOnes").WithMultiplicity(Multiplicity.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("a4db0d75-3dff-45ac-9c1d-623bca046b4a"), new Guid("5ed577d8-f048-42b8-9fb4-38b88ebf35f1"), new Guid("c1b45f09-59fe-4484-8999-e2a3d9147919")).WithObjectTypes(badUi, Person).WithSingularName("PersonOne").WithPluralName("PersonOnes").WithMultiplicity(Multiplicity.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("a8621048-48b5-43c4-b10b-17225958d177"), new Guid("718eaf0b-1b62-43b2-b336-c9820d806b28"), new Guid("1663525b-5add-4a96-a596-5d736d466985")).WithObjectTypes(badUi, organisation).WithSingularName("CompanyMany").WithPluralName("CompanyManies").WithMultiplicity(Multiplicity.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("c93a102e-ecdb-4189-a0fc-eeea8b4b85d4"), new Guid("2225f3e0-1304-4a55-9b89-29563fe52e3c"), new Guid("7f2dc0db-4628-45a8-8cc5-2cc1b87e0eb3")).WithObjectTypes(badUi, AllorsString).WithSize(256).Build();

            // Three
            new RelationTypeBuilder(domain, new Guid("1697f09c-0d3d-4e5e-9f3f-9d3ae0718fd3"), new Guid("dc813d9a-84e9-4995-8d2c-0ef449b12024"), new Guid("25737278-d039-47c5-8749-19f22ad7a4c3")).WithObjectTypes(three, four).WithMultiplicity(Multiplicity.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("4ace9948-4a22-465c-aa40-61c8fd65784d"), new Guid("6e20b25f-3ecd-447e-8a93-3977a53452b6"), new Guid("f8f85b3d-371c-42df-8414-cf034c339917")).WithObjectTypes(three, AllorsString).WithSize(-1).Build();

            // Second
            new RelationTypeBuilder(domain, new Guid("4f0eba0d-09b4-4bbc-8e42-15de94921ab5"), new Guid("08d8689d-88ce-496d-95e4-f20af0677cac"), new Guid("ec263924-1234-4b53-9d33-91e167d6862f")).WithObjectTypes(second, third).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("8a7b7af9-f421-4e96-a1a7-04d4c4bdd1d7"), new Guid("e986349f-fc8c-4627-9bf7-966ad6299cff"), new Guid("3f37f82c-3f7a-4d4c-b775-4ff09c105f92")).WithObjectTypes(second, AllorsBoolean).WithSingularName("IsDerived").WithPluralName("IsDeriveds").Build();

            // DerivationLogI12
            new RelationTypeBuilder(domain, new Guid("0b89b096-a69a-495c-acfe-b24a9b27e320"), new Guid("e178ed0f-7764-4836-bd6f-fcb7f5d62346"), new Guid("007a6d25-8506-483d-9140-414c0056d812")).WithObjectTypes(derivationLogI12, AllorsUnique).WithSingularName("UniqueId").WithPluralName("UniqueIds").Build();

            // I1
            new RelationTypeBuilder(domain, new Guid("06b72534-49a8-4f6d-a991-bc4aaf6f939f"), new Guid("854c6ec4-51d4-4d68-bd26-4168c26707de"), new Guid("9fd09ce4-3f52-4889-b018-fd9374656e8c")).WithObjectTypes(i1, i1).WithSingularName("I1Many2One").WithPluralName("I1Many2Ones").WithMultiplicity(Multiplicity.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("0a2895ec-0102-493d-9b94-e12e94b4a403"), new Guid("295bfc0e-e123-4ac8-84da-45e8d77b1865"), new Guid("94c8ca3f-45d6-4f70-8b4a-5d469b0ee897")).WithObjectTypes(i1, i12).WithSingularName("I12Many2Many").WithPluralName("I12Many2Manies").WithMultiplicity(Multiplicity.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("0acbea28-f8aa-477c-b296-b8976d9b10a5"), new Guid("5b4da68a-6aeb-4d5c-8e09-5bef3b1358a9"), new Guid("5e8608ed-7987-40d0-a877-a244d6520554")).WithObjectTypes(i1, i2).WithSingularName("I2Many2Many").WithPluralName("I2Many2Manies").WithMultiplicity(Multiplicity.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("194580f4-e0e3-4b52-b9ba-6020171be4e9"), new Guid("39a81eb4-e1bb-45ef-8126-21cf233ba684"), new Guid("98017570-bc3b-442b-9e51-b16565fa443c")).WithObjectTypes(i1, i2).WithSingularName("I2Many2One").WithPluralName("I2Many2Ones").WithMultiplicity(Multiplicity.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("28ceffc2-c776-4a0a-9825-a6d1bcb265dc"), new Guid("0287a603-59e5-4241-8b2e-a21698476e67"), new Guid("fec573a7-5ab3-4f30-9b50-7d720b4af4b4")).WithObjectTypes(i1, AllorsString).WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("2e85d74a-8d13-4bc0-ae4f-42b305e79373"), new Guid("d6ccfcb8-623e-4852-a878-d7cb377af853"), new Guid("ec030f88-1060-4c2b-bda1-d9c5dc4fc9d3")).WithObjectTypes(i1, i12).WithSingularName("I12Many2One").WithPluralName("I12Many2Ones").WithMultiplicity(Multiplicity.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("39e28141-fd6b-4f49-8884-d5400f6c57ff"), new Guid("9118c09c-e8c2-4685-a464-9be9ece2e746"), new Guid("a4b456e2-b45f-4398-875b-4ba99ead49fe")).WithObjectTypes(i1, i2).WithSingularName("I2One2Many").WithPluralName("I2One2Manies").WithMultiplicity(Multiplicity.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("4506a14b-22f1-41fe-972b-40fab7c6dd31"), new Guid("54c659d3-98ff-45e6-b734-bc45f13428d8"), new Guid("d75a5613-4ed9-494f-accf-352d9e115ba9")).WithObjectTypes(i1, c2).WithSingularName("C2One2Many").WithPluralName("C2One2Manies").WithMultiplicity(Multiplicity.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("593914b1-af95-4992-9703-2b60f4ea0926"), new Guid("ee0f3844-928b-4968-9077-afd255554c8b"), new Guid("bca02f1e-a026-4c0b-9762-1bd52d49b953")).WithObjectTypes(i1, c1).WithSingularName("C1One2One").WithPluralName("C1One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("5cb44331-fd8c-4f73-8994-161f702849b6"), new Guid("2484aae6-db3b-4795-be76-016b33cbb679"), new Guid("c9f9dd15-54b4-4847-8b7e-ac88063804a3")).WithObjectTypes(i1, AllorsInteger).Build();
            new RelationTypeBuilder(domain, new Guid("6199e5b4-133d-4d0e-9941-207316164ec8"), new Guid("75342efb-659c-43a9-8340-1e110087141c"), new Guid("920f26a7-971a-4771-81b1-af3972c997ff")).WithObjectTypes(i1, c2).WithSingularName("C2Many2Many").WithPluralName("C2Many2Manies").WithMultiplicity(Multiplicity.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("670c753e-8ea0-40b1-bfc9-7388074191d3"), new Guid("b1c6c329-09e3-4b07-8ddf-e6a4fd8d0285"), new Guid("6d36c9f9-1426-46a5-8d4f-7275a51c9c17")).WithObjectTypes(i1, i1).WithSingularName("I1One2Many").WithPluralName("I1One2Manies").WithMultiplicity(Multiplicity.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("6bb3ba6d-ffc7-4700-9723-c323b9b2d233"), new Guid("86623fe9-c7cc-4328-85d9-b0dfce2b9a59"), new Guid("9c64a761-136a-43aa-bef9-6bcd1259d591")).WithObjectTypes(i1, i1).WithSingularName("I1Many2Many").WithPluralName("I1Many2Manies").WithMultiplicity(Multiplicity.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("6c3d04be-6f95-44b8-863a-245e150e3110"), new Guid("e6c314af-d366-4169-b28d-9dc83d694079"), new Guid("631a2bdb-ceca-43b2-abb8-9c9ea743c9de")).WithObjectTypes(i1, AllorsBoolean).Build();
            new RelationTypeBuilder(domain, new Guid("818b4013-5ef1-4455-9f0d-9a39fa3425bb"), new Guid("335902bc-6bfa-4c7b-b52f-9a617c746afd"), new Guid("56e68d93-a62f-4090-a93a-8f0f364b08bd")).WithObjectTypes(i1, AllorsDecimal).WithPrecision(10).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("a51d9d21-40ec-44b9-853d-8c18f54d659d"), new Guid("1d785350-3f68-4f8d-86d4-74a0cd8adac7"), new Guid("222d2644-197d-4420-a01a-276b35ad61d1")).WithObjectTypes(i1, i12).WithSingularName("I12One2One").WithPluralName("I12One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("a5761a0e-5c10-407a-bd68-0c4f69d78968"), new Guid("b6cf882a-e27a-40e3-9a0d-43ade4d236b6"), new Guid("3950129b-6ac5-4eae-b5c2-de12500b0561")).WithObjectTypes(i1, i2).WithSingularName("I2One2One").WithPluralName("I2One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("b6e0fce0-14fc-46e3-995d-1b6e3699ed96"), new Guid("ddc18ebf-0b61-441f-854a-0f964859035e"), new Guid("3899bad1-d563-4f65-85b1-2b274b6a278f")).WithObjectTypes(i1, c2).WithSingularName("C2One2One").WithPluralName("C2One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("b89092f1-8775-4b6a-99ef-f8626bc770bd"), new Guid("d0b99a68-2104-4c4d-ba4c-73d725e406e9"), new Guid("6303d423-6cc4-4933-9546-4b6b39fa0ae4")).WithObjectTypes(i1, c1).WithSingularName("C1One2Many").WithPluralName("C1One2Manies").WithMultiplicity(Multiplicity.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("b9c67658-4abc-41f3-9434-c8512a482179"), new Guid("ba4fa583-b169-4327-a60a-fc0d2c142b3f"), new Guid("bbd469af-25f5-47aa-86f6-80d3bba53ce5")).WithObjectTypes(i1, AllorsBinary).WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("bcc9eee6-fa07-4d37-be84-b691bfce24be"), new Guid("b6c7354a-4997-4764-826a-0c9989431d3b"), new Guid("7da3b7ea-2e1a-400c-adbf-436d35720ae9")).WithObjectTypes(i1, c1).WithSingularName("C1Many2Many").WithPluralName("C1Many2Manies").WithMultiplicity(Multiplicity.ManyToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("cdb758bf-ecaf-4d99-88fb-58df9258c13c"), new Guid("62961c44-f0ab-4edf-9aa7-63312643e6b4"), new Guid("e33e809e-bbd3-4ecc-b46e-e233c5c93ce6")).WithObjectTypes(i1, AllorsFloat).Build();
            new RelationTypeBuilder(domain, new Guid("e1b13216-7210-4c24-a668-83b40162a21b"), new Guid("f14f50da-635f-47d0-9f3d-28364b767637"), new Guid("911abf5b-ea84-4ffe-b6fb-558b4af50503")).WithObjectTypes(i1, i1).WithSingularName("I1One2One").WithPluralName("I1One2Ones").WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("e3126228-342a-4415-a2e8-d52eceaeaf89"), new Guid("202575b6-aaff-46ce-9e8a-e976a8a9d263"), new Guid("2598d7df-a764-4b6e-bf91-5234404b97c2")).WithObjectTypes(i1, c1).WithSingularName("C1Many2One").WithPluralName("C1Many2Ones").WithMultiplicity(Multiplicity.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("e386cca6-e738-4c37-8bfc-b23057d7a0be"), new Guid("a3af5653-20c0-410c-9a6f-160e10e2fe69"), new Guid("6c708f4b-9fb1-412b-84c8-02f03efede5e")).WithObjectTypes(i1, i12).WithSingularName("I12One2Many").WithPluralName("I12One2Manies").WithMultiplicity(Multiplicity.OneToMany).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("ef1a0a5e-1794-4478-9d0a-517182355206"), new Guid("7b80b14e-dd35-4e7c-ba85-ac7860a5dc28"), new Guid("1d51d303-f68b-4dca-9299-a6376e13c90e")).WithObjectTypes(i1, c2).WithSingularName("C2Many2One").WithPluralName("C2Many2Ones").WithMultiplicity(Multiplicity.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("f9d7411e-7993-4e43-a7e2-726f1e44e29c"), new Guid("84ae4441-5f83-4196-8439-483311b05055"), new Guid("5ebf419f-1c7f-46f2-844c-0f54321888ee")).WithObjectTypes(i1, AllorsUnique).Build();

            // Person
            new RelationTypeBuilder(domain, new Guid("0375a3d3-1a1b-4cbb-b735-1fe508bcc672"), new Guid("ebaedf39-1af9-42b7-83dc-8945450cebf2"), new Guid("86685c44-5196-46dd-9260-e40a434e9a52")).WithObjectTypes(Person, address).WithSingularName("MainAddress").WithPluralName("MainAddresses").WithMultiplicity(Multiplicity.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("15de4e58-c5ef-4ebb-9bf6-5ab06a02c5a4"), new Guid("be22968c-a450-418f-8f2e-f6140a56589c"), new Guid("ad249eb0-6cf2-4bcb-b3d1-3ff1282cd2f9")).WithObjectTypes(Person, AllorsString).WithSingularName("TinyMCEText").WithPluralName("TinyMCETexts").WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("1b057406-3343-426b-ab5b-ceb93ba02446"), new Guid("91d44bdd-7b17-4fa7-aeb7-625571b252b9"), new Guid("93d01c4a-0aa3-4d7c-a6d8-139b8ed1ffcc")).WithObjectTypes(Person, AllorsString).WithSingularName("Text").WithPluralName("Texts").WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("2a25125f-3545-4209-afc6-523eb0d8851e"), new Guid("94b038b3-2dd6-42a8-9cd6-800ddbef104c"), new Guid("fb6dcca2-14a6-4b00-bd3e-81acf59fbbe2")).WithObjectTypes(Person, AllorsInteger).WithSingularName("Age").WithPluralName("Ages").Build();
            new RelationTypeBuilder(domain, new Guid("54f11f06-8d3f-4d58-bcdc-d40e6820fdad"), new Guid("03a7ffcc-4291-4ae1-a2ab-69f7257fbf04"), new Guid("abd2a4b3-4b17-48d4-b465-0ffcb5a2664d")).WithObjectTypes(Person, AllorsBoolean).WithSingularName("IsStudent").WithPluralName("AreStudent").Build();
            new RelationTypeBuilder(domain, new Guid("6340de2a-c3b1-4893-a7f3-cb924b82fa0e"), new Guid("b6ea4ac5-088a-4773-8410-6813d0185d7c"), new Guid("5a472c98-481f-407c-b53e-eaaa7e7a5340")).WithObjectTypes(Person, mailboxAddress).WithSingularName("MailboxAddress").WithPluralName("MailboxAddresses").WithMultiplicity(Multiplicity.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("654f6c84-62f2-4c0a-9d68-532ed3f39447"), new Guid("5ec6caf4-4752-4a89-92ec-13fd69b444f2"), new Guid("34704a90-d513-4fe2-a1ed-ad6d89399c73")).WithObjectTypes(Person, gender).WithSingularName("Gender").WithPluralName("Genders").WithMultiplicity(Multiplicity.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("688ebeb9-8a53-4e8d-b284-3faa0a01ef7c"), new Guid("8a181cec-7bae-4248-8e24-8abc7e01eea2"), new Guid("e431d53c-37ed-4fde-86a9-755f354c1d75")).WithObjectTypes(Person, AllorsString).WithSingularName("FullName").WithPluralName("FullNames").WithIsDerived(true).WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("6b626ba5-0c45-48c7-8b6b-5ea85e002d90"), new Guid("520bb966-6e8a-46a4-a3c0-18422af13cba"), new Guid("66e20063-ab51-417a-8ce4-135bb6e115c1")).WithObjectTypes(Person, AllorsInteger).WithSingularName("ShirtSize").WithPluralName("ShirtSizes").Build();
            new RelationTypeBuilder(domain, new Guid("6cc34453-ac7a-4004-8380-033f92324e99"), new Guid("5a99b822-8c51-4cf6-82e9-ee4ca311216a"), new Guid("cc14daec-604d-4ca6-9908-a57c10ba1403")).WithObjectTypes(Person, AllorsString).WithSingularName("CKEditorText").WithPluralName("CKEditorTexts").WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("a8a3b4b8-c4f2-4054-ab2a-2eac6fd058e4"), new Guid("0fdeacf1-35bd-473d-88a9-acd65803f731"), new Guid("656f11e4-7652-4b4d-9dda-28cfe16333ec")).WithObjectTypes(Person, AllorsBoolean).WithSingularName("IsMarried").WithPluralName("AreMarried").Build();
            new RelationTypeBuilder(domain, new Guid("afc32e62-c310-421b-8c1d-6f2b0bb88b54"), new Guid("c21ebc52-6b32-4af7-847e-d3d7e1c4defe"), new Guid("0aab73c3-f997-4dd9-885a-2c1c892adb0e")).WithObjectTypes(Person, AllorsDecimal).WithSingularName("Weight").WithPluralName("Weights").WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("b3ddd2df-8a5a-4747-bd4f-1f1eb37386b3"), new Guid("912b48f5-215e-4cc0-a83b-56b74d986608"), new Guid("f6624fac-db8e-4fb2-9e86-18021b59d31d")).WithObjectTypes(Person, Media).WithSingularName("Photo").WithPluralName("Photos").WithMultiplicity(Multiplicity.ManyToOne).WithIsIndexed(true).Build();
            new RelationTypeBuilder(domain, new Guid("e9e7c874-4d94-42ff-a4c9-414d05ff9533"), new Guid("da5e0427-79f7-4259-8a68-0071aa4c6273"), new Guid("c922b44f-6c6f-4e8b-901d-6558e79bb558")).WithObjectTypes(Person, address).WithSingularName("Address").WithPluralName("Addresses").WithMultiplicity(Multiplicity.ManyToMany).WithIsIndexed(true).Build();
            
            // Singleton
            new RelationTypeBuilder(domain, new Guid("9ce2ef9b-2376-474d-9aa2-d23fbe1ed236"), new Guid("04bc6904-bd6e-4401-9720-088ebf1fb392"), new Guid("7ab62a77-c098-4ad6-836d-53ae820df951")).WithObjectTypes(Singleton, StringTemplate).WithSingularName("PersonTemplate").WithPluralName("PersonTemplates").WithMultiplicity(Multiplicity.ManyToOne).WithIsIndexed(true).Build();


            // MethodTypes
            // C1
            new MethodTypeBuilder(domain, new Guid("A80E3732-DAF2-4AD4-9378-B4BC13E74DDE")).WithObjectType(c1).WithName("ClassMethod").Build();
            new MethodTypeBuilder(domain, new Guid("336DC840-BDD8-45CC-8B95-DD0EA55F130D")).WithObjectType(i1).WithName("InterfaceMethod").Build();
            new MethodTypeBuilder(domain, new Guid("5C7F1AB4-0B61-416D-97F4-660663F0E933")).WithObjectType(s1).WithName("SuperinterfaceMethod").Build();

            // Organisation
            new MethodTypeBuilder(domain, new Guid("55AAC529-BEAE-4D29-B069-DECDA86710A9")).WithObjectType(organisation).WithName("JustDoIt").Build();
        }
    }
}