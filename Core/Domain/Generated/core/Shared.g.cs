// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	public partial interface SharedBuilder : UserInterfaceableBuilder , global::System.IDisposable
	{	
	}

	public partial class Shareds : global::Allors.ObjectsBase<Shared>
	{
		public static readonly SharedMeta Meta = SharedMeta.Instance;

		public Shareds(Allors.ISession session) : base(session)
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