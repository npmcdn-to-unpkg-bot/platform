// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	public partial interface UniquelyIdentifiableBuilder :  global::System.IDisposable
	{	

		global::System.Guid? UniqueId {get;}

		
	}

	public partial class UniquelyIdentifiables : global::Allors.ObjectsBase<UniquelyIdentifiable>
	{
		public static readonly UniquelyIdentifiableMeta Meta = UniquelyIdentifiableMeta.Instance;

		public UniquelyIdentifiables(Allors.ISession session) : base(session)
		{
		}

		public override Allors.Meta.Composite ObjectType
		{
			get
			{
				return Meta.ObjectType;
			}
		}
	}

}