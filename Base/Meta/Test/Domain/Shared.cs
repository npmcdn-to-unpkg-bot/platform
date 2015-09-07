namespace Allors.Meta
{
	using System;

	#region Allors
	[Id("5c3876c3-c3be-46aa-a598-a68b964d329e")]
	#endregion
	[Plural("Shareds")]
  	public partial class SharedInterface: Interface
	{

		public static SharedInterface Instance {get; internal set;}

		internal SharedInterface() : base(MetaPopulation.Instance)
        {
        }
	}
}