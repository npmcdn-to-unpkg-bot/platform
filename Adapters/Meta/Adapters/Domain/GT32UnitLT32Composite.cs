namespace Allors.Meta
{
	#region Allors
	[Id("7683eb7f-cbac-4947-ac29-4ef15ae47597")]
	#endregion
	public partial class GT32UnitLT32CompositeClass : Class
	{
		public static GT32UnitLT32CompositeClass Instance {get; internal set;}

		internal GT32UnitLT32CompositeClass() : base(MetaPopulation.Instance)
        {
        }
	}
}