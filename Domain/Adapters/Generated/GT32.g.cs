// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Domain
{
	public partial class GT32 : GT32Allors 
	{
		public GT32(Allors.IStrategy allors) : base(allors) {}

		public static GT32 Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (GT32) allorsSession.Instantiate(allorsObjectId);		
		}
	}

	[System.Diagnostics.DebuggerNonUserCode]
	public abstract class GT32Allors : Allors.ObjectBase
	{
		protected GT32Allors(Allors.IStrategy allors) : base(allors){}
	}

	public static class GT32Meta
	{
		public static readonly global::Allors.Meta.Class ObjectType = (Allors.Meta.Class)global::Allors.Meta.Repository.Environment.Find( new System.Guid("4f6301b3-6f0a-40c2-8267-4f8631bae706") );

	}
}