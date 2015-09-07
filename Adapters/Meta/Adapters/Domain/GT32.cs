namespace Allors.Meta
{
	#region Allors
	[Id("4f6301b3-6f0a-40c2-8267-4f8631bae706")]
	#endregion
	public partial class GT32Class : Class
	{
		public static GT32Class Instance {get; internal set;}

		internal GT32Class() : base(MetaPopulation.Instance)
        {
        }
	}
}