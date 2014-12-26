// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	public partial interface UserInterfaceable :  AccessControlledObject, Allors.IObject
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

		public global::Allors.Meta.Interface ObjectType = global::Allors.Meta.Interfaces.UserInterfaceable;

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

		public global::Allors.Meta.MethodType PrepareDerivation 
		{
			get
			{
				return global::Allors.Meta.MethodTypes.DerivablePrepareDerivation;
			}
		} 
		public global::Allors.Meta.MethodType Derive 
		{
			get
			{
				return global::Allors.Meta.MethodTypes.DerivableDerive;
			}
		} 
		public global::Allors.Meta.MethodType ApplySecurityOnDerive 
		{
			get
			{
				return global::Allors.Meta.MethodTypes.DerivableApplySecurityOnDerive;
			}
		} 
		public global::Allors.Meta.MethodType OnPostBuild 
		{
			get
			{
				return global::Allors.Meta.MethodTypes.ObjectOnPostBuild;
			}
		} 
		public global::Allors.Meta.MethodType ApplySecurityOnPostBuild 
		{
			get
			{
				return global::Allors.Meta.MethodTypes.ObjectApplySecurityOnPostBuild;
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