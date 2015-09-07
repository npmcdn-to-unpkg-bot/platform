namespace Allors.Meta
{
	#region Allors
	[Id("a53f1aed-0e3f-4c3c-9600-dc579cccf893")]
	#endregion
	[Inherit(typeof(DeletableInterface))]
	public partial class SecurityTokenClass : Class
	{
		public static SecurityTokenClass Instance {get; internal set;}

		internal SecurityTokenClass() : base(MetaPopulation.Instance)
        {
        }
	}
}