namespace Allors.Meta
{
	using System;

	#region Allors
	[Id("b5d151c7-0b18-4280-80d1-77b46162dba8")]
	#endregion
	[Inherit(typeof(BudgetInterface))]

	[Plural("OperatingBudgets")]
	public partial class OperatingBudgetClass : Class
	{

		public static OperatingBudgetClass Instance {get; internal set;}

		internal OperatingBudgetClass() : base(MetaPopulation.Instance)
        {
        }
	}
}