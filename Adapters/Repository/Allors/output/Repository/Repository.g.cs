namespace Allors.Meta
{
	using System;
	using Allors.Meta;

	public partial static class Repository
	{
		static void Base(Environment env)
		{
            	var domain = Domain.Create(env, new Guid("26B81EE6-08B6-48E8-931C-B8D944ED1C42"));
				domain.Name = "Base";

				var AllorsString = new ClassBuilder(domain, new Guid("ad7f5ddc-bedb-4aaa-97ac-d6693a009ba9")).WithSingularName("AllorsString").WithPluralName("AllorsStrings").Build();
  

				var AllorsInteger = new ClassBuilder(domain, new Guid("ccd6f134-26de-4103-bff9-a37ec3e997a3")).WithSingularName("AllorsInteger").WithPluralName("AllorsIntegers").Build();
  

				var AllorsLong = new ClassBuilder(domain, new Guid("e8989069-024b-4389-ac77-a98c4dfff25a")).WithSingularName("AllorsLong").WithPluralName("AllorsLongs").Build();
  

				var AllorsDecimal = new ClassBuilder(domain, new Guid("da866d8e-2c40-41a8-ae5b-5f6dae0b89c8")).WithSingularName("AllorsDecimal").WithPluralName("AllorsDecimals").Build();
  

				var AllorsDouble = new ClassBuilder(domain, new Guid("ffcabd07-f35f-4083-bef6-f6c47970ca5d")).WithSingularName("AllorsDouble").WithPluralName("AllorsDoubles").Build();
  

				var AllorsBoolean = new ClassBuilder(domain, new Guid("b5ee6cea-4e2b-498e-a5dd-24671d896477")).WithSingularName("AllorsBoolean").WithPluralName("AllorsBooleans").Build();
  

				var AllorsDate = new ClassBuilder(domain, new Guid("c402fbda-133a-4b59-858a-832fbd2ec565")).WithSingularName("AllorsDate").WithPluralName("AllorsDates").Build();
  

				var AllorsDateTime = new ClassBuilder(domain, new Guid("c4c09343-61d3-418c-ade2-fe6fd588f128")).WithSingularName("AllorsDateTime").WithPluralName("AllorsDateTimes").Build();
  

				var AllorsUnique = new ClassBuilder(domain, new Guid("6dc0a1a8-88a4-4614-adb4-92dd3d017c0e")).WithSingularName("AllorsUnique").WithPluralName("AllorsUniques").Build();
  

				var AllorsBinary = new ClassBuilder(domain, new Guid("c28e515b-cae8-4d6b-95bf-062aec8042fc")).WithSingularName("AllorsBinary").WithPluralName("AllorsBinarys").Build();
  


				var LocalisedText = new ClassBuilder(domain, new Guid("020f5d4d-4a59-4d7b-865a-d72fc70e4d97")).WithSingularName("LocalisedText").WithPluralName("LocalisedTexts").Build();
  

				var StringTemplate = new ClassBuilder(domain, new Guid("0c50c02a-cc9c-4617-8530-15a24d4ac969")).WithSingularName("StringTemplate").WithPluralName("StringTemplates").Build();
  

				var Locale = new ClassBuilder(domain, new Guid("45033ae6-85b5-4ced-87ce-02518e6c27fd")).WithSingularName("Locale").WithPluralName("Locales").Build();
  

				var SearchFragment = new ClassBuilder(domain, new Guid("490d150f-3322-4616-a75c-71e4d94b3e03")).WithSingularName("SearchFragment").WithPluralName("SearchFragments").Build();
  

				var Language = new ClassBuilder(domain, new Guid("4a0eca4b-281f-488d-9c7e-497de882c044")).WithSingularName("Language").WithPluralName("Languages").Build();
  

				var SearchData = new ClassBuilder(domain, new Guid("56794636-cfad-47df-8567-84c8ee993ade")).WithSingularName("SearchData").WithPluralName("SearchDatas").Build();
  
  
				var Printable = new InterfaceBuilder(domain, new Guid("61207a42-3199-4249-baa4-9dd11dc0f5b1")).WithSingularName("Printable").WithPluralName("Printables").Build();
  

				var MediaContent = new ClassBuilder(domain, new Guid("6c20422e-cb3e-4402-bb40-dacaf584405e")).WithSingularName("MediaContent").WithPluralName("MediaContents").Build();
  
  
				var Localised = new InterfaceBuilder(domain, new Guid("7979a17c-0829-46df-a0d4-1b01775cfaac")).WithSingularName("Localised").WithPluralName("Localiseds").Build();
  
  
				var Period = new InterfaceBuilder(domain, new Guid("80adbbfd-952e-46f3-a744-78e0ce42bc80")).WithSingularName("Period").WithPluralName("Periods").Build();
  
  
				var SearchResult = new InterfaceBuilder(domain, new Guid("a0ac7040-6984-4267-a200-919875e08909")).WithSingularName("SearchResult").WithPluralName("SearchResults").Build();
  

				var MediaType = new ClassBuilder(domain, new Guid("aa7d61f8-6618-47a0-9cf2-e75dd81dbd5b")).WithSingularName("MediaType").WithPluralName("MediaTypes").Build();
  

				var PrintQueue = new ClassBuilder(domain, new Guid("b45705e3-0dc6-4296-824a-76bb6af223d3")).WithSingularName("PrintQueue").WithPluralName("PrintQueues").Build();
  

				var Enumeration = new ClassBuilder(domain, new Guid("b7bcc22f-03f0-46fd-b738-4e035921d445")).WithSingularName("Enumeration").WithPluralName("Enumerations").Build();
  

				var Country = new ClassBuilder(domain, new Guid("c22bf60e-6428-4d10-8194-94f7be396f28")).WithSingularName("Country").WithPluralName("Countries").Build();
  

				var Person = new ClassBuilder(domain, new Guid("c799ca62-a554-467d-9aa2-1663293bb37f")).WithSingularName("Person").WithPluralName("Persons").Build();
  

				var Image = new ClassBuilder(domain, new Guid("caa2a2de-9454-4812-a69f-9d3728706345")).WithSingularName("Image").WithPluralName("Images").Build();
  

				var Media = new ClassBuilder(domain, new Guid("da5b86a3-4f33-4c0d-965d-f4fbc1179374")).WithSingularName("Media").WithPluralName("Medias").Build();
  

				var Currency = new ClassBuilder(domain, new Guid("fd397adf-40b4-4ef8-b449-dd5a24273df3")).WithSingularName("Currency").WithPluralName("Currencies").Build();
  
  
				var Commentable = new InterfaceBuilder(domain, new Guid("fdd52472-e863-4e91-bb01-1dada2acc8f6")).WithSingularName("Commentable").WithPluralName("Commentables").Build();
  
  
				var Searchable = new InterfaceBuilder(domain, new Guid("ff34f3f1-6a17-404f-a9e5-5cffcdaa3d31")).WithSingularName("Searchable").WithPluralName("Searchables").Build();
  

				var Counter = new ClassBuilder(domain, new Guid("0568354f-e3d9-439e-baac-b7dce31b956a")).WithSingularName("Counter").WithPluralName("Counters").Build();
  
  
				var UniquelyIdentifiable = new InterfaceBuilder(domain, new Guid("122ccfe1-f902-44c1-9d6c-6f6a0afa9469")).WithSingularName("UniquelyIdentifiable").WithPluralName("UniquelyIdentifiables").Build();
  

				var Singleton = new ClassBuilder(domain, new Guid("313b97a5-328c-4600-9dd2-b5bc146fb13b")).WithSingularName("Singleton").WithPluralName("Singletons").Build();
  

				var UserGroup = new ClassBuilder(domain, new Guid("60065f5d-a3c2-4418-880d-1026ab607319")).WithSingularName("UserGroup").WithPluralName("UserGroups").Build();
  

				var Permission = new ClassBuilder(domain, new Guid("7fded183-3337-4196-afb0-3266377944bc")).WithSingularName("Permission").WithPluralName("Permissions").Build();
  
  
				var User = new InterfaceBuilder(domain, new Guid("a0309c3b-6f80-4777-983e-6e69800df5be")).WithSingularName("User").WithPluralName("Users").Build();
  

				var SecurityToken = new ClassBuilder(domain, new Guid("a53f1aed-0e3f-4c3c-9600-dc579cccf893")).WithSingularName("SecurityToken").WithPluralName("SecurityTokens").Build();
  
  
				var SecurityTokenOwner = new InterfaceBuilder(domain, new Guid("a69cad9c-c2f1-463f-9af1-873ce65aeea6")).WithSingularName("SecurityTokenOwner").WithPluralName("SecurityTokenOwners").Build();
  

				var Transition = new ClassBuilder(domain, new Guid("a7e490c0-ce29-4298-97c4-519904bb755a")).WithSingularName("Transition").WithPluralName("Transitions").Build();
  
  
				var Transitional = new InterfaceBuilder(domain, new Guid("ab2179ad-9eac-4b61-8d84-81cd777c4926")).WithSingularName("Transitional").WithPluralName("Transitionals").Build();
  

				var Login = new ClassBuilder(domain, new Guid("ad7277a8-eda4-4128-a990-b47fe43d120a")).WithSingularName("Login").WithPluralName("Logins").Build();
  

				var Role = new ClassBuilder(domain, new Guid("af6fe5f4-e5bc-4099-bcd1-97528af6505d")).WithSingularName("Role").WithPluralName("Roles").Build();
  

				var AccessControl = new ClassBuilder(domain, new Guid("c4d93d5e-34c3-4731-9d37-47a8e801d9a8")).WithSingularName("AccessControl").WithPluralName("AccessControls").Build();
  
  
				var AccessControlledObject = new InterfaceBuilder(domain, new Guid("eb0ff756-3e3d-4cf9-8935-8802a73d2df2")).WithSingularName("AccessControlledObject").WithPluralName("AccessControlledObjects").Build();
  
  
				var UserInterfaceable = new InterfaceBuilder(domain, new Guid("eea17b39-8912-40b3-8403-293bd5a3316d")).WithSingularName("UserInterfaceable").WithPluralName("UserInterfaceables").Build();
  

				var ObjectState = new ClassBuilder(domain, new Guid("f991813f-3146-4431-96d0-554aa2186887")).WithSingularName("ObjectState").WithPluralName("ObjectStates").Build();
  


				// Inheritances
				// LocalisedText
				new InheritanceBuilder(domain, new Guid("20881529-dfd1-428a-8aa3-ecea4fe4c8c4")).WithSubtype(LocalisedText).WithSupertype(Searchable).Build();
				new InheritanceBuilder(domain, new Guid("c2b526fd-920d-470a-8a40-405b7e4d8335")).WithSubtype(LocalisedText).WithSupertype(UserInterfaceable).Build();
				new InheritanceBuilder(domain, new Guid("ebe85e2a-084a-452c-896f-aaf390c5bf1a")).WithSubtype(LocalisedText).WithSupertype(Localised).Build();

				// StringTemplate
				new InheritanceBuilder(domain, new Guid("714a9702-01f8-48fc-8add-2f50a8b0bd91")).WithSubtype(StringTemplate).WithSupertype(UniquelyIdentifiable).Build();
				new InheritanceBuilder(domain, new Guid("84fc6495-3e2c-4a99-b0bc-7d818c24eb0b")).WithSubtype(StringTemplate).WithSupertype(Localised).Build();

				// Locale
				new InheritanceBuilder(domain, new Guid("317927bf-e978-4239-b257-a443a22e4665")).WithSubtype(Locale).WithSupertype(UserInterfaceable).Build();

				// SearchFragment
				// Language
				new InheritanceBuilder(domain, new Guid("a4ebd1f9-84db-4888-ba53-414b67b03c73")).WithSubtype(Language).WithSupertype(UserInterfaceable).Build();
				new InheritanceBuilder(domain, new Guid("f26de80c-f10a-4904-978f-aa9848845ec6")).WithSubtype(Language).WithSupertype(Searchable).Build();

				// SearchData
				// Printable
				new InheritanceBuilder(domain, new Guid("82285900-358c-426f-a592-c7ae138287ed")).WithSubtype(Printable).WithSupertype(UserInterfaceable).Build();
				new InheritanceBuilder(domain, new Guid("9bc4b1c9-7e87-4b5b-bcf8-c02b462f0d53")).WithSubtype(Printable).WithSupertype(UniquelyIdentifiable).Build();

				// MediaContent
				// Localised
				// Period
				// SearchResult
				new InheritanceBuilder(domain, new Guid("859646f9-1012-4b8f-b440-157a9afe071f")).WithSubtype(SearchResult).WithSupertype(UserInterfaceable).Build();

				// MediaType
				new InheritanceBuilder(domain, new Guid("8d9af4d9-0b71-46de-a0ca-1bbfef4cdd8f")).WithSubtype(MediaType).WithSupertype(UserInterfaceable).Build();

				// PrintQueue
				new InheritanceBuilder(domain, new Guid("234d4a69-601d-4cf2-b8ce-169198f1ce11")).WithSubtype(PrintQueue).WithSupertype(AccessControlledObject).Build();
				new InheritanceBuilder(domain, new Guid("4954b9f3-85ec-4f57-8c70-40953c3c9296")).WithSubtype(PrintQueue).WithSupertype(UserInterfaceable).Build();
				new InheritanceBuilder(domain, new Guid("e4518c12-8dd9-4762-93b8-ba3e42b6bf0d")).WithSubtype(PrintQueue).WithSupertype(UniquelyIdentifiable).Build();

				// Enumeration
				new InheritanceBuilder(domain, new Guid("9b0a0816-0ec4-4e6d-9617-ba819f7081a5")).WithSubtype(Enumeration).WithSupertype(UserInterfaceable).Build();
				new InheritanceBuilder(domain, new Guid("b5e48ee4-971a-4487-b09c-d0cb397aa0e9")).WithSubtype(Enumeration).WithSupertype(UniquelyIdentifiable).Build();

				// Country
				new InheritanceBuilder(domain, new Guid("1771a830-0d21-4c5e-8d1c-56db36de55b2")).WithSubtype(Country).WithSupertype(UserInterfaceable).Build();
				new InheritanceBuilder(domain, new Guid("7abaf304-080c-4368-ae1f-5be4a03ed403")).WithSubtype(Country).WithSupertype(Searchable).Build();

				// Person
				new InheritanceBuilder(domain, new Guid("306eb440-10ac-40e3-969d-14e3fdab134a")).WithSubtype(Person).WithSupertype(User).Build();
				new InheritanceBuilder(domain, new Guid("4d6db8b5-4f01-4638-806b-7bdc6698901f")).WithSubtype(Person).WithSupertype(AccessControlledObject).Build();
				new InheritanceBuilder(domain, new Guid("9cff65a7-70df-4614-868e-b007cf8d88b8")).WithSubtype(Person).WithSupertype(UniquelyIdentifiable).Build();
				new InheritanceBuilder(domain, new Guid("b4a45e69-a44e-435c-89eb-4a396c5d641f")).WithSubtype(Person).WithSupertype(SearchResult).Build();
				new InheritanceBuilder(domain, new Guid("c0149a55-baa6-44fa-a5c0-135e5602ac25")).WithSubtype(Person).WithSupertype(UserInterfaceable).Build();
				new InheritanceBuilder(domain, new Guid("e8fd24dd-5ffd-4689-aa0b-fe667977cd65")).WithSubtype(Person).WithSupertype(Searchable).Build();

				// Image
				// Media
				new InheritanceBuilder(domain, new Guid("c8cd0830-d1a7-4343-8049-dc18c34c213e")).WithSubtype(Media).WithSupertype(UniquelyIdentifiable).Build();
				new InheritanceBuilder(domain, new Guid("dae544a9-9dea-4b84-99c7-2b701868333d")).WithSubtype(Media).WithSupertype(UserInterfaceable).Build();

				// Currency
				new InheritanceBuilder(domain, new Guid("09b3ac36-f944-4316-9749-99d4f12ffe74")).WithSubtype(Currency).WithSupertype(UserInterfaceable).Build();

				// Commentable
				// Searchable
				// Counter
				new InheritanceBuilder(domain, new Guid("f2a0c00d-ba20-44bd-94ec-1173370d77c9")).WithSubtype(Counter).WithSupertype(UniquelyIdentifiable).Build();

				// UniquelyIdentifiable
				// Singleton
				new InheritanceBuilder(domain, new Guid("dc655fb2-bb19-4338-a641-e95689c58409")).WithSubtype(Singleton).WithSupertype(UserInterfaceable).Build();

				// UserGroup
				new InheritanceBuilder(domain, new Guid("6147b424-b6a9-44b9-b173-30d259165a51")).WithSubtype(UserGroup).WithSupertype(UniquelyIdentifiable).Build();
				new InheritanceBuilder(domain, new Guid("ff0d36e7-49d3-4bea-88d0-f40e8ddb714e")).WithSubtype(UserGroup).WithSupertype(UserInterfaceable).Build();
				new InheritanceBuilder(domain, new Guid("9968c432-0071-49b1-9923-3690d0b32803")).WithSubtype(UserGroup).WithSupertype(Searchable).Build();

				// Permission
				new InheritanceBuilder(domain, new Guid("aec174d4-5633-462c-91a1-10d3e782fdb4")).WithSubtype(Permission).WithSupertype(UserInterfaceable).Build();

				// User
				new InheritanceBuilder(domain, new Guid("17c51f3d-869f-4f1e-95e0-011021837b69")).WithSubtype(User).WithSupertype(SecurityTokenOwner).Build();
				new InheritanceBuilder(domain, new Guid("42aae0bd-7080-4c11-8cbd-1634aa046d32")).WithSubtype(User).WithSupertype(UserInterfaceable).Build();
				new InheritanceBuilder(domain, new Guid("765d49c1-f1ef-4af9-b295-08d0b010b7fe")).WithSubtype(User).WithSupertype(Localised).Build();

				// SecurityToken
				// SecurityTokenOwner
				// Transition
				// Transitional
				new InheritanceBuilder(domain, new Guid("c9e990a7-7853-4b34-860d-672224d36162")).WithSubtype(Transitional).WithSupertype(AccessControlledObject).Build();

				// Login
				// Role
				new InheritanceBuilder(domain, new Guid("4e737d59-0330-4f4e-a3f3-7ec617d63748")).WithSubtype(Role).WithSupertype(UserInterfaceable).Build();
				new InheritanceBuilder(domain, new Guid("816ab651-b27d-4f4f-83d7-39e7b501b2c0")).WithSubtype(Role).WithSupertype(UniquelyIdentifiable).Build();

				// AccessControl
				new InheritanceBuilder(domain, new Guid("b4c7e051-3605-41e6-a78b-edb1c70bde9d")).WithSubtype(AccessControl).WithSupertype(UserInterfaceable).Build();

				// AccessControlledObject
				// UserInterfaceable
				new InheritanceBuilder(domain, new Guid("d583715e-56c3-4212-81c7-3fa4de12144b")).WithSubtype(UserInterfaceable).WithSupertype(AccessControlledObject).Build();

				// ObjectState
				new InheritanceBuilder(domain, new Guid("dd9d3cf5-5c9b-444a-a4b1-a4d807597cf6")).WithSubtype(ObjectState).WithSupertype(UniquelyIdentifiable).Build();




				// RelationTypes
				// LocalisedText
				new RelationTypeBuilder(domain, new Guid("50dc85f0-3d22-4bc1-95d9-153674b89f7a")).WithObjectTypes(LocalisedText, AllorsString).WithSingularName("Text")  .WithPluralName("Texts")      .WithSize(-1).Build();

				// StringTemplate
				new RelationTypeBuilder(domain, new Guid("2f88f9f8-3c22-40d3-885c-2abd43af96cc")).WithObjectTypes(StringTemplate, AllorsString).WithSingularName("Body")  .WithPluralName("Bodies")      .WithSize(-1).Build();
				new RelationTypeBuilder(domain, new Guid("c501103b-037a-4961-93df-2dbb74b88a76")).WithObjectTypes(StringTemplate, AllorsString).WithSingularName("Name")  .WithPluralName("Names")      .WithSize(256).Build();

				// Locale
				new RelationTypeBuilder(domain, new Guid("2a2c6f77-e6a2-4eab-bfe3-5d35a8abd7f7")).WithObjectTypes(Locale, AllorsString).WithSingularName("Name")  .WithPluralName("Names")      .WithSize(256).Build();
				new RelationTypeBuilder(domain, new Guid("d8cac34a-9bb2-4190-bd2a-ec0b87e04cf5")).WithObjectTypes(Locale, Language).WithSingularName("Language")  .WithPluralName("Languages")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
				new RelationTypeBuilder(domain, new Guid("ea778b77-2929-4ab4-ad99-bf2f970401a9")).WithObjectTypes(Locale, Country).WithSingularName("Country")  .WithPluralName("Countries")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();

				// SearchFragment
				new RelationTypeBuilder(domain, new Guid("f7da337d-0650-4051-bf2c-c163d7d061bc")).WithObjectTypes(SearchFragment, AllorsString).WithSingularName("LowerCaseText")  .WithPluralName("LowerCaseTexts")    .WithIsIndexed(true)  .WithSize(256).Build();

				// Language
				new RelationTypeBuilder(domain, new Guid("be482902-beb5-4a76-8ad0-c1b1c1c0e5c4")).WithObjectTypes(Language, AllorsString).WithSingularName("Name")  .WithPluralName("Names")    .WithIsIndexed(true)  .WithSize(256).Build();
				new RelationTypeBuilder(domain, new Guid("d2a32d9f-21cc-4f9d-b0d3-a9b75da66907")).WithObjectTypes(Language, AllorsString).WithSingularName("IsoCode")  .WithPluralName("IsoCodes")      .WithSize(256).Build();
				new RelationTypeBuilder(domain, new Guid("f091b264-e6b1-4a57-bbfb-8225cbe8190c")).WithObjectTypes(Language, LocalisedText).WithSingularName("LocalisedName")  .WithPluralName("LocalisedNames")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();

				// SearchData
				new RelationTypeBuilder(domain, new Guid("46bbb42c-0969-4b5a-92e5-0c8bae2fd16a")).WithObjectTypes(SearchData, AllorsString).WithSingularName("CharacterBoundaryText")  .WithPluralName("CharacterBoundaryTexts")      .WithSize(-1).Build();
				new RelationTypeBuilder(domain, new Guid("7c38c20c-0555-4752-be27-372694a1f81a")).WithObjectTypes(SearchData, AllorsString).WithSingularName("PreviousCharacterBoundaryText")  .WithPluralName("PreviousCharacterBoundaryTexts")      .WithSize(-1).Build();
				new RelationTypeBuilder(domain, new Guid("a0c28d27-7be1-488d-85bf-2f84d4c61a55")).WithObjectTypes(SearchData, SearchFragment).WithSingularName("SearchFragment")  .WithPluralName("SearchFragments")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
				new RelationTypeBuilder(domain, new Guid("a51d3109-7a75-4a8d-9ace-ad9181e64f7d")).WithObjectTypes(SearchData, AllorsString).WithSingularName("PreviousWordBoundaryText")  .WithPluralName("PreviousWordBoundaryTexts")      .WithSize(-1).Build();
				new RelationTypeBuilder(domain, new Guid("d72f473f-8aaf-4abd-be01-f0a683c72721")).WithObjectTypes(SearchData, AllorsString).WithSingularName("WordBoundaryText")  .WithPluralName("WordBoundaryTexts")      .WithSize(-1).Build();

				// Printable
				new RelationTypeBuilder(domain, new Guid("c75d4e4c-d47c-4757-bcb0-25b6daedec9e")).WithObjectTypes(Printable, AllorsString).WithSingularName("PrintContent")  .WithPluralName("PrintContents")  .WithIsDerived("true")    .WithSize(-1).Build();

				// MediaContent
				new RelationTypeBuilder(domain, new Guid("0756d508-44b7-405e-bf92-bc09e5702e63")).WithObjectTypes(MediaContent, AllorsBinary).WithSingularName("Value")  .WithPluralName("Values")  .WithIsDerived("true")    .WithSize(-1).Build();
				new RelationTypeBuilder(domain, new Guid("890598a9-0be4-49ee-8dd8-3581ee9355e6")).WithObjectTypes(MediaContent, AllorsString).WithSingularName("Hash")  .WithPluralName("Hashes")  .WithIsDerived("true")  .WithIsIndexed(true)  .WithSize(1024).Build();

				// Localised
				new RelationTypeBuilder(domain, new Guid("8c005a4e-5ffe-45fd-b279-778e274f4d83")).WithObjectTypes(Localised, Locale).WithSingularName("Locale")  .WithPluralName("Locales")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();

				// Period
				new RelationTypeBuilder(domain, new Guid("5aeb31c7-03d4-4314-bbb2-fca5704b1eab")).WithObjectTypes(Period, AllorsDate).WithSingularName("FromDate")  .WithPluralName("FromDates")      .Build();
				new RelationTypeBuilder(domain, new Guid("d7576ce2-da27-487a-86aa-b0912f745bc0")).WithObjectTypes(Period, AllorsDate).WithSingularName("ThroughDate")  .WithPluralName("ThroughDates")      .Build();

				// SearchResult
				// MediaType
				new RelationTypeBuilder(domain, new Guid("19e52bd9-26cb-4e74-9c28-9f01e684f3da")).WithObjectTypes(MediaType, AllorsString).WithSingularName("DefaultFileExtension")  .WithPluralName("DefaultFileExtensions")  .WithIsDerived("true")    .WithSize(256).Build();
				new RelationTypeBuilder(domain, new Guid("5fcee025-29fd-42d8-ad5a-75cb88d8aef0")).WithObjectTypes(MediaType, AllorsString).WithSingularName("Name")  .WithPluralName("Names")      .WithSize(256).Build();

				// PrintQueue
				new RelationTypeBuilder(domain, new Guid("679156a1-f683-4772-b724-54b318eb3cb3")).WithObjectTypes(PrintQueue, Printable).WithSingularName("Printable")  .WithPluralName("Printables")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
				new RelationTypeBuilder(domain, new Guid("7a85e090-55cf-47f5-912e-4bd87c66a060")).WithObjectTypes(PrintQueue, AllorsString).WithSingularName("Name")  .WithPluralName("Names")    .WithIsIndexed(true)  .WithSize(256).Build();

				// Enumeration
				new RelationTypeBuilder(domain, new Guid("450966da-263e-4666-adf2-b2851c064941")).WithObjectTypes(Enumeration, LocalisedText).WithSingularName("LocalisedName")  .WithPluralName("LocalisedNames")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
				new RelationTypeBuilder(domain, new Guid("459ea6f3-6143-410e-9646-fa4c450b2f67")).WithObjectTypes(Enumeration, AllorsBoolean).WithSingularName("IsActive")  .WithPluralName("AreActive")    .WithIsIndexed(true)  .Build();
				new RelationTypeBuilder(domain, new Guid("8b75617d-7309-4011-b512-8bee2bab9611")).WithObjectTypes(Enumeration, AllorsString).WithSingularName("Name")  .WithPluralName("Names")    .WithIsIndexed(true)  .WithSize(256).Build();

				// Country
				new RelationTypeBuilder(domain, new Guid("62009cef-7424-4ec0-8953-e92b3cd6639d")).WithObjectTypes(Country, Currency).WithSingularName("Currency")  .WithPluralName("Currencies")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
				new RelationTypeBuilder(domain, new Guid("6b9c977f-b394-440e-9781-5d56733b60da")).WithObjectTypes(Country, AllorsString).WithSingularName("Name")  .WithPluralName("Names")    .WithIsIndexed(true)  .WithSize(256).Build();
				new RelationTypeBuilder(domain, new Guid("8236a702-a76d-4bb5-9afd-acacb1508261")).WithObjectTypes(Country, LocalisedText).WithSingularName("LocalisedName")  .WithPluralName("LocalisedNames")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
				new RelationTypeBuilder(domain, new Guid("f93acc4e-f89e-4610-ada9-e58f21c165bc")).WithObjectTypes(Country, AllorsString).WithSingularName("IsoCode")  .WithPluralName("IsoCodes")      .WithSize(2).Build();

				// Person
				new RelationTypeBuilder(domain, new Guid("8a3e4664-bb40-4208-8e90-a1b5be323f27")).WithObjectTypes(Person, AllorsString).WithSingularName("LastName")  .WithPluralName("LastNames")      .WithSize(256).Build();
				new RelationTypeBuilder(domain, new Guid("eb18bb28-da9c-47b4-a091-2f8f2303dcb6")).WithObjectTypes(Person, AllorsString).WithSingularName("MiddleName")  .WithPluralName("MiddleNames")      .WithSize(256).Build();
				new RelationTypeBuilder(domain, new Guid("ed4b710a-fe24-4143-bb96-ed1bd9beae1a")).WithObjectTypes(Person, AllorsString).WithSingularName("FirstName")  .WithPluralName("FirstNames")      .WithSize(256).Build();

				// Image
				new RelationTypeBuilder(domain, new Guid("366410a7-7d51-4d7c-82fd-3444bdc0b3f7")).WithObjectTypes(Image, Media).WithSingularName("Original")  .WithPluralName("Originals")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
				new RelationTypeBuilder(domain, new Guid("59689164-7a45-45d4-98fa-f8cf50c62899")).WithObjectTypes(Image, Media).WithSingularName("Responsive")  .WithPluralName("Responsives")    .WithIsIndexed(true)  .Build();
				new RelationTypeBuilder(domain, new Guid("d149b012-1dc2-4bd1-a650-26b7c6f9024b")).WithObjectTypes(Image, AllorsString).WithSingularName("OriginalFilename")  .WithPluralName("OriginalFilenames")      .WithSize(256).Build();

				// Media
				new RelationTypeBuilder(domain, new Guid("49481792-06f0-49a1-b32f-28d265815a24")).WithObjectTypes(Media, MediaType).WithSingularName("MediaType")  .WithPluralName("MediaTypes")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived("true")  .WithIsIndexed(true)  .Build();
				new RelationTypeBuilder(domain, new Guid("67082a51-1502-490b-b8db-537799e550bd")).WithObjectTypes(Media, MediaContent)    .WithCardinality(Cardinalities.ManyToOne).WithIsDerived("true")  .WithIsIndexed(true)  .Build();

				// Currency
				new RelationTypeBuilder(domain, new Guid("294a4bdc-f03a-47a2-a649-419e6b9021a3")).WithObjectTypes(Currency, AllorsString).WithSingularName("IsoCode")  .WithPluralName("IsoCodes")      .WithSize(256).Build();
				new RelationTypeBuilder(domain, new Guid("74c8308b-1b76-4218-9532-f01c9d1e146b")).WithObjectTypes(Currency, AllorsString).WithSingularName("Name")  .WithPluralName("Names")    .WithIsIndexed(true)  .WithSize(256).Build();
				new RelationTypeBuilder(domain, new Guid("82797074-8d6c-4d61-a885-34ae7133a503")).WithObjectTypes(Currency, AllorsString).WithSingularName("Symbol")  .WithPluralName("Symbols")      .WithSize(256).Build();
				new RelationTypeBuilder(domain, new Guid("e9fc0472-cf7a-4e02-b061-cb42b6f5c273")).WithObjectTypes(Currency, LocalisedText).WithSingularName("LocalisedName")  .WithPluralName("LocalisedNames")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();

				// Commentable
				new RelationTypeBuilder(domain, new Guid("d800f9a2-fadd-45f1-8731-4dac177c6b1b")).WithObjectTypes(Commentable, AllorsString).WithSingularName("Comment")  .WithPluralName("Comments")      .WithSize(-1).Build();

				// Searchable
				new RelationTypeBuilder(domain, new Guid("5f38c771-10db-456e-ac31-6833f7087b50")).WithObjectTypes(Searchable, SearchData).WithSingularName("SearchData")  .WithPluralName("SearchDatas")  .WithIsDerived("true")  .WithIsIndexed(true)  .Build();

				// Counter
				new RelationTypeBuilder(domain, new Guid("309d07d9-8dea-4e99-a3b8-53c0d360bc54")).WithObjectTypes(Counter, AllorsLong).WithSingularName("Value")  .WithPluralName("Values")      .Build();

				// UniquelyIdentifiable
				new RelationTypeBuilder(domain, new Guid("e1842d87-8157-40e7-b06e-4375f311f2c3")).WithObjectTypes(UniquelyIdentifiable, AllorsUnique).WithSingularName("UniqueId")  .WithPluralName("UniqueIds")  .WithIsDerived("true")  .WithIsIndexed(true)  .Build();

				// Singleton
				new RelationTypeBuilder(domain, new Guid("d9ea02e5-9aa1-4cbe-9318-06324529a923")).WithObjectTypes(Singleton, SecurityToken).WithSingularName("AdministratorSecurityToken")  .WithPluralName("AdministratorSecurityTokens")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
				new RelationTypeBuilder(domain, new Guid("f16652b0-b712-43d7-8d4e-34a22487514d")).WithObjectTypes(Singleton, User).WithSingularName("Guest")  .WithPluralName("Guests")      .Build();
				new RelationTypeBuilder(domain, new Guid("f579494b-e550-4be6-9d93-84618ac78704")).WithObjectTypes(Singleton, SecurityToken).WithSingularName("DefaultSecurityToken")  .WithPluralName("DefaultSecurityTokens")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
				new RelationTypeBuilder(domain, new Guid("64aed238-7009-4157-8395-7eb58ebf7889")).WithObjectTypes(Singleton, PrintQueue).WithSingularName("DefaultPrintQueue")  .WithPluralName("DefaultPrintQueues")    .WithIsIndexed(true)  .Build();
				new RelationTypeBuilder(domain, new Guid("9c1634ab-be99-4504-8690-ed4b39fec5bc")).WithObjectTypes(Singleton, Locale).WithSingularName("DefaultLocale")  .WithPluralName("DefaultLocales")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
				new RelationTypeBuilder(domain, new Guid("9e5a3413-ed33-474f-adf2-149ad5a80719")).WithObjectTypes(Singleton, Locale).WithSingularName("Locale")  .WithPluralName("Locales")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();

				// UserGroup
				new RelationTypeBuilder(domain, new Guid("2f8cf270-a153-4e0d-b844-991d577222d4")).WithObjectTypes(UserGroup, Role).WithSingularName("Role")  .WithPluralName("Roles")  .WithIsDerived("true")  .WithIsIndexed(true)  .Build();
				new RelationTypeBuilder(domain, new Guid("585bb5cf-9ba4-4865-9027-3667185abc4f")).WithObjectTypes(UserGroup, User).WithSingularName("Member")  .WithPluralName("Members")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
				new RelationTypeBuilder(domain, new Guid("be9dc116-a7ea-4a4b-aaca-eb0f91fc3741")).WithObjectTypes(UserGroup, UserGroup).WithSingularName("Parent")  .WithPluralName("Parents")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived("true")  .WithIsIndexed(true)  .Build();
				new RelationTypeBuilder(domain, new Guid("e94e7f05-78bd-4291-923f-38f82d00e3f4")).WithObjectTypes(UserGroup, AllorsString).WithSingularName("Name")  .WithPluralName("Names")    .WithIsIndexed(true)  .WithSize(256).Build();

				// Permission
				new RelationTypeBuilder(domain, new Guid("097bb620-f068-440e-8d02-ef9d8be1d0f0")).WithObjectTypes(Permission, AllorsUnique).WithSingularName("OperandTypePointer")  .WithPluralName("OperandTypePointers")    .WithIsIndexed(true)  .Build();
				new RelationTypeBuilder(domain, new Guid("29b80857-e51b-4dec-b859-887ed74b1626")).WithObjectTypes(Permission, AllorsUnique).WithSingularName("ConcreteClassPointer")  .WithPluralName("ConcreteClassePointers")    .WithIsIndexed(true)  .Build();
				new RelationTypeBuilder(domain, new Guid("9d73d437-4918-4f20-b9a7-3ce23a04bd7b")).WithObjectTypes(Permission, AllorsInteger).WithSingularName("OperationEnum")  .WithPluralName("OperationEnums")    .WithIsIndexed(true)  .Build();

				// User
				new RelationTypeBuilder(domain, new Guid("0b3b650b-fcd4-4475-b5c4-e2ee4f39b0be")).WithObjectTypes(User, AllorsBoolean).WithSingularName("UserEmailConfirmed")  .WithPluralName("UserEmailConfirmeds")      .Build();
				new RelationTypeBuilder(domain, new Guid("5e8ab257-1a1c-4448-aacc-71dbaaba525b")).WithObjectTypes(User, AllorsString).WithSingularName("UserName")  .WithPluralName("UserNames")      .WithSize(256).Build();
				new RelationTypeBuilder(domain, new Guid("c1ae3652-5854-4b68-9890-a954067767fc")).WithObjectTypes(User, AllorsString).WithSingularName("UserEmail")  .WithPluralName("UserEmails")      .WithSize(256).Build();
				new RelationTypeBuilder(domain, new Guid("ea0c7596-c0b8-4984-bc25-cb4b4857954e")).WithObjectTypes(User, AllorsString).WithSingularName("UserPasswordHash")  .WithPluralName("UserPasswordHashes")      .WithSize(256).Build();

				// SecurityToken
				// SecurityTokenOwner
				new RelationTypeBuilder(domain, new Guid("5fb15e8b-011c-46f7-83dd-485d4cc4f9f2")).WithObjectTypes(SecurityTokenOwner, SecurityToken).WithSingularName("OwnerSecurityToken")  .WithPluralName("OwnerSecurityTokens")  .WithIsDerived("true")  .WithIsIndexed(true)  .Build();

				// Transition
				new RelationTypeBuilder(domain, new Guid("c6ee1a42-05fa-462b-b04f-811f01c6b646")).WithObjectTypes(Transition, ObjectState).WithSingularName("FromState")  .WithPluralName("FromStates")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
				new RelationTypeBuilder(domain, new Guid("dd19e7f8-83b7-4ff1-b475-02c4296b47e4")).WithObjectTypes(Transition, ObjectState).WithSingularName("ToState")  .WithPluralName("ToStates")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();

				// Transitional
				// Login
				new RelationTypeBuilder(domain, new Guid("18262218-a14f-48c3-87a5-87196d3b5974")).WithObjectTypes(Login, AllorsString).WithSingularName("Key")  .WithPluralName("Keys")    .WithIsIndexed(true)  .WithSize(256).Build();
				new RelationTypeBuilder(domain, new Guid("7a82e721-d0b7-4567-aaef-bd3987ae6d01")).WithObjectTypes(Login, AllorsString).WithSingularName("Provider")  .WithPluralName("Providers")    .WithIsIndexed(true)  .WithSize(256).Build();
				new RelationTypeBuilder(domain, new Guid("c2d950ad-39d3-40f1-8817-11a026e9890b")).WithObjectTypes(Login, User).WithSingularName("User")  .WithPluralName("Users")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();

				// Role
				new RelationTypeBuilder(domain, new Guid("51e56ae1-72dc-443f-a2a3-f5aa3650f8d2")).WithObjectTypes(Role, Permission).WithSingularName("Permission")  .WithPluralName("Permissions")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
				new RelationTypeBuilder(domain, new Guid("934bcbbe-5286-445c-a1bd-e2fcc786c448")).WithObjectTypes(Role, AllorsString).WithSingularName("Name")  .WithPluralName("Names")      .WithSize(256).Build();

				// AccessControl
				new RelationTypeBuilder(domain, new Guid("0dbbff5c-3dca-4257-b2da-442d263dcd86")).WithObjectTypes(AccessControl, UserGroup).WithSingularName("SubjectGroup")  .WithPluralName("SubjectGroups")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
				new RelationTypeBuilder(domain, new Guid("37dd1e27-ba75-404c-9410-c6399d28317c")).WithObjectTypes(AccessControl, User).WithSingularName("Subject")  .WithPluralName("Subjects")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
				new RelationTypeBuilder(domain, new Guid("6503574b-8bab-4da8-a19d-23a9bcffe01e")).WithObjectTypes(AccessControl, SecurityToken).WithSingularName("Object")  .WithPluralName("Objects")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
				new RelationTypeBuilder(domain, new Guid("69a9dae8-678d-4c1c-a464-2e5aa5caf39e")).WithObjectTypes(AccessControl, Role).WithSingularName("Role")  .WithPluralName("Roles")  .WithCardinality(Cardinalities.ManyToOne)    .Build();

				// AccessControlledObject
				new RelationTypeBuilder(domain, new Guid("5c70ca14-4601-4c65-9b0d-cb189f90be27")).WithObjectTypes(AccessControlledObject, Permission).WithSingularName("DeniedPermission")  .WithPluralName("DeniedPermissions")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
				new RelationTypeBuilder(domain, new Guid("b816fccd-08e0-46e0-a49c-7213c3604416")).WithObjectTypes(AccessControlledObject, SecurityToken).WithSingularName("SecurityToken")  .WithPluralName("SecurityTokens")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();

				// UserInterfaceable
				new RelationTypeBuilder(domain, new Guid("6412301d-95ec-44c2-8c71-cc03de5327b9")).WithObjectTypes(UserInterfaceable, AllorsString).WithSingularName("DisplayName")  .WithPluralName("DisplayNames")  .WithIsDerived("true")  .WithIsIndexed(true)  .WithSize(256).Build();

				// ObjectState
				new RelationTypeBuilder(domain, new Guid("59338f0b-40e7-49e8-ba1a-3ecebf96aebe")).WithObjectTypes(ObjectState, Permission).WithSingularName("DeniedPermission")  .WithPluralName("DeniedPermissions")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();



		}
	}
}