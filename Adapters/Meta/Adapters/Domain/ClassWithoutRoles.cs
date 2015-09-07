namespace Allors.Meta
{
	using System;

	#region Allors
	[Id("e1008840-6d7c-4d44-b2ad-1545d23f90d8")]
	#endregion
	public partial class ClassWithoutRolesClass : Class
	{

		public static ClassWithoutRolesClass Instance {get; internal set;}

		internal ClassWithoutRolesClass() : base(MetaPopulation.Instance)
        {
			this.SingularName = "ClassWithoutRoles";
			this.PluralName = "ClassesWithoutRoles";
        }
	}
}