// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Domain
{
	public partial interface Period : Allors.IObject
	{
	}

	public static class PeriodMeta
	{
		public static readonly global::Allors.Meta.Interface ObjectType = (Allors.Meta.Interface)global::Allors.Meta.Repository.MetaPopulation.Find( new System.Guid("80adbbfd-952e-46f3-a744-78e0ce42bc80") );

		public static readonly global::Allors.Meta.RoleType PeriodFromDate = ((Allors.Meta.RelationType)global::Allors.Meta.Repository.MetaPopulation.Find( new System.Guid("5aeb31c7-03d4-4314-bbb2-fca5704b1eab"))).RoleType;
		public static readonly global::Allors.Meta.RoleType PeriodThroughDate = ((Allors.Meta.RelationType)global::Allors.Meta.Repository.MetaPopulation.Find( new System.Guid("d7576ce2-da27-487a-86aa-b0912f745bc0"))).RoleType;

	}
}