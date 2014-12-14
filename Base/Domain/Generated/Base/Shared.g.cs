// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	public partial interface Shared :  UserInterfaceable, Allors.IObjectBase
	{


		global::Allors.Extent<Two> TwosWhereShared
		{ 
			get;
		}

		bool ExistTwosWhereShared
		{
			get;
		}

	}

	public class SharedMeta
	{
		public static readonly SharedMeta Instance = new SharedMeta();

		public global::Allors.Meta.Interface interface = global::Allors.Meta.Interfaces.Shared;

		public global::Allors.Meta.RoleType DisplayName 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.UserInterfaceableDisplayName;
			}
		} 
		public global::Allors.Meta.RoleType DeniedPermission 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.AccessControlledObjectDeniedPermission;
			}
		} 
		public global::Allors.Meta.RoleType SecurityToken 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.AccessControlledObjectSecurityToken;
			}
		} 

		public global::Allors.Meta.AssociationType TwosWhereShared 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.TwoShared;
			}
		} 

	}

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