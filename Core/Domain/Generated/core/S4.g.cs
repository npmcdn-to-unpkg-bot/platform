// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	public partial interface S4Builder :  global::System.IDisposable
	{	
	}

	public partial class S4s : global::Allors.ObjectsBase<S4>
	{
		public static readonly S4Meta Meta = S4Meta.Instance;

		public S4s(Allors.ISession session) : base(session)
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