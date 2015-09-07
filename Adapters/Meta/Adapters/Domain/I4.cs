namespace Allors.Meta
{
	using System;

	#region Allors
	[Id("7a49be0e-cb91-4e1e-b113-ac67ec969935")]
	#endregion
	[Inherit(typeof(S4Interface))]
	[Inherit(typeof(S1234Interface))]

  	public partial class I4Interface: Interface
	{

		public static I4Interface Instance {get; internal set;}

		internal I4Interface() : base(MetaPopulation.Instance)
        {
			this.SingularName = "I4";
			this.PluralName = "I4s";
        }
	}
}