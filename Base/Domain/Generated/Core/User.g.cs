// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	public partial interface User :  SecurityTokenOwner,UserInterfaceable,Localised, Allors.IObjectBase
	{


		global::System.Boolean? UserEmailConfirmed 
		{
			get;
			set;
		}

		bool ExistUserEmailConfirmed{get;}

		void RemoveUserEmailConfirmed();


		global::System.String UserName 
		{
			get;
			set;
		}

		bool ExistUserName{get;}

		void RemoveUserName();


		global::System.String UserEmail 
		{
			get;
			set;
		}

		bool ExistUserEmail{get;}

		void RemoveUserEmail();


		global::System.String UserPasswordHash 
		{
			get;
			set;
		}

		bool ExistUserPasswordHash{get;}

		void RemoveUserPasswordHash();



		Singleton SingletonWhereGuest
		{
			get;
		}

		bool ExistSingletonWhereGuest
		{
			get;
		}


		global::Allors.Extent<UserGroup> UserGroupsWhereMember
		{ 
			get;
		}

		bool ExistUserGroupsWhereMember
		{
			get;
		}


		global::Allors.Extent<Login> LoginsWhereUser
		{ 
			get;
		}

		bool ExistLoginsWhereUser
		{
			get;
		}


		global::Allors.Extent<AccessControl> AccessControlsWhereSubject
		{ 
			get;
		}

		bool ExistAccessControlsWhereSubject
		{
			get;
		}

	}

	public class UserMeta
	{
		public static readonly UserMeta Instance = new UserMeta();

		public global::Allors.Meta.Interface ObjectType = global::Allors.Meta.Interfaces.User;

		public global::Allors.Meta.RoleType UserEmailConfirmed 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.UserUserEmailConfirmed;
			}
		} 
		public global::Allors.Meta.RoleType UserName 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.UserUserName;
			}
		} 
		public global::Allors.Meta.RoleType UserEmail 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.UserUserEmail;
			}
		} 
		public global::Allors.Meta.RoleType UserPasswordHash 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.UserUserPasswordHash;
			}
		} 
		public global::Allors.Meta.RoleType OwnerSecurityToken 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.SecurityTokenOwnerOwnerSecurityToken;
			}
		} 
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
		public global::Allors.Meta.RoleType Locale 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.LocalisedLocale;
			}
		} 

		public global::Allors.Meta.AssociationType SingletonWhereGuest 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.SingletonGuest;
			}
		} 
		public global::Allors.Meta.AssociationType UserGroupsWhereMember 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.UserGroupMember;
			}
		} 
		public global::Allors.Meta.AssociationType LoginsWhereUser 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.LoginUser;
			}
		} 
		public global::Allors.Meta.AssociationType AccessControlsWhereSubject 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.AccessControlSubject;
			}
		} 

	}
}