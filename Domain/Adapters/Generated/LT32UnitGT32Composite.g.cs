// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Domain
{
	public partial class LT32UnitGT32Composite : LT32UnitGT32CompositeAllors 
	{
		public LT32UnitGT32Composite(Allors.IStrategy allors) : base(allors) {}

		public static LT32UnitGT32Composite Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (LT32UnitGT32Composite) allorsSession.Instantiate(allorsObjectId);		
		}
	}

	[System.Diagnostics.DebuggerNonUserCode]
	public abstract class LT32UnitGT32CompositeAllors : Allors.ObjectBase
	{
		protected LT32UnitGT32CompositeAllors(Allors.IStrategy allors) : base(allors){}
	}

	public static class LT32UnitGT32CompositeMeta
	{
		public static readonly global::Allors.Meta.Class ObjectType = (Allors.Meta.Class)global::Allors.Meta.Repository.Environment.Find( new System.Guid("15ea889f-21d6-4682-aca2-c2987f592f0e") );

	}
}