// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
    using Allors.Meta;

    public partial interface AccessControlledObject : Allors.IObject
	{


		global::Allors.Extent<Permission> DeniedPermissions
		{ 
			get;
			set;
		}

		void AddDeniedPermission (Permission value);

		void RemoveDeniedPermission (Permission value);

		bool ExistDeniedPermissions
		{
			get;
		}

		void RemoveDeniedPermissions();


		global::Allors.Extent<SecurityToken> SecurityTokens
		{ 
			get;
			set;
		}

		void AddSecurityToken (SecurityToken value);

		void RemoveSecurityToken (SecurityToken value);

		bool ExistSecurityTokens
		{
			get;
		}

		void RemoveSecurityTokens();

	}

	public class AccessControlledObjectMeta
	{
		public static readonly AccessControlledObjectMeta Instance = new AccessControlledObjectMeta();

		public global::Allors.Meta.Interface ObjectType = global::Allors.Meta.Interfaces.AccessControlledObject;

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

	public partial interface AccessControlledObjectBuilder :  global::System.IDisposable
	{	

		global::System.Collections.Generic.List<Permission> DeniedPermission {get;}		

		

		global::System.Collections.Generic.List<SecurityToken> SecurityToken {get;}		

		
	}

	public partial class AccessControlledObjects : global::Allors.ObjectsBase<AccessControlledObject>
	{
		public static readonly AccessControlledObjectMeta Meta = AccessControlledObjectMeta.Instance;

		public AccessControlledObjects(Allors.ISession session) : base(session)
		{
		}

		public override Composite ObjectType
		{
			get
			{
				return Meta.ObjectType;
			}
		}
	}

}