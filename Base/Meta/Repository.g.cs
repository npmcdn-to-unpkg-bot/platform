namespace Allors.Meta
{
	using System;
	using Allors.Meta;


	public static partial class Repository
	{

 		public static Domain Core;
 		public static Domain Base;

		public static Unit AllorsString;
        public static Unit AllorsInteger;
        public static Unit AllorsDateTime;
        public static Unit AllorsDecimal;
        public static Unit AllorsFloat;
        public static Unit AllorsBoolean;
        public static Unit AllorsUnique;
        public static Unit AllorsBinary;

		public static Class LocalisedText;
		public static Class Counter;
		public static Class StringTemplate;
		public static Interface UniquelyIdentifiable;
		public static Class Singleton;
		public static Class Locale;
		public static Class SearchFragment;
		public static Class Language;
		public static Class SearchData;
		public static Class UserGroup;
		public static Interface Printable;
		public static Class MediaContent;
		public static Interface Localised;
		public static Class Permission;
		public static Interface Period;
		public static Interface User;
		public static Interface SearchResult;
		public static Class SecurityToken;
		public static Interface SecurityTokenOwner;
		public static Class Transition;
		public static Class MediaType;
		public static Interface Transitional;
		public static Class Login;
		public static Class Role;
		public static Class PrintQueue;
		public static Interface Enumeration;
		public static Class Country;
		public static Class AccessControl;
		public static Class Person;
		public static Class Image;
		public static Interface Derivable;
		public static Class Media;
		public static Interface AccessControlledObject;
		public static Interface UserInterfaceable;
		public static Interface ObjectState;
		public static Class Currency;
		public static Interface Commentable;
		public static Interface Searchable;

		public static RelationType MediaContentValue;
		public static RelationType EnumerationLocalisedName;
		public static RelationType PermissionOperandTypePointer;
		public static RelationType UserUserEmailConfirmed;
		public static RelationType AccessControlSubjectGroup;
		public static RelationType LoginKey;
		public static RelationType MediaTypeDefaultFileExtension;
		public static RelationType CurrencyIsoCode;
		public static RelationType PermissionConcreteClassPointer;
		public static RelationType LocaleName;
		public static RelationType StringTemplateBody;
		public static RelationType UserGroupRole;
		public static RelationType CounterValue;
		public static RelationType ImageOriginal;
		public static RelationType AccessControlSubject;
		public static RelationType EnumerationName;
		public static RelationType SearchDataCharacterBoundaryText;
		public static RelationType MediaMediaType;
		public static RelationType LocalisedTextText;
		public static RelationType RolePermission;
		public static RelationType UserGroupMember;
		public static RelationType ObjectStateDeniedPermission;
		public static RelationType ImageResponsive;
		public static RelationType PeriodFromDate;
		public static RelationType AccessControlledObjectDeniedPermission;
		public static RelationType UserUserName;
		public static RelationType SearchableSearchData;
		public static RelationType SecurityTokenOwnerOwnerSecurityToken;
		public static RelationType MediaTypeName;
		public static RelationType CountryCurrency;
		public static RelationType UserInterfaceableDisplayName;
		public static RelationType SingletonDefaultPrintQueue;
		public static RelationType AccessControlObject;
		public static RelationType MediaMediaContent;
		public static RelationType PrintQueuePrintable;
		public static RelationType AccessControlRole;
		public static RelationType CountryName;
		public static RelationType CurrencyName;
		public static RelationType LoginProvider;
		public static RelationType PrintQueueName;
		public static RelationType SearchDataPreviousCharacterBoundaryText;
		public static RelationType CountryLocalisedName;
		public static RelationType CurrencySymbol;
		public static RelationType MediaContentHash;
		public static RelationType PersonLastName;
		public static RelationType LocalisedLocale;
		public static RelationType RoleName;
		public static RelationType SingletonDefaultLocale;
		public static RelationType PermissionOperationEnum;
		public static RelationType SingletonLocale;
		public static RelationType SearchDataSearchFragment;
		public static RelationType SearchDataPreviousWordBoundaryText;
		public static RelationType AccessControlledObjectSecurityToken;
		public static RelationType ObjectStateName;
		public static RelationType LanguageName;
		public static RelationType UserGroupParent;
		public static RelationType UserUserEmail;
		public static RelationType LoginUser;
		public static RelationType StringTemplateName;
		public static RelationType TransitionFromState;
		public static RelationType PrintablePrintContent;
		public static RelationType ImageOriginalFilename;
		public static RelationType LanguageIsoCode;
		public static RelationType SearchDataWordBoundaryText;
		public static RelationType PeriodThroughDate;
		public static RelationType CommentableComment;
		public static RelationType LocaleLanguage;
		public static RelationType SingletonAdministratorSecurityToken;
		public static RelationType TransitionToState;
		public static RelationType UniquelyIdentifiableUniqueId;
		public static RelationType UserGroupName;
		public static RelationType CurrencyLocalisedName;
		public static RelationType UserUserPasswordHash;
		public static RelationType LocaleCountry;
		public static RelationType PersonMiddleName;
		public static RelationType PersonFirstName;
		public static RelationType LanguageLocalisedName;
		public static RelationType SingletonGuest;
		public static RelationType SingletonDefaultSecurityToken;
		public static RelationType EnumerationIsActive;
		public static RelationType SearchFragmentLowerCaseText;
		public static RelationType CountryIsoCode;


		static void Init(MetaPopulation meta)
		{
            Core = new Domain(meta, new Guid("A5F4E562-323A-41C6-893F-12D7C6A9BD76")) { Name = "Core" };
			Base = new Domain(meta, new Guid("770538dd-7b19-4694-bdce-cf04dcf9cf62")) { Name = "Base" };

			// Units
			AllorsString = new UnitBuilder(Core, UnitIds.StringId).WithSingularName("AllorsString").WithPluralName("AllorsStrings").WithUnitTag(UnitTags.AllorsString).Build();
            AllorsInteger = new UnitBuilder(Core, UnitIds.IntegerId).WithSingularName("AllorsInteger").WithPluralName("AllorsIntegers").WithUnitTag(UnitTags.AllorsInteger).Build();
            AllorsDateTime = new UnitBuilder(Core, UnitIds.DateTimeId).WithSingularName("AllorsDateTime").WithPluralName("AllorsDateTimes").WithUnitTag(UnitTags.AllorsDateTime).Build();
            AllorsDecimal = new UnitBuilder(Core, UnitIds.DecimalId).WithSingularName("AllorsDecimal").WithPluralName("AllorsDecimals").WithUnitTag(UnitTags.AllorsDecimal).Build();
            AllorsFloat = new UnitBuilder(Core, UnitIds.FloatId).WithSingularName("AllorsFloat").WithPluralName("AllorsFloats").WithUnitTag(UnitTags.AllorsFloat).Build();
            AllorsBoolean = new UnitBuilder(Core, UnitIds.BooleanId).WithSingularName("AllorsBoolean").WithPluralName("AllorsBooleans").WithUnitTag(UnitTags.AllorsBoolean).Build();
            AllorsUnique = new UnitBuilder(Core, UnitIds.Unique).WithSingularName("AllorsUnique").WithPluralName("AllorsUniques").WithUnitTag(UnitTags.AllorsUnique).Build();
            AllorsBinary = new UnitBuilder(Core, UnitIds.BinaryId).WithSingularName("AllorsBinary").WithPluralName("AllorsBinaries").WithUnitTag(UnitTags.AllorsBinary).Build();

			// Composites
			LocalisedText = new ClassBuilder(Base, new Guid("020f5d4d-4a59-4d7b-865a-d72fc70e4d97"))
			.WithSingularName("LocalisedText")
			.WithPluralName("LocalisedTexts")
			.Build();
  

			Counter = new ClassBuilder(Base, new Guid("0568354f-e3d9-439e-baac-b7dce31b956a"))
			.WithSingularName("Counter")
			.WithPluralName("Counters")
			.Build();
  

			StringTemplate = new ClassBuilder(Base, new Guid("0c50c02a-cc9c-4617-8530-15a24d4ac969"))
			.WithSingularName("StringTemplate")
			.WithPluralName("StringTemplates")
			.Build();
  
  
			UniquelyIdentifiable = new InterfaceBuilder(Base, new Guid("122ccfe1-f902-44c1-9d6c-6f6a0afa9469"))
			.WithSingularName("UniquelyIdentifiable")
			.WithPluralName("UniquelyIdentifiables")
			.Build();
  

			Singleton = new ClassBuilder(Base, new Guid("313b97a5-328c-4600-9dd2-b5bc146fb13b"))
			.WithSingularName("Singleton")
			.WithPluralName("Singletons")
			.Build();
  

			Locale = new ClassBuilder(Base, new Guid("45033ae6-85b5-4ced-87ce-02518e6c27fd"))
			.WithSingularName("Locale")
			.WithPluralName("Locales")
			.Build();
  

			SearchFragment = new ClassBuilder(Base, new Guid("490d150f-3322-4616-a75c-71e4d94b3e03"))
			.WithSingularName("SearchFragment")
			.WithPluralName("SearchFragments")
			.Build();
  

			Language = new ClassBuilder(Base, new Guid("4a0eca4b-281f-488d-9c7e-497de882c044"))
			.WithSingularName("Language")
			.WithPluralName("Languages")
			.Build();
  

			SearchData = new ClassBuilder(Base, new Guid("56794636-cfad-47df-8567-84c8ee993ade"))
			.WithSingularName("SearchData")
			.WithPluralName("SearchDatas")
			.Build();
  

			UserGroup = new ClassBuilder(Base, new Guid("60065f5d-a3c2-4418-880d-1026ab607319"))
			.WithSingularName("UserGroup")
			.WithPluralName("UserGroups")
			.Build();
  
  
			Printable = new InterfaceBuilder(Base, new Guid("61207a42-3199-4249-baa4-9dd11dc0f5b1"))
			.WithSingularName("Printable")
			.WithPluralName("Printables")
			.Build();
  

			MediaContent = new ClassBuilder(Base, new Guid("6c20422e-cb3e-4402-bb40-dacaf584405e"))
			.WithSingularName("MediaContent")
			.WithPluralName("MediaContents")
			.Build();
  
  
			Localised = new InterfaceBuilder(Base, new Guid("7979a17c-0829-46df-a0d4-1b01775cfaac"))
			.WithSingularName("Localised")
			.WithPluralName("Localiseds")
			.Build();
  

			Permission = new ClassBuilder(Base, new Guid("7fded183-3337-4196-afb0-3266377944bc"))
			.WithSingularName("Permission")
			.WithPluralName("Permissions")
			.Build();
  
  
			Period = new InterfaceBuilder(Base, new Guid("80adbbfd-952e-46f3-a744-78e0ce42bc80"))
			.WithSingularName("Period")
			.WithPluralName("Periods")
			.Build();
  
  
			User = new InterfaceBuilder(Base, new Guid("a0309c3b-6f80-4777-983e-6e69800df5be"))
			.WithSingularName("User")
			.WithPluralName("Users")
			.Build();
  
  
			SearchResult = new InterfaceBuilder(Base, new Guid("a0ac7040-6984-4267-a200-919875e08909"))
			.WithSingularName("SearchResult")
			.WithPluralName("SearchResults")
			.Build();
  

			SecurityToken = new ClassBuilder(Base, new Guid("a53f1aed-0e3f-4c3c-9600-dc579cccf893"))
			.WithSingularName("SecurityToken")
			.WithPluralName("SecurityTokens")
			.Build();
  
  
			SecurityTokenOwner = new InterfaceBuilder(Base, new Guid("a69cad9c-c2f1-463f-9af1-873ce65aeea6"))
			.WithSingularName("SecurityTokenOwner")
			.WithPluralName("SecurityTokenOwners")
			.Build();
  

			Transition = new ClassBuilder(Base, new Guid("a7e490c0-ce29-4298-97c4-519904bb755a"))
			.WithSingularName("Transition")
			.WithPluralName("Transitions")
			.Build();
  

			MediaType = new ClassBuilder(Base, new Guid("aa7d61f8-6618-47a0-9cf2-e75dd81dbd5b"))
			.WithSingularName("MediaType")
			.WithPluralName("MediaTypes")
			.Build();
  
  
			Transitional = new InterfaceBuilder(Base, new Guid("ab2179ad-9eac-4b61-8d84-81cd777c4926"))
			.WithSingularName("Transitional")
			.WithPluralName("Transitionals")
			.Build();
  

			Login = new ClassBuilder(Base, new Guid("ad7277a8-eda4-4128-a990-b47fe43d120a"))
			.WithSingularName("Login")
			.WithPluralName("Logins")
			.Build();
  

			Role = new ClassBuilder(Base, new Guid("af6fe5f4-e5bc-4099-bcd1-97528af6505d"))
			.WithSingularName("Role")
			.WithPluralName("Roles")
			.Build();
  

			PrintQueue = new ClassBuilder(Base, new Guid("b45705e3-0dc6-4296-824a-76bb6af223d3"))
			.WithSingularName("PrintQueue")
			.WithPluralName("PrintQueues")
			.Build();
  
  
			Enumeration = new InterfaceBuilder(Base, new Guid("b7bcc22f-03f0-46fd-b738-4e035921d445"))
			.WithSingularName("Enumeration")
			.WithPluralName("Enumerations")
			.Build();
  

			Country = new ClassBuilder(Base, new Guid("c22bf60e-6428-4d10-8194-94f7be396f28"))
			.WithSingularName("Country")
			.WithPluralName("Countries")
			.Build();
  

			AccessControl = new ClassBuilder(Base, new Guid("c4d93d5e-34c3-4731-9d37-47a8e801d9a8"))
			.WithSingularName("AccessControl")
			.WithPluralName("AccessControls")
			.Build();
  

			Person = new ClassBuilder(Base, new Guid("c799ca62-a554-467d-9aa2-1663293bb37f"))
			.WithSingularName("Person")
			.WithPluralName("Persons")
			.Build();
  

			Image = new ClassBuilder(Base, new Guid("caa2a2de-9454-4812-a69f-9d3728706345"))
			.WithSingularName("Image")
			.WithPluralName("Images")
			.Build();
  
  
			Derivable = new InterfaceBuilder(Base, new Guid("ccde0070-ad05-4336-8cab-a5b27bef5c29"))
			.WithSingularName("Derivable")
			.WithPluralName("Derivables")
			.Build();
  

			Media = new ClassBuilder(Base, new Guid("da5b86a3-4f33-4c0d-965d-f4fbc1179374"))
			.WithSingularName("Media")
			.WithPluralName("Medias")
			.Build();
  
  
			AccessControlledObject = new InterfaceBuilder(Base, new Guid("eb0ff756-3e3d-4cf9-8935-8802a73d2df2"))
			.WithSingularName("AccessControlledObject")
			.WithPluralName("AccessControlledObjects")
			.Build();
  
  
			UserInterfaceable = new InterfaceBuilder(Base, new Guid("eea17b39-8912-40b3-8403-293bd5a3316d"))
			.WithSingularName("UserInterfaceable")
			.WithPluralName("UserInterfaceables")
			.Build();
  
  
			ObjectState = new InterfaceBuilder(Base, new Guid("f991813f-3146-4431-96d0-554aa2186887"))
			.WithSingularName("ObjectState")
			.WithPluralName("ObjectStates")
			.Build();
  

			Currency = new ClassBuilder(Base, new Guid("fd397adf-40b4-4ef8-b449-dd5a24273df3"))
			.WithSingularName("Currency")
			.WithPluralName("Currencies")
			.Build();
  
  
			Commentable = new InterfaceBuilder(Base, new Guid("fdd52472-e863-4e91-bb01-1dada2acc8f6"))
			.WithSingularName("Commentable")
			.WithPluralName("Commentables")
			.Build();
  
  
			Searchable = new InterfaceBuilder(Base, new Guid("ff34f3f1-6a17-404f-a9e5-5cffcdaa3d31"))
			.WithSingularName("Searchable")
			.WithPluralName("Searchables")
			.Build();
  


			// Inheritances
				// LocalisedText
				new InheritanceBuilder(Base, new Guid("20881529-dfd1-428a-8aa3-ecea4fe4c8c4"))
				.WithSubtype(LocalisedText)
				.WithSupertype(Searchable).Build();
				new InheritanceBuilder(Base, new Guid("c2b526fd-920d-470a-8a40-405b7e4d8335"))
				.WithSubtype(LocalisedText)
				.WithSupertype(UserInterfaceable).Build();
				new InheritanceBuilder(Base, new Guid("ebe85e2a-084a-452c-896f-aaf390c5bf1a"))
				.WithSubtype(LocalisedText)
				.WithSupertype(Localised).Build();

				// Counter
				new InheritanceBuilder(Base, new Guid("f2a0c00d-ba20-44bd-94ec-1173370d77c9"))
				.WithSubtype(Counter)
				.WithSupertype(UniquelyIdentifiable).Build();

				// StringTemplate
				new InheritanceBuilder(Base, new Guid("714a9702-01f8-48fc-8add-2f50a8b0bd91"))
				.WithSubtype(StringTemplate)
				.WithSupertype(UniquelyIdentifiable).Build();
				new InheritanceBuilder(Base, new Guid("84fc6495-3e2c-4a99-b0bc-7d818c24eb0b"))
				.WithSubtype(StringTemplate)
				.WithSupertype(Localised).Build();

				// UniquelyIdentifiable
				// Singleton
				new InheritanceBuilder(Base, new Guid("dc655fb2-bb19-4338-a641-e95689c58409"))
				.WithSubtype(Singleton)
				.WithSupertype(UserInterfaceable).Build();

				// Locale
				new InheritanceBuilder(Base, new Guid("317927bf-e978-4239-b257-a443a22e4665"))
				.WithSubtype(Locale)
				.WithSupertype(UserInterfaceable).Build();

				// SearchFragment
				new InheritanceBuilder(Base, new Guid("3ecd13de-c25f-48fc-a1d1-7a78767e825c"))
				.WithSubtype(SearchFragment)
				.WithSupertype(Derivable).Build();

				// Language
				new InheritanceBuilder(Base, new Guid("a4ebd1f9-84db-4888-ba53-414b67b03c73"))
				.WithSubtype(Language)
				.WithSupertype(UserInterfaceable).Build();
				new InheritanceBuilder(Base, new Guid("f26de80c-f10a-4904-978f-aa9848845ec6"))
				.WithSubtype(Language)
				.WithSupertype(Searchable).Build();

				// SearchData
				new InheritanceBuilder(Base, new Guid("04af874d-d58c-4038-b98c-e822bfae2c3b"))
				.WithSubtype(SearchData)
				.WithSupertype(Derivable).Build();

				// UserGroup
				new InheritanceBuilder(Base, new Guid("6147b424-b6a9-44b9-b173-30d259165a51"))
				.WithSubtype(UserGroup)
				.WithSupertype(UniquelyIdentifiable).Build();
				new InheritanceBuilder(Base, new Guid("9968c432-0071-49b1-9923-3690d0b32803"))
				.WithSubtype(UserGroup)
				.WithSupertype(Searchable).Build();
				new InheritanceBuilder(Base, new Guid("ff0d36e7-49d3-4bea-88d0-f40e8ddb714e"))
				.WithSubtype(UserGroup)
				.WithSupertype(UserInterfaceable).Build();

				// Printable
				new InheritanceBuilder(Base, new Guid("82285900-358c-426f-a592-c7ae138287ed"))
				.WithSubtype(Printable)
				.WithSupertype(UserInterfaceable).Build();
				new InheritanceBuilder(Base, new Guid("9bc4b1c9-7e87-4b5b-bcf8-c02b462f0d53"))
				.WithSubtype(Printable)
				.WithSupertype(UniquelyIdentifiable).Build();

				// MediaContent
				new InheritanceBuilder(Base, new Guid("6621c785-2a51-4472-8ecb-258760a62179"))
				.WithSubtype(MediaContent)
				.WithSupertype(Derivable).Build();

				// Localised
				// Permission
				new InheritanceBuilder(Base, new Guid("aec174d4-5633-462c-91a1-10d3e782fdb4"))
				.WithSubtype(Permission)
				.WithSupertype(UserInterfaceable).Build();

				// Period
				// User
				new InheritanceBuilder(Base, new Guid("17c51f3d-869f-4f1e-95e0-011021837b69"))
				.WithSubtype(User)
				.WithSupertype(SecurityTokenOwner).Build();
				new InheritanceBuilder(Base, new Guid("42aae0bd-7080-4c11-8cbd-1634aa046d32"))
				.WithSubtype(User)
				.WithSupertype(UserInterfaceable).Build();
				new InheritanceBuilder(Base, new Guid("765d49c1-f1ef-4af9-b295-08d0b010b7fe"))
				.WithSubtype(User)
				.WithSupertype(Localised).Build();

				// SearchResult
				new InheritanceBuilder(Base, new Guid("859646f9-1012-4b8f-b440-157a9afe071f"))
				.WithSubtype(SearchResult)
				.WithSupertype(UserInterfaceable).Build();

				// SecurityToken
				// SecurityTokenOwner
				// Transition
				// MediaType
				new InheritanceBuilder(Base, new Guid("8d9af4d9-0b71-46de-a0ca-1bbfef4cdd8f"))
				.WithSubtype(MediaType)
				.WithSupertype(UserInterfaceable).Build();

				// Transitional
				new InheritanceBuilder(Base, new Guid("c9e990a7-7853-4b34-860d-672224d36162"))
				.WithSubtype(Transitional)
				.WithSupertype(AccessControlledObject).Build();

				// Login
				new InheritanceBuilder(Base, new Guid("28799476-9ca8-43bf-a06c-d2b33f91e436"))
				.WithSubtype(Login)
				.WithSupertype(Derivable).Build();

				// Role
				new InheritanceBuilder(Base, new Guid("4e737d59-0330-4f4e-a3f3-7ec617d63748"))
				.WithSubtype(Role)
				.WithSupertype(UserInterfaceable).Build();
				new InheritanceBuilder(Base, new Guid("816ab651-b27d-4f4f-83d7-39e7b501b2c0"))
				.WithSubtype(Role)
				.WithSupertype(UniquelyIdentifiable).Build();

				// PrintQueue
				new InheritanceBuilder(Base, new Guid("234d4a69-601d-4cf2-b8ce-169198f1ce11"))
				.WithSubtype(PrintQueue)
				.WithSupertype(AccessControlledObject).Build();
				new InheritanceBuilder(Base, new Guid("4954b9f3-85ec-4f57-8c70-40953c3c9296"))
				.WithSubtype(PrintQueue)
				.WithSupertype(UserInterfaceable).Build();
				new InheritanceBuilder(Base, new Guid("e4518c12-8dd9-4762-93b8-ba3e42b6bf0d"))
				.WithSubtype(PrintQueue)
				.WithSupertype(UniquelyIdentifiable).Build();

				// Enumeration
				new InheritanceBuilder(Base, new Guid("9b0a0816-0ec4-4e6d-9617-ba819f7081a5"))
				.WithSubtype(Enumeration)
				.WithSupertype(UserInterfaceable).Build();
				new InheritanceBuilder(Base, new Guid("b5e48ee4-971a-4487-b09c-d0cb397aa0e9"))
				.WithSubtype(Enumeration)
				.WithSupertype(UniquelyIdentifiable).Build();

				// Country
				new InheritanceBuilder(Base, new Guid("1771a830-0d21-4c5e-8d1c-56db36de55b2"))
				.WithSubtype(Country)
				.WithSupertype(UserInterfaceable).Build();
				new InheritanceBuilder(Base, new Guid("7abaf304-080c-4368-ae1f-5be4a03ed403"))
				.WithSubtype(Country)
				.WithSupertype(Searchable).Build();

				// AccessControl
				new InheritanceBuilder(Base, new Guid("b4c7e051-3605-41e6-a78b-edb1c70bde9d"))
				.WithSubtype(AccessControl)
				.WithSupertype(UserInterfaceable).Build();

				// Person
				new InheritanceBuilder(Base, new Guid("306eb440-10ac-40e3-969d-14e3fdab134a"))
				.WithSubtype(Person)
				.WithSupertype(User).Build();
				new InheritanceBuilder(Base, new Guid("4d6db8b5-4f01-4638-806b-7bdc6698901f"))
				.WithSubtype(Person)
				.WithSupertype(AccessControlledObject).Build();
				new InheritanceBuilder(Base, new Guid("9cff65a7-70df-4614-868e-b007cf8d88b8"))
				.WithSubtype(Person)
				.WithSupertype(UniquelyIdentifiable).Build();
				new InheritanceBuilder(Base, new Guid("b4a45e69-a44e-435c-89eb-4a396c5d641f"))
				.WithSubtype(Person)
				.WithSupertype(SearchResult).Build();
				new InheritanceBuilder(Base, new Guid("c0149a55-baa6-44fa-a5c0-135e5602ac25"))
				.WithSubtype(Person)
				.WithSupertype(UserInterfaceable).Build();
				new InheritanceBuilder(Base, new Guid("e8fd24dd-5ffd-4689-aa0b-fe667977cd65"))
				.WithSubtype(Person)
				.WithSupertype(Searchable).Build();

				// Image
				// Derivable
				// Media
				new InheritanceBuilder(Base, new Guid("c8cd0830-d1a7-4343-8049-dc18c34c213e"))
				.WithSubtype(Media)
				.WithSupertype(UniquelyIdentifiable).Build();
				new InheritanceBuilder(Base, new Guid("dae544a9-9dea-4b84-99c7-2b701868333d"))
				.WithSubtype(Media)
				.WithSupertype(UserInterfaceable).Build();

				// AccessControlledObject
				new InheritanceBuilder(Base, new Guid("b97d7219-f7d9-4df0-80a0-b624c46359aa"))
				.WithSubtype(AccessControlledObject)
				.WithSupertype(Derivable).Build();

				// UserInterfaceable
				new InheritanceBuilder(Base, new Guid("d583715e-56c3-4212-81c7-3fa4de12144b"))
				.WithSubtype(UserInterfaceable)
				.WithSupertype(AccessControlledObject).Build();

				// ObjectState
				new InheritanceBuilder(Base, new Guid("dd9d3cf5-5c9b-444a-a4b1-a4d807597cf6"))
				.WithSubtype(ObjectState)
				.WithSupertype(UniquelyIdentifiable).Build();

				// Currency
				new InheritanceBuilder(Base, new Guid("09b3ac36-f944-4316-9749-99d4f12ffe74"))
				.WithSubtype(Currency)
				.WithSupertype(UserInterfaceable).Build();

				// Commentable
				// Searchable


			// RelationTypes
				// LocalisedText
				LocalisedTextText = new RelationTypeBuilder(Base, new Guid("50dc85f0-3d22-4bc1-95d9-153674b89f7a"),new Guid("accd061b-20b9-4a24-bb2c-c2f7276f43ab"),new Guid("8d3f68e1-fa6e-414f-aa4d-25fcc2c975d6"))
				.WithObjectTypes(LocalisedText, AllorsString)
				.WithSingularName("Text")  
				.WithPluralName("Texts")  
				  
				  
				.WithSize(-1)
				.Build();


				// Counter
				CounterValue = new RelationTypeBuilder(Base, new Guid("309d07d9-8dea-4e99-a3b8-53c0d360bc54"),new Guid("0c807020-5397-4cdb-8380-52899b7af6b7"),new Guid("ab60f6a7-d913-4377-ab47-97f0fb9d8f3b"))
				.WithObjectTypes(Counter, AllorsInteger)
				.WithSingularName("Value")  
				.WithPluralName("Values")  
				  
				  
				.Build();


				// StringTemplate
				StringTemplateBody = new RelationTypeBuilder(Base, new Guid("2f88f9f8-3c22-40d3-885c-2abd43af96cc"),new Guid("9ad9b285-2a91-4bd9-90dd-8f963ef0a465"),new Guid("3fcb83d0-11c5-48ba-ba9c-5126f0b4e9f4"))
				.WithObjectTypes(StringTemplate, AllorsString)
				.WithSingularName("Body")  
				.WithPluralName("Bodies")  
				  
				  
				.WithSize(-1)
				.Build();

				StringTemplateName = new RelationTypeBuilder(Base, new Guid("c501103b-037a-4961-93df-2dbb74b88a76"),new Guid("1bcdddcc-e462-4d59-af2d-7346245cb271"),new Guid("37bd5d22-89f1-47a4-b6bd-8841e194b213"))
				.WithObjectTypes(StringTemplate, AllorsString)
				.WithSingularName("Name")  
				.WithPluralName("Names")  
				  
				  
				.WithSize(256)
				.Build();


				// UniquelyIdentifiable
				UniquelyIdentifiableUniqueId = new RelationTypeBuilder(Base, new Guid("e1842d87-8157-40e7-b06e-4375f311f2c3"),new Guid("fe413e96-cfcf-4e8d-9f23-0fa4f457fdf1"),new Guid("d73fd9a4-13ee-4fa9-8925-d93eca328bf6"))
				.WithObjectTypes(UniquelyIdentifiable, AllorsUnique)
				.WithSingularName("UniqueId")  
				.WithPluralName("UniqueIds")  
				.WithIsDerived(true)  
				.WithIsIndexed(true)  
				.Build();


				// Singleton
				SingletonDefaultPrintQueue = new RelationTypeBuilder(Base, new Guid("64aed238-7009-4157-8395-7eb58ebf7889"),new Guid("2f79ecfe-5fd4-44d1-9c39-457bb3dc6815"),new Guid("d861c8f8-7362-4805-9941-661a99ab11ac"))
				.WithObjectTypes(Singleton, PrintQueue)
				.WithSingularName("DefaultPrintQueue")  
				.WithPluralName("DefaultPrintQueues")  
				  
				.WithIsIndexed(true)  
				.Build();

				SingletonDefaultLocale = new RelationTypeBuilder(Base, new Guid("9c1634ab-be99-4504-8690-ed4b39fec5bc"),new Guid("45a4205d-7c02-40d4-8d97-6d7d59e05def"),new Guid("1e051b37-cf30-43ed-a623-dd2928d6d0a3"))
				.WithObjectTypes(Singleton, Locale)
				.WithSingularName("DefaultLocale")  
				.WithPluralName("DefaultLocales")  
				.WithMultiplicity(Multiplicity.ManyToOne)
				  
				.WithIsIndexed(true)  
				.Build();

				SingletonLocale = new RelationTypeBuilder(Base, new Guid("9e5a3413-ed33-474f-adf2-149ad5a80719"),new Guid("33d5d8b9-3472-48d8-ab1a-83d00d9cb691"),new Guid("e75a8956-4d02-49ba-b0cf-747b7a9f350d"))
				.WithObjectTypes(Singleton, Locale)
				.WithSingularName("Locale")  
				.WithPluralName("Locales")  
				.WithMultiplicity(Multiplicity.OneToMany)
				  
				.WithIsIndexed(true)  
				.Build();

				SingletonAdministratorSecurityToken = new RelationTypeBuilder(Base, new Guid("d9ea02e5-9aa1-4cbe-9318-06324529a923"),new Guid("6247e69d-4789-4ee0-a75b-c2de44a5fcce"),new Guid("c11f31e1-75a7-4b23-9d58-7dfec256b658"))
				.WithObjectTypes(Singleton, SecurityToken)
				.WithSingularName("AdministratorSecurityToken")  
				.WithPluralName("AdministratorSecurityTokens")  
				.WithMultiplicity(Multiplicity.ManyToOne)
				  
				.WithIsIndexed(true)  
				.Build();

				SingletonGuest = new RelationTypeBuilder(Base, new Guid("f16652b0-b712-43d7-8d4e-34a22487514d"),new Guid("c92466b5-55ba-496a-8880-2821f32f8f8e"),new Guid("3a12d798-40c3-40e0-ba9f-9d01b1e39e89"))
				.WithObjectTypes(Singleton, User)
				.WithSingularName("Guest")  
				.WithPluralName("Guests")  
				  
				  
				.Build();

				SingletonDefaultSecurityToken = new RelationTypeBuilder(Base, new Guid("f579494b-e550-4be6-9d93-84618ac78704"),new Guid("33f17e75-99cc-417e-99f3-c29080f08f0a"),new Guid("ca9e3469-583c-4950-ba2c-1bc3a0fc3e96"))
				.WithObjectTypes(Singleton, SecurityToken)
				.WithSingularName("DefaultSecurityToken")  
				.WithPluralName("DefaultSecurityTokens")  
				.WithMultiplicity(Multiplicity.ManyToOne)
				  
				.WithIsIndexed(true)  
				.Build();


				// Locale
				LocaleName = new RelationTypeBuilder(Base, new Guid("2a2c6f77-e6a2-4eab-bfe3-5d35a8abd7f7"),new Guid("09422255-fa17-41d8-991b-d21d7b37c6c5"),new Guid("647db2b3-997b-4c3a-9ae2-d49b410933c1"))
				.WithObjectTypes(Locale, AllorsString)
				.WithSingularName("Name")  
				.WithPluralName("Names")  
				  
				  
				.WithSize(256)
				.Build();

				LocaleLanguage = new RelationTypeBuilder(Base, new Guid("d8cac34a-9bb2-4190-bd2a-ec0b87e04cf5"),new Guid("af501892-3c83-41d1-826b-f5c4cb1de7fe"),new Guid("ed32b12a-00ad-420b-9dfa-f1c6ce773fcd"))
				.WithObjectTypes(Locale, Language)
				.WithSingularName("Language")  
				.WithPluralName("Languages")  
				.WithMultiplicity(Multiplicity.ManyToOne)
				  
				.WithIsIndexed(true)  
				.Build();

				LocaleCountry = new RelationTypeBuilder(Base, new Guid("ea778b77-2929-4ab4-ad99-bf2f970401a9"),new Guid("bb5904f5-feb0-47eb-903a-0351d55f0d8c"),new Guid("b2fc6e06-3881-427e-b4cc-8457a65f8076"))
				.WithObjectTypes(Locale, Country)
				.WithSingularName("Country")  
				.WithPluralName("Countries")  
				.WithMultiplicity(Multiplicity.ManyToOne)
				  
				.WithIsIndexed(true)  
				.Build();


				// SearchFragment
				SearchFragmentLowerCaseText = new RelationTypeBuilder(Base, new Guid("f7da337d-0650-4051-bf2c-c163d7d061bc"),new Guid("225d6642-89ec-4e3a-965c-36412a7f4ce8"),new Guid("fe8190d7-d69b-425a-ade0-b2b1aed71801"))
				.WithObjectTypes(SearchFragment, AllorsString)
				.WithSingularName("LowerCaseText")  
				.WithPluralName("LowerCaseTexts")  
				  
				.WithIsIndexed(true)  
				.WithSize(256)
				.Build();


				// Language
				LanguageName = new RelationTypeBuilder(Base, new Guid("be482902-beb5-4a76-8ad0-c1b1c1c0e5c4"),new Guid("d3369fa9-afb7-4d5a-b476-3e4d43cce0fd"),new Guid("308d73b0-1b65-40a9-88f1-288848849c51"))
				.WithObjectTypes(Language, AllorsString)
				.WithSingularName("Name")  
				.WithPluralName("Names")  
				  
				.WithIsIndexed(true)  
				.WithSize(256)
				.Build();

				LanguageIsoCode = new RelationTypeBuilder(Base, new Guid("d2a32d9f-21cc-4f9d-b0d3-a9b75da66907"),new Guid("6c860e73-d12e-4e35-897e-ed9f8fd8eba0"),new Guid("84f904a6-8dcc-4089-bda6-34325ade6367"))
				.WithObjectTypes(Language, AllorsString)
				.WithSingularName("IsoCode")  
				.WithPluralName("IsoCodes")  
				  
				  
				.WithSize(256)
				.Build();

				LanguageLocalisedName = new RelationTypeBuilder(Base, new Guid("f091b264-e6b1-4a57-bbfb-8225cbe8190c"),new Guid("6650af3b-f537-4c2f-afff-6773552315cd"),new Guid("5e9fcced-727d-42a2-95e6-a0f9d8be4ec7"))
				.WithObjectTypes(Language, LocalisedText)
				.WithSingularName("LocalisedName")  
				.WithPluralName("LocalisedNames")  
				.WithMultiplicity(Multiplicity.OneToMany)
				  
				.WithIsIndexed(true)  
				.Build();


				// SearchData
				SearchDataCharacterBoundaryText = new RelationTypeBuilder(Base, new Guid("46bbb42c-0969-4b5a-92e5-0c8bae2fd16a"),new Guid("9c1b3242-aadf-4f6f-a24e-73a21a55b8a5"),new Guid("395ffaca-97cd-4af3-a36e-3935bd775a8a"))
				.WithObjectTypes(SearchData, AllorsString)
				.WithSingularName("CharacterBoundaryText")  
				.WithPluralName("CharacterBoundaryTexts")  
				  
				  
				.WithSize(-1)
				.Build();

				SearchDataPreviousCharacterBoundaryText = new RelationTypeBuilder(Base, new Guid("7c38c20c-0555-4752-be27-372694a1f81a"),new Guid("c966f138-c511-4f65-90de-e43bbdfa0e46"),new Guid("dccc3db5-ccae-4a4d-9d61-e30bb4292c88"))
				.WithObjectTypes(SearchData, AllorsString)
				.WithSingularName("PreviousCharacterBoundaryText")  
				.WithPluralName("PreviousCharacterBoundaryTexts")  
				  
				  
				.WithSize(-1)
				.Build();

				SearchDataSearchFragment = new RelationTypeBuilder(Base, new Guid("a0c28d27-7be1-488d-85bf-2f84d4c61a55"),new Guid("5c229928-80b4-47f6-a182-1b2c34ed5d7b"),new Guid("594d3231-5d79-4c97-908f-558d0fa346c3"))
				.WithObjectTypes(SearchData, SearchFragment)
				.WithSingularName("SearchFragment")  
				.WithPluralName("SearchFragments")  
				.WithMultiplicity(Multiplicity.ManyToMany)
				  
				.WithIsIndexed(true)  
				.Build();

				SearchDataPreviousWordBoundaryText = new RelationTypeBuilder(Base, new Guid("a51d3109-7a75-4a8d-9ace-ad9181e64f7d"),new Guid("de49d50b-dad1-4751-9821-ba02cb4b1a58"),new Guid("1ac5bb87-9352-4f37-a0f0-244b0a352e9c"))
				.WithObjectTypes(SearchData, AllorsString)
				.WithSingularName("PreviousWordBoundaryText")  
				.WithPluralName("PreviousWordBoundaryTexts")  
				  
				  
				.WithSize(-1)
				.Build();

				SearchDataWordBoundaryText = new RelationTypeBuilder(Base, new Guid("d72f473f-8aaf-4abd-be01-f0a683c72721"),new Guid("c1876168-3e12-48e8-8ca6-6c511d8b6a8c"),new Guid("e4911a7d-140c-4e31-8e99-69668bf2cc18"))
				.WithObjectTypes(SearchData, AllorsString)
				.WithSingularName("WordBoundaryText")  
				.WithPluralName("WordBoundaryTexts")  
				  
				  
				.WithSize(-1)
				.Build();


				// UserGroup
				UserGroupRole = new RelationTypeBuilder(Base, new Guid("2f8cf270-a153-4e0d-b844-991d577222d4"),new Guid("46f531f2-b211-4f2a-902d-7198cda9c50d"),new Guid("a1b92c88-79d9-4a4f-bb99-0fde4e251a28"))
				.WithObjectTypes(UserGroup, Role)
				  
				  
				.WithIsDerived(true)  
				.WithIsIndexed(true)  
				.Build();

				UserGroupMember = new RelationTypeBuilder(Base, new Guid("585bb5cf-9ba4-4865-9027-3667185abc4f"),new Guid("1e2d1e31-ed80-4435-8850-7663d9c5f41d"),new Guid("c552f0b7-95ce-4d45-aaea-56bc8365eee4"))
				.WithObjectTypes(UserGroup, User)
				.WithSingularName("Member")  
				.WithPluralName("Members")  
				.WithMultiplicity(Multiplicity.ManyToMany)
				  
				.WithIsIndexed(true)  
				.Build();

				UserGroupParent = new RelationTypeBuilder(Base, new Guid("be9dc116-a7ea-4a4b-aaca-eb0f91fc3741"),new Guid("d8d8fdf7-f261-449b-b611-7c58dc43f6d3"),new Guid("6ec327af-86bc-4c79-8f00-bcb399686bf3"))
				.WithObjectTypes(UserGroup, UserGroup)
				.WithSingularName("Parent")  
				.WithPluralName("Parents")  
				.WithMultiplicity(Multiplicity.ManyToOne)
				.WithIsDerived(true)  
				.WithIsIndexed(true)  
				.Build();

				UserGroupName = new RelationTypeBuilder(Base, new Guid("e94e7f05-78bd-4291-923f-38f82d00e3f4"),new Guid("75859e2c-c1a3-4f4c-bb37-4064d0aa81d0"),new Guid("9d3c1eec-bf10-4a79-a37f-bc6a20ff2a79"))
				.WithObjectTypes(UserGroup, AllorsString)
				.WithSingularName("Name")  
				.WithPluralName("Names")  
				  
				.WithIsIndexed(true)  
				.WithSize(256)
				.Build();


				// Printable
				PrintablePrintContent = new RelationTypeBuilder(Base, new Guid("c75d4e4c-d47c-4757-bcb0-25b6daedec9e"),new Guid("480b7df7-b463-4038-a48d-35b8a8af899e"),new Guid("8d530dcd-2c3b-4d1d-8acc-9963338968ed"))
				.WithObjectTypes(Printable, AllorsString)
				.WithSingularName("PrintContent")  
				.WithPluralName("PrintContents")  
				.WithIsDerived(true)  
				  
				.WithSize(-1)
				.Build();


				// MediaContent
				MediaContentValue = new RelationTypeBuilder(Base, new Guid("0756d508-44b7-405e-bf92-bc09e5702e63"),new Guid("76e6547b-8dcf-4e69-ae2d-c8f8c33989e9"),new Guid("85170945-b020-485b-bb6f-c4122992ebfd"))
				.WithObjectTypes(MediaContent, AllorsBinary)
				.WithSingularName("Value")  
				.WithPluralName("Values")  
				.WithIsDerived(true)  
				  
				.WithSize(-1)
				.Build();

				MediaContentHash = new RelationTypeBuilder(Base, new Guid("890598a9-0be4-49ee-8dd8-3581ee9355e6"),new Guid("3cf7f10e-dc56-4a50-95a5-fe7fae0be291"),new Guid("70823e7d-5829-4db7-99e0-f6c5f2b0e87b"))
				.WithObjectTypes(MediaContent, AllorsString)
				.WithSingularName("Hash")  
				.WithPluralName("Hashes")  
				.WithIsDerived(true)  
				.WithIsIndexed(true)  
				.WithSize(1024)
				.Build();


				// Localised
				LocalisedLocale = new RelationTypeBuilder(Base, new Guid("8c005a4e-5ffe-45fd-b279-778e274f4d83"),new Guid("6684d98b-cd43-4612-bf9d-afefe02a0d43"),new Guid("d43b92ac-9e6f-4238-9625-1e889be054cf"))
				.WithObjectTypes(Localised, Locale)
				.WithSingularName("Locale")  
				.WithPluralName("Locales")  
				.WithMultiplicity(Multiplicity.ManyToOne)
				  
				.WithIsIndexed(true)  
				.Build();


				// Permission
				PermissionOperandTypePointer = new RelationTypeBuilder(Base, new Guid("097bb620-f068-440e-8d02-ef9d8be1d0f0"),new Guid("3442728c-164a-477c-87be-19a789229585"),new Guid("3fd81194-2f6f-43e7-9c6b-91f5e3e118ac"))
				.WithObjectTypes(Permission, AllorsUnique)
				.WithSingularName("OperandTypePointer")  
				.WithPluralName("OperandTypePointers")  
				  
				.WithIsIndexed(true)  
				.Build();

				PermissionConcreteClassPointer = new RelationTypeBuilder(Base, new Guid("29b80857-e51b-4dec-b859-887ed74b1626"),new Guid("8ffed1eb-b64e-4341-bbb6-348ed7f06e83"),new Guid("cadaca05-55ba-4a13-8758-786ff29c8e46"))
				.WithObjectTypes(Permission, AllorsUnique)
				.WithSingularName("ConcreteClassPointer")  
				.WithPluralName("ConcreteClassePointers")  
				  
				.WithIsIndexed(true)  
				.Build();

				PermissionOperationEnum = new RelationTypeBuilder(Base, new Guid("9d73d437-4918-4f20-b9a7-3ce23a04bd7b"),new Guid("891734d6-4855-4b33-8b3b-f46fd6103149"),new Guid("d29ce0ed-fba8-409d-8675-dc95e1566cfb"))
				.WithObjectTypes(Permission, AllorsInteger)
				.WithSingularName("OperationEnum")  
				.WithPluralName("OperationEnums")  
				  
				.WithIsIndexed(true)  
				.Build();


				// Period
				PeriodFromDate = new RelationTypeBuilder(Base, new Guid("5aeb31c7-03d4-4314-bbb2-fca5704b1eab"),new Guid("8cf0bd14-753d-4f34-99b3-7a6b0d90c3d4"),new Guid("0da8ef4e-53b7-4152-b219-7e0cebbca268"))
				.WithObjectTypes(Period, AllorsDateTime)
				.WithSingularName("FromDate")  
				.WithPluralName("FromDates")  
				  
				  
				.Build();

				PeriodThroughDate = new RelationTypeBuilder(Base, new Guid("d7576ce2-da27-487a-86aa-b0912f745bc0"),new Guid("cb2fa6c1-f826-45f0-a03f-00e6cb268ebb"),new Guid("4e021875-5bae-4f01-8deb-641016cd2f8d"))
				.WithObjectTypes(Period, AllorsDateTime)
				.WithSingularName("ThroughDate")  
				.WithPluralName("ThroughDates")  
				  
				  
				.Build();


				// User
				UserUserEmailConfirmed = new RelationTypeBuilder(Base, new Guid("0b3b650b-fcd4-4475-b5c4-e2ee4f39b0be"),new Guid("c89a8e3f-6f76-41ac-b4dc-839f9080d917"),new Guid("1b1409b8-add7-494c-a895-002fc969ac7b"))
				.WithObjectTypes(User, AllorsBoolean)
				.WithSingularName("UserEmailConfirmed")  
				.WithPluralName("UserEmailConfirmeds")  
				  
				  
				.Build();

				UserUserName = new RelationTypeBuilder(Base, new Guid("5e8ab257-1a1c-4448-aacc-71dbaaba525b"),new Guid("eca7ef36-8928-4116-bfce-1896a685fe8c"),new Guid("3b7d40a0-18ea-4018-b797-6417723e1890"))
				.WithObjectTypes(User, AllorsString)
				.WithSingularName("UserName")  
				.WithPluralName("UserNames")  
				  
				  
				.WithSize(256)
				.Build();

				UserUserEmail = new RelationTypeBuilder(Base, new Guid("c1ae3652-5854-4b68-9890-a954067767fc"),new Guid("111104a2-1181-4958-92f6-6528cef79af7"),new Guid("58e35754-91a9-4956-aa66-ca48d05c7042"))
				.WithObjectTypes(User, AllorsString)
				.WithSingularName("UserEmail")  
				.WithPluralName("UserEmails")  
				  
				  
				.WithSize(256)
				.Build();

				UserUserPasswordHash = new RelationTypeBuilder(Base, new Guid("ea0c7596-c0b8-4984-bc25-cb4b4857954e"),new Guid("8537ddb5-8ce2-4f35-a16f-207f2519ba9c"),new Guid("75ee3ec2-02bb-4666-a6f0-bac84c844dfa"))
				.WithObjectTypes(User, AllorsString)
				.WithSingularName("UserPasswordHash")  
				.WithPluralName("UserPasswordHashes")  
				  
				  
				.WithSize(256)
				.Build();


				// SearchResult
				// SecurityToken
				// SecurityTokenOwner
				SecurityTokenOwnerOwnerSecurityToken = new RelationTypeBuilder(Base, new Guid("5fb15e8b-011c-46f7-83dd-485d4cc4f9f2"),new Guid("cdc21c1c-918e-4622-a01f-a3de06a8c802"),new Guid("2acda9b3-89e8-475f-9d70-b9cde334409c"))
				.WithObjectTypes(SecurityTokenOwner, SecurityToken)
				.WithSingularName("OwnerSecurityToken")  
				.WithPluralName("OwnerSecurityTokens")  
				.WithIsDerived(true)  
				.WithIsIndexed(true)  
				.Build();


				// Transition
				TransitionFromState = new RelationTypeBuilder(Base, new Guid("c6ee1a42-05fa-462b-b04f-811f01c6b646"),new Guid("ae7fa215-20bb-4472-9d25-ee3174f40fdb"),new Guid("e79fa7b8-870a-4a6e-8522-bb39437e0650"))
				.WithObjectTypes(Transition, ObjectState)
				.WithSingularName("FromState")  
				.WithPluralName("FromStates")  
				.WithMultiplicity(Multiplicity.ManyToMany)
				  
				.WithIsIndexed(true)  
				.Build();

				TransitionToState = new RelationTypeBuilder(Base, new Guid("dd19e7f8-83b7-4ff1-b475-02c4296b47e4"),new Guid("c88c9ab2-af38-45ca-9caa-fcb5715da129"),new Guid("c68eb959-1b2c-48a7-b15a-944a576944ef"))
				.WithObjectTypes(Transition, ObjectState)
				.WithSingularName("ToState")  
				.WithPluralName("ToStates")  
				.WithMultiplicity(Multiplicity.ManyToOne)
				  
				.WithIsIndexed(true)  
				.Build();


				// MediaType
				MediaTypeDefaultFileExtension = new RelationTypeBuilder(Base, new Guid("19e52bd9-26cb-4e74-9c28-9f01e684f3da"),new Guid("b1928c18-ef98-4cee-b03c-660221046486"),new Guid("7223c1e2-d722-440b-8345-ab4cfe56d0e9"))
				.WithObjectTypes(MediaType, AllorsString)
				.WithSingularName("DefaultFileExtension")  
				.WithPluralName("DefaultFileExtensions")  
				.WithIsDerived(true)  
				  
				.WithSize(256)
				.Build();

				MediaTypeName = new RelationTypeBuilder(Base, new Guid("5fcee025-29fd-42d8-ad5a-75cb88d8aef0"),new Guid("0353bfc3-552c-43c7-bfe2-666d2a8199dc"),new Guid("437caa53-1838-4cc4-a403-d65cf3b64358"))
				.WithObjectTypes(MediaType, AllorsString)
				.WithSingularName("Name")  
				.WithPluralName("Names")  
				  
				  
				.WithSize(256)
				.Build();


				// Transitional
				// Login
				LoginKey = new RelationTypeBuilder(Base, new Guid("18262218-a14f-48c3-87a5-87196d3b5974"),new Guid("3f067cf5-4fcb-4be4-9afb-15ba37700658"),new Guid("e5393717-f46c-4a4c-a87f-3e4684428860"))
				.WithObjectTypes(Login, AllorsString)
				.WithSingularName("Key")  
				.WithPluralName("Keys")  
				  
				.WithIsIndexed(true)  
				.WithSize(256)
				.Build();

				LoginProvider = new RelationTypeBuilder(Base, new Guid("7a82e721-d0b7-4567-aaef-bd3987ae6d01"),new Guid("2f2ef41d-8310-45fd-8ab5-e5d067862e3d"),new Guid("c8e3851a-bc57-4acb-934a-4adfc37b1da7"))
				.WithObjectTypes(Login, AllorsString)
				.WithSingularName("Provider")  
				.WithPluralName("Providers")  
				  
				.WithIsIndexed(true)  
				.WithSize(256)
				.Build();

				LoginUser = new RelationTypeBuilder(Base, new Guid("c2d950ad-39d3-40f1-8817-11a026e9890b"),new Guid("e8091111-9f92-41a9-b4b1-4e8f277ea575"),new Guid("150daf84-13ce-4b5f-83e6-64c7ef4f81c6"))
				.WithObjectTypes(Login, User)
				  
				  
				.WithMultiplicity(Multiplicity.ManyToOne)
				  
				.WithIsIndexed(true)  
				.Build();


				// Role
				RolePermission = new RelationTypeBuilder(Base, new Guid("51e56ae1-72dc-443f-a2a3-f5aa3650f8d2"),new Guid("47af1a0f-497d-4a19-887b-79e5fb77c8bd"),new Guid("7e6a71b0-2194-47f8-b562-cb4a15e335b6"))
				.WithObjectTypes(Role, Permission)
				  
				  
				.WithMultiplicity(Multiplicity.ManyToMany)
				  
				.WithIsIndexed(true)  
				.Build();

				RoleName = new RelationTypeBuilder(Base, new Guid("934bcbbe-5286-445c-a1bd-e2fcc786c448"),new Guid("05785884-ca83-43de-a6f3-86d3fa7ec82a"),new Guid("8d87c74f-53ed-4e1d-a2ea-12190ac233d2"))
				.WithObjectTypes(Role, AllorsString)
				.WithSingularName("Name")  
				.WithPluralName("Names")  
				  
				  
				.WithSize(256)
				.Build();


				// PrintQueue
				PrintQueuePrintable = new RelationTypeBuilder(Base, new Guid("679156a1-f683-4772-b724-54b318eb3cb3"),new Guid("9124aa32-3ed5-4a1a-8988-961eea280b86"),new Guid("432f8b01-0bb8-4bd2-8a41-107b6d043a40"))
				.WithObjectTypes(PrintQueue, Printable)
				.WithSingularName("Printable")  
				.WithPluralName("Printables")  
				.WithMultiplicity(Multiplicity.ManyToMany)
				  
				.WithIsIndexed(true)  
				.Build();

				PrintQueueName = new RelationTypeBuilder(Base, new Guid("7a85e090-55cf-47f5-912e-4bd87c66a060"),new Guid("01fa325c-4b41-4cbf-9ffe-65d25e0ae694"),new Guid("285adf08-7f1b-4dfe-8db5-cbf4a9d0cb59"))
				.WithObjectTypes(PrintQueue, AllorsString)
				.WithSingularName("Name")  
				.WithPluralName("Names")  
				  
				.WithIsIndexed(true)  
				.WithSize(256)
				.Build();


				// Enumeration
				EnumerationLocalisedName = new RelationTypeBuilder(Base, new Guid("07e034f1-246a-4115-9662-4c798f31343f"),new Guid("bcf428fd-0263-488c-b9ac-963ceca1c972"),new Guid("919fdad7-830e-4b12-b23c-f433951236af"))
				.WithObjectTypes(Enumeration, LocalisedText)
				.WithSingularName("LocalisedName")  
				.WithPluralName("LocalisedNames")  
				.WithMultiplicity(Multiplicity.OneToMany)
				  
				.WithIsIndexed(true)  
				.Build();

				EnumerationName = new RelationTypeBuilder(Base, new Guid("3d3ae4d0-bac6-4645-8a53-3e9f7f9af086"),new Guid("004cc333-b8ae-4952-ae13-f2ab80eb018c"),new Guid("5850860d-c772-402f-815b-7634c9a1e697"))
				.WithObjectTypes(Enumeration, AllorsString)
				.WithSingularName("Name")  
				.WithPluralName("Names")  
				  
				.WithIsIndexed(true)  
				.WithSize(256)
				.Build();

				EnumerationIsActive = new RelationTypeBuilder(Base, new Guid("f57bb62e-77a8-4519-81e6-539d54b71cb7"),new Guid("a8993304-52c0-4b53-9982-6caa5675467a"),new Guid("0c6faf5a-eac9-454c-bd53-3b8409e56d34"))
				.WithObjectTypes(Enumeration, AllorsBoolean)
				.WithSingularName("IsActive")  
				.WithPluralName("AreActive")  
				  
				.WithIsIndexed(true)  
				.Build();


				// Country
				CountryCurrency = new RelationTypeBuilder(Base, new Guid("62009cef-7424-4ec0-8953-e92b3cd6639d"),new Guid("323173ee-385c-4f74-8b78-ff05735460f8"),new Guid("4ca5a640-5d9e-4910-95ed-6872c7ea13d2"))
				.WithObjectTypes(Country, Currency)
				.WithSingularName("Currency")  
				.WithPluralName("Currencies")  
				.WithMultiplicity(Multiplicity.ManyToOne)
				  
				.WithIsIndexed(true)  
				.Build();

				CountryName = new RelationTypeBuilder(Base, new Guid("6b9c977f-b394-440e-9781-5d56733b60da"),new Guid("6e3532ae-3528-4114-9274-54fc08effd0d"),new Guid("60f1f9a3-13d1-485f-bc77-fda1f9ef1815"))
				.WithObjectTypes(Country, AllorsString)
				.WithSingularName("Name")  
				.WithPluralName("Names")  
				  
				.WithIsIndexed(true)  
				.WithSize(256)
				.Build();

				CountryLocalisedName = new RelationTypeBuilder(Base, new Guid("8236a702-a76d-4bb5-9afd-acacb1508261"),new Guid("9b682612-50f9-43f3-abde-4d0cb5156f0d"),new Guid("99c52c13-ef50-4f68-a32f-fef660aa3044"))
				.WithObjectTypes(Country, LocalisedText)
				.WithSingularName("LocalisedName")  
				.WithPluralName("LocalisedNames")  
				.WithMultiplicity(Multiplicity.OneToMany)
				  
				.WithIsIndexed(true)  
				.Build();

				CountryIsoCode = new RelationTypeBuilder(Base, new Guid("f93acc4e-f89e-4610-ada9-e58f21c165bc"),new Guid("ea0efe67-89f2-4317-97e7-f0e14358e718"),new Guid("4fe997d6-d221-432b-9f09-4f77735c109b"))
				.WithObjectTypes(Country, AllorsString)
				.WithSingularName("IsoCode")  
				.WithPluralName("IsoCodes")  
				  
				  
				.WithSize(2)
				.Build();


				// AccessControl
				AccessControlSubjectGroup = new RelationTypeBuilder(Base, new Guid("0dbbff5c-3dca-4257-b2da-442d263dcd86"),new Guid("92e8d639-9205-422b-b4ff-c7e8c2980abf"),new Guid("2d9b7157-5409-40d3-bd3e-6911df9aface"))
				.WithObjectTypes(AccessControl, UserGroup)
				.WithSingularName("SubjectGroup")  
				.WithPluralName("SubjectGroups")  
				.WithMultiplicity(Multiplicity.ManyToMany)
				  
				.WithIsIndexed(true)  
				.Build();

				AccessControlSubject = new RelationTypeBuilder(Base, new Guid("37dd1e27-ba75-404c-9410-c6399d28317c"),new Guid("3d74101d-97bc-46fb-9548-96cb7aa01b39"),new Guid("e0303a17-bf7a-4a7f-bb4b-0a447c56aaaf"))
				.WithObjectTypes(AccessControl, User)
				.WithSingularName("Subject")  
				.WithPluralName("Subjects")  
				.WithMultiplicity(Multiplicity.ManyToMany)
				  
				.WithIsIndexed(true)  
				.Build();

				AccessControlObject = new RelationTypeBuilder(Base, new Guid("6503574b-8bab-4da8-a19d-23a9bcffe01e"),new Guid("cae9e5c2-afa1-46f4-b930-69d4e810038f"),new Guid("ab2b4b9c-87dd-4712-b123-f5f9271c6193"))
				.WithObjectTypes(AccessControl, SecurityToken)
				.WithSingularName("Object")  
				.WithPluralName("Objects")  
				.WithMultiplicity(Multiplicity.ManyToMany)
				  
				.WithIsIndexed(true)  
				.Build();

				AccessControlRole = new RelationTypeBuilder(Base, new Guid("69a9dae8-678d-4c1c-a464-2e5aa5caf39e"),new Guid("ec79e57d-be81-430a-b12f-08ffd1e94af3"),new Guid("370d3d12-3164-4753-8a72-1c604bda1b64"))
				.WithObjectTypes(AccessControl, Role)
				  
				  
				.WithMultiplicity(Multiplicity.ManyToOne)
				  
				  
				.Build();


				// Person
				PersonLastName = new RelationTypeBuilder(Base, new Guid("8a3e4664-bb40-4208-8e90-a1b5be323f27"),new Guid("9b48ff56-afef-4501-ac97-6173731ff2c9"),new Guid("ace04ad8-bf64-4fc3-8216-14a720d3105d"))
				.WithObjectTypes(Person, AllorsString)
				.WithSingularName("LastName")  
				.WithPluralName("LastNames")  
				  
				  
				.WithSize(256)
				.Build();

				PersonMiddleName = new RelationTypeBuilder(Base, new Guid("eb18bb28-da9c-47b4-a091-2f8f2303dcb6"),new Guid("e3a4d7b2-c5f1-4101-9aab-a0135d37a5a5"),new Guid("a86fc7a6-dedd-4da9-a250-75c9ec730d22"))
				.WithObjectTypes(Person, AllorsString)
				.WithSingularName("MiddleName")  
				.WithPluralName("MiddleNames")  
				  
				  
				.WithSize(256)
				.Build();

				PersonFirstName = new RelationTypeBuilder(Base, new Guid("ed4b710a-fe24-4143-bb96-ed1bd9beae1a"),new Guid("1ea9eca4-eed0-4f61-8903-c99feae961ad"),new Guid("f10ea049-6d24-4ca2-8efa-032fcf3000b3"))
				.WithObjectTypes(Person, AllorsString)
				.WithSingularName("FirstName")  
				.WithPluralName("FirstNames")  
				  
				  
				.WithSize(256)
				.Build();


				// Image
				ImageOriginal = new RelationTypeBuilder(Base, new Guid("366410a7-7d51-4d7c-82fd-3444bdc0b3f7"),new Guid("9d45e17e-962b-4f9b-a029-c1c1562e5260"),new Guid("9ed94fa8-e01e-4f63-9932-d56134616474"))
				.WithObjectTypes(Image, Media)
				.WithSingularName("Original")  
				.WithPluralName("Originals")  
				.WithMultiplicity(Multiplicity.ManyToOne)
				  
				.WithIsIndexed(true)  
				.Build();

				ImageResponsive = new RelationTypeBuilder(Base, new Guid("59689164-7a45-45d4-98fa-f8cf50c62899"),new Guid("386c7cfc-4bec-4564-a7c4-b2c1bccf6ebe"),new Guid("ce4c0fbb-5bdb-4c7f-a70a-b930c1020624"))
				.WithObjectTypes(Image, Media)
				.WithSingularName("Responsive")  
				.WithPluralName("Responsives")  
				  
				.WithIsIndexed(true)  
				.Build();

				ImageOriginalFilename = new RelationTypeBuilder(Base, new Guid("d149b012-1dc2-4bd1-a650-26b7c6f9024b"),new Guid("75fccc6e-1c89-4e0f-88c2-527eb3b0d71d"),new Guid("2f1c8149-f94a-448b-a832-4994f635c48f"))
				.WithObjectTypes(Image, AllorsString)
				.WithSingularName("OriginalFilename")  
				.WithPluralName("OriginalFilenames")  
				  
				  
				.WithSize(256)
				.Build();


				// Derivable
				// Media
				MediaMediaType = new RelationTypeBuilder(Base, new Guid("49481792-06f0-49a1-b32f-28d265815a24"),new Guid("7ca17a9e-0b68-445f-8080-84b08ca0eb2d"),new Guid("f1008c56-b375-4aa8-ac7e-c1f7ef9b2080"))
				.WithObjectTypes(Media, MediaType)
				.WithSingularName("MediaType")  
				.WithPluralName("MediaTypes")  
				.WithMultiplicity(Multiplicity.ManyToOne)
				.WithIsDerived(true)  
				.WithIsIndexed(true)  
				.Build();

				MediaMediaContent = new RelationTypeBuilder(Base, new Guid("67082a51-1502-490b-b8db-537799e550bd"),new Guid("e8537dcf-1bd7-46c4-a37c-077bee6a78a1"),new Guid("02fe1ce8-c019-4a40-bd6f-b38d2f47a288"))
				.WithObjectTypes(Media, MediaContent)
				  
				  
				.WithMultiplicity(Multiplicity.ManyToOne)
				.WithIsDerived(true)  
				.WithIsIndexed(true)  
				.Build();


				// AccessControlledObject
				AccessControlledObjectDeniedPermission = new RelationTypeBuilder(Base, new Guid("5c70ca14-4601-4c65-9b0d-cb189f90be27"),new Guid("267053f0-43b4-4cc7-a0e2-103992b2d0c5"),new Guid("867765fa-49dc-462f-b430-3c0e264c5283"))
				.WithObjectTypes(AccessControlledObject, Permission)
				.WithSingularName("DeniedPermission")  
				.WithPluralName("DeniedPermissions")  
				.WithMultiplicity(Multiplicity.ManyToMany)
				  
				.WithIsIndexed(true)  
				.Build();

				AccessControlledObjectSecurityToken = new RelationTypeBuilder(Base, new Guid("b816fccd-08e0-46e0-a49c-7213c3604416"),new Guid("1739db0d-fe6b-42e1-a6a5-286536ff4f56"),new Guid("9f722315-385a-42ab-b84e-83063b0e5b0d"))
				.WithObjectTypes(AccessControlledObject, SecurityToken)
				  
				  
				.WithMultiplicity(Multiplicity.ManyToMany)
				  
				.WithIsIndexed(true)  
				.Build();


				// UserInterfaceable
				UserInterfaceableDisplayName = new RelationTypeBuilder(Base, new Guid("6412301d-95ec-44c2-8c71-cc03de5327b9"),new Guid("26ed3463-5701-4e56-aa82-635314463a0f"),new Guid("b129523b-a86f-4e68-b0a5-a826b8e8dfac"))
				.WithObjectTypes(UserInterfaceable, AllorsString)
				.WithSingularName("DisplayName")  
				.WithPluralName("DisplayNames")  
				.WithIsDerived(true)  
				.WithIsIndexed(true)  
				.WithSize(256)
				.Build();


				// ObjectState
				ObjectStateDeniedPermission = new RelationTypeBuilder(Base, new Guid("59338f0b-40e7-49e8-ba1a-3ecebf96aebe"),new Guid("fca0f3f6-bdd6-4405-93b3-35dd769bff0e"),new Guid("c338f087-559c-4239-9c6a-1f691e58ed16"))
				.WithObjectTypes(ObjectState, Permission)
				.WithSingularName("DeniedPermission")  
				.WithPluralName("DeniedPermissions")  
				.WithMultiplicity(Multiplicity.ManyToMany)
				  
				.WithIsIndexed(true)  
				.Build();

				ObjectStateName = new RelationTypeBuilder(Base, new Guid("b86f9e42-fe10-4302-ab7c-6c6c7d357c39"),new Guid("052ec640-3150-458a-99d5-0edce6eb6149"),new Guid("945cbba6-4b09-4b87-931e-861b147c3823"))
				.WithObjectTypes(ObjectState, AllorsString)
				.WithSingularName("Name")  
				.WithPluralName("Names")  
				  
				.WithIsIndexed(true)  
				.WithSize(256)
				.Build();


				// Currency
				CurrencyIsoCode = new RelationTypeBuilder(Base, new Guid("294a4bdc-f03a-47a2-a649-419e6b9021a3"),new Guid("f9eec7c6-c4cd-4d8c-a5f7-44855328cd7e"),new Guid("09d74027-4457-4788-803c-24b857245565"))
				.WithObjectTypes(Currency, AllorsString)
				.WithSingularName("IsoCode")  
				.WithPluralName("IsoCodes")  
				  
				  
				.WithSize(256)
				.Build();

				CurrencyName = new RelationTypeBuilder(Base, new Guid("74c8308b-1b76-4218-9532-f01c9d1e146b"),new Guid("2cb43671-c648-4bd4-ac08-7302c29246e7"),new Guid("e7c93764-d634-4187-97ed-9248ea56bab2"))
				.WithObjectTypes(Currency, AllorsString)
				.WithSingularName("Name")  
				.WithPluralName("Names")  
				  
				.WithIsIndexed(true)  
				.WithSize(256)
				.Build();

				CurrencySymbol = new RelationTypeBuilder(Base, new Guid("82797074-8d6c-4d61-a885-34ae7133a503"),new Guid("0d4524d0-503f-494d-87a4-cbc239b278e1"),new Guid("43e13383-ea7f-4aa1-872c-eec0b53a998e"))
				.WithObjectTypes(Currency, AllorsString)
				.WithSingularName("Symbol")  
				.WithPluralName("Symbols")  
				  
				  
				.WithSize(256)
				.Build();

				CurrencyLocalisedName = new RelationTypeBuilder(Base, new Guid("e9fc0472-cf7a-4e02-b061-cb42b6f5c273"),new Guid("06b8f2b2-91f0-4b89-ae19-b47de4524556"),new Guid("e1301b8f-25cc-4ace-884e-79af1d303f53"))
				.WithObjectTypes(Currency, LocalisedText)
				.WithSingularName("LocalisedName")  
				.WithPluralName("LocalisedNames")  
				.WithMultiplicity(Multiplicity.OneToMany)
				  
				.WithIsIndexed(true)  
				.Build();


				// Commentable
				CommentableComment = new RelationTypeBuilder(Base, new Guid("d800f9a2-fadd-45f1-8731-4dac177c6b1b"),new Guid("d3aadbc5-e488-4346-ac34-9e14cb8d2350"),new Guid("8b41d441-cd12-49d0-823c-b8a3163baadc"))
				.WithObjectTypes(Commentable, AllorsString)
				.WithSingularName("Comment")  
				.WithPluralName("Comments")  
				  
				  
				.WithSize(-1)
				.Build();


				// Searchable
				SearchableSearchData = new RelationTypeBuilder(Base, new Guid("5f38c771-10db-456e-ac31-6833f7087b50"),new Guid("79ac2f17-8c23-47a1-bf42-8e8bf5bd6c2b"),new Guid("2bf47342-6152-4d66-98db-54918a5c678a"))
				.WithObjectTypes(Searchable, SearchData)
				.WithSingularName("SearchData")  
				.WithPluralName("SearchDatas")  
				.WithIsDerived(true)  
				.WithIsIndexed(true)  
				.Build();



		}
	}
}