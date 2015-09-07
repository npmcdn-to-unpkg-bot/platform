namespace Allors.Meta
{
	using System;

	#region Allors
	[Id("5b24107d-f5e8-499b-94f7-2bf712493546")]
	#endregion
  	public partial class S3Interface: Interface
	{

		public static S3Interface Instance {get; internal set;}

		internal S3Interface() : base(MetaPopulation.Instance)
        {
			this.SingularName = "S3";
			this.PluralName = "S3s";
        }
	}
}