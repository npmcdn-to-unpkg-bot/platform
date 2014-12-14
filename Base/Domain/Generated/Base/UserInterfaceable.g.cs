// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	public partial interface UserInterfaceable :  AccessControlledObject, Allors.IObjectBase
	{


		global::System.String DisplayName 
		{
			get;
			set;
		}

		bool ExistDisplayName{get;}

		void RemoveDisplayName();

	}

	public class UserInterfaceableMeta
	{
		public static readonly UserInterfaceableMeta Instance = new UserInterfaceableMeta();

		public global::Allors.Meta.Interface interface = global::Allors.Meta.Interfaces.UserInterfaceable;

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

	}

	public partial interface UserInterfaceableBuilder : AccessControlledObjectBuilder , global::System.IDisposable
	{	

		global::System.String DisplayName {get;}

		
	}

	public partial class UserInterfaceables : global::Allors.ObjectsBase<UserInterfaceable>
	{
		public static readonly UserInterfaceableMeta Meta = UserInterfaceableMeta.Instance;

		public UserInterfaceables(Allors.ISession session) : base(session)
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