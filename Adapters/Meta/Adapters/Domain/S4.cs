namespace Allors.Meta
{
	#region Allors
	[Id("5b348bcb-823d-4cbe-b3ac-a18b1cd96581")]
	#endregion
  	public partial class S4Interface: Interface
	{
		public static S4Interface Instance {get; internal set;}

		internal S4Interface() : base(MetaPopulation.Instance)
        {
        }
	}
}