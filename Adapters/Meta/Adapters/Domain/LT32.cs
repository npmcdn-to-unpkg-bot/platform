namespace Allors.Meta
{
	using System;

	#region Allors
	[Id("67c8d19f-1947-487c-8884-dbd76033aab0")]
	#endregion
	public partial class LT32Class : Class
	{

		public static LT32Class Instance {get; internal set;}

		internal LT32Class() : base(MetaPopulation.Instance)
        {
			this.SingularName = "LT32";
			this.PluralName = "LT32s";
        }
	}
}