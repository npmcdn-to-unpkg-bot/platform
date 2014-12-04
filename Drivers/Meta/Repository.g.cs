namespace Allors.Meta
{
	using System;


	public static partial class Repository
	{

 		public static Domain Core;
 		public static Domain Adapters;

		public static Unit AllorsString;
        public static Unit AllorsInteger;
        public static Unit AllorsDecimal;
        public static Unit AllorsFloat;
        public static Unit AllorsBoolean;
        public static Unit AllorsUnique;
        public static Unit AllorsBinary;


		static void Init(MetaPopulation meta)
		{
            Core = new Domain(meta, new Guid("A5F4E562-323A-41C6-893F-12D7C6A9BD76")) { Name = "Core" };
			Adapters = new Domain(meta, new Guid("0900c2a6-c90c-4d75-be21-32cc796260d1")) { Name = "Adapters" };

			// Units
			AllorsString = new UnitBuilder(Core, UnitIds.StringId).WithSingularName("AllorsString").WithPluralName("AllorsStrings").WithUnitTag(UnitTags.AllorsString).Build();
            AllorsInteger = new UnitBuilder(Core, UnitIds.IntegerId).WithSingularName("AllorsInteger").WithPluralName("AllorsIntegers").WithUnitTag(UnitTags.AllorsInteger).Build();
            AllorsDecimal = new UnitBuilder(Core, UnitIds.DecimalId).WithSingularName("AllorsDecimal").WithPluralName("AllorsDecimals").WithUnitTag(UnitTags.AllorsDecimal).Build();
            AllorsFloat = new UnitBuilder(Core, UnitIds.FloatId).WithSingularName("AllorsFloat").WithPluralName("AllorsFloats").WithUnitTag(UnitTags.AllorsFloat).Build();
            AllorsBoolean = new UnitBuilder(Core, UnitIds.BooleanId).WithSingularName("AllorsBoolean").WithPluralName("AllorsBooleans").WithUnitTag(UnitTags.AllorsBoolean).Build();
            AllorsUnique = new UnitBuilder(Core, UnitIds.Unique).WithSingularName("AllorsUnique").WithPluralName("AllorsUniques").WithUnitTag(UnitTags.AllorsUnique).Build();
            AllorsBinary = new UnitBuilder(Core, UnitIds.BinaryId).WithSingularName("AllorsBinary").WithPluralName("AllorsBinaries").WithUnitTag(UnitTags.AllorsBinary).Build();

			// Composites

			// Inheritances

			// RelationTypes
		}
	}
}