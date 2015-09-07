namespace Allors.Meta
{
	#region Allors
	[Id("071d291d-fcc6-4511-8aa2-2d30fdeede8f")]
	#endregion
	public partial class ClassWithoutUnitRolesClass : Class
	{
		public static ClassWithoutUnitRolesClass Instance {get; internal set;}

		internal ClassWithoutUnitRolesClass() : base(MetaPopulation.Instance)
        {
        }
	}
}